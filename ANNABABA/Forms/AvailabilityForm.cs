using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Windows.Forms;

namespace ANNABABA.Forms
{
    public partial class AvailabilityForm : Form
    {

        private SqlCeConnection _cn = new SqlCeConnection(@"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf");

        public AvailabilityForm(DateTime dtAnnadhanamDate)
        {
            MaximizeBox = false;
            InitializeComponent();

            #region AVAILABILITY DETAILS
            try
            {
                _cn.Open();
                DateTime dtPeriodFrom = dtAnnadhanamDate.Date;
                DateTime dtPeriodTo = dtAnnadhanamDate.Date.AddDays(7);

                label1.Text = dtPeriodFrom.ToString("dd-MMM-yy");
                label2.Text = dtPeriodFrom.AddDays(1).ToString("dd-MMM-yy");
                label3.Text = dtPeriodFrom.AddDays(2).ToString("dd-MMM-yy");
                label4.Text = dtPeriodFrom.AddDays(3).ToString("dd-MMM-yy");
                label5.Text = dtPeriodFrom.AddDays(4).ToString("dd-MMM-yy");
                label6.Text = dtPeriodFrom.AddDays(5).ToString("dd-MMM-yy");
                label7.Text = dtPeriodFrom.AddDays(6).ToString("dd-MMM-yy");

                label8.Text = dtPeriodFrom.DayOfWeek.ToString();
                label9.Text = dtPeriodFrom.AddDays(1).DayOfWeek.ToString();
                label10.Text = dtPeriodFrom.AddDays(2).DayOfWeek.ToString();
                label11.Text = dtPeriodFrom.AddDays(3).DayOfWeek.ToString();
                label12.Text = dtPeriodFrom.AddDays(4).DayOfWeek.ToString();
                label13.Text = dtPeriodFrom.AddDays(5).DayOfWeek.ToString();
                label14.Text = dtPeriodFrom.AddDays(6).DayOfWeek.ToString();

                button1.Text = (Convert.ToDateTime(dtPeriodFrom).Date >= (Convert.ToDateTime("22-JAN-2016").Date)) ? "15" : "10";
                button2.Text = (Convert.ToDateTime(dtPeriodFrom.AddDays(1)).Date >= (Convert.ToDateTime("22-JAN-2016").Date)) ? "15" : "10";
                button3.Text = (Convert.ToDateTime(dtPeriodFrom.AddDays(2)).Date >= (Convert.ToDateTime("22-JAN-2016").Date)) ? "15" : "10";
                button4.Text = (Convert.ToDateTime(dtPeriodFrom.AddDays(3)).Date >= (Convert.ToDateTime("22-JAN-2016").Date)) ? "15" : "10";
                button5.Text = (Convert.ToDateTime(dtPeriodFrom.AddDays(4)).Date >= (Convert.ToDateTime("22-JAN-2016").Date)) ? "15" : "10";
                button6.Text = (Convert.ToDateTime(dtPeriodFrom.AddDays(5)).Date >= (Convert.ToDateTime("22-JAN-2016").Date)) ? "15" : "10";
                button7.Text = (Convert.ToDateTime(dtPeriodFrom.AddDays(6)).Date >= (Convert.ToDateTime("22-JAN-2016").Date)) ? "15" : "10";

                string strSelectQuery = "SELECT coalesce(COUNT(ReceiptNumber),0) as intCount, AnadhanamDate FROM tblAnnadhanamDetails WHERE AnadhanamDate>='" + dtPeriodFrom.ToString("dd-MMM-yyyy") + "' AND AnadhanamDate<'" + dtPeriodTo.ToString("dd-MMM-yyyy") + "' GROUP BY AnadhanamDate ORDER BY AnadhanamDate";
                SqlCeCommand cm = new SqlCeCommand(strSelectQuery, _cn);

                cm.CommandText = strSelectQuery;
                cm.CommandType = CommandType.Text;
                cm.Connection = _cn;

                var dataReader = cm.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);

                var result = (from DataRow dr in dataTable.Rows
                              select new AvailabilityDetails()
                              {
                                  IntCount = Convert.ToInt32(dr["intCount"]),
                                  DtAnnadhanamDate = Convert.ToString(dr["AnadhanamDate"]),
                                  IntBalanceCount = (Convert.ToDateTime(Convert.ToString(dr["AnadhanamDate"])).Date >= (Convert.ToDateTime("22-JAN-2016").Date)) ? (15 - Convert.ToInt32(dr["intCount"])) : (10 - Convert.ToInt32(dr["intCount"])),
                              }).ToList();

                if (result.Count > 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        if (Convert.ToDateTime(result[i].DtAnnadhanamDate).Date == Convert.ToDateTime(label1.Text).Date)
                        {
                            button1.Text = result[i].IntBalanceCount.ToString();
                        }
                        if (Convert.ToDateTime(result[i].DtAnnadhanamDate).Date == Convert.ToDateTime(label2.Text).Date)
                        {
                            button2.Text = result[i].IntBalanceCount.ToString();
                        }
                        if (Convert.ToDateTime(result[i].DtAnnadhanamDate).Date == Convert.ToDateTime(label3.Text).Date)
                        {
                            button3.Text = result[i].IntBalanceCount.ToString();
                        }
                        if (Convert.ToDateTime(result[i].DtAnnadhanamDate).Date == Convert.ToDateTime(label4.Text).Date)
                        {
                            button4.Text = result[i].IntBalanceCount.ToString();
                        }
                        if (Convert.ToDateTime(result[i].DtAnnadhanamDate).Date == Convert.ToDateTime(label5.Text).Date)
                        {
                            button5.Text = result[i].IntBalanceCount.ToString();
                        }
                        if (Convert.ToDateTime(result[i].DtAnnadhanamDate).Date == Convert.ToDateTime(label6.Text).Date)
                        {
                            button6.Text = result[i].IntBalanceCount.ToString();
                        }
                        if (Convert.ToDateTime(result[i].DtAnnadhanamDate).Date == Convert.ToDateTime(label7.Text).Date)
                        {
                            button7.Text = result[i].IntBalanceCount.ToString();
                        }
                    }
                }
                _cn.Close();
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

            #endregion
        }
    }

    public class AvailabilityDetails
    {
        public int IntCount { get; set; }
        public string DtAnnadhanamDate { get; set; }
        public int IntBalanceCount { get; set; }
    }
}
