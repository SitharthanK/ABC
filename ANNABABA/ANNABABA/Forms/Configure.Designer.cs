namespace ANNABABA.Forms
{
    partial class Configure
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtEffectiveDate = new System.Windows.Forms.DateTimePicker();
            this.lblAnnadhanamDate = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDrawnOn = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nmcMonths = new System.Windows.Forms.NumericUpDown();
            this.nmcReceipts = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvConfigurationDetails = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmcMonths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmcReceipts)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfigurationDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dtEffectiveDate
            // 
            this.dtEffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEffectiveDate.Location = new System.Drawing.Point(164, 34);
            this.dtEffectiveDate.Margin = new System.Windows.Forms.Padding(0);
            this.dtEffectiveDate.MinDate = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            this.dtEffectiveDate.Name = "dtEffectiveDate";
            this.dtEffectiveDate.Size = new System.Drawing.Size(111, 27);
            this.dtEffectiveDate.TabIndex = 7;
            this.dtEffectiveDate.Value = new System.DateTime(2020, 7, 26, 12, 24, 32, 0);
            // 
            // lblAnnadhanamDate
            // 
            this.lblAnnadhanamDate.AutoSize = true;
            this.lblAnnadhanamDate.ForeColor = System.Drawing.Color.Black;
            this.lblAnnadhanamDate.Location = new System.Drawing.Point(45, 38);
            this.lblAnnadhanamDate.Name = "lblAnnadhanamDate";
            this.lblAnnadhanamDate.Size = new System.Drawing.Size(111, 19);
            this.lblAnnadhanamDate.TabIndex = 8;
            this.lblAnnadhanamDate.Text = "Effective Date :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(7, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "Total No.of receipts : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDrawnOn
            // 
            this.lblDrawnOn.AutoSize = true;
            this.lblDrawnOn.ForeColor = System.Drawing.Color.Black;
            this.lblDrawnOn.Location = new System.Drawing.Point(46, 102);
            this.lblDrawnOn.Name = "lblDrawnOn";
            this.lblDrawnOn.Size = new System.Drawing.Size(115, 19);
            this.lblDrawnOn.TabIndex = 17;
            this.lblDrawnOn.Text = "No.of months : ";
            this.lblDrawnOn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(164, 168);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 32);
            this.button2.TabIndex = 20;
            this.button2.Text = "Configure";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnConfigure_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nmcMonths);
            this.groupBox1.Controls.Add(this.nmcReceipts);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.lblDrawnOn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblAnnadhanamDate);
            this.groupBox1.Controls.Add(this.dtEffectiveDate);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(691, 212);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Provide Details: ";
            // 
            // nmcMonths
            // 
            this.nmcMonths.Location = new System.Drawing.Point(164, 100);
            this.nmcMonths.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nmcMonths.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmcMonths.Name = "nmcMonths";
            this.nmcMonths.Size = new System.Drawing.Size(111, 27);
            this.nmcMonths.TabIndex = 24;
            this.nmcMonths.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmcMonths.ThousandsSeparator = true;
            this.nmcMonths.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // nmcReceipts
            // 
            this.nmcReceipts.Location = new System.Drawing.Point(164, 69);
            this.nmcReceipts.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nmcReceipts.Name = "nmcReceipts";
            this.nmcReceipts.Size = new System.Drawing.Size(111, 27);
            this.nmcReceipts.TabIndex = 23;
            this.nmcReceipts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmcReceipts.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(75, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 19);
            this.label1.TabIndex = 22;
            this.label1.Text = "Password : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtPassword.Location = new System.Drawing.Point(164, 133);
            this.txtPassword.MaxLength = 15;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(111, 24);
            this.txtPassword.TabIndex = 21;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvConfigurationDetails);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.Maroon;
            this.groupBox2.Location = new System.Drawing.Point(18, 236);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(691, 267);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configuration Details : ";
            // 
            // dgvConfigurationDetails
            // 
            this.dgvConfigurationDetails.AllowUserToAddRows = false;
            this.dgvConfigurationDetails.AllowUserToDeleteRows = false;
            this.dgvConfigurationDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvConfigurationDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvConfigurationDetails.ColumnHeadersHeight = 28;
            this.dgvConfigurationDetails.Enabled = false;
            this.dgvConfigurationDetails.Location = new System.Drawing.Point(18, 27);
            this.dgvConfigurationDetails.Name = "dgvConfigurationDetails";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvConfigurationDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvConfigurationDetails.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvConfigurationDetails.RowTemplate.ReadOnly = true;
            this.dgvConfigurationDetails.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvConfigurationDetails.Size = new System.Drawing.Size(656, 221);
            this.dgvConfigurationDetails.TabIndex = 0;
            // 
            // Configure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Turquoise;
            this.ClientSize = new System.Drawing.Size(726, 526);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "Configure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmcMonths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmcReceipts)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfigurationDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtEffectiveDate;
        private System.Windows.Forms.Label lblAnnadhanamDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDrawnOn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nmcMonths;
        private System.Windows.Forms.NumericUpDown nmcReceipts;
        private System.Windows.Forms.DataGridView dgvConfigurationDetails;
    }
}