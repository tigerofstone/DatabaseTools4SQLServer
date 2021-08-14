using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Upgrade.UpGradeApp
{
	/// <summary>
	/// EditUpGradeSQLForm 的摘要说明。
	/// </summary>
	public class EditUpgradeSQLForm : System.Windows.Forms.Form
	{
		public string strVersion = "";
		public string strDataBase = "";
		public bool blnDefault = true;
        public int intDTIndex = -1;
        public System.Data.DataTable objDT = null;

		private string strConnection = "";
		private string strID = "";
		private bool blnUpdate = false;

		private System.Windows.Forms.TextBox editfchrID;
		private System.Windows.Forms.TextBox editfintVersion;
		private System.Windows.Forms.TextBox editfchrDataBase;
		private System.Windows.Forms.TextBox editfintOrder;
		private System.Windows.Forms.TextBox editfchrPrompt;
		private System.Windows.Forms.TextBox editfchrSQLText;
		private System.Windows.Forms.TextBox editfchrNote;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.Label lblfintVersion;
		private System.Windows.Forms.Label lblfchrDataBase;
		private System.Windows.Forms.Label lblfintOrder;
		private System.Windows.Forms.Label lblfchrPrompt;
		private System.Windows.Forms.Label lblfchrNote;
		private System.Windows.Forms.Label lblfchrSQLText;
        private GroupBox groupBox1;
        private Label label1;
        private PictureBox pbEdit;
        private Button btnPre;
        private Button btnNext;
        private Button btnNew;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label2;
        private ComboBox cbSQLRunType;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public EditUpgradeSQLForm(string strConn, string strMainID,Upgrade.UpGradeApp.ListQueryForm.Version objVer, string strDB)
		{
			InitializeComponent();
			
			strID = "{" + strMainID + "}";
			strConnection = strConn;
			strVersion = objVer.intVerNumber.ToString().Trim();
            blnDefault = objVer.blnDefault;
			strDataBase = strDB;
			this.GetData(strConn, strMainID);

            //if (!blnUpdate)
            //{
            //    this.btnPre.Enabled = false;
            //    this.btnNext.Enabled = false;
            //}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditUpgradeSQLForm));
            this.editfchrID = new System.Windows.Forms.TextBox();
            this.editfintVersion = new System.Windows.Forms.TextBox();
            this.editfchrDataBase = new System.Windows.Forms.TextBox();
            this.editfintOrder = new System.Windows.Forms.TextBox();
            this.editfchrPrompt = new System.Windows.Forms.TextBox();
            this.editfchrSQLText = new System.Windows.Forms.TextBox();
            this.editfchrNote = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblfintVersion = new System.Windows.Forms.Label();
            this.lblfchrDataBase = new System.Windows.Forms.Label();
            this.lblfintOrder = new System.Windows.Forms.Label();
            this.lblfchrPrompt = new System.Windows.Forms.Label();
            this.lblfchrNote = new System.Windows.Forms.Label();
            this.lblfchrSQLText = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSQLRunType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbEdit = new System.Windows.Forms.PictureBox();
            this.btnPre = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEdit)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // editfchrID
            // 
            this.editfchrID.Location = new System.Drawing.Point(339, 17);
            this.editfchrID.Name = "editfchrID";
            this.editfchrID.ReadOnly = true;
            this.editfchrID.Size = new System.Drawing.Size(275, 21);
            this.editfchrID.TabIndex = 47;
            this.editfchrID.WordWrap = false;
            // 
            // editfintVersion
            // 
            this.editfintVersion.Location = new System.Drawing.Point(93, 17);
            this.editfintVersion.Name = "editfintVersion";
            this.editfintVersion.Size = new System.Drawing.Size(153, 21);
            this.editfintVersion.TabIndex = 30;
            this.editfintVersion.WordWrap = false;
            // 
            // editfchrDataBase
            // 
            this.editfchrDataBase.Location = new System.Drawing.Point(93, 44);
            this.editfchrDataBase.Name = "editfchrDataBase";
            this.editfchrDataBase.Size = new System.Drawing.Size(153, 21);
            this.editfchrDataBase.TabIndex = 31;
            this.editfchrDataBase.WordWrap = false;
            // 
            // editfintOrder
            // 
            this.editfintOrder.Location = new System.Drawing.Point(339, 44);
            this.editfintOrder.Name = "editfintOrder";
            this.editfintOrder.Size = new System.Drawing.Size(169, 21);
            this.editfintOrder.TabIndex = 32;
            this.editfintOrder.WordWrap = false;
            // 
            // editfchrPrompt
            // 
            this.editfchrPrompt.Location = new System.Drawing.Point(93, 71);
            this.editfchrPrompt.Name = "editfchrPrompt";
            this.editfchrPrompt.Size = new System.Drawing.Size(541, 21);
            this.editfchrPrompt.TabIndex = 36;
            this.editfchrPrompt.WordWrap = false;
            // 
            // editfchrSQLText
            // 
            this.editfchrSQLText.BackColor = System.Drawing.Color.LightCyan;
            this.editfchrSQLText.Location = new System.Drawing.Point(4, 137);
            this.editfchrSQLText.MaxLength = 800000000;
            this.editfchrSQLText.Multiline = true;
            this.editfchrSQLText.Name = "editfchrSQLText";
            this.editfchrSQLText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.editfchrSQLText.Size = new System.Drawing.Size(928, 450);
            this.editfchrSQLText.TabIndex = 37;
            this.editfchrSQLText.WordWrap = false;
            // 
            // editfchrNote
            // 
            this.editfchrNote.Location = new System.Drawing.Point(93, 98);
            this.editfchrNote.Name = "editfchrNote";
            this.editfchrNote.Size = new System.Drawing.Size(541, 21);
            this.editfchrNote.TabIndex = 38;
            this.editfchrNote.WordWrap = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(96, 59);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(84, 33);
            this.btnUpdate.TabIndex = 25;
            this.btnUpdate.Text = "保存(&U)";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblfintVersion
            // 
            this.lblfintVersion.Location = new System.Drawing.Point(12, 15);
            this.lblfintVersion.Name = "lblfintVersion";
            this.lblfintVersion.Size = new System.Drawing.Size(84, 23);
            this.lblfintVersion.TabIndex = 27;
            this.lblfintVersion.Text = "版本：";
            this.lblfintVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblfchrDataBase
            // 
            this.lblfchrDataBase.Location = new System.Drawing.Point(12, 42);
            this.lblfchrDataBase.Name = "lblfchrDataBase";
            this.lblfchrDataBase.Size = new System.Drawing.Size(84, 23);
            this.lblfchrDataBase.TabIndex = 28;
            this.lblfchrDataBase.Text = "升级数据库：";
            this.lblfchrDataBase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblfintOrder
            // 
            this.lblfintOrder.Location = new System.Drawing.Point(268, 44);
            this.lblfintOrder.Name = "lblfintOrder";
            this.lblfintOrder.Size = new System.Drawing.Size(71, 23);
            this.lblfintOrder.TabIndex = 29;
            this.lblfintOrder.Text = "运行顺序：";
            this.lblfintOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblfchrPrompt
            // 
            this.lblfchrPrompt.Location = new System.Drawing.Point(12, 71);
            this.lblfchrPrompt.Name = "lblfchrPrompt";
            this.lblfchrPrompt.Size = new System.Drawing.Size(84, 23);
            this.lblfchrPrompt.TabIndex = 33;
            this.lblfchrPrompt.Text = "提示与说明：";
            this.lblfchrPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblfchrNote
            // 
            this.lblfchrNote.Location = new System.Drawing.Point(49, 98);
            this.lblfchrNote.Name = "lblfchrNote";
            this.lblfchrNote.Size = new System.Drawing.Size(47, 23);
            this.lblfchrNote.TabIndex = 35;
            this.lblfchrNote.Text = "备注：";
            this.lblfchrNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblfchrSQLText
            // 
            this.lblfchrSQLText.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblfchrSQLText.Location = new System.Drawing.Point(670, 111);
            this.lblfchrSQLText.Name = "lblfchrSQLText";
            this.lblfchrSQLText.Size = new System.Drawing.Size(156, 20);
            this.lblfchrSQLText.TabIndex = 34;
            this.lblfchrSQLText.Text = "要执行的SQL语句：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.editfchrNote);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.editfchrDataBase);
            this.groupBox1.Controls.Add(this.editfintOrder);
            this.groupBox1.Controls.Add(this.editfchrPrompt);
            this.groupBox1.Controls.Add(this.lblfchrNote);
            this.groupBox1.Controls.Add(this.lblfchrPrompt);
            this.groupBox1.Controls.Add(this.lblfintOrder);
            this.groupBox1.Controls.Add(this.editfchrID);
            this.groupBox1.Controls.Add(this.lblfchrDataBase);
            this.groupBox1.Controls.Add(this.editfintVersion);
            this.groupBox1.Controls.Add(this.lblfintVersion);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbSQLRunType);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(650, 129);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据升级信息";
            // 
            // cbSQLRunType
            // 
            this.cbSQLRunType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSQLRunType.DropDownWidth = 500;
            this.cbSQLRunType.FormattingEnabled = true;
            this.cbSQLRunType.Items.AddRange(new object[] {
            "0：执行更新数据结构、数据等的SQL 语句",
            "1：判断的SQL 语句，返回一列的结果集",
            "2：判断的SQL 语句，返回一列的结果集，如果有值则提示并询问是否继续",
            "3：判断的SQL 语句，返回一列的结果集，如果有值则提示并继续升级"});
            this.cbSQLRunType.Location = new System.Drawing.Point(93, 97);
            this.cbSQLRunType.MaxDropDownItems = 20;
            this.cbSQLRunType.Name = "cbSQLRunType";
            this.cbSQLRunType.Size = new System.Drawing.Size(276, 20);
            this.cbSQLRunType.TabIndex = 50;
            this.cbSQLRunType.Visible = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(2, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 23);
            this.label2.TabIndex = 49;
            this.label2.Text = "脚本执行类型：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(310, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 48;
            this.label1.Text = "ID：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbEdit
            // 
            this.pbEdit.ErrorImage = global::Upgrade.Properties.Resources.hourglass;
            this.pbEdit.Image = global::Upgrade.Properties.Resources.hourglass;
            this.pbEdit.InitialImage = global::Upgrade.Properties.Resources.locked;
            this.pbEdit.Location = new System.Drawing.Point(874, 26);
            this.pbEdit.Name = "pbEdit";
            this.pbEdit.Size = new System.Drawing.Size(50, 50);
            this.pbEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbEdit.TabIndex = 49;
            this.pbEdit.TabStop = false;
            // 
            // btnPre
            // 
            this.btnPre.Location = new System.Drawing.Point(6, 14);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(33, 23);
            this.btnPre.TabIndex = 50;
            this.btnPre.Text = "<--";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(45, 14);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(33, 23);
            this.btnNext.TabIndex = 51;
            this.btnNext.Text = "-->";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(6, 15);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(62, 30);
            this.btnNew.TabIndex = 52;
            this.btnNew.Text = "新增(&N)";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.btnUpdate);
            this.groupBox2.Controls.Add(this.btnNew);
            this.groupBox2.Location = new System.Drawing.Point(668, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(186, 98);
            this.groupBox2.TabIndex = 53;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnPre);
            this.groupBox3.Controls.Add(this.btnNext);
            this.groupBox3.Location = new System.Drawing.Point(6, 51);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(86, 41);
            this.groupBox3.TabIndex = 53;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "翻页";
            // 
            // EditUpgradeSQLForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(937, 591);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pbEdit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.editfchrSQLText);
            this.Controls.Add(this.lblfchrSQLText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditUpgradeSQLForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "更新数据库";
            this.Load += new System.EventHandler(this.EditUpGradeSQLForm_Load);
            this.SizeChanged += new System.EventHandler(this.EditUpGradeSQLForm_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEdit)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


		
		
		private void GetData(string strConn, string strMainID)
		{
			if(strMainID != "")
			{
				string strSQL;

				System.Data.OleDb.OleDbCommand objComm;
				System.Data.OleDb.OleDbConnection objConn = new System.Data.OleDb.OleDbConnection();
				System.Data.OleDb.OleDbDataAdapter objDA  = new System.Data.OleDb.OleDbDataAdapter();
				System.Data.DataTable objDT  = new System.Data.DataTable();

				strConnection = strConn;
				objConn.ConnectionString = strConnection;
				objConn.Open();

				//=====================================
                strSQL = "Select fchrID, fintVersion, fchrDataBase, fintOrder, fchrPrompt, fchrSQLText, Isnull(fchrNote,'') as fchrNote " +
					"From UpgradeDataBaseSQL Where fchrID = '" + strMainID + "' ";

				objComm = objConn.CreateCommand();
				objComm.CommandTimeout = 600;
				objComm.CommandText = strSQL;
				objComm.ExecuteNonQuery();

				//=====================================
				objDT.Clear();
				objDT.TableName = "UpgradeDataBaseSQL";
				objDA.SelectCommand = objComm;
				objDA.Fill(objDT);

				//=================================================
				blnUpdate = true;
				if(objDT.Rows.Count > 0)
				{
					strID = "{" + strMainID + "}";
					this.editfchrID.Text = strID.ToUpper();
					this.editfintVersion.Text = ((int)objDT.Rows[0]["fintVersion"]).ToString().Trim();
					this.editfchrDataBase.Text = ((string)objDT.Rows[0]["fchrDataBase"]).Trim();
					this.editfintOrder.Text = ((int)objDT.Rows[0]["fintOrder"]).ToString().Trim();
					this.editfchrPrompt.Text = ((string)objDT.Rows[0]["fchrPrompt"]).Trim();
					this.editfchrSQLText.Text = objDT.Rows[0]["fchrSQLText"].ToString().Trim();
					this.editfchrNote.Text = ((string)objDT.Rows[0]["fchrNote"]).Trim();
                    //this.cbSQLRunType.SelectedIndex = (int)objDT.Rows[0]["fintSQLType"];
				}
			}
			else
			{
				blnUpdate = false;
				strID = "{" + System.Guid.NewGuid().ToString().ToUpper() + "}";
				this.editfchrID.Text = strID.ToUpper();
				this.editfintVersion.Text = strVersion;
				this.editfchrDataBase.Text = strDataBase;
				this.editfintOrder.Text = "";
				this.editfchrPrompt.Text = "";
				this.editfchrSQLText.Text = "";
				this.editfchrNote.Text = "";
                this.cbSQLRunType.SelectedIndex = 0;
			}
		}


		private void EditUpGradeSQLForm_SizeChanged(object sender, System.EventArgs e)
		{
			this.editfchrSQLText.Top = 138;
			this.editfchrSQLText.Left = 2;
			this.editfchrSQLText.Width = this.Width-10;
			this.editfchrSQLText.Height = (this.Height - 165 > 0 ? this.Height - 165 : 10);
		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			//MessageBox.Show("现程序不支持。","提示", MessageBoxButtons.OK,MessageBoxIcon.Stop);

			try
			{
				string strSQL;

				System.Data.OleDb.OleDbCommand objComm;
				System.Data.OleDb.OleDbConnection objConn = new System.Data.OleDb.OleDbConnection();

				objConn.ConnectionString = strConnection;
				objConn.Open();

				//=====================================
				if(blnUpdate)
				{
					strSQL = "Update UpgradeDataBaseSQL Set " +
												"fintVersion = " + this.editfintVersion.Text + ", " +
												"fchrDataBase = '" + this.editfchrDataBase.Text + "', " +
												"fintOrder = " + this.editfintOrder.Text + ", " +
												"fchrPrompt = '" + this.editfchrPrompt.Text + "', " +
												"fchrSQLText = '" + this.editfchrSQLText.Text.Replace("'","''") + "', " +
                                                //"fintSQLType = " + this.cbSQLRunType.SelectedIndex.ToString() + ", " +
												"fchrNote = '" + this.editfchrNote.Text + "' " +
							"Where fchrID = '" + strID + "' ";
				}
				else
				{
                    strSQL = "Insert Into UpgradeDataBaseSQL (fchrID, fintVersion, fchrDataBase, fintOrder, fchrPrompt, fchrSQLText, fchrNote) " + 
                                                     "Values ('" + strID + "'," + this.editfintVersion.Text + ",  '" + this.editfchrDataBase.Text + "', " + 
                                                              "" + this.editfintOrder.Text + ",  '" + this.editfchrPrompt.Text + "', " +
                                                              "'" + this.editfchrSQLText.Text.Replace("'", "''") + "', " +  
                                                              " '" + this.editfchrNote.Text + "' )";
				}
				objComm = objConn.CreateCommand();
				objComm.CommandTimeout = 600;
				objComm.CommandText = strSQL;
				objComm.ExecuteNonQuery();
				
				MessageBox.Show("“" + this.editfchrPrompt.Text + "”，保存成功！","提示", MessageBoxButtons.OK,MessageBoxIcon.Information);

                ((ListQueryForm)this.Owner).QueryList();
                blnUpdate = true;
			}
			catch(System.Exception Exp)
			{
				MessageBox.Show(Exp.Message,"提示", MessageBoxButtons.OK,MessageBoxIcon.Stop);
			}
		}

		private void EditUpGradeSQLForm_Load(object sender, System.EventArgs e)
		{
            this.pbEdit.Image = global::Upgrade.Properties.Resources.unlocked;
            blnDefault = true;
			if(! blnDefault)
			{
				this.editfchrDataBase.ReadOnly = true;
				this.editfchrDataBase.BackColor = System.Drawing.Color.White;
                //this.editfchrID.ReadOnly = true;
                //this.editfchrID.BackColor = System.Drawing.Color.White;
				this.editfchrNote.ReadOnly = true;
				this.editfchrNote.BackColor = System.Drawing.Color.White;
				this.editfchrPrompt.ReadOnly = true;
				this.editfchrPrompt.BackColor = System.Drawing.Color.White;
				this.editfchrSQLText.ReadOnly = true;
                this.editfchrSQLText.BackColor = System.Drawing.Color.LightCyan;
				this.editfintOrder.ReadOnly = true;
				this.editfintOrder.BackColor = System.Drawing.Color.White;
				this.editfintVersion.ReadOnly = true;
				this.editfintVersion.BackColor = System.Drawing.Color.White;
				this.btnUpdate.Enabled = false;
                this.btnNew.Enabled = false;
                this.pbEdit.Image = global::Upgrade.Properties.Resources.locked;
			}

            EditUpGradeSQLForm_SizeChanged(sender, e);

            this.Text = this.Text + "    当前数据库实例：" + AppClass.PublicStaticCls.strServer;
		}

        private void btnPre_Click(object sender, EventArgs e)
        {
            this.MovePreNextRow(-1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.MovePreNextRow(1);
        }


        private void MovePreNextRow(int intStep)
        {
            if (objDT != null)
            {
                if ((intStep > 0 && intDTIndex < this.objDT.Rows.Count - 1) || (intStep < 0 && intDTIndex > 0))
                {
                    intDTIndex = intDTIndex + intStep;
                    string strMainID = this.objDT.Rows[intDTIndex]["fchrID"].ToString().Trim();
                    this.GetData(strConnection, strMainID);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.GetData(this.strConnection, "");
            blnUpdate = false;
        }
	}
}
