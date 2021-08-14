using System;
using System.Collections.Generic;
using System.Text;

namespace Upgrade.AppClass
{
    public abstract class DatabaseObjects
    {
        public System.Data.SqlClient.SqlConnection objSQLConn;

        public DatabaseObjects()
        {
        }

        public DatabaseObjects(System.Data.SqlClient.SqlConnection objConn)
        {
            objSQLConn = objConn;
        }

        public abstract System.Data.DataTable getDatabaseObjects(System.Data.SqlClient.SqlConnection objConn);

        public System.Data.DataTable getDatabaseObjects()
        {
            try
            {
                return getDatabaseObjects(objSQLConn);
            }
            catch (System.Exception e)
            {
                throw (e);
            }
        }

        public string getObjectScript(System.Data.SqlClient.SqlConnection objConn, string strObjectID)
        {
            try
            {
                string strResualt = "";
                System.Data.DataTable objDT = new System.Data.DataTable();
                System.Data.SqlClient.SqlCommand objComm;
                System.Data.SqlClient.SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();

                objComm = objConn.CreateCommand();
                objComm.CommandTimeout = 600;
                objComm.CommandText = "SELECT XTYPE FROM SYSOBJECTS WHERE ID = " + strObjectID;
                objComm.ExecuteNonQuery();
                objDA.SelectCommand = objComm;
                objDT.Clear();
                objDA.Fill(objDT);

                if(objDT.Rows.Count > 0)
                {
                    if(   objDT.Rows[0]["XTYPE"].ToString().Trim() == "FN"  
                       || objDT.Rows[0]["XTYPE"].ToString().Trim() ==  "IF" 
                       || objDT.Rows[0]["XTYPE"].ToString().Trim() ==  "TF")
                    {
                        AppClass.MakeDBFuncViewProcSQLCls objMDBFVP = new AppClass.MakeDBFuncViewProcSQLCls();
                        strResualt = objMDBFVP.getObjectScript(objConn, "", strObjectID, "1");
                    }
                    if(objDT.Rows[0]["XTYPE"].ToString().Trim() == "V")
                    {
                        AppClass.MakeDBFuncViewProcSQLCls objMDBFVP = new AppClass.MakeDBFuncViewProcSQLCls();
                        strResualt = objMDBFVP.getObjectScript(objConn, "", strObjectID, "2");
                    }
                    if(objDT.Rows[0]["XTYPE"].ToString().Trim() == "P")
                    {
                        AppClass.MakeDBFuncViewProcSQLCls objMDBFVP = new AppClass.MakeDBFuncViewProcSQLCls();
                        strResualt = objMDBFVP.getObjectScript(objConn, "", strObjectID, "3");
                    }
                }

                return strResualt;
            }
            catch (System.Exception e)
            {
                throw (e);
            }
        }

        public string getObjectScript(string strObjectID)
        {
            return getObjectScript(this.objSQLConn, strObjectID);
        }
        
    }
}
