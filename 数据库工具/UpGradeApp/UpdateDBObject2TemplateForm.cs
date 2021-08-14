using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Upgrade.UpGradeApp
{
	/// <summary>
	/// UpdateDBObject2TemplateForm 的摘要说明。
	/// </summary>
	public class UpdateDBObject2TemplateForm : System.Windows.Forms.Form
	{
		public string strServer;
		public string strUser;
		public string strPW;
		public string strVer;
		public string strTemplateConn;


		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Label lbInfo;
		private System.Windows.Forms.Label lbPrInfo;
		private System.Windows.Forms.ProgressBar pbSingleObject;
		private System.Windows.Forms.ProgressBar pbTotalPress;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.TextBox tbModelView;
		private System.Windows.Forms.TextBox tbModelProc;
		private System.Windows.Forms.TextBox tbEntDataFunc;
		private System.Windows.Forms.TextBox tbEntDataView;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox tbModelFunc;
		private System.Windows.Forms.TextBox tbEntDataProc;
		private System.Windows.Forms.Button btnUpdate;

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UpdateDBObject2TemplateForm()
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateDBObject2TemplateForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbTotalPress = new System.Windows.Forms.ProgressBar();
            this.pbSingleObject = new System.Windows.Forms.ProgressBar();
            this.lbPrInfo = new System.Windows.Forms.Label();
            this.lbInfo = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbModelFunc = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbModelView = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbModelProc = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tbEntDataFunc = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tbEntDataView = new System.Windows.Forms.TextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tbEntDataProc = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pbTotalPress);
            this.groupBox1.Controls.Add(this.pbSingleObject);
            this.groupBox1.Controls.Add(this.lbPrInfo);
            this.groupBox1.Controls.Add(this.lbInfo);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Location = new System.Drawing.Point(10, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(828, 142);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // pbTotalPress
            // 
            this.pbTotalPress.Location = new System.Drawing.Point(9, 81);
            this.pbTotalPress.Maximum = 6;
            this.pbTotalPress.Name = "pbTotalPress";
            this.pbTotalPress.Size = new System.Drawing.Size(810, 16);
            this.pbTotalPress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbTotalPress.TabIndex = 3;
            // 
            // pbSingleObject
            // 
            this.pbSingleObject.Location = new System.Drawing.Point(9, 37);
            this.pbSingleObject.Name = "pbSingleObject";
            this.pbSingleObject.Size = new System.Drawing.Size(809, 16);
            this.pbSingleObject.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbSingleObject.TabIndex = 2;
            // 
            // lbPrInfo
            // 
            this.lbPrInfo.BackColor = System.Drawing.SystemColors.Control;
            this.lbPrInfo.Location = new System.Drawing.Point(14, 62);
            this.lbPrInfo.Name = "lbPrInfo";
            this.lbPrInfo.Size = new System.Drawing.Size(400, 17);
            this.lbPrInfo.TabIndex = 1;
            this.lbPrInfo.Text = "转换数据库";
            this.lbPrInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbInfo
            // 
            this.lbInfo.BackColor = System.Drawing.SystemColors.Control;
            this.lbInfo.ForeColor = System.Drawing.Color.Navy;
            this.lbInfo.Location = new System.Drawing.Point(14, 18);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(403, 18);
            this.lbInfo.TabIndex = 0;
            this.lbInfo.Text = "转换对象";
            this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(644, 106);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(93, 30);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "转换保存(&S)";
            this.btnUpdate.Click += new System.EventHandler(this.tnUpdate_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(743, 106);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "退出(&E)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(-2, 152);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(852, 424);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbModelFunc);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(844, 399);
            this.tabPage1.TabIndex = 6;
            this.tabPage1.Text = "MODEL.Function";
            // 
            // tbModelFunc
            // 
            this.tbModelFunc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbModelFunc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbModelFunc.Location = new System.Drawing.Point(0, 0);
            this.tbModelFunc.MaxLength = 800000000;
            this.tbModelFunc.Multiline = true;
            this.tbModelFunc.Name = "tbModelFunc";
            this.tbModelFunc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbModelFunc.Size = new System.Drawing.Size(844, 399);
            this.tbModelFunc.TabIndex = 1;
            this.tbModelFunc.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbModelView);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(844, 399);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "MODEL.View";
            // 
            // tbModelView
            // 
            this.tbModelView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbModelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbModelView.Location = new System.Drawing.Point(0, 0);
            this.tbModelView.MaxLength = 800000000;
            this.tbModelView.Multiline = true;
            this.tbModelView.Name = "tbModelView";
            this.tbModelView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbModelView.Size = new System.Drawing.Size(844, 399);
            this.tbModelView.TabIndex = 0;
            this.tbModelView.WordWrap = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tbModelProc);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(844, 399);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "MODEL.Procedure";
            // 
            // tbModelProc
            // 
            this.tbModelProc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbModelProc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbModelProc.Location = new System.Drawing.Point(0, 0);
            this.tbModelProc.MaxLength = 800000000;
            this.tbModelProc.Multiline = true;
            this.tbModelProc.Name = "tbModelProc";
            this.tbModelProc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbModelProc.Size = new System.Drawing.Size(844, 399);
            this.tbModelProc.TabIndex = 1;
            this.tbModelProc.WordWrap = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tbEntDataFunc);
            this.tabPage4.Location = new System.Drawing.Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(844, 399);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "EntData.Function";
            // 
            // tbEntDataFunc
            // 
            this.tbEntDataFunc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbEntDataFunc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbEntDataFunc.Location = new System.Drawing.Point(0, 0);
            this.tbEntDataFunc.MaxLength = 800000000;
            this.tbEntDataFunc.Multiline = true;
            this.tbEntDataFunc.Name = "tbEntDataFunc";
            this.tbEntDataFunc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbEntDataFunc.Size = new System.Drawing.Size(844, 399);
            this.tbEntDataFunc.TabIndex = 1;
            this.tbEntDataFunc.WordWrap = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.tbEntDataView);
            this.tabPage5.Location = new System.Drawing.Point(4, 21);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(844, 399);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "EntData.View";
            // 
            // tbEntDataView
            // 
            this.tbEntDataView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbEntDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbEntDataView.Location = new System.Drawing.Point(0, 0);
            this.tbEntDataView.MaxLength = 800000000;
            this.tbEntDataView.Multiline = true;
            this.tbEntDataView.Name = "tbEntDataView";
            this.tbEntDataView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbEntDataView.Size = new System.Drawing.Size(844, 399);
            this.tbEntDataView.TabIndex = 1;
            this.tbEntDataView.WordWrap = false;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.tbEntDataProc);
            this.tabPage6.Location = new System.Drawing.Point(4, 21);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(844, 399);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "EntData.Procedure";
            // 
            // tbEntDataProc
            // 
            this.tbEntDataProc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbEntDataProc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbEntDataProc.Location = new System.Drawing.Point(0, 0);
            this.tbEntDataProc.MaxLength = 800000000;
            this.tbEntDataProc.Multiline = true;
            this.tbEntDataProc.Name = "tbEntDataProc";
            this.tbEntDataProc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbEntDataProc.Size = new System.Drawing.Size(844, 399);
            this.tbEntDataProc.TabIndex = 2;
            this.tbEntDataProc.WordWrap = false;
            // 
            // UpdateDBObject2TemplateForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(847, 574);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpdateDBObject2TemplateForm";
            this.Text = "导出自定义函数、视图、存储过程的升级脚本保存至模板库";
            this.Resize += new System.EventHandler(this.UpdateDBObject2TemplateForm_Resize);
            this.Load += new System.EventHandler(this.UpdateDBObject2TemplateForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void tnUpdate_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.btnUpdate.Enabled = false;
                this.btnExit.Enabled = false;

				string strObjectSrcipt = "";
				Upgrade.AppClass.MakeSQLScriptCls objMakeScript = new Upgrade.AppClass.MakeSQLScriptCls();
				System.Data.OleDb.OleDbConnection objSQLConnect = null;
				System.Data.OleDb.OleDbConnection objSQLConnTemplate = null;
				System.Data.DataTable objDT = new System.Data.DataTable();
				Upgrade.AppClass.PublicDBCls objPDBC = new Upgrade.AppClass.PublicDBCls();
				Upgrade.AppClass.MakeSQLScriptCls objSQLMake = new Upgrade.AppClass.MakeSQLScriptCls();

				objPDBC.strServer =strServer;
				objPDBC.strUser = strUser;
				objPDBC.strPW = strPW;
				objPDBC.strVer = strVer;
								
				objSQLConnTemplate = objPDBC.getConnect("U8DRP_Template", 1);
				
				this.pbTotalPress.Value = 0;
				//============= MODEL 库数据库对象 ====================================================================
				//自定义函数				
				this.lbPrInfo.Text = "正在导出 MODEL 数据库自定义函数 ......";
				this.lbPrInfo.Refresh();
				objSQLConnect = objPDBC.getConnect("MODEL", 1);
				objDT = objMakeScript.getDatabaseObjects(objSQLConnect, "MODEL", "1");			

				strObjectSrcipt = getObjectsScript(objSQLConnect, objDT, "1", objSQLMake);
				this.lbInfo.Text = "正在保存：MODEL 数据库自定义函数脚本";
				this.lbInfo.Refresh();
				objSQLMake.setObjectScriptUpdate(objSQLConnTemplate, "APP", "1", strObjectSrcipt);

				this.pbTotalPress.Value = this.pbTotalPress.Value + 1;
				this.pbTotalPress.Refresh();
				this.tbModelFunc.Text = strObjectSrcipt;
				this.tabControl1.SelectedTab = this.tabPage1;
				this.tbModelFunc.Refresh();

				
				//视图
				this.lbPrInfo.Text = "正在导出 MODEL 数据库视图 ......";
				this.lbPrInfo.Refresh();
				objDT.Clear();
				objDT = objMakeScript.getDatabaseObjects(objSQLConnect, "MODEL", "2");

				strObjectSrcipt = getObjectsScript(objSQLConnect, objDT, "2", objSQLMake);
				this.lbInfo.Text = "正在保存：MODEL 数据库视图脚本";
				this.lbInfo.Refresh();
				objSQLMake.setObjectScriptUpdate(objSQLConnTemplate, "APP", "2", strObjectSrcipt);
				
                this.pbTotalPress.Value = this.pbTotalPress.Value + 1;
				this.pbTotalPress.Refresh();
				this.tbModelView.Text = strObjectSrcipt;
				this.tabControl1.SelectedTab = this.tabPage2;
				this.tbModelView.Refresh();


				//存储过程
				this.lbPrInfo.Text = "正在导出 MODEL 数据库存储过程 ......";
				this.lbPrInfo.Refresh();
				objDT.Clear();
				objDT = objMakeScript.getDatabaseObjects(objSQLConnect, "MODEL", "3");

				strObjectSrcipt = getObjectsScript(objSQLConnect, objDT, "3", objSQLMake);
				this.lbInfo.Text = "正在保存：MODEL 数据库存储过程脚本";
				this.lbInfo.Refresh();
				objSQLMake.setObjectScriptUpdate(objSQLConnTemplate, "APP", "3", strObjectSrcipt);
				
				this.pbTotalPress.Value = this.pbTotalPress.Value + 1;
				this.pbTotalPress.Refresh();
				this.tbModelProc.Text = strObjectSrcipt;
				this.tabControl1.SelectedTab = this.tabPage3;
				this.tbModelProc.Refresh();


				//============= U8DRP_EntData 库数据库对象 ====================================================================
				objSQLConnect = objPDBC.getConnect("U8DRP_EntData_T", 1);
				
				//自定义函数			
				this.lbPrInfo.Text = "正在导出 U8DRP_EntData 数据库自定义函数 ......";
				this.lbPrInfo.Refresh();
				objDT.Clear();
				objDT = objMakeScript.getDatabaseObjects(objSQLConnect, "EntData", "1");
			
				strObjectSrcipt = getObjectsScript(objSQLConnect, objDT, "1", objSQLMake);
				this.lbInfo.Text = "正在保存：U8DRP_EntData 数据库自定义函数 ";
				this.lbInfo.Refresh();
				objSQLMake.setObjectScriptUpdate(objSQLConnTemplate, "U8DRP_EntData", "1", strObjectSrcipt);
				
				this.pbTotalPress.Value = this.pbTotalPress.Value + 1;
				this.pbTotalPress.Refresh();
				this.tbEntDataFunc.Text = strObjectSrcipt;
				this.tabControl1.SelectedTab = this.tabPage4;
				this.tbEntDataFunc.Refresh();


				//视图
				this.lbPrInfo.Text = "正在导出 U8DRP_EntData 数据库视图 ......";
				this.lbPrInfo.Refresh();
				objDT.Clear();
				objDT = objMakeScript.getDatabaseObjects(objSQLConnect, "EntData", "2");

				strObjectSrcipt = getObjectsScript(objSQLConnect, objDT, "2", objSQLMake);
				this.lbInfo.Text = "正在保存：U8DRP_EntData 数据库视图";
				this.lbInfo.Refresh();
				objSQLMake.setObjectScriptUpdate(objSQLConnTemplate, "U8DRP_EntData", "2", strObjectSrcipt);
				
				this.pbTotalPress.Value = this.pbTotalPress.Value + 1;
				this.pbTotalPress.Refresh();
				this.tbEntDataView.Text = strObjectSrcipt;
				this.tabControl1.SelectedTab = this.tabPage5;
				this.tbEntDataView.Refresh();

				//存储过程
				this.lbPrInfo.Text = "正在导出 U8DRP_EntData 数据库存储过程 ......";
				this.lbPrInfo.Refresh();
				objDT.Clear();
				objDT = objMakeScript.getDatabaseObjects(objSQLConnect, "EntData", "3");

				strObjectSrcipt = getObjectsScript(objSQLConnect, objDT, "3", objSQLMake);
				this.lbInfo.Text = "正在保存：U8DRP_EntData 数据库存储过程";
				this.lbInfo.Refresh();
				objSQLMake.setObjectScriptUpdate(objSQLConnTemplate, "U8DRP_EntData", "3", strObjectSrcipt);
				
				this.pbTotalPress.Value = this.pbTotalPress.Value + 1;
				this.pbTotalPress.Refresh();
				this.tbEntDataProc.Text = strObjectSrcipt;
				this.tabControl1.SelectedTab = this.tabPage6;
				this.tbEntDataProc.Refresh();

				System.Threading.Thread.Sleep(300);
				MessageBox.Show("数据库对象升级脚本导出成功。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information); 
				
                this.btnUpdate.Enabled = true;
                this.btnExit.Enabled = true;
			}
			catch(System.Exception exp)
			{
				MessageBox.Show(exp.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error); 

				this.btnUpdate.Enabled = true;
                this.btnExit.Enabled = true;
			}
		
		}

		//====================================================================================================================================================================
		private string getObjectsScript(System.Data.OleDb.OleDbConnection objSQLConnect, System.Data.DataTable objDT, string strObjectType,Upgrade.AppClass.MakeSQLScriptCls objSQLMake)
		{
			try
			{
				string strScript = "";
                System.Text.StringBuilder objSB = new System.Text.StringBuilder();
				int i;

                objSB.Append("");
				if(objDT.Rows.Count != 0)
				{
					this.pbSingleObject.Maximum = objDT.Rows.Count;
					this.pbSingleObject.Value = 0;
					for(i = 0; i < objDT.Rows.Count; i++)
					{						
						this.lbInfo.Text = "正在导出：" +  objDT.Rows[i]["name"].ToString().Trim();
						this.lbInfo.Refresh();

						//strScript = strScript + objSQLMake.getObjectScript(objSQLConnect, objDT.Rows[i]["name"].ToString().Trim(), objDT.Rows[i]["id"].ToString().Trim(), strObjectType);
                        strScript = objSQLMake.getObjectScript(objSQLConnect, objDT.Rows[i]["name"].ToString().Trim(), objDT.Rows[i]["id"].ToString().Trim(), strObjectType);
                        objSB.Append(strScript);

						this.pbSingleObject.Value = i + 1;
						this.pbSingleObject.Refresh();
					}
				}
                return objSB.ToString();
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}

		private void UpdateDBObject2TemplateForm_Resize(object sender, System.EventArgs e)
		{
			this.tabControl1.Top = 150;
			this.tabControl1.Left = -1;
			this.tabControl1.Width = this.Width - 2;
			this.tabControl1.Height = (this.Height - 173 > 0 ? this.Height - 173 : 173);
		}

        private void UpdateDBObject2TemplateForm_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + "    当前数据库实例：" + AppClass.PublicStaticCls.strServer;

            System.Data.OleDb.OleDbConnection objSQLConnTemplate = new System.Data.OleDb.OleDbConnection();
            Upgrade.AppClass.PublicDBCls objPDBC = new Upgrade.AppClass.PublicDBCls();
            System.Data.DataTable objDT = new System.Data.DataTable();
            System.Data.OleDb.OleDbDataAdapter objDA = new System.Data.OleDb.OleDbDataAdapter();
            System.Data.OleDb.OleDbCommand objComm = null;
            try
            {
                objPDBC.strServer = this.strServer;
                objPDBC.strUser = this.strUser;
                objPDBC.strPW = this.strPW;
                objSQLConnTemplate = objPDBC.getConnect("U8DRP_Template", 1);
                objComm = objSQLConnTemplate.CreateCommand();

                objComm.CommandText = "SELECT MAX(fintVerNumber) FROM VerSet";
                objComm.ExecuteNonQuery();
                objDA.SelectCommand = objComm;
                objDA.Fill(objDT);

                if (objDT.Rows.Count > 0)
                {
                    this.strVer = ((int)(objDT.Rows[0][0])).ToString();
                }
            }
            catch(Exception E)
            {
                MessageBox.Show("发生错误：\r\n" + E.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (objSQLConnTemplate.State == System.Data.ConnectionState.Open)
                {
                    objSQLConnTemplate.Close();
                }
                objSQLConnTemplate.Dispose();
                objSQLConnTemplate = null;
                objPDBC = null;
                objDT = null;
                objDA = null;
                objComm = null;
            }
        }
		
	}
}
