namespace ANNABABA.Models
{
    using ANNABABA.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="Devotee" />.
    /// </summary>
    internal class Devotee
    {
        /// <summary>
        /// Gets or sets the ReceiptNumber.
        /// </summary>
        internal int ? ReceiptNumber { get; set; }

        /// <summary>
        /// Gets or sets the DevoteeName.
        /// </summary>
        internal string DevoteeName { get; set; }

        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        internal string Address { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        internal int ? CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        internal string Country { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        internal int ? StateCode { get; set; }

        /// <summary>
        /// Gets or sets the State.
        /// </summary>
        internal string State { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        internal int ? CityCode { get; set; }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        internal string City { get; set; }

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        internal int ? Amount { get; set; }

        /// <summary>
        /// Gets or sets the ReceiptCreatedDate.
        /// </summary>
        internal DateTime ReceiptCreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the AnadhanamDate.
        /// </summary>
        internal DateTime AnadhanamDate { get; set; }

        /// <summary>
        /// Gets or sets the ChequeNo.
        /// </summary>
        internal string ChequeNo { get; set; }

        /// <summary>
        /// Gets or sets the ChequeDate.
        /// </summary>
        internal DateTime ChequeDate { get; set; }

        /// <summary>
        /// Gets or sets the ChequeDrawn.
        /// </summary>
        internal string ChequeDrawn { get; set; }

        /// <summary>
        /// Gets or sets the Mode.
        /// </summary>
        internal PaymentMode PaymentMode { get; set; }

        /// <summary>
        /// Gets or sets the ContactNumber.
        /// </summary>
        internal string ContactNumber { get; set; }

        internal bool ValidateDevoteeDetails(Devotee devotee)
        {
            bool blnSubmit = true;

            if (string.IsNullOrWhiteSpace(devotee.ContactNumber.ToString()) && devotee.ContactNumber.ToString().Length < 10)
            {
                blnSubmit = false;
                MessageBox.Show("Please enter valid mobile number !...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return blnSubmit;
            }

            if (string.IsNullOrWhiteSpace(devotee.ReceiptNumber.ToString()))
            {
                blnSubmit = false;
                MessageBox.Show("Please enter valid receipt number !...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return blnSubmit;
            }

            if (string.IsNullOrWhiteSpace(devotee.DevoteeName))
            {
                blnSubmit = false;
                MessageBox.Show("Please enter valid devotee Name !...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return blnSubmit;
            }

            if (string.IsNullOrWhiteSpace(devotee.Address))
            {
                blnSubmit = false;
                MessageBox.Show("Please enter valid devotee address !...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return blnSubmit;
            }

            if (string.IsNullOrWhiteSpace(devotee.Country))
            {
                blnSubmit = false;
                MessageBox.Show("Please choose county details !...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return blnSubmit;
            }

            if (string.IsNullOrWhiteSpace(devotee.State))
            {
                blnSubmit = false;
                MessageBox.Show("Please choose state details !...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return blnSubmit;
            }

            if (string.IsNullOrWhiteSpace(devotee.City))
            {
                blnSubmit = false;
                MessageBox.Show("Please choose city details !...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return blnSubmit;
            }

            if (devotee.AnadhanamDate.Day == 1 && devotee.AnadhanamDate.Month == 1)
            {
                blnSubmit = false;
                MessageBox.Show("Anadhanam cannot be made on this day,please choose other date !...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return blnSubmit;
            }
            else if (devotee.AnadhanamDate.Date > DateTime.Now.AddMonths(4).Date)
            {
                blnSubmit = false;
                string strMessage = "Anadhanam date must not exceed more than 4 months (" + (DateTime.Now.AddMonths(4).Date).ToString("dd-MMM-yyyy") + ")!...";
                MessageBox.Show(strMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return blnSubmit;
            }
            else if (devotee.AnadhanamDate.Date < DateTime.Now.Date)
            {
                blnSubmit = false;
                MessageBox.Show("Anadhanam date must be Today or Above,You cannot select past date !...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return blnSubmit;
            }

            if (!string.IsNullOrWhiteSpace(Convert.ToString(devotee.PaymentMode)))
            {
                string strPaymentMode = Convert.ToString(devotee.PaymentMode);

                if (strPaymentMode == "CHEQUE")
                {
                    if (string.IsNullOrWhiteSpace(devotee.ChequeNo))
                    {
                        blnSubmit = false;
                        MessageBox.Show("Please enter cheque number & proceed !...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return blnSubmit;
                    }

                    if (string.IsNullOrWhiteSpace(devotee.ChequeDrawn))
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

    }
}
