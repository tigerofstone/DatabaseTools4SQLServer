using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Upgrade.UpGradeApp
{
	/// <summary>
	/// ExportInsertSQLForm 的摘要说明。
	/// </summary>
	public class ExportInsertSQLForm : System.Windows.Forms.Form
	{
		public struct clsItem
		{
			public string strName ;
			public string strID ;

			public override string ToString()
			{
				return strName;
			}
		}

		private string strConnection = "";
		private System.Data.OleDb.OleDbConnection objConn = new System.Data.OleDb.OleDbConnection();
		private System.Data.OleDb.OleDbDataAdapter objDA  = new System.Data.OleDb.OleDbDataAdapter();

        private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cbTables;
		private System.Windows.Forms.Button btnChange;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbPKField;
		private System.Windows.Forms.CheckBox cbUpdateSQL;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbWhere;
		private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.TabControl tcSQL;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.RichTextBox rtbSQL;
        private TextBox tbOrder;
        private Label label3;
        private TextBox tbModifyFields;
        private Label label4;
        private TextBox tbNotModifyFields;
        private Label label6;

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ExportInsertSQLForm()
		{
			InitializeComponent();

				
		}

		public ExportInsertSQLForm(string strConnect, string strDataBase)
		{
			strConnection = strConnect;
			objConn.ConnectionString = strConnection;
			objConn.Open();

			InitializeComponent();	

			System.Data.DataTable objDTList  = new System.Data.DataTable();
			objDTList = GetDataBaseTables(strDataBase);
			SetListTables(objDTList);

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportInsertSQLForm));
            this.label5 = new System.Windows.Forms.Label();
            this.cbTables = new System.Windows.Forms.ComboBox();
            this.btnChange = new System.Windows.Forms.Button();
            this.tbPKField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbUpdateSQL = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbNotModifyFields = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbModifyFields = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbOrder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbWhere = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.tcSQL = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtbSQL = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.tcSQL.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(18, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "数据表：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbTables
            // 
            this.cbTables.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbTables.ItemHeight = 12;
            this.cbTables.Location = new System.Drawing.Point(82, 15);
            this.cbTables.MaxDropDownItems = 25;
            this.cbTables.Name = "cbTables";
            this.cbTables.Size = new System.Drawing.Size(252, 20);
            this.cbTables.TabIndex = 18;
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(731, 55);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(81, 32);
            this.btnChange.TabIndex = 20;
            this.btnChange.Text = "生成(&C)";
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // tbPKField
            // 
            this.tbPKField.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPKField.Location = new System.Drawing.Point(436, 14);
            this.tbPKField.Name = "tbPKField";
            this.tbPKField.Size = new System.Drawing.Size(270, 21);
            this.tbPKField.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(372, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 14);
            this.label1.TabIndex = 22;
            this.label1.Text = "主键字段：";
            // 
            // cbUpdateSQL
            // 
            this.cbUpdateSQL.Location = new System.Drawing.Point(731, 16);
            this.cbUpdateSQL.Name = "cbUpdateSQL";
            this.cbUpdateSQL.Size = new System.Drawing.Size(73, 24);
            this.cbUpdateSQL.TabIndex = 23;
            this.cbUpdateSQL.Text = "包含修改";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbNotModifyFields);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbModifyFields);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbOrder);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbWhere);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbTables);
            this.groupBox1.Controls.Add(this.btnChange);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbPKField);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbUpdateSQL);
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(834, 118);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            // 
            // tbNotModifyFields
            // 
            this.tbNotModifyFields.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbNotModifyFields.Location = new System.Drawing.Point(437, 42);
            this.tbNotModifyFields.Name = "tbNotModifyFields";
            this.tbNotModifyFields.Size = new System.Drawing.Size(252, 21);
            this.tbNotModifyFields.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(350, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 16);
            this.label6.TabIndex = 30;
            this.label6.Text = "不更新字段：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbModifyFields
            // 
            this.tbModifyFields.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbModifyFields.Location = new System.Drawing.Point(82, 42);
            this.tbModifyFields.Name = "tbModifyFields";
            this.tbModifyFields.Size = new System.Drawing.Size(262, 21);
            this.tbModifyFields.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 28;
            this.label4.Text = "更新字段：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbOrder
            // 
            this.tbOrder.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbOrder.Location = new System.Drawing.Point(82, 91);
            this.tbOrder.Name = "tbOrder";
            this.tbOrder.Size = new System.Drawing.Size(422, 21);
            this.tbOrder.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 26;
            this.label3.Text = "排序：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbWhere
            // 
            this.tbWhere.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbWhere.Location = new System.Drawing.Point(82, 66);
            this.tbWhere.Name = "tbWhere";
            this.tbWhere.Size = new System.Drawing.Size(640, 21);
            this.tbWhere.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 24;
            this.label2.Text = "查询条件：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(20, 148);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(768, 17);
            this.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbProgress.TabIndex = 25;
            this.pbProgress.Visible = false;
            // 
            // tcSQL
            // 
            this.tcSQL.Controls.Add(this.tabPage2);
            this.tcSQL.Location = new System.Drawing.Point(20, 171);
            this.tcSQL.Name = "tcSQL";
            this.tcSQL.SelectedIndex = 0;
            this.tcSQL.Size = new System.Drawing.Size(826, 368);
            this.tcSQL.TabIndex = 26;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtbSQL);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(818, 343);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "RichText 显示";
            // 
            // rtbSQL
            // 
            this.rtbSQL.BackColor = System.Drawing.Color.LightCyan;
            this.rtbSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSQL.Location = new System.Drawing.Point(0, 0);
            this.rtbSQL.Name = "rtbSQL";
            this.rtbSQL.Size = new System.Drawing.Size(818, 343);
            this.rtbSQL.TabIndex = 0;
            this.rtbSQL.Text = "";
            this.rtbSQL.WordWrap = false;
            // 
            // ExportInsertSQLForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(873, 594);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.tcSQL);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExportInsertSQLForm";
            this.Text = "表数据输出Insert SQL语句";
            this.Load += new System.EventHandler(this.ExportInsertSQLForm_Load);
            this.SizeChanged += new System.EventHandler(this.ExportInsertSQLForm_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tcSQL.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void ExportInsertSQLForm_Load(object sender, System.EventArgs e)
		{
			ExportInsertSQLForm_SizeChanged(sender, e);

            this.Text = this.Text + "    当前数据库实例：" + AppClass.PublicStaticCls.strServer;
		}

		private void ExportInsertSQLForm_SizeChanged(object sender, System.EventArgs e)
		{
			this.tcSQL.Top = 133;
			this.tcSQL.Left = 0;			
			this.tcSQL.Width = this.Width - 5;			
			this.tcSQL.Height  = this.Height - 157;
		}

		private System.Data.DataTable GetDataBaseTables(string strDataBase)
		{
			string strSQL;
			System.Data.DataTable objDTRs  = new System.Data.DataTable();
			System.Data.OleDb.OleDbCommand objComm;
			
			strSQL = "Select [name],[id] from sysobjects Where xtype='U' Order By name";

			objComm = this.objConn.CreateCommand();
			objComm.CommandTimeout = 600;
			objComm.CommandText = strSQL;
			objComm.ExecuteNonQuery();
			
			objDTRs.Clear();
			objDTRs.TableName = "Tables";
			this.objDA.SelectCommand = objComm;
			this.objDA.Fill(objDTRs);		
	
			return objDTRs;
		}

		private void SetListTables(System.Data.DataTable objDTList)
		{
			clsItem objItem;
			if(objDTList.Rows.Count > 0)
			{
				for(int i = 0; i < objDTList.Rows.Count; i++)
				{
					objItem.strName = (string)objDTList.Rows[i]["name"];
					objItem.strID = objDTList.Rows[i]["id"].ToString();
					this.cbTables.Items.Add(objItem);
				}

				this.cbTables.SelectedIndex = 0;
			}
			
		}

		private clsItem GetItemByName(string strObjectName)
		{
			string strSQL;
			clsItem objItem = new clsItem(); 
			System.Data.DataTable objDTRs  = new System.Data.DataTable();
			System.Data.OleDb.OleDbCommand objComm;

			objItem.strID = "";
			objItem.strName = "";
			
			strSQL = "Select [name],[id] from sysobjects Where xtype='U' AND [name] = '" + strObjectName + "' Order By name";

			objComm = this.objConn.CreateCommand();
			objComm.CommandTimeout = 600;
			objComm.CommandText = strSQL;
			objComm.ExecuteNonQuery();
			
			objDTRs.Clear();
			objDTRs.TableName = "Tables";
			this.objDA.SelectCommand = objComm;
			this.objDA.Fill(objDTRs);		
			
			if(objDTRs.Rows.Count > 0)
			{
				objItem.strID = (string)objDTRs.Rows[0]["id"];
				objItem.strName = (string)objDTRs.Rows[0]["name"];
			}

			return objItem;
		}


		private void btnChange_Click(object sender, System.EventArgs e)
		{
			try
			{
				clsItem objItem = new clsItem();
				AppClass.MakeTableData2SQLCls objData2SQL;
				string strInsertSQL = "";

				// 得到主键的数据类型 
				if(this.cbTables.SelectedIndex >= 0)
				{
					objItem = (clsItem)this.cbTables.SelectedItem;
				}
				else
				{
					objItem = this.GetItemByName(this.cbTables.Text.Trim());
				}
				if(objItem.strID.Trim() == "")
				{
					MessageBox.Show("数据库中没有此数据表。", "系统提示", MessageBoxButtons.OK,MessageBoxIcon.Stop);
					return;
				}

				objData2SQL = new Upgrade.AppClass.MakeTableData2SQLCls(objConn);

                this.btnChange.Enabled = false;
				this.pbProgress.Width = this.Width - 200;
				this.pbProgress.Height = 20;
				this.pbProgress.Top = this.Height / 2 - 10;
				this.pbProgress.Left = 100;
				this.pbProgress.Visible = true;		


				objData2SQL.objPB = this.pbProgress;
                strInsertSQL = objData2SQL.makeSQLString(objItem.strID, objItem.strName, this.tbPKField.Text.Trim(),
                                                         changeFieldsString(this.tbModifyFields.Text.Trim()),
                                                         changeFieldsString(this.tbNotModifyFields.Text.Trim()), 
                                                         this.tbWhere.Text.Trim(), this.tbOrder.Text.Trim(), this.cbUpdateSQL.Checked);

				this.rtbSQL.Text = strInsertSQL;
			}
			catch(System.Exception E)
			{
				MessageBox.Show(E.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error); 
			}
			finally
			{
				this.pbProgress.Visible = false;
                this.btnChange.Enabled = true;
				System.GC.Collect();
			}

		}

        private string changeFieldsString(string strFields)
        {
            string strResult = "";

            if (strFields != "" && strFields.IndexOf("'") < 0)
            {
                strResult = strFields.Replace(" ", "");
                strResult = strResult.Replace(",", "', '");
                strResult = "'" + strResult + "'";
            }
            else
            {
                strResult = strFields;
            }

            return strResult;
        }
	
	}
}
	
	
