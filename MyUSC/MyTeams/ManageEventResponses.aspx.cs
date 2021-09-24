using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.MyTeams
{
	public partial class ManageEventResponses : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Master.SelectMTMenuItemOnPageLoad(MyTeamsPage.Schedule);
		}

		protected void DrawResponse( Table tbl, UserAccount user, EventResponse resp )
		{
			TableRow tr = new TableRow();
			tr.CssClass = "trNormal";

			//Player
			TableCell td1 = new TableCell();
			td1.CssClass = "tdInput";
			Label lblPlayer = new Label();
			lblPlayer.CssClass = "medNormalText";
			lblPlayer.Text = user.DisplayName();
			td1.Controls.Add( lblPlayer );

			//Response
			TableCell td2 = new TableCell();
			td2.CssClass = "tdInput";
			TBSCDropDown ddResponse = new TBSCDropDown();
			ListItem li = new ListItem("Yes", "1");
			if( resp.Response == EventResponse.ResponseTypes.Yes )
			{
				li.Selected = true;
			}
			ddResponse.Items.Add(li);
			li = new ListItem("No", "2");
			if (resp.Response == EventResponse.ResponseTypes.No)
			{
				li.Selected = true;
			}
			ddResponse.Items.Add(li);
			li = new ListItem("Maybe", "3");
			if (resp.Response == EventResponse.ResponseTypes.Maybe)
			{
				li.Selected = true;
			}
			ddResponse.Items.Add(li);
			li = new ListItem("None", "4");
			if (resp.Response == EventResponse.ResponseTypes.NoResponse ||
			resp.Response == EventResponse.ResponseTypes.Undefined )
			{
				li.Selected = true;
			}
			ddResponse.Items.Add(li);
			ddResponse.AutoPostBack = true;

			td2.Controls.Add( ddResponse );


			//Login
			TableCell td3 = new TableCell();
			td3.CssClass = "tdInput";
			Label lblLogin = new Label();
			lblLogin.CssClass = "medNormalText";
			lblLogin.Text = user.LastLogin.ToShortDateString();
			td3.Controls.Add(lblLogin);

			// Notes
			TableCell td4 = new TableCell();
			td4.CssClass = "tdInput";
			Label lblNotes = new Label();
			lblNotes.CssClass = "smNormalText";
			lblNotes.Text = resp.Notes;
			td4.Controls.Add(lblNotes);

			tr.Controls.Add( td1 );
			tr.Controls.Add( td2 );
			tr.Controls.Add( td3 );
			tr.Controls.Add( td4 );
			
			tbl.Controls.Add( tr );
		}
	}
}