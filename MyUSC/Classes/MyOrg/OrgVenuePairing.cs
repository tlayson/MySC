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
	public class OrgVenuePairing : USCBaseItem
	{
#region Member Variables
		private long m_lVenueID;
		private long m_lOrgID;
		private bool m_fHideVenue;
#endregion

#region Init
		protected void InitVenuePairing()
		{
			m_lVenueID = -1;
			m_lOrgID = -1;
			m_fHideVenue = false;
		}

		public OrgVenuePairing()
		{
			InitVenuePairing();
		}
#endregion

#region Accessors
		public long VenueID
		{
			get
			{
				return this.m_lVenueID;
			}
			set
			{
				this.m_lVenueID = value;
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

		public bool HideVenue
		{
			get
			{
				return this.m_fHideVenue;
			}
			set
			{
				this.m_fHideVenue = value;
			}
		}

#endregion

#region Update

/*
	@HideVenue bit,
	@LastUpdate nvarchar(max),
	@VenueID bigint,
	@OrgID bigint
*/
// Currently only hide/shows pairing in the orgs list

		public bool Update( UserAccount acct )
		{
			bool fRet = false;
			string strClean = acct.UserName + " -- " + DateTime.Now.ToString();
			this.LastUpdate = Sanitize(strClean);

			SqlParameter[] paramArray = new SqlParameter[4];
			paramArray[0] = new SqlParameter("@HideVenue", SQLBitFromBool(this.HideVenue));
			paramArray[1] = new SqlParameter("@LastUpdate", this.LastUpdate);
			paramArray[2] = new SqlParameter("@VenueID", this.VenueID);
			paramArray[3] = new SqlParameter("@OrgID", this.OrgID);

			if (ExecuteSPNoValue("sp_UpdateVenueUsage", paramArray))
			{
				fRet = true;
			}

			return fRet;
		}

		public bool Delete( UserAccount acct )
		{
			bool fRet = false;
			bool fDelete = true;
			string strClean = acct.UserName + " -- " + DateTime.Now.ToString();
			this.LastUpdate = Sanitize(strClean);

			// Check to make sure the venue is not public or has not been referenced by another org.
			// TODO: Need graceful way to fail and allow user to assign to someone else

//			if( this.PublicVenue )
//			{
//				
//			}

			if( fDelete )
			{
				SqlParameter[] paramArray = new SqlParameter[2];
				paramArray[0] = new SqlParameter("@VenueID", this.VenueID);
				paramArray[1] = new SqlParameter("@OrgID", this.OrgID);

				if (ExecuteSPNoValue("sp_DeleteVenue", paramArray))
				{
					fRet = true;
				}
			}

			return fRet;
		}
#endregion
	}

	public class OrgVenuePairingList : USCBaseList
	{
#region Column Constants
/*
	VenueID bigint NOT NULL,
	OrgID bigint NOT NULL,
	HideVenue bit NOT NULL,  -- Allows removal of owned public venues from orgs list
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
 */
		const int colVenueID = 0;
		const int colOrgID = 1;
		const int colOrgHideVenue = 2;
		const int colCreator = 3;
		const int colCreateDate = 4;
		const int colLastUpdate = 5;
#endregion

		public SortedDictionary<long, object> m_sdOrgVenuePairs = new SortedDictionary<long, object>();

		public OrgVenuePairingList()
		{
		}

#region Load

		public bool LoadVenueList( string cnxString, long orgID )
		{
			bool fRet = false;
			m_strConnectionString = cnxString;

			m_sdOrgVenuePairs.Clear();

			if (0 > orgID)
			{
				return false;
			}

			SqlDataReader reader = null;
			SqlConnection sqlConn = null;
			SqlParameter[] paramArray = new SqlParameter[1];
			paramArray[0] = new SqlParameter("@OrgID", orgID);

			if (ExecuteSPRows("sp_GetOrgVenueList", paramArray, out reader, out sqlConn) && null != reader)
			{
				try
				{
					while (reader.Read())
					{
						IDataRecord dr = (IDataRecord)reader;
						OrgVenuePairing ovp = new OrgVenuePairing();
						fRet = ReadOrgVenue(dr, ovp);
						if (fRet)
						{
							long lIndex = m_sdOrgVenuePairs.Count + 1;
							m_sdOrgVenuePairs.Add(lIndex, ovp);
						}
					}
					reader.Close();
					sqlConn.Close();
					fRet = true;
				}
				catch (Exception ex)
				{
					String strIntro = "Error loading org venues for " + orgID;
					EvtLog.WriteException(strIntro, ex, 1);
				}
			}

			return fRet;
		}

		private bool ReadOrgVenue(IDataRecord dr, OrgVenuePairing ovp)
		{
			bool fRet = true;
			try
			{
				ovp.ConnectionString = m_strConnectionString;
				ovp.VenueID = ObjectToLong(dr[colVenueID]);
				ovp.OrgID = ObjectToLong(dr[colOrgID]);
				ovp.HideVenue = ObjectToBool(dr[colOrgHideVenue]);
				ovp.Creator = ObjectToString(dr[colCreator]);
				ovp.CreateDate = ObjectToDateTime(dr[colCreateDate]);
				ovp.LastUpdate = ObjectToString(dr[colLastUpdate]);
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("ReadOrgVenue.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}
#endregion

#region AddVenueUsage
/*
	@VenueID bigint,
	@OrgID bigint, 
	@Creator nvarchar(50)
*/
		public bool AddVenueUsage(OrgVenuePairing ovp, UserAccount acct)
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[3];
			paramArray[0] = new SqlParameter("@VenueID", ovp.VenueID);
			paramArray[1] = new SqlParameter("@OrgID", ovp.OrgID);
			paramArray[2] = new SqlParameter("@Creator", Sanitize(USCBase.Truncate(acct.UserName, 49, true)));

			if (ExecuteSPNoValue("sp_AddVenueUsage", paramArray))
			{
				object tmp = null;
				if( !m_sdOrgVenuePairs.TryGetValue( ovp.VenueID, out tmp ) )
				{
					m_sdOrgVenuePairs.Add(ovp.VenueID, ovp);
				}

				fRet = true;
			}

			return fRet;
		}
#endregion

		public bool DoesPairingExist( long lOrgID, long lVenueID )
		{
			bool fRet = false;
			foreach (KeyValuePair<long, object> kvp in m_sdOrgVenuePairs)
			{
				OrgVenuePairing ovp = (OrgVenuePairing)kvp.Value;
				if( null != ovp && ovp.OrgID == lOrgID && ovp.VenueID == lVenueID )
				{
					fRet = true;
					break;
				}
			}

			return fRet;
		}

		public bool Update( OrgVenuePairing ovp, UserAccount acct )
		{
			return ovp.Update(acct);
		}

		public bool Delete( OrgVenuePairing ovp, UserAccount acct )
		{
			return ovp.Delete(acct);
		}

	}

}