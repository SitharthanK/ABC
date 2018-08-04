using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace ANNABABA.Forms
{
    public partial class ReceiptReport : Form
    {
        public ReceiptReport(string strReceiptNumber, string strName, string strFullAddress, DateTime dtAnadhanamDate,
                             string strChequeNumber, DateTime dtChequeDate, string strChequeDrawn, string strPaymentMode)
        {
            InitializeComponent();
            MaximizeBox = false;

            ReportParameter[] param = new ReportParameter[8];
            param[0] = new ReportParameter("ReceiptNumber", strReceiptNumber);
            param[1] = new ReportParameter("Address", strFullAddress);
            param[2] = new ReportParameter("AnnadhanamDate", dtAnadhanamDate.ToString("dd-MMM-yyyy"));
            param[3] = new ReportParameter("ChequeNumber", (strPaymentMode == "Cash" ? "NIL" : strChequeNumber));
            param[4] = new ReportParameter("ChequeDate", (strPaymentMode == "Cash" ? "NIL" : dtChequeDate.ToString("dd-MMM-yyyy")));
            param[5] = new ReportParameter("ChequeDrawnOn", (strPaymentMode == "Cash" ? "NIL" : strChequeDrawn));
            param[6] = new ReportParameter("NameOfDevotee", strName);
            param[7] = new ReportParameter("TodayDate", DateTime.Now.ToString("dd-MMM-yyyy"));

            ReceiptReportViewer.LocalReport.SetParameters(param);
            ReceiptReportViewer.LocalReport.Refresh();
        }

        private void ReceiptReport_Load(object sender, EventArgs e)
        {
            string exportOption = "Excel";
            RenderingExtension extension = ReceiptReportViewer.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
            if (extension != null)
            {
                FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null) fieldInfo.SetValue(extension, false);
            }

            string exportOption1 = "Word";
            RenderingExtension extension1 = ReceiptReportViewer.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption1, StringComparison.CurrentCultureIgnoreCase));

            if (extension != null)
            {
                FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo != null) fieldInfo.SetValue(extension1, false);
            }
            ReceiptReportViewer.RefreshReport();
        }

        private void ReceiptReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReceiptReportViewer.LocalReport.ReleaseSandboxAppDomain();
            ReceiptReportViewer.Dispose();
        }
    }
}
