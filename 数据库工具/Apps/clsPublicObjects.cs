using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Upgrade.Apps
{
    public class clsPublicObjects
    {
    }


    public class clsU8ChartData
    {
        public static System.Collections.Generic.List<U8BizData> htU8BizObjTableData = new System.Collections.Generic.List<U8BizData>();
        public static System.Collections.Generic.List<U8BizYearData> htU8BizObjTableYearData = new System.Collections.Generic.List<U8BizYearData>();

        public static System.Collections.Generic.List<U8BizData> htU8UUBizObjTableData = new System.Collections.Generic.List<U8BizData>();
        public static System.Collections.Generic.List<U8BizYearData> htU8UUBizObjTableYearData = new System.Collections.Generic.List<U8BizYearData>();

        public clsU8ChartData()
        {

        }
        
    }

    public class U8BizData
    {
        public int iNo;
        public string sU8TableName;
        public string sU8ObjectName;
        public int iU8TableCount;
        public double dblU8TableSpace;

    }

    public class U8BizYearData
    {
        public int iNo;
        public string sU8TableName;
        public string sU8ObjectName;
        public System.Collections.Hashtable oU8BizYearCount;
    }
    public struct U8BizObjYearCount
    {
        public int sU8BizYear;
        public double dblU8TableCount;
    }


    public class U8BizTableSpaceComparer : IComparer<U8BizData>
    {
        public int Compare(U8BizData first, U8BizData second)
       {
           return first.dblU8TableSpace.CompareTo(second.dblU8TableSpace);
         }
     }

    public class U8BizTableSpaceComparerDESC : IComparer<U8BizData>
    {
        public int Compare(U8BizData first, U8BizData second)
        {
            return 0 - first.dblU8TableSpace.CompareTo(second.dblU8TableSpace);
        }
    }

    public class U8BizTableCountComparer : IComparer<U8BizData>
    {
        public int Compare(U8BizData first, U8BizData second)
        {
            return first.iU8TableCount.CompareTo(second.iU8TableCount);
        }
    }

    public class U8BizTableCountComparerDESC : IComparer<U8BizData>
    {
        public int Compare(U8BizData first, U8BizData second)
        {
            return 0 - first.iU8TableCount.CompareTo(second.iU8TableCount);
        }
    }

    public class U8BizTableCountComparerTableName : IComparer<U8BizData>
    {
        public int Compare(U8BizData first, U8BizData second)
        {
            return first.sU8ObjectName.CompareTo(second.sU8ObjectName);
        }
    }

   
}
