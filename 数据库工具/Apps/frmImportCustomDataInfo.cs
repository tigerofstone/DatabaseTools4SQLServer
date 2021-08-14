using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using System.Transactions;


namespace Upgrade.Apps
{
    public partial class frmImportCustomDataInfo : Form
    {
        public string strServer = "LOCALHOST", strDatabase = "Master";
        public string strUser = "sa", strPassword = "sa";
        public bool blnIntegratedSecurity = true;
        public string strSQLConnection;
        
        object _oMissValue = System.Reflection.Missing.Value;
        private int _iRowMax = 10000;
        private int _iNullRowMax = 5;

        public frmImportCustomDataInfo()
        {
            InitializeComponent();
        }

        private void frmImportCustomDataInfo_Load(object sender, EventArgs e)
        {
            try
            {
                System.Data.SqlClient.SqlConnection objSQLConnection = null;
                objSQLConnection = this.GetSQLConnection();
                objSQLConnection.Close();
                objSQLConnection = null;
            }
            catch (Exception E)
            {
                MessageBox.Show("系统提示", "打开数据连接出现问题：" + E.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectExcel_Click(object sender, EventArgs e)
        {
            string strFileName = "";
            OpenFileDialog objOFD = new OpenFileDialog();

            try
            {
                objOFD.ShowHelp = false;
                objOFD.Multiselect = false;
                objOFD.CheckFileExists = true;
                objOFD.DereferenceLinks = true;
                objOFD.RestoreDirectory = true;
                objOFD.Filter = "Microsoft Excel 2000&2007&2010 文件(*.xls)|*.xls|Microsoft Excel 2007-2010 文件(*.xlsx)|*.xlsx;"; 
                objOFD.Title = "选择要导入的客户数据库信息";

                if (objOFD.ShowDialog() == DialogResult.OK)
                {
                    strFileName = objOFD.FileName;
                    this.tbFileName.Text = strFileName;

                    if (strFileName != "")
                    {
                        this.ShowExcelSheet(strFileName);
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("系统错误", ee.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                this.AnalyseExcel2Dataqbase(this.tbFileName.Text.Trim(), this.cbExcelSheet.Text.Trim());
                MessageBox.Show("导入数据成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception E)
            {
                MessageBox.Show("导入资料出错："+ E.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void ShowImportInformation(string strInformation)
        {
            this.upbProgress.Text = strInformation;
            this.upbProgress.Refresh();
        }


        //=========================================================================================================
        private System.Data.SqlClient.SqlConnection GetSQLConnection()
        {
            AppClass.PublicDBCls objPDBC = new AppClass.PublicDBCls();
            return objPDBC.getSQLConnection(this.strServer, this.strDatabase, this.strUser, this.strPassword, this.blnIntegratedSecurity, out strSQLConnection);
        }


        private void ShowExcelSheet(string strExcelFileName)
        {
            Microsoft.Office.Interop.Excel.Application objExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook objExcelWK;

            try
            {
                int intCount;
                string strSheetName;


                this.cbExcelSheet.Items.Clear();

                if (objExcelApp != null)
                {
                    objExcelWK = objExcelApp.Workbooks.Open(strExcelFileName, _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue, 
                                                                              _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue);

                    intCount = objExcelWK.Sheets.Count;
                    for (int i = 1; i <= intCount; i++)
                    {
                        strSheetName = ((Microsoft.Office.Interop.Excel._Worksheet)objExcelWK.Worksheets.Item[i]).Name;

                        this.cbExcelSheet.Items.Add(strSheetName);
                    }

                    if (intCount > 0)
                    {
                        this.cbExcelSheet.SelectedIndex = intCount - 1;
                    }

                    if (objExcelWK != null)
                    {
                        objExcelWK.Close(false, _oMissValue, _oMissValue);
                    }
                }
                else
                {
                    throw new Exception("加载 Excel 文件出错。");
                }
            }
            catch (System.Exception e)
            {
                throw new Exception("加载 Excel 文件出错：" + e.Message);
            }
            finally
            {
                objExcelApp.Workbooks.Close();

                objExcelApp = null;
                objExcelWK = null;
            }
        }

        private void AnalyseExcel2Dataqbase(string strFileName, string strSheet)
        {
            Microsoft.Office.Interop.Excel.Application objExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook objExcelWK;
            Microsoft.Office.Interop.Excel.Worksheet objExcelSheet = null;

            System.Data.SqlClient.SqlConnection objSQLConnection = null;
            System.Data.SqlClient.SqlCommand objSQLComm = null;
            System.Data.SqlClient.SqlTransaction objSQLTran = null;

            try
            {

                string strMainID = System.Guid.NewGuid().ToString();
                string _sItemID = System.Guid.NewGuid().ToString();
                string _sItemYearID = System.Guid.NewGuid().ToString();
                int intNullRowFlag = 0;
                int intMaxExcelRow = 0;

                if (objExcelApp != null)
                {
                    objExcelWK = objExcelApp.Workbooks.Open(strFileName, _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue, _oMissValue);
                    objExcelSheet = (Microsoft.Office.Interop.Excel.Worksheet)objExcelWK.Sheets[strSheet];
                }
                else
                {
                    return;
                }

                #region 把抽取数据添加到数据库中
                using (System.Transactions.TransactionScope _oTS = new TransactionScope())
                {
                    try
                    {
                        objSQLConnection = this.GetSQLConnection();
                        objSQLComm = objSQLConnection.CreateCommand();
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show("系统提示", "打开数据连接出现问题：" + E.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //objSQLTran = objSQLConnection.BeginTransaction("InsertCustomerData");
                    //objSQLComm.Transaction = objSQLTran;
                    //==============================================================================================================================================================
                    int intDatabaseNO = 0; int intCustomerName = 0; int intDBName = 0; int intU8Version = 0; int intU8AccountNO = 0;
                    //int int序号 = 0;    int int业务对象 = 0;     int int主表记录数 = 0; int int子表记录数 = 0; int int业务最早日期 = 0;
                    //int int业务最晚日期 = 0;    int int主表数据空间 = 0;     int int主表索引空间 = 0; int int子表数据空间 = 0; int int子表索引空间 = 0;

                    this.GetMainRowNum(objExcelSheet, out intDatabaseNO, out intCustomerName, out intDBName, out intU8Version, out  intU8AccountNO);
                    intNullRowFlag = 0;
                    intMaxExcelRow = this.MaxLoopExcelRow(objExcelSheet);
                    this.upbProgress.Maximum = intMaxExcelRow + 1;
                    for (int iRow = 2; iRow <= _iRowMax; iRow++)
                    {
                        this.upbProgress.Value = iRow;
                        this.ShowImportInformation("执行进度：第 [Value] 共 [Maximum] " + "，正在处理第 " + iRow.ToString() + " 行");
                        //如果有连续5个空行，循环即结束。
                        if (intNullRowFlag < _iNullRowMax)
                        {
                            if (((Range)objExcelSheet.Cells[iRow, 1]).Text.ToString().Trim() != "" && ((Range)objExcelSheet.Cells[iRow, 2]).Text.ToString().Trim() != ""
                                && ((Range)objExcelSheet.Cells[iRow, 3]).Text.ToString().Trim() != "" && ((Range)objExcelSheet.Cells[iRow, 4]).Text.ToString().Trim() != "")
                            {
                                this.ShowImportInformation("执行进度：第 [Value] 共 [Maximum] " + "正在处理第 " + iRow.ToString() + " 行：帐套信息。");
                                strMainID = System.Guid.NewGuid().ToString();
                                this.CustDataMain2DB(objExcelSheet, strMainID, iRow, objSQLComm, intDatabaseNO, intCustomerName, intDBName, intU8Version, intU8AccountNO);
                                intNullRowFlag = 0;
                            }
                            else if (((Range)objExcelSheet.Cells[iRow, 1]).Text.ToString().Trim() == "" && ((Range)objExcelSheet.Cells[iRow, 3]).Text.ToString().Trim() != "")
                            {
                                if (((Range)objExcelSheet.Cells[iRow, 2]).Text.ToString().Trim() != "")
                                {
                                    this.ShowImportInformation("执行进度：第 [Value] 共 [Maximum] " + "正在处理第 " + iRow.ToString() + " 行：业务对象数据量信息。");
                                    _sItemID = System.Guid.NewGuid().ToString().ToUpper();
                                    this.CustDataItem2DB(objExcelSheet, strMainID, _sItemID, iRow, objSQLComm);
                                    intNullRowFlag = 0;
                                }
                                else
                                {
                                    this.ShowImportInformation("执行进度：第 [Value] 共 [Maximum] " + "正在处理第 " + iRow.ToString() + " 行：业务对象年度数据量信息。");
                                    _sItemYearID = System.Guid.NewGuid().ToString().ToUpper();
                                    this.CustDataItemYear2DB(objExcelSheet, strMainID, _sItemID, _sItemYearID, iRow, objSQLComm);
                                }
                            }
                            else if (((Range)objExcelSheet.Cells[iRow, 1]).Text.ToString().Trim() == ""
                                      && ((Range)objExcelSheet.Cells[iRow, 2]).Text.ToString().Trim() == ""
                                      && ((Range)objExcelSheet.Cells[iRow, 3]).Text.ToString().Trim() == "")
                            {
                                intNullRowFlag = intNullRowFlag + 1;
                            }
                        }
                        else
                        {
                            break;
                        }

                        ////================================================================================================================================================================                    
                    }
                    _oTS.Complete();
                    //objSQLTran.Commit();
                #endregion
                }
            }
            catch (Exception E)
            {
                objSQLTran.Rollback();
                throw E;
            }
            finally
            {
                if (objExcelApp != null)
                {
                    objExcelApp.Workbooks.Close();
                    objExcelApp.Quit();
                }

                objExcelApp = null;
                objExcelWK = null;
                objSQLTran = null;
            }
            

        }

        private int MaxLoopExcelRow(Microsoft.Office.Interop.Excel.Worksheet objExcelSheet)
        {

            this.ShowImportInformation("正在寻找最大处理数据行....");

            int __intMaxEcelRow = 0;

            int intNullRowFlag = 0;            
            this.upbProgress.Maximum = _iRowMax;
            for (int iRow = 2; iRow <= _iRowMax; iRow++)
            {
                //如果有连续5个空行，循环即结束。
                if (intNullRowFlag < _iNullRowMax)
                {
                    this.ShowImportInformation("执行进度：第 [Value] 共 [Maximum] ");
                    if (((Range)objExcelSheet.Cells[iRow, 1]).Text.ToString().Trim() != "" && ((Range)objExcelSheet.Cells[iRow, 2]).Text.ToString().Trim() != ""
                        && ((Range)objExcelSheet.Cells[iRow, 3]).Text.ToString().Trim() != "" && ((Range)objExcelSheet.Cells[iRow, 4]).Text.ToString().Trim() != "")
                    {                        
                        intNullRowFlag = 0;
                    }
                    else if (((Range)objExcelSheet.Cells[iRow, 1]).Text.ToString().Trim() == "" && ((Range)objExcelSheet.Cells[iRow, 3]).Text.ToString().Trim() != "")
                    {                        
                        intNullRowFlag = 0;                        
                    }
                    else if (((Range)objExcelSheet.Cells[iRow, 1]).Text.ToString().Trim() == ""
                              && ((Range)objExcelSheet.Cells[iRow, 2]).Text.ToString().Trim() == ""
                              && ((Range)objExcelSheet.Cells[iRow, 3]).Text.ToString().Trim() == "")
                    {
                        intNullRowFlag = intNullRowFlag + 1;
                    }
                }
                else
                {
                    break;
                }

                __intMaxEcelRow = __intMaxEcelRow + 1;
                this.upbProgress.Value = iRow;
            }

            return __intMaxEcelRow + 1;
        }



        private void GetMainRowNum(Microsoft.Office.Interop.Excel.Worksheet objExcelSheet,
                                   out int intDatabaseNO, out int intCustomerName, out int intDBName, out int intU8Version, out  int intU8AccountNO)
        {
            Microsoft.Office.Interop.Excel.Range objRowRange = null;

            objRowRange = (Microsoft.Office.Interop.Excel.Range)objExcelSheet.Rows[1, _oMissValue];
            intDatabaseNO = objRowRange.Find("数据库编码", _oMissValue, _oMissValue, _oMissValue, _oMissValue, XlSearchDirection.xlNext, _oMissValue, _oMissValue, _oMissValue).Column;
            intCustomerName = objRowRange.Find("单位名称", _oMissValue, _oMissValue, _oMissValue, _oMissValue, XlSearchDirection.xlNext, _oMissValue, _oMissValue, _oMissValue).Column;
            intDBName = objRowRange.Find("数据库名称", _oMissValue, _oMissValue, _oMissValue, _oMissValue, XlSearchDirection.xlNext, _oMissValue, _oMissValue, _oMissValue).Column;            
            intU8AccountNO = objRowRange.Find("账套编号", _oMissValue, _oMissValue, _oMissValue, _oMissValue, XlSearchDirection.xlNext, _oMissValue, _oMissValue, _oMissValue).Column;
            if (objRowRange.Find("版本", _oMissValue, _oMissValue, _oMissValue, _oMissValue, XlSearchDirection.xlNext, _oMissValue, _oMissValue, _oMissValue) != null)
            {
                intU8Version = objRowRange.Find("版本", _oMissValue, _oMissValue, _oMissValue, _oMissValue, XlSearchDirection.xlNext, _oMissValue, _oMissValue, _oMissValue).Column;
            }
            else
            {
                intU8Version = 0;
            }
        }

        private void CustDataMain2DB(Microsoft.Office.Interop.Excel.Worksheet objExcelSheet, string strMainID, int iRow, System.Data.SqlClient.SqlCommand objSQLComm,
                                     int intDatabaseNO, int intCustomerName,int intDBName, int intU8Version, int intU8AccountNO)
        {
            string strSQL = "";

            string strDBNO = "";
            string strCustomerName = "";
            string strDBName = "";
            string strU8Version = "";
            string strU8AccountNO = ""; 

            strDBNO = ((Range)objExcelSheet.Cells[iRow, intDatabaseNO]).Text.ToString().Trim();
            strCustomerName = ((Range)objExcelSheet.Cells[iRow, intCustomerName]).Text.ToString().Trim();
            strDBName = ((Range)objExcelSheet.Cells[iRow, intDBName]).Text.ToString().Trim();
            strU8Version = (intU8Version != 0 ? ((Range)objExcelSheet.Cells[iRow, intU8Version]).Text.ToString().Trim() : "8.720");
            strU8AccountNO = ((Range)objExcelSheet.Cells[iRow, intU8AccountNO]).Text.ToString().Trim();

            strSQL = @"INSERT INTO [dbo].[CustDataMain]([chrID],[chrCustomerName],[chrU8Version],[chrDBName],[chrU8AccountNO],[chrCustInstanceDBNO], [dtmCreateDate])
                       VALUES('" + strMainID + "','" + strCustomerName + "','" + strU8Version + "','" + strDBName + "','" + strU8AccountNO + "','" + strDBNO + "', GETDATE())";
            objSQLComm.CommandText = strSQL;
            objSQLComm.ExecuteNonQuery();
        }


        private void CustDataItem2DB(Microsoft.Office.Interop.Excel.Worksheet objExcelSheet, string strMainID, string strItemID, int iRow, System.Data.SqlClient.SqlCommand objSQLComm)
        {            
            string strSQL;
            string str序号; string str业务对象; string str主表记录数; string str子表记录数; string str业务最早日期;
            string str业务最晚日期; string str主表数据空间; string str主表索引空间; string str子表数据空间; string str子表索引空间;

            str序号 = ((Range)objExcelSheet.Cells[iRow, 2]).Text.ToString().Trim();
            if (str序号 != "序号")
            {
                str业务对象 = ((Range)objExcelSheet.Cells[iRow, 3]).Text.ToString().Trim();
                str主表记录数 = (((Range)objExcelSheet.Cells[iRow, 4]).Text.ToString().Trim() != "" ? ((Range)objExcelSheet.Cells[iRow, 4]).Text.ToString().Trim() : "0");
                str子表记录数 = (((Range)objExcelSheet.Cells[iRow, 5]).Text.ToString().Trim() != "" ? ((Range)objExcelSheet.Cells[iRow, 5]).Text.ToString().Trim() : "0");
                str业务最早日期 = ((Range)objExcelSheet.Cells[iRow, 6]).Text.ToString().Trim();
                str业务最晚日期 = ((Range)objExcelSheet.Cells[iRow, 7]).Text.ToString().Trim();
                str主表数据空间 = ((Range)objExcelSheet.Cells[iRow, 8]).Text.ToString().Trim();
                str主表索引空间 = ((Range)objExcelSheet.Cells[iRow, 9]).Text.ToString().Trim();
                str子表数据空间 = ((Range)objExcelSheet.Cells[iRow, 10]).Text.ToString().Trim();
                str子表索引空间 = ((Range)objExcelSheet.Cells[iRow, 11]).Text.ToString().Trim();


                strSQL = @"INSERT INTO [dbo].[CustDataItem]([chrID],[chrItemID],[chrItemTypeNO],[chrItemName],[intItemMainCount],[intItemDetailCount],
                                [dtmOperFirst],[dtmOperLast],[chrMainDataSpace],[chrMainIndexSpace],[chrDetailDataSpasce],[chrDetailIndexSpace],[dtmCreateDate])
                       VALUES('" + strMainID + "','" + strItemID + "','" + str序号 + "','" + str业务对象 + "'," + str主表记录数 + "," + str子表记录数 + "," 
                                  + (str业务最早日期 == "" ? "NULL" : "'" + str业务最早日期 + "'") + ", " 
                                  + (str业务最晚日期 == "" ? "NULL" : "'" + str业务最晚日期 + "'") + ", " +
                                  "'" + str主表数据空间 + "', '" + str主表索引空间 + "', " +
                                  "'" + str子表数据空间 + "', '" + str子表索引空间 + "', GETDATE())";
                objSQLComm.CommandText = strSQL;
                objSQLComm.ExecuteNonQuery();
            }
        }

        private void CustDataItemYear2DB(Microsoft.Office.Interop.Excel.Worksheet objExcelSheet, string strMainID, string strItemID, string strItemYearID, int iRow, System.Data.SqlClient.SqlCommand objSQLComm)
        {
            string strSQL;
            string str主表时间年份; string str主表记录数; string str子表记录数;

            str主表时间年份 = ((Range)objExcelSheet.Cells[iRow, 3]).Text.ToString().Trim();
            if (str主表时间年份 != "主表时间年份")
            {
                str主表记录数 = (((Range)objExcelSheet.Cells[iRow, 4]).Text.ToString().Trim() != "" ? ((Range)objExcelSheet.Cells[iRow, 4]).Text.ToString().Trim() : "0");
                str子表记录数 = (((Range)objExcelSheet.Cells[iRow, 5]).Text.ToString().Trim() != "" ? ((Range)objExcelSheet.Cells[iRow, 5]).Text.ToString().Trim() : "0");

                strSQL = @"INSERT INTO [dbo].[CustDataItemYear]([chrDetailYearID],[chrItemID], [chrID], [intDataYear],[intItemMainCount],[intItemDetailCount],[dtmCreateDate])
                       VALUES('" + strItemYearID + "','" + strItemID + "','" + strMainID + "'," + str主表时间年份 + ",'" + str主表记录数 + "'," + str子表记录数 + ", GETDATE())";
                objSQLComm.CommandText = strSQL;
                objSQLComm.ExecuteNonQuery();
            }
        }



    }
}
