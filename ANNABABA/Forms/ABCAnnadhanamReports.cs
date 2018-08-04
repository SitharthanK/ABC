using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace ANNABABA.Forms
{
    public partial class AbcAnnadhanamReports : Form
    {
        #region CONSTRUCTOR
        public AbcAnnadhanamReports()
        {
            InitializeComponent();

            dtPeriodTo.MinDate = Convert.ToDateTime("1-JAN-2011");
            dtPeriodTo.MaxDate = DateTime.Now.AddMonths(4);

            dtPeriodFrom.MinDate = Convert.ToDateTime("1-JAN-2011");
            dtPeriodFrom.MaxDate = DateTime.Now.AddMonths(4);

            dtPeriodTo.Value = DateTime.Now.AddMonths(4).Date;
            dtPeriodFrom.Value = dtPeriodTo.Value.AddMonths(-1);

            MaximizeBox = false;

            DateTime periodFrom = new DateTime(dtPeriodFrom.Value.Year, dtPeriodFrom.Value.Month, dtPeriodFrom.Value.Day, 12, 0, 0);
            DateTime periodTo = new DateTime(dtPeriodTo.Value.Year, dtPeriodTo.Value.Month, dtPeriodTo.Value.Day, 12, 0, 0);

            ReportModeDetails();

            ReportParameter[] param = new ReportParameter[3];
            param[0] = new ReportParameter("dtPeriodFrom", periodFrom.ToString("dd-MMM-yyyy"));
            param[1] = new ReportParameter("dtPeriodTo", periodTo.ToString("dd-MMM-yyyy"));
            if (cmbTypes.SelectedIndex == 2 && !string.IsNullOrEmpty(txtreceiptnumber.Text))
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
        #endregion

        #region PAGE LOAD

        private void ABCAnnadhanamReorts_Load(object sender, EventArgs e)
        {
            DateTime periodFrom = new DateTime(dtPeriodFrom.Value.Year, dtPeriodFrom.Value.Month, dtPeriodFrom.Value.Day, 12, 0, 0);
            DateTime periodTo = new DateTime(dtPeriodTo.Value.Year, dtPeriodTo.Value.Month, dtPeriodTo.Value.Day, 12, 0, 0);

            string exportOption = "Excel";
            RenderingExtension extension = reportViewer1.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
            if (extension != null)
            {
                FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null) fieldInfo.SetValue(extension, false);
            }

            string exportOption1 = "Word";
            RenderingExtension extension1 = reportViewer1.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption1, StringComparison.CurrentCultureIgnoreCase));

            if (extension != null)
            {
                FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null) fieldInfo.SetValue(extension1, false);
            }
            tblAnnadhanamDetailsTableAdapter.FillByAnnadhanamDate(ABCAnnadhanamReportsDataset.tblAnnadhanamDetails, periodFrom, periodTo);
            reportViewer1.RefreshReport();
        }
        #endregion

        #region REPORT MODE
        public static Dictionary<int, string> reportModeList = new Dictionary<int, string>()
        {
            { 1, "Annadhanam Date"},
            { 2, "Receipt Date"},
            { 3, "Receipt Number"},
        };

        public void ReportModeDetails()
        {
            cmbTypes.DataSource = new BindingSource(reportModeList, null);
            cmbTypes.DisplayMember = "Value";
            cmbTypes.ValueMember = "Key";
            cmbTypes.SelectedValue = 1;
        }

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
        #endregion

        #region DATE CHANGED EVENT
        private void dtPeriodTo_ValueChanged(object sender, EventArgs e)
        {
            if (dtPeriodTo.Value.Date < dtPeriodFrom.Value.Date)
                MessageBox.Show(@"To Date should be greater than From date", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
                ReportDetails();

        }

        private void dtPeriodFrom_ValueChanged(object sender, EventArgs e)
        {
            if (dtPeriodFrom.Value.Date > dtPeriodTo.Value.Date)
                MessageBox.Show(@"From Date should be lesser than To date", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                ReportDetails();

        }

        #endregion

        #region RECEIPT NUMBER TEXT BOX
        private void txtreceiptnumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void txtreceiptnumber_Leave(object sender, EventArgs e)
        {
            ReportDetails();
        }

        #endregion

        #region REPORT DETAILS
        protected void ReportDetails()
        {
            try
            {
                DateTime periodFrom = new DateTime(dtPeriodFrom.Value.Year, dtPeriodFrom.Value.Month, dtPeriodFrom.Value.Day, 12, 0, 0);
                DateTime periodTo = new DateTime(dtPeriodTo.Value.Year, dtPeriodTo.Value.Month, dtPeriodTo.Value.Day, 12, 0, 0);

                ReportParameter[] param = new ReportParameter[3];
                param[0] = new ReportParameter("dtPeriodFrom", periodFrom.ToString("dd-MMM-yyyy"));
                param[1] = new ReportParameter("dtPeriodTo", periodTo.ToString("dd-MMM-yyyy"));
                if (cmbTypes.SelectedIndex == 2 && !string.IsNullOrEmpty(txtreceiptnumber.Text))
                {
                    param[2] = new ReportParameter("txtReceiptNumber", Convert.ToString(txtreceiptnumber.Text));
                }
                else
                {
                    param[2] = new ReportParameter("txtReceiptNumber", "0");
                }
                reportViewer1.LocalReport.SetParameters(param);

                ABCAnnadhanamReportsDatasetTableAdapters.tblAnnadhanamDetailsTableAdapter adapter = new ABCAnnadhanamReportsDatasetTableAdapters.tblAnnadhanamDetailsTableAdapter();
                ABCAnnadhanamReportsDataset.tblAnnadhanamDetailsDataTable table =
                    new ABCAnnadhanamReportsDataset.tblAnnadhanamDetailsDataTable();
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

                ReportDataSource ds = new ReportDataSource("ABCReportsDataset", (DataTable)table);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(ds);
                reportViewer1.LocalReport.Refresh();
                reportViewer1.RefreshReport();
            }
            catch (Exception)
            {
                // ignored
            }
        }
        #endregion

        #region CLOSING
        private void ABCAnnadhanamReports_FormClosing(object sender, FormClosingEventArgs e)
        {
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            reportViewer1.Dispose();
        }
        #endregion       
    }
}
