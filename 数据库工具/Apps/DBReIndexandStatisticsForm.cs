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
    public partial class DBReIndexandStatisticsFrom : Form
    {
        public string strConnection = "";
        public string strInstance = "";
        public string strUser = "";
        public string strPassword = "";
        public bool blnIntegrated = false;

        private string strSQLVersion = "";

        private System.Collections.Hashtable objDBFragInfo = new System.Collections.Hashtable();

        private DataSet objDSState = new DataSet();
        private DataTable objDTStateDB = new DataTable("Database");
        private DataTable objDTStateTable = new DataTable("Table");

        private int intStop = 0;

        public DBReIndexandStatisticsFrom()
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

                this.strSQLVersion = getSQLVersion(objSQLConnect.CreateCommand());

                this.groupBox_SelectTable.Enabled = false;

                this.lbDBTableAll.Focus();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
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

            this.groupBox_SelectDBTable.Height = this.Height - 16;
            this.groupBox_SelectTable.Height = this.Height - 290;
            this.lbDBTableAll.Height = this.Height - 355;
            this.lbSelectDBTables.Height = this.Height - 355;
        }

        private void groupBox_SelectDBTable_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Gainsboro, 1), new Rectangle(cklDBList.Location.X - 1, cklDBList.Location.Y - 1,
                                      cklDBList.ClientRectangle.Width + 2, cklDBList.ClientRectangle.Height + 2));
        }
        private void groupBox_SelectTable_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Gainsboro, 1), new Rectangle(lbDBTableAll.Location.X - 1, lbDBTableAll.Location.Y - 1,
                                                  lbDBTableAll.ClientRectangle.Width + 2, lbDBTableAll.ClientRectangle.Height + 2));
            e.Graphics.DrawRectangle(new Pen(Color.Gainsboro, 1), new Rectangle(lbSelectDBTables.Location.X - 1, lbSelectDBTables.Location.Y - 1,
                                                  lbSelectDBTables.ClientRectangle.Width + 2, lbSelectDBTables.ClientRectangle.Height + 2));
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
                this.groupBox_SelectTable.Enabled = true;
            }
            else
            {
                this.ClearDBFragInfo();

                this.groupBox_SelectTable.Enabled = false;
            }
        }

        private void cklDBList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (this.cklDBList.GetItemCheckState(this.cklDBList.SelectedIndex) == CheckState.Checked)
            //{
            //    this.ListDBTables();
            //    this.groupBox_SelectTable.Enabled = true;
            //}
            //else
            //{
            //    this.ClearDBFragInfo();

            //    this.groupBox_SelectTable.Enabled = false;
            //}
            this.cklDBList_SelectedIndexChanged(sender, e);
        }

        private void cklDBList_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.cklDBList_SelectedIndexChanged(sender, e);
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
            string strDBIndexReError = "";
            string strInfo = "";
            DateTime objDT1, objDT2;
            objDT1 = System.DateTime.Now;
            
            try
            {
                this.SetControlState(false);
                this.utcMainInfo.Tabs[0].Selected = true;

                this.DBDefrag(out strDBIndexReError);

                this.SetControlState(true);

                if (this.objDTStateTable.Rows.Count > 0)
                {
                    this.ugDBDefragInfo.ActiveRow = (this.ugDBDefragInfo.GetRow(Infragistics.Win.UltraWinGrid.ChildRow.Last).GetChild(Infragistics.Win.UltraWinGrid.ChildRow.Last)).GetSibling(Infragistics.Win.UltraWinGrid.SiblingRow.Last);
                    this.ugDBDefragInfo.Selected.Rows.Clear();
                    this.ugDBDefragInfo.ActiveRow.Selected = true;
                }
                objDT2 = System.DateTime.Now;

                if (strDBIndexReError != "")
                {
                    strInfo = "�������ݿ���ɣ�/r/n��������̷������⣺" + strDBIndexReError + "/r/n�ܹ�ִ��ʱ��Ϊ�� " + (objDT2 - objDT1).TotalMilliseconds.ToString("#,###.###") + " ���롣";
                }
                else
                {
                    strInfo = "�������ݿ���ɣ�\r\n�ܹ�ִ��ʱ��Ϊ�� " + (objDT2 - objDT1).TotalSeconds.ToString("#,###.###") + " �롣";
                }

                MessageBox.Show(strInfo, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnShowIndexInfo_Click(object sender, EventArgs e)
        {
            DateTime objDT1, objDT2;
            objDT1 = System.DateTime.Now;

            try
            {
                this.SetControlState(false);
                this.utcMainInfo.Tabs[2].Selected = true;
                //this.ultraTabPageControl3.Show();
                this.ShowTableIndexInfo();

                this.SetControlState(true);

                if (this.objDTStateTable.Rows.Count > 0)
                {
                    this.ugTBIndexInfo.ActiveRow = (this.ugTBIndexInfo.GetRow(Infragistics.Win.UltraWinGrid.ChildRow.Last).GetChild(Infragistics.Win.UltraWinGrid.ChildRow.Last)).GetSibling(Infragistics.Win.UltraWinGrid.SiblingRow.Last).GetChild(Infragistics.Win.UltraWinGrid.ChildRow.Last).GetSibling(Infragistics.Win.UltraWinGrid.SiblingRow.Last);
                    this.ugTBIndexInfo.Selected.Rows.Clear();
                    this.ugTBIndexInfo.ActiveRow.Selected = true;

                    this.ugTBIndexInfo.Rows.ExpandAll(true);
                }
                objDT2 = System.DateTime.Now;
                MessageBox.Show("�鿴���ݿ���ɣ�\r\n�ܹ�ִ��ʱ��Ϊ�� " + (objDT2 - objDT1).TotalSeconds.ToString("#,###.###") + " �롣", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                if (this.intStop == 0)
                {
                    MessageBox.Show("�鿴���ݿⷢ������\r\n" + "������Ϣ��" + exp.Message + "\r\n��ջ��Ϣ��" + exp.StackTrace, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            
        }


        private void SetControlState(bool bState)
        {

            this.btnRunConfig.Enabled = bState;
            this.cklDBList.Enabled = bState;
            this.groupBox_SelectTable.Enabled = bState;
            this.btnShowIndexInfo.Enabled = bState;

            if (!bState)
            {
                this.SetListBoxIndex(-1);

                this.objDTStateTable.Clear();
                this.objDTStateDB.Clear();
                this.utRunStateSimple.Nodes.Clear();
            }

            //this.btnRunConfig.Enabled = true;
            //this.cklDBList.Enabled = true;
            //this.groupBox_SelectTable.Enabled = true;
            //this.btnShowIndexInfo.Enabled = true;
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
                objReader.Close();
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


        private DataSet objDSTBIndexInfo = new DataSet();
        private DataTable objDTIXDB = new DataTable("Database");
        private DataTable objDTIXTable = new DataTable("Table");
        private DataTable objDTIXTableIndex = new DataTable("Index");

        int intInitIndexFlag = 0;

        private void InitializeIndexDS()
        {
            if (this.intInitIndexFlag == 0)
            {
                this.objDTIXDB.Columns.Add("���ݿ�", Type.GetType("System.String"));
                this.objDTIXDB.Columns.Add("��ʼʱ��", Type.GetType("System.String"));
                this.objDTIXDB.Columns.Add("����ʱ��", Type.GetType("System.String"));

                this.objDTIXTable.Columns.Add("���ݿ�", Type.GetType("System.String"));
                this.objDTIXTable.Columns.Add("���ݱ�����", Type.GetType("System.String"));

                this.objDTIXTableIndex.Columns.Add("���ݿ�", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("��������", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("��������", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("���� ID", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("ɨ��ҳ��", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("������¼��", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("ɨ������", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("ÿ������ƽ��ҳ��", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("ɨ���ܶ� [��Ѽ���:ʵ�ʼ���]", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("��ɨ����Ƭ", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("ÿҳ��ƽ�������ֽ���", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("ƽ��ҳ�ܶ�(��)", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("�����ļ���", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("������С��¼��С", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("��������¼��С", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("����ƽ����¼��С", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("������ǰ�Ƽ�¼��", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("���л�����", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("��������������", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("������ʵ������", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("�߼�ɨ����Ƭ", Type.GetType("System.String"));

                this.objDSTBIndexInfo.Tables.Add(this.objDTIXDB);
                this.objDSTBIndexInfo.Tables.Add(this.objDTIXTable);
                this.objDSTBIndexInfo.Tables.Add(this.objDTIXTableIndex);

                this.objDSTBIndexInfo.Relations.Add("DBID", this.objDSTBIndexInfo.Tables["Database"].Columns["���ݿ�"], this.objDSTBIndexInfo.Tables["Table"].Columns["���ݿ�"]);
                this.objDSTBIndexInfo.Relations.Add("TBALEID",
                                                     new DataColumn[] { this.objDSTBIndexInfo.Tables["Table"].Columns["���ݿ�"], this.objDSTBIndexInfo.Tables["Table"].Columns["���ݱ�����"] },
                                                     new DataColumn[] { this.objDSTBIndexInfo.Tables["Index"].Columns["���ݿ�"], this.objDSTBIndexInfo.Tables["Index"].Columns["��������"] });


                this.ugTBIndexInfo.SetDataBinding(this.objDSTBIndexInfo, null);

                this.ugTBIndexInfo.DisplayLayout.Override.DefaultRowHeight = 22;
                this.ugTBIndexInfo.DisplayLayout.Bands[1].Columns["���ݿ�"].Hidden = true;
                this.ugTBIndexInfo.DisplayLayout.Bands[2].Columns["���ݿ�"].Hidden = true;
                this.ugTBIndexInfo.DisplayLayout.Bands[2].Columns["��������"].Hidden = true;


                this.ugTBIndexInfo.DisplayLayout.Bands[0].Columns[0].Width = 300;
                this.ugTBIndexInfo.DisplayLayout.Bands[0].Columns[2].Width = 200;
                this.ugTBIndexInfo.DisplayLayout.Bands[0].Columns[2].Width = 200;

                this.ugTBIndexInfo.DisplayLayout.Bands[1].Columns[1].Width = 300;
                this.ugTBIndexInfo.DisplayLayout.Bands[2].Columns[2].Width = 200;

                this.intInitIndexFlag = 1;
            }
            this.objDTIXTableIndex.Clear();
            this.objDTIXTable.Clear();
            this.objDTIXDB.Clear();
        }

        
        private void ShowTableIndexInfo()
        {
            try
            {
                string strDBName = "";

                this.intStop = 0;

                this.upbAllDB.Minimum = 0;
                this.upbAllDB.Maximum = this.cklDBList.CheckedItems.Count;

                this.upbAllDB.Value = 0;
                this.InitializeIndexDS();

                for (int i = 0; i < this.cklDBList.CheckedItems.Count; i++)
                {
                    strDBName = (string)this.cklDBList.Items[this.cklDBList.CheckedIndices[i]];

                    //������
                    this.objDTIXDB.Rows.Add(new string[] { strDBName, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "" });

                    this.upbAllDB.Text = "���ڲ�ѯ���ݿ⣺" + strDBName + "����" + ((int)(i + 1)).ToString() + "������" + this.upbAllDB.Maximum.ToString() + "�������ȣ�[Value]/[Range]";
                    this.upbOneDB.Text = "�����������ݱ��� [Value] ������ [Range] �������ȣ�[Value]/[Range]����� [Percent]%";
                    Application.DoEvents();

                    this.ShowSingleDB(strDBName, (string)((System.Collections.ArrayList)this.objDBFragInfo[strDBName])[0],
                                                 (System.Collections.ArrayList)((System.Collections.ArrayList)this.objDBFragInfo[strDBName])[2]);

                    //ֹͣ
                    if (this.intStop == 1) return;

                    this.objDTIXDB.Rows[i]["����ʱ��"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");

                    this.upbAllDB.Value = i + 1;
                    Application.DoEvents();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        
        }

        private void ShowSingleDB(string strDBName, string siIsSelectTables, System.Collections.ArrayList objSelectedTables)
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
                objComm.CommandTimeout = 600;

                this.upbOneDB.Minimum = 0;
                this.upbOneDB.Value = 0;

                if (siIsSelectTables == "1")
                {
                    this.upbOneDB.Maximum = objSelectedTables.Count;
                    for (int i = 0; i < objSelectedTables.Count; i++)
                    {
                        try
                        {
                            this.ShowTableIndex(objComm, strDBName, (string)objSelectedTables[i]);;
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debugger.Log(0, null, "��ʾ���ݿ���������" + e.Message);                       
                            continue;
                        }                      
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
                        try
                        {
                            this.ShowTableIndex(objComm, strDBName, (string)objDT.Rows[i][0]);
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debugger.Log(0, null, "��ʾ���ݿ���������" + e.Message);
                            continue;
                        }
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

        private void ShowTableIndex(SqlCommand objComm, string strDBName, string strTableName)
        {
            System.Data.DataTable objSQLDT = new DataTable();
            System.Data.SqlClient.SqlDataAdapter objSQLDA = new SqlDataAdapter();

            try
            {
                if(strDBName == "UFMeta_003") 
                       objComm.CommandText = "";
                this.objDTIXTable.Rows.Add(new string[] { strDBName, strTableName });
                //=====================================================================
                objComm.CommandText = @"IF OBJECT_ID('TEMPDB..#tracestatus') IS NOT NULL 
                                            DROP TABLE #tracestatus
                                        CREATE TABLE #tracestatus 
                                        (
                                            �������� NVARCHAR(100),
                                            [������ ID] INT,
                                            �������� NVARCHAR(100),
                                            [���� ID]   INT,
                                            �����ļ��� INT,
                                            ɨ��ҳ�� INT,
                                            ������¼�� INT,
                                            ������С��¼��С FLOAT,
                                            ��������¼��С FLOAT,
                                            ����ƽ����¼��С FLOAT,
                                            ������ǰ�Ƽ�¼�� INT,
                                            ɨ������ INT,
                                            ���л����� INT,
                                            ÿҳ��ƽ�������ֽ��� FLOAT,
                                            [ƽ��ҳ�ܶ�(��)] FLOAT,
                                            [ɨ���ܶ� [��Ѽ���:ʵ�ʼ���]]] FLOAT,
                                            �������������� INT,
                                            ������ʵ������ INT,
                                            �߼�ɨ����Ƭ FLOAT,
                                            ��ɨ����Ƭ FLOAT
                                        )
                                        INSERT INTO #tracestatus  
                                            EXEC ('DBCC SHOWCONTIG([" + strTableName + @"]) WITH TABLERESULTS, ALL_INDEXES, NO_INFOMSGS')

                                        SELECT *, CAST(ɨ��ҳ�� AS FLOAT) / (CASE CAST(ɨ������ AS FLOAT) WHEN 0 THEN 1 ELSE CAST(ɨ������ AS FLOAT) END) AS ÿ������ƽ��ҳ�� FROM #tracestatus ORDER BY ��������
                                           
                                        IF OBJECT_ID('TEMPDB..#tracestatus') IS NOT NULL 
                                            DROP TABLE #tracestatus";
                objComm.ExecuteNonQuery();
                objSQLDA.SelectCommand = objComm;
                objSQLDA.Fill(objSQLDT);

                for (int i = 0; i < objSQLDT.Rows.Count; i++)
                {
                    this.objDTIXTableIndex.Rows.Add(new string[] { strDBName,
                                                                   objSQLDT.Rows[i]["��������"].ToString(),
                                                                   objSQLDT.Rows[i]["��������"].ToString(), objSQLDT.Rows[i]["���� ID"].ToString(),
                                                                   objSQLDT.Rows[i]["ɨ��ҳ��"].ToString(), objSQLDT.Rows[i]["������¼��"].ToString(),
                                                                   objSQLDT.Rows[i]["ɨ������"].ToString(), objSQLDT.Rows[i]["ÿ������ƽ��ҳ��"].ToString(), 
                                                                   objSQLDT.Rows[i]["ɨ���ܶ� [��Ѽ���:ʵ�ʼ���]"].ToString(), objSQLDT.Rows[i]["��ɨ����Ƭ"].ToString(),
                                                                   objSQLDT.Rows[i]["ÿҳ��ƽ�������ֽ���"].ToString(), objSQLDT.Rows[i]["ƽ��ҳ�ܶ�(��)"].ToString(),
                                                                   objSQLDT.Rows[i]["�����ļ���"].ToString(), objSQLDT.Rows[i]["������С��¼��С"].ToString(),
                                                                   objSQLDT.Rows[i]["��������¼��С"].ToString(), objSQLDT.Rows[i]["����ƽ����¼��С"].ToString(),
                                                                   objSQLDT.Rows[i]["������ǰ�Ƽ�¼��"].ToString(), objSQLDT.Rows[i]["���л�����"].ToString(),
                                                                   objSQLDT.Rows[i]["��������������"].ToString(),
                                                                   objSQLDT.Rows[i]["������ʵ������"].ToString(), objSQLDT.Rows[i]["�߼�ɨ����Ƭ"].ToString()
                                                                  });
                }
                //=====================================================================
                this.upbOneDB.Value = this.upbOneDB.Value + 1;

                this.ugTBIndexInfo.ActiveRow = (this.ugTBIndexInfo.GetRow(Infragistics.Win.UltraWinGrid.ChildRow.Last).GetChild(Infragistics.Win.UltraWinGrid.ChildRow.Last)).GetSibling(Infragistics.Win.UltraWinGrid.SiblingRow.Last).GetChild(Infragistics.Win.UltraWinGrid.ChildRow.Last).GetSibling(Infragistics.Win.UltraWinGrid.SiblingRow.Last);
                this.ugTBIndexInfo.Selected.Rows.Clear();
                this.ugTBIndexInfo.ActiveRow.Selected = true;

                this.ugTBIndexInfo.Rows.ExpandAll(true);

                Application.DoEvents();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                objSQLDT.Clear();
                objSQLDT = null;

            }
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
            this.objDTStateTable.Columns.Add("��ʱ(����)", Type.GetType("System.String"));
            this.objDTStateTable.Columns.Add("��ʱ(��)", Type.GetType("System.String"));

            this.objDSState.Tables.Add(this.objDTStateDB);
            this.objDSState.Tables.Add(this.objDTStateTable);

            this.objDSState.Relations.Add("ID", this.objDSState.Tables["Database"].Columns["���ݿ�"], this.objDSState.Tables["Table"].Columns["���ݿ�"]);

            this.ugDBDefragInfo.SetDataBinding(this.objDTStateDB, null);

            this.ugDBDefragInfo.DisplayLayout.Bands[1].Columns[0].Width = 60;
            this.ugDBDefragInfo.DisplayLayout.Bands[1].Columns[1].Width = 200;
            this.ugDBDefragInfo.DisplayLayout.Bands[1].Columns[2].Width = 60;
            this.ugDBDefragInfo.DisplayLayout.Bands[1].Columns[3].Width = 150;
            this.ugDBDefragInfo.DisplayLayout.Bands[1].Columns[4].Width = 150;
            this.ugDBDefragInfo.DisplayLayout.Bands[1].Columns[5].Width = 70;
            this.ugDBDefragInfo.DisplayLayout.Bands[1].Columns[6].Width = 60;
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
                               "Application Name=̽����� MX ProcessProgram;Connection Timeout=15;Pooling=false;";

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
        private void DBDefrag(out string strReIndexError)
        {
            try
            {
                string strDBName = "";
                Infragistics.Win.UltraWinTree.UltraTreeNode objTreeNode;

                strReIndexError = "";

                this.intStop = 0;
                this.upbAllDB.Minimum = 0;
                this.upbAllDB.Maximum = this.cklDBList.CheckedItems.Count;

                this.upbAllDB.Value = 0;
                for (int i = 0; i < this.cklDBList.CheckedItems.Count; i++)
                {
                    strDBName = (string)this.cklDBList.Items[this.cklDBList.CheckedIndices[i]];

                    //������
                    this.objDTStateDB.Rows.Add(new string[] { strDBName, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "" });

                    objTreeNode = new Infragistics.Win.UltraWinTree.UltraTreeNode(strDBName, strDBName + "  ��ʼʱ�䣺" + 
                                                                                             System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                    objTreeNode.Override.NodeAppearance.Image = global::Upgrade.Properties.Resources.DB_Refresh;
                    this.utRunStateSimple.Nodes.Add(objTreeNode);

                    this.upbAllDB.Text = "�����������ݿ⣺" + strDBName + "����" + ((int)(i + 1)).ToString() + "������" + 
                                                              this.upbAllDB.Maximum.ToString() + "�������ȣ�[Value]/[Range]";

                    Application.DoEvents();

                    this.DefragSingleDB(strDBName, (string)((System.Collections.ArrayList)this.objDBFragInfo[strDBName])[0],
                                                   (System.Collections.ArrayList)((System.Collections.ArrayList)this.objDBFragInfo[strDBName])[2], 
                                                   out strReIndexError);
                                        
                    //ֹͣ
                    if (this.intStop == 1) return;

                    this.objDTStateDB.Rows[i]["����ʱ��"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
                    this.utRunStateSimple.Nodes[strDBName].Text = this.utRunStateSimple.Nodes[strDBName].Text + "  ����ʱ�䣺" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") ;

                    this.upbAllDB.Value = i + 1;
                    Application.DoEvents();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private void DefragSingleDB(string strDBName, string siIsSelectTables, System.Collections.ArrayList objSelectedTables, out string strError)
        {
            SqlConnection objConn = new SqlConnection();
            SqlCommand objComm;
            SqlDataAdapter objDA = new SqlDataAdapter();
            DataTable objDT = new DataTable();

            strError = "";

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

                    this.upbOneDB.Text = "�����������ݱ��� [Value] ������ [Range] �������ȣ�[Value]/[Range]����� [Percent]%";

                    for (int i = 0; i < objSelectedTables.Count; i++)
                    {
                        try
                        {
                            this.DefragTableIndexAndStatistics(objComm, strDBName, (string)objSelectedTables[i], this.strSQLVersion);
                        }
                        catch(Exception e)
                        {
                            strError = strError + e.Message + "\r\n";
                        }
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
                    this.upbOneDB.Text = "�����������ݱ��� [Value] ������ [Range] �������ȣ�[Value]/[Range]����� [Percent]%";

                    for (int i = 0; i < objDT.Rows.Count; i++)
                    {
                        try
                        {
                            this.DefragTableIndexAndStatistics(objComm, strDBName, (string)objDT.Rows[i][0], this.strSQLVersion);
                        }
                        catch (Exception e)
                        {
                            strError = strError + e.Message + "\r\n";
                        }
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

        private void DefragTableIndexAndStatistics(SqlCommand objComm, string strDBName, string strTableName, string strSQLVer)
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
                objRow[3] = objDT1.ToString("yyyy-MM-dd HH:mm:ss:fff");
                objRow[4] = "......";
                objRow[5] = "......";
                this.objDTStateTable.Rows.Add(objRow);

                this.utRunStateSimple.Nodes[strDBName].Nodes.Add(strDBName + "-DBO-" + strTableName,
                                             strDBName + ".." + strTableName + "  ��ʼʱ�䣺" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "  ");
                this.utRunStateSimple.Nodes[strDBName].Nodes[strDBName + "-DBO-" + strTableName].Override.NodeAppearance.Image = global::Upgrade.Properties.Resources.Table_1;
                this.utRunStateSimple.ActiveNode = this.utRunStateSimple.Nodes[strDBName].Nodes[strDBName + "-DBO-" + strTableName];

                this.ugDBDefragInfo.ActiveRow = (this.ugDBDefragInfo.GetRow(Infragistics.Win.UltraWinGrid.ChildRow.Last).GetChild(Infragistics.Win.UltraWinGrid.ChildRow.Last)).GetSibling(Infragistics.Win.UltraWinGrid.SiblingRow.Last);
                this.ugDBDefragInfo.Selected.Rows.Clear();
                this.ugDBDefragInfo.ActiveRow.Selected = true;

                Application.DoEvents();

                //=====================================================================
                int isExist = 0;
                System.Data.SqlClient.SqlDataReader objSQLDR;

                objComm.CommandText = "select [name] from sys.sysobjects where xtype = 'u' and [name] = '" + strTableName + "'";
                objSQLDR = objComm.ExecuteReader();
                if (objSQLDR.Read())
                {
                    isExist = 1;
                }
                objSQLDR.Close();

                if (isExist == 1)
                {
                    if (strSQLVer == "2000")
                        objComm.CommandText = "DBCC DBREINDEX([" + strTableName + "])";
                    else
                        objComm.CommandText = "ALTER INDEX ALL ON [" + strTableName + "] REBUILD";
                    objComm.ExecuteNonQuery();

                    objComm.CommandText = "UPDATE STATISTICS [" + strTableName + "]";
                    objComm.ExecuteNonQuery();
                }                
                //=====================================================================


                objDT2 = System.DateTime.Now;
                objRow[2] = "���";
                objRow[4] = objDT2.ToString("yyyy-MM-dd HH:mm:ss:fff");
                objRow[5] = (objDT2 - objDT1).TotalMilliseconds.ToString("#,#0") + "ms";
                objRow[6] = (objDT2 - objDT1).TotalSeconds.ToString("#,#0") + "s";

                this.utRunStateSimple.Nodes[strDBName].Nodes[strDBName + "-DBO-" + strTableName].Text = (this.utRunStateSimple.Nodes[strDBName].Nodes[strDBName + "-DBO-" + strTableName].Text + "����ʱ�䣺" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "");

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

        private string getSQLVersion(SqlCommand objComm)
        {
            System.Data.SqlClient.SqlDataReader objDR = null;
            try
            {
                objComm.CommandText = @"SELECT 1 AS Ver WHERE @@VERSION LIKE 'Microsoft SQL Server  2000 %'";
                objDR = objComm.ExecuteReader();

                if (objDR.Read())
                    return "2000";
                else
                    return "200X";
            }
            catch (Exception e)
            {
                return "";
                throw e;
            }
            finally
            {
                if (objDR != null)
                {
                    if (!objDR.IsClosed) objDR.Close();
                }
                objDR = null;
            }
        }

        #endregion



        private void frmDBReIndexandStatistics_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.intStop = 1;
        }

        private void groupBox_SelectDBTable_Enter(object sender, EventArgs e)
        {

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