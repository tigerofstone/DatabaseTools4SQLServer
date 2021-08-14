using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Xml;

namespace Upgrade.Apps
{
    public partial class frmDBReIndexandStatistics : Form
    {
        public string strConnection = "";
        public string strInstance = "";
        public string strUser = "";
        public string strPassword = "";
        public bool blnIntegrated = false;

        private System.Collections.Hashtable objDBFragInfo = new System.Collections.Hashtable();

        private DataSet objDSState = new DataSet();
        private DataTable objDTStateDB = new DataTable("Database");
        private DataTable objDTStateTable = new DataTable("Table");

        private int intStop = 0;

        public frmDBReIndexandStatistics()
        {
            InitializeComponent();
        }

        private void frmDBReIndexandStatistics_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection objSQLConnect = new SqlConnection();
                objSQLConnect = this.getConnection(this.strInstance, "master", this.strUser, this.strPassword, this.blnIntegrated);

                this.FullDatabaseList(this.getAllDatabases(objSQLConnect));

                this.InitializeTreeDS();

                this.groupBox2.Enabled = false;                
            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }

        private void frmDBReIndexandStatistics_Resize(object sender, EventArgs e)
        {
            this.groupBox3.Left = 472;
            this.groupBox3.Width = this.Width - 472 - 13;
            this.groupBox3.Height = this.Height - 30;

            this.utcMainInfo.Top = 107;
            this.utcMainInfo.Left = 3;
            this.utcMainInfo.Width = this.groupBox3.Width - 6;
            this.utcMainInfo.Height = this.groupBox3.Height - 107 - 6;

            this.upbAllDB.Width = this.groupBox3.Width - 14;
            this.upbOneDB.Width = this.upbAllDB.Width;
        }




        #region �ؼ��¼�����

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cklDBList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cklDBList.GetItemCheckState(this.cklDBList.SelectedIndex) == CheckState.Checked)
            {
                this.ListDBTables();
                this.groupBox2.Enabled = true;
            }
            else
            {
                this.ClearDBFragInfo();

                this.groupBox2.Enabled = false;
            }
        }

        private void cklDBList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.cklDBList.GetItemCheckState(this.cklDBList.SelectedIndex) == CheckState.Checked)
            {
                this.ListDBTables();
                this.groupBox2.Enabled = true;
            }
            else
            {
                this.ClearDBFragInfo();

                this.groupBox2.Enabled = false;
            }
        }


        private void rbFragAll_Click(object sender, EventArgs e)
        {
            if (this.rbFragAll.Checked && this.cklDBList.SelectedIndex >= 0)
            {
                this.SetItemsState(false);
                this.SetDBTablesSelect();
            }
        }


        private void rbSelectTables_Click(object sender, EventArgs e)
        {
            if (this.rbSelectTables.Checked && this.cklDBList.SelectedIndex >= 0)
            {
                this.SetItemsState(true);
                this.ShowDBFragInfo();
                this.SetDBTablesSelect();
            }
        }
        private void rbSelectTables_CheckedChanged(object sender, EventArgs e)
        {
            this.SetItemsState(true);
        }

        private void rbFragAll_CheckedChanged(object sender, EventArgs e)
        {
            this.SetItemsState(false);
        }


        private void btnSelectAllTables_Click(object sender, EventArgs e)
        {
            this.PushSelectToOther(1, 1);
        }

        private void btnSelectTable_Click(object sender, EventArgs e)
        {
            this.PushSelectToOther(-1, 1);
        }

        private void btnRamoveTable_Click(object sender, EventArgs e)
        {
            this.PushSelectToOther(-1, -1);
        }

        private void btnRemoveAllTables_Click(object sender, EventArgs e)
        {
            this.PushSelectToOther(1, -1);
        }

        private void btnRunConfig_Click(object sender, EventArgs e)
        {
            DateTime objDT1, objDT2;
            objDT1 = System.DateTime.Now;

            try
            {
                this.btnRunConfig.Enabled = false;
                this.cklDBList.Enabled = false;
                this.groupBox2.Enabled = false;
                this.SetListBoxIndex(-1);

                this.objDTStateTable.Clear();
                this.objDTStateDB.Clear();
                this.utRunStateSimple.Nodes.Clear();
                this.ControlBox = true;

                this.DBDefrag();

                this.btnRunConfig.Enabled = true;
                this.cklDBList.Enabled = true;
                this.groupBox2.Enabled = true;
                this.ControlBox = true;

                if (this.objDTStateTable.Rows.Count > 0)
                {
                    this.ugDBDefragInfo.ActiveRow = (this.ugDBDefragInfo.GetRow(Infragistics.Win.UltraWinGrid.ChildRow.Last).GetChild(Infragistics.Win.UltraWinGrid.ChildRow.Last)).GetSibling(Infragistics.Win.UltraWinGrid.SiblingRow.Last);
                    this.ugDBDefragInfo.Selected.Rows.Clear();
                    this.ugDBDefragInfo.ActiveRow.Selected = true;
                }
                objDT2 = System.DateTime.Now;
                MessageBox.Show("�������ݿ���ɣ�\r\n�ܹ�ִ��ʱ��Ϊ�� " + (objDT2 - objDT1).TotalMilliseconds.ToString("#,#") + " ���롣", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                if (this.intStop == 0)
                {
                    MessageBox.Show("�������ݿⷢ������/r/n" + "������Ϣ��" + exp.Message + "/r/n��ջ��Ϣ��" + exp.StackTrace, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            
        }

        private void lbDBTableAll_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnSelectTable_Click(sender, null);
        }

        private void lbSelectDBTables_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnRamoveTable_Click(sender, null);
        }


        #endregion



        #region  ����˽�з���

        private void SetItemsState(bool bState)
        {
            this.lbDBTableAll.Enabled = bState;
            this.lbSelectDBTables.Enabled = bState;

            this.label4.Enabled = bState;
            this.label5.Enabled = bState;
            this.btnSelectAllTables.Enabled = bState;
            this.btnSelectTable.Enabled = bState;
            this.btnRemoveAllTables.Enabled = bState;
            this.btnRamoveTable.Enabled = bState;
        }

        private void SetListBoxIndex(int n)
        {
            this.lbDBTableAll.SelectedIndex = n;
            this.lbSelectDBTables.SelectedIndex = n;
        }

        /// <summary>
        /// ������ݿ��б�
        /// </summary>
        /// <param name="objReader"></param>
        private void FullDatabaseList(SqlDataReader objReader)
        {
            if (objReader != null)
            {
                while (objReader.Read())
                {
                    this.cklDBList.Items.Add(objReader.GetString(0));
                }
            }
        }

        //����ѡ��
        private void SetDBTablesSelect()
        {
            System.Collections.ArrayList objInfo = new System.Collections.ArrayList();

            if (this.objDBFragInfo.Contains(this.cklDBList.SelectedItem.ToString().Trim()))
            {
                objInfo = (System.Collections.ArrayList)this.objDBFragInfo[this.cklDBList.SelectedItem.ToString().Trim()];

                objInfo[0] = (this.rbSelectTables.Checked ? "1" : "0");
            }
            else
            {
                MessageBox.Show("�������ݿ�ѡ�����001��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //������ݱ��б�
        private void ListDBTables()
        {
            System.Collections.ArrayList objInfo = new System.Collections.ArrayList();
            System.Collections.ArrayList objListTables = new System.Collections.ArrayList();
            System.Collections.ArrayList objSelectedTables = new System.Collections.ArrayList();

            if (this.objDBFragInfo.Contains(this.cklDBList.SelectedItem.ToString().Trim()))
            {
                objInfo = (System.Collections.ArrayList)this.objDBFragInfo[this.cklDBList.SelectedItem.ToString().Trim()];
                objListTables = (System.Collections.ArrayList)objInfo[1];
                objSelectedTables = (System.Collections.ArrayList)objInfo[2];

                if ((string)objInfo[0] == "1")
                { 
                    this.rbSelectTables.Checked = true; this.rbFragAll.Checked = false;
                    this.SetItemsState(true);
                }
                else
                { 
                    this.rbSelectTables.Checked = false; this.rbFragAll.Checked = true;
                    this.SetItemsState(false);
                }

                if ((objListTables.Count > 0 || objSelectedTables.Count > 0) || this.rbFragAll.Checked)
                {
                    this.lbDBTableAll.DataSource = objListTables;
                    this.lbSelectDBTables.DataSource = objSelectedTables;
                }
                else if ((this.rbSelectTables.Checked) && !(objListTables.Count > 0 || objSelectedTables.Count > 0))
                {
                    objListTables = this.GetDBTables();

                    this.lbDBTableAll.DataSource = objListTables;
                    this.lbSelectDBTables.DataSource = objSelectedTables;

                    objInfo.Add(objListTables);
                    objInfo.Add(objSelectedTables);
                    this.objDBFragInfo.Add(this.cklDBList.SelectedItem.ToString().Trim(), objInfo);
                }                
            }
            else
            {
                this.rbSelectTables.Checked = false; this.rbFragAll.Checked = true;
                objInfo.Add((this.rbSelectTables.Checked ? "1" : "0"));

                objInfo.Add(objListTables);
                objInfo.Add(objSelectedTables);
                this.objDBFragInfo.Add(this.cklDBList.SelectedItem.ToString().Trim(), objInfo);
            }

        }


        private void ShowDBFragInfo()
        {
            System.Collections.ArrayList objInfo = new System.Collections.ArrayList();
            System.Collections.ArrayList objListTables = new System.Collections.ArrayList();
            System.Collections.ArrayList objSelectedTables = new System.Collections.ArrayList();

            if (this.objDBFragInfo.Contains(this.cklDBList.SelectedItem.ToString().Trim()))
            {
                objInfo = (System.Collections.ArrayList)this.objDBFragInfo[this.cklDBList.SelectedItem.ToString().Trim()];
                objListTables = (System.Collections.ArrayList)objInfo[1];
                objSelectedTables = (System.Collections.ArrayList)objInfo[2];

                this.SetDBTableInfo(ref objInfo, ref objListTables, ref objSelectedTables);
                this.objDBFragInfo[this.cklDBList.SelectedItem.ToString().Trim()] = objInfo;
            }
        }

        private void SetDBTableInfo(ref System.Collections.ArrayList objInfo, ref System.Collections.ArrayList objListTables,
                                    ref System.Collections.ArrayList objSelectedTables)
        {
            if (objListTables.Count > 0 || objSelectedTables.Count > 0)
            {
                this.lbDBTableAll.DataSource = objListTables;
                this.lbSelectDBTables.DataSource = objSelectedTables;
            }
            else if (this.rbSelectTables.Checked)
            {
                objListTables = this.GetDBTables();

                this.lbDBTableAll.DataSource = objListTables;
                this.lbSelectDBTables.DataSource = objSelectedTables;

                objInfo[1] = objListTables;
                objInfo[2] = objSelectedTables;
            }
        }




        private System.Collections.ArrayList GetDBTables()
        {
            //������ݱ��б�
            System.Collections.ArrayList objListTables = new System.Collections.ArrayList();

            SqlDataReader objRD = this.getAllTables(this.getConnection(this.strInstance, this.cklDBList.SelectedItem.ToString().Trim(),
                                                                       this.strUser, this.strPassword, this.blnIntegrated));
            while (objRD.Read())
            {
                objListTables.Add(objRD.GetString(0).Trim());
            }
            if (!objRD.IsClosed) objRD.Close();

            return objListTables;
        }



        private void ClearDBFragInfo()
        {
            this.rbSelectTables.Checked = false; this.rbFragAll.Checked = true;
            if (this.lbDBTableAll.DataSource == null && this.lbSelectDBTables.DataSource == null)
            {
                this.lbDBTableAll.Items.Clear();
                this.lbSelectDBTables.Items.Clear();
            }
            else
            {
                this.lbDBTableAll.DataSource = null;
                this.lbSelectDBTables.DataSource = null;
            }           
        }


        private void PushSelectToOther(int iIsAll, int iDirsectionL2S)
        {
            if (this.rbSelectTables.Checked)
            {
                System.Collections.ArrayList objInfo = new System.Collections.ArrayList();
                System.Collections.ArrayList objListTables = new System.Collections.ArrayList();
                System.Collections.ArrayList objSelectedTables = new System.Collections.ArrayList();

                objInfo = (System.Collections.ArrayList)this.objDBFragInfo[this.cklDBList.SelectedItem.ToString().Trim()];
                objListTables = (System.Collections.ArrayList)objInfo[1];
                objSelectedTables = (System.Collections.ArrayList)objInfo[2];

                if (iDirsectionL2S == 1)
                    this.SetSelectDataAndList(iIsAll, ref objSelectedTables, ref objListTables, ref this.lbSelectDBTables, ref this.lbDBTableAll);
                else
                    this.SetSelectDataAndList(iIsAll, ref objListTables, ref objSelectedTables, ref this.lbDBTableAll, ref this.lbSelectDBTables);

                this.SetListBoxIndex(-1);

                //�޸����ݼ�
                objInfo = (System.Collections.ArrayList)this.objDBFragInfo[this.cklDBList.SelectedItem.ToString().Trim()];
                objInfo[1] = objListTables;
                objInfo[2] = objSelectedTables;

                this.objDBFragInfo[this.cklDBList.SelectedItem.ToString().Trim()] = objInfo;
            }
        }

        private void SetSelectDataAndList(int iIsAll, ref System.Collections.ArrayList objListTables, ref System.Collections.ArrayList objSelectedTables, 
                                          ref System.Windows.Forms.ListBox objlbList, ref System.Windows.Forms.ListBox objlbSelects)
        {
            //=====================================================================================
            if (iIsAll == 1)
            {
                for (int i = 0; i < objlbSelects.Items.Count; i++)
                {
                    objListTables.Add((string)objlbSelects.Items[i]);
                }
            }
            else if (iIsAll == -1)
            {
                for (int i = objlbSelects.SelectedItems.Count - 1; i >= 0; i--)
                {
                    objListTables.Add((string)objlbSelects.SelectedItems[i]);

                    objSelectedTables.RemoveAt(objlbSelects.SelectedIndices[i]);
                }
            }
            objListTables.Sort();

            objlbList.DataSource = null;
            objlbList.DataSource = objListTables;
            //objlbList.Refresh();

            if (iIsAll == 1) objSelectedTables.Clear();
            objlbSelects.DataSource = null;
            objlbSelects.DataSource = objSelectedTables;
            //=======================================================================================

        }





        #endregion



        #region  ����˽�к���

        /// <summary>
        /// ��ʼ��״̬������Դ
        /// </summary>
        private void InitializeTreeDS()
        {
            this.objDTStateDB.Columns.Add("���ݿ�", Type.GetType("System.String"));
            this.objDTStateDB.Columns.Add("��ʼʱ��", Type.GetType("System.String"));
            this.objDTStateDB.Columns.Add("����ʱ��", Type.GetType("System.String"));

            this.objDTStateTable.Columns.Add("���ݿ�", Type.GetType("System.String"));
            this.objDTStateTable.Columns.Add("���ݱ�", Type.GetType("System.String"));
            this.objDTStateTable.Columns.Add("Ŀǰ״̬", Type.GetType("System.String"));
            this.objDTStateTable.Columns.Add("��ʼʱ��", Type.GetType("System.String"));
            this.objDTStateTable.Columns.Add("����ʱ��", Type.GetType("System.String"));
            this.objDTStateTable.Columns.Add("��ʱ", Type.GetType("System.String"));

            this.objDSState.Tables.Add(this.objDTStateDB);
            this.objDSState.Tables.Add(this.objDTStateTable);

            this.objDSState.Relations.Add("ID", this.objDSState.Tables["Database"].Columns["���ݿ�"], this.objDSState.Tables["Table"].Columns["���ݿ�"]);

            this.ugDBDefragInfo.SetDataBinding(this.objDTStateDB, null);

            this.ugDBDefragInfo.DisplayLayout.Bands[1].Columns[1].Width = 200;
            this.ugDBDefragInfo.DisplayLayout.Bands[1].Columns[2].Width = 80;
            this.ugDBDefragInfo.DisplayLayout.Bands[1].Columns[3].Width = 180;
            this.ugDBDefragInfo.DisplayLayout.Bands[1].Columns[4].Width = 180;
            this.ugDBDefragInfo.DisplayLayout.Bands[1].Columns[5].Width = 100;
        }




        /// <summary>
        /// ������ݿ�����
        /// </summary>
        /// <param name="sInstance"></param>
        /// <param name="sUser"></param>
        /// <param name="sPassword"></param>
        /// <param name="bIntergrated"></param>
        /// <returns></returns>
        private System.Data.SqlClient.SqlConnection getConnection(string sInstance, string sDatabase, string sUser, string sPassword, bool bIntergrated)
        {
            try
            {
                SqlConnection objSQLConnect = new SqlConnection();

                string strConnection;

                strConnection = "Data Source=" + sInstance + ";Initial Catalog=" + sDatabase + ";" +
                               (bIntergrated ? "Integrated Security=true;" : "User ID=" + sUser + ";PassWord=" + sPassword + ";") +
                               "Application Name=̽����� MX ProcessProgram;Connection Timeout=600;Pooling=false;";

                objSQLConnect.ConnectionString = strConnection;
                objSQLConnect.Open();

                if (objSQLConnect.State != System.Data.ConnectionState.Open)
                {
                    System.Exception objExp = new Exception("�������ݿ�ʧ�ܡ�");
                    objSQLConnect = null;

                    throw (objExp);
                }

                return objSQLConnect;

            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        private SqlDataReader getAllDatabases(SqlConnection objConn)
        {
            string strSQL = "";

            SqlDataReader objDR = null;
            SqlCommand objSQLComm = new SqlCommand();

            try
            {
                objSQLComm = objConn.CreateCommand();
                objSQLComm.CommandTimeout = 600;

                objSQLComm.CommandText = "SELECT @@VERSION AS SQLVersion";
                objDR = objSQLComm.ExecuteReader();

                objDR.Read();
                if (objDR.GetString(0).IndexOf("SQL Server 2005") < 0)
                {
                    strSQL = "SELECT [name] AS DBNAME, DBID FROM sysdatabases WHERE [Name] NOT IN ('master', 'model', 'msdb', 'tempdb', 'AdventureWorks', 'AdventureWorksDW') ORDER BY Name ";
                }
                else
                {
                    strSQL = "SELECT [name] AS DBNAME, DBID FROM sys.sysdatabases WHERE [Name] NOT IN ('master', 'model', 'msdb', 'tempdb', 'AdventureWorks', 'AdventureWorksDW') ORDER BY Name ";
                }
                objDR.Close();
                objSQLComm.CommandText = strSQL;
                objDR = objSQLComm.ExecuteReader();

                return objDR;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                objSQLComm = null;
            }
        }

        private SqlDataReader getAllTables(SqlConnection objConn)
        {
            string strSQL = "";

            SqlDataReader objDR = null;
            SqlCommand objSQLComm = new SqlCommand();

            try
            {
                objSQLComm = objConn.CreateCommand();
                objSQLComm.CommandTimeout = 600;

                strSQL = @"if EXISTS(Select 1 Where @@version Like 'Microsoft SQL Server  2000 %')
	                                SELECT [NAME] FROM sysobjects WHERE [xtype] = 'U' Order By [NAME]
                            ELSE
	                                SELECT [NAME] FROM sys.tables WHERE [type] = 'U' Order By [NAME]";
                objSQLComm.CommandText = strSQL;
                objDR = objSQLComm.ExecuteReader();

                return objDR;
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                objSQLComm = null;
            }
        }


        /// <summary>
        /// ����ѡ�������ݿ����ݱ�
        /// </summary>
        private void DBDefrag()
        {
            try
            {
                string strDBName = "";
                Infragistics.Win.UltraWinTree.UltraTreeNode objTreeNode;

                this.intStop = 0;

                this.upbAllDB.Minimum = 0;
                this.upbAllDB.Maximum = this.cklDBList.CheckedItems.Count;

                this.upbAllDB.Value = 0;
                for (int i = 0; i < this.cklDBList.CheckedItems.Count; i++)
                {
                    strDBName = (string)this.cklDBList.Items[this.cklDBList.CheckedIndices[i]];

                    //������
                    this.objDTStateDB.Rows.Add(new string[] { strDBName, System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff"), "" });

                    objTreeNode = new Infragistics.Win.UltraWinTree.UltraTreeNode(strDBName, strDBName + "  ��ʼʱ�䣺" + System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff"));
                    objTreeNode.Override.NodeAppearance.Image = global::Upgrade.Properties.Resources.DB_Refresh;
                    this.utRunStateSimple.Nodes.Add(objTreeNode);

                    this.upbAllDB.Text = "�����������ݿ⣺" + strDBName + "����" + ((int)(i + 1)).ToString() + "������" + this.upbAllDB.Maximum.ToString() + "�������ȣ�[Value]/[Range]";
                    Application.DoEvents();

                    this.DefragSingleDB(strDBName, (string)((System.Collections.ArrayList)this.objDBFragInfo[strDBName])[0],
                                                   (System.Collections.ArrayList)((System.Collections.ArrayList)this.objDBFragInfo[strDBName])[2]);
                                        
                    //ֹͣ
                    if (this.intStop == 1) return;

                    this.objDTStateDB.Rows[i]["����ʱ��"] = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff");
                    this.utRunStateSimple.Nodes[strDBName].Text = this.utRunStateSimple.Nodes[strDBName].Text + "  ����ʱ�䣺" + System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff") ;

                    this.upbAllDB.Value = i + 1;
                    Application.DoEvents();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private void DefragSingleDB(string strDBName, string siIsSelectTables, System.Collections.ArrayList objSelectedTables)
        {
            SqlConnection objConn = new SqlConnection();
            SqlCommand objComm;
            SqlDataAdapter objDA = new SqlDataAdapter();
            DataTable objDT = new DataTable();

            try
            {
                objConn = this.getConnection(this.strInstance, strDBName, this.strUser, this.strPassword, this.blnIntegrated);
                if (objConn.State != ConnectionState.Open) throw (new Exception("�����ݿ����������⡣"));
                objComm = objConn.CreateCommand();
                objComm.CommandTimeout = 36000;

                this.upbOneDB.Minimum = 0;
                this.upbOneDB.Value = 0;

                if (siIsSelectTables == "1")
                {
                    this.upbOneDB.Maximum = objSelectedTables.Count ;
                    for (int i = 0; i < objSelectedTables.Count; i++)
                    {
                        this.DefragTableIndexAndStatistics(objComm, strDBName, (string)objSelectedTables[i]);
                    }
                }
                else if (siIsSelectTables == "0")
                {
                    objComm.CommandText = @"IF EXISTS(SELECT 1 WHERE @@VERSION LIKE 'Microsoft SQL Server  2000 %')
	                                            SELECT [NAME] FROM sysobjects WHERE [xtype] = 'U' Order By [NAME]
                                            ELSE
	                                            SELECT [NAME] FROM sys.tables WHERE [type] = 'U' Order By [NAME]";
                    objComm.ExecuteNonQuery();
                    objDA.SelectCommand = objComm;
                    objDA.Fill(objDT);

                    this.upbOneDB.Maximum = objDT.Rows.Count;
                    for (int i = 0; i < objDT.Rows.Count; i++)
                    {
                        this.DefragTableIndexAndStatistics(objComm, strDBName, (string)objDT.Rows[i][0]);
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            finally
            {
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn = null;

                objComm = null;
                objDA = null;
                objDT = null;
            }
        }

        private void DefragTableIndexAndStatistics(SqlCommand objComm, string strDBName, string strTableName)
        {
            try
            {
                DateTime objDT1, objDT2;
                objDT1 = System.DateTime.Now;
                //������
                System.Data.DataRow objRow = this.objDTStateTable.NewRow();
                objRow[0] = strDBName;
                objRow[1] = strTableName;
                objRow[2] = "�������� ......";
                objRow[3] = objDT1.ToString("yyyy-MM-dd hh:mm:ss:fff");
                objRow[4] = "......";
                objRow[5] = "......";
                this.objDTStateTable.Rows.Add(objRow);

                this.utRunStateSimple.Nodes[strDBName].Nodes.Add(strDBName + "-DBO-" + strTableName,
                                             strDBName + ".." + strTableName + "  ��ʼʱ�䣺" + System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff") + "  ");
                this.utRunStateSimple.Nodes[strDBName].Nodes[strDBName + "-DBO-" + strTableName].Override.NodeAppearance.Image = global::Upgrade.Properties.Resources.Table_1;
                this.utRunStateSimple.ActiveNode = this.utRunStateSimple.Nodes[strDBName].Nodes[strDBName + "-DBO-" + strTableName];

                this.ugDBDefragInfo.ActiveRow = (this.ugDBDefragInfo.GetRow(Infragistics.Win.UltraWinGrid.ChildRow.Last).GetChild(Infragistics.Win.UltraWinGrid.ChildRow.Last)).GetSibling(Infragistics.Win.UltraWinGrid.SiblingRow.Last);
                this.ugDBDefragInfo.Selected.Rows.Clear();
                this.ugDBDefragInfo.ActiveRow.Selected = true;

                Application.DoEvents();


                //=====================================================================
                objComm.CommandText = "DBCC DBREINDEX([" + strTableName + "])";
                objComm.ExecuteNonQuery();

                objComm.CommandText = "UPDATE STATISTICS [" + strTableName + "]";
                objComm.ExecuteNonQuery();
                //=====================================================================


                objDT2 = System.DateTime.Now;
                objRow[2] = "���";
                objRow[4] = objDT2.ToString("yyyy-MM-dd hh:mm:ss:fff");
                objRow[5] = (objDT2 - objDT1).TotalMilliseconds.ToString("#,#0") + "ms" + " �� " + (objDT2 - objDT1).TotalSeconds.ToString("#,#0") + "s";

                this.utRunStateSimple.Nodes[strDBName].Nodes[strDBName + "-DBO-" + strTableName].Text = (this.utRunStateSimple.Nodes[strDBName].Nodes[strDBName + "-DBO-" + strTableName].Text + "����ʱ�䣺" + System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff") + "");

                this.upbOneDB.Value = this.upbOneDB.Value + 1;
                this.ugDBDefragInfo.Rows.ExpandAll(true);                

                this.utRunStateSimple.ExpandAll();
                Application.DoEvents();

            }
            catch (Exception e)
            {
                throw e;
            }
        }



        #endregion



        private void frmDBReIndexandStatistics_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.intStop = 1;
        }




    }


















    //=====================================================================================
    //if (iIsAll == 1)
    //{
    //    for (int i = 0; i < this.lbSelectDBTables.Items.Count; i++)
    //    {
    //        objListTables.Add((string)this.lbSelectDBTables.Items[i]);
    //    }
    //}
    //else if (iIsAll == -1)
    //{
    //    for (int i = this.lbSelectDBTables.SelectedItems.Count - 1; i >= 0; i--)
    //    {
    //        objListTables.Add((string)this.lbSelectDBTables.SelectedItems[i]);

    //        objSelectedTables.RemoveAt(this.lbSelectDBTables.SelectedIndices[i]);
    //    }
    //}
    //objListTables.Sort();

    //this.lbDBTableAll.DataSource = null;
    //this.lbDBTableAll.DataSource = objListTables;
    //this.lbDBTableAll.Refresh();

    //if (iIsAll == 1) objSelectedTables.Clear();
    //this.lbSelectDBTables.DataSource = null;
    //this.lbSelectDBTables.DataSource = objSelectedTables;



}