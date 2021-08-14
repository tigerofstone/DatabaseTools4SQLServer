using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Upgrade.UpGradeApp
{
	/// <summary>
	/// MakeFuncsViewsProcsForm 的摘要说明。
	/// </summary>
	public class MakeFuncsViewsProcsForm : System.Windows.Forms.Form
	{
		public string strInstance;
		public string strUserName;
		public string strPassword;
		public string strConn;

		private System.Windows.Forms.ComboBox cbInstance;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton rbView;
		private System.Windows.Forms.RadioButton rbFunction;
		private System.Windows.Forms.RadioButton rbProcedure;
		private System.Windows.Forms.Button btnMake;
		private System.Windows.Forms.TextBox tbResult;
		private System.Windows.Forms.RichTextBox rtResult;
		private System.Windows.Forms.TabControl tcControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
        private Panel plInfo;
        private Label lbInfo;
        private ProgressBar pbProcess;
        private GroupBox groupBox4;
        private TextBox tbPassword;
        private TextBox tbUsername;
        private Label label3;
        private Label label1;
        private Label label4;
        private ComboBox cbDatabase;
        private ComboBox cbFunctions;
        private ComboBox cbProcedures;
        private ComboBox cbViews;
        private Button btnLink;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MakeFuncsViewsProcsForm()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		private void MakeFuncsViewsProcsForm_Resize(object sender, System.EventArgs e)
		{
			this.tcControl.Top = 150;
			this.tcControl.Left = 2;
			this.tcControl.Width = this.Width - 10;
            this.tcControl.Height = (this.Height - 177 > 0 ? this.Height - 177 : 100);

            this.plInfo.Top = this.Height / 2 - 10;
            this.plInfo.Left = 30;
            this.plInfo.Width = this.Width - 110;
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MakeFuncsViewsProcsForm));
            this.cbInstance = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnLink = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMake = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDatabase = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbProcedures = new System.Windows.Forms.ComboBox();
            this.cbViews = new System.Windows.Forms.ComboBox();
            this.cbFunctions = new System.Windows.Forms.ComboBox();
            this.rbProcedure = new System.Windows.Forms.RadioButton();
            this.rbView = new System.Windows.Forms.RadioButton();
            this.rbFunction = new System.Windows.Forms.RadioButton();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.rtResult = new System.Windows.Forms.RichTextBox();
            this.tcControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.plInfo = new System.Windows.Forms.Panel();
            this.lbInfo = new System.Windows.Forms.Label();
            this.pbProcess = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tcControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.plInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbInstance
            // 
            this.cbInstance.Location = new System.Drawing.Point(89, 17);
            this.cbInstance.Name = "cbInstance";
            this.cbInstance.Size = new System.Drawing.Size(183, 20);
            this.cbInstance.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Location = new System.Drawing.Point(10, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "数据库实例：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.btnMake);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(9, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(822, 142);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnLink);
            this.groupBox4.Controls.Add(this.tbPassword);
            this.groupBox4.Controls.Add(this.tbUsername);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.cbInstance);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox4.Location = new System.Drawing.Point(9, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(292, 126);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            // 
            // btnLink
            // 
            this.btnLink.Location = new System.Drawing.Point(204, 95);
            this.btnLink.Name = "btnLink";
            this.btnLink.Size = new System.Drawing.Size(68, 24);
            this.btnLink.TabIndex = 11;
            this.btnLink.Text = "连接(&L)";
            this.btnLink.UseVisualStyleBackColor = true;
            this.btnLink.Click += new System.EventHandler(this.btnLink_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(89, 68);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(161, 21);
            this.tbPassword.TabIndex = 10;
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(89, 43);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(161, 21);
            this.tbUsername.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "口令：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "用户名：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnMake
            // 
            this.btnMake.Location = new System.Drawing.Point(737, 109);
            this.btnMake.Name = "btnMake";
            this.btnMake.Size = new System.Drawing.Size(75, 26);
            this.btnMake.TabIndex = 11;
            this.btnMake.Text = "生成(&M)";
            this.btnMake.Click += new System.EventHandler(this.btnMake_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbDatabase);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(305, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(426, 126);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Location = new System.Drawing.Point(10, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "数据库：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbDatabase
            // 
            this.cbDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDatabase.Location = new System.Drawing.Point(66, 12);
            this.cbDatabase.Name = "cbDatabase";
            this.cbDatabase.Size = new System.Drawing.Size(244, 20);
            this.cbDatabase.TabIndex = 8;
            this.cbDatabase.SelectedIndexChanged += new System.EventHandler(this.cbDatabase_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbProcedures);
            this.groupBox3.Controls.Add(this.cbViews);
            this.groupBox3.Controls.Add(this.cbFunctions);
            this.groupBox3.Controls.Add(this.rbProcedure);
            this.groupBox3.Controls.Add(this.rbView);
            this.groupBox3.Controls.Add(this.rbFunction);
            this.groupBox3.Location = new System.Drawing.Point(6, 32);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(411, 87);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // cbProcedures
            // 
            this.cbProcedures.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProcedures.FormattingEnabled = true;
            this.cbProcedures.Location = new System.Drawing.Point(99, 59);
            this.cbProcedures.MaxDropDownItems = 25;
            this.cbProcedures.Name = "cbProcedures";
            this.cbProcedures.Size = new System.Drawing.Size(306, 20);
            this.cbProcedures.TabIndex = 12;
            // 
            // cbViews
            // 
            this.cbViews.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbViews.FormattingEnabled = true;
            this.cbViews.Location = new System.Drawing.Point(99, 36);
            this.cbViews.MaxDropDownItems = 25;
            this.cbViews.Name = "cbViews";
            this.cbViews.Size = new System.Drawing.Size(306, 20);
            this.cbViews.TabIndex = 11;
            // 
            // cbFunctions
            // 
            this.cbFunctions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFunctions.FormattingEnabled = true;
            this.cbFunctions.Location = new System.Drawing.Point(99, 13);
            this.cbFunctions.MaxDropDownItems = 25;
            this.cbFunctions.Name = "cbFunctions";
            this.cbFunctions.Size = new System.Drawing.Size(306, 20);
            this.cbFunctions.TabIndex = 10;
            // 
            // rbProcedure
            // 
            this.rbProcedure.Location = new System.Drawing.Point(11, 59);
            this.rbProcedure.Name = "rbProcedure";
            this.rbProcedure.Size = new System.Drawing.Size(76, 24);
            this.rbProcedure.TabIndex = 9;
            this.rbProcedure.Text = "存储过程";
            // 
            // rbView
            // 
            this.rbView.Checked = true;
            this.rbView.Location = new System.Drawing.Point(11, 36);
            this.rbView.Name = "rbView";
            this.rbView.Size = new System.Drawing.Size(51, 24);
            this.rbView.TabIndex = 8;
            this.rbView.TabStop = true;
            this.rbView.Text = "视图";
            // 
            // rbFunction
            // 
            this.rbFunction.Location = new System.Drawing.Point(11, 13);
            this.rbFunction.Name = "rbFunction";
            this.rbFunction.Size = new System.Drawing.Size(86, 24);
            this.rbFunction.TabIndex = 7;
            this.rbFunction.Text = "自定义函数";
            // 
            // tbResult
            // 
            this.tbResult.BackColor = System.Drawing.Color.Azure;
            this.tbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbResult.Location = new System.Drawing.Point(0, 0);
            this.tbResult.MaxLength = 800000000;
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResult.Size = new System.Drawing.Size(449, 349);
            this.tbResult.TabIndex = 8;
            this.tbResult.WordWrap = false;
            // 
            // rtResult
            // 
            this.rtResult.BackColor = System.Drawing.Color.LightCyan;
            this.rtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtResult.Location = new System.Drawing.Point(0, 0);
            this.rtResult.Name = "rtResult";
            this.rtResult.ShowSelectionMargin = true;
            this.rtResult.Size = new System.Drawing.Size(449, 349);
            this.rtResult.TabIndex = 9;
            this.rtResult.Text = "";
            this.rtResult.WordWrap = false;
            // 
            // tcControl
            // 
            this.tcControl.Controls.Add(this.tabPage1);
            this.tcControl.Controls.Add(this.tabPage2);
            this.tcControl.Location = new System.Drawing.Point(1, 146);
            this.tcControl.Name = "tcControl";
            this.tcControl.SelectedIndex = 0;
            this.tcControl.Size = new System.Drawing.Size(457, 374);
            this.tcControl.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbResult);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(449, 349);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Text 框";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtResult);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(449, 349);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "RichText 框";
            // 
            // plInfo
            // 
            this.plInfo.BackColor = System.Drawing.SystemColors.Control;
            this.plInfo.Controls.Add(this.lbInfo);
            this.plInfo.Controls.Add(this.pbProcess);
            this.plInfo.Location = new System.Drawing.Point(472, 205);
            this.plInfo.Name = "plInfo";
            this.plInfo.Size = new System.Drawing.Size(259, 40);
            this.plInfo.TabIndex = 11;
            this.plInfo.Visible = false;
            // 
            // lbInfo
            // 
            this.lbInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbInfo.Location = new System.Drawing.Point(0, 0);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(259, 20);
            this.lbInfo.TabIndex = 9;
            this.lbInfo.Text = "提示：";
            this.lbInfo.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pbProcess
            // 
            this.pbProcess.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbProcess.Location = new System.Drawing.Point(0, 20);
            this.pbProcess.Name = "pbProcess";
            this.pbProcess.Size = new System.Drawing.Size(259, 20);
            this.pbProcess.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbProcess.TabIndex = 8;
            // 
            // MakeFuncsViewsProcsForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(888, 544);
            this.Controls.Add(this.plInfo);
            this.Controls.Add(this.tcControl);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MakeFuncsViewsProcsForm";
            this.Text = "生成自定义函数、视图、存储过程脚本";
            this.Resize += new System.EventHandler(this.MakeFuncsViewsProcsForm_Resize);
            this.Load += new System.EventHandler(this.MakeFuncsViewsProcsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tcControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.plInfo.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		
		

		//================================================================================================================
		private void MakeFuncsViewsProcsForm_Load(object sender, System.EventArgs e)
		{
             if (this.strInstance != "")
             {
                 this.cbInstance.Items.Add(this.strInstance);
                 this.cbInstance.SelectedIndex = 0;
             }
             if (this.strUserName != "") this.tbUsername.Text = this.strUserName;
             if (this.strPassword != "") this.tbPassword.Text = this.strPassword;

             MakeFuncsViewsProcsForm_Resize(sender, e);
		}

		private void btnMake_Click(object sender, System.EventArgs e)
		{
			string strObjectSrcipt = "";
            string strObjectID = "0";

			try
			{
                AppClass.DatabaseObjects objDO = null ;
                System.Data.SqlClient.SqlConnection objConn;

                if (this.rbFunction.Checked) 
                {
                    objDO = new AppClass.DBFunctions();
                    strObjectID = ((AppClass.SysItem)this.cbFunctions.Items[this.cbFunctions.SelectedIndex]).ID;                                    
                }
                if (this.rbView.Checked) 
                {
                    objDO = new AppClass.DBViews();
                    strObjectID = ((AppClass.SysItem)this.cbViews.Items[this.cbViews.SelectedIndex]).ID;
                }
                if (this.rbProcedure.Checked) 
                {
                    objDO = new AppClass.DBProcedures();
                    strObjectID = ((AppClass.SysItem)this.cbProcedures.Items[this.cbProcedures.SelectedIndex]).ID;
                }

                objConn = AppClass.PublicDBCls.getSQLConnect(strInstance, this.cbDatabase.Text.Trim(), strUserName, strPassword);
                strObjectSrcipt = objDO.getObjectScript(objConn, strObjectID);
			}
			catch(System.Exception Exp)
			{
				MessageBox.Show(Exp.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			
			strObjectSrcipt = strObjectSrcipt.Replace("\n", "").Replace("\r", "\r\n");
			this.tbResult.Text = strObjectSrcipt;			
			this.rtResult.Text = strObjectSrcipt;
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            try
            {
                this.strInstance = this.cbInstance.Text.Trim();
                this.strUserName = this.tbUsername.Text.Trim();
                this.strPassword = this.tbPassword.Text;

                setDBObjectsShow(this.cbDatabase, "Master");
                setDBObjectsShow(this.cbFunctions, this.cbDatabase.Text.Trim());
                setDBObjectsShow(this.cbViews, this.cbDatabase.Text.Trim());
                setDBObjectsShow(this.cbProcedures, this.cbDatabase.Text.Trim());

            }
            catch (System.Exception E)
            {
                MessageBox.Show(E.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDBObjectsShow(this.cbFunctions, this.cbDatabase.Text.Trim());
            setDBObjectsShow(this.cbViews, this.cbDatabase.Text.Trim());
            setDBObjectsShow(this.cbProcedures, this.cbDatabase.Text.Trim());
        }





        //公共操作函数
        private void setDBObjectsShow(ComboBox objCBox, string strDatabase)
        {
            try
            {
                System.Data.DataTable objDT = this.getDatabases(this.strInstance, strDatabase, this.strUserName, this.strPassword, objCBox);
                AppClass.SysItem sctItem;

                objCBox.Items.Clear();
                objCBox.Text = "";
                for (int i = 0; i < objDT.Rows.Count; i++)
                {
                    sctItem = new AppClass.SysItem();

                    sctItem.Name = (string)objDT.Rows[i]["name"];
                    sctItem.ID = (objDT.Rows[i]["id"]).ToString();

                    objCBox.Items.Add(sctItem);
                }
                //if (objCBox != this.cbDatabase)
                //{
                //    sctItem = new AppClass.SysItem();

                //    sctItem.Name = "-- 全部 --";
                //    sctItem.ID = "0";

                //    objCBox.Items.Add(sctItem);
                //}

                objCBox.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        
        private DataTable getDatabases(string strInstance, string strDatabase, string strUsername, string strPassword, ComboBox objCBox)
        {
            try
            {
                System.Data.DataTable objDT = null;
                AppClass.DatabaseObjects objPDB = new AppClass.DBDatabases();
                System.Data.SqlClient.SqlConnection objConn;

                if(objCBox == this.cbDatabase)
                {
                    objPDB = new AppClass.DBDatabases();
                }
                else if(objCBox == this.cbFunctions)
                {
                    objPDB = new AppClass.DBFunctions();
                }
                else if(objCBox == this.cbViews)
                {
                    objPDB = new AppClass.DBViews();
                }
                else if (objCBox == this.cbProcedures)
                {
                    objPDB = new AppClass.DBProcedures();
                }
                else 
                {
                    throw(new Exception("内部错误：传入的数据库对象类型不正确。"));
                }

                objConn = AppClass.PublicDBCls.getSQLConnect(strInstance, strDatabase, strUsername, strPassword);

                objDT = objPDB.getDatabaseObjects(objConn);

                return objDT;
            }
            catch (Exception e)
            {
                throw(e);
            }           
        }

        

        
        

        
	}
}

















