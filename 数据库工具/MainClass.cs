using System;
using System.Windows;
using System.Windows.Forms ;
using System.IO;

namespace Upgrade
{
	/// <summary>
	/// MainClass 的摘要说明。
	/// </summary>
	public class MainClass
	{
        public static string strStartType = "";

		public MainClass()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        public string _sAppNO = "";
        public string _sSQLConn = "";

        /*
         *      _sAppNO：
         *      U8DBINFO     打开数据量与数据信息
         *      DBREINDEX    整理索引与统计信息
         *      DBSHRINK     收缩数据库
         *      U8DBPROC     数据库进程与阻塞信息
         */



        /// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main(String[] args) 
		{
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (DateTime.Now.CompareTo(new DateTime(2015, 7, 1)) >= 0)
            {
                Application.Exit();
            }


			try
			{				
				if(args.Length > 0)
				{
                    MainClass objMain = new MainClass();
                    string sInstance = ""; string sDatabase = ""; string sUser = ""; string sPassword = "";

					strStartType = args[0].Trim();
                    objMain.SplitConnString(args[1].Trim(), ref sInstance, ref sDatabase, ref sUser, ref sPassword);
                                        
                    if (strStartType.Trim() == "DBSHRINK")
                    {
                        objMain.Show_DBShrinkFile(sInstance, sPassword);
                    }

                    if (strStartType.Trim() == "DBREINDEX")
                    {
                        objMain.Show_DBReIndexStatics(sInstance, sPassword);
                    }

                    if (strStartType.Trim() == "U8DBINFO")
                    {
                        objMain.Show_U8DBInfo(sInstance, sPassword);
                    }

                    if (strStartType.Trim() == "U8DBPROC")
                    {
                        objMain.Show_DBProcessesBlocked(args[1].Trim(), sInstance, sPassword);
                    }
				}
				else
				{
					System.Xml.XmlDocument objXMLDom = new System.Xml.XmlDocument();
                    Upgrade.DBMainForm objMainForm = new Upgrade.DBMainForm();
					string strAppPath;
			
					strAppPath = System.Windows.Forms.Application.StartupPath; 
					objXMLDom.Load(strAppPath + "\\SystemConfig.xml");

                    objMainForm.objXMLDom = objXMLDom;

					Application.Run(objMainForm);

                    return;
                }

                #region 打开自带系统登录界面



                if (strStartType == "2")
                {
                    System.Xml.XmlDocument objXMLDom = new System.Xml.XmlDocument();
                    Upgrade.MainForm objMainForm = new Upgrade.MainForm();
                    string strAppPath;

                    strAppPath = System.Windows.Forms.Application.StartupPath;
                    objXMLDom.Load(strAppPath + "\\SystemConfig.xml");

                    objMainForm.objXMLDom = objXMLDom;
                    objMainForm.intUseDRPUpgrade = args.Length > 1 ? int.Parse(args[1]) : 0;
                    //objMainForm.intUseDRPUpgrade = 1;

                    Application.Run(objMainForm);
                }

                #region 分销升级脚本专用自动生成数据库对象
                if (strStartType == "1")
				{
					System.Xml.XmlDocument objXMLDom = new System.Xml.XmlDocument();
					Upgrade.UpGradeApp.UpdateDBObject2TemplateStartForm objMainForm = new Upgrade.UpGradeApp.UpdateDBObject2TemplateStartForm();

					objMainForm.strServer = args[1].Trim();
					objMainForm.strUser = args[2].Trim();
					objMainForm.strPW = args[3].Trim();
					objMainForm.strVer = args[4].Trim();
					objMainForm.strTemplateConn = "";

					Application.Run(objMainForm);

					bool exist = true;
					if ( !File.Exists( Application.StartupPath+"\\BuildSql.Log" ) )
					{
						exist = false;
					}
					FileStream fs = null;
					if ( exist )
						fs = new FileStream(Application.StartupPath+"\\BuildSql.Log",FileMode.Append);
					else
						fs = new FileStream(Application.StartupPath+"\\BuildSql.Log",FileMode.OpenOrCreate);
					StreamWriter sw = new StreamWriter(fs);
					sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"：生建SQL脚本完成！");
					sw.Close();
					fs.Close();
                }
                #endregion

                #endregion // 打开自带系统登录界面
            }
			catch(System.Exception objExp)
			{
				System.Windows.Forms.MessageBox.Show("系统错误。" + "\n" + objExp.Message,"提示",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Stop);
				System.Windows.Forms.Application.Exit();
			}

            
		}


        public string SplitConnString(string strInputConn, ref string sInstance, ref string sDatabase, ref string sUser, ref string sPassword)
        {
            string[] sConnArr = null;

            sConnArr = strInputConn.Split(new char[] { ';' }, StringSplitOptions.None);

            if (sConnArr != null)
            {
                for (int i = 0; i < sConnArr.Length; i++)
                {
                    if (sConnArr[i].Trim().IndexOf("data source=") >= 0)
                    {
                        sInstance = sConnArr[i].Trim().Substring(sConnArr[i].Trim().IndexOf("=") + 1);
                    }
                    if (sConnArr[i].Trim().IndexOf("user id=") >= 0)
                    {
                        sUser = sConnArr[i].Trim().Substring(sConnArr[i].Trim().IndexOf("=") + 1);
                    }
                    if (sConnArr[i].Trim().IndexOf("password=") >= 0)
                    {
                        sPassword = sConnArr[i].Trim().Substring(sConnArr[i].Trim().IndexOf("=") + 1);
                    }
                    if (sConnArr[i].Trim().IndexOf("initial catalog=") >= 0)
                    {
                        sDatabase = sConnArr[i].Trim().Substring(sConnArr[i].Trim().IndexOf("=") + 1);
                    }
                }
            }

            return strInputConn;
        }




        public void Show_DBShrinkFile(string strInstance, string strPassword)
        {
            try
            {                
                Apps.frmDBShrinkFile objForm = new Apps.frmDBShrinkFile();
                objForm.strInstance = strInstance;
                objForm.strUser = "SA";
                objForm.strPassword = strPassword;
                objForm.blnIntegrated = false;

                Application.Run(objForm);
            }
            catch (System.Exception Exp)
            {
                MessageBox.Show(Exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        public void Show_DBReIndexStatics(string strInstance, string strPassword)
        {
            try
            {
                Apps.DBReIndexandStatisticsFrom objForm = new Apps.DBReIndexandStatisticsFrom();
                objForm.strInstance = strInstance;
                objForm.strUser = "SA";
                objForm.strPassword = strPassword;
                objForm.blnIntegrated = false;

                Application.Run(objForm);
            }
            catch (System.Exception Exp)
            {
                MessageBox.Show(Exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public void Show_U8DBInfo(string strInstance, string strPassword)
        {
            try
            {
                Apps.DBAndTableInfoForm objForm = new Apps.DBAndTableInfoForm();
                objForm.strInstance = strInstance;
                objForm.strUser = "SA";
                objForm.strPassword = strPassword;
                objForm.blnIntegrated = false;
                objForm.intCareU8DBInfo = 1;
                objForm.intCheckU8DB = 1;
                objForm.intInComeType = 1;

                Application.Run(objForm);                
            }
            catch (System.Exception Exp)
            {
                MessageBox.Show(Exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        public void Show_DBProcessesBlocked(string strConnection, string strInstance, string strPassword)
        {
            try
            {
                Apps.ProcessBlockedForm objForm = new Apps.ProcessBlockedForm();
                objForm.strConnection = strConnection;
                objForm.strInstance = strInstance;

                Application.Run(objForm);
            }
            catch (System.Exception Exp)
            {
                MessageBox.Show(Exp.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        


	}
}
