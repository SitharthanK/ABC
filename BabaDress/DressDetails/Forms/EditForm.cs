﻿using DressDetails.Helper;
using DressDetails.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace DressDetails.Forms
{
    public partial class EditForm : Form
    {
        #region PROPERTY
        Timer _timer;
        private readonly string _userName;
        public readonly int userId, isAdmin;
        public bool blnDataGridCreations;
        #endregion

        #region CONSTRUCTOR
        public EditForm(int id, string name, int admin)
        {
            InitializeComponent();
            ddlYear_DataBing();
            ddlMonth_DataBing();
            StartTimer();

            MaximizeBox = false;
            blnDataGridCreations = false;
            userId = id; _userName = name; isAdmin = admin;
            lblUsername.Text = @"Welcome.: " + _userName;

            if (isAdmin == 1)
                AddMenuAndItems();
        }
        #endregion

        #region MENU DETAILS

        private void AddMenuAndItems()
        {
            Menu = new MainMenu();
            MenuItem oFile = new MenuItem("File");
            MenuItem oCreate = new MenuItem("Create");
            MenuItem oProfile = new MenuItem("Profile");

            /*FILE MENU ITEMS*/
            Menu.MenuItems.Add(oFile);
            oFile.MenuItems.Add("Exit", ExitApplication_click);

            /*CREATE MENU ITEMS*/
            Menu.MenuItems.Add(oCreate);
            oCreate.MenuItems.Add("Create", Create_Click);

            /*PROFILE MENU ITEMS*/
            Menu.MenuItems.Add(oProfile);
            oProfile.MenuItems.Add("View Profile", Profile_Click);
        }

        private void Create_Click(object sender, EventArgs e)
        {
            CreateForm obj = new CreateForm(userId, _userName, isAdmin);
            Hide();
            obj.ShowDialog();
        }

        private void Profile_Click(object sender, EventArgs e)
        {
            ProfileForm obj = new ProfileForm(userId, _userName, isAdmin);
            Hide();
            obj.ShowDialog();
        }
        private void ExitApplication_click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region TIMER VALIDATIONS

        private void StartTimer()
        {
            _timer = new Timer { Interval = 1000 };
            _timer.Tick += tmr_Tick;
            _timer.Enabled = true;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            lblDateValue.Text = DateTime.Now.ToString(CultureInfo.CurrentCulture);
        }
        #endregion

        #region DROP DOWN LIST - MONTH & YEAR
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

        #endregion

        #region DRESS DETAILS
        private void GetDressDetails()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ddlYear.SelectedValue.ToString()) && !ddlYear.SelectedValue.ToString().Contains("Select") &&
                    !string.IsNullOrWhiteSpace(ddlMonth.SelectedValue.ToString()) && !ddlMonth.SelectedValue.ToString().Contains("Select") && ddlMonth.SelectedValue.ToString() != "0")
                {
                    List<DevoteeDetails> lstDressDetails = SqlHelper.GetDressDetails(Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));

                    if (!blnDataGridCreations)
                        DataGridCreation();
                    PolulateGridDetails(lstDressDetails);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridCreation()
        {
            DataGridTextBoxColumn serlNo = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("SLNO", "Slno.");
            serlNo.ReadOnly = true;
            serlNo.TextBox.Name = "SLNO";
            serlNo.Alignment = HorizontalAlignment.Center;

            DataGridTextBoxColumn bookingDate = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("BOOKINGDATE", "Booking Date");
            bookingDate.ReadOnly = true;
            bookingDate.TextBox.Name = "BOOKINGDATE";
            bookingDate.Alignment = HorizontalAlignment.Center;

            DataGridTextBoxColumn days = new DataGridTextBoxColumn();
            dgMonthdetails.Columns.Add("Days", "Days");
            days.ReadOnly = true;
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
            btnSave.HeaderText = @"Update";
            btnSave.Text = "Update";
            btnSave.Name = "btnSave";
            btnSave.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn btnClear = new DataGridViewButtonColumn();
            dgMonthdetails.Columns.Add(btnClear);
            btnClear.HeaderText = @"Clear";
            btnClear.Text = "Clear";
            btnClear.Name = "btnClear";
            btnClear.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            dgMonthdetails.Columns.Add(btnDelete);
            btnDelete.HeaderText = @"Delete";
            btnDelete.Text = "Delete";
            btnDelete.Name = "btnDelete";
            btnDelete.UseColumnTextForButtonValue = true;

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
                    dgMonthdetails.Rows.Add(row.RowNumber.ToString(), DateTime.Parse(row.BookDate).ToString("dd/MMM/yyyy"), day.DayOfWeek.ToString(), row.Name, row.Address, row.ContactNumber, "", "","", row.CreatedBy, row.CreatedOn);
                else
                    dgMonthdetails.Rows.Add(row.RowNumber.ToString(), day.Date.ToString("dd/MMM/yyyy"), day.DayOfWeek.ToString(), "", "", "", "", "", "","","");
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
                    DateTime bookingDate = DateTime.Parse(row.Cells["BOOKINGDATE"].Value.ToString());

                    if (SqlHelper.ValidateBeforeEdit(date, bookingDate.Month.ToString(), bookingDate.Year.ToString()))
                        SqlHelper.UpdateDressDetails(date, devoteeName, address, contactNumber,userId);
                    else
                        MessageBox.Show(@"No bookings were found", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (e.ColumnIndex == 8)
            {
                DataGridViewRow row = dgMonthdetails.Rows[e.RowIndex];
                string date = row.Cells["BOOKINGDATE"].Value.ToString();
                DateTime bookingDate = DateTime.Parse(row.Cells["BOOKINGDATE"].Value.ToString());

                string message = "Do you want to delete this record?";
                string title = "Delete";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);

                if (SqlHelper.ValidateBeforeEdit(date, bookingDate.Month.ToString(), bookingDate.Year.ToString()))
                {
                    if (result == DialogResult.Yes)
                        SqlHelper.DeleteDressDetails(date);
                }
                else
                    MessageBox.Show(@"No bookings were found", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
            

        //private bool CheckIfRecordExistsDb(string bookingDate, string month, string year)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(month) && !string.IsNullOrWhiteSpace(year))
        //        {
        //            _con = new SqlConnection(_conn);
        //            _con.Open();
        //            string strQuery = @"SELECT * FROM  DRESSDETAILS  WHERE BookedMonth='" + month + "' AND BookedYear='" + year + "'";
        //            SqlCommand cm = new SqlCommand(strQuery, _con)
        //            {
        //                CommandText = strQuery,
        //                CommandType = CommandType.Text,
        //                Connection = _con
        //            };
        //            var dataReader = cm.ExecuteReader();
        //            DataTable dataTable = new DataTable();
        //            dataTable.Load(dataReader);
        //            if (dataTable != null && dataTable.Rows.Count > 0)
        //            {
        //                int count = (from DataRow dr in dataTable.Rows
        //                             where (dr["BOOKEDDATE"] != null && Convert.ToDateTime(dr["BOOKEDDATE"]).Date == Convert.ToDateTime(bookingDate).Date)
        //                             select new DevoteeDetails()
        //                             {
        //                                 BookDate = Convert.ToString(dr["BOOKEDDATE"]),
        //                             }).Count();
        //                if (count == 1)
        //                    return true;
        //            }
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    finally
        //    {
        //        _con.Close();
        //    }
        //}       
        #endregion
    }
}
