using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Upgrade.Apps
{
    public partial class ProcessBlockedForm : Form
    {
        public string strConnection = "";
        public string strInstance = "";
        public SqlConnection objSQLConnect = new SqlConnection();

        private DataTable objDTResult;

        public ProcessBlockedForm()
        {
            InitializeComponent();
        }

        private void ProcessBlockedForm_Load(object sender, EventArgs e)
        {
            try
            {
                //objSQLConnect.ConnectionString = this.strConnection;
                //objSQLConnect.Open();

                //if (objSQLConnect.State != System.Data.ConnectionState.Open)
                //{
                //    System.Exception objExp = new Exception("连接数据库失败。");

                //    strConnection = "";

                //    throw (objExp);
                //}

                this.OpenConnection();

                this.InitForm();
                this.InitFormStyle();
                this.tcProcess.TabPages.Remove(this.tabPage6);
            }
            catch (System.Exception E)
            {
                throw (E);
            }

            ProcessBlockedForm_Resize(sender, e);
            this.Text = "探测数据库进程阻塞   数据库实例：" + this.strInstance + "  本机名称：" + System.Net.Dns.GetHostName();

        }

        private void OpenConnection()
        {
            try
            {
                objSQLConnect.ConnectionString = this.strConnection;
                objSQLConnect.Open();

                if (objSQLConnect.State != System.Data.ConnectionState.Open)
                {
                    System.Exception objExp = new Exception("连接数据库失败。");

                    strConnection = "";

                    throw (objExp);
                }
            }
            catch (System.Exception E)
            {
                throw (E);
            }
        }


        private void InitFormStyle()
        {
            //tcInfo.
        }



        private void InitForm()
        {
            CommonOperation objCommOperation = new CommonOperation();
            //初始化数据阻塞对象页签对象
            System.Collections.ArrayList htRecType = new System.Collections.ArrayList();

            htRecType = objCommOperation.LockRequestTypeCollect();
            this.cbRequestLockType.DataSource = htRecType;
            this.cbRequestLockType.DisplayMember = "DisplayLockRequestType";

            System.Collections.ArrayList ALRscType = new System.Collections.ArrayList();
            ALRscType = objCommOperation.LockRecourceTypeCollect();
            this.cbResourceLockType.DataSource = ALRscType;
            this.cbResourceLockType.DisplayMember = "Display";

            System.Collections.ArrayList alReqStatus = new System.Collections.ArrayList();
            alReqStatus = objCommOperation.RequestStatusColect();
            this.cbReqStatus.DataSource = alReqStatus;
            this.cbReqStatus.DisplayMember = "Display";

            this.ugDBLockRecourse.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ugDBLockRecourse.DisplayLayout.Override.AllowMultiCellOperations = Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.All;

            this.cbSQLWtiteToFile_CheckedChanged(null, null);
            this.cbBlockSQLWriteToFile_CheckedChanged(null, null);

            this.cbProcessesOrderBy.SelectedIndex = 1;
            this.cbSQLProcOrderBy.SelectedIndex = 1;

            if (Upgrade.MainClass.strStartType == "2") this.btnCleanBlocked.Visible = true;


            
        }


        //private void tpProcessRefresh_Paint(object sender, PaintEventArgs e)
        //{
        //    //Rectangle rec = new Rectangle(this.cbProcessesOrderBy.Location.X + 4, this.cbProcessesOrderBy.Location.Y - 2,
        //    //                              this.cbProcessesOrderBy.Width + 1, this.cbProcessesOrderBy.Height + 1);

        //    //// 标签背景填充颜色，也可以是图片 
        //    //SolidBrush bru = new SolidBrush(Color.DarkGray);
        //    //Pen penDrw = new Pen(bru);
        //    //e.Graphics.DrawRectangle(penDrw, rec);
        //}


        private void ProcessBlockedForm_Resize(object sender, EventArgs e)
        {
            this.tcProcess.Top = 73;
            this.tcProcess.Left = 0;
            this.tcProcess.Height = this.tcInfo.Height - 98;
            this.tcProcess.Width = this.tcInfo.Width - 4;

            this.dgBlockPrecess.Top = 40;
            this.dgBlockPrecess.Left = 0;
            this.dgBlockPrecess.Height = this.tcInfo.Height - 95;
            this.dgBlockPrecess.Width = this.tcInfo.Width - 21;

            this.ugDBLockRecourse.Top = 100;
            this.ugDBLockRecourse.Left = 0;
            this.ugDBLockRecourse.Height = this.tcInfo.Height - 128;
            this.ugDBLockRecourse.Width = this.tcInfo.Width - 10;

            this.gbSQLProcInfo.Top = 72;
            this.gbSQLProcInfo.Left = 4;
            this.gbSQLProcInfo.Height = this.tcInfo.Height - 98;
            this.gbSQLProcInfo.Width = this.tcInfo.Width - 15;

            this.tcBlocked.Top = 80;
            this.tcBlocked.Left = 4;
            this.tcBlocked.Height = this.tcInfo.Height - 80;
            this.tcBlocked.Width = this.tcInfo.Width - 15;

            this.rtbSQLLang.Top = 41;
            this.rtbSQLLang.Left = 6;
            //this.rtbSQLLang.Width = this.tabControl1.Width - 18;
            //this.rtbSQLLang.Height = this.tabControl1.Height - 75;


        }


        private void tcInfo_DrawItem(object sender, DrawItemEventArgs e)
        {
            this.TabControl_DrawItem_User(this.tcInfo, sender, e);
        }
        private void tcProcess_DrawItem(object sender, DrawItemEventArgs e)
        {
            this.TabControl_DrawItem_User(this.tcProcess, sender, e);
        }
        private void tcBlocked_DrawItem(object sender, DrawItemEventArgs e)
        {
            this.TabControl_DrawItem_User(this.tcBlocked, sender, e);
        }
        
        private void TabControl_DrawItem_User(TabControl Control, object sender, DrawItemEventArgs e)
        {
            //获取TabControl主控件的工作区域 
            Rectangle rec = Control.ClientRectangle;

            //新建一个StringFormat对象，用于对标签文字的布局设置 
            StringFormat StrFormat = new StringFormat();
            StrFormat.LineAlignment = StringAlignment.Center;// 设置文字垂直方向居中 
            StrFormat.Alignment = StringAlignment.Center;// 设置文字水平方向居中          

            // 标签背景填充颜色，也可以是图片 
            SolidBrush bru = new SolidBrush(System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239))))));
            SolidBrush bruFont = new SolidBrush(Color.FromArgb(0, 0, 0));// 标签字体颜色 
            Font font = new System.Drawing.Font("宋体", 9F);//设置标签字体样式 
            e.Graphics.FillRectangle(bru, rec);
           
            //绘制标签样式 
            //SolidBrush bruBroder = new SolidBrush(Color.Silver);
            //Pen penDrwBorder = new Pen(bruBroder);
            for (int i = 0; i < Control.TabPages.Count; i++)
            {
                //获取标签头的工作区域 
                Rectangle recChild = Control.GetTabRect(i);
                ////绘制标签头背景颜色 
                e.Graphics.FillRectangle(bru, recChild);
                //绘制标签头的文字 
                e.Graphics.DrawString(Control.TabPages[i].Text, font, bruFont, recChild, StrFormat);

                
            }
        }

        private void tabPage5_Paint(object sender, PaintEventArgs e)
        {
            //SolidBrush bruBroder = new SolidBrush(Color.Silver);
            //Pen penDrwBorder = new Pen(bruBroder);

            //tabPage5.

            //recChild = new Rectangle(3, 21, Control.TabPages[i].Width - 4, Control.TabPages[i].Height - 1);
            //e.Graphics.DrawRectangle(penDrwBorder, recChild);
        }

        //private void cbProcessesOrderBy_DrawItem(object sender, DrawItemEventArgs e)
        //{

        //}



        private void tcProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }



        private void btnProcessRefresh_Click(object sender, EventArgs e)
        {
            System.Data.DataTable objDT = new DataTable();
            System.Data.SqlClient.SqlDataAdapter objSQLDA = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();

            if (this.cbTextSQL.Checked) this.DisplaySQLProcLang(
                              getProcessesExcuteInfo((this.cbProcessRun.Checked ? 1 : 0), (this.cbIncludeMeProcess.Checked ? 1 : 0), 
                                                     (this.cbIncludeMeProg.Checked ? 1 : 0), this.cbProcessesOrderBy.Text, this.dgProcess, this.cbListProcess));
            
            if(this.cbListRunSQL.Checked)getProcessesExcuteInfoTable((this.cbProcessRun.Checked ? 1 : 0));

        }

        private void DisplaySQLProcLang(System.Text.StringBuilder _sbSQLText)
        {
            
            if (_sbSQLText != null)
            {
                this.rtbProcessInfo.Text = _sbSQLText.ToString();
                if (this.cbBlockSQLWriteToFile.Checked && this.tbFileName.Text.Trim() != "")
                {
                    System.IO.StreamWriter objSW;
                    if (System.IO.File.Exists(this.tbFileName.Text.Trim()))
                        objSW = new System.IO.StreamWriter(this.tbFileName.Text.Trim(), true, System.Text.Encoding.UTF8);
                    else
                        objSW = System.IO.File.CreateText(this.tbFileName.Text.Trim());

                    objSW.WriteLine(_sbSQLText.ToString());
                    objSW.Close();
                }
            }
        }

        private void btnLockRecourseQuery_Click(object sender, EventArgs e)
        {
            System.Data.DataTable objDT = new DataTable();

            objDT = this.getTransactionLockRecourse();

            this.ugDBLockRecourse.DataSource = objDT;
        }

        private void btnSQLProcShow_Click(object sender, EventArgs e)
        {
            this.ugSQLProcInfo.DataSource = this.getSQLProcessInfo(int.Parse(this.nudSQLProcTop.Value.ToString()), 
                                                                   this.cbProcessesOrderBy.SelectedIndex + 1, 
                                                                   (this.rbSQLProcDESC.Checked ? "DESC" : "ASC"));
            this.ugSQLProcInfo.DisplayLayout.Bands[0].Columns[0].Hidden = true;
            this.ugSQLProcInfo.DisplayLayout.Bands[0].Columns[1].Format = "yyyy-MM-dd HH:mm:ss:fff";
            this.ugSQLProcInfo.DisplayLayout.Bands[0].Columns[1].Width = 170;
            this.ugSQLProcInfo.DisplayLayout.Bands[0].Columns[2].Format = "yyyy-MM-dd HH:mm:ss:fff";
            this.ugSQLProcInfo.DisplayLayout.Bands[0].Columns[2].Width = 170;
            this.ugSQLProcInfo.DisplayLayout.Bands[0].Columns[16].Width = 360;
        }

        private void btnCleanBlocked_Click(object sender, EventArgs e)
        {
            this.KillBlockProcesses();
        }



        private void ugSQLProcInfo_AfterRowActivate(object sender, EventArgs e)
        {
            this.rbSQLProcSQL.Text = (string)this.ugSQLProcInfo.ActiveRow.Cells[16].Value;
        }

        

        private void ProcessBlockedForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (objSQLConnect.State == System.Data.ConnectionState.Open)
            {
                objSQLConnect.Close();
                objSQLConnect = null;
            }
        }


        System.Data.DataTable objDTTemp = new DataTable();
        System.Data.DataTable objDT = new DataTable();            
        private void btnBlockPreRefresh_Click(object sender, EventArgs e)
        {
            if (objDT != null)
            {
                objDT.Clear();
                objDT = null;
            }
            if (objDTTemp != null)
            {
                objDTTemp.Clear();
                objDTTemp = null;
            }

            string strBlockedSQLLag = "";

            objDT = this.getBlockedProcess();
            this.dgBlockPrecess.DataSource = objDT;
            this.dgBlockPrecess.Refresh();

            strBlockedSQLLag = getBlockedSQLLang(objDT);

            if (this.cbSortByBlock.Checked)
            {
                objDTResult = new DataTable();
                //================================================================================
                objDTResult.Columns.Add("进程 ID", Type.GetType("System.Int32"));
                objDTResult.Columns.Add("阻塞进程 ID", Type.GetType("System.Int32"));
                objDTResult.Columns.Add("CPU 耗时", Type.GetType("System.Int32"));
                objDTResult.Columns.Add("磁盘 IO", Type.GetType("System.Int32"));
                objDTResult.Columns.Add("客户端名称", Type.GetType("System.String"));
                objDTResult.Columns.Add("数据库", Type.GetType("System.String"));
                objDTResult.Columns.Add("执行程序名称", Type.GetType("System.String"));
                objDTResult.Columns.Add("等待时间", Type.GetType("System.Int32"));
                objDTResult.Columns.Add("引起阻塞的客户端名称", Type.GetType("System.String"));
                //================================================================================
                this.dgBlockPrecess.DataSource = objDT;
                this.dgBlockPrecess.Refresh();
            }
            this.rtbSQLLang.Text = strBlockedSQLLag;

            ///写文件
            this.writeBlockInfo(this.tbBlockFile.Text.Trim(), strBlockedSQLLag);
        }

        private void writeBlockInfo(string sFile, string sInfoInput)
        {
            if (sInfoInput != "" && sFile != "")
            {
                System.IO.StreamWriter objSW;
                if (System.IO.File.Exists(sFile))
                    objSW = new System.IO.StreamWriter(sFile, true, System.Text.Encoding.UTF8);
                else
                    objSW = System.IO.File.CreateText(sFile);

                objSW.WriteLine(sInfoInput);
                objSW.Close();
            }
        }



        private System.Data.DataTable getBlockedProcess()
        {
            System.Data.DataTable objDT = new DataTable();

            System.Data.SqlClient.SqlDataAdapter objSQLDA = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();

            string strSQL;

            strSQL = @"Declare @Tmp table(spid int, blocked int, cpu int, physical_io bigint, hostname nvarchar(128), program_name nvarchar(128), waittime bigint, dbid int)
                       Declare @TmpBLS table(spid int, blocked int, cpu int, physical_io bigint, hostname nvarchar(128), program_name nvarchar(128), waittime bigint, dbid int)
                       Declare @TmpBLB table(spid int, blocked int, cpu int, physical_io bigint, hostname nvarchar(128), program_name nvarchar(128), waittime bigint, dbid int)

                       Insert into @Tmp(spid, blocked, cpu, physical_io, hostname, program_name, dbid, waittime)
                       Select spid, blocked, cpu, physical_io, hostname, program_name, dbid, waittime from MASTER..sysprocesses WITH (NOLOCK) 

                       Insert into @TmpBLS(spid, blocked, cpu, physical_io, hostname, program_name, dbid, waittime)
                       Select spid, blocked, cpu, physical_io, hostname, program_name, dbid, waittime from MASTER..sysprocesses WITH (NOLOCK) 
                       Where blocked <> 0
                       
                       Insert into @TmpBLS(spid, blocked, cpu, physical_io, hostname, program_name, dbid, waittime)
                       Select spid, blocked, cpu, physical_io, hostname, program_name, dbid, waittime From @Tmp 
                       Where spid IN (SELECT blocked FROM @TmpBLS WHERE blocked NOT IN (SELECT spid FROM @TmpBLS)) AND spid NOT IN (SELECT spid FROM @TmpBLS)

                       Select spid AS [进程 ID], blocked AS [阻塞进程 ID], 
                              cpu AS [CPU 耗时], physical_io [磁盘 IO], hostname AS 客户端名称, 
                              (Select top 1 name From MASTER..sysdatabases Where dbid = BLS.dbid) AS [数据库], 
                              program_name AS 执行程序名称, waittime AS [等待时间],
                              (Select top 1 hostname From @Tmp Where spid = BLS.spid) AS [引起阻塞的客户端名称] 
                       From @TmpBLS BLS Order By blocked, spid";

            objSQLComm = this.objSQLConnect.CreateCommand();
            objSQLComm.CommandTimeout = 600;
            objSQLComm.CommandText = strSQL;
            objSQLComm.ExecuteNonQuery();

            objDT.Clear();
            objDT.TableName = "SP_WHO_Process";
            objSQLDA.SelectCommand = objSQLComm;
            objSQLDA.Fill(objDT);

            return objDT;
        }

        private void getBlockInfo(System.Data.DataTable objDT)
        {
            System.Data.DataTable objDTTemp = new DataTable();
            System.Data.DataView objDTView = new DataView();
            System.Data.DataRow objDRow;

            objDTTemp = objDT.Copy();

            objDTView = objDTTemp.DefaultView;
            objDTView.RowFilter = "[阻塞进程 ID] = 0";

            for (int i = 0; i < objDTView.Count; i++)
            {
                int intSPID = 0;

                objDRow = setResultDataRow(objDTView, objDTView[i].Row, i);
                objDTResult.Rows.Add(objDRow);

                intSPID = (int)objDT.Rows[i]["进程 ID"];
                
                getBlockedLinkInfo(objDT, intSPID);
            }

            //=========================================================================
            objDTView = objDTTemp.DefaultView;
            objDTView.RowFilter = "[进程 ID] = [阻塞进程 ID]";
            for (int i = 0; i < objDTView.Count; i++)
            {
                objDRow = setResultDataRow(objDTView, objDTView[i].Row, i);
                objDTResult.Rows.Add(objDRow);
            }
        }

        private void getBlockedLinkInfo(System.Data.DataTable objDT, int intSPID)
        {
            try
            {
                System.Data.DataView objDTV = new DataView();
                System.Data.DataTable objDTR = new DataTable();
                System.Data.DataRow objDRow;

                objDTR = objDT.Copy();
                objDTV = objDTR.DefaultView;
                objDTV.RowFilter = "[阻塞进程 ID] = '" + intSPID.ToString() + "'";
                if (objDTV.Count != 0)
                {
                    for (int i = 0; i < objDTV.Count; i++)
                    {
                        objDRow = setResultDataRow(objDTV, objDTV[i].Row, i);
                        objDTResult.Rows.Add(objDRow);

                        getBlockedLinkInfo(objDT, (int)objDTV[i]["进程 ID"]);
                    }

                }
            }
            catch(System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private System.Data.DataRow setResultDataRow(System.Data.DataView objDTV, System.Data.DataRow objData, int intIndex)
        {
            System.Data.DataRow objDRow = objDTResult.NewRow();
            objDRow["进程 ID"] = objDTV[intIndex]["进程 ID"];
            objDRow["阻塞进程 ID"] = objDTV[intIndex]["阻塞进程 ID"];
            objDRow["CPU 耗时"] = objDTV[intIndex]["CPU 耗时"];
            objDRow["磁盘 IO"] = objDTV[intIndex]["磁盘 IO"];
            objDRow["客户端名称"] = objDTV[intIndex]["客户端名称"];
            objDRow["数据库"] = objDTV[intIndex]["数据库"];
            objDRow["执行程序名称"] = objDTV[intIndex]["执行程序名称"];
            objDRow["等待时间"] = objDTV[intIndex]["等待时间"];
            objDRow["引起阻塞的客户端名称"] = objDTV[intIndex]["引起阻塞的客户端名称"];

            return objDRow;
        }
        
        private String getBlockedSQLLang(System.Data.DataTable objDT)
        {
            try
            {
                System.Data.DataTable objDTSQL = new DataTable();
                System.Data.DataTable objDTBuffer = new DataTable();
                System.Data.SqlClient.SqlDataAdapter objSQLDA = new System.Data.SqlClient.SqlDataAdapter();
                System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();

                string strSQLLang = "";
                string strSQLProcess = "";

                objSQLComm = this.objSQLConnect.CreateCommand();
                objSQLComm.CommandTimeout = 600;

                objSQLComm.CommandText = "SELECT GETDATE() AS DOTIME";
                objSQLComm.ExecuteNonQuery();

                objDTSQL.Clear();
                objDTSQL.TableName = "TEMP";
                objSQLDA.SelectCommand = objSQLComm;
                objSQLDA.Fill(objDTSQL);

                strSQLProcess = "刷新阻塞信息时间：" + ((DateTime)objDTSQL.Rows[0]["DOTIME"]).ToString("yyyy-MM-dd HH:mm:ss:fff") + "\r\n\r\n";

                for (int i = 0; i < objDT.Rows.Count; i++)
                {
                    if (objDT.Rows[i]["阻塞进程 ID"].ToString().Trim() == "0")
                    {
                        strSQLProcess = strSQLProcess + "-------------------------------------------------------------------------------------------------------\r\n" +
                                        "进程 【" + objDT.Rows[i]["进程 ID"].ToString() + "】 阻塞其他进程 -- 客户端为【" + objDT.Rows[i]["客户端名称"].ToString().Trim() + "】" + "\r\n";

                        strSQLLang = strSQLLang + "\r\n-------------------------------------------------------------------------------------------------------\r\n";
                    }
                    else
                    {
                        strSQLProcess = strSQLProcess + "进程 【" + objDT.Rows[i]["进程 ID"].ToString() + "】-- 客户端为【" + objDT.Rows[i]["客户端名称"].ToString().Trim() + "】 被 " +
                                                        "进程 【" + objDT.Rows[i]["阻塞进程 ID"].ToString() + "】 -- 客户端为【" + objDT.Rows[i]["客户端名称"].ToString().Trim() + "】 阻塞" + "\r\n";
                    }

                    try
                    {
                        objSQLComm.CommandText = "DBCC INPUTBUFFER(" + objDT.Rows[i]["进程 ID"].ToString() + ")";
                        objSQLComm.ExecuteNonQuery();
                        
                        objDTBuffer.Clear();
                        objSQLDA.SelectCommand = objSQLComm;
                        objSQLDA.Fill(objDTBuffer);

                        if (objDTBuffer.Rows[0]["EventInfo"] != System.DBNull.Value)
                        {
                            strSQLLang = strSQLLang + "\r\n进程【" + objDT.Rows[i]["进程 ID"].ToString() + "】 -- 客户端为 【" + objDT.Rows[i]["客户端名称"].ToString().Trim() + "】 信息如下：" + "\r\n " +
                                                          ((string)objDTBuffer.Rows[0]["EventInfo"]).Trim().Replace("\0", "\r\n    ") + "\r\n ";
                        }
                        else
                        {
                            strSQLLang = strSQLLang + "\r\n进程【" + objDT.Rows[i]["进程 ID"].ToString() + "】 -- 客户端为 【" + objDT.Rows[i]["客户端名称"].ToString().Trim() + "】 信息如下：" + "\r\n " +
                                                      "  NULL   \r\n ";
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                return strSQLProcess + "-------------------------------------------------------------------------------------------------------\r\n" + 
                                       "=======================================================================================================\r\n" + strSQLLang;
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
                return "";
            }
        }
        
        private StringBuilder getProcessesExcuteInfo(int intProcessStatus, int intIncludeMe, int intIncludeMeProg,string strOrderBy, DataGrid _DGShow, CheckBox _isList)
        {
            System.Data.DataTable objDTSQL = new DataTable();
            System.Data.DataTable objDTBuffer = new DataTable();
            System.Data.SqlClient.SqlDataAdapter objSQLDA = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();

            string strSQL = "";
            StringBuilder objSB = new StringBuilder("");
            

            objSQLComm = this.objSQLConnect.CreateCommand();
            objSQLComm.CommandTimeout = 600;

            strSQL = @"SELECT  ISNULL(SD.NAME,'') AS DBNAME,SPID,BLOCKED,HOSTNAME,PROGRAM_NAME,CPU,PHYSICAL_IO,KPID,
                               LASTWAITTYPE,SP.STATUS AS STATUS,WAITTIME,WAITRESOURCE,SP.DBID,
                               UID,MEMUSAGE,LOGIN_TIME,LAST_BATCH,ECID,OPEN_TRAN,WAITTYPE,
                               HOSTPROCESS,CMD,NT_DOMAIN,NT_USERNAME,
                               NET_ADDRESS,NET_LIBRARY,LOGINAME,MODE,STATUS2,
                               CRDATE,RESERVED,CATEGORY,CMPTLEVEL,FILENAME,VERSION
                       FROM MASTER..SYSPROCESSES SP WITH (NOLOCK) LEFT JOIN MASTER..SYSDATABASES SD WITH (NOLOCK) ON SP.DBID = SD.DBID 
                       WHERE 1 = 1 "
                     + (intIncludeMe == 1 ? "" : "AND hostname <> '" + System.Net.Dns.GetHostName().Trim() + "' ")
                     + (intIncludeMeProg == 1 ? "" : "AND SPID <> @@SPID ")
                     + (intProcessStatus == 0 ? "" : "AND UPPER(SP.STATUS) <> 'SLEEPING' "); //+ (intProcessStatus == 0 ? "" : "WHERE cmd IN ('running', 'runnable') ") 
            if (strOrderBy != "")
            {
                if (strOrderBy == "按进程 SPID 排序 -- 正序")
                {
                    strSQL = strSQL + " ORDER BY SPID";
                }
                else if (strOrderBy == "按处理时间排序   -- 倒序")
                {
                    strSQL = strSQL + " ORDER BY CPU DESC";
                }
                else if (strOrderBy == "按处理时间排序   -- 正序")
                {
                    strSQL = strSQL + " ORDER BY CPU";
                }
                else if (strOrderBy == "按客户端名称排序 -- 正序")
                {
                    strSQL = strSQL + " ORDER BY HOSTNAME";
                }
            }
            else
            {
                strSQL = strSQL + " ORDER BY SPID";
            }
            objSQLComm.CommandText = strSQL;
            objSQLComm.ExecuteNonQuery();

            objDTSQL.Clear();
            objDTSQL.TableName = "Processes";
            objSQLDA.SelectCommand = objSQLComm;
            objSQLDA.Fill(objDTSQL);


            if (_isList.Checked)
            {
                _DGShow.DataSource = objDTSQL;
                _DGShow.Refresh();
            }
            else
            {
                _DGShow.DataSource = null;
            }

            strSQL = "刷新时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "\r\n\r\n";
            objSB.Append(strSQL);

            for (int i = 0; i < objDTSQL.Rows.Count; i++)
            {
                try
                {
                    objSQLComm.CommandText = "DBCC INPUTBUFFER(" + objDTSQL.Rows[i]["SPID"].ToString() + ")";
                    objSQLComm.ExecuteNonQuery();

                    objDTBuffer.Clear();
                    objDTBuffer.TableName = "ProcessInfo";
                    objSQLDA.SelectCommand = objSQLComm;
                    objSQLDA.Fill(objDTBuffer);

                    if (objDTBuffer.Rows.Count > 0 && objDTBuffer.Rows[0]["EventInfo"].ToString().Trim() != "")
                    {
                        strSQL = "===============================================================================================================================================================================================" + "\r\n" +
                                 "进程 SPID：           " + objDTSQL.Rows[i]["SPID"].ToString().Trim() + "\r\n" + 
                                 "客户端：              " + objDTSQL.Rows[i]["HOSTNAME"].ToString().Trim() + "\r\n" +
                                 "执行时间：            " + ((int)objDTSQL.Rows[i]["cpu"]).ToString("#0") + " 毫秒， " + ((double)((double)((int)objDTSQL.Rows[i]["cpu"]) / 1000.00)).ToString("#0.000") + " 秒\r\n" +
                                 "运行数据库：          " + objDTSQL.Rows[i]["DBNAME"].ToString().Trim() + "\r\n" +
                                 "运行程序：            " + objDTSQL.Rows[i]["PROGRAM_NAME"].ToString().Trim().Replace("\0", "") + "\r\n" +
                                 "最初连接时间：        " + ((DateTime)objDTSQL.Rows[i]["login_time"]).ToString("yyyy-MM-dd HH:mm:ss:fff").Trim() + "\r\n" + 
                                 "最后命令开始执行时间：" + ((DateTime)objDTSQL.Rows[i]["last_batch"]).ToString("yyyy-MM-dd HH:mm:ss:fff").Trim() + "\r\n" +
                                 "进程状态：            " + objDTSQL.Rows[i]["STATUS"].ToString().ToUpper().Trim().Replace("\0", "") + "\r\n" +
                                 "正在执行的命令状态：  " + objDTSQL.Rows[i]["CMD"].ToString().Trim() + "          " + "最后等待状态：   " + objDTSQL.Rows[i]["LASTWAITTYPE"].ToString().Trim().Replace("\0", "") + "\r\n" +
                                 "最后运行 SQL 语句：   " + "\r\n    " + (objDTBuffer.Rows.Count > 0 ? objDTBuffer.Rows[0]["EventInfo"].ToString().Trim().Replace("\0","\r\n     ") : "") + "\r\n" +
                                 "================================================================================================================================================================================================" + "\r\n\r\n";
                        objSB.Append(strSQL);
                    }
                }
                catch(System.Exception e)
                {
                    MessageBox.Show(e.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            return objSB;
        }

        private void getProcessesExcuteInfoTable(int intProcessStatus)
        {
            System.Data.DataTable objDTSQL = new DataTable();
            System.Data.DataTable objDTBuffer = new DataTable();
            System.Data.SqlClient.SqlDataAdapter objSQLDA = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();

            string strSQL = "";
            StringBuilder objSB = new StringBuilder("");

            objSQLComm = this.objSQLConnect.CreateCommand();
            objSQLComm.CommandTimeout = 600;

            objSQLComm.CommandText = "Select spid as 进程ID, hostname as 客户端名称, [program_name] as 程序, lastwaittype as 等待类型, " +
                                            "cpu as [CPU 总执行时间], physical_io as [物理 IO], " + 
                                            "Convert(nvarchar(50), last_batch, 121) as 最后执行时间, status as 状态 " +
                                     "FROM SYSPROCESSES WITH (NOLOCK) "
                                     + (intProcessStatus == 0 ? "" : "WHERE UPPER(cmd) <> 'AWAITING COMMAND' ")
                                     + "ORDER BY SPID";
            objSQLComm.ExecuteNonQuery();

            objDTSQL.Clear();
            objDTSQL.TableName = "Processes";
            objSQLDA.SelectCommand = objSQLComm;
            objSQLDA.Fill(objDTSQL);

            objDTSQL.Columns.Add("最后执行语句", Type.GetType("System.String"));

            for (int i = 0; i < objDTSQL.Rows.Count; i++)
            {
                try
                {
                    objSQLComm.CommandText = "DBCC INPUTBUFFER(" + objDTSQL.Rows[i]["进程ID"].ToString() + ")";
                    objSQLComm.ExecuteNonQuery();

                    objDTBuffer.Clear(); 
                    objDTBuffer.TableName = "ProcessInfo";
                    objSQLDA.SelectCommand = objSQLComm;
                    objSQLDA.Fill(objDTBuffer);

                    strSQL = "===================================================================================================" + "  \r\n" +
                             "进程：" + objDTSQL.Rows[i]["进程ID"].ToString().Trim() + "  \r\n" +
                             "客户端：" + objDTSQL.Rows[i]["客户端名称"].ToString().Trim() + "  \r\n" +
                             "程序：" + objDTSQL.Rows[i]["程序"].ToString().Trim() + "  \r\n" +
                             "最后运行 SQL 语句：" + "\r\n" + (objDTBuffer.Rows.Count > 0 ? objDTBuffer.Rows[0]["EventInfo"].ToString().Trim().Replace("\0", "\r\n    ") : "") + 
                             "  \r\n\r\n";

                    objDTSQL.Rows[i]["最后执行语句"] = strSQL;
                }
                catch (System.Exception e)
                {
                    MessageBox.Show(e.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            this.dgProcessInfo.DataSource = objDTSQL;

            if(this.tbFileName.Text.Trim() != "")
                objDTSQL.WriteXml(this.tbFileName.Text.Trim());
        }

        private System.Data.DataTable getTransactionLockRecourse()
        {
            System.Data.DataTable objDT = new DataTable();

            System.Data.SqlClient.SqlDataAdapter objSQLDA = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();
            System.Data.SqlClient.SqlDataReader objSQLDR;
            string strSQL = ""; string strSQLFields = ""; string strSQLFrom = ""; string strSQLWhere = "";
            string strVersion = "";

            objSQLComm = this.objSQLConnect.CreateCommand();
            objSQLComm.CommandTimeout = 600;

            objSQLComm.CommandText = "SELECT @@VERSION AS Version";
            objSQLDR = objSQLComm.ExecuteReader();
            if (objSQLDR.Read()) strVersion = objSQLDR.GetString(0);
            objSQLDR.Close();



            strSQLFields = @"
                     SELECT CONVERT(INT, REQ_SPID) AS SPID, LTRIM(RTRIM(P.HOSTNAME)) AS 客户端名称,         
                            RTRIM(LTRIM(RSC_DBID)) AS [数据库 ID], LTRIM(RTRIM(D.NAME)) AS 数据库名称,
                            LTRIM(RTRIM(RSC_OBJID)) AS [对象 ID], LTRIM(RTRIM(O.[NAME]))  AS 对象名称, 
                            rsc_type AS [锁类型 ID], SUBSTRING (V.NAME, 1, 4) AS 锁类型,
                            (CASE L.rsc_type WHEN 1 THEN 'NULL 资源（未使用）' WHEN 2 then '数据库' WHEN 3 THEN '文件' WHEN 4 THEN '索引' 
                                             WHEN 5 THEN '表' WHEN 6 then '页' WHEN 7 THEN '键' WHEN 8 THEN '区' WHEN 9 THEN 'RID（行 ID）' 
                                             WHEN 10 THEN '应用程序' END) AS 锁定资源类型,
                            SUBSTRING(U.NAME, 1, 8) AS MODE,  
                            (CASE L.req_mode WHEN 1 THEN '架构稳定性' WHEN 2 THEN '架构修改' WHEN 3 THEN '共享' WHEN 4 THEN '更新' 
                                             WHEN 5 THEN '排他' WHEN 6 THEN '意向共享' WHEN 7 THEN '意向更新' WHEN 8 THEN '意向排他' 
                                             WHEN 9 THEN '共享意向更新' WHEN 10 THEN '共享意向排他' WHEN 11 THEN '更新意向排他' ELSE SUBSTRING(U.NAME, 1, 8)
                             END) AS 锁请求模式, 
                            SUBSTRING(RSC_TEXT, 1, 32) AS 锁定资源, 
                            (CASE L.REQ_STATUS WHEN 1 THEN '已授予' WHEN 2 THEN '正在转换' WHEN 3 THEN '正在等待' END) AS 锁请求的状态,
                            SUBSTRING (X.NAME, 1, 5) AS [LOCK STATUS], RSC_INDID AS [资源关联的索引 ID], 
                            req_transactionID AS [事务 ID], 
                            LTRIM(RTRIM(P.PROGRAM_NAME)) AS 运行程序,
                            req_ownertype,req_refcnt ";

            strSQLWhere = strSQLWhere + @"
                    WHERE 1 = 1 ";

            if (this.tbClientName.Text.Trim() != "") strSQLWhere = strSQLWhere + @"AND LTRIM(RTRIM(P.HOSTNAME)) = '" + this.tbClientName.Text.Trim() + "' ";
            //if (this.tbDatabaseName.Text.Trim() != "") strSQL = strSQLWhere + @"AND LTRIM(RTRIM(D.NAME)) = '" + this.tbDatabaseName.Text.Trim() + "' ";
            if (this.tbTableName.Text.Trim() != "") strSQLWhere = strSQLWhere + @"AND LTRIM(RTRIM(O.[NAME])) = '" + this.tbTableName.Text.Trim() + "' ";
            if (this.tbLockRecourse.Text.Trim() != "") strSQLWhere = strSQLWhere + @"AND SUBSTRING(RSC_TEXT, 1, 32)  = '" + this.tbLockRecourse.Text.Trim() + "' ";
            if (this.cbRequestLockType.SelectedIndex != 0) strSQLWhere = strSQLWhere + @"AND L.req_mode  = " + ((SQLServerDBEnumType)this.cbRequestLockType.SelectedItem).value.ToString() + " ";
            if (this.cbResourceLockType.SelectedIndex != 0) strSQLWhere = strSQLWhere + @"AND L.rsc_type  = " + ((SQLServerDBEnumType)this.cbResourceLockType.SelectedItem).value.ToString() + " ";
            if (this.cbReqStatus.SelectedIndex != 0) strSQLWhere = strSQLWhere + @"AND L.req_status  = " + ((SQLServerDBEnumType)this.cbReqStatus.SelectedItem).value.ToString() + " ";
            if (!this.cbIncldeMe.Checked) strSQLWhere = strSQLWhere + @"AND NOT LTRIM(RTRIM(P.HOSTNAME))  = '" + System.Net.Dns.GetHostName().Trim() + "' ";
            
            strSQL = "";
            if (this.tbDatabaseName.Text.Trim() != "")
            {
                if (strVersion.IndexOf("Microsoft SQL Server 2000", 1) < 0)
                {
                    strSQL = strSQLFields + @" 
                                     FROM MASTER.SYS.SYSLOCKINFO L WITH(NOLOCK) INNER JOIN MASTER.DBO.SPT_VALUES V ON (L.RSC_TYPE = V.NUMBER AND V.TYPE = 'LR')
								                                                INNER JOIN MASTER.DBO.SPT_VALUES X ON (L.REQ_STATUS = X.NUMBER AND X.TYPE = 'LS') 
								                                   INNER JOIN MASTER.DBO.SPT_VALUES U ON (L.REQ_MODE + 1 = U.NUMBER AND U.TYPE = 'L')
					                                               INNER JOIN MASTER.SYS.SYSPROCESSES P WITH(NOLOCK) ON P.SPID = CONVERT (SMALLINT, L.REQ_SPID)
					                                               INNER JOIN MASTER.SYS.DATABASES D WITH(NOLOCK) ON D.DATABASE_ID = L.RSC_DBID
					                                               INNER JOIN " + this.tbDatabaseName.Text.Trim() + ".SYS.SYSOBJECTS O WITH(NOLOCK) ON O.ID = L.RSC_OBJID ";
                     strSQL = strSQL + @"
                                              " +  strSQLWhere;
                }
                else
                {
                    strSQL = strSQLFields + @" 
                                     FROM  MASTER.DBO.SYSLOCKINFO L WITH(NOLOCK) INNER JOIN MASTER.DBO.SPT_VALUES V ON (L.RSC_TYPE = V.NUMBER AND V.TYPE = 'LR')
                                                                                 INNER JOIN MASTER.DBO.SPT_VALUES X ON (L.REQ_STATUS = X.NUMBER AND X.TYPE = 'LS') 
							                                       INNER JOIN MASTER.DBO.SPT_VALUES U ON (L.REQ_MODE + 1 = U.NUMBER AND U.TYPE = 'L')
							                                       INNER JOIN MASTER.DBO.SYSPROCESSES P WITH(NOLOCK) ON P.SPID = CONVERT (SMALLINT, L.REQ_SPID)
							                                       INNER JOIN MASTER.DBO.SYSDATABASES D WITH(NOLOCK) ON D.dbid = L.RSC_DBID
							                                       INNER JOIN " + this.tbDatabaseName.Text.Trim() + "..SYSOBJECTS O WITH(NOLOCK) ON O.ID = L.RSC_OBJID ";
                    strSQL = strSQL + @"
                                              " + strSQLWhere;
                }
            }
            else
            {
                if (strVersion.IndexOf("Microsoft SQL Server 2000", 1) < 0)
                {
                    objSQLComm.CommandText = "SELECT RTRIM(LTRIM(database_id)) AS [DBID], LTRIM(RTRIM([NAME])) AS 数据库名称 From MASTER.SYS.DATABASES WITH(NOLOCK) WHERE NOT NAME IN ('MASTER', 'ReportServer', 'ReportServerTempDB') ";
                }
                else
                {
                    objSQLComm.CommandText = "SELECT RTRIM(LTRIM(DBID)) AS [DBID], LTRIM(RTRIM([NAME])) AS 数据库名称 From MASTER.DBO.SYSDATABASES WITH(NOLOCK) WHERE NOT NAME IN ('MASTER', 'ReportServer', 'ReportServerTempDB') ";
                }
                objSQLDR = objSQLComm.ExecuteReader();


                if (strVersion.IndexOf("Microsoft SQL Server 2000", 1) < 0)
                {
                    strSQL = strSQL + @"
                                         " + strSQLFields + @"
                                         FROM MASTER.SYS.SYSLOCKINFO L WITH(NOLOCK) INNER JOIN MASTER.DBO.SPT_VALUES V ON (L.RSC_TYPE = V.NUMBER AND V.TYPE = 'LR')
								                                                    INNER JOIN MASTER.DBO.SPT_VALUES X ON (L.REQ_STATUS = X.NUMBER AND X.TYPE = 'LS') 
								                                       INNER JOIN MASTER.DBO.SPT_VALUES U ON (L.REQ_MODE + 1 = U.NUMBER AND U.TYPE = 'L')
					                                                   INNER JOIN MASTER.SYS.SYSPROCESSES P WITH(NOLOCK) ON P.SPID = CONVERT (SMALLINT, L.REQ_SPID)
					                                                   INNER JOIN MASTER.SYS.DATABASES D WITH(NOLOCK) ON D.DATABASE_ID = L.RSC_DBID
					                                                   INNER JOIN MASTER.SYS.SYSOBJECTS O WITH(NOLOCK) ON O.ID = L.RSC_OBJID ";
                    strSQL = strSQL + @"
                                              " + strSQLWhere + @"
                                              ";
                }
                else
                {
                    strSQLFrom = strSQLFrom + @"
                                         FROM  MASTER.DBO.SYSLOCKINFO L WITH(NOLOCK) INNER JOIN MASTER.DBO.SPT_VALUES V ON (L.RSC_TYPE = V.NUMBER AND V.TYPE = 'LR')
                                                                                     INNER JOIN MASTER.DBO.SPT_VALUES X ON (L.REQ_STATUS = X.NUMBER AND X.TYPE = 'LS') 
							                                           INNER JOIN MASTER.DBO.SPT_VALUES U ON (L.REQ_MODE + 1 = U.NUMBER AND U.TYPE = 'L')
							                                           INNER JOIN MASTER.DBO.SYSPROCESSES P WITH(NOLOCK) ON P.SPID = CONVERT (SMALLINT, L.REQ_SPID)
							                                           INNER JOIN MASTER.DBO.SYSDATABASES D WITH(NOLOCK) ON D.dbid = L.RSC_DBID
							                                           INNER JOIN MASTER..SYSOBJECTS O WITH(NOLOCK) ON O.ID = L.RSC_OBJID ";
                    strSQL = strSQL + @"
                                              " + strSQLWhere + @"
                                              ";
                }



                while (objSQLDR.Read())
                {
                    if (strVersion.IndexOf("Microsoft SQL Server 2000", 1) < 0)
                    {
                        strSQL = strSQL + @"UNION 
                                           " + strSQLFields + @"
                                         FROM MASTER.SYS.SYSLOCKINFO L WITH(NOLOCK) INNER JOIN MASTER.DBO.SPT_VALUES V ON (L.RSC_TYPE = V.NUMBER AND V.TYPE = 'LR')
								                                                    INNER JOIN MASTER.DBO.SPT_VALUES X ON (L.REQ_STATUS = X.NUMBER AND X.TYPE = 'LS') 
								                                       INNER JOIN MASTER.DBO.SPT_VALUES U ON (L.REQ_MODE + 1 = U.NUMBER AND U.TYPE = 'L')
					                                                   INNER JOIN MASTER.SYS.SYSPROCESSES P WITH(NOLOCK) ON P.SPID = CONVERT (SMALLINT, L.REQ_SPID)
					                                                   INNER JOIN MASTER.SYS.DATABASES D WITH(NOLOCK) ON D.DATABASE_ID = L.RSC_DBID
					                                                   INNER JOIN " + objSQLDR.GetString(1).Trim() + ".SYS.SYSOBJECTS O WITH(NOLOCK) ON O.ID = L.RSC_OBJID ";
                        strSQL = strSQL + @"
                                              " + strSQLWhere + @"
                                              ";
                    }
                    else
                    {
                        strSQL = strSQL + @"UNION 
                                           " + strSQLFields + @"
                                         FROM  MASTER.DBO.SYSLOCKINFO L WITH(NOLOCK) INNER JOIN MASTER.DBO.SPT_VALUES V ON (L.RSC_TYPE = V.NUMBER AND V.TYPE = 'LR')
                                                                                     INNER JOIN MASTER.DBO.SPT_VALUES X ON (L.REQ_STATUS = X.NUMBER AND X.TYPE = 'LS') 
							                                           INNER JOIN MASTER.DBO.SPT_VALUES U ON (L.REQ_MODE + 1 = U.NUMBER AND U.TYPE = 'L')
							                                           INNER JOIN MASTER.DBO.SYSPROCESSES P WITH(NOLOCK) ON P.SPID = CONVERT (SMALLINT, L.REQ_SPID)
							                                           INNER JOIN MASTER.DBO.SYSDATABASES D WITH(NOLOCK) ON D.dbid = L.RSC_DBID
							                                           INNER JOIN " + objSQLDR.GetString(1).Trim() + "..SYSOBJECTS O WITH(NOLOCK) ON O.ID = L.RSC_OBJID ";
                        strSQL = strSQL + @"
                                              " + strSQLWhere + @"
                                              ";
                    }
                }
            }
            if (!objSQLDR.IsClosed) objSQLDR.Close();


            strSQL = strSQL + @" --ORDER BY SPID, 客户端名称; ";

            objSQLComm.CommandText = strSQL;
            objSQLComm.ExecuteNonQuery();

            objDT.Clear();
            objDT.TableName = "SP_WHO_Process";
            objSQLDA.SelectCommand = objSQLComm;
            objSQLDA.Fill(objDT);

            return objDT;
        }

        private System.Data.DataTable getSQLProcessInfo(int intTop, int intOrderBy, string strOrderType)
        {
            System.Data.DataTable objDT = new DataTable();

            System.Data.SqlClient.SqlDataAdapter objSQLDA = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();
            System.Data.SqlClient.SqlDataReader objSQLDR = null;

            try
            {
                string strSQL;
                string strVersion = "";

                objSQLComm = this.objSQLConnect.CreateCommand();
                objSQLComm.CommandTimeout = 600;

                objSQLComm.CommandText = "USE Master";
                objSQLComm.ExecuteNonQuery();

                objSQLComm.CommandText = "SELECT @@VERSION AS Version";
                objSQLDR = objSQLComm.ExecuteReader();

                if (objSQLDR.Read()) strVersion = objSQLDR.GetString(0);
                if (objSQLDR != null && !objSQLDR.IsClosed) objSQLDR.Close();

                if (strVersion.IndexOf("Microsoft SQL Server 2000", 1) > 0)
                {
                    MessageBox.Show("数据库版本为 Microsoft SQL Server 2000 或以下版本，不能使用。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }

                strSQL = @"SELECT TOP " + intTop.ToString() + @" SQL_HANDLE AS N'SQL 语句句柄', creation_time  N'语句编译时间', last_execution_time  N'最后执行时间', 
                                   last_elapsed_time/1000 N'最后执行所用时间(ms)', last_worker_time /1000 N'最后执行计划所用时间(ms)', 
                                   execution_count  N'执行次数', 
                                   last_physical_reads N'最后执行物理读次数', last_logical_reads N'最后执行逻辑读次数',
                                   total_physical_reads/execution_count N'平均物理读取次数', total_physical_reads N'物理读取总次数', 
                                   total_logical_reads/execution_count N'平均逻辑读次数',total_logical_reads  N'逻辑读取总次数',
                                   total_logical_writes N'逻辑写入总次数',  total_worker_time/1000 N'所用CPU总时间(ms)', 
                                   total_elapsed_time/1000  N'执行合计花费时间(ms)', (total_elapsed_time / execution_count)/1000  N'平均执行时间(ms)',
                                    SUBSTRING(st.text, (qs.statement_start_offset/2) + 1,((CASE statement_end_offset WHEN -1 THEN DATALENGTH(st.text) ELSE qs.statement_end_offset END - qs.statement_start_offset)/2) + 1) N'执行语句'
                            FROM sys.dm_exec_query_stats AS qs CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) ST
                            where SUBSTRING(st.text, (qs.statement_start_offset/2) + 1,
                                     (((CASE statement_end_offset WHEN -1 THEN DATALENGTH(st.text) ELSE qs.statement_end_offset END) - qs.statement_start_offset) / 2) + 1) not like '%fetch%'
                            ORDER BY ";
                switch (intOrderBy)
                {
                    case 1:
                        strSQL = strSQL + "[最后执行时间] ";
                        break;
                    case 2:
                        strSQL = strSQL + "[最后执行所用时间(ms)] ";
                        break;
                    case 3:
                        strSQL = strSQL + "[最后执行计划所用时间(ms)] ";
                        break;
                    case 4:
                        strSQL = strSQL + "[最后执行物理读次数] ";
                        break;
                    case 5:
                        strSQL = strSQL + "[最后执行逻辑读次数] ";
                        break;
                    case 6:
                        strSQL = strSQL + "[平均逻辑读次数] ";
                        break;
                    case 7:
                        strSQL = strSQL + "[平均物理读取次数] ";
                        break;
                    case 8:
                        strSQL = strSQL + "[平均执行时间(ms)] ";
                        break;
                    case 9:
                        strSQL = strSQL + "[执行次数] ";
                        break;
                }
                strSQL = strSQL + strOrderType;

                objSQLComm.CommandText = strSQL;
                objSQLComm.ExecuteNonQuery();

                objDT.Clear();
                objDT.TableName = "ServerProcessInfo";
                objSQLDA.SelectCommand = objSQLComm;
                objSQLDA.Fill(objDT);

                return objDT;
            }
            catch (Exception e)
            { 
                return null;
                throw e;
            } 
            finally
            {
                objSQLDA.Dispose();
                objSQLDA = null;

                objSQLComm.Dispose();
                objSQLComm = null;

                if (objSQLDR != null && !objSQLDR.IsClosed) objSQLDR.Close();
            }
        }

        private void KillBlockProcesses()
        {
            System.Data.DataTable objDTTemp = new DataTable();
            System.Data.DataView objDTView = new DataView();

            System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();
            objSQLComm = this.objSQLConnect.CreateCommand();
            objSQLComm.CommandTimeout = 600;

            try
            {
                if (objDT != null && objDT.Rows.Count > 0)
                {
                    objDTTemp = objDT.Copy();

                    objDTView = objDTTemp.DefaultView;
                    objDTView.RowFilter = "[阻塞进程 ID] = 0";

                    for (int i = 0; i < objDTView.Count; i++)
                    {
                        objSQLComm.CommandText = "KILL " + objDTView[i]["进程 ID"].ToString().Trim();
                        objSQLComm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objDTTemp != null) { objDTTemp.Clear(); objDTTemp = null; }
                if (objDTView != null) { objDTView = null; }
                if (objSQLComm != null) { objSQLComm = null; }

            }
        }




       //=========================================================================================================================================================

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog objSFD = new SaveFileDialog();
            objSFD.Filter = "文本文件 (*.TXT)|*.TXT";
            objSFD.ShowDialog();
            if (objSFD.FileName != "")
            {
                this.tbFileName.Text = objSFD.FileName;
            }
        }

        private void btnSaveBlock_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog objSFD = new SaveFileDialog();
            objSFD.Filter = "文本文件 (*.TXT)|*.TXT";
            objSFD.ShowDialog();
            if (objSFD.FileName != "")
            {
                this.tbBlockFile.Text = objSFD.FileName;
            }
        }

        private void cbSQLWtiteToFile_CheckedChanged(object sender, EventArgs e)
        {
            this.tbFileName.Enabled = this.cbSQLWtiteToFile.Checked;
            this.btnSaveFile.Enabled = this.cbSQLWtiteToFile.Checked;
        }

        private void cbBlockSQLWriteToFile_CheckedChanged(object sender, EventArgs e)
        {
            this.tbBlockFile.Enabled = this.cbBlockSQLWriteToFile.Checked;
            this.btnSaveBlock.Enabled = this.cbBlockSQLWriteToFile.Checked;
        }

        

        private void ProcessBlockedForm_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F5)
            {
                this.btnBlockPreRefresh_Click(sender, e);
            }
        }

        private void ProcessBlockedForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.btnBlockPreRefresh_Click(sender, e);
            }
        }

        private void dgBlockPrecess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.btnBlockPreRefresh_Click(sender, e);
            }
        }

        private void tcInfo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.btnBlockPreRefresh_Click(sender, e);
            }
        }

        

        private void rtbProcessInfo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.btnProcessRefresh_Click(sender, e);
            }
        }

        private void ugDBLockRecourse_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                this.ugDBLockRecourse.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.Copy);
            }
        }

       

        
        

       

        

        
        
        

        

     

        






        
        






        

      

        

        
    }

    public class SQLServerDBEnumType
    {
        public string Code;
        public string Name;
        public int value;

        public string Display
        {
            get
            {
                return (Code + " -- " + Name);
            }
        }

        public string DisplayLockRequestType
        {
            get
            {
                return (value <= 11 && value >= 0 ? Code + " -- " + Name : Code);
            }
        }

        public string ToString(string strType)
        {
            if (strType.ToUpper() == "LOCKREQUESTTYPE")
                return (value <= 11 && value >= 0 ? Code + " -- " + Name : Code);
            else
                return Code + " -- " + Name;
        }

        public new string ToString()
        {
            return Code + " -- " + Name;
        }
    }

    public class CommonOperation
    {
        public System.Collections.ArrayList LockRequestTypeCollect()
        {
            SQLServerDBEnumType _objItem;
            System.Collections.ArrayList _LockRequestType = new System.Collections.ArrayList();

            _objItem = new SQLServerDBEnumType();
            _objItem.value = -1; _objItem.Code = "ALL"; _objItem.Name = "所有";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 0;  _objItem.Code = "NULL";  _objItem.Name = "不授权访问资源";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 1; _objItem.Code = "Sch-S"; _objItem.Name = "架构稳定性";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 2; _objItem.Code = "Sch-M"; _objItem.Name = "架构修改";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 3; _objItem.Code = "S"; _objItem.Name = "共享";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 4; _objItem.Code = "U"; _objItem.Name = "更新";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 5; _objItem.Code = "X"; _objItem.Name = "排他";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 6; _objItem.Code = "IS"; _objItem.Name = "意向共享";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 7; _objItem.Code = "IU"; _objItem.Name = "意向更新";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 8; _objItem.Code = "IX"; _objItem.Name = "意向排他";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 9; _objItem.Code = "SIU"; _objItem.Name = "共享意向更新";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 10; _objItem.Code = "SIX"; _objItem.Name = "共享意向排他";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 11; _objItem.Code = "UIX"; _objItem.Name = "更新意向排他";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 12; _objItem.Code = "BU"; _objItem.Name = "BU";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 13; _objItem.Code = "RangeS-S"; _objItem.Name = "RangeS-S";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 14; _objItem.Code = "RangeS-U"; _objItem.Name = "RangeS-U";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 15; _objItem.Code = "RangeIn-Null"; _objItem.Name = "RangeIn-Null";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 16; _objItem.Code = "RangeIn-S"; _objItem.Name = "RangeIn-S";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 17; _objItem.Code = "RangeIn-U"; _objItem.Name = "RangeIn-U";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 18; _objItem.Code = "RangeIn-X"; _objItem.Name = "RangeIn-X";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 19; _objItem.Code = "RangeX-S"; _objItem.Name = "RangeX-S";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 20; _objItem.Code = "RangeX-U"; _objItem.Name = "RangeX-U";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 21; _objItem.Code = "RangeX-X"; _objItem.Name = "RangeX-X";
            _LockRequestType.Add(_objItem);

            return _LockRequestType;
        }

        public System.Collections.ArrayList LockRecourceTypeCollect()
        {
            SQLServerDBEnumType _objItem;
            System.Collections.ArrayList _LockRequestType = new System.Collections.ArrayList();

            _objItem = new SQLServerDBEnumType();
            _objItem.value = -1; _objItem.Code = "ALL"; _objItem.Name = "所有";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 1; _objItem.Code = "NUL"; _objItem.Name = "NULL 资源（未使用）";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 2; _objItem.Code = "DB"; _objItem.Name = "数据库";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 3; _objItem.Code = "FIL"; _objItem.Name = "文件";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 4; _objItem.Code = "IND"; _objItem.Name = "索引";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 5; _objItem.Code = "TAB"; _objItem.Name = "表";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 6; _objItem.Code = "PAG"; _objItem.Name = "页";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 7; _objItem.Code = "KEY"; _objItem.Name = "键";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 8; _objItem.Code = "EXT"; _objItem.Name = "区";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 9; _objItem.Code = "RID"; _objItem.Name = "RID（行 ID）";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 10; _objItem.Code = "APP"; _objItem.Name = "应用程序";
            _LockRequestType.Add(_objItem);

            return _LockRequestType;
        }

        public System.Collections.ArrayList RequestStatusColect()
        {
            SQLServerDBEnumType _objItem;
            System.Collections.ArrayList _RequestStatusType = new System.Collections.ArrayList();

            _objItem = new SQLServerDBEnumType();
            _objItem.value = -1; _objItem.Code = "ALL"; _objItem.Name = "所有";
            _RequestStatusType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 1; _objItem.Code = "GRANT"; _objItem.Name = "已授予";
            _RequestStatusType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 2; _objItem.Code = "CNVT"; _objItem.Name = "正在转换";
            _RequestStatusType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 3; _objItem.Code = "WAIT"; _objItem.Name = "正在等待";
            _RequestStatusType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 4; _objItem.Code = "RELN"; _objItem.Name = "RELN";
            _RequestStatusType.Add(_objItem);

             _objItem = new SQLServerDBEnumType();
            _objItem.value = 5; _objItem.Code = "BLCKN"; _objItem.Name = "被阻塞";
            _RequestStatusType.Add(_objItem);

            return _RequestStatusType;
        }
    }

}
