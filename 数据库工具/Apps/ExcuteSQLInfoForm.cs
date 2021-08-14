using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Upgrade.Apps
{
    public partial class ExcuteSQLInfoForm : Form
    {
        public string strConnection = "";
        public string strInstance = "";
        public System.Data.SqlClient.SqlConnection objSQLConnect = null;

        public ExcuteSQLInfoForm()
        {
            InitializeComponent();
        }

        private void btnExcute_Click(object sender, EventArgs e)
        {
            System.DateTime dtPreTime = System.DateTime.Now;
            System.DateTime dtNowTime;
            System.TimeSpan tsMarginTime;

            System.Data.DataTable objDT = new DataTable();
            System.Data.SqlClient.SqlDataReader objSDR;
            System.Data.SqlClient.SqlDataAdapter objSQLDA = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.SqlClient.SqlCommand objSQLComm = new System.Data.SqlClient.SqlCommand();
            System.Data.SqlClient.SqlCommand objSQLComm1 = new System.Data.SqlClient.SqlCommand();

            String strInfo = "";

            try
            {
                objSQLConnect = new System.Data.SqlClient.SqlConnection();

                objSQLConnect.ConnectionString = this.strConnection + "Pooling=false;Max Pool Size=1;";

                dtNowTime = DateTime.Now;
                dtPreTime = System.DateTime.Now;
                objSQLConnect.Open();
                tsMarginTime = System.DateTime.Now - dtPreTime;

                if (objSQLConnect.State != System.Data.ConnectionState.Open)
                {
                    System.Exception objExp = new Exception("连接数据库失败。");
                    strConnection = "";
                    throw (objExp);
                }

                strInfo = strInfo + "打开数据库链接时间：" + ((double)((double)tsMarginTime.TotalMilliseconds / 1000.0)).ToString() + " 秒。\r\n\r\n";

                if (this.cbExcuteSQL.Text.Trim() != "")
                {
                    objSQLComm1 = this.objSQLConnect.CreateCommand();
                    objSQLComm1.CommandTimeout = 600;

                    objSQLComm1.CommandText = this.cbExcuteSQL.Text;
                    dtPreTime = System.DateTime.Now;
                    objSDR = objSQLComm1.ExecuteReader();

                    tsMarginTime = System.DateTime.Now - dtPreTime;
                    strInfo = strInfo + "服务器端游标(SQLDataReader)执行时间：" + ((double)((double)tsMarginTime.TotalMilliseconds / 1000.0)).ToString() + " 秒。\r\n\r\n";
                    objSDR.Close();

                    objSQLComm = this.objSQLConnect.CreateCommand();
                    objSQLComm.CommandTimeout = 600;
                    objSQLComm.CommandText = this.cbExcuteSQL.Text;

                    dtPreTime = System.DateTime.Now;
                    objSQLComm.ExecuteNonQuery();

                    tsMarginTime = System.DateTime.Now - dtPreTime;
                    strInfo = strInfo + "执行SQL(SqlCommand)时间：" + ((double)((double)tsMarginTime.TotalMilliseconds / 1000.0)).ToString() + " 秒。\r\n\r\n";

                    objDT.Clear();
                    objDT.TableName = "SQLExcuteInfo";

                    dtPreTime = System.DateTime.Now;
                    objSQLDA.SelectCommand = objSQLComm;
                    objSQLDA.Fill(objDT);
                    tsMarginTime = System.DateTime.Now - dtPreTime;
                    strInfo = strInfo + "得到数据(DataTable)时间：" + ((double)((double)tsMarginTime.TotalMilliseconds / 1000.0)).ToString() + " 秒。\r\n\r\n";

                    dtPreTime = System.DateTime.Now;
                    this.dgData.DataSource = objDT;
                    this.dgData.Refresh();
                    tsMarginTime = System.DateTime.Now - dtPreTime;
                    strInfo = strInfo + "列表控件展示时间：" + ((double)((double)tsMarginTime.TotalMilliseconds / 1000.0)).ToString() + " 秒。\r\n\r\n";

                    tsMarginTime = System.DateTime.Now - dtNowTime;
                    strInfo = strInfo + "总运行时间：" + ((double)((double)tsMarginTime.TotalMilliseconds / 1000.0)).ToString() + " 秒。\r\n\r\n";

                    this.tbInfo.Text = strInfo;

                    objSDR = null;
                    objSQLConnect.Close();
                    objSQLConnect = null;
                }

            }
            catch (System.Exception E)
            {
                objSQLConnect = null;
                throw (E);
            }
        }

        private void ExcuteSQLInfoForm_Load(object sender, EventArgs e)
        {

        }
    }
}