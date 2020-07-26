namespace ANNABABA.Forms
{
    using ANNABABA.Helpers;
    using ANNABABA.Models;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="Configure" />.
    /// </summary>
    public partial class Configure : Form
    {
        /// <summary>
        /// Gets or sets the configurationDetails.
        /// </summary>
        private ConfigurationDetails configurationDetails { get; set; }

        /// <summary>
        /// Gets or sets the lstConfiguration.
        /// </summary>
        private static List<ConfigurationDetails> lstConfiguration { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Configure"/> class.
        /// </summary>
        public Configure()
        {
            InitializeComponent();
            dtEffectiveDate.MinDate = DateTime.Now.Date;
            dtEffectiveDate.MaxDate = DateTime.Now.AddYears(1);
            dtEffectiveDate.Value = DateTime.Now.Date;
            nmcReceipts.Value = 5;
            nmcMonths.Value = 4;
            txtPassword.Text = Convert.ToString(ConfigurationManager.AppSettings["Password"]);

            LoadConfigurationDetails();
        }

        /// <summary>
        /// The LoadConfigurationDetails.
        /// </summary>
        private void LoadConfigurationDetails()
        {
            lstConfiguration = SqlHelper.GetConfigurationDetails();

            if (lstConfiguration?.Count > 0)
            {
                dgvConfigurationDetails.Rows.Clear();
                dgvConfigurationDetails.ColumnCount = 3;
                dgvConfigurationDetails.Columns[0].Name = "Effective Date";
                dgvConfigurationDetails.Columns[0].Width = 200;

                dgvConfigurationDetails.Columns[1].Name = "Total No.of receipts";
                dgvConfigurationDetails.Columns[1].Width = 200;

                dgvConfigurationDetails.Columns[2].Name = "No.of months booking in advance";
                dgvConfigurationDetails.Columns[2].Width = 250;
                                
                foreach (ConfigurationDetails config in lstConfiguration)
                {
                    string[] row = new string[]
                    {
                        config.effectiveDate.ToString("dd-MMM-yyyy"),
                        config.TotalNoOfReceipts.ToString(),
                        config.NoOfMonths.ToString()
                    };
                    dgvConfigurationDetails.Rows.Add(row);
                }

                dgvConfigurationDetails.Enabled = false;
                dgvConfigurationDetails.ForeColor = Color.Black;
                dgvConfigurationDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvConfigurationDetails.Refresh();
            }
            else
            {
                MessageBox.Show("No Records Found...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// The btnConfigure_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void btnConfigure_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            DateTime effectivedate = new DateTime(dtEffectiveDate.Value.Year, dtEffectiveDate.Value.Month, dtEffectiveDate.Value.Day, 0, 0, 0);

            configurationDetails = new ConfigurationDetails
            {
                effectiveDate = effectivedate,
                TotalNoOfReceipts = Convert.ToInt32(nmcReceipts.Value),
                NoOfMonths = Convert.ToInt32(nmcMonths.Value)
            };

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter valid password...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!string.IsNullOrWhiteSpace(password) && password != Convert.ToString(ConfigurationManager.AppSettings["Password"]))
            {
                MessageBox.Show("Please enter valid password...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (configurationDetails.TotalNoOfReceipts < 5)
            {
                MessageBox.Show("Total No.of Receipts should be greater than 5.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (configurationDetails.NoOfMonths < 3)
            {
                MessageBox.Show("Min No.of Months for booking annadhanam should be greater than or equal to 3.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlHelper.AddConfiguration(configurationDetails);

                dtEffectiveDate.MinDate = DateTime.Now.Date;
                dtEffectiveDate.MaxDate = DateTime.Now.AddYears(1);
                dtEffectiveDate.Value = DateTime.Now.Date;

                nmcReceipts.Value = 5;
                nmcMonths.Value = 3;

                LoadConfigurationDetails();
            }
        }
    }
}
