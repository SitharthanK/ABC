namespace ANNABABA
{
    using ANNABABA.Forms;
    using ANNABABA.Helpers;
    using ANNABABA.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="Create" />.
    /// </summary>
    public partial class Create : Form
    {
        #region Properties
        /// <summary>
        /// Gets or sets the bookedCount.
        /// </summary>
        private int BookedCount { get; set; }

        /// <summary>
        /// Gets or sets the noofmonths.
        /// </summary>
        private int NoOfMonths { get; set; }

        /// <summary>
        /// Gets or sets the totalCount.
        /// </summary>
        private int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether onPageload.
        /// </summary>
        public bool onPageload { get; set; }

        /// <summary>
        /// Gets or sets the lstCountryDetails.
        /// </summary>
        private static List<CountryDetails> lstCountryDetails { get; set; }

        /// <summary>
        /// Gets or sets the lstStateList
        /// Defines the StateList.........
        /// </summary>
        private static List<StateDetails> lstStateDetails { get; set; }

        /// <summary>
        /// Gets or sets the lstCityDetails.
        /// </summary>
        private static List<CityDetails> lstCityDetails { get; set; }

        /// <summary>
        /// Gets or sets the lstConfiguration.
        /// </summary>
        private static List<ConfigurationDetails> lstConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the lstAvailability.
        /// </summary>
        private static List<Models.Availability> lstAvailability { get; set; }

        /// <summary>
        /// Defines the tmr.
        /// </summary>
        internal System.Windows.Forms.Timer tmr = null;

        /// <summary>
        /// Defines the CityList.
        /// </summary>
        private readonly object CityList;
        #endregion

        #region MenuBar and it's events
        /// <summary>
        /// The AddMenuAndItems.
        /// </summary>
        public void AddMenuAndItems()
        {
            this.Menu = new MainMenu();
            MenuItem OFile = new MenuItem("File");
            MenuItem OEdit = new MenuItem("Edit");
            MenuItem OView = new MenuItem("View");
            MenuItem OSettings = new MenuItem("Settings");

            /*FILE MENU ITEMS*/
            this.Menu.MenuItems.Add(OFile);
            OFile.MenuItems.Add("Exit", new EventHandler(ExitApplication_click));


            /*EDIT MENU ITEMS*/
            this.Menu.MenuItems.Add(OEdit);
            OEdit.MenuItems.Add("Update", new EventHandler(Update_Click));


            /*VIEW MENU ITEMS*/
            this.Menu.MenuItems.Add(OView);
            OView.MenuItems.Add("Receipt", new EventHandler(PrintRecipt_Click));

            this.Menu.MenuItems.Add(OView);
            OView.MenuItems.Add("Report", new EventHandler(Report_Click));

            this.Menu.MenuItems.Add(OView);
            OView.MenuItems.Add("Records", new EventHandler(Records_Click));

            this.Menu.MenuItems.Add(OSettings);
            OSettings.MenuItems.Add("Configure", new EventHandler(ConfigureApplication_click));
        }

        private void Records_Click(object sender, EventArgs e)
        {
            Records objRecords = new Records();
            objRecords.ShowDialog();
        }

        /// <summary>
        /// The ConfigureApplication_click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void ConfigureApplication_click(object sender, EventArgs e)
        {
            Configure objConfigure = new Configure();
            objConfigure.ShowDialog();
        }

        /// <summary>
        /// The Update_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void Update_Click(object sender, EventArgs e)
        {
            Edit obj = new Edit();
            obj.ShowDialog();
        }

        /// <summary>
        /// The Report_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void Report_Click(object sender, EventArgs e)
        {
            Reports obj = new Reports();
            obj.ShowDialog();
        }

        /// <summary>
        /// The PrintRecipt_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void PrintRecipt_Click(object sender, EventArgs e)
        {
            View obj = new View();
            obj.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Create"/> class.
        /// </summary>
       
        
        #region Constructor
        public Create()
        {
            onPageload = true;

            MaximizeBox = false;
            InitializeComponent();
            StartTimer();
            AddMenuAndItems();

            dtAnadhanamDate.Format = DateTimePickerFormat.Custom;
            dtAnadhanamDate.CustomFormat = "dd-MMM-yyyy";

            dtChequeDate.Format = DateTimePickerFormat.Custom;
            dtChequeDate.CustomFormat = "dd-MMM-yyyy";

            dtAnadhanamDate.MinDate = DateTime.Now;
            dtAnadhanamDate.MaxDate = DateTime.Now.AddMonths(4).Date;

            dtChequeDate.Value = DateTime.Now.Date;
            dtChequeDate.MinDate = DateTime.Now.AddMonths(-2).Date;
            dtChequeDate.MaxDate = DateTime.Now.Date;

            LoadCountryDetails();
            PaymentModeDetails();

            txtoldReceiptNumber.Visible = false;
            btnEditDetails.Visible = false;
            lblSearchRecipt.Visible = false;

            lstConfiguration = SqlHelper.GetConfigurationDetails();
            NoOfMonths = lstConfiguration.Where(x => x.effectiveDate <= DateTime.Now.Date).Select(x => x.NoOfMonths).FirstOrDefault();
            GetAnnadhanamAvailabilityDate();
            onPageload = false;
        }

        /// <summary>
        /// The AnnaBabaCharities_Load.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void AnnaBabaCharities_Load(object sender, EventArgs e)
        {
            GetReceiptNumber();
        } 
        #endregion

        /// <summary>
        /// The StartTimer.
        /// </summary>
        private void StartTimer()
        {
            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 1000;
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Enabled = true;
        }

        /// <summary>
        /// The tmr_Tick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        internal void tmr_Tick(object sender, EventArgs e)
        {
            lblDateValue.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// The PaymentModeDetails.
        /// </summary>
        public void PaymentModeDetails()
        {
            cmbPaymentMode.DataSource = Enum.GetNames(typeof(PaymentMode));
            cmbPaymentMode.SelectedItem = PaymentMode.CASH;

            txtChequeNumber.Enabled = false;
            txtDrawnOn.Enabled = false;
            dtChequeDate.Enabled = false;
        }

        /// <summary>
        /// The dtAnadhanamDate_ValueChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void dtAnadhanamDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime dtAnandhanDate = dtAnadhanamDate.Value.Date;

            if (dtAnandhanDate.Day == 1 && dtAnandhanDate.Month == 1)
            {
                MessageBox.Show("Anadhanam cannot be made on this day,please choose other date !...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtAnandhanDate.Date > DateTime.Now.AddMonths(4).Date)
            {
                dtAnadhanamDate.Value = DateTime.Now.AddMonths(4).Date;
                MessageBox.Show("Anadhanam date must not exceed more than 4 months !...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dtAnandhanDate.Date < DateTime.Now.Date)
            {
                dtAnadhanamDate.Value = DateTime.Now.Date;
                MessageBox.Show("Anadhanam date must be Today or Above,You cannot select past date !...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// The GetReceiptNumber.
        /// </summary>
        private void GetReceiptNumber()
        {
            txtReceiptNumber.Text = SqlHelper.GetLastReceiptNumber();
            txtReceiptNumber.Enabled = false;

            this.txtoldReceiptNumber.Text = Convert.ToString((Convert.ToInt32(this.txtReceiptNumber.Text) - 1));
            this.txtoldReceiptNumber.Visible = false;
        }

        /// <summary>
        /// The LoadCountryDetails.
        /// </summary>
        private void LoadCountryDetails()
        {
            int countryCode = 0;

            lstCountryDetails = new List<CountryDetails>();
            lstCountryDetails = SqlHelper.GetCountryDetails();

            if (lstCountryDetails?.Count > 0)
            {
                countryCode = Convert.ToInt32(lstCountryDetails.Where(x => x.CountryName.ToUpper() == "INDIA").Select(x => x.CountryID).FirstOrDefault());
            }
            else
            {
                lstCountryDetails = new List<CountryDetails>()
                    {
                        new CountryDetails { CountryID = "0", CountryName = ("<--No Records-->").ToString() }
                    };

                countryCode = Convert.ToInt32(lstCountryDetails.Where(x => x.CountryName == "< --No Records-- >").Select(x => x.CountryID).FirstOrDefault());
            }

            cmbCountry.DataSource = new BindingSource(lstCountryDetails, null);
            cmbCountry.DisplayMember = "CountryName";
            cmbCountry.ValueMember = "CountryID";
            cmbCountry.SelectedValue = Convert.ToString(countryCode);
            if (countryCode != 0)
            {
                LoadStateDetails(countryCode);
            }
        }

        /// <summary>
        /// The LoadStateDetails.
        /// </summary>
        /// <param name="countryCode">The countryCode<see cref="int"/>.</param>
        private void LoadStateDetails(int countryCode)
        {
            int stateCode = 0;
            if (countryCode != 0)
            {
                cmbState.Enabled = true;
                cmbCity.Enabled = false;

                try
                {
                    lstStateDetails = new List<StateDetails>();
                    lstStateDetails = SqlHelper.GetStateDetails(countryCode);

                    if (lstStateDetails?.Count > 0)
                    {
                        if (onPageload == true)
                        {
                            stateCode = Convert.ToInt32(lstStateDetails.Where(x => x.StateName.ToUpper() == "TAMIL NADU").Select(x => x.StateID).FirstOrDefault());
                        }
                        else
                        {
                            stateCode = Convert.ToInt32(lstStateDetails.Select(x => x.StateID).FirstOrDefault());
                        }
                    }
                    else
                    {
                        lstStateDetails = new List<StateDetails>
                        {
                           new StateDetails { CountryID = "0", StateID = "0", StateName = ("<--No Records-->").ToString() }
                        };
                        stateCode = Convert.ToInt32(lstStateDetails.Where(x => x.StateName == "< --No Records-- >").Select(x => x.StateID).FirstOrDefault());
                    }

                    cmbState.DataSource = new BindingSource(lstStateDetails, null);
                    cmbState.DisplayMember = "StateName";
                    cmbState.ValueMember = "StateID";
                    cmbState.SelectedValue = Convert.ToString(stateCode);

                    if (stateCode != 0)
                    {
                        LoadCityDetails(stateCode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                cmbState.Enabled = false;
                cmbCity.Enabled = false;
            }
        }

        /// <summary>
        /// The LoadCityDetails.
        /// </summary>
        /// <param name="statecode">The statecode<see cref="int"/>.</param>
        /// <returns>The <see cref="DataSet"/>.</returns>
        private DataSet LoadCityDetails(int statecode)
        {
            if (statecode != 0)
            {
                try
                {
                    int intCityCode;

                    lstCityDetails = new List<CityDetails>();
                    lstCityDetails = SqlHelper.GetCityDetails(statecode);


                    if (lstCityDetails?.Count > 0)
                    {
                        cmbCity.Enabled = true;
                        intCityCode = Convert.ToInt32(lstCityDetails.Select(x => x.CityID).FirstOrDefault());
                    }
                    else
                    {
                        lstCityDetails.Add(new CityDetails { CountryID = "0", StateID = "0", CityID = "0", CityName = ("<--No Records-->").ToString() });
                        intCityCode = Convert.ToInt32(lstCityDetails.Where(x => x.CityName == "< --No Records-- >").Select(x => x.StateID).FirstOrDefault());
                        cmbCity.Enabled = false;

                    }
                    cmbCity.DataSource = new BindingSource(lstCityDetails, null);
                    cmbCity.DisplayMember = "CityName";
                    cmbCity.ValueMember = "CityID";
                    cmbCity.SelectedValue = lstCityDetails.FirstOrDefault().CityID.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                cmbCity.Enabled = false;
            }
            return null;
        }

        /// <summary>
        /// The btnClear_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            onPageload = true;
            LoadCountryDetails();
            PaymentModeDetails();
            txtAddress.Text = "";
            txtName.Text = "";
            txtChequeNumber.Text = "";
            txtDrawnOn.Text = "";

            txtMobileNumber.Text = "";
            lstConfiguration = SqlHelper.GetConfigurationDetails();
            NoOfMonths = lstConfiguration.Where(x => x.effectiveDate <= DateTime.Now.Date).Select(x => x.NoOfMonths).FirstOrDefault();
            GetAnnadhanamAvailabilityDate();
            dtChequeDate.Value = DateTime.Now.Date;
            onPageload = false;
        }

        /// <summary>
        /// The GetAnnadhanamAvailabilityDate.
        /// </summary>
        private void GetAnnadhanamAvailabilityDate()
        {
            lstAvailability = SqlHelper.GetAnnadhanamAvailabilityDate(NoOfMonths);
            dtAnadhanamDate.MinDate = lstAvailability.Where(n => n.balanceReceiptCount > 0).Select(n => n.annadhanamDate.Date).FirstOrDefault();
            dtAnadhanamDate.Value = lstAvailability.Where(n => n.balanceReceiptCount > 0).Select(n => n.annadhanamDate.Date).FirstOrDefault();
        }

        /// <summary>
        /// The btnPrint_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {

            BookedCount = SqlHelper.GetAnnadhanamDayCountByDate(dtAnadhanamDate.Value.Date);
            TotalCount = SqlHelper.GetAnnadhanamTotalCountByDate(dtAnadhanamDate.Value.Date);
            if (BookedCount<TotalCount)
            {
               
                if (ValidateDevoteeDetails())
                {
                    Devotee devotee = AssignDevoteeDetails();
                    SqlHelper.CreateAnnadhanam(devotee);
                    clear();
                    txtReceiptNumber.Text = SqlHelper.GetLastReceiptNumber();
                }
            }
            else
            {
                MessageBox.Show("We have already issued " + TotalCount + " receipt for the Anadhanam Date (" + dtAnadhanamDate.Value.Date.ToString("dd-MMM-yyyy") + ") Please Choose Another Date!...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        internal bool ValidateDevoteeDetails()
        {
            bool blnSubmit = true;

            if (string.IsNullOrWhiteSpace(txtMobileNumber.Text) && txtMobileNumber.Text.Length <= 10)
            {
                blnSubmit = false;
                MessageBox.Show("Please enter valid mobile number !...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return blnSubmit;
            }

            if (string.IsNullOrWhiteSpace(txtReceiptNumber.Text))
            {
                blnSubmit = false;
                MessageBox.Show("Please enter valid receipt number !...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return blnSubmit;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                blnSubmit = false;
                MessageBox.Show("Please enter valid devotee Name !...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return blnSubmit;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                blnSubmit = false;
                MessageBox.Show("Please enter valid devotee address !...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return blnSubmit;
            }

            CountryDetails country = (CountryDetails)cmbCountry.SelectedItem;
            if (string.IsNullOrWhiteSpace(country.CountryName))
            {
                blnSubmit = false;
                MessageBox.Show("Please choose county details !...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return blnSubmit;
            }

            StateDetails state = (StateDetails)cmbState.SelectedItem;
            if (string.IsNullOrWhiteSpace(state.StateName))
            {
                blnSubmit = false;
                MessageBox.Show("Please choose state details !...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return blnSubmit;
            }

            CityDetails city = (CityDetails)cmbCity.SelectedItem;
            if (string.IsNullOrWhiteSpace(city.CityName))
            {
                blnSubmit = false;
                MessageBox.Show("Please choose city details !...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return blnSubmit;
            }

            if (dtAnadhanamDate.Value.Day == 1 && dtAnadhanamDate.Value.Month == 1)
            {
                blnSubmit = false;
                MessageBox.Show("Anadhanam cannot be made on this day,please choose other date !...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return blnSubmit;
            }
            else if (dtAnadhanamDate.Value.Date > DateTime.Now.AddMonths(4).Date)
            {
                blnSubmit = false;
                string strMessage = "Anadhanam date must not exceed more than 4 months (" + (DateTime.Now.AddMonths(4).Date).ToString("dd-MMM-yyyy") + ")!...";
                MessageBox.Show(strMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return blnSubmit;
            }
            else if (dtAnadhanamDate.Value.Date < DateTime.Now.Date)
            {
                blnSubmit = false;
                MessageBox.Show("Anadhanam date must be Today or Above,You cannot select past date !...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return blnSubmit;
            }

            if (!string.IsNullOrWhiteSpace(Convert.ToString(cmbPaymentMode.Text.ToUpper())))
            {
                string strPaymentMode = Convert.ToString(cmbPaymentMode.Text.ToUpper());

                if (strPaymentMode == "CHEQUE")
                {
                    if (string.IsNullOrWhiteSpace(txtChequeNumber.Text))
                    {
                        blnSubmit = false;
                        MessageBox.Show("Please enter cheque number & proceed !...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return blnSubmit;
                    }

                    if (string.IsNullOrWhiteSpace(txtDrawnOn.Text))
                    {
                        blnSubmit = false;
                        MessageBox.Show("Please enter cheque drawn details & Proceed !...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return blnSubmit;
                    }
                }
            }
            else
            {
                blnSubmit = false;
                MessageBox.Show("Please Select Payment Mode!...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return blnSubmit;
            }

            return blnSubmit;
        }

        /// <summary>
        /// The AssignDevoteeDetails.
        /// </summary>
        /// <returns>The <see cref="Devotee"/>.</returns>
        private Devotee AssignDevoteeDetails()
        {
            Devotee devotee = new Devotee();
            devotee.ReceiptNumber = Convert.ToInt32(txtReceiptNumber.Text);
            devotee.DevoteeName = Convert.ToString(txtName.Text);
            devotee.Address = Convert.ToString(txtAddress.Text);

            CountryDetails selCountry = (CountryDetails)cmbCountry.SelectedItem;
            devotee.CountryCode = Convert.ToInt32(selCountry.CountryID);
            devotee.Country = Convert.ToString(selCountry.CountryName);

            StateDetails selState = (StateDetails)cmbState.SelectedItem;
            devotee.StateCode = Convert.ToInt32(selState.StateID);
            devotee.State = Convert.ToString(selState.StateName);

            CityDetails selCity = (CityDetails)cmbCity.SelectedItem;
            devotee.CityCode = Convert.ToInt32(selCity.CityID);
            devotee.City = Convert.ToString(selCity.CityName);

            devotee.Amount = 500;
            devotee.ReceiptCreatedDate = DateTime.Now.Date;
            devotee.AnadhanamDate = Convert.ToDateTime(dtAnadhanamDate.Value.Date);
            devotee.ContactNumber = Convert.ToString(txtMobileNumber.Text);

            if (cmbPaymentMode.SelectedIndex == 1)
            {
                devotee.PaymentMode = PaymentMode.CHEQUE;
                devotee.ChequeNo = Convert.ToString(txtChequeNumber.Text);
                devotee.ChequeDate = Convert.ToDateTime(dtChequeDate.Value.Date);
                devotee.ChequeDrawn = Convert.ToString(txtDrawnOn.Text);
            }
            else
            {
                devotee.PaymentMode = PaymentMode.CASH;
            }

            return devotee;
        }

        /// <summary>
        /// The clear.
        /// </summary>
        private void clear()
        {
            onPageload = true;
            LoadCountryDetails();
            PaymentModeDetails();

            txtAddress.Text = "";
            txtName.Text = "";
            txtChequeNumber.Text = "";
            txtDrawnOn.Text = "";
            txtMobileNumber.Text = "";

            lstConfiguration = SqlHelper.GetConfigurationDetails();
            NoOfMonths = lstConfiguration.Where(x => x.effectiveDate <= DateTime.Now.Date).Select(x => x.NoOfMonths).FirstOrDefault();
            GetAnnadhanamAvailabilityDate();

            dtChequeDate.Value = DateTime.Now.Date;
            onPageload = false;
        }

        /// <summary>
        /// The button2_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            Availability obj = new Availability(dtAnadhanamDate.Value.Date);
            obj.ShowDialog();
        }

        /// <summary>
        /// The btnEditDetails_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void btnLoadDevoteeDetails_Click(object sender, EventArgs e)
        {
            if (this.txtoldReceiptNumber.Text != "" && this.txtoldReceiptNumber.Text != null)
            {
                if (this.txtoldReceiptNumber.Visible == true)
                {
                    LoadDevoteeDetails(this.txtoldReceiptNumber.Text);
                }
            }
            else
            {
                MessageBox.Show("Enter Valid Receipt Number", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The GetData.
        /// </summary>
        /// <param name="strReceiptNumber">The strReceiptNumber<see cref="string"/>.</param>
        private void LoadDevoteeDetails(string strReceiptNumber)
        {
            try
            {
                Devotee devotee = SqlHelper.GetDevoteeDetails(strReceiptNumber);
                if (devotee?.ReceiptNumber != null)
                {
                    {
                        txtName.Text = Convert.ToString(devotee.DevoteeName);
                        txtAddress.Text = Convert.ToString(devotee.Address);
                        txtMobileNumber.Text = Convert.ToString(devotee.ContactNumber);

                        lstCountryDetails = SqlHelper.GetCountryDetails();
                        string countryCode = lstCountryDetails.Where(x => x.CountryName == devotee.Country).Select(x => x.CountryID).FirstOrDefault();
                        cmbCountry.SelectedValue = countryCode;

                        lstStateDetails = SqlHelper.GetStateDetails(Convert.ToInt32(countryCode));
                        string stateCode = (from p in lstStateDetails where p.StateName.Contains(Convert.ToString(devotee.State).Trim()) select p.StateID).FirstOrDefault();
                        cmbState.SelectedValue = stateCode;

                        lstCityDetails = SqlHelper.GetCityDetails(Convert.ToInt32(stateCode));
                        string cityCode = lstCityDetails.Where(x => x.CityName == devotee.City).Select(x => x.CityID).FirstOrDefault();
                        cmbCity.SelectedValue = cityCode;

                        if (devotee.PaymentMode == PaymentMode.CHEQUE)
                        {
                            txtChequeNumber.Enabled = true;
                            txtDrawnOn.Enabled = true;
                            dtChequeDate.Enabled = true;

                            txtChequeNumber.Text = (!string.IsNullOrWhiteSpace(devotee.ChequeNo)) ? devotee.ChequeNo : string.Empty;
                            txtDrawnOn.Text = (!string.IsNullOrWhiteSpace(devotee.ChequeDrawn)) ? devotee.ChequeDrawn : string.Empty;
                            if (devotee?.ChequeDate.Date != null)
                            {
                                DateTime dtPreviousChequeDate = Convert.ToDateTime(devotee.ChequeDate).Date;
                                if (dtPreviousChequeDate >= dtChequeDate.MinDate && dtPreviousChequeDate <= dtChequeDate.MinDate)
                                    dtChequeDate.Value = Convert.ToDateTime(devotee.ChequeDate).Date;
                            }
                            cmbPaymentMode.SelectedIndex = 1;
                        }
                        else
                        {
                            txtChequeNumber.Text = devotee.ChequeNo;
                            txtDrawnOn.Text = devotee.ChequeDrawn;
                            cmbPaymentMode.SelectedIndex = 0;
                            dtChequeDate.Value = DateTime.Now.Date;

                            txtChequeNumber.Enabled = false;
                            txtDrawnOn.Enabled = false;
                            dtChequeDate.Enabled = false;
                        }
                    }
                    this.txtoldReceiptNumber.Visible = false;
                    this.checkBox1.Checked = false;
                    this.btnEditDetails.Visible = false;
                    lblSearchRecipt.Visible = false;
                }
                else
                {
                    MessageBox.Show("Enter Valid Receipt / Mobile Number", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.txtoldReceiptNumber.Visible = true;
                    this.checkBox1.Checked = true;
                    this.btnEditDetails.Visible = true;
                    lblSearchRecipt.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The txtOldReceiptNumber_KeyPress.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="KeyPressEventArgs"/>.</param>
        private void txtOldReceiptNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// The checkBox1_CheckedChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == true)
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

        /// <summary>
        /// The AnnaBabaCharities_FormClosing.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="FormClosingEventArgs"/>.</param>
        private void AnnaBabaCharities_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
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
        /// The cmbPaymentMode_SelectedValueChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void cmbPaymentMode_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbPaymentMode.Text.ToUpper() == "CASH")
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

        /// <summary>
        /// The cmbCountry_SelectedValueChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void cmbCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbCountry.SelectedItem != null)
            {
                CountryDetails selectedCountry = (CountryDetails)cmbCountry.SelectedItem;
                LoadStateDetails(Convert.ToInt32(selectedCountry.CountryID));
            }
        }

        /// <summary>
        /// The cmbState_SelectedValueChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void cmbState_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbState.SelectedItem != null)
            {
                StateDetails selectedState = (StateDetails)cmbState.SelectedItem;
                LoadCityDetails(Convert.ToInt32(selectedState.StateID));
            }
        }

        /// <summary>
        /// The cmbCity_SelectedValueChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void cmbCity_SelectedValueChanged(object sender, EventArgs e)
        {
            CityDetails selectedCity = (CityDetails)cmbCity.SelectedItem;
        }

        /// <summary>
        /// The txtMobileNumber_KeyPress.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="KeyPressEventArgs"/>.</param>
        private void txtMobileNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }
    }
}
