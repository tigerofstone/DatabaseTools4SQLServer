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
    public partial class U9DBAndTableInfoForm : Form
    {
        public string strConnection = "";
        public string strInstance = "";
        public string strUser = "";
        public string strPassword = "";
        public bool blnIntegrated = false;

        public int intCareU9DBInfo = 1;
        public int intCheckU9DB = 1;

        public System.Data.SqlClient.SqlConnection objSQLConnect = new System.Data.SqlClient.SqlConnection();
        
        private DataSet objU9DS = new DataSet();
        private DataTable objU9DB = new DataTable();
        private DataTable objU9Table = new DataTable();
        private DataTable objU9Table_Chield = new DataTable();
        private DataTable objU9Table_Third = new DataTable();
        private DataTable objU9TableYearInfo = new DataTable();
        private DataSet objU9DS4Org = new DataSet();
        private DataTable objU9Org= new DataTable();
        private DataTable objU9DB4Org= new DataTable();

        public U9DBAndTableInfoForm()
        {
            InitializeComponent();            
        }

        private void DBAndTableInfoForm_Resize(object sender, EventArgs e)
        {           
            this.utcMainInfo.Left = 0;
            this.utcMainInfo.Top = 79;

            this.utcMainInfo.Width = this.Width - 7;
            this.utcMainInfo.Height = this.Height - 103;

            if (this.upbInfoGress.Visible) this.SetProgressForm();
        }

        private void DBAndTableInfoForm_Load(object sender, EventArgs e)
        {
            DBAndTableInfoForm_Resize(sender, e);

            this.InitFormControl();

            this.SetU9DT();
        }

        private void InitFormControl()
        {
            this.ugDBInfo.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ugDBInfo.DisplayLayout.Override.AllowMultiCellOperations = Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.All;

            this.ugDBU9Tables.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ugDBU9Tables.DisplayLayout.Override.AllowMultiCellOperations = Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.All;
            
            this.ugTable.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ugTable.DisplayLayout.Override.AllowMultiCellOperations = Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.All;
            
            this.ugU9Tables.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ugU9Tables.DisplayLayout.Override.AllowMultiCellOperations = Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.All;

            this.Text = this.Text + "    数据库：" + strInstance;

            this.ugU9Tables.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            
        }

        

        private void ugDBU9Tables_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            e.Layout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;

        }

        private void ugDBInfo_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            e.Layout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowAllTablesInfoDetail();
                
                if (this.cbU9ShowOrgInfo.Checked)
                {
                    this.ShowU9OrganizitionInfo();
                }
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message + "\r\n" + E.StackTrace);
            }
        }

        private void btnSelectField_Click(object sender, EventArgs e)
        {
            SaveFileDialog ExportFileDialog = new SaveFileDialog();

            ExportFileDialog.Filter = "Excel 97-2003 工作簿 (*.xls)|*.xls";
            ExportFileDialog.FilterIndex = 2;
            ExportFileDialog.RestoreDirectory = true;
            ExportFileDialog.CheckFileExists = false;

            if (ExportFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.tbFileName.Text = ExportFileDialog.FileName.Trim();
            }

            this.btnExport_Click(sender, e);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tbFileName.Text.Trim() != "")
                {
                    if (!System.IO.File.Exists(this.tbFileName.Text))
                    {
                        this.ExportExcel();
                        MessageBox.Show("导出 Excel 文件成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {                       
                        DialogResult sctReturn;
                        sctReturn = MessageBox.Show("文件已存在，是否覆盖此文件？。", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (sctReturn == System.Windows.Forms.DialogResult.Yes)
                        {
                            this.ExportExcel();
                            MessageBox.Show("导出 Excel 文件成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("文件导出错误。/r/n" + ex.InnerException, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExportExcel()
        {
            Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ugeeExporter = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter();
            Infragistics.Excel.Workbook objWB = new Infragistics.Excel.Workbook();

            try
            {
                ugeeExporter.Export(this.ugDBInfo, objWB.Worksheets.Add("数据库信息"));
                ugeeExporter.Export(this.ugTable, objWB.Worksheets.Add(this.cbDataBase.Text + " 全部数据表信息"));
                ugeeExporter.Export(this.ugU9Tables, objWB.Worksheets.Add(this.cbDataBase.Text + "U9主要业务对象信息"));
                ugeeExporter.Export(this.ugDBU9Tables, objWB.Worksheets.Add("数据库中U9业务对象信息"));
                ugeeExporter.Export(this.ugU9Org, objWB.Worksheets.Add("U9组织信息"));
                objWB.Save(this.tbFileName.Text);
                //Infragistics.Excel.BIFF8Writer.WriteWorkbookToFile(objWB, this.tbFileName.Text);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                objWB = null;
                ugeeExporter.Dispose();
                ugeeExporter = null;
            }
        }


        public void ShowAllTablesInfoDetail()
        {
            this.SetProgressForm();
            this.upbInfoGress.Visible = true;
            this.SetFormObjectState(false);

            this.ShowAllTableInfo();

            this.upbInfoGress.Value = 0;
            this.upbInfoGress.Visible = false;

            this.ugTable.Text = "数据库 " + this.cbDataBase.Text.Trim() + " 中所有数据表信息";
            this.SetFormObjectState(true);
        }



        public void ShowAllDatabaseInfoDetail()
        {

            this.SetProgressForm();
            try
            {
                this.upbInfoGress.Visible = true;
                this.upbInfoGress.Text = "查询所有数据库";
                this.SetFormObjectState(false);

                this.ShowAllDatabase();
                this.ShowAllDatabaseInfo();

                this.ugDBInfo.DisplayLayout.Bands[1].Columns["数据库"].Hidden = true;
                this.ugDBInfo.DisplayLayout.Bands[2].Columns["数据库"].Hidden = true;

                this.upbInfoGress.Value = 0;
                this.upbInfoGress.Visible = false;
                this.SetFormObjectState(true);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void SetProgressForm()
        {
            this.upbInfoGress.Left = 35;
            this.upbInfoGress.Top = this.Height / 2 - 30;
            this.upbInfoGress.Width = this.Width - 70;
        }

        private void SetFormObjectState(bool blnState)
        {
            this.btnRefresh.Enabled = blnState;
            this.btnExport.Enabled = blnState;
            this.cbDataBase.Enabled = blnState;
            this.btnSelectField.Enabled = blnState;
            this.utcMainInfo.Enabled = blnState;
            this.cbIncludeAllTables.Enabled = blnState;
            this.chbIncludeU9.Enabled = blnState;
            this.cbYearInfo.Enabled = blnState;
            this.cbU9ShowOrgInfo.Enabled = blnState;
        }

        private void SetU9DT()
        {
            this.objU9DB.TableName = "U9Databases";
            this.objU9DB.Columns.Add("数据库编码", Type.GetType("System.Int32"));
            this.objU9DB.Columns.Add("账套编号", Type.GetType("System.String"));
            this.objU9DB.Columns.Add("数据库名称", Type.GetType("System.String"));
            this.objU9DB.Columns.Add("账套名称", Type.GetType("System.String"));
            this.objU9DB.Columns.Add("单位名称", Type.GetType("System.String"));
            this.objU9DS.Tables.Add(objU9DB);

            this.objU9Table.TableName = "U9Tables";
            this.objU9Table.Columns.Add("数据库名称", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("序号", Type.GetType("System.Int32"));
            this.objU9Table.Columns.Add("数据表名", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("业务对象", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("主表记录数", Type.GetType("System.Int32"));
            this.objU9Table.Columns.Add("子表记录数", Type.GetType("System.Int32"));
            this.objU9Table.Columns.Add("业务最早日期", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("业务最晚日期", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("主表数据空间", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("主表索引空间", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("子表数据空间", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("子表索引空间", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("数据类型", Type.GetType("System.String"));
            this.objU9DS.Tables.Add(objU9Table);

            this.objU9TableYearInfo.TableName = "U9TablesYearInfo";
            this.objU9TableYearInfo.Columns.Add("数据库名称", Type.GetType("System.String"));
            this.objU9TableYearInfo.Columns.Add("数据表名", Type.GetType("System.String"));
            this.objU9TableYearInfo.Columns.Add("业务对象", Type.GetType("System.String"));
            this.objU9TableYearInfo.Columns.Add("主表时间年份", Type.GetType("System.String"));
            this.objU9TableYearInfo.Columns.Add("主表记录数", Type.GetType("System.Int32"));
            this.objU9TableYearInfo.Columns.Add("子表记录数", Type.GetType("System.Int32"));
            this.objU9DS.Tables.Add(objU9TableYearInfo);

            this.objU9DS.Relations.Add("U9TablesLink",
                   this.objU9DS.Tables["U9Databases"].Columns["数据库名称"],
                   this.objU9DS.Tables["U9Tables"].Columns["数据库名称"]);

            this.objU9DS.Relations.Add("U9TablesYearInfoLink",
                   new System.Data.DataColumn[] { this.objU9DS.Tables["U9Tables"].Columns["数据库名称"], this.objU9DS.Tables["U9Tables"].Columns["数据表名"] },
                   new System.Data.DataColumn[] { this.objU9DS.Tables["U9TablesYearInfo"].Columns["数据库名称"], this.objU9DS.Tables["U9TablesYearInfo"].Columns["数据表名"] });

            this.objU9DB4Org.TableName = "U9Databases4Org";
            this.objU9DB4Org.Columns.Add("账套编号", Type.GetType("System.String"));
            this.objU9DB4Org.Columns.Add("数据库名称", Type.GetType("System.String"));
            this.objU9DB4Org.Columns.Add("账套名称", Type.GetType("System.String"));
            this.objU9DB4Org.Columns.Add("单位名称", Type.GetType("System.String"));
            this.objU9DS4Org.Tables.Add(this.objU9DB4Org);

            this.objU9Org.TableName = "U9Org";
            this.objU9Org.Columns.Add("数据库名称", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("ID", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("组织编码", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("组织名称", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("组织形态", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("企业类型", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("法人组织", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("营运组织", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("核算组织", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("物流组织", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("工厂组织", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("人事组织", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("资产组织", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("服务组织", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("门户组织", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("结算中心", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("预算组织", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("管理类型", Type.GetType("System.String"));
            this.objU9DS4Org.Tables.Add(this.objU9Org);

            this.objU9DS4Org.Relations.Add("U9DBLink", this.objU9DS4Org.Tables["U9Databases4Org"].Columns["数据库名称"], this.objU9DS4Org.Tables["U9Org"].Columns["数据库名称"]);
        }

        private void ShowAllTableInfo()
        {
            System.Data.DataTable objDT = new DataTable();
            System.Data.DataSet objU9DS = new DataSet();

            if (this.cbIncludeAllTables.Checked)
            {
                objDT = GetTableAllInfo(this.cbDataBase.Text);
                this.ugTable.DataSource = objDT;
            }

            if (this.chbIncludeU9.Checked)
            {
                objU9DS = this.GetU9TableAllInfo(this.cbDataBase.Text, (this.cbYearInfo.Checked ? 1 : 0));
                this.ugU9Tables.DataSource = objU9DS;
                this.ugDBU9Tables.DataSource = this.objU9DB;

                if (this.cbYearInfo.Checked)
                {
                    this.ugU9Tables.DisplayLayout.Bands[1].Columns["数据表名"].Hidden = true;
                    this.ugU9Tables.DisplayLayout.Bands[1].Columns["业务对象"].Hidden = true;

                    this.ugDBU9Tables.DisplayLayout.Bands[1].Columns["数据库名称"].Hidden = true;
                    this.ugDBU9Tables.DisplayLayout.Bands[2].Columns["数据库名称"].Hidden = true;
                    this.ugDBU9Tables.DisplayLayout.Bands[2].Columns["数据表名"].Hidden = true;
                    this.ugDBU9Tables.DisplayLayout.Bands[2].Columns["业务对象"].Hidden = true;
                }

                this.ugDBU9Tables.DisplayLayout.Bands[1].Columns["数据表名"].Hidden = true;
            }


        }

        private void ShowAllDatabase()
        {
            DataTable objDT = null;

            objDT = this.GetAllDatabase(this.objSQLConnect);

            for (int i = 0; i < objDT.Rows.Count; i++)
            {
                this.cbDataBase.Items.Add(((string)objDT.Rows[i][0]).Trim());
            }

            if (this.cbDataBase.Items.Count > 0) this.cbDataBase.SelectedIndex = 0;
        }

        private void ShowAllDatabaseInfo()
        {
            DataSet objDS = null;

            objDS = this.DatabaseInfoDataSet();
            this.ugDBInfo.DataSource = objDS;
        }

        private DataSet DatabaseInfoDataSet()
        {
            DataTable objDTAllDB = null;
            DataTable[] objDTDBInfo = new DataTable[2];
            DataSet objDTAllDBInfo = new DataSet();

            objDTAllDB = null;

            objDTAllDB = this.GetAllDatabase(this.objSQLConnect);
            objDTDBInfo = this.GetDBInfo(objDTAllDB);

            objDTAllDBInfo.Tables.Add(objDTAllDB);
            objDTAllDBInfo.Tables.Add(objDTDBInfo[0]);
            objDTAllDBInfo.Tables.Add(objDTDBInfo[1]);

            objDTAllDBInfo.Relations.Add("DatabasesName_1",
                                           objDTAllDBInfo.Tables["Databases"].Columns["数据库"],
                                           objDTAllDBInfo.Tables["DatabaseDetail"].Columns["数据库"]);
            objDTAllDBInfo.Relations.Add("DatabasesName_2",
                               objDTAllDBInfo.Tables["DatabaseDetail"].Columns["数据库"],
                               objDTAllDBInfo.Tables["DatabaseFileDetail"].Columns["数据库"]);

            return objDTAllDBInfo;
        }


        private void ShowU9OrganizitionInfo()
        {
            this.getU9Organizations(this.cbDataBase.Text.Trim());
            this.ugU9Org.DataSource = this.objU9DS4Org;

            this.ugU9Org.DisplayLayout.Bands[1].Columns["数据库名称"].Hidden = true;
        }

 




/*
 * 数据库操作
 */
        private DataTable GetAllDatabase(System.Data.SqlClient.SqlConnection objConn)
        {
            try
            {
                System.Data.DataTable objDTSQL = new DataTable();
                System.Data.SqlClient.SqlDataAdapter objSQLDA = new System.Data.SqlClient.SqlDataAdapter();
                System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();
                System.Data.SqlClient.SqlDataReader objSQLReader;

                string strVersion = "";

                objConn = this.OpenSQLConnection(objConn, "Master");                   

                objSQLComm = this.objSQLConnect.CreateCommand();
                objSQLComm.CommandTimeout = 600;

                objSQLComm.CommandText = @"SELECT @@VERSION AS 数据库版本";
                objSQLReader = objSQLComm.ExecuteReader();

                if (objSQLReader.Read())
                {
                    strVersion = objSQLReader.GetString(0).Trim();
                }

                objSQLReader.Close();
                if (strVersion.IndexOf("Microsoft SQL Server  2000") >= 0)
                {
                    objSQLComm.CommandText = @"SELECT LTRIM(RTRIM([NAME])) AS 数据库, DBID AS [数据库 ID] FROM sysdatabases ORDER BY [NAME]";
                }
                else
                {
                    objSQLComm.CommandText = @"SELECT LTRIM(RTRIM([NAME])) AS 数据库, DTB.DATABASE_ID AS [数据库 ID]
                                                FROM master.sys.databases AS dtb LEFT OUTER JOIN sys.database_mirroring AS dmi ON dmi.database_id = dtb.database_id
                                                WHERE (CAST(case when dtb.name in ('master','model','msdb','tempdb') then 1 else dtb.is_distributor end AS bit)=0 and CAST(isnull(dtb.source_database_id, 0) AS bit)=0 
                                                            and (
                                                                CASE when DATABASEPROPERTY(dtb.name,'IsShutDown') is null then 0x200 else 0 end |
                                                                case when 1 = dtb.is_in_standby then 0x40 else 0 end |
                                                                case when 1 = dtb.is_cleanly_shutdown then 0x80 else 0 end |
                                                                case dtb.state when 1 then 0x2 when 2 then 0x8 when 3 then 0x4 when 4 then 0x10 when 5 then 0x100 when 6 then 0x20 else 1 end) & '62'=0)
                                                ORDER BY dtb.name ASC";
                }

                objDTSQL.Clear();
                objDTSQL.TableName = "Databases";
                objSQLDA.SelectCommand = objSQLComm;
                objSQLDA.Fill(objDTSQL);

                return objDTSQL;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

       
        private System.Data.DataTable[] GetDBInfo(DataTable objDTAllDB)
        {
            try
            {
                string strError = "";

                System.Data.SqlClient.SqlConnection objConn = new System.Data.SqlClient.SqlConnection();
                DataSet objDSSQL = new DataSet();
                DataTable objDTResult = new DataTable();
                System.Data.DataTable objDTResult_1 = new DataTable();
                System.Data.SqlClient.SqlDataAdapter objSQLDA = new System.Data.SqlClient.SqlDataAdapter();
                System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();

                objDTResult.TableName = "DatabaseDetail";
                objDTResult.Columns.Add("数据库", Type.GetType("System.String"));
                objDTResult.Columns.Add("数据库总空间", Type.GetType("System.String"));
                objDTResult.Columns.Add("数据空间", Type.GetType("System.String"));
                objDTResult.Columns.Add("数据已使用空间", Type.GetType("System.String"));
                objDTResult.Columns.Add("索引已使用空间", Type.GetType("System.String"));
                objDTResult.Columns.Add("日志空间", Type.GetType("System.String"));
                objDTResult.Columns.Add("日志已使用空间", Type.GetType("System.String"));
                objDTResult.Columns.Add("未分配空间", Type.GetType("System.String"));
                objDTResult.Columns.Add("保留空间", Type.GetType("System.String"));
                objDTResult.Columns.Add("未使用空间", Type.GetType("System.String"));

                objDTResult_1.TableName = "DatabaseFileDetail";
                objDTResult_1.Columns.Add("数据库", Type.GetType("System.String"));
                objDTResult_1.Columns.Add("Name", Type.GetType("System.String"));
                objDTResult_1.Columns.Add("FileID", Type.GetType("System.Int32"));
                objDTResult_1.Columns.Add("FileName", Type.GetType("System.String"));
                objDTResult_1.Columns.Add("Usage", Type.GetType("System.String"));
                objDTResult_1.Columns.Add("Size", Type.GetType("System.String"));
                objDTResult_1.Columns.Add("FileGroup", Type.GetType("System.String"));
                objDTResult_1.Columns.Add("Growth", Type.GetType("System.String"));

                this.upbInfoGress.Maximum = objDTAllDB.Rows.Count;
                this.upbInfoGress.Value = 0;

                for (int i = 0; i < objDTAllDB.Rows.Count; i++)
                {
                    try
                    {
                        this.upbInfoGress.Text = "打开 " + ((string)(objDTAllDB.Rows[i]["数据库"])).Trim() + " 连接    [Completed] of [Range] Completed";
                        Application.DoEvents();

                        objConn = this.OpenSQLConnection(objConn, (string)(objDTAllDB.Rows[i]["数据库"]));

                        this.upbInfoGress.Text = "添加 " + ((string)(objDTAllDB.Rows[i]["数据库"])).Trim() + " 基本信息    [Completed] of [Range] Completed";
                        Application.DoEvents();

                        objDSSQL.Clear();
                        objSQLComm = objConn.CreateCommand();
                        objSQLComm.CommandTimeout = 600;

                        objSQLComm.CommandText = @"SP_SPACEUSED;" + "\r\n" +
                                                 @"CREATE TABLE #TRAN_LOG_SPACE_USAGE (DATABASE_NAME SYSNAME, LOG_SIZE_MB FLOAT, LOG_SPACE_USED FLOAT, STATUS INT); 
                                               INSERT INTO #TRAN_LOG_SPACE_USAGE EXEC('DBCC SQLPERF(LOGSPACE)') ; 
                                               SELECT CAST(LOG_SIZE_MB AS DECIMAL(10,2)) AS 日志空间, 
                                                      CAST(CONVERT(FLOAT,LOG_SPACE_USED) AS DECIMAL(10,1)) AS [日志已使用比例 (%)], 
                                                      CAST(LOG_SIZE_MB * LOG_SPACE_USED / 100 AS DECIMAL(10,2))  AS 日志已使用空间, 
                                                      CAST(CONVERT(FLOAT,(100-LOG_SPACE_USED)) AS DECIMAL(10,1)) AS [日志未使用比例 (%)], 
                                                      CAST(LOG_SIZE_MB * (100 - LOG_SPACE_USED) /100 AS DECIMAL(10,2)) AS 日志未使用空间
                                               FROM #TRAN_LOG_SPACE_USAGE 
                                               WHERE DATABASE_NAME = DB_NAME()
                                               DROP TABLE #TRAN_LOG_SPACE_USAGE";
                        objSQLComm.ExecuteNonQuery();
                        objSQLDA.SelectCommand = objSQLComm;
                        objSQLDA.Fill(objDSSQL);

                        if (objDSSQL.Tables.Count > 0)
                        {
                            objDTResult.Rows.Add(new object[] { 
                                                            ((string)(objDTAllDB.Rows[i]["数据库"])).Trim(),
                                                            ((string)(objDSSQL.Tables[0].Rows[0]["database_size"])).Trim(),
                                                            (Decimal.Parse(((string)(objDSSQL.Tables[0].Rows[0]["database_size"])).Trim().Substring(0, ((string)(objDSSQL.Tables[0].Rows[0]["database_size"])).Trim().Length - 2))
                                                              -(Decimal)objDSSQL.Tables[2].Rows[0]["日志空间"]).ToString() + " MB",
                                                            ((string)(objDSSQL.Tables[1].Rows[0]["data"])).Trim(),
                                                            ((string)(objDSSQL.Tables[1].Rows[0]["index_size"])).Trim(),                        
                                                            ((Decimal)objDSSQL.Tables[2].Rows[0]["日志空间"]).ToString("0.00").Trim() + " MB",
                                                            ((Decimal)objDSSQL.Tables[2].Rows[0]["日志已使用空间"]).ToString("0.00").Trim() + " MB",
                                                            ((string)(objDSSQL.Tables[0].Rows[0]["unallocated space"])).Trim(),
                                                            ((string)(objDSSQL.Tables[1].Rows[0]["reserved"])).Trim(),
                                                            ((string)(objDSSQL.Tables[1].Rows[0]["unused"])).Trim()
                                                          });
                        }

                        this.upbInfoGress.Text = "添加 " + ((string)(objDTAllDB.Rows[i]["数据库"])).Trim() + " 文件信息    [Completed] of [Range] Completed";
                        Application.DoEvents();

                        objDSSQL = null;
                        objDSSQL = new DataSet();
                        objSQLComm.CommandText = @"SP_HELPDB '" + ((string)(objDTAllDB.Rows[i]["数据库"])).Trim() + "'";
                        objSQLComm.ExecuteNonQuery();
                        objSQLDA.SelectCommand = objSQLComm;
                        objSQLDA.Fill(objDSSQL);
                        if (objDSSQL.Tables.Count > 0)
                        {
                            for (int j = 0; j < objDSSQL.Tables[1].Rows.Count; j++)
                            {
                                objDTResult_1.Rows.Add(new object[] { ((string)(objDTAllDB.Rows[i]["数据库"])).Trim(),
                                                                  ((string)(objDSSQL.Tables[1].Rows[j]["name"])).Trim(),
                                                                   objDSSQL.Tables[1].Rows[j]["fileid"],
                                                                   ((string)(objDSSQL.Tables[1].Rows[j]["filename"])).Trim(),
                                                                   ((string)(objDSSQL.Tables[1].Rows[j]["usage"])).Trim(),
                                                                   (Double.Parse(objDSSQL.Tables[1].Rows[j]["size"].ToString().Substring(0, objDSSQL.Tables[1].Rows[j]["size"].ToString().Length-3)) / 1024.00).ToString("0.00") + " MB",
                                                                   (objDSSQL.Tables[1].Rows[j]["filegroup"] != System.DBNull.Value ? ((string)(objDSSQL.Tables[1].Rows[j]["filegroup"])).Trim() : "NULL"),
                                                                   ((string)(objDSSQL.Tables[1].Rows[j]["growth"])).Trim() });
                            }
                        }

                        objSQLComm = null;
                        objConn.Close();
                        this.upbInfoGress.Text = "完成 " + ((string)(objDTAllDB.Rows[i]["数据库"])).Trim() + " 文件信息    [Completed] of [Range] Completed";

                        this.upbInfoGress.Value = i + 1;
                        //this.upbInfoGress.Refresh();
                        Application.DoEvents();
                    }
                    catch (Exception exp)
                    {
                        strError = strError + "\r\n\r\n" + exp.Message;
                    }
                }
                return new DataTable[2] { objDTResult, objDTResult_1 };
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private System.Data.DataTable GetTableAllInfo(string strDBName)
        {
            
            System.Data.SqlClient.SqlConnection objConn = new System.Data.SqlClient.SqlConnection();
            DataTable objDTResult = new DataTable();
            DataTable objDTTables = new DataTable();
            System.Data.SqlClient.SqlDataReader objDR;
            System.Data.SqlClient.SqlDataAdapter objSQLDA = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();

            objDTResult.TableName = "DatabaseDetail";
            objDTResult.Columns.Add("表名", Type.GetType("System.String"));
            objDTResult.Columns.Add("记录数", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("数据空间", Type.GetType("System.String"));
            objDTResult.Columns.Add("索引数据空间", Type.GetType("System.String"));
            objDTResult.Columns.Add("保留空间", Type.GetType("System.String"));
            objDTResult.Columns.Add("未使用空间", Type.GetType("System.String"));
            try
            {
                objConn = this.OpenSQLConnection(objConn, strDBName);                

                objSQLComm = objConn.CreateCommand();
                objSQLComm.CommandTimeout = 600;

                objSQLComm.CommandText = @"IF EXISTS(Select 1 Where @@version Like 'Microsoft SQL Server 2005%')
                                               SELECT LTRIM(RTRIM(S.[Name])) + '.' + LTRIM(RTRIM(T.[Name])) AS [NAME] FROM sys.tables T WITH(NOLOCK) INNER JOIN SYS.SCHEMAS S ON T.schema_id = S.schema_id WHERE [type] = 'U' Order By T.[NAME]
                                           ELSE
                                               SELECT [NAME] FROM sysobjects WITH(NOLOCK) WHERE [xtype] = 'U' Order By [NAME]";
                objSQLComm.ExecuteNonQuery();
                objSQLDA.SelectCommand = objSQLComm;
                objSQLDA.Fill(objDTTables);

                this.upbInfoGress.Maximum = objDTTables.Rows.Count;
                this.upbInfoGress.Value = 0;
                this.upbInfoGress.Text = "打开 " + strDBName.Trim() + " 数据库中数据表    [Completed] Of [Range] Completed";

                for (int i = 0; i < objDTTables.Rows.Count; i++)
                {
                    objSQLComm.CommandText = @"EXEC SP_SPACEUSED N'" + ((string)objDTTables.Rows[i]["NAME"]).Trim() + @"'";
                    objDR = objSQLComm.ExecuteReader();

                    if (objDR.Read())
                    {
                        objDTResult.Rows.Add(new object[] { objDR.GetString(0), int.Parse(objDR.GetString(1)), objDR.GetString(3), objDR.GetString(4), objDR.GetString(2), objDR.GetString(5) });
                    }
                    objDR.Close();

                    this.upbInfoGress.Value = i + 1;
                    Application.DoEvents();
                }
                return objDTResult;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return objDTResult;
            }
        }


        private System.Data.DataSet GetU9TableAllInfo(string strDBName, int iIncludeYearInfo)
        {            
            System.Data.SqlClient.SqlConnection objConn = new System.Data.SqlClient.SqlConnection();
            DataSet objDSResult = new DataSet();
            DataTable objDTResult = new DataTable();
            DataTable objDTResultYearInfo = new DataTable();
            DataTable objDTTables = new DataTable();
            System.Data.SqlClient.SqlDataReader objDR = null;
            System.Data.SqlClient.SqlDataAdapter objSQLDA = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();
            System.Xml.XmlDocument objXMLDom = new System.Xml.XmlDocument();
            System.Xml.XmlElement objXMLEle;
            string strVersion = "";
            int intRow = 0;

            string strMisTable = "";
            //string strSQL = "";
            int intIncludeU9DB = 0;

            int int主表记录数 = 0;                  int int子表记录数 = 0;
            string str主表数据空间 = "";            string str主表索引空间 = "";
            string str子表数据空间 = "";            string str子表索引空间 = "";
            string str业务最早日期 = "";            string str业务最晚日期 = "";
            //int intYear主表记录数 = 0; int intYear子表记录数 = 0;

            //int intYear主表时间年份;

            objDTResult.TableName = "ObjectDetail";
            objDTResult.Columns.Add("序号", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("数据表名", Type.GetType("System.String"));
            objDTResult.Columns.Add("业务对象",     Type.GetType("System.String"));
            objDTResult.Columns.Add("主表记录数",   Type.GetType("System.Int32"));
            objDTResult.Columns.Add("子表记录数",   Type.GetType("System.Int32"));
            objDTResult.Columns.Add("业务最早日期", Type.GetType("System.String"));
            objDTResult.Columns.Add("业务最晚日期", Type.GetType("System.String"));
            objDTResult.Columns.Add("主表数据空间", Type.GetType("System.String"));
            objDTResult.Columns.Add("主表索引空间", Type.GetType("System.String"));
            objDTResult.Columns.Add("子表数据空间", Type.GetType("System.String"));
            objDTResult.Columns.Add("子表索引空间", Type.GetType("System.String"));
            objDTResult.Columns.Add("数据类型", Type.GetType("System.String"));

            objDTResultYearInfo.TableName = "YearInfo";
            objDTResultYearInfo.Columns.Add("数据表名", Type.GetType("System.String"));
            objDTResultYearInfo.Columns.Add("业务对象", Type.GetType("System.String"));
            objDTResultYearInfo.Columns.Add("主表维度信息", Type.GetType("System.String"));
            objDTResultYearInfo.Columns.Add("主表记录数", Type.GetType("System.Int32"));
            objDTResultYearInfo.Columns.Add("子表记录数", Type.GetType("System.Int32"));

            objDSResult.Tables.Add(objDTResult); objDSResult.Tables.Add(objDTResultYearInfo);
            objDSResult.Relations.Add("YearInfo", objDTResult.Columns["数据表名"], objDTResultYearInfo.Columns["数据表名"]);

            try
            {
                this.upbInfoGress.Text = "打开 " + strDBName + " 数据库";

                objConn = this.OpenSQLConnection(objConn, strDBName);

                objSQLComm = objConn.CreateCommand();
                objSQLComm.CommandTimeout = 600;

                if (this.intCheckU9DB == 1)
                {
                    this.upbInfoGress.Text = "判断数据库 " + strDBName + " 是否U9数据库";
                    if (this.CheckISU9DB(objSQLComm, objDR, strDBName, ref strVersion) <= 0)
                    {
                        //MessageBox.Show("此数据库不是 ERP-U9 数据库。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return objDSResult;
                    }
                }

                this.upbInfoGress.Text = "取数据库 " + strDBName + " 对应 U9 帐套信息";

                intIncludeU9DB = this.U9DBInfo(objSQLComm, objDR, strDBName);
                if (intIncludeU9DB <= 0 && this.intCareU9DBInfo == 1)
                {
                    return objDSResult;
                }                

                //========== 打开数据表 ============================================================================================================================================================
                objXMLDom.Load(Application.StartupPath + "\\U9ERPConfig.xml");
                System.Xml.XmlNodeList objXMLNL;
                objXMLNL = objXMLDom.SelectNodes("./Config/U9Tables[@Version = 'V3.0']/Table");

                this.upbInfoGress.FillAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                this.upbInfoGress.Refresh();
                this.upbInfoGress.Maximum = objXMLNL.Count;
                this.upbInfoGress.Value = 0;
                this.upbInfoGress.Text = "打开 " + strDBName.Trim() + " 数据库中主要的 ERP-U9 数据表    [Completed] Of [Range] Completed                  ";


                for (int i = 0; i < objXMLNL.Count; i++)
                {
                    int主表记录数 = 0;             int子表记录数 = 0;
                    str主表数据空间 = "";          str主表索引空间 = "";
                    str子表数据空间 = "";          str子表索引空间 = "";
                    str业务最早日期 = "";          str业务最晚日期 = "";

                    //intYear主表时间年份 = 0;

                    objXMLEle = (System.Xml.XmlElement)objXMLNL[i];

                    #region 判断数据表存在
                    //=================================//
                    objSQLComm.CommandText = @"SELECT * FROM sysobjects With(nolock) 
                                               WHERE TYPE = 'U' AND NAME = '" + objXMLEle.Attributes["TableName"].Value.ToString().Trim() + "'";
                    objDR = objSQLComm.ExecuteReader();
                    if (!objDR.Read())
                    {
                        strMisTable = strMisTable + "    业务对象：" + objXMLEle.Attributes["Object"].Value.ToString().Trim() +
                                                    "    主表 [" + objXMLEle.Attributes["TableName"].Value.ToString() + "]\r\n";
                        objDR.Close();
                        continue;
                    }
                    if (!objDR.IsClosed) objDR.Close();

                    if (objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() != "")
                    {
                        //=================================//
                        objSQLComm.CommandText = @"SELECT * FROM sysobjects With(nolock) 
                                                   WHERE TYPE = 'U' AND NAME = '" + objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() + "'";
                        objDR = objSQLComm.ExecuteReader();
                        if (!objDR.Read())
                        {
                            strMisTable = strMisTable + "    业务对象：" + objXMLEle.Attributes["Object"].Value.ToString().Trim() +
                                                        "    子表 [" + objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() + "]\r\n";
                            objDR.Close();
                            continue;
                        }
                        if (!objDR.IsClosed) objDR.Close();
                    }

                    //=================================//
                    #endregion

                    #region  查询数据表信息
                    objSQLComm.CommandText = @"EXEC SP_SPACEUSED N'" + objXMLEle.Attributes["TableName"].Value.ToString().Trim() + "'";
                    objDR = objSQLComm.ExecuteReader();

                    if (objDR.Read())
                    {
                        int主表记录数 = int.Parse(objDR.GetString(1));
                        str主表数据空间 = objDR.GetString(3);                        
                        str主表索引空间 = objDR.GetString(4);
                    }
                    if (!objDR.IsClosed) objDR.Close();
                   
                    //=================================//
                    if (objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() != "")
                    {
                        objSQLComm.CommandText = @"SELECT (SELECT CONVERT(NVARCHAR(30), MIN([" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + @"] ), 120) 
                                                           FROM [" + objXMLEle.Attributes["TableName"].Value.ToString().Trim() + @"] WITH(NOLOCK)) AS dMinDate, 
                                                          (SELECT CONVERT(NVARCHAR(30), MAX([" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + @"] ), 120) 
                                                           FROM [" + objXMLEle.Attributes["TableName"].Value.ToString().Trim() + @"] WITH(NOLOCK)) AS dMaxDate ";
                        objDR = objSQLComm.ExecuteReader();
                        if (objDR.Read())
                        {
                            str业务最早日期 = objDR.IsDBNull(0) ? "" : objDR.GetString(0);        str业务最晚日期 = objDR.IsDBNull(1) ? "" : objDR.GetString(1);

                            if (str业务最早日期.Length > 10)
                            {
                                str业务最早日期 = System.DateTime.Parse(str业务最早日期).ToString("yyyy-MM-dd");
                                str业务最晚日期 = System.DateTime.Parse(str业务最晚日期).ToString("yyyy-MM-dd");
                            }
                        }
                        objDR.Close();
                    }

                    if (objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() != "")
                    {
                        objSQLComm.CommandText = @"EXEC SP_SPACEUSED N'" + objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() + "'";
                        objDR = objSQLComm.ExecuteReader();

                        if (objDR.Read())
                        {
                            int子表记录数 = int.Parse(objDR.GetString(1));
                            str子表数据空间 = objDR.GetString(3);                          str子表索引空间 = objDR.GetString(4);
                        }
                        objDR.Close();
                    }     
                    #endregion

                    intRow = intRow + 1;
                    objDTResult.Rows.Add(new object[] { intRow, objXMLEle.Attributes["TableName"].Value.ToString().Trim(),
                                                        objXMLEle.Attributes["Object"].Value.ToString(), 
                                                        int主表记录数, int子表记录数, str业务最早日期, str业务最晚日期,
                                                        str主表数据空间, str主表索引空间, str子表数据空间, str子表索引空间, 
                                                        (objXMLEle.Attributes["Type"] != null ? objXMLEle.Attributes["Type"].Value.ToString().Trim() : "")
                                                      });
                    if (intIncludeU9DB != 2)
                    {
                        this.objU9Table.Rows.Add(new object[] {strDBName.Trim(), intRow, objXMLEle.Attributes["TableName"].Value.ToString().Trim(),
                                                          objXMLEle.Attributes["Object"].Value.ToString(), 
                                                          int主表记录数, int子表记录数, str业务最早日期, str业务最晚日期,
                                                          str主表数据空间, str主表索引空间, str子表数据空间, str子表索引空间,
                                                        (objXMLEle.Attributes["Type"] != null ? objXMLEle.Attributes["Type"].Value.ToString().Trim() : "")
                                                         });  
                        
                    }

                    #region 查找子表等其他信息

                    this.getU9ChieldTablesInfo(strDBName.Trim(), ref intRow, objXMLEle, objConn, objSQLComm, intIncludeU9DB, ref strMisTable, ref objDTResult);

                    #endregion

                    Application.DoEvents();

                    #region  查询主表各年数据量
                    //--------------- 查询主表各年数据量 -------------------------------------------------------------------------------------------------------------
                    if (iIncludeYearInfo == 1 && objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() != "")
                    {
                        this.getU9TableDimensionsInfo(strDBName, objXMLEle, objSQLComm, intIncludeU9DB, ref objDTResultYearInfo,ref strMisTable);
                    }
                    //----------------------------------------------------------------------------------------------------------------------------------------------
                    #endregion

                    this.upbInfoGress.Value = i + 1;
                    Application.DoEvents();
                }
                if (strMisTable != "")
                {
                    objXMLEle = (System.Xml.XmlElement)objXMLDom.SelectSingleNode("./Config/U9Tables");

                    if(objXMLEle.Attributes["Version"] != null)
                        MessageBox.Show("以下数据表在数据库中不存在：\r\n" + strMisTable.Substring(0, strMisTable.Length - 2) + "。\r\n" + 
                                        "可能由于与配置文件中 ERP-U9 版本：" + objXMLEle.Attributes["Version"].Value.ToString() + " 不一致。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("以下数据表在数据库中不存在：\r\n" + strMisTable.Substring(0, strMisTable.Length - 2) + "。\r\n" +
                                        "可能由于与配置文件中 ERP-U9 版本不一致。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return objDSResult;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return objDSResult;
            }
            finally
            {
                if (objDR != null)
                {
                    if (!objDR.IsClosed) objDR.Close();
                }
            }
        }

        /// <summary>
        /// 取下级表信息
        /// </summary>
        /// <param name="strDBName"></param>
        /// <param name="intRow"></param>
        /// <param name="objXMLEleTable"></param>
        /// <param name="objConn"></param>
        /// <param name="objComm"></param>
        /// <param name="intIncludeU9DB"></param>
        /// <param name="strMistake"></param>
        /// <param name="objDTResult"></param>
        private void getU9ChieldTablesInfo(string strDBName, ref int intRow, System.Xml.XmlElement objXMLEleTable, System.Data.SqlClient.SqlConnection objConn, 
                                           System.Data.SqlClient.SqlCommand objComm, int intIncludeU9DB, ref string strMistake, ref System.Data.DataTable objDTResult)
        {
            System.Xml.XmlElement _oXMLEleTable;
            System.Xml.XmlNodeList _oXMLNodListTables = null;
            System.Data.SqlClient.SqlDataReader _oRD;

            try
            {
                if (objXMLEleTable.HasChildNodes)
                {
                    _oXMLNodListTables = objXMLEleTable.ChildNodes;
                    for (int i = 0; i < _oXMLNodListTables.Count; i++)
                    {
                        if (_oXMLNodListTables[i].NodeType != System.Xml.XmlNodeType.Comment)
                        {
                            _oXMLEleTable = (System.Xml.XmlElement)_oXMLNodListTables[i];

                            #region 判断数据表存在

                            objComm.CommandText = @"SELECT * FROM sysobjects With(nolock) 
                                            WHERE TYPE = 'U' AND NAME = '" + _oXMLEleTable.Attributes["TableName"].Value.ToString().Trim() + "'";
                            _oRD = objComm.ExecuteReader();
                            if (!_oRD.Read())
                            {
                                strMistake = strMistake + "    业务对象：" + _oXMLEleTable.Attributes["Object"].Value.ToString().Trim() +
                                                            "    主表 [" + _oXMLEleTable.Attributes["TableName"].Value.ToString() + "]\r\n";
                                _oRD.Close();
                                continue;
                            }
                            if (!_oRD.IsClosed) _oRD.Close();

                            #endregion

                            #region 查询数据表信息

                            objComm.CommandText = @"EXEC SP_SPACEUSED N'" + _oXMLEleTable.Attributes["TableName"].Value.ToString().Trim() + "'";
                            _oRD = objComm.ExecuteReader();

                            if (_oRD.Read())
                            {
                                objDTResult.Rows.Add(new object[] { intRow++, _oXMLEleTable.Attributes["TableName"].Value.ToString().Trim() ,
                                                                 _oXMLEleTable.Attributes["Object"].Value.ToString(), 
                                                                 int.Parse(_oRD.GetString(1)), 0, "", "",
                                                                 _oRD.GetString(3), _oRD.GetString(4), 0, 0, 
                                                                 (_oXMLEleTable.Attributes["Type"] != null ? _oXMLEleTable.Attributes["Type"].Value.ToString().Trim() : "")
                                                               });
                                if (intIncludeU9DB != 2)
                                {
                                    this.objU9Table.Rows.Add(new object[] { strDBName, intRow, _oXMLEleTable.Attributes["TableName"].Value.ToString().Trim(),
                                                                        _oXMLEleTable.Attributes["Object"].Value.ToString(), 
                                                                        int.Parse(_oRD.GetString(1)), 0, "", "",
                                                                        _oRD.GetString(3), _oRD.GetString(4), 0, 0, 
                                                                       (_oXMLEleTable.Attributes["Type"] != null ? _oXMLEleTable.Attributes["Type"].Value.ToString().Trim() : "")
                                                                      });

                                }

                            }
                            if (!_oRD.IsClosed) _oRD.Close();

                            #endregion

                            Application.DoEvents();
                            //-----------------------------------
                            if (_oXMLEleTable.HasChildNodes) this.getU9ChieldTablesInfo(strDBName, ref intRow, _oXMLEleTable, objConn, objComm, intIncludeU9DB, ref strMistake, ref objDTResult);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                strMistake = strMistake + e.Message + "/r/n";
            }
            finally
            {

                _oXMLEleTable = null;
                _oXMLNodListTables = null;
                _oRD = null;
            }
        }

        private void getU9TableDimensionsInfo(string strDBName, System.Xml.XmlElement objXMLEle, System.Data.SqlClient.SqlCommand objSQLComm, int intIncludeU9DB,
                                              ref System.Data.DataTable objDTResultYearInfo, ref string strMisTable)
        {
            System.Data.SqlClient.SqlDataReader objDR = null;
            string strSQL = "";
            string strColumns = "";
            string strDimension = "";
            string strSelects = "";
            string intYear主表时间年份;  int intYear主表记录数;  int intYear子表记录数;

            if (objXMLEle.GetAttribute("TableName") == "SM_SO")
            {
                strSQL = "";
            }
            try
            {
                strColumns = objXMLEle.Attributes["DimensionsKey"].Value.ToString().Trim().Replace(", ", "', '");
                string[] _Str = objXMLEle.Attributes["DimensionsKey"].Value.ToString().Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
               
                objSQLComm.CommandText = @"SELECT Count(*) as Nums FROM SYSCOLUMNS C INNER JOIN SYSOBJECTS O ON C.ID=O.ID " +
                                          "WHERE O.NAME = '" + objXMLEle.Attributes["TableName"].Value.ToString().Trim() + "' " +
                                                "AND C.NAME IN( '" + strColumns + "')";
                if (objDR != null && !objDR.IsClosed) objDR.Close();
                objDR = objSQLComm.ExecuteReader();

                if (objDR.Read())
                {
                    if (objDR.GetInt32(0) != _Str.Length)
                    {
                        strMisTable = strMisTable + "数据表："+ objXMLEle.Attributes["TableName"].Value.ToString().Trim() + " 中汇总字段中包含不存在的字段；/r/n";
                        return;
                    }

                    strDimension = objXMLEle.Attributes["DimensionsKey"].Value.ToString().Trim().Replace(objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim(),
                                                                                                   "YEAR([" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + "])");
                    for (int i = 0; i < _Str.Length; i++)
                    {
                        strSelects = strSelects + "+ CAST(R." + _Str[i].Trim() + " AS NVARCHAR(100)) + '####'";
                    }
                    strSelects = strSelects.Substring(2, strSelects.Length - 10);
                    strSelects = strSelects.Replace("R." + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim(),
                                                   "YEAR(R.[" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + "]) ");

                    if (objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() != "" && objXMLEle.Attributes["KeyName"].Value.ToString().Trim() != "")
                    {
                        strSQL = @"DECLARE @T1 TABLE(iYear NVARCHAR(300), iMainCount INT)
                                DECLARE @T2 TABLE(iYear NVARCHAR(300), iDetailCount INT)

                                INSERT INTO @T1 (iYear,	iMainCount) 
                                SELECT " + strSelects + @" AS iBillYear, ISNULL(Count(*), 0) AS MainCount 
                                FROM [" + objXMLEle.Attributes["TableName"].Value.ToString().Trim() + @"] R WITH(NOLOCK) 
                                GROUP BY "+ strSelects  + @"

                                INSERT INTO @T2 (iYear,	iDetailCount) 
                                SELECT " + strSelects + @" AS iBillYear, ISNULL(Count(*), 0) AS DetailCount
                                FROM [" + objXMLEle.Attributes["TableName"].Value.ToString().Trim() + @"] R WITH(NOLOCK) LEFT JOIN [" + objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() + @"] RS WITH(NOLOCK) 
                                            ON R.ID = RS." + objXMLEle.Attributes["KeyName"].Value.ToString().Trim() + @"
                                GROUP BY " + strSelects + @"

                                SELECT ISNULL(A.iYear, '') AS iYear, iMainCount, ISNULL(iDetailCount,0) AS  iDetailCount FROM @T1 A LEFT JOIN @T2 B ON A.iYear = B.iYear ORDER BY A.iYear";
                    }
                    else
                    {
                        strSQL = @"SELECT ISNULL(" + strSelects + @", '') AS iBillYear, ISNULL(Count(*), 0) AS iMainCount, 0 AS iDetailCount " +
                                    "FROM [" + objXMLEle.Attributes["TableName"].Value.ToString().Trim() + "] R " +
                                    "GROUP BY " + strSelects + " " +
                                    "ORDER BY " + strSelects;
                    }
                    objSQLComm.CommandText = strSQL;
                    if (!objDR.IsClosed)
                    { objDR.Close(); objDR = null; }
                    objDR = objSQLComm.ExecuteReader();

                    if (objDR.HasRows)
                    {
                        while (objDR.Read())
                        {
                            try
                            {
                                intYear主表时间年份 = objDR.GetString(0);
                                intYear主表记录数 = objDR.GetInt32(1);
                                intYear子表记录数 = objDR.GetInt32(2);
                                objDTResultYearInfo.Rows.Add(new object[] { objXMLEle.Attributes["TableName"].Value.ToString().Trim(), objXMLEle.Attributes["Object"].Value.ToString(), 
                                                                        intYear主表时间年份.ToString(), intYear主表记录数, intYear子表记录数 });

                                if (intIncludeU9DB != 2)
                                    this.objU9TableYearInfo.Rows.Add(new object[] { strDBName.Trim(), objXMLEle.Attributes["TableName"].Value.ToString().Trim(), objXMLEle.Attributes["Object"].Value.ToString(), 
                                                                                intYear主表时间年份.ToString(), intYear主表记录数, intYear子表记录数 });
                            }
                            catch (Exception Ex)
                            {
                                strMisTable = strMisTable + "    业务对象：" + objXMLEle.Attributes["Object"].Value.ToString().Trim() +
                                                            " 读取年度数据有错误：" + Ex.Message + "；\r\n";
                                continue;
                            }
                        }
                    }
                }
                if (!objDR.IsClosed) objDR.Close();
            }
            catch (Exception e)
            {
                strMisTable = strMisTable + e.Message;
            }
            finally
            {
                if (objDR != null && !objDR.IsClosed) objDR.Close();
            }
        }

        private void getU9Organizations(string strDBName)
        {
            DataView view = new DataView();
            view.Table = this.objU9DB4Org;
            view.RowFilter = "数据库名称 = '" + strDBName.Trim() + "'";
            view.RowStateFilter = DataViewRowState.CurrentRows;
            if (view.Count <= 0)
            {
                SqlConnection _objSQLConn = new SqlConnection();
                SqlCommand _command = new SqlCommand();
                DataTable _dataTable = new DataTable();
                SqlDataAdapter _adapter = new SqlDataAdapter();
                this.upbInfoGress.Text = "打开 " + strDBName + " 数据库";
                _command = this.OpenSQLConnection(_objSQLConn, strDBName).CreateCommand();
                _command.CommandTimeout = 600;
                _command.CommandText = "SELECT O.ID, O.Code, OT.[Name], " + "\r\n" +                                           
                                            "(CASE O.OrgClassify WHEN 0 THEN '集团' WHEN 1 THEN '公司' WHEN 2 THEN '事业部'WHEN 3 THEN '工厂' WHEN 4 THEN '部门' END) AS 组织形态, " + "\r\n" +   
                                            "(CASE O.CompanyType WHEN 0 THEN '工业企业' WHEN 1 THEN '商品流通企业' END) AS 企业类型, " + "\r\n" +                                            
                                            "(CASE O.IsLegacyOrg WHEN 1 THEN '√' WHEN 0 THEN '' END) AS 法人组织, " + "\r\n" +                                            
                                            "(CASE O.IsOperatingOrg WHEN 1 THEN '√' WHEN 0 THEN '' END) AS 营运组织, " + "\r\n" +                                            
                                            "(CASE O.IsAuditOrg WHEN 1 THEN '√' WHEN 0 THEN '' END) AS 核算组织, " + "\r\n" +                                             
                                            "(CASE O.IsInventoryOrg WHEN 1 THEN '√' WHEN 0 THEN '' END) AS 物流组织, " + "\r\n" +                                             
                                            "(CASE O.IsPlantOrg WHEN 1 THEN '√' WHEN 0 THEN '' END) AS 工厂组织, " + "\r\n" +                                           
                                            "(CASE O.IsHrOrg WHEN 1 THEN '√' WHEN 0 THEN '' END) AS 人事组织, " + "\r\n" +                                           
                                            "(CASE O.IsAssetOrg WHEN 1 THEN '√' WHEN 0 THEN '' END) AS 资产组织, " + "\r\n" +                                           
                                            "(CASE O.IsServiceOrg WHEN 1 THEN '√' WHEN 0 THEN '' END) AS 服务组织, " + "\r\n" +                                           
                                            "(CASE O.IsPortalOrg WHEN 1 THEN '√' WHEN 0 THEN '' END) AS 门户组织, " + "\r\n" +                                           
                                            "(CASE O.IsSettlementOrg WHEN 1 THEN '√' WHEN 0 THEN '' END) AS 结算中心, " + "\r\n" +                                           
                                            "(CASE O.IsBudgetOrg WHEN 1 THEN '√' WHEN 0 THEN '' END) AS 预算组织, " + "\r\n" +
                                            "(CASE O.ManageType WHEN 1 THEN '外部物流' WHEN 0 THEN '内部物流' END) AS 管理类型 " + "\r\n" +
                                        "FROM Base_Organization O INNER JOIN  dbo.Base_Organization_Trl OT ON O.ID = OT.ID " + "\r\n" +
                                        "WHERE OT.SysMLFlag = 'zh-CN' ORDER BY O.Code, O.ID";
                _command.ExecuteNonQuery();
                _adapter.SelectCommand = _command;
                _adapter.Fill(_dataTable);
                this.objU9DB4Org.Rows.Add(new object[] { "", strDBName.Trim(), "", "" });
                for (int i = 0; i < _dataTable.Rows.Count; i++)
                {
                    this.objU9Org.Rows.Add(new object[] { strDBName.Trim(), _dataTable.Rows[i][0].ToString(), _dataTable.Rows[i][1].ToString(), 
                                                                            _dataTable.Rows[i][2].ToString(), _dataTable.Rows[i][3].ToString(), 
                                                                            _dataTable.Rows[i][4].ToString(), _dataTable.Rows[i][5].ToString(), 
                                                                            _dataTable.Rows[i][6].ToString(), _dataTable.Rows[i][7].ToString(), 
                                                                            _dataTable.Rows[i][8].ToString(), _dataTable.Rows[i][9].ToString(), 
                                                                            _dataTable.Rows[i][10].ToString(), _dataTable.Rows[i][11].ToString(), 
                                                                            _dataTable.Rows[i][12].ToString(), _dataTable.Rows[i][13].ToString(), 
                                                                            _dataTable.Rows[i][14].ToString(), _dataTable.Rows[i][15].ToString(), 
                                                                            _dataTable.Rows[i][16].ToString()
                                          });
                }
            }
        }

 



        private int U9DBInfo(System.Data.SqlClient.SqlCommand objSQLComm, System.Data.SqlClient.SqlDataReader objDR, string strDBName)
        {
            System.Data.DataTable objDT = new DataTable();

            try
            {
                System.Data.SqlClient.SqlDataAdapter objDA = new SqlDataAdapter();
                DataView view = new DataView();
                view.Table = this.objU9DB;
                view.RowFilter = "数据库名称 = '" + strDBName.Trim() + "'";
                view.RowStateFilter = DataViewRowState.CurrentRows;
                //view.Sort = "数据库名称";
                //view.ToTable();

                //上一次查询已经包括此库的结果了
                if (view.Count > 0) return 2;

                if (objDR != null && !objDR.IsClosed) objDR.Close();
                objSQLComm.CommandText = @"SELECT [name] cDBName, dbid FROM master..sysdatabases 
                                           WHERE [name]= 'UFSystem'";
                objDR = objSQLComm.ExecuteReader();

                if (objDR != null && !objDR.Read() && this.intCareU9DBInfo == 0)
                {
                    if (objDR != null && !objDR.IsClosed) objDR.Close();
                    objSQLComm.CommandText = @"SELECT [name] cDBName, Cast(dbid as int) AS BatabaseID FROM  master.dbo.sysdatabases WHERE [name] = '" + strDBName.Trim() + "'";
                    objDR = objSQLComm.ExecuteReader();

                    if (objDR.Read())
                    {
                        this.objU9DB.Rows.Add(new Object[] { objDR.GetInt32(1), "", strDBName.Trim(), "", "" });
                    }
                    else
                    {
                        MessageBox.Show("所选数据库发生错误。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;
                    }
                    if (!objDR.IsClosed) objDR.Close();
                    return 1;
                }



                return 1;
            }
            finally
            {
                if (objDR != null)
                {
                    if (!objDR.IsClosed) objDR.Close();
                }
                objDT = null;
            }
        }



        private int CheckISU9DB(System.Data.SqlClient.SqlCommand objSQLComm, System.Data.SqlClient.SqlDataReader objDR, string strDBName, ref string strU9Version)
        {
            try
            {

                strU9Version = "";
                if (objDR != null && !objDR.IsClosed) objDR.Close();
                objSQLComm.CommandText = @"SELECT Count(*) FROM sysobjects With(nolock) WHERE type = 'U' AND NAME IN ('GL_Voucher', 'CBO_ItemMaster', 'InvTrans_WhQoh', 'InvTrans_TransLine')";
                objDR = objSQLComm.ExecuteReader();
                if (objDR.Read())
                {
                    if (objDR.GetInt32(0) != 4)
                    {
                        MessageBox.Show("选中的数据库：" + strDBName.Trim() + " 不是 ERP-U9 的业务数据库。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;
                    }
                }

                if (!objDR.IsClosed) objDR.Close();
                objSQLComm.CommandText = @"SELECT ProductDisplayName, ProductVersion FROM dbo.System_ProductInfo";
                objDR = objSQLComm.ExecuteReader();
                if (objDR.Read())
                {
                    strU9Version = objDR.GetSqlString(1).ToString().Trim();
                }
                else
                {
                    MessageBox.Show("选中的数据库：" + strDBName.Trim() + " 不是 ERP-U9 的业务数据库。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }
                if (!objDR.IsClosed) objDR.Close();
                return 1;
            }
            finally
            {
                if (objDR != null)
                {
                    if (!objDR.IsClosed) objDR.Close();
                }
            }
        }


        private SqlConnection OpenSQLConnection(SqlConnection objSQLConn, string strDBName)
        {
            try
            {
                if (objSQLConn.State != ConnectionState.Open)
                {
                    objSQLConn.ConnectionString = "Data Source=" + this.strInstance + ";Initial Catalog=" + strDBName.Trim() + ";" +
                                               (blnIntegrated ? "Integrated Security=true;" : "User ID=" + this.strUser + ";PassWord=" + this.strPassword + ";") +
                                               "Application Name=探测程序 MX ProcessProgram;Connection Timeout=0;Pooling=false;";
                    objSQLConn.Open();
                }
                return objSQLConn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private void ugDBInfo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                this.ugDBInfo.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.Copy);
            }
        }       

        private void ugTable_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                this.ugTable.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.Copy);
            }
        }

        private void ugU9Tables_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                this.ugU9Tables.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.Copy);
            }
        }

        private void ugDBU9Tables_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                this.ugDBU9Tables.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.Copy);
            }
        }

        private void chbIncludeU9_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chbIncludeU9.Checked)
                this.cbYearInfo.Enabled = true;
            else
                this.cbYearInfo.Enabled = false;

        }

        

        
    }
}
