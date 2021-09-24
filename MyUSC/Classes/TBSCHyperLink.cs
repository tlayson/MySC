using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

[assembly: TagPrefix("MyUSC.Classes", "MSC")]
namespace MyUSC.Classes
{
	[
		ToolboxData("<{0}:TBSCHyperLink ID='TBSCHyperLinkID' runat=\"server\"> </{0}:TBSCHyperLink>")
	]
	public class TBSCHyperLink : HyperLink
	{
		private object m_userData;
		private long m_userValue1;
		private long m_userValue2;
		private long m_userValue3;
		private long m_userValue4;

		public TBSCHyperLink()
		{
			m_userData = null;
			m_userValue1 = -1;
			m_userValue2 = -1;
			m_userValue3 = -1;
			m_userValue4 = -1;
		}

		public object UserData
		{
			get
			{
				return this.m_userData;
			}
			set
			{
				this.m_userData = value;
			}
		}
		
		public long UserValue1
		{
			get
			{
				return this.m_userValue1;
			}
			set
			{
				this.m_userValue1 = value;
			}
		}

		public long UserValue2
		{
			get
			{
				return this.m_userValue2;
			}
			set
			{
				this.m_userValue2 = value;
			}
		}

		public long UserValue3
		{
			get
			{
				return this.m_userValue3;
			}
			set
			{
				this.m_userValue3 = value;
			}
		}

		public long UserValue4
		{
			get
			{
				return this.m_userValue4;
			}
			set
			{
				this.m_userValue4 = value;
			}
		}
		
	}
}