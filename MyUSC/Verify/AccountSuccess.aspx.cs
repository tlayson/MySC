using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC.Verify
{
	public partial class AccountSuccess : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Congratulations!  Your account was successfully created.  An email was sent to the email account that you registered with.<br><br>");
			sb.Append("When you receive the email, click on the link contained within the email to verify your email address.<br><br>");
			sb.Append("Thank you for joining MySportsConnect.net.  We hope you enjoy your time with us.<br><br>");

			litAccountSuccess.Text = sb.ToString();
		}

		protected void OnClickOK(object sender, ImageClickEventArgs e)
		{
			RedirectToLoginPage();
		}
	}
}