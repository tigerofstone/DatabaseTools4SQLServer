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
                //    System.Exception objExp = new Exception("�������ݿ�ʧ�ܡ�");

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
            this.Text = "̽�����ݿ��������   ���ݿ�ʵ����" + this.strInstance + "  �������ƣ�" + System.Net.Dns.GetHostName();

        }

        private void OpenConnection()
        {
            try
            {
                objSQLConnect.ConnectionString = this.strConnection;
                objSQLConnect.Open();

                if (objSQLConnect.State != System.Data.ConnectionState.Open)
                {
                    System.Exception objExp = new Exception("�������ݿ�ʧ�ܡ�");

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
            //��ʼ��������������ҳǩ����
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

        //    //// ��ǩ���������ɫ��Ҳ������ͼƬ 
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
            //��ȡTabControl���ؼ��Ĺ������� 
            Rectangle rec = Control.ClientRectangle;

            //�½�һ��StringFormat�������ڶԱ�ǩ���ֵĲ������� 
            StringFormat StrFormat = new StringFormat();
            StrFormat.LineAlignment = StringAlignment.Center;// �������ִ�ֱ������� 
            StrFormat.Alignment = StringAlignment.Center;// ��������ˮƽ�������          

            // ��ǩ���������ɫ��Ҳ������ͼƬ 
            SolidBrush bru = new SolidBrush(System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239))))));
            SolidBrush bruFont = new SolidBrush(Color.FromArgb(0, 0, 0));// ��ǩ������ɫ 
            Font font = new System.Drawing.Font("����", 9F);//���ñ�ǩ������ʽ 
            e.Graphics.FillRectangle(bru, rec);
           
            //���Ʊ�ǩ��ʽ 
            //SolidBrush bruBroder = new SolidBrush(Color.Silver);
            //Pen penDrwBorder = new Pen(bruBroder);
            for (int i = 0; i < Control.TabPages.Count; i++)
            {
                //��ȡ��ǩͷ�Ĺ������� 
                Rectangle recChild = Control.GetTabRect(i);
                ////���Ʊ�ǩͷ������ɫ 
                e.Graphics.FillRectangle(bru, recChild);
                //���Ʊ�ǩͷ������ 
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
                objDTResult.Columns.Add("���� ID", Type.GetType("System.Int32"));
                objDTResult.Columns.Add("�������� ID", Type.GetType("System.Int32"));
                objDTResult.Columns.Add("CPU ��ʱ", Type.GetType("System.Int32"));
                objDTResult.Columns.Add("���� IO", Type.GetType("System.Int32"));
                objDTResult.Columns.Add("�ͻ�������", Type.GetType("System.String"));
                objDTResult.Columns.Add("���ݿ�", Type.GetType("System.String"));
                objDTResult.Columns.Add("ִ�г�������", Type.GetType("System.String"));
                objDTResult.Columns.Add("�ȴ�ʱ��", Type.GetType("System.Int32"));
                objDTResult.Columns.Add("���������Ŀͻ�������", Type.GetType("System.String"));
                //================================================================================
                this.dgBlockPrecess.DataSource = objDT;
                this.dgBlockPrecess.Refresh();
            }
            this.rtbSQLLang.Text = strBlockedSQLLag;

            ///д�ļ�
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

                       Select spid AS [���� ID], blocked AS [�������� ID], 
                              cpu AS [CPU ��ʱ], physical_io [���� IO], hostname AS �ͻ�������, 
                              (Select top 1 name From MASTER..sysdatabases Where dbid = BLS.dbid) AS [���ݿ�], 
                              program_name AS ִ�г�������, waittime AS [�ȴ�ʱ��],
                              (Select top 1 hostname From @Tmp Where spid = BLS.spid) AS [���������Ŀͻ�������] 
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
            objDTView.RowFilter = "[�������� ID] = 0";

            for (int i = 0; i < objDTView.Count; i++)
            {
                int intSPID = 0;

                objDRow = setResultDataRow(objDTView, objDTView[i].Row, i);
                objDTResult.Rows.Add(objDRow);

                intSPID = (int)objDT.Rows[i]["���� ID"];
                
                getBlockedLinkInfo(objDT, intSPID);
            }

            //=========================================================================
            objDTView = objDTTemp.DefaultView;
            objDTView.RowFilter = "[���� ID] = [�������� ID]";
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
                objDTV.RowFilter = "[�������� ID] = '" + intSPID.ToString() + "'";
                if (objDTV.Count != 0)
                {
                    for (int i = 0; i < objDTV.Count; i++)
                    {
                        objDRow = setResultDataRow(objDTV, objDTV[i].Row, i);
                        objDTResult.Rows.Add(objDRow);

                        getBlockedLinkInfo(objDT, (int)objDTV[i]["���� ID"]);
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
            objDRow["���� ID"] = objDTV[intIndex]["���� ID"];
            objDRow["�������� ID"] = objDTV[intIndex]["�������� ID"];
            objDRow["CPU ��ʱ"] = objDTV[intIndex]["CPU ��ʱ"];
            objDRow["���� IO"] = objDTV[intIndex]["���� IO"];
            objDRow["�ͻ�������"] = objDTV[intIndex]["�ͻ�������"];
            objDRow["���ݿ�"] = objDTV[intIndex]["���ݿ�"];
            objDRow["ִ�г�������"] = objDTV[intIndex]["ִ�г�������"];
            objDRow["�ȴ�ʱ��"] = objDTV[intIndex]["�ȴ�ʱ��"];
            objDRow["���������Ŀͻ�������"] = objDTV[intIndex]["���������Ŀͻ�������"];

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

                strSQLProcess = "ˢ��������Ϣʱ�䣺" + ((DateTime)objDTSQL.Rows[0]["DOTIME"]).ToString("yyyy-MM-dd HH:mm:ss:fff") + "\r\n\r\n";

                for (int i = 0; i < objDT.Rows.Count; i++)
                {
                    if (objDT.Rows[i]["�������� ID"].ToString().Trim() == "0")
                    {
                        strSQLProcess = strSQLProcess + "-------------------------------------------------------------------------------------------------------\r\n" +
                                        "���� ��" + objDT.Rows[i]["���� ID"].ToString() + "�� ������������ -- �ͻ���Ϊ��" + objDT.Rows[i]["�ͻ�������"].ToString().Trim() + "��" + "\r\n";

                        strSQLLang = strSQLLang + "\r\n-------------------------------------------------------------------------------------------------------\r\n";
                    }
                    else
                    {
                        strSQLProcess = strSQLProcess + "���� ��" + objDT.Rows[i]["���� ID"].ToString() + "��-- �ͻ���Ϊ��" + objDT.Rows[i]["�ͻ�������"].ToString().Trim() + "�� �� " +
                                                        "���� ��" + objDT.Rows[i]["�������� ID"].ToString() + "�� -- �ͻ���Ϊ��" + objDT.Rows[i]["�ͻ�������"].ToString().Trim() + "�� ����" + "\r\n";
                    }

                    try
                    {
                        objSQLComm.CommandText = "DBCC INPUTBUFFER(" + objDT.Rows[i]["���� ID"].ToString() + ")";
                        objSQLComm.ExecuteNonQuery();
                        
                        objDTBuffer.Clear();
                        objSQLDA.SelectCommand = objSQLComm;
                        objSQLDA.Fill(objDTBuffer);

                        if (objDTBuffer.Rows[0]["EventInfo"] != System.DBNull.Value)
                        {
                            strSQLLang = strSQLLang + "\r\n���̡�" + objDT.Rows[i]["���� ID"].ToString() + "�� -- �ͻ���Ϊ ��" + objDT.Rows[i]["�ͻ�������"].ToString().Trim() + "�� ��Ϣ���£�" + "\r\n " +
                                                          ((string)objDTBuffer.Rows[0]["EventInfo"]).Trim().Replace("\0", "\r\n    ") + "\r\n ";
                        }
                        else
                        {
                            strSQLLang = strSQLLang + "\r\n���̡�" + objDT.Rows[i]["���� ID"].ToString() + "�� -- �ͻ���Ϊ ��" + objDT.Rows[i]["�ͻ�������"].ToString().Trim() + "�� ��Ϣ���£�" + "\r\n " +
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
                if (strOrderBy == "������ SPID ���� -- ����")
                {
                    strSQL = strSQL + " ORDER BY SPID";
                }
                else if (strOrderBy == "������ʱ������   -- ����")
                {
                    strSQL = strSQL + " ORDER BY CPU DESC";
                }
                else if (strOrderBy == "������ʱ������   -- ����")
                {
                    strSQL = strSQL + " ORDER BY CPU";
                }
                else if (strOrderBy == "���ͻ����������� -- ����")
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

            strSQL = "ˢ��ʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "\r\n\r\n";
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
                                 "���� SPID��           " + objDTSQL.Rows[i]["SPID"].ToString().Trim() + "\r\n" + 
                                 "�ͻ��ˣ�              " + objDTSQL.Rows[i]["HOSTNAME"].ToString().Trim() + "\r\n" +
                                 "ִ��ʱ�䣺            " + ((int)objDTSQL.Rows[i]["cpu"]).ToString("#0") + " ���룬 " + ((double)((double)((int)objDTSQL.Rows[i]["cpu"]) / 1000.00)).ToString("#0.000") + " ��\r\n" +
                                 "�������ݿ⣺          " + objDTSQL.Rows[i]["DBNAME"].ToString().Trim() + "\r\n" +
                                 "���г���            " + objDTSQL.Rows[i]["PROGRAM_NAME"].ToString().Trim().Replace("\0", "") + "\r\n" +
                                 "�������ʱ�䣺        " + ((DateTime)objDTSQL.Rows[i]["login_time"]).ToString("yyyy-MM-dd HH:mm:ss:fff").Trim() + "\r\n" + 
                                 "������ʼִ��ʱ�䣺" + ((DateTime)objDTSQL.Rows[i]["last_batch"]).ToString("yyyy-MM-dd HH:mm:ss:fff").Trim() + "\r\n" +
                                 "����״̬��            " + objDTSQL.Rows[i]["STATUS"].ToString().ToUpper().Trim().Replace("\0", "") + "\r\n" +
                                 "����ִ�е�����״̬��  " + objDTSQL.Rows[i]["CMD"].ToString().Trim() + "          " + "���ȴ�״̬��   " + objDTSQL.Rows[i]["LASTWAITTYPE"].ToString().Trim().Replace("\0", "") + "\r\n" +
                                 "������� SQL ��䣺   " + "\r\n    " + (objDTBuffer.Rows.Count > 0 ? objDTBuffer.Rows[0]["EventInfo"].ToString().Trim().Replace("\0","\r\n     ") : "") + "\r\n" +
                                 "================================================================================================================================================================================================" + "\r\n\r\n";
                        objSB.Append(strSQL);
                    }
                }
                catch(System.Exception e)
                {
                    MessageBox.Show(e.Message, "ϵͳ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            objSQLComm.CommandText = "Select spid as ����ID, hostname as �ͻ�������, [program_name] as ����, lastwaittype as �ȴ�����, " +
                                            "cpu as [CPU ��ִ��ʱ��], physical_io as [���� IO], " + 
                                            "Convert(nvarchar(50), last_batch, 121) as ���ִ��ʱ��, status as ״̬ " +
                                     "FROM SYSPROCESSES WITH (NOLOCK) "
                                     + (intProcessStatus == 0 ? "" : "WHERE UPPER(cmd) <> 'AWAITING COMMAND' ")
                                     + "ORDER BY SPID";
            objSQLComm.ExecuteNonQuery();

            objDTSQL.Clear();
            objDTSQL.TableName = "Processes";
            objSQLDA.SelectCommand = objSQLComm;
            objSQLDA.Fill(objDTSQL);

            objDTSQL.Columns.Add("���ִ�����", Type.GetType("System.String"));

            for (int i = 0; i < objDTSQL.Rows.Count; i++)
            {
                try
                {
                    objSQLComm.CommandText = "DBCC INPUTBUFFER(" + objDTSQL.Rows[i]["����ID"].ToString() + ")";
                    objSQLComm.ExecuteNonQuery();

                    objDTBuffer.Clear(); 
                    objDTBuffer.TableName = "ProcessInfo";
                    objSQLDA.SelectCommand = objSQLComm;
                    objSQLDA.Fill(objDTBuffer);

                    strSQL = "===================================================================================================" + "  \r\n" +
                             "���̣�" + objDTSQL.Rows[i]["����ID"].ToString().Trim() + "  \r\n" +
                             "�ͻ��ˣ�" + objDTSQL.Rows[i]["�ͻ�������"].ToString().Trim() + "  \r\n" +
                             "����" + objDTSQL.Rows[i]["����"].ToString().Trim() + "  \r\n" +
                             "������� SQL ��䣺" + "\r\n" + (objDTBuffer.Rows.Count > 0 ? objDTBuffer.Rows[0]["EventInfo"].ToString().Trim().Replace("\0", "\r\n    ") : "") + 
                             "  \r\n\r\n";

                    objDTSQL.Rows[i]["���ִ�����"] = strSQL;
                }
                catch (System.Exception e)
                {
                    MessageBox.Show(e.Message, "ϵͳ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                     SELECT CONVERT(INT, REQ_SPID) AS SPID, LTRIM(RTRIM(P.HOSTNAME)) AS �ͻ�������,         
                            RTRIM(LTRIM(RSC_DBID)) AS [���ݿ� ID], LTRIM(RTRIM(D.NAME)) AS ���ݿ�����,
                            LTRIM(RTRIM(RSC_OBJID)) AS [���� ID], LTRIM(RTRIM(O.[NAME]))  AS ��������, 
                            rsc_type AS [������ ID], SUBSTRING (V.NAME, 1, 4) AS ������,
                            (CASE L.rsc_type WHEN 1 THEN 'NULL ��Դ��δʹ�ã�' WHEN 2 then '���ݿ�' WHEN 3 THEN '�ļ�' WHEN 4 THEN '����' 
                                             WHEN 5 THEN '��' WHEN 6 then 'ҳ' WHEN 7 THEN '��' WHEN 8 THEN '��' WHEN 9 THEN 'RID���� ID��' 
                                             WHEN 10 THEN 'Ӧ�ó���' END) AS ������Դ����,
                            SUBSTRING(U.NAME, 1, 8) AS MODE,  
                            (CASE L.req_mode WHEN 1 THEN '�ܹ��ȶ���' WHEN 2 THEN '�ܹ��޸�' WHEN 3 THEN '����' WHEN 4 THEN '����' 
                                             WHEN 5 THEN '����' WHEN 6 THEN '������' WHEN 7 THEN '�������' WHEN 8 THEN '��������' 
                                             WHEN 9 THEN '�����������' WHEN 10 THEN '������������' WHEN 11 THEN '������������' ELSE SUBSTRING(U.NAME, 1, 8)
                             END) AS ������ģʽ, 
                            SUBSTRING(RSC_TEXT, 1, 32) AS ������Դ, 
                            (CASE L.REQ_STATUS WHEN 1 THEN '������' WHEN 2 THEN '����ת��' WHEN 3 THEN '���ڵȴ�' END) AS �������״̬,
                            SUBSTRING (X.NAME, 1, 5) AS [LOCK STATUS], RSC_INDID AS [��Դ���������� ID], 
                            req_transactionID AS [���� ID], 
                            LTRIM(RTRIM(P.PROGRAM_NAME)) AS ���г���,
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
                    objSQLComm.CommandText = "SELECT RTRIM(LTRIM(database_id)) AS [DBID], LTRIM(RTRIM([NAME])) AS ���ݿ����� From MASTER.SYS.DATABASES WITH(NOLOCK) WHERE NOT NAME IN ('MASTER', 'ReportServer', 'ReportServerTempDB') ";
                }
                else
                {
                    objSQLComm.CommandText = "SELECT RTRIM(LTRIM(DBID)) AS [DBID], LTRIM(RTRIM([NAME])) AS ���ݿ����� From MASTER.DBO.SYSDATABASES WITH(NOLOCK) WHERE NOT NAME IN ('MASTER', 'ReportServer', 'ReportServerTempDB') ";
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


            strSQL = strSQL + @" --ORDER BY SPID, �ͻ�������; ";

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
                    MessageBox.Show("���ݿ�汾Ϊ Microsoft SQL Server 2000 �����°汾������ʹ�á�", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }

                strSQL = @"SELECT TOP " + intTop.ToString() + @" SQL_HANDLE AS N'SQL �����', creation_time  N'������ʱ��', last_execution_time  N'���ִ��ʱ��', 
                                   last_elapsed_time/1000 N'���ִ������ʱ��(ms)', last_worker_time /1000 N'���ִ�мƻ�����ʱ��(ms)', 
                                   execution_count  N'ִ�д���', 
                                   last_physical_reads N'���ִ�����������', last_logical_reads N'���ִ���߼�������',
                                   total_physical_reads/execution_count N'ƽ�������ȡ����', total_physical_reads N'�����ȡ�ܴ���', 
                                   total_logical_reads/execution_count N'ƽ���߼�������',total_logical_reads  N'�߼���ȡ�ܴ���',
                                   total_logical_writes N'�߼�д���ܴ���',  total_worker_time/1000 N'����CPU��ʱ��(ms)', 
                                   total_elapsed_time/1000  N'ִ�кϼƻ���ʱ��(ms)', (total_elapsed_time / execution_count)/1000  N'ƽ��ִ��ʱ��(ms)',
                                    SUBSTRING(st.text, (qs.statement_start_offset/2) + 1,((CASE statement_end_offset WHEN -1 THEN DATALENGTH(st.text) ELSE qs.statement_end_offset END - qs.statement_start_offset)/2) + 1) N'ִ�����'
                            FROM sys.dm_exec_query_stats AS qs CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) ST
                            where SUBSTRING(st.text, (qs.statement_start_offset/2) + 1,
                                     (((CASE statement_end_offset WHEN -1 THEN DATALENGTH(st.text) ELSE qs.statement_end_offset END) - qs.statement_start_offset) / 2) + 1) not like '%fetch%'
                            ORDER BY ";
                switch (intOrderBy)
                {
                    case 1:
                        strSQL = strSQL + "[���ִ��ʱ��] ";
                        break;
                    case 2:
                        strSQL = strSQL + "[���ִ������ʱ��(ms)] ";
                        break;
                    case 3:
                        strSQL = strSQL + "[���ִ�мƻ�����ʱ��(ms)] ";
                        break;
                    case 4:
                        strSQL = strSQL + "[���ִ�����������] ";
                        break;
                    case 5:
                        strSQL = strSQL + "[���ִ���߼�������] ";
                        break;
                    case 6:
                        strSQL = strSQL + "[ƽ���߼�������] ";
                        break;
                    case 7:
                        strSQL = strSQL + "[ƽ�������ȡ����] ";
                        break;
                    case 8:
                        strSQL = strSQL + "[ƽ��ִ��ʱ��(ms)] ";
                        break;
                    case 9:
                        strSQL = strSQL + "[ִ�д���] ";
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
                    objDTView.RowFilter = "[�������� ID] = 0";

                    for (int i = 0; i < objDTView.Count; i++)
                    {
                        objSQLComm.CommandText = "KILL " + objDTView[i]["���� ID"].ToString().Trim();
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
            objSFD.Filter = "�ı��ļ� (*.TXT)|*.TXT";
            objSFD.ShowDialog();
            if (objSFD.FileName != "")
            {
                this.tbFileName.Text = objSFD.FileName;
            }
        }

        private void btnSaveBlock_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog objSFD = new SaveFileDialog();
            objSFD.Filter = "�ı��ļ� (*.TXT)|*.TXT";
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
            _objItem.value = -1; _objItem.Code = "ALL"; _objItem.Name = "����";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 0;  _objItem.Code = "NULL";  _objItem.Name = "����Ȩ������Դ";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 1; _objItem.Code = "Sch-S"; _objItem.Name = "�ܹ��ȶ���";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 2; _objItem.Code = "Sch-M"; _objItem.Name = "�ܹ��޸�";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 3; _objItem.Code = "S"; _objItem.Name = "����";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 4; _objItem.Code = "U"; _objItem.Name = "����";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 5; _objItem.Code = "X"; _objItem.Name = "����";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 6; _objItem.Code = "IS"; _objItem.Name = "������";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 7; _objItem.Code = "IU"; _objItem.Name = "�������";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 8; _objItem.Code = "IX"; _objItem.Name = "��������";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 9; _objItem.Code = "SIU"; _objItem.Name = "�����������";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 10; _objItem.Code = "SIX"; _objItem.Name = "������������";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 11; _objItem.Code = "UIX"; _objItem.Name = "������������";
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
            _objItem.value = -1; _objItem.Code = "ALL"; _objItem.Name = "����";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 1; _objItem.Code = "NUL"; _objItem.Name = "NULL ��Դ��δʹ�ã�";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 2; _objItem.Code = "DB"; _objItem.Name = "���ݿ�";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 3; _objItem.Code = "FIL"; _objItem.Name = "�ļ�";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 4; _objItem.Code = "IND"; _objItem.Name = "����";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 5; _objItem.Code = "TAB"; _objItem.Name = "��";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 6; _objItem.Code = "PAG"; _objItem.Name = "ҳ";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 7; _objItem.Code = "KEY"; _objItem.Name = "��";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 8; _objItem.Code = "EXT"; _objItem.Name = "��";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 9; _objItem.Code = "RID"; _objItem.Name = "RID���� ID��";
            _LockRequestType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 10; _objItem.Code = "APP"; _objItem.Name = "Ӧ�ó���";
            _LockRequestType.Add(_objItem);

            return _LockRequestType;
        }

        public System.Collections.ArrayList RequestStatusColect()
        {
            SQLServerDBEnumType _objItem;
            System.Collections.ArrayList _RequestStatusType = new System.Collections.ArrayList();

            _objItem = new SQLServerDBEnumType();
            _objItem.value = -1; _objItem.Code = "ALL"; _objItem.Name = "����";
            _RequestStatusType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 1; _objItem.Code = "GRANT"; _objItem.Name = "������";
            _RequestStatusType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 2; _objItem.Code = "CNVT"; _objItem.Name = "����ת��";
            _RequestStatusType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 3; _objItem.Code = "WAIT"; _objItem.Name = "���ڵȴ�";
            _RequestStatusType.Add(_objItem);

            _objItem = new SQLServerDBEnumType();
            _objItem.value = 4; _objItem.Code = "RELN"; _objItem.Name = "RELN";
            _RequestStatusType.Add(_objItem);

             _objItem = new SQLServerDBEnumType();
            _objItem.value = 5; _objItem.Code = "BLCKN"; _objItem.Name = "������";
            _RequestStatusType.Add(_objItem);

            return _RequestStatusType;
        }
    }

}
