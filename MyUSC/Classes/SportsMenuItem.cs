using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MyUSC.Classes;

namespace MyUSC.Classes
{
	public class SportsMenuItem : USCBaseItem
	{
		protected long m_lRSSFeedKey;
		protected string m_strName;
		protected string m_strDescription;
		protected string m_strLogoUrl;
		protected string m_strWebsite;
		protected string m_strRSSNotes;
		protected float m_flSequence;

		private void Init()
		{
			m_lRSSFeedKey = 0;
			m_strName = "";
			m_strDescription = "";
			m_strLogoUrl = "";
			m_flSequence = 1;
			m_strWebsite = "";
			m_strRSSNotes = "";
		}

		public SportsMenuItem()
		{
			Init();
		}

#region Accessors
		public long RSSFeedKey
		{
			get
			{
				return this.m_lRSSFeedKey;
			}
			set
			{
				this.m_lRSSFeedKey = value;
			}
		}

		public string Name
		{
			get
			{
				return this.m_strName;
			}
			set
			{
				this.m_strName = value;
			}
		}

		public string Description
		{
			get
			{
				return this.m_strDescription;
			}
			set
			{
				this.m_strDescription = value;
			}
		}

		public string LogoUrl
		{
			get
			{
				return this.m_strLogoUrl;
			}
			set
			{
				this.m_strLogoUrl = value;
			}
		}

		public float Sequence
		{
			get
			{
				return this.m_flSequence;
			}
			set
			{
				this.m_flSequence = value;
			}
		}

		public string Website
		{
			get
			{
				return this.m_strWebsite;
			}
			set
			{
				this.m_strWebsite = value;
			}
		}

		public string RSSNotes
		{
			get
			{
				return this.m_strRSSNotes;
			}
			set
			{
				this.m_strRSSNotes = value;
			}
		}

#endregion
	}
}