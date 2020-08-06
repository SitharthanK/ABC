namespace DressDetails.Forms
{
    using DressDetails.Helper;
    using DressDetails.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="ProfileForm" />.
    /// </summary>
    public partial class ProfileForm : Form
    {
        /// <summary>
        /// Defines the _timer.
        /// </summary>
        internal Timer _timer;
        private bool blnDataGridCreations;

        /// <summary>
        /// Defines the _userName.
        /// </summary>
        private readonly string _userName;

        /// <summary>
        /// Defines the userId, isAdmin.....
        /// </summary>
        public readonly int userId, isAdmin;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileForm"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="admin">The admin<see cref="int"/>.</param>
        public ProfileForm(int id, string name, int admin)
        {
            InitializeComponent();           
            IsAdminListDetails();
            IsActiveDetails();
            StartTimer();
            blnDataGridCreations = false;
            DataGridCreation();

            userId = id; _userName = name; isAdmin = admin;
            lblLoginName.Text = @"Welcome.: " + _userName;

            if (isAdmin == 1)
                AddMenuAndItems();

            DataTable dataTable = SqlHelper.LoadProfileDetails();
            LoadGridDetails(dataTable);
        }

        /// <summary>
        /// The AddMenuAndItems.
        /// </summary>
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

        /// <summary>
        /// The Create_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void Create_Click(object sender, EventArgs e)
        {
            CreateForm obj = new CreateForm(userId, _userName, isAdmin);
            Hide();
            obj.ShowDialog();
        }

        /// <summary>
        /// The Update_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void Update_Click(object sender, EventArgs e)
        {
            EditForm obj = new EditForm(userId, _userName, isAdmin);
            Hide();
            obj.ShowDialog();
        }

        /// <summary>
        /// The ExitApplication_click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void ExitApplication_click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// The StartTimer.
        /// </summary>
        private void StartTimer()
        {
            _timer = new Timer { Interval = 1000 };
            _timer.Tick += tmr_Tick;
            _timer.Enabled = true;
        }

        /// <summary>
        /// The tmr_Tick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        internal void tmr_Tick(object sender, EventArgs e)
        {
            lblDateValue.Text = DateTime.Now.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Defines the ActiveList.
        /// </summary>
        private static readonly Dictionary<int, string> ActiveList = new Dictionary<int, string>()
        {
            {0, "Inactive"},
            {1, "Active"},
        };

        /// <summary>
        /// The IsActiveDetails.
        /// </summary>
        private void IsActiveDetails()
        {
            cmbIsActive.DataSource = new BindingSource(ActiveList, null);
            cmbIsActive.DisplayMember = "Value";
            cmbIsActive.ValueMember = "Key";
            cmbIsActive.SelectedValue = 1;
        }

        /// <summary>
        /// Defines the AdminList.
        /// </summary>
        private static readonly Dictionary<int, string> AdminList = new Dictionary<int, string>()
        {
            {0, "No"},
            {1, "Yes"},

        };

        /// <summary>
        /// The IsAdminListDetails.
        /// </summary>
        private void IsAdminListDetails()
        {
            cmbIsAdmin.DataSource = new BindingSource(AdminList, null);
            cmbIsAdmin.DisplayMember = "Value";
            cmbIsAdmin.ValueMember = "Key";
            cmbIsAdmin.SelectedValue = 0;
        }

        /// <summary>
        /// The btnCreate_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
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
                try
                {
                    LoginDetails loginDetails = AssignValues();
                    if (SqlHelper.AddProfile(loginDetails))
                    {
                        MessageBox.Show(@"User Added Sucessfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadGridDetails(SqlHelper.LoadProfileDetails());
                    }
                    else
                    {
                        MessageBox.Show(@"Adding User Failed", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// The AssignValues.
        /// </summary>
        /// <returns>The <see cref="LoginDetails"/>.</returns>
        private LoginDetails AssignValues()
        {
            LoginDetails loginDetails = new LoginDetails();
            loginDetails.Name = txtName.Text;
            loginDetails.Address = txtAddress.Text;
            loginDetails.UserName = txtUsername.Text;
            loginDetails.Password = txtPassword.Text;
            loginDetails.Id = (!string.IsNullOrWhiteSpace(txtID.Text) ? Convert.ToInt32(txtID.Text) : 0);
            loginDetails.ContactNumber = txtMobileNumber.Text;
            loginDetails.IsActiveUser = Convert.ToBoolean(cmbIsActive.SelectedValue);
            loginDetails.IsAdminUser = Convert.ToBoolean(cmbIsAdmin.SelectedValue);
            loginDetails.UserId = Convert.ToString(userId);
            return loginDetails;
        }

        /// <summary>
        /// The LoadGridDetails.
        /// </summary>
        /// <param name="dt">The dt<see cref="DataTable"/>.</param>
        private void LoadGridDetails(DataTable dt)
        {
            DataGridCreation();
            dgLogindetails.Rows.Clear();
            if (dt?.Rows?.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dgLogindetails.Rows.Add(dr["Id"].ToString(), dr["Name"].ToString(), dr["Address"].ToString(), dr["UserName"].ToString(), 
                        dr["ContactNumber"].ToString(),(Convert.ToBoolean(dr["ISAdmin"]) ? "Yes" : "No"),(Convert.ToBoolean(dr["IsActive"]) ? "Yes" : "No"));
                }
                dgLogindetails.Refresh();
            }
            dgLogindetails.Refresh();
        }

        /// <summary>
        /// The DataGridCreation.
        /// </summary>
        private void DataGridCreation()
        {
            if (!blnDataGridCreations)
            {
                DataGridTextBoxColumn serlNo = new DataGridTextBoxColumn();
                dgLogindetails.Columns.Add("Id", "Id.");
                serlNo.TextBox.Name = "Id";
                serlNo.Alignment = HorizontalAlignment.Center;

                DataGridTextBoxColumn devoteename = new DataGridTextBoxColumn();
                dgLogindetails.Columns.Add("Name", "Name");
                devoteename.TextBox.Name = "Name";
                devoteename.TextBox.CharacterCasing = CharacterCasing.Upper;
                devoteename.Alignment = HorizontalAlignment.Left;

                DataGridTextBoxColumn address = new DataGridTextBoxColumn();
                dgLogindetails.Columns.Add("Address", "Address");
                address.TextBox.Name = "Address";
                address.Alignment = HorizontalAlignment.Left;

                DataGridTextBoxColumn UserName = new DataGridTextBoxColumn();
                dgLogindetails.Columns.Add("UserName", "User Name");
                UserName.TextBox.Name = "UserName";
                UserName.Alignment = HorizontalAlignment.Left;

                DataGridTextBoxColumn contactNumber = new DataGridTextBoxColumn();
                dgLogindetails.Columns.Add("ContactNumber", "Contact Number");
                contactNumber.TextBox.Name = "ContactNumber";
                contactNumber.Alignment = HorizontalAlignment.Right;

                DataGridTextBoxColumn ISAdmin = new DataGridTextBoxColumn();
                dgLogindetails.Columns.Add("ISAdmin", "Is Admin User");
                ISAdmin.TextBox.Name = "ISAdmin";
                ISAdmin.Alignment = HorizontalAlignment.Left;

                DataGridTextBoxColumn IsActive = new DataGridTextBoxColumn();
                dgLogindetails.Columns.Add("IsActive", "Is Active User");
                IsActive.TextBox.Name = "IsActive";
                IsActive.Alignment = HorizontalAlignment.Left;

                dgLogindetails.Columns[0].Width = 50;
                dgLogindetails.Columns[1].Width = 120;
                dgLogindetails.Columns[2].Width = 100;
                dgLogindetails.Columns[3].Width = 140;
                dgLogindetails.Columns[4].Width = 250;
                dgLogindetails.Columns[5].Width = 150;
                dgLogindetails.Columns[6].Width = 150;

                dgLogindetails.Columns["Id"].ReadOnly = true;
                dgLogindetails.Columns["Name"].ReadOnly = true;
                dgLogindetails.Columns["Address"].ReadOnly = true;
                dgLogindetails.Columns["UserName"].ReadOnly = true;
                dgLogindetails.Columns["ContactNumber"].ReadOnly = true;
                dgLogindetails.Columns["ISAdmin"].ReadOnly = true;
                dgLogindetails.Columns["IsActive"].ReadOnly = true;
                blnDataGridCreations = true;
            }
            dgLogindetails.Rows.Clear();
            dgLogindetails.Refresh();
        }

        /// <summary>
        /// The btnUpdate_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
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
                try
                {
                    LoginDetails loginDetails = AssignValues();
                    if (SqlHelper.UpdateProfile(loginDetails))
                    {
                        MessageBox.Show(@"Updated Sucessfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadGridDetails(SqlHelper.LoadProfileDetails());
                    }
                    else
                    {
                        MessageBox.Show(@"Updation Failed", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// The btnClear_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
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

        /// <summary>
        /// The btnEditDetails_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void btnEditDetails_Click(object sender, EventArgs e)
        {
            try
            {
                txtID.Enabled = false;
                txtName.Enabled = false;
                txtUsername.Enabled = false;

                if (!string.IsNullOrWhiteSpace(txtID.Text))
                {
                    LoginDetails loginDetails = SqlHelper.GetProfileDetailsById(Convert.ToInt32(txtID.Text));
                    txtName.Text = loginDetails.Name;
                    txtAddress.Text = loginDetails.Address;
                    txtMobileNumber.Text = loginDetails.ContactNumber;
                    txtUsername.Text = loginDetails.UserName;
                    txtPassword.Text = loginDetails.Password;
                    cmbIsAdmin.SelectedValue = (loginDetails.IsAdmin == "Yes") ? 1 : 2;
                    cmbIsActive.SelectedValue = (loginDetails.IsActive == "Yes") ? 1 : 2;
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
    }
}
