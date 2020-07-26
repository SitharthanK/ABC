namespace ANNABABA
{
    partial class Edit
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
            this.button2 = new System.Windows.Forms.Button();
            this.lblDrawnOn = new System.Windows.Forms.Label();
            this.txtDrawnOn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMobileNumber = new System.Windows.Forms.TextBox();
            this.lblMobile = new System.Windows.Forms.Label();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.cmbCity = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtChequeDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtChequeNumber = new System.Windows.Forms.TextBox();
            this.cmbPaymentMode = new System.Windows.Forms.ComboBox();
            this.lblPaymentMode = new System.Windows.Forms.Label();
            this.lblAnnadhanamDate = new System.Windows.Forms.Label();
            this.dtAnadhanamDate = new System.Windows.Forms.DateTimePicker();
            this.lblState = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblDateValue = new System.Windows.Forms.Label();
            this.grbReceiptDetails = new System.Windows.Forms.GroupBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.txtReceiptNumber = new System.Windows.Forms.TextBox();
            this.lblReceiptNumber = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grbReceiptDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(348, 226);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 28);
            this.button2.TabIndex = 20;
            this.button2.Text = "Availability";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblDrawnOn
            // 
            this.lblDrawnOn.AutoSize = true;
            this.lblDrawnOn.ForeColor = System.Drawing.Color.Black;
            this.lblDrawnOn.Location = new System.Drawing.Point(117, 388);
            this.lblDrawnOn.Name = "lblDrawnOn";
            this.lblDrawnOn.Size = new System.Drawing.Size(90, 19);
            this.lblDrawnOn.TabIndex = 17;
            this.lblDrawnOn.Text = "Drawn On : ";
            this.lblDrawnOn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDrawnOn
            // 
            this.txtDrawnOn.Location = new System.Drawing.Point(208, 384);
            this.txtDrawnOn.MaxLength = 100;
            this.txtDrawnOn.Name = "txtDrawnOn";
            this.txtDrawnOn.Size = new System.Drawing.Size(392, 27);
            this.txtDrawnOn.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(97, 349);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 19);
            this.label3.TabIndex = 15;
            this.label3.Text = "Cheque Date :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMobileNumber);
            this.groupBox1.Controls.Add(this.lblMobile);
            this.groupBox1.Controls.Add(this.cmbState);
            this.groupBox1.Controls.Add(this.cmbCountry);
            this.groupBox1.Controls.Add(this.lblCountry);
            this.groupBox1.Controls.Add(this.cmbCity);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.lblDrawnOn);
            this.groupBox1.Controls.Add(this.txtDrawnOn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtChequeDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtChequeNumber);
            this.groupBox1.Controls.Add(this.cmbPaymentMode);
            this.groupBox1.Controls.Add(this.lblPaymentMode);
            this.groupBox1.Controls.Add(this.lblAnnadhanamDate);
            this.groupBox1.Controls.Add(this.dtAnadhanamDate);
            this.groupBox1.Controls.Add(this.lblState);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox1.Location = new System.Drawing.Point(20, 113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(691, 438);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Devotee Information : ";
            // 
            // txtMobileNumber
            // 
            this.txtMobileNumber.Location = new System.Drawing.Point(423, 265);
            this.txtMobileNumber.MaxLength = 10;
            this.txtMobileNumber.Name = "txtMobileNumber";
            this.txtMobileNumber.Size = new System.Drawing.Size(177, 27);
            this.txtMobileNumber.TabIndex = 32;
            this.txtMobileNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMobileNumber_KeyPress);
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.ForeColor = System.Drawing.Color.Black;
            this.lblMobile.Location = new System.Drawing.Point(350, 268);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(69, 19);
            this.lblMobile.TabIndex = 31;
            this.lblMobile.Text = "Mobile : ";
            this.lblMobile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbState
            // 
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.IntegralHeight = false;
            this.cmbState.Location = new System.Drawing.Point(206, 150);
            this.cmbState.MaxDropDownItems = 10;
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(162, 27);
            this.cmbState.TabIndex = 30;
            this.cmbState.SelectedValueChanged += new System.EventHandler(this.cmbState_SelectedValueChanged);
            // 
            // cmbCountry
            // 
            this.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountry.Location = new System.Drawing.Point(208, 188);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(159, 27);
            this.cmbCountry.TabIndex = 28;
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.ForeColor = System.Drawing.Color.Black;
            this.lblCountry.Location = new System.Drawing.Point(131, 190);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(76, 19);
            this.lblCountry.TabIndex = 29;
            this.lblCountry.Text = "Country : ";
            this.lblCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCity
            // 
            this.cmbCity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCity.DropDownHeight = 300;
            this.cmbCity.IntegralHeight = false;
            this.cmbCity.Location = new System.Drawing.Point(423, 148);
            this.cmbCity.MaxDropDownItems = 20;
            this.cmbCity.Name = "cmbCity";
            this.cmbCity.Size = new System.Drawing.Size(177, 27);
            this.cmbCity.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(372, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 19);
            this.label4.TabIndex = 25;
            this.label4.Text = "City : ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtChequeDate
            // 
            this.dtChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtChequeDate.Location = new System.Drawing.Point(209, 345);
            this.dtChequeDate.Name = "dtChequeDate";
            this.dtChequeDate.Size = new System.Drawing.Size(129, 27);
            this.dtChequeDate.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(74, 310);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "Cheque Number : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtChequeNumber
            // 
            this.txtChequeNumber.Location = new System.Drawing.Point(207, 306);
            this.txtChequeNumber.MaxLength = 100;
            this.txtChequeNumber.Name = "txtChequeNumber";
            this.txtChequeNumber.Size = new System.Drawing.Size(393, 27);
            this.txtChequeNumber.TabIndex = 12;
            // 
            // cmbPaymentMode
            // 
            this.cmbPaymentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMode.FormattingEnabled = true;
            this.cmbPaymentMode.Location = new System.Drawing.Point(208, 267);
            this.cmbPaymentMode.MaxDropDownItems = 2;
            this.cmbPaymentMode.Name = "cmbPaymentMode";
            this.cmbPaymentMode.Size = new System.Drawing.Size(130, 27);
            this.cmbPaymentMode.TabIndex = 10;
            this.cmbPaymentMode.SelectedValueChanged += new System.EventHandler(this.cmbPaymentMode_SelectedValueChanged);
            // 
            // lblPaymentMode
            // 
            this.lblPaymentMode.AutoSize = true;
            this.lblPaymentMode.ForeColor = System.Drawing.Color.Black;
            this.lblPaymentMode.Location = new System.Drawing.Point(145, 270);
            this.lblPaymentMode.Name = "lblPaymentMode";
            this.lblPaymentMode.Size = new System.Drawing.Size(61, 19);
            this.lblPaymentMode.TabIndex = 9;
            this.lblPaymentMode.Text = "Mode : ";
            this.lblPaymentMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAnnadhanamDate
            // 
            this.lblAnnadhanamDate.AutoSize = true;
            this.lblAnnadhanamDate.ForeColor = System.Drawing.Color.Black;
            this.lblAnnadhanamDate.Location = new System.Drawing.Point(66, 230);
            this.lblAnnadhanamDate.Name = "lblAnnadhanamDate";
            this.lblAnnadhanamDate.Size = new System.Drawing.Size(136, 19);
            this.lblAnnadhanamDate.TabIndex = 8;
            this.lblAnnadhanamDate.Text = "Anadhanam Date :";
            // 
            // dtAnadhanamDate
            // 
            this.dtAnadhanamDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtAnadhanamDate.Location = new System.Drawing.Point(208, 226);
            this.dtAnadhanamDate.Name = "dtAnadhanamDate";
            this.dtAnadhanamDate.Size = new System.Drawing.Size(130, 27);
            this.dtAnadhanamDate.TabIndex = 7;
            this.dtAnadhanamDate.UseWaitCursor = true;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.ForeColor = System.Drawing.Color.Black;
            this.lblState.Location = new System.Drawing.Point(148, 152);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(57, 19);
            this.lblState.TabIndex = 6;
            this.lblState.Text = "State : ";
            this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(207, 79);
            this.txtAddress.MaxLength = 200;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(393, 59);
            this.txtAddress.TabIndex = 3;
            this.txtAddress.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(129, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Address : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Location = new System.Drawing.Point(208, 39);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(392, 27);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(143, 43);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(61, 19);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name : ";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnUpdate);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox2.Location = new System.Drawing.Point(20, 555);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(691, 60);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(353, 21);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(62, 32);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(265, 20);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 32);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Blue;
            this.lblHeader.Location = new System.Drawing.Point(147, 7);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(439, 42);
            this.lblHeader.TabIndex = 5;
            this.lblHeader.Text = "ANNA BABA CHARITIES";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(418, 20);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(49, 19);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "Date :";
            // 
            // lblDateValue
            // 
            this.lblDateValue.AutoSize = true;
            this.lblDateValue.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateValue.Location = new System.Drawing.Point(466, 21);
            this.lblDateValue.Name = "lblDateValue";
            this.lblDateValue.Size = new System.Drawing.Size(0, 18);
            this.lblDateValue.TabIndex = 4;
            // 
            // grbReceiptDetails
            // 
            this.grbReceiptDetails.Controls.Add(this.btnEdit);
            this.grbReceiptDetails.Controls.Add(this.lblDateValue);
            this.grbReceiptDetails.Controls.Add(this.lblDate);
            this.grbReceiptDetails.Controls.Add(this.txtReceiptNumber);
            this.grbReceiptDetails.Controls.Add(this.lblReceiptNumber);
            this.grbReceiptDetails.Location = new System.Drawing.Point(22, 49);
            this.grbReceiptDetails.Name = "grbReceiptDetails";
            this.grbReceiptDetails.Size = new System.Drawing.Size(691, 56);
            this.grbReceiptDetails.TabIndex = 4;
            this.grbReceiptDetails.TabStop = false;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(355, 18);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(25, 25);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "-";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // txtReceiptNumber
            // 
            this.txtReceiptNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReceiptNumber.Location = new System.Drawing.Point(205, 18);
            this.txtReceiptNumber.MaxLength = 10;
            this.txtReceiptNumber.Multiline = true;
            this.txtReceiptNumber.Name = "txtReceiptNumber";
            this.txtReceiptNumber.Size = new System.Drawing.Size(140, 25);
            this.txtReceiptNumber.TabIndex = 1;
            this.txtReceiptNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReceiptNumber_KeyPress);
            // 
            // lblReceiptNumber
            // 
            this.lblReceiptNumber.AutoSize = true;
            this.lblReceiptNumber.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceiptNumber.Location = new System.Drawing.Point(42, 20);
            this.lblReceiptNumber.Name = "lblReceiptNumber";
            this.lblReceiptNumber.Size = new System.Drawing.Size(159, 19);
            this.lblReceiptNumber.TabIndex = 0;
            this.lblReceiptNumber.Text = "Receipt / Mobile No .:";
            // 
            // Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Turquoise;
            this.ClientSize = new System.Drawing.Size(731, 633);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.grbReceiptDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Edit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit - [Edit Form]";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.grbReceiptDetails.ResumeLayout(false);
            this.grbReceiptDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblDrawnOn;
        private System.Windows.Forms.TextBox txtDrawnOn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtChequeDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtChequeNumber;
        private System.Windows.Forms.ComboBox cmbPaymentMode;
        private System.Windows.Forms.Label lblPaymentMode;
        private System.Windows.Forms.Label lblAnnadhanamDate;
        private System.Windows.Forms.DateTimePicker dtAnadhanamDate;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.RichTextBox txtAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblDateValue;
        private System.Windows.Forms.GroupBox grbReceiptDetails;
        private System.Windows.Forms.TextBox txtReceiptNumber;
        private System.Windows.Forms.Label lblReceiptNumber;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ComboBox cmbCity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.TextBox txtMobileNumber;
        private System.Windows.Forms.Label lblMobile;

    }
}