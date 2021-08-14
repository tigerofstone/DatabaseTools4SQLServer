using System;
using System.Data;
using System.Data.SqlClient;

namespace Upgrade.AppClass
{
	/// <summary>
	/// PublicDBCls ��ժҪ˵����
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
					System.Exception objExp = new Exception("�������ݿ�ʧ�ܡ�");

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
        /// ������ݿ����ӣ��Ϸ���������������֤
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
					System.Exception objExp = new Exception("�������ݿ�ʧ�ܡ�");

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
        /// ������ݿ�����
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
                               " Connection Timeout=10; Application Name=���ݿ⹤�� MX ProcessProgram;Connection Timeout=600;Pooling=false;";

                objSQLConnect.ConnectionString = strConnection;
                objSQLConnect.Open();

                if (objSQLConnect.State != System.Data.ConnectionState.Open)
                {
                    System.Exception objExp = new Exception("�������ݿ�ʧ�ܡ�");
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
        /// ������ݿ�����
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
                               "Application Name=���ݿ⹤�� MX ProcessProgram;Connection Timeout=600;Pooling=false;";

                objSQLConnect.ConnectionString = _strConnection;
                objSQLConnect.Open();

                if (objSQLConnect.State != System.Data.ConnectionState.Open)
                {
                    System.Exception objExp = new Exception("�������ݿ�ʧ�ܡ�");
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
                               "Application Name=���ݿ⹤�� MX ProcessProgram;Connection Timeout=10;Pooling=false;";

                objSQLConnect.ConnectionString = _strConnection;
                objSQLConnect.Open();

                if (objSQLConnect.State != System.Data.ConnectionState.Open)
                {
                    System.Exception objExp = new Exception("δ�ܴ�ָ�������ݿ⣬�������ݿ�ʧ�ܡ�");
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
        /// ������ݿ�����
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
                    System.Exception objExp = new Exception("�������ݿ�ʧ�ܡ�");
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
        /// ������ݿ������Ƿ���������SA��¼Master���ݿ��Ƿ�����
        /// </summary>
        /// <param name="strInstance">ʵ����</param>
        /// <param name="strPassword">SA����</param>
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
