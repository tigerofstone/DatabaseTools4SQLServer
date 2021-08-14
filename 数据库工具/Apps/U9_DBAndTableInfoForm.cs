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

            this.Text = this.Text + "    ���ݿ⣺" + strInstance;

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

            ExportFileDialog.Filter = "Excel 97-2003 ������ (*.xls)|*.xls";
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
                        MessageBox.Show("���� Excel �ļ��ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {                       
                        DialogResult sctReturn;
                        sctReturn = MessageBox.Show("�ļ��Ѵ��ڣ��Ƿ񸲸Ǵ��ļ�����", "��ʾ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (sctReturn == System.Windows.Forms.DialogResult.Yes)
                        {
                            this.ExportExcel();
                            MessageBox.Show("���� Excel �ļ��ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("�ļ���������/r/n" + ex.InnerException, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExportExcel()
        {
            Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ugeeExporter = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter();
            Infragistics.Excel.Workbook objWB = new Infragistics.Excel.Workbook();

            try
            {
                ugeeExporter.Export(this.ugDBInfo, objWB.Worksheets.Add("���ݿ���Ϣ"));
                ugeeExporter.Export(this.ugTable, objWB.Worksheets.Add(this.cbDataBase.Text + " ȫ�����ݱ���Ϣ"));
                ugeeExporter.Export(this.ugU9Tables, objWB.Worksheets.Add(this.cbDataBase.Text + "U9��Ҫҵ�������Ϣ"));
                ugeeExporter.Export(this.ugDBU9Tables, objWB.Worksheets.Add("���ݿ���U9ҵ�������Ϣ"));
                ugeeExporter.Export(this.ugU9Org, objWB.Worksheets.Add("U9��֯��Ϣ"));
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

            this.ugTable.Text = "���ݿ� " + this.cbDataBase.Text.Trim() + " ���������ݱ���Ϣ";
            this.SetFormObjectState(true);
        }



        public void ShowAllDatabaseInfoDetail()
        {

            this.SetProgressForm();
            try
            {
                this.upbInfoGress.Visible = true;
                this.upbInfoGress.Text = "��ѯ�������ݿ�";
                this.SetFormObjectState(false);

                this.ShowAllDatabase();
                this.ShowAllDatabaseInfo();

                this.ugDBInfo.DisplayLayout.Bands[1].Columns["���ݿ�"].Hidden = true;
                this.ugDBInfo.DisplayLayout.Bands[2].Columns["���ݿ�"].Hidden = true;

                this.upbInfoGress.Value = 0;
                this.upbInfoGress.Visible = false;
                this.SetFormObjectState(true);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            this.objU9DB.Columns.Add("���ݿ����", Type.GetType("System.Int32"));
            this.objU9DB.Columns.Add("���ױ��", Type.GetType("System.String"));
            this.objU9DB.Columns.Add("���ݿ�����", Type.GetType("System.String"));
            this.objU9DB.Columns.Add("��������", Type.GetType("System.String"));
            this.objU9DB.Columns.Add("��λ����", Type.GetType("System.String"));
            this.objU9DS.Tables.Add(objU9DB);

            this.objU9Table.TableName = "U9Tables";
            this.objU9Table.Columns.Add("���ݿ�����", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("���", Type.GetType("System.Int32"));
            this.objU9Table.Columns.Add("���ݱ���", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("ҵ�����", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("�����¼��", Type.GetType("System.Int32"));
            this.objU9Table.Columns.Add("�ӱ��¼��", Type.GetType("System.Int32"));
            this.objU9Table.Columns.Add("ҵ����������", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("ҵ����������", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("�������ݿռ�", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("���������ռ�", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("�ӱ����ݿռ�", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("�ӱ������ռ�", Type.GetType("System.String"));
            this.objU9Table.Columns.Add("��������", Type.GetType("System.String"));
            this.objU9DS.Tables.Add(objU9Table);

            this.objU9TableYearInfo.TableName = "U9TablesYearInfo";
            this.objU9TableYearInfo.Columns.Add("���ݿ�����", Type.GetType("System.String"));
            this.objU9TableYearInfo.Columns.Add("���ݱ���", Type.GetType("System.String"));
            this.objU9TableYearInfo.Columns.Add("ҵ�����", Type.GetType("System.String"));
            this.objU9TableYearInfo.Columns.Add("����ʱ�����", Type.GetType("System.String"));
            this.objU9TableYearInfo.Columns.Add("�����¼��", Type.GetType("System.Int32"));
            this.objU9TableYearInfo.Columns.Add("�ӱ��¼��", Type.GetType("System.Int32"));
            this.objU9DS.Tables.Add(objU9TableYearInfo);

            this.objU9DS.Relations.Add("U9TablesLink",
                   this.objU9DS.Tables["U9Databases"].Columns["���ݿ�����"],
                   this.objU9DS.Tables["U9Tables"].Columns["���ݿ�����"]);

            this.objU9DS.Relations.Add("U9TablesYearInfoLink",
                   new System.Data.DataColumn[] { this.objU9DS.Tables["U9Tables"].Columns["���ݿ�����"], this.objU9DS.Tables["U9Tables"].Columns["���ݱ���"] },
                   new System.Data.DataColumn[] { this.objU9DS.Tables["U9TablesYearInfo"].Columns["���ݿ�����"], this.objU9DS.Tables["U9TablesYearInfo"].Columns["���ݱ���"] });

            this.objU9DB4Org.TableName = "U9Databases4Org";
            this.objU9DB4Org.Columns.Add("���ױ��", Type.GetType("System.String"));
            this.objU9DB4Org.Columns.Add("���ݿ�����", Type.GetType("System.String"));
            this.objU9DB4Org.Columns.Add("��������", Type.GetType("System.String"));
            this.objU9DB4Org.Columns.Add("��λ����", Type.GetType("System.String"));
            this.objU9DS4Org.Tables.Add(this.objU9DB4Org);

            this.objU9Org.TableName = "U9Org";
            this.objU9Org.Columns.Add("���ݿ�����", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("ID", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("��֯����", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("��֯����", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("��֯��̬", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("��ҵ����", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("������֯", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("Ӫ����֯", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("������֯", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("������֯", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("������֯", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("������֯", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("�ʲ���֯", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("������֯", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("�Ż���֯", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("��������", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("Ԥ����֯", Type.GetType("System.String"));
            this.objU9Org.Columns.Add("��������", Type.GetType("System.String"));
            this.objU9DS4Org.Tables.Add(this.objU9Org);

            this.objU9DS4Org.Relations.Add("U9DBLink", this.objU9DS4Org.Tables["U9Databases4Org"].Columns["���ݿ�����"], this.objU9DS4Org.Tables["U9Org"].Columns["���ݿ�����"]);
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
                    this.ugU9Tables.DisplayLayout.Bands[1].Columns["���ݱ���"].Hidden = true;
                    this.ugU9Tables.DisplayLayout.Bands[1].Columns["ҵ�����"].Hidden = true;

                    this.ugDBU9Tables.DisplayLayout.Bands[1].Columns["���ݿ�����"].Hidden = true;
                    this.ugDBU9Tables.DisplayLayout.Bands[2].Columns["���ݿ�����"].Hidden = true;
                    this.ugDBU9Tables.DisplayLayout.Bands[2].Columns["���ݱ���"].Hidden = true;
                    this.ugDBU9Tables.DisplayLayout.Bands[2].Columns["ҵ�����"].Hidden = true;
                }

                this.ugDBU9Tables.DisplayLayout.Bands[1].Columns["���ݱ���"].Hidden = true;
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
                                           objDTAllDBInfo.Tables["Databases"].Columns["���ݿ�"],
                                           objDTAllDBInfo.Tables["DatabaseDetail"].Columns["���ݿ�"]);
            objDTAllDBInfo.Relations.Add("DatabasesName_2",
                               objDTAllDBInfo.Tables["DatabaseDetail"].Columns["���ݿ�"],
                               objDTAllDBInfo.Tables["DatabaseFileDetail"].Columns["���ݿ�"]);

            return objDTAllDBInfo;
        }


        private void ShowU9OrganizitionInfo()
        {
            this.getU9Organizations(this.cbDataBase.Text.Trim());
            this.ugU9Org.DataSource = this.objU9DS4Org;

            this.ugU9Org.DisplayLayout.Bands[1].Columns["���ݿ�����"].Hidden = true;
        }

 




/*
 * ���ݿ����
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

                objSQLComm.CommandText = @"SELECT @@VERSION AS ���ݿ�汾";
                objSQLReader = objSQLComm.ExecuteReader();

                if (objSQLReader.Read())
                {
                    strVersion = objSQLReader.GetString(0).Trim();
                }

                objSQLReader.Close();
                if (strVersion.IndexOf("Microsoft SQL Server  2000") >= 0)
                {
                    objSQLComm.CommandText = @"SELECT LTRIM(RTRIM([NAME])) AS ���ݿ�, DBID AS [���ݿ� ID] FROM sysdatabases ORDER BY [NAME]";
                }
                else
                {
                    objSQLComm.CommandText = @"SELECT LTRIM(RTRIM([NAME])) AS ���ݿ�, DTB.DATABASE_ID AS [���ݿ� ID]
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
                MessageBox.Show(e.Message, "ϵͳ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                objDTResult.Columns.Add("���ݿ�", Type.GetType("System.String"));
                objDTResult.Columns.Add("���ݿ��ܿռ�", Type.GetType("System.String"));
                objDTResult.Columns.Add("���ݿռ�", Type.GetType("System.String"));
                objDTResult.Columns.Add("������ʹ�ÿռ�", Type.GetType("System.String"));
                objDTResult.Columns.Add("������ʹ�ÿռ�", Type.GetType("System.String"));
                objDTResult.Columns.Add("��־�ռ�", Type.GetType("System.String"));
                objDTResult.Columns.Add("��־��ʹ�ÿռ�", Type.GetType("System.String"));
                objDTResult.Columns.Add("δ����ռ�", Type.GetType("System.String"));
                objDTResult.Columns.Add("�����ռ�", Type.GetType("System.String"));
                objDTResult.Columns.Add("δʹ�ÿռ�", Type.GetType("System.String"));

                objDTResult_1.TableName = "DatabaseFileDetail";
                objDTResult_1.Columns.Add("���ݿ�", Type.GetType("System.String"));
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
                        this.upbInfoGress.Text = "�� " + ((string)(objDTAllDB.Rows[i]["���ݿ�"])).Trim() + " ����    [Completed] of [Range] Completed";
                        Application.DoEvents();

                        objConn = this.OpenSQLConnection(objConn, (string)(objDTAllDB.Rows[i]["���ݿ�"]));

                        this.upbInfoGress.Text = "��� " + ((string)(objDTAllDB.Rows[i]["���ݿ�"])).Trim() + " ������Ϣ    [Completed] of [Range] Completed";
                        Application.DoEvents();

                        objDSSQL.Clear();
                        objSQLComm = objConn.CreateCommand();
                        objSQLComm.CommandTimeout = 600;

                        objSQLComm.CommandText = @"SP_SPACEUSED;" + "\r\n" +
                                                 @"CREATE TABLE #TRAN_LOG_SPACE_USAGE (DATABASE_NAME SYSNAME, LOG_SIZE_MB FLOAT, LOG_SPACE_USED FLOAT, STATUS INT); 
                                               INSERT INTO #TRAN_LOG_SPACE_USAGE EXEC('DBCC SQLPERF(LOGSPACE)') ; 
                                               SELECT CAST(LOG_SIZE_MB AS DECIMAL(10,2)) AS ��־�ռ�, 
                                                      CAST(CONVERT(FLOAT,LOG_SPACE_USED) AS DECIMAL(10,1)) AS [��־��ʹ�ñ��� (%)], 
                                                      CAST(LOG_SIZE_MB * LOG_SPACE_USED / 100 AS DECIMAL(10,2))  AS ��־��ʹ�ÿռ�, 
                                                      CAST(CONVERT(FLOAT,(100-LOG_SPACE_USED)) AS DECIMAL(10,1)) AS [��־δʹ�ñ��� (%)], 
                                                      CAST(LOG_SIZE_MB * (100 - LOG_SPACE_USED) /100 AS DECIMAL(10,2)) AS ��־δʹ�ÿռ�
                                               FROM #TRAN_LOG_SPACE_USAGE 
                                               WHERE DATABASE_NAME = DB_NAME()
                                               DROP TABLE #TRAN_LOG_SPACE_USAGE";
                        objSQLComm.ExecuteNonQuery();
                        objSQLDA.SelectCommand = objSQLComm;
                        objSQLDA.Fill(objDSSQL);

                        if (objDSSQL.Tables.Count > 0)
                        {
                            objDTResult.Rows.Add(new object[] { 
                                                            ((string)(objDTAllDB.Rows[i]["���ݿ�"])).Trim(),
                                                            ((string)(objDSSQL.Tables[0].Rows[0]["database_size"])).Trim(),
                                                            (Decimal.Parse(((string)(objDSSQL.Tables[0].Rows[0]["database_size"])).Trim().Substring(0, ((string)(objDSSQL.Tables[0].Rows[0]["database_size"])).Trim().Length - 2))
                                                              -(Decimal)objDSSQL.Tables[2].Rows[0]["��־�ռ�"]).ToString() + " MB",
                                                            ((string)(objDSSQL.Tables[1].Rows[0]["data"])).Trim(),
                                                            ((string)(objDSSQL.Tables[1].Rows[0]["index_size"])).Trim(),                        
                                                            ((Decimal)objDSSQL.Tables[2].Rows[0]["��־�ռ�"]).ToString("0.00").Trim() + " MB",
                                                            ((Decimal)objDSSQL.Tables[2].Rows[0]["��־��ʹ�ÿռ�"]).ToString("0.00").Trim() + " MB",
                                                            ((string)(objDSSQL.Tables[0].Rows[0]["unallocated space"])).Trim(),
                                                            ((string)(objDSSQL.Tables[1].Rows[0]["reserved"])).Trim(),
                                                            ((string)(objDSSQL.Tables[1].Rows[0]["unused"])).Trim()
                                                          });
                        }

                        this.upbInfoGress.Text = "��� " + ((string)(objDTAllDB.Rows[i]["���ݿ�"])).Trim() + " �ļ���Ϣ    [Completed] of [Range] Completed";
                        Application.DoEvents();

                        objDSSQL = null;
                        objDSSQL = new DataSet();
                        objSQLComm.CommandText = @"SP_HELPDB '" + ((string)(objDTAllDB.Rows[i]["���ݿ�"])).Trim() + "'";
                        objSQLComm.ExecuteNonQuery();
                        objSQLDA.SelectCommand = objSQLComm;
                        objSQLDA.Fill(objDSSQL);
                        if (objDSSQL.Tables.Count > 0)
                        {
                            for (int j = 0; j < objDSSQL.Tables[1].Rows.Count; j++)
                            {
                                objDTResult_1.Rows.Add(new object[] { ((string)(objDTAllDB.Rows[i]["���ݿ�"])).Trim(),
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
                        this.upbInfoGress.Text = "��� " + ((string)(objDTAllDB.Rows[i]["���ݿ�"])).Trim() + " �ļ���Ϣ    [Completed] of [Range] Completed";

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
                MessageBox.Show(e.Message, "ϵͳ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            objDTResult.Columns.Add("����", Type.GetType("System.String"));
            objDTResult.Columns.Add("��¼��", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("���ݿռ�", Type.GetType("System.String"));
            objDTResult.Columns.Add("�������ݿռ�", Type.GetType("System.String"));
            objDTResult.Columns.Add("�����ռ�", Type.GetType("System.String"));
            objDTResult.Columns.Add("δʹ�ÿռ�", Type.GetType("System.String"));
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
                this.upbInfoGress.Text = "�� " + strDBName.Trim() + " ���ݿ������ݱ�    [Completed] Of [Range] Completed";

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
                MessageBox.Show(e.Message, "ϵͳ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            int int�����¼�� = 0;                  int int�ӱ��¼�� = 0;
            string str�������ݿռ� = "";            string str���������ռ� = "";
            string str�ӱ����ݿռ� = "";            string str�ӱ������ռ� = "";
            string strҵ���������� = "";            string strҵ���������� = "";
            //int intYear�����¼�� = 0; int intYear�ӱ��¼�� = 0;

            //int intYear����ʱ�����;

            objDTResult.TableName = "ObjectDetail";
            objDTResult.Columns.Add("���", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("���ݱ���", Type.GetType("System.String"));
            objDTResult.Columns.Add("ҵ�����",     Type.GetType("System.String"));
            objDTResult.Columns.Add("�����¼��",   Type.GetType("System.Int32"));
            objDTResult.Columns.Add("�ӱ��¼��",   Type.GetType("System.Int32"));
            objDTResult.Columns.Add("ҵ����������", Type.GetType("System.String"));
            objDTResult.Columns.Add("ҵ����������", Type.GetType("System.String"));
            objDTResult.Columns.Add("�������ݿռ�", Type.GetType("System.String"));
            objDTResult.Columns.Add("���������ռ�", Type.GetType("System.String"));
            objDTResult.Columns.Add("�ӱ����ݿռ�", Type.GetType("System.String"));
            objDTResult.Columns.Add("�ӱ������ռ�", Type.GetType("System.String"));
            objDTResult.Columns.Add("��������", Type.GetType("System.String"));

            objDTResultYearInfo.TableName = "YearInfo";
            objDTResultYearInfo.Columns.Add("���ݱ���", Type.GetType("System.String"));
            objDTResultYearInfo.Columns.Add("ҵ�����", Type.GetType("System.String"));
            objDTResultYearInfo.Columns.Add("����ά����Ϣ", Type.GetType("System.String"));
            objDTResultYearInfo.Columns.Add("�����¼��", Type.GetType("System.Int32"));
            objDTResultYearInfo.Columns.Add("�ӱ��¼��", Type.GetType("System.Int32"));

            objDSResult.Tables.Add(objDTResult); objDSResult.Tables.Add(objDTResultYearInfo);
            objDSResult.Relations.Add("YearInfo", objDTResult.Columns["���ݱ���"], objDTResultYearInfo.Columns["���ݱ���"]);

            try
            {
                this.upbInfoGress.Text = "�� " + strDBName + " ���ݿ�";

                objConn = this.OpenSQLConnection(objConn, strDBName);

                objSQLComm = objConn.CreateCommand();
                objSQLComm.CommandTimeout = 600;

                if (this.intCheckU9DB == 1)
                {
                    this.upbInfoGress.Text = "�ж����ݿ� " + strDBName + " �Ƿ�U9���ݿ�";
                    if (this.CheckISU9DB(objSQLComm, objDR, strDBName, ref strVersion) <= 0)
                    {
                        //MessageBox.Show("�����ݿⲻ�� ERP-U9 ���ݿ⡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return objDSResult;
                    }
                }

                this.upbInfoGress.Text = "ȡ���ݿ� " + strDBName + " ��Ӧ U9 ������Ϣ";

                intIncludeU9DB = this.U9DBInfo(objSQLComm, objDR, strDBName);
                if (intIncludeU9DB <= 0 && this.intCareU9DBInfo == 1)
                {
                    return objDSResult;
                }                

                //========== �����ݱ� ============================================================================================================================================================
                objXMLDom.Load(Application.StartupPath + "\\U9ERPConfig.xml");
                System.Xml.XmlNodeList objXMLNL;
                objXMLNL = objXMLDom.SelectNodes("./Config/U9Tables[@Version = 'V3.0']/Table");

                this.upbInfoGress.FillAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                this.upbInfoGress.Refresh();
                this.upbInfoGress.Maximum = objXMLNL.Count;
                this.upbInfoGress.Value = 0;
                this.upbInfoGress.Text = "�� " + strDBName.Trim() + " ���ݿ�����Ҫ�� ERP-U9 ���ݱ�    [Completed] Of [Range] Completed                  ";


                for (int i = 0; i < objXMLNL.Count; i++)
                {
                    int�����¼�� = 0;             int�ӱ��¼�� = 0;
                    str�������ݿռ� = "";          str���������ռ� = "";
                    str�ӱ����ݿռ� = "";          str�ӱ������ռ� = "";
                    strҵ���������� = "";          strҵ���������� = "";

                    //intYear����ʱ����� = 0;

                    objXMLEle = (System.Xml.XmlElement)objXMLNL[i];

                    #region �ж����ݱ����
                    //=================================//
                    objSQLComm.CommandText = @"SELECT * FROM sysobjects With(nolock) 
                                               WHERE TYPE = 'U' AND NAME = '" + objXMLEle.Attributes["TableName"].Value.ToString().Trim() + "'";
                    objDR = objSQLComm.ExecuteReader();
                    if (!objDR.Read())
                    {
                        strMisTable = strMisTable + "    ҵ�����" + objXMLEle.Attributes["Object"].Value.ToString().Trim() +
                                                    "    ���� [" + objXMLEle.Attributes["TableName"].Value.ToString() + "]\r\n";
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
                            strMisTable = strMisTable + "    ҵ�����" + objXMLEle.Attributes["Object"].Value.ToString().Trim() +
                                                        "    �ӱ� [" + objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() + "]\r\n";
                            objDR.Close();
                            continue;
                        }
                        if (!objDR.IsClosed) objDR.Close();
                    }

                    //=================================//
                    #endregion

                    #region  ��ѯ���ݱ���Ϣ
                    objSQLComm.CommandText = @"EXEC SP_SPACEUSED N'" + objXMLEle.Attributes["TableName"].Value.ToString().Trim() + "'";
                    objDR = objSQLComm.ExecuteReader();

                    if (objDR.Read())
                    {
                        int�����¼�� = int.Parse(objDR.GetString(1));
                        str�������ݿռ� = objDR.GetString(3);                        
                        str���������ռ� = objDR.GetString(4);
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
                            strҵ���������� = objDR.IsDBNull(0) ? "" : objDR.GetString(0);        strҵ���������� = objDR.IsDBNull(1) ? "" : objDR.GetString(1);

                            if (strҵ����������.Length > 10)
                            {
                                strҵ���������� = System.DateTime.Parse(strҵ����������).ToString("yyyy-MM-dd");
                                strҵ���������� = System.DateTime.Parse(strҵ����������).ToString("yyyy-MM-dd");
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
                            int�ӱ��¼�� = int.Parse(objDR.GetString(1));
                            str�ӱ����ݿռ� = objDR.GetString(3);                          str�ӱ������ռ� = objDR.GetString(4);
                        }
                        objDR.Close();
                    }     
                    #endregion

                    intRow = intRow + 1;
                    objDTResult.Rows.Add(new object[] { intRow, objXMLEle.Attributes["TableName"].Value.ToString().Trim(),
                                                        objXMLEle.Attributes["Object"].Value.ToString(), 
                                                        int�����¼��, int�ӱ��¼��, strҵ����������, strҵ����������,
                                                        str�������ݿռ�, str���������ռ�, str�ӱ����ݿռ�, str�ӱ������ռ�, 
                                                        (objXMLEle.Attributes["Type"] != null ? objXMLEle.Attributes["Type"].Value.ToString().Trim() : "")
                                                      });
                    if (intIncludeU9DB != 2)
                    {
                        this.objU9Table.Rows.Add(new object[] {strDBName.Trim(), intRow, objXMLEle.Attributes["TableName"].Value.ToString().Trim(),
                                                          objXMLEle.Attributes["Object"].Value.ToString(), 
                                                          int�����¼��, int�ӱ��¼��, strҵ����������, strҵ����������,
                                                          str�������ݿռ�, str���������ռ�, str�ӱ����ݿռ�, str�ӱ������ռ�,
                                                        (objXMLEle.Attributes["Type"] != null ? objXMLEle.Attributes["Type"].Value.ToString().Trim() : "")
                                                         });  
                        
                    }

                    #region �����ӱ��������Ϣ

                    this.getU9ChieldTablesInfo(strDBName.Trim(), ref intRow, objXMLEle, objConn, objSQLComm, intIncludeU9DB, ref strMisTable, ref objDTResult);

                    #endregion

                    Application.DoEvents();

                    #region  ��ѯ�������������
                    //--------------- ��ѯ������������� -------------------------------------------------------------------------------------------------------------
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
                        MessageBox.Show("�������ݱ������ݿ��в����ڣ�\r\n" + strMisTable.Substring(0, strMisTable.Length - 2) + "��\r\n" + 
                                        "���������������ļ��� ERP-U9 �汾��" + objXMLEle.Attributes["Version"].Value.ToString() + " ��һ�¡�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("�������ݱ������ݿ��в����ڣ�\r\n" + strMisTable.Substring(0, strMisTable.Length - 2) + "��\r\n" +
                                        "���������������ļ��� ERP-U9 �汾��һ�¡�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return objDSResult;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ϵͳ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// ȡ�¼�����Ϣ
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

                            #region �ж����ݱ����

                            objComm.CommandText = @"SELECT * FROM sysobjects With(nolock) 
                                            WHERE TYPE = 'U' AND NAME = '" + _oXMLEleTable.Attributes["TableName"].Value.ToString().Trim() + "'";
                            _oRD = objComm.ExecuteReader();
                            if (!_oRD.Read())
                            {
                                strMistake = strMistake + "    ҵ�����" + _oXMLEleTable.Attributes["Object"].Value.ToString().Trim() +
                                                            "    ���� [" + _oXMLEleTable.Attributes["TableName"].Value.ToString() + "]\r\n";
                                _oRD.Close();
                                continue;
                            }
                            if (!_oRD.IsClosed) _oRD.Close();

                            #endregion

                            #region ��ѯ���ݱ���Ϣ

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
            string intYear����ʱ�����;  int intYear�����¼��;  int intYear�ӱ��¼��;

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
                        strMisTable = strMisTable + "���ݱ�"+ objXMLEle.Attributes["TableName"].Value.ToString().Trim() + " �л����ֶ��а��������ڵ��ֶΣ�/r/n";
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
                                intYear����ʱ����� = objDR.GetString(0);
                                intYear�����¼�� = objDR.GetInt32(1);
                                intYear�ӱ��¼�� = objDR.GetInt32(2);
                                objDTResultYearInfo.Rows.Add(new object[] { objXMLEle.Attributes["TableName"].Value.ToString().Trim(), objXMLEle.Attributes["Object"].Value.ToString(), 
                                                                        intYear����ʱ�����.ToString(), intYear�����¼��, intYear�ӱ��¼�� });

                                if (intIncludeU9DB != 2)
                                    this.objU9TableYearInfo.Rows.Add(new object[] { strDBName.Trim(), objXMLEle.Attributes["TableName"].Value.ToString().Trim(), objXMLEle.Attributes["Object"].Value.ToString(), 
                                                                                intYear����ʱ�����.ToString(), intYear�����¼��, intYear�ӱ��¼�� });
                            }
                            catch (Exception Ex)
                            {
                                strMisTable = strMisTable + "    ҵ�����" + objXMLEle.Attributes["Object"].Value.ToString().Trim() +
                                                            " ��ȡ��������д���" + Ex.Message + "��\r\n";
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
            view.RowFilter = "���ݿ����� = '" + strDBName.Trim() + "'";
            view.RowStateFilter = DataViewRowState.CurrentRows;
            if (view.Count <= 0)
            {
                SqlConnection _objSQLConn = new SqlConnection();
                SqlCommand _command = new SqlCommand();
                DataTable _dataTable = new DataTable();
                SqlDataAdapter _adapter = new SqlDataAdapter();
                this.upbInfoGress.Text = "�� " + strDBName + " ���ݿ�";
                _command = this.OpenSQLConnection(_objSQLConn, strDBName).CreateCommand();
                _command.CommandTimeout = 600;
                _command.CommandText = "SELECT O.ID, O.Code, OT.[Name], " + "\r\n" +                                           
                                            "(CASE O.OrgClassify WHEN 0 THEN '����' WHEN 1 THEN '��˾' WHEN 2 THEN '��ҵ��'WHEN 3 THEN '����' WHEN 4 THEN '����' END) AS ��֯��̬, " + "\r\n" +   
                                            "(CASE O.CompanyType WHEN 0 THEN '��ҵ��ҵ' WHEN 1 THEN '��Ʒ��ͨ��ҵ' END) AS ��ҵ����, " + "\r\n" +                                            
                                            "(CASE O.IsLegacyOrg WHEN 1 THEN '��' WHEN 0 THEN '' END) AS ������֯, " + "\r\n" +                                            
                                            "(CASE O.IsOperatingOrg WHEN 1 THEN '��' WHEN 0 THEN '' END) AS Ӫ����֯, " + "\r\n" +                                            
                                            "(CASE O.IsAuditOrg WHEN 1 THEN '��' WHEN 0 THEN '' END) AS ������֯, " + "\r\n" +                                             
                                            "(CASE O.IsInventoryOrg WHEN 1 THEN '��' WHEN 0 THEN '' END) AS ������֯, " + "\r\n" +                                             
                                            "(CASE O.IsPlantOrg WHEN 1 THEN '��' WHEN 0 THEN '' END) AS ������֯, " + "\r\n" +                                           
                                            "(CASE O.IsHrOrg WHEN 1 THEN '��' WHEN 0 THEN '' END) AS ������֯, " + "\r\n" +                                           
                                            "(CASE O.IsAssetOrg WHEN 1 THEN '��' WHEN 0 THEN '' END) AS �ʲ���֯, " + "\r\n" +                                           
                                            "(CASE O.IsServiceOrg WHEN 1 THEN '��' WHEN 0 THEN '' END) AS ������֯, " + "\r\n" +                                           
                                            "(CASE O.IsPortalOrg WHEN 1 THEN '��' WHEN 0 THEN '' END) AS �Ż���֯, " + "\r\n" +                                           
                                            "(CASE O.IsSettlementOrg WHEN 1 THEN '��' WHEN 0 THEN '' END) AS ��������, " + "\r\n" +                                           
                                            "(CASE O.IsBudgetOrg WHEN 1 THEN '��' WHEN 0 THEN '' END) AS Ԥ����֯, " + "\r\n" +
                                            "(CASE O.ManageType WHEN 1 THEN '�ⲿ����' WHEN 0 THEN '�ڲ�����' END) AS �������� " + "\r\n" +
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
                view.RowFilter = "���ݿ����� = '" + strDBName.Trim() + "'";
                view.RowStateFilter = DataViewRowState.CurrentRows;
                //view.Sort = "���ݿ�����";
                //view.ToTable();

                //��һ�β�ѯ�Ѿ������˿�Ľ����
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
                        MessageBox.Show("��ѡ���ݿⷢ������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("ѡ�е����ݿ⣺" + strDBName.Trim() + " ���� ERP-U9 ��ҵ�����ݿ⡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("ѡ�е����ݿ⣺" + strDBName.Trim() + " ���� ERP-U9 ��ҵ�����ݿ⡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                               "Application Name=̽����� MX ProcessProgram;Connection Timeout=0;Pooling=false;";
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
