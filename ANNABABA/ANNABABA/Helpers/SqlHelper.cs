namespace ANNABABA.Helpers
{
    using ANNABABA.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlServerCe;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="SqlHelper" />.
    /// </summary>
    public static class SqlHelper
    {
        /// <summary>
        /// Gets or sets the sqlQuery.
        /// </summary>
        private static string sqlQuery { get; set; }

        /// <summary>
        /// Gets or sets the receiptNumber.
        /// </summary>
        private static string receiptNumber { get; set; }

        /// <summary>
        /// Gets or sets the lstCountryList
        /// Defines the CountryList.........
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
        /// Gets or sets the lstAvailability.
        /// </summary>
        private static List<Availability> lstAvailability { get; set; }

        /// <summary>
        /// Gets or sets the lstAvailabilityByWeek.
        /// </summary>
        private static List<Availability> lstAvailabilityByWeek { get; set; }

        /// <summary>
        /// Gets or sets the lstConfiguration.
        /// </summary>
        private static List<ConfigurationDetails> lstConfiguration { get; set; }

        /// <summary>
        /// Defines the con.
        /// </summary>
        private static SqlCeConnection con = new SqlCeConnection(@"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf");

        /// <summary>
        /// The OpenSqlCeConnection.
        /// </summary>
        private static void OpenSqlCeConnection()
        {
            try
            {
                con = new SqlCeConnection(@"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf");
                con.Open();
            }
            catch (SqlCeInvalidDatabaseFormatException ex)
            {
                string connStringCI = @"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf; LCID= 1033";
                string connStringCS = @"Data Source=D:\AnnaBaba\ABCAnadhanamDetails.sdf; LCID= 1033; Case Sensitive=true";

                SqlCeEngine engine = new SqlCeEngine(connStringCI);
                engine.Upgrade(connStringCS);

                con = null;
                con = new SqlCeConnection(connStringCI);
                con.Open();
            }
            catch (SqlCeException ex)
            {
                MessageBox.Show(ex.Message, "Annadhanam", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Annadhanam", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        /// <summary>
        /// The AddConfiguration.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="ConfigurationDetails"/>.</param>
        internal static void AddConfiguration(ConfigurationDetails configuration)
        {
            OpenSqlCeConnection();
            sqlQuery = "INSERT INTO configuration ([EffectiveDate],[NoOfReceipts],[NoOfMonths])VALUES(@EffectiveDate,@NoOfReceipts, @NoOfMonths)";

            SqlCeCommand cm = new SqlCeCommand(sqlQuery, con);
            cm.Parameters.AddWithValue("@EffectiveDate", configuration.effectiveDate);
            cm.Parameters.AddWithValue("@NoOfReceipts", configuration.TotalNoOfReceipts);
            cm.Parameters.AddWithValue("@NoOfMonths", configuration.NoOfMonths);
            try
            {
                int intAffectedRow = cm.ExecuteNonQuery();
                con.Close();

                if (intAffectedRow > 0)
                {
                    MessageBox.Show("Configuration were added sucessfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Insertion Failed", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The GetLastReceiptDetails.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetLastReceiptNumber()
        {
            try
            {
                OpenSqlCeConnection();
                sqlQuery = "SELECT coalesce(MAX(ReceiptNumber),0) as LastReceiptNumber  FROM tblAnnadhanamDetails";
                SqlCeCommand cm = new SqlCeCommand(sqlQuery, con);
                SqlCeDataReader reader;

                cm.CommandText = sqlQuery;
                cm.CommandType = CommandType.Text;
                cm.Connection = con;

                reader = cm.ExecuteReader();

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        string s = String.Format("{0}", reader["LastReceiptNumber"]);
                        receiptNumber = ((s == "") ? "1" : (Convert.ToInt32(s) + 1).ToString());
                    }
                }
                reader.Close();
                con.Close();

            }
            catch (SqlCeException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return receiptNumber;
        }

        /// <summary>
        /// The GetCountryDetails.
        /// </summary>
        /// <returns>The <see cref="List{CountryDetails}"/>.</returns>
        internal static List<CountryDetails> GetCountryDetails()
        {
            lstCountryDetails = new List<CountryDetails>();

            OpenSqlCeConnection();

            try
            {
                sqlQuery = "SELECT CountryId,CountyName,IsActive FROM CountryDetails WHERE IsActive='Y'";

                SqlCeCommand cm = new SqlCeCommand(sqlQuery, con)
                {
                    CommandText = sqlQuery,
                    CommandType = CommandType.Text,
                    Connection = con
                };

                SqlCeDataReader dataReader = cm.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        lstCountryDetails.Add(new CountryDetails
                        {
                            CountryID = Convert.ToString(dr.ItemArray[0]),
                            CountryName = Convert.ToString(dr.ItemArray[1]),
                            isActive = Convert.ToString(dr.ItemArray[2])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            return lstCountryDetails;
        }

        /// <summary>
        /// The GetStateDetails.
        /// </summary>
        /// <param name="intCountryCode">The intCountryCode<see cref="int"/>.</param>
        /// <returns>The <see cref="List{StateDetails}"/>.</returns>
        internal static List<StateDetails> GetStateDetails(int intCountryCode)
        {
            lstStateDetails = new List<StateDetails>();
            OpenSqlCeConnection();
            try
            {
                sqlQuery = "Select CountryId,StateId,StateName,IsActive from StateDetails WHERE CountryId=" + intCountryCode + " AND IsActive='Y' ORDER BY StateName ASC ";
                SqlCeCommand cm = new SqlCeCommand(sqlQuery, con)
                {
                    CommandText = sqlQuery,
                    CommandType = CommandType.Text,
                    Connection = con
                };

                var dataReader = cm.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    lstStateDetails = (from DataRow dr in dataTable.Rows
                                       select new StateDetails()
                                       {
                                           CountryID = Convert.ToString(dr.ItemArray[0]),
                                           StateID = Convert.ToString(dr.ItemArray[1]),
                                           StateName = Convert.ToString(dr.ItemArray[2]),
                                           isActive = Convert.ToString(dr.ItemArray[3])
                                       }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            return lstStateDetails;
        }

        /// <summary>
        /// The CreateAnnadhanam.
        /// </summary>
        /// <param name="devotee">The devotee<see cref="Devotee"/>.</param>
        internal static void CreateAnnadhanam(Devotee devotee)
        {
            if (devotee.PaymentMode == PaymentMode.CASH)
            {
                sqlQuery = "INSERT INTO tblAnnadhanamDetails(ReceiptNumber,DevoteeName,Address,CountryCode,Country,StateCode,State,CityCode,City,Amount,ReceiptCreatedDate,AnadhanamDate,Mode,ContactNumber)VALUES(@ReceiptNumber,@DevoteeName, @Address,@CountryCode,@Country,@StateCode,@State,@CityCode,@City,@Amount,@ReceiptCreatedDate,@AnadhanamDate,@Mode,@ContactNumber)";
            }
            else
            {
                sqlQuery = "INSERT INTO tblAnnadhanamDetails(ReceiptNumber,DevoteeName,Address,CountryCode,Country,StateCode,State,CityCode,City,Amount,ReceiptCreatedDate,AnadhanamDate,ChequeNo,ChequeDate,ChequeDrawn,Mode,ContactNumber)VALUES(@ReceiptNumber,@DevoteeName, @Address,@CountryCode,@Country,@StateCode,@State,@CityCode,@City,@Amount,@ReceiptCreatedDate,@AnadhanamDate,@ChequeNo,@ChequeDate,@ChequeDrawn,@Mode,@ContactNumber)";
            }

            DateTime dtAnadhanamDates = new DateTime(devotee.AnadhanamDate.Date.Year, devotee.AnadhanamDate.Date.Month, devotee.AnadhanamDate.Date.Day, 12, 0, 0);
            OpenSqlCeConnection();

            SqlCeCommand cm = new SqlCeCommand(sqlQuery, con);
            cm.Parameters.AddWithValue("@ReceiptNumber", devotee.ReceiptNumber);
            cm.Parameters.AddWithValue("@DevoteeName", devotee.DevoteeName);
            cm.Parameters.AddWithValue("@Address", devotee.Address);
            cm.Parameters.AddWithValue("@CountryCode", devotee.CountryCode);
            cm.Parameters.AddWithValue("@Country", devotee.Country);
            cm.Parameters.AddWithValue("@StateCode", devotee.StateCode);
            cm.Parameters.AddWithValue("@State", devotee.State);
            cm.Parameters.AddWithValue("@CityCode", devotee.CityCode);
            cm.Parameters.AddWithValue("@City", devotee.City);
            cm.Parameters.AddWithValue("@Amount", 500);
            cm.Parameters.AddWithValue("@ReceiptCreatedDate", DateTime.Now.Date);
            cm.Parameters.AddWithValue("@AnadhanamDate", dtAnadhanamDates);
            cm.Parameters.AddWithValue("@Mode", devotee.PaymentMode);
            cm.Parameters.AddWithValue("@ContactNumber", devotee.ContactNumber);

            if (devotee.PaymentMode == PaymentMode.CHEQUE)
            {
                cm.Parameters.AddWithValue("@ChequeNo", devotee.ContactNumber);
                cm.Parameters.AddWithValue("@ChequeDate", devotee.ChequeDate);
                cm.Parameters.AddWithValue("@ChequeDrawn", devotee.ChequeDrawn);
            }

            try
            {
                int intAffectedRow = cm.ExecuteNonQuery();
                con.Close();

                if (intAffectedRow > 0)
                {
                    MessageBox.Show("Anadhanam Created Sucessfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Receipt receiptForm = new Receipt(devotee);
                    receiptForm.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Insertion Failed", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The UpdateAnnadhanam.
        /// </summary>
        /// <param name="devotee">The devotee<see cref="Devotee"/>.</param>
        internal static void UpdateAnnadhanam(Devotee devotee)
        {
            DateTime dtAnadhanamDates = new DateTime(devotee.AnadhanamDate.Date.Year, devotee.AnadhanamDate.Date.Month, devotee.AnadhanamDate.Date.Day, 12, 0, 0);

            OpenSqlCeConnection();

            string paymentMode = string.Empty;
            if (devotee.PaymentMode == PaymentMode.CASH)
            {
                sqlQuery = "UPDATE tblAnnadhanamDetails Set DevoteeName='" + devotee.DevoteeName.Trim() + "',Address='" + devotee.Address + "',CountryCode=" + devotee.CountryCode + ",Country='" + devotee.Country + "',StateCode=" + devotee.StateCode + ",State='" + devotee.State + "',CityCode=" + devotee.CityCode + ",City='" + devotee.City + "',AnadhanamDate='" + dtAnadhanamDates.ToString("dd-MMM-yyyy HH:mm:ss") + "', Mode='" + devotee.PaymentMode + "',ContactNumber='" + devotee.ContactNumber + "' WHERE ReceiptNumber='" + devotee.ReceiptNumber.ToString() + "'";
            }
            else
            {
                sqlQuery = "UPDATE tblAnnadhanamDetails Set DevoteeName='" + devotee.DevoteeName.Trim() + "',Address='" + devotee.Address + "',CountryCode=" + devotee.CountryCode + ",Country='" + devotee.Country + "',StateCode=" + devotee.StateCode + ",State='" + devotee.State + "',CityCode=" + devotee.CityCode + ",City='" + devotee.City + "',AnadhanamDate='" + dtAnadhanamDates.ToString("dd-MMM-yyyy HH:mm:ss") + "',ChequeNo='" + devotee.ChequeNo + "',ChequeDate='" + devotee.ChequeDate + "',ChequeDrawn='" + devotee.ChequeDrawn + "',Mode='" + devotee.PaymentMode + "',ContactNumber='" + devotee.ContactNumber + "' WHERE ReceiptNumber='" + devotee.ReceiptNumber.ToString() + "'";
            }

            try
            {
                SqlCeCommand cm = new SqlCeCommand(sqlQuery, con);
                int intAffectedRow = cm.ExecuteNonQuery();
                con.Close();

                if (intAffectedRow > 0)
                {
                    MessageBox.Show("Anadhanam Updated Sucessfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Receipt receiptForm = new Receipt(devotee);
                    receiptForm.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Updation Failed", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The GetCityDetails.
        /// </summary>
        /// <param name="intStateCode">The intStateCode<see cref="int"/>.</param>
        /// <returns>The <see cref="List{CityDetails}"/>.</returns>
        internal static List<CityDetails> GetCityDetails(int intStateCode)
        {
            lstCityDetails = new List<CityDetails>();
            if (intStateCode != 0)
            {
                OpenSqlCeConnection();
                try
                {
                    sqlQuery = "Select CountryId,StateId,CityId,CityName,IsActive from CITYDETAILS WHERE StateId= " + intStateCode + " AND IsActive='Y' ORDER BY CityName ASC ";
                    SqlCeCommand cm = new SqlCeCommand(sqlQuery, con)
                    {
                        CommandText = sqlQuery,
                        CommandType = CommandType.Text,
                        Connection = con
                    };

                    var dataReader = cm.ExecuteReader();
                    var dataTable = new DataTable();
                    dataTable.Load(dataReader);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        lstCityDetails = (from DataRow dr in dataTable.Rows
                                          select new CityDetails()
                                          {
                                              CountryID = Convert.ToString(dr.ItemArray[0]),
                                              StateID = Convert.ToString(dr.ItemArray[1]),
                                              CityID = Convert.ToString(dr.ItemArray[2]),
                                              CityName = Convert.ToString(dr.ItemArray[3]),
                                              isActive = Convert.ToString(dr.ItemArray[4])
                                          }).ToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }
            }
            return lstCityDetails;
        }

        /// <summary>
        /// The GetAnnadhanamCountDetails.
        /// </summary>
        /// <param name="annadhanamDate">The annadhanamDate<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        internal static int GetAnnadhanamDayCountByDate(DateTime annadhanamDate)
        {
            int bookedcount = 0;
            try
            {

                OpenSqlCeConnection();
                DateTime dtFrom = new DateTime(annadhanamDate.Year, annadhanamDate.Month, annadhanamDate.Day, 0, 0, 0);
                DateTime dtTo = new DateTime(annadhanamDate.Year, annadhanamDate.Month, annadhanamDate.Day, 23, 59, 59);
                sqlQuery = "SELECT COUNT(ReceiptNumber) AS CNT FROM  tblAnnadhanamDetails WHERE (AnadhanamDate>='" + dtFrom + "' AND  AnadhanamDate<='" + dtTo + "')";

                using (con)
                {
                    using (SqlCeDataAdapter da = new SqlCeDataAdapter())
                    {
                        SqlCeCommand cmd = new SqlCeCommand(sqlQuery, con)
                        {
                            Connection = con
                        };
                        da.SelectCommand = cmd;

                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        con.Close();


                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            if (dr.ItemArray[0] != null)
                                bookedcount = Convert.ToInt32(dr.ItemArray[0]);
                        }
                    }
                }
            }
            catch (SqlCeException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return bookedcount;
        }

        /// <summary>
        /// The GetData.
        /// </summary>
        /// <param name="strReceiptNumber">The strReceiptNumber<see cref="string"/>.</param>
        /// <returns>The <see cref="DataSet"/>.</returns>
        internal static Devotee GetDevoteeDetails(string strReceiptNumber)
        {
            Devotee devotee = new Devotee();
            OpenSqlCeConnection();
            try
            {
                sqlQuery = "SELECT Top 1 ReceiptNumber,DevoteeName,Address,Country,State,City,Amount,ReceiptCreatedDate,AnadhanamDate,ChequeNo,ChequeDate,ChequeDrawn,Mode,ContactNumber FROM  tblAnnadhanamDetails WHERE ReceiptNumber='" + strReceiptNumber + "' OR ContactNumber='" + strReceiptNumber + "' Order by ReceiptCreatedDate DESC ";
                using (con)
                {
                    using (SqlCeDataAdapter da = new SqlCeDataAdapter())
                    {
                        SqlCeCommand cmd = new SqlCeCommand(sqlQuery, con)
                        {
                            Connection = con
                        };
                        da.SelectCommand = cmd;

                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        con.Close();


                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            if (dr != null)
                            {
                                if (!string.IsNullOrWhiteSpace(Convert.ToString(dr.ItemArray[12])) && Convert.ToString(dr.ItemArray[12]).ToUpper() == "CHEQUE")
                                {
                                    devotee.PaymentMode = PaymentMode.CHEQUE;
                                    devotee.ChequeNo = Convert.ToString(dr.ItemArray[9]);
                                    devotee.ChequeDate = Convert.ToDateTime(dr.ItemArray[10]);
                                    devotee.ChequeDrawn = Convert.ToString(dr.ItemArray[11]);
                                }
                                else
                                {
                                    devotee.PaymentMode = PaymentMode.CASH;
                                }
                                devotee.ReceiptNumber = Convert.ToInt32(dr.ItemArray[0]);
                                devotee.DevoteeName = Convert.ToString(dr.ItemArray[1]);
                                devotee.Address = Convert.ToString(dr.ItemArray[2]);
                                devotee.Country = Convert.ToString(dr.ItemArray[3]);
                                devotee.State = Convert.ToString(dr.ItemArray[4]);
                                devotee.City = Convert.ToString(dr.ItemArray[5]);
                                devotee.Amount = Convert.ToInt32(dr.ItemArray[6]);
                                devotee.ReceiptCreatedDate = Convert.ToDateTime(dr.ItemArray[7]);
                                devotee.AnadhanamDate = Convert.ToDateTime(dr.ItemArray[8]);
                                devotee.ContactNumber = Convert.ToString(dr.ItemArray[13]);
                            }
                        }
                    }
                }
            }
            catch (SqlCeException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return devotee;
        }

        /// <summary>
        /// The GetAnnadhanamCountDetails.
        /// </summary>
        /// <param name="annadhanamDate">The annadhanamDate<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        internal static int GetAnnadhanamTotalCountByDate(DateTime annadhanamDate)
        {
            int totalReceiptCount = 0;
            try
            {
                OpenSqlCeConnection();
                annadhanamDate = new DateTime(annadhanamDate.Year, annadhanamDate.Month, annadhanamDate.Day, 23, 59, 59);
                sqlQuery = "SELECT TOP 1 NoOfReceipts FROM configuration WHERE EffectiveDate<='" + annadhanamDate + "' ORDER BY EffectiveDate DESC";

                using (con)
                {
                    using (SqlCeDataAdapter da = new SqlCeDataAdapter())
                    {
                        SqlCeCommand cmd = new SqlCeCommand(sqlQuery, con)
                        {
                            Connection = con
                        };
                        da.SelectCommand = cmd;

                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        con.Close();


                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            if (dr.ItemArray[0] != null)
                                totalReceiptCount = Convert.ToInt32(dr.ItemArray[0]);
                        }
                    }
                }
            }
            catch (SqlCeException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return totalReceiptCount;
        }

        /// <summary>
        /// The GetAnnadhanamAvailabilityDate.
        /// </summary>
        /// <param name="noofMonths">The noofMonths<see cref="int"/>.</param>
        /// <returns>The <see cref="List{Availability}"/>.</returns>
        internal static List<Availability> GetAnnadhanamAvailabilityDate(int noofMonths)
        {
            lstAvailability = new List<Availability>();

            DateTime PeriodFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime PeriodTo = new DateTime(DateTime.Now.AddMonths(noofMonths).Date.Year, DateTime.Now.AddMonths(noofMonths).Date.Month, DateTime.Now.AddMonths(noofMonths).Date.Day, 23, 59, 59);

            for (DateTime dt = PeriodFrom; dt <= PeriodTo; dt = dt.AddDays(1))
            {
                lstAvailability.Add(new Availability { annadhanamDate = dt.Date, bookedReceiptCount = 0 });
            }

            try
            {

                OpenSqlCeConnection();
                string strSelectQuery = "SELECT AnadhanamDate,COALESCE(COUNT(ReceiptNumber),0)  FROM tblAnnadhanamDetails WHERE AnadhanamDate>='" + PeriodFrom.ToString("dd-MMM-yyyy") + "' AND AnadhanamDate <='" + PeriodTo.ToString("dd-MMM-yyyy") + "' GROUP BY AnadhanamDate ORDER BY AnadhanamDate ASC";
                SqlCeCommand cm = new SqlCeCommand(strSelectQuery, con)
                {
                    CommandText = strSelectQuery,
                    CommandType = CommandType.Text,
                    Connection = con
                };

                var dataReader = cm.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    List<ConfigurationDetails> configurationDetails = new List<ConfigurationDetails>();
                    configurationDetails = SqlHelper.GetConfigurationDetails();

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        foreach (Availability avail in lstAvailability.Where(n => n.annadhanamDate.Date == Convert.ToDateTime(dr.ItemArray[0]).Date))
                        {
                            avail.bookedReceiptCount = Convert.ToInt32(dr.ItemArray[1]);
                        }
                    }

                    foreach (Availability avail in lstAvailability)
                    {
                        ConfigurationDetails config = configurationDetails
                            .OrderByDescending(n => n.effectiveDate)
                            .Where(n => n.effectiveDate.Date <= avail.annadhanamDate.Date)
                            .FirstOrDefault();

                        avail.totalReceiptCount = config.TotalNoOfReceipts;
                        avail.balanceReceiptCount = (config.TotalNoOfReceipts - avail.bookedReceiptCount);
                    }
                }

                con.Close();
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
            finally
            {
            }
            return lstAvailability;
        }

        /// <summary>
        /// The GetAnnadhanamAvailabilityDate.
        /// </summary>
        /// <param name="dtAnnadhanamDate">The dtAnnadhanamDate<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="List{Availability}"/>.</returns>
        internal static List<Availability> GetAnnadhanamAvailabilityByWeek(DateTime dtAnnadhanamDate)
        {
            lstAvailabilityByWeek = new List<Availability>();

            DateTime PeriodFrom = new DateTime(dtAnnadhanamDate.Year, dtAnnadhanamDate.Month, dtAnnadhanamDate.Day, 0, 0, 0);
            DateTime PeriodTo = new DateTime(dtAnnadhanamDate.Date.AddDays(7).Year, dtAnnadhanamDate.Date.AddDays(7).Month, dtAnnadhanamDate.Date.AddDays(7).Day, 23, 59, 59);

            for (DateTime dt = PeriodFrom; dt <= PeriodTo; dt = dt.AddDays(1))
            {
                lstAvailabilityByWeek.Add(new Availability { annadhanamDate = dt.Date, bookedReceiptCount = 0 });
            }

            try
            {

                OpenSqlCeConnection();
                string strSelectQuery = "SELECT AnadhanamDate,COALESCE(COUNT(ReceiptNumber),0)  FROM tblAnnadhanamDetails WHERE AnadhanamDate>='" + PeriodFrom.ToString("dd-MMM-yyyy") + "' AND AnadhanamDate <='" + PeriodTo.ToString("dd-MMM-yyyy") + "' GROUP BY AnadhanamDate ORDER BY AnadhanamDate ASC";
                SqlCeCommand cm = new SqlCeCommand(strSelectQuery, con)
                {
                    CommandText = strSelectQuery,
                    CommandType = CommandType.Text,
                    Connection = con
                };

                var dataReader = cm.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    List<ConfigurationDetails> configurationDetails = new List<ConfigurationDetails>();
                    configurationDetails = SqlHelper.GetConfigurationDetails();

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        foreach (Availability avail in lstAvailabilityByWeek.Where(n => n.annadhanamDate.Date == Convert.ToDateTime(dr.ItemArray[0]).Date))
                        {
                            avail.bookedReceiptCount = Convert.ToInt32(dr.ItemArray[1]);
                        }
                    }

                    foreach (Availability avail in lstAvailabilityByWeek)
                    {
                        ConfigurationDetails config = configurationDetails
                            .OrderByDescending(n => n.effectiveDate)
                            .Where(n => n.effectiveDate.Date <= avail.annadhanamDate.Date)
                            .FirstOrDefault();

                        avail.totalReceiptCount = config.TotalNoOfReceipts;
                        avail.balanceReceiptCount = (config.TotalNoOfReceipts - avail.bookedReceiptCount);
                    }
                }

                con.Close();
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
            finally
            {
            }
            return lstAvailabilityByWeek;
        }

        /// <summary>
        /// The GetAnnadhanamCountDetails.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        internal static List<ConfigurationDetails> GetConfigurationDetails()
        {
            lstConfiguration = new List<ConfigurationDetails>();
            try
            {
                OpenSqlCeConnection();
                sqlQuery = "SELECT EffectiveDate, NoOfReceipts,NoOfMonths FROM configuration order by EffectiveDate Desc";

                using (con)
                {
                    using (SqlCeDataAdapter da = new SqlCeDataAdapter())
                    {
                        SqlCeCommand cmd = new SqlCeCommand(sqlQuery, con)
                        {
                            Connection = con
                        };
                        da.SelectCommand = cmd;

                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        con.Close();


                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                lstConfiguration.Add(new ConfigurationDetails
                                {
                                    effectiveDate = Convert.ToDateTime(dr.ItemArray[0]),
                                    TotalNoOfReceipts = Convert.ToInt32(dr.ItemArray[1]),
                                    NoOfMonths = Convert.ToInt32(dr.ItemArray[2]),
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlCeException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lstConfiguration;
        }

        /// <summary>
        /// The GetAnnadhanamAvailabilityDate.
        /// </summary>
        /// <param name="dtAnnadhanamDate">The dtAnnadhanamDate<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="List{Availability}"/>.</returns>
        internal static List<Devotee> GetListOfReceiptNumberbyAnnaDhanamDate(DateTime dtAnnadhanamDate)
        {
            List<Devotee> lstReceiptNumber = new List<Devotee>();
            DateTime PeriodFrom = new DateTime(dtAnnadhanamDate.Date.Year, dtAnnadhanamDate.Date.Month, dtAnnadhanamDate.Date.Day, 1, 0, 0);
            DateTime PeriodTo = new DateTime(dtAnnadhanamDate.Date.Year, dtAnnadhanamDate.Date.Month, dtAnnadhanamDate.Date.Day, 23, 59, 59);

            try
            {

                OpenSqlCeConnection();
                string strSelectQuery = "SELECT COALESCE(ReceiptNumber,0)  FROM tblAnnadhanamDetails WHERE AnadhanamDate>='" + PeriodFrom.ToString() + "' AND AnadhanamDate <='" + PeriodTo.ToString() + "' ORDER BY ReceiptNumber ASC";
                SqlCeCommand cm = new SqlCeCommand(strSelectQuery, con)
                {
                    CommandText = strSelectQuery,
                    CommandType = CommandType.Text,
                    Connection = con
                };

                var dataReader = cm.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        lstReceiptNumber.Add(new Devotee { ReceiptNumber = Convert.ToInt32(dr.ItemArray[0]) });
                    }
                }
                con.Close();
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
            finally
            {
            }
            return lstReceiptNumber;
        }
    }
}
