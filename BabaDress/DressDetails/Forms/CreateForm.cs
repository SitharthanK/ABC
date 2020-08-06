namespace DressDetails.Forms
{
    using DressDetails.Helper;
    using DressDetails.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="CreateForm" />.
    /// </summary>
    public partial class CreateForm : Form
    {
        /// <summary>
        /// Defines the _timer.
        /// </summary>
        internal Timer _timer;

        /// <summary>
        /// Defines the _conn, _userName.
        /// </summary>
        private readonly string _conn, _userName;

        /// <summary>
        /// Defines the userId, isAdmin.
        /// </summary>
        public readonly int userId, isAdmin;

        /// <summary>
        /// Defines the blnDataGridCreations.
        /// </summary>
        public bool blnDataGridCreations;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateForm"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="admin">The admin<see cref="int"/>.</param>
        public CreateForm(int id, string name, int admin)
        {
            InitializeComponent();
            ddlYear_DataBing();
            ddlMonth_DataBing();
            StartTimer();

            MaximizeBox = false;

            userId = id; _userName = name; isAdmin = admin;
            LblUsername.Text = @"Welcome.: " + _userName;
            blnDataGridCreations = false;

            if (isAdmin == 1)
                AddMenuAndItems();
        }

        /// <summary>
        /// The StartTimer.
        /// </summary>
        private void StartTimer()
        {
            _timer = new Timer { Interval = 1000 };
            _timer.Tick += tmr_Tick;
            _timer.Enabled = true;
        }

        /// <summary>
        /// The tmr_Tick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        internal void tmr_Tick(object sender, EventArgs e)
        {
            lblDateValue.Text = DateTime.Now.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// The AddMenuAndItems.
        /// </summary>
        private void AddMenuAndItems()
        {
            Menu = new MainMenu();
            MenuItem oFile = new MenuItem("File");
            MenuItem oEdit = new MenuItem("Edit");
            MenuItem oProfile = new MenuItem("Profile");

            /*FILE MENU ITEMS*/
            Menu.MenuItems.Add(oFile);
            oFile.MenuItems.Add("Exit", ExitApplication_click);

            /*EDIT MENU ITEMS*/
            Menu.MenuItems.Add(oEdit);
            oEdit.MenuItems.Add("Update", Update_Click);

            /*EDIT MENU ITEMS*/
            Menu.MenuItems.Add(oProfile);
            oProfile.MenuItems.Add("View Profile", Profile_Click);
        }

        /// <summary>
        /// The Update_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void Update_Click(object sender, EventArgs e)
        {
            EditForm obj = new EditForm(userId, _userName, isAdmin);
            Hide();
            obj.ShowDialog();
        }

        /// <summary>
        /// The Profile_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void Profile_Click(object sender, EventArgs e)
        {
            ProfileForm obj = new ProfileForm(userId, _userName, isAdmin);
            Hide();
            obj.ShowDialog();
        }

        /// <summary>
        /// The ExitApplication_click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void ExitApplication_click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Defines the _monthList.
        /// </summary>
        private readonly Dictionary<int, string> _monthList = new Dictionary<int, string>()
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

        /// <summary>
        /// The ddlMonth_DataBing.
        /// </summary>
        private void ddlMonth_DataBing()
        {
            ddlMonth.DataSource = new BindingSource(_monthList, null);
            ddlMonth.DisplayMember = "Value";
            ddlMonth.ValueMember = "Key";
        }

        /// <summary>
        /// The ddlYear_DataBing.
        /// </summary>
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

        /// <summary>
        /// The ddlMonth_SelectedValueChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void ddlMonth_SelectedValueChanged(object sender, EventArgs e)
        {
            GetDressDetails();
        }

        /// <summary>
        /// The ddlYear_SelectedValueChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void ddlYear_SelectedValueChanged(object sender, EventArgs e)
        {
            GetDressDetails();
        }

        /// <summary>
        /// The GetDressDetails.
        /// </summary>
        private void GetDressDetails()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ddlYear.SelectedValue.ToString()) && !ddlYear.SelectedValue.ToString().Contains("Select") &&
                    !string.IsNullOrWhiteSpace(ddlMonth.SelectedValue.ToString()) && !ddlMonth.SelectedValue.ToString().Contains("Select") && ddlMonth.SelectedValue.ToString() != "0")
                {
                    List<DevoteeDetails> lstDressDetails = SqlHelper.GetDressDetails(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
                    if (!blnDataGridCreations)
                    {
                        DataGridCreation();
                    }

                    PolulateGridDetails(lstDressDetails);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The DataGridCreation.
        /// </summary>
        private void DataGridCreation()
        {
            DataGridTextBoxColumn serlNo = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("SLNO", "Slno.");
            serlNo.TextBox.Name = "SLNO";
            serlNo.Alignment = HorizontalAlignment.Center;

            DataGridTextBoxColumn bookingDate = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("BOOKINGDATE", "Booking Date");
            bookingDate.TextBox.Name = "BOOKINGDATE";
            bookingDate.Alignment = HorizontalAlignment.Center;

            DataGridTextBoxColumn days = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("Days", "Days");
            days.TextBox.Name = "Days";
            days.Alignment = HorizontalAlignment.Left;

            DataGridTextBoxColumn devoteename = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("DEVOTEENAME", "Devotee Name");
            devoteename.TextBox.Name = "DEVOTEENAME";
            devoteename.TextBox.CharacterCasing = CharacterCasing.Upper;
            devoteename.Alignment = HorizontalAlignment.Left;

            DataGridTextBoxColumn address = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("ADDRESS", "Address");
            address.TextBox.Name = "ADDRESS";
            address.Alignment = HorizontalAlignment.Left;

            DataGridTextBoxColumn contactNumber = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("CONTACTNUMBER", "Contact Number");
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

            DataGridTextBoxColumn CreatedBy = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("CREATEDBY", "Created By");
            CreatedBy.TextBox.Name = "CREATEDBY";
            CreatedBy.Alignment = HorizontalAlignment.Left;

            DataGridTextBoxColumn CreatedOn = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("CREATEDON", "Created On");
            CreatedOn.TextBox.Name = "CREATEDON";
            CreatedOn.Alignment = HorizontalAlignment.Left;

            dgMonthdetails.Columns[0].Width = 50;
            dgMonthdetails.Columns[1].Width = 120;
            dgMonthdetails.Columns[2].Width = 100;
            dgMonthdetails.Columns[3].Width = 140;
            dgMonthdetails.Columns[4].Width = 250;
            dgMonthdetails.Columns[5].Width = 150;

            dgMonthdetails.Columns["SLNO"].ReadOnly = true;
            dgMonthdetails.Columns["BOOKINGDATE"].ReadOnly = true;
            dgMonthdetails.Columns["Days"].ReadOnly = true;
            dgMonthdetails.Columns["CREATEDBY"].ReadOnly = true;
            dgMonthdetails.Columns["CREATEDON"].ReadOnly = true;

            dgMonthdetails.Refresh();
            blnDataGridCreations = true;
        }

        /// <summary>
        /// The PolulateGridDetails.
        /// </summary>
        /// <param name="lstDressDetails">The lstDressDetails<see cref="List{DevoteeDetails}"/>.</param>
        private void PolulateGridDetails(List<DevoteeDetails> lstDressDetails)
        {
            dgMonthdetails.Rows.Clear();
            dgMonthdetails.Refresh();

            DateTime startDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue.ToString()), 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            for (var day = startDate.Date; day <= endDate; day = day.AddDays(1))
            {
                var row = lstDressDetails.Where(n => Convert.ToDateTime(n.BookDate).Date == day.Date).FirstOrDefault();

                if (row != null && Convert.ToDateTime(day).Date == Convert.ToDateTime(row.BookDate).Date)
                    dgMonthdetails.Rows.Add(row.RowNumber.ToString(), DateTime.Parse(row.BookDate).ToString("dd/MMM/yyyy"), day.DayOfWeek.ToString(), row.Name, row.Address, row.ContactNumber, "", "", row.CreatedBy, row.CreatedOn);
                else
                    dgMonthdetails.Rows.Add(row.RowNumber.ToString(), day.Date.ToString("dd/MMM/yyyy"), day.DayOfWeek.ToString(), "", "", "" ,"", "", "", "");
            }

            foreach (DataGridViewRow row in dgMonthdetails.Rows)
            {
                if (row.Index % 2 == 0)
                    row.DefaultCellStyle.BackColor = Color.LemonChiffon;
                else

                    row.DefaultCellStyle.BackColor = Color.White;

                row.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                row.DefaultCellStyle.ForeColor = Color.Black;

                row.DefaultCellStyle.Font = new Font("Tahoma", 9.75F);
                row.MinimumHeight = 50;
                row.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgMonthdetails.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
                dgMonthdetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dgMonthdetails.Refresh();
            dgMonthdetails.Focus();
        }

        /// <summary>
        /// The InsertDressDetails.
        /// </summary>
        /// <param name="date">The date<see cref="string"/>.</param>
        /// <param name="devoteeName">The devoteeName<see cref="string"/>.</param>
        /// <param name="address">The address<see cref="string"/>.</param>
        /// <param name="contactNumber">The contactNumber<see cref="string"/>.</param>
        private void InsertDressDetails(string date, string devoteeName, string address, string contactNumber)
        {
            SqlHelper.InsertDressDetails(date, devoteeName, address, contactNumber, userId);
        }

        /// <summary>
        /// The dgMonthdetails_CellContentClick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="DataGridViewCellEventArgs"/>.</param>
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

                    if (SqlHelper.ValidateBeforeCreate(date, month, year))
                        InsertDressDetails(date, devoteeName, address, contactNumber);
                    else
                        MessageBox.Show(@"Booked Failed - Recored Already Exists.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (e.ColumnIndex == 7)
            {
                DataGridViewRow row = dgMonthdetails.Rows[e.RowIndex];
                if (row != null)
                {
                    string date = row.Cells["BOOKINGDATE"].Value.ToString();
                    string month = Convert.ToDateTime(date).Month.ToString();
                    string year = Convert.ToDateTime(date).Year.ToString();

                    if (SqlHelper.ValidateBeforeCreate(date, month, year))
                    {
                        row.Cells["DEVOTEENAME"].Value = "";
                        row.Cells["ADDRESS"].Value = "";
                        row.Cells["CONTACTNUMBER"].Value = "";
                    }
                    else
                        MessageBox.Show(@"Clear Failed - Devotee Details cannot be edited!.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
