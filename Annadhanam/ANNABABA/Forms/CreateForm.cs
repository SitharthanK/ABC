using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Windows.Forms;

namespace ANNABABA.Forms
{
    public partial class CreateForm : Form
    {
        #region CONSTRUCTOR

        public CreateForm()
        {
            OnPageload = true;

            MaximizeBox = false;
            InitializeComponent();
            StartTimer();
            AddMenuAndItems();

            dtAnadhanamDate.Format = DateTimePickerFormat.Custom;
            dtAnadhanamDate.CustomFormat = @"dd-MMM-yyyy";

            dtChequeDate.Format = DateTimePickerFormat.Custom;
            dtChequeDate.CustomFormat = @"dd-MMM-yyyy";

            dtAnadhanamDate.MinDate = DateTime.Now;
            dtAnadhanamDate.MaxDate = DateTime.Now.AddMonths(4).Date;

            dtChequeDate.Value = DateTime.Now.Date;
            dtChequeDate.MinDate = DateTime.Now.AddMonths(-2).Date;
            dtChequeDate.MaxDate = DateTime.Now.Date;

            GetCountryData();
            PaymentModeDetails();

            txtoldReceiptNumber.Visible = false;
            btnEditDetails.Visible = false;
            lblSearchRecipt.Visible = false;

            GetAnnaAvailabilityDate();

            OnPageload = false;
        }

        #endregion

        #region PROPERTY

        private SqlCeConnection _cn = new SqlCeConnection(@"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf");

        private List<CountryDetails> _countryList = new List<CountryDetails>();
        private List<StateDetails> _stateList = new List<StateDetails>();
        private List<CityDetails> _cityList = new List<CityDetails>();

        private bool OnPageload { get; set; }
        private int _intCount, _intCountryCode, _intStateCode, _intCityCode;
        private const string Format = "{0}";
        private Timer _timer;

        #endregion

        #region MENU DETAILS

        private void AddMenuAndItems()
        {
            Menu = new MainMenu();
            MenuItem oFile = new MenuItem("File");
            MenuItem oEdit = new MenuItem("Edit");
            MenuItem oView = new MenuItem("View");

            /*FILE MENU ITEMS*/
            Menu.MenuItems.Add(oFile);
            oFile.MenuItems.Add("Exit", ExitApplication_click);

            /*EDIT MENU ITEMS*/
            Menu.MenuItems.Add(oEdit);
            oEdit.MenuItems.Add("Update", Update_Click);

            /*VIEW MENU ITEMS*/
            Menu.MenuItems.Add(oView);
            oView.MenuItems.Add("Receipt", PrintRecipt_Click);

            Menu.MenuItems.Add(oView);
            oView.MenuItems.Add("Report", Report_Click);
        }

        private void Update_Click(object sender, EventArgs e)
        {
            EditForm obj = new EditForm();
            obj.ShowDialog();
        }

        private void Report_Click(object sender, EventArgs e)
        {
            ViewReportForm obj = new ViewReportForm();
            obj.ShowDialog();
        }

        private void PrintRecipt_Click(object sender, EventArgs e)
        {
            ViewReceiptForm obj = new ViewReceiptForm();
            obj.ShowDialog();
        }

        #endregion

        #region PAGE LOAD

        private void AnnaBabaCharities_Load(object sender, EventArgs e)
        {
            GetReceiptNumberDetails();
            GetAnnadhanamCountDetails();
        }

        #endregion

        #region TIMER VALIDATIONS

        private void StartTimer()
        {
            _timer = new Timer {Interval = 1000};
            _timer.Tick += tmr_Tick;
            _timer.Enabled = true;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            lblDateValue.Text = DateTime.Now.ToString();
        }

        #endregion

        #region PAYMENT MODE

        private static readonly Dictionary<int, string> PaymentModeList = new Dictionary<int, string>()
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

        #region ANNADHANAM DATE VALIDATIONS

        private void dtAnadhanamDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime dtAnandhanDate = dtAnadhanamDate.Value.Date;

            if (dtAnandhanDate.Day == 1 && dtAnandhanDate.Month == 1)
            {
                MessageBox.Show(@"Anadhanam cannot be made on this day,please choose other date !...", @"Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtAnandhanDate.Date > DateTime.Now.AddMonths(4).Date)
            {
                dtAnadhanamDate.Value = DateTime.Now.AddMonths(4).Date;
                MessageBox.Show(@"Anadhanam date must not exceed more than 4 months !...", @"Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtAnandhanDate.Date < DateTime.Now.Date)
            {
                dtAnadhanamDate.Value = DateTime.Now.Date;
                MessageBox.Show(@"Anadhanam date must be Today or Above,You cannot select past date !...",
                    @"Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region LAST RECEIPT NUMBER DETAILS

        private void GetReceiptNumberDetails()
        {
            try
            {
                OpenSqlCeConnection();
                string strSelectQuery =
                    "SELECT coalesce(MAX(ReceiptNumber),0) as LastReceiptNumber  FROM tblAnnadhanamDetails";
                SqlCeCommand cm = new SqlCeCommand(strSelectQuery, _cn)
                {
                    CommandText = strSelectQuery,
                    CommandType = CommandType.Text,
                    Connection = _cn
                };

                var reader = cm.ExecuteReader();

                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            string s = String.Format(Format, reader["LastReceiptNumber"]);
                            txtReceiptNumber.Text = ((s == "") ? "1" : (Convert.ToInt32(s) + 1).ToString());
                            txtReceiptNumber.Enabled = false;

                            txtoldReceiptNumber.Text = Convert.ToString((Convert.ToInt32(txtReceiptNumber.Text) - 1));
                            txtoldReceiptNumber.Visible = false;
                        }
                    }
                }
                finally
                {
                    reader?.Close();
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
        }

        #endregion

        #region COUNTRY DETAILS

        private void GetCountryData()
        {
            OpenSqlCeConnection();
            string strQuery = "SELECT CountryId,CountyName,IsActive FROM  CountryDetails WHERE IsActive='Y'";

            try
            {
                SqlCeCommand cm = new SqlCeCommand(strQuery, _cn)
                {
                    CommandText = strQuery,
                    CommandType = CommandType.Text,
                    Connection = _cn
                };

                var dataReader = cm.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);

                if (dataTable.Rows.Count > 0)
                {
                    _countryList = (from DataRow dr in dataTable.Rows
                        select new CountryDetails()
                        {
                            CountryId = Convert.ToString(dr["CountryId"]),
                            CountryName = Convert.ToString(dr["CountyName"]),
                            IsActive = Convert.ToString(dr["IsActive"])
                        }).ToList();

                    string countryCode = (from p in _countryList
                        where p.CountryName.Trim().ToUpper().Contains("INDIA")
                        select p.CountryId).First();
                    _intCountryCode = Convert.ToInt32(countryCode);

                    cmbCountry.DataSource = new BindingSource(_countryList, null);
                    cmbCountry.DisplayMember = "CountryName";
                    cmbCountry.ValueMember = "CountryId";
                    cmbCountry.SelectedValue = Convert.ToString(_intCountryCode);

                    if (_countryList != null && _countryList.Any())
                    {
                        cmbState.Enabled = true;
                        GetStateData();
                    }
                    else
                    {
                        _intCountryCode = 0;
                        _countryList = new List<CountryDetails>
                        {
                            new CountryDetails
                            {
                                CountryId = _intCountryCode.ToString(),
                                CountryName = Convert.ToString("<--No Records-->")
                            }
                        };

                        cmbCountry.DataSource = new BindingSource(_countryList, null);
                        cmbCountry.DisplayMember = "CountryName";
                        cmbCountry.ValueMember = "CountryId";
                        cmbCountry.SelectedValue = _countryList.FirstOrDefault()?.CountryName;

                        cmbState.Enabled = false;
                        cmbCity.Enabled = false;
                    }
                }
                else
                {
                    _intCountryCode = 0;
                    _countryList = new List<CountryDetails>
                    {
                        new CountryDetails
                        {
                            CountryId = _intCountryCode.ToString(),
                            CountryName = Convert.ToString("<--No Records-->")
                        }
                    };

                    cmbCountry.DataSource = new BindingSource(_countryList, null);
                    cmbCountry.DisplayMember = "CountryName";
                    cmbCountry.ValueMember = "CountryId";
                    cmbCountry.SelectedValue = _countryList.FirstOrDefault()?.CountryId;

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
                _cn.Close();
            }
        }

        #endregion

        #region STATE DETAILS

        private void GetStateData()
        {
            if (_intCountryCode != 0)
            {
                cmbState.Enabled = true;
                cmbCity.Enabled = false;

                OpenSqlCeConnection();
                try
                {
                    string strQuery = "Select CountryId,StateId,StateName,IsActive from StateDetails WHERE CountryId=" +
                                      _intCountryCode + " AND IsActive='Y' ORDER BY StateName ASC ";
                    SqlCeCommand cm = new SqlCeCommand(strQuery, _cn)
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.Text,
                        Connection = _cn
                    };

                    var dataReader = cm.ExecuteReader();
                    var dataTable = new DataTable();
                    dataTable.Load(dataReader);

                    if (dataTable.Rows.Count > 0)
                    {
                        _stateList = (from DataRow dr in dataTable.Rows
                            select new StateDetails()
                            {
                                CountryId = Convert.ToString(dr["CountryId"]),
                                StateId = Convert.ToString(dr["StateId"]),
                                StateName = Convert.ToString(dr["StateName"]),
                                IsActive = Convert.ToString(dr["IsActive"])
                            }).ToList();

                        cmbState.DataSource = new BindingSource(_stateList, null);
                        cmbState.DisplayMember = "StateName";
                        cmbState.ValueMember = "StateId";
                        cmbState.SelectedValue = Convert.ToString(_intStateCode);

                        if (OnPageload)
                        {
                            string stateCode = (from p in _stateList
                                where p.StateName.Trim().ToUpper().Contains("TAMIL NADU")
                                select p.StateId).First();
                            _intStateCode = Convert.ToInt32(stateCode);
                            cmbState.SelectedValue = Convert.ToString(_intStateCode);

                            if (_intStateCode != 0 && (_stateList != null && _stateList.Any()))
                            {
                                GetCityData();
                            }
                        }
                        else
                        {
                            if (_intStateCode != 0 && (_stateList != null && _stateList.Any()))
                            {
                                GetCityData();
                            }
                        }
                    }
                    else
                    {
                        _intStateCode = 0;
                        _stateList = new List<StateDetails>
                        {
                            new StateDetails
                            {
                                CountryId = _intCountryCode.ToString(),
                                StateId = _intStateCode.ToString(),
                                StateName = Convert.ToString("<--No Records-->")
                            }
                        };
                        cmbState.DataSource = new BindingSource(_stateList, null);
                        cmbState.DisplayMember = "StateName";
                        cmbState.ValueMember = "StateId";
                        cmbState.SelectedValue = _stateList.FirstOrDefault()?.StateId.ToString();
                        cmbCity.Enabled = false;

                        _intCityCode = 0;
                        _cityList = new List<CityDetails>();
                        _cityList.Add(new CityDetails
                        {
                            CountryId = _intCountryCode.ToString(),
                            StateId = _intStateCode.ToString(),
                            CityId = _intCityCode.ToString(),
                            CityName = Convert.ToString("<--No Records-->")
                        });
                        cmbCity.DataSource = new BindingSource(_cityList, null);
                        cmbCity.DisplayMember = "CityName";
                        cmbCity.ValueMember = "CityId";
                        cmbCity.SelectedValue = _cityList.FirstOrDefault()?.CityId.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _cn.Close();
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

        private void GetCityData()
        {
            if (_intStateCode != 0 && _stateList != null && _stateList.Any())
            {
                cmbCity.Enabled = true;
                OpenSqlCeConnection();
                try
                {
                    string strQuery =
                        "Select CityId,CountryId,StateId,CityName,IsActive from CITYDETAILS WHERE CountryId=" +
                        _intCountryCode + " AND StateId= " + _intStateCode + " AND IsActive='Y' ORDER BY CityName ASC ";
                    SqlCeCommand cm = new SqlCeCommand(strQuery, _cn)
                    {
                        CommandText = strQuery,
                        CommandType = CommandType.Text,
                        Connection = _cn
                    };

                    var dataReader = cm.ExecuteReader();
                    var dataTable = new DataTable();
                    dataTable.Load(dataReader);

                    if (dataTable.Rows.Count > 0)
                    {
                        _cityList = (from DataRow dr in dataTable.Rows
                            select new CityDetails()
                            {
                                CountryId = Convert.ToString(dr["CountryId"]),
                                StateId = Convert.ToString(dr["StateId"]),
                                CityId = Convert.ToString(dr["CityId"]),
                                CityName = Convert.ToString(dr["CityName"]),
                                IsActive = Convert.ToString(dr["IsActive"])
                            }).ToList();

                        cmbCity.DataSource = new BindingSource(_cityList, null);
                        cmbCity.DisplayMember = "CityName";
                        cmbCity.ValueMember = "CityId";
                        cmbCity.SelectedValue = _cityList.FirstOrDefault()?.CityId.ToString();

                        if (OnPageload == true)
                        {
                            string stateCode = (from p in _stateList
                                where p.StateName.ToUpper().Contains("TAMIL NADU")
                                select p.StateId).First();

                            if (!string.IsNullOrWhiteSpace(stateCode) && Convert.ToInt32(stateCode) == _intStateCode)
                            {
                                string cityCode = (from p in _cityList
                                    where p.CityName.Trim().ToUpper().Contains("CHENNAI")
                                    select p.CityId).First();
                                _intCityCode = Convert.ToInt32(cityCode);
                                cmbCity.SelectedValue = Convert.ToString(_intCityCode);
                            }
                            else
                            {
                                cmbCity.DataSource = new BindingSource(_cityList, null);
                                cmbCity.DisplayMember = "CityName";
                                cmbCity.ValueMember = "CityId";
                                cmbCity.SelectedValue = _cityList.FirstOrDefault()?.CityId.ToString();
                            }
                        }
                        else
                        {
                            var cityCode = (from p in _cityList
                                where p.CityName.Trim().ToUpper().Contains("CHENNAI")
                                select p);
                            if (cityCode.FirstOrDefault()?.CityName.Trim().ToUpper() == "CHENNAI")
                            {
                                string cityCodes = (from p in _cityList
                                    where p.CityName.Trim().ToUpper().Contains("CHENNAI")
                                    select p.CityId).First();
                                _intCityCode = Convert.ToInt32(cityCodes);
                                cmbCity.SelectedValue = Convert.ToString(_intCityCode);
                            }
                            else
                            {
                                cmbCity.DataSource = new BindingSource(_cityList, null);
                                cmbCity.DisplayMember = "CityName";
                                cmbCity.ValueMember = "CityId";
                                cmbCity.SelectedValue = _cityList.FirstOrDefault()?.CityId.ToString();
                            }
                        }
                    }
                    else
                    {
                        _intCityCode = 0;
                        _cityList = new List<CityDetails>
                        {
                            new CityDetails
                            {
                                CountryId = _intCountryCode.ToString(),
                                StateId = _intStateCode.ToString(),
                                CityId = _intCityCode.ToString(),
                                CityName = ("<--No Records-->").ToString()
                            }
                        };
                        cmbCity.DataSource = new BindingSource(_cityList, null);
                        cmbCity.DisplayMember = "CityName";
                        cmbCity.ValueMember = "CityId";
                        cmbCity.SelectedValue = _cityList.FirstOrDefault()?.CityId.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _cn.Close();
                }
            }
            else
            {
                cmbCity.Enabled = false;
            }
        }

        #endregion

        #region BUTTON CLEAR

        private void btnClear_Click(object sender, EventArgs e)
        {
            OnPageload = true;
            GetCountryData();
            PaymentModeDetails();
            txtAddress.Text = "";
            txtName.Text = "";
            txtChequeNumber.Text = "";
            txtDrawnOn.Text = "";

            txtMobileNumber.Text = "";
            GetAnnaAvailabilityDate();
            dtChequeDate.Value = DateTime.Now.Date;
            OnPageload = false;
        }

        #endregion

        #region PRINT

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string strPaymentMode = "", strState = "", strCountry = "", strName = "", strContactNumber = "";
            string strAddress = "", strCity = "", strChequeNumber = "", strChequeDrawn = "";
            int intReceiptNumber;
            bool blnSubmit = false;

            GetAnnadhanamCountDetails();

            if (dtAnadhanamDate.Value.Date >= (Convert.ToDateTime("22-JAN-2016").Date))
            {
                if (_intCount < 15)
                {
                    strContactNumber = txtMobileNumber.Text;
                    strContactNumber = !string.IsNullOrEmpty(strContactNumber) ? strContactNumber : "";

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

                    if (_intStateCode != 0 && cmbState.SelectedItem != null)
                    {
                        StateDetails selectedState = (StateDetails) cmbState.SelectedItem;
                        strState = Convert.ToString(selectedState.StateName);
                        blnSubmit = true;
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Choose State & Proceed !...", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    if (_intCityCode != 0 && cmbCity.SelectedItem != null)
                    {
                        CityDetails selectedCity = (CityDetails) cmbCity.SelectedItem;
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

                    DateTime dtAnandhanDate = dtAnadhanamDate.Value.Date;

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
                        string strMessage = "Anadhanam date must not exceed more than 4 months (" +
                                            (DateTime.Now.AddMonths(4).Date).ToString("dd-MMM-yyyy") + ")!...";
                        MessageBox.Show(strMessage, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (dtAnandhanDate.Date < DateTime.Now.Date)
                    {
                        blnSubmit = false;
                        dtAnadhanamDate.Value = DateTime.Now.Date;
                        MessageBox.Show(@"Anadhanam date must be Today or Above,You cannot select past date !...",
                            @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            }

                            return;
                        }
                        else
                            blnSubmit = true;
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

                    if (!string.IsNullOrEmpty(txtMobileNumber.Text) && txtMobileNumber.Text.Length == 10)
                    {
                        blnSubmit = true;
                        strContactNumber = txtMobileNumber.Text.ToString();
                    }

                    #endregion

                    #region SUBMIT & PRINT

                    if (blnSubmit)
                    {
                        string strInsertQuery;
                        DateTime dtAnadhanamDates = new DateTime(dtAnadhanamDate.Value.Year,
                            dtAnadhanamDate.Value.Month, dtAnadhanamDate.Value.Day, 12, 0, 0);
                        OpenSqlCeConnection();
                        if (strPaymentMode.ToUpper() == "CASH")
                            strInsertQuery =
                                "INSERT INTO tblAnnadhanamDetails(ReceiptNumber,DevoteeName,Address,CountryCode,Country,StateCode,State,CityCode,City,Amount,ReceiptCreatedDate,AnadhanamDate,Mode,ContactNumber)VALUES(@ReceiptNumber,@DevoteeName, @Address,@CountryCode,@Country,@StateCode,@State,@CityCode,@City,@Amount,@ReceiptCreatedDate,@AnadhanamDate,@Mode,@ContactNumber)";
                        else
                            strInsertQuery =
                                "INSERT INTO tblAnnadhanamDetails(ReceiptNumber,DevoteeName,Address,CountryCode,Country,StateCode,State,CityCode,City,Amount,ReceiptCreatedDate,AnadhanamDate,ChequeNo,ChequeDate,ChequeDrawn,Mode,ContactNumber)VALUES(@ReceiptNumber,@DevoteeName, @Address,@CountryCode,@Country,@StateCode,@State,@CityCode,@City,@Amount,@ReceiptCreatedDate,@AnadhanamDate,@ChequeNo,@ChequeDate,@ChequeDrawn,@Mode,@ContactNumber)";

                        SqlCeCommand cm = new SqlCeCommand(strInsertQuery, _cn);
                        cm.Parameters.AddWithValue("@ReceiptNumber", intReceiptNumber);
                        cm.Parameters.AddWithValue("@DevoteeName", strName);
                        cm.Parameters.AddWithValue("@Address", strAddress);
                        cm.Parameters.AddWithValue("@CountryCode", _intCountryCode);
                        cm.Parameters.AddWithValue("@Country", strCountry);
                        cm.Parameters.AddWithValue("@StateCode", _intStateCode);
                        cm.Parameters.AddWithValue("@State", strState);
                        cm.Parameters.AddWithValue("@CityCode", _intCityCode);
                        cm.Parameters.AddWithValue("@City", strCity);
                        cm.Parameters.AddWithValue("@Amount", 500);
                        cm.Parameters.AddWithValue("@ReceiptCreatedDate", DateTime.Now.Date);
                        cm.Parameters.AddWithValue("@AnadhanamDate", dtAnadhanamDates);
                        cm.Parameters.AddWithValue("@Mode", strPaymentMode);
                        cm.Parameters.AddWithValue("@ContactNumber", strContactNumber);
                        if (strPaymentMode.ToUpper() == "CHEQUE")
                        {
                            cm.Parameters.AddWithValue("@ChequeNo", strChequeNumber);
                            cm.Parameters.AddWithValue("@ChequeDate", dtChequeDate.Value);
                            cm.Parameters.AddWithValue("@ChequeDrawn", strChequeDrawn);
                        }

                        string strFullAddress = strAddress + ", " + strCity + "," + strState + "," + strCountry + ".";

                        try
                        {
                            int intAffectedRow = cm.ExecuteNonQuery();
                            if (intAffectedRow > 0)
                            {
                                MessageBox.Show(@"Anadhanam Created Sucessfully", Application.ProductName,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                PrintForm obj = new PrintForm(intReceiptNumber.ToString(), strName,
                                    strFullAddress, dtAnadhanamDates, strChequeNumber, dtChequeDate.Value.Date,
                                    strChequeDrawn, strPaymentMode);
                                Clear();
                                obj.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show(@"Insertion Failed", Application.ProductName, MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }

                            _cn.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }

                        GetReceiptNumberDetails();
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

                    if (_intCityCode != 0 && cmbState.SelectedItem != null)
                    {
                        CityDetails selectedCity = (CityDetails) cmbState.SelectedItem;
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

                    DateTime dtAnandhanDate = dtAnadhanamDate.Value.Date;

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
                        string strMessage = "Anadhanam date must not exceed more than 4 months (" +
                                            (DateTime.Now.AddMonths(4).Date).ToString("dd-MMM-yyyy") + ")!...";
                        MessageBox.Show(strMessage, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (dtAnandhanDate.Date < DateTime.Now.Date)
                    {
                        blnSubmit = false;
                        dtAnadhanamDate.Value = DateTime.Now.Date;
                        MessageBox.Show(@"Anadhanam date must be Today or Above,You cannot select past date !...",
                            @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    if (_intStateCode != 0 && cmbState.SelectedItem != null)
                    {
                        StateDetails selectedState = (StateDetails) cmbState.SelectedItem;
                        strState = Convert.ToString(selectedState.StateName);
                        blnSubmit = true;
                    }
                    else
                    {
                        blnSubmit = false;
                        MessageBox.Show(@"Please Choose State & Proceed !...", @"Information", MessageBoxButtons.OK,
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

                    #endregion

                    #region SUBMIT & PRINT

                    if (blnSubmit)
                    {
                        OpenSqlCeConnection();
                        string strInsertQuery;
                        if (strPaymentMode.ToUpper() == "CASH")
                            strInsertQuery =
                                "INSERT INTO tblAnnadhanamDetails(ReceiptNumber,DevoteeName,Address,CountryCode,Country,StateCode,State,CityCode,City,Amount,ReceiptCreatedDate,AnadhanamDate,Mode)VALUES(@ReceiptNumber,@DevoteeName, @Address,@CountryCode,@Country,@StateCode,@State,@CityCode,@City,@Amount,@ReceiptCreatedDate,@AnadhanamDate,@Mode)";
                        else
                            strInsertQuery =
                                "INSERT INTO tblAnnadhanamDetails(ReceiptNumber,DevoteeName,Address,CountryCode,Country,StateCode,State,CityCode,City,Amount,ReceiptCreatedDate,AnadhanamDate,ChequeNo,ChequeDate,ChequeDrawn,Mode)VALUES(@ReceiptNumber,@DevoteeName, @Address,@CountryCode,@Country,@StateCode,@State,@CityCode,@City,@Amount,@ReceiptCreatedDate,@AnadhanamDate,@ChequeNo,@ChequeDate,@ChequeDrawn,@Mode)";

                        SqlCeCommand cm = new SqlCeCommand(strInsertQuery, _cn);
                        cm.Parameters.AddWithValue("@ReceiptNumber", intReceiptNumber);
                        cm.Parameters.AddWithValue("@DevoteeName", strName);
                        cm.Parameters.AddWithValue("@Address", strAddress);
                        cm.Parameters.AddWithValue("@CountryCode", _intCountryCode);
                        cm.Parameters.AddWithValue("@Country", strCountry);
                        cm.Parameters.AddWithValue("@StateCode", _intStateCode);
                        cm.Parameters.AddWithValue("@State", strState);
                        cm.Parameters.AddWithValue("@CityCode", _intCityCode);
                        cm.Parameters.AddWithValue("@City", strCity);
                        cm.Parameters.AddWithValue("@Amount", 500);
                        cm.Parameters.AddWithValue("@ReceiptCreatedDate", DateTime.Now.Date);
                        cm.Parameters.AddWithValue("@AnadhanamDate", dtAnadhanamDate.Value.Date);
                        cm.Parameters.AddWithValue("@Mode", strPaymentMode);

                        if (strPaymentMode.ToUpper() == "CHEQUE")
                        {
                            cm.Parameters.AddWithValue("@ChequeNo", strChequeNumber);
                            cm.Parameters.AddWithValue("@ChequeDate", dtChequeDate.Value);
                            cm.Parameters.AddWithValue("@ChequeDrawn", strChequeDrawn);
                        }

                        string strFullAddress = strAddress + ", " + strCity + "," + strState + "," + strCountry + ".";

                        try
                        {
                            int intAffectedRow = cm.ExecuteNonQuery();
                            if (intAffectedRow > 0)
                            {
                                MessageBox.Show(@"Anadhanam Created Sucessfully", Application.ProductName,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                PrintForm obj = new PrintForm(intReceiptNumber.ToString(), strName,
                                    strFullAddress, dtAnadhanamDate.Value.Date, strChequeNumber,
                                    dtChequeDate.Value.Date, strChequeDrawn, strPaymentMode);
                                Clear();
                                obj.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show(@"Insertion Failed", Application.ProductName, MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }

                            _cn.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }

                        GetReceiptNumberDetails();
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

        #region ANNABABA CHARITIES SHOWN

        private void AnnaBabaCharities_Shown(object sender, EventArgs e)
        {
            try
            {
                OpenSqlCeConnection();
            }
            catch (SqlCeException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
            }
            finally
            {
                _cn.Close();
            }
        }

        #endregion

        #region ANNADHANAM COUNT DETAILS

        private void GetAnnadhanamCountDetails()
        {
            try
            {
                OpenSqlCeConnection();
                DateTime dtAnadhanamDates = new DateTime(dtAnadhanamDate.Value.Year, dtAnadhanamDate.Value.Month,
                    dtAnadhanamDate.Value.Day, 12, 0, 0);
                string strCurrentDate = dtAnadhanamDates.ToString();
                string strSelectQuery =
                    "SELECT COUNT(ReceiptNumber) AS CNT FROM  tblAnnadhanamDetails WHERE (AnadhanamDate='" +
                    strCurrentDate + "')";
                SqlCeCommand cm = new SqlCeCommand(strSelectQuery, _cn)
                {
                    CommandText = strSelectQuery,
                    CommandType = CommandType.Text,
                    Connection = _cn
                };
                var reader = cm.ExecuteReader();

                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            string strAnnadhanamDayCount = String.Format(Format, reader["CNT"]);
                            _intCount = !string.IsNullOrEmpty(strAnnadhanamDayCount)
                                ? Convert.ToInt32(strAnnadhanamDayCount)
                                : 0;
                        }
                    }
                }
                finally
                {
                    reader?.Close();
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
        }

        #endregion

        #region CLEAR

        private void Clear()
        {
            OnPageload = true;
            GetCountryData();
            PaymentModeDetails();

            txtAddress.Text = "";
            txtName.Text = "";
            txtChequeNumber.Text = "";
            txtDrawnOn.Text = "";
            txtMobileNumber.Text = "";

            GetAnnaAvailabilityDate();
            dtChequeDate.Value = DateTime.Now.Date;
            OnPageload = false;
        }

        #endregion

        #region AVAILABILITY

        private void Availability_Click(object sender, EventArgs e)
        {
            AvailabilityForm obj = new AvailabilityForm(dtAnadhanamDate.Value.Date);
            obj.ShowDialog();
        }

        #endregion

        #region GET RECEIPT NUMBER DETAILS

        private void btnEditDetails_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtoldReceiptNumber.Text))
            {
                if (txtoldReceiptNumber.Visible)
                {
                    GetData(txtoldReceiptNumber.Text);
                }
            }
            else
            {
                MessageBox.Show(@"Enter Valid Receipt Number", Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void GetData(string strReceiptNumber)
        {
            OpenSqlCeConnection();
            try
            {
                string strQuery =
                    "SELECT ReceiptNumber,DevoteeName,Address,Country,State,City,Amount,ReceiptCreatedDate,AnadhanamDate,ChequeNo,ChequeDate,ChequeDrawn,Mode,ContactNumber FROM  tblAnnadhanamDetails WHERE ReceiptNumber='" +
                    strReceiptNumber + "' OR ContactNumber='" + strReceiptNumber + "'";
                using (_cn)
                {
                    using (SqlCeDataAdapter da = new SqlCeDataAdapter())
                    {
                        SqlCeCommand cmd = new SqlCeCommand(strQuery, _cn)
                        {
                            Connection = _cn
                        };
                        da.SelectCommand = cmd;
                        var ds = new DataSet();
                        da.Fill(ds);
                        _cn.Close();
                        var dt = ds.Tables[0].Clone();

                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow lastRow = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1];
                            dt.Rows.Add(lastRow.ItemArray);

                            foreach (DataRow dr in dt.Rows)
                            {
                                #region ASSIGINING VALUES TO THE FIELD

                                txtName.Text = Convert.ToString(dr["DevoteeName"]);
                                txtAddress.Text = Convert.ToString(dr["Address"]);
                                txtMobileNumber.Text = Convert.ToString(dr["ContactNumber"]);

                                string strCity = Convert.ToString(dr["City"]);
                                var state = (from p in _stateList
                                    where p.StateName.Contains(Convert.ToString(dr["State"]).Trim())
                                    select p.StateId).First();

                                if (state != null && state.Any())
                                {
                                    var stateValues = (from p in _stateList
                                        where p.StateName.Contains(Convert.ToString(dr["State"]).Trim())
                                        select p.StateId).First();
                                    _intStateCode = Convert.ToInt32(stateValues);
                                    cmbState.SelectedValue = Convert.ToString(_intStateCode);
                                }
                                else
                                {
                                }

                                var city = ((from p in _cityList
                                    where p.CityName.Contains(strCity.Trim())
                                    select p.CityId));

                                if (city.Any())
                                {
                                    var cityValues =
                                        (from p in _cityList
                                            where p.CityName.Contains(Convert.ToString(dr["City"]).Trim())
                                            select p.CityId).First();
                                    _intCityCode = Convert.ToInt32(cityValues);
                                    cmbCity.SelectedValue = Convert.ToString(_intCityCode);
                                }
                                else
                                {
                                    _intCityCode = 0;
                                    _cityList = new List<CityDetails>
                                    {
                                        new CityDetails
                                        {
                                            CountryId = _intCountryCode.ToString(),
                                            StateId = _intStateCode.ToString(),
                                            CityId = _intCityCode.ToString(),
                                            CityName = "<--No Recor" + "ds-->"
                                        }
                                    };
                                    cmbCity.DataSource = new BindingSource(_cityList, null);
                                    cmbCity.DisplayMember = "CityName";
                                    cmbCity.ValueMember = "CityId";
                                    cmbCity.SelectedValue = _cityList.FirstOrDefault()?.CityId;
                                }

                                var paymentModeValues = (from p in PaymentModeList
                                    where p.Value.Contains(Convert.ToString(dr["Mode"]))
                                    select p.Key).First();
                                cmbPaymentMode.SelectedValue = Convert.ToInt32(paymentModeValues);

                                if (cmbPaymentMode.SelectedValue.ToString() == "2")
                                {
                                    txtChequeNumber.Enabled = true;
                                    txtDrawnOn.Enabled = true;
                                    dtChequeDate.Enabled = true;

                                    txtChequeNumber.Text = Convert.ToString(dr["ChequeNo"]) ?? string.Empty;
                                    txtDrawnOn.Text = Convert.ToString(dr["ChequeDrawn"]) ?? string.Empty;
                                    if (Convert.ToDateTime(dr["ChequeDate"]).Date != null)
                                    {
                                        DateTime dtPreviousChequeDate = Convert.ToDateTime(dr["ChequeDate"]).Date;
                                        if (dtPreviousChequeDate >= dtChequeDate.MinDate &&
                                            dtPreviousChequeDate <= dtChequeDate.MinDate)
                                            dtChequeDate.Value = Convert.ToDateTime(dr["ChequeDate"]).Date;
                                    }
                                }
                                else
                                {
                                    txtChequeNumber.Enabled = false;
                                    txtDrawnOn.Enabled = false;
                                    dtChequeDate.Enabled = false;
                                }

                                break;

                                #endregion
                            }

                            txtoldReceiptNumber.Visible = false;
                            checkBox1.Checked = false;
                            btnEditDetails.Visible = false;
                            lblSearchRecipt.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show(@"Enter Valid Receipt / Mobile Number", Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtoldReceiptNumber.Visible = true;
                            checkBox1.Checked = true;
                            btnEditDetails.Visible = true;
                            lblSearchRecipt.Visible = true;
                        }

                        da.Dispose();
                        ds.Dispose();
                    }
                }
            }
            finally
            {
                _cn.Close();
            }
        }

        #endregion

        #region ACCEPT NUMBERS ONLY

        private void txtOldReceiptNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region CHECKBOX CHANGE EVENT

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                btnEditDetails.Visible = true;
                txtoldReceiptNumber.Visible = true;
                lblSearchRecipt.Visible = true;
            }
            else
            {
                btnEditDetails.Visible = false;
                txtoldReceiptNumber.Visible = false;
                lblSearchRecipt.Visible = false;
            }
        }

        #endregion

        #region CLOSING

        private void AnnaBabaCharities_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void ExitApplication_click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region NEXT ANNADHANAM AVAILABILITY DATE

        private void GetAnnaAvailabilityDate()
        {
            DateTime from = DateTime.Now; //.AddMonths(1).Date;
            DateTime to = DateTime.Now.AddMonths(4).Date;

            DateTime periodFrom = new DateTime(from.Year, from.Month, from.Day, 12, 0, 0);
            DateTime periodTo = new DateTime(to.Year, to.Month, to.Day, 12, 0, 0);
            try
            {
                OpenSqlCeConnection();
                string strSelectQuery =
                    "SELECT COALESCE(COUNT(ReceiptNumber),0) as intCount, AnadhanamDate FROM tblAnnadhanamDetails WHERE AnadhanamDate>='" +
                    periodFrom.ToString("dd-MMM-yyyy") + "' AND AnadhanamDate<'" + periodTo.ToString("dd-MMM-yyyy") +
                    "' GROUP BY AnadhanamDate ORDER BY AnadhanamDate ASC";
                SqlCeCommand cm = new SqlCeCommand(strSelectQuery, _cn)
                {
                    CommandText = strSelectQuery,
                    CommandType = CommandType.Text,
                    Connection = _cn
                };


                var dataReader = cm.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);

                if (dataTable.Rows.Count > 0)
                {
                    var result = (from DataRow dr in dataTable.Rows
                        select new AnnadhanamAvailability()
                        {
                            IntCount = Convert.ToInt32(dr["intCount"]),
                            DtAnnadhanamDate = new DateTime(Convert.ToDateTime(dr["AnadhanamDate"]).Year,
                                Convert.ToDateTime(dr["AnadhanamDate"]).Month,
                                Convert.ToDateTime(dr["AnadhanamDate"]).Day, 12, 0, 0),
                            IntBalanceCount =
                                (Convert.ToDateTime(Convert.ToString(dr["AnadhanamDate"])).Date >=
                                 (Convert.ToDateTime("22-JAN-2016").Date))
                                    ? (15 - Convert.ToInt32(dr["intCount"]))
                                    : (10 - Convert.ToInt32(dr["intCount"])),
                        }).ToList();


                    int counts = Convert.ToInt32((periodTo.Date - periodFrom.Date).TotalDays);
                    for (int i = 0; i < counts; i++)
                    {
                        DateTime dt = new DateTime(periodFrom.AddDays(i).Year, periodFrom.AddDays(i).Month,
                            periodFrom.AddDays(i).Day, 12, 0, 0);
                        if (dt.Day == 1 && dt.Month == 1)
                        {
                        }
                        else
                        {
                            var match = result.FirstOrDefault(stringToCheck => stringToCheck.DtAnnadhanamDate == dt);

                            if ((match == null) && (dt.Date >= Convert.ToDateTime("22-JAN-2016").Date))
                            {
                                result.Add(new AnnadhanamAvailability
                                {
                                    IntCount = Convert.ToInt32("15"),
                                    DtAnnadhanamDate = dt,
                                    IntBalanceCount = Convert.ToInt32("15")
                                });
                            }
                            else if ((match == null) && (dt.Date < Convert.ToDateTime("22-JAN-2016").Date))
                            {
                                result.Add(new AnnadhanamAvailability
                                {
                                    IntCount = Convert.ToInt32("10"),
                                    DtAnnadhanamDate = dt,
                                    IntBalanceCount = Convert.ToInt32("10")
                                });
                            }
                            else
                            {
                            }
                        }
                    }

                    if (result.Any())
                    {
                        int count = (from element in result
                            where element.IntBalanceCount > 0
                            orderby element.DtAnnadhanamDate ascending
                            select element).Count();

                        if (count > 0)
                        {
                            var elements = (from element in result
                                where element.IntBalanceCount > 0
                                orderby element.DtAnnadhanamDate ascending
                                select element).First();

                            dtAnadhanamDate.MinDate = elements.DtAnnadhanamDate.Date;
                            dtAnadhanamDate.Value = elements.DtAnnadhanamDate.Date;
                        }
                        else
                        {
                            var elements = (from element in result
                                orderby element.DtAnnadhanamDate ascending
                                select element).Last();
                            if (elements.DtAnnadhanamDate.AddDays(1).Date >= DateTime.Now.AddMonths(4).Date)
                            {
                                dtAnadhanamDate.MinDate = DateTime.Now.AddMonths(4).Date;
                                dtAnadhanamDate.Value = DateTime.Now.AddMonths(4).Date;
                            }
                            else
                            {
                                dtAnadhanamDate.MinDate = elements.DtAnnadhanamDate.AddDays(1).Date;
                                dtAnadhanamDate.Value = elements.DtAnnadhanamDate.AddDays(1).Date;
                            }
                        }
                    }
                    else
                    {
                        dtAnadhanamDate.MinDate = DateTime.Now.AddMonths(1).Date;
                        dtAnadhanamDate.Value = DateTime.Now.AddMonths(4).Date;
                    }
                }
                else
                {
                    dtAnadhanamDate.MinDate = DateTime.Now.AddMonths(1).Date;
                    dtAnadhanamDate.Value = DateTime.Now.AddMonths(4).Date;
                }
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

        #region PAYMENT MODE CHANGE VALIDATIONS

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
            }
        }

        #endregion

        #region COUNTRY,STATE,CITY ,MOBILE NUMBER CHANGED EVENT

        private void cmbState_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbState.SelectedItem != null)
            {
                StateDetails selectedState = (StateDetails) cmbState.SelectedItem;
                _intStateCode = Convert.ToInt32(selectedState.StateId);
                GetCityData();
            }
        }

        private void cmbCity_SelectedValueChanged(object sender, EventArgs e)
        {
            CityDetails selectedCity = (CityDetails) cmbCity.SelectedItem;
            _intCityCode = Convert.ToInt32(selectedCity.CityId);
        }

        private void txtMobileNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char) Keys.Back)))
                e.Handled = true;
        }

        #endregion

        #region OPEN SQL CONNECTION

        private void OpenSqlCeConnection()
        {
            try
            {
                _cn = new SqlCeConnection(@"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf");
                _cn.Open();
            }
            catch (SqlCeInvalidDatabaseFormatException)
            {
                string connStringCI = @"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf; LCID= 1033";
                string connStringCS =
                    @"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf; LCID= 1033; Case Sensitive=true";

                SqlCeEngine engine = new SqlCeEngine(connStringCI);
                engine.Upgrade(connStringCS);

                _cn = null;
                _cn = new SqlCeConnection(connStringCI);
                _cn.Open();
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

    //NEXT ANNADHANAM AVAILABILITY
    internal class AnnadhanamAvailability
    {
        internal int IntCount { get; set; }
        internal DateTime DtAnnadhanamDate { get; set; }
        internal int IntBalanceCount { get; set; }
    }

    //COUNTRY
    internal class CountryDetails
    {
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string IsActive { get; set; }
    }

    //STATE
    internal class StateDetails
    {
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }
        public string IsActive { get; set; }
    }

    internal class CityDetails
    {
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public string IsActive { get; set; }
    }

    #endregion
}