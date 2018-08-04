namespace ANNABABA.Forms
{
    partial class DressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.grbReceiptDetails = new System.Windows.Forms.GroupBox();
            this.ddlYear = new System.Windows.Forms.ComboBox();
            this.ddlMonth = new System.Windows.Forms.ComboBox();
            this.lblReceiptNumber = new System.Windows.Forms.Label();
            this.lblSearchRecipt = new System.Windows.Forms.Label();
            this.lblDateValue = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgMonthdetails = new System.Windows.Forms.DataGridView();
            this.grbReceiptDetails.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMonthdetails)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Blue;
            this.lblHeader.Location = new System.Drawing.Point(131, 13);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(439, 42);
            this.lblHeader.TabIndex = 2;
            this.lblHeader.Text = "ANNA BABA CHARITIES";
            // 
            // grbReceiptDetails
            // 
            this.grbReceiptDetails.Controls.Add(this.ddlYear);
            this.grbReceiptDetails.Controls.Add(this.ddlMonth);
            this.grbReceiptDetails.Controls.Add(this.lblReceiptNumber);
            this.grbReceiptDetails.Controls.Add(this.lblSearchRecipt);
            this.grbReceiptDetails.Controls.Add(this.lblDateValue);
            this.grbReceiptDetails.Controls.Add(this.lblDate);
            this.grbReceiptDetails.Location = new System.Drawing.Point(12, 62);
            this.grbReceiptDetails.Name = "grbReceiptDetails";
            this.grbReceiptDetails.Size = new System.Drawing.Size(693, 56);
            this.grbReceiptDetails.TabIndex = 3;
            this.grbReceiptDetails.TabStop = false;
            // 
            // ddlYear
            // 
            this.ddlYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlYear.IntegralHeight = false;
            this.ddlYear.ItemHeight = 13;
            this.ddlYear.Location = new System.Drawing.Point(352, 19);
            this.ddlYear.MaxDropDownItems = 10;
            this.ddlYear.Name = "ddlYear";
            this.ddlYear.Size = new System.Drawing.Size(86, 21);
            this.ddlYear.TabIndex = 11;
            this.ddlYear.SelectedValueChanged += new System.EventHandler(this.ddlYear_SelectedValueChanged);
            // 
            // ddlMonth
            // 
            this.ddlMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlMonth.IntegralHeight = false;
            this.ddlMonth.ItemHeight = 13;
            this.ddlMonth.Location = new System.Drawing.Point(122, 19);
            this.ddlMonth.MaxDropDownItems = 10;
            this.ddlMonth.Name = "ddlMonth";
            this.ddlMonth.Size = new System.Drawing.Size(147, 21);
            this.ddlMonth.TabIndex = 10;
            this.ddlMonth.SelectedValueChanged += new System.EventHandler(this.ddlMonth_SelectedValueChanged);
            // 
            // lblReceiptNumber
            // 
            this.lblReceiptNumber.AutoSize = true;
            this.lblReceiptNumber.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceiptNumber.Location = new System.Drawing.Point(301, 19);
            this.lblReceiptNumber.Name = "lblReceiptNumber";
            this.lblReceiptNumber.Size = new System.Drawing.Size(46, 19);
            this.lblReceiptNumber.TabIndex = 9;
            this.lblReceiptNumber.Text = "Year :";
            // 
            // lblSearchRecipt
            // 
            this.lblSearchRecipt.AutoSize = true;
            this.lblSearchRecipt.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchRecipt.Location = new System.Drawing.Point(55, 20);
            this.lblSearchRecipt.Name = "lblSearchRecipt";
            this.lblSearchRecipt.Size = new System.Drawing.Size(68, 19);
            this.lblSearchRecipt.TabIndex = 8;
            this.lblSearchRecipt.Text = "Month .:";
            // 
            // lblDateValue
            // 
            this.lblDateValue.AutoSize = true;
            this.lblDateValue.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateValue.Location = new System.Drawing.Point(510, 21);
            this.lblDateValue.Name = "lblDateValue";
            this.lblDateValue.Size = new System.Drawing.Size(0, 18);
            this.lblDateValue.TabIndex = 4;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(462, 20);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(49, 19);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "Date :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgMonthdetails);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox1.Location = new System.Drawing.Point(14, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(691, 443);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Monthly Details : ";
            // 
            // dgMonthdetails
            // 
            this.dgMonthdetails.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgMonthdetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMonthdetails.Location = new System.Drawing.Point(17, 26);
            this.dgMonthdetails.Name = "dgMonthdetails";
            this.dgMonthdetails.Size = new System.Drawing.Size(661, 402);
            this.dgMonthdetails.TabIndex = 0;
            this.dgMonthdetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMonthdetails_CellContentClick);
            // 
            // BookDressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Turquoise;
            this.ClientSize = new System.Drawing.Size(719, 598);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbReceiptDetails);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BookDressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BookDress";
            this.grbReceiptDetails.ResumeLayout(false);
            this.grbReceiptDetails.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMonthdetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox grbReceiptDetails;
        private System.Windows.Forms.Label lblDateValue;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblSearchRecipt;
        private System.Windows.Forms.Label lblReceiptNumber;
        private System.Windows.Forms.ComboBox ddlMonth;
        private System.Windows.Forms.ComboBox ddlYear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgMonthdetails;
    }
}