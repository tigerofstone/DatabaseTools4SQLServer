using System;

namespace Upgrade.AppClass
{
	/// <summary>
	/// MyDBUpdateException ��ժҪ˵����
	/// </summary>
	public class MyDBUpdateException : System.Exception
	{
		public string strErrMessage;

		public MyDBUpdateException() : base()
		{
			strErrMessage = null;
		}

		
		public MyDBUpdateException(string strMessage) : base(strMessage)
		{
			strErrMessage = strMessage;
		}
		
		public MyDBUpdateException(string strMessage,Exception ErrException):base(strMessage,ErrException)
		{
			strErrMessage = strMessage;
		}
		
		public MyDBUpdateException(string strMessage,string strFileName):base(strMessage)
		{
			strErrMessage = strMessage;
		}

		public override string ToString()
		{
			return "ϵͳ�����ࣻ" + "/n" + base.ToString();
		}
	}
}
