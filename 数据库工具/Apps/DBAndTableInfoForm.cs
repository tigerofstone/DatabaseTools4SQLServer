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
    public partial class DBAndTableInfoForm : Form
    {
        public int intInComeType = 0;

        public string strConnection = "";
        public string strInstance = "";
        public string strUser = "";
        public string strPassword = "";
        public bool blnIntegrated = false;

        public int intCareU8DBInfo = 1;
        public int intCheckU8DB = 1;

        private double dblSelectedDBSpaceMB = 0;

        public System.Data.SqlClient.SqlConnection objSQLConnect = new System.Data.SqlClient.SqlConnection();
        
        private DataSet objU8DS = new DataSet();
        private DataTable objU8DB = new DataTable();
        private DataTable objU8Table = new DataTable();
        private DataTable objU8TableYearInfo = new DataTable();

        private string strCurrentDBName = "";

        public DBAndTableInfoForm()
        {
            InitializeComponent();            
        }

        private void DBAndTableInfoForm_Resize(object sender, EventArgs e)
        {           
            this.utcMainInfo.Left = 0;
            this.utcMainInfo.Top = 79;

            this.utcMainInfo.Width = this.Width - 15;
            this.utcMainInfo.Height = this.Height - 120;

            if (this.upbInfoGress.Visible) this.SetProgressForm();

            //==============================================================================================
            this.pnChartAll.Width = this.Width - this.chartAllTableSpace.Width - 18;
            this.chartTop20Table.Width = this.Width - this.chartTop20TablePie.Width - 18;
            if (this.chartAlltable.Width <= 800)
            {
                this.chartAlltable.Width = this.Width - this.chartAllTableSpace.Width - 18;
            }

            this.label2.Left = 10;
            this.label2.Top = this.Height - 217;
            this.pnTopTableNum.Left = 118;
            this.pnTopTableNum.Top = this.Height - 222;

            this.label3.Left = 535;
            this.label3.Top = this.Height - 217;
            this.pnOrderby.Left = 605;
            this.pnOrderby.Top = this.Height - 222;
            //==============================================================================================
            //==============================================================================================
            this.pnChartU8TableCount.Width = this.Width - this.chartU8TableSpace.Width - 18;
            this.chartU8TableTop20Count.Width = this.Width - this.chartU8TableTop20Space.Width - 18;
            if (this.chartU8TableCount.Width <= 800)
            {
                this.chartU8TableCount.Width = this.Width - this.chartU8TableSpace.Width - 18;
            }

            //this.label5.Left = 10;
            //this.label5.Top = this.Height - 217;
            //this.pnChartU8TableTopN.Left = 118;
            //this.pnChartU8TableTopN.Top = this.Height - 222;

            //this.label4.Left = 535;
            //this.label4.Top = this.Height - 217;
            //this.pnChartU8TableOrderBy.Left = 605;
            //this.pnChartU8TableOrderBy.Top = this.Height - 222;

            this.label4.Left = 10;
            this.label4.Top = this.Height - 200;
            this.pnChartU8TableOrderBy.Left = 108;
            this.pnChartU8TableOrderBy.Top = this.Height - 205;
            //==============================================================================================
            //==============================================================================================
            this.pnChartUUTableCount.Width = this.Width - this.chartUUTableSpace.Width - 18;
            //if (this.chartUUTableCount.Width <= 800)
            //{
            //    this.chartUUTableCount.Width = this.pnChartUUTableCount.Width - 2;
            //}
            //==============================================================================================
        }

        private void DBAndTableInfoForm_Load(object sender, EventArgs e)
        {
            try
            {
                DBAndTableInfoForm_Resize(sender, e);

                this.InitFormControl();

                this.SetU8DT();
                if(this.intInComeType == 1)
                    this.ShowAllDatabaseInfoDetail();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message + "��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitFormControl()
        {
            this.ugDBInfo.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ugDBInfo.DisplayLayout.Override.AllowMultiCellOperations = Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.All;

            this.ugDBU8Tables.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ugDBU8Tables.DisplayLayout.Override.AllowMultiCellOperations = Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.All;
            
            this.ugTable.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ugTable.DisplayLayout.Override.AllowMultiCellOperations = Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.All;
            
            this.ugU8Tables.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ugU8Tables.DisplayLayout.Override.AllowMultiCellOperations = Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.All;

            this.Text = this.Text + "    ���ݿ⣺" + strInstance;


            this.cbTableNum.Items.AddRange(new object[] {"10", "20", "50", "100", "150", "200", "250", "300",
                                                        "500", "1000", "2000", "3000", "4000", "5000", "ȫ��"});
            this.cbTableNum.SelectedIndex = 2;
            this.cbOrderBy.SelectedIndex = 0;

            this.cbU8ChartTopN.SelectedIndex = this.cbU8ChartTopN.Items.Count-1;
            this.cbU8ChartOrderBy.SelectedIndex = 0;

            this.cbYearInfo.Enabled = this.chbIncludeU8.Checked;
            this.chbU8UUYearInfo.Enabled = this.chbIncludeU8UU.Checked;

            

            //�������ϵͳѡ���ҳǩ            
            try
            {
                System.Xml.XmlDocument objXMLDom = new System.Xml.XmlDocument();
                System.Xml.XmlElement objXMLEle;

                objXMLDom.Load(Application.StartupPath + "\\U8ERPConfig.xml");

                if (objXMLDom.SelectSingleNode("./Config/OtherTables") != null && objXMLDom.SelectNodes("./Config/OtherTables/Table").Count > 0)
                {
                    objXMLEle = (System.Xml.XmlElement)objXMLDom.SelectSingleNode("./Config/OtherTables");

                    this.cbOtherSystemInfo.Text = "չʾ" + objXMLEle.Attributes["Name"].Value.Trim() + "������Ϣ";
                    this.cbOtherSystemInfo.Visible = true;

                    this.utcMainInfo.Tabs[4].Visible = true;
                    this.utpcOtherSystemTables.Tab.Text = objXMLEle.Attributes["Name"].Value.Trim() + "����";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ʼ��������" + e.Message + "��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DBAndTableInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }


        private void pnDatabase_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                                    new Rectangle(pnDatabase.ClientRectangle.X, pnDatabase.ClientRectangle.Y,
                                                  pnDatabase.ClientRectangle.Width, pnDatabase.ClientRectangle.Height),
                                    Color.Gainsboro, ButtonBorderStyle.Solid);
        }


        private void pnExcelfile_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                                    new Rectangle(pnExcelfile.ClientRectangle.X, pnExcelfile.ClientRectangle.Y,
                                                  pnExcelfile.ClientRectangle.Width, pnExcelfile.ClientRectangle.Height),
                                    Color.Gainsboro, ButtonBorderStyle.Solid);
        }

        


        

        private void ugDBU8Tables_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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
                this.strCurrentDBName = this.cbDataBase.Text.Trim();

                this.ShowAllTablesInfoDetail();

                this.utcAllTableInfo_SelectedTabChanged(sender, null);
                this.utcU8BizTable_SelectedTabChanged(sender, null);
                this.utcU8UU_SelectedTabChanged(sender, null);
                
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
                if((this.ugDBU8Tables.DataSource == null || ((System.Data.DataTable)this.ugDBU8Tables.DataSource).Rows.Count<=0)
                    && (this.ugU8Tables.DataSource == null || ((System.Data.DataSet)this.ugU8Tables.DataSource).Tables[0].Rows.Count <= 0))
                {
                    if (MessageBox.Show("�������ݲ�������û��ѡ����ʾ U8 ������Ϣ�����ݿ����ݱ���Ϣ���Ƿ����������", "ϵͳ��Ϣ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }

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
                MessageBox.Show("�ļ���������\r\n" + ex.Message + "\r\n" + ex.StackTrace, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                ugeeExporter.Export(this.ugU8Tables, objWB.Worksheets.Add(this.cbDataBase.Text + "����U8+��Ҫҵ�������Ϣ"));
                ugeeExporter.Export(this.ugUUTable, objWB.Worksheets.Add(this.cbDataBase.Text + "����U8+ UUҵ�������Ϣ"));
                ugeeExporter.Export(this.ugDBU8Tables, objWB.Worksheets.Add("���ݿ���U8ҵ�������Ϣ"));
  
                objWB.Save(this.tbFileName.Text);
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














        /*===========================================================================================================================================*/
        public void ShowAllTablesInfoDetail()
        {
            this.SetProgressForm();
            this.upbInfoGress.Visible = true;
            this.SetFormObjectState(false);      

            this.ShowAllTableInfo();

            this.SetDBTableShowInfo();

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
            this.chbIncludeU8.Enabled = blnState;
            this.cbYearInfo.Enabled = blnState;
            this.cbOtherSystemInfo.Enabled = blnState;
            this.chbIncludeU8UU.Enabled = blnState;
            this.chbU8UUYearInfo.Enabled = blnState;
        }


        private void SetDBTableShowInfo()
        {
            this.ugTable.DisplayLayout.Bands[0].Columns[1].Format = "#,##0";
            this.ugTable.DisplayLayout.Bands[0].Columns[2].Format = "#,##0.###";
            this.ugTable.DisplayLayout.Bands[0].Columns[3].Format = "#,##0.###";
            this.ugTable.DisplayLayout.Bands[0].Columns[4].Format = "#,##0.###";
            this.ugTable.DisplayLayout.Bands[0].Columns[5].Format = "#,##0.###";

            this.ugU8Tables.DisplayLayout.Bands[0].Columns[5].Format = "#,##0";
            this.ugU8Tables.DisplayLayout.Bands[0].Columns[6].Format = "#,##0.###";
            this.ugU8Tables.DisplayLayout.Bands[0].Columns[7].Format = "#,##0.###";
            this.ugU8Tables.DisplayLayout.Bands[0].Columns[8].Format = "#,##0.###";
            this.ugU8Tables.DisplayLayout.Bands[0].Columns[9].Format = "#,##0.###";
        }

        private void SetU8DT()
        {
            this.objU8DB.TableName = "U8Databases";
            this.objU8DB.Columns.Add("���ݿ����", Type.GetType("System.Int32"));
            this.objU8DB.Columns.Add("���ױ��", Type.GetType("System.String"));
            this.objU8DB.Columns.Add("���ݿ�����", Type.GetType("System.String"));
            this.objU8DB.Columns.Add("��������", Type.GetType("System.String"));
            this.objU8DB.Columns.Add("��λ����", Type.GetType("System.String"));
            this.objU8DB.Columns.Add("�汾", Type.GetType("System.String"));
            this.objU8DS.Tables.Add(objU8DB);

            this.objU8Table.TableName = "U8Tables";
            this.objU8Table.Columns.Add("���ݿ�����", Type.GetType("System.String"));
            this.objU8Table.Columns.Add("���", Type.GetType("System.Int32"));
            this.objU8Table.Columns.Add("ҵ�����", Type.GetType("System.String"));
            this.objU8Table.Columns.Add("�����¼��", Type.GetType("System.Int32"));
            this.objU8Table.Columns.Add("�ӱ��¼��", Type.GetType("System.Int32"));
            this.objU8Table.Columns.Add("ҵ����������", Type.GetType("System.String"));
            this.objU8Table.Columns.Add("ҵ����������", Type.GetType("System.String"));
            this.objU8Table.Columns.Add("�������ݿռ�(MB)", Type.GetType("System.String"));
            this.objU8Table.Columns.Add("���������ռ�(MB)", Type.GetType("System.String"));
            this.objU8Table.Columns.Add("�ӱ����ݿռ�(MB)", Type.GetType("System.String"));
            this.objU8Table.Columns.Add("�ӱ������ռ�(MB)", Type.GetType("System.String"));
            this.objU8DS.Tables.Add(objU8Table);

            this.objU8TableYearInfo.TableName = "U8TablesYearInfo";
            this.objU8TableYearInfo.Columns.Add("���ݿ�����", Type.GetType("System.String"));
            this.objU8TableYearInfo.Columns.Add("ҵ�����", Type.GetType("System.String"));
            this.objU8TableYearInfo.Columns.Add("����ʱ�����", Type.GetType("System.Int32"));
            this.objU8TableYearInfo.Columns.Add("�����¼��", Type.GetType("System.Int32"));
            this.objU8TableYearInfo.Columns.Add("�ӱ��¼��", Type.GetType("System.Int32"));
            this.objU8DS.Tables.Add(objU8TableYearInfo);

            this.objU8DS.Relations.Add("U8TablesLink",
                   this.objU8DS.Tables["U8Databases"].Columns["���ݿ�����"],
                   this.objU8DS.Tables["U8Tables"].Columns["���ݿ�����"]);

            this.objU8DS.Relations.Add("U8TablesYaerInfoLink",
                   new System.Data.DataColumn[] {this.objU8DS.Tables["U8Tables"].Columns["���ݿ�����"],  this.objU8DS.Tables["U8Tables"].Columns["ҵ�����"]},
                   new System.Data.DataColumn[] { this.objU8DS.Tables["U8TablesYearInfo"].Columns["���ݿ�����"], this.objU8DS.Tables["U8TablesYearInfo"].Columns["ҵ�����"] });
        }

        private void ShowAllTableInfo()
        {
            System.Data.DataTable objDT = new DataTable();
            System.Data.DataSet objU8DS = new DataSet();
            System.Data.DataSet objU8UUDS = new DataSet();

            if (this.cbIncludeAllTables.Checked)
            {
                objDT = GetTableAllInfo(this.cbDataBase.Text);                

                this.ugTable.DataSource = objDT;

            }

            if (this.chbIncludeU8.Checked)
            {
                objU8DS = this.GetU8TableAllInfo(this.cbDataBase.Text, (this.cbYearInfo.Checked ? 1 : 0));
                this.ugU8Tables.DataSource = objU8DS;
                this.ugDBU8Tables.DataSource = this.objU8DB;

                if (this.cbYearInfo.Checked)
                {
                    this.ugU8Tables.DisplayLayout.Bands[1].Columns["ҵ�����"].Hidden = true;
                    this.ugDBU8Tables.DisplayLayout.Bands[1].Columns["���ݿ�����"].Hidden = true;
                    this.ugDBU8Tables.DisplayLayout.Bands[2].Columns["ҵ�����"].Hidden = true;
                    this.ugDBU8Tables.DisplayLayout.Bands[2].Columns["���ݿ�����"].Hidden = true;
                }
            }

            if (this.chbIncludeU8UU.Checked)
            {
                if (this.checkUTUExists() == 0)
                {
                    MessageBox.Show("UU���ݿⲻ���ڣ������¼�����ݿ⡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Upgrade.Apps.clsBizDataInfo objUUBizInfo = new clsBizDataInfo();
                objUUBizInfo.strConnection = this.strConnection;
                objUUBizInfo.strInstance = this.strInstance;
                objUUBizInfo.strUser = this.strUser;
                objUUBizInfo.strPassword = this.strPassword;
                objUUBizInfo.blnIntegrated = this.blnIntegrated;

                objU8UUDS = objUUBizInfo.GetU8UUTableAllInfo("UTU", this.chbU8UUYearInfo.Checked, this.upbInfoGress);
                this.ugUUTable.DataSource = objU8UUDS;

            }

            if (this.cbOtherSystemInfo.Checked)
            {
                System.Data.DataTable objOtherDT = new DataTable();

                objOtherDT = this.GetOtherSystemTableInfo(this.cbDataBase.Text);

                this.ugOtherSystemTables.DataSource = objOtherDT;
            }
        }

        private int checkUTUExists()
        {
            for (int i = 0; i < this.cbDataBase.Items.Count; i++)
            {
                if (this.cbDataBase.Items[i].ToString() == "UTU")
                    return 1;
            }
            return 0;
        }

        private void ShowAllDatabase()
        {
            DataTable objDT = null;

            objDT = this.GetAllDatabase(this.objSQLConnect);

            if (objDT != null)
            {
                for (int i = 0; i < objDT.Rows.Count; i++)
                {
                    this.cbDataBase.Items.Add(((string)objDT.Rows[i][0]).Trim());
                }

                if (this.cbDataBase.Items.Count > 0) this.cbDataBase.SelectedIndex = 0;
            }
        }

        private void ShowAllDatabaseInfo()
        {
            DataSet objDS = null;

            objDS = this.DatabaseInfoDataSet();
            this.ugDBInfo.DataSource = objDS;
        }


        private DataSet objDTAllDBInfo = new DataSet();
        private DataSet DatabaseInfoDataSet()
        {
            DataTable objDTAllDB = null;
            DataTable[] objDTDBInfo = new DataTable[2];
            //DataSet objDTAllDBInfo = new DataSet();

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
                    objSQLComm.CommandText = @"SELECT LTRIM(RTRIM([NAME])) AS ���ݿ�, DTB.DATABASE_ID AS [���ݿ� ID], CAST(0 AS FLOAT) AS [�ܿռ�(MB)], CAST(0 AS FLOAT) AS [���ݿռ�(MB)], CAST(0 AS FLOAT) AS [��־�ռ�(MB)]
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
                throw e;
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
                objDTResult.Columns.Add("���ݿ��ܿռ�(MB)", Type.GetType("System.String"));
                objDTResult.Columns.Add("���ݿռ�(MB)", Type.GetType("System.String"));
                objDTResult.Columns.Add("������ʹ�ÿռ�(MB)", Type.GetType("System.String"));
                objDTResult.Columns.Add("������ʹ�ÿռ�(MB)", Type.GetType("System.String"));
                objDTResult.Columns.Add("��־�ռ�(MB)", Type.GetType("System.String"));
                objDTResult.Columns.Add("��־��ʹ�ÿռ�(MB)", Type.GetType("System.String"));
                objDTResult.Columns.Add("δ����ռ�(MB)", Type.GetType("System.String"));
                objDTResult.Columns.Add("�����ռ�(MB)", Type.GetType("System.String"));
                objDTResult.Columns.Add("δʹ�ÿռ�(MB)", Type.GetType("System.String"));

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

                        System.Data.DataRow objRow = null;
                        if (objDSSQL.Tables.Count > 0)
                        {
                            objRow = objDTResult.NewRow();
                            objRow[0] = ((string)(objDTAllDB.Rows[i]["���ݿ�"])).Trim();
                            objRow[1] = ((string)(objDSSQL.Tables[0].Rows[0]["database_size"])).Replace("MB", "").Trim();
                            objRow[2] = (decimal.Parse(((string)(objDSSQL.Tables[0].Rows[0]["database_size"])).Trim().Substring(0, ((string)(objDSSQL.Tables[0].Rows[0]["database_size"])).Trim().Length - 2))
                                                 - (decimal)objDSSQL.Tables[2].Rows[0]["��־�ռ�"]).ToString("#,###.##0");
                            objRow[3] = (double.Parse(((string)(objDSSQL.Tables[1].Rows[0]["data"])).Replace("KB", "")) / 1024).ToString("#,###.##0").Trim();
                            objRow[4] = (double.Parse(((string)(objDSSQL.Tables[1].Rows[0]["index_size"])).Replace("KB", "")) / 1024).ToString("#,###.##0").Trim();
                            objRow[5] = ((decimal)objDSSQL.Tables[2].Rows[0]["��־�ռ�"]).ToString("#,###.##0").Trim();
                            objRow[6] = ((decimal)objDSSQL.Tables[2].Rows[0]["��־��ʹ�ÿռ�"]).ToString("#,###.##0").Trim();
                            objRow[7] = ((string)(objDSSQL.Tables[0].Rows[0]["unallocated space"])).Replace("MB", "").Trim(); ;
                            objRow[8] = (double.Parse(((string)(objDSSQL.Tables[1].Rows[0]["reserved"])).Replace("KB", "")) / 1024).ToString("#,##0.##0").Trim();
                            objRow[9] = (double.Parse(((string)(objDSSQL.Tables[1].Rows[0]["unused"])).Replace("KB", "")) / 1024).ToString("#,##0.##0").Trim();

                            objDTResult.Rows.Add(objRow);

                            objDTAllDB.Rows[i][2] = double.Parse((string)objRow[1]);
                            objDTAllDB.Rows[i][3] = double.Parse((string)objRow[2]);
                            objDTAllDB.Rows[i][4] = double.Parse((string)objRow[5]); ;
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
            objDTResult.Columns.Add("���ݿռ�(MB)", Type.GetType("System.Double"));
            objDTResult.Columns.Add("�������ݿռ�(MB)", Type.GetType("System.Double"));
            objDTResult.Columns.Add("�����ռ�(MB)", Type.GetType("System.Double"));
            objDTResult.Columns.Add("δʹ�ÿռ�(MB)", Type.GetType("System.Double"));
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
                        objDTResult.Rows.Add(new object[] { objDR.GetString(0), int.Parse(objDR.GetString(1)), 
                                                            int.Parse(objDR.GetString(3).Replace("KB", "")) / 1024.0, 
                                                            int.Parse(objDR.GetString(4).Replace("KB", "")) / 1024.0, 
                                                            int.Parse(objDR.GetString(2).Replace("KB", "")) / 1024.0, 
                                                            int.Parse(objDR.GetString(5).Replace("KB", "")) / 1024.0 });
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


        private void SetU8DSTableStruct(DataSet objDSResult, DataTable objDTResult, DataTable objDTResultYearInfo)
        {
            objDTResult.TableName = "ObjectDetail";
            objDTResult.Columns.Add("���", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("ҵ�����", Type.GetType("System.String"));
            objDTResult.Columns.Add("�����¼��", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("�ӱ��¼��", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("ҵ����������", Type.GetType("System.String"));
            objDTResult.Columns.Add("ҵ����������", Type.GetType("System.String"));
            objDTResult.Columns.Add("�������ݿռ�(MB)", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("���������ռ�(MB)", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("�ӱ����ݿռ�(MB)", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("�ӱ������ռ�(MB)", Type.GetType("System.Int32"));

            objDTResultYearInfo.TableName = "YearInfo";
            objDTResultYearInfo.Columns.Add("ҵ�����", Type.GetType("System.String"));
            objDTResultYearInfo.Columns.Add("����ʱ�����", Type.GetType("System.Int32"));
            objDTResultYearInfo.Columns.Add("�����¼��", Type.GetType("System.Int32"));
            objDTResultYearInfo.Columns.Add("�ӱ��¼��", Type.GetType("System.Int32"));

            objDSResult.Tables.Add(objDTResult); objDSResult.Tables.Add(objDTResultYearInfo);
            objDSResult.Relations.Add("YearInfo", objDTResult.Columns["ҵ�����"], objDTResultYearInfo.Columns["ҵ�����"]);
        }

        private System.Data.DataSet GetU8TableAllInfo(string strDBName, int iIncludeYearInfo)
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

            Upgrade.Apps.U8BizData oBizInfo;
            Upgrade.Apps.U8BizYearData oBizYearInfo;

            string sMainTableName = ""; string sDetailTableName = ""; int iBizObjNO = 0;

            string strVersion = "";

            string strMisTable = "";
            string strSQL = "";
            int intIncludeU8DB = 0;

            int int�����¼�� = 0;                  int int�ӱ��¼�� = 0;
            double str�������ݿռ� = 0;             double str���������ռ� = 0;
            double str�ӱ����ݿռ� = 0;             double str�ӱ������ռ� = 0;
            string strҵ���������� = "";            string strҵ���������� = "";

            int intYear����ʱ����� = 0;
            int intYear�����¼�� = 0;              int intYear�ӱ��¼�� = 0;           

            try
            {
                this.SetU8DSTableStruct(objDSResult, objDTResult, objDTResultYearInfo);  //����U8������Ϣ�ṹ

                this.upbInfoGress.Text = "�� " + strDBName + " ���ݿ�";

                objConn = this.OpenSQLConnection(objConn, strDBName);

                objSQLComm = objConn.CreateCommand();
                objSQLComm.CommandTimeout = 600;

                if (this.intCheckU8DB == 1)
                {
                    this.upbInfoGress.Text = "�ж����ݿ� " + strDBName + " �Ƿ� U8+ ���ݿ�";
                    if (this.CheckISU8DB(objSQLComm, objDR, strDBName,ref strVersion) <= 0)
                    {
                        //MessageBox.Show("�����ݿⲻ�� ����U8+ ���ݿ⡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return objDSResult;
                    }
                }

                this.upbInfoGress.Text = "ȡ���ݿ� " + strDBName + " ��Ӧ U8+ ������Ϣ";

                intIncludeU8DB = this.U8DBInfo(objSQLComm, objDR, strDBName, strVersion);
                if (intIncludeU8DB <= 0 && this.intCareU8DBInfo == 1)
                {
                    return objDSResult;
                }                

                //========== �����ݱ� ============================================================================================================================================================
                objXMLDom.Load(Application.StartupPath + "\\U8ERPConfig.xml");
                System.Xml.XmlNodeList objXMLNL;
                if (double.Parse(strVersion) < 10)
                { objXMLNL = objXMLDom.SelectNodes("/Config/U8Tables[@Version = '8.900']/Table"); }
                else if (double.Parse(strVersion) <= 10.1 && double.Parse(strVersion) <= 10)
                { objXMLNL = objXMLDom.SelectNodes("/Config/U8Tables[@Version = '10.100']/Table"); }
                else if (double.Parse(strVersion) >= 11)
                { objXMLNL = objXMLDom.SelectNodes("/Config/U8Tables[@Version = '11.000']/Table"); }
                else
                { objXMLNL = objXMLDom.SelectNodes("/Config/U8Tables[@Version = '" + strVersion + "']/Table"); }
                //objXMLNL = objXMLDom.SelectNodes("./Config/U8Tables[@Version = '" + (double.Parse(strVersion) < 10 ? "8.900" : strVersion) + "']/Table");

                this.upbInfoGress.FillAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                this.upbInfoGress.Refresh();
                this.upbInfoGress.Maximum = objXMLNL.Count;
                this.upbInfoGress.Value = 0;
                this.upbInfoGress.Text = "�� " + strDBName.Trim() + " ���ݿ�����Ҫ������U8+ҵ���������    [Completed] Of [Range] Completed                  ";

                //���ͼ������
                clsU8ChartData.htU8BizObjTableData.Clear();
                clsU8ChartData.htU8BizObjTableYearData.Clear();

                for (int i = 0; i < objXMLNL.Count; i++)
                {

                    this.upbInfoGress.Value = i + 1;
                    Application.DoEvents();

                    int�����¼�� = 0;             int�ӱ��¼�� = 0;
                    str�������ݿռ� = 0;          str���������ռ� = 0;
                    str�ӱ����ݿռ� = 0;          str�ӱ������ռ� = 0;
                    strҵ���������� = "";          strҵ���������� = "";

                    intYear����ʱ����� = 0;
                    intYear�����¼�� = 0;         intYear�ӱ��¼�� = 0;

                    sMainTableName = "";           sDetailTableName = "";

                    objXMLEle = (System.Xml.XmlElement)objXMLNL[i];

                    #region �ж����ݱ����
                    //=================================//                 
                    objSQLComm.CommandText = @"SELECT * FROM sysobjects With(nolock) 
                                               WHERE TYPE = 'U' AND NAME = '" + objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() + "'";
                    objDR = objSQLComm.ExecuteReader();
                    if (!objDR.Read())
                    {
                        strMisTable = strMisTable + "    ҵ�����" + objXMLEle.Attributes["Object"].Value.ToString().Trim() +
                                                    "    ���� [" + objXMLEle.Attributes["MainTableName"].Value.ToString() + "]\r\n";
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

                    try
                    {
                        #region  ��ѯ���ݱ���Ϣ
                        sMainTableName = objXMLEle.Attributes["MainTableName"].Value.ToString().Trim();

                        objSQLComm.CommandText = @"EXEC SP_SPACEUSED N'" + objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() + "'";
                        objDR = objSQLComm.ExecuteReader();

                        if (objDR.Read())
                        {
                            int�����¼�� = int.Parse(objDR.GetString(1));
                            str�������ݿռ� = double.Parse(objDR.GetString(3).Replace("KB", "")) / 1024;
                            str���������ռ� = double.Parse(objDR.GetString(4).Replace("KB", "")) / 1024;
                        }
                        if (!objDR.IsClosed) objDR.Close();

                        //=================================//
                        //this.upbInfoGress.Text = "�� " + strDBName.Trim() + " ���ݿ�����Ҫ�� ERP-U8 ���ݱ�ҵ�����" + objXMLEle.Attributes["Object"].Value.ToString().Trim() + " ҵ����Ϣ    [Completed] Of [Range] Completed                  ";

                        if (objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() != "")
                        {
                            objSQLComm.CommandText = @"SELECT (SELECT CONVERT(NVARCHAR(30), MIN([" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + @"] ), 120) 
                                                           FROM [" + objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() + @"] WITH(NOLOCK)) AS dMinDate, 
                                                          (SELECT CONVERT(NVARCHAR(30), MAX([" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + @"] ), 120) 
                                                           FROM [" + objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() + @"] WITH(NOLOCK)) AS dMaxDate ";
                            objDR = objSQLComm.ExecuteReader();
                            if (objDR.Read())
                            {
                                strҵ���������� = objDR.IsDBNull(0) ? "" : objDR.GetString(0); strҵ���������� = objDR.IsDBNull(1) ? "" : objDR.GetString(1);

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
                            sDetailTableName = objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim();
                            //this.upbInfoGress.Text = "�� " + strDBName.Trim() + " ���ݿ�����Ҫ�� ERP-U8 ���ݱ�ҵ�����" + objXMLEle.Attributes["Object"].Value.ToString().Trim() + "���ӱ�" + objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() + " �ռ���Ϣ    [Completed] Of [Range] Completed                  ";

                            objSQLComm.CommandText = @"EXEC SP_SPACEUSED N'" + objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() + "'";
                            objDR = objSQLComm.ExecuteReader();

                            if (objDR.Read())
                            {
                                int�ӱ��¼�� = int.Parse(objDR.GetString(1));
                                str�ӱ����ݿռ� = double.Parse(objDR.GetString(3).Replace("KB", "")) / 1024;
                                str�ӱ������ռ� = double.Parse(objDR.GetString(4).Replace("KB", "")) / 1024;
                            }
                            objDR.Close();
                        }
                        #endregion

                        objDTResult.Rows.Add(new object[] { i + 1, objXMLEle.Attributes["Object"].Value.ToString(), 
                                                        int�����¼��, int�ӱ��¼��, strҵ����������, strҵ����������,
                                                        str�������ݿռ�, str���������ռ�, str�ӱ����ݿռ�, str�ӱ������ռ�
                                                      });
                        #region ����ͼ������
                        oBizInfo = new U8BizData();
                        oBizInfo.sU8TableName = sMainTableName;
                        oBizInfo.sU8ObjectName = objXMLEle.Attributes["Object"].Value.ToString();
                        oBizInfo.iU8TableCount = int�����¼��;
                        oBizInfo.dblU8TableSpace = str�������ݿռ� + str���������ռ�;
                        iBizObjNO = iBizObjNO + 1;
                        Upgrade.Apps.clsU8ChartData.htU8BizObjTableData.Add(oBizInfo);
                        if (sDetailTableName != "")
                        {
                            oBizInfo = new U8BizData();
                            oBizInfo.sU8TableName = sDetailTableName;
                            oBizInfo.sU8ObjectName = objXMLEle.Attributes["Object"].Value.ToString() + "�ӱ�";
                            oBizInfo.iU8TableCount = int�ӱ��¼��;
                            oBizInfo.dblU8TableSpace = str�ӱ����ݿռ� + str�ӱ������ռ�;
                            iBizObjNO = iBizObjNO + 1;
                            Upgrade.Apps.clsU8ChartData.htU8BizObjTableData.Add(oBizInfo);
                        }
                        #endregion


                        if (intIncludeU8DB != 2)
                        {
                            this.objU8Table.Rows.Add(new object[] { strDBName.Trim(), i + 1, objXMLEle.Attributes["Object"].Value.ToString(), 
                                                                int�����¼��, int�ӱ��¼��, strҵ����������, strҵ����������,
                                                                str�������ݿռ�, str���������ռ�, str�ӱ����ݿռ�, str�ӱ������ռ�
                                                               });

                        }
                        Application.DoEvents();

                        #region  ��ѯ�������������
                        //--------------- ��ѯ������������� -------------------------------------------------------------------------------------------------------------
                        if (iIncludeYearInfo == 1 && objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() != "")
                        {
                            objSQLComm.CommandText = @"SELECT C.[NAME] AS CNAME, O.[NAME] AS TNAME FROM SYSCOLUMNS C INNER JOIN SYSOBJECTS O ON C.ID=O.ID " +
                                                      "WHERE O.NAME = '" + objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() + "' " +
                                                            "AND C.NAME = '" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + "' AND C.XTYPE = 61 ";
                            if (!objDR.IsClosed) objDR.Close();
                            objDR = objSQLComm.ExecuteReader();

                            if (objDR.Read())
                            {
                                if (objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() != "" && objXMLEle.Attributes["KeyName"].Value.ToString().Trim() != ""
                                    && (objXMLEle.Attributes["DetailForeignKeyName"] == null || objXMLEle.Attributes["DetailForeignKeyName"].Value.ToString().Trim() == ""))
                                {

                                    strSQL = @"DECLARE @T1 TABLE(iYear INT, iMainCount INT)
                                            DECLARE @T2 TABLE(iYear INT, iDetailCount INT)

                                            INSERT INTO @T1 (iYear,	iMainCount) 
                                            SELECT YEAR([" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + @"]) AS iBillYear, ISNULL(Count(*), 0) AS MainCount 
                                            FROM [" + objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() + @"] R WITH(NOLOCK) 
                                            GROUP BY YEAR([" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + "]) " + @"

                                            INSERT INTO @T2 (iYear,	iDetailCount) 
                                            SELECT YEAR(R.[" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + @"]) AS iBillYear, ISNULL(Count(*), 0) AS DetailCount
                                            FROM [" + objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() + @"] R WITH(NOLOCK) LEFT JOIN [" + objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() + @"] RS WITH(NOLOCK) " +
                                                    @"ON R." + objXMLEle.Attributes["KeyName"].Value.ToString().Trim() + @" = RS." + objXMLEle.Attributes["KeyName"].Value.ToString().Trim() + @"
                                            GROUP BY YEAR(R.[" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + @"])

                                            SELECT ISNULL(A.iYear,0) AS iYear, iMainCount, ISNULL(iDetailCount, 0) AS iDetailCount FROM @T1 A LEFT JOIN @T2 B ON A.iYear = B.iYear ORDER BY A.iYear DESC";

                                    //if (objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() == "Transvouch")
                                    //    strSQL = strSQL;
                                }
                                else if (objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() != ""
                                         && objXMLEle.Attributes["KeyName"].Value.ToString().Trim() != ""
                                         && (objXMLEle.Attributes["DetailForeignKeyName"] != null && objXMLEle.Attributes["DetailForeignKeyName"].Value.ToString().Trim() != ""))
                                {
                                    strSQL = @"DECLARE @T1 TABLE(iYear INT, iMainCount INT)
                                            DECLARE @T2 TABLE(iYear INT, iDetailCount INT)

                                            INSERT INTO @T1 (iYear,	iMainCount) 
                                            SELECT YEAR([" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + @"]) AS iBillYear, ISNULL(Count(*), 0) AS MainCount 
                                            FROM [" + objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() + @"] R WITH(NOLOCK) 
                                            GROUP BY YEAR([" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + "]) " + @"

                                            INSERT INTO @T2 (iYear,	iDetailCount) 
                                            SELECT YEAR(R.[" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + @"]) AS iBillYear, ISNULL(Count(*), 0) AS DetailCount
                                            FROM [" + objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() + @"] R WITH(NOLOCK) LEFT JOIN [" + objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() + @"] RS WITH(NOLOCK) " +
                                                     @"ON R." + objXMLEle.Attributes["KeyName"].Value.ToString().Trim() + @" = RS." + objXMLEle.Attributes["DetailForeignKeyName"].Value.ToString().Trim() + @"
                                            GROUP BY YEAR(R.[" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + @"])

                                            SELECT ISNULL(A.iYear,0) AS iYear, iMainCount, ISNULL(iDetailCount, 0) AS iDetailCount FROM @T1 A LEFT JOIN @T2 B ON A.iYear = B.iYear ORDER BY A.iYear DESC";

                                    //if (objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() == "Transvouch")
                                    //    strSQL = strSQL;
                                }
                                else
                                {
                                    strSQL = @"SELECT ISNULL(YEAR([" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + "]), 0) AS iBillYear, " +
                                                    "ISNULL(Count(*), 0) AS iMainCount, 0 AS iDetailCount " +
                                              "FROM [" + objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() + "] " +
                                              "GROUP BY YEAR([" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + "]) " +
                                              "ORDER BY YEAR([" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + "]) DESC";
                                }
                                objSQLComm.CommandText = strSQL;
                                if (!objDR.IsClosed)
                                { objDR.Close(); objDR = null; }
                                objDR = objSQLComm.ExecuteReader();

                                #region ����ͼ������
                                oBizYearInfo = new U8BizYearData();
                                oBizYearInfo.sU8TableName = sMainTableName;
                                oBizYearInfo.sU8ObjectName = objXMLEle.Attributes["Object"].Value.ToString();
                                oBizYearInfo.oU8BizYearCount = new System.Collections.Hashtable();
                                #endregion

                                if (objDR.HasRows)
                                {
                                    while (objDR.Read())
                                    {
                                        try
                                        {
                                            intYear����ʱ����� = objDR.GetInt32(0); intYear�����¼�� = objDR.GetInt32(1); intYear�ӱ��¼�� = objDR.GetInt32(2);
                                            objDTResultYearInfo.Rows.Add(new object[] { objXMLEle.Attributes["Object"].Value.ToString(), intYear����ʱ�����, intYear�����¼��, intYear�ӱ��¼�� });

                                            if (intIncludeU8DB != 2)
                                                this.objU8TableYearInfo.Rows.Add(new object[] { strDBName.Trim(), objXMLEle.Attributes["Object"].Value.ToString(), intYear����ʱ�����, intYear�����¼��, intYear�ӱ��¼�� });

                                            oBizYearInfo.oU8BizYearCount.Add(intYear����ʱ�����, new int[2] { intYear�����¼��, intYear�ӱ��¼�� });
                                        }
                                        catch (Exception Ex)
                                        {
                                            strMisTable = strMisTable + "    ҵ�����" + objXMLEle.Attributes["Object"].Value.ToString().Trim() +
                                                                        " ��ȡ��������д���" + Ex.Message + "��\r\n";
                                            continue;
                                        }
                                    }
                                }

                                Upgrade.Apps.clsU8ChartData.htU8BizObjTableYearData.Add(oBizYearInfo);

                            }
                            if (!objDR.IsClosed) objDR.Close();
                        }
                        //----------------------------------------------------------------------------------------------------------------------------------------------
                        #endregion

                    }
                    catch (Exception Exc)
                    {
                        strMisTable = strMisTable + "    ҵ�����" + objXMLEle.Attributes["Object"].Value.ToString().Trim() +
                                                                        " ��ȡ��������д���" + Exc.Message + "��\r\n";
                        continue;
                    }
                    Application.DoEvents();
                }
                if (strMisTable != "")
                {
                    objXMLEle = (System.Xml.XmlElement)objXMLDom.SelectSingleNode("./Config/U8Tables");

                    //if(objXMLEle.Attributes["Version"] != null)
                    //    MessageBox.Show("�������ݱ������ݿ��в����ڣ�\r\n" + strMisTable.Substring(0, strMisTable.Length - 2) + "��\r\n" + 
                    //                    "���������������ļ���U8+�汾��" + objXMLEle.Attributes["Version"].Value.ToString() + " ��һ�¡�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //else
                    //    MessageBox.Show("�������ݱ������ݿ��в����ڣ�\r\n" + strMisTable.Substring(0, strMisTable.Length - 2) + "��\r\n" +
                    //                    "���������������ļ���U8+�汾��һ�¡�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (objXMLEle.Attributes["Version"] != null)

                        this.WriteLog("�������ݱ������ݿ��в����ڣ�\r\n" + strMisTable.Substring(0, strMisTable.Length - 2) + "��\r\n" +
                                        "���������������ļ���U8+�汾��" + objXMLEle.Attributes["Version"].Value.ToString() + " ��һ�¡�");
                    else
                        this.WriteLog("�������ݱ������ݿ��в����ڣ�\r\n" + strMisTable.Substring(0, strMisTable.Length - 2) + "��\r\n" +
                                        "���������������ļ���U8+�汾��һ�¡�");

                    
                    MessageBox.Show("ִ�г�ȡ���ݱ���Ϣ��ɡ�\r\n�������ݶ��������ݣ�������鿴�����־��" , "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void WriteLog(string sInfo)
        {
            string sAppLogPath = System.Windows.Forms.Application.StartupPath + @"\Logs";
            string sAppLog =sAppLogPath + @"\" + "DatabaseTablesInfo_" + this.cbDataBase.Text.Trim() + "_" + System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".log";

            if (!System.IO.Directory.Exists(sAppLogPath))
            {
                System.IO.Directory.CreateDirectory(sAppLogPath);
            }

            System.IO.StreamWriter SW;
            if (!System.IO.File.Exists(sAppLog))
            {
                SW = System.IO.File.CreateText(sAppLog);
            }
            else
            {
                SW = System.IO.File.AppendText(sAppLog);
            }
            SW.WriteLine("ִ�����ݳ�ȡʱ�䣺" +System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:SS")); 
            SW.WriteLine("=============================================================================================");
            SW.WriteLine(sInfo);
            SW.WriteLine("=============================================================================================");

            SW.Close(); 
        }



        private System.Data.DataTable GetOtherSystemTableInfo(string strDBName)
        {
            System.Data.SqlClient.SqlConnection objConn = new System.Data.SqlClient.SqlConnection();
            DataTable objDTResult = new DataTable();
            DataTable objDTTables = new DataTable();
            System.Data.SqlClient.SqlDataReader objDR = null;
            System.Data.SqlClient.SqlDataAdapter objSQLDA = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();
            System.Xml.XmlDocument objXMLDom = new System.Xml.XmlDocument();
            System.Xml.XmlElement objXMLEle;
            string strMisTable = "";
            string strSystemName = "";

            int int�����¼�� = 0;
            int int�ӱ��¼�� = 0;
            string str�������ݿռ� = "";
            string str���������ռ� = "";
            string str�ӱ����ݿռ� = "";
            string str�ӱ������ռ� = "";
            string strҵ���������� = "";
            string strҵ���������� = "";

            objDTResult.TableName = "ObjectDetail";
            objDTResult.Columns.Add("���", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("ҵ�����", Type.GetType("System.String"));
            objDTResult.Columns.Add("�����¼��", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("�ӱ��¼��", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("ҵ����������", Type.GetType("System.String"));
            objDTResult.Columns.Add("ҵ����������", Type.GetType("System.String"));
            objDTResult.Columns.Add("�������ݿռ�", Type.GetType("System.String"));
            objDTResult.Columns.Add("���������ռ�", Type.GetType("System.String"));
            objDTResult.Columns.Add("�ӱ����ݿռ�", Type.GetType("System.String"));
            objDTResult.Columns.Add("�ӱ������ռ�", Type.GetType("System.String"));

            try
            {
                this.upbInfoGress.Text = "�� " + strDBName + " ���ݿ�";

                objConn = this.OpenSQLConnection(objConn, strDBName);

                objSQLComm = objConn.CreateCommand();
                objSQLComm.CommandTimeout = 600;

                 objXMLDom.Load(Application.StartupPath + "\\U8ERPConfig.xml");

                objXMLEle = (System.Xml.XmlElement)objXMLDom.SelectSingleNode("./Config/OtherTables");
                strSystemName = objXMLEle.Attributes["Name"].Value.Trim();
                this.upbInfoGress.Text = "ȡ���ݿ� " + strDBName + " ��Ӧ" + strSystemName + "������Ϣ";           

                //========== �����ݱ� ============================================================================================================================================================
                this.upbInfoGress.FillAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                this.upbInfoGress.Refresh();
                this.upbInfoGress.Maximum = objXMLDom.SelectNodes("./Config/OtherTables/Table").Count;
                this.upbInfoGress.Value = 0;
                this.upbInfoGress.Text = "�� " + strDBName.Trim() + " ���ݿ�����Ҫ��" + strSystemName + "���ݱ�    [Completed] Of [Range] Completed                  ";

                for (int i = 0; i < objXMLDom.SelectNodes("./Config/OtherTables/Table").Count; i++)
                {                    
                    int�����¼�� = 0;
                    int�ӱ��¼�� = 0;
                    str�������ݿռ� = "";
                    str���������ռ� = "";
                    str�ӱ����ݿռ� = "";
                    str�ӱ������ռ� = "";
                    strҵ���������� = "";
                    strҵ���������� = "";

                    objXMLEle = (System.Xml.XmlElement)objXMLDom.SelectNodes("./Config/OtherTables/Table")[i];
                    
                    this.upbInfoGress.Text = "�����ݿ� " + strDBName.Trim() + " ����Ҫ��" + strSystemName + "ҵ�����" + objXMLEle.Attributes["Object"].Value.ToString().Trim() + " ҵ����Ϣ    [Completed] Of [Range] Completed                  ";
                    
                    //=================================//
                    objSQLComm.CommandText = @"SELECT * FROM sysobjects With(nolock) 
                                               WHERE TYPE = 'U' AND NAME = '" + objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() + "'";
                    objDR = objSQLComm.ExecuteReader();
                    if (!objDR.Read())
                    {
                        strMisTable = strMisTable + "    ҵ�����" + objXMLEle.Attributes["Object"].Value.ToString().Trim() +
                                                    "    ���� [" + objXMLEle.Attributes["MainTableName"].Value.ToString() + "]\r\n";
                        objDR.Close();
                        continue;
                    }
                    if (!objDR.IsClosed) objDR.Close();

                    if (objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() != "")
                    {
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
                    objSQLComm.CommandText = @"EXEC SP_SPACEUSED N'" + objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() + "'";
                    objDR = objSQLComm.ExecuteReader();

                    if (objDR.Read())
                    {
                        int�����¼�� = int.Parse(objDR.GetString(1));
                        str�������ݿռ� = objDR.GetString(3);
                        str���������ռ� = objDR.GetString(4);
                    }
                    objDR.Close();

                    //=================================//

                    if (objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() != "")
                    {
                        objSQLComm.CommandText = @"SELECT (SELECT CONVERT(NVARCHAR(30), MIN([" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + @"] ), 120) 
                                                           FROM [" + objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() + @"] WITH(NOLOCK)) AS dMinDate, 
                                                          (SELECT CONVERT(NVARCHAR(30), MAX([" + objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() + @"] ), 120) 
                                                           FROM [" + objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() + @"] WITH(NOLOCK)) AS dMaxDate ";
                        objDR = objSQLComm.ExecuteReader();
                        if (objDR.Read())
                        {
                            strҵ���������� = objDR.IsDBNull(0) ? "" : objDR.GetString(0);
                            strҵ���������� = objDR.IsDBNull(1) ? "" : objDR.GetString(1);

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
                            str�ӱ����ݿռ� = objDR.GetString(3);
                            str�ӱ������ռ� = objDR.GetString(4);
                        }
                        objDR.Close();
                    }
                    objDTResult.Rows.Add(new object[] { i + 1, objXMLEle.Attributes["Object"].Value.ToString(), 
                                                        int�����¼��, int�ӱ��¼��, strҵ����������, strҵ����������,
                                                        str�������ݿռ�, str���������ռ�, str�ӱ����ݿռ�, str�ӱ������ռ�
                                                      });

                    this.upbInfoGress.Value = i + 1;
                    Application.DoEvents();
                }
                if (strMisTable != "")
                {
                    objXMLEle = (System.Xml.XmlElement)objXMLDom.SelectSingleNode("./Config/OtherTables");

                    if (objXMLEle.Attributes["Version"] != null)
                        MessageBox.Show("�������ݱ������ݿ��в����ڣ�\r\n" + strMisTable.Substring(0, strMisTable.Length - 2) + "��\r\n" +
                                        "���������������ļ���" + strSystemName + "�汾��" + objXMLEle.Attributes["Version"].Value.ToString() + " ��һ�¡�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("�������ݱ������ݿ��в����ڣ�\r\n" + strMisTable.Substring(0, strMisTable.Length - 2) + "��\r\n" +
                                        "���������������ļ���" + strSystemName + "�汾��һ�¡�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return objDTResult;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ϵͳ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return objDTResult;
            }
            finally
            {
                if (objDR != null)
                {
                    if (!objDR.IsClosed) objDR.Close();
                }
            }
        }



        private int U8DBInfo(System.Data.SqlClient.SqlCommand objSQLComm, System.Data.SqlClient.SqlDataReader objDR, string strDBName, string strVersion)
        {
            System.Data.DataTable objDT = new DataTable();

            try
            {
                System.Data.SqlClient.SqlDataAdapter objDA = new SqlDataAdapter();
                DataView view = new DataView();
                view.Table = this.objU8DB;
                view.RowFilter = "���ݿ����� = '" + strDBName.Trim() + "'";
                view.RowStateFilter = DataViewRowState.CurrentRows;

                //��һ�β�ѯ�Ѿ������˿�Ľ����
                if (view.Count > 0) return 2;

                if (objDR != null && !objDR.IsClosed) objDR.Close();
                objSQLComm.CommandText = @"SELECT [name] cDBName, dbid FROM master..sysdatabases 
                                           WHERE [name]= 'UFSystem'";
                objDR = objSQLComm.ExecuteReader();

                if (objDR != null && !objDR.Read() && this.intCareU8DBInfo == 0)
                {
                    if (objDR != null && !objDR.IsClosed) objDR.Close();
                    objSQLComm.CommandText = @"SELECT [name] cDBName, dbid AS BatabaseID FROM  master.dbo.sysdatabases WHERE [name] = '" + strDBName.Trim() + "'";
                    objDR = objSQLComm.ExecuteReader();

                    if (objDR.Read())
                    {
                        this.objU8DB.Rows.Add(new Object[] { objDR.GetInt32(1), "", strDBName.Trim(), "", "" });
                    }
                    else
                    {
                        MessageBox.Show("��ѡ���ݿⷢ������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;
                    }
                    if (!objDR.IsClosed) objDR.Close();
                    return 1;
                }

                if (objDR != null && !objDR.IsClosed) objDR.Close();
                objSQLComm.CommandText = @"SELECT 'UFDATA_' + UA.cAcc_Id + '_' + CAST(UP.iYear AS NVARCHAR(10)) AS cDBName, sdb.dbid, 
                                                   UA.cAcc_Id, UA.cAcc_Name, UA.cUnitName  
                                           FROM UFSystem..UA_Account UA INNER JOIN UFSystem..UA_Period UP ON UA.cAcc_Id = UP.cAcc_Id
						                                                INNER JOIN master..sysdatabases sdb ON sdb.name = 'UFDATA_' + UA.cAcc_Id + '_' + CAST(UP.iYear AS NVARCHAR(10))
                                           WHERE 'UFDATA_' + UA.cAcc_Id + '_' + CAST(UP.iYear AS NVARCHAR(10)) = '" + strDBName.Trim() + @"' 
                                           GROUP BY  UA.cAcc_Id, UP.iYear, UA.cAcc_Name, UA.cUnitName, sdb.dbid";
                objDR = objSQLComm.ExecuteReader();
                if (objDR.Read())
                {
                    this.objU8DB.Rows.Add(new Object[] { int.Parse(objDR.GetInt16(1).ToString()), objDR.GetString(2), strDBName.Trim(), objDR.GetString(3), objDR.GetString(4), strVersion });
                }
                else if (!objDR.Read() && this.intCareU8DBInfo == 0)
                {
                    if (!objDR.IsClosed) objDR.Close();
                    objSQLComm.CommandText = @"SELECT [name] cDBName, dbid AS BatabaseID FROM  master..sysdatabases
                                               WHERE [name] = '" + strDBName.Trim() + "'";
                    objDR = objSQLComm.ExecuteReader();
                    if (objDR.Read())
                    {
                        this.objU8DB.Rows.Add(new Object[] { int.Parse(objDR.GetInt16(1).ToString()), "", strDBName.Trim(), "", "" });
                    }
                    if (!objDR.IsClosed) objDR.Close();
                }
                else
                {
                    MessageBox.Show("������ѡ�е����ݿ⣺" + strDBName.Trim() + "��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
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



        private int CheckISU8DB(System.Data.SqlClient.SqlCommand objSQLComm, System.Data.SqlClient.SqlDataReader objDR, string strDBName, ref string strU8Version)
        {
            try
            {

                strU8Version = "";

                objSQLComm.CommandText = @"SELECT cValue FROM AccInformation WHERE cSysID = 'AA' AND cID = 99";
                objDR = objSQLComm.ExecuteReader();
                if (objDR.Read())
                {
                    strU8Version = objDR.GetSqlString(0).ToString().Trim();
                }
                else
                {
                    MessageBox.Show("ѡ�е����ݿ⣺" + strDBName.Trim() + " �޷��ж�ҵ���汾������U8+��ҵ�����ݿ⡣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                    //strU8Version = "10.0";
                }

                if (!objDR.IsClosed) objDR.Close();

                if (double.Parse(strU8Version) >= 10)
                {
                    objSQLComm.CommandText = @"SELECT Count(*) FROM sysobjects With(nolock) 
                                               WHERE type = 'U' AND NAME IN ('gl_accvouch', 'Inventory', 'CurrentStock', 'bas_part', 'mom_order', 'rdrecord32')";
                }
                else
                {
                    objSQLComm.CommandText = @"SELECT Count(*) FROM sysobjects With(nolock) 
                                               WHERE type = 'U' AND NAME IN ('gl_accvouch', 'Inventory', 'CurrentStock', 'bas_part', 'mom_order', 'rdrecord')";
                }
                objDR = objSQLComm.ExecuteReader();
                if (objDR.Read())
                {
                    if (objDR.GetInt32(0) != 6)
                    {
                        MessageBox.Show("ѡ�е����ݿ⣺" + strDBName.Trim() + " ����U8+��ҵ�����ݿ⣬���¼�汾�����ݿ�ṹ��һ�¡�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;
                    }
                }
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("ѡ�е����ݿ⣺" + strDBName.Trim() + "����U8+��ҵ�����ݿ⣬���޷��ж�ҵ���汾��\r\n������������" + e.InnerException, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
                //strU8Version = "10.0";
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
                                               "Connection Timeout=10;Application Name=̽����� MX ProcessProgram;Pooling=true;";
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

        private void ugU8Tables_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                this.ugU8Tables.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.Copy);
            }
        }

        private void ugDBU8Tables_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                this.ugDBU8Tables.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.Copy);
            }
        }

        private void chbIncludeU8_CheckedChanged(object sender, EventArgs e)
        {
             this.cbYearInfo.Enabled = this.chbIncludeU8.Checked;
        }
        private void chbIncludeU8UU_CheckedChanged(object sender, EventArgs e)
        {
            this.chbU8UUYearInfo.Enabled = this.chbIncludeU8UU.Checked;
        }


        #region ���ݿ�ȫ�����ݱ�������ͼ��չʾ
        //====================================================================================================================================================
        private string strGraphDatabaseName_All = "";
        private string strGraphDatabaseName_Top = "";
        private void utcAllTableInfo_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            
            if (this.utcAllTableInfo.SelectedTab.Key == "ALLTABLEGRAPH")
            {
                if (this.strGraphDatabaseName_All == this.strCurrentDBName)
                {
                    return;
                }
                System.Data.DataTable objDT = new DataTable();
                if (this.ugTable.DataSource != null)
                {
                    objDT = (System.Data.DataTable)this.ugTable.DataSource;
                }
                if (objDT.Rows.Count > 0)
                {
                    Upgrade.Apps.clsDBChartShow objChartShow = new clsDBChartShow();
                    objChartShow.DrawAllTableGraph(objDT, this.chartAlltable, (this.cbTableNum.Text == "ȫ��" ? 2000000 : int.Parse(this.cbTableNum.Text)), this.cbOrderBy.SelectedIndex);
                    objChartShow.DrawAllTableSpaceGraph(objDT, this.chartAllTableSpace, this.getSelectedDBSpace(this.strCurrentDBName));
                }
                this.strGraphDatabaseName_All = this.strCurrentDBName;
                this.intPreTableNum = (this.cbTableNum.Text == "ȫ��" ? 2000000 : int.Parse(this.cbTableNum.Text));
            }
            else if (this.utcAllTableInfo.SelectedTab.Key == "ALLTABLETOP20GRAPH")
            {
                if (this.strGraphDatabaseName_Top == this.strCurrentDBName)
                {
                    return;
                }

                System.Data.DataTable objDT = new DataTable();
                if (this.ugTable.DataSource != null)
                {
                    objDT = (System.Data.DataTable)this.ugTable.DataSource;
                }
                if (objDT.Rows.Count > 0)
                {
                    Upgrade.Apps.clsDBChartShow objChartShow = new clsDBChartShow();
                    objChartShow.DrawTop20TableGraph(objDT, this.chartTop20Table);
                    objChartShow.DrawTop20TablePieGraph(objDT, this.chartTop20TablePie, this.getSelectedDBSpace(this.strCurrentDBName));
                }
                this.strGraphDatabaseName_Top = this.strCurrentDBName;
            }            
        }

        private int intPreTableNum = 0;
        private int intPreTableOrder = 0;
        private void cbTableNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.utcAllTableInfo.SelectedTab.Key == "ALLTABLEGRAPH")
            {
                //this.strGraphDatabaseName_All == this.strCurrentDBName && 
                if (this.intPreTableNum == (this.cbTableNum.Text == "ȫ��" ? 2000000 : int.Parse(this.cbTableNum.Text)))
                {
                    return;
                }
                System.Data.DataTable objDT = new DataTable();
                if (this.ugTable.DataSource != null)
                {
                    objDT = (System.Data.DataTable)this.ugTable.DataSource;
                }
                if (objDT.Rows.Count > 0)
                {
                    Upgrade.Apps.clsDBChartShow objChartShow = new clsDBChartShow();
                    objChartShow.DrawAllTableGraph(objDT, this.chartAlltable, (this.cbTableNum.Text == "ȫ��" ? 2000000 : int.Parse(this.cbTableNum.Text)), this.cbOrderBy.SelectedIndex);
                    //this.DrawAllTableTop20Graph(objDT, this.chartAllTableSpace);
                }
                this.strGraphDatabaseName_All = this.strCurrentDBName;
                this.intPreTableNum = (this.cbTableNum.Text == "ȫ��" ? 2000000 : int.Parse(this.cbTableNum.Text));
            }
            
        }

        private void utcAllTableInfo_ActiveTabChanged(object sender, Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs e)
        {

        }

        private void cbOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.utcAllTableInfo.SelectedTab.Key == "ALLTABLEGRAPH")
            {
                //this.strGraphDatabaseName_All == this.strCurrentDBName && 
                if (this.intPreTableOrder == this.cbOrderBy.SelectedIndex)
                {
                    return;
                }
                System.Data.DataTable objDT = new DataTable();
                if (this.ugTable.DataSource != null)
                {
                    objDT = (System.Data.DataTable)this.ugTable.DataSource;
                }
                if (objDT.Rows.Count > 0)
                {
                    Upgrade.Apps.clsDBChartShow objChartShow = new clsDBChartShow();
                    objChartShow.DrawAllTableGraph(objDT, this.chartAlltable, (this.cbTableNum.Text == "ȫ��" ? 2000000 : int.Parse(this.cbTableNum.Text)), this.cbOrderBy.SelectedIndex);
                    //this.DrawAllTableTop20Graph(objDT, this.chartAllTableSpace);
                }
                this.strGraphDatabaseName_All = this.strCurrentDBName;
                this.intPreTableOrder = this.cbOrderBy.SelectedIndex;
            }
            
        }


        #endregion


        #region ҵ������ͼ��չʾ
        //=========================================================================================================================================
        private string strU8GraphDatabaseName_All = "";
        private string strU8GraphDatabaseName_Top = "";
        private int intPreBizTableNum = 0;
        private int intPreBizTableOrder = 0;
        private void utcU8BizTable_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            if (this.utcU8BizTable.SelectedTab.Key == "ALLU8BIZGRAPH")
            {
                if (this.strU8GraphDatabaseName_All == this.strCurrentDBName)
                {
                    return;
                }
                if (Upgrade.Apps.clsU8ChartData.htU8BizObjTableData.Count > 0)
                {
                    Upgrade.Apps.clsDBChartShow objChartShow = new clsDBChartShow();

                    objChartShow.DrawU8AllTableGraph(Upgrade.Apps.clsU8ChartData.htU8BizObjTableData, this.chartU8TableCount, 1,
                                                    (this.cbU8ChartTopN.Text == "ȫ��" ? 2000000 : int.Parse(this.cbU8ChartTopN.Text)), this.cbU8ChartOrderBy.SelectedIndex);
                    objChartShow.DrawU8AllTableSpaceGraph(Upgrade.Apps.clsU8ChartData.htU8BizObjTableData, this.chartU8TableSpace, this.getSelectedDBSpace(this.strCurrentDBName));

                    this.strU8GraphDatabaseName_All = this.strCurrentDBName;
                    this.intPreBizTableNum = (this.cbU8ChartTopN.Text == "ȫ��" ? 2000000 : int.Parse(this.cbU8ChartTopN.Text));
                }                
            }
            else if (this.utcU8BizTable.SelectedTab.Key == "ALLU8Top20GRAPH")
            {
                if (this.strU8GraphDatabaseName_Top == this.strCurrentDBName)
                {
                    return;
                }
                if (Upgrade.Apps.clsU8ChartData.htU8BizObjTableData.Count > 0)
                {
                    Upgrade.Apps.clsDBChartShow objChartShow = new clsDBChartShow();

                    objChartShow.DrawU8Top20TableGraph(Upgrade.Apps.clsU8ChartData.htU8BizObjTableData, this.chartU8TableTop20Count);
                    objChartShow.DrawU8Top20TablePieGraph(Upgrade.Apps.clsU8ChartData.htU8BizObjTableData, this.chartU8TableTop20Space, this.getSelectedDBSpace(this.strCurrentDBName));
                }
                this.strU8GraphDatabaseName_Top = this.strCurrentDBName;
            }
            
        }

        private void cbU8ChartTopN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.utcU8BizTable.SelectedTab.Key == "ALLU8BIZGRAPH")
            {
                //this.strU8GraphDatabaseName_All == this.strCurrentDBName && 
                if (this.intPreBizTableNum == (this.cbU8ChartTopN.Text == "ȫ��" ? 2000000 : int.Parse(this.cbU8ChartTopN.Text)))
                {
                    return;
                }

                if (Upgrade.Apps.clsU8ChartData.htU8BizObjTableData.Count > 0)
                {
                    Upgrade.Apps.clsDBChartShow objChartShow = new clsDBChartShow();
                    objChartShow.DrawU8AllTableGraph(Upgrade.Apps.clsU8ChartData.htU8BizObjTableData, this.chartU8TableCount, 1, (this.cbU8ChartTopN.Text == "ȫ��" ? 2000000 : int.Parse(this.cbU8ChartTopN.Text)), this.cbU8ChartOrderBy.SelectedIndex);
                }
                //this.strU8GraphDatabaseName_All = this.strCurrentDBName;
                this.intPreBizTableNum = (this.cbU8ChartTopN.Text == "ȫ��" ? 2000000 : int.Parse(this.cbU8ChartTopN.Text));
            }
            
        }

        private void cbU8ChartOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chbIncludeU8.Checked)
            {
                if (this.utcU8BizTable.SelectedTab.Key == "ALLU8BIZGRAPH")
                {
                    //this.strU8GraphDatabaseName_All == this.strCurrentDBName && 
                    if (this.intPreBizTableOrder == this.cbU8ChartOrderBy.SelectedIndex)
                    {
                        return;
                    }
                    if (Upgrade.Apps.clsU8ChartData.htU8BizObjTableData.Count > 0)
                    {
                        Upgrade.Apps.clsDBChartShow objChartShow = new clsDBChartShow();
                        objChartShow.DrawU8AllTableGraph(Upgrade.Apps.clsU8ChartData.htU8BizObjTableData, this.chartU8TableCount, 1, (this.cbU8ChartTopN.Text == "ȫ��" ? 2000000 : int.Parse(this.cbU8ChartTopN.Text)), this.cbU8ChartOrderBy.SelectedIndex);
                    }
                    //this.strU8GraphDatabaseName_All = this.strCurrentDBName;
                    this.intPreBizTableOrder = this.cbU8ChartOrderBy.SelectedIndex;
                }
            }
        }




        #endregion


        #region UU��ͼ
        private string strU8UUGraphDatabaseName = "";
        private void utcU8UU_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            if (this.chbIncludeU8UU.Checked)
            {
                if (this.utcU8UU.SelectedTab.Key == "U8UUBIZGRAPH")
                {
                    if (this.strU8UUGraphDatabaseName == this.strCurrentDBName)
                    {
                        return;
                    }
                    if (Upgrade.Apps.clsU8ChartData.htU8UUBizObjTableData.Count > 0)
                    {
                        Upgrade.Apps.clsDBChartShow objChartShow = new clsDBChartShow();

                        objChartShow.DrawU8AllTableGraph(Upgrade.Apps.clsU8ChartData.htU8UUBizObjTableData, this.chartUUTableCount, 0, 20000, 0);
                        objChartShow.DrawU8AllTableSpaceGraph(Upgrade.Apps.clsU8ChartData.htU8UUBizObjTableData, this.chartUUTableSpace, this.getSelectedDBSpace(this.strCurrentDBName));

                        //this.chartUUTableCount.
                    }
                    this.strU8UUGraphDatabaseName = this.strCurrentDBName;
                }            
            }
        }
        #endregion



       



        private double getSelectedDBSpace(string strDatabaseName)
        {
            double dblSelectedDBSpace = 0;
            
            DataView oDV = ((System.Data.DataSet)this.ugDBInfo.DataSource).Tables["DatabaseDetail"].DefaultView;

            try
            {
                oDV.RowFilter = "���ݿ�='" + strDatabaseName.Trim() + "'";
                dblSelectedDBSpace = double.Parse(((string)oDV[0]["�����ռ�(MB)"]));
                this.dblSelectedDBSpaceMB = dblSelectedDBSpace;

                return dblSelectedDBSpace;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PropertyForm o = new PropertyForm();

            o.propertyGrid1.SelectedObject = this.chartU8TableCount;

            o.ShowDialog();

        }

        

       





   


        

        

       
       

        

        

    }
}