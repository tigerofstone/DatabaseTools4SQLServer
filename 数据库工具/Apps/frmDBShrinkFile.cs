using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Upgrade.Apps
{
    public partial class frmDBShrinkFile : Form
    {
        public string strConnection = "";
        public string strInstance = "";
        public string strUser = "";
        public string strPassword = "";
        public bool blnIntegrated = false;

        private System.Data.SqlClient.SqlConnection _objSQLConn = new System.Data.SqlClient.SqlConnection();

        private System.Collections.Hashtable _htDBFileInfo = new System.Collections.Hashtable();
        private System.Collections.Hashtable _htDBFilShrink = new System.Collections.Hashtable();

        private BackgroundWorker bkWorker = new BackgroundWorker();  

        public frmDBShrinkFile()
        {
            InitializeComponent();

            this.InitBKWorker();
        }

        private void frmDBShrinkFile_Load(object sender, EventArgs e)
        {
            try
            {
                InitFormInfo();

                this.frmDBShrinkFile_Resize(null, null);
            }
            catch (Exception E)
            {
                MessageBox.Show("运行出现错误：" + E.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void InitFormInfo()
        {
            try
            {
                Upgrade.AppClass.PublicDBCls objDBCls = new AppClass.PublicDBCls();
                this._objSQLConn = objDBCls.getSQLConnection(this.strInstance, "MASTER", this.strUser, this.strPassword, this.blnIntegrated);

                Upgrade.AppClass.DBDatabases objDB = new AppClass.DBDatabases();
                System.Data.DataTable objDT = objDB.getUserDatabase(this._objSQLConn);

                this.InitDBList(objDT);

                objDT = null;
                objDBCls = null;
                objDB = null;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private void InitDBList(System.Data.DataTable objDT)
        {
            try
            {
                this.clbDatabases.Items.Clear();
                if (objDT.Rows.Count > 0)
                {
                    for (int i = 0; i < objDT.Rows.Count; i++)
                    {
                        this.clbDatabases.Items.Add(objDT.Rows[i][0].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("初始化数据库列表出错。\r\n" + e.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel_SelectFileType_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                                    new Rectangle(panel_SelectFileType.ClientRectangle.X, panel_SelectFileType.ClientRectangle.Y,
                                                  panel_SelectFileType.ClientRectangle.Width, panel_SelectFileType.ClientRectangle.Height),
                                    Color.Gainsboro, ButtonBorderStyle.Solid);
        }

        private void panel_Main_Paint(object sender, PaintEventArgs e)
        {
            
            e.Graphics.Clear(this.panel_Main.BackColor);
            
            //ControlPaint.DrawBorder(e.Graphics,
            //                        new Rectangle(panel_Main.ClientRectangle.X, panel_Main.ClientRectangle.Y,
            //                                      panel_Main.ClientRectangle.Width, panel_Main.ClientRectangle.Height),
            //                        Color.Gainsboro, ButtonBorderStyle.Solid);

            e.Graphics.DrawRectangle(new Pen(Color.Gainsboro, 1), new Rectangle(0, 0,
                                                  panel_Main.ClientRectangle.Width - 1, panel_Main.ClientRectangle.Height - 1));

            e.Graphics.DrawRectangle(new Pen(Color.Gainsboro, 1), new Rectangle(clbDatabases.Location.X - 1, clbDatabases.Location.Y - 1,
                                                  clbDatabases.ClientRectangle.Width + 2, clbDatabases.ClientRectangle.Height + 2));            

            e.Graphics.DrawRectangle(new Pen(Color.Gainsboro, 1), new Rectangle(dvwDBFiles.Location.X - 1, dvwDBFiles.Location.Y - 1,
                                                  dvwDBFiles.ClientRectangle.Width + 1, dvwDBFiles.ClientRectangle.Height + 2));
        }

        private void dvwDBFiles_Paint(object sender, PaintEventArgs e)
        {

        }


        private void frmDBShrinkFile_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void frmDBShrinkFile_Resize(object sender, EventArgs e)
        {
            this.clbDatabases.Height = this.Height - 205;
            this.dvwDBFiles.Height = this.clbDatabases.Height + 2;
            this.dvwDBFiles.Width = this.Width - 324;

            this.upbActive.Width = this.Width - 60;
            this.upbShrinkFileProgress.Width = this.Width - 60;

            this.upbActive.Top = this.clbDatabases.Height + 86 + 14;
            this.upbShrinkFileProgress.Top = this.clbDatabases.Height + 86 + 27 +22;

            this.panel_Main.Height = this.Height - 44;
            this.panel_Main.Width = this.Width - 28;

            this.panel_Main.Refresh();
        }





        private void frmDBShrinkFile_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.CloseBKWorker();
            //this.Close();
        }



        private void InitBKWorker()
        {
            //背景进程处理收缩数据库
            this.bkWorker.WorkerReportsProgress = true;
            this.bkWorker.WorkerSupportsCancellation = true;
            this.bkWorker.DoWork += new DoWorkEventHandler(bkWorker_DoWork);
            this.bkWorker.ProgressChanged += new ProgressChangedEventHandler(ProgessChanged);
            this.bkWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteWork);
        }

        private void CloseBKWorker()
        {
            if (this.bkWorker != null)
            {
                if (this.bkWorker.IsBusy) this.bkWorker.CancelAsync();
                this.bkWorker.DoWork -= new DoWorkEventHandler(bkWorker_DoWork);
                this.bkWorker.ProgressChanged -= new ProgressChangedEventHandler(ProgessChanged);
                this.bkWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(CompleteWork);
                this.bkWorker = null;
            }
        }



        //===============================================================================================================================================
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.CloseBKWorker();
            this.Close();
        }


       

        //=====  控件事件 =======================================================================
        

        
        private void clbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this._htDBFileInfo.ContainsKey(this.clbDatabases.SelectedItem.ToString().Trim()))
                {
                    Upgrade.AppClass.DBDatabases objDB = new AppClass.DBDatabases();

                    System.Data.DataTable objDT = new DataTable();
                    objDT = objDB.getUserDatabaseFileInfo(this._objSQLConn, this.clbDatabases.SelectedItem.ToString().Trim());

                    this.HashTableDBInfoDeal(this.clbDatabases.SelectedItem.ToString().Trim(), objDT, this.cbShrinkLog.Checked, this.cbShrinkData.Checked);
                }
                this.ShowDBInfoList(this.clbDatabases.SelectedItem.ToString().Trim(), this.cbShrinkLog.Checked, this.cbShrinkData.Checked);
            }
            catch (Exception E)
            {
                MessageBox.Show("获取数据库文件列表出错，可能是此数据库有问题或状态有问题。\r\n" + E.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clbDatabases_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (e.CurrentValue == CheckState.Unchecked && e.NewValue == CheckState.Checked)
                {
                    this.SetShrinkFileList();
                }
                if (e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
                {
                    if (this._htDBFilShrink.ContainsKey(((string)this.clbDatabases.SelectedItem).Trim()))
                    {
                        this._htDBFilShrink.Remove(((string)this.clbDatabases.SelectedItem).Trim());
                    }
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("设置数据库文件列表出错，可能是此数据库有问题或状态有问题。\r\n" + E.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void dvwDBFiles_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //this.SetShrinkFile(this.clbDatabases.SelectedItem.ToString().Trim());
        }

     

        private void dvwDBFiles_Leave(object sender, EventArgs e)
        {
            //this.dvwDBFiles.ReadOnly = true;
            //this.SetShrinkFile(this.clbDatabases.SelectedItem.ToString().Trim());
            //this.dvwDBFiles.ReadOnly = false;
        }

        private void SetShrinkFile(string sDBName)
        {
            if (this.dvwDBFiles.Rows.Count > 0 && this.dvwDBFiles.CurrentRow != null)
            {
                bool blnRowSelect = false;
                Upgrade.AppClass.DBSingleFileInfo[] objDBFile;

                objDBFile = ((Upgrade.AppClass.DBSingleFileInfo[])this._htDBFileInfo[sDBName]);
                for (int i = 0; i < objDBFile.Length; i++)
                {
                    blnRowSelect = Convert.ToBoolean(this.dvwDBFiles[0, i].Value);
                    ((Upgrade.AppClass.DBSingleFileInfo[])this._htDBFileInfo[sDBName])[i].bSelect = blnRowSelect;                    
                }               
            }
        }

        private void cbShrinkLog_CheckedChanged(object sender, EventArgs e)
        {
            this.SetSelectState();
        }

        private void cbShrinkData_CheckedChanged(object sender, EventArgs e)
        {
            this.SetSelectState();
        }



        private void btnShrinkFile_Click(object sender, EventArgs e)
        {
            try
            {
                this.upbShrinkFileProgress.Maximum = this._htDBFilShrink.Count;
                this.upbShrinkFileProgress.Value = 0;

                this.ControlState(false);

                bkWorker.RunWorkerAsync(new object[] { this._objSQLConn, this._htDBFilShrink, this.cbShrinkLog.Checked, this.cbShrinkData.Checked, 
                                                   this.nudLogPercent.Value, this.nudDataPercent.Value, this.nudLogSpace.Value, this.nudDataSpace.Value });

                for (int i = 1; i <= 100; i++)
                {
                    this.upbActive.Value = i;

                    System.Threading.Thread.Sleep(100);
                    {
                        if (i == 100)
                        {
                            i = 1;
                        }
                    }
                    if (this.bkWorker == null || (this.bkWorker != null && !this.bkWorker.IsBusy)) break;

                    Application.DoEvents();
                }
                this.ControlState(true);
            }
            catch (Exception E)
            {
                MessageBox.Show("收缩数据库出现系统问题。\r\n" + (string)E.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void ControlState(bool bState)
        {
            this.btnShrinkFile.Enabled = bState;
            this.clbDatabases.Enabled = bState;
            this.dvwDBFiles.Enabled = bState;
            this.cbShrinkData.Enabled = bState;
            this.cbShrinkLog.Enabled = bState;
            this.nudLogSpace.Enabled = bState;
            this.nudDataSpace.Enabled = bState;

            this.panel3.Enabled = bState;
            this.panel4.Enabled = bState;

            if (bState)
            {
                //this.upbActive.Value = 0;
                this.upbActive.Text = "完成收缩数据库，请选择其他数据库或文件收缩......";

                this.upbActive.Value = 0;
            }
            else
            {
                this.upbActive.Value = 0;
                this.upbActive.Text = "正在执行收缩数据库，请稍等......";
            }
        }
        



        //=== 业务私有函数======

        private void SetSelectState()
        {
            if (this.clbDatabases.SelectedIndex >= 0)
            {
                this.ShowDBInfoList(this.clbDatabases.SelectedItem.ToString().Trim(), this.cbShrinkLog.Checked, this.cbShrinkData.Checked);
            }
            this.SetShrinkFileSelectedList();
        }

        private void SetShrinkFileSelectedList()
        {
            bool bSelect;
            string strDBName = "";
            Upgrade.AppClass.DBSingleFileInfo[] objDBFile;

            foreach (System.Collections.DictionaryEntry entry in this._htDBFileInfo)
            {
                objDBFile = ((Upgrade.AppClass.DBSingleFileInfo[])entry.Value);
                for (int i = 0; i < objDBFile.Length; i++)
                {
                    bSelect = ((this.cbShrinkLog.Checked && objDBFile[i].iFileType == 1) || (this.cbShrinkData.Checked && objDBFile[i].iFileType == 0));
                    objDBFile[i].bSelect = bSelect;
                }
            }

            System.Collections.Generic.Dictionary<string, Upgrade.AppClass.DBSingleFileInfo> objDatabase;
            foreach (System.Collections.DictionaryEntry entry in this._htDBFilShrink)
            {
                strDBName = (string)entry.Key;
                objDatabase = ((System.Collections.Generic.Dictionary<string, Upgrade.AppClass.DBSingleFileInfo>)this._htDBFilShrink[strDBName]);
                objDatabase.Clear();

                objDBFile = ((Upgrade.AppClass.DBSingleFileInfo[])this._htDBFileInfo[strDBName]);
                for (int i = 0; i < objDBFile.Length; i++)
                {
                    if (objDBFile[i].bSelect)
                        objDatabase.Add(objDBFile[i].sLogicName, objDBFile[i]);
                }
            }
        }



        private void SetShrinkFileList()
        {
            System.Collections.Generic.Dictionary<string, Upgrade.AppClass.DBSingleFileInfo> objDatabase = new System.Collections.Generic.Dictionary<string, Upgrade.AppClass.DBSingleFileInfo>();
            Upgrade.AppClass.DBSingleFileInfo[] objDBFile;

            objDBFile = (Upgrade.AppClass.DBSingleFileInfo[])this._htDBFileInfo[((string)this.clbDatabases.SelectedItem).Trim()];
            for (int i = 0; i < objDBFile.Length; i++)
            {
                if (objDBFile[i].bSelect)
                    objDatabase.Add(objDBFile[i].sLogicName, objDBFile[i]);
            }
            if (this._htDBFilShrink.ContainsKey(((string)this.clbDatabases.SelectedItem).Trim()))
            {
                this._htDBFilShrink.Remove(((string)this.clbDatabases.SelectedItem).Trim());
            }
            this._htDBFilShrink.Add(((string)this.clbDatabases.SelectedItem).Trim(), objDatabase);            
        }

        

        private void HashTableDBInfoDeal(string strDatabaseName, System.Data.DataTable objDT, bool bLog, bool bData)
        {
            if(objDT.Rows.Count > 0)
            {
                Upgrade.AppClass.DBSingleFileInfo[] objDBFile = new Upgrade.AppClass.DBSingleFileInfo[objDT.Rows.Count];
                for (int i = 0; i < objDT.Rows.Count; i++)
                {
                    objDBFile[i].sDatabaseName = strDatabaseName;
                    objDBFile[i].iFileID = (int)objDT.Rows[i]["FILE_ID"];
                    objDBFile[i].iFileType = int.Parse(((Byte)objDT.Rows[i]["TYPE"]).ToString());
                    objDBFile[i].sFileTypeDesc = (string)objDT.Rows[i]["Type_Desc"];
                    objDBFile[i].sLogicName = (string)objDT.Rows[i]["Logic_NAME"];
                    objDBFile[i].sPhysicalName = (string)objDT.Rows[i]["Physical_Name"];
                    objDBFile[i].dFileSize = (double)objDT.Rows[i]["Size"];
                    objDBFile[i].bSelect = ((bLog && int.Parse(((Byte)objDT.Rows[i]["TYPE"]).ToString()) == 1) || (bData && int.Parse(((Byte)objDT.Rows[i]["TYPE"]).ToString()) == 0));
                }
                this._htDBFileInfo.Add(strDatabaseName, objDBFile);
            }
        }

        private void ShowDBInfoList(string strDatabaseName, bool bLog, bool bData)
        {
            this.dvwDBFiles.Rows.Clear();

            Upgrade.AppClass.DBSingleFileInfo[] objDBFile;

            objDBFile = (Upgrade.AppClass.DBSingleFileInfo[])this._htDBFileInfo[strDatabaseName];

            for (int i = 0; i < objDBFile.Length; i++)
            {
                objDBFile[i].bSelect = ((bLog && objDBFile[i].iFileType == 1) || (bData && objDBFile[i].iFileType == 0));

                this.dvwDBFiles.Rows.Add(1);
                this.dvwDBFiles.Rows[i].Cells[0].Value = objDBFile[i].bSelect;
                //this.dvwDBFiles.Rows[i].Cells[0].ReadOnly = !((bLog && objDBFile[i].iFileType == 1) || (bData && objDBFile[i].iFileType == 0));
                this.dvwDBFiles.Rows[i].Cells[1].Value = objDBFile[i].sLogicName;
                this.dvwDBFiles.Rows[i].Cells[2].Value = objDBFile[i].sFileTypeDesc;
                this.dvwDBFiles.Rows[i].Cells[3].Value = objDBFile[i].dFileSize;
                this.dvwDBFiles.Rows[i].Cells[4].Value = objDBFile[i].sPhysicalName;
            }

            this.dvwDBFiles.Refresh();
        }



        //======================================================================================================================================
        public void bkWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 事件处理，指定处理函数   
            BackgroundWorker bkworker = sender as BackgroundWorker;
            try
            {
                string sError = "";
                DateTime objDTBegin = DateTime.Now;

                //string strInstance = "";
                //string strUser = "";
                //string strPassword = "";
                //bool blnIntegrated = false;
                System.Collections.Generic.Dictionary<string, Upgrade.AppClass.DBSingleFileInfo> objDatabaseLogicName;
                Upgrade.AppClass.DBSingleFileInfo objDBFile;
                Upgrade.AppClass.DBDatabases objDBObject = new AppClass.DBDatabases();
                object[] Arg = (object[])e.Argument;
                System.Data.SqlClient.SqlConnection objConn;

                int iShrinkLogFile2Space = 1000;          int iShrinkDataFile2Space = 1000;
                int iShrinkLogFile2Percent = 10;          int iShrinkDataFile2Percent = 10;
                string strDatabaseName = "";              string strDatabaseLogicName = "";
                int intDBFileType = 1;
                string strSQLVersion = "2005";

                int ipercentComplete = 0;
                System.Collections.Hashtable htShrinkDBFile = (System.Collections.Hashtable)(Arg[1]);
                objConn = (System.Data.SqlClient.SqlConnection)(Arg[0]);
                iShrinkLogFile2Percent = int.Parse(((Decimal)Arg[4]).ToString());
                iShrinkDataFile2Percent = int.Parse(((Decimal)Arg[5]).ToString());
                iShrinkLogFile2Space = int.Parse(((Decimal)Arg[6]).ToString());
                iShrinkDataFile2Space = int.Parse(((Decimal)Arg[7]).ToString());

                strSQLVersion = objDBObject.getSQLServerVersion(objConn);
                
                foreach (System.Collections.DictionaryEntry entry in htShrinkDBFile)
                {
                    if (bkworker.CancellationPending)
                    {
                        e.Cancel = true;
                    }
                    ipercentComplete = ipercentComplete + 1;
                    strDatabaseName = ((string)entry.Key);
                    objDatabaseLogicName = (System.Collections.Generic.Dictionary<string, Upgrade.AppClass.DBSingleFileInfo>)entry.Value;

                    bkworker.ReportProgress(ipercentComplete, strDatabaseName);
                    foreach (System.Collections.Generic.KeyValuePair<string, Upgrade.AppClass.DBSingleFileInfo> DBLogicName in objDatabaseLogicName)
                    {
                        strDatabaseLogicName = ((string)DBLogicName.Key).Trim();
                        objDBFile = ((Upgrade.AppClass.DBSingleFileInfo)DBLogicName.Value);
                        intDBFileType = objDBFile.iFileType;

                        try
                        {
                            objDBObject.ShrinkDatabaseFile(objConn, strSQLVersion, strDatabaseName, strDatabaseLogicName, intDBFileType,
                                                           iShrinkLogFile2Percent, iShrinkDataFile2Percent, iShrinkLogFile2Space, iShrinkDataFile2Space);
                        }
                        catch (Exception ee)
                        {
                            sError = sError + "\r\n 数据库：" + strDatabaseName + "，逻辑文件：" + strDatabaseLogicName + "，出现问题：" + ee.Message;
                        }
                    }
                    System.Threading.Thread.Sleep(200);
                    
                }
                string iDoWorkSecend = (double.Parse((DateTime.Now - objDTBegin).TotalMilliseconds.ToString()) / 1000).ToString("#,0.000");
                if (sError != "")
                {
                    MessageBox.Show("收缩数据库出现问题。\r\n" + sError, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                e.Result = iDoWorkSecend;
            }
            catch (Exception E)
            {
                e.Cancel = true;
                MessageBox.Show("收缩数据库出现问题。\r\n" + E.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        public void ProgessChanged(object sender, ProgressChangedEventArgs e)
        {
            this.upbShrinkFileProgress.Value = e.ProgressPercentage;
            this.upbShrinkFileProgress.Text = "正在收缩数据库：[" + ((string)e.UserState).Trim() + "]。    第 [Value] 个  共 [Range] 个";
        }

        public void CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("收缩数据库完成，共耗时：" + (string)e.Result + " 秒。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.ControlState(true);
        }

        
        

        

        

        

        

        

        

        

        

        
        

        
        
        

        

        
    }
}
