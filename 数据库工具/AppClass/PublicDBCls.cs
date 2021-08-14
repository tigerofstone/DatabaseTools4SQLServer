using System;
using System.Data;
using System.Data.SqlClient;

namespace Upgrade.AppClass
{
	/// <summary>
	/// PublicDBCls 的摘要说明。
	/// </summary>
	public class PublicDBCls
	{

        public string strServer = "";
        public string strDatabaseName = "";
		public string strUser = "";
		public string strPW = "";
		public string strVer = "";
        public bool blnIntergrated = false;
		public string strDBConnection = "";


		public PublicDBCls()
		{

		}

		public System.Data.OleDb.OleDbConnection getConnect(string strDatabase, int intGetType) 
		{
			try
			{
				string strConnection;
				System.Data.OleDb.OleDbConnection objSQLConnect = new System.Data.OleDb.OleDbConnection ();
				
				strConnection = "Data Source=" + this.strServer + ";Initial Catalog=" + 
					(strDatabase == "" ? "U8DRP_Template" : strDatabase) + ";User ID=" + this.strUser + ";" + 
					"PassWord=" + this.strPW  + ";" +
					"Tag with column collation when possible=False;Use Procedure for Prepare=1;Auto Translate=True;Persist Security Info=False;" + 
					"Provider=SQLOLEDB.1;Use Encryption for Data=False;Packet Size=4096";

				objSQLConnect.ConnectionString = strConnection;
				objSQLConnect.Open();

				if(objSQLConnect.State != System.Data.ConnectionState.Open)
				{
					System.Exception objExp = new Exception("连接数据库失败。");

					strConnection = "";

					throw(objExp);
				}
				if(intGetType == 0)
				{
					objSQLConnect.Close();
					objSQLConnect = null;
				}
				
				return objSQLConnect;
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}

        /// <summary>
        /// 获得数据库连接，老方法不包含集成认证
        /// </summary>
        /// <param name="strInstance"></param>
        /// <param name="strDatabase"></param>
        /// <param name="strUserName"></param>
        /// <param name="strPassWord"></param>
        /// <returns></returns>
		public static System.Data.SqlClient.SqlConnection getSQLConnect(string strInstance, string strDatabase, string strUserName, string strPassWord) 
		{
			try
			{
				string strConnection;
				System.Data.SqlClient.SqlConnection objSQLConnect = new System.Data.SqlClient.SqlConnection();
				
				strConnection = "server=" + strInstance + ";database=" + strDatabase	+ ";" +
								"User ID=" + strUserName + ";" + "PassWord=" + strPassWord  + ";" +
					            "Connect Timeout=10;Persist Security Info=False;";
				
				objSQLConnect.ConnectionString = strConnection;
				objSQLConnect.Open();

				if(objSQLConnect.State != System.Data.ConnectionState.Open)
				{
					System.Exception objExp = new Exception("连接数据库失败。");

					strConnection = "";

					throw(objExp);
				}
				
				return objSQLConnect;
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}

        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <param name="sInstance"></param>
        /// <param name="sUser"></param>
        /// <param name="sPassword"></param>
        /// <param name="bIntergrated"></param>
        /// <returns></returns>
        public System.Data.SqlClient.SqlConnection getSQLConnection(string sInstance, string sDatabase, string sUser, string sPassword, bool bIntergrated)
        {
            try
            {
                SqlConnection objSQLConnect = new SqlConnection();

                string strConnection;

                strConnection = "Data Source=" + sInstance + ";Initial Catalog=" + sDatabase + ";" +
                               (bIntergrated ? "Integrated Security=true;" : "User ID=" + sUser + ";PassWord=" + sPassword + ";") +
                               " Connection Timeout=10; Application Name=数据库工具 MX ProcessProgram;Connection Timeout=600;Pooling=false;";

                objSQLConnect.ConnectionString = strConnection;
                objSQLConnect.Open();

                if (objSQLConnect.State != System.Data.ConnectionState.Open)
                {
                    System.Exception objExp = new Exception("连接数据库失败。");
                    objSQLConnect = null;

                    throw (objExp);
                }

                strDBConnection = strConnection;

                return objSQLConnect;

            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <param name="sInstance"></param>
        /// <param name="sUser"></param>
        /// <param name="sPassword"></param>
        /// <param name="bIntergrated"></param>
        /// <returns></returns>
        public System.Data.SqlClient.SqlConnection getSQLConnection(string sInstance, string sDatabase, string sUser, string sPassword, bool bIntergrated, out string strConnection)
        {
            try
            {
                SqlConnection objSQLConnect = new SqlConnection();

                string _strConnection;

                _strConnection = "Data Source=" + sInstance + ";Initial Catalog=" + sDatabase + ";" +
                               (bIntergrated ? "Integrated Security=true;" : "User ID=" + sUser + ";PassWord=" + sPassword + ";") +
                               "Application Name=数据库工具 MX ProcessProgram;Connection Timeout=600;Pooling=false;";

                objSQLConnect.ConnectionString = _strConnection;
                objSQLConnect.Open();

                if (objSQLConnect.State != System.Data.ConnectionState.Open)
                {
                    System.Exception objExp = new Exception("连接数据库失败。");
                    objSQLConnect = null;

                    throw (objExp);
                }

                strConnection = _strConnection;

                return objSQLConnect;

            }
            catch (Exception e)
            {
                throw (e);
            }
        }


        public System.Data.SqlClient.SqlConnection getSQLConnection()
        {
            try
            {
                SqlConnection objSQLConnect = new SqlConnection();

                string _strConnection;

                _strConnection = "Data Source=" + this.strServer + ";Initial Catalog=" + this.strDatabaseName + ";" +
                               (this.blnIntergrated ? "Integrated Security=true;" : "User ID=" + this.strUser + ";PassWord=" + this.strPW + ";") +
                               "Application Name=数据库工具 MX ProcessProgram;Connection Timeout=10;Pooling=false;";

                objSQLConnect.ConnectionString = _strConnection;
                objSQLConnect.Open();

                if (objSQLConnect.State != System.Data.ConnectionState.Open)
                {
                    System.Exception objExp = new Exception("未能打开指定的数据库，连接数据库失败。");
                    objSQLConnect = null;
                    throw (objExp);
                }

                this.strDBConnection = _strConnection;

                return objSQLConnect;

            }
            catch (Exception e)
            {
                throw (e);
            }
        }


        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <param name="sInstance"></param>
        /// <param name="sUser"></param>
        /// <param name="sPassword"></param>
        /// <param name="bIntergrated"></param>
        /// <returns></returns>
        public System.Data.SqlClient.SqlConnection getSQLConnection(string strConnection)
        {
            try
            {
                SqlConnection objSQLConnect = new SqlConnection();

                objSQLConnect.ConnectionString = strConnection;
                objSQLConnect.Open();

                if (objSQLConnect.State != System.Data.ConnectionState.Open)
                {
                    System.Exception objExp = new Exception("连接数据库失败。");
                    objSQLConnect = null;

                    throw (objExp);
                }

                return objSQLConnect;

            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public System.Data.DataTable getDataTable(System.Data.SqlClient.SqlConnection objConn, string strScript)
        {
            System.Data.DataTable objDT = new System.Data.DataTable();
            System.Data.SqlClient.SqlCommand objComm;
            System.Data.SqlClient.SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();

            try
            {
                objComm = objConn.CreateCommand();
                objComm.CommandTimeout = 600;
                objComm.CommandText = strScript;
                objComm.ExecuteNonQuery();

                objDA.SelectCommand = objComm;
                objDA.Fill(objDT);                

                return objDT;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                objComm = null;
                objDA = null;
            }
        }


        /// <summary>
        /// 检查数据库连接是否正常，以SA登录Master数据库是否正常
        /// </summary>
        /// <param name="strInstance">实例名</param>
        /// <param name="strPassword">SA口令</param>
        /// <returns></returns>
        public string checkConnect(string strInstance, string strPassword)
        {
            try
            {
                this.strServer = strInstance;
                this.strDatabaseName = "Master";
                this.strUser = "SA";
                this.strPW = strPassword;

                this.getSQLConnection();
                return this.strDBConnection;
            }
            catch (Exception E)
            {
                throw E;
            }
        }
	}
}
