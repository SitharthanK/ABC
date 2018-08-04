using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Windows.Forms;

namespace ANNABABA.Forms
{
    public partial class BookDressForm : Form
    {
        Timer tmr = null;
        public SqlCeConnection cn = new SqlCeConnection(@"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf");
        public BookDressForm()
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
            tmr = new Timer();
            tmr.Interval = 1000;
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Enabled = true;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            lblDateValue.Text = DateTime.Now.ToString();
        }
        #endregion   

        public static Dictionary<int, string> MonthList = new Dictionary<int, string>()
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
            ddlMonth.DataSource = new BindingSource(MonthList, null);
            ddlMonth.DisplayMember = "Value";
            ddlMonth.ValueMember = "Key";
        }

        private void ddlYear_DataBing()
        {
            Dictionary<string, string> YearList = new Dictionary<string, string>();
            YearList.Add("<-Select->", "<-Select->");
            int currentYear = Convert.ToInt32(DateTime.Now.Year.ToString());
            for (int i = currentYear; i <= currentYear + 5; i++)
            {
                YearList.Add(i.ToString(), i.ToString());
            }
            ddlYear.DataSource = new BindingSource(YearList, null);
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
                DataGridViewRow row = this.dgMonthdetails.Rows[e.RowIndex];
                if (row != null)
                {                  
                    string DevoteeName = row.Cells["DevoteeName"].Value.ToString();
                    string Address = row.Cells["Address"].Value.ToString();
                    string ContactNumber = row.Cells["ContactNumber"].Value.ToString();
                    string Date = row.Cells["Date"].Value.ToString();

                    InsertDressDetails(Date,DevoteeName, Address, ContactNumber);
                    MessageBox.Show("You Have Selected " + ((e.RowIndex + 1).ToString() + DevoteeName + Address) + " Row Button");
                }
            }
            if (e.ColumnIndex == 7)
            {
                DataGridViewRow row = this.dgMonthdetails.Rows[e.RowIndex];
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
                cn = new SqlCeConnection(@"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf");
                cn.Open();
            }
            catch (SqlCeInvalidDatabaseFormatException ex)
            {
                string connStringCI = @"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf; LCID= 1033";
                string connStringCS = @"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf; LCID= 1033; Case Sensitive=true";

                SqlCeEngine engine = new SqlCeEngine(connStringCI);
                engine.Upgrade(connStringCS);

                cn = null;
                cn = new SqlCeConnection(connStringCI);
                cn.Open();
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
        #region DRESS DETAILS
        private void GetDressDetails()
        {
            try
            {
                if (ddlYear.SelectedValue != null && !string.IsNullOrWhiteSpace(ddlYear.SelectedValue.ToString()) && !ddlYear.SelectedValue.ToString().Contains("Select") &&
                      ddlMonth.SelectedValue != null && !string.IsNullOrWhiteSpace(ddlMonth.SelectedValue.ToString()) && !ddlMonth.SelectedValue.ToString().Contains("Select"))
                {
                    DataTable dataTable = new DataTable();
                    DataTable GridDetails = new DataTable();                   
                    OpenSqlCeConnection();
                    string strQuery = "SELECT * FROM  BABADRESSDETAILS  WHERE Month='" + ddlMonth.SelectedValue + "' OR Year='" + ddlYear.SelectedValue + "'";
                    SqlCeCommand cm = new SqlCeCommand(strQuery, cn);
                    cm.CommandText = strQuery;
                    cm.CommandType = CommandType.Text;
                    cm.Connection = cn;
                    var dataReader = cm.ExecuteReader();
                    dataTable.Load(dataReader);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        GridDetails = polulateGridDetails(dataTable);
                        //var rows = dataTable.Rows.Contains(startDate);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        private DataTable polulateGridDetails(DataTable Table)
        {
            int RowNo = 0;

            DataTable dataTable = new DataTable();
            DataRow dRow;
            dataTable.Columns.Add("SlNo", typeof(string));
            dataTable.Columns.Add("Date", typeof(string));
            dataTable.Columns.Add("Day", typeof(string));
           
            DateTime startDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue.ToString()), 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            for (var day = startDate.Date; day <= endDate; day = day.AddDays(1))
            {
                RowNo = RowNo + 1;
                dRow = dataTable.NewRow();
                dRow["SlNo"] = RowNo.ToString();
                dRow["Date"] = day.Date.ToString("dd/MMM/yyyy");
                dRow["Day"] = day.DayOfWeek.ToString();
                dataTable.Rows.Add(dRow);
            }
            dgMonthdetails.DataSource = dataTable;
            DataGridTextBoxColumn DevoteeName = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("DevoteeName", "Devotee Name");
            DevoteeName.Width = 400;
            DevoteeName.TextBox.Name = "DevoteeName";

            DataGridTextBoxColumn Address = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("Address", "Address");
            Address.Width = 600;
            Address.TextBox.Name = "Address";

            DataGridTextBoxColumn ContactNumber = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("ContactNumber", "Contact Number");
            ContactNumber.Width = 400;
            ContactNumber.TextBox.Name = "ContactNumber";

            DataGridViewButtonColumn btnSave = new DataGridViewButtonColumn();
            dgMonthdetails.Columns.Add(btnSave);
            btnSave.HeaderText = "Save";
            btnSave.Text = "Save";
            btnSave.Name = "btnSave";
            btnSave.UseColumnTextForButtonValue = true;
            dgMonthdetails.Refresh();

            DataGridViewButtonColumn btnClear = new DataGridViewButtonColumn();
            dgMonthdetails.Columns.Add(btnClear);
            btnClear.HeaderText = "Clear";
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
            return dataTable;
        }

        private void InsertDressDetails(string Date, string DevoteeName, string Address, string ContactNumber)
        {
            DateTime BookingDate = DateTime.Parse(Date);
            string  Month = BookingDate.Month.ToString(), Year = BookingDate.Year.ToString();
            string strInsertQuery = "INSERT INTO BABADRESSDETAILS(BOOKINGDATE,DEVOTEENAME,ADDRESS,CONTACTNUMBER,MONTH,YEAR)VALUES(@BOOKINGDATE,@DEVOTEENAME,@ADDRESS,@CONTACTNUMBER,@MONTH,@YEAR)";

           
            if (string.IsNullOrWhiteSpace(DevoteeName))
            {
                MessageBox.Show(@"Type Devotee Name", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (string.IsNullOrWhiteSpace(Address))
            {
                MessageBox.Show(@"Type Devotee Address", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (string.IsNullOrWhiteSpace(ContactNumber))
            {
                MessageBox.Show(@"Type ContactNumber", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            OpenSqlCeConnection();

            SqlCeCommand cm = new SqlCeCommand(strInsertQuery, cn);
            cm.Parameters.AddWithValue("@BOOKINGDATE", BookingDate);
            cm.Parameters.AddWithValue("@DEVOTEENAME", DevoteeName);
            cm.Parameters.AddWithValue("@ADDRESS", Address);
            cm.Parameters.AddWithValue("@CONTACTNUMBER", ContactNumber);
            cm.Parameters.AddWithValue("@MONTH", Month);
            cm.Parameters.AddWithValue("@YEAR", Year);            
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
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}