namespace Upgrade.Apps
{
    partial class frmDBReIndexandStatistics
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Database", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("数据库", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("开始时间");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("结束时间");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Table");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Table", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("数据库", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("数据表");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("目前状态");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("开始时间");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("结束时间");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("耗时");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDBReIndexandStatistics));
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.utRunStateSimple = new Infragistics.Win.UltraWinTree.UltraTree();
            this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ugDBDefragInfo = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRunConfig = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRemoveAllTables = new System.Windows.Forms.Button();
            this.btnSelectAllTables = new System.Windows.Forms.Button();
            this.btnRamoveTable = new System.Windows.Forms.Button();
            this.btnSelectTable = new System.Windows.Forms.Button();
            this.lbSelectDBTables = new System.Windows.Forms.ListBox();
            this.rbSelectTables = new System.Windows.Forms.RadioButton();
            this.rbFragAll = new System.Windows.Forms.RadioButton();
            this.lbDBTableAll = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cklDBList = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.utcMainInfo = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.upbOneDB = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
            this.upbAllDB = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ultraTabPageControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.utRunStateSimple)).BeginInit();
            this.ultraTabPageControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugDBDefragInfo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.utcMainInfo)).BeginInit();
            this.utcMainInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraTabPageControl1
            // 
            this.ultraTabPageControl1.Controls.Add(this.utRunStateSimple);
            this.ultraTabPageControl1.Location = new System.Drawing.Point(2, 21);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            this.ultraTabPageControl1.Size = new System.Drawing.Size(510, 535);
            // 
            // utRunStateSimple
            // 
            this.utRunStateSimple.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.utRunStateSimple.Dock = System.Windows.Forms.DockStyle.Fill;
            this.utRunStateSimple.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.utRunStateSimple.Location = new System.Drawing.Point(0, 0);
            this.utRunStateSimple.Name = "utRunStateSimple";
            this.utRunStateSimple.SettingsKey = "frmDBReIndexandStatistics.utRunStateSimple";
            this.utRunStateSimple.Size = new System.Drawing.Size(510, 535);
            this.utRunStateSimple.TabIndex = 0;
            // 
            // ultraTabPageControl2
            // 
            this.ultraTabPageControl2.Controls.Add(this.ugDBDefragInfo);
            this.ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl2.Name = "ultraTabPageControl2";
            this.ultraTabPageControl2.Size = new System.Drawing.Size(510, 535);
            // 
            // ugDBDefragInfo
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            appearance1.TextVAlignAsString = "Middle";
            this.ugDBDefragInfo.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 107;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 162;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 155;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4});
            ultraGridColumn5.Header.VisiblePosition = 0;
            ultraGridColumn5.Width = 88;
            ultraGridColumn6.Header.VisiblePosition = 1;
            ultraGridColumn7.Header.VisiblePosition = 2;
            ultraGridColumn8.Header.VisiblePosition = 3;
            ultraGridColumn9.Header.VisiblePosition = 4;
            ultraGridColumn10.Header.VisiblePosition = 5;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10});
            this.ugDBDefragInfo.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ugDBDefragInfo.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.ugDBDefragInfo.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ugDBDefragInfo.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.ugDBDefragInfo.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ugDBDefragInfo.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.ugDBDefragInfo.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ugDBDefragInfo.DisplayLayout.GroupByBox.Hidden = true;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ugDBDefragInfo.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.ugDBDefragInfo.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ugDBDefragInfo.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ugDBDefragInfo.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.ugDBDefragInfo.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.ugDBDefragInfo.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            this.ugDBDefragInfo.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.ugDBDefragInfo.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ugDBDefragInfo.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ugDBDefragInfo.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.ugDBDefragInfo.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ugDBDefragInfo.DisplayLayout.Override.CellAppearance = appearance8;
            this.ugDBDefragInfo.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ugDBDefragInfo.DisplayLayout.Override.CellPadding = 0;
            this.ugDBDefragInfo.DisplayLayout.Override.DefaultRowHeight = 22;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ugDBDefragInfo.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.ugDBDefragInfo.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.ugDBDefragInfo.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ugDBDefragInfo.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ugDBDefragInfo.DisplayLayout.Override.RowAlternateAppearance = appearance11;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.BorderColor = System.Drawing.Color.Silver;
            this.ugDBDefragInfo.DisplayLayout.Override.RowAppearance = appearance12;
            this.ugDBDefragInfo.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ExtendFirstColumn;
            this.ugDBDefragInfo.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.ugDBDefragInfo.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Free;
            this.ugDBDefragInfo.DisplayLayout.Override.RowSizingArea = Infragistics.Win.UltraWinGrid.RowSizingArea.EntireRow;
            appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ugDBDefragInfo.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
            this.ugDBDefragInfo.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ugDBDefragInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ugDBDefragInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ugDBDefragInfo.Location = new System.Drawing.Point(0, 0);
            this.ugDBDefragInfo.Name = "ugDBDefragInfo";
            this.ugDBDefragInfo.Size = new System.Drawing.Size(510, 535);
            this.ugDBDefragInfo.TabIndex = 0;
            this.ugDBDefragInfo.Text = "ultraGrid1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRunConfig);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.cklDBList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 668);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnRunConfig
            // 
            this.btnRunConfig.Image = global::Upgrade.Properties.Resources.DB_Refresh;
            this.btnRunConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRunConfig.Location = new System.Drawing.Point(233, 159);
            this.btnRunConfig.Name = "btnRunConfig";
            this.btnRunConfig.Size = new System.Drawing.Size(203, 45);
            this.btnRunConfig.TabIndex = 5;
            this.btnRunConfig.Text = "执行整理索引与统计信息(&R)";
            this.btnRunConfig.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRunConfig.UseVisualStyleBackColor = true;
            this.btnRunConfig.Click += new System.EventHandler(this.btnRunConfig_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnRemoveAllTables);
            this.groupBox2.Controls.Add(this.btnSelectAllTables);
            this.groupBox2.Controls.Add(this.btnRamoveTable);
            this.groupBox2.Controls.Add(this.btnSelectTable);
            this.groupBox2.Controls.Add(this.lbSelectDBTables);
            this.groupBox2.Controls.Add(this.rbSelectTables);
            this.groupBox2.Controls.Add(this.rbFragAll);
            this.groupBox2.Controls.Add(this.lbDBTableAll);
            this.groupBox2.Location = new System.Drawing.Point(9, 255);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(448, 408);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择指定数据库数据表";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(259, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "已选择要整理的数据表：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "数据表列表：";
            // 
            // btnRemoveAllTables
            // 
            this.btnRemoveAllTables.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRemoveAllTables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveAllTables.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRemoveAllTables.Location = new System.Drawing.Point(197, 262);
            this.btnRemoveAllTables.Name = "btnRemoveAllTables";
            this.btnRemoveAllTables.Size = new System.Drawing.Size(52, 25);
            this.btnRemoveAllTables.TabIndex = 10;
            this.btnRemoveAllTables.Text = "<<";
            this.btnRemoveAllTables.UseVisualStyleBackColor = true;
            this.btnRemoveAllTables.Click += new System.EventHandler(this.btnRemoveAllTables_Click);
            // 
            // btnSelectAllTables
            // 
            this.btnSelectAllTables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAllTables.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectAllTables.Location = new System.Drawing.Point(197, 132);
            this.btnSelectAllTables.Name = "btnSelectAllTables";
            this.btnSelectAllTables.Size = new System.Drawing.Size(52, 25);
            this.btnSelectAllTables.TabIndex = 7;
            this.btnSelectAllTables.Text = ">>";
            this.btnSelectAllTables.UseVisualStyleBackColor = true;
            this.btnSelectAllTables.Click += new System.EventHandler(this.btnSelectAllTables_Click);
            // 
            // btnRamoveTable
            // 
            this.btnRamoveTable.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRamoveTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRamoveTable.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRamoveTable.Location = new System.Drawing.Point(197, 221);
            this.btnRamoveTable.Name = "btnRamoveTable";
            this.btnRamoveTable.Size = new System.Drawing.Size(52, 25);
            this.btnRamoveTable.TabIndex = 9;
            this.btnRamoveTable.Text = "<";
            this.btnRamoveTable.UseVisualStyleBackColor = true;
            this.btnRamoveTable.Click += new System.EventHandler(this.btnRamoveTable_Click);
            // 
            // btnSelectTable
            // 
            this.btnSelectTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectTable.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectTable.Location = new System.Drawing.Point(197, 163);
            this.btnSelectTable.Name = "btnSelectTable";
            this.btnSelectTable.Size = new System.Drawing.Size(52, 25);
            this.btnSelectTable.TabIndex = 8;
            this.btnSelectTable.Text = ">";
            this.btnSelectTable.UseVisualStyleBackColor = true;
            this.btnSelectTable.Click += new System.EventHandler(this.btnSelectTable_Click);
            // 
            // lbSelectDBTables
            // 
            this.lbSelectDBTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbSelectDBTables.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSelectDBTables.FormattingEnabled = true;
            this.lbSelectDBTables.HorizontalScrollbar = true;
            this.lbSelectDBTables.ItemHeight = 12;
            this.lbSelectDBTables.Location = new System.Drawing.Point(255, 62);
            this.lbSelectDBTables.Name = "lbSelectDBTables";
            this.lbSelectDBTables.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbSelectDBTables.Size = new System.Drawing.Size(184, 338);
            this.lbSelectDBTables.TabIndex = 6;
            this.lbSelectDBTables.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbSelectDBTables_MouseDoubleClick);
            // 
            // rbSelectTables
            // 
            this.rbSelectTables.AutoSize = true;
            this.rbSelectTables.Location = new System.Drawing.Point(119, 17);
            this.rbSelectTables.Name = "rbSelectTables";
            this.rbSelectTables.Size = new System.Drawing.Size(107, 16);
            this.rbSelectTables.TabIndex = 3;
            this.rbSelectTables.Text = "选择数据表整理";
            this.rbSelectTables.UseVisualStyleBackColor = true;
            this.rbSelectTables.CheckedChanged += new System.EventHandler(this.rbSelectTables_CheckedChanged);
            this.rbSelectTables.Click += new System.EventHandler(this.rbSelectTables_Click);
            // 
            // rbFragAll
            // 
            this.rbFragAll.AutoSize = true;
            this.rbFragAll.Checked = true;
            this.rbFragAll.Location = new System.Drawing.Point(6, 17);
            this.rbFragAll.Name = "rbFragAll";
            this.rbFragAll.Size = new System.Drawing.Size(107, 16);
            this.rbFragAll.TabIndex = 2;
            this.rbFragAll.TabStop = true;
            this.rbFragAll.Text = "整理所有数据表";
            this.rbFragAll.UseVisualStyleBackColor = true;
            this.rbFragAll.CheckedChanged += new System.EventHandler(this.rbFragAll_CheckedChanged);
            this.rbFragAll.Click += new System.EventHandler(this.rbFragAll_Click);
            // 
            // lbDBTableAll
            // 
            this.lbDBTableAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDBTableAll.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDBTableAll.FormattingEnabled = true;
            this.lbDBTableAll.HorizontalScrollbar = true;
            this.lbDBTableAll.ItemHeight = 12;
            this.lbDBTableAll.Location = new System.Drawing.Point(6, 62);
            this.lbDBTableAll.Name = "lbDBTableAll";
            this.lbDBTableAll.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbDBTableAll.Size = new System.Drawing.Size(185, 338);
            this.lbDBTableAll.TabIndex = 5;
            this.lbDBTableAll.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbDBTableAll_MouseDoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(251, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(154, 96);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // cklDBList
            // 
            this.cklDBList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cklDBList.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cklDBList.FormattingEnabled = true;
            this.cklDBList.Location = new System.Drawing.Point(9, 34);
            this.cklDBList.Name = "cklDBList";
            this.cklDBList.Size = new System.Drawing.Size(202, 210);
            this.cklDBList.TabIndex = 1;
            this.cklDBList.SelectedIndexChanged += new System.EventHandler(this.cklDBList_SelectedIndexChanged);
            this.cklDBList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.cklDBList_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选取要整理的数据库：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.utcMainInfo);
            this.groupBox3.Controls.Add(this.upbOneDB);
            this.groupBox3.Controls.Add(this.upbAllDB);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(472, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(520, 668);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // utcMainInfo
            // 
            this.utcMainInfo.Controls.Add(this.ultraTabSharedControlsPage1);
            this.utcMainInfo.Controls.Add(this.ultraTabPageControl1);
            this.utcMainInfo.Controls.Add(this.ultraTabPageControl2);
            this.utcMainInfo.Location = new System.Drawing.Point(3, 107);
            this.utcMainInfo.Name = "utcMainInfo";
            this.utcMainInfo.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.utcMainInfo.Size = new System.Drawing.Size(514, 558);
            this.utcMainInfo.TabIndex = 5;
            this.utcMainInfo.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.TopLeft;
            ultraTab1.TabPage = this.ultraTabPageControl1;
            ultraTab1.Text = "执行情况简单信息";
            ultraTab2.TabPage = this.ultraTabPageControl2;
            ultraTab2.Text = "执行情况列表信息";
            this.utcMainInfo.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1,
            ultraTab2});
            this.utcMainInfo.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005;
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(510, 535);
            // 
            // upbOneDB
            // 
            appearance14.BackColor = System.Drawing.Color.WhiteSmoke;
            appearance14.ForeColor = System.Drawing.Color.Black;
            appearance14.ForeColorDisabled = System.Drawing.Color.Black;
            this.upbOneDB.Appearance = appearance14;
            this.upbOneDB.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance15.BackColor = System.Drawing.Color.CornflowerBlue;
            appearance15.BackColor2 = System.Drawing.Color.SteelBlue;
            appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance15.ForeColor = System.Drawing.Color.Black;
            this.upbOneDB.FillAppearance = appearance15;
            this.upbOneDB.Location = new System.Drawing.Point(8, 58);
            this.upbOneDB.Name = "upbOneDB";
            this.upbOneDB.Size = new System.Drawing.Size(506, 23);
            this.upbOneDB.TabIndex = 4;
            this.upbOneDB.Text = "[Value]/[Range]";
            this.upbOneDB.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.upbOneDB.Value = 36;
            // 
            // upbAllDB
            // 
            appearance16.BackColor = System.Drawing.Color.WhiteSmoke;
            appearance16.ForeColor = System.Drawing.Color.Black;
            appearance16.ForeColorDisabled = System.Drawing.Color.Black;
            appearance16.TextHAlignAsString = "Center";
            appearance16.TextVAlignAsString = "Middle";
            this.upbAllDB.Appearance = appearance16;
            this.upbAllDB.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance17.BackColor = System.Drawing.Color.CornflowerBlue;
            appearance17.BackColor2 = System.Drawing.Color.SteelBlue;
            appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.VerticalBump;
            appearance17.ForeColor = System.Drawing.Color.Black;
            this.upbAllDB.FillAppearance = appearance17;
            this.upbAllDB.Location = new System.Drawing.Point(8, 30);
            this.upbAllDB.Name = "upbAllDB";
            this.upbAllDB.Size = new System.Drawing.Size(506, 23);
            this.upbAllDB.TabIndex = 3;
            this.upbAllDB.Text = "[Value]/[Range]";
            this.upbAllDB.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.upbAllDB.Value = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "执行进度：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "执行情况：";
            // 
            // frmDBReIndexandStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 668);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDBReIndexandStatistics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "整理数据库数据表索引及统计信息";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDBReIndexandStatistics_FormClosing);
            this.Load += new System.EventHandler(this.frmDBReIndexandStatistics_Load);
            this.Resize += new System.EventHandler(this.frmDBReIndexandStatistics_Resize);
            this.ultraTabPageControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.utRunStateSimple)).EndInit();
            this.ultraTabPageControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugDBDefragInfo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.utcMainInfo)).EndInit();
            this.utcMainInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox cklDBList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbSelectTables;
        private System.Windows.Forms.RadioButton rbFragAll;
        private System.Windows.Forms.Button btnSelectAllTables;
        private System.Windows.Forms.ListBox lbSelectDBTables;
        private System.Windows.Forms.ListBox lbDBTableAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRemoveAllTables;
        private System.Windows.Forms.Button btnRamoveTable;
        private System.Windows.Forms.Button btnSelectTable;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Infragistics.Win.UltraWinProgressBar.UltraProgressBar upbAllDB;
        private Infragistics.Win.UltraWinProgressBar.UltraProgressBar upbOneDB;
        private System.Windows.Forms.Button btnRunConfig;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl utcMainInfo;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private Infragistics.Win.UltraWinTree.UltraTree utRunStateSimple;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl2;
        private Infragistics.Win.UltraWinGrid.UltraGrid ugDBDefragInfo;

    }
}