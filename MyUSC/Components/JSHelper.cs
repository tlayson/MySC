using System;

namespace SamplePortal
{
	/// <summary>
	/// Web Page JaveScript Helper
	/// </summary>
	public class JSHelper
	{
		private JSHelper()
		{
		}

		/// <summary>
		/// Show a JS message box.
		/// </summary>
		/// <param name="msg"></param>
		public static void MsgBox(String msg)
		{
			System.Web.HttpContext.Current.Response.Write("<script language='javascript'>alert('" + msg + "');</script>");
		}
		

	}
}
