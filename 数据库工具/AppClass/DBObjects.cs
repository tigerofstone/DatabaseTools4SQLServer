using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Upgrade.AppClass
{
    public class DBDatabases : AppClass.DatabaseObjects
    {
        public DBDatabases()
        {
        }

        public override System.Data.DataTable getDatabaseObjects(System.Data.SqlClient.SqlConnection objConn)
        {
            try
            {
                AppClass.PublicDBCls objPDB = new AppClass.PublicDBCls();

                return objPDB.getDataTable(objConn, "SELECT [NAME], DBID AS id FROM MASTER..SYSDATABASES  WITH(NOLOCK) ORDER BY [NAME]");
            }
            catch (System.Exception e)
            {
                throw (e);
            }
        }

        public System.Data.DataTable getUserDatabase(System.Data.SqlClient.SqlConnection objConn)
        {
            try
            {
                AppClass.PublicDBCls objPDB = new AppClass.PublicDBCls();

                return objPDB.getDataTable(objConn, @"SELECT [NAME], DBID AS id FROM MASTER..SYSDATABASES WITH(NOLOCK) 
                                                      WHERE NOT [NAME] IN ('master',  'model', 'msdb', 'tempdb','ReportServer', 'ReportServerTempDB') 
                                                      ORDER BY [NAME]");
            }
            catch (System.Exception e)
            {
                throw (e);
            }
        }

        public System.Data.DataTable getDatabaseObjects(string strConnection)
        {
            
            System.Data.SqlClient.SqlConnection objSQLConnect = null;
            try
            {
                AppClass.PublicDBCls objPDB = new AppClass.PublicDBCls();
                objSQLConnect = objPDB.getSQLConnection(strConnection);
                return objPDB.getDataTable(objSQLConnect, "SELECT [NAME], DBID AS id FROM MASTER..SYSDATABASES  WITH(NOLOCK) ORDER BY [NAME]");
            }
            catch (System.Exception e)
            {
                throw (e);
            }
            finally
            {
                if(objSQLConnect != null && objSQLConnect.State == System.Data.ConnectionState.Open)
                {
                    objSQLConnect.Close();
                    objSQLConnect.Dispose();
                    objSQLConnect = null;
                }
            }
        }

        public System.Data.DataTable getUserDatabaseFileInfo(System.Data.SqlClient.SqlConnection objConn, string sDatabaseName)
        {
            try
            {
                AppClass.PublicDBCls objPDB = new AppClass.PublicDBCls();

                return objPDB.getDataTable(objConn,
                                            @"SELECT [FILE_ID], NAME AS Logic_Name, TYPE, 
                                                      (CASE TYPE WHEN 0 THEN '行数据' WHEN 1 THEN '日志' WHEN 2 THEN '流文件'WHEN 4 THEN '全文检索数据' WHEN 3 THEN '其他数据' END) AS Type_Desc,  
                                                      CONVERT(FLOAT, CONVERT(BIGINT, [size]) * 8)/1024.0 AS [Size], Physical_Name
                                              FROM " + sDatabaseName + ".sys.database_files WITH(NOLOCK)");
            }
            catch (System.Exception e)
            {
                throw (e);
            }
        }


        public string getSQLServerVersion(System.Data.SqlClient.SqlConnection objSQLConnect)
        {
            System.Data.DataTable objDT = new System.Data.DataTable();
            System.Data.SqlClient.SqlCommand objComm = null;
            System.Data.SqlClient.SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();

            try
            {
                string strScript = "";


                objComm = objSQLConnect.CreateCommand();
                objComm.CommandTimeout = 1200;

                strScript = @"SELECT @@VERSION AS SQLVERSION";
                objComm.CommandText = strScript;
                objComm.ExecuteNonQuery();
                objDA.SelectCommand = objComm;
                objDA.Fill(objDT);

                if (objDT.Rows[0][0].ToString().IndexOf("Microsoft SQL Server 2005", 0) >= 0)
                {
                    return "2005";
                }
                else if (objDT.Rows[0][0].ToString().IndexOf("Microsoft SQL Server 2008 R2", 0) >= 0)
                {
                    return "2008 R2";
                }
                else if (objDT.Rows[0][0].ToString().IndexOf("Microsoft SQL Server 2008", 0) >= 0)
                {
                    return "2008";
                }
                else if (objDT.Rows[0][0].ToString().IndexOf("Microsoft SQL Server 2012", 0) >= 0)
                {
                    return "2012";
                }
                else if (objDT.Rows[0][0].ToString().IndexOf("Microsoft SQL Server 2014", 0) >= 0)
                {
                    return "2014";
                }
                else //SQL 2000 的处理
                {
                    return "2000";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objDT != null) objDT.Dispose();
                if (objComm != null) objComm.Dispose();
                if (objDA != null) objDA.Dispose();
            }
        }

        public void ShrinkDatabaseFile(string strInstance, string sDatabaseName, string strUserName, string strPassWord, bool bIntergrated,
                                       string sVersion, string sDatabaseLogicName, int iFileType, int iShrinkLogFile2Percent, int iShrinkDataFile2Percent, int iLogSpace, int iDataSpace)
        {
            System.Data.SqlClient.SqlConnection objConn = null;
            try
            {
                Upgrade.AppClass.PublicDBCls objPDB = new PublicDBCls();

                objConn = objPDB.getSQLConnection(strInstance, sDatabaseName, strUserName, strPassWord, bIntergrated);
                this.ShrinkDatabaseFile(objConn, sVersion, sDatabaseName, sDatabaseLogicName, iFileType, iShrinkLogFile2Percent, iShrinkDataFile2Percent,  iLogSpace, iDataSpace);
            }
            catch (System.Exception e)
            {
                throw (e);
            }
            finally
            {
                if (objConn != null)
                {
                    objConn.Close();
                    objConn = null;
                }                
            }
        }

        public void ShrinkDatabaseFile(System.Data.SqlClient.SqlConnection objConn, string sVersion,
                                       string sDatabaseName, string sDatabaseLogicName, int iFileType, 
                                       int iLogFile2Percent, int iShrinkDataFile2Percent, int iLogSpace, int iDataSpace)
        {
            System.Data.DataTable objDT = new System.Data.DataTable();
            DataSet objDSSQL = new DataSet();
            System.Data.SqlClient.SqlCommand objComm = null;
            System.Data.SqlClient.SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();
            string strDBRECOVERY_MODEL = "FULL";
            
            try
            {
                objComm = objConn.CreateCommand();
                objComm.CommandTimeout = 28800;

                objComm.CommandText = @"USE " + sDatabaseName;
                objComm.ExecuteNonQuery();

                if (iFileType == 1)
                {
                    if (sVersion == "2008")
                    {
                        objComm.CommandText = @"SELECT RECOVERY_MODEL, (CASE RECOVERY_MODEL WHEN 1 THEN 'FULL' WHEN 3 THEN 'SIMPLE' END) AS RECOVERY_MODEL_DESC 
                                            FROM " + sDatabaseName + ".[SYS].[DATABASES] WHERE [NAME] = DB_NAME()";
                        objComm.ExecuteNonQuery();
                        objDA.SelectCommand = objComm;
                        objDA.Fill(objDT);
                        strDBRECOVERY_MODEL = objDT.Rows[0][1].ToString();

                        if (sVersion == "2008" && strDBRECOVERY_MODEL == "FULL")
                        {
                            objComm.CommandText = @"ALTER DATABASE [" + sDatabaseName + "] SET RECOVERY SIMPLE WITH NO_WAIT";
                            objComm.ExecuteNonQuery();
                        }
                    }

                    objComm.CommandText = @"DBCC SHRINKFILE(" + sDatabaseLogicName + ", " + iLogSpace.ToString() + ", TRUNCATEONLY)";
                    objComm.ExecuteNonQuery();
                    objComm.CommandText = @"DBCC SHRINKFILE(" + sDatabaseLogicName + ", " + iLogSpace.ToString() + ")";
                    objComm.ExecuteNonQuery();

                    if (sVersion == "2008" && strDBRECOVERY_MODEL == "FULL")
                    {
                        objComm.CommandText = @"ALTER DATABASE [" + sDatabaseName + "] SET RECOVERY FULL WITH NO_WAIT";
                        objComm.ExecuteNonQuery();
                    }

                    this.SetDatabaseFileSize(objConn, objComm, objDA, sDatabaseName, sDatabaseLogicName, "LOG", iLogFile2Percent);                    
                }
                else if (iFileType == 0)
                {
                    objComm.CommandText = @"DBCC SHRINKFILE(" + sDatabaseLogicName + ", " + iDataSpace.ToString() + ")";
                    objComm.CommandTimeout = 28800;
                    objComm.ExecuteNonQuery();

                    this.SetDatabaseFileSize(objConn, objComm, objDA, sDatabaseName, sDatabaseLogicName, "DATA", iLogFile2Percent);  
                }
            }
            catch (System.Exception e)
            {
                throw (e);
            }
            finally
            {
                if (objDT != null) objDT.Dispose();
                if (objComm != null) objComm.Dispose();
                if (objDA != null) objDA.Dispose();

                objDT = null;
                objComm = null;
                objDA = null;
            }
        }

        public void SetDatabaseFileSize(System.Data.SqlClient.SqlConnection objConn, System.Data.SqlClient.SqlCommand objComm, System.Data.SqlClient.SqlDataAdapter objDA,
                                        string sDatabaseName, string sDatabaseLogicName, string sFileType, int iFile2IncreasePercent)
        {
            try
            {
                System.Data.DataTable objDT = new System.Data.DataTable();
                double dblSpace = 0;

                objDT.Clear();
                objComm.CommandText = @"SELECT SIZE * 8 /1024 AS TABLESPACE,  * FROM SYS.SYSFILES WHERE [name] = '" + sDatabaseLogicName + "'";
                objComm.ExecuteNonQuery();
                objDA.SelectCommand = objComm;
                objDA.Fill(objDT);
                dblSpace = double.Parse(((int)objDT.Rows[0]["TABLESPACE"]).ToString()) * (double)(1.0 + iFile2IncreasePercent / 100.0) + 10;

                objComm.CommandText = @"ALTER DATABASE [" + sDatabaseName + @"] MODIFY FILE ( NAME = N'" + sDatabaseLogicName + "', SIZE = " + dblSpace.ToString("###") + "MB )";
                objComm.ExecuteNonQuery();

                objDT = null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        





    }


    public class DBViews : AppClass.DatabaseObjects 
    {
        public DBViews()
        {
        }

        public override System.Data.DataTable getDatabaseObjects(System.Data.SqlClient.SqlConnection objConn)
        {
            try 
            {
                AppClass.PublicDBCls objPDB = new AppClass.PublicDBCls();

                return objPDB.getDataTable(objConn, "SELECT name, id FROM SYSOBJECTS WITH(NOLOCK) WHERE TYPE = 'V' AND  status >= 0 Order By name");
            }
            catch (System.Exception e)
            {
                throw (e);
            }
        }
    }


    public class DBFunctions : AppClass.DatabaseObjects
    {
        public DBFunctions()
        {
        }

        public override System.Data.DataTable getDatabaseObjects(System.Data.SqlClient.SqlConnection objConn)
        {
            try
            {
                AppClass.PublicDBCls objPDB = new AppClass.PublicDBCls();

                return objPDB.getDataTable(objConn, "SELECT name, id FROM SYSOBJECTS WITH(NOLOCK) WHERE (TYPE = 'FN' OR TYPE = 'IF' OR TYPE = 'TF') AND  status >= 0 Order By name");
            }
            catch (System.Exception e)
            {
                throw (e);
            }
        }
    }


    public class DBProcedures : AppClass.DatabaseObjects
    {
        public DBProcedures()
        {
        }

        public override System.Data.DataTable getDatabaseObjects(System.Data.SqlClient.SqlConnection objConn)
        {
            try
            {
                AppClass.PublicDBCls objPDB = new AppClass.PublicDBCls();

                return objPDB.getDataTable(objConn, "SELECT name, id FROM SYSOBJECTS WITH(NOLOCK) WHERE (TYPE = 'P' OR TYPE = 'X') AND  status >= 0 Order By name");
            }
            catch (System.Exception e)
            {
                throw (e);
            }
        }
    }

    public struct DBSingleFileInfo
    {
        public string sDatabaseName;
        public int iFileID;
        public string sLogicName;
        public int iFileType;
        public string sFileTypeDesc;
        public string sPhysicalName;
        public string sState;
        public string sStatedesc;
        public double dFileSize;
        public bool bSelect;
    }

}
