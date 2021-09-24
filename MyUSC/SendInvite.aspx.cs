using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using CuteEditor;
using CuteEditor.ImageEditor;

namespace MyUSC
{
	public partial class SendInvite : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if( !IsPostBack )
			{
				litInstructions.Text = BuildInstructions();
				txtDetails.Text = BuildSampleEmail();
			}
		}

		private string BuildInstructions()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Enter the email address(es) below of the person(s) you would like to invite to join MySportsConnect.net.  ");
			sb.Append("To enter multiple email addreses, seperate them using a ;<br>");
			sb.Append("example: person1@mail.com; person2@email.net");

			return sb.ToString();
		}

		private string BuildSampleEmail()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Hello,").AppendLine("<br><br>");
			sb.Append("I'd like to invite you to try a new website for sports fans.  The current version allows you to chat with your friends and follow the news for your favorite sports and teams.").Append("<br><br>");
			sb.Append("New features coming soon, will allow you to create and manage pages for your own youth and amateur teams.  ").Append("<br><br>");
			sb.Append("If you are interested in giving the site a try, go to <a href=\"http://www.mysportsconnect.net\">http://www.mysportsconnect.net</a> and create an account.");
			sb.Append("  It's fun and it's totally free.<br><br>");
			sb.Append("I hope to see you on the site soon.<br>").Append("Your name here");

			return sb.ToString();
		}

		protected void OnClickSend(object sender, ImageClickEventArgs e)
		{
			string strTo = txtEmailAddress.Text;
			string strEmail = txtDetails.Text;
			UserAccount acct = Master.GetActiveUser();
			if (null != acct)
			{
				// Send message
				EmailUtil email = new EmailUtil( Master.g_SiteAdmin );
				email.SendInfoMail(acct.Email, strTo, "Website invitation", strEmail);
				Response.Redirect("Friends.aspx");
			}
			else
			{
				Master.AlertUser("There was a problem sending the email.  Please re-login and try again.");
				RedirectToLoginPage();
			}
		}

		protected void OnClickCancel(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("Friends.aspx");
		}
	}
}