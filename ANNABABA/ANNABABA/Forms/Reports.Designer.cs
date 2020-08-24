namespace ANNABABA
{
    partial class Reports
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
            this.tblAnnadhanamDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ABCAnnadhanamReportsDataset = new ANNABABA.ABCAnnadhanamReportsDataset();
            this.grbParams = new System.Windows.Forms.GroupBox();
            this.txtreceiptnumber = new System.Windows.Forms.TextBox();
            this.lblReceiptnumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTypes = new System.Windows.Forms.ComboBox();
            this.dtPeriodFrom = new System.Windows.Forms.DateTimePicker();
            this.lblRPTTODate = new System.Windows.Forms.Label();
            this.dtPeriodTo = new System.Windows.Forms.DateTimePicker();
            this.lblRPTFromDate = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tblAnnadhanamDetailsTableAdapter = new ANNABABA.ABCAnnadhanamReportsDatasetTableAdapters.tblAnnadhanamDetailsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.tblAnnadhanamDetailsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ABCAnnadhanamReportsDataset)).BeginInit();
            this.grbParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblAnnadhanamDetailsBindingSource
            // 
            this.tblAnnadhanamDetailsBindingSource.DataMember = "tblAnnadhanamDetails";
            this.tblAnnadhanamDetailsBindingSource.DataSource = this.ABCAnnadhanamReportsDataset;
            // 
            // ABCAnnadhanamReportsDataset
            // 
            this.ABCAnnadhanamReportsDataset.DataSetName = "ABCAnnadhanamReportsDataset";
            this.ABCAnnadhanamReportsDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grbParams
            // 
            this.grbParams.BackColor = System.Drawing.Color.Turquoise;
            this.grbParams.Controls.Add(this.txtreceiptnumber);
            this.grbParams.Controls.Add(this.lblReceiptnumber);
            this.grbParams.Controls.Add(this.label1);
            this.grbParams.Controls.Add(this.cmbTypes);
            this.grbParams.Controls.Add(this.dtPeriodFrom);
            this.grbParams.Controls.Add(this.lblRPTTODate);
            this.grbParams.Controls.Add(this.dtPeriodTo);
            this.grbParams.Controls.Add(this.lblRPTFromDate);
            this.grbParams.Location = new System.Drawing.Point(6, 8);
            this.grbParams.Name = "grbParams";
            this.grbParams.Size = new System.Drawing.Size(710, 65);
            this.grbParams.TabIndex = 8;
            this.grbParams.TabStop = false;
            this.grbParams.Text = "Search Parameters";
            // 
            // txtreceiptnumber
            // 
            this.txtreceiptnumber.Location = new System.Drawing.Point(385, 26);
            this.txtreceiptnumber.MaxLength = 10;
            this.txtreceiptnumber.Name = "txtreceiptnumber";
            this.txtreceiptnumber.Size = new System.Drawing.Size(120, 20);
            this.txtreceiptnumber.TabIndex = 8;
            this.txtreceiptnumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtreceiptnumber_KeyPress);
            this.txtreceiptnumber.Leave += new System.EventHandler(this.txtreceiptnumber_Leave);
            // 
            // lblReceiptnumber
            // 
            this.lblReceiptnumber.AutoSize = true;
            this.lblReceiptnumber.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceiptnumber.Location = new System.Drawing.Point(275, 29);
            this.lblReceiptnumber.Name = "lblReceiptnumber";
            this.lblReceiptnumber.Size = new System.Drawing.Size(104, 15);
            this.lblReceiptnumber.TabIndex = 7;
            this.lblReceiptnumber.Text = "Receipt Number :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(59, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Types :";
            // 
            // cmbTypes
            // 
            this.cmbTypes.DropDownHeight = 120;
            this.cmbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypes.FormattingEnabled = true;
            this.cmbTypes.IntegralHeight = false;
            this.cmbTypes.ItemHeight = 13;
            this.cmbTypes.Location = new System.Drawing.Point(116, 26);
            this.cmbTypes.Name = "cmbTypes";
            this.cmbTypes.Size = new System.Drawing.Size(118, 21);
            this.cmbTypes.TabIndex = 5;
            this.cmbTypes.SelectedValueChanged += new System.EventHandler(this.cmbTypes_SelectedValueChanged);
            // 
            // dtPeriodFrom
            // 
            this.dtPeriodFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPeriodFrom.Location = new System.Drawing.Point(363, 27);
            this.dtPeriodFrom.Name = "dtPeriodFrom";
            this.dtPeriodFrom.Size = new System.Drawing.Size(91, 20);
            this.dtPeriodFrom.TabIndex = 1;
            this.dtPeriodFrom.ValueChanged += new System.EventHandler(this.dtPeriodFrom_ValueChanged);
            // 
            // lblRPTTODate
            // 
            this.lblRPTTODate.AutoSize = true;
            this.lblRPTTODate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRPTTODate.Location = new System.Drawing.Point(498, 29);
            this.lblRPTTODate.Name = "lblRPTTODate";
            this.lblRPTTODate.Size = new System.Drawing.Size(65, 15);
            this.lblRPTTODate.TabIndex = 4;
            this.lblRPTTODate.Text = "Period To :";
            // 
            // dtPeriodTo
            // 
            this.dtPeriodTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPeriodTo.Location = new System.Drawing.Point(565, 28);
            this.dtPeriodTo.Name = "dtPeriodTo";
            this.dtPeriodTo.Size = new System.Drawing.Size(87, 20);
            this.dtPeriodTo.TabIndex = 2;
            this.dtPeriodTo.ValueChanged += new System.EventHandler(this.dtPeriodTo_ValueChanged);
            // 
            // lblRPTFromDate
            // 
            this.lblRPTFromDate.AutoSize = true;
            this.lblRPTFromDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRPTFromDate.Location = new System.Drawing.Point(277, 30);
            this.lblRPTFromDate.Name = "lblRPTFromDate";
            this.lblRPTFromDate.Size = new System.Drawing.Size(82, 15);
            this.lblRPTFromDate.TabIndex = 3;
            this.lblRPTFromDate.Text = "Period From :";
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "ABCReportsDataset";
            reportDataSource1.Value = this.tblAnnadhanamDetailsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANNABABA.RDLC.ABCReportData.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(6, 92);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(710, 484);
            this.reportViewer1.TabIndex = 9;
            // 
            // tblAnnadhanamDetailsTableAdapter
            // 
            this.tblAnnadhanamDetailsTableAdapter.ClearBeforeFill = true;
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Turquoise;
            this.ClientSize = new System.Drawing.Size(732, 588);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.grbParams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Reports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Anadhanam - [Reports Form]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ABCAnnadhanamReports_FormClosing);
            this.Load += new System.EventHandler(this.ABCAnnadhanamReorts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tblAnnadhanamDetailsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ABCAnnadhanamReportsDataset)).EndInit();
            this.grbParams.ResumeLayout(false);
            this.grbParams.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbParams;
        private System.Windows.Forms.DateTimePicker dtPeriodFrom;
        private System.Windows.Forms.Label lblRPTTODate;
        private System.Windows.Forms.DateTimePicker dtPeriodTo;
        private System.Windows.Forms.Label lblRPTFromDate;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource tblAnnadhanamDetailsBindingSource;
        private ABCAnnadhanamReportsDataset ABCAnnadhanamReportsDataset;
        private ABCAnnadhanamReportsDatasetTableAdapters.tblAnnadhanamDetailsTableAdapter tblAnnadhanamDetailsTableAdapter;
        private System.Windows.Forms.ComboBox cmbTypes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblReceiptnumber;
        private System.Windows.Forms.TextBox txtreceiptnumber;
        //private ABCDataReportTableAdapters.tblAnnadhanamDetailsTableAdapter tblAnnadhanamDetailsTableAdapter;
        //private ANNABABA.ABCDataReport ABCDataReport;


    }
}