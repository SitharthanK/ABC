namespace DressDetails.Forms
{
    partial class ProfileForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbIsAdmin = new System.Windows.Forms.ComboBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtMobileNumber = new System.Windows.Forms.TextBox();
            this.lblMobile = new System.Windows.Forms.Label();
            this.lblCountry = new System.Windows.Forms.Label();
            this.cmbIsActive = new System.Windows.Forms.ComboBox();
            this.lblActive = new System.Windows.Forms.Label();
            this.lblIsAdmin = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.grbReceiptDetails = new System.Windows.Forms.GroupBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.btnEditDetails = new System.Windows.Forms.Button();
            this.lblDateValue = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblReceiptNumber = new System.Windows.Forms.Label();
            this.dgLogindetails = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblLoginName = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grbReceiptDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLogindetails)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCreate);
            this.groupBox2.Controls.Add(this.btnUpdate);
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox2.Location = new System.Drawing.Point(24, 405);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(691, 61);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(219, 21);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 32);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(307, 21);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 32);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(393, 21);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 32);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbIsAdmin);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.txtMobileNumber);
            this.groupBox1.Controls.Add(this.lblMobile);
            this.groupBox1.Controls.Add(this.lblCountry);
            this.groupBox1.Controls.Add(this.cmbIsActive);
            this.groupBox1.Controls.Add(this.lblActive);
            this.groupBox1.Controls.Add(this.lblIsAdmin);
            this.groupBox1.Controls.Add(this.lblUsername);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox1.Location = new System.Drawing.Point(24, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(691, 269);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Profile Information : ";
            // 
            // cmbIsAdmin
            // 
            this.cmbIsAdmin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIsAdmin.FormattingEnabled = true;
            this.cmbIsAdmin.Location = new System.Drawing.Point(417, 148);
            this.cmbIsAdmin.MaxDropDownItems = 2;
            this.cmbIsAdmin.Name = "cmbIsAdmin";
            this.cmbIsAdmin.Size = new System.Drawing.Size(160, 27);
            this.cmbIsAdmin.TabIndex = 30;
            // 
            // txtPassword
            // 
            this.txtPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPassword.Location = new System.Drawing.Point(183, 187);
            this.txtPassword.MaxLength = 100;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(161, 27);
            this.txtPassword.TabIndex = 29;
            // 
            // txtUsername
            // 
            this.txtUsername.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsername.Location = new System.Drawing.Point(183, 151);
            this.txtUsername.MaxLength = 100;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(161, 27);
            this.txtUsername.TabIndex = 28;
            // 
            // txtMobileNumber
            // 
            this.txtMobileNumber.Location = new System.Drawing.Point(183, 221);
            this.txtMobileNumber.MaxLength = 10;
            this.txtMobileNumber.Name = "txtMobileNumber";
            this.txtMobileNumber.Size = new System.Drawing.Size(161, 27);
            this.txtMobileNumber.TabIndex = 27;
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.ForeColor = System.Drawing.Color.Black;
            this.lblMobile.Location = new System.Drawing.Point(115, 227);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(69, 19);
            this.lblMobile.TabIndex = 26;
            this.lblMobile.Text = "Mobile : ";
            this.lblMobile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.ForeColor = System.Drawing.Color.Black;
            this.lblCountry.Location = new System.Drawing.Point(97, 193);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(86, 19);
            this.lblCountry.TabIndex = 25;
            this.lblCountry.Text = "Password : ";
            this.lblCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbIsActive
            // 
            this.cmbIsActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIsActive.FormattingEnabled = true;
            this.cmbIsActive.Location = new System.Drawing.Point(418, 187);
            this.cmbIsActive.MaxDropDownItems = 2;
            this.cmbIsActive.Name = "cmbIsActive";
            this.cmbIsActive.Size = new System.Drawing.Size(159, 27);
            this.cmbIsActive.TabIndex = 10;
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.ForeColor = System.Drawing.Color.Black;
            this.lblActive.Location = new System.Drawing.Point(355, 192);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(63, 19);
            this.lblActive.TabIndex = 9;
            this.lblActive.Text = "Active : ";
            this.lblActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIsAdmin
            // 
            this.lblIsAdmin.AutoSize = true;
            this.lblIsAdmin.ForeColor = System.Drawing.Color.Black;
            this.lblIsAdmin.Location = new System.Drawing.Point(352, 151);
            this.lblIsAdmin.Name = "lblIsAdmin";
            this.lblIsAdmin.Size = new System.Drawing.Size(62, 19);
            this.lblIsAdmin.TabIndex = 8;
            this.lblIsAdmin.Text = "Admin :";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.ForeColor = System.Drawing.Color.Black;
            this.lblUsername.Location = new System.Drawing.Point(87, 153);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(95, 19);
            this.lblUsername.TabIndex = 6;
            this.lblUsername.Text = "User Name : ";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(183, 79);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(0);
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
            this.label1.Location = new System.Drawing.Point(106, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Address : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Location = new System.Drawing.Point(183, 39);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(393, 27);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(120, 43);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(61, 19);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name : ";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Blue;
            this.lblHeader.Location = new System.Drawing.Point(161, 10);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(365, 35);
            this.lblHeader.TabIndex = 5;
            this.lblHeader.Text = "ANNA BABA CHARITIES";
            // 
            // grbReceiptDetails
            // 
            this.grbReceiptDetails.Controls.Add(this.txtID);
            this.grbReceiptDetails.Controls.Add(this.btnEditDetails);
            this.grbReceiptDetails.Controls.Add(this.lblDateValue);
            this.grbReceiptDetails.Controls.Add(this.lblDate);
            this.grbReceiptDetails.Controls.Add(this.lblReceiptNumber);
            this.grbReceiptDetails.Location = new System.Drawing.Point(24, 78);
            this.grbReceiptDetails.Name = "grbReceiptDetails";
            this.grbReceiptDetails.Size = new System.Drawing.Size(691, 56);
            this.grbReceiptDetails.TabIndex = 4;
            this.grbReceiptDetails.TabStop = false;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(181, 17);
            this.txtID.MaxLength = 10;
            this.txtID.Multiline = true;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(140, 24);
            this.txtID.TabIndex = 6;
            // 
            // btnEditDetails
            // 
            this.btnEditDetails.Location = new System.Drawing.Point(325, 17);
            this.btnEditDetails.Name = "btnEditDetails";
            this.btnEditDetails.Size = new System.Drawing.Size(25, 25);
            this.btnEditDetails.TabIndex = 5;
            this.btnEditDetails.Text = "-";
            this.btnEditDetails.UseVisualStyleBackColor = true;
            this.btnEditDetails.Click += new System.EventHandler(this.btnEditDetails_Click);
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
            // lblReceiptNumber
            // 
            this.lblReceiptNumber.AutoSize = true;
            this.lblReceiptNumber.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceiptNumber.Location = new System.Drawing.Point(143, 19);
            this.lblReceiptNumber.Name = "lblReceiptNumber";
            this.lblReceiptNumber.Size = new System.Drawing.Size(31, 19);
            this.lblReceiptNumber.TabIndex = 0;
            this.lblReceiptNumber.Text = "ID :";
            // 
            // dgLogindetails
            // 
            this.dgLogindetails.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgLogindetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLogindetails.Location = new System.Drawing.Point(9, 21);
            this.dgLogindetails.Name = "dgLogindetails";
            this.dgLogindetails.Size = new System.Drawing.Size(671, 247);
            this.dgLogindetails.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgLogindetails);
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox3.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox3.Location = new System.Drawing.Point(24, 473);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(694, 274);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Login Details : ";
            // 
            // lblLoginName
            // 
            this.lblLoginName.AutoSize = true;
            this.lblLoginName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginName.ForeColor = System.Drawing.Color.Maroon;
            this.lblLoginName.Location = new System.Drawing.Point(515, 58);
            this.lblLoginName.MinimumSize = new System.Drawing.Size(200, 0);
            this.lblLoginName.Name = "lblLoginName";
            this.lblLoginName.Size = new System.Drawing.Size(200, 17);
            this.lblLoginName.TabIndex = 9;
            this.lblLoginName.Text = "Welcome: ";
            // 
            // ProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Turquoise;
            this.ClientSize = new System.Drawing.Size(730, 769);
            this.Controls.Add(this.lblLoginName);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.grbReceiptDetails);
            this.Name = "ProfileForm";
            this.Text = "ProfileForm";
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbReceiptDetails.ResumeLayout(false);
            this.grbReceiptDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLogindetails)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMobileNumber;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.Label lblActive;
        private System.Windows.Forms.Label lblIsAdmin;
        private System.Windows.Forms.RichTextBox txtAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox grbReceiptDetails;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Button btnEditDetails;
        private System.Windows.Forms.Label lblDateValue;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblReceiptNumber;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ComboBox cmbIsAdmin;
        private System.Windows.Forms.ComboBox cmbIsActive;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.DataGridView dgLogindetails;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblLoginName;
    }
}