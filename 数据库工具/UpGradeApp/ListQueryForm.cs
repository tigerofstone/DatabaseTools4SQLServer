using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Upgrade.UpGradeApp
{
	/// <summary>
	/// ListQueryForm 的摘要说明。
	/// </summary>
	public class ListQueryForm : System.Windows.Forms.Form
	{
		private string strConnection = "";
		private System.Data.OleDb.OleDbConnection objConn = new System.Data.OleDb.OleDbConnection();
		private System.Data.OleDb.OleDbDataAdapter objDA  = new System.Data.OleDb.OleDbDataAdapter();
		private	System.Data.DataTable objDT  = new System.Data.DataTable();

		private System.Windows.Forms.DataGrid dgData;
		private System.Windows.Forms.Label ss;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox gbCondition;
        private System.Windows.Forms.ComboBox cbVer;
		private System.Windows.Forms.TextBox tbKeyWord;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button btnAddInfo;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnViewEdit;
        private ComboBox cbDatabase;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ListQueryForm()
		{
			strConnection =  "Data Source=NOCONA-DRP\\SQL1;Initial Catalog=U8DRP_Template;User ID=sample;PassWord=sample;" +
				             "Tag with column collation when possible=False;Use Procedure for Prepare=1;Auto Translate=True;Persist Security Info=False;" + 
				             "Provider=SQLOLEDB.1;Workstation ID=MX2008;Use Encryption for Data=False;Packet Size=4096";

			InitializeComponent();			
		}

		public ListQueryForm(string strConnect, string strVersion)
		{
			strConnection = strConnect;
			objConn.ConnectionString = strConnection;
			objConn.Open();

			InitializeComponent();	
		
			//this.cbVer.Text = strVersion;
			//this.tbKeyWord.Text  = strVersion;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListQueryForm));
            this.dgData = new System.Windows.Forms.DataGrid();
            this.ss = new System.Windows.Forms.Label();
            this.cbVer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbKeyWord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbCondition = new System.Windows.Forms.GroupBox();
            this.cbDatabase = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnViewEdit = new System.Windows.Forms.Button();
            this.btnAddInfo = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.gbCondition.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgData
            // 
            this.dgData.DataMember = "";
            this.dgData.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgData.Location = new System.Drawing.Point(2, 88);
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            this.dgData.Size = new System.Drawing.Size(872, 439);
            this.dgData.TabIndex = 0;
            // 
            // ss
            // 
            this.ss.Location = new System.Drawing.Point(9, 24);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(54, 11);
            this.ss.TabIndex = 1;
            this.ss.Text = "版本号：";
            // 
            // cbVer
            // 
            this.cbVer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVer.ItemHeight = 12;
            this.cbVer.Location = new System.Drawing.Point(70, 19);
            this.cbVer.MaxDropDownItems = 50;
            this.cbVer.Name = "cbVer";
            this.cbVer.Size = new System.Drawing.Size(224, 20);
            this.cbVer.TabIndex = 2;
            this.cbVer.SelectedIndexChanged += new System.EventHandler(this.cbVer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(308, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "数据库：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbKeyWord
            // 
            this.tbKeyWord.Location = new System.Drawing.Point(72, 50);
            this.tbKeyWord.Name = "tbKeyWord";
            this.tbKeyWord.Size = new System.Drawing.Size(223, 21);
            this.tbKeyWord.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 11);
            this.label2.TabIndex = 6;
            this.label2.Text = "关键字：";
            // 
            // gbCondition
            // 
            this.gbCondition.Controls.Add(this.cbDatabase);
            this.gbCondition.Controls.Add(this.button2);
            this.gbCondition.Controls.Add(this.ss);
            this.gbCondition.Controls.Add(this.cbVer);
            this.gbCondition.Controls.Add(this.tbKeyWord);
            this.gbCondition.Controls.Add(this.label1);
            this.gbCondition.Controls.Add(this.label2);
            this.gbCondition.Location = new System.Drawing.Point(10, 0);
            this.gbCondition.Name = "gbCondition";
            this.gbCondition.Size = new System.Drawing.Size(533, 82);
            this.gbCondition.TabIndex = 7;
            this.gbCondition.TabStop = false;
            this.gbCondition.Paint += new System.Windows.Forms.PaintEventHandler(this.gbCondition_Paint);
            // 
            // cbDatabase
            // 
            this.cbDatabase.Items.AddRange(new object[] {
            "APP",
            "U8DRP_EntData",
            "U8DRP_DataBase"});
            this.cbDatabase.Location = new System.Drawing.Point(372, 19);
            this.cbDatabase.Name = "cbDatabase";
            this.cbDatabase.Size = new System.Drawing.Size(147, 20);
            this.cbDatabase.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(451, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 26);
            this.button2.TabIndex = 9;
            this.button2.Text = "查询(&Q)";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnViewEdit
            // 
            this.btnViewEdit.Location = new System.Drawing.Point(565, 47);
            this.btnViewEdit.Name = "btnViewEdit";
            this.btnViewEdit.Size = new System.Drawing.Size(102, 30);
            this.btnViewEdit.TabIndex = 8;
            this.btnViewEdit.Text = "查看，更改(&E)";
            this.btnViewEdit.Click += new System.EventHandler(this.btnViewEdit_Click);
            // 
            // btnAddInfo
            // 
            this.btnAddInfo.Location = new System.Drawing.Point(565, 12);
            this.btnAddInfo.Name = "btnAddInfo";
            this.btnAddInfo.Size = new System.Drawing.Size(68, 27);
            this.btnAddInfo.TabIndex = 9;
            this.btnAddInfo.Text = "新增(&A)";
            this.btnAddInfo.Click += new System.EventHandler(this.btnAddInfo_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(639, 11);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(63, 27);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "删除(&D)";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // ListQueryForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(913, 528);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddInfo);
            this.Controls.Add(this.btnViewEdit);
            this.Controls.Add(this.gbCondition);
            this.Controls.Add(this.dgData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ListQueryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "列表";
            this.Load += new System.EventHandler(this.ListQueryForm_Load);
            this.SizeChanged += new System.EventHandler(this.ListQueryForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.gbCondition.ResumeLayout(false);
            this.gbCondition.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void ListQueryForm_Load(object sender, System.EventArgs e)
		{
			/*显示版本信息*/
			objDT = QueryVersion();
			ShowVersion(objDT);
			objDT.Clear();

			this.ListQueryForm_SizeChanged(sender, e);
			this.cbVer.SelectedIndex = 0;

            this.Text = this.Text + "    数据库实例：" + AppClass.PublicStaticCls.strServer;
		}

        private void gbCondition_Paint(object sender, PaintEventArgs e)
        {
            //System.Drawing.Pen objPen = new System.Drawing.Pen(Color.Black, 1);

            //e.Graphics.DrawLine(objPen, this.cbVer.Left, this.cbVer.Top - 1, this.cbVer.Left + this.cbVer.Width, this.cbVer.Top - 1);
            //e.Graphics.DrawLine(objPen, this.cbVer.Left, this.cbVer.Top + this.cbVer.Height, this.cbVer.Left + this.cbVer.Width, this.cbVer.Top + this.cbVer.Height);
            //e.Graphics.DrawLine(objPen, this.cbVer.Left - 1, this.cbVer.Top - 1, this.cbVer.Left - 1, this.cbVer.Top + this.cbVer.Height);
            //e.Graphics.DrawLine(objPen, this.cbVer.Left + this.cbVer.Width, this.cbVer.Top, this.cbVer.Left + this.cbVer.Width, this.cbVer.Top + this.cbVer.Height);

            //e.Graphics.DrawLine(objPen, this.cbDatabase.Left, this.cbDatabase.Top - 1, this.cbDatabase.Left + this.cbDatabase.Width, this.cbDatabase.Top - 1);
            //e.Graphics.DrawLine(objPen, this.cbDatabase.Left, this.cbDatabase.Top + this.cbDatabase.Height, this.cbDatabase.Left + this.cbDatabase.Width, this.cbDatabase.Top + this.cbDatabase.Height);
            //e.Graphics.DrawLine(objPen, this.cbDatabase.Left - 1, this.cbDatabase.Top - 1, this.cbDatabase.Left - 1, this.cbDatabase.Top + this.cbDatabase.Height);
            //e.Graphics.DrawLine(objPen, this.cbDatabase.Left + this.cbDatabase.Width, this.cbDatabase.Top, this.cbDatabase.Left + this.cbDatabase.Width, this.cbDatabase.Top + this.cbDatabase.Height);
        }
        
		private void button2_Click(object sender, System.EventArgs e)
		{
            this.QueryList();
		}

		private System.Data.DataTable QueryVersion()
		{
			try
			{
				string strSQL;
				System.Data.OleDb.OleDbCommand objComm;
				System.Data.DataTable objDTTemp  = new System.Data.DataTable();
			
				strSQL = "SELECT fintVerNumber,fchrVersion, fchrName FROM VerSet ORDER BY fintVerNumber DESC ";

				objComm = this.objConn.CreateCommand();
				objComm.CommandTimeout = 600;
				objComm.CommandText = strSQL;
				objComm.ExecuteNonQuery();
			
				objDTTemp.Clear();
				objDTTemp.TableName = "VerSet";
				objDA.SelectCommand = objComm;
				objDA.Fill(objDTTemp);

				return objDTTemp;
			}
			catch(System.Exception e)
			{
				throw e;
			}
		}

        public void QueryList()
        {
            this.QueryUpgradeList();

            this.ShowList();
        }

		private void ShowVersion(System.Data.DataTable objDTVer)
		{
			try
			{
				Version sctVer;
				for(int i = 0; i < objDTVer.Rows.Count; i++)
				{
					sctVer.intVerNumber = (int)objDTVer.Rows[i]["fintVerNumber"];
					sctVer.strVersionNO = (string)objDTVer.Rows[i]["fchrVersion"];
					sctVer.strVersionName = (string)objDTVer.Rows[i]["fchrName"];
					sctVer.blnDefault = (i == 0 ? true : false);

					this.cbVer.Items.Add(sctVer);
				}
			}
			catch(System.Exception e)
			{
				throw e;
			}
		}

		private void QueryUpgradeList()
		{			
			string strSQL;
			System.Data.OleDb.OleDbCommand objComm;

            strSQL = "Select fchrID,fintVersion,fchrDataBase,fintOrder,fchrPrompt, "
                          //+ "(CASE fintSQLType WHEN 0 THEN '0：更新脚本' WHEN 1 THEN '1：判断脚本，拒绝' " +
                          //                    "WHEN 2 THEN '2：判断脚本，是否继续' WHEN 3 THEN '3：判断脚本，继续升级' END) AS fchrSQLType, " 
                          + "fchrNote From UpgradeDataBaseSQL Where 1 = 1 ";
			
			if(this.cbVer.Text.Trim() != "") strSQL = strSQL + "And fintVersion = '" + ((Version)this.cbVer.Items[this.cbVer.SelectedIndex]).intVerNumber.ToString().Trim() + "' ";
			if(this.cbDatabase.Text.Trim() != "") strSQL = strSQL + "And fchrDataBase Like '%" + this.cbDatabase.Text.Trim() + "%' ";
			if(this.tbKeyWord.Text.Trim() != "") strSQL = strSQL + "And (fchrPrompt Like '%" + this.tbKeyWord.Text.Trim() + "%' " +
                                                                         "Or fchrSQLText Like '%" + this.tbKeyWord.Text.Trim() + "%') ";
			
			strSQL = strSQL + "Order By fintVersion,fchrDataBase,fintOrder";

			objComm = this.objConn.CreateCommand();
			objComm.CommandTimeout = 600;
			objComm.CommandText = strSQL;
			objComm.ExecuteNonQuery();
			
			this.objDT.Clear();
			this.objDT.TableName = "UpgradeDataBaseSQL";
			this.objDA.SelectCommand = objComm;
			this.objDA.Fill(this.objDT);
		}

		private void ShowList()
		{			
			this.SetListDataGrideStyle();

			this.dgData.DataSource = null;
			this.dgData.DataSource = this.objDT;
		}

		private void SetListDataGrideStyle()
		{
			System.Windows.Forms.DataGridTableStyle objTabSty = new DataGridTableStyle();

			if(this.dgData.TableStyles.Count > 0 )
			{
				this.dgData.TableStyles.Clear();
			}

			objTabSty.GridColumnStyles.Clear();
	
			objTabSty.MappingName = "UpgradeDataBaseSQL";
			
			System.Windows.Forms.DataGridColumnStyle objComSty0 = new DataGridTextBoxColumn();
			objComSty0.HeaderText = "ID";
			objComSty0.MappingName = "fchrID";
			objComSty0.Width = 100;
			objComSty0.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			objComSty0.ReadOnly = true;
			objTabSty.GridColumnStyles.Add(objComSty0);

			System.Windows.Forms.DataGridColumnStyle objComSty1 = new DataGridTextBoxColumn();
			objComSty1.HeaderText = "版本号";
			objComSty1.MappingName = "fintVersion";
			objComSty1.Width = 60;
			objComSty1.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			objTabSty.GridColumnStyles.Add(objComSty1);
			
			System.Windows.Forms.DataGridColumnStyle objComSty2 = new DataGridTextBoxColumn();
			objComSty2.HeaderText = "数据库";
			objComSty2.MappingName = "fchrDataBase";
			objComSty2.Width = 100;
			objComSty2.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			objTabSty.GridColumnStyles.Add(objComSty2);
		
			System.Windows.Forms.DataGridColumnStyle objComSty3 = new DataGridTextBoxColumn();
			objComSty3.HeaderText = "顺序";
			objComSty3.MappingName = "fintOrder";
			objComSty3.Width = 35;
			objComSty3.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			objTabSty.GridColumnStyles.Add(objComSty3);

			System.Windows.Forms.DataGridColumnStyle objComSty4 = new DataGridTextBoxColumn();
			objComSty4.HeaderText = "说明，提示";
			objComSty4.MappingName = "fchrPrompt";
			objComSty4.Width = 250;
			objComSty4.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
			objTabSty.GridColumnStyles.Add(objComSty4);

            //System.Windows.Forms.DataGridColumnStyle objComSty5 = new DataGridTextBoxColumn();
            //objComSty5.HeaderText = "脚本执行类型";
            //objComSty5.MappingName = "fchrSQLType";
            //objComSty5.Width = 110;
            //objComSty5.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            //objTabSty.GridColumnStyles.Add(objComSty5);

			System.Windows.Forms.DataGridColumnStyle objComSty6 = new DataGridTextBoxColumn();
            objComSty6.HeaderText = "备注";
            objComSty6.MappingName = "fchrNote";
            objComSty6.Width = 200;
            objComSty6.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            objTabSty.GridColumnStyles.Add(objComSty6);
			
			objTabSty.AllowSorting = true;
			objTabSty.RowHeaderWidth = 20;
			objTabSty.BackColor = Color.Azure;
			objTabSty.AlternatingBackColor = Color.AliceBlue;
			
			this.dgData.TableStyles.Add(objTabSty);	
		}

		private void ListQueryForm_SizeChanged(object sender, System.EventArgs e)
		{
			this.dgData.Top = 88;
			this.dgData.Left = 2;
			this.dgData.Width = this.Width-10;
			this.dgData.Height = (this.Height - 113 > 0 ? this.Height - 113 : 10);
		}

		private void btnViewEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID;
			
				if(objDT.Rows.Count > 0)
				{
					strID = objDT.Rows[this.dgData.CurrentRowIndex]["fchrID"].ToString();

					Upgrade.UpGradeApp.EditUpgradeSQLForm objForm = new Upgrade.UpGradeApp.EditUpgradeSQLForm(strConnection,strID, (Version)this.cbVer.Items[this.cbVer.SelectedIndex], this.cbDatabase.Text.Trim());
                    objForm.objDT = objDT;
                    objForm.intDTIndex = this.dgData.CurrentRowIndex;

                    objForm.Owner = this;
                    objForm.ShowDialog();
				}
			}
			catch(System.Exception Exp)
			{
				MessageBox.Show(Exp.Message,"提示", MessageBoxButtons.OK,MessageBoxIcon.Stop);
			}

		}

		private void btnAddInfo_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strID = "";

				Upgrade.UpGradeApp.EditUpgradeSQLForm objForm = new Upgrade.UpGradeApp.EditUpgradeSQLForm(strConnection,strID, (Version)this.cbVer.Items[this.cbVer.SelectedIndex], this.cbDatabase.Text.Trim());
                objForm.Owner = this;
				objForm.ShowDialog();
			}
			catch(System.Exception Exp)
			{
				MessageBox.Show(Exp.Message,"提示", MessageBoxButtons.OK,MessageBoxIcon.Stop);
			}

		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			System.Data.OleDb.OleDbCommand objComm;
			System.Data.OleDb.OleDbTransaction objTransaction = null;

			try
			{
				string strID;				
			
				if(objDT.Rows.Count > 0)
				{
                    if (MessageBox.Show("是否要删除【" + objDT.Rows[this.dgData.CurrentRowIndex]["fchrDataBase"].ToString() + "，顺序为" +
                                                         objDT.Rows[this.dgData.CurrentRowIndex]["fintOrder"].ToString() + "，" + 
                                                         objDT.Rows[this.dgData.CurrentRowIndex]["fchrPrompt"].ToString() + "】升级脚本？", 
                                        "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

				strID = objDT.Rows[this.dgData.CurrentRowIndex]["fchrID"].ToString();
				
				string strSQL;
				
				objTransaction = objConn.BeginTransaction();
		
				strSQL = "Delete From UpgradeDataBaseSQL Where 1 < 2 ";
		
				if(this.cbVer.Text.Trim() != "") strSQL = strSQL + "And fintVersion = " + ((Version)this.cbVer.Items[this.cbVer.SelectedIndex]).intVerNumber + " ";
				if(strID != "") strSQL = strSQL + "And fchrID = '" + strID + "' ";

				objComm = this.objConn.CreateCommand();

				objComm.Transaction = objTransaction;
				objComm.CommandTimeout = 600;
				objComm.CommandText = strSQL;
				objComm.ExecuteNonQuery();			

				objTransaction.Commit();

				this.QueryUpgradeList();

				this.ShowList();				
			}
			catch(System.Exception Exp)
			{
				MessageBox.Show(Exp.Message,"提示", MessageBoxButtons.OK,MessageBoxIcon.Stop);

				objTransaction.Rollback();
			}
		}

		private void cbVer_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            this.btnDelete.Enabled = true;
            this.btnAddInfo.Enabled = true;

            //if (((Version)this.cbVer.Items[this.cbVer.SelectedIndex]).blnDefault)
            //{
            //    this.btnDelete.Enabled = true;
            //    this.btnAddInfo.Enabled = true;
            //}
            //else
            //{
            //    this.btnDelete.Enabled = false;
            //    this.btnAddInfo.Enabled = false;
            //}
		}

		
		// 内部类
		public struct Version 
		{
			public int intVerNumber;
			public string strVersionNO;
			public string strVersionName;
			public bool blnDefault;

			public override string ToString()
			{
				int i;
				i = strVersionNO.Length;
				
				return (i == 10 ? strVersionNO : strVersionNO + new string(' ', 10 - i)) + "" + strVersionName;
			}
		}

        

        
		
	}
}
