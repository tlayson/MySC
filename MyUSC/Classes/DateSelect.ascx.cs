using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyUSC.Classes
{
	public partial class DateSelect : System.Web.UI.UserControl
	{
		
		protected void InitControls()
		{
		}

		public override string ToString()
		{
			return m_dateSelectTable.ToString();
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			InitControls();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		public void SetDate( string strDate )
		{
			m_dateSelectTable.SetDate( strDate );
		}

		public void SetDate( DateTime dt )
		{
			m_dateSelectTable.SetDate( dt );
		}

		public string Month
		{
			get
			{
				return m_dateSelectTable.Month;
			}
			set
			{
				m_dateSelectTable.Month = value;
			}
		}

		public string Day
		{
			get
			{
				return m_dateSelectTable.Day;
			}
			set
			{
				m_dateSelectTable.Day = value;
			}
		}

		public string Year
		{
			get
			{
				return m_dateSelectTable.Year;
			}
			set
			{
				m_dateSelectTable.Year = value;
			}
		}

		public void Enable( bool fEnable )
		{
			m_dateSelectTable.Enable( fEnable );
		}
	}
}