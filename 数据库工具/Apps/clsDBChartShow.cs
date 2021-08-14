using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Upgrade.Apps
{
    class clsDBChartShow
    {
        public clsDBChartShow()
        {
        }

        // 构造数据表图形
        public void DrawAllTableGraph(System.Data.DataTable objDTS, System.Windows.Forms.DataVisualization.Charting.Chart objChart, int iTableCount, int iTableOyderBy)
        {
            try
            {
                int iTemp = 0;
                
                objChart.Series[0].Points.Clear();
                System.Data.DataRow[] drArray = null;
                if (iTableOyderBy == 0)
                {
                    drArray = objDTS.Select(null, "记录数 DESC", DataViewRowState.CurrentRows);
                }
                else if (iTableOyderBy == 1)
                {
                    drArray = objDTS.Select(null, "表名 ASC", DataViewRowState.CurrentRows);
                }
                
                objChart.Series[0].Points.Clear();
                objChart.ChartAreas[0].AxisY.Maximum = Double.NaN;

                for (int i = 0; (i < iTableCount && i < drArray.Length); i++)
                {
                    System.Windows.Forms.DataVisualization.Charting.DataPoint oPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint();

                    oPoint.SetValueXY((string)drArray[i]["表名"], double.Parse(drArray[i]["记录数"].ToString()));
                    oPoint.LegendText = (string)drArray[i]["表名"];
                    oPoint.ToolTip = "#LEGENDTEXT:#VAL";
                    objChart.Series[0].Points.Add(oPoint);
                }

                if (drArray.Length > 50 && iTableCount > 50)
                {
                    objChart.Dock = DockStyle.Left;

                    if (drArray.Length >= iTableCount) iTemp = iTableCount;
                    if (drArray.Length < iTableCount) iTemp = drArray.Length;

                    objChart.Width = (int)(1000 * (double)(iTemp / 50.0));
                    //this.chartAlltable.Width = (int)(1000 * 20);
                    objChart.ChartAreas[0].Position.Auto = false;
                    objChart.ChartAreas[0].Position.Width = 100;
                    objChart.ChartAreas[0].Position.Height = 80;
                    objChart.ChartAreas[0].Position.X = 0;
                    if (objChart.Width > 30000)
                    {
                        objChart.Width = objChart.Width;
                        objChart.ChartAreas[0].InnerPlotPosition.X = (float)(0.2);
                        objChart.ChartAreas[0].InnerPlotPosition.Width = (float)(99.6);
                    }
                    else
                    {

                        objChart.ChartAreas[0].InnerPlotPosition.X = (float)(0.4);
                        objChart.ChartAreas[0].InnerPlotPosition.Width = (float)(99.2);
                    }
                }
                else
                {
                    objChart.Dock = DockStyle.Fill;
                    objChart.ChartAreas[0].Position.Auto = true;
                    objChart.ChartAreas[0].InnerPlotPosition.Auto = true;
                }
                objChart.ChartAreas[0].AxisX.LabelStyle.TruncatedLabels = false;

                //MessageBox.Show(objChart.Width.ToString());

            }
            catch (Exception E)
            {
                throw E;
            }

        }

        public void DrawAllTableSpaceGraph(System.Data.DataTable objDTS, System.Windows.Forms.DataVisualization.Charting.Chart objChart, double dblDBSpac)
        {
            objChart.Series[0].Points.Clear();
            System.Data.DataRow[] drArray = objDTS.Select(null, "保留空间(MB) DESC", DataViewRowState.CurrentRows);
            System.Windows.Forms.DataVisualization.Charting.DataPoint oPoint;
            double dblTop20Space = 0;

            int i = 0;
            for (i = 0; i < 3000 && i < objDTS.Rows.Count; i++)
            {
                oPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint();

                oPoint.SetValueXY((string)drArray[i]["表名"], double.Parse(drArray[i]["保留空间(MB)"].ToString()));
                oPoint.IsValueShownAsLabel = false;
                oPoint.LegendText = (string)drArray[i]["表名"];
                oPoint.Label = "#PERCENT{P1}";
                oPoint.ToolTip = "#LEGENDTEXT:#PERCENT{P1}";
                if (i == 0)
                {
                    oPoint.CustomProperties = "Exploded=true";
                    oPoint.Label = "#LEGENDTEXT:#PERCENT{P1}";

                }
                objChart.Series[0].Points.Add(oPoint);

                dblTop20Space += double.Parse(drArray[i]["保留空间(MB)"].ToString());
            }

            if (dblDBSpac > dblTop20Space)
            {
                oPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
                oPoint.SetValueXY("其他", dblDBSpac - dblTop20Space);
                oPoint.IsValueShownAsLabel = false;
                oPoint.LegendText = "其他";
                oPoint.Label = "#LEGENDTEXT:#PERCENT{P1}";
                oPoint.ToolTip = "#LEGENDTEXT:#PERCENT{P1}";
                oPoint.CustomProperties = "Exploded=true";
                objChart.Series[0].Points.Add(oPoint);
            }
        }
        
        // 构造Top 20 数据表图形
        public void DrawTop20TableGraph(System.Data.DataTable objDTS, System.Windows.Forms.DataVisualization.Charting.Chart objChart)
        {
            objChart.Series[0].Points.Clear();

            objChart.ChartAreas[0].AxisY.Maximum = Double.NaN;

            System.Data.DataRow[] drArray = objDTS.Select(null, "记录数 DESC", DataViewRowState.CurrentRows);

            System.Windows.Forms.DataVisualization.Charting.DataPoint oPoint;

            for (int i = 0; i < 20 && i <= drArray.Length; i++)
            {
                oPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint();

                oPoint.SetValueXY((string)drArray[i]["表名"], double.Parse(drArray[i]["记录数"].ToString()));
                oPoint.LegendText = (string)drArray[i]["表名"];
                oPoint.ToolTip = "#LEGENDTEXT:#VAL";
                oPoint.IsValueShownAsLabel = true;
                objChart.Series[0].Points.Add(oPoint);
            }
            objChart.Series[0].IsValueShownAsLabel = true;
            objChart.ChartAreas[0].AxisX.Interval = 1;
            objChart.ChartAreas[0].AxisX.IntervalOffset = 1;
            //objChart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
        }
        public void DrawTop20TablePieGraph(System.Data.DataTable objDTS, System.Windows.Forms.DataVisualization.Charting.Chart objChart, double dblDBSpac)
        {
            objChart.Series[0].Points.Clear();

            System.Data.DataRow[] drArray = objDTS.Select(null, "保留空间(MB) DESC", DataViewRowState.CurrentRows);
            //System.Data.DataRow[] drArray = objDTS.Select(null, "记录数 DESC", DataViewRowState.CurrentRows);
            System.Windows.Forms.DataVisualization.Charting.DataPoint oPoint;

            double dblTop20Space = 0, dblTable = 0;
            int i = 0;
            for (i = 0; i < 20 && i < drArray.Length; i++)
            {
                dblTable = double.Parse(drArray[i]["保留空间(MB)"].ToString());
                oPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint();

                oPoint.SetValueXY((string)drArray[i]["表名"], dblTable);
                oPoint.LegendText = (string)drArray[i]["表名"];
                oPoint.Label = "#PERCENT{P1}";
                oPoint.ToolTip = "#LEGENDTEXT:#PERCENT{P1}";
                //if (i == 0) oPoint.CustomProperties = "Exploded=true";
                objChart.Series[0].Points.Add(oPoint);

                dblTop20Space += dblTable;
            }

            if (dblDBSpac > dblTop20Space)
            {
                oPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
                oPoint.SetValueXY("其他", dblDBSpac - dblTop20Space);
                oPoint.IsValueShownAsLabel = false;
                oPoint.LegendText = "其他";
                oPoint.Label = "#LEGENDTEXT:#PERCENT{P1}";
                oPoint.ToolTip = "#LEGENDTEXT:#PERCENT{P1}";
                oPoint.CustomProperties = "Exploded=true";
                objChart.Series[0].Points.Add(oPoint);

                oPoint.LabelForeColor = System.Drawing.Color.Gray;
            }
        }
       



        // 构造数据表图形
        public void DrawU8AllTableGraph(System.Collections.Generic.List<U8BizData> objList, System.Windows.Forms.DataVisualization.Charting.Chart objChart, int iResizeChart, int iTableCount, int iTableOyderBy)
        {
            try
            {
                int iTemp = 0;
                U8BizData oBizInfo = new U8BizData();

                objChart.Series[0].Points.Clear();

                if (iTableOyderBy == 0)
                {
                    objList.Sort(new Upgrade.Apps.U8BizTableCountComparerDESC());
                }
                else if (iTableOyderBy == 1)
                {
                    objList.Sort(new Upgrade.Apps.U8BizTableCountComparerTableName());
                }

                objChart.ChartAreas[0].AxisY.Maximum = Double.NaN;

                for (int i = 0; (i < iTableCount && i < objList.Count); i++)
                {
                    System.Windows.Forms.DataVisualization.Charting.DataPoint oPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint();

                    oBizInfo = (U8BizData)objList[i];

                    oPoint.SetValueXY(oBizInfo.sU8ObjectName, oBizInfo.iU8TableCount);
                    oPoint.LegendText = oBizInfo.sU8ObjectName;
                    oPoint.ToolTip = "#LEGENDTEXT:#VAL";
                    objChart.Series[0].Points.Add(oPoint);
                }

                if (iResizeChart == 1)
                {
                    if (objList.Count >= 50 && iTableCount >= 50)
                    {
                        objChart.Dock = DockStyle.Left;

                        if (objList.Count >= iTableCount) iTemp = iTableCount;
                        if (objList.Count < iTableCount) iTemp = objList.Count;

                        objChart.Width = (int)(1100 * (double)(iTemp / 50.0));
                        objChart.ChartAreas[0].Position.Auto = false;

                        if (objList.Count < 200 && iTableCount < 200)
                        {
                            objChart.ChartAreas[0].InnerPlotPosition.Height = 60;
                            objChart.ChartAreas[0].InnerPlotPosition.Width = 96;
                            objChart.ChartAreas[0].InnerPlotPosition.X = 4;
                            objChart.ChartAreas[0].Position.Width = 100;
                            //objChart.ChartAreas[0].Position.Height = 80;
                            objChart.ChartAreas[0].Position.X = 0;
                        }
                        else
                        {
                            objChart.ChartAreas[0].InnerPlotPosition.Height = 60;
                            objChart.ChartAreas[0].InnerPlotPosition.Width = float.Parse("99"); ;
                            objChart.ChartAreas[0].InnerPlotPosition.X = float.Parse("0.7");
                            objChart.ChartAreas[0].Position.Width = float.Parse("100");
                            //objChart.ChartAreas[0].Position.Height = 80;
                            objChart.ChartAreas[0].Position.X = 0;
                        }

                        //if (objChart.Width > 30000)
                        //{
                        //    objChart.ChartAreas[0].InnerPlotPosition.X = (float)(0.1);
                        //    objChart.ChartAreas[0].InnerPlotPosition.Width = (float)(99.9);
                        //}
                        //else
                        //{
                        //    objChart.ChartAreas[0].InnerPlotPosition.Height = 60;
                        //    objChart.ChartAreas[0].InnerPlotPosition.Width = float.Parse("99"); ;
                        //    objChart.ChartAreas[0].InnerPlotPosition.X = float.Parse("0.7");
                        //    objChart.ChartAreas[0].Position.Width = float.Parse("100");
                        //    //objChart.ChartAreas[0].Position.Height = 80;
                        //    objChart.ChartAreas[0].Position.X = 1;
                        //}
                    }
                    else
                    {
                        objChart.Dock = DockStyle.Fill;
                        objChart.ChartAreas[0].Position.Auto = true;
                        objChart.ChartAreas[0].InnerPlotPosition.Auto = true;
                    }
                }
                objChart.ChartAreas[0].AxisX.LabelStyle.TruncatedLabels = false;

            }
            catch (Exception E)
            {
                throw E;
            }

        }
        public void DrawU8AllTableSpaceGraph(System.Collections.Generic.List<U8BizData> objList, System.Windows.Forms.DataVisualization.Charting.Chart objChart, double dblDBSpace)
        {
            double dblTop20Space = 0, dblTable = 0;
            U8BizData oBizInfo = new U8BizData();
            System.Windows.Forms.DataVisualization.Charting.DataPoint oPoint;

            objChart.Series[0].Points.Clear();
            objList.Sort(new Upgrade.Apps.U8BizTableSpaceComparerDESC());
            for (int i = 0; i < 2000 && i < objList.Count; i++)
            {
                oPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint();

                oBizInfo = (U8BizData)objList[i];
                dblTable = oBizInfo.dblU8TableSpace;
                oPoint.SetValueXY(oBizInfo.sU8ObjectName, oBizInfo.dblU8TableSpace);
                oPoint.IsValueShownAsLabel = false;
                oPoint.LegendText = oBizInfo.sU8ObjectName;
                oPoint.Label = "#PERCENT{P1}";
                oPoint.ToolTip = "#LEGENDTEXT:#PERCENT{P2}";
                if (i == 0)
                {
                    oPoint.CustomProperties = "Exploded=true";
                    oPoint.Label = "#LEGENDTEXT:#PERCENT{P1}";

                }
                objChart.Series[0].Points.Add(oPoint);

                dblTop20Space = dblTop20Space + dblTable;
            }


            oPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            oPoint.SetValueXY("其他", dblDBSpace - dblTop20Space);
            oPoint.IsValueShownAsLabel = false;
            oPoint.LegendText = "其他";
            oPoint.Label = "#LEGENDTEXT:#PERCENT{P1}";
            oPoint.ToolTip = "#LEGENDTEXT:#PERCENT{P2}";
            oPoint.CustomProperties = "Exploded=true";
            objChart.Series[0].Points.Add(oPoint);

            oPoint.LabelForeColor = System.Drawing.Color.Gray;
        }

        // 构造Top 20 数据表图形
        public void DrawU8Top20TableGraph(System.Collections.Generic.List<U8BizData> objList, System.Windows.Forms.DataVisualization.Charting.Chart objChart)
        {            
            U8BizData oBizInfo = new U8BizData();

            objChart.Series[0].Points.Clear();
            objChart.ChartAreas[0].AxisY.Maximum = Double.NaN;

            objList.Sort(new U8BizTableCountComparerDESC());
            for (int i = 0; (i < 25 && i < objList.Count); i++)
            {
                System.Windows.Forms.DataVisualization.Charting.DataPoint oPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint();

                oBizInfo = (U8BizData)objList[i];

                oPoint.SetValueXY(oBizInfo.sU8ObjectName, oBizInfo.iU8TableCount);
                oPoint.LegendText = oBizInfo.sU8ObjectName;
                oPoint.ToolTip = "#LEGENDTEXT:#VAL";
                objChart.Series[0].Points.Add(oPoint);
            }

            //objChart.Series[0].IsValueShownAsLabel = true;
            objChart.ChartAreas[0].AxisX.Interval = 1;
            objChart.ChartAreas[0].AxisX.IntervalOffset = 1;
            //objChart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
        }
        public void DrawU8Top20TablePieGraph(System.Collections.Generic.List<U8BizData> objList, System.Windows.Forms.DataVisualization.Charting.Chart objChart, double dblDBSpace)
        {
            U8BizData oBizInfo = new U8BizData();

            objChart.Series[0].Points.Clear();
            objList.Sort(new Upgrade.Apps.U8BizTableSpaceComparerDESC());

            System.Windows.Forms.DataVisualization.Charting.DataPoint oPoint;
            double dblTop20Space = 0, dblTable = 0;

            for (int i = 0; i < 25 && i < objList.Count; i++)
            {
                oPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint();

                oBizInfo = (U8BizData)objList[i];
                dblTable = oBizInfo.dblU8TableSpace;
                oPoint.SetValueXY(oBizInfo.sU8ObjectName, oBizInfo.dblU8TableSpace);
                oPoint.IsValueShownAsLabel = false;
                oPoint.LegendText = oBizInfo.sU8ObjectName;
                oPoint.Label = "#PERCENT{P1}";
                oPoint.ToolTip = "#LEGENDTEXT:#PERCENT{P2}";
                if (i == 0)
                {
                    oPoint.CustomProperties = "Exploded=true";
                    oPoint.Label = "#LEGENDTEXT:#PERCENT{P1}";

                }
                objChart.Series[0].Points.Add(oPoint);

                dblTop20Space += dblTable;
            }

            oPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
            oPoint.SetValueXY("其他", dblDBSpace - dblTop20Space);
            oPoint.IsValueShownAsLabel = false;
            oPoint.LegendText = "其他";
            oPoint.Label = "#LEGENDTEXT:#PERCENT{P1}";
            oPoint.ToolTip = "#LEGENDTEXT:#PERCENT{P2}";
            oPoint.CustomProperties = "Exploded=true";
            objChart.Series[0].Points.Add(oPoint);

            oPoint.LabelForeColor = System.Drawing.Color.Gray;
        }
    }
}
