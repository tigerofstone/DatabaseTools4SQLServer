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
                MessageBox.Show(exp.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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



        #region 控件事件处理

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
                    strInfo = "整理数据库完成！/r/n但整理过程发生问题：" + strDBIndexReError + "/r/n总共执行时间为： " + (objDT2 - objDT1).TotalMilliseconds.ToString("#,###.###") + " 毫秒。";
                }
                else
                {
                    strInfo = "整理数据库完成！\r\n总共执行时间为： " + (objDT2 - objDT1).TotalSeconds.ToString("#,###.###") + " 秒。";
                }

                MessageBox.Show(strInfo, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                if (this.intStop == 0)
                {
                    MessageBox.Show("整理数据库发生错误！/r/n" + "错误信息：" + exp.Message + "/r/n堆栈信息：" + exp.StackTrace, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("查看数据库完成！\r\n总共执行时间为： " + (objDT2 - objDT1).TotalSeconds.ToString("#,###.###") + " 秒。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                if (this.intStop == 0)
                {
                    MessageBox.Show("查看数据库发生错误！\r\n" + "错误信息：" + exp.Message + "\r\n堆栈信息：" + exp.StackTrace, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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



        #region  界面私有方法

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
        /// 填充数据库列表
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

        //设置选项
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
                MessageBox.Show("设置数据库选项错误：001！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //填充数据表列表
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
            //填充数据表列表
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

                //修改数据集
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
                this.objDTIXDB.Columns.Add("数据库", Type.GetType("System.String"));
                this.objDTIXDB.Columns.Add("开始时间", Type.GetType("System.String"));
                this.objDTIXDB.Columns.Add("结束时间", Type.GetType("System.String"));

                this.objDTIXTable.Columns.Add("数据库", Type.GetType("System.String"));
                this.objDTIXTable.Columns.Add("数据表名称", Type.GetType("System.String"));

                this.objDTIXTableIndex.Columns.Add("数据库", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("对象名称", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("索引名称", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("索引 ID", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("扫描页数", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("索引记录数", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("扫描区数", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("每个区的平均页数", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("扫描密度 [最佳计数:实际计数]", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("区扫描碎片", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("每页的平均可用字节数", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("平均页密度(满)", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("索引的级别", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("索引最小记录大小", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("索引最大记录大小", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("索引平均记录大小", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("索引被前推记录数", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("区切换次数", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("区更改理想数量", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("区更改实际数量", Type.GetType("System.String"));
                this.objDTIXTableIndex.Columns.Add("逻辑扫描碎片", Type.GetType("System.String"));

                this.objDSTBIndexInfo.Tables.Add(this.objDTIXDB);
                this.objDSTBIndexInfo.Tables.Add(this.objDTIXTable);
                this.objDSTBIndexInfo.Tables.Add(this.objDTIXTableIndex);

                this.objDSTBIndexInfo.Relations.Add("DBID", this.objDSTBIndexInfo.Tables["Database"].Columns["数据库"], this.objDSTBIndexInfo.Tables["Table"].Columns["数据库"]);
                this.objDSTBIndexInfo.Relations.Add("TBALEID",
                                                     new DataColumn[] { this.objDSTBIndexInfo.Tables["Table"].Columns["数据库"], this.objDSTBIndexInfo.Tables["Table"].Columns["数据表名称"] },
                                                     new DataColumn[] { this.objDSTBIndexInfo.Tables["Index"].Columns["数据库"], this.objDSTBIndexInfo.Tables["Index"].Columns["对象名称"] });


                this.ugTBIndexInfo.SetDataBinding(this.objDSTBIndexInfo, null);

                this.ugTBIndexInfo.DisplayLayout.Override.DefaultRowHeight = 22;
                this.ugTBIndexInfo.DisplayLayout.Bands[1].Columns["数据库"].Hidden = true;
                this.ugTBIndexInfo.DisplayLayout.Bands[2].Columns["数据库"].Hidden = true;
                this.ugTBIndexInfo.DisplayLayout.Bands[2].Columns["对象名称"].Hidden = true;


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

                    //设置树
                    this.objDTIXDB.Rows.Add(new string[] { strDBName, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "" });

                    this.upbAllDB.Text = "正在查询数据库：" + strDBName + "，第" + ((int)(i + 1)).ToString() + "个，共" + this.upbAllDB.Maximum.ToString() + "个，进度：[Value]/[Range]";
                    this.upbOneDB.Text = "正在整理数据表，第 [Value] 个，共 [Range] 个，进度：[Value]/[Range]，完成 [Percent]%";
                    Application.DoEvents();

                    this.ShowSingleDB(strDBName, (string)((System.Collections.ArrayList)this.objDBFragInfo[strDBName])[0],
                                                 (System.Collections.ArrayList)((System.Collections.ArrayList)this.objDBFragInfo[strDBName])[2]);

                    //停止
                    if (this.intStop == 1) return;

                    this.objDTIXDB.Rows[i]["结束时间"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");

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
                if (objConn.State != ConnectionState.Open) throw (new Exception("打开数据库连接有问题。"));
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
                            System.Diagnostics.Debugger.Log(0, null, "显示数据库索引错误：" + e.Message);                       
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
                            System.Diagnostics.Debugger.Log(0, null, "显示数据库索引错误：" + e.Message);
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
                                            对象名称 NVARCHAR(100),
                                            [对象名 ID] INT,
                                            索引名称 NVARCHAR(100),
                                            [索引 ID]   INT,
                                            索引的级别 INT,
                                            扫描页数 INT,
                                            索引记录数 INT,
                                            索引最小记录大小 FLOAT,
                                            索引最大记录大小 FLOAT,
                                            索引平均记录大小 FLOAT,
                                            索引被前推记录数 INT,
                                            扫描区数 INT,
                                            区切换次数 INT,
                                            每页的平均可用字节数 FLOAT,
                                            [平均页密度(满)] FLOAT,
                                            [扫描密度 [最佳计数:实际计数]]] FLOAT,
                                            区更改理想数量 INT,
                                            区更改实际数量 INT,
                                            逻辑扫描碎片 FLOAT,
                                            区扫描碎片 FLOAT
                                        )
                                        INSERT INTO #tracestatus  
                                            EXEC ('DBCC SHOWCONTIG([" + strTableName + @"]) WITH TABLERESULTS, ALL_INDEXES, NO_INFOMSGS')

                                        SELECT *, CAST(扫描页数 AS FLOAT) / (CASE CAST(扫描区数 AS FLOAT) WHEN 0 THEN 1 ELSE CAST(扫描区数 AS FLOAT) END) AS 每个区的平均页数 FROM #tracestatus ORDER BY 索引名称
                                           
                                        IF OBJECT_ID('TEMPDB..#tracestatus') IS NOT NULL 
                                            DROP TABLE #tracestatus";
                objComm.ExecuteNonQuery();
                objSQLDA.SelectCommand = objComm;
                objSQLDA.Fill(objSQLDT);

                for (int i = 0; i < objSQLDT.Rows.Count; i++)
                {
                    this.objDTIXTableIndex.Rows.Add(new string[] { strDBName,
                                                                   objSQLDT.Rows[i]["对象名称"].ToString(),
                                                                   objSQLDT.Rows[i]["索引名称"].ToString(), objSQLDT.Rows[i]["索引 ID"].ToString(),
                                                                   objSQLDT.Rows[i]["扫描页数"].ToString(), objSQLDT.Rows[i]["索引记录数"].ToString(),
                                                                   objSQLDT.Rows[i]["扫描区数"].ToString(), objSQLDT.Rows[i]["每个区的平均页数"].ToString(), 
                                                                   objSQLDT.Rows[i]["扫描密度 [最佳计数:实际计数]"].ToString(), objSQLDT.Rows[i]["区扫描碎片"].ToString(),
                                                                   objSQLDT.Rows[i]["每页的平均可用字节数"].ToString(), objSQLDT.Rows[i]["平均页密度(满)"].ToString(),
                                                                   objSQLDT.Rows[i]["索引的级别"].ToString(), objSQLDT.Rows[i]["索引最小记录大小"].ToString(),
                                                                   objSQLDT.Rows[i]["索引最大记录大小"].ToString(), objSQLDT.Rows[i]["索引平均记录大小"].ToString(),
                                                                   objSQLDT.Rows[i]["索引被前推记录数"].ToString(), objSQLDT.Rows[i]["区切换次数"].ToString(),
                                                                   objSQLDT.Rows[i]["区更改理想数量"].ToString(),
                                                                   objSQLDT.Rows[i]["区更改实际数量"].ToString(), objSQLDT.Rows[i]["逻辑扫描碎片"].ToString()
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



        #region  公共私有函数

        /// <summary>
        /// 初始化状态树数据源
        /// </summary>
        private void InitializeTreeDS()
        {
            this.objDTStateDB.Columns.Add("数据库", Type.GetType("System.String"));
            this.objDTStateDB.Columns.Add("开始时间", Type.GetType("System.String"));
            this.objDTStateDB.Columns.Add("结束时间", Type.GetType("System.String"));

            this.objDTStateTable.Columns.Add("数据库", Type.GetType("System.String"));
            this.objDTStateTable.Columns.Add("数据表", Type.GetType("System.String"));
            this.objDTStateTable.Columns.Add("目前状态", Type.GetType("System.String"));
            this.objDTStateTable.Columns.Add("开始时间", Type.GetType("System.String"));
            this.objDTStateTable.Columns.Add("结束时间", Type.GetType("System.String"));
            this.objDTStateTable.Columns.Add("耗时(毫秒)", Type.GetType("System.String"));
            this.objDTStateTable.Columns.Add("耗时(秒)", Type.GetType("System.String"));

            this.objDSState.Tables.Add(this.objDTStateDB);
            this.objDSState.Tables.Add(this.objDTStateTable);

            this.objDSState.Relations.Add("ID", this.objDSState.Tables["Database"].Columns["数据库"], this.objDSState.Tables["Table"].Columns["数据库"]);

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
        /// 获得数据库连接
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
                               "Application Name=探测程序 MX ProcessProgram;Connection Timeout=15;Pooling=false;";

                objSQLConnect.ConnectionString = strConnection;
                objSQLConnect.Open();

                if (objSQLConnect.State != System.Data.ConnectionState.Open)
                {
                    System.Exception objExp = new Exception("连接数据库失败。");
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
        /// 整理选定的数据库数据表
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

                    //设置树
                    this.objDTStateDB.Rows.Add(new string[] { strDBName, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "" });

                    objTreeNode = new Infragistics.Win.UltraWinTree.UltraTreeNode(strDBName, strDBName + "  开始时间：" + 
                                                                                             System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                    objTreeNode.Override.NodeAppearance.Image = global::Upgrade.Properties.Resources.DB_Refresh;
                    this.utRunStateSimple.Nodes.Add(objTreeNode);

                    this.upbAllDB.Text = "正在整理数据库：" + strDBName + "，第" + ((int)(i + 1)).ToString() + "个，共" + 
                                                              this.upbAllDB.Maximum.ToString() + "个，进度：[Value]/[Range]";

                    Application.DoEvents();

                    this.DefragSingleDB(strDBName, (string)((System.Collections.ArrayList)this.objDBFragInfo[strDBName])[0],
                                                   (System.Collections.ArrayList)((System.Collections.ArrayList)this.objDBFragInfo[strDBName])[2], 
                                                   out strReIndexError);
                                        
                    //停止
                    if (this.intStop == 1) return;

                    this.objDTStateDB.Rows[i]["结束时间"] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
                    this.utRunStateSimple.Nodes[strDBName].Text = this.utRunStateSimple.Nodes[strDBName].Text + "  结束时间：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") ;

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
                if (objConn.State != ConnectionState.Open) throw (new Exception("打开数据库连接有问题。"));
                objComm = objConn.CreateCommand();
                objComm.CommandTimeout = 36000;

                this.upbOneDB.Minimum = 0;
                this.upbOneDB.Value = 0;               

                if (siIsSelectTables == "1")
                {
                    this.upbOneDB.Maximum = objSelectedTables.Count ;

                    this.upbOneDB.Text = "正在整理数据表，第 [Value] 个，共 [Range] 个，进度：[Value]/[Range]，完成 [Percent]%";

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
                    this.upbOneDB.Text = "正在整理数据表，第 [Value] 个，共 [Range] 个，进度：[Value]/[Range]，完成 [Percent]%";

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
                //设置树
                System.Data.DataRow objRow = this.objDTStateTable.NewRow();
                objRow[0] = strDBName;
                objRow[1] = strTableName;
                objRow[2] = "正在整理 ......";
                objRow[3] = objDT1.ToString("yyyy-MM-dd HH:mm:ss:fff");
                objRow[4] = "......";
                objRow[5] = "......";
                this.objDTStateTable.Rows.Add(objRow);

                this.utRunStateSimple.Nodes[strDBName].Nodes.Add(strDBName + "-DBO-" + strTableName,
                                             strDBName + ".." + strTableName + "  开始时间：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "  ");
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
                objRow[2] = "完成";
                objRow[4] = objDT2.ToString("yyyy-MM-dd HH:mm:ss:fff");
                objRow[5] = (objDT2 - objDT1).TotalMilliseconds.ToString("#,#0") + "ms";
                objRow[6] = (objDT2 - objDT1).TotalSeconds.ToString("#,#0") + "s";

                this.utRunStateSimple.Nodes[strDBName].Nodes[strDBName + "-DBO-" + strTableName].Text = (this.utRunStateSimple.Nodes[strDBName].Nodes[strDBName + "-DBO-" + strTableName].Text + "结束时间：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "");

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