using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC
{
	public partial class AboutUSC : USCPageBase
	{

		protected AboutUSC()
		{
			_PAGENAME = "AboutUSC";
			_USERTYPE = "Pre-Login";
			_pgUSCMaster = (USCMaster)Master;
		}

		
		protected void Page_Load(object sender, EventArgs e)
		{
			// Cast the loosely-typed Page.Master property and then set the GridMessageText property 
			string strCnx = Master.g_strConnectionString;
			strCnx = "";

			Master.SelectMenuItem( SelectedPage.About );
		}
	}
}