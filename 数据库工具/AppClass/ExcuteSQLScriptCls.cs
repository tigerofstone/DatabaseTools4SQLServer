using System;
using System.Collections;
using System.Data;
using System.Xml;
using System.IO;


namespace Upgrade.AppClass
{
	/// <summary>
	/// ExcuteSQLScriptCls 的摘要说明。
	/// </summary>
	public class ExcuteSQLScriptCls
	{
		public ArrayList objSQLInstances;
		public ArrayList objSQLDatabases;

		public string strUserName;
		public string strPassWord;
		public string strSQLScript;

        public System.Windows.Forms.Label objLabel = null;
        public System.Windows.Forms.ProgressBar objProcess = null;

		public ExcuteSQLScriptCls()
		{

		}

		public ExcuteSQLScriptCls(ArrayList SQLInstances, ArrayList SQLDatabases, string UserName, string PassWord, string SQLScript)
		{
			objSQLInstances = SQLInstances;
			objSQLDatabases = SQLDatabases;
			strUserName = UserName;
			strPassWord = PassWord;
			strSQLScript = SQLScript;
		}
		
		public void ExcuteSQLScript()
		{
			ArrayList alScripts;
			string strResult = "";

			alScripts = getSQLScripts(strSQLScript);
			if(alScripts.Count >= 0)
			{
                if (this.objProcess != null)
                {
                    this.objProcess.Minimum = 0;
                    this.objProcess.Maximum = objSQLInstances.Count * objSQLDatabases.Count;
                    this.objProcess.Value = 0;
                }
				for(int i = 0; i < objSQLInstances.Count; i++)
				{
                    for(int j = 0; j < objSQLDatabases.Count; j++)
					{
                        try
                        {
                            EcecScriptsByServer((string)objSQLInstances[i], (string)objSQLDatabases[j], alScripts);                            
                        }
                        catch (System.Exception e)
                        {
                            strResult = strResult + "在实例：“" + (string)objSQLInstances[i] + "”的数据库“" + (string)objSQLDatabases[j] + "”中出现错误：\r\n" + e.Message + "\r\n";
                        }
                        finally
                        {
                            if (this.objProcess != null) this.objProcess.Value = this.objProcess.Value + 1;
                        }
					}
				}
			}

			if(strResult != "") throw(new AppClass.MyDBUpdateException(strResult));
		}

		private ArrayList getSQLScripts(string strScript)
		{
			string[] strScripts = null;
			string strTemp;
			string strSingleSQL = "";
			ArrayList objAL = null;

			System.Text.RegularExpressions.Regex objRegex = new System.Text.RegularExpressions.Regex("(\r\n)");

			try
			{
				strScripts = objRegex.Split(strScript);

				objAL = new ArrayList();

				if(strScript.IndexOf("\r\n") < 0) 
				{
					objAL.Add(strScript);
					return objAL;
				}
				for(int i = 0; i < strScripts.Length; i++)
				{
					strTemp = strScripts[i];
					if(strTemp.Trim().ToUpper() != "GO")
					{
						if(strTemp != "\r\n") 
                            strSingleSQL = strSingleSQL + strTemp + "\r\n";
					
						strTemp = strTemp.Trim();
					}
					else
					{
                        if (strSingleSQL != "")
                        {
                            objAL.Add(strSingleSQL);
                            strSingleSQL = "";
                        }
					}

                    if (i == strScripts.Length - 1 && (strSingleSQL != "" && strSingleSQL != "\r\n"))
                    {
                        objAL.Add(strSingleSQL);
                        strSingleSQL = "";
                    }
				}

				return objAL;
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}

        		
		private void EcecScriptsByServer(string strInstances, string strDataBase, ArrayList strScript)
		{
			try
			{
				System.Data.SqlClient.SqlConnection objConn;
				System.Data.DataTable objDBDT;
				string strErrMessage = "";
			
				if(strDataBase != "Model & AppX" ) 
				{
                    ShowFormInfo("提示：正在服务器实例“" + strInstances + "”的数据库“" + strDataBase + "”中执行脚本......");
                    EcecScripts(strInstances, strDataBase, strUserName, strPassWord, strScript);
				}
                else if (strDataBase == "U8Data")
                {
                    objConn = AppClass.PublicDBCls.getSQLConnect(strInstances, "Master", strUserName, strPassWord);
                    objDBDT = getDataTable(objConn, "SELECT name FROM sysdatabases WHERE [NAME] LIKE 'UFDATA_%' Order By [name]");
                    for (int i = 0; i < objDBDT.Rows.Count; i++)
                    {
                        try
                        {
                            ShowFormInfo("提示：正在服务器实例“" + strInstances + "”的数据库“" + (string)objDBDT.Rows[i]["name"] + "”中执行脚本......");
                            EcecScripts(strInstances, (string)objDBDT.Rows[i]["name"], strUserName, this.strPassWord, strScript);
                        }
                        catch (System.Exception e)
                        {
                            strErrMessage = strErrMessage + "数据库“" + (string)objDBDT.Rows[i]["name"] + "”中错误：\r\n" + e.Message + "\r\n";
                        }
                    }
                    if (strErrMessage != "") throw (new System.Exception(strErrMessage));
                }
                else if (strDataBase == "Model & AppX")
				{	
					objConn = AppClass.PublicDBCls.getSQLConnect(strInstances, "U8DRP_DataBase", strUserName, strPassWord);
                    objDBDT = getDataTable(objConn, "Select fchrServerName,fchrDatabaseName,fchrDBuserName,fchrDBpassword FROM Sys_UserDataBase WHERE fchrDatabaseName LIKE 'app%' Order By fchrServerName,fchrDatabaseName");
					for(int i = 0; i < objDBDT.Rows.Count; i++)
					{
						try
						{
                            ShowFormInfo("提示：正在服务器实例“" + (string)objDBDT.Rows[i]["fchrServerName"] + "”的数据库“" + (string)objDBDT.Rows[i]["fchrDatabaseName"] + "”中执行脚本......");
                            EcecScripts((string)objDBDT.Rows[i]["fchrServerName"], (string)objDBDT.Rows[i]["fchrDatabaseName"], (string)objDBDT.Rows[i]["fchrDBuserName"], (string)objDBDT.Rows[i]["fchrDBpassword"], strScript);
                        }
						catch(System.Exception e)
						{
							strErrMessage = strErrMessage +  "数据库“" + (string)objDBDT.Rows[i]["fchrDatabaseName"] + "”中错误：\r\n" + e.Message + "\r\n";
						}	
					}
					objDBDT = null;
                    objDBDT = getDataTable(objConn, "SELECT DISTINCT fchrServerName,fchrDBuserName,fchrDBpassword FROM Sys_UserDataBase WHERE fchrDatabaseName LIKE 'app%' Order By fchrServerName");
					for(int i = 0; i < objDBDT.Rows.Count; i++)
					{
						try
						{
                            ShowFormInfo("提示：正在服务器实例“" + (string)objDBDT.Rows[i]["fchrServerName"] + "”的数据库“" + "Model" + "”中执行脚本......");
                            EcecScripts((string)objDBDT.Rows[i]["fchrServerName"], "Model", (string)objDBDT.Rows[i]["fchrDBuserName"], (string)objDBDT.Rows[i]["fchrDBpassword"], strScript);
						}
						catch(System.Exception e)
						{
							strErrMessage = strErrMessage +  "数据库“Model”中错误：\r\n" + e.Message + "\r\n";
						}	
					}

					if(strErrMessage != "") throw(new System.Exception(strErrMessage));
				}
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}

		private void EcecScripts(string strInstance, string strDatabase, string strUserName, string strPassWord, ArrayList strScript)
		{
			System.Data.SqlClient.SqlCommand objComm;
			System.Data.SqlClient.SqlConnection objConn = null;
			string strErrMessage = "";

            try
            {
                objConn = AppClass.PublicDBCls.getSQLConnect(strInstance, strDatabase, strUserName, strPassWord);
                objComm = objConn.CreateCommand();
                objComm.CommandTimeout = 600;

                for (int k = 0; k < strScript.Count; k++)
                {
                    try
                    {
                        objComm.CommandText = (string)strScript[k];
                        objComm.ExecuteNonQuery();
                    }
                    catch (System.Exception e)
                    {
                        strErrMessage = strErrMessage + e.Message + "\r\n";
                    }
                }

                if (strErrMessage != "") throw (new System.Exception(strErrMessage));
            }
            catch (System.Exception e)
            {
                strErrMessage = strErrMessage + e.Message + "\r\n";
                if (strErrMessage != "") throw (new System.Exception(strErrMessage));
            }
            finally
            {
                objComm = null;
                if(objConn != null && objConn.State == ConnectionState.Open)objConn.Close();
                objConn = null;
            }            
		}


		private System.Data.DataTable getDataTable(System.Data.SqlClient.SqlConnection objConn, string strScript)
		{
            System.Data.DataTable objDT = new System.Data.DataTable();
			System.Data.SqlClient.SqlCommand objComm;
			System.Data.SqlClient.SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();

			objComm = objConn.CreateCommand();
			objComm.CommandTimeout = 600;
			objComm.CommandText = strScript;
			objComm.ExecuteNonQuery();

			objDA.SelectCommand = objComm;
			objDA.Fill(objDT);
			
			objComm = null;
			objDA = null;

			return objDT;
		}

        private void ShowFormInfo(string strInfo)
        {
            if (this.objLabel != null)
            {
                this.objLabel.Text = strInfo;
                this.objLabel.Refresh();
                System.Threading.Thread.Sleep(100);
            }
        }
	}
}
