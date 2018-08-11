namespace DressDetails.Forms
{
    partial class EditForm
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
            this.ddlYear = new System.Windows.Forms.ComboBox();
            this.grbReceiptDetails = new System.Windows.Forms.GroupBox();
            this.lblReceiptNumber = new System.Windows.Forms.Label();
            this.ddlMonth = new System.Windows.Forms.ComboBox();
            this.lblSearchRecipt = new System.Windows.Forms.Label();
            this.lblDateValue = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgMonthdetails = new System.Windows.Forms.DataGridView();
            this.lblUsername = new System.Windows.Forms.Label();
            this.grbReceiptDetails.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMonthdetails)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlYear
            // 
            this.ddlYear.DropDownHeight = 200;
            this.ddlYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlYear.DropDownWidth = 100;
            this.ddlYear.IntegralHeight = false;
            this.ddlYear.ItemHeight = 13;
            this.ddlYear.Location = new System.Drawing.Point(194, 19);
            this.ddlYear.MaxDropDownItems = 13;
            this.ddlYear.Name = "ddlYear";
            this.ddlYear.Size = new System.Drawing.Size(86, 21);
            this.ddlYear.TabIndex = 13;
            this.ddlYear.SelectedValueChanged += new System.EventHandler(this.ddlYear_SelectedValueChanged);
            // 
            // grbReceiptDetails
            // 
            this.grbReceiptDetails.Controls.Add(this.ddlYear);
            this.grbReceiptDetails.Controls.Add(this.lblReceiptNumber);
            this.grbReceiptDetails.Controls.Add(this.ddlMonth);
            this.grbReceiptDetails.Controls.Add(this.lblSearchRecipt);
            this.grbReceiptDetails.Controls.Add(this.lblDateValue);
            this.grbReceiptDetails.Controls.Add(this.lblDate);
            this.grbReceiptDetails.Location = new System.Drawing.Point(29, 68);
            this.grbReceiptDetails.Name = "grbReceiptDetails";
            this.grbReceiptDetails.Size = new System.Drawing.Size(919, 56);
            this.grbReceiptDetails.TabIndex = 9;
            this.grbReceiptDetails.TabStop = false;
            // 
            // lblReceiptNumber
            // 
            this.lblReceiptNumber.AutoSize = true;
            this.lblReceiptNumber.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceiptNumber.Location = new System.Drawing.Point(143, 19);
            this.lblReceiptNumber.Name = "lblReceiptNumber";
            this.lblReceiptNumber.Size = new System.Drawing.Size(50, 19);
            this.lblReceiptNumber.TabIndex = 12;
            this.lblReceiptNumber.Text = "Year .:";
            // 
            // ddlMonth
            // 
            this.ddlMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlMonth.IntegralHeight = false;
            this.ddlMonth.ItemHeight = 13;
            this.ddlMonth.Location = new System.Drawing.Point(402, 19);
            this.ddlMonth.MaxDropDownItems = 10;
            this.ddlMonth.Name = "ddlMonth";
            this.ddlMonth.Size = new System.Drawing.Size(147, 21);
            this.ddlMonth.TabIndex = 10;
            this.ddlMonth.SelectedValueChanged += new System.EventHandler(this.ddlMonth_SelectedValueChanged);
            // 
            // lblSearchRecipt
            // 
            this.lblSearchRecipt.AutoSize = true;
            this.lblSearchRecipt.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchRecipt.Location = new System.Drawing.Point(335, 20);
            this.lblSearchRecipt.Name = "lblSearchRecipt";
            this.lblSearchRecipt.Size = new System.Drawing.Size(68, 19);
            this.lblSearchRecipt.TabIndex = 8;
            this.lblSearchRecipt.Text = "Month .:";
            // 
            // lblDateValue
            // 
            this.lblDateValue.AutoSize = true;
            this.lblDateValue.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateValue.Location = new System.Drawing.Point(658, 21);
            this.lblDateValue.Name = "lblDateValue";
            this.lblDateValue.Size = new System.Drawing.Size(0, 18);
            this.lblDateValue.TabIndex = 4;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(610, 20);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(49, 19);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "Date :";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Blue;
            this.lblHeader.Location = new System.Drawing.Point(269, 15);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(439, 42);
            this.lblHeader.TabIndex = 8;
            this.lblHeader.Text = "ANNA BABA CHARITIES";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgMonthdetails);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox1.Location = new System.Drawing.Point(29, 132);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(919, 490);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Monthly Details : ";
            // 
            // dgMonthdetails
            // 
            this.dgMonthdetails.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgMonthdetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMonthdetails.Location = new System.Drawing.Point(17, 26);
            this.dgMonthdetails.Name = "dgMonthdetails";
            this.dgMonthdetails.Size = new System.Drawing.Size(896, 442);
            this.dgMonthdetails.TabIndex = 0;
            this.dgMonthdetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMonthdetails_CellContentClick);
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.Maroon;
            this.lblUsername.Location = new System.Drawing.Point(743, 27);
            this.lblUsername.MinimumSize = new System.Drawing.Size(200, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(200, 17);
            this.lblUsername.TabIndex = 12;
            this.lblUsername.Text = "Welcome: ";
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Turquoise;
            this.ClientSize = new System.Drawing.Size(976, 640);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.grbReceiptDetails);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.groupBox1);
            this.Name = "EditForm";
            this.Text = "EditForm";
            this.grbReceiptDetails.ResumeLayout(false);
            this.grbReceiptDetails.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMonthdetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddlYear;
        private System.Windows.Forms.GroupBox grbReceiptDetails;
        private System.Windows.Forms.Label lblReceiptNumber;
        private System.Windows.Forms.ComboBox ddlMonth;
        private System.Windows.Forms.Label lblSearchRecipt;
        private System.Windows.Forms.Label lblDateValue;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgMonthdetails;
        private System.Windows.Forms.Label lblUsername;
    }
}