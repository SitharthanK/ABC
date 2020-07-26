using System;
using System.Linq;
using System.Windows.Forms;
using ANNABABA.Models;
using Microsoft.Reporting.WinForms;

namespace ANNABABA
{
    public partial class Receipt : Form
    {
        public Receipt(Devotee devotee)
        {
            InitializeComponent();
            MaximizeBox = false;
           
            string strFullAddress = devotee.Address + ", " + devotee.City + "," + devotee.State + "," + devotee.Country + ".";

            ReportParameter[] param = new ReportParameter[8];
            param[0] = new ReportParameter("ReceiptNumber",Convert.ToString(devotee.ReceiptNumber));
            param[1] = new ReportParameter("Address", strFullAddress);
            param[2] = new ReportParameter("AnnadhanamDate", devotee.AnadhanamDate.ToString("dd-MMM-yyyy"));
            param[3] = new ReportParameter("ChequeNumber", (devotee.PaymentMode == PaymentMode.CASH ? "NIL" : devotee.ChequeNo));
            param[4] = new ReportParameter("ChequeDate", (devotee.PaymentMode == PaymentMode.CASH ? "NIL" : devotee.ChequeDate.ToString("dd-MMM-yyyy")));
            param[5] = new ReportParameter("ChequeDrawnOn", (devotee.PaymentMode == PaymentMode.CASH ? "NIL" : devotee.ChequeDrawn));
            param[6] = new ReportParameter("NameOfDevotee", devotee.DevoteeName);
            param[7] = new ReportParameter("TodayDate", DateTime.Now.ToString("dd-MMM-yyyy"));

            this.ReceiptReportViewer.LocalReport.SetParameters(param);
            this.ReceiptReportViewer.LocalReport.Refresh();
        }

        private void ReceiptReport_Load(object sender, EventArgs e)
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
            this.ReceiptReportViewer.RefreshReport();
        }

        private void ReceiptReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ReceiptReportViewer.LocalReport.ReleaseSandboxAppDomain();
            this.ReceiptReportViewer.Dispose();
        }
    }
}
