using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

[assembly: TagPrefix("MyUSC.Classes.MyOrg", "MyUSC.Classes.MyOrg")]
namespace MyUSC.Classes.MyOrg
{
	[
		ToolboxData("<{0}:SeasonDetails ID='sdSeasonDetailsID' runat=\"server\"> </{0}:SeasonDetails>")
	]
	public partial class SeasonDetails : System.Web.UI.UserControl
	{

		protected SeasonDetails()
		{
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			hfSeasonID.Value = "-1";
			txtSeasonName.Text = "";
			txtComments.Text = "";
			chkDefault.Checked = false;
			chkShare.Checked = false;
		}

		public void InitSeason(Season season)
		{
			if (null != season)
			{
				hfSeasonID.Value = season.SeasonID.ToString();
				txtSeasonName.Text = season.SeasonName;
				txtComments.Text = season.Comments;
				dsStartDate.SetDate( season.SeasonDate );
				chkDefault.Checked = season.IsDefault;
				chkShare.Checked = season.Share;
			}
			else
			{
				hfSeasonID.Value = "-1";
			}
		}

		protected void GetData( Season season )
		{
			season.SeasonName = txtSeasonName.Text;
			season.Comments = txtComments.Text;
			season.IsDefault = chkDefault.Checked;
			season.Share = chkShare.Checked;

			DateTime dt = new DateTime();
			if( DateTime.TryParse( dsStartDate.ToString(), out dt ) )
			{
				season.SeasonDate = dt;
			}
		}

		protected void OnClickOK(object sender, EventArgs e)
		{
			Season season = null;
			long lOrgID = ((USCPageBase)Parent).GetSessionOrgID();
			UserAccount acct = ((USCMaster)((USCPageBase)Parent).MasterPg).GetActiveUser();
			Organization org = ((USCMaster)((USCPageBase)Parent).MasterPg).g_OrgList.GetOrganization(lOrgID);

			string strSeasonID = hfSeasonID.Value;
			if( strSeasonID == "-1" || strSeasonID == "" )
			{
				season = new Season();
			}
			else
			{
				long lSeasonID = Convert.ToInt64( strSeasonID );
				season = org.orgSeasonList.GetSeason( lSeasonID );
			}

			GetData( season );

			if( -1 == season.SeasonID )
			{
				season.Creator = acct.UserName;
				org.orgSeasonList.Add( season );
			}
			else
			{
				season.Update( acct );
			}
		}

		protected void OnClickCancel(object sender, EventArgs e)
		{

		}
	}
}