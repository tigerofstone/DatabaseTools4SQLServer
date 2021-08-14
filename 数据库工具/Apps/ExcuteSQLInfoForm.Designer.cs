namespace Upgrade.Apps
{
    partial class ExcuteSQLInfoForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnExcute = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbExcuteSQL = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbInfo = new System.Windows.Forms.TextBox();
            this.dgData = new System.Windows.Forms.DataGrid();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExcute
            // 
            this.btnExcute.Location = new System.Drawing.Point(754, 14);
            this.btnExcute.Name = "btnExcute";
            this.btnExcute.Size = new System.Drawing.Size(71, 27);
            this.btnExcute.TabIndex = 0;
            this.btnExcute.Text = "执行(&E)";
            this.btnExcute.UseVisualStyleBackColor = true;
            this.btnExcute.Click += new System.EventHandler(this.btnExcute_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "执行 SQL 语句：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbExcuteSQL);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnExcute);
            this.groupBox1.Location = new System.Drawing.Point(5, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(834, 48);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // cbExcuteSQL
            // 
            this.cbExcuteSQL.DropDownWidth = 750;
            this.cbExcuteSQL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbExcuteSQL.FormattingEnabled = true;
            this.cbExcuteSQL.ItemHeight = 12;
            this.cbExcuteSQL.Items.AddRange(new object[] {
            "Select pubufts From flt_definemeta_cache with (nolock) where filterid=\'AP[__]应付核销" +
                "明细表\' and localeid=\'zh-cn\'",
            "Select idec From foreigncurrency where cexch_name=N\'人民币\' or cexch_code=N\'人民币\'",
            "Select top 1000 * From CurrentStock",
            "Select * from Item",
            "Select * from StockReceipt"});
            this.cbExcuteSQL.Location = new System.Drawing.Point(99, 18);
            this.cbExcuteSQL.Name = "cbExcuteSQL";
            this.cbExcuteSQL.Size = new System.Drawing.Size(649, 20);
            this.cbExcuteSQL.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbInfo);
            this.groupBox2.Controls.Add(this.dgData);
            this.groupBox2.Location = new System.Drawing.Point(5, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(868, 503);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // tbInfo
            // 
            this.tbInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbInfo.Location = new System.Drawing.Point(524, 12);
            this.tbInfo.Multiline = true;
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbInfo.Size = new System.Drawing.Size(335, 485);
            this.tbInfo.TabIndex = 1;
            // 
            // dgData
            // 
            this.dgData.AlternatingBackColor = System.Drawing.Color.LightCyan;
            this.dgData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgData.DataMember = "";
            this.dgData.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgData.Location = new System.Drawing.Point(5, 12);
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            this.dgData.Size = new System.Drawing.Size(513, 485);
            this.dgData.TabIndex = 0;
            // 
            // ExcuteSQLInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 556);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ExcuteSQLInfoForm";
            this.Text = "SQL Server 语句执行情况";
            this.Load += new System.EventHandler(this.ExcuteSQLInfoForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExcute;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbInfo;
        private System.Windows.Forms.DataGrid dgData;
        private System.Windows.Forms.ComboBox cbExcuteSQL;
    }
}