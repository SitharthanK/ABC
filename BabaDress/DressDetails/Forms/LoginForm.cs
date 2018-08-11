using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace DressDetails.Forms
{
    public partial class LoginForm : Form
    {
        SqlConnection _con;
        public LoginForm()
        {
            InitializeComponent();
            rbtnUser.Checked = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtUserName.Text))
                {
                    string strQuery;
                    string conn = ConfigurationManager.ConnectionStrings["Conn"].ToString();
                    _con = new SqlConnection(conn);
                    _con.Open();
                    if (rbtnAdmin.Checked)
                        strQuery = @"SELECT ID,Name,IsAdmin FROM  LOGINDETAILS WHERE UserName='" + txtUserName.Text + "' AND Password='" + txtPassword.Text + "' AND IsActive=1 AND IsAdmin=1";
                    else
                        strQuery = @"SELECT ID,Name,IsAdmin FROM  LOGINDETAILS WHERE UserName='" + txtUserName.Text + "' AND IsActive=1 AND IsAdmin=0";

                    SqlCommand cm = new SqlCommand(strQuery, _con)
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.Text,
                        Connection = _con
                    };
                    var dataReader = cm.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    if (dataTable.Rows.Count > 0)
                    {
                        var row = (from DataRow dr in dataTable.Rows
                                   select new LoginDetails()
                                   {
                                       Id = Convert.ToInt32(dr["ID"]),
                                       Name = Convert.ToString(dr["Name"]),
                                       IsAdmin = Convert.ToInt32(dr["IsAdmin"]),
                                   }).FirstOrDefault();

                        CreateForm objCreate = new CreateForm(row.Id, row.Name, row.IsAdmin);
                        Hide();
                        objCreate.Show();
                    }
                    else
                        MessageBox.Show(@"Incorrect UserName or Password", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }
    }

    internal class LoginDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IsAdmin { get; set; }
    }
}