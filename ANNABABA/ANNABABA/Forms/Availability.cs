namespace ANNABABA
{
    using ANNABABA.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="Availability" />.
    /// </summary>
    public partial class Availability : Form
    {
        /// <summary>
        /// Gets or sets the lstAvailability.
        /// </summary>
        private static List<Models.Availability> lstAvailability { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Availability"/> class.
        /// </summary>
        /// <param name="dtAnnadhanamDate">The dtAnnadhanamDate<see cref="DateTime"/>.</param>
        public Availability(DateTime dtAnnadhanamDate)
        {
            MaximizeBox = false;
            InitializeComponent();

            try
            {
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

                lstAvailability = SqlHelper.GetAnnadhanamAvailabilityDate(1);

                button1.Text = lstAvailability.Where(n => n.annadhanamDate.Date == dtPeriodFrom.Date).Select(n => Convert.ToString(n.balanceReceiptCount)).FirstOrDefault();
                button2.Text = lstAvailability.Where(n => n.annadhanamDate.Date.AddDays(1) == dtPeriodFrom.Date).Select(n => Convert.ToString(n.balanceReceiptCount)).FirstOrDefault();
                button3.Text = lstAvailability.Where(n => n.annadhanamDate.Date.AddDays(2) == dtPeriodFrom.Date).Select(n => Convert.ToString(n.balanceReceiptCount)).FirstOrDefault();
                button4.Text = lstAvailability.Where(n => n.annadhanamDate.Date.AddDays(3) == dtPeriodFrom.Date).Select(n => Convert.ToString(n.balanceReceiptCount)).FirstOrDefault();
                button5.Text = lstAvailability.Where(n => n.annadhanamDate.Date.AddDays(4) == dtPeriodFrom.Date).Select(n => Convert.ToString(n.balanceReceiptCount)).FirstOrDefault();
                button6.Text = lstAvailability.Where(n => n.annadhanamDate.Date.AddDays(5) == dtPeriodFrom.Date).Select(n => Convert.ToString(n.balanceReceiptCount)).FirstOrDefault();
                button7.Text = lstAvailability.Where(n => n.annadhanamDate.Date.AddDays(6) == dtPeriodFrom.Date).Select(n => Convert.ToString(n.balanceReceiptCount)).FirstOrDefault();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }
    }

    /// <summary>
    /// Defines the <see cref="AvailabilityDetails" />.
    /// </summary>
    public class AvailabilityDetails
    {
        /// <summary>
        /// Gets or sets the intCount.
        /// </summary>
        public int intCount { get; set; }

        /// <summary>
        /// Gets or sets the dtAnnadhanamDate.
        /// </summary>
        public string dtAnnadhanamDate { get; set; }

        /// <summary>
        /// Gets or sets the intBalanceCount.
        /// </summary>
        public int intBalanceCount { get; set; }
    }
}
