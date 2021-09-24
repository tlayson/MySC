using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.MyAccount
{
	public partial class PswdSent : USCPageBase
	{
		protected PswdSent()
		{
			_PAGENAME = "PswdSent";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}