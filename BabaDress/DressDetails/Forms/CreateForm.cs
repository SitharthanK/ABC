using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace DressDetails.Forms
{
    public partial class CreateForm : Form
    {
        Timer _timer;
        private SqlConnection _con = null;
        private readonly string _conn = string.Empty;
        public CreateForm()
        {
            InitializeComponent();
            ddlYear_DataBing();
            ddlMonth_DataBing();
            StartTimer();
            MaximizeBox = false;
            _conn = ConfigurationManager.ConnectionStrings["Conn"].ToString();
        }

        #region TIMER VALIDATIONS

        private void StartTimer()
        {
            _timer = new Timer { Interval = 1000 };
            _timer.Tick += tmr_Tick;
            _timer.Enabled = true;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            lblDateValue.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }
        #endregion   

        private static Dictionary<int, string> monthList = new Dictionary<int, string>()
        {
            { 0, "<-Select->"},
            { 1, "January"},
            { 2, "February"},
            { 3, "March"},
            { 4, "April"},
            { 5, "May"},
            { 6, "June"},
            { 7, "July"},
            { 8, "August"},
            { 9, "September"},
            {10, "October"},
            {11, "November"},
            {12, "December"},
        };

        private void ddlMonth_DataBing()
        {
            ddlMonth.DataSource = new BindingSource(monthList, null);
            ddlMonth.DisplayMember = "Value";
            ddlMonth.ValueMember = "Key";
        }

        private void ddlYear_DataBing()
        {
            Dictionary<string, string> yearList = new Dictionary<string, string>();
            yearList.Add("<-Select->", "<-Select->");
            int currentYear = Convert.ToInt32(DateTime.Now.Year.ToString());
            for (int i = currentYear; i <= currentYear + 5; i++)
            {
                yearList.Add(i.ToString(), i.ToString());
            }
            ddlYear.DataSource = new BindingSource(yearList, null);
            ddlYear.DisplayMember = "Value";
            ddlYear.ValueMember = "Key";
        }

        private void ddlMonth_SelectedValueChanged(object sender, EventArgs e)
        {
            GetDressDetails();
        }

        private void ddlYear_SelectedValueChanged(object sender, EventArgs e)
        {
            GetDressDetails();
        }
        private void dgMonthdetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                DataGridViewRow row = dgMonthdetails.Rows[e.RowIndex];
                if (row != null)
                {
                    string devoteeName = row.Cells["DEVOTEENAME"].Value.ToString();
                    string address = row.Cells["ADDRESS"].Value.ToString();
                    string contactNumber = row.Cells["CONTACTNUMBER"].Value.ToString();
                    string date = row.Cells["BOOKINGDATE"].Value.ToString();
                    string month = Convert.ToDateTime(date).Month.ToString();
                    string year = Convert.ToDateTime(date).Year.ToString();

                    if (CheckIfRecordExistsAlready(date, month, year))
                        InsertDressDetails(date, devoteeName, address, contactNumber);
                    else
                        MessageBox.Show(@"Booked Failed - Devotee Details cannot be edited!.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (e.ColumnIndex == 7)
            {
                DataGridViewRow row = dgMonthdetails.Rows[e.RowIndex];
                if (row != null)
                {
                    row.Cells["DEVOTEENAME"].Value = "";
                    row.Cells["ADDRESS"].Value = "";
                    row.Cells["CONTACTNUMBER"].Value = "";
                }
            }
        }

        #region DRESS DETAILS
        private void GetDressDetails()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ddlYear.SelectedValue.ToString()) && !ddlYear.SelectedValue.ToString().Contains("Select") &&
                    !string.IsNullOrWhiteSpace(ddlMonth.SelectedValue.ToString()) && !ddlMonth.SelectedValue.ToString().Contains("Select") && ddlMonth.SelectedValue.ToString()!="0")
                {
                    string conn = ConfigurationManager.ConnectionStrings["Conn"].ToString();
                    _con = new SqlConnection(conn);
                    _con.Open();

                    string strQuery = @"SELECT * FROM  DRESSDETAILS  WHERE BookedMonth='" + ddlMonth.SelectedValue + "' AND BookedYear='" + ddlYear.SelectedValue + "'";
                    SqlCommand cm = new SqlCommand(strQuery, _con)
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.Text,
                        Connection = _con
                    };
                    var dataReader = cm.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    PolulateGridDetails(dataTable);
                    _con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PolulateGridDetails(DataTable table)
        {
            int rowNo = 0;
            DataGridTextBoxColumn slno = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("SLNO", "Slno.");
            slno.Width = 30;
            slno.ReadOnly = true;
            slno.TextBox.Name = "SLNO";
            slno.Alignment = HorizontalAlignment.Center;

            DataGridTextBoxColumn bookingDate = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("BOOKINGDATE", "Booking Date");
            bookingDate.ReadOnly = true;
            bookingDate.Width = 100;
            bookingDate.TextBox.Name = "BOOKINGDATE";
            bookingDate.Alignment = HorizontalAlignment.Center;

            DataGridTextBoxColumn days = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("Days", "Days");
            days.ReadOnly = true;
            days.Width = 100;
            days.TextBox.Name = "Days";
            days.Alignment = HorizontalAlignment.Left;

            DataGridTextBoxColumn devoteename = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("DEVOTEENAME", "Devotee Name");
            devoteename.Width = 600;
            devoteename.TextBox.Name = "DEVOTEENAME";
            devoteename.Alignment = HorizontalAlignment.Left;

            DataGridTextBoxColumn address = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("ADDRESS", "Address");
            address.Width = 600;
            address.TextBox.Name = "ADDRESS";
            address.Alignment = HorizontalAlignment.Left;

            DataGridTextBoxColumn contactNumber = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("CONTACTNUMBER", "Contact Number");
            contactNumber.Width = 400;
            contactNumber.TextBox.Name = "CONTACTNUMBER";
            contactNumber.Alignment = HorizontalAlignment.Right;

            DataGridViewButtonColumn btnSave = new DataGridViewButtonColumn();
            dgMonthdetails.Columns.Add(btnSave);
            btnSave.HeaderText = @"Create";
            btnSave.Text = "Create";
            btnSave.Name = "btnSave";
            btnSave.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn btnClear = new DataGridViewButtonColumn();
            dgMonthdetails.Columns.Add(btnClear);
            btnClear.HeaderText = @"Clear";
            btnClear.Text = "Clear";
            btnClear.Name = "btnClear";
            btnClear.UseColumnTextForButtonValue = true;
            dgMonthdetails.Refresh();

            DateTime startDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue.ToString()), 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            for (var day = startDate.Date; day <= endDate; day = day.AddDays(1))
            {
                var row = (from DataRow dr in table.Rows
                           where (dr["BOOKEDDATE"] != null && Convert.ToDateTime(dr["BOOKEDDATE"]).Date == day.Date)
                           select new DressDetails()
                           {
                               BookDate = Convert.ToString(dr["BOOKEDDATE"]),
                               Name = Convert.ToString(dr["DEVOTEENAME"]),
                               Address = Convert.ToString(dr["ADDRESS"]),
                               ContactNumber = Convert.ToString(dr["CONTACTNUMBER"]),
                               Month = Convert.ToString(dr["BOOKEDMONTH"]),
                               Year = Convert.ToString(dr["BOOKEDYEAR"])
                           }).FirstOrDefault();

                rowNo = rowNo + 1;
                if (row != null && Convert.ToDateTime(day).Date == Convert.ToDateTime(row.BookDate).Date)
                {
                    dgMonthdetails.Rows.Add(rowNo.ToString(), DateTime.Parse(row.BookDate).ToString("dd/MMM/yyyy"), day.DayOfWeek.ToString(), row.Name, row.Address, row.ContactNumber);
                }
                else
                    dgMonthdetails.Rows.Add(rowNo.ToString(), day.Date.ToString("dd/MMM/yyyy"), day.DayOfWeek.ToString(), "", "", "");

            }
            dgMonthdetails.Refresh(); ;
        }

        private void InsertDressDetails(string date, string devoteeName, string address, string contactNumber)
        {
            DateTime bookingDate = DateTime.Parse(date);
            string month = bookingDate.Month.ToString(), year = bookingDate.Year.ToString();
            string strInsertQuery = "INSERT INTO DRESSDETAILS(BOOKEDDATE,DEVOTEENAME,ADDRESS,CONTACTNUMBER,BOOKEDMONTH,BOOKEDYEAR,InsertedBy)VALUES(@BOOKEDDATE,@DEVOTEENAME,@ADDRESS,@CONTACTNUMBER,@BOOKEDMONTH,@BOOKEDYEAR,@InsertedBy)";


            if (string.IsNullOrWhiteSpace(devoteeName))
            {
                MessageBox.Show(@"Type Devotee Name", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show(@"Type Devotee Address", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (string.IsNullOrWhiteSpace(contactNumber))
            {
                MessageBox.Show(@"Type ContactNumber", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _con = new SqlConnection(_conn);
            _con.Open();

            SqlCommand cm = new SqlCommand(strInsertQuery, _con);
            cm.Parameters.AddWithValue("@BOOKEDDATE", bookingDate);
            cm.Parameters.AddWithValue("@DEVOTEENAME", devoteeName);
            cm.Parameters.AddWithValue("@ADDRESS", address);
            cm.Parameters.AddWithValue("@CONTACTNUMBER", contactNumber);
            cm.Parameters.AddWithValue("@BOOKEDMONTH", month);
            cm.Parameters.AddWithValue("@BOOKEDYEAR", year);
            cm.Parameters.AddWithValue("@InsertedBy", 1000);
            try
            {
                int intAffectedRow = cm.ExecuteNonQuery();
                if (intAffectedRow > 0)
                {
                    MessageBox.Show(@"Booked Sucessfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(@"Booked Failed", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                _con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckIfRecordExistsAlready(string bookingDate, string month, string year)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(month) && !string.IsNullOrWhiteSpace(year))
                {
                    _con = new SqlConnection(_conn);
                    _con.Open();
                    string strQuery = @"SELECT * FROM  DRESSDETAILS  WHERE BookedMonth='" + month + "' AND BookedYear='" + year + "'";
                    SqlCommand cm = new SqlCommand(strQuery, _con)
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.Text,
                        Connection = _con
                    };
                    var dataReader = cm.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        int count = (from DataRow dr in dataTable.Rows
                                     where (dr["BOOKEDDATE"] != null && Convert.ToDateTime(dr["BOOKEDDATE"]).Date == Convert.ToDateTime(bookingDate).Date)
                                     select new DressDetails()
                                     {
                                         BookDate = Convert.ToString(dr["BOOKEDDATE"]),
                                     }).Count();
                        if (count == 1)
                            return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                _con.Close();
            }
        }
        #endregion
    }

    internal class DressDetails
    {
        public string BookDate { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string IsCreated { get; set; }
        public string IsUpdated { get; set; }
    }
}
