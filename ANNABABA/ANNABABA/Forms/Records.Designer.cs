namespace ANNABABA.Forms
{
    partial class Records
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
            this.rbtnDay = new System.Windows.Forms.RadioButton();
            this.btnGetRecords = new System.Windows.Forms.Button();
            this.rbtnWeek = new System.Windows.Forms.RadioButton();
            this.rbtnMonth = new System.Windows.Forms.RadioButton();
            this.rbtnYear = new System.Windows.Forms.RadioButton();
            this.lblRPTTODate = new System.Windows.Forms.Label();
            this.dtPeriod = new System.Windows.Forms.DateTimePicker();
            this.lblRPTFromDate = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tblAnnadhanamDetailsTableAdapter = new ANNABABA.ABCAnnadhanamReportsDatasetTableAdapters.tblAnnadhanamDetailsTableAdapter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgDevoteeDetails = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.tblAnnadhanamDetailsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ABCAnnadhanamReportsDataset)).BeginInit();
            this.grbParams.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDevoteeDetails)).BeginInit();
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
            this.grbParams.Controls.Add(this.rbtnDay);
            this.grbParams.Controls.Add(this.btnGetRecords);
            this.grbParams.Controls.Add(this.rbtnWeek);
            this.grbParams.Controls.Add(this.rbtnMonth);
            this.grbParams.Controls.Add(this.rbtnYear);
            this.grbParams.Controls.Add(this.lblRPTTODate);
            this.grbParams.Controls.Add(this.dtPeriod);
            this.grbParams.Controls.Add(this.lblRPTFromDate);
            this.grbParams.Location = new System.Drawing.Point(22, 12);
            this.grbParams.Name = "grbParams";
            this.grbParams.Size = new System.Drawing.Size(766, 65);
            this.grbParams.TabIndex = 9;
            this.grbParams.TabStop = false;
            this.grbParams.Text = "Search Parameters";
            // 
            // rbtnDay
            // 
            this.rbtnDay.AutoSize = true;
            this.rbtnDay.Location = new System.Drawing.Point(288, 24);
            this.rbtnDay.Name = "rbtnDay";
            this.rbtnDay.Size = new System.Drawing.Size(44, 17);
            this.rbtnDay.TabIndex = 6;
            this.rbtnDay.TabStop = true;
            this.rbtnDay.Text = "Day";
            this.rbtnDay.UseVisualStyleBackColor = true;
            // 
            // btnGetRecords
            // 
            this.btnGetRecords.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetRecords.ForeColor = System.Drawing.Color.Maroon;
            this.btnGetRecords.Location = new System.Drawing.Point(627, 21);
            this.btnGetRecords.Name = "btnGetRecords";
            this.btnGetRecords.Size = new System.Drawing.Size(86, 25);
            this.btnGetRecords.TabIndex = 5;
            this.btnGetRecords.Text = "Get Details";
            this.btnGetRecords.UseVisualStyleBackColor = true;
            this.btnGetRecords.Click += new System.EventHandler(this.btnGetRecords_Click);
            // 
            // rbtnWeek
            // 
            this.rbtnWeek.AutoSize = true;
            this.rbtnWeek.Location = new System.Drawing.Point(226, 24);
            this.rbtnWeek.Name = "rbtnWeek";
            this.rbtnWeek.Size = new System.Drawing.Size(54, 17);
            this.rbtnWeek.TabIndex = 3;
            this.rbtnWeek.TabStop = true;
            this.rbtnWeek.Text = "Week";
            this.rbtnWeek.UseVisualStyleBackColor = true;
            // 
            // rbtnMonth
            // 
            this.rbtnMonth.AutoSize = true;
            this.rbtnMonth.Location = new System.Drawing.Point(162, 24);
            this.rbtnMonth.Name = "rbtnMonth";
            this.rbtnMonth.Size = new System.Drawing.Size(55, 17);
            this.rbtnMonth.TabIndex = 2;
            this.rbtnMonth.TabStop = true;
            this.rbtnMonth.Text = "Month";
            this.rbtnMonth.UseVisualStyleBackColor = true;
            // 
            // rbtnYear
            // 
            this.rbtnYear.AutoSize = true;
            this.rbtnYear.Location = new System.Drawing.Point(98, 24);
            this.rbtnYear.Name = "rbtnYear";
            this.rbtnYear.Size = new System.Drawing.Size(47, 17);
            this.rbtnYear.TabIndex = 1;
            this.rbtnYear.TabStop = true;
            this.rbtnYear.Text = "Year";
            this.rbtnYear.UseVisualStyleBackColor = true;
            // 
            // lblRPTTODate
            // 
            this.lblRPTTODate.AutoSize = true;
            this.lblRPTTODate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRPTTODate.Location = new System.Drawing.Point(376, 25);
            this.lblRPTTODate.Name = "lblRPTTODate";
            this.lblRPTTODate.Size = new System.Drawing.Size(82, 15);
            this.lblRPTTODate.TabIndex = 4;
            this.lblRPTTODate.Text = "Period From :";
            // 
            // dtPeriod
            // 
            this.dtPeriod.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPeriod.Location = new System.Drawing.Point(463, 24);
            this.dtPeriod.Name = "dtPeriod";
            this.dtPeriod.Size = new System.Drawing.Size(87, 20);
            this.dtPeriod.TabIndex = 4;
            // 
            // lblRPTFromDate
            // 
            this.lblRPTFromDate.AutoSize = true;
            this.lblRPTFromDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRPTFromDate.Location = new System.Drawing.Point(24, 24);
            this.lblRPTFromDate.Name = "lblRPTFromDate";
            this.lblRPTFromDate.Size = new System.Drawing.Size(66, 15);
            this.lblRPTFromDate.TabIndex = 3;
            this.lblRPTFromDate.Text = "Search By :";
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
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Turquoise;
            this.groupBox1.Controls.Add(this.dgDevoteeDetails);
            this.groupBox1.Location = new System.Drawing.Point(22, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(766, 364);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Records";
            // 
            // dgDevoteeDetails
            // 
            this.dgDevoteeDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDevoteeDetails.Location = new System.Drawing.Point(7, 20);
            this.dgDevoteeDetails.Name = "dgDevoteeDetails";
            this.dgDevoteeDetails.Size = new System.Drawing.Size(753, 338);
            this.dgDevoteeDetails.TabIndex = 0;
            // 
            // Records
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Turquoise;
            this.ClientSize = new System.Drawing.Size(800, 459);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbParams);
            this.MaximizeBox = false;
            this.Name = "Records";
            this.Text = "Anadhanam - [Details Form]";
            ((System.ComponentModel.ISupportInitialize)(this.tblAnnadhanamDetailsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ABCAnnadhanamReportsDataset)).EndInit();
            this.grbParams.ResumeLayout(false);
            this.grbParams.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDevoteeDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource tblAnnadhanamDetailsBindingSource;
        private ABCAnnadhanamReportsDataset ABCAnnadhanamReportsDataset;
        private System.Windows.Forms.GroupBox grbParams;
        private System.Windows.Forms.Label lblRPTFromDate;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private ABCAnnadhanamReportsDatasetTableAdapters.tblAnnadhanamDetailsTableAdapter tblAnnadhanamDetailsTableAdapter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnYear;
        private System.Windows.Forms.RadioButton rbtnWeek;
        private System.Windows.Forms.RadioButton rbtnMonth;
        private System.Windows.Forms.Button btnGetRecords;
        private System.Windows.Forms.DataGridView dgDevoteeDetails;
        private System.Windows.Forms.Label lblRPTTODate;
        private System.Windows.Forms.DateTimePicker dtPeriod;
        private System.Windows.Forms.RadioButton rbtnDay;
    }
}