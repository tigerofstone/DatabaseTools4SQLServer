namespace Upgrade.Apps
{
    partial class frmImportCustomDataInfo
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportCustomDataInfo));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSelectExcel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbExcelSheet = new System.Windows.Forms.ComboBox();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.upbProgress = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择要导入的Excel：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(895, 202);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSelectExcel);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.tbFileName);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.btnImportExcel);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(875, 92);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // btnSelectExcel
            // 
            this.btnSelectExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectExcel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectExcel.Image = global::Upgrade.Properties.Resources.Excel1;
            this.btnSelectExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectExcel.Location = new System.Drawing.Point(769, 16);
            this.btnSelectExcel.Name = "btnSelectExcel";
            this.btnSelectExcel.Size = new System.Drawing.Size(98, 26);
            this.btnSelectExcel.TabIndex = 10;
            this.btnSelectExcel.Text = "   选择文件";
            this.btnSelectExcel.UseVisualStyleBackColor = true;
            this.btnSelectExcel.Click += new System.EventHandler(this.btnSelectExcel_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbExcelSheet);
            this.panel1.Location = new System.Drawing.Point(169, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(594, 24);
            this.panel1.TabIndex = 9;
            // 
            // cbExcelSheet
            // 
            this.cbExcelSheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbExcelSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExcelSheet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbExcelSheet.FormattingEnabled = true;
            this.cbExcelSheet.Location = new System.Drawing.Point(0, 0);
            this.cbExcelSheet.Name = "cbExcelSheet";
            this.cbExcelSheet.Size = new System.Drawing.Size(592, 20);
            this.cbExcelSheet.TabIndex = 7;
            // 
            // tbFileName
            // 
            this.tbFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFileName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbFileName.Location = new System.Drawing.Point(169, 16);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(594, 21);
            this.tbFileName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(8, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "选择要输出的Excel工作表：";
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportExcel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnImportExcel.Image = global::Upgrade.Properties.Resources.Table_1;
            this.btnImportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportExcel.Location = new System.Drawing.Point(769, 50);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(98, 26);
            this.btnImportExcel.TabIndex = 2;
            this.btnImportExcel.Text = "    导入数据(&I)";
            this.btnImportExcel.UseVisualStyleBackColor = true;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.upbProgress);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(875, 79);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // upbProgress
            // 
            this.upbProgress.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance1.BackColor = System.Drawing.Color.DarkSlateGray;
            appearance1.BackColor2 = System.Drawing.Color.CadetBlue;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance1.FontData.Name = "宋体";
            appearance1.FontData.SizeInPoints = 10F;
            appearance1.ForeColor = System.Drawing.Color.Black;
            appearance1.TextHAlignAsString = "Left";
            appearance1.TextVAlignAsString = "Middle";
            this.upbProgress.FillAppearance = appearance1;
            this.upbProgress.Location = new System.Drawing.Point(6, 34);
            this.upbProgress.Name = "upbProgress";
            this.upbProgress.Size = new System.Drawing.Size(861, 33);
            this.upbProgress.TabIndex = 3;
            this.upbProgress.Text = "执行进度：第 [Value] 共 [Maximum] ，正在处理第 [Value] 行。";
            this.upbProgress.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            this.upbProgress.Value = 70;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "导入进度：";
            // 
            // frmImportCustomDataInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 227);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmImportCustomDataInfo";
            this.Text = "导入客户数据信息";
            this.Load += new System.EventHandler(this.frmImportCustomDataInfo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Infragistics.Win.UltraWinProgressBar.UltraProgressBar upbProgress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnImportExcel;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbExcelSheet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSelectExcel;
    }
}