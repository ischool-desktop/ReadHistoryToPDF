namespace ReadHistoryGenerator
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRun = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtChart = new System.Windows.Forms.TextBox();
            this.txtPdf = new System.Windows.Forms.TextBox();
            this.txtXmlFile = new System.Windows.Forms.TextBox();
            this.btnDraw = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.txtPattern = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRun.Location = new System.Drawing.Point(243, 154);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(107, 23);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "合併產生 PDF";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "資料";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "PDF 輸出";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "圖表輸出";
            // 
            // txtChart
            // 
            this.txtChart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChart.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ReadHistoryGenerator.Properties.Settings.Default, "TextCharts", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtChart.Location = new System.Drawing.Point(71, 39);
            this.txtChart.Name = "txtChart";
            this.txtChart.Size = new System.Drawing.Size(279, 22);
            this.txtChart.TabIndex = 3;
            this.txtChart.Text = global::ReadHistoryGenerator.Properties.Settings.Default.TextCharts;
            // 
            // txtPdf
            // 
            this.txtPdf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPdf.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ReadHistoryGenerator.Properties.Settings.Default, "TextPdf", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPdf.Location = new System.Drawing.Point(71, 68);
            this.txtPdf.Name = "txtPdf";
            this.txtPdf.Size = new System.Drawing.Size(279, 22);
            this.txtPdf.TabIndex = 3;
            this.txtPdf.Text = global::ReadHistoryGenerator.Properties.Settings.Default.TextPdf;
            // 
            // txtXmlFile
            // 
            this.txtXmlFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtXmlFile.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ReadHistoryGenerator.Properties.Settings.Default, "XmlFile", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtXmlFile.Location = new System.Drawing.Point(71, 10);
            this.txtXmlFile.Name = "txtXmlFile";
            this.txtXmlFile.Size = new System.Drawing.Size(279, 22);
            this.txtXmlFile.TabIndex = 2;
            this.txtXmlFile.Text = global::ReadHistoryGenerator.Properties.Settings.Default.XmlFile;
            // 
            // btnDraw
            // 
            this.btnDraw.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDraw.Location = new System.Drawing.Point(130, 154);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(107, 23);
            this.btnDraw.TabIndex = 4;
            this.btnDraw.Text = "產生圖表";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(19, 159);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(46, 12);
            this.lblProgress.TabIndex = 6;
            this.lblProgress.Text = "(進度...)";
            // 
            // txtPattern
            // 
            this.txtPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPattern.Location = new System.Drawing.Point(71, 95);
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(279, 22);
            this.txtPattern.TabIndex = 3;
            this.txtPattern.Text = "2015101{0}01";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "Pattern";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 189);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.txtPattern);
            this.Controls.Add(this.txtChart);
            this.Controls.Add(this.txtPdf);
            this.Controls.Add(this.txtXmlFile);
            this.Controls.Add(this.btnRun);
            this.Name = "MainForm";
            this.Text = "讀書紀錄產生器";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtXmlFile;
        private System.Windows.Forms.TextBox txtPdf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtChart;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.TextBox txtPattern;
        private System.Windows.Forms.Label label4;
    }
}

