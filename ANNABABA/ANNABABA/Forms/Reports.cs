namespace ANNABABA
{
    using Microsoft.Reporting.WinForms;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="Reports" />.
    /// </summary>
    public partial class Reports : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Reports"/> class.
        /// </summary>
        public Reports()
        {
            InitializeComponent();

            dtPeriodTo.MinDate = Convert.ToDateTime("1-JAN-2011");
            dtPeriodTo.MaxDate = DateTime.Now.AddMonths(4);

            dtPeriodFrom.MinDate = Convert.ToDateTime("1-JAN-2011");
            dtPeriodFrom.MaxDate = DateTime.Now.AddMonths(4);

            dtPeriodTo.Value = DateTime.Now.AddMonths(4).Date;
            dtPeriodFrom.Value = dtPeriodTo.Value.AddMonths(-1);

            MaximizeBox = false;

            DateTime periodFrom = new DateTime(dtPeriodFrom.Value.Year, dtPeriodFrom.Value.Month, dtPeriodFrom.Value.Day, 0, 0, 0);
            DateTime periodTo = new DateTime(dtPeriodTo.Value.Year, dtPeriodTo.Value.Month, dtPeriodTo.Value.Day, 23, 59, 59);

            ReportModeDetails();

            ReportParameter[] param = new ReportParameter[3];
            param[0] = new ReportParameter("dtPeriodFrom", periodFrom.ToString("dd-MMM-yyyy HH:mm:ss"));
            param[1] = new ReportParameter("dtPeriodTo", periodTo.ToString("dd-MMM-yyyy HH:mm:ss"));
            if (cmbTypes.SelectedIndex == 2 && txtreceiptnumber.Text != null && txtreceiptnumber.Text != "")
            {
                param[2] = new ReportParameter("txtReceiptNumber", Convert.ToString(txtreceiptnumber.Text));
            }
            else
            {
                param[2] = new ReportParameter("txtReceiptNumber", "0");
            }

            reportViewer1.LocalReport.SetParameters(param);
            reportViewer1.LocalReport.Refresh();
        }

        /// <summary>
        /// The ABCAnnadhanamReorts_Load.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void ABCAnnadhanamReorts_Load(object sender, EventArgs e)
        {
            DateTime periodFrom = new DateTime(dtPeriodFrom.Value.Year, dtPeriodFrom.Value.Month, dtPeriodFrom.Value.Day, 12, 0, 0);
            DateTime periodTo = new DateTime(dtPeriodTo.Value.Year, dtPeriodTo.Value.Month, dtPeriodTo.Value.Day, 12, 0, 0);

            string exportOption = "Excel";
            RenderingExtension extension = reportViewer1.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
            if (extension != null)
            {
                System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                fieldInfo.SetValue(extension, false);
            }

            string exportOption1 = "Word";
            RenderingExtension extension1 = reportViewer1.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption1, StringComparison.CurrentCultureIgnoreCase));

            if (extension != null)
            {
                System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                fieldInfo.SetValue(extension1, false);
            }
            tblAnnadhanamDetailsTableAdapter.FillByAnnadhanamDate(ABCAnnadhanamReportsDataset.tblAnnadhanamDetails, periodFrom, periodTo);
            reportViewer1.RefreshReport();
        }

        /// <summary>
        /// Defines the ReportModeList.
        /// </summary>
        public static Dictionary<int, string> ReportModeList = new Dictionary<int, string>()
        {
            { 1, "Annadhanam Date"},
            { 2, "Receipt Date"},
            { 3, "Receipt Number"},
        };

        /// <summary>
        /// The ReportModeDetails.
        /// </summary>
        public void ReportModeDetails()
        {
            cmbTypes.DataSource = new BindingSource(ReportModeList, null);
            cmbTypes.DisplayMember = "Value";
            cmbTypes.ValueMember = "Key";
            cmbTypes.SelectedValue = 1;
        }

        /// <summary>
        /// The cmbTypes_SelectedValueChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void cmbTypes_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbTypes.SelectedIndex == 2)
            {
                dtPeriodFrom.Visible = false;
                dtPeriodTo.Visible = false;
                lblRPTFromDate.Visible = false;
                lblRPTTODate.Visible = false;
                lblReceiptnumber.Visible = true;
                txtreceiptnumber.Visible = true;
            }
            else
            {
                if (cmbTypes.SelectedIndex == 0)
                {
                    dtPeriodTo.MinDate = Convert.ToDateTime("1-JAN-2011");
                    dtPeriodTo.MaxDate = DateTime.Now.AddMonths(4);

                    dtPeriodFrom.MinDate = Convert.ToDateTime("1-JAN-2011");
                    dtPeriodFrom.MaxDate = DateTime.Now.AddMonths(4);

                    dtPeriodTo.Value = DateTime.Now.AddMonths(4).Date;
                    dtPeriodFrom.Value = dtPeriodTo.Value.AddMonths(-1);
                }
                else if (cmbTypes.SelectedIndex == 1)
                {
                    dtPeriodFrom.Value = DateTime.Now.Date.AddMonths(-1);
                    dtPeriodTo.Value = DateTime.Now.Date;

                    dtPeriodTo.MinDate = Convert.ToDateTime("1-JAN-2011");
                    dtPeriodTo.MaxDate = DateTime.Now;

                    dtPeriodFrom.MinDate = Convert.ToDateTime("1-JAN-2011");
                    dtPeriodFrom.MaxDate = DateTime.Now;

                }
                dtPeriodFrom.Visible = true;
                dtPeriodTo.Visible = true;
                lblRPTFromDate.Visible = true;
                lblRPTTODate.Visible = true;

                lblReceiptnumber.Visible = false;
                txtreceiptnumber.Visible = false;
                ReportDetails();
            }
        }

        /// <summary>
        /// The dtPeriodTo_ValueChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void dtPeriodTo_ValueChanged(object sender, EventArgs e)
        {            
            if (dtPeriodTo.Value.Date <  dtPeriodFrom.Value.Date)
            {
                MessageBox.Show("To Date should be greater than From date", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ReportDetails();
            }
        }

        /// <summary>
        /// The dtPeriodFrom_ValueChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void dtPeriodFrom_ValueChanged(object sender, EventArgs e)
        {
            if (dtPeriodFrom.Value.Date >  dtPeriodTo.Value.Date)
            {
                MessageBox.Show("From Date should be lesser than To date", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ReportDetails();
            }
        }

        /// <summary>
        /// The txtreceiptnumber_KeyPress.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="KeyPressEventArgs"/>.</param>
        private void txtreceiptnumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// The txtreceiptnumber_Leave.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void txtreceiptnumber_Leave(object sender, EventArgs e)
        {
            ReportDetails();
        }

        /// <summary>
        /// The ReportDetails.
        /// </summary>
        protected void ReportDetails()
        {
            try
            {
                DateTime periodFrom = new DateTime(dtPeriodFrom.Value.Year, dtPeriodFrom.Value.Month, dtPeriodFrom.Value.Day, 0, 0, 0);
                DateTime periodTo = new DateTime(dtPeriodTo.Value.Year, dtPeriodTo.Value.Month, dtPeriodTo.Value.Day, 23, 59, 59);

                ReportParameter[] param = new ReportParameter[3];
                param[0] = new ReportParameter("dtPeriodFrom", periodFrom.ToString("dd-MMM-yyyy HH:mm:ss"));
                param[1] = new ReportParameter("dtPeriodTo", periodTo.ToString("dd-MMM-yyyy HH:mm:ss"));
                if (cmbTypes.SelectedIndex == 2 && txtreceiptnumber.Text != null && txtreceiptnumber.Text != "")
                {
                    param[2] = new ReportParameter("txtReceiptNumber", Convert.ToString(txtreceiptnumber.Text));
                }
                else
                {
                    param[2] = new ReportParameter("txtReceiptNumber", "0");
                }
                reportViewer1.LocalReport.SetParameters(param);

                ABCAnnadhanamReportsDatasetTableAdapters.tblAnnadhanamDetailsTableAdapter adapter = new ABCAnnadhanamReportsDatasetTableAdapters.tblAnnadhanamDetailsTableAdapter();
                ABCAnnadhanamReportsDataset.tblAnnadhanamDetailsDataTable table = new ANNABABA.ABCAnnadhanamReportsDataset.tblAnnadhanamDetailsDataTable();
                if (cmbTypes.SelectedIndex == 0)
                {
                    adapter.FillByAnnadhanamDate(table, periodFrom, periodTo);
                }
                if (cmbTypes.SelectedIndex == 1)
                {
                    adapter.FillByReceiptCreatedDate(table, periodFrom, periodTo);
                }
                if (cmbTypes.SelectedIndex == 2)
                {
                    if (txtreceiptnumber.Text.Length > 0)
                        adapter.FillByReceiptNumberDetails(table, Convert.ToInt64(txtreceiptnumber.Text));
                }
                reportViewer1.Visible = true;

                ReportDataSource DS = new ReportDataSource("ABCReportsDataset", (DataTable)table);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(DS);
                reportViewer1.LocalReport.Refresh();
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            finally
            {
            }
        }

        /// <summary>
        /// The ABCAnnadhanamReports_FormClosing.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="FormClosingEventArgs"/>.</param>
        private void ABCAnnadhanamReports_FormClosing(object sender, FormClosingEventArgs e)
        {
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            reportViewer1.Dispose();
        }
    }
}
