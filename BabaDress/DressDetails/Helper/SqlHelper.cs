namespace DressDetails.Helper
{
    using DressDetails.Models;
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
        /// Gets or sets the _con.
        /// </summary>
        private static SqlCeConnection _con { get; set; }

        /// <summary>
        /// Gets or sets the sqlQuery.
        /// </summary>
        private static string sqlQuery { get; set; }

        /// <summary>
        /// Gets or sets the lstDressDetails.
        /// </summary>
        public static List<DevoteeDetails> lstDressDetails { get; set; }

        /// <summary>
        /// The OpenSqlCeConnection.
        /// </summary>
        private static void OpenSqlCeConnection()
        {
            try
            {
                _con = new SqlCeConnection(@"Data Source=D:\AnnaBaba\BabaDress.sdf;");
                _con.Open();
            }
            catch (SqlCeInvalidDatabaseFormatException ex)
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
        /// The GetDressDetails.
        /// </summary>
        /// <param name="Month">The Month<see cref="int"/>.</param>
        /// <param name="Year">The Year<see cref="int"/>.</param>
        /// <returns>The <see cref="List{DevoteeDetails}"/>.</returns>
        internal static List<DevoteeDetails> GetDressDetails(int Month, int Year)
        {
            int RowNumber = 1;
            int lastDayOfMonth = DateTime.DaysInMonth(Year, Month);
            DateTime startDate = new DateTime(year: Year, month: Month, day: 1, hour: 0, minute: 0, second: 0);
            DateTime endDate = new DateTime(year: Year, month: Month, day: lastDayOfMonth, hour: 23, minute: 59, second: 59);
            lstDressDetails = new List<DevoteeDetails>();

            for (DateTime dt = startDate; dt <= endDate; dt = dt.AddDays(1))
            {
                lstDressDetails.Add(new DevoteeDetails()
                {
                    RowNumber = Convert.ToString(RowNumber),
                    BookDate = dt.Date.ToString("dd-MMM-yyyy"),
                    Name = string.Empty,
                    Address = string.Empty,
                    ContactNumber = string.Empty,
                    Month = string.Empty,
                    Year = string.Empty,
                    IsCreated = string.Empty,
                    IsUpdated = string.Empty,
                    CreatedBy = string.Empty,
                });

                RowNumber++;
            }

            OpenSqlCeConnection();

            string strQuery = @"SELECT T1.DEVOTEENAME,T1.ADDRESS,T1.CONTACTNUMBER,T1.BOOKEDDATE,T1.BOOKEDMONTH,T1.BOOKEDYEAR,T2.NAME,T1.InsertedOn FROM  DRESSDETAILS T1 INNER JOIN LOGINDETAILS T2 ON T1.InsertedBy=T2.ID  WHERE T1.BookedMonth='" + Month + "' AND T1.BookedYear='" + Year + "'";

            SqlCeCommand cm = new SqlCeCommand(strQuery, _con)
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = _con
            };
            var dataReader = cm.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);

            if (dataTable?.Rows?.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    foreach (DevoteeDetails dress in lstDressDetails
                        .Where(n => Convert.ToDateTime(n.BookDate).Date == Convert.ToDateTime(dr.ItemArray[3]).Date))
                    {
                        dress.Name = Convert.ToString(dr.ItemArray[0]);
                        dress.Address = Convert.ToString(dr.ItemArray[1]);
                        dress.ContactNumber = Convert.ToString(dr.ItemArray[2]);
                        dress.BookDate = Convert.ToDateTime(dr.ItemArray[3]).ToString("dd-MMM-yyyy");
                        dress.Month = Convert.ToString(dr.ItemArray[4]);
                        dress.Year = Convert.ToString(dr.ItemArray[5]);
                        dress.CreatedBy = Convert.ToString(dr.ItemArray[6]);
                        dress.CreatedOn = Convert.ToDateTime(dr.ItemArray[7]);
                    }
                }
            }

            _con.Close();

            return lstDressDetails;
        }

        /// <summary>
        /// The Login.
        /// </summary>
        /// <param name="username">The username<see cref="string"/>.</param>
        /// <param name="password">The password<see cref="string"/>.</param>
        /// <param name="IsAdmin">The IsAdmin<see cref="bool"/>.</param>
        /// <returns>The <see cref="LoginDetails"/>.</returns>
        internal static LoginDetails Login(string username, string password, bool IsAdmin)
        {
            LoginDetails loginDetails = null;
            OpenSqlCeConnection();

            if (IsAdmin)
                sqlQuery = @"SELECT ID,Name,IsAdmin FROM  LOGINDETAILS WHERE UserName='" + username + "' AND Password='" + password + "' AND IsActive=1 AND IsAdmin=1";
            else
                sqlQuery = @"SELECT ID,Name,IsAdmin FROM  LOGINDETAILS WHERE UserName='" + username + "' AND Password='" + password + "' AND IsActive=1 AND IsAdmin=0";

            SqlCeCommand cm = new SqlCeCommand(sqlQuery, _con)
            {
                CommandText = sqlQuery,
                CommandType = CommandType.Text,
                Connection = _con
            };
            var dataReader = cm.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            if (dataTable.Rows.Count > 0)
            {
                loginDetails = new LoginDetails();
                loginDetails = (from DataRow dr in dataTable.Rows
                                select new LoginDetails()
                                {
                                    Id = Convert.ToInt32(dr.ItemArray[0]),
                                    Name = Convert.ToString(dr.ItemArray[1]),
                                    IsAdmin = Convert.ToString(dr.ItemArray[2]),
                                    IsAdminUser =Convert.ToBoolean(dr.ItemArray[2])
                                }).FirstOrDefault();
            }
            _con.Close();
            return loginDetails;
        }

        /// <summary>
        /// The InsertDressDetails.
        /// </summary>
        /// <param name="date">The date<see cref="string"/>.</param>
        /// <param name="devoteeName">The devoteeName<see cref="string"/>.</param>
        /// <param name="address">The address<see cref="string"/>.</param>
        /// <param name="contactNumber">The contactNumber<see cref="string"/>.</param>
        /// <param name="userId">The userId<see cref="int"/>.</param>
        internal static void InsertDressDetails(string date, string devoteeName, string address, string contactNumber, int userId)
        {
            DateTime bookingDate = DateTime.Parse(date);

            string month = bookingDate.Month.ToString(), year = bookingDate.Year.ToString();

            sqlQuery = "INSERT INTO DRESSDETAILS(BOOKEDDATE,DEVOTEENAME,ADDRESS,CONTACTNUMBER,BOOKEDMONTH,BOOKEDYEAR,InsertedBy,InsertedOn)VALUES(@BOOKEDDATE,@DEVOTEENAME,@ADDRESS,@CONTACTNUMBER,@BOOKEDMONTH,@BOOKEDYEAR,@InsertedBy,@InsertedOn)";

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

            SqlCeCommand cmd = new SqlCeCommand(sqlQuery, _con);
            cmd.Parameters.AddWithValue("@BOOKEDDATE", bookingDate);
            cmd.Parameters.AddWithValue("@DEVOTEENAME", devoteeName.ToUpper());
            cmd.Parameters.AddWithValue("@ADDRESS", address);
            cmd.Parameters.AddWithValue("@CONTACTNUMBER", contactNumber);
            cmd.Parameters.AddWithValue("@BOOKEDMONTH", month);
            cmd.Parameters.AddWithValue("@BOOKEDYEAR", year);
            cmd.Parameters.AddWithValue("@InsertedBy", userId.ToString());
            cmd.Parameters.AddWithValue("@InsertedOn", DateTime.Now.Date.ToString());
            try
            {
                int intAffectedRow = cmd.ExecuteNonQuery();
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

        /// <summary>
        /// The CheckIfRecordExistsDb.
        /// </summary>
        /// <param name="bookingDate">The bookingDate<see cref="string"/>.</param>
        /// <param name="month">The month<see cref="string"/>.</param>
        /// <param name="year">The year<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        internal static bool ValidateBeforeEdit(string bookingDate, string month, string year)
        {
            bool isExists = false;
            try
            {
                if (DateTime.TryParse(bookingDate, out DateTime result))
                {
                    OpenSqlCeConnection();


                    sqlQuery = @"SELECT BOOKEDDATE FROM  DRESSDETAILS  WHERE BOOKEDDATE='" + result.Date + "'";
                    SqlCeCommand cm = new SqlCeCommand(sqlQuery, _con)
                    {
                        CommandText = sqlQuery,
                        CommandType = CommandType.Text,
                        Connection = _con
                    };
                    var dataReader = cm.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        int count = (from DataRow dr in dataTable.Rows
                                     where (dr.ItemArray[0] != null && Convert.ToDateTime(dr.ItemArray[0]).Date == Convert.ToDateTime(bookingDate).Date)
                                     select new DevoteeDetails()
                                     {
                                         BookDate = Convert.ToString(dr.ItemArray[0]),
                                     }).Count();

                        if (count == 1)
                            isExists= true;
                    }
                }
                return isExists;
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


        internal static bool ValidateBeforeCreate(string bookingDate, string month, string year)
        {
            bool isExists = true;
            try
            {

                if (DateTime.TryParse(bookingDate,out DateTime result))
                {
                    OpenSqlCeConnection();


                    sqlQuery = @"SELECT BOOKEDDATE FROM  DRESSDETAILS  WHERE BOOKEDDATE='" + result.Date + "'";
                    SqlCeCommand cm = new SqlCeCommand(sqlQuery, _con)
                    {
                        CommandText = sqlQuery,
                        CommandType = CommandType.Text,
                        Connection = _con
                    };
                    var dataReader = cm.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        int count = (from DataRow dr in dataTable.Rows
                                     where (dr.ItemArray[0] != null && 
                                     Convert.ToDateTime(dr.ItemArray[0]).Date == Convert.ToDateTime(bookingDate).Date)
                                     select new DevoteeDetails()
                                     {
                                         BookDate = Convert.ToString(dr.ItemArray[0]),
                                     }).Count();

                        if (count == 1)
                            isExists = false;
                    }
                }
                return isExists;
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

        /// <summary>
        /// The UpdateDressDetails.
        /// </summary>
        /// <param name="date">The date<see cref="string"/>.</param>
        /// <param name="devoteeName">The devoteeName<see cref="string"/>.</param>
        /// <param name="address">The address<see cref="string"/>.</param>
        /// <param name="contactNumber">The contactNumber<see cref="string"/>.</param>
        /// <param name="userId">The userId<see cref="int"/>.</param>
        internal static void UpdateDressDetails(string date, string devoteeName, string address, string contactNumber, int userId)
        {
            DateTime bookingDate = DateTime.Parse(date);

            sqlQuery = "UPDATE DRESSDETAILS Set DEVOTEENAME='" + devoteeName.ToUpper() + "',Address='" + address.Trim() + "',ContactNumber='" + contactNumber + "',UpdatedBy='" + userId + "',UpdatedOn='" + DateTime.Now +
                                "' WHERE BOOKEDDATE='" + bookingDate + "'";

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

            var cm = new SqlCeCommand(sqlQuery, _con);
            try
            {
                var intAffectedRow = cm.ExecuteNonQuery();
                if (intAffectedRow > 0)
                {
                    MessageBox.Show(@"Updated Sucessfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(@"Updation Failed", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                _con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The DeleteDressDetails.
        /// </summary>
        /// <param name="date">The date<see cref="string"/>.</param>
        internal static void DeleteDressDetails(string date)
        {
            DateTime bookingDate = DateTime.Parse(date);

            sqlQuery = "DELETE FROM DRESSDETAILS WHERE BOOKEDDATE='" + bookingDate + "'";
            OpenSqlCeConnection();

            var cm = new SqlCeCommand(sqlQuery, _con);
            try
            {
                var intAffectedRow = cm.ExecuteNonQuery();
                if (intAffectedRow > 0)
                {
                    MessageBox.Show(@"Deleted Sucessfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(@"Deletion Failed", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                _con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The LoadProfileDetails.
        /// </summary>
        /// <returns>The <see cref="DataTable"/>.</returns>
        internal static DataTable LoadProfileDetails()
        {
            DataTable dataTable = null;
            try
            {
                OpenSqlCeConnection();

                string strQuery = @"SELECT Id,Name,Address,UserName,ContactNumber,ISAdmin,ISActive FROM LOGINDETAILS";
                SqlCeCommand cm = new SqlCeCommand(strQuery, _con)
                {
                    CommandText = strQuery,
                    CommandType = CommandType.Text,
                    Connection = _con
                };
                var dataReader = cm.ExecuteReader();
                dataTable = new DataTable();
                dataTable.Load(dataReader);

                _con.Close();

            }
            catch (SqlCeException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }

        /// <summary>
        /// The btnCreate_Click.
        /// </summary>
        /// <param name="loginDetails">The loginDetails<see cref="LoginDetails"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        internal static bool AddProfile(LoginDetails loginDetails)
        {
            bool isSucceed = false;

            sqlQuery = "INSERT INTO LOGINDETAILS(NAME,ADDRESS,USERNAME,PASSWORD,CONTACTNUMBER,ISADMIN,ISACTIVE,INSERTEDBY)VALUES(@NAME,@ADDRESS,@USERNAME,@PASSWORD,@CONTACTNUMBER,@ISADMIN,@ISACTIVE,@INSERTEDBY)";
            OpenSqlCeConnection();

            SqlCeCommand cm = new SqlCeCommand(sqlQuery, _con);
            cm.Parameters.AddWithValue("@NAME", loginDetails.Name);
            cm.Parameters.AddWithValue("@ADDRESS", loginDetails.Address);
            cm.Parameters.AddWithValue("@USERNAME", loginDetails.UserName);
            cm.Parameters.AddWithValue("@PASSWORD", loginDetails.Password);
            cm.Parameters.AddWithValue("@CONTACTNUMBER", loginDetails.ContactNumber);
            cm.Parameters.AddWithValue("@ISADMIN", loginDetails.IsAdminUser);
            cm.Parameters.AddWithValue("@ISACTIVE", loginDetails.IsActiveUser);
            cm.Parameters.AddWithValue("@INSERTEDBY", loginDetails.UserId);
            try
            {
                int intAffectedRow = cm.ExecuteNonQuery();
                if (intAffectedRow > 0)
                {
                    isSucceed = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _con.Close();
            return isSucceed;
        }

        /// <summary>
        /// The UpdateProfile.
        /// </summary>
        /// <param name="loginDetails">The loginDetails<see cref="LoginDetails"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        internal static bool UpdateProfile(LoginDetails loginDetails)
        {
            bool isSucceed = false;
            int isAdmin = 0, isActive = 0;

            if (loginDetails.IsAdminUser)
                isAdmin = 1;
            if (loginDetails.IsActiveUser)
                isActive = 1;

            sqlQuery = "UPDATE [LOGINDETAILS] Set [Address]='" + loginDetails.Address + "', [Password] ='" + loginDetails.Password + "', [ContactNumber] ='" + loginDetails.ContactNumber +
                "', [IsActive] =" + isActive + ", [IsAdmin] =" + isAdmin + " WHERE [ID]='" + loginDetails.Id + "'";

            OpenSqlCeConnection();

            SqlCeCommand cm = new SqlCeCommand(sqlQuery, _con);
            try
            {
                var intAffectedRow = cm.ExecuteNonQuery();
                if (intAffectedRow > 0)
                {
                    isSucceed = true;
                }

                _con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            return isSucceed;
        }

        /// <summary>
        /// The GetProfileDetailsById.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="LoginDetails"/>.</returns>
        internal static LoginDetails GetProfileDetailsById(int id)
        {
            LoginDetails loginDetails = null;
            string strQuery = @"SELECT Id,Name,Address,UserName,Password,ContactNumber,ISAdmin,ISActive FROM LOGINDETAILS Where ID=" + id;

            OpenSqlCeConnection();
            SqlCeCommand cm = new SqlCeCommand(strQuery, _con)
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = _con
            };
            var dataReader = cm.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            if (dataTable.Rows.Count > 0)
            {
                loginDetails = new LoginDetails();
                loginDetails = (from DataRow dr in dataTable.Rows
                                select new LoginDetails()
                                {
                                    Id = Convert.ToInt32(dr.ItemArray[0]),
                                    Name = Convert.ToString(dr.ItemArray[1]),
                                    Address = Convert.ToString(dr.ItemArray[2]),
                                    UserName = Convert.ToString(dr.ItemArray[3]),
                                    Password = Convert.ToString(dr.ItemArray[4]),
                                    ContactNumber = Convert.ToString(dr.ItemArray[5]),
                                    IsAdmin = Convert.ToBoolean(dr.ItemArray[6]) == true ? "Yes" : "No",
                                    IsActive = Convert.ToBoolean(dr.ItemArray[7]) == true ? "Yes" : "No"
                                }).FirstOrDefault();

            }
            else
                MessageBox.Show(@"Enter Valid ID!...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            _con.Close();

            return loginDetails;
        }
    }
}
