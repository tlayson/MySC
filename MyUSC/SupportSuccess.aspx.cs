using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC
{
	public partial class SupportSuccess : USCPageBase
	{
		protected SupportSuccess()
		{
			_PAGENAME = "SupportSuccess";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			lblSuccessMsg.Text = "Your support request has been sent successfully.  We will get back to you as soon as possible.  Thank you for contacting us.";
		}

		protected void OnClickOK(object sender, ImageClickEventArgs e)
		{
			UserAccount acct = GetActiveUser();
			if( null != acct )
			{
				RedirectToDefault( acct );
			}
			else
			{
				RedirectToLoginPage();
			}
		}
	}
}