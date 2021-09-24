using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC
{
	public partial class PrivacyStatement : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if( !IsPostBack )
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("We are a family friendly site and your privacy is very important to us as we continue to have children that have access to the site. We designed our Data Use Policy to make important disclosures about how you can use MySportsConnect.com to share with others and how we collect and can use your content and information.  We encourage you to read the Data Use Policy, and to use it to help you make informed decisions.");
				lblPrivacyText.Text = sb.ToString();
			}
		}

		protected void OnClickReturn(object sender, ImageClickEventArgs e)
		{
			IsUserLoggedIn( true );
			RedirectToDefault( GetActiveUser() );
		}
	}
}