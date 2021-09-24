using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyUSC.Classes
{
	public class Address : USCBaseItem
	{
#region Member Variables
		protected string m_strAddress1;
		protected string m_strAddress2;
		protected string m_strCity;
		protected string m_strState;
		protected string m_strZip;
		protected string m_strCountry;
		protected string m_strEmail;
		protected bool m_bEmailValid;
		protected string m_strPhone;
		protected string m_strCell;
		protected string m_strFax;
		protected string m_strURL;
#endregion

#region INIT
		public Address()
		{
			InitAddress();
		}

		protected void InitAddress()
		{
			m_strAddress1 = "";
			m_strAddress2 = "";
			m_strCity = "";
			m_strState = "";
			m_strZip = "";
			m_strCountry = "";
			m_strEmail = "";
			m_bEmailValid = false;
			m_strPhone = "";
			m_strCell = "";
			m_strFax = "";
			m_strURL = "";
		}

#endregion

#region Accessors
		public string Address1
		{
			get
			{
				return this.m_strAddress1;
			}
			set
			{
				this.m_strAddress1 = value;
			}
		}

		public string Address2
		{
			get
			{
				return this.m_strAddress2;
			}
			set
			{
				this.m_strAddress2 = value;
			}
		}

		public string City
		{
			get
			{
				return this.m_strCity;
			}
			set
			{
				this.m_strCity = value;
			}
		}

		public string State
		{
			get
			{
				return this.m_strState;
			}
			set
			{
				this.m_strState = value;
			}
		}

		public string Zip
		{
			get
			{
				return this.m_strZip;
			}
			set
			{
				this.m_strZip = value;
			}
		}

		public string Country
		{
			get
			{
				return this.m_strCountry;
			}
			set
			{
				this.m_strCountry = value;
			}
		}

		public string Email
		{
			get
			{
				return this.m_strEmail;
			}
			set
			{
				this.m_strEmail = value;
			}
		}

		public bool EmailValid
		{
			get
			{
				return this.m_bEmailValid;
			}
			set
			{
				this.m_bEmailValid = value;
			}
		}

		public string Phone
		{
			get
			{
				return this.m_strPhone;
			}
			set
			{
				this.m_strPhone = value;
			}
		}

		public string Cell
		{
			get
			{
				return this.m_strCell;
			}
			set
			{
				this.m_strCell = value;
			}
		}

		public string Fax
		{
			get
			{
				return this.m_strFax;
			}
			set
			{
				this.m_strFax = value;
			}
		}

		public string URL
		{
			get
			{
				return this.m_strURL;
			}
			set
			{
				this.m_strURL = value;
			}
		}

#endregion

	}
}