using System;

namespace Upgrade.AppClass
{
	/// <summary>
	/// MakeSQLScriptCls 的摘要说明。
	/// </summary>
	public class MakeSQLScriptCls
	{
		public MakeSQLScriptCls()
		{
			
		}

		//===========================================================================================================
		public string getLastVerNumber(System.Data.OleDb.OleDbConnection objSQLConnect)
		{
			try
			{
				string strVerNumber = "";
				//System.Data.OleDb.OleDbConnection objSQLConnect = this.getConnect("", 1);
				System.Data.DataTable objDT = new System.Data.DataTable();
				System.Data.OleDb.OleDbCommand objComm;
				System.Data.OleDb.OleDbDataAdapter objDA = new System.Data.OleDb.OleDbDataAdapter();

				objComm = objSQLConnect.CreateCommand();
				objComm.CommandTimeout = 600;
				objComm.CommandText = "SELECT fintVerNumber,fchrVersion,fchrName  FROM U8DRP_Template.dbo.VerSet " +
                                      "WHERE fintVerNumber = (SELECT max(fintVerNumber) FROM U8DRP_Template.dbo.VerSet)";
				objComm.ExecuteNonQuery();

				objDA.SelectCommand = objComm;
				objDA.Fill(objDT);

				if(objDT.Rows.Count != 0)
				{
					strVerNumber = objDT.Rows[0]["fintVerNumber"].ToString();
				}
				return strVerNumber;
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}


		public System.Data.DataTable getLastVerNumberAll(System.Data.OleDb.OleDbConnection objSQLConnect)
		{
			try
			{
				//System.Data.OleDb.OleDbConnection objSQLConnect = this.getConnect("", 1);
				System.Data.DataTable objDT = new System.Data.DataTable();
				System.Data.OleDb.OleDbCommand objComm;
				System.Data.OleDb.OleDbDataAdapter objDA = new System.Data.OleDb.OleDbDataAdapter();

				objComm = objSQLConnect.CreateCommand();
				objComm.CommandTimeout = 600;
				objComm.CommandText = "SELECT fintVerNumber,fchrVersion,fchrName  FROM U8DRP_Template.dbo.VerSet " +
					                  "WHERE fintVerNumber = (SELECT max(fintVerNumber) FROM U8DRP_Template.dbo.VerSet)";
				objComm.ExecuteNonQuery();

				objDA.SelectCommand = objComm;
				objDA.Fill(objDT);

				return objDT;
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}


		public System.Data.DataTable getDatabaseObjects(System.Data.OleDb.OleDbConnection objSQLConnect, string strDatabase, string strObjectType)
		{
			try
			{
				string strSQL;
				string strWhere = "";
				string strVerNumber = this.getLastVerNumber(objSQLConnect);
				//System.Data.OleDb.OleDbConnection objSQLConnect = this.getConnect("MODEL", 1);
				System.Data.DataTable objDT = new System.Data.DataTable();
				System.Data.DataTable objObjectDT = new System.Data.DataTable();
				System.Data.OleDb.OleDbCommand objComm;
				System.Data.OleDb.OleDbDataAdapter objDA = new System.Data.OleDb.OleDbDataAdapter();


				objComm = objSQLConnect.CreateCommand();
				objComm.CommandTimeout = 600;

				//================ 视图 ============================================================================================================
				if(strObjectType == "1")
				{
					strWhere = "xtype in (N'FN', N'IF', N'TF') ";
				}
				else if(strObjectType == "2")
				{
					strWhere = "OBJECTPROPERTY(id, N'IsView') = 1 AND [NAME] like 'vw_U8DRP_%' ";
				}
				else if(strObjectType == "3")
				{
					strWhere = "OBJECTPROPERTY(id, N'IsProcedure') = 1 AND [NAME] like 's_U8DRP_%' ";
				}

				//先做的视对象
				strSQL = "Select 0 as fintOrder,0 AS fintObjectOrder, [name], [id] " +
						 "FROM sysobjects  S " +
						 "Where [name] Not IN(Select fchrObjectName " +
							                 "From U8DRP_Template.DBO.UpgradeObjectConfig " +
							                 "WHERE fintVersion = " + strVerNumber + "  " +
												   "AND fchrDatabase = '" + strDatabase + "' AND fintObjectType = " + strObjectType + ")  " +
			                    "AND " +  strWhere  + 
						 "UNION ALL " + //"\r\n\r\n" +
						 "SELECT 1 AS fintOrder, U.fintObjectOrder, S.[name], S.[id] " +
						 "FROM sysobjects S INNER JOIN U8DRP_Template.DBO.UpgradeObjectConfig U " +
								"ON U.fintVersion = " + strVerNumber + " AND U.fchrDatabase = '" + strDatabase + "' " + 
                                    "AND U.fintObjectType = " + strObjectType + " AND S.[name]= U.fchrObjectName " +
						 "Where " +  strWhere  + " " +
						 "Order By fintOrder, fintObjectOrder ,[name]";
				objComm.CommandText = strSQL; 
				objComm.ExecuteNonQuery();
				objDA.SelectCommand = objComm;
				objDA.Fill(objDT);				
				//==================================================================================================================================

				return objDT;
			}
			catch(System.Exception e)
			{
				throw(e);
			}

		}


		public string getDatabaseObjectScript(System.Data.OleDb.OleDbConnection objSQLConnect, string strDatabase, string strObjectType)
		{
			try
			{
				string strScript = "";
				string strSQL;
				int i;
				string strWhere = "";
				string strVerNumber = this.getLastVerNumber(objSQLConnect);
				//System.Data.OleDb.OleDbConnection objSQLConnect = this.getConnect("MODEL", 1);
				System.Data.DataTable objDT = new System.Data.DataTable();
				System.Data.DataTable objObjectDT = new System.Data.DataTable();
				System.Data.OleDb.OleDbCommand objComm;
				System.Data.OleDb.OleDbDataAdapter objDA = new System.Data.OleDb.OleDbDataAdapter();


				objComm = objSQLConnect.CreateCommand();
				objComm.CommandTimeout = 600;

				//================ 视图 ============================================================================================================
				if(strObjectType == "1")
				{
					strWhere = "xtype in (N'FN', N'IF', N'TF') ";
				}
				else if(strObjectType == "2")
				{
					strWhere = "OBJECTPROPERTY(id, N'IsView') = 1 AND [NAME] like 'vw_U8DRP_%' ";
				}
				else if(strObjectType == "3")
				{
					strWhere = "OBJECTPROPERTY(id, N'IsProcedure') = 1 AND [NAME] like 's_U8DRP_%' ";
				}

				//先做的视对象
				strSQL = "Select 0 as fintOrder,0 AS fintObjectOrder, [name], [id] " +
						 "FROM sysobjects  S " +
						 "Where [name] Not IN(Select fchrObjectName " +
											 "From U8DRP_Template.DBO.UpgradeObjectConfig " +
											 "WHERE fintVersion = " + strVerNumber + "  " +
											       "AND fchrDatabase = '" + strDatabase + "' AND fintObjectType = " + strObjectType + ")  " +
					            "AND " +  strWhere  + 
					     "UNION ALL " + 
					     "SELECT 1 AS fintOrder, U.fintObjectOrder, S.[name], S.[id] " +
					     "FROM sysobjects S INNER JOIN U8DRP_Template.DBO.UpgradeObjectConfig U " +
					                             "ON fintVersion = " + strVerNumber + " AND fchrDatabase = '" + strDatabase + "' " + 
					                                 "AND fintObjectType = " + strObjectType + " AND S.[name]= U.fchrObjectName " +
					     "Where " +  strWhere  + " " +
					     "Order By fintOrder, fintObjectOrder ,[name]";
				objComm.CommandText = strSQL; 
				objComm.ExecuteNonQuery();
				objDA.SelectCommand = objComm;
				objDA.Fill(objDT);

				if(objDT.Rows.Count != 0)
				{
					strScript = "";
					for(i = 0; i < objDT.Rows.Count; i++)
					{
						strScript = strScript + this.getObjectScript(objSQLConnect, objDT.Rows[i]["name"].ToString().Trim(), objDT.Rows[i]["id"].ToString().Trim(), strObjectType);
					}
				}
								
				//==================================================================================================================================

				return strScript;
			}
			catch(System.Exception e)
			{
				throw(e);
			}

		}


		public string getObjectScript(System.Data.OleDb.OleDbConnection objSQLConnect, 
                                      string strObjectName, string strObjectID, string strObjectType)
		{
			try
			{
				string strResualt = "";
				System.Data.DataTable objDT = new System.Data.DataTable();
				System.Data.OleDb.OleDbCommand objComm;
				System.Data.OleDb.OleDbDataAdapter objDA = new System.Data.OleDb.OleDbDataAdapter();

				objComm = objSQLConnect.CreateCommand();
				objComm.CommandTimeout = 600;
				objComm.CommandText = "Select [id], colid, [text] from dbo.syscomments Where ID = " + strObjectID + " Order By [colid]";
				objComm.ExecuteNonQuery();

				objDA.SelectCommand = objComm;
				objDA.Fill(objDT);

				if(objDT.Rows.Count != 0)
				{
					if(strObjectType == "1")
					{
						strResualt = "IF EXISTS (SELECT * FROM DBO.SYSOBJECTS " + 
												"WHERE id = object_id(N'[dbo].[" + strObjectName + "]') AND xtype in (N'FN', N'IF', N'TF')) " + "\r\n" +
									"    DROP FUNCTION [DBO].[" + strObjectName + "] "+ "\r\n"  +
									"GO" + "\r\n\r\n" ;
					}
					else if(strObjectType == "2")
					{
						strResualt = "IF EXISTS (SELECT * FROM DBO.SYSOBJECTS " + 
							"WHERE id = object_id(N'[dbo].[" + strObjectName + "]') AND OBJECTPROPERTY(id, N'IsView') = 1) " + "\r\n" +
							"    DROP VIEW [DBO].[" + strObjectName + "] "+ "\r\n"  +
							"GO" + "\r\n\r\n" ;
					}
					else if(strObjectType == "3")
					{
						strResualt = "IF EXISTS (SELECT * FROM DBO.SYSOBJECTS " + 
							"WHERE id = object_id(N'[dbo].[" + strObjectName + "]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1) " + "\r\n" +
							"    DROP PROCEDURE [DBO].[" + strObjectName + "] "+ "\r\n"  +
							"GO" + "\r\n\r\n" ;
					}

					for(int i = 0; i < objDT.Rows.Count; i++)
					{
						strResualt = strResualt + ((string)objDT.Rows[i]["text"]);
					}
					strResualt = strResualt.Replace("\n", "\r\n").Replace("\r\r", "\r");
                    strResualt = strResualt + "\r\nGO" + "\r\n\r\n";
				}
				return strResualt;
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}


        /// <summary>
        /// 生成数据库脚本，包括视图，自定义函数，存储过程
        /// </summary>
        /// <param name="objSQLConnect"></param>
        /// <param name="strObjectName"></param>
        /// <param name="strObjectID"></param>
        /// <param name="strObjectType"></param>
        /// <returns></returns>
        public string getObjectScript_VFP(System.Data.SqlClient.SqlConnection objSQLConnect, string strObjectName, 
                                          string strObjectID, string strObjectType, string strSQLVersion)
        {
            System.Data.SqlClient.SqlDataReader objDR = null;
            try
            {
                string strResualt = "";
                System.Data.SqlClient.SqlCommand objComm;

                strSQLVersion = "2000";

                objComm = objSQLConnect.CreateCommand();
                objComm.CommandTimeout = 600;

                if (strSQLVersion == "2000") 
                    objComm.CommandText = "SELECT [ID], COLID, [TEXT] FROM DBO.SYSCOMMENTS WHERE ID = " + strObjectID + " ORDER BY [COLID]";
                else if (strSQLVersion == "200X") 
                    objComm.CommandText = "SELECT SM.[object_id] AS ID, 0 AS COLID, [definition] AS TEXT FROM SYS.SQL_MODULES SM INNER JOIN SYS.OBJECTS O ON SM.[object_ID] = O.[Object_ID] WHERE SM.[object_id] = " + strObjectID + " ORDER BY [COLID]";
                objDR = objComm.ExecuteReader();


                if (objDR.Read())
                {
                    if (strObjectType == "1")
                    {
                        if (strSQLVersion == "2000")
                        {
                            strResualt = "IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE id = object_id(N'[dbo].[" + strObjectName + "]') AND xtype in (N'FN', N'IF', N'TF')) " + "\r\n" +
                                          "    DROP FUNCTION [DBO].[" + strObjectName + "] " + "\r\n" +
                                        "GO " + "\r\n\r\n";
                        }
                        else if (strSQLVersion == "200X")
                        {
                            strResualt = "IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE [object_id]  = object_id(N'[dbo].[" + strObjectName + "]') AND type in (N'FN', N'IF', N'TF')) " + "\r\n" +
                                         "    DROP FUNCTION [DBO].[" + strObjectName + "] " + "\r\n" +
                                         "GO " + "\r\n\r\n";
                        }
                    }
                    else if (strObjectType == "2")
                    {
                        if (strSQLVersion == "2000")
                        {
                            strResualt = "IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE id = object_id(N'[dbo].[" + strObjectName + "]') AND OBJECTPROPERTY(id, N'IsView') = 1) " + "\r\n" +
                                         "    DROP VIEW [DBO].[" + strObjectName + "] " + "\r\n" +
                                         "GO " + "\r\n\r\n";
                        }
                        else if (strSQLVersion == "200X")
                        {
                            strResualt = "IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE [object_id] = object_id(N'[dbo].[" + strObjectName + "]') AND OBJECTPROPERTY([object_id], N'IsView') = 1) " + "\r\n" +
                                         "    DROP VIEW [DBO].[" + strObjectName + "] " + "\r\n" +
                                         "GO " + "\r\n\r\n";
                        }
                    }
                    else if (strObjectType == "3")
                    {
                        if (strSQLVersion == "2000")
                        {
                            strResualt = "IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE id = object_id(N'[dbo].[" + strObjectName + "]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1) " + "\r\n" +
                                        "    DROP PROCEDURE [DBO].[" + strObjectName + "] " + "\r\n" +
                                        "GO " + "\r\n\r\n";
                        }
                        if (strSQLVersion == "200X")
                        {
                            strResualt = "IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE [object_id] = object_id(N'[dbo].[" + strObjectName + "]') AND OBJECTPROPERTY([object_id], N'IsProcedure') = 1) " + "\r\n" +
                                        "    DROP PROCEDURE [DBO].[" + strObjectName + "] " + "\r\n" +
                                        "GO " + "\r\n\r\n";
                        }
                    }
                    else if (strObjectType == "4")
                    {
                        if (strSQLVersion == "2000")
                        {
                            strResualt = "IF EXISTS (SELECT * FROM DBO.SYSOBJECTS WHERE id = object_id(N'[dbo].[" + strObjectName + "]') AND OBJECTPROPERTY(id, N'IsTrigger') = 1) " + "\r\n" +
                                        "    DROP TRIGGER " + strObjectName + " " + "\r\n" +
                                        "GO " + "\r\n\r\n";
                        }
                        if (strSQLVersion == "200X")
                        {
                            strResualt = "IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE [object_id] = object_id(N'[dbo].[" + strObjectName + "]') AND OBJECTPROPERTY([object_id], N'IsTrigger') = 1) " + "\r\n" +
                                        "    DROP TRIGGER " + strObjectName + " " + "\r\n" +
                                        "GO " + "\r\n\r\n";
                        }
                    }

                    strResualt = strResualt + objDR.GetString(2);
                    while (objDR.Read())
                    {
                        strResualt = strResualt + objDR.GetString(2);
                    }
                    objDR.Close();

                    strResualt = strResualt.Replace("\n", "\r\n").Replace("\r\r", "\r");
                    strResualt = strResualt + "\r\nGO" + "\r\n\r\n";
                }
                return strResualt;
            }
            catch (System.Exception e)
            {
                throw (e);
            }
            finally
            {
                if (objDR != null) { objDR.Close(); objDR = null; }
            }
        }


        /// <summary>
        /// 生成数据库索引脚本
        /// </summary>
        /// <param name="objSQLConnect"></param>
        /// <param name="strObjectName"></param>
        /// <param name="strObjectID"></param>
        /// <param name="strObjectType"></param>
        /// <returns></returns>
        public string getObjectScript_Index(System.Data.SqlClient.SqlConnection objSQLConnect, string strTableName, string strIndexName, bool bDropRebuild, string strSQLVersion)
        {
            try
            {
                string strResualt = "";
                string strFields = "";
                string strIncludes = "";
                string strWith = "";
                string strFieldsHead = "";
                System.Data.SqlClient.SqlCommand objComm;
                System.Data.SqlClient.SqlDataReader objDR = null;


                #region 查询指定索引具体信息
                objComm = objSQLConnect.CreateCommand();
                objComm.CommandTimeout = 600;
                if (strSQLVersion == "200X")
                {
                    objComm.CommandText = @"SELECT O.NAME AS TableName, IX.[OBJECT_ID], IX.[NAME], IX.[INDEX_ID], CAST(IX.TYPE AS INT) AS IndexType, IX.TYPE_DESC, IC.index_column_id, IC.column_id,
	                                               C.NAME AS ColumnName, C.system_type_id, IC.is_descending_key AS [Descending],
                                                   CAST(CASE IX.index_id WHEN 1 THEN 1 ELSE 0 END AS bit) AS [IsClustered], IC.is_included_column AS [IsIncluded],
                                                   SCHEMA_NAME(TB.schema_id) AS [TableSchema], IX.allow_row_locks AS [AllowRowLocks], IX.allow_page_locks AS [AllowPageLocks]
                                            FROM sys.indexes IX INNER JOIN sys.index_columns IC ON IX.[index_id] = IC.[index_id] AND IX.[object_id] = IC.[object_id]
                                                                INNER JOIN sys.columns C ON C.column_id = IC.column_id AND IC.[object_id] = C.[object_id]
                                                                INNER JOIN sys.objects O ON IX.[object_ID] = O.[object_ID]
                                                                INNER JOIN sys.tables AS TB  ON (IX.index_id > 0 and IX.is_hypothetical = 0) AND (IX.object_id=TB.object_id)
                                            WHERE O.NAME = '" + strTableName + "' AND IX.[name] = '" + strIndexName + @"' 
                                            ORDER BY O.NAME, IX.NAME, IC.index_column_id";
                }
                else if (strSQLVersion == "2000")
                {
                    objComm.CommandText = @"SELECT O.NAME AS TableName, IX.[ID], IX.[NAME], IX.[indid], CAST((CASE WHEN IX.indid > 1 THEN IX.indid WHEN IX.indid = 1 THEN 1 END) AS INT) AS TYPE, 
                                                  (CASE WHEN IX.indid > 1 THEN 'NONCLUSTER' WHEN IX.indid = 1 THEN 'CLUSTER' END) AS TYPE_DESC, IC.keyno AS index_column_id, IC.colid AS column_id,
	                                               C.NAME AS ColumnName, C.XType AS system_type_id, CAST(INDEXKEY_PROPERTY(ic.id, ic.indid, ic.keyno, N'IsDescending') AS bit) AS [Descending],
                                                   CAST(CASE IX.index_id WHEN 1 THEN 1 ELSE 0 END AS bit) AS [IsClustered], CAST(0 AS BIT) AS [IsIncluded],
                                                   'DBO' AS [TableSchema], '1' AS [AllowRowLocks], '1' AS [AllowPageLocks]
                                            FROM sysindexes IX INNER JOIN sysindexkeys IC ON IX.[indid] = IC.[indid] AND IX.[ID] = IC.[ID]
                                                                INNER JOIN  syscolumns C ON C.colid = IC.colid AND IC.[ID] = C.[ID]
                                                                INNER JOIN sysobjects O ON IX.[ID] = O.[ID]
                                            WHERE O.NAME = '" + strTableName + "' AND IX.[name] = '" + strIndexName + @"' 
                                            ORDER BY O.NAME, IX.NAME, IC.keyno ";
                }
                objDR = objComm.ExecuteReader();
                #endregion

                #region 构造不同 SQL Server 的脚本
                
                if (strSQLVersion == "200X")
                {
                    strResualt = "IF" + (bDropRebuild ? " " : " NOT ") + @"EXISTS(SELECT * FROM sys.indexes IX INNER JOIN sys.index_columns IC ON IX.[index_id] = IC.[index_id] AND IX.[object_id] = IC.[object_id] INNER JOIN  sys.columns C ON C.column_id = IC.column_id AND IC.[object_id] = C.[object_id] INNER JOIN sys.objects O ON IX.[object_ID] = O.[object_ID]
                                    WHERE O.NAME = '" + strTableName + "' AND IX.[name] = '" + strIndexName + @"')";
                }
                else if (strSQLVersion == "2000")
                {
                    strResualt = @"IF" + (bDropRebuild ? " " : " NOT ") + @"EXISTS(SELECT * FROM sysindexes IX INNER JOIN sysindexkeys IC ON IX.[indid] = IC.[indid] AND IX.[ID] = IC.[ID] INNER JOIN  syscolumns C ON C.colid = IC.colid AND IC.[ID] = C.[ID] INNER JOIN sysobjects O ON IX.[ID] = O.[ID] 
                                     WHERE O.NAME = '" + strTableName + "' AND IX.[name] = '" + strIndexName + @"')";

                }
                if (objDR.Read())
                {
                    if (bDropRebuild)
                    {
                        strResualt = strResualt + "\r\n    DROP INDEX [" + strIndexName + "] ON [" + ((string)objDR["TableSchema"]).Trim() + "].[" + strTableName + "]  " + "\r\n GO \r\n\r\n";
                    }
                }
                else
                {
                    throw new Exception("没有找到制定的索引：" + strTableName + "." + strIndexName + "。");
                }
                #endregion
                
                #region 构造索引内容具体信息
                strFields = "";
                strIncludes = "";
                strWith = "";
                
                strFields = "    " + objDR["ColumnName"].ToString().Trim() + ((bool)objDR["Descending"] ? " DESC" : "");
                if ((int)objDR["IndexType"] == 1) strFieldsHead = @"CREATE CLUSTERED INDEX [" + strIndexName + "] ON [" + ((string)objDR["TableSchema"]).Trim() + "].[" + strTableName + "]  \r\n(";
                else if ((int)objDR["IndexType"] > 1) strFieldsHead = @"CREATE NONCLUSTERED INDEX [" + strIndexName + "] ON [" + ((string)objDR["TableSchema"]).Trim() + "].[" + strTableName + "]  \r\n(";

                if (strSQLVersion == "200X") strWith = "WITH(ALLOW_ROW_LOCKS  = " + ((bool)objDR["AllowRowLocks"] ? "ON" : "OFF") + ", ALLOW_PAGE_LOCKS  = " + ((bool)objDR["AllowPageLocks"] ? "ON" : "OFF") + ")";                

                while (objDR.Read())
                {
                    if (!(bool)objDR["IsIncluded"])
                    {
                        strFields = strFields + ", \r\n    " + ((string)objDR["ColumnName"]).Trim() + ((bool)objDR["Descending"] ? " DESC" : "") + "";
                    }
                    else
                    {
                        strIncludes = strIncludes + ", \r\n    " + ((string)objDR["ColumnName"]).Trim();
                    }
                }
                objDR.Close();

                strResualt = strResualt + strFieldsHead + "\r\n" + strFields + "\r\n)";
                if (strIncludes != "")
                    strResualt = strResualt + "\r\nINCLUDE\r\n(" + strIncludes.Trim().Substring(1, strIncludes.Length - 1) + "\r\n)";
                if (strWith != "")
                    strResualt = strResualt + "\r\n" + strWith;
                #endregion

                
                strResualt = strResualt + "\r\nGO \r\n\r\n";

                return strResualt;
            }
            catch (System.Exception e)
            {
                throw (e);
            }
        }


		public void setObjectScriptUpdate(System.Data.OleDb.OleDbConnection objSQLConnect,string strDataBase, string strObjectType, string strContent)
		{
			try
			{
				string strSQL = "";
				string strOrder = "";
				string strPrompt = "";
				string strVerNumber = "";				
				string strVer = "";
				System.Data.DataTable objDT = new System.Data.DataTable();
				System.Data.OleDb.OleDbCommand objComm;
				System.Data.OleDb.OleDbDataAdapter objDA = new System.Data.OleDb.OleDbDataAdapter();
				
				objDT = this.getLastVerNumberAll(objSQLConnect);
				strVerNumber = objDT.Rows[0]["fintVerNumber"].ToString();
				strVer = (string)objDT.Rows[0]["fchrVersion"];

				if(strObjectType == "1")
				{
					strOrder = "2001";
					strPrompt = "升级 v" + strVer + "：更新自定义函数";
				}
				if(strObjectType == "2")
				{
					strOrder = "3001";
					strPrompt = "升级 v" + strVer + "：更新视图";
				}
				if(strObjectType == "3")
				{
					strOrder = "4001";
					strPrompt = "升级 v" + strVer + "：更新存储过程";
				}

				objComm = objSQLConnect.CreateCommand();
				objComm.CommandTimeout = 600;
				
				strSQL = "SELECT fchrID, fintVersion, fchrDataBase, fintOrder, fchrPrompt, fchrSQLText, fchrNote, fchrBugNo " + 
					     "FROM UpgradeDataBaseSQL WHERE fintVersion = '" + strVerNumber + "' AND fchrDataBase = '" + strDataBase + "' " +
				                                       "AND fintOrder = " + strOrder;
				objComm.CommandText = strSQL;
				objComm.ExecuteNonQuery();

				objDA.SelectCommand = objComm;
				objDT.Clear();
				objDA.Fill(objDT);
				
				if(objDT.Rows.Count > 0)
				{
					strSQL = "UPDATE UpgradeDataBaseSQL SET fchrSQLText = '" +strContent.Replace("'", "''") + "' WHERE fchrID = '" + objDT.Rows[0]["fchrID"].ToString().ToUpper() + "'";					
				}
				else
				{
					strSQL = "INSERT INTO UpgradeDataBaseSQL (fchrID, fintVersion, fchrDataBase, fintOrder, fchrPrompt, fchrSQLText, fchrNote) " +
						     "VALUES (NEWID(),  '" + strVerNumber + "', '" + strDataBase + "', " + strOrder + ",'" + strPrompt + "', '" + strContent.Replace("'", "''") + "', '')";
				}
				objComm.CommandText = strSQL;
				objComm.ExecuteNonQuery();				
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}


	}
}
