using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Upgrade.Apps
{
    public partial class BatchMakeDBObjectScriptForm : Form
    {
        public string strInstance = "";
        public string strDBName = "";
        public string strLoginName = "sa";
        public string strPassword = "";
        public bool blnIntegratedSecurity = false;

        private string strSQLVersion = "200X";

        public BatchMakeDBObjectScriptForm()
        {
            InitializeComponent();
        }

        private void BatchMakeDBObjectScriptForm_Load(object sender, EventArgs e)
        {
            //设置登录信息
            this.cbInstance.Text = this.strInstance;
            this.cbDatabase.Text = this.strDBName;
            this.tbLoginName.Text = this.strLoginName;
            this.tbPassword.Text = this.strPassword;
            this.cbIntegrated.Checked = blnIntegratedSecurity;
            this.panelProgress.Visible = false;

            if (blnIntegratedSecurity) { this.tbLoginName.ReadOnly = true; this.tbPassword.ReadOnly = true; }
            else { this.tbLoginName.ReadOnly = false; this.tbPassword.ReadOnly = false; }

            this.BatchMakeDBObjectScriptForm_Resize(null, null);
        }

        private void BatchMakeDBObjectScriptForm_Resize(object sender, EventArgs e)
        {
            this.gbCondition.Height = this.Height - 25;
            this.tcSelectObjects.Height = this.gbCondition.Height - 295;
            this.utcScripts.Height = this.Height - 35;
            this.utcScripts.Width = this.Width - 478;

            this.panelProgress.Left = 90;
            this.panelProgress.Top = this.Width / 3 - 45;
            this.panelProgress.Width = this.Width - 180;
            this.upbAllType.Width = this.panelProgress.Width - 8;
            this.upbObjects.Width = this.panelProgress.Width - 8;
            this.btnCloseProgressBar.Left= this.panelProgress.Width - 27; 
        }


        private void cbIntegrated_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbIntegrated.Checked) { this.tbLoginName.ReadOnly = true; this.tbPassword.ReadOnly = true; }
            else { this.tbLoginName.ReadOnly = false; this.tbPassword.ReadOnly = false; }

        }

        private void btnCloseProgressBar_Click(object sender, EventArgs e)
        {
            this.panelProgress.Visible = false;
        }




        private void btnLink_Click(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlConnection objSQLConnect = new System.Data.SqlClient.SqlConnection();

            try
            {
                this.upbAllType.Maximum = 6;
                this.upbObjects.Maximum = 100; this.upbObjects.Value = 100; this.upbObjects.Text = "......";
                this.upbObjects.Refresh();
                this.panelProgress.Refresh();

                this.panelProgress.Visible = true;
                this.panelProgress.Refresh();

                //打开数据库连接
                AppClass.PublicDBCls objPDBCls = new Upgrade.AppClass.PublicDBCls();
                objSQLConnect = objPDBCls.getSQLConnection(this.cbInstance.Text, this.cbDatabase.Text, this.tbLoginName.Text, this.tbPassword.Text, this.cbIntegrated.Checked);
                this.upbAllType.Value = 1; this.upbAllType.Text = "已连接数据库......[Formatted]"; this.upbAllType.Refresh();

                //数据库版本
                this.strSQLVersion = this.getSQLServerVersion(objSQLConnect);
                this.upbAllType.Value = 2; this.upbAllType.Text = "已获得数据库版本......[Formatted]"; this.upbAllType.Refresh();

                //列举查询索引
                this.ListTableIndexs(objSQLConnect, this.tbIndexName.Text.Trim(), this.strSQLVersion);
                this.upbAllType.Value = 3; this.upbAllType.Text = "已列举查询索引......[Formatted]"; this.upbAllType.Refresh();

                //列举存储过程
                this.ListProcedures(objSQLConnect, this.tbProduceName.Text.Trim(), strSQLVersion);
                this.upbAllType.Value = 4; this.upbAllType.Text = "已列举存储过程......[Formatted]"; this.upbAllType.Refresh();

                //列举自定义函数
                this.ListFunctions(objSQLConnect, this.tbFunctionName.Text.Trim(), this.strSQLVersion);
                this.upbAllType.Value = 5; this.upbAllType.Text = "已列举自定义函数......[Formatted]"; this.upbAllType.Refresh();

                //列举视图
                this.ListViews(objSQLConnect, this.tbViewName.Text.Trim(), this.strSQLVersion);
                this.upbAllType.Value = 6; this.upbAllType.Text = "已列举自定义函数......[Formatted]"; this.upbAllType.Refresh();

                //列举触发器
                this.ListTrigers(objSQLConnect, this.tbViewName.Text.Trim(), this.strSQLVersion);
                this.upbAllType.Value = 6; this.upbAllType.Text = "已列举数据表触发器......[Formatted]"; this.upbAllType.Refresh();

                //列举数据库
                this.ListDatabases(objSQLConnect, strSQLVersion);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (objSQLConnect != null) { objSQLConnect.Close(); objSQLConnect = null; }
            }
        }



        private void btnMakeScript_Click(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlConnection objSQLConnect = new System.Data.SqlClient.SqlConnection();

            try
            {
                System.Text.StringBuilder sbResult = new System.Text.StringBuilder();
                System.Text.StringBuilder sbSingleResult = new System.Text.StringBuilder();

                this.upbAllType.Maximum = 6;
                this.panelProgress.Visible = true;
                this.panelProgress.Refresh();

                //打开数据库连接
                AppClass.PublicDBCls objPDBCls = new Upgrade.AppClass.PublicDBCls();
                objSQLConnect = objPDBCls.getSQLConnection(this.cbInstance.Text, this.cbDatabase.Text, this.tbLoginName.Text, this.tbPassword.Text, this.cbIntegrated.Checked);
                this.upbAllType.Value = 1; this.upbAllType.Text = "已连接数据库，正在生成索引脚本......[Formatted]"; this.upbAllType.Refresh();

                //生成索引脚本
                sbSingleResult = new System.Text.StringBuilder();
                this.MakeIndexsScript(objSQLConnect, ref sbSingleResult, this.strSQLVersion, this.cbIndexDrop.Checked);
                sbResult.Append(sbSingleResult.ToString());
                this.rtbIndexScript.Text = sbSingleResult.ToString();
                this.upbAllType.Value = 2; this.upbAllType.Text = "已生成索引脚本，正在生成自定义函数脚本......[Formatted]"; this.upbAllType.Refresh();

                //生成自定义函数脚本
                sbSingleResult = new System.Text.StringBuilder();
                this.MakeFunctionsScript(objSQLConnect, ref sbSingleResult, this.strSQLVersion);
                sbResult.Append(sbSingleResult.ToString());
                this.rtbFunctionScript.Text = sbSingleResult.ToString();
                this.upbAllType.Value = 3; this.upbAllType.Text = "已生成自定义函数脚本，正在生成视图脚本......[Formatted]"; this.upbAllType.Refresh();

                //生成视图脚本
                sbSingleResult = new System.Text.StringBuilder();
                this.MakeViewsScript(objSQLConnect, ref sbSingleResult, this.strSQLVersion);
                sbResult.Append(sbSingleResult.ToString());
                this.rtbViewScript.Text = sbSingleResult.ToString();
                this.upbAllType.Value = 4; this.upbAllType.Text = "已生成视图脚本，正在生成存储过程脚本......[Formatted]"; this.upbAllType.Refresh();

                //生成存储过程脚本
                sbSingleResult = new System.Text.StringBuilder();
                this.MakeProceduresScript(objSQLConnect, ref sbSingleResult, this.strSQLVersion);
                sbResult.Append(sbSingleResult.ToString());
                this.rtbProceduerScript.Text = sbSingleResult.ToString();
                this.upbAllType.Value = 5; this.upbAllType.Text = "已生成存储过程脚本，正在生成触发器脚本......[Formatted]"; this.upbAllType.Refresh();

                //生成触发器脚本
                sbSingleResult = new System.Text.StringBuilder();
                this.MakeTrigerScript(objSQLConnect, ref sbSingleResult, this.strSQLVersion);
                sbResult.Append(sbSingleResult.ToString());
                this.rtbTriger.Text = sbSingleResult.ToString();
                this.upbAllType.Value = 6; this.upbAllType.Text = "已生成触发器脚本......[Formatted]"; this.upbAllType.Refresh();

                this.rtbScripts.Text = sbResult.ToString();
            }
            catch (Exception E)
            {
                throw E;
            }
            finally
            {
                if (objSQLConnect != null) { objSQLConnect.Close(); objSQLConnect = null; }
            }
        }


        #region  列举数据库对象

        private string getSQLServerVersion(System.Data.SqlClient.SqlConnection objSQLConnect)
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

                if (objDT.Rows[0][0].ToString().IndexOf("Microsoft SQL Server  2000", 0) < 0)
                {
                    return "200X";
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

        /// <summary>
        /// 列举索引
        /// </summary>
        /// <param name="objSQLConnect"></param>
        /// <param name="strLikeIndexName"></param>
        /// <param name="strSQLVersion"></param>
        private void ListTableIndexs(System.Data.SqlClient.SqlConnection objSQLConnect, string strLikeIndexName, string strSQLVersion)
        {
            System.Data.DataTable objDT = new System.Data.DataTable();
            System.Data.SqlClient.SqlCommand objComm = null;
            System.Data.SqlClient.SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();

            try
            {
                string strScript = "";
                objComm = objSQLConnect.CreateCommand();
                objComm.CommandTimeout = 1200;
                
                objDT.Clear();

                if (strSQLVersion == "200X")
                {
                    strScript = @"SELECT DISTINCT O.NAME AS TableName, IX.[OBJECT_ID], IX.[NAME] AS IndexName, IX.[INDEX_ID] 
                                  FROM sys.objects O INNER JOIN  sys.indexes IX ON IX.[OBJECT_ID] = O.[OBJECT_ID]                    
                                  WHERE O.TYPE = 'U' AND IX.[INDEX_ID] >= 1 " + (strLikeIndexName != "" ? @"AND IX.[Name] LIKE '%" + strLikeIndexName+ "%' " : "") +
                                 "ORDER BY O.NAME, IX.[NAME]";
                }
                else //SQL 2000 的处理
                {
                    strScript = @"SELECT TBL.[name] AS TableName, TBL.ID AS [OBJECT_ID], I.[Name] AS IndexName, I.indid AS INDEX_ID
                                  FROM SYSOBJECTS AS TBL INNER JOIN SYSUSERS AS stbl ON stbl.uid = tbl.uid INNER JOIN SYSINDEXES AS i ON i.indid > 0 and i.indid < 255  AND i.id=tbl.id AND '1' != INDEXPROPERTY(i.id,i.name,N'IsStatistics') AND '1' != INDEXPROPERTY(i.id,i.name,N'IsHypothetical')
                                  WHERE TBL.type='U'" + (strLikeIndexName != "" ? @"AND I.[Name] LIKE '%" + strLikeIndexName+ "%' " : "") +
                                 "ORDER BY tbl.[name] , I.[Name] ";
                }
                objComm.CommandText = strScript;
                objComm.ExecuteNonQuery();
                objDA.SelectCommand = objComm;
                objDA.Fill(objDT);

                this.ulvIndexs.Items.Clear();
                Infragistics.Win.UltraWinListView.UltraListViewItem objULIItem = null;
                for (int i = 0; i < objDT.Rows.Count; i++)
                {
                    objULIItem = new Infragistics.Win.UltraWinListView.UltraListViewItem();
                    objULIItem.Key = objDT.Rows[i]["OBJECT_ID"].ToString().Trim() + "." + objDT.Rows[i]["INDEX_ID"].ToString().Trim();
                    objULIItem.Value = objDT.Rows[i]["TableName"].ToString().Trim() + "." + objDT.Rows[i]["IndexName"].ToString().Trim();
                    objULIItem.Appearance.Image = Upgrade.Properties.Resources.DB_Index;

                    this.ulvIndexs.Items.Add(objULIItem);
                }                
            }
            catch(Exception e)
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

       /// <summary>
       /// 列举存储过程
       /// </summary>
       /// <param name="objSQLConnect"></param>
       /// <param name="strLikeIndexName"></param>
       /// <param name="strSQLVersion"></param>
        private void ListProcedures(System.Data.SqlClient.SqlConnection objSQLConnect, string strLikeProcedureName, string strSQLVersion)
        {
            System.Data.DataTable objDT = new System.Data.DataTable();
            System.Data.SqlClient.SqlCommand objComm = null;
            System.Data.SqlClient.SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();

            try
            {
                string strScript = "";
                objComm = objSQLConnect.CreateCommand();
                objComm.CommandTimeout = 1200;
                objDT.Clear();
                if (strSQLVersion == "200X")
                {                    
                    strScript = "SELECT [OBJECT_ID], [NAME] FROM sys.objects WHERE TYPE = 'P' AND is_ms_shipped = 0 " +
                                 (strLikeProcedureName != "" ? "AND [Name] LIKE '%" + strLikeProcedureName + "%' " : "") +
                                "ORDER BY [Name] ";
                }
                else //SQL 2000 的处理
                {
                    strScript = "SELECT [ID] AS [OBJECT_ID], [NAME] FROM sysobjects WHERE TYPE = 'P' AND (CAST(CASE WHEN (OBJECTPROPERTY(ID, N'IsMSShipped') = 1) THEN 1 WHEN 1 = OBJECTPROPERTY(ID, N'IsSystemTable') THEN 1 ELSE 0 END AS bit) = 0) " +
                                 (strLikeProcedureName != "" ? "AND [Name] LIKE '%" + strLikeProcedureName + "%' " : "") +
                                "ORDER BY [Name] ";
                }
                objComm.CommandText = strScript;
                objComm.ExecuteNonQuery();
                objDA.SelectCommand = objComm;
                objDA.Fill(objDT);

                this.ulvProcedures.Items.Clear();
                Infragistics.Win.UltraWinListView.UltraListViewItem objULIItem = null;
                for (int i = 0; i < objDT.Rows.Count; i++)
                {
                    objULIItem = new Infragistics.Win.UltraWinListView.UltraListViewItem();
                    objULIItem.Key = objDT.Rows[i]["OBJECT_ID"].ToString().Trim();
                    objULIItem.Value = objDT.Rows[i]["NAME"].ToString().Trim();
                    objULIItem.Appearance.Image = Upgrade.Properties.Resources.DB_Procedure;

                    this.ulvProcedures.Items.Add(objULIItem);

                    Application.DoEvents();
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

        /// <summary>
        /// 列举自定义函数
        /// </summary>
        /// <param name="objSQLConnect"></param>
        /// <param name="strLikeProcedureName"></param>
        /// <param name="strSQLVersion"></param>
        private void ListFunctions(System.Data.SqlClient.SqlConnection objSQLConnect, string strLikeFunctionName, string strSQLVersion)
        {
            System.Data.DataTable objDT = new System.Data.DataTable();
            System.Data.SqlClient.SqlCommand objComm = null;
            System.Data.SqlClient.SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();

            try
            {
                string strScript = "";
                objComm = objSQLConnect.CreateCommand();
                objComm.CommandTimeout = 1200;

                objDT.Clear();
                if (strSQLVersion == "200X")
                {
                    strScript = "select [OBJECT_ID], [NAME] FROM SYS.OBJECTS WHERE TYPE IN (N'FN', N'IF', N'TF') " +
                                 (strLikeFunctionName != "" ? "AND [Name] LIKE '%" + strLikeFunctionName + "%' " : "") +
                                "ORDER BY [Name] ";
                }
                else //SQL 2000 的处理
                {
                    strScript = "SELECT [ID] AS [OBJECT_ID], [NAME]  FROM SYSOBJECTS WHERE TYPE IN (N'FN', N'IF', N'TF') AND CAST(CASE WHEN (OBJECTPROPERTY(id, N'IsMSShipped') = 1) THEN 1 WHEN 1 = OBJECTPROPERTY(id, N'IsSystemTable') THEN 1 ELSE 0 END AS bit) = 0 " +
                                 (strLikeFunctionName != "" ? "AND [Name] LIKE '%" + strLikeFunctionName + "%' " : "") +
                                "ORDER BY [Name] ";

                }
                objComm.CommandText = strScript;
                objComm.ExecuteNonQuery();
                objDA.SelectCommand = objComm;
                objDA.Fill(objDT);

                this.ulvFunctions.Items.Clear();
                Infragistics.Win.UltraWinListView.UltraListViewItem objULIItem = null;
                for (int i = 0; i < objDT.Rows.Count; i++)
                {
                    objULIItem = new Infragistics.Win.UltraWinListView.UltraListViewItem();
                    objULIItem.Key = objDT.Rows[i]["OBJECT_ID"].ToString().Trim();
                    objULIItem.Value = objDT.Rows[i]["NAME"].ToString().Trim();
                    objULIItem.Appearance.Image = Upgrade.Properties.Resources.DB_Functions;

                    this.ulvFunctions.Items.Add(objULIItem);

                    Application.DoEvents();
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

        /// <summary>
        /// 列举视图
        /// </summary>
        /// <param name="objSQLConnect"></param>
        /// <param name="strLikeFunctionName"></param>
        /// <param name="strSQLVersion"></param>
        private void ListViews(System.Data.SqlClient.SqlConnection objSQLConnect, string strLikeViewName, string strSQLVersion)
        {
            System.Data.DataTable objDT = new System.Data.DataTable();
            System.Data.SqlClient.SqlCommand objComm = null;
            System.Data.SqlClient.SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();

            try
            {
                string strScript = "";
                objComm = objSQLConnect.CreateCommand();
                objComm.CommandTimeout = 1200;
                objDT.Clear(); 
                
                if (strSQLVersion == "200X")
                {
                    strScript = "SELECT [object_ID], [NAME] FROM sys.Objects WHERE [TYPE] = 'V' " +
                                 (strLikeViewName != "" ? "AND [Name] LIKE '%" + strLikeViewName + "%' " : "") +
                                "ORDER BY [Name] ";
                }
                else //SQL 2000 的处理
                {
                    strScript = "SELECT ID AS [object_ID], [NAME] FROM SYSOBJECTS WHERE [TYPE] = 'V' AND CAST(CASE WHEN (OBJECTPROPERTY(id, N'IsMSShipped') = 1) THEN 1 WHEN 1 = OBJECTPROPERTY(id, N'IsSystemTable') THEN 1 ELSE 0 END AS bit) = 0 " +
                                 (strLikeViewName != "" ? "AND [Name] LIKE '%" + strLikeViewName + "%' " : "") +
                                "ORDER BY [Name] ";
                }
                objComm.CommandText = strScript;
                objComm.ExecuteNonQuery();
                objDA.SelectCommand = objComm;
                objDA.Fill(objDT);

                this.ulvViews.Items.Clear();
                Infragistics.Win.UltraWinListView.UltraListViewItem objULIItem = null;
                for (int i = 0; i < objDT.Rows.Count; i++)
                {
                    objULIItem = new Infragistics.Win.UltraWinListView.UltraListViewItem();
                    objULIItem.Key = objDT.Rows[i]["OBJECT_ID"].ToString().Trim();
                    objULIItem.Value = objDT.Rows[i]["NAME"].ToString().Trim();
                    objULIItem.Appearance.Image = Upgrade.Properties.Resources.Table_1;

                    this.ulvViews.Items.Add(objULIItem);

                    Application.DoEvents();
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

        

        /// <summary>
        /// 列举数据表触发器
        /// </summary>
        /// <param name="objSQLConnect"></param>
        /// <param name="strSQLVersion"></param>
        private void ListTrigers(System.Data.SqlClient.SqlConnection objSQLConnect, string strLikeTrigerName, string strSQLVersion)
        {
            System.Data.DataTable objDT = new System.Data.DataTable();
            System.Data.SqlClient.SqlCommand objComm = null;
            System.Data.SqlClient.SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();

            try
            {
                string strScript = "";
                objComm = objSQLConnect.CreateCommand();
                objComm.CommandTimeout = 1200;
                
                objDT.Clear();

                if (strSQLVersion == "200X")
                {
                    strScript = @"SELECT PO.[Name] + '.' + O.[NAME] AS NAME, O.OBJECT_ID FROM sys.objects O INNER JOIN sys.objects PO ON O.Parent_object_id = PO.object_id
                                  WHERE O.type = 'TR'  " + (strLikeTrigerName != "" ? @"AND O.[Name] LIKE '%" + strLikeTrigerName + "%' " : "") +
                                 "ORDER BY NAME";
                }
                else //SQL 2000 的处理
                {
                    strScript = @"SELECT PO.[Name] + '.' + O.[NAME] AS NAME, O.ID FROM sysobjects O INNER JOIN sysobjects PO ON O.Parent_obj = PO.id
                                  WHERE O.xtype = 'TR'  " + (strLikeTrigerName != "" ? @"AND O.[Name] LIKE '%" + strLikeTrigerName + "%' " : "") +
                                 "ORDER BY NAME";
                }
                objComm.CommandText = strScript;
                objComm.ExecuteNonQuery();
                objDA.SelectCommand = objComm;
                objDA.Fill(objDT);

                this.ulvTriger.Items.Clear();
                Infragistics.Win.UltraWinListView.UltraListViewItem objULIItem = null;
                for (int i = 0; i < objDT.Rows.Count; i++)
                {
                    objULIItem = new Infragistics.Win.UltraWinListView.UltraListViewItem();
                    objULIItem.Key = objDT.Rows[i]["OBJECT_ID"].ToString().Trim();
                    objULIItem.Value = objDT.Rows[i]["NAME"].ToString().Trim();
                    objULIItem.Appearance.Image = Upgrade.Properties.Resources.DB_Index;

                    this.ulvTriger.Items.Add(objULIItem);
                }                
            }
            catch(Exception e)
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


        /// <summary>
        /// 列举数据库
        /// </summary>
        /// <param name="objSQLConnect"></param>
        /// <param name="strLikeViewName"></param>
        /// <param name="strSQLVersion"></param>
        private void ListDatabases(System.Data.SqlClient.SqlConnection objSQLConnect, string strSQLVersion)
        {
            System.Data.DataTable objDT = new System.Data.DataTable();
            System.Data.SqlClient.SqlCommand objComm = null;
            System.Data.SqlClient.SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();

            try
            {
                string strScript = "";
                objComm = objSQLConnect.CreateCommand();
                objComm.CommandTimeout = 1200;
                objDT.Clear();

                if (strSQLVersion == "200X")
                {
                    strScript = "SELECT NAME, Database_id FROM MASTER.sys.Databases ORDER BY NAME ";
                }
                else //SQL 2000 的处理
                {
                    strScript = "SELECT NAME, DBID AS Database_id FROM MASTER.dbo.sysDatabases ORDER BY NAME ";
                }
                objComm.CommandText = strScript;
                objComm.ExecuteNonQuery();
                objDA.SelectCommand = objComm;
                objDA.Fill(objDT);

                this.cbDatabase.Items.Clear();
                for (int i = 0; i < objDT.Rows.Count; i++)
                {
                    this.cbDatabase.Items.Add(objDT.Rows[i]["NAME"].ToString().Trim());

                    Application.DoEvents();
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

   

        #endregion

        #region 导出数据库脚本

        private void MakeIndexsScript(System.Data.SqlClient.SqlConnection objSQLConnect, ref System.Text.StringBuilder sbResult, string strSQLServerVersion, bool bDropRebuild)
        {
            if (this.ulvIndexs.CheckedItems.Count == 0) return;

            string strTableName = "";
            string strIndexName = "";
            int intDotIndex = 0;

            this.upbObjects.Maximum = this.ulvIndexs.CheckedItems.Count;

            AppClass.MakeSQLScriptCls objMakeScript = new Upgrade.AppClass.MakeSQLScriptCls();

            sbResult.Append("/*\r\n  =========================================================================================================================================================================================================== \r\n " +
                            "  生成选择的数据表索引脚本 \r\n  导出时间：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                            "\r\n  ===========================================================================================================================================================================================================\r\n*/\r\n");
            for (int i = 0; i < this.ulvIndexs.CheckedItems.Count; i++)
            {
                intDotIndex = this.ulvIndexs.CheckedItems[i].Text.IndexOf(".");
                strTableName = this.ulvIndexs.CheckedItems[i].Text.Substring(0, intDotIndex);
                strIndexName = this.ulvIndexs.CheckedItems[i].Text.Substring(intDotIndex + 1, this.ulvIndexs.CheckedItems[i].Text.Length - intDotIndex - 1);

                sbResult.Append(objMakeScript.getObjectScript_Index(objSQLConnect, strTableName, strIndexName, bDropRebuild, strSQLServerVersion));

                this.upbObjects.Value = i + 1;
                this.upbObjects.Text = "正在生成数据表索引脚本：" + this.ulvIndexs.CheckedItems[i].Text + "  ...... [Value]/[Maximum] ......";
                this.upbObjects.Refresh();

                System.Windows.Forms.Application.DoEvents();
            }
            sbResult.Append("/*\r\n  ===========================================================================================================================================================================================================  \r\n*/\r\n\r\n\r\n\r\n");
        }

        private void MakeFunctionsScript(System.Data.SqlClient.SqlConnection objSQLConnect, ref System.Text.StringBuilder sbResult, string strSQLServerVersion)
        {
            if (this.ulvFunctions.CheckedItems.Count == 0) return;

            AppClass.MakeSQLScriptCls objMakeScript = new Upgrade.AppClass.MakeSQLScriptCls();

            this.upbObjects.Maximum = this.ulvFunctions.CheckedItems.Count;

            sbResult.Append("/*\r\n  =========================================================================================================================================================================================================== \r\n " + 
                            "  生成选择的自定义函数脚本 \r\n  导出时间：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + 
                              "\r\n  ===========================================================================================================================================================================================================\r\n*/\r\n");
            for (int i = 0; i < this.ulvFunctions.CheckedItems.Count; i++)
            {
                sbResult.Append(objMakeScript.getObjectScript_VFP(objSQLConnect, this.ulvFunctions.CheckedItems[i].Text, this.ulvFunctions.CheckedItems[i].Key, "1", strSQLServerVersion));

                this.upbObjects.Value = i + 1;
                this.upbObjects.Text = "正在生成自定义函数脚本：" + this.ulvFunctions.CheckedItems[i].Text + "  ...... [Value]/[Maximum] ......";
                this.upbObjects.Refresh();

                System.Windows.Forms.Application.DoEvents();
            }
            sbResult.Append("/*\r\n  ===========================================================================================================================================================================================================  \r\n*/\r\n\r\n\r\n\r\n");
        }

        private void MakeViewsScript(System.Data.SqlClient.SqlConnection objSQLConnect, ref System.Text.StringBuilder sbResult, string strSQLServerVersion)
        {
            if (this.ulvViews.CheckedItems.Count == 0) return;

            AppClass.MakeSQLScriptCls objMakeScript = new Upgrade.AppClass.MakeSQLScriptCls();

            this.upbObjects.Maximum = this.ulvViews.CheckedItems.Count;
            
            sbResult.Append("/*\r\n  =========================================================================================================================================================================================================== \r\n " +
                            "  生成选择的视图脚本 \r\n  导出时间：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                            "\r\n  ===========================================================================================================================================================================================================\r\n*/\r\n");
            for (int i = 0; i < this.ulvViews.CheckedItems.Count; i++)
            {
                sbResult.Append(objMakeScript.getObjectScript_VFP(objSQLConnect, this.ulvViews.CheckedItems[i].Text, this.ulvViews.CheckedItems[i].Key, "2", strSQLServerVersion));

                this.upbObjects.Value = i + 1;
                this.upbObjects.Text = "正在生成视图脚本：" + this.ulvViews.CheckedItems[i].Text + "  ...... [Value]/[Maximum] ......";
                this.upbObjects.Refresh();

                System.Windows.Forms.Application.DoEvents();
            }
            sbResult.Append("/*\r\n  ===========================================================================================================================================================================================================  \r\n*/\r\n\r\n\r\n\r\n");
        }

        private void MakeProceduresScript(System.Data.SqlClient.SqlConnection objSQLConnect, ref System.Text.StringBuilder sbResult, string strSQLServerVersion)
        {
            if (this.ulvProcedures.CheckedItems.Count == 0) return;

            AppClass.MakeSQLScriptCls objMakeScript = new Upgrade.AppClass.MakeSQLScriptCls();

            this.upbObjects.Maximum = this.ulvTriger.CheckedItems.Count;

            sbResult.Append("/*\r\n  =========================================================================================================================================================================================================== \r\n " +
                            "  生成选择的触发器脚本 \r\n  导出时间：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                            "\r\n  ===========================================================================================================================================================================================================\r\n*/\r\n");
            for (int i = 0; i < this.ulvProcedures.CheckedItems.Count; i++)
            {
                sbResult.Append(objMakeScript.getObjectScript_VFP(objSQLConnect, this.ulvProcedures.CheckedItems[i].Text, this.ulvProcedures.CheckedItems[i].Key, "3", strSQLServerVersion));

                this.upbObjects.Value = i + 1;
                this.upbObjects.Text = "正在生成触发器脚本：" + this.ulvProcedures.CheckedItems[i].Text + "  ...... [Value]/[Maximum] ......";
                this.upbObjects.Refresh();

                System.Windows.Forms.Application.DoEvents();
            }
            sbResult.Append("/*\r\n  ===========================================================================================================================================================================================================  \r\n*/\r\n\r\n\r\n\r\n");
        }

        private void MakeTrigerScript(System.Data.SqlClient.SqlConnection objSQLConnect, ref System.Text.StringBuilder sbResult, string strSQLServerVersion)
        {
            if (this.ulvTriger.CheckedItems.Count == 0) return;

            AppClass.MakeSQLScriptCls objMakeScript = new Upgrade.AppClass.MakeSQLScriptCls();

            this.upbObjects.Maximum = this.ulvTriger.CheckedItems.Count;

            sbResult.Append("/*\r\n  =========================================================================================================================================================================================================== \r\n " +
                            "  生成选择的触发器脚本 \r\n  导出时间：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                            "\r\n  ===========================================================================================================================================================================================================\r\n*/\r\n");
            for (int i = 0; i < this.ulvTriger.CheckedItems.Count; i++)
            {
                sbResult.Append(objMakeScript.getObjectScript_VFP(objSQLConnect, this.ulvTriger.CheckedItems[i].Text, this.ulvTriger.CheckedItems[i].Key, "3", strSQLServerVersion));

                this.upbObjects.Value = i + 1;
                this.upbObjects.Text = "正在生成触发器脚本：" + this.ulvTriger.CheckedItems[i].Text + "  ...... [Value]/[Maximum] ......";
                this.upbObjects.Refresh();

                System.Windows.Forms.Application.DoEvents();
            }
            sbResult.Append("/*\r\n  ===========================================================================================================================================================================================================  \r\n*/\r\n\r\n\r\n\r\n");
        }

        #endregion

        private void cbDatabase_DropDown(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlConnection objSQLConnect = new System.Data.SqlClient.SqlConnection();
            System.Data.DataTable objDT = null;

            try
            {
                AppClass.PublicDBCls objPDBCls = new Upgrade.AppClass.PublicDBCls();
                objSQLConnect = objPDBCls.getSQLConnection(this.cbInstance.Text, this.cbDatabase.Text, this.tbLoginName.Text, this.tbPassword.Text, this.cbIntegrated.Checked);

                AppClass.DBDatabases objDB = new AppClass.DBDatabases();
                objDT = objDB.getDatabaseObjects(objSQLConnect);

                this.cbDatabase.Items.Clear();
                for (int i = 0; i < objDT.Rows.Count; i++)
                {
                    this.cbDatabase.Items.Add(((string)objDT.Rows[i]["NAME"]).Trim());
                }
                //this.lbDatabase.SelectedIndex = 0;
            }
            catch (Exception E)
            {
                MessageBox.Show("获得指定 Microsoft SQL Server 实例数据库列表失败！/r/n" + E.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                objDT.Dispose();
                objDT = null;
            }
        }

        

        



    }
}
