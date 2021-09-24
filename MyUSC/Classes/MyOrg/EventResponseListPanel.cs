using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

[assembly: TagPrefix("MyUSC.Classes", "MSC")]
namespace MyUSC.Classes.MyOrg
{
	[
		ToolboxData("<{0}:EventResponseListPanel ID='erlpID' runat=\"server\"> </{0}:EventResponseListPanel>")
	]
	public class EventResponseListPanel : TBSCPanel
	{
		public Table m_tblResponseList;

		public EventResponseListPanel()
		{
			this.Visible = false;

			// Add the section title
			Table tbl = new Table();
			tbl.CssClass = "tblDlgEventResponseList";
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgControlTitle";
			TableCell td = new TableCell();
			td.CssClass = "tdDlgControlNormal";
			Label lbl = new Label();
			lbl.Text = "Member Responses";
			td.Controls.Add( lbl );
			tr.Controls.Add( td );
			tbl.Controls.Add( tr );

			tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			td = new TableCell();
			td.CssClass = "tdDlgControlNormal";
			m_tblResponseList = new Table();
			m_tblResponseList.CssClass = "tblDlgEventResponseList";
			td.Controls.Add( m_tblResponseList );
			tr.Controls.Add( td );
			tbl.Controls.Add( tr );

			// Add the OK button at the bottom
			tr = new TableRow();
			tr.CssClass = "trDlgControlNormal";
			td = new TableCell();
			td.CssClass = "tdDlgControlCenter";
			TBSCButton btn = new TBSCButton();
			btn.CssClass = "btnOK";
			btn.Click += OnClickOK;
			btn.Text = "OK";
			td.Controls.Add( btn );
			tr.Controls.Add( td );
			tbl.Controls.Add( tr );

			this.Controls.Add( tbl );
		}

		public bool ShowList
		{
			get
			{
				return this.Visible;
			}
			set
			{
				this.Visible = value;
			}
		}

		protected void DrawEmptyList()
		{
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgEventResponseList";

			TableCell td = new TableCell();
			td.CssClass = "tdDlgEventResponseListHead";
			Label lbl = new Label();
			lbl.Text = "No responses found for this event.";
			td.Controls.Add( lbl);
			tr.Controls.Add( td );

			m_tblResponseList.Controls.Add( tr );
		}

		protected void DrawHeader()
		{
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgEventResponseList";

			TableCell td = new TableCell();
			td.CssClass = "tdDlgEventResponseListLeft";
			Label lbl = new Label();
			lbl.Text = "Name";
			td.Controls.Add( lbl);
			tr.Controls.Add( td );

			td = new TableCell();
			td.CssClass = "tdDlgEventResponseListCenter";
			lbl = new Label();
			lbl.Text = "Comments";
			td.Controls.Add( lbl);
			tr.Controls.Add( td );

			td = new TableCell();
			td.CssClass = "tdDlgEventResponseListRight";
			lbl = new Label();
			lbl.Text = "Last Login";
			td.Controls.Add( lbl);
			tr.Controls.Add( td );

			m_tblResponseList.Controls.Add( tr );
		}

		protected void DrawSectionHeader( string strSection )
		{
			TableRow tr = new TableRow();
			tr.CssClass = "trDlgEventResponseList";

			TableCell td = new TableCell();
			td.CssClass = "tdDlgEventResponseListHead";
			td.ColumnSpan = 3;
			Label lbl = new Label();
			lbl.Text = strSection;
			td.Controls.Add( lbl);
			tr.Controls.Add( td );

			m_tblResponseList.Controls.Add( tr );
		}

		protected bool DrawResponse( USCPageBase parentPage, EventResponse evtr )
		{
			bool fRet = false;

			UserAccount acct = parentPage.GetUserFromID( evtr.MemberID );
			if( null != acct )
			{
				TableRow tr = new TableRow();
				tr.CssClass = "trDlgEventResponseList";

				TableCell td = new TableCell();
				td.CssClass = "tdDlgEventResponseListLeft";
				Label lbl = new Label();
				lbl.Text = acct.DisplayName();
				td.Controls.Add( lbl);
				tr.Controls.Add( td );

				td = new TableCell();
				td.CssClass = "tdDlgEventResponseListCenter";
				lbl = new Label();
				lbl.Text = evtr.Notes;
				td.Controls.Add( lbl);
				tr.Controls.Add( td );

				td = new TableCell();
				td.CssClass = "tdDlgEventResponseListRight";
				lbl = new Label();
				lbl.Text = evtr.ResponseDate.ToShortDateString();
				td.Controls.Add( lbl);
				tr.Controls.Add( td );

				m_tblResponseList.Controls.Add( tr );

				fRet = true;
			}
			else
			{
				string strErr = "DrawResponse: Unable to load user acct ";
				strErr += evtr.MemberID;
				EvtLog.WriteEvent( strErr, EventLogEntryType.Error, 0, 0 );
			}

			return fRet;
		}

		public bool LoadResponseList( USCPageBase parentPage, long lOrgID, long lEventID )
		{
			bool fRet = true;
			bool fListDrawn = false;
			Organization org = parentPage.GetOrgByID(lOrgID);
			EventResponse.ResponseTypes rtLast = EventResponse.ResponseTypes.Yes;

			m_tblResponseList.Controls.Clear();

			long lIndex = 0;
			foreach( KeyValuePair<long, object> kvp in org.orgEventResponseList.m_sdOrgEventResponse )
			{
				EventResponse evtr = (EventResponse)kvp.Value;
				if( null != evtr )
				{
					// Responses should be in eventID order.  If the ID is smaller just fall through.

					// If its the ID we are looking for, draw it
					if( evtr.EventID == lEventID )
					{
						lIndex++;
						// Special case for first response
						if( lIndex == 1 )
						{
							DrawHeader();
							rtLast = evtr.Response;
							DrawSectionHeader( evtr.ResponseTypeToString() );
						}
						else
						{
							// Check to see if we need to start a new section
							if( evtr.Response > rtLast )
							{
								rtLast = evtr.Response;
								DrawSectionHeader( evtr.ResponseTypeToString() );
							}
						}
						DrawResponse( parentPage, evtr );
						fListDrawn = true;
					}
					else if( evtr.EventID > lEventID )
					{
						// If the ID is larger just fall through.
						break;
					}
				}

				if( 0 == lIndex && evtr.EventID == lEventID )
				{
					DrawEmptyList();
					fListDrawn = true;
				}
				
			}

			if( !fListDrawn )
			{
				DrawEmptyList();
			}
			return fRet;
		}

		protected void OnClickOK(object sender, EventArgs e)
		{
			m_tblResponseList.Controls.Clear();
			this.Visible = false;
		}
	}
}