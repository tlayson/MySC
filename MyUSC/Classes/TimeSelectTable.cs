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
namespace MyUSC.Classes
{
	[
		ToolboxData("<{0}:TimeSelectTable ID='tstID' runat=\"server\"> </{0}:TimeSelectTable>")
	]
	public class TimeSelectTable : Table
	{
		private TBSCDropDown m_ddlHour;
		private TBSCDropDown m_ddlMinute;
		private TBSCDropDown m_ddlAMPM;

		public string Hour
		{
			get
			{
				return m_ddlHour.SelectedValue;
			}
			set
			{
				m_ddlHour.SelectedValue = value;
			}
		}

		public string Minute
		{
			get
			{
				return m_ddlMinute.SelectedValue;
			}
			set
			{
				m_ddlMinute.SelectedValue = value;
			}
		}

		public string AMPM
		{
			get
			{
				return m_ddlAMPM.SelectedValue;
			}
			set
			{
				m_ddlAMPM.SelectedValue = value;
			}
		}

		public TimeSelectTable()
		{
			m_ddlHour = new TBSCDropDown();
			m_ddlMinute = new TBSCDropDown();
			m_ddlAMPM = new TBSCDropDown();

			TableRow tr = new TableRow();
			this.Controls.Add( tr );

			TableCell td = new TableCell();
			td.Controls.Add( m_ddlHour );
			FillHourDD();
			tr.Controls.Add( td );

			td = new TableCell();
			td.Controls.Add( m_ddlMinute );
			FillMinuteDD();
			tr.Controls.Add( td );

			td = new TableCell();
			td.Controls.Add( m_ddlAMPM );
			FillAMPMDD();
			tr.Controls.Add( td );
		}

		private void FillHourDD()
		{
			ListItem li = new ListItem( "12", "12" );
			m_ddlHour.Items.Add( li );
			li = new ListItem( "1", "1" );
			m_ddlHour.Items.Add( li );
			li = new ListItem( "2", "2" );
			m_ddlHour.Items.Add( li );
			li = new ListItem( "3", "3" );
			m_ddlHour.Items.Add( li );
			li = new ListItem( "4", "4" );
			m_ddlHour.Items.Add( li );
			li = new ListItem( "5", "5" );
			m_ddlHour.Items.Add( li );
			li = new ListItem( "6", "6" );
			m_ddlHour.Items.Add( li );
			li = new ListItem( "7", "7" );
			m_ddlHour.Items.Add( li );
			li = new ListItem( "8", "8" );
			m_ddlHour.Items.Add( li );
			li = new ListItem( "9", "9" );
			m_ddlHour.Items.Add( li );
			li = new ListItem( "10", "10" );
			m_ddlHour.Items.Add( li );
			li = new ListItem( "11", "11" );
			m_ddlHour.Items.Add( li );
		}

		private void FillMinuteDD()
		{
			ListItem li = new ListItem( "00", "00" );
			m_ddlMinute.Items.Add( li );
			li = new ListItem( "05", "05" );
			m_ddlMinute.Items.Add( li );
			li = new ListItem( "10", "10" );
			m_ddlMinute.Items.Add( li );
			li = new ListItem( "15", "15" );
			m_ddlMinute.Items.Add( li );
			li = new ListItem( "20", "20" );
			m_ddlMinute.Items.Add( li );
			li = new ListItem( "25", "25" );
			m_ddlMinute.Items.Add( li );
			li = new ListItem( "30", "30" );
			m_ddlMinute.Items.Add( li );
			li = new ListItem( "35", "35" );
			m_ddlMinute.Items.Add( li );
			li = new ListItem( "40", "40" );
			m_ddlMinute.Items.Add( li );
			li = new ListItem( "45", "45" );
			m_ddlMinute.Items.Add( li );
			li = new ListItem( "50", "50" );
			m_ddlMinute.Items.Add( li );
			li = new ListItem( "55", "55" );
			m_ddlMinute.Items.Add( li );
		}

		private void FillAMPMDD()
		{
			ListItem li = new ListItem( "AM", "AM" );
			m_ddlAMPM.Items.Add( li );
			li = new ListItem( "PM", "PM" );
			m_ddlAMPM.Items.Add( li );
		}

		public string ToString( bool f24Hr )
		{
			string strRet = "";

			string strHour = Hour;
			string strMinute = Minute;

			if (strHour.Length > 0 && strMinute.Length > 0 )
			{
				StringBuilder sb = new StringBuilder();
				if( f24Hr && AMPM == "PM")
				{
					int nHour = int.Parse( strHour );
					nHour += 12;
					if( nHour >= 24 )
					{
						nHour = 12;
					}
					strHour = nHour.ToString();
				}
				sb.Append(strHour);
				sb.Append(":");
				sb.Append(strMinute);
				if( !f24Hr )
				{
					sb.Append(" ");
					sb.Append( AMPM );
				}

				strRet = sb.ToString();
			}

			return strRet;
		}

		public override string ToString()
		{
			return ToString( true );
		}

		public void SetDate(string strDate)
		{
			DateTime dt = new DateTime();
			if (DateTime.TryParse(strDate, out dt))
			{
				SetDate(dt);
			}
			else
			{
				Hour = "12";
				Minute = "00";
				AMPM = "AM";
			}
		}

		public void SetDate(DateTime dt)
		{
			if (null != dt )
			{
				bool fAM = true;
				int nHour = dt.Hour;
				if( nHour >= 12 )
				{
					if( nHour > 12 )
					{
						nHour -= 12;
					}
					fAM = false;
				}
				else if (nHour == 0)
				{
					nHour = 12;
					fAM = true;
				}

				Hour = nHour.ToString();
				Minute = dt.Minute.ToString();
				if( fAM )
				{
					AMPM = "AM";
				}
				else
				{
					AMPM = "PM";
				}
			}
			else
			{
				Hour = "12";
				Minute = "00";
				AMPM = "AM";
			}
		}

	}
}