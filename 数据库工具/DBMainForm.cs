using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Upgrade
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class DBMainForm : System.Windows.Forms.Form
	{
		private struct ServerItem
		{
			public string strInstance;
			public string strDescription;
			public string strLoginName;
			public string strPassWord;
            public int intIntegratedSecurity;

			public override string ToString()
			{
				return strInstance;
			}
		}
		private struct Ver
		{
			public string VerNumber;
			public string Version;
			public string Name;

			public override string ToString()
			{
				return Name;
			}
		}
		
	
		public string strServer = "NOCONA-DRP\\DRP861", strDatabase = "U8DRP_Template";
		public string strUser = "sample", strPW = "sample";
        public int intIntegratedSecurity = 0;
		public string strConnection;
        public string strSQLConnection;

		public System.Xml.XmlDocument objXMLDom = null;
        public System.Windows.Forms.ToolTip objToolTip = new System.Windows.Forms.ToolTip();
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox gbMain;
		private System.Windows.Forms.ComboBox lbServer;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbUser;
		private System.Windows.Forms.TextBox tbPW;
        private System.Windows.Forms.ComboBox lbDatabase;
		private System.Windows.Forms.Button btnData2Script;
		private System.Windows.Forms.Button btnUpGradeScript;
		private System.Windows.Forms.Button btnUpdateAllTestServer;
		private System.Windows.Forms.Button btnMakeScript;
		private System.Windows.Forms.Button btnExport2Template;
        private MenuStrip menuMain;
        private ToolStripMenuItem menu数据库工具;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem menu退出;
        private ComboBox cbVer;
        private Label label5;
        private TabControl tabControl2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private HelpProvider helpProvider1;
        private ToolStripMenuItem 探测阻塞进程BToolStripMenuItem;
        private TabPage tabPage1;
        private Button btnBlockProcess;
        private Button btnSQL语句执行情况;
        private Button btn_LoginCount;
        private Button btnAllTableInfo;
        private CheckBox cbIntegrated;
        private Button btnAllTableInfo_1;
        private Button btnBlockProcess_1;
        private GroupBox groupBox1;
        private Button btnDBReIndex;
        private Panel panel1;
        private Button btnBatchMakeDBObjects;
        private Button btnUpdateAllServer;
        private Button btnAllTableInfo_U9;
        private Button btnGetCPUNetcardNO;
        private PictureBox pbNetCard;
        private PictureBox pbCPU;
        private Button btnShrinkDB;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

        public DBMainForm()
		{
			InitializeComponent();
			
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBMainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbServer = new System.Windows.Forms.ComboBox();
            this.cbIntegrated = new System.Windows.Forms.CheckBox();
            this.tbPW = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbDatabase = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbVer = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnMakeScript = new System.Windows.Forms.Button();
            this.btnUpdateAllTestServer = new System.Windows.Forms.Button();
            this.btnData2Script = new System.Windows.Forms.Button();
            this.btnExport2Template = new System.Windows.Forms.Button();
            this.btnUpGradeScript = new System.Windows.Forms.Button();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menu数据库工具 = new System.Windows.Forms.ToolStripMenuItem();
            this.探测阻塞进程BToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu退出 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnAllTableInfo = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_LoginCount = new System.Windows.Forms.Button();
            this.btnBlockProcess = new System.Windows.Forms.Button();
            this.btnSQL语句执行情况 = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.btnAllTableInfo_1 = new System.Windows.Forms.Button();
            this.btnBlockProcess_1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShrinkDB = new System.Windows.Forms.Button();
            this.pbCPU = new System.Windows.Forms.PictureBox();
            this.pbNetCard = new System.Windows.Forms.PictureBox();
            this.btnGetCPUNetcardNO = new System.Windows.Forms.Button();
            this.btnUpdateAllServer = new System.Windows.Forms.Button();
            this.btnBatchMakeDBObjects = new System.Windows.Forms.Button();
            this.btnDBReIndex = new System.Windows.Forms.Button();
            this.btnAllTableInfo_U9 = new System.Windows.Forms.Button();
            this.gbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCPU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNetCard)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "服务器\\实例名：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbMain
            // 
            this.gbMain.Controls.Add(this.panel1);
            this.gbMain.Controls.Add(this.cbIntegrated);
            this.gbMain.Controls.Add(this.tbPW);
            this.gbMain.Controls.Add(this.tbUser);
            this.gbMain.Controls.Add(this.label4);
            this.gbMain.Controls.Add(this.label3);
            this.gbMain.Controls.Add(this.label1);
            this.gbMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbMain.Location = new System.Drawing.Point(72, 44);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(853, 262);
            this.gbMain.TabIndex = 2;
            this.gbMain.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbServer);
            this.panel1.Location = new System.Drawing.Point(245, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(570, 46);
            this.panel1.TabIndex = 14;
            // 
            // lbServer
            // 
            this.lbServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbServer.Location = new System.Drawing.Point(0, 0);
            this.lbServer.MaxDropDownItems = 10;
            this.lbServer.Name = "lbServer";
            this.lbServer.Size = new System.Drawing.Size(568, 32);
            this.lbServer.TabIndex = 1;
            this.lbServer.SelectedIndexChanged += new System.EventHandler(this.lbServer_SelectedIndexChanged);
            this.lbServer.Click += new System.EventHandler(this.lbServer_Click);
            this.lbServer.Leave += new System.EventHandler(this.lbServer_Leave);
            // 
            // cbIntegrated
            // 
            this.cbIntegrated.AutoSize = true;
            this.cbIntegrated.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbIntegrated.Location = new System.Drawing.Point(243, 206);
            this.cbIntegrated.Name = "cbIntegrated";
            this.cbIntegrated.Size = new System.Drawing.Size(229, 28);
            this.cbIntegrated.TabIndex = 4;
            this.cbIntegrated.Text = "是否集成用户认证";
            this.cbIntegrated.UseVisualStyleBackColor = true;
            this.cbIntegrated.CheckedChanged += new System.EventHandler(this.cbIntegrated_CheckedChanged);
            // 
            // tbPW
            // 
            this.tbPW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPW.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tbPW.Location = new System.Drawing.Point(243, 148);
            this.tbPW.Name = "tbPW";
            this.tbPW.PasswordChar = '*';
            this.tbPW.Size = new System.Drawing.Size(572, 35);
            this.tbPW.TabIndex = 3;
            this.tbPW.TextChanged += new System.EventHandler(this.tbPW_TextChanged);
            // 
            // tbUser
            // 
            this.tbUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbUser.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tbUser.Location = new System.Drawing.Point(243, 92);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(572, 35);
            this.tbUser.TabIndex = 2;
            this.tbUser.TextChanged += new System.EventHandler(this.tbUser_TextChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(137, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 34);
            this.label4.TabIndex = 6;
            this.label4.Text = "口令：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(104, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 34);
            this.label3.TabIndex = 5;
            this.label3.Text = "用户名：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbDatabase
            // 
            this.lbDatabase.Location = new System.Drawing.Point(1562, 472);
            this.lbDatabase.MaxDropDownItems = 10;
            this.lbDatabase.Name = "lbDatabase";
            this.lbDatabase.Size = new System.Drawing.Size(197, 32);
            this.lbDatabase.TabIndex = 4;
            this.lbDatabase.SelectedIndexChanged += new System.EventHandler(this.lbDatabase_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1430, 474);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 34);
            this.label2.TabIndex = 3;
            this.label2.Text = "数据库：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbVer
            // 
            this.cbVer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVer.Enabled = false;
            this.cbVer.Location = new System.Drawing.Point(1562, 378);
            this.cbVer.Name = "cbVer";
            this.cbVer.Size = new System.Drawing.Size(197, 32);
            this.cbVer.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(1454, 384);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 34);
            this.label5.TabIndex = 9;
            this.label5.Text = "版本：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnMakeScript
            // 
            this.btnMakeScript.Font = new System.Drawing.Font("宋体", 9F);
            this.btnMakeScript.Location = new System.Drawing.Point(464, 26);
            this.btnMakeScript.Name = "btnMakeScript";
            this.btnMakeScript.Size = new System.Drawing.Size(214, 78);
            this.btnMakeScript.TabIndex = 6;
            this.btnMakeScript.Text = "生成数据库对象脚本(&C)";
            this.btnMakeScript.Click += new System.EventHandler(this.btnMakeScript_Click);
            // 
            // btnUpdateAllTestServer
            // 
            this.btnUpdateAllTestServer.Font = new System.Drawing.Font("宋体", 9F);
            this.btnUpdateAllTestServer.Location = new System.Drawing.Point(236, 26);
            this.btnUpdateAllTestServer.Name = "btnUpdateAllTestServer";
            this.btnUpdateAllTestServer.Size = new System.Drawing.Size(215, 78);
            this.btnUpdateAllTestServer.TabIndex = 5;
            this.btnUpdateAllTestServer.Text = "脚本更新服务器(&U)";
            this.btnUpdateAllTestServer.Click += new System.EventHandler(this.btnUpdateAllTestServer_Click);
            // 
            // btnData2Script
            // 
            this.btnData2Script.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnData2Script.Font = new System.Drawing.Font("宋体", 9F);
            this.btnData2Script.Location = new System.Drawing.Point(13, 24);
            this.btnData2Script.Name = "btnData2Script";
            this.btnData2Script.Size = new System.Drawing.Size(210, 80);
            this.btnData2Script.TabIndex = 4;
            this.btnData2Script.Text = "由表数据生成SQL脚本(&C)";
            this.btnData2Script.Click += new System.EventHandler(this.btnData2Script_Click);
            // 
            // btnExport2Template
            // 
            this.btnExport2Template.Location = new System.Drawing.Point(215, 12);
            this.btnExport2Template.Name = "btnExport2Template";
            this.btnExport2Template.Size = new System.Drawing.Size(225, 76);
            this.btnExport2Template.TabIndex = 4;
            this.btnExport2Template.Text = "保存数据库对象升级脚本(&E)";
            this.btnExport2Template.Click += new System.EventHandler(this.btnExport2Template_Click);
            // 
            // btnUpGradeScript
            // 
            this.btnUpGradeScript.Font = new System.Drawing.Font("宋体", 9F);
            this.btnUpGradeScript.Location = new System.Drawing.Point(17, 12);
            this.btnUpGradeScript.Name = "btnUpGradeScript";
            this.btnUpGradeScript.Size = new System.Drawing.Size(178, 76);
            this.btnUpGradeScript.TabIndex = 3;
            this.btnUpGradeScript.Text = "升级脚本(&G)";
            this.btnUpGradeScript.Click += new System.EventHandler(this.btnUpGradeScript_Click);
            // 
            // menuMain
            // 
            this.menuMain.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1014, 24);
            this.menuMain.TabIndex = 7;
            // 
            // menu数据库工具
            // 
            this.menu数据库工具.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.探测阻塞进程BToolStripMenuItem,
            this.toolStripSeparator1,
            this.menu退出});
            this.menu数据库工具.Name = "menu数据库工具";
            this.menu数据库工具.Size = new System.Drawing.Size(77, 20);
            this.menu数据库工具.Text = "数据库工具";
            // 
            // 探测阻塞进程BToolStripMenuItem
            // 
            this.探测阻塞进程BToolStripMenuItem.Image = global::Upgrade.Properties.Resources.Search;
            this.探测阻塞进程BToolStripMenuItem.Name = "探测阻塞进程BToolStripMenuItem";
            this.探测阻塞进程BToolStripMenuItem.Size = new System.Drawing.Size(322, 44);
            this.探测阻塞进程BToolStripMenuItem.Text = "探测阻塞进程(&B)";
            this.探测阻塞进程BToolStripMenuItem.Click += new System.EventHandler(this.探测阻塞进程BToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(319, 6);
            // 
            // menu退出
            // 
            this.menu退出.Name = "menu退出";
            this.menu退出.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.menu退出.Size = new System.Drawing.Size(322, 44);
            this.menu退出.Text = "退出(&E)";
            this.menu退出.Click += new System.EventHandler(this.menu退出_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(1346, 630);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(413, 298);
            this.tabControl2.TabIndex = 8;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnAllTableInfo);
            this.tabPage3.Controls.Add(this.btnMakeScript);
            this.tabPage3.Controls.Add(this.btnUpdateAllTestServer);
            this.tabPage3.Controls.Add(this.btnData2Script);
            this.tabPage3.Location = new System.Drawing.Point(8, 39);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(397, 251);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "数据库工具";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnAllTableInfo
            // 
            this.btnAllTableInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnAllTableInfo.Font = new System.Drawing.Font("宋体", 9F);
            this.btnAllTableInfo.Location = new System.Drawing.Point(13, 116);
            this.btnAllTableInfo.Name = "btnAllTableInfo";
            this.btnAllTableInfo.Size = new System.Drawing.Size(210, 80);
            this.btnAllTableInfo.TabIndex = 7;
            this.btnAllTableInfo.Text = "查看数据库和数据表信息(&T)";
            this.btnAllTableInfo.Click += new System.EventHandler(this.btnAllTableInfo_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_LoginCount);
            this.tabPage1.Controls.Add(this.btnBlockProcess);
            this.tabPage1.Controls.Add(this.btnSQL语句执行情况);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(397, 251);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "效率并发测试";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_LoginCount
            // 
            this.btn_LoginCount.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_LoginCount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_LoginCount.Location = new System.Drawing.Point(17, 12);
            this.btn_LoginCount.Name = "btn_LoginCount";
            this.btn_LoginCount.Size = new System.Drawing.Size(202, 78);
            this.btn_LoginCount.TabIndex = 11;
            this.btn_LoginCount.Text = "登录人数(&L)";
            this.btn_LoginCount.Click += new System.EventHandler(this.btn_LoginCount_Click);
            // 
            // btnBlockProcess
            // 
            this.btnBlockProcess.Font = new System.Drawing.Font("宋体", 9F);
            this.btnBlockProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBlockProcess.Location = new System.Drawing.Point(230, 12);
            this.btnBlockProcess.Name = "btnBlockProcess";
            this.btnBlockProcess.Size = new System.Drawing.Size(201, 78);
            this.btnBlockProcess.TabIndex = 9;
            this.btnBlockProcess.Text = "探测阻塞进程(&B)";
            this.btnBlockProcess.Click += new System.EventHandler(this.btnBlockProcess_Click);
            // 
            // btnSQL语句执行情况
            // 
            this.btnSQL语句执行情况.Font = new System.Drawing.Font("宋体", 9F);
            this.btnSQL语句执行情况.Location = new System.Drawing.Point(442, 12);
            this.btnSQL语句执行情况.Name = "btnSQL语句执行情况";
            this.btnSQL语句执行情况.Size = new System.Drawing.Size(215, 78);
            this.btnSQL语句执行情况.TabIndex = 10;
            this.btnSQL语句执行情况.Text = "SQL 语句执行情况(&U)";
            this.btnSQL语句执行情况.Click += new System.EventHandler(this.btnSQL语句执行情况_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnExport2Template);
            this.tabPage4.Controls.Add(this.btnUpGradeScript);
            this.tabPage4.Location = new System.Drawing.Point(8, 39);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(397, 251);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "分销升级工具";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnAllTableInfo_1
            // 
            this.btnAllTableInfo_1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnAllTableInfo_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllTableInfo_1.Font = new System.Drawing.Font("宋体", 9F);
            this.btnAllTableInfo_1.Image = global::Upgrade.Properties.Resources.DB_User;
            this.btnAllTableInfo_1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAllTableInfo_1.Location = new System.Drawing.Point(22, 40);
            this.btnAllTableInfo_1.Name = "btnAllTableInfo_1";
            this.btnAllTableInfo_1.Size = new System.Drawing.Size(446, 80);
            this.btnAllTableInfo_1.TabIndex = 5;
            this.btnAllTableInfo_1.Text = "ERP-U8 数据库与表信息(&T)";
            this.btnAllTableInfo_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAllTableInfo_1.Click += new System.EventHandler(this.btnAllTableInfo_1_Click);
            // 
            // btnBlockProcess_1
            // 
            this.btnBlockProcess_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBlockProcess_1.Font = new System.Drawing.Font("宋体", 9F);
            this.btnBlockProcess_1.Image = global::Upgrade.Properties.Resources.DB_Delete;
            this.btnBlockProcess_1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBlockProcess_1.Location = new System.Drawing.Point(481, 40);
            this.btnBlockProcess_1.Name = "btnBlockProcess_1";
            this.btnBlockProcess_1.Size = new System.Drawing.Size(444, 80);
            this.btnBlockProcess_1.TabIndex = 5;
            this.btnBlockProcess_1.Text = "数据库探测阻塞进程(&B)  ";
            this.btnBlockProcess_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBlockProcess_1.Click += new System.EventHandler(this.btnBlockProcess_1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnShrinkDB);
            this.groupBox1.Controls.Add(this.pbCPU);
            this.groupBox1.Controls.Add(this.pbNetCard);
            this.groupBox1.Controls.Add(this.btnGetCPUNetcardNO);
            this.groupBox1.Controls.Add(this.btnUpdateAllServer);
            this.groupBox1.Controls.Add(this.btnBatchMakeDBObjects);
            this.groupBox1.Controls.Add(this.btnDBReIndex);
            this.groupBox1.Controls.Add(this.btnBlockProcess_1);
            this.groupBox1.Controls.Add(this.btnAllTableInfo_1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(26, 314);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(953, 332);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // btnShrinkDB
            // 
            this.btnShrinkDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShrinkDB.Font = new System.Drawing.Font("宋体", 9F);
            this.btnShrinkDB.Image = global::Upgrade.Properties.Resources.Database_003_img2ico_net_1_;
            this.btnShrinkDB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShrinkDB.Location = new System.Drawing.Point(22, 228);
            this.btnShrinkDB.Name = "btnShrinkDB";
            this.btnShrinkDB.Size = new System.Drawing.Size(446, 80);
            this.btnShrinkDB.TabIndex = 18;
            this.btnShrinkDB.Text = "收缩数据库文件(&I)     ";
            this.btnShrinkDB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnShrinkDB.UseVisualStyleBackColor = true;
            this.btnShrinkDB.Click += new System.EventHandler(this.btnShrinkDB_Click);
            // 
            // pbCPU
            // 
            this.pbCPU.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbCPU.Image = global::Upgrade.Properties.Resources.CPU;
            this.pbCPU.Location = new System.Drawing.Point(113, 352);
            this.pbCPU.Name = "pbCPU";
            this.pbCPU.Size = new System.Drawing.Size(67, 60);
            this.pbCPU.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCPU.TabIndex = 17;
            this.pbCPU.TabStop = false;
            // 
            // pbNetCard
            // 
            this.pbNetCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbNetCard.Image = global::Upgrade.Properties.Resources.NetCard;
            this.pbNetCard.Location = new System.Drawing.Point(30, 352);
            this.pbNetCard.Name = "pbNetCard";
            this.pbNetCard.Size = new System.Drawing.Size(68, 64);
            this.pbNetCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbNetCard.TabIndex = 16;
            this.pbNetCard.TabStop = false;
            this.pbNetCard.Click += new System.EventHandler(this.pbNetCard_Click);
            // 
            // btnGetCPUNetcardNO
            // 
            this.btnGetCPUNetcardNO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetCPUNetcardNO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetCPUNetcardNO.Location = new System.Drawing.Point(22, 340);
            this.btnGetCPUNetcardNO.Name = "btnGetCPUNetcardNO";
            this.btnGetCPUNetcardNO.Size = new System.Drawing.Size(446, 86);
            this.btnGetCPUNetcardNO.TabIndex = 15;
            this.btnGetCPUNetcardNO.Text = "获得CPU和网卡号(&CPU)";
            this.btnGetCPUNetcardNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGetCPUNetcardNO.UseVisualStyleBackColor = true;
            this.btnGetCPUNetcardNO.Click += new System.EventHandler(this.btnGetCPUNetcardNO_Click);
            // 
            // btnUpdateAllServer
            // 
            this.btnUpdateAllServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateAllServer.Font = new System.Drawing.Font("宋体", 9F);
            this.btnUpdateAllServer.Image = global::Upgrade.Properties.Resources.A403;
            this.btnUpdateAllServer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdateAllServer.Location = new System.Drawing.Point(481, 224);
            this.btnUpdateAllServer.Name = "btnUpdateAllServer";
            this.btnUpdateAllServer.Size = new System.Drawing.Size(446, 80);
            this.btnUpdateAllServer.TabIndex = 14;
            this.btnUpdateAllServer.Text = "批量更新相关服务器(&U)  ";
            this.btnUpdateAllServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdateAllServer.Click += new System.EventHandler(this.btnUpdateAllServer_Click);
            // 
            // btnBatchMakeDBObjects
            // 
            this.btnBatchMakeDBObjects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatchMakeDBObjects.Font = new System.Drawing.Font("宋体", 9F);
            this.btnBatchMakeDBObjects.Image = global::Upgrade.Properties.Resources.CuneiEdit;
            this.btnBatchMakeDBObjects.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBatchMakeDBObjects.Location = new System.Drawing.Point(481, 132);
            this.btnBatchMakeDBObjects.Name = "btnBatchMakeDBObjects";
            this.btnBatchMakeDBObjects.Size = new System.Drawing.Size(446, 80);
            this.btnBatchMakeDBObjects.TabIndex = 13;
            this.btnBatchMakeDBObjects.Text = "批量生成数据库对象脚本(&C)";
            this.btnBatchMakeDBObjects.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBatchMakeDBObjects.Click += new System.EventHandler(this.btnBatchMakeDBObjects_Click);
            // 
            // btnDBReIndex
            // 
            this.btnDBReIndex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDBReIndex.Image = global::Upgrade.Properties.Resources.DB_Refresh;
            this.btnDBReIndex.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDBReIndex.Location = new System.Drawing.Point(22, 136);
            this.btnDBReIndex.Name = "btnDBReIndex";
            this.btnDBReIndex.Size = new System.Drawing.Size(446, 80);
            this.btnDBReIndex.TabIndex = 7;
            this.btnDBReIndex.Text = "整理数据库碎片(&I)     ";
            this.btnDBReIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDBReIndex.UseVisualStyleBackColor = true;
            this.btnDBReIndex.Click += new System.EventHandler(this.btnDBReIndex_Click);
            // 
            // btnAllTableInfo_U9
            // 
            this.btnAllTableInfo_U9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnAllTableInfo_U9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllTableInfo_U9.Font = new System.Drawing.Font("宋体", 9F);
            this.btnAllTableInfo_U9.Image = global::Upgrade.Properties.Resources.DB_User;
            this.btnAllTableInfo_U9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAllTableInfo_U9.Location = new System.Drawing.Point(793, 810);
            this.btnAllTableInfo_U9.Name = "btnAllTableInfo_U9";
            this.btnAllTableInfo_U9.Size = new System.Drawing.Size(446, 80);
            this.btnAllTableInfo_U9.TabIndex = 15;
            this.btnAllTableInfo_U9.Text = "查看U9数据库和数据表信息(&T)";
            this.btnAllTableInfo_U9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAllTableInfo_U9.Click += new System.EventHandler(this.btnAllTableInfo_U9_Click);
            // 
            // DBMainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(13, 28);
            this.ClientSize = new System.Drawing.Size(1014, 673);
            this.Controls.Add(this.btnAllTableInfo_U9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbVer);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbDatabase);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.Name = "DBMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库 & 升级工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbMain.ResumeLayout(false);
            this.gbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCPU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNetCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		
		private void Form1_Load(object sender, System.EventArgs e)
		{
			InitComponets();
			
			objToolTip.AutoPopDelay = 100000;
			objToolTip.AutomaticDelay = 300;
			objToolTip.ReshowDelay = 300;
			objToolTip.SetToolTip(this.btnExport2Template, "导出自定义函数、视图、存储过程的升级脚本保存至模板库。");

            AppClass.PublicStaticCls.strServer = this.lbServer.Text.Trim();
            AppClass.PublicStaticCls.strDataBase = this.lbDatabase.Text.Trim();
		}

		#region  附加主窗体内部函数

		private void InitComponets()
		{
			if(objXMLDom != null)
			{
				//MessageBox.Show(objXMLDom.InnerXml);
                Upgrade.DBMainForm.ServerItem objSI = new Upgrade.DBMainForm.ServerItem();
                Upgrade.DBMainForm.Ver objVer = new Upgrade.DBMainForm.Ver();
				System.Xml.XmlNodeList objXMLNodeList;
				System.Xml.XmlElement objXMLEle;
				//列出数据库服务器
				objXMLNodeList = this.objXMLDom.SelectNodes("/SystemConfig/DBServers/Server");
				for(int i = 0;i < objXMLNodeList.Count; i++)
				{
					objXMLEle = (System.Xml.XmlElement)objXMLNodeList.Item(i);
					
					objSI.strInstance = objXMLEle.GetAttribute("Instance").Trim();
					objSI.strDescription  = objXMLEle.GetAttribute("Description").Trim();
					objSI.strLoginName = objXMLEle.GetAttribute("LoginName").Trim();
					objSI.strPassWord = objXMLEle.GetAttribute("PassWord").Trim();
                    objSI.intIntegratedSecurity = int.Parse(objXMLEle.GetAttribute("IntegratedSecurity").Trim());
		
					this.lbServer.Items.Add(objSI);
				}
				this.lbServer.SelectedIndex = 0;
				//列出数据库
				objXMLNodeList = this.objXMLDom.SelectNodes("/SystemConfig/DataBases/DataBase");
				for(int i = 0;i < objXMLNodeList.Count; i++)
				{
					objXMLEle = (System.Xml.XmlElement)objXMLNodeList.Item(i);
			
					this.lbDatabase.Items.Add(objXMLEle.GetAttribute("Name").Trim());
				}
				this.lbDatabase.SelectedIndex = 0;
				//列出版本
				objXMLNodeList = this.objXMLDom.SelectNodes("/SystemConfig/VerSet/Ver");
				for(int i = 0;i < objXMLNodeList.Count; i++)
				{
					objXMLEle = (System.Xml.XmlElement)objXMLNodeList.Item(i);
					
					objVer.Name = objXMLEle.GetAttribute("Name").Trim();
					objVer.VerNumber = objXMLEle.GetAttribute("VerNumber").Trim();
					objVer.Version = objXMLEle.GetAttribute("Version").Trim();
		
					this.cbVer.Items.Add(objVer);
				}
                if (this.cbVer.Items.Count > 0) this.cbVer.SelectedIndex = 0;
			}
		}

        private void lbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void tbUser_TextChanged(object sender, EventArgs e)
        {
            //Upgrade.MainForm.ServerItem objSI = (Upgrade.MainForm.ServerItem)this.lbServer.Items[this.lbServer.SelectedIndex];

            //objSI.strLoginName = this.tbUser.Text.Trim();
        }

        private void tbPW_TextChanged(object sender, EventArgs e)
        {
            //Upgrade.MainForm.ServerItem objSI = (Upgrade.MainForm.ServerItem)this.lbServer.Items[this.lbServer.SelectedIndex];

            //objSI.strPassWord = this.tbPW.Text.Trim();
        }


		private void checkConnect(string strDatabase)
		{
			try
			{
				System.Data.OleDb.OleDbConnection objSQLConnect = new System.Data.OleDb.OleDbConnection ();
				
				strServer = this.lbServer.Text.Trim();
				if(strDatabase == "") 
				{
					strDatabase = this.lbDatabase.Text.Trim();
				}
				strUser = this.tbUser.Text.Trim();
				strPW = this.tbPW.Text.Trim();

                if (this.cbIntegrated.Checked)
                {
                    strConnection = "Data Source=" + strServer + ";Initial Catalog=" + strDatabase + ";Integrated Security=SSPI;" +
                                    "Tag with column collation when possible=False;Use Procedure for Prepare=1;Auto Translate=True;Persist Security Info=False;" +
                                    "Provider=SQLOLEDB.1;Use Encryption for Data=False;Packet Size=4096";
                }
                else
                {
                    strConnection = "Data Source=" + strServer + ";Initial Catalog=" + strDatabase + ";User ID=" + strUser + ";PassWord=" + strPW + ";" +
                                    "Tag with column collation when possible=False;Use Procedure for Prepare=1;Auto Translate=True;Persist Security Info=False;" +
                                    "Provider=SQLOLEDB.1;Use Encryption for Data=False;Packet Size=4096";
                }
				objSQLConnect.ConnectionString = strConnection;
				objSQLConnect.Open();

				if(objSQLConnect.State != System.Data.ConnectionState.Open)
				{
					System.Exception objExp = new Exception("连接数据库失败。");

					strConnection = "";

					throw(objExp);
				}
				objSQLConnect.Close();
				objSQLConnect = null;
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}


        private void checkSQLConnect(string strDatabase)
        {
            try
            {
                strServer = this.lbServer.Text.Trim();
                if (strDatabase == "")
                {
                    strDatabase = this.lbDatabase.Text.Trim();
                }
                else
                {
                    strDatabase = "MASTER";
                }
                strUser = this.tbUser.Text.Trim();
                strPW = this.tbPW.Text.Trim();

                if (this.cbIntegrated.Checked)
                {
                    strSQLConnection = "Data Source=" + strServer + ";Initial Catalog=" + strDatabase + ";Integrated Security=true;" +
                                       "Application Name = 探测阻塞程序 MX ProcessProgram;Connection Timeout = 300;";

                }
                else
                {
                    strSQLConnection = "Data Source=" + strServer + ";Initial Catalog=" + strDatabase + ";User ID=" + strUser + ";" +
                                       "PassWord=" + strPW + ";" +
                                       "Application Name = 探测阻塞程序 MX ProcessProgram;Connection Timeout = 300;";
                }
            }
            catch (System.Exception e)
            {
                throw (e);
            }
        }


        
		

		#endregion

		private void lbServer_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            Upgrade.DBMainForm.ServerItem objSI = (Upgrade.DBMainForm.ServerItem)this.lbServer.Items[this.lbServer.SelectedIndex];

			this.tbUser.Text = objSI.strLoginName.Trim();
			this.tbPW.Text = objSI.strPassWord.Trim();
            this.cbIntegrated.Checked = objSI.intIntegratedSecurity > 0 ? true : false;

            this.cbIntegrated_CheckedChanged(sender, e);

            AppClass.PublicStaticCls.strServer = this.lbServer.Text;
            AppClass.PublicStaticCls.strDataBase = this.lbDatabase.Text;
		}

        private void cbIntegrated_CheckedChanged(object sender, EventArgs e)
        {
            this.tbUser.Enabled = !this.cbIntegrated.Checked;
            this.tbPW.Enabled = !this.cbIntegrated.Checked;
            this.label3.Enabled = !this.cbIntegrated.Checked;
            this.label4.Enabled = !this.cbIntegrated.Checked;
        }


        private void lbServer_Click(object sender, EventArgs e)
        {
            AppClass.PublicStaticCls.strServer = this.lbServer.Text.Trim();
            AppClass.PublicStaticCls.strDataBase = this.lbDatabase.Text.Trim();
        }

        private void lbServer_Leave(object sender, EventArgs e)
        {
            AppClass.PublicStaticCls.strServer = this.lbServer.Text.Trim();
            AppClass.PublicStaticCls.strDataBase = this.lbDatabase.Text.Trim();
        }


		
			
		private void button2_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.checkConnect("");			

				//=============================================================================
				Upgrade.UpGradeApp.ListQueryForm objForm =  new Upgrade.UpGradeApp.ListQueryForm(this.strConnection, this.cbVer.Text.Trim());
				
				//obj.ShowDialog();

				objForm.Show();
				//this.Activate();
			}
			catch(System.Exception Exp)
			{
				MessageBox.Show(Exp.Message,"提示", MessageBoxButtons.OK,MessageBoxIcon.Stop);
			}
		}

		
		private void btnData2Script_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.checkConnect("");			

				//=============================================================================
				Upgrade.UpGradeApp.ExportInsertSQLForm objForm =  new Upgrade.UpGradeApp.ExportInsertSQLForm(this.strConnection, this.lbDatabase.Text.Trim());

				objForm.Show();
			}
			catch(System.Exception Exp)
			{
				MessageBox.Show(Exp.Message,"提示", MessageBoxButtons.OK,MessageBoxIcon.Stop);
			}
		}

		private void btnUpGradeScript_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.checkConnect("U8DRP_Template");			

				//=============================================================================
				Upgrade.UpGradeApp.ListQueryForm objForm =  new Upgrade.UpGradeApp.ListQueryForm(this.strConnection, this.cbVer.Text.Trim());

				objForm.Show();
			}
			catch(System.Exception Exp)
			{
				MessageBox.Show(Exp.Message,"提示", MessageBoxButtons.OK,MessageBoxIcon.Stop);
			}
		}

		private void btnUpdateAllTestServer_Click(object sender, System.EventArgs e)
		{
			try
			{
				//=============================================================================
				Upgrade.UpGradeApp.UpdateAllTestServerForm objForm =  new Upgrade.UpGradeApp.UpdateAllTestServerForm();
				
				//obj.ShowDialog();

				objForm.Show();
				//this.Activate();
			}
			catch(System.Exception Exp)
			{
				MessageBox.Show(Exp.Message,"提示", MessageBoxButtons.OK,MessageBoxIcon.Stop);
			}
		}

		private void btnMakeScript_Click(object sender, System.EventArgs e)
		{
			try
			{
				Upgrade.UpGradeApp.MakeFuncsViewsProcsForm objForm =  new Upgrade.UpGradeApp.MakeFuncsViewsProcsForm();
				
				objForm.strInstance = this.lbServer.Text.Trim();
                objForm.strUserName = this.tbUser.Text.Trim();
                objForm.strPassword = this.tbPW.Text.Trim();
	
				objForm.Show();
			}
			catch(System.Exception Exp)
			{
				MessageBox.Show(Exp.Message,"提示", MessageBoxButtons.OK,MessageBoxIcon.Stop);
			}
		}

		private void btnExport2Template_Click(object sender, System.EventArgs e)
		{
			try
			{
				Upgrade.UpGradeApp.UpdateDBObject2TemplateForm objForm =  new Upgrade.UpGradeApp.UpdateDBObject2TemplateForm();

                objForm.strServer = this.lbServer.Text.Trim();
                objForm.strUser = this.tbUser.Text.Trim();
                objForm.strPW = this.tbPW.Text.Trim();

                Upgrade.DBMainForm.Ver objVer = (Upgrade.DBMainForm.Ver)this.cbVer.Items[this.cbVer.SelectedIndex];
				objForm.strVer = objVer.VerNumber;

				objForm.Show();
			}
			catch(System.Exception Exp)
			{
				MessageBox.Show(Exp.Message,"提示", MessageBoxButtons.OK,MessageBoxIcon.Stop);
			}
		
		}

        private void btnBlockProcess_Click(object sender, EventArgs e)
        {
            try
            {
                Upgrade.Apps.ProcessBlockedForm objForm = new Upgrade.Apps.ProcessBlockedForm();

                this.checkSQLConnect("Master");
                objForm.strConnection = strSQLConnection;
                objForm.strInstance = this.strServer;

                objForm.Show();
            }
            catch (System.Exception Exp)
            {
                MessageBox.Show(Exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnSQL语句执行情况_Click(object sender, EventArgs e)
        {
            try
            {
                Apps.ExcuteSQLInfoForm objForm = new Apps.ExcuteSQLInfoForm();

                this.checkSQLConnect(this.lbDatabase.Text);
                objForm.strConnection = strSQLConnection;
                objForm.strInstance = this.strServer;

                objForm.Show();
            }
            catch (System.Exception Exp)
            {
                MessageBox.Show(Exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btn_LoginCount_Click(object sender, EventArgs e)
        {
            try
            {
                Apps.LoginCountForm objForm = new Apps.LoginCountForm();

                this.checkSQLConnect(this.lbDatabase.Text);
                objForm.strConnection = strSQLConnection;
                objForm.strInstance = this.strServer;

                objForm.Show();
            }
            catch (System.Exception Exp)
            {
                MessageBox.Show(Exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnAllTableInfo_Click(object sender, EventArgs e)
        {
            try
            {
                Apps.DBAndTableInfoForm objForm = new Apps.DBAndTableInfoForm();
                this.checkSQLConnect(this.lbDatabase.Text);

                objForm.strConnection = strSQLConnection;
                objForm.strInstance = this.strServer;
                objForm.strUser = this.strUser;
                objForm.strPassword = this.strPW;
                objForm.blnIntegrated = this.cbIntegrated.Checked;

                objForm.Show();

                try
                {
                    objForm.ShowAllDatabaseInfoDetail();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (System.Exception Exp)
            {
                MessageBox.Show(Exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }



        #region  菜单事件
        private void menu数据生成脚本_Click(object sender, EventArgs e)
        {
            btnData2Script_Click(sender, e);
        }

        private void menu更新测试服务_Click(object sender, EventArgs e)
        {
            btnUpdateAllTestServer_Click(sender, e);
        }

        private void menu提取数据库对象_Click(object sender, EventArgs e)
        {
            btnMakeScript_Click(sender, e);
        }

        private void menu升级脚本_Click(object sender, EventArgs e)
        {
            btnUpGradeScript_Click(sender, e);
        }

        private void menu保存数据库对象至数据库_Click(object sender, EventArgs e)
        {
            btnExport2Template_Click(sender, e);
        }

        

        private void 探测阻塞进程BToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnBlockProcess_Click(sender, e);
        }


        private void menu退出_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        #endregion

        private void btnAllTableInfo_1_Click(object sender, EventArgs e)
        {
            this.btnAllTableInfo_Click(sender, e);
        }

        private void btnAllTableInfo_U9_Click(object sender, EventArgs e)
        {
            try
            {
                Apps.U9DBAndTableInfoForm objForm = new Apps.U9DBAndTableInfoForm();
                this.checkSQLConnect(this.lbDatabase.Text);

                objForm.strConnection = strSQLConnection;
                objForm.strInstance = this.strServer;
                objForm.strUser = this.strUser;
                objForm.strPassword = this.strPW;
                objForm.blnIntegrated = this.cbIntegrated.Checked;
                objForm.intCareU9DBInfo = 0;
                objForm.intCheckU9DB = 0;

                objForm.Show();

                try
                {
                    objForm.ShowAllDatabaseInfoDetail();
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (System.Exception Exp)
            {
                MessageBox.Show(Exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }        
        }





        private void btnBlockProcess_1_Click(object sender, EventArgs e)
        {
            this.btnBlockProcess_Click(sender, e);
        }

        private void btnDBReIndex_Click(object sender, EventArgs e)
        {
            try
            {
                Apps.DBReIndexandStatisticsFrom objForm = new Apps.DBReIndexandStatisticsFrom();
                this.checkSQLConnect(this.lbDatabase.Text);

                objForm.strConnection = strSQLConnection;
                objForm.strInstance = this.lbServer.Text.Trim();
                objForm.strUser = this.tbUser.Text.Trim();
                objForm.strPassword = this.tbPW.Text.Trim();
                objForm.blnIntegrated = this.cbIntegrated.Checked;

                objForm.Show();
            }
            catch (System.Exception Exp)
            {
                MessageBox.Show(Exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnBatchMakeDBObjects_Click(object sender, EventArgs e)
        {
            try
            {
                Upgrade.Apps.BatchMakeDBObjectScriptForm objForm = new Upgrade.Apps.BatchMakeDBObjectScriptForm();

                objForm.strInstance = this.lbServer.Text.Trim();
                objForm.strDBName = this.lbDatabase.Text.Trim();
                objForm.strLoginName = this.tbUser.Text.Trim();
                objForm.strPassword = this.tbPW.Text.Trim();
                objForm.blnIntegratedSecurity = this.cbIntegrated.Checked;

                objForm.Show();
            }
            catch (System.Exception Exp)
            {
                MessageBox.Show(Exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnUpdateAllServer_Click(object sender, EventArgs e)
        {
            try
            {
                //=============================================================================
                Upgrade.UpGradeApp.UpdateAllTestServerForm objForm = new Upgrade.UpGradeApp.UpdateAllTestServerForm();

                objForm.Show();
            }
            catch (System.Exception Exp)
            {
                MessageBox.Show(Exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnGetCPUNetcardNO_Click(object sender, EventArgs e)
        {
            this.GetCPUNetcardNOForm();
        }
        
        private void pbNetCard_Click_1(object sender, EventArgs e)
        {
            this.GetCPUNetcardNOForm();
        }

        private void pbNetCard_Click(object sender, EventArgs e)
        {
            this.GetCPUNetcardNOForm();
        }

        private void GetCPUNetcardNOForm()
        {
            try
            {
                Apps.frmCPUNetCardInfo objForm = new Apps.frmCPUNetCardInfo();

                objForm.Show();
            }
            catch (System.Exception Exp)
            {
                MessageBox.Show(Exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnShrinkDB_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    this.checkSQLConnect(this.lbDatabase.Text);
                    this.checkConnect("MASTER");
                }
                catch (Exception E)
                {
                    MessageBox.Show("不能连接数据库：" + this.strServer + "，请确定数据库实例和数据库名称是否正确。\r\n" + E.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Apps.frmDBShrinkFile objForm = new Apps.frmDBShrinkFile();
                objForm.strConnection = this.strSQLConnection;
                objForm.strInstance = this.strServer;
                objForm.strUser = this.strUser;
                objForm.strPassword = this.strPW;
                objForm.blnIntegrated = this.cbIntegrated.Checked;

                objForm.Show();
            }
            catch (System.Exception Exp)
            {
                MessageBox.Show(Exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        
        

        

        

       


        

        

       



        

        
        
		
		
	}

	
	

}
 