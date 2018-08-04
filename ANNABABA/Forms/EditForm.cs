using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Windows.Forms;

namespace ANNABABA.Forms
{
    public partial class EditForm : Form
    {
        private SqlCeConnection _con = new SqlCeConnection(@"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf");
        private int _intCount;
        private bool OnPageload { get; set; }

        private int _intCountryCode;
        private int _intStateCode;
        private int _intCityCode;

        private List<EditCountryDetails> _editCountryList = new List<EditCountryDetails>();
        private List<EditStateDetails> _editStateList = new List<EditStateDetails>();
        private List<EditCityDetails> _editCityList = new List<EditCityDetails>();


        private Timer _timer;

        #region PAGE LOAD

        public EditForm()
        {
            OnPageload = true;
            MaximizeBox = false;
            InitializeComponent();
            StartTimer();
            GetEditCountryData();
            PaymentModeDetails();

            dtAnadhanamDate.Format = DateTimePickerFormat.Custom;
            dtAnadhanamDate.CustomFormat = @"dd-MMM-yyyy";

            dtChequeDate.Format = DateTimePickerFormat.Custom;
            dtChequeDate.CustomFormat = @"dd-MMM-yyyy";

            dtAnadhanamDate.Value = DateTime.Now.AddMonths(4);

            dtAnadhanamDate.MaxDate = DateTime.Now.AddMonths(4).Date;
            dtChequeDate.MaxDate = DateTime.Now.Date;

            OnPageload = false;
        }

        #endregion

        #region TIMER VALIDATIONS

        private void StartTimer()
        {
            _timer = new Timer {Interval = 1000};
            _timer.Tick += tmr_Tick;
            _timer.Enabled = true;
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            lblDateValue.Text = DateTime.Now.ToString();
        }

        #endregion

        #region PAYMENT MODE

        private static Dictionary<int, string> PaymentModeList = new Dictionary<int, string>()
        {
            {1, "Cash"},
            {2, "Cheque"},
        };

        private void PaymentModeDetails()
        {
            cmbPaymentMode.DataSource = new BindingSource(PaymentModeList, null);
            cmbPaymentMode.DisplayMember = "Value";
            cmbPaymentMode.ValueMember = "Key";
            cmbPaymentMode.SelectedValue = 1;

            txtChequeNumber.Enabled = false;
            txtDrawnOn.Enabled = false;
            dtChequeDate.Enabled = false;
        }

        #endregion

        #region ANNADHANAM COUNT DETAILS

        private void GetAnnadhanamCountDetails()
        {
            try
            {
                var dtAnadhanamDates = new DateTime(dtAnadhanamDate.Value.Year, dtAnadhanamDate.Value.Month,
                    dtAnadhanamDate.Value.Day, 12, 0, 0);
                OpenSqlCeConnection();
                var strCurrentDate = Convert.ToString(dtAnadhanamDates);
                var strSelectQuery =
                    "SELECT COUNT(ReceiptNumber) AS CNT FROM  tblAnnadhanamDetails WHERE  (ReceiptNumber <>'" +
                    txtReceiptNumber.Text.ToString() + "' AND AnadhanamDate='" + strCurrentDate + "')";
                var cm = new SqlCeCommand(strSelectQuery, _con)
                {
                    CommandText = strSelectQuery,
                    CommandType = CommandType.Text,
                    Connection = _con
                };

                var reader = cm.ExecuteReader();

                try
                {
                    if (reader != null)
                        while (reader.Read())
                        {
                            var strAnnadhanamDayCount = $"{reader["CNT"]}";
                            if (!string.IsNullOrEmpty(strAnnadhanamDayCount))
                                _intCount = Convert.ToInt32(strAnnadhanamDayCount);
                            else
                                _intCount = 0;
                        }
                }
                finally
                {
                    reader?.Close();
                }

                _con.Close();
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

        #region EDIT NUMBER DETAILS

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReceiptNumber.Text))
            {
                txtReceiptNumber.Enabled = false;
                GetData(txtReceiptNumber.Text);
            }
            else
            {
                MessageBox.Show(@"Enter Valid Receipt Number", Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void GetData(string strReceiptNumber)
        {
            try
            {
                var strQuery =
                    "SELECT ReceiptNumber,DevoteeName,Address,Country,State,City,Amount,ReceiptCreatedDate,AnadhanamDate,ChequeNo,ChequeDate,ChequeDrawn,Mode,ContactNumber FROM  tblAnnadhanamDetails WHERE ReceiptNumber='" +
                    strReceiptNumber + "' OR ContactNumber='" + strReceiptNumber + "'";
                OpenSqlCeConnection();

                using (_con)
                {
                    using (var da = new SqlCeDataAdapter())
                    {
                        var cmd = new SqlCeCommand(strQuery, _con)
                        {
                            Connection = _con
                        };
                        da.SelectCommand = cmd;
                        var ds = new DataSet();
                        da.Fill(ds);
                        _con.Close();

                        var dt = ds.Tables[0].Clone();

                        if (ds.Tables[0]?.Rows.Count > 0)
                        {
                            var lastRow = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1];
                            dt.Rows.Add(lastRow.ItemArray);

                            foreach (DataRow dr in dt.Rows)
                            {
                                txtReceiptNumber.Text = Convert.ToString(dr["ReceiptNumber"]);
                                txtName.Text = Convert.ToString(dr["DevoteeName"]);
                                txtAddress.Text = Convert.ToString(dr["Address"]);
                                var strCity = Convert.ToString(dr["City"]);
                                dtAnadhanamDate.Value = Convert.ToDateTime(dr["AnadhanamDate"]).Date;
                                txtMobileNumber.Text = Convert.ToString(dr["ContactNumber"]);

                                if (Convert.ToString(dr["State"]) != null)
                                {
                                    var stateValues = (from p in _editStateList
                                        where p.StateName.Contains(Convert.ToString(dr["State"]).Trim())
                                        select p.StateId).First();
                                    cmbState.SelectedValue = Convert.ToString(stateValues);
                                }

                                var city = from p in _editCityList
                                    where p.CityName.Contains(strCity.Trim())
                                    select p.CityId;
                                if (city?.Count() > 0)
                                {
                                    var cityValues = (from p in _editCityList
                                        where p.CityName.Contains(Convert.ToString(dr["City"]).Trim())
                                        select p.CityId).First();
                                    _intCityCode = Convert.ToInt32(cityValues);
                                    cmbCity.SelectedValue = Convert.ToString(_intCityCode);
                                }
                                else
                                {
                                    _intCityCode = 0;
                                    _editCityList = new List<EditCityDetails>
                                    {
                                        new EditCityDetails
                                        {
                                            CountryId = _intCountryCode.ToString(),
                                            StateId = _intStateCode.ToString(),
                                            CityId = _intCityCode.ToString(),
                                            CityName = Convert.ToString("<--No Records-->"),
                                        }
                                    };
                                    cmbCity.DataSource = new BindingSource(_editCityList, null);
                                    cmbCity.DisplayMember = "CityName";
                                    cmbCity.ValueMember = "CityID";
                                    cmbCity.SelectedValue = _editCityList.FirstOrDefault()?.CityId;
                                }

                                var paymentModeValues = (from p in PaymentModeList
                                    where p.Value.Contains(Convert.ToString(dr["Mode"]))
                                    select p.Key).First();
                                cmbPaymentMode.SelectedValue = Convert.ToInt32(paymentModeValues);
                                txtChequeNumber.Text = Convert.ToString(dr["ChequeNo"]);
                                if (dr["ChequeDate"] != null && dr["ChequeDate"].ToString() != "")
                                    dtChequeDate.Value = Convert.ToDateTime(dr["ChequeDate"]).Date;
                                txtDrawnOn.Text = Convert.ToString(dr["ChequeDrawn"]);

                                if (cmbPaymentMode?.SelectedValue.ToString() == "2")
                                {
                                    txtChequeNumber.Enabled = true;
                                    txtDrawnOn.Enabled = true;
                                    dtChequeDate.Enabled = true;
                                }
                                else
                                {
                                    txtChequeNumber.Enabled = false;
                                    txtDrawnOn.Enabled = false;
                                    dtChequeDate.Enabled = false;
                                }

                                break;
                            }
                        }
                        else
                        {
                            MessageBox.Show(@"Enter Valid Receipt / Mobile Number", Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtReceiptNumber.Enabled = true;
                        }

                        da.Dispose();
                        ds.Dispose();
                    }
                }
            }
            finally
            {
                _con.Close();
            }
        }

        #endregion

        #region UPDATE PRINT

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string strPaymentMode,
                strState = string.Empty,
                strCountry = string.Empty,
                strName = string.Empty,
                strContactNumber = string.Empty,
                strAddress = string.Empty;
            string strCity = string.Empty, strChequeNumber = string.Empty, strChequeDrawn = string.Empty;
            int intReceiptNumber;
            GetAnnadhanamCountDetails();

            bool blnSubmit;
            var dtAnandhanDate = dtAnadhanamDate.Value.Date;
            if (dtAnadhanamDate.Value.Date >= Convert.ToDateTime("22-JAN-2016").Date)
            {
                if (_intCount < 15)
                {
                    #region VALIDATIONS

                    if (!string.IsNullOrEmpty(txtReceiptNumber.Text))
                    {
                        intReceiptNumber = Convert.ToInt32(txtReceiptNumber.Text);
                        blnSubmit = true;
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Enter Valid Receipt number !...", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    if (txtName.Text != null && txtName.Text.Trim() != "")
                    {
                        strName = txtName.Text;
                        blnSubmit = true;
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Type Name & Proceed !...", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    if (txtAddress.Text != null && txtAddress.Text.Trim() != "")
                    {
                        blnSubmit = true;
                        strAddress = txtAddress.Text;
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Type Address & Proceed !...", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    if (_intCityCode != 0 && cmbCity.SelectedItem != null)
                    {
                        var selectedCity = (EditCityDetails) cmbCity.SelectedItem;
                        strCity = Convert.ToString(selectedCity.CityName);
                        blnSubmit = true;
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Choose City & Proceed !...", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    if (dtAnandhanDate.Day == 1 && dtAnandhanDate.Month == 1)
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Anadhanam cannot be made on this day,please choose other date !...",
                            @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (dtAnandhanDate.Date > DateTime.Now.AddMonths(4).Date)
                    {
                        blnSubmit = false;
                        dtAnadhanamDate.Value = DateTime.Now.AddMonths(4).Date;
                        var strMessage = "Anadhanam date must not exceed more than 4 months (" +
                                            DateTime.Now.AddMonths(4).Date.ToString("dd-MMM-yyyy") + ")!...";
                        MessageBox.Show(strMessage, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (!string.IsNullOrEmpty(cmbCountry.Text))
                    {
                        blnSubmit = true;
                        strCountry = cmbCountry.Text;
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Select Country!...", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    if (!string.IsNullOrEmpty(cmbState.Text))
                    {
                        blnSubmit = true;
                        strState = cmbState.Text;
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Select State!...", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    if (!string.IsNullOrEmpty(cmbPaymentMode.Text))
                    {
                        strPaymentMode = cmbPaymentMode.Text;

                        if (strPaymentMode == "Cheque")
                        {
                            if (txtChequeNumber.Text != null && txtChequeNumber.Text.Trim() != "")
                            {
                                strChequeNumber = txtChequeNumber.Text.Trim();
                                blnSubmit = true;
                            }
                            else
                            {
                                blnSubmit = false;
                                MessageBox.Show(@"Please Type Cheque Number & Proceed !...", @"Information",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (txtDrawnOn.Text != null && txtDrawnOn.Text.Trim() != "")
                            {
                                strChequeDrawn = txtDrawnOn.Text.Trim();
                                blnSubmit = true;
                            }
                            else
                            {
                                blnSubmit = false;
                                MessageBox.Show(@"Please Type Cheque Number & Proceed !...", @"Information",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            blnSubmit = true;
                        }
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Select Payment Mode!...", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtMobileNumber.Text) && txtMobileNumber.Text.Length < 10 ||
                        txtMobileNumber.Text.Length > 10)
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Contact Number should contain 10 digits !..", @"Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (!string.IsNullOrEmpty(txtMobileNumber.Text) && txtMobileNumber.Text.Length == 10)
                    {
                        blnSubmit = true;
                        strContactNumber = txtMobileNumber.Text.ToString();
                    }

                    #endregion

                    #region SUBMIT & PRINT

                    if (blnSubmit)
                    {
                        DateTime? dtChequeDates = null;
                        if (strPaymentMode.ToUpper() == "CHEQUE")
                            dtChequeDates = dtChequeDate.Value.Date;
                        var dtAnadhanamDates = new DateTime(dtAnadhanamDate.Value.Year,
                            dtAnadhanamDate.Value.Month, dtAnadhanamDate.Value.Day, 12, 0, 0);

                        OpenSqlCeConnection();
                        var strUpdateQuery = "UPDATE tblAnnadhanamDetails Set DevoteeName='" + strName.Trim() +
                                                "',Address='" + strAddress.Trim() + "',CountryCode=" + _intCountryCode +
                                                ",Country='" + strCountry + "',StateCode=" + _intStateCode +
                                                ",State='" + strState + "',CityCode=" + _intCityCode + ",City='" +
                                                strCity + "',AnadhanamDate='" + dtAnadhanamDates + "',ChequeNo='" +
                                                strChequeNumber + "',ChequeDate='" + dtChequeDates + "',ChequeDrawn='" +
                                                strChequeDrawn + "',Mode='" + strPaymentMode + "',ContactNumber='" +
                                                strContactNumber + "' WHERE ReceiptNumber='" +
                                                intReceiptNumber.ToString() + "'";

                        var strFullAddress = strAddress + ", " + strCity + "," + strState + "," + strCountry + ".";

                        var cm = new SqlCeCommand(strUpdateQuery, _con);
                        try
                        {
                            var intAffectedRow = cm.ExecuteNonQuery();
                            if (intAffectedRow > 0)
                            {
                                MessageBox.Show(@"Anadhanam Updated Sucessfully", Application.ProductName,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                var obj = new ReceiptReport(intReceiptNumber.ToString(), strName,
                                    strFullAddress, dtAnadhanamDates, strChequeNumber, dtChequeDate.Value.Date,
                                    strChequeDrawn, strPaymentMode);
                                Clear();
                                obj.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show(@"Updation Failed", Application.ProductName, MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }

                            cm.ExecuteNonQuery();
                            _con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }

                    #endregion
                }
                else
                {
                    MessageBox.Show(
                        @"We have already issued 15 receipt for the Anadhanam Date (" +
                        dtAnadhanamDate.Value.ToString("dd-MMM-yyyy") + @") Please Choose Another Date!...",
                        @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (_intCount < 10)
                {
                    #region VALIDATIONS

                    if (!string.IsNullOrEmpty(txtReceiptNumber.Text))
                    {
                        intReceiptNumber = Convert.ToInt32(txtReceiptNumber.Text);
                        blnSubmit = true;
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Type Name & Proceed !...", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    if (txtName.Text != null && txtName.Text.Trim() != "")
                    {
                        strName = txtName.Text;
                        blnSubmit = true;
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Type Name & Proceed !...", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    if (txtAddress.Text != null && txtAddress.Text.Trim() != "")
                    {
                        blnSubmit = true;
                        strAddress = txtAddress.Text;
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Type Address & Proceed !...", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    if (_intCityCode != 0 && cmbCity.SelectedItem != null)
                    {
                        var selectedCity = (EditCityDetails) cmbCity.SelectedItem;
                        strCity = Convert.ToString(selectedCity.CityName);
                        blnSubmit = true;
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Choose City & Proceed !...", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }


                    if (dtAnandhanDate.Day == 1 && dtAnandhanDate.Month == 1)
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Anadhanam cannot be made on this day,please choose other date !...",
                            @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (dtAnandhanDate.Date > DateTime.Now.AddMonths(4).Date)
                    {
                        blnSubmit = false;
                        dtAnadhanamDate.Value = DateTime.Now.AddMonths(4).Date;
                        var strMessage = "Anadhanam date must not exceed more than 4 months (" +
                                            DateTime.Now.AddMonths(4).Date.ToString("dd-MMM-yyyy") + ")!...";
                        MessageBox.Show(strMessage, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (!string.IsNullOrEmpty(cmbCountry.Text))
                    {
                        blnSubmit = true;
                        strCountry = cmbCountry.Text;
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Select Country!...", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    if (!string.IsNullOrEmpty(cmbState.Text))
                    {
                        blnSubmit = true;
                        strState = cmbState.Text;
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Select State!...", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    if (!string.IsNullOrEmpty(cmbPaymentMode.Text))
                    {
                        strPaymentMode = cmbPaymentMode.Text;

                        if (strPaymentMode == "Cheque")
                        {
                            if (txtChequeNumber.Text != null && txtChequeNumber.Text.Trim() != "")
                            {
                                strChequeNumber = txtChequeNumber.Text.Trim();
                                blnSubmit = true;
                            }
                            else
                            {
                                blnSubmit = false;
                                MessageBox.Show(@"Please Type Cheque Number & Proceed !...", @"Information",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (txtDrawnOn.Text != null && txtDrawnOn.Text.Trim() != "")
                            {
                                strChequeDrawn = txtDrawnOn.Text.Trim();
                                blnSubmit = true;
                            }
                            else
                            {
                                blnSubmit = false;
                                MessageBox.Show(@"Please Type Cheque Number & Proceed !...", @"Information",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            blnSubmit = true;
                        }
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Select Payment Mode!...", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtMobileNumber.Text) && txtMobileNumber.Text.Length < 10 ||
                        txtMobileNumber.Text.Length > 10)
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Contact Number should contain 10 digits !..", @"Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (!string.IsNullOrEmpty(txtMobileNumber.Text) && txtMobileNumber.Text.Length == 10)
                    {
                        blnSubmit = true;
                        strContactNumber = txtMobileNumber.Text.ToString();
                    }

                    #endregion

                    #region SUBMIT & PRINT

                    if (blnSubmit)
                    {
                        DateTime? dtChequeDates = null;
                        if (strPaymentMode.ToUpper() == "CHEQUE")
                            dtChequeDates = dtChequeDate.Value.Date;

                        var dtAnadhanamDates = new DateTime(dtAnadhanamDate.Value.Year,
                            dtAnadhanamDate.Value.Month, dtAnadhanamDate.Value.Day, 12, 0, 0);

                        OpenSqlCeConnection();
                        var strUpdateQuery = "UPDATE tblAnnadhanamDetails Set DevoteeName='" + strName.Trim() +
                                                "',Address='" + strAddress.Trim() + "',CountryCode=" + _intCountryCode +
                                                ",Country='" + strCountry + "',StateCode=" + _intStateCode +
                                                ",State='" + strState + "',CityCode=" + _intCityCode + ",City='" +
                                                strCity + "',AnadhanamDate='" + dtAnadhanamDates + "',ChequeNo='" +
                                                strChequeNumber + "',ChequeDate='" + dtChequeDates + "',ChequeDrawn='" +
                                                strChequeDrawn + "',Mode='" + strPaymentMode + "',ContactNumber='" +
                                                strContactNumber + "' WHERE ReceiptNumber='" +
                                                intReceiptNumber.ToString() + "'";

                        var strFullAddress = strAddress + ", " + strCountry + ", " + strState + ", " + strCity + ".";

                        var cm = new SqlCeCommand(strUpdateQuery, _con);
                        try
                        {
                            var intAffectedRow = cm.ExecuteNonQuery();
                            if (intAffectedRow > 0)
                            {
                                MessageBox.Show(@"Anadhanam Updated Sucessfully", Application.ProductName,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                var obj = new ReceiptReport(intReceiptNumber.ToString(), strName,
                                    strFullAddress, dtAnadhanamDates, strChequeNumber, dtChequeDate.Value.Date,
                                    strChequeDrawn, strPaymentMode);
                                Clear();
                                obj.Show();
                            }
                            else
                            {
                                MessageBox.Show(@"Updation Failed", Application.ProductName, MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }

                            cm.ExecuteNonQuery();
                            _con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }

                    #endregion
                }
                else
                {
                    MessageBox.Show(
                        @"We have already issued 10 receipt for the Anadhanam Date (" +
                        dtAnadhanamDate.Value.ToString("dd-MMM-yyyy") + @") Please Choose Another Date!...",
                        @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion

        #region CLEAR

        private void Clear()
        {
            txtReceiptNumber.Enabled = false;
            OnPageload = true;
            GetEditCountryData();
            txtAddress.Text = "";
            txtName.Text = "";
            txtChequeNumber.Text = "";
            txtDrawnOn.Text = "";

            txtMobileNumber.Text = "";
            txtReceiptNumber.Text = "";
            txtReceiptNumber.Enabled = true;
            dtAnadhanamDate.Value = DateTime.Now.AddMonths(4).Date;
            dtChequeDate.Value = DateTime.Now.Date;
            OnPageload = false;
        }


        private void BtnClear_Click(object sender, EventArgs e)
        {
            OnPageload = true;
            txtReceiptNumber.Enabled = true;
            GetEditCountryData();
            PaymentModeDetails();

            txtAddress.Text = "";
            txtName.Text = "";
            txtChequeNumber.Text = "";
            txtDrawnOn.Text = "";

            txtMobileNumber.Text = "";
            txtReceiptNumber.Text = "";
            dtAnadhanamDate.Value = DateTime.Now.AddMonths(4).Date;
            dtChequeDate.Value = DateTime.Now.Date;
            OnPageload = false;
        }

        #endregion

        #region AVAILABILITY CHECK

        private void Availabality_Click(object sender, EventArgs e)
        {
            var obj = new Availability(dtAnadhanamDate.Value.Date);
            obj.ShowDialog();
        }

        #endregion

        #region ACCEPT NUMBERS ONLY

        private void txtReceiptNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        #endregion

        #region PAYMENT MODE SELECTION

        private void cmbPaymentMode_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbPaymentMode.Text == @"Cash")
            {
                txtChequeNumber.Enabled = false;
                txtDrawnOn.Enabled = false;
                dtChequeDate.Enabled = false;
            }
            else
            {
                txtChequeNumber.Enabled = true;
                txtDrawnOn.Enabled = true;
                dtChequeDate.Enabled = true;
                dtChequeDate.Value = DateTime.Now.Date;
            }
        }

        #endregion

        #region COUNTRY DETAILS

        private void GetEditCountryData()
        {
            OpenSqlCeConnection();
            var strQuery = "SELECT CountryId,CountyName,IsActive FROM  CountryDetails WHERE IsActive='Y'";

            try
            {
                var cm = new SqlCeCommand(strQuery, _con)
                {
                    CommandText = strQuery,
                    CommandType = CommandType.Text,
                    Connection = _con
                };

                var dataReader = cm.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);

                if (dataTable?.Rows.Count > 0)
                {
                    _editCountryList = (from DataRow dr in dataTable.Rows
                        select new EditCountryDetails()
                        {
                            CountryId = Convert.ToString(dr["CountryId"]),
                            CountryName = Convert.ToString(dr["CountyName"]),
                            IsActive = Convert.ToString(dr["IsActive"])
                        }).ToList();

                    var countryCode = (from p in _editCountryList
                        where p.CountryName.Trim().ToUpper().Contains("INDIA")
                        select p.CountryId).First();
                    _intCountryCode = Convert.ToInt32(countryCode);

                    cmbCountry.DataSource = new BindingSource(_editCountryList, null);
                    cmbCountry.DisplayMember = "CountryName";
                    cmbCountry.ValueMember = "CountryID";
                    cmbCountry.SelectedValue = Convert.ToString(_intCountryCode);

                    if (_editCountryList != null && _editCountryList.Any())
                    {
                        cmbState.Enabled = true;
                        GetEditStateData();
                    }
                    else
                    {
                        _intCountryCode = 0;
                        _editCountryList = new List<EditCountryDetails>
                        {
                            new EditCountryDetails
                            {
                                CountryId = _intCountryCode.ToString(),
                                CountryName = "<--No Records-->".ToString()
                            }
                        };

                        cmbCountry.DataSource = new BindingSource(_editCountryList, null);
                        cmbCountry.DisplayMember = "CountryName";
                        cmbCountry.ValueMember = "CountryID";
                        cmbCountry.SelectedValue = _editCountryList.FirstOrDefault()?.CountryName.ToString();

                        cmbState.Enabled = false;
                        cmbCity.Enabled = false;
                    }
                }
                else
                {
                    _intCountryCode = 0;
                    _editCountryList = new List<EditCountryDetails>
                    {
                        new EditCountryDetails
                        {
                            CountryId = _intCountryCode.ToString(),
                            CountryName = Convert.ToString("<--No Records-->")
                        }
                    };

                    cmbCountry.DataSource = new BindingSource(_editCountryList, null);
                    cmbCountry.DisplayMember = "CountryName";
                    cmbCountry.ValueMember = "CountryID";
                    cmbCountry.SelectedValue = _editCountryList.FirstOrDefault()?.CountryId;

                    cmbState.Enabled = false;
                    cmbCity.Enabled = false;
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

        #endregion

        #region STATE DETAILS

        private void GetEditStateData()
        {
            if (_intCountryCode != 0)
            {
                cmbState.Enabled = true;
                cmbCity.Enabled = false;

                OpenSqlCeConnection();
                try
                {
                    var strQuery = "Select CountryId,StateId,StateName,IsActive from StateDetails WHERE CountryId=" +
                                      _intCountryCode + " AND IsActive='Y' ORDER BY StateName ASC ";
                    var cm = new SqlCeCommand(strQuery, _con)
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.Text,
                        Connection = _con
                    };

                    var dataReader = cm.ExecuteReader();
                    var dataTable = new DataTable();
                    dataTable.Load(dataReader);

                    if (dataTable?.Rows.Count > 0)
                    {
                        _editStateList = (from DataRow dr in dataTable.Rows
                            select new EditStateDetails()
                            {
                                CountryId = Convert.ToString(dr["CountryId"]),
                                StateId = Convert.ToString(dr["StateId"]),
                                StateName = Convert.ToString(dr["StateName"]),
                                IsActive = Convert.ToString(dr["IsActive"])
                            }).ToList();

                        cmbState.DataSource = new BindingSource(_editStateList, null);
                        cmbState.DisplayMember = "StateName";
                        cmbState.ValueMember = "StateID";
                        cmbState.SelectedValue = Convert.ToString(_intStateCode);

                        if (OnPageload)
                        {
                            var stateCode = (from p in _editStateList
                                where p.StateName.Trim().ToUpper().Contains("TAMIL NADU")
                                select p.StateId).First();
                            _intStateCode = Convert.ToInt32(stateCode);
                            cmbState.SelectedValue = Convert.ToString(_intStateCode);

                            if (_intStateCode != 0 && _editStateList != null && _editStateList.Any())
                                GetEditCityData();
                        }
                        else
                        {
                            if (_intStateCode != 0 && _editStateList != null && _editStateList.Any())
                                GetEditCityData();
                        }
                    }
                    else
                    {
                        _intStateCode = 0;
                        _editStateList = new List<EditStateDetails>
                        {
                            new EditStateDetails
                            {
                                CountryId = _intCountryCode.ToString(),
                                StateId = _intStateCode.ToString(),
                                StateName = Convert.ToString("<--No Records-->")
                            }
                        };
                        cmbState.DataSource = new BindingSource(_editStateList, null);
                        cmbState.DisplayMember = "StateName";
                        cmbState.ValueMember = "StateID";
                        cmbState.SelectedValue = _editStateList.FirstOrDefault()?.StateId;
                        cmbCity.Enabled = false;

                        _intCityCode = 0;
                        _editCityList = new List<EditCityDetails>
                        {
                            new EditCityDetails
                            {
                                CountryId = _intCountryCode.ToString(),
                                StateId = _intStateCode.ToString(),
                                CityId = _intCityCode.ToString(),
                                CityName = Convert.ToString("<--No Records-->")
                            }
                        };
                        cmbCity.DataSource = new BindingSource(_editCityList, null);
                        cmbCity.DisplayMember = "CityName";
                        cmbCity.ValueMember = "CityID";
                        cmbCity.SelectedValue = _editCityList.FirstOrDefault()?.CityId;
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
            else
            {
                cmbState.Enabled = false;
                cmbCity.Enabled = false;
            }
        }

        #endregion

        #region CITY DETAILS

        private void GetEditCityData()
        {
            if (_intStateCode != 0)
            {
                cmbCity.Enabled = true;

                var strQuery =
                    "Select CityId,CountryId,StateId,CityName,IsActive from CITYDETAILS WHERE CountryId=" +
                    _intCountryCode + " AND StateId= " + _intStateCode + " AND IsActive='Y' ORDER BY CityName ASC ";

                try
                {
                    OpenSqlCeConnection();
                    var cm = new SqlCeCommand(strQuery, _con)
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.Text,
                        Connection = _con
                    };

                    var dataReader = cm.ExecuteReader();
                    var dataTable = new DataTable();
                    dataTable.Load(dataReader);

                    if (dataTable.Rows.Count > 0)
                    {
                        _editCityList = (from DataRow dr in dataTable.Rows
                            select new EditCityDetails()
                            {
                                CountryId = Convert.ToString(dr["CountryId"]),
                                StateId = Convert.ToString(dr["StateId"]),
                                CityId = Convert.ToString(dr["CityID"]),
                                CityName = Convert.ToString(dr["CityName"]),
                                IsActive = Convert.ToString(dr["IsActive"])
                            }).ToList();

                        cmbCity.DataSource = new BindingSource(_editCityList, null);
                        cmbCity.DisplayMember = "CityName";
                        cmbCity.ValueMember = "CityID";
                        cmbCity.SelectedValue = _editCityList.FirstOrDefault()?.CityId;
                    }
                    else
                    {
                        _intCityCode = 0;
                        _editCityList = new List<EditCityDetails>
                        {
                            new EditCityDetails
                            {
                                CountryId = _intCountryCode.ToString(),
                                StateId = _intStateCode.ToString(),
                                CityId = _intCityCode.ToString(),
                                CityName = Convert.ToString("<--No Records-->")
                            }
                        };
                        cmbCity.DataSource = new BindingSource(_editCityList, null);
                        cmbCity.DisplayMember = "CityName";
                        cmbCity.ValueMember = "CityID";
                        cmbCity.SelectedValue = _editCityList.FirstOrDefault()?.CityId;
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
            else
            {
                cmbCity.Enabled = false;
            }
        }

        #endregion

        #region COUNTRY,STATE,CITY CHANGED EVENT

        private void cmbState_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbState.SelectedItem != null)
            {
                var selectedState = (EditStateDetails) cmbState.SelectedItem;
                _intStateCode = Convert.ToInt32(selectedState.StateId);
                GetEditCityData();
            }
        }

        private void txtMobileNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == (char) Keys.Back))
                e.Handled = true;
        }

        #endregion

        #region MyRegion

        private void OpenSqlCeConnection()
        {
            try
            {
                _con = new SqlCeConnection(@"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf");
                _con.Open();
            }
            catch (SqlCeInvalidDatabaseFormatException)
            {
                var connStringCI = @"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf; LCID= 1033";
                var connStringCS =
                    @"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf; LCID= 1033; Case Sensitive=true";

                var engine = new SqlCeEngine(connStringCI);
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
    }

    #region CLASS

    //COUNTRY
    public class EditCountryDetails
    {
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string IsActive { get; set; }
    }

    //STATE
    public class EditStateDetails
    {
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }
        public string IsActive { get; set; }
    }

    //CITY DETAILS
    public class EditCityDetails
    {
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public string IsActive { get; set; }
    }

    #endregion
}