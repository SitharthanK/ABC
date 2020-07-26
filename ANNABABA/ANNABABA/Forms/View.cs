using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using Microsoft.Reporting.WinForms;
using ANNABABA.Helpers;
using ANNABABA.Models;

namespace ANNABABA
{
    public partial class View : Form
    {
        public bool blnOpenCreateWindow = false;
        public SqlCeConnection Con = new SqlCeConnection(@"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf");

        public View()
        {
            InitializeComponent();
            MaximizeBox = false;
        }

        private void PrintInputForm_Load(object sender, EventArgs e)
        {
            string exportOption = "Excel";
            RenderingExtension extension = ReceiptReportViewer.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
            if (extension != null)
            {
                System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                fieldInfo.SetValue(extension, false);
            }

            string exportOption1 = "Word";
            RenderingExtension extension1 = ReceiptReportViewer.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption1, StringComparison.CurrentCultureIgnoreCase));

            if (extension != null)
            {
                System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                fieldInfo.SetValue(extension1, false);
            }
        }

        private void btnGetDetails_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtReceiptNumber.Text))
            {
                Devotee devotee=SqlHelper.GetDevoteeDetails(txtReceiptNumber.Text);
                GetData(devotee);
            }
            else
            {
                MessageBox.Show("Enter Valid Receipt Number", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void GetData( Devotee devotee)
        {
            try
            {
                string strFullAddress = devotee.Address + ", " + devotee.City + "," + devotee.State + "," + devotee.Country + ".";

                ReportParameter[] param = new ReportParameter[8];
                param[0] = new ReportParameter("ReceiptNumber",Convert.ToString(devotee.ReceiptNumber));
                param[1] = new ReportParameter("Address", strFullAddress);
                param[2] = new ReportParameter("AnnadhanamDate", devotee.AnadhanamDate.Date.ToString("dd-MMM-yyyy"));
                param[3] = new ReportParameter("ChequeNumber", (devotee.PaymentMode == PaymentMode.CASH ? "NIL" : devotee.ChequeNo));
                param[4] = new ReportParameter("ChequeDate", (devotee.PaymentMode == PaymentMode.CASH ? "NIL" : devotee.ChequeDate.Date.ToString("dd-MMM-yyyy")));
                param[5] = new ReportParameter("ChequeDrawnOn", (devotee.PaymentMode == PaymentMode.CASH ? "NIL" : devotee.ChequeDrawn));
                param[6] = new ReportParameter("NameOfDevotee", devotee.DevoteeName);
                param[7] = new ReportParameter("TodayDate", devotee.ReceiptCreatedDate.Date.ToString("dd-MMM-yyyy"));

                this.ReceiptReportViewer.LocalReport.SetParameters(param);
                this.ReceiptReportViewer.LocalReport.Refresh();
                this.ReceiptReportViewer.RefreshReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Con.Close();
            }

        }
        #region Open SqlCe Connection
        private void OpenSqlCeConnection()
        {
            try
            {
                Con = new SqlCeConnection(@"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf");
                Con.Open();
            }
            catch (SqlCeInvalidDatabaseFormatException ex)
            {
                string connStringCI = @"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf; LCID= 1033";
                string connStringCS = @"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf; LCID= 1033; Case Sensitive=true";

                SqlCeEngine engine = new SqlCeEngine(connStringCI);
                engine.Upgrade(connStringCS);

                Con = null;
                Con = new SqlCeConnection(connStringCI);
                Con.Open();
            }
            catch (SqlCeException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }
        #endregion
        private void PrintInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ReceiptReportViewer.LocalReport.ReleaseSandboxAppDomain();
            this.ReceiptReportViewer.Dispose();
        }
    }
}
