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
	public class MainForm : System.Windows.Forms.Form
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

            public Ver(string sVerNumber, string sVersion, string sName)
            {
                VerNumber = sVerNumber;
                Version = sVersion;
                Name = sName;
            }

			public override string ToString()
			{
				return Name;
			}
		}
		
	
		public string strServer = "LOCALHOST", strDatabase = "Master";
		public string strUser = "sa", strPW = "sa";
        public int intIntegratedSecurity = 0;
		public string strConnection;
        public string strSQLConnection;

        public int intUseDRPUpgrade = 0;
        public int intCareU8DBInfo = 0;
        public int intCheckU8DB = 0;

		public System.Xml.XmlDocument objXMLDom = null;
        public System.Windows.Forms.ToolTip objToolTip = new System.Windows.Forms.ToolTip();

        public System.Windows.Forms.ComboBox lbServer;
        public System.Windows.Forms.ComboBox lbDatabase;
        public System.Windows.Forms.TextBox tbUser;
        public System.Windows.Forms.TextBox tbPW;


		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox gbMain;
		
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
        
		private System.Windows.Forms.Button btnData2Script;
		private System.Windows.Forms.Button btnUpGradeScript;
		private System.Windows.Forms.Button btnUpdateAllTestServer;
		private System.Windows.Forms.Button btnMakeScript;
		private System.Windows.Forms.Button btnExport2Template;
        private MenuStrip menuMain;
        private ToolStripMenuItem menu数据库工具;
        private ToolStripMenuItem menu数据生成脚本;
        private ToolStripMenuItem menu更新测试服务器;
        private ToolStripMenuItem menu提取数据库对象;
        private ToolStripMenuItem menu数据库升级工具;
        private ToolStripMenuItem menu升级脚本;
        private ToolStripMenuItem menu保存数据库对象至数据库;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem menu退出;
        private ToolStripMenuItem 探测阻塞进程BToolStripMenuItem;
        private Button btnBlockProcess;
        private Button btnSQL语句执行情况;
        private Button btn_LoginCount;
        private Button btnAllTableInfo;
        private CheckBox cbIntegrated;
        private Button btnDBReIndex;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl utMainTab;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl2;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl3;
        private Panel panel1;
        private Panel panel2;
        private Button btnMyDBReIndex;
        private Button btnBatchMakeDBObjects;
        private Button btnAllTableInfo_U9;
        private Button btnCutDBInfo2DB;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl4;
        private Button btnGetCPUNetcardNO;
        private PictureBox pbCPU;
        private PictureBox pbNetCard;
        private Button btnShrinkDB;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
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
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.btnShrinkDB = new System.Windows.Forms.Button();
            this.btnCutDBInfo2DB = new System.Windows.Forms.Button();
            this.btnBatchMakeDBObjects = new System.Windows.Forms.Button();
            this.btnMyDBReIndex = new System.Windows.Forms.Button();
            this.btnAllTableInfo = new System.Windows.Forms.Button();
            this.btnData2Script = new System.Windows.Forms.Button();
            this.btnUpdateAllTestServer = new System.Windows.Forms.Button();
            this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.btn_LoginCount = new System.Windows.Forms.Button();
            this.btnBlockProcess = new System.Windows.Forms.Button();
            this.btnSQL语句执行情况 = new System.Windows.Forms.Button();
            this.ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.btnExport2Template = new System.Windows.Forms.Button();
            this.btnUpGradeScript = new System.Windows.Forms.Button();
            this.btnMakeScript = new System.Windows.Forms.Button();
            this.ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.pbCPU = new System.Windows.Forms.PictureBox();
            this.pbNetCard = new System.Windows.Forms.PictureBox();
            this.btnGetCPUNetcardNO = new System.Windows.Forms.Button();
            this.btnAllTableInfo_U9 = new System.Windows.Forms.Button();
            this.btnDBReIndex = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.cbIntegrated = new System.Windows.Forms.CheckBox();
            this.tbPW = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbDatabase = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbServer = new System.Windows.Forms.ComboBox();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menu数据库工具 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu数据生成脚本 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu更新测试服务器 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu提取数据库对象 = new System.Windows.Forms.ToolStripMenuItem();
            this.探测阻塞进程BToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu退出 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu数据库升级工具 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu升级脚本 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu保存数据库对象至数据库 = new System.Windows.Forms.ToolStripMenuItem();
            this.utMainTab = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.ultraTabPageControl1.SuspendLayout();
            this.ultraTabPageControl2.SuspendLayout();
            this.ultraTabPageControl3.SuspendLayout();
            this.ultraTabPageControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCPU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNetCard)).BeginInit();
            this.gbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.utMainTab)).BeginInit();
            this.utMainTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraTabPageControl1
            // 
            this.ultraTabPageControl1.Controls.Add(this.btnShrinkDB);
            this.ultraTabPageControl1.Controls.Add(this.btnCutDBInfo2DB);
            this.ultraTabPageControl1.Controls.Add(this.btnBatchMakeDBObjects);
            this.ultraTabPageControl1.Controls.Add(this.btnMyDBReIndex);
            this.ultraTabPageControl1.Controls.Add(this.btnAllTableInfo);
            this.ultraTabPageControl1.Controls.Add(this.btnData2Script);
            this.ultraTabPageControl1.Controls.Add(this.btnUpdateAllTestServer);
            this.ultraTabPageControl1.Location = new System.Drawing.Point(1, 36);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            this.ultraTabPageControl1.Size = new System.Drawing.Size(925, 405);
            // 
            // btnShrinkDB
            // 
            this.btnShrinkDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShrinkDB.Font = new System.Drawing.Font("宋体", 9F);
            this.btnShrinkDB.Image = global::Upgrade.Properties.Resources.Database_003_img2ico_net_1_;
            this.btnShrinkDB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShrinkDB.Location = new System.Drawing.Point(24, 298);
            this.btnShrinkDB.Name = "btnShrinkDB";
            this.btnShrinkDB.Size = new System.Drawing.Size(420, 80);
            this.btnShrinkDB.TabIndex = 14;
            this.btnShrinkDB.Text = "收缩数据库文件(&I)     ";
            this.btnShrinkDB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnShrinkDB.UseVisualStyleBackColor = true;
            this.btnShrinkDB.Click += new System.EventHandler(this.btnShrinkDB_Click);
            // 
            // btnCutDBInfo2DB
            // 
            this.btnCutDBInfo2DB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnCutDBInfo2DB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCutDBInfo2DB.Font = new System.Drawing.Font("宋体", 9F);
            this.btnCutDBInfo2DB.Image = global::Upgrade.Properties.Resources.SOUND_1;
            this.btnCutDBInfo2DB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCutDBInfo2DB.Location = new System.Drawing.Point(464, 206);
            this.btnCutDBInfo2DB.Name = "btnCutDBInfo2DB";
            this.btnCutDBInfo2DB.Size = new System.Drawing.Size(420, 80);
            this.btnCutDBInfo2DB.TabIndex = 13;
            this.btnCutDBInfo2DB.Text = "U8 导入客户数据信息(&T)";
            this.btnCutDBInfo2DB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCutDBInfo2DB.Click += new System.EventHandler(this.btnCutDBInfo2DB_Click);
            // 
            // btnBatchMakeDBObjects
            // 
            this.btnBatchMakeDBObjects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatchMakeDBObjects.Font = new System.Drawing.Font("宋体", 9F);
            this.btnBatchMakeDBObjects.Image = global::Upgrade.Properties.Resources.CuneiEdit;
            this.btnBatchMakeDBObjects.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBatchMakeDBObjects.Location = new System.Drawing.Point(462, 114);
            this.btnBatchMakeDBObjects.Name = "btnBatchMakeDBObjects";
            this.btnBatchMakeDBObjects.Size = new System.Drawing.Size(422, 80);
            this.btnBatchMakeDBObjects.TabIndex = 12;
            this.btnBatchMakeDBObjects.Text = "批量生成数据库对象脚本(&C)";
            this.btnBatchMakeDBObjects.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBatchMakeDBObjects.Click += new System.EventHandler(this.btnBatchMakeDBObjects_Click);
            // 
            // btnMyDBReIndex
            // 
            this.btnMyDBReIndex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMyDBReIndex.Font = new System.Drawing.Font("宋体", 9F);
            this.btnMyDBReIndex.Image = global::Upgrade.Properties.Resources.DB_Refresh;
            this.btnMyDBReIndex.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMyDBReIndex.Location = new System.Drawing.Point(28, 114);
            this.btnMyDBReIndex.Name = "btnMyDBReIndex";
            this.btnMyDBReIndex.Size = new System.Drawing.Size(421, 80);
            this.btnMyDBReIndex.TabIndex = 11;
            this.btnMyDBReIndex.Text = "整理数据库碎片(&I)     ";
            this.btnMyDBReIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMyDBReIndex.UseVisualStyleBackColor = true;
            this.btnMyDBReIndex.Click += new System.EventHandler(this.btnMyDBReIndex_Click);
            // 
            // btnAllTableInfo
            // 
            this.btnAllTableInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnAllTableInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllTableInfo.Font = new System.Drawing.Font("宋体", 9F);
            this.btnAllTableInfo.Image = global::Upgrade.Properties.Resources.DB_User;
            this.btnAllTableInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAllTableInfo.Location = new System.Drawing.Point(28, 206);
            this.btnAllTableInfo.Name = "btnAllTableInfo";
            this.btnAllTableInfo.Size = new System.Drawing.Size(421, 80);
            this.btnAllTableInfo.TabIndex = 8;
            this.btnAllTableInfo.Text = "用友U8+数据库与表信息(&T)";
            this.btnAllTableInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAllTableInfo.Click += new System.EventHandler(this.btnAllTableInfo_Click);
            // 
            // btnData2Script
            // 
            this.btnData2Script.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnData2Script.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnData2Script.Font = new System.Drawing.Font("宋体", 9F);
            this.btnData2Script.Image = global::Upgrade.Properties.Resources.Script;
            this.btnData2Script.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnData2Script.Location = new System.Drawing.Point(462, 22);
            this.btnData2Script.Name = "btnData2Script";
            this.btnData2Script.Size = new System.Drawing.Size(422, 80);
            this.btnData2Script.TabIndex = 7;
            this.btnData2Script.Text = "表数据生成SQL脚本(&C)  ";
            this.btnData2Script.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnData2Script.Click += new System.EventHandler(this.btnData2Script_Click);
            // 
            // btnUpdateAllTestServer
            // 
            this.btnUpdateAllTestServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateAllTestServer.Font = new System.Drawing.Font("宋体", 9F);
            this.btnUpdateAllTestServer.Image = global::Upgrade.Properties.Resources.A403;
            this.btnUpdateAllTestServer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdateAllTestServer.Location = new System.Drawing.Point(28, 22);
            this.btnUpdateAllTestServer.Name = "btnUpdateAllTestServer";
            this.btnUpdateAllTestServer.Size = new System.Drawing.Size(421, 80);
            this.btnUpdateAllTestServer.TabIndex = 10;
            this.btnUpdateAllTestServer.Text = "脚本更新服务器(&U)    ";
            this.btnUpdateAllTestServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdateAllTestServer.Click += new System.EventHandler(this.btnUpdateAllTestServer_Click);
            // 
            // ultraTabPageControl2
            // 
            this.ultraTabPageControl2.Controls.Add(this.btn_LoginCount);
            this.ultraTabPageControl2.Controls.Add(this.btnBlockProcess);
            this.ultraTabPageControl2.Controls.Add(this.btnSQL语句执行情况);
            this.ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl2.Name = "ultraTabPageControl2";
            this.ultraTabPageControl2.Size = new System.Drawing.Size(925, 405);
            // 
            // btn_LoginCount
            // 
            this.btn_LoginCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LoginCount.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_LoginCount.Image = global::Upgrade.Properties.Resources.A09175;
            this.btn_LoginCount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_LoginCount.Location = new System.Drawing.Point(505, 24);
            this.btn_LoginCount.Name = "btn_LoginCount";
            this.btn_LoginCount.Size = new System.Drawing.Size(375, 80);
            this.btn_LoginCount.TabIndex = 11;
            this.btn_LoginCount.Text = "数据库服务器登录人数(&L)";
            this.btn_LoginCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_LoginCount.Click += new System.EventHandler(this.btn_LoginCount_Click);
            // 
            // btnBlockProcess
            // 
            this.btnBlockProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBlockProcess.Font = new System.Drawing.Font("宋体", 9F);
            this.btnBlockProcess.Image = global::Upgrade.Properties.Resources.DB_Delete;
            this.btnBlockProcess.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnBlockProcess.Location = new System.Drawing.Point(26, 24);
            this.btnBlockProcess.Name = "btnBlockProcess";
            this.btnBlockProcess.Size = new System.Drawing.Size(457, 80);
            this.btnBlockProcess.TabIndex = 9;
            this.btnBlockProcess.Text = "探测数据库服务器阻塞进程(&B)";
            this.btnBlockProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBlockProcess.Click += new System.EventHandler(this.btnBlockProcess_Click);
            // 
            // btnSQL语句执行情况
            // 
            this.btnSQL语句执行情况.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSQL语句执行情况.Font = new System.Drawing.Font("宋体", 9F);
            this.btnSQL语句执行情况.Location = new System.Drawing.Point(26, 116);
            this.btnSQL语句执行情况.Name = "btnSQL语句执行情况";
            this.btnSQL语句执行情况.Size = new System.Drawing.Size(457, 80);
            this.btnSQL语句执行情况.TabIndex = 10;
            this.btnSQL语句执行情况.Text = "SQL 语句执行情况(&U)";
            this.btnSQL语句执行情况.Click += new System.EventHandler(this.btnSQL语句执行情况_Click);
            // 
            // ultraTabPageControl3
            // 
            this.ultraTabPageControl3.Controls.Add(this.btnExport2Template);
            this.ultraTabPageControl3.Controls.Add(this.btnUpGradeScript);
            this.ultraTabPageControl3.Controls.Add(this.btnMakeScript);
            this.ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl3.Name = "ultraTabPageControl3";
            this.ultraTabPageControl3.Size = new System.Drawing.Size(925, 405);
            // 
            // btnExport2Template
            // 
            this.btnExport2Template.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport2Template.Font = new System.Drawing.Font("宋体", 9F);
            this.btnExport2Template.Location = new System.Drawing.Point(451, 28);
            this.btnExport2Template.Name = "btnExport2Template";
            this.btnExport2Template.Size = new System.Drawing.Size(401, 76);
            this.btnExport2Template.TabIndex = 4;
            this.btnExport2Template.Text = "保存数据库对象升级脚本(&E)";
            this.btnExport2Template.Click += new System.EventHandler(this.btnExport2Template_Click);
            // 
            // btnUpGradeScript
            // 
            this.btnUpGradeScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpGradeScript.Font = new System.Drawing.Font("宋体", 9F);
            this.btnUpGradeScript.Location = new System.Drawing.Point(26, 28);
            this.btnUpGradeScript.Name = "btnUpGradeScript";
            this.btnUpGradeScript.Size = new System.Drawing.Size(412, 76);
            this.btnUpGradeScript.TabIndex = 3;
            this.btnUpGradeScript.Text = "U8分销系统升级脚本(&G)";
            this.btnUpGradeScript.Click += new System.EventHandler(this.btnUpGradeScript_Click);
            // 
            // btnMakeScript
            // 
            this.btnMakeScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMakeScript.Font = new System.Drawing.Font("宋体", 9F);
            this.btnMakeScript.Image = global::Upgrade.Properties.Resources.Image_Object;
            this.btnMakeScript.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMakeScript.Location = new System.Drawing.Point(26, 116);
            this.btnMakeScript.Name = "btnMakeScript";
            this.btnMakeScript.Size = new System.Drawing.Size(412, 80);
            this.btnMakeScript.TabIndex = 11;
            this.btnMakeScript.Text = "生成数据库对象脚本(&C)";
            this.btnMakeScript.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMakeScript.Click += new System.EventHandler(this.btnMakeScript_Click);
            // 
            // ultraTabPageControl4
            // 
            this.ultraTabPageControl4.Controls.Add(this.pbCPU);
            this.ultraTabPageControl4.Controls.Add(this.pbNetCard);
            this.ultraTabPageControl4.Controls.Add(this.btnGetCPUNetcardNO);
            this.ultraTabPageControl4.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl4.Name = "ultraTabPageControl4";
            this.ultraTabPageControl4.Size = new System.Drawing.Size(925, 405);
            // 
            // pbCPU
            // 
            this.pbCPU.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbCPU.Image = global::Upgrade.Properties.Resources.CPU;
            this.pbCPU.Location = new System.Drawing.Point(30, 32);
            this.pbCPU.Name = "pbCPU";
            this.pbCPU.Size = new System.Drawing.Size(74, 66);
            this.pbCPU.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCPU.TabIndex = 1;
            this.pbCPU.TabStop = false;
            this.pbCPU.Click += new System.EventHandler(this.pbCPU_Click);
            // 
            // pbNetCard
            // 
            this.pbNetCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbNetCard.Image = global::Upgrade.Properties.Resources.NetCard;
            this.pbNetCard.Location = new System.Drawing.Point(115, 34);
            this.pbNetCard.Name = "pbNetCard";
            this.pbNetCard.Size = new System.Drawing.Size(74, 58);
            this.pbNetCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbNetCard.TabIndex = 2;
            this.pbNetCard.TabStop = false;
            this.pbNetCard.Click += new System.EventHandler(this.pbNetCard_Click);
            // 
            // btnGetCPUNetcardNO
            // 
            this.btnGetCPUNetcardNO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetCPUNetcardNO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGetCPUNetcardNO.Location = new System.Drawing.Point(22, 22);
            this.btnGetCPUNetcardNO.Name = "btnGetCPUNetcardNO";
            this.btnGetCPUNetcardNO.Size = new System.Drawing.Size(418, 86);
            this.btnGetCPUNetcardNO.TabIndex = 0;
            this.btnGetCPUNetcardNO.Text = "获得CPU和网卡号";
            this.btnGetCPUNetcardNO.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGetCPUNetcardNO.UseVisualStyleBackColor = true;
            this.btnGetCPUNetcardNO.Click += new System.EventHandler(this.btnGetCPUNetcardNO_Click);
            // 
            // btnAllTableInfo_U9
            // 
            this.btnAllTableInfo_U9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnAllTableInfo_U9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllTableInfo_U9.Font = new System.Drawing.Font("宋体", 9F);
            this.btnAllTableInfo_U9.Image = global::Upgrade.Properties.Resources.DB_User;
            this.btnAllTableInfo_U9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAllTableInfo_U9.Location = new System.Drawing.Point(1270, 320);
            this.btnAllTableInfo_U9.Name = "btnAllTableInfo_U9";
            this.btnAllTableInfo_U9.Size = new System.Drawing.Size(446, 80);
            this.btnAllTableInfo_U9.TabIndex = 13;
            this.btnAllTableInfo_U9.Text = "查看U9数据库和数据表信息(&T)";
            this.btnAllTableInfo_U9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAllTableInfo_U9.Click += new System.EventHandler(this.btnAllTableInfo_U9_Click);
            // 
            // btnDBReIndex
            // 
            this.btnDBReIndex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDBReIndex.Image = global::Upgrade.Properties.Resources.DB_Refresh;
            this.btnDBReIndex.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDBReIndex.Location = new System.Drawing.Point(1270, 206);
            this.btnDBReIndex.Name = "btnDBReIndex";
            this.btnDBReIndex.Size = new System.Drawing.Size(405, 80);
            this.btnDBReIndex.TabIndex = 9;
            this.btnDBReIndex.Text = "整理数据库索引(&I)   ";
            this.btnDBReIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDBReIndex.UseVisualStyleBackColor = true;
            this.btnDBReIndex.Click += new System.EventHandler(this.btnDBReIndex_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(33, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "服务器\\实例名：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbMain
            // 
            this.gbMain.Controls.Add(this.cbIntegrated);
            this.gbMain.Controls.Add(this.tbPW);
            this.gbMain.Controls.Add(this.tbUser);
            this.gbMain.Controls.Add(this.label4);
            this.gbMain.Controls.Add(this.label3);
            this.gbMain.Controls.Add(this.label2);
            this.gbMain.Controls.Add(this.label1);
            this.gbMain.Controls.Add(this.panel1);
            this.gbMain.Controls.Add(this.panel2);
            this.gbMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbMain.Location = new System.Drawing.Point(52, 56);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(871, 304);
            this.gbMain.TabIndex = 2;
            this.gbMain.TabStop = false;
            // 
            // cbIntegrated
            // 
            this.cbIntegrated.AutoSize = true;
            this.cbIntegrated.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbIntegrated.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbIntegrated.Location = new System.Drawing.Point(251, 264);
            this.cbIntegrated.Name = "cbIntegrated";
            this.cbIntegrated.Size = new System.Drawing.Size(229, 28);
            this.cbIntegrated.TabIndex = 5;
            this.cbIntegrated.Text = "是否集成用户认证";
            this.cbIntegrated.UseVisualStyleBackColor = true;
            this.cbIntegrated.CheckedChanged += new System.EventHandler(this.cbIntegrated_CheckedChanged);
            // 
            // tbPW
            // 
            this.tbPW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPW.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPW.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tbPW.Location = new System.Drawing.Point(251, 206);
            this.tbPW.Name = "tbPW";
            this.tbPW.PasswordChar = '*';
            this.tbPW.Size = new System.Drawing.Size(557, 35);
            this.tbPW.TabIndex = 4;
            this.tbPW.TextChanged += new System.EventHandler(this.tbPW_TextChanged);
            // 
            // tbUser
            // 
            this.tbUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbUser.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbUser.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tbUser.Location = new System.Drawing.Point(254, 150);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(554, 35);
            this.tbUser.TabIndex = 3;
            this.tbUser.TextChanged += new System.EventHandler(this.tbUser_TextChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(152, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 34);
            this.label4.TabIndex = 6;
            this.label4.Text = "口令：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(119, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 34);
            this.label3.TabIndex = 5;
            this.label3.Text = "用户名：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(128, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 34);
            this.label2.TabIndex = 3;
            this.label2.Text = "数据库：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbDatabase);
            this.panel1.Location = new System.Drawing.Point(251, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(559, 44);
            this.panel1.TabIndex = 10;
            // 
            // lbDatabase
            // 
            this.lbDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDatabase.DropDownHeight = 260;
            this.lbDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbDatabase.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDatabase.IntegralHeight = false;
            this.lbDatabase.Location = new System.Drawing.Point(0, 0);
            this.lbDatabase.MaxDropDownItems = 10;
            this.lbDatabase.Name = "lbDatabase";
            this.lbDatabase.Size = new System.Drawing.Size(557, 32);
            this.lbDatabase.TabIndex = 2;
            this.lbDatabase.DropDown += new System.EventHandler(this.lbDatabase_DropDown);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lbServer);
            this.panel2.Location = new System.Drawing.Point(251, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(559, 44);
            this.panel2.TabIndex = 11;
            // 
            // lbServer
            // 
            this.lbServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbServer.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbServer.Location = new System.Drawing.Point(0, 0);
            this.lbServer.MaxDropDownItems = 10;
            this.lbServer.Name = "lbServer";
            this.lbServer.Size = new System.Drawing.Size(557, 32);
            this.lbServer.TabIndex = 1;
            this.lbServer.SelectedIndexChanged += new System.EventHandler(this.lbServer_SelectedIndexChanged);
            this.lbServer.Click += new System.EventHandler(this.lbServer_Click);
            this.lbServer.Leave += new System.EventHandler(this.lbServer_Leave);
            // 
            // menuMain
            // 
            this.menuMain.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(995, 24);
            this.menuMain.TabIndex = 7;
            // 
            // menu数据库工具
            // 
            this.menu数据库工具.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu数据生成脚本,
            this.menu更新测试服务器,
            this.menu提取数据库对象,
            this.探测阻塞进程BToolStripMenuItem,
            this.toolStripSeparator1,
            this.menu退出});
            this.menu数据库工具.Name = "menu数据库工具";
            this.menu数据库工具.Size = new System.Drawing.Size(77, 20);
            this.menu数据库工具.Text = "数据库工具";
            // 
            // menu数据生成脚本
            // 
            this.menu数据生成脚本.Name = "menu数据生成脚本";
            this.menu数据生成脚本.Size = new System.Drawing.Size(391, 44);
            this.menu数据生成脚本.Text = "由数据生成脚本(&D)";
            this.menu数据生成脚本.Click += new System.EventHandler(this.menu数据生成脚本_Click);
            // 
            // menu更新测试服务器
            // 
            this.menu更新测试服务器.Name = "menu更新测试服务器";
            this.menu更新测试服务器.Size = new System.Drawing.Size(391, 44);
            this.menu更新测试服务器.Text = "更新测试服务器(&M)";
            this.menu更新测试服务器.Click += new System.EventHandler(this.menu更新测试服务_Click);
            // 
            // menu提取数据库对象
            // 
            this.menu提取数据库对象.Name = "menu提取数据库对象";
            this.menu提取数据库对象.Size = new System.Drawing.Size(391, 44);
            this.menu提取数据库对象.Text = "提取数据库对象SQL(&S)";
            this.menu提取数据库对象.Click += new System.EventHandler(this.menu提取数据库对象_Click);
            // 
            // 探测阻塞进程BToolStripMenuItem
            // 
            this.探测阻塞进程BToolStripMenuItem.Image = global::Upgrade.Properties.Resources.Search;
            this.探测阻塞进程BToolStripMenuItem.Name = "探测阻塞进程BToolStripMenuItem";
            this.探测阻塞进程BToolStripMenuItem.Size = new System.Drawing.Size(391, 44);
            this.探测阻塞进程BToolStripMenuItem.Text = "探测阻塞进程(&B)";
            this.探测阻塞进程BToolStripMenuItem.Click += new System.EventHandler(this.探测阻塞进程BToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(388, 6);
            // 
            // menu退出
            // 
            this.menu退出.Name = "menu退出";
            this.menu退出.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.menu退出.Size = new System.Drawing.Size(391, 44);
            this.menu退出.Text = "退出(&E)";
            this.menu退出.Click += new System.EventHandler(this.menu退出_Click);
            // 
            // menu数据库升级工具
            // 
            this.menu数据库升级工具.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu升级脚本,
            this.menu保存数据库对象至数据库});
            this.menu数据库升级工具.Name = "menu数据库升级工具";
            this.menu数据库升级工具.Size = new System.Drawing.Size(101, 20);
            this.menu数据库升级工具.Text = "数据库升级工具";
            // 
            // menu升级脚本
            // 
            this.menu升级脚本.Image = global::Upgrade.Properties.Resources.Main;
            this.menu升级脚本.Name = "menu升级脚本";
            this.menu升级脚本.Size = new System.Drawing.Size(440, 44);
            this.menu升级脚本.Text = "升级脚本(&G)";
            this.menu升级脚本.Click += new System.EventHandler(this.menu升级脚本_Click);
            // 
            // menu保存数据库对象至数据库
            // 
            this.menu保存数据库对象至数据库.Name = "menu保存数据库对象至数据库";
            this.menu保存数据库对象至数据库.Size = new System.Drawing.Size(440, 44);
            this.menu保存数据库对象至数据库.Text = "保存数据库对象至数据库(&E)";
            this.menu保存数据库对象至数据库.Click += new System.EventHandler(this.menu保存数据库对象至数据库_Click);
            // 
            // utMainTab
            // 
            this.utMainTab.Controls.Add(this.ultraTabSharedControlsPage1);
            this.utMainTab.Controls.Add(this.ultraTabPageControl1);
            this.utMainTab.Controls.Add(this.ultraTabPageControl2);
            this.utMainTab.Controls.Add(this.ultraTabPageControl3);
            this.utMainTab.Controls.Add(this.ultraTabPageControl4);
            this.utMainTab.Location = new System.Drawing.Point(26, 386);
            this.utMainTab.Name = "utMainTab";
            this.utMainTab.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.utMainTab.Size = new System.Drawing.Size(927, 442);
            this.utMainTab.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
            this.utMainTab.TabIndex = 6;
            ultraTab1.TabPage = this.ultraTabPageControl1;
            ultraTab1.Text = "数据库工具";
            ultraTab2.TabPage = this.ultraTabPageControl2;
            ultraTab2.Text = "效率并发测试";
            ultraTab3.TabPage = this.ultraTabPageControl3;
            ultraTab3.Text = "分销升级工具";
            ultraTab4.TabPage = this.ultraTabPageControl4;
            ultraTab4.Text = "杂项";
            this.utMainTab.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1,
            ultraTab2,
            ultraTab3,
            ultraTab4});
            this.utMainTab.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.utMainTab.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.utMainTab.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005;
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(925, 405);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(13, 28);
            this.ClientSize = new System.Drawing.Size(995, 854);
            this.Controls.Add(this.btnAllTableInfo_U9);
            this.Controls.Add(this.btnDBReIndex);
            this.Controls.Add(this.gbMain);
            this.Controls.Add(this.utMainTab);
            this.Controls.Add(this.menuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库 & 升级工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ultraTabPageControl1.ResumeLayout(false);
            this.ultraTabPageControl2.ResumeLayout(false);
            this.ultraTabPageControl3.ResumeLayout(false);
            this.ultraTabPageControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCPU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNetCard)).EndInit();
            this.gbMain.ResumeLayout(false);
            this.gbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.utMainTab)).EndInit();
            this.utMainTab.ResumeLayout(false);
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

            Infragistics.Win.UltraWinProgressBar.UltraProgressBar uuu = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
            uuu.Maximum = 1000;
            uuu.Value = 100;
            uuu.Dispose();
            uuu = null;
		}



        


		#region  附加主窗体内部函数

		private void InitComponets()
		{
			if(objXMLDom != null)
			{
				//MessageBox.Show(objXMLDom.InnerXml);
				Upgrade.MainForm.ServerItem objSI = new Upgrade.MainForm.ServerItem();
				//Upgrade.MainForm.Ver objVer = new Upgrade.MainForm.Ver();
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

                objXMLEle = (System.Xml.XmlElement)this.objXMLDom.SelectSingleNode("/SystemConfig/U8ERPConfig/CareAccountInfo");
                this.intCareU8DBInfo = int.Parse(objXMLEle.InnerText);

                objXMLEle = (System.Xml.XmlElement)this.objXMLDom.SelectSingleNode("/SystemConfig/U8ERPConfig/CheckU8DB");
                this.intCheckU8DB = int.Parse(objXMLEle.InnerText);             
			}

            if (this.intUseDRPUpgrade == 0)
            {
                this.utMainTab.Tabs.RemoveAt(2);
            }
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
                    strConnection = "Data Source=" + strServer + ";Initial Catalog=" + strDatabase + ";Integrated Security=SSPI; Connection Timeout=10; " +
                                    "Tag with column collation when possible=False;Use Procedure for Prepare=1;Auto Translate=True;Persist Security Info=False;" +
                                    "Provider=SQLOLEDB.1;Use Encryption for Data=False;Packet Size=4096";
                }
                else
                {
                    strConnection = "Data Source=" + strServer + ";Initial Catalog=" + strDatabase + ";User ID=" + strUser + ";PassWord=" + strPW + "; Connection Timeout=10; " +
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


        public void checkSQLConnect(string strDatabase)
        {
            try
            {
                strServer = this.lbServer.Text.Trim();
                if (strDatabase == "")
                {
                    strDatabase = "MASTER";
                }
                else
                {
                    strDatabase = this.lbDatabase.Text.Trim();
                }
                strUser = this.tbUser.Text.Trim();
                strPW = this.tbPW.Text.Trim();

                if (this.cbIntegrated.Checked)
                {
                    this.strSQLConnection = "Data Source=" + strServer + ";Initial Catalog=" + strDatabase + ";Integrated Security=true;" +
                                            "Application Name = 数据库工具 MX ProcessProgram;Connection Timeout = 300;";

                }
                else
                {
                    this.strSQLConnection = "Data Source=" + strServer + ";Initial Catalog=" + strDatabase + ";User ID=" + strUser + ";" +
                                            "PassWord=" + strPW + ";" +
                                            "Application Name = 数据库工具 MX ProcessProgram;Connection Timeout = 300;";
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
			Upgrade.MainForm.ServerItem objSI = (Upgrade.MainForm.ServerItem)this.lbServer.Items[this.lbServer.SelectedIndex];

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
				Upgrade.UpGradeApp.ListQueryForm objForm =  new Upgrade.UpGradeApp.ListQueryForm(this.strConnection, "872");
				
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
				Upgrade.UpGradeApp.ListQueryForm objForm =  new Upgrade.UpGradeApp.ListQueryForm(this.strConnection, "872");

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

        private void btnBatchMakeDBObjects_Click(object sender, EventArgs e)
        {
            try
            {
                Upgrade.Apps.BatchMakeDBObjectScriptForm objForm = new Upgrade.Apps.BatchMakeDBObjectScriptForm();

                objForm.strInstance = this.lbServer.Text.Trim();
                objForm.strDBName = this.lbDatabase.Text.Trim();
                objForm.strLoginName = this.tbUser.Text.Trim();
                objForm.strPassword = this.tbPW.Text.Trim();
                objForm.blnIntegratedSecurity = this.cbIntegrated.Checked ;

                objForm.Show();
            }
            catch (System.Exception Exp)
            {
                MessageBox.Show(Exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

                //Upgrade.MainForm.Ver objVer = (Upgrade.MainForm.Ver)this.cbVer.Items[this.cbVer.SelectedIndex];
                //objForm.strVer = objVer.VerNumber;

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

        public void btnAllTableInfo_Click(object sender, EventArgs e)
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
                objForm.intCareU8DBInfo = this.intCareU8DBInfo;
                objForm.intCheckU8DB = this.intCheckU8DB;

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

        public void btnShrinkDB_Click(object sender, EventArgs e)
        {
            try
            {               
                try
                {
                    this.checkSQLConnect(this.lbDatabase.Text);
                    this.checkConnect("MASTER");
                }
                catch(Exception E)
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
                objForm.intCareU9DBInfo = this.intCareU8DBInfo;
                objForm.intCheckU9DB = this.intCheckU8DB;

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



        private void btnDBReIndex_Click(object sender, EventArgs e)
        {
            try
            {
                Apps.frmDBReIndexandStatistics objForm = new Apps.frmDBReIndexandStatistics();
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

        private void btnMyDBReIndex_Click(object sender, EventArgs e)
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

        private void btnCutDBInfo2DB_Click(object sender, EventArgs e)
        {
            try
            {
                Apps.frmImportCustomDataInfo objForm = new Apps.frmImportCustomDataInfo();
                this.checkSQLConnect(this.lbDatabase.Text);

                objForm.strSQLConnection = strSQLConnection;
                objForm.strServer = this.lbServer.Text.Trim();
                objForm.strDatabase = this.lbDatabase.Text.Trim();
                objForm.strUser = this.tbUser.Text.Trim();
                objForm.strPassword = this.tbPW.Text.Trim();
                objForm.blnIntegratedSecurity = this.cbIntegrated.Checked;

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
        private void pbNetCard_Click(object sender, EventArgs e)
        {
            this.GetCPUNetcardNOForm();
        }

        private void pbCPU_Click(object sender, EventArgs e)
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

        private void btnBlockProcess_1_Click(object sender, EventArgs e)
        {
            this.btnBlockProcess_Click(sender, e);
        }

        private void lbDatabase_DropDown(object sender, EventArgs e)
        {
            System.Data.DataTable objDT = null;
            try
            {
                this.checkSQLConnect(this.lbDatabase.Text);
                AppClass.DBDatabases objDB = new AppClass.DBDatabases();

                objDT = objDB.getDatabaseObjects(strSQLConnection);

                this.lbDatabase.Items.Clear();
                for (int i = 0; i < objDT.Rows.Count; i++)
                {
                    this.lbDatabase.Items.Add(((string)objDT.Rows[i]["NAME"]).Trim());
                }
                //this.lbDatabase.SelectedIndex = 0;
            }
            catch (Exception E)
            {
                MessageBox.Show("获得指定 Microsoft SQL Server 实例数据库列表失败！/r/n" + E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objDT.Dispose();
                objDT = null;
            }
        }

        

    
		
		
	}

	
	

}
 