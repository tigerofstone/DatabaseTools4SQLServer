using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Upgrade.UpGradeApp
{
	/// <summary>
	/// UpdateAllTestServerForm 的摘要说明。
	/// </summary>
	public class UpdateAllTestServerForm : System.Windows.Forms.Form
	{
		
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbSrever;
		private System.Windows.Forms.CheckBox cbModel;
		private System.Windows.Forms.CheckBox cbEntData;
		private System.Windows.Forms.CheckBox cbTemplate;
		private System.Windows.Forms.TextBox tbScript;
		private System.Windows.Forms.ListBox lbServers;
		private System.Windows.Forms.TextBox tbUser;
		private System.Windows.Forms.TextBox tbPW;
		private System.Windows.Forms.TabControl tcScript;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnDo;
		private System.Windows.Forms.Button btnDeleteAll;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.TextBox tbError;
		private System.Windows.Forms.ProgressBar pbProcess;
		private System.Windows.Forms.Panel plInfo;
		private System.Windows.Forms.Label lbInfo;
        private CheckBox cbU8Data;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public UpdateAllTestServerForm()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateAllTestServerForm));
            this.tbSrever = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbModel = new System.Windows.Forms.CheckBox();
            this.cbEntData = new System.Windows.Forms.CheckBox();
            this.cbTemplate = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbU8Data = new System.Windows.Forms.CheckBox();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnDo = new System.Windows.Forms.Button();
            this.tbPW = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbServers = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tbScript = new System.Windows.Forms.TextBox();
            this.tcScript = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbError = new System.Windows.Forms.TextBox();
            this.pbProcess = new System.Windows.Forms.ProgressBar();
            this.plInfo = new System.Windows.Forms.Panel();
            this.lbInfo = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tcScript.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.plInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSrever
            // 
            this.tbSrever.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSrever.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSrever.Location = new System.Drawing.Point(120, 20);
            this.tbSrever.Name = "tbSrever";
            this.tbSrever.Size = new System.Drawing.Size(223, 21);
            this.tbSrever.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "服务器实例名称：";
            // 
            // cbModel
            // 
            this.cbModel.Location = new System.Drawing.Point(435, 104);
            this.cbModel.Name = "cbModel";
            this.cbModel.Size = new System.Drawing.Size(205, 21);
            this.cbModel.TabIndex = 2;
            this.cbModel.Text = "分销-前台业务库(Model、AppX)";
            // 
            // cbEntData
            // 
            this.cbEntData.Location = new System.Drawing.Point(435, 125);
            this.cbEntData.Name = "cbEntData";
            this.cbEntData.Size = new System.Drawing.Size(183, 21);
            this.cbEntData.TabIndex = 3;
            this.cbEntData.Text = "分销-后台数据库(EntData)";
            // 
            // cbTemplate
            // 
            this.cbTemplate.Location = new System.Drawing.Point(435, 146);
            this.cbTemplate.Name = "cbTemplate";
            this.cbTemplate.Size = new System.Drawing.Size(183, 21);
            this.cbTemplate.TabIndex = 4;
            this.cbTemplate.Text = "分销-模板库(Template)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbU8Data);
            this.groupBox1.Controls.Add(this.btnDeleteAll);
            this.groupBox1.Controls.Add(this.btnDo);
            this.groupBox1.Controls.Add(this.tbPW);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbUser);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lbServers);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.cbTemplate);
            this.groupBox1.Controls.Add(this.tbSrever);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbModel);
            this.groupBox1.Controls.Add(this.cbEntData);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(768, 190);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // cbU8Data
            // 
            this.cbU8Data.Checked = true;
            this.cbU8Data.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbU8Data.Location = new System.Drawing.Point(435, 83);
            this.cbU8Data.Name = "cbU8Data";
            this.cbU8Data.Size = new System.Drawing.Size(194, 21);
            this.cbU8Data.TabIndex = 16;
            this.cbU8Data.Text = "U8-业务数据库(UFDATA)";
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(347, 112);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(65, 24);
            this.btnDeleteAll.TabIndex = 15;
            this.btnDeleteAll.Text = "全删(&R)";
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnDo
            // 
            this.btnDo.Location = new System.Drawing.Point(661, 144);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(86, 29);
            this.btnDo.TabIndex = 14;
            this.btnDo.Text = "执行(&D)";
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // tbPW
            // 
            this.tbPW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPW.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPW.Location = new System.Drawing.Point(492, 48);
            this.tbPW.Name = "tbPW";
            this.tbPW.PasswordChar = '*';
            this.tbPW.Size = new System.Drawing.Size(126, 21);
            this.tbPW.TabIndex = 13;
            this.tbPW.Text = "sample";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(435, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "口令：";
            // 
            // tbUser
            // 
            this.tbUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbUser.Location = new System.Drawing.Point(492, 19);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(126, 21);
            this.tbUser.TabIndex = 11;
            this.tbUser.Text = "sample";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(435, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "用户名：";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(347, 80);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(65, 24);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "删除(&D)";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 30);
            this.label2.TabIndex = 8;
            this.label2.Text = "要更新的服务器实例列表：";
            // 
            // lbServers
            // 
            this.lbServers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbServers.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbServers.HorizontalScrollbar = true;
            this.lbServers.ItemHeight = 12;
            this.lbServers.Location = new System.Drawing.Point(94, 47);
            this.lbServers.Name = "lbServers";
            this.lbServers.Size = new System.Drawing.Size(249, 122);
            this.lbServers.TabIndex = 7;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(346, 19);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(65, 24);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "添加(&A)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tbScript
            // 
            this.tbScript.BackColor = System.Drawing.Color.AliceBlue;
            this.tbScript.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbScript.Location = new System.Drawing.Point(0, 0);
            this.tbScript.MaxLength = 800000000;
            this.tbScript.Multiline = true;
            this.tbScript.Name = "tbScript";
            this.tbScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbScript.Size = new System.Drawing.Size(694, 345);
            this.tbScript.TabIndex = 6;
            this.tbScript.WordWrap = false;
            // 
            // tcScript
            // 
            this.tcScript.Controls.Add(this.tabPage1);
            this.tcScript.Controls.Add(this.tabPage2);
            this.tcScript.Location = new System.Drawing.Point(-1, 197);
            this.tcScript.Name = "tcScript";
            this.tcScript.SelectedIndex = 0;
            this.tcScript.Size = new System.Drawing.Size(702, 370);
            this.tcScript.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbScript);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(694, 345);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "要执行脚本";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbError);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(694, 345);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "执行结果";
            // 
            // tbError
            // 
            this.tbError.BackColor = System.Drawing.Color.LightBlue;
            this.tbError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbError.Location = new System.Drawing.Point(0, 0);
            this.tbError.Multiline = true;
            this.tbError.Name = "tbError";
            this.tbError.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbError.Size = new System.Drawing.Size(694, 345);
            this.tbError.TabIndex = 0;
            this.tbError.WordWrap = false;
            // 
            // pbProcess
            // 
            this.pbProcess.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbProcess.Location = new System.Drawing.Point(0, 20);
            this.pbProcess.Name = "pbProcess";
            this.pbProcess.Size = new System.Drawing.Size(734, 20);
            this.pbProcess.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbProcess.TabIndex = 8;
            // 
            // plInfo
            // 
            this.plInfo.BackColor = System.Drawing.SystemColors.Control;
            this.plInfo.Controls.Add(this.lbInfo);
            this.plInfo.Controls.Add(this.pbProcess);
            this.plInfo.Location = new System.Drawing.Point(16, 319);
            this.plInfo.Name = "plInfo";
            this.plInfo.Size = new System.Drawing.Size(734, 40);
            this.plInfo.TabIndex = 9;
            this.plInfo.Visible = false;
            // 
            // lbInfo
            // 
            this.lbInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbInfo.Location = new System.Drawing.Point(0, 0);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(734, 20);
            this.lbInfo.TabIndex = 9;
            this.lbInfo.Text = "提示：";
            this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UpdateAllTestServerForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(874, 566);
            this.Controls.Add(this.plInfo);
            this.Controls.Add(this.tcScript);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpdateAllTestServerForm";
            this.Text = "更新所有测试服务器";
            this.Load += new System.EventHandler(this.UpdateAllTestServerForm_Load);
            this.SizeChanged += new System.EventHandler(this.UpdateAllTestServerForm_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tcScript.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.plInfo.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void UpdateAllTestServerForm_Load(object sender, System.EventArgs e)
		{
			UpdateAllTestServerForm_SizeChanged(sender, e);
		}

		private void UpdateAllTestServerForm_SizeChanged(object sender, System.EventArgs e)
		{
			this.tcScript.Top = 200;
			this.tcScript.Left = -1;
			this.tcScript.Width = this.Width - 2;
			this.tcScript.Height = (this.Height - 223 > 0 ? this.Height - 223 : 10);

            this.plInfo.Top = this.Height / 2 - 10;
            this.plInfo.Left = 30;
            this.plInfo.Width = this.Width - 110;
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if(this.tbSrever.Text.Trim() != "" && ValidateListServers(this.tbSrever.Text.Trim()))
			{
				this.lbServers.Items.Add(this.tbSrever.Text.Trim());
			}	
		}
		//验证有没有此服务器
		private bool ValidateListServers(string strItem)
		{
			int i;
			for(i = 0; i < this.lbServers.Items.Count ; i++)
			{
				if(this.lbServers.Items[i].ToString().Trim() == strItem) break;
			}

			if(i == this.lbServers.Items.Count) 
				return true;
			else
				return false;
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			if(this.lbServers.SelectedIndex >= 0)
			{
				this.lbServers.Items.Remove(this.lbServers.Items[this.lbServers.SelectedIndex]);				
			}
		}
		
		private void btnDeleteAll_Click(object sender, System.EventArgs e)
		{
			this.lbServers.Items.Clear();
		}

		private void btnDo_Click(object sender, System.EventArgs e)
		{
			if(this.tbScript.Text.Trim() == "")
			{
				MessageBox.Show("没有要执行的数据库脚本。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information); 
				return;
			}

            try
            {
                System.Collections.ArrayList objSvrList = new System.Collections.ArrayList();
                System.Collections.ArrayList objDBList = new System.Collections.ArrayList();

                Upgrade.AppClass.ExcuteSQLScriptCls objExecS;

                for (int i = 0; i < this.lbServers.Items.Count; i++)
                {
                    objSvrList.Add(this.lbServers.Items[i].ToString().Trim());
                }
                if (this.cbU8Data.Checked) objDBList.Add("U8Data");
                if (this.cbModel.Checked) objDBList.Add("Model & AppX");
                if (this.cbEntData.Checked) objDBList.Add("U8DRP_EntData");
                if (this.cbTemplate.Checked) objDBList.Add("U8DRP_Template");

                this.plInfo.Visible = true;
                this.tbError.Text = "";

                objExecS = new AppClass.ExcuteSQLScriptCls(objSvrList, objDBList, this.tbUser.Text, this.tbPW.Text, this.tbScript.Text);
                objExecS.objLabel = this.lbInfo;
                objExecS.objProcess = this.pbProcess;
                objExecS.ExcuteSQLScript();

                MessageBox.Show("执行成功。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(AppClass.MyDBUpdateException E)
            {
                MessageBox.Show("执行 SQL 语句出现错误，请查正后再运行。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tbError.Text = E.strErrMessage;
                this.tcScript.TabPages[1].Select();
            }
            catch(System.Exception E)
            {
                MessageBox.Show(E.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.plInfo.Visible = false;
            }
		}

	}
}
