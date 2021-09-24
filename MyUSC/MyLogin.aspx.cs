using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC
{
	public partial class MyLogin : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			((USCMaster)Master).HideAds();

		}
	}
}