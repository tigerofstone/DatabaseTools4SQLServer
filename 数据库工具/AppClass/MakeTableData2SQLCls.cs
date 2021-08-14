using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace Upgrade.AppClass
{
	/// <summary>
	/// MakeTableData2SQLCls 的摘要说明。
	/// </summary>
	public class MakeTableData2SQLCls
	{
		public struct stcPKItem
		{
			public string Name ;
			public string DataType ;

			public override string ToString()
			{
				return Name;
			}
		}

		public string strConn;
		public System.Data.OleDb.OleDbConnection objConn;

		public System.Windows.Forms.ProgressBar objPB = null;
		
		public MakeTableData2SQLCls()
		{
			
		}

		public MakeTableData2SQLCls(string strConnection)
		{
			objConn = new System.Data.OleDb.OleDbConnection();

			strConn = strConnection;
			objConn.ConnectionString = strConnection;
			objConn.Open();
		}

		public MakeTableData2SQLCls(System.Data.OleDb.OleDbConnection objConnection)
		{
			objConn = objConnection;
			strConn = objConn.ConnectionString;
		}

		//生成修改语句
        public string makeSQLString(string strTableID, string strTableName, string strPKFields, string strModifyFields, 
                                    string strNotModifyFields, string strTableWhere, string strTableOrder, bool blnIncludeUpdate)
		{
            try
            {
                bool blnPrimaryKey;                

                System.Collections.ArrayList objAL = null;
                System.Data.DataTable objDTField = new System.Data.DataTable();
                System.Data.DataTable objDTAllField = new System.Data.DataTable();
                System.Data.DataTable objDTData = new System.Data.DataTable();

                blnPrimaryKey = (strPKFields.Trim() != "" ? true : false);

                //得到主键名称和类型
                if (blnPrimaryKey) objAL = getPrimaryKeyInfo(strTableID, strPKFields);
                //得到有条件的字段集合
                if (strModifyFields.Trim() != "" || strNotModifyFields.Trim() != "")
                {
                    objDTField = getTableFields(strTableID, strModifyFields, strNotModifyFields);
                    objDTAllField = getTableFields(strTableID, "", "");
                }
                else
                {
                    objDTField = getTableFields(strTableID, strModifyFields, strNotModifyFields);
                    objDTAllField = objDTField.Copy();
                }
                                
                //数据集
                objDTData = getUserDataTable(strTableName, strPKFields, objDTField, strTableWhere, strTableOrder);

                return CreateSQLString(objDTData, blnPrimaryKey, objAL, strTableName, objDTAllField, objDTField, blnIncludeUpdate);
            }
            catch (System.Exception Exp)
            {
                //MessageBox.Show(Exp.Message,"提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw (Exp);
            }
        }

        private string CreateSQLString(System.Data.DataTable objDTData, bool blnPrimaryKey, System.Collections.ArrayList objAL,
                                       string strTableName,  System.Data.DataTable objDTAllField, System.Data.DataTable objDTField, 
                                       bool blnIncludeUpdate)
        {
				//===================================================================================================================
            try
			{
                string strInsertSQL = "";
                string strFieldSQL = "";
                string strWhere = "";

                string[] strArr = new string[2];
                System.Text.StringBuilder objSB = new System.Text.StringBuilder();
                System.Data.DataRow objRow;
                
                if (objDTData.Rows.Count > 0)
                {
                    //处理进程条
                    if (objPB != null)
                    {
                        objPB.Minimum = 0;
                        objPB.Maximum = objDTData.Rows.Count - 1;
                    }

                    for (int i = 0; i < objDTData.Rows.Count; i++)
                    {
                        objRow = objDTData.Rows[i];
                        strInsertSQL = "";

                        //条件项
                        if (blnPrimaryKey)
                        {
                            strWhere = getDataWhere(objRow, objAL);
                            strInsertSQL = strInsertSQL + "IF NOT EXISTS(SELECT * FROM " + strTableName + " " + strWhere + ") \r\n";
                        }

                        //新增项
                        strArr = getDataInsert(objRow, objDTAllField);
                        strInsertSQL = strInsertSQL + "    INSERT INTO " + strTableName + " (" + strArr[0] + ") " + "\r\n" + "    VALUES (" + strArr[1].Trim() + ") \r\n";

                        //修改项
                        if (blnPrimaryKey && blnIncludeUpdate)
                        {
                            strFieldSQL = getDataUpdate(objRow, objDTField);

                            strInsertSQL = strInsertSQL + "ELSE \r\n";
                            strInsertSQL = strInsertSQL + "    UPDATE " + strTableName + " SET " + strFieldSQL + " " + strWhere + "\r\n";
                        }

                        objSB.Append(strInsertSQL + "\r\n");

                        //处理进程条数值
                        if (objPB != null) objPB.Value = i;
                    }
                }
                else
                {
                    throw(new Exception("没有符合条件的数据，或此表中没有数据记录。"));
                }
				
				return objSB.ToString();
			}
			catch(System.Exception Exp)
			{
				//MessageBox.Show(Exp.Message,"提示", MessageBoxButtons.OK,MessageBoxIcon.Error);
                throw (Exp);
			}
		}

		private System.Collections.ArrayList getPrimaryKeyInfo(string strTableID, string strPrimaryKey)
		{
			try
			{
				string strSQL;
				string strPK;
				stcPKItem objPKItem;
				ArrayList objAL = new ArrayList();
				System.Data.DataTable objDTField  = new System.Data.DataTable();

				strPK = "'" + strPrimaryKey.Replace(" ","").Replace(",", "','") + "'";
				strSQL = "Select SC.[name] as fieldName,ST.[name] as fieldType From SYSCOLUMNS SC INNER JOIN SYSTYPES ST ON SC.xtype = ST.xtype " +
					     "Where SC.[id] = '" + strTableID+ "' AND not ST.[name] = 'sysname' AND SC.[name] IN (" + strPK +")";
				
				objDTField = getDataTable(strSQL);
				
				if(objDTField.Rows.Count == 0)
				{
					throw(new System.Exception("没有找到指定的主键。"));
				}

				for(int i = 0; i < objDTField.Rows.Count; i++)
				{
					objPKItem = new stcPKItem();
					objPKItem.Name = objDTField.Rows[i]["fieldName"].ToString().Trim();
					objPKItem.DataType = objDTField.Rows[i]["fieldType"].ToString().Trim();

					objAL.Add(objPKItem);
				}
				
				return objAL;
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}

        private System.Data.DataTable getTableFields(string strTableID, string strInFields, string strNotInFields)
		{
			try
			{
				string strSQL;

				strSQL = "Select SC.[name] as fieldName,ST.[name] as fieldType From syscolumns SC inner join systypes ST ON SC.xtype = ST.xtype " +
					     "Where [id] = '" + strTableID + "' AND NOT ST.[name] = 'sysname' " +
                         (strInFields != "" ? "AND SC.[name] IN (" + strInFields + ") " : "") +
                         (strNotInFields != "" ? "AND NOT SC.[name] IN (" + strNotInFields + ") " : "") +
                         "Order By  SC.[name]";
				
				return getDataTable(strSQL);
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}

        private System.Data.DataTable getUserDataTable(string strTableName, string strPKFields, DataTable objDTField, string strWhere, string strOrder)
		{
			try
			{
				string strSQL;

				strSQL = "Select * ";

                //for(int i = 0; i < objDTField.Rows.Count; i++)
                //{
                //    strSQL = strSQL + objDTField.Rows[i]["fieldName"].ToString() + ", ";
                //}
                //strSQL = strSQL.Substring(0, strSQL.Length - 2);
                //strSQL = strSQL + (strPKFields == "" ? " " : ", " + strPKFields + " ");				

				strSQL = strSQL + "From " + strTableName + " WITH(NOLOCK)";
				if(strWhere != "")
				{
					strSQL = strSQL + " Where " + strWhere;
				}
                if (strOrder != "")
                {
                    strSQL = strSQL + " Order By " + strOrder;
                }

				return getDataTable(strSQL);
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}


		private string getDataWhere(System.Data.DataRow objRow, System.Collections.ArrayList objAL)
		{
			try
			{
				string strWhere = "";
				string strPKType, strPKName;
				
				for(int i = 0; i < objAL.Count; i++)
				{
					strPKType = ((stcPKItem)objAL[i]).DataType.ToLower();
					strPKName = ((stcPKItem)objAL[i]).Name;

					if(!objRow[strPKName].Equals(System.DBNull.Value))
					{
						strWhere = strWhere + strPKName + " = " + getFieldValue(objRow, strPKName, strPKType);
					}
					else
					{
						strWhere = strWhere + strPKName + " IS NULL";
					}
					strWhere = strWhere + " AND ";
				}
				strWhere = "Where " + strWhere.Substring(0, strWhere.Length - 5);
				strWhere = strWhere.Trim();

				return strWhere;
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}

		private string[] getDataInsert(System.Data.DataRow objRow, System.Data.DataTable objDTField)
		{
			try
			{
				string[] strArr = new string[2];
				string strDataSQL, strFieldSQL;
				string strFDName, strFDType;
				
				strDataSQL = "";
				strFieldSQL = "" ;
				for(int j = 0; j < objDTField.Rows.Count; j++)
				{
					strFDType = objDTField.Rows[j]["fieldType"].ToString().Trim().ToLower();
					strFDName = objDTField.Rows[j]["fieldName"].ToString().Trim();

					strFieldSQL = strFieldSQL + strFDName + ", ";
					strDataSQL = strDataSQL + getFieldValue(objRow, strFDName, strFDType) + ", ";					
				}
				strFieldSQL = strFieldSQL.Substring(0,strFieldSQL.Length - 2);
				strDataSQL = strDataSQL.Substring(0,strDataSQL.Length - 2);
						
				strArr[0] = strFieldSQL;
				strArr[1] = strDataSQL;

				return strArr;
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}


		private string getDataUpdate(System.Data.DataRow objRow, System.Data.DataTable objDTField)
		{
			try
			{
				string strFieldSQL;
				string strFDName, strFDType;
				
				strFieldSQL = "";
				for(int j = 0; j < objDTField.Rows.Count; j++)
				{
					strFDType = objDTField.Rows[j]["fieldType"].ToString().Trim();
					strFDName = objDTField.Rows[j]["fieldName"].ToString().Trim();

					strFieldSQL = strFieldSQL + strFDName + " = ";						
					strFieldSQL = strFieldSQL + getFieldValue(objRow, strFDName, strFDType) + ", ";
				}
				strFieldSQL = strFieldSQL.Substring(0,strFieldSQL.Length - 2);
						
				return strFieldSQL;
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}

		private string getFieldValue(System.Data.DataRow objRow, string strFDName, string strFDType)
		{
			try
			{
				string strValue;

				if(! objRow[strFDName].Equals(System.DBNull.Value))
				{
					if(strFDType == "uniqueidentifier" || strFDType == "datetime" || strFDType == "text"
						|| strFDType == "ntext" || strFDType == "varchar" || strFDType == "char" || strFDType == "timestamp"
						|| strFDType == "nvarchar" || strFDType == "nchar")
					{
						if(strFDType == "uniqueidentifier")
						{
							strValue = "'{" + objRow[strFDName].ToString().Trim().ToUpper() + "}'";
						}
						else if(strFDType == "datetime")
						{
							strValue = "'" + ((DateTime)objRow[strFDName]).ToString("yyy-MM-dd HH:mm:ss:fff").Trim() + "'";
						}
						else
						{
							strValue = "'" + objRow[strFDName].ToString().Trim().Replace("'", "''") + "'";
						}
					}
					else
					{
						if(strFDType == "bit")
						{
							strValue = ((bool)objRow[strFDName] ? "1" : "0") ;
						}
						else
						{
							strValue = objRow[strFDName].ToString();
						}
					}
				}
				else
				{
					strValue = "NULL";
				}

				return strValue;
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}


		private System.Data.DataTable getDataTable(string strSQL)
		{
			try
			{
				System.Data.DataTable objDT  = new System.Data.DataTable();
				System.Data.OleDb.OleDbDataAdapter objDAT  = new System.Data.OleDb.OleDbDataAdapter();
				System.Data.OleDb.OleDbCommand objComm;

				objComm = this.objConn.CreateCommand();
				objComm.CommandTimeout = 600;
				objComm.CommandText = strSQL;
				objComm.ExecuteNonQuery();
			
				objDT.Clear();
				objDT.TableName = "Fields";
				objDAT.SelectCommand = objComm;
				objDAT.Fill(objDT);	

				return objDT;
			}
			catch(System.Exception e)
			{
				throw(e);
			}
		}
	}
}
