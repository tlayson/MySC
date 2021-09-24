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
		ToolboxData("<{0}:DateSelectTable ID='dstID' runat=\"server\"> </{0}:DateSelectTable>")
	]
	public class DateSelectTable : Table
	{
		public enum DateSelectTypes { Historic = 1, CurrentRange = 2, Future = 3 }

		private TBSCDropDown m_ddlYear;
		private TBSCDropDown m_ddlMonth;
		private TBSCDropDown m_ddlDay;
		private DateSelectTypes m_dstDateType;

		private void InitControls( DateSelectTypes dst )
		{
			m_ddlDay = new TBSCDropDown();
			m_ddlMonth = new TBSCDropDown();
			m_ddlYear = new TBSCDropDown();

			TableRow tr = new TableRow();
			this.Controls.Add( tr );

			TableCell td = new TableCell();
			td.Controls.Add( m_ddlMonth );
			FillMonthDD();
			tr.Controls.Add( td );

			td = new TableCell();
			td.Controls.Add( m_ddlDay );
			FillDayDD();
			tr.Controls.Add( td );

			td = new TableCell();
			td.Controls.Add( m_ddlYear );
			FillYearDD( dst );
			tr.Controls.Add( td );
		}
		
		public DateSelectTable( DateSelectTypes dst )
		{
			InitControls( dst );
		}

		public DateSelectTable()
		{
			InitControls( m_dstDateType );
		}

		private void FillMonthDD()
		{
			ListItem li = new ListItem( "---", "" );
			m_ddlMonth.Items.Add( li );
			li = new ListItem( "January", "1" );
			m_ddlMonth.Items.Add( li );
			li = new ListItem( "February", "2" );
			m_ddlMonth.Items.Add( li );
			li = new ListItem( "March", "3" );
			m_ddlMonth.Items.Add( li );
			li = new ListItem( "April", "4" );
			m_ddlMonth.Items.Add( li );
			li = new ListItem( "May", "5" );
			m_ddlMonth.Items.Add( li );
			li = new ListItem( "June", "6" );
			m_ddlMonth.Items.Add( li );
			li = new ListItem( "July", "7" );
			m_ddlMonth.Items.Add( li );
			li = new ListItem( "August", "8" );
			m_ddlMonth.Items.Add( li );
			li = new ListItem( "September", "9" );
			m_ddlMonth.Items.Add( li );
			li = new ListItem( "October", "10" );
			m_ddlMonth.Items.Add( li );
			li = new ListItem( "November", "11" );
			m_ddlMonth.Items.Add( li );
			li = new ListItem( "December", "12" );
			m_ddlMonth.Items.Add( li );
		}

		private void FillDayDD()
		{
			int nDay = 1;
			ListItem li = new ListItem();
			li.Text = "---";
			li.Value = "";
			m_ddlDay.Items.Add(li);

			while (nDay <= 31)
			{
				li = new ListItem();
				li.Text = nDay.ToString();
				li.Value = nDay.ToString();
				m_ddlDay.Items.Add(li);
				nDay++;
			}

		}

		private void FillYearDD( DateSelectTypes dst )
		{
			m_ddlYear.Items.Clear();

			ListItem li = new ListItem();
			li.Text = "---";
			li.Value = "";
			m_ddlYear.Items.Add(li);

			int nYear = DateTime.Now.Year;
			int nStop = nYear - 130;
			if( dst == DateSelectTypes.CurrentRange )
			{
				nYear = DateTime.Now.Year + 20;
				nStop = nYear - 20;
			}
			else if( dst == DateSelectTypes.Future )
			{
				nYear = DateTime.Now.Year;
				nStop = nYear + 20;
			}

			if( dst == DateSelectTypes.Future )
			{
				while( nYear <= nStop )
				{
					li = new ListItem();
					li.Text = nYear.ToString();
					li.Value = nYear.ToString();
					m_ddlYear.Items.Add(li);
					nYear++;
				}
			}
			else
			{
				while( nYear >= nStop )
				{
					li = new ListItem();
					li.Text = nYear.ToString();
					li.Value = nYear.ToString();
					m_ddlYear.Items.Add(li);
					nYear--;
				}
			}
		}

		public DateSelectTypes DateType
		{
			get
			{
				return m_dstDateType;
			}
			set
			{
				m_dstDateType = value;
			}
		}

		public string Month
		{
			get
			{
				return m_ddlMonth.SelectedValue;
			}
			set
			{
				m_ddlMonth.SelectedValue = value;
			}
		}

		public string Day
		{
			get
			{
				return m_ddlDay.SelectedValue;
			}
			set
			{
				m_ddlDay.SelectedValue = value;
			}
		}

		public string Year
		{
			get
			{
				return m_ddlYear.SelectedValue;
			}
			set
			{
				m_ddlYear.SelectedValue = value;
			}
		}

		public void Enable( bool fEnable )
		{
			m_ddlMonth.Enabled = fEnable;
			m_ddlDay.Enabled = fEnable;
			m_ddlYear.Enabled = fEnable;
		}

		public void SetDate( string strDate )
		{
			DateTime dt = new DateTime();
			if( DateTime.TryParse( strDate, out dt ) )
			{
				SetDate( dt );
			}
			else
			{
				Month = "";
				Day = "";
				Year = "";
			}
		}

		public void SetDate( DateTime dt )
		{
			if (null != dt && dt.Year > 1850 )
			{
				Month = dt.Month.ToString();
				Day = dt.Day.ToString();
				Year = dt.Year.ToString();
			}
			else
			{
				Month = "";
				Day = "";
				Year = "";
			}
		}

		public override string ToString()
		{
			string strRet = "";
			string strMonth = Month;
			string strDay = Day;
			string strYear = Year;

			if (strMonth.Length > 0 && strDay.Length > 0 && strYear.Length > 0)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(strYear);
				sb.Append("-");
				sb.Append(strMonth);
				sb.Append("-");
				sb.Append(strDay);

				strRet = sb.ToString();
			}

			return strRet;
		}

	}
}