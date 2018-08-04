using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace ANNABABA.Forms
{
    public partial class PrintInputForm : Form
    {
        public SqlCeConnection con = new SqlCeConnection(@"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf");

        public PrintInputForm()
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
                fieldInfo?.SetValue(extension, false);
            }

            string exportOption1 = "Word";
            RenderingExtension extension1 = ReceiptReportViewer.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption1, StringComparison.CurrentCultureIgnoreCase));

            if (extension != null)
            {
                System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                fieldInfo?.SetValue(extension1, false);
            }
        }

        private void btnGetDetails_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReceiptNumber.Text))
            {
                GetData(txtReceiptNumber.Text);
            }
            else
            {
                MessageBox.Show(@"Enter Valid Receipt Number", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void GetData(string strReceiptNumber)
        {
            string strName = string.Empty,strFullAddress = string.Empty,strChequeNumber = string.Empty, strChequeDrawn = string.Empty, strPaymentMode = string.Empty;
            DateTime dtAnadhanamDate = new DateTime(), dtChequeDate = new DateTime();

            OpenSqlCeConnection();
            try
            {
                string strQuery = "SELECT ReceiptNumber,DevoteeName,Address,Country,State,City,Amount,ReceiptCreatedDate,AnadhanamDate,ChequeNo,ChequeDate,ChequeDrawn,Mode,ContactNumber FROM  tblAnnadhanamDetails WHERE ReceiptNumber='" + strReceiptNumber + "' OR ContactNumber='" + strReceiptNumber + "'";

                using (con)
                {
                    using (SqlCeDataAdapter da = new SqlCeDataAdapter())
                    {
                        SqlCeCommand cmd = new SqlCeCommand(strQuery, con);
                        cmd.Connection = con;
                        da.SelectCommand = cmd;
                        var ds = new DataSet();
                        da.Fill(ds);
                        con.Close();

                        DataTable dt;
                        dt = ds.Tables[0].Clone();

                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow lastRow = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1];
                            dt.Rows.Add(lastRow.ItemArray);

                            foreach (DataRow dr in dt.Rows)
                            {
                                strReceiptNumber = Convert.ToString(dr["ReceiptNumber"]);
                                strName = Convert.ToString(dr["DevoteeName"]);
                                var strAddress = Convert.ToString(dr["Address"]);
                                var strCity = Convert.ToString(dr["City"]);
                                var strState = Convert.ToString(dr["State"]);
                                var strCountry = Convert.ToString(dr["Country"]);
                                dtAnadhanamDate = Convert.ToDateTime(dr["AnadhanamDate"]).Date;
                                strPaymentMode = Convert.ToString(dr["Mode"]);

                                if (strPaymentMode.ToUpper() == "CHEQUE")
                                {
                                    dtChequeDate = Convert.ToDateTime(dr["ChequeDate"]).Date;
                                    strChequeNumber = Convert.ToString(dr["ChequeNo"]);
                                    strChequeDrawn = Convert.ToString(dr["ChequeDrawn"]);
                                }

                                strFullAddress = strAddress + ", " + strCity + "," + strState + "," + strCountry + ".";
                                break;
                            }

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
                            ReceiptReportViewer.RefreshReport();
                        }
                        else
                        {
                            MessageBox.Show(@"Enter Valid Receipt / Mobile Number", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        da.Dispose();
                        ds.Dispose();
                    }
                }
            }
            finally
            {
                con.Close();
            }
        }
        #region Open SqlCe Connection
        private void OpenSqlCeConnection()
        {
            try
            {
                con = new SqlCeConnection(@"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf");
                con.Open();
            }
            catch (SqlCeInvalidDatabaseFormatException)
            {
                string connStringCI = @"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf; LCID= 1033";
                string connStringCS = @"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf; LCID= 1033; Case Sensitive=true";

                SqlCeEngine engine = new SqlCeEngine(connStringCI);
                engine.Upgrade(connStringCS);

                con = null;
                con = new SqlCeConnection(connStringCI);
                con.Open();
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
        }
        #endregion
        private void PrintInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReceiptReportViewer.LocalReport.ReleaseSandboxAppDomain();
            ReceiptReportViewer.Dispose();
        }
    }
}
