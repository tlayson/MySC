using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

[assembly: TagPrefix("MyUSC.Classes", "MSC")]
namespace MyUSC.Classes.MyOrg
{
	[
		ToolboxData("<{0}:ViewEventDetailsTable ID='vedtID' runat=\"server\"> </{0}:ViewEventDetailsTable>")
	]
	public class ViewEventDetailsTable : Table
	{
#region Members
		private USCPageBase m_parentPage;
		private Organization m_org;
		private Event m_Event;
		private Label m_lblTitle;
		private TBSCButton m_btnOK;
#endregion

		public string Title
		{
			get
			{
				return this.m_lblTitle.Text;
			}
			set
			{
				this.m_lblTitle.Text = value;
			}
		}

#region Creation
		public ViewEventDetailsTable( long lOrgID )
		{
			this.CssClass = "tblDlgControlOuter";
			this.Width = 650;
		}
#endregion

#region Display
		public void SetEventDetails( USCPageBase parentPage, Organization org, Event evt )
		{
			m_parentPage = parentPage;
			m_org = org;
			m_Event = evt;

			AddTitleRow();
			AddOrgRow();
			AddSeasonRow();
			AddNameRow();
			AddTypeRow();
			AddVenueRow();
			AddDateRow();
			AddTimeRow();
			AddOpponentRow();
			AddHomeAwayRow();
			AddUniformRow();
			AddResultsRow();
			AddCommentsRow();
			AddWebsiteRow();
			AddOptionsRow();
			AddButtonsRow();
		}

#region AddRows
		protected void AddTitleRow()
		{
			//Title
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlTitle";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			td1.ColumnSpan = 2;
			m_lblTitle = new Label();
			m_lblTitle.Text = "Event Details";
			td1.Controls.Add( m_lblTitle );
			tr.Controls.Add( td1 );
			this.Controls.Add( tr );
		}
		
		protected void AddOrgRow()
		{
			//Org Name
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			td1.ColumnSpan = 2;
			Label lbl = new Label();
			lbl.Text = m_org.OrgName;
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			this.Controls.Add( tr );
		}
		
		protected void AddSeasonRow()
		{
			//Season
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Season : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			lbl = new Label();
			lbl.Text = "TODO: Load season name";
			td2.Controls.Add( lbl );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}
		
		protected void AddNameRow()
		{
			//Event Name
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Event Name : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			lbl = new Label();
			lbl.Text = m_Event.EventName;
			td2.Controls.Add( lbl );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}
		
		protected void AddTypeRow()
		{
			//Type
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Event Type : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			//Dropdown
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			lbl = new Label();
			lbl.Text = m_Event.EventTypeToString();
			td2.Controls.Add( lbl );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}
		
		protected void AddVenueRow()
		{
			//Venue
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Location : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			//Dropdown
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			lbl = new Label();
			if( m_Event.AltLocation.Length > 0 )
			{
				lbl.Text = m_Event.AltLocation;
			}
			else
			{
				Venue venue = m_parentPage.MasterPg.g_VenueList.GetVenue( m_Event.VenueID );
				if( null != venue )
				{
					//TODO: Make this a link
					lbl.Text = venue.VenueName;
				}
				else
				{
					lbl.Text = "TBD";
				}
			}
			td2.Controls.Add( lbl );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}
		
		protected void AddDateRow()
		{
			//Event Date
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Date : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			// Date Select
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			lbl = new Label();
			lbl.Text = m_Event.EventDate.ToShortDateString();
			td2.Controls.Add( lbl );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}
		
		protected void AddTimeRow()
		{
			//Time
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Time : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			// Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			lbl = new Label();
			lbl.Text = m_Event.EventDate.ToShortTimeString();
			td2.Controls.Add( lbl );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}

		protected void AddOpponentRow()
		{
			//Opponent
			if (m_Event.Opponent.Length > 0)
			{
				//Label
				TableRow tr = new TableRow();
				tr.CssClass = "trDlgControlNormal";
				TableCell td1 = new TableCell();
				td1.CssClass = "tdDlgControlNormal";
				Label lbl = new Label();
				lbl.Text = "Time : ";
				td1.Controls.Add(lbl);
				tr.Controls.Add(td1);
				// Value
				TableCell td2 = new TableCell();
				td2.CssClass = "tdDlgControlNormal";
				lbl = new Label();
				lbl.Text = m_Event.Opponent;
				td2.Controls.Add(lbl);
				tr.Controls.Add(td2);
				this.Controls.Add(tr);
			}
		}

		protected void AddHomeAwayRow()
		{
			//HomeAway
			//TODO: Figure out when to show this
			if (true)
			{
				//Label
				TableRow tr = new TableRow();
				tr.CssClass = "trDlgControlNormal";
				TableCell td1 = new TableCell();
				td1.CssClass = "tdDlgControlNormal";
				Label lbl = new Label();
				lbl.Text = "Home/Away : ";
				td1.Controls.Add(lbl);
				tr.Controls.Add(td1);
				//Value
				TableCell td2 = new TableCell();
				td2.CssClass = "tdDlgControlNormal";
				lbl = new Label();
				lbl.Text = m_Event.HomeAway;
				td2.Controls.Add(lbl);
				tr.Controls.Add(td2);
				this.Controls.Add(tr);
			}
		}

		protected void AddUniformRow()
		{
			//Uniform
			if( m_Event.Uniform.Length > 0 )
			{
				//Label
				TableRow tr = new TableRow();
				tr.CssClass = "trDlgControlNormal";
				TableCell td1 = new TableCell();
				td1.CssClass = "tdDlgControlNormal";
				Label lbl = new Label();
				lbl.Text = "Uniform : ";
				td1.Controls.Add(lbl);
				tr.Controls.Add(td1);
				//Value
				TableCell td2 = new TableCell();
				td2.CssClass = "tdDlgControlNormal";
				lbl = new Label();
				lbl.Text = m_Event.Uniform;
				td2.Controls.Add(lbl);
				tr.Controls.Add(td2);
				this.Controls.Add(tr);
			}
		}

		protected void AddResultsRow()
		{
			//Result
			if( m_Event.EventResult.Length > 0 )
			{
				//Label
				TableRow tr = new TableRow();
				tr.CssClass = "trDlgControlNormal";
				TableCell td1 = new TableCell();
				td1.CssClass = "tdDlgControlNormal";
				Label lbl = new Label();
				lbl.Text = "Result : ";
				td1.Controls.Add(lbl);
				tr.Controls.Add(td1);
				//Value
				TableCell td2 = new TableCell();
				td2.CssClass = "tdDlgControlNormal";
				lbl = new Label();
				lbl.Text = m_Event.EventResult;
				td2.Controls.Add(lbl);
				tr.Controls.Add(td2);
				this.Controls.Add(tr);
			}
		}

		protected void AddCommentsRow()
		{
			//Comments
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Comments :";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			this.Controls.Add(tr);

			//Textbox
			tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			Literal lit = new Literal();
			lit.Text = m_Event.Comments;
			td2.Controls.Add( lit );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}

		protected void AddWebsiteRow()
		{
			//Website
			if (m_Event.URL.Length > 0)
			{
				//Label
				TableRow tr = new TableRow();
				tr.CssClass = "trDlgControlNormal";
				TableCell td1 = new TableCell();
				td1.CssClass = "tdDlgControlNormal";
				Label lbl = new Label();
				lbl.Text = "Event Website : ";
				td1.Controls.Add(lbl);
				tr.Controls.Add(td1);
				//Value
				TableCell td2 = new TableCell();
				td2.CssClass = "tdDlgControlNormal";
				lbl = new Label();
				lbl.Text = m_Event.URL;
				td2.Controls.Add(lbl);
				tr.Controls.Add(td2);
				this.Controls.Add(tr);
			}
		}

		protected void AddOptionsRow()
		{
			//Response
			//Label
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Response requested : ";
			td1.Controls.Add( lbl );
			tr.Controls.Add( td1 );
			//Textbox
			TableCell td2 = new TableCell();
			td2.CssClass = "tdDlgControlNormal";
			lbl = new Label();
			if( m_Event.RequestResponse )
			{
				lbl.Text = "Yes";
			}
			else
			{
				lbl.Text = "No";
			}
			td2.Controls.Add( lbl );
			tr.Controls.Add( td2 );
			this.Controls.Add( tr );
		}

		protected void AddButtonsRow()
		{
			//OKCancel
			//OK
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			TableCell td1 = new TableCell();
			td1.CssClass = "tdCenter";
			td1.ColumnSpan = 2;
			m_btnOK = new TBSCButton();
			m_btnOK.Click += OnClickOK;
			m_btnOK.Text = "OK";
			m_btnOK.CssClass = "btnOK";
			m_btnOK.UserData = m_parentPage;
			td1.Controls.Add( m_btnOK );
			tr.Controls.Add( td1 );
		}

		protected void OnClickOK(object sender, EventArgs e)
		{
			TBSCButton btn = (TBSCButton)sender;
			USCPageBase parentPage = (USCPageBase)btn.UserData;
			if( null != parentPage )
			{
				parentPage.ProcessChildClick( 1 );
			}
		}
#endregion
#endregion
	}
}