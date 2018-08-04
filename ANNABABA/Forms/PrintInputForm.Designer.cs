namespace ANNABABA.Forms
{
    partial class PrintInputForm
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.grbReceiptDetails = new System.Windows.Forms.GroupBox();
            this.btnGetDetails = new System.Windows.Forms.Button();
            this.lblDateValue = new System.Windows.Forms.Label();
            this.txtReceiptNumber = new System.Windows.Forms.TextBox();
            this.lblReceiptNumber = new System.Windows.Forms.Label();
            this.ReceiptReportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ABCReceiptDataset = new ANNABABA.ABCReceiptDataset();
            this.ReceiptTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grbReceiptDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ABCReceiptDataset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReceiptTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grbReceiptDetails
            // 
            this.grbReceiptDetails.Controls.Add(this.btnGetDetails);
            this.grbReceiptDetails.Controls.Add(this.lblDateValue);
            this.grbReceiptDetails.Controls.Add(this.txtReceiptNumber);
            this.grbReceiptDetails.Controls.Add(this.lblReceiptNumber);
            this.grbReceiptDetails.Location = new System.Drawing.Point(9, 6);
            this.grbReceiptDetails.Name = "grbReceiptDetails";
            this.grbReceiptDetails.Size = new System.Drawing.Size(696, 53);
            this.grbReceiptDetails.TabIndex = 5;
            this.grbReceiptDetails.TabStop = false;
            // 
            // btnGetDetails
            // 
            this.btnGetDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetDetails.ForeColor = System.Drawing.Color.Maroon;
            this.btnGetDetails.Location = new System.Drawing.Point(419, 19);
            this.btnGetDetails.Name = "btnGetDetails";
            this.btnGetDetails.Size = new System.Drawing.Size(94, 25);
            this.btnGetDetails.TabIndex = 2;
            this.btnGetDetails.Text = "View Recipt";
            this.btnGetDetails.UseVisualStyleBackColor = true;
            this.btnGetDetails.Click += new System.EventHandler(this.btnGetDetails_Click);
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
            // txtReceiptNumber
            // 
            this.txtReceiptNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReceiptNumber.Location = new System.Drawing.Point(269, 19);
            this.txtReceiptNumber.MaxLength = 10;
            this.txtReceiptNumber.Multiline = true;
            this.txtReceiptNumber.Name = "txtReceiptNumber";
            this.txtReceiptNumber.Size = new System.Drawing.Size(140, 25);
            this.txtReceiptNumber.TabIndex = 1;
            // 
            // lblReceiptNumber
            // 
            this.lblReceiptNumber.AutoSize = true;
            this.lblReceiptNumber.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceiptNumber.Location = new System.Drawing.Point(106, 21);
            this.lblReceiptNumber.Name = "lblReceiptNumber";
            this.lblReceiptNumber.Size = new System.Drawing.Size(159, 19);
            this.lblReceiptNumber.TabIndex = 0;
            this.lblReceiptNumber.Text = "Receipt / Mobile No .:";
            // 
            // ReceiptReportViewer
            // 
            reportDataSource1.Name = "ABCReceiptDataset";
            reportDataSource1.Value = this.ReceiptTableBindingSource;
            this.ReceiptReportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.ReceiptReportViewer.LocalReport.ReportEmbeddedResource = "ANNABABA.RDLC.ABCReceiptReport.rdlc";
            this.ReceiptReportViewer.Location = new System.Drawing.Point(9, 65);
            this.ReceiptReportViewer.Name = "ReceiptReportViewer";
            this.ReceiptReportViewer.Size = new System.Drawing.Size(697, 522);
            this.ReceiptReportViewer.TabIndex = 2;
            // 
            // ABCReceiptDataset
            // 
            this.ABCReceiptDataset.DataSetName = "ABCReceiptDataset";
            this.ABCReceiptDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReceiptTableBindingSource
            // 
            this.ReceiptTableBindingSource.DataMember = "ReceiptTable";
            this.ReceiptTableBindingSource.DataSource = this.ABCReceiptDataset;
            // 
            // PrintInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Turquoise;
            this.ClientSize = new System.Drawing.Size(717, 602);
            this.Controls.Add(this.ReceiptReportViewer);
            this.Controls.Add(this.grbReceiptDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PrintInputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AnnaBabaCharities - [ Receipt Input Form]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PrintInputForm_FormClosing);
            this.Load += new System.EventHandler(this.PrintInputForm_Load);
            this.grbReceiptDetails.ResumeLayout(false);
            this.grbReceiptDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ABCReceiptDataset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReceiptTableBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbReceiptDetails;
        private System.Windows.Forms.Button btnGetDetails;
        private System.Windows.Forms.Label lblDateValue;
        private System.Windows.Forms.TextBox txtReceiptNumber;
        private System.Windows.Forms.Label lblReceiptNumber;
        private Microsoft.Reporting.WinForms.ReportViewer ReceiptReportViewer;
        private System.Windows.Forms.BindingSource ReceiptTableBindingSource;
        private ABCReceiptDataset ABCReceiptDataset;
    }
}