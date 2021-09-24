using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;

namespace MyUSC
{
	public partial class FriendDetails : USCPageBase
	{
		protected FriendDetails()
		{
			_PAGENAME = "FriendDetails";
			_pgUSCMaster = (USCMaster)Master;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			Master.SelectMenuItem(SelectedPage.Friends);

			if (!IsPostBack)
			{
				//here we check to see if the user is logged in
				UserAccount acct = GetActiveUser();
				if (null == acct)
				{
					RedirectToLoginPage();
				}

				string strFriendID = Request.QueryString["FriendID"];
				if (null != strFriendID && strFriendID.Length > 0)
				{
					long lKey = Convert.ToInt64(strFriendID);
					UserAccount friendAcct = Master.g_AccountsList.GetAccountByKey( lKey );
					if( null != friendAcct )
					{
						lblUserNameValue.Text = friendAcct.UserName;
						lblNameLabel.Text = friendAcct.DisplayName();
						lblCityValue.Text = friendAcct.City;
						lblStateValue.Text = friendAcct.State;
						lblZipValue.Text = friendAcct.Zip;
						lblCountryValue.Text = friendAcct.Country;
						lblSportsInterestValue.Text = friendAcct.Preferences.Interests;
						if (acct.PhotoFile.Length > 0)
						{
							string strImageURL = "~/UsersFolder/" + friendAcct.UserName + "/DisplayPhoto/" + friendAcct.PhotoFile;
							imgUserPhoto.ImageUrl = strImageURL;
						}
					}
				}
			}

		}
	}
}