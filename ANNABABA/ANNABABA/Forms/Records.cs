namespace ANNABABA.Forms
{
    using ANNABABA.Helpers;
    using ANNABABA.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="Records" />.
    /// </summary>
    public partial class Records : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Records"/> class.
        /// </summary>
        public Records()
        {
            InitializeComponent();
            InitializeDataGridView();
            rbtnDay.Checked = true;


            dtPeriod.MinDate = Convert.ToDateTime("1-JAN-2011");
            dtPeriod.MaxDate = DateTime.Now.AddMonths(4);
            dtPeriod.Value = DateTime.Now;
        }

        /// <summary>
        /// The btnGetRecords_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void btnGetRecords_Click(object sender, EventArgs e)
        {

            DateTime periodFrom = new DateTime(dtPeriod.Value.Year, dtPeriod.Value.Month, dtPeriod.Value.Day, 0, 0, 0);
            DateTime periodTo = new DateTime(dtPeriod.Value.Year, dtPeriod.Value.Month, dtPeriod.Value.Day, 23, 59, 59);

            if (rbtnDay.Checked)
            {
                periodFrom = new DateTime(dtPeriod.Value.Year, dtPeriod.Value.Month, dtPeriod.Value.Day, 0, 0, 0);
                periodTo = new DateTime(dtPeriod.Value.Year, dtPeriod.Value.Month, dtPeriod.Value.Day, 23, 59, 59);
            }
            if (rbtnWeek.Checked)
            {
                periodFrom = new DateTime(dtPeriod.Value.Year, dtPeriod.Value.Month, dtPeriod.Value.Day, 0, 0, 0);
                periodTo = new DateTime(dtPeriod.Value.AddDays(7).Year, dtPeriod.Value.AddDays(7).Month, dtPeriod.Value.AddDays(7).Day, 23, 59, 59);
            }
            if (rbtnMonth.Checked)
            {                 
                periodFrom = new DateTime(dtPeriod.Value.Year, dtPeriod.Value.Month, 1, 0, 0, 0);
                periodTo = new DateTime(dtPeriod.Value.Year, dtPeriod.Value.Month, DateTime.DaysInMonth(periodFrom.Year, periodFrom.Month), 23, 59, 59) ;
            }
            if (rbtnYear.Checked)
            {
                periodFrom = new DateTime(dtPeriod.Value.Year,1, 1, 0, 0, 0);
                periodTo = new DateTime(dtPeriod.Value.AddYears(1).Year,12, 31, 23, 59, 59);
            }

            List<Devotee> lstDevotee = SqlHelper.GetDevoteeDetailsByDate(periodFrom, periodTo);
            LoadDevoteeDetails(lstDevotee);
        }

        /// <summary>
        /// The InitializeDataGridView.
        /// </summary>
        private void InitializeDataGridView()
        {
            // Initialize basic DataGridView properties.
            dgDevoteeDetails.Dock = DockStyle.Fill;
            dgDevoteeDetails.BackgroundColor = Color.White;

            // Set property values appropriate for read-only display and 
            // limited interactivity. 
            dgDevoteeDetails.AllowUserToAddRows = false;
            dgDevoteeDetails.AllowUserToDeleteRows = false;
            dgDevoteeDetails.AllowUserToOrderColumns = true;
            dgDevoteeDetails.ReadOnly = true;
            dgDevoteeDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgDevoteeDetails.MultiSelect = false;
            dgDevoteeDetails.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgDevoteeDetails.AllowUserToResizeColumns = false;
            dgDevoteeDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgDevoteeDetails.AllowUserToResizeRows = false;
            dgDevoteeDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;


            // Set the background color for all rows and for alternating rows. 
            // The value for alternating rows overrides the value for all rows. 
            dgDevoteeDetails.RowsDefaultCellStyle.BackColor = Color.LightYellow;
            dgDevoteeDetails.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSkyBlue;

            // Set the row and column header styles.
            dgDevoteeDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgDevoteeDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgDevoteeDetails.RowHeadersDefaultCellStyle.BackColor = Color.Maroon;

            dgDevoteeDetails.Rows.Clear();
            dgDevoteeDetails.ColumnCount = 13;
            dgDevoteeDetails.Columns[0].Name = "Receipt Number";
            dgDevoteeDetails.Columns[0].Width = 80;
            dgDevoteeDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgDevoteeDetails.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgDevoteeDetails.Columns[0].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgDevoteeDetails.Columns[1].Name = "Name";
            dgDevoteeDetails.Columns[1].Width = 150;
            dgDevoteeDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgDevoteeDetails.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgDevoteeDetails.Columns[1].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgDevoteeDetails.Columns[2].Name = "Address";
            dgDevoteeDetails.Columns[2].Width = 200;
            dgDevoteeDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgDevoteeDetails.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgDevoteeDetails.Columns[2].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgDevoteeDetails.Columns[3].Name = "Country";
            dgDevoteeDetails.Columns[3].Width = 100;
            dgDevoteeDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgDevoteeDetails.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgDevoteeDetails.Columns[3].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgDevoteeDetails.Columns[4].Name = "State";
            dgDevoteeDetails.Columns[4].Width = 100;
            dgDevoteeDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgDevoteeDetails.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgDevoteeDetails.Columns[4].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgDevoteeDetails.Columns[5].Name = "City";
            dgDevoteeDetails.Columns[5].Width = 100;
            dgDevoteeDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgDevoteeDetails.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgDevoteeDetails.Columns[5].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgDevoteeDetails.Columns[6].Name = "Annadhanam Date";
            dgDevoteeDetails.Columns[6].Width = 80;
            dgDevoteeDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgDevoteeDetails.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgDevoteeDetails.Columns[6].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgDevoteeDetails.Columns[7].Name = "Amount";
            dgDevoteeDetails.Columns[7].Width = 80;
            dgDevoteeDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgDevoteeDetails.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgDevoteeDetails.Columns[7].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgDevoteeDetails.Columns[8].Name = "Payment Mode";
            dgDevoteeDetails.Columns[8].Width = 80;
            dgDevoteeDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgDevoteeDetails.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgDevoteeDetails.Columns[8].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgDevoteeDetails.Columns[9].Name = "Cheque Number";
            dgDevoteeDetails.Columns[9].Width = 120;
            dgDevoteeDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgDevoteeDetails.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgDevoteeDetails.Columns[9].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgDevoteeDetails.Columns[10].Name = "Cheque Date";
            dgDevoteeDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgDevoteeDetails.Columns[10].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgDevoteeDetails.Columns[10].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgDevoteeDetails.Columns[11].Name = "Cheque Drawn";
            dgDevoteeDetails.Columns[11].Width = 120;
            dgDevoteeDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgDevoteeDetails.Columns[11].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgDevoteeDetails.Columns[11].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgDevoteeDetails.Columns[12].Name = "Receipt Created Date";
            dgDevoteeDetails.Columns[0].HeaderCell.Style.ForeColor = Color.Black;
            dgDevoteeDetails.Columns[12].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgDevoteeDetails.Columns[12].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        /// <summary>
        /// The LoadDevoteeDetails.
        /// </summary>
        /// <param name="lstDevotee">The lstDevotee<see cref="List{Devotee}"/>.</param>
        private void LoadDevoteeDetails(List<Devotee> lstDevotee)
        {

            if (lstDevotee?.Count > 0)
            {
                dgDevoteeDetails.Rows.Clear();

                foreach (Devotee devotee in lstDevotee)
                {
                    string[] row = new string[]
                    {
                        devotee.ReceiptNumber.ToString(),
                        devotee.DevoteeName.ToString(),
                        devotee.Address.ToString(),
                        devotee.Country.ToString(),
                        devotee.State.ToString(),
                        devotee.City.ToString(),
                        devotee.AnadhanamDate.ToString("dd-MMM-yyyy"),
                        devotee.Amount.ToString(),
                        devotee.PaymentMode.ToString(),
                        devotee.PaymentMode==PaymentMode.CHEQUE?devotee.ChequeNo.ToString():string.Empty,
                        devotee.PaymentMode==PaymentMode.CHEQUE?devotee.ChequeDate.ToString("dd-MMM-yyyy"):string.Empty,
                        devotee.PaymentMode==PaymentMode.CHEQUE?devotee.ChequeDrawn.ToString():string.Empty,
                        devotee.ReceiptCreatedDate.ToString("dd-MMM-yyyy"),
                    };
                    dgDevoteeDetails.Rows.Add(row);
                }

                dgDevoteeDetails.ReadOnly = false;
                dgDevoteeDetails.Refresh();
            }
            else
            {
                MessageBox.Show("No Records Found...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
