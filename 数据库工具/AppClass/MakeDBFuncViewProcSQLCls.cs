using System;
using System.Collections.Generic;
using System.Text;

namespace Upgrade.AppClass
{
    class MakeDBFuncViewProcSQLCls
    {
        public MakeDBFuncViewProcSQLCls()
		{
			
		}

		

		public System.Data.DataTable getDatabaseObjects(System.Data.OleDb.OleDbConnection objSQLConnect, string strDatabase, string strObjectType)
		{
			try
			{
				string strSQL;
				string strWhere = "";
				string strVerNumber = "0";//= this.getLastVerNumber(objSQLConnect);
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

        public string getDatabaseObjectScript(System.Data.SqlClient.SqlConnection objSQLConnect, string strDatabase, string strObjectType)
		{
			try
			{
				string strScript = "";
				string strSQL;
				int i;
				string strWhere = "";
                string strVerNumber = "0";// = this.getLastVerNumber(objSQLConnect);
				//System.Data.OleDb.OleDbConnection objSQLConnect = this.getConnect("MODEL", 1);
				System.Data.DataTable objDT = new System.Data.DataTable();
				System.Data.DataTable objObjectDT = new System.Data.DataTable();
                System.Data.SqlClient.SqlCommand objComm;
                System.Data.SqlClient.SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();


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
								
				return strScript;
			}
			catch(System.Exception e)
			{
				throw(e);
			}

		}

        public string getObjectScript(System.Data.SqlClient.SqlConnection objSQLConnect, string strObjectName, string strObjectID, string strObjectType)
		{
			try
			{
                string strResualt = "";
                System.Data.DataTable objDT = new System.Data.DataTable();
                System.Data.SqlClient.SqlCommand objComm;
                System.Data.SqlClient.SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();

                objComm = objSQLConnect.CreateCommand();
                objComm.CommandTimeout = 600;
                objComm.CommandText = "Select [id], colid, [text] from dbo.syscomments Where ID = " + strObjectID + " Order By [colid]";
                objComm.ExecuteNonQuery();

                objDA.SelectCommand = objComm;
                objDA.Fill(objDT);

                if (objDT.Rows.Count != 0)
                {
                    if (strObjectType == "1")
                    {
                        strResualt = "if exists (select * from dbo.sysobjects " +
                                                "where id = object_id(N'[dbo].[" + strObjectName + "]') and xtype in (N'FN', N'IF', N'TF')) " + "\r\n" +
                                    "    drop function [dbo].[" + strObjectName + "] " + "\r\n" +
                                    "GO" + "\r\n\r\n";
                    }
                    else if (strObjectType == "2")
                    {
                        strResualt = "if exists (select * from dbo.sysobjects " +
                            "where id = object_id(N'[dbo].[" + strObjectName + "]') and OBJECTPROPERTY(id, N'IsView') = 1) " + "\r\n" +
                            "    drop view [dbo].[" + strObjectName + "] " + "\r\n" +
                            "GO" + "\r\n\r\n";
                    }
                    else if (strObjectType == "3")
                    {
                        strResualt = "if exists (select * from dbo.sysobjects " +
                            "where id = object_id(N'[dbo].[" + strObjectName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1) " + "\r\n" +
                            "    drop procedure [dbo].[" + strObjectName + "] " + "\r\n" +
                            "GO" + "\r\n\r\n";
                    }

                    for (int i = 0; i < objDT.Rows.Count; i++)
                    {
                        strResualt = strResualt + ((string)objDT.Rows[i]["text"]);
                    }
                    strResualt = strResualt + "\r\nGO" + "\r\n\r\n";
                    strResualt = strResualt.Replace("\n", "").Replace("\r", "\r\n");
                }
                
				return strResualt;
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}

    }
}
