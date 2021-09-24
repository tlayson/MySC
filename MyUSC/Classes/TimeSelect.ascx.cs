using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyUSC.Classes
{
	public partial class TimeSelect : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public string ToString( bool f24Hr )
		{
			return m_timeSelectTable.ToString( f24Hr );
		}

		public override string ToString()
		{
			return m_timeSelectTable.ToString( true );
		}

		public void SetDate(string strDate)
		{
			m_timeSelectTable.SetDate( strDate );
		}

		public void SetDate(DateTime dt)
		{
			m_timeSelectTable.SetDate( dt );
		}

		public string Hour
		{
			get
			{
				return m_timeSelectTable.Hour;
			}
			set
			{
				m_timeSelectTable.Hour = value;
			}
		}

		public string Minute
		{
			get
			{
				return m_timeSelectTable.Minute;
			}
			set
			{
				m_timeSelectTable.Minute = value;
			}
		}

		public string AMPM
		{
			get
			{
				return m_timeSelectTable.AMPM;
			}
			set
			{
				m_timeSelectTable.AMPM = value;
			}
		}
	}
}