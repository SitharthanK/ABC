using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace ANNABABA.Forms
{
    public partial class DressForm : Form
    {
        Timer _timer;
        private SqlCeConnection _con = new SqlCeConnection(@"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf");
        public DressForm()
        {
            InitializeComponent();
            ddlYear_DataBing();
            ddlMonth_DataBing();
            StartTimer();
            MaximizeBox = false;
        }

        #region TIMER VALIDATIONS

        private void StartTimer()
        {
            _timer = new Timer {Interval = 1000};
            _timer.Tick += tmr_Tick;
            _timer.Enabled = true;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            lblDateValue.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }
        #endregion   

        public static Dictionary<int, string> monthList = new Dictionary<int, string>()
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

        public void ddlMonth_DataBing()
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
                    string devoteeName = row.Cells["DevoteeName"].Value.ToString();
                    string address = row.Cells["Address"].Value.ToString();
                    string contactNumber = row.Cells["ContactNumber"].Value.ToString();
                    string date = row.Cells["Date"].Value.ToString();

                    InsertDressDetails(date,devoteeName, address, contactNumber);
                    MessageBox.Show(@"You Have Selected " + ((e.RowIndex + 1).ToString() + devoteeName + address) + @" Row Button");
                }
            }
            if (e.ColumnIndex == 7)
            {
                DataGridViewRow row = dgMonthdetails.Rows[e.RowIndex];
                if (row != null)
                {
                    row.Cells["DevoteeName"].Value = "";
                    row.Cells["Address"].Value = "";
                    row.Cells["ContactNumber"].Value = "";
                }
            }
        }

        #region OPEN SQL CONNECTION
        private void OpenSqlCeConnection()
        {
            try
            {
                _con = new SqlCeConnection(@"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf");
                _con.Open();
            }
            catch (SqlCeInvalidDatabaseFormatException)
            {
                string connStringCI = @"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf; LCID= 1033";
                string connStringCS = @"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf; LCID= 1033; Case Sensitive=true";

                SqlCeEngine engine = new SqlCeEngine(connStringCI);
                engine.Upgrade(connStringCS);

                _con = null;
                _con = new SqlCeConnection(connStringCI);
                _con.Open();
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
        #region DRESS DETAILS
        private void GetDressDetails()
        {
            try
            {
                if (ddlYear.SelectedValue != null && !string.IsNullOrWhiteSpace(ddlYear.SelectedValue.ToString()) && !ddlYear.SelectedValue.ToString().Contains("Select") &&
                      ddlMonth.SelectedValue != null && !string.IsNullOrWhiteSpace(ddlMonth.SelectedValue.ToString()) && !ddlMonth.SelectedValue.ToString().Contains("Select"))
                {
                    OpenSqlCeConnection();
                    string strQuery = "SELECT * FROM  BABADRESSDETAILS  WHERE Month='" + ddlMonth.SelectedValue + "' OR Year='" + ddlYear.SelectedValue + "'";
                    SqlCeCommand cm = new SqlCeCommand(strQuery, _con)
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
                        PolulateGridDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _con.Close();
            }
        }

        private void PolulateGridDetails()
        {
            int rowNo = 0;

            var dataTable = new DataTable();
            dataTable.Columns.Add("SlNo", typeof(string));
            dataTable.Columns.Add("Date", typeof(string));
            dataTable.Columns.Add("Day", typeof(string));
           
            DateTime startDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue.ToString()), 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            for (var day = startDate.Date; day <= endDate; day = day.AddDays(1))
            {
                rowNo = rowNo + 1;
                var dRow = dataTable.NewRow();
                dRow["SlNo"] = rowNo.ToString();
                dRow["Date"] = day.Date.ToString("dd/MMM/yyyy");
                dRow["Day"] = day.DayOfWeek.ToString();
                dataTable.Rows.Add(dRow);
            }
            dgMonthdetails.DataSource = dataTable;
            DataGridTextBoxColumn devoteeName = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("DevoteeName", "Devotee Name");
            devoteeName.Width = 400;
            devoteeName.TextBox.Name = "DevoteeName";

            DataGridTextBoxColumn address = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("Address", "Address");
            address.Width = 600;
            address.TextBox.Name = "Address";

            DataGridTextBoxColumn contactNumber = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("ContactNumber", "Contact Number");
            contactNumber.Width = 400;
            contactNumber.TextBox.Name = "ContactNumber";

            DataGridViewButtonColumn btnSave = new DataGridViewButtonColumn();
            dgMonthdetails.Columns.Add(btnSave);
            btnSave.HeaderText = @"Save";
            btnSave.Text = "Save";
            btnSave.Name = "btnSave";
            btnSave.UseColumnTextForButtonValue = true;
            dgMonthdetails.Refresh();

            DataGridViewButtonColumn btnClear = new DataGridViewButtonColumn();
            dgMonthdetails.Columns.Add(btnClear);
            btnClear.HeaderText = @"Clear";
            btnClear.Text = "Clear";
            btnClear.Name = "btnClear";
            btnClear.UseColumnTextForButtonValue = true;

            dgMonthdetails.Columns["SlNo"].ReadOnly = true;
            dgMonthdetails.Columns["Date"].ReadOnly = true;
            dgMonthdetails.Columns["Day"].ReadOnly = true;
            dgMonthdetails.ForeColor = Color.Black;

            dgMonthdetails.Columns["SlNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgMonthdetails.Columns["Date"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgMonthdetails.Columns["Day"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgMonthdetails.Columns["DevoteeName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgMonthdetails.Columns["Address"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgMonthdetails.Refresh();
        }

        private void InsertDressDetails(string date, string devoteeName, string address, string contactNumber)
        {
            DateTime bookingDate = DateTime.Parse(date);
            string  month = bookingDate.Month.ToString(), year = bookingDate.Year.ToString();
            string strInsertQuery = "INSERT INTO BABADRESSDETAILS(BOOKINGDATE,DEVOTEENAME,ADDRESS,CONTACTNUMBER,MONTH,YEAR)VALUES(@BOOKINGDATE,@DEVOTEENAME,@ADDRESS,@CONTACTNUMBER,@MONTH,@YEAR)";

           
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
            OpenSqlCeConnection();

            SqlCeCommand cm = new SqlCeCommand(strInsertQuery, _con);
            cm.Parameters.AddWithValue("@BOOKINGDATE", bookingDate);
            cm.Parameters.AddWithValue("@DEVOTEENAME", devoteeName);
            cm.Parameters.AddWithValue("@ADDRESS", address);
            cm.Parameters.AddWithValue("@CONTACTNUMBER", contactNumber);
            cm.Parameters.AddWithValue("@MONTH", month);
            cm.Parameters.AddWithValue("@YEAR", year);            
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
        #endregion
    }
}