using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Security;

namespace MyUSC.Classes.MyOrg
{
	public class Affiliate : USCBaseItem
	{
#region Member Variables
		private long m_lOrgID;
		private long m_lAffiliateID;
		private long m_lParentID;
		private AffiliateTypes m_nAffiliateType;
		private string m_strNote;
		private bool m_fDeleted;
#endregion

#region Init
		protected void InitAffiliate()
		{
			m_lOrgID = -1;
			m_lAffiliateID = -1;
			m_lParentID = -1;
			m_nAffiliateType = AffiliateTypes.Undefined;
			m_strNote = "";
			m_fDeleted = false;
		}

		public Affiliate()
		{
			InitAffiliate();
		}
#endregion

#region Accessors
		public long AffiliateIdx
		{
			get
			{
				return this.m_lKey;
			}
			set
			{
				this.m_lKey = value;
			}
		}

		public long OrgID
		{
			get
			{
				return this.m_lOrgID;
			}
			set
			{
				this.m_lOrgID = value;
			}
		}

		public long AffiliateID
		{
			get
			{
				return this.m_lAffiliateID;
			}
			set
			{
				this.m_lAffiliateID = value;
			}
		}

		public long ParentID
		{
			get
			{
				return this.m_lParentID;
			}
			set
			{
				this.m_lParentID = value;
			}
		}

		public AffiliateTypes AffiliateType
		{
			get
			{
				return this.m_nAffiliateType;
			}
			set
			{
				this.m_nAffiliateType = value;
			}
		}

		public string Note
		{
			get
			{
				return this.m_strNote;
			}
			set
			{
				this.m_strNote = value;
			}
		}

		public bool Deleted
		{
			get
			{
				return this.m_fDeleted;
			}
			set
			{
				this.m_fDeleted = value;
			}
		}

		public string GetAffiliateTypeString()
		{
//	public enum AffiliateTypes { Undefined = -1, Sponsor = 0, Charter = 1, Parent = 2, Peer = 3, Child = 4, None = 10 }
			string strRet = "";
			switch( m_nAffiliateType )
			{
				case AffiliateTypes.Sponsor:
				{
					strRet = "Sponsor";
					break;
				}
				case AffiliateTypes.Charter:
				{
					strRet = "Charter";
					break;
				}
				case AffiliateTypes.Parent:
				{
					strRet = "Parent";
					break;
				}
				case AffiliateTypes.Peer:
				{
					strRet = "Peer";
					break;
				}
				case AffiliateTypes.Child:
				{
					strRet = "Child";
					break;
				}
				default:
				{
					strRet = "Other";
					break;
				}
			}

			return strRet;
		}

#endregion

#region Update

/*
CREATE PROCEDURE sp_UpdateAffiliate
	@OrgID bigint, 
	@AffiliateID bigint,
	@AffiliateType int,
	@ParentID bigint,
	@Note nvarchar(200),
	@Deleted bit,
	@Update nvarchar(max),
	@Key bigint
*/
		public bool Update(UserAccount acct)
		{
			bool fRet = false;
			string strClean = acct.UserName + " -- " + DateTime.Now.ToString();
			this.LastUpdate = Sanitize(strClean);

			SqlParameter[] paramArray = new SqlParameter[8];
			paramArray[0] = new SqlParameter("@OrgID", OrgID);
			paramArray[1] = new SqlParameter("@AffiliateID", this.AffiliateID);
			paramArray[2] = new SqlParameter("@AffiliateType", this.AffiliateType);
			paramArray[3] = new SqlParameter("@ParentID", this.ParentID);
			paramArray[4] = new SqlParameter("@Note", Sanitize(USCBase.Truncate(this.Note, 199, true)));
			paramArray[5] = new SqlParameter("@Deleted", SQLBitFromBool(this.Deleted));
			paramArray[6] = new SqlParameter("@Update", this.LastUpdate);
			paramArray[7] = new SqlParameter("@Key", Key);

			if (ExecuteSPNoValue("sp_UpdateOrgAffiliate", paramArray))
			{
				fRet = true;
			}

			return fRet;
		}
#endregion

	}

	public class AffiliatesList : USCBaseList
	{
#region Column Constants
		const int colKey = 0;
		const int colOrgID = 1;
		const int colAffiliateID = 2;
		const int colAffiliateType = 3;
		const int colParentID = 4;
		const int colNote = 5;
		const int colDeleted = 6;
		const int colCreator = 7;
		const int colCreateDate = 8;
		const int colLastUpdate = 9;
#endregion

		private long m_lOrgID;

		public AffiliatesList()
		{
			m_lOrgID = -1;
		}

		public long OrgID
		{
			get
			{
				return this.m_lOrgID;
			}
			set
			{
				this.m_lOrgID = value;
			}
		}

		public SortedDictionary<long, object> m_sdAffiliates = new SortedDictionary<long, object>();

		public void Init(string cnxString, long orgID, bool fForce)
		{
			m_strConnectionString = cnxString;
			OrgID = orgID;
			Load();
		}

#region Load
		public bool Load()
		{
			bool fRet = false;

			m_sdAffiliates.Clear();

			if (0 > OrgID)
			{
				return false;
			}

			SqlDataReader reader = null;
			SqlConnection sqlConn = null;
			SqlParameter[] paramArray = new SqlParameter[1];
			paramArray[0] = new SqlParameter("@OrgID", OrgID);

			if (ExecuteSPRows("sp_GetOrgAffiliates", paramArray, out reader, out sqlConn) && null != reader)
			{
				try
				{
					while (reader.Read())
					{
						IDataRecord dr = (IDataRecord)reader;
						Affiliate affiliate = new Affiliate();
						fRet = ReadAffiliate(dr, affiliate);
						if( fRet && affiliate.Deleted != true )
						{
							long lIndex = m_sdAffiliates.Count + 1;
							m_sdAffiliates.Add( lIndex, affiliate );
						}
					}
					reader.Close();
					sqlConn.Close();
					fRet = true;
				}
				catch (Exception ex)
				{
					String strIntro = "Error loading Affiliate for org ID = " + OrgID;
					EvtLog.WriteException( strIntro, ex, 1 );
				}
			}

			return fRet;
		}

		private bool ReadAffiliate(IDataRecord dr, Affiliate affiliate)
		{
			bool fRet = true;
			try
			{
				affiliate.ConnectionString = m_strConnectionString;
				affiliate.Key = ObjectToLong(dr[colKey]);
				affiliate.OrgID = ObjectToLong(dr[colOrgID]);
				affiliate.AffiliateID = ObjectToLong(dr[colAffiliateID]);
				affiliate.AffiliateType = (AffiliateTypes)ObjectToInt(dr[colAffiliateType]);
				affiliate.ParentID = ObjectToLong(dr[colParentID]);
				affiliate.Note = ObjectToString(dr[colNote]);
				affiliate.Deleted = ObjectToBool(dr[colDeleted]);

				affiliate.Creator = ObjectToString(dr[colCreator]);
				affiliate.CreateDate = ObjectToDateTime(dr[colCreateDate]);
				affiliate.LastUpdate = ObjectToString(dr[colLastUpdate]);
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("MyOrgAffiliates.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}
#endregion Load

		public int Count
		{
			get
			{
				return m_sdAffiliates.Count;
			}
		}

		public Affiliate GetAffiliate(long index)
		{
			Affiliate affiliate = null;
			foreach (KeyValuePair<long, object> kvp in m_sdAffiliates)
			{
				Affiliate affT = (Affiliate)kvp.Value;
				if( null != affT && affT.AffiliateID == index )
				{
					affiliate = affT;
					break;
				}
			}
			return affiliate;
		}

/*
CREATE PROCEDURE sp_AddAffiliate
	@OrgID bigint, 
	@AffiliateID bigint, 
	@AffiliateType int, 
	@ParentID bigint,
	@Note nvarchar(200),
	@Creator nvarchar(50)
*/
		public bool Add(Affiliate affiliate)
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[6];
			paramArray[0] = new SqlParameter("@OrgID", affiliate.OrgID);
			paramArray[1] = new SqlParameter("@AffiliateID", affiliate.AffiliateID);
			paramArray[2] = new SqlParameter("@AffiliateType", affiliate.AffiliateType);
			paramArray[3] = new SqlParameter("@ParentID", affiliate.ParentID);
			paramArray[4] = new SqlParameter("@Note", Sanitize(USCBase.Truncate(affiliate.Note, 199, true)));
			paramArray[5] = new SqlParameter("@Creator", Sanitize(USCBase.Truncate(affiliate.Creator, 49, true)));

			affiliate.Key = ExecuteSPInsert("sp_AddOrgAffiliate", paramArray);
			if (affiliate.Key > 0)
			{
				Load();
				fRet = true;
			}

			return fRet;
		}

		public bool Update(Affiliate affiliate, UserAccount acct)
		{
			return affiliate.Update(acct);
		}

		public bool Delete(Affiliate affiliate, UserAccount acct)
		{
			// TODO: Delete for real
			affiliate.Deleted = true;
			return affiliate.Update(acct);
		}

	}

}