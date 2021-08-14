namespace Upgrade.Apps
{
    partial class frmDBShrinkFile
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
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDBShrinkFile));
            this.label1 = new System.Windows.Forms.Label();
            this.clbDatabases = new System.Windows.Forms.CheckedListBox();
            this.panel_Main = new System.Windows.Forms.Panel();
            this.nudDataSpace = new System.Windows.Forms.NumericUpDown();
            this.nudLogSpace = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.upbShrinkFileProgress = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
            this.upbActive = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.dvwDBFiles = new System.Windows.Forms.DataGridView();
            this.选择 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel_SelectFileType = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbShrinkData = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbShrinkLog = new System.Windows.Forms.CheckBox();
            this.btnShrinkFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nudLogPercent = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudDataPercent = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.panel_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLogSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwDBFiles)).BeginInit();
            this.panel_SelectFileType.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLogPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataPercent)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(18, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库：";
            // 
            // clbDatabases
            // 
            this.clbDatabases.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbDatabases.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbDatabases.FormattingEnabled = true;
            this.clbDatabases.Items.AddRange(new object[] {
            "afasdfasfd",
            "afdasdfasdf",
            "awerewqrqw",
            "frwerewqr",
            "werqwerqwe"});
            this.clbDatabases.Location = new System.Drawing.Point(15, 86);
            this.clbDatabases.Name = "clbDatabases";
            this.clbDatabases.Size = new System.Drawing.Size(257, 496);
            this.clbDatabases.Sorted = true;
            this.clbDatabases.TabIndex = 1;
            this.clbDatabases.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbDatabases_ItemCheck);
            this.clbDatabases.SelectedIndexChanged += new System.EventHandler(this.clbDatabases_SelectedIndexChanged);
            // 
            // panel_Main
            // 
            this.panel_Main.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel_Main.Controls.Add(this.nudDataSpace);
            this.panel_Main.Controls.Add(this.nudLogSpace);
            this.panel_Main.Controls.Add(this.label5);
            this.panel_Main.Controls.Add(this.btnExit);
            this.panel_Main.Controls.Add(this.label4);
            this.panel_Main.Controls.Add(this.upbShrinkFileProgress);
            this.panel_Main.Controls.Add(this.upbActive);
            this.panel_Main.Controls.Add(this.label2);
            this.panel_Main.Controls.Add(this.dvwDBFiles);
            this.panel_Main.Controls.Add(this.panel_SelectFileType);
            this.panel_Main.Controls.Add(this.label1);
            this.panel_Main.Controls.Add(this.clbDatabases);
            this.panel_Main.Location = new System.Drawing.Point(10, 9);
            this.panel_Main.Name = "panel_Main";
            this.panel_Main.Size = new System.Drawing.Size(1088, 655);
            this.panel_Main.TabIndex = 2;
            this.panel_Main.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Main_Paint);
            // 
            // nudDataSpace
            // 
            this.nudDataSpace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudDataSpace.Location = new System.Drawing.Point(745, 414);
            this.nudDataSpace.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudDataSpace.Name = "nudDataSpace";
            this.nudDataSpace.Size = new System.Drawing.Size(78, 21);
            this.nudDataSpace.TabIndex = 10;
            this.nudDataSpace.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudDataSpace.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudDataSpace.Visible = false;
            // 
            // nudLogSpace
            // 
            this.nudLogSpace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudLogSpace.Location = new System.Drawing.Point(505, 414);
            this.nudLogSpace.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudLogSpace.Name = "nudLogSpace";
            this.nudLogSpace.Size = new System.Drawing.Size(75, 21);
            this.nudLogSpace.TabIndex = 8;
            this.nudLogSpace.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudLogSpace.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudLogSpace.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(636, 418);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "收缩文件至(MB)：";
            this.label5.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(977, 15);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 31);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "退出(&E)";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(399, 418);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "收缩文件至(MB)：";
            this.label4.Visible = false;
            // 
            // upbShrinkFileProgress
            // 
            appearance3.BackColor = System.Drawing.Color.Transparent;
            appearance3.BorderColor = System.Drawing.Color.DarkGray;
            this.upbShrinkFileProgress.Appearance = appearance3;
            this.upbShrinkFileProgress.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.Color.SteelBlue;
            this.upbShrinkFileProgress.FillAppearance = appearance4;
            this.upbShrinkFileProgress.Location = new System.Drawing.Point(15, 621);
            this.upbShrinkFileProgress.Name = "upbShrinkFileProgress";
            this.upbShrinkFileProgress.Size = new System.Drawing.Size(1056, 24);
            this.upbShrinkFileProgress.TabIndex = 7;
            this.upbShrinkFileProgress.Text = "收缩数据库。    第 [Value] 个  共 [Range] 个";
            this.upbShrinkFileProgress.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.upbShrinkFileProgress.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // upbActive
            // 
            appearance1.BackColor = System.Drawing.Color.Transparent;
            appearance1.BorderColor = System.Drawing.Color.DarkGray;
            appearance1.ForeColor = System.Drawing.Color.Black;
            this.upbActive.Appearance = appearance1;
            this.upbActive.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance2.BackColor = System.Drawing.Color.SteelBlue;
            appearance2.BorderColor = System.Drawing.Color.Gray;
            this.upbActive.FillAppearance = appearance2;
            this.upbActive.Location = new System.Drawing.Point(15, 588);
            this.upbActive.Name = "upbActive";
            this.upbActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.upbActive.Size = new System.Drawing.Size(1056, 27);
            this.upbActive.TabIndex = 6;
            this.upbActive.Text = "执行收缩数据库，请稍等......";
            this.upbActive.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.upbActive.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(285, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "数据库文件：";
            // 
            // dvwDBFiles
            // 
            this.dvwDBFiles.AllowUserToAddRows = false;
            this.dvwDBFiles.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.GhostWhite;
            this.dvwDBFiles.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dvwDBFiles.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.dvwDBFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dvwDBFiles.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvwDBFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dvwDBFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvwDBFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.选择,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dvwDBFiles.GridColor = System.Drawing.Color.DarkGray;
            this.dvwDBFiles.Location = new System.Drawing.Point(279, 86);
            this.dvwDBFiles.MultiSelect = false;
            this.dvwDBFiles.Name = "dvwDBFiles";
            this.dvwDBFiles.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvwDBFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dvwDBFiles.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dvwDBFiles.RowTemplate.Height = 23;
            this.dvwDBFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvwDBFiles.Size = new System.Drawing.Size(792, 496);
            this.dvwDBFiles.TabIndex = 3;
            this.dvwDBFiles.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvwDBFiles_CellLeave);
            this.dvwDBFiles.Paint += new System.Windows.Forms.PaintEventHandler(this.dvwDBFiles_Paint);
            this.dvwDBFiles.Leave += new System.EventHandler(this.dvwDBFiles_Leave);
            // 
            // 选择
            // 
            this.选择.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.选择.HeaderText = "选择";
            this.选择.Name = "选择";
            this.选择.ReadOnly = true;
            this.选择.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.选择.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.选择.Width = 40;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "逻辑名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 130;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "文件类型";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column3.HeaderText = "文件大小(MB)";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 110;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "文件路径";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 320;
            // 
            // panel_SelectFileType
            // 
            this.panel_SelectFileType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.panel_SelectFileType.Controls.Add(this.panel4);
            this.panel_SelectFileType.Controls.Add(this.panel3);
            this.panel_SelectFileType.Controls.Add(this.btnShrinkFile);
            this.panel_SelectFileType.Controls.Add(this.label3);
            this.panel_SelectFileType.Location = new System.Drawing.Point(15, 10);
            this.panel_SelectFileType.Name = "panel_SelectFileType";
            this.panel_SelectFileType.Size = new System.Drawing.Size(956, 49);
            this.panel_SelectFileType.TabIndex = 2;
            this.panel_SelectFileType.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_SelectFileType_Paint);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.nudLogPercent);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.cbShrinkData);
            this.panel4.Location = new System.Drawing.Point(453, 9);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(341, 32);
            this.panel4.TabIndex = 5;
            // 
            // cbShrinkData
            // 
            this.cbShrinkData.AutoSize = true;
            this.cbShrinkData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbShrinkData.Location = new System.Drawing.Point(8, 8);
            this.cbShrinkData.Name = "cbShrinkData";
            this.cbShrinkData.Size = new System.Drawing.Size(129, 16);
            this.cbShrinkData.TabIndex = 1;
            this.cbShrinkData.Text = "收缩数据库数据文件";
            this.cbShrinkData.UseVisualStyleBackColor = true;
            this.cbShrinkData.CheckedChanged += new System.EventHandler(this.cbShrinkData_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.nudDataPercent);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.cbShrinkLog);
            this.panel3.Location = new System.Drawing.Point(97, 9);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(332, 32);
            this.panel3.TabIndex = 4;
            // 
            // cbShrinkLog
            // 
            this.cbShrinkLog.AutoSize = true;
            this.cbShrinkLog.Checked = true;
            this.cbShrinkLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShrinkLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbShrinkLog.Location = new System.Drawing.Point(7, 8);
            this.cbShrinkLog.Name = "cbShrinkLog";
            this.cbShrinkLog.Size = new System.Drawing.Size(129, 16);
            this.cbShrinkLog.TabIndex = 0;
            this.cbShrinkLog.Text = "收缩数据库日志文件";
            this.cbShrinkLog.UseVisualStyleBackColor = true;
            this.cbShrinkLog.CheckedChanged += new System.EventHandler(this.cbShrinkLog_CheckedChanged);
            // 
            // btnShrinkFile
            // 
            this.btnShrinkFile.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnShrinkFile.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnShrinkFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShrinkFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.btnShrinkFile.Location = new System.Drawing.Point(806, 8);
            this.btnShrinkFile.Name = "btnShrinkFile";
            this.btnShrinkFile.Size = new System.Drawing.Size(128, 31);
            this.btnShrinkFile.TabIndex = 5;
            this.btnShrinkFile.Text = "收缩数据库文件(&S)";
            this.btnShrinkFile.UseVisualStyleBackColor = false;
            this.btnShrinkFile.Click += new System.EventHandler(this.btnShrinkFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "选择收缩类型：";
            // 
            // nudLogPercent
            // 
            this.nudLogPercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudLogPercent.Location = new System.Drawing.Point(266, 6);
            this.nudLogPercent.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudLogPercent.Name = "nudLogPercent";
            this.nudLogPercent.Size = new System.Drawing.Size(61, 21);
            this.nudLogPercent.TabIndex = 13;
            this.nudLogPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudLogPercent.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(157, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "文件预留空间%：";
            // 
            // nudDataPercent
            // 
            this.nudDataPercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudDataPercent.Location = new System.Drawing.Point(256, 6);
            this.nudDataPercent.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudDataPercent.Name = "nudDataPercent";
            this.nudDataPercent.Size = new System.Drawing.Size(60, 21);
            this.nudDataPercent.TabIndex = 14;
            this.nudDataPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudDataPercent.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(166, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "文件预留空间%：";
            // 
            // frmDBShrinkFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1110, 676);
            this.Controls.Add(this.panel_Main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDBShrinkFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "收缩数据库";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDBShrinkFile_FormClosed);
            this.Load += new System.EventHandler(this.frmDBShrinkFile_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmDBShrinkFile_Paint);
            this.Resize += new System.EventHandler(this.frmDBShrinkFile_Resize);
            this.panel_Main.ResumeLayout(false);
            this.panel_Main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLogSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwDBFiles)).EndInit();
            this.panel_SelectFileType.ResumeLayout(false);
            this.panel_SelectFileType.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLogPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataPercent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbDatabases;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.Panel panel_SelectFileType;
        private System.Windows.Forms.CheckBox cbShrinkData;
        private System.Windows.Forms.CheckBox cbShrinkLog;
        private System.Windows.Forms.DataGridView dvwDBFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnShrinkFile;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 选择;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private Infragistics.Win.UltraWinProgressBar.UltraProgressBar upbShrinkFileProgress;
        private Infragistics.Win.UltraWinProgressBar.UltraProgressBar upbActive;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.NumericUpDown nudDataSpace;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.NumericUpDown nudLogSpace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudLogPercent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudDataPercent;
        private System.Windows.Forms.Label label7;
    }
}