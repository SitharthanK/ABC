namespace ANNABABA.Forms
{
    using ANNABABA.Helpers;
    using ANNABABA.Models;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Drawing;
    using System.Linq;
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
            InitializeDataGridView();
            
            dtEffectiveDate.MinDate = DateTime.Now.Date;
            dtEffectiveDate.MaxDate = DateTime.Now.AddYears(1);
            dtEffectiveDate.Value = DateTime.Now.Date;
            
            nmcReceipts.Value = 5;
            nmcMonths.Value = 4;
            
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

                foreach (ConfigurationDetails config in lstConfiguration)
                {
                    string[] row = new string[]
                    {
                        config.effectiveDate.ToString("dd-MMM-yyyy").ToUpper(),
                        config.TotalNoOfReceipts.ToString(),
                        config.NoOfMonths.ToString()
                    };
                    dgvConfigurationDetails.Rows.Add(row);                    
                }

                dgvConfigurationDetails.Enabled = false;              
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
            else if (lstConfiguration.Any(n => n.effectiveDate.Date == dtEffectiveDate.Value.Date))
            {
                MessageBox.Show("Record already exists in out database. Please choose another date !..", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void InitializeDataGridView()
        {
            // Initialize basic DataGridView properties.
            dgvConfigurationDetails.Dock = DockStyle.Fill;
            dgvConfigurationDetails.BackgroundColor = Color.White;

            // Set property values appropriate for read-only display and 
            // limited interactivity. 
            dgvConfigurationDetails.AllowUserToAddRows = false;
            dgvConfigurationDetails.AllowUserToDeleteRows = false;
            dgvConfigurationDetails.AllowUserToOrderColumns = true;
            dgvConfigurationDetails.ReadOnly = true;
            dgvConfigurationDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvConfigurationDetails.MultiSelect = false;
            dgvConfigurationDetails.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvConfigurationDetails.AllowUserToResizeColumns = false;
            dgvConfigurationDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvConfigurationDetails.AllowUserToResizeRows = false;
            dgvConfigurationDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                   
            // Set the background color for all rows and for alternating rows. 
            // The value for alternating rows overrides the value for all rows. 
            dgvConfigurationDetails.RowsDefaultCellStyle.BackColor = Color.LightYellow;
            dgvConfigurationDetails.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSkyBlue;

            // Set the row and column header styles.
            dgvConfigurationDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvConfigurationDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgvConfigurationDetails.RowHeadersDefaultCellStyle.BackColor = Color.Maroon;

            dgvConfigurationDetails.Rows.Clear();
            dgvConfigurationDetails.ColumnCount = 3;
            dgvConfigurationDetails.Columns[0].Name = "Effective Date";
            dgvConfigurationDetails.Columns[0].Width = 160;
            dgvConfigurationDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgvConfigurationDetails.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvConfigurationDetails.Columns[0].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvConfigurationDetails.Columns[1].Name = "Total No.Of Receipts / Day";
            dgvConfigurationDetails.Columns[1].Width = 160;
            dgvConfigurationDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgvConfigurationDetails.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvConfigurationDetails.Columns[1].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvConfigurationDetails.Columns[2].Name = "No Of Months Booking In Advance";
            dgvConfigurationDetails.Columns[2].Width = 290;
            dgvConfigurationDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgvConfigurationDetails.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvConfigurationDetails.Columns[2].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
    }
}
