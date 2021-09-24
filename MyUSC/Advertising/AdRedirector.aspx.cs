using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using MyUSC.Classes;

namespace MyUSC.Advertising
{
	public partial class AdRedirector : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			String adName = Request.QueryString["ad"];
			String redirect = Request.QueryString["target"];
			string source = Request.QueryString["source"];
			if (adName == null || redirect == null)
			{
				// Just go back to site.
				redirect = "http://www.mysportsconnect.net";
			}

			String strRootPath = Server.MapPath("~");
			USCBase.UpdateClicks(strRootPath, adName, source);

			Response.Redirect(redirect);
		}
	}
}