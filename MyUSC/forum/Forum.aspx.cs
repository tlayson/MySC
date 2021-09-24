using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.forum
{
	public partial class Forum : USCPageBase
	{
		protected Forum()
		{
			_PAGENAME = "Forums";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if( !IsPostBack )
			{
				//here we check to see if the user is logged in
				UserAccount acct = GetActiveUser();
				if( null == acct )
				{
					RedirectToLoginPage();
				}
			}
			Master.SelectMenuItem(SelectedPage.Forums);
		}
	}
}