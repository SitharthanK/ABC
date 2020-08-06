namespace DressDetails.Forms
{
    using DressDetails.Helper;
    using DressDetails.Models;
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="LoginForm" />.
    /// </summary>
    public partial class LoginForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginForm"/> class.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            rbtnUser.Checked = true;
        }

        /// <summary>
        /// The btnLogin_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtUserName.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                   LoginDetails loginDetails= SqlHelper.Login(txtUserName.Text, txtPassword.Text, rbtnAdmin.Checked);

                    if (loginDetails!=null)
                    {

                        CreateForm objCreate = new CreateForm(loginDetails.Id, loginDetails.Name, (loginDetails.IsAdminUser == true ? 1 : 0));
                        this.Hide();
                        objCreate.Show();
                    }
                    else
                        MessageBox.Show(@"Incorrect UserName or Password", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                    MessageBox.Show(@"Incorrect UserName or Password", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The btnClear_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }
    }
}
