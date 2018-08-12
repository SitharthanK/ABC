using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace DressDetails.Forms
{
    public partial class ProfileForm : Form
    {
        Timer _timer;
        public SqlConnection Con { get; private set; }
        private readonly string _userName;
        public readonly int userId, isAdmin;
        public ProfileForm(int id, string name, int admin)
        {
            InitializeComponent();
            LoadGridDetails();
            IsAdminListDetails();
            IsActiveDetails();
            StartTimer();

            userId = id; _userName = name; isAdmin = admin;
            lblLoginName.Text = @"Welcome.: " + _userName;

            if (isAdmin == 1)
                AddMenuAndItems();
        }
        #region MENU DETAILS

        private void AddMenuAndItems()
        {
            Menu = new MainMenu();
            MenuItem oFile = new MenuItem("File");
            MenuItem oCreate = new MenuItem("Create");
            MenuItem oEdit = new MenuItem("Edit");

            /*FILE MENU ITEMS*/
            Menu.MenuItems.Add(oFile);
            oFile.MenuItems.Add("Exit", ExitApplication_click);

            /*CREATE MENU ITEMS*/
            Menu.MenuItems.Add(oCreate);
            oCreate.MenuItems.Add("Create", Create_Click);

            /*EDIT MENU ITEMS*/
            Menu.MenuItems.Add(oEdit);
            oEdit.MenuItems.Add("Edit", Update_Click);
        }
        private void Create_Click(object sender, EventArgs e)
        {
            CreateForm obj = new CreateForm(userId, _userName, isAdmin);
            Hide();
            obj.ShowDialog();
        }
        private void Update_Click(object sender, EventArgs e)
        {
            EditForm obj = new EditForm(userId, _userName, isAdmin);
            Hide();
            obj.ShowDialog();
        }

        private void ExitApplication_click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region TIMER VALIDATIONS

        private void StartTimer()
        {
            _timer = new Timer { Interval = 1000 };
            _timer.Tick += tmr_Tick;
            _timer.Enabled = true;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            lblDateValue.Text = DateTime.Now.ToString(CultureInfo.CurrentCulture);
        }
        #endregion

        #region ACTIVE & ADMIN MODE

        private static readonly Dictionary<int, string> ActiveList = new Dictionary<int, string>()
        {
            {0, "Inactive"},
            {1, "Active"},
        };

        private void IsActiveDetails()
        {
            cmbIsActive.DataSource = new BindingSource(ActiveList, null);
            cmbIsActive.DisplayMember = "Value";
            cmbIsActive.ValueMember = "Key";
            cmbIsActive.SelectedValue = 1;
        }

        private static readonly Dictionary<int, string> AdminList = new Dictionary<int, string>()
        {
            {0, "No"},
            {1, "Yes"},
            
        };

        private void IsAdminListDetails()
        {
            cmbIsAdmin.DataSource = new BindingSource(AdminList, null);
            cmbIsAdmin.DisplayMember = "Value";
            cmbIsAdmin.ValueMember = "Key";
            cmbIsAdmin.SelectedValue = 0;
        }

        #endregion

        #region LOAD GRID DETAILS

        private void LoadGridDetails()
        {
            try
            {
                string conn = ConfigurationManager.ConnectionStrings["Conn"].ToString();
                Con = new SqlConnection(conn);
                Con.Open();

                string strQuery = @"SELECT Id,Name,Address,UserName,ContactNumber,ISAdmin,ISActive FROM LOGINDETAILS";
                SqlCommand cm = new SqlCommand(strQuery, Con)
                {
                    CommandText = strQuery,
                    CommandType = CommandType.Text,
                    Connection = Con
                };
                var dataReader = cm.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                if (dataTable.Rows.Count > 0)
                {
                    var table = (from DataRow dr in dataTable.Rows
                                 select new LoginDetails()
                                 {
                                     Id = Convert.ToInt32(dr["Id"]),
                                     Name = Convert.ToString(dr["Name"]),
                                     Address = Convert.ToString(dr["Address"]),
                                     UserName = Convert.ToString(dr["UserName"]),
                                     ContactNumber = Convert.ToString(dr["ContactNumber"]),
                                     IsAdmin = Convert.ToBoolean(dr["ISAdmin"]) ? "Yes" : "No",
                                     IsActive = Convert.ToBoolean(dr["IsActive"]) ? "Yes" : "No"
                                 });
                    dgLogindetails.DataSource = table.ToList();
                }
                else
                    dgLogindetails.DataSource = dataTable;
                Con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Button Click Events

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show(@"Provide Name.", Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show(@"Provide Address.", Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show(@"Provide User name.", Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(@"Provide Password.", Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (string.IsNullOrWhiteSpace(txtMobileNumber.Text))
            {
                MessageBox.Show(@"Provide Contact number.", Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                string strInsertQuery = "INSERT INTO LOGINDETAILS(NAME,ADDRESS,USERNAME,PASSWORD,CONTACTNUMBER,ISADMIN,ISACTIVE,INSERTEDBY)VALUES(@NAME,@ADDRESS,@USERNAME,@PASSWORD,@CONTACTNUMBER,@ISADMIN,@ISACTIVE,@INSERTEDBY)";
                string conn = ConfigurationManager.ConnectionStrings["Conn"].ToString();
                Con = new SqlConnection(conn);
                Con.Open();

                SqlCommand cm = new SqlCommand(strInsertQuery, Con);
                cm.Parameters.AddWithValue("@NAME", txtName.Text);
                cm.Parameters.AddWithValue("@ADDRESS", txtAddress.Text);
                cm.Parameters.AddWithValue("@USERNAME", txtUsername.Text);
                cm.Parameters.AddWithValue("@PASSWORD", txtPassword.Text);
                cm.Parameters.AddWithValue("@CONTACTNUMBER", txtMobileNumber.Text);
                cm.Parameters.AddWithValue("@ISADMIN", cmbIsAdmin.SelectedValue);
                cm.Parameters.AddWithValue("@ISACTIVE", cmbIsActive.SelectedValue);
                cm.Parameters.AddWithValue("@INSERTEDBY", userId.ToString());
                try
                {
                    int intAffectedRow = cm.ExecuteNonQuery();
                    if (intAffectedRow > 0)
                    {
                        MessageBox.Show(@"User Added Sucessfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadGridDetails();
                    }
                    else
                    {
                        MessageBox.Show(@"Adding User Failed", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show(@"Provide Name.", Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show(@"Provide Address.", Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show(@"Provide User name.", Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(@"Provide Password.", Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (string.IsNullOrWhiteSpace(txtMobileNumber.Text))
            {
                MessageBox.Show(@"Provide Contact number.", Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                var strUpdateQuery = "UPDATE LoginDETAILS Set ADDRESS='" + txtAddress.Text + "',Password='" +
                                     txtPassword.Text + "',ContactNumber='" + txtMobileNumber.Text +
                                     "',IsActive=" + cmbIsActive.SelectedValue + ",IsAdmin=" +
                                     cmbIsAdmin.SelectedValue +
                                     "WHERE ID='" + txtID.Text + "'";
                string conn = ConfigurationManager.ConnectionStrings["Conn"].ToString();
                Con = new SqlConnection(conn);
                Con.Open();

                var cm = new SqlCommand(strUpdateQuery, Con);
                try
                {
                    var intAffectedRow = cm.ExecuteNonQuery();
                    if (intAffectedRow > 0)
                    {
                        MessageBox.Show(@"Updated Sucessfully", Application.ProductName, MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        LoadGridDetails();
                    }
                    else
                    {
                        MessageBox.Show(@"Updation Failed", Application.ProductName, MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }

                    Con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtMobileNumber.Text = "";

            cmbIsAdmin.SelectedValue = 0;
            cmbIsActive.SelectedValue = 1;

            txtID.Enabled = true;
            txtName.Enabled = true;
            txtUsername.Enabled = true;
        }

        private void btnEditDetails_Click(object sender, EventArgs e)
        {
            try
            {
                txtID.Enabled = false;
                txtName.Enabled = false;
                txtUsername.Enabled = false;

                if (!string.IsNullOrWhiteSpace(txtID.Text))
                {
                    string conn = ConfigurationManager.ConnectionStrings["Conn"].ToString();
                    Con = new SqlConnection(conn);
                    Con.Open();

                    string strQuery =  @"SELECT Id,Name,Address,UserName,Password,ContactNumber,ISAdmin,ISActive FROM LOGINDETAILS Where ID=" + txtID.Text;
                    SqlCommand cm = new SqlCommand(strQuery, Con)
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.Text,
                        Connection = Con
                    };
                    var dataReader = cm.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    if (dataTable.Rows.Count > 0)
                    {
                        var row = (from DataRow dr in dataTable.Rows
                                   select new LoginDetails()
                                   {
                                       Id = Convert.ToInt32(dr["Id"]),
                                       Name = Convert.ToString(dr["Name"]),
                                       Address = Convert.ToString(dr["Address"]),
                                       UserName = Convert.ToString(dr["UserName"]),
                                       ContactNumber = Convert.ToString(dr["ContactNumber"]),
                                       Password = Convert.ToString(dr["Password"]),
                                       IsAdmin = Convert.ToBoolean(dr["ISAdmin"]) ? "Yes" : "No",
                                       IsActive = Convert.ToBoolean(dr["IsActive"]) ? "Yes" : "No"
                                   }).FirstOrDefault();
                        txtName.Text = row.Name;
                        txtAddress.Text = row.Address;
                        txtMobileNumber.Text = row.ContactNumber;
                        txtUsername.Text = row.UserName;
                        txtPassword.Text = row.Password;
                        cmbIsAdmin.SelectedValue = (row.IsAdmin == "Yes") ? 1 : 2;
                        cmbIsActive.SelectedValue = (row.IsAdmin == "Yes") ? 1 : 2;
                    }
                    else
                        MessageBox.Show(@"Enter Valid ID!...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Con.Close();
                }
                else
                {
                    MessageBox.Show(@"Enter Valid ID!...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
        #endregion

        private class LoginDetails
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string UserName { get; set; }
            public string ContactNumber { get; set; }
            public string IsAdmin { get; set; }
            public string IsActive { get; set; }
            public string Password { get; set; }
        }
    }
}
