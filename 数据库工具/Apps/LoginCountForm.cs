using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Shared;


  
namespace Upgrade.Apps
{
    public partial class LoginCountForm : Form
    {
        public string strConnection = "";
        public string strInstance = "";
        public System.Data.SqlClient.SqlConnection objSQLConnect = new System.Data.SqlClient.SqlConnection();

        private System.Data.DataSet objLoginDT = new DataSet();

        private int intRangeMax = 100;

        public LoginCountForm()
        {
            InitializeComponent();

            this.SetUltraGridStyle_LoginAll();
            this.SetUltraGridStyle();
        }

        public LoginCountForm(string strConn, string strInst)
        {
            InitializeComponent();

            this.SetUltraGridStyle();
            this.SetUltraGridStyle_LoginAll();

            strConnection = strConn;
            strInstance = strInst;
        }



        private void LoginCountForm_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + "    制定服务器：" + this.strInstance;            

            this.LoginCountForm_Resize(sender, e);
            this.SetUltraGridStyle();
            this.SetUltraGridStyle_LoginAll();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            System.Data.DataSet objDS = null;
            System.Data.DataTable objDTDetail = null;

            objDS = this.getLoginCount();
            this.ugd_LoginCount.DataSource = objDS.Tables[0];
            this.label1.Text = "系统登录客户端数： " + objDS.Tables[0].Rows.Count.ToString("#,##0") + "   ";

            if (this.cbIncDetail.Checked)
            {
                objDTDetail = this.getLoginDetail();
                this.ugd_LoginDetail.DataSource = objDTDetail;
                this.label1.Text = this.label1.Text + "SQL Server 进程明细：  " + objDTDetail.Rows.Count.ToString("#,##0") + "。";
            }
            else
            {
                this.ugd_LoginDetail.DataSource = null;
            }

            if (this.cbDisplayGraph.Checked)
            {
                this.ucClentConn.Data.DataSource = objDS.Tables[1];
                this.ucClentConn.DataBind();

                this.ucClentConn.Axis.Y.RangeType = (Infragistics.UltraChart.Shared.Styles.AxisRangeType)System.Enum.Parse(typeof(Infragistics.UltraChart.Shared.Styles.AxisRangeType), "Custom");
                this.ucClentConn.Axis.Y.RangeMin = 0;
                this.ucClentConn.Axis.Y.RangeMax = this.intRangeMax + this.intRangeMax / 10 + 1;
                this.ucClentConn.ColumnChart.ColumnSpacing = 2;
            }

            this.LoginDataSet();
            this.ugd_LoginAll.DataSource = this.objLoginDT;
            this.ugd_LoginAll.DisplayLayout.Bands[0].Columns[1].Width = 200;
            this.ugd_LoginAll.DisplayLayout.Bands[0].Columns[2].Width = 200;
            this.ugd_LoginAll.DisplayLayout.Bands[1].Columns[2].Width = 200;
            this.ugd_LoginAll.DisplayLayout.Bands[1].Columns[3].Width = 200;
            this.ugd_LoginAll.DisplayLayout.Bands[1].Columns[6].Width = 200;
        }


        private Infragistics.Win.UltraWinGrid.UltraGridBand SetUltraGridStyle_Column()
        {
            Infragistics.Win.UltraWinGrid.UltraGridBand objDataBand = new Infragistics.Win.UltraWinGrid.UltraGridBand("LoginCount", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn UGColumnComputer = new Infragistics.Win.UltraWinGrid.UltraGridColumn("客户端名称");
            Infragistics.Win.UltraWinGrid.UltraGridColumn UGColumnLLogin = new Infragistics.Win.UltraWinGrid.UltraGridColumn("最后操作时间");
            Infragistics.Win.UltraWinGrid.UltraGridColumn UGColumnLLoginStart = new Infragistics.Win.UltraWinGrid.UltraGridColumn("最早操作时间");

            Infragistics.Win.Appearance objAppearanceHead = new Infragistics.Win.Appearance();
            objAppearanceHead.BackColor = System.Drawing.Color.White;
            objAppearanceHead.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(242)))), ((int)(((byte)(199)))));
            objAppearanceHead.BackGradientStyle = GradientStyle.BackwardDiagonal;
            this.ugd_LoginCount.DisplayLayout.Appearance = objAppearanceHead;

            Infragistics.Win.Appearance objAppearanceCell = new Infragistics.Win.Appearance();
            //objAppearanceCell.BackColor = System.Drawing.Color.Green;
            objAppearanceCell.TextTrimming = TextTrimming.EllipsisWord;
            objAppearanceCell.TextHAlign = HAlign.Center;
            objAppearanceCell.TextVAlign = VAlign.Middle;

            Infragistics.Win.Appearance objAppearCell_1 = new Infragistics.Win.Appearance();
            //objAppearCell_1.BackColor = System.Drawing.Color.Wheat;
            objAppearCell_1.TextTrimming = TextTrimming.EllipsisWord;
            objAppearCell_1.TextHAlign = HAlign.Left;
            objAppearCell_1.TextVAlign = VAlign.Middle;

            UGColumnComputer.CellAppearance = objAppearCell_1;
            UGColumnComputer.CellMultiLine = DefaultableBoolean.True;
            UGColumnComputer.Header.Appearance = objAppearanceCell;
            UGColumnComputer.Header.VisiblePosition = 1;
            UGColumnComputer.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(150, 10);

            Infragistics.Win.Appearance objAppearCell_2 = new Infragistics.Win.Appearance();
            //objAppearCell_2.BackColor = System.Drawing.Color.Wheat;
            objAppearCell_2.TextTrimming = TextTrimming.EllipsisWord;
            objAppearCell_2.TextHAlign = HAlign.Left;
            objAppearCell_2.TextVAlign = VAlign.Middle;

            UGColumnLLogin.CellAppearance = objAppearCell_2;
            UGColumnLLogin.Format = "yyyy-MM-dd hh:mm:ss:fff";
            UGColumnLLogin.CellMultiLine = DefaultableBoolean.True;
            UGColumnLLogin.Header.Appearance = objAppearanceCell;
            UGColumnLLogin.Header.VisiblePosition = 1;
            UGColumnLLogin.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(200, 10);
            UGColumnLLogin.Width = 100;

            UGColumnLLoginStart.CellAppearance = objAppearCell_2;
            UGColumnLLoginStart.Format = "yyyy-MM-dd hh:mm:ss:fff";
            UGColumnLLoginStart.CellMultiLine = DefaultableBoolean.True;
            UGColumnLLoginStart.Header.Appearance = objAppearanceCell;
            UGColumnLLoginStart.Header.VisiblePosition = 1;
            UGColumnLLoginStart.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(200, 10);
            UGColumnLLogin.Width = 100;

            objDataBand.Columns.AddRange(new object[] { UGColumnComputer, UGColumnLLogin, UGColumnLLoginStart });

            return objDataBand;
        }

        private void SetUltraGridStyle()
        {
            Infragistics.Win.UltraWinGrid.UltraGridBand objDataBand = this.SetUltraGridStyle_Column();

            this.ugd_LoginCount.Text = "系统登陆人数明细";
            //this.ugd_LoginCount.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.ugd_LoginCount.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
            this.ugd_LoginCount.DisplayLayout.BorderStyleCaption = UIElementBorderStyle.Solid;
            this.ugd_LoginCount.DisplayLayout.Override.RowAlternateAppearance.BackColor = System.Drawing.Color.Honeydew;
            this.ugd_LoginCount.DisplayLayout.Override.RowAlternateAppearance.ForeColor = System.Drawing.Color.Black;
            this.ugd_LoginCount.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(170, 184, 131);
            this.ugd_LoginCount.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnRowChange;
            this.ugd_LoginCount.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.None;
            this.ugd_LoginCount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.Vertical;
            this.ugd_LoginCount.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
            this.ugd_LoginCount.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
            this.ugd_LoginCount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.ugd_LoginCount.DisplayLayout.BandsSerializer.Add(objDataBand);            
        }

        private void SetUltraGridStyle_LoginAll()
        {
            Infragistics.Win.UltraWinGrid.UltraGridBand objDataBand = this.SetUltraGridStyle_Column();

            this.ugd_LoginAll.Text = "系统登陆人数汇总";
            //this.ugd_LoginAll.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
            this.ugd_LoginAll.DisplayLayout.CaptionVisible = DefaultableBoolean.False;
            this.ugd_LoginAll.DisplayLayout.BorderStyleCaption = UIElementBorderStyle.Solid;
            this.ugd_LoginAll.DisplayLayout.Override.RowAlternateAppearance.BackColor = System.Drawing.Color.Honeydew;
            this.ugd_LoginAll.DisplayLayout.Override.RowAlternateAppearance.ForeColor = System.Drawing.Color.Black;
            this.ugd_LoginAll.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(170, 184, 131);
            this.ugd_LoginAll.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnRowChange;
            this.ugd_LoginAll.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.None;
            this.ugd_LoginAll.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.Vertical;
            this.ugd_LoginAll.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
            this.ugd_LoginAll.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
            this.ugd_LoginAll.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;

            this.ugd_LoginAll.DisplayLayout.BandsSerializer.Add(objDataBand);                 
        }


        private void LoginDataSet()
        {
            DataSet objDSLogin = null;
            DataTable objDTLoginDetail = null;
            DataTable objDTLoginCount = null;

            this.objLoginDT = null;
            this.objLoginDT = new DataSet();

            objDSLogin = this.getLoginCount();

            objDTLoginDetail = this.getLoginDetail();

            objDTLoginCount = objDSLogin.Tables[0].Copy();
            objDTLoginCount.TableName = "LoginCount";
            this.objLoginDT.Tables.Add(objDTLoginCount);
            this.objLoginDT.Tables.Add(objDTLoginDetail);

            this.objLoginDT.Relations.Add("ClientName", 
                                           this.objLoginDT.Tables["LoginCount"].Columns["客户端名称"], 
                                           this.objLoginDT.Tables["LoginCountDetail"].Columns["客户端名称"]);
        }

        private System.Data.DataSet getLoginCount()
        {
            try
            {
                System.Data.DataSet objDS = new DataSet();
                System.Data.SqlClient.SqlDataAdapter objSQLDA = new System.Data.SqlClient.SqlDataAdapter();
                System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();

                string strMyComputer = System.Net.Dns.GetHostName().Trim();

                if (this.objSQLConnect.State != ConnectionState.Open)
                {
                    this.objSQLConnect.ConnectionString = strConnection;
                    this.objSQLConnect.Open();
                }

                objSQLComm = this.objSQLConnect.CreateCommand();
                objSQLComm.CommandTimeout = 600;
                objSQLComm.CommandText = @"DECLARE @T TABLE(客户端名称 NVARCHAR(200), 最早操作时间 DATETIME, 最后操作时间 DATETIME, 明细进程和 INT)
                                           IF EXISTS(SELECT 1 WHERE @@VERSION LIKE 'Microsoft SQL Server  2005%')
                                                INSERT INTO @T (客户端名称, 最早操作时间, 最后操作时间, 明细进程和) 
                                                SELECT hostname AS 客户端名称, MIN(last_batch) AS 最早操作时间, MAX(last_batch) AS 最后操作时间, COUNT(*) AS 明细进程和 
                                                FROM MASTER.SYS.SYSPROCESSES WITH (NOLOCK)
                                                WHERE HOSTNAME <> '' " +  (!this.cbIncludeMe.Checked ? @"AND HOSTNAME <> '" + strMyComputer + "' " : " ") + @" --AND HOSTNAME <> @@SERVERNAME
                                                GROUP BY HOSTNAME
                                                ORDER BY  HOSTNAME
                                           ELSE
                                                INSERT INTO @T (客户端名称, 最早操作时间, 最后操作时间, 明细进程和) 
                                                SELECT hostname AS 客户端名称, MIN(last_batch) AS 最早操作时间, MAX(last_batch) AS 最后操作时间, COUNT(*) AS 明细进程和 
                                                FROM MASTER..SYSPROCESSES WITH (NOLOCK)
                                                WHERE HOSTNAME <> ''  " + (!this.cbIncludeMe.Checked ? @"AND HOSTNAME <> '" + strMyComputer + "' " : " ") + @" --AND HOSTNAME <> @@SERVERNAME
                                                GROUP BY HOSTNAME
                                                ORDER BY  HOSTNAME
                                                
                                            SELECT 客户端名称, 最早操作时间, 最后操作时间, 明细进程和 FROM @T LoginCount ORDER BY 客户端名称
                                            SELECT 客户端名称 AS Client, 明细进程和 AS PCount FROM @T ClientLoginCount ORDER BY 客户端名称 
                                            SELECT MAX(明细进程和) FROM @T ";
                                           
                objSQLComm.ExecuteNonQuery();

                objDS.Clear();
                objSQLDA.SelectCommand = objSQLComm;
                objSQLDA.Fill(objDS);

                this.intRangeMax = (int)objDS.Tables[2].Rows[0][0];
                return objDS;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private System.Data.DataTable getLoginDetail()
        {
            try
            {
                System.Data.DataTable objDTSQL = new DataTable();
                System.Data.SqlClient.SqlDataAdapter objSQLDA = new System.Data.SqlClient.SqlDataAdapter();
                System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();

                string strMyComputer = System.Net.Dns.GetHostName().Trim();

                if (this.objSQLConnect.State != ConnectionState.Open)
                {
                    this.objSQLConnect.ConnectionString = strConnection;
                    this.objSQLConnect.Open();
                }

                objSQLComm = this.objSQLConnect.CreateCommand();
                objSQLComm.CommandTimeout = 600;
                objSQLComm.CommandText = @"IF EXISTS(SELECT 1 WHERE @@VERSION LIKE 'Microsoft SQL Server  2005%')
                                                SELECT HOSTNAME AS 客户端名称,spid AS 登陆进程ID, CONVERT(NVARCHAR(30), login_time, 121) AS 登陆时间, CONVERT(NVARCHAR(30), last_batch, 121) AS 最后操作时间, status AS 状态, blocked AS 阻塞进程, program_name AS 执行程序
                                                FROM MASTER.SYS.SYSPROCESSES WITH (NOLOCK)
                                                WHERE HOSTNAME <> '' " + (!this.cbIncludeMe.Checked ? @"AND HOSTNAME <> '" + strMyComputer + "' " : " ") + @" --AND HOSTNAME <> @@SERVERNAME
                                                ORDER BY  HOSTNAME, 最后操作时间
                                           ELSE
                                                SELECT HOSTNAME AS 客户端名称,spid AS 登陆进程ID, CONVERT(NVARCHAR(30), login_time, 121) AS 登陆时间, CONVERT(NVARCHAR(30), last_batch, 121)  AS 最后操作时间, status AS 状态, blocked AS 阻塞进程, program_name AS 执行程序
                                                FROM MASTER..SYSPROCESSES WITH (NOLOCK)
                                                WHERE HOSTNAME <> '' " + (!this.cbIncludeMe.Checked ? @"AND HOSTNAME <> '" + strMyComputer + "' " : " ") + @" --AND HOSTNAME <> @@SERVERNAME
                                                ORDER BY  HOSTNAME, 最后操作时间	";
                objSQLComm.ExecuteNonQuery();

                objDTSQL.Clear();
                objDTSQL.TableName = "LoginCountDetail";
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

        private void LoginCountForm_Resize(object sender, EventArgs e)
        {
            this.utcLogonConnection.Left = 0;
            this.utcLogonConnection.Top = 49;

            this.utcLogonConnection.Width = this.Width - 7;
            this.utcLogonConnection.Height = this.Height - 76;
        }

        

    }
}