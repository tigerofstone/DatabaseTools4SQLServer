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
    public class clsBizDataInfo
    {
        public string strConnection = "";
        public string strInstance = "";
        public string strUser = "";
        public string strPassword = "";
        public bool blnIntegrated = false;

        public clsBizDataInfo()
        {

        }

        private SqlConnection OpenSQLDBConnection(SqlConnection objSQLConn, string strDBName)
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

        private SqlConnection OpenSQLConnection(SqlConnection objSQLConn, string strConnection)
        {
            try
            {
                if (objSQLConn.State != ConnectionState.Open)
                {
                    objSQLConn.ConnectionString = strConnection;
                    objSQLConn.Open();
                }
                return objSQLConn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private int CheckISU8DB(System.Data.SqlClient.SqlCommand objSQLComm, System.Data.SqlClient.SqlDataReader objDR, string strDBName, ref string strU8Version)
        {
            try
            {
                strU8Version = "";
                if (objDR != null && !objDR.IsClosed) objDR.Close();
                objSQLComm.CommandText = @"SELECT cValue FROM AccInformation WHERE cSysID = 'AA' AND cID = 99";
                objDR = objSQLComm.ExecuteReader();
                if (objDR.Read())
                {
                    strU8Version = objDR.GetSqlString(0).ToString().Trim();
                }
                else
                {
                    MessageBox.Show("选中的数据库：" + strDBName.Trim() + "无法判断业务库版本，或不是U8+的业务数据库。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                    //strU8Version = "10.0";
                }

                if (objDR != null && !objDR.IsClosed) objDR.Close();
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
                        MessageBox.Show("选中的数据库：" + strDBName.Trim() + " 不是U8+的业务数据库，或记录版本与数据库结构不一致。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;
                    }
                }
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("选中的数据库：" + strDBName.Trim() + "无法判断业务库版本，或不是U8+的业务数据库。\r\n" + e.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return 0;
            }
            finally
            {
                if (objDR != null)
                {
                    if (!objDR.IsClosed) objDR.Close();
                }
            }
        }

        private int CheckISU8UUDB(System.Data.SqlClient.SqlCommand objSQLComm, System.Data.SqlClient.SqlDataReader objDR, string strDBName, ref string strU8Version)
        {
            try
            {
                strU8Version = "";
                if (objDR != null && !objDR.IsClosed) objDR.Close();
                objSQLComm.CommandText = @"SELECT cValue FROM AccInformation WHERE cSysID = 'AA' AND cID = 99";
                objDR = objSQLComm.ExecuteReader();
                if (objDR.HasRows && objDR.Read())
                {
                    strU8Version = objDR.GetSqlString(0).ToString().Trim();
                }
                else
                {
                    MessageBox.Show("选中的数据库：" + strDBName.Trim() + "无法判断业务库版本，或不是 U8+ UU 的业务数据库。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                if (objDR != null && !objDR.IsClosed) objDR.Close();
                objSQLComm.CommandText = @"SELECT Count(*) FROM sysobjects With(nolock) 
                                           WHERE type = 'U' AND NAME IN ('UTU_NormalMessage', 'UTU_Discussion', 'UTU_User', 'UTU_MessageCenter', 'UTU_Broadcast')";

                objDR = objSQLComm.ExecuteReader();
                if (objDR.Read())
                {
                    if (objDR.GetInt32(0) != 5)
                    {
                        MessageBox.Show("选中的数据库：" + strDBName.Trim() + " 不是 U8+ UU 的业务数据库，或记录版本与数据库结构不一致。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;
                    }
                }
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("选中的数据库：" + strDBName.Trim() + "无法判断业务库版本，或不是 U8+ UU 的业务数据库。\r\n" + e.Message , "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                return 0;
            }
            finally
            {
                if (objDR != null)
                {
                    if (objDR != null && !objDR.IsClosed) objDR.Close();
                }
            }
        }

        private int CheckIsTableExist(System.Data.SqlClient.SqlCommand objSQLComm, string strTableName)
        {
            System.Data.SqlClient.SqlDataReader objDR = null;
            try
            {
                if (objDR != null && !objDR.IsClosed) objDR.Close();
                objSQLComm.CommandText = @"SELECT ID FROM sysobjects With(nolock) WHERE TYPE = 'U' AND NAME = '" + strTableName + "'";
                objDR = objSQLComm.ExecuteReader();
                if (objDR.HasRows &&  objDR.Read())
                {
                    return 1;
                }
                else
                {
                    return -1;
                }                
            }
            finally
            {
                if (objDR != null)
                {
                    if (objDR != null && !objDR.IsClosed) objDR.Close();
                    objDR = null;
                }
            }
        }



        private void SetU8DSTableStruct(DataSet objDSResult, DataTable objDTResult, DataTable objDTResultYearInfo)
        {
            objDTResult.TableName = "ObjectDetail";
            objDTResult.Columns.Add("序号", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("业务对象", Type.GetType("System.String"));
            objDTResult.Columns.Add("主表记录数", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("子表记录数", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("业务最早日期", Type.GetType("System.String"));
            objDTResult.Columns.Add("业务最晚日期", Type.GetType("System.String"));
            objDTResult.Columns.Add("主表数据空间(MB)", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("主表索引空间(MB)", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("子表数据空间(MB)", Type.GetType("System.Int32"));
            objDTResult.Columns.Add("子表索引空间(MB)", Type.GetType("System.Int32"));

            objDTResultYearInfo.TableName = "YearInfo";
            objDTResultYearInfo.Columns.Add("业务对象", Type.GetType("System.String"));
            objDTResultYearInfo.Columns.Add("主表时间年份", Type.GetType("System.Int32"));
            objDTResultYearInfo.Columns.Add("主表记录数", Type.GetType("System.Int32"));
            objDTResultYearInfo.Columns.Add("子表记录数", Type.GetType("System.Int32"));

            objDSResult.Tables.Add(objDTResult); objDSResult.Tables.Add(objDTResultYearInfo);
            objDSResult.Relations.Add("YearInfo", objDTResult.Columns["业务对象"], objDTResultYearInfo.Columns["业务对象"]);
        }

        private void GetTableCountAndSpace(System.Data.SqlClient.SqlConnection objConn, System.Data.SqlClient.SqlCommand objSQLComm, System.Data.SqlClient.SqlDataReader objDR,
                                           string str数据表名, ref int int记录数, ref double dbl数据空间, ref double dbl索引空间)
        {
            if (objDR != null && !objDR.IsClosed) objDR.Close();
            if (str数据表名 != "")
            {                
                objSQLComm.CommandText = @"EXEC SP_SPACEUSED N'" + str数据表名 + "'";
                objDR = objSQLComm.ExecuteReader();

                if (objDR.Read())
                {
                    int记录数 = int.Parse(objDR.GetString(1));
                    dbl数据空间 = double.Parse(objDR.GetString(3).Replace("KB", "")) / 1024;
                    dbl索引空间 = double.Parse(objDR.GetString(4).Replace("KB", "")) / 1024;
                }
                if (objDR != null && !objDR.IsClosed) objDR.Close();
            }
        }

        private void GetTableStartAndEndDate(System.Data.SqlClient.SqlConnection objConn, System.Data.SqlClient.SqlCommand objSQLComm, System.Data.SqlClient.SqlDataReader objDR,
                                           string str数据表名, string str业务时间字段, ref string str业务最早日期, ref string str业务最晚日期)
        {
            if (objDR != null && !objDR.IsClosed) objDR.Close();
            if (str业务时间字段 != "")
            {
                objSQLComm.CommandText = @"SELECT CONVERT(NVARCHAR(30), MIN([" + str业务时间字段 + @"] ), 120)  AS dMinDate, 
                                                  CONVERT(NVARCHAR(30), MAX([" + str业务时间字段 + @"] ), 120) AS dMaxDate 
                                           FROM [" + str数据表名 + @"] WITH(NOLOCK) ";
                objDR = objSQLComm.ExecuteReader();
                if (objDR.Read())
                {
                    str业务最早日期 = objDR.IsDBNull(0) ? "" : objDR.GetString(0); 
                    str业务最晚日期 = objDR.IsDBNull(1) ? "" : objDR.GetString(1);

                    if (str业务最早日期.Length > 10)
                    {
                        str业务最早日期 = System.DateTime.Parse(str业务最早日期).ToString("yyyy-MM-dd");
                        str业务最晚日期 = System.DateTime.Parse(str业务最晚日期).ToString("yyyy-MM-dd");
                    }
                }
                if (!objDR.IsClosed) objDR.Close();
            }
        }

        private int CheckDateType(System.Data.SqlClient.SqlConnection objConn, System.Data.SqlClient.SqlCommand objSQLComm, System.Data.SqlClient.SqlDataReader objDR,
                                           string str数据表名, string str业务时间字段)
        {
            int iResult = 0;

            if (objDR != null && !objDR.IsClosed) objDR.Close();
            objSQLComm.CommandText = @"SELECT C.[NAME] AS CNAME, O.[NAME] AS TNAME FROM SYSCOLUMNS C INNER JOIN SYSOBJECTS O ON C.ID=O.ID " +
                                                  "WHERE O.NAME = '" + str数据表名 + "' " + "AND C.NAME = '" + str业务时间字段 + "' AND C.XTYPE = 61 ";
            objDR = objSQLComm.ExecuteReader();

            if (objDR.Read())
            {
                iResult = 1;
            }
            else
            {
                iResult = -1;
            }

            if (objDR != null && !objDR.IsClosed) objDR.Close();
            return iResult;            
        }

        private System.Data.SqlClient.SqlDataReader GetBizObjectYearCountInfo(System.Data.SqlClient.SqlConnection objConn, System.Data.SqlClient.SqlCommand objSQLComm,
                                           string str主表数据表名, string str子表数据表名, string str主表外键值, string str子表外键值, string str业务时间字段)
        {
            string strSQL = "";
            System.Data.SqlClient.SqlDataReader objDR = null;

            if (this.CheckDateType(objConn, objSQLComm, objDR, str主表数据表名, str业务时间字段) > 0)
            {
                if (str子表数据表名 != "" && str主表外键值 != "" && (str子表外键值 == null || str子表外键值 == ""))
                {
                    strSQL = @"DECLARE @T1 TABLE(iYear INT, iMainCount INT)
                            DECLARE @T2 TABLE(iYear INT, iDetailCount INT)

                            INSERT INTO @T1 (iYear,	iMainCount) 
                            SELECT YEAR([" + str业务时间字段 + @"]) AS iBillYear, ISNULL(Count(*), 0) AS MainCount 
                            FROM [" + str主表数据表名 + @"] R WITH(NOLOCK) 
                            GROUP BY YEAR([" + str业务时间字段 + "]) " + @"

                            INSERT INTO @T2 (iYear,	iDetailCount) 
                            SELECT YEAR(R.[" + str业务时间字段 + @"]) AS iBillYear, ISNULL(Count(*), 0) AS DetailCount
                            FROM [" + str主表数据表名 + @"] R WITH(NOLOCK) LEFT JOIN [" + str子表数据表名 + @"] RS WITH(NOLOCK) " +
                            @"ON R." + str主表外键值 + @" = RS." + str主表外键值 + @"
                            GROUP BY YEAR(R.[" + str业务时间字段 + @"])

                            SELECT ISNULL(A.iYear,0) AS iYear, iMainCount, ISNULL(iDetailCount, 0) AS iDetailCount FROM @T1 A LEFT JOIN @T2 B ON A.iYear = B.iYear ORDER BY A.iYear DESC";
                }
                else if (str子表数据表名 != "" && str主表外键值 != "" && (str子表外键值 != null && str子表外键值 != ""))
                {
                    strSQL = @"DECLARE @T1 TABLE(iYear INT, iMainCount INT)
                            DECLARE @T2 TABLE(iYear INT, iDetailCount INT)

                            INSERT INTO @T1 (iYear,	iMainCount) 
                            SELECT YEAR([" + str业务时间字段 + @"]) AS iBillYear, ISNULL(Count(*), 0) AS MainCount 
                            FROM [" + str主表数据表名 + @"] R WITH(NOLOCK) 
                            GROUP BY YEAR([" + str业务时间字段 + "]) " + @"

                            INSERT INTO @T2 (iYear,	iDetailCount) 
                            SELECT YEAR(R.[" + str业务时间字段 + @"]) AS iBillYear, ISNULL(Count(*), 0) AS DetailCount
                            FROM [" + str主表数据表名 + @"] R WITH(NOLOCK) LEFT JOIN [" + str子表数据表名 + @"] RS WITH(NOLOCK) " +
                                @"ON R." + str主表外键值 + @" = RS." + str子表外键值 + @"
                            GROUP BY YEAR(R.[" + str业务时间字段 + @"])

                            SELECT ISNULL(A.iYear,0) AS iYear, iMainCount, ISNULL(iDetailCount, 0) AS iDetailCount FROM @T1 A LEFT JOIN @T2 B ON A.iYear = B.iYear ORDER BY A.iYear DESC";

                    //if (objXMLEle.Attributes["MainTableName"].Value.ToString().Trim() == "Transvouch")
                    //    strSQL = strSQL;
                }
                else
                {
                    strSQL = @"SELECT ISNULL(YEAR([" + str业务时间字段 + "]), 0) AS iBillYear, " +
                                "ISNULL(Count(*), 0) AS iMainCount, 0 AS iDetailCount " +
                            "FROM [" + str主表数据表名 + "] " +
                            "GROUP BY YEAR([" + str业务时间字段 + "]) " +
                            "ORDER BY YEAR([" + str业务时间字段 + "]) DESC";
                }

                objSQLComm.CommandText = strSQL;
                if (objDR != null && !objDR.IsClosed) objDR.Close();
                objDR = objSQLComm.ExecuteReader();
            }
            return objDR;
        }


        public System.Data.DataSet GetU8UUTableAllInfo(string strDBName, bool bIncludeYearInfo,
                                                     Infragistics.Win.UltraWinProgressBar.UltraProgressBar oUProcessBar)
        {
            System.Data.SqlClient.SqlConnection objConn = new System.Data.SqlClient.SqlConnection();
            DataSet objDSResult = new DataSet();
            DataTable objDTResult = new DataTable(); 
            DataTable objDTResultYearInfo = new DataTable();
            DataTable objDTTables = new DataTable();
            System.Data.SqlClient.SqlDataReader objDR = null;
            System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();
            System.Xml.XmlDocument objXMLDom = new System.Xml.XmlDocument();
            System.Xml.XmlNodeList objXMLNL;
            System.Xml.XmlElement objXMLEle;

            Upgrade.Apps.U8BizData oBizInfo;
            Upgrade.Apps.U8BizYearData oBizYearInfo;

            string sMainTableName = ""; string sDetailTableName = ""; int iBizObjNO = 0;

            string strVersion = "";
            string strMisTable = "";  

            int int主表记录数 = 0; int int子表记录数 = 0;
            double str主表数据空间 = 0; double str主表索引空间 = 0;
            double str子表数据空间 = 0; double str子表索引空间 = 0;
            string str业务最早日期 = ""; string str业务最晚日期 = "";

            int intYear主表时间年份 = 0;
            int intYear主表记录数 = 0; int intYear子表记录数 = 0;

            try
            {
                this.SetU8DSTableStruct(objDSResult, objDTResult, objDTResultYearInfo);  //设置U8数据信息结构

                oUProcessBar.Text = "打开 " + strDBName + " 数据库";

                objConn = this.OpenSQLDBConnection(objConn, strDBName);

                objSQLComm = objConn.CreateCommand();                
                objSQLComm.CommandTimeout = 600;


                oUProcessBar.Text = "判断数据库 " + strDBName + " 是否用友U8+ 数据库";

                if (this.CheckISU8UUDB(objSQLComm, objDR, strDBName, ref strVersion) <= 0)
                {
                    return objDSResult;
                }
                objXMLDom.Load(Application.StartupPath + "\\U8ERPConfig.xml");
                if (double.Parse(strVersion) == 11)
                { 
                    objXMLNL = objXMLDom.SelectNodes("./Config/U8UUBizObject/U8UTUTable[@Version='11.000']/Table"); 
                }
                else if (double.Parse(strVersion) == 11.1)
                {
                    objXMLNL = objXMLDom.SelectNodes("./Config/U8UUBizObject/U8UTUTable[@Version='11.100']/Table");
                }
                else if (double.Parse(strVersion) == 12)
                {
                    objXMLNL = objXMLDom.SelectNodes("./Config/U8UUBizObject/U8UTUTable[@Version='12.000']/Table");
                }
                else
                { 
                    return objDSResult;  
                }
                
                //========== 打开数据表 ============================================================================================================================================================
                oUProcessBar.FillAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                oUProcessBar.Refresh();
                oUProcessBar.Maximum = objXMLNL.Count;
                oUProcessBar.Value = 0;
                oUProcessBar.Text = "打开 " + strDBName.Trim() + " 数据库中主要的用友U8+ UU业务对象数据    [Completed] Of [Range] Completed                  ";

                //清除图形数据
                Upgrade.Apps.clsU8ChartData.htU8UUBizObjTableData.Clear();
                Upgrade.Apps.clsU8ChartData.htU8UUBizObjTableYearData.Clear();

                for (int i = 0; i < objXMLNL.Count; i++)
                {
                    oUProcessBar.Value = i + 1;
                    Application.DoEvents();

                    int主表记录数 = 0; int子表记录数 = 0;
                    str主表数据空间 = 0; str主表索引空间 = 0;
                    str子表数据空间 = 0; str子表索引空间 = 0;
                    str业务最早日期 = ""; str业务最晚日期 = "";

                    intYear主表时间年份 = 0;
                    intYear主表记录数 = 0; intYear子表记录数 = 0;

                    sMainTableName = ""; sDetailTableName = "";

                    objXMLEle = (System.Xml.XmlElement)objXMLNL[i];

                    #region 判断数据表存在
                    //=================================//

                    if (objXMLEle.Attributes["MainTableName"].Value.ToString().Trim()=="UTU_Config")
                    {
                        objXMLEle = (System.Xml.XmlElement)objXMLNL[i];
                    }

                    if (CheckIsTableExist(objSQLComm, objXMLEle.Attributes["MainTableName"].Value.ToString().Trim()) < 0)
                    {
                        strMisTable = strMisTable + "    业务对象：" + objXMLEle.Attributes["Object"].Value.ToString().Trim() +
                                                    "    主表 [" + objXMLEle.Attributes["MainTableName"].Value.ToString() + "]\r\n";
                        continue;
                    }                    

                    if (objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() != "")
                    {
                        if (CheckIsTableExist(objSQLComm, objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim()) < 0)
                        {
                            strMisTable = strMisTable + "    业务对象：" + objXMLEle.Attributes["Object"].Value.ToString().Trim() +
                                                        "    子表 [" + objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim() + "]\r\n";
                            continue;
                        }
                    }
                    //=================================//
                    #endregion

                    #region  查询数据表信息
                    if ((objXMLEle.Attributes["IsChieldInfo"] != null ? objXMLEle.Attributes["IsChieldInfo"].Value.ToString().Trim() : "0") == "0")
                    {
                        this.GetTableCountAndSpace(objConn, objSQLComm, objDR, objXMLEle.Attributes["MainTableName"].Value.ToString().Trim(),
                                                   ref int主表记录数, ref str主表数据空间, ref str主表索引空间);

                        this.GetTableCountAndSpace(objConn, objSQLComm, objDR, objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim(),
                                                   ref int子表记录数, ref str子表数据空间, ref str子表索引空间);
                    }
                    else
                    {
                        this.GetTableCountAndSpace(objConn, objSQLComm, objDR, objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim(),
                                                   ref int主表记录数, ref str主表数据空间, ref str主表索引空间); 
                    }

                    this.GetTableStartAndEndDate(objConn, objSQLComm, objDR, objXMLEle.Attributes["MainTableName"].Value.ToString().Trim(),
                                                                         objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim(),
                                             ref str业务最早日期, ref str业务最晚日期);  
                                      
                    #endregion

                    objDTResult.Rows.Add(new object[] { i + 1, objXMLEle.Attributes["Object"].Value.ToString(), 
                                                        int主表记录数, int子表记录数, str业务最早日期, str业务最晚日期,
                                                        str主表数据空间, str主表索引空间, str子表数据空间, str子表索引空间
                                                      });
                    #region 补充图形数据
                    oBizInfo = new U8BizData();
                    oBizInfo.sU8TableName = sMainTableName;
                    oBizInfo.sU8ObjectName = objXMLEle.Attributes["Object"].Value.ToString();
                    oBizInfo.iU8TableCount = int主表记录数;
                    oBizInfo.dblU8TableSpace = str主表数据空间 + str主表索引空间;
                    iBizObjNO = iBizObjNO + 1;
                    Upgrade.Apps.clsU8ChartData.htU8UUBizObjTableData.Add(oBizInfo);
                    if (sDetailTableName != "")
                    {
                        oBizInfo = new U8BizData();
                        oBizInfo.sU8TableName = sDetailTableName;
                        oBizInfo.sU8ObjectName = objXMLEle.Attributes["Object"].Value.ToString() + "子表";
                        oBizInfo.iU8TableCount = int子表记录数;
                        oBizInfo.dblU8TableSpace = str子表数据空间 + str子表索引空间;
                        iBizObjNO = iBizObjNO + 1;
                        Upgrade.Apps.clsU8ChartData.htU8UUBizObjTableData.Add(oBizInfo);
                    }
                    #endregion

                    Application.DoEvents();

                    #region  查询主表各年数据量
                    //--------------- 查询主表各年数据量 -------------------------------------------------------------------------------------------------------------
                    if (bIncludeYearInfo && objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim() != "")
                    {

                        objDR = this.GetBizObjectYearCountInfo(objConn, objSQLComm,
                                                               objXMLEle.Attributes["MainTableName"].Value.ToString().Trim(),
                                                               objXMLEle.Attributes["DetailTableName"].Value.ToString().Trim(),
                                                               objXMLEle.Attributes["KeyName"].Value.ToString().Trim(),
                                                               (objXMLEle.Attributes["DetailForeignKeyName"] != null ? objXMLEle.Attributes["DetailForeignKeyName"].Value.ToString().Trim() : ""),
                                                               objXMLEle.Attributes["ObjectOperationTime"].Value.ToString().Trim());
                            #region 补充图形数据
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
                                        intYear主表时间年份 = objDR.GetInt32(0);
                                        if ((objXMLEle.Attributes["IsChieldInfo"] != null ? objXMLEle.Attributes["IsChieldInfo"].Value.ToString().Trim() : "0") == "0")
                                        {
                                            intYear主表记录数 = objDR.GetInt32(1); intYear子表记录数 = objDR.GetInt32(2);
                                        }
                                        else
                                        {
                                            intYear主表记录数 = objDR.GetInt32(2); intYear子表记录数 = 0;
                                        }
                                        objDTResultYearInfo.Rows.Add(new object[] { objXMLEle.Attributes["Object"].Value.ToString(), intYear主表时间年份, intYear主表记录数, intYear子表记录数 });

                                        oBizYearInfo.oU8BizYearCount.Add(intYear主表时间年份, new int[2] { intYear主表记录数, intYear子表记录数 });
                                    }
                                    catch (Exception Ex)
                                    {
                                        strMisTable = strMisTable + "    业务对象：" + objXMLEle.Attributes["Object"].Value.ToString().Trim() +
                                                                    " 读取年度数据有错误：" + Ex.Message + "；\r\n";
                                        continue;
                                    }
                                }
                            }

                            Upgrade.Apps.clsU8ChartData.htU8UUBizObjTableYearData.Add(oBizYearInfo);

                        }
                        if (!objDR.IsClosed) objDR.Close();                    
                    //----------------------------------------------------------------------------------------------------------------------------------------------
                    #endregion
                    
                    Application.DoEvents();
                }
                oUProcessBar.Text = "完成 " + strDBName.Trim() + " 数据库中主要的用友U8+ UU 业务对象数据查询";
                if (strMisTable != "")
                {
                    objXMLEle = (System.Xml.XmlElement)objXMLDom.SelectSingleNode("./Config/U8UUBizObject/U8UTUTable");

                    if (objXMLEle.Attributes["Version"] != null)
                        MessageBox.Show("以下数据表在数据库中不存在：\r\n" + strMisTable.Substring(0, strMisTable.Length - 2) + "。\r\n" +
                                        "可能由于与配置文件中 U8+ UU 版本：" + objXMLEle.Attributes["Version"].Value.ToString() + " 不一致。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("以下数据表在数据库中不存在：\r\n" + strMisTable.Substring(0, strMisTable.Length - 2) + "。\r\n" +
                                        "可能由于与配置文件中 U8+ UU 版本不一致。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
