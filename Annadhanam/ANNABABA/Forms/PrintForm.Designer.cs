namespace ANNABABA.Forms
{
    partial class PrintForm
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
            this.ReceiptTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ABCReceiptDataset = new ANNABABA.ABCReceiptDataset();
            this.ReceiptReportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.ReceiptTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ABCReceiptDataset)).BeginInit();
            this.SuspendLayout();
            // 
            // ReceiptTableBindingSource
            // 
            this.ReceiptTableBindingSource.DataMember = "ReceiptTable";
            this.ReceiptTableBindingSource.DataSource = this.ABCReceiptDataset;
            // 
            // ABCReceiptDataset
            // 
            this.ABCReceiptDataset.DataSetName = "ABCReceiptDataset";
            this.ABCReceiptDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReceiptReportViewer
            // 
            this.ReceiptReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ABCReceiptDataset";
            reportDataSource1.Value = this.ReceiptTableBindingSource;
            this.ReceiptReportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.ReceiptReportViewer.LocalReport.ReportEmbeddedResource = "ANNABABA.RDLC.ABCReceiptReport.rdlc";
            this.ReceiptReportViewer.Location = new System.Drawing.Point(0, 0);
            this.ReceiptReportViewer.Name = "ReceiptReportViewer";
            this.ReceiptReportViewer.Size = new System.Drawing.Size(691, 516);
            this.ReceiptReportViewer.TabIndex = 0;
            // 
            // PrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Turquoise;
            this.ClientSize = new System.Drawing.Size(691, 516);
            this.Controls.Add(this.ReceiptReportViewer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PrintForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AnnaBabaCharities - [Print Form]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReceiptReport_FormClosing);
            this.Load += new System.EventHandler(this.ReceiptReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReceiptTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ABCReceiptDataset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer ReceiptReportViewer;
        private System.Windows.Forms.BindingSource ReceiptTableBindingSource;
        private ABCReceiptDataset ABCReceiptDataset;
    }
}