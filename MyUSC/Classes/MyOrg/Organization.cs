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
	public enum OrgTypes { Undefined = -1, Organization = 1, Region = 2, State = 3, District = 4, League = 5, Division = 6, Team = 7, Other = 10 }
	public enum AffiliateTypes { Undefined = -1, Sponsor = 0, Charter = 1, Parent = 2, Peer = 3, Child = 4, None = 10 }
	public enum OrgAccessTypes { Undefined = -1, Owner = 1, Admin = 2, Contributor = 3, Member = 4, Follower = 5, Guest = 6, Banned = 10 }
	public enum OrgPageID { Undefined = -1, Home = 0, Roster = 1, Schedule = 2, MsgBoard = 3, Media = 4, Email = 5, Stats = 6, Venue = 7, Manage = 100 }

	public class Organization : Address
	{

#region Member Variables
		private bool m_fListsLoaded;
		private string m_strOrgName;
		private string m_strOrgDescription;
		private OrgTypes m_orgType;
		private long m_lOwnerAccountID;
		private string m_strLogoURL;
		private bool m_fShowContactInfo;	// Share contact info with other org members
		private bool m_fAllowMemberRequests;	// Allow people to request membership
		private bool m_fAllowFollowerRequests;	// Allow people to request to follow
		private bool m_fAllowGuestViews;	// Allow anyone to view
		private bool m_fDeleted;	// Mark as deleted/inactive

		public OrgPageOptionList orgPageOptionList;
		public EventList orgEventList;
		public OrgMemberList orgMemberList;
		public OrgOfficialsList orgOfficialsList;
		public OrgVenuePairingList orgVenueList;
		public SeasonList orgSeasonList;
		public EventResponseList orgEventResponseList;
		public AffiliatesList orgAffiliateList;
		public OrgInfo orgInfo;

#endregion

#region Init

		public void LoadLists()
		{
			if (0 > OrgID)
			{
				return;
			}

			if ( null == orgInfo )
			{
				orgInfo = new OrgInfo( OrgID, m_strConnectionString );
			}
			orgInfo.Load();

			if ( null == orgSeasonList )
			{
				orgSeasonList = new SeasonList();
			}
			orgSeasonList.Init(m_strConnectionString, OrgID, true);
			if( orgSeasonList.Count == 0 )
			{
				CreateInitialSeason();
			}

			if (null == orgPageOptionList)
			{
				orgPageOptionList = new OrgPageOptionList();
			}
			orgPageOptionList.Init( m_strConnectionString, OrgID, true );

			if (null == orgEventList)
			{
				orgEventList = new EventList();
			}
			orgEventList.Init(m_strConnectionString, OrgID, true);

			if (null == orgMemberList)
			{
				orgMemberList = new OrgMemberList();
			}
			orgMemberList.Init(m_strConnectionString, OrgID, true);

			if (null == orgOfficialsList)
			{
				orgOfficialsList = new OrgOfficialsList();
			}
			orgOfficialsList.Init(m_strConnectionString, OrgID, true);

			if (null == orgVenueList)
			{
				orgVenueList = new OrgVenuePairingList();
			}
			orgVenueList.LoadVenueList( m_strConnectionString, OrgID );

			if (null == orgEventResponseList)
			{
				orgEventResponseList = new EventResponseList();
			}
			orgEventResponseList.Init( m_strConnectionString, OrgID, true );

			if (null == orgAffiliateList)
			{
				orgAffiliateList = new AffiliatesList();
			}
			orgAffiliateList.Init( m_strConnectionString, OrgID, true );

			ListsLoaded = true;
		}
		
		protected void InitOrganization()
		{
			m_fListsLoaded = false;
			m_strOrgName = "";
			m_strOrgDescription = "";
			m_orgType = OrgTypes.Team;
			m_lOwnerAccountID = -1;
			m_nLanguage = 1;
			m_strLogoURL = "";
			m_fShowContactInfo = true;
			m_fAllowMemberRequests = true;
			m_fAllowFollowerRequests = true;
			m_fAllowGuestViews = true;
			m_fDeleted = false;
		}

		public Organization()
		{
			InitOrganization();
		}

		public void CreateInitialSeason()
		{
			Season initSeason = new Season();
			initSeason.OrgID = OrgID;
			initSeason.ConnectionString = ConnectionString;
			initSeason.SeasonName = "Initial Season";
			initSeason.IsDefault = true;
			initSeason.Share = true;

			orgSeasonList.Add(initSeason);
		}

#endregion

#region Accessors
		public bool ListsLoaded
		{
			get
			{
				return this.m_fListsLoaded;
			}
			set
			{
				this.m_fListsLoaded = value;
			}
		}

		public long OrgID
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

		public string OrgName
		{
			get
			{
				return this.m_strOrgName;
			}
			set
			{
				this.m_strOrgName = value;
			}
		}

		public string OrgDescription
		{
			get
			{
				return this.m_strOrgDescription;
			}
			set
			{
				this.m_strOrgDescription = value;
			}
		}

		public OrgTypes OrgType
		{
			get
			{
				return this.m_orgType;
			}
			set
			{
				this.m_orgType = value;
			}
		}

		public long OwnerAccountID
		{
			get
			{
				return this.m_lOwnerAccountID;
			}
			set
			{
				this.m_lOwnerAccountID = value;
			}
		}

		public string LogoURL
		{
			get
			{
				return this.m_strLogoURL;
			}
			set
			{
				this.m_strLogoURL = value;
			}
		}

		public bool ShowContactInfo
		{
			get
			{
				return this.m_fShowContactInfo;
			}
			set
			{
				this.m_fShowContactInfo = value;
			}
		}

		public bool AllowMemberRequests
		{
			get
			{
				return this.m_fAllowMemberRequests;
			}
			set
			{
				this.m_fAllowMemberRequests = value;
			}
		}

		public bool AllowFollowerRequests
		{
			get
			{
				return this.m_fAllowFollowerRequests;
			}
			set
			{
				this.m_fAllowFollowerRequests = value;
			}
		}

		public bool AllowGuestViews
		{
			get
			{
				return this.m_fAllowGuestViews;
			}
			set
			{
				this.m_fAllowGuestViews = value;
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

#endregion

/*
CREATE PROCEDURE sp_UpdateOrg
	@OrgName nvarchar(250),
	@OrgDescription nvarchar(500),
	@OrgType int, 
	@OwnerID bigint, 
	@Language int, 
	@Address1 nvarchar(50),
	@Address2 nvarchar(50),
	@City nvarchar(50),
	@State nvarchar(50),
	@PostalCode nvarchar(50),
	@Country nvarchar(50),
	@EmailAddress nvarchar(50),
	@Phone nvarchar(50),
	@Cell nvarchar(50),
	@Fax nvarchar(50),
	@URL nvarchar(250),
	@LogoURL nvarchar(250),
	@ShowContact bigint, 
	@AllowMemberRequests bit, 
	@AllowFollowerRequests bit, 
	@AllowGuestViews bit, 
	@Deleted bit,
	@Update nvarchar(max),
	@Key bigint
 */
#region Update
		public bool Update(UserAccount acct)
		{
			bool fRet = false;
			string strClean = acct.UserName + " -- " + DateTime.Now.ToString();
			this.LastUpdate = Sanitize(strClean);

			SqlParameter[] paramArray = new SqlParameter[24];
			paramArray[0] = new SqlParameter("@OrgName", Sanitize(USCBase.Truncate(this.OrgName, 249, true)));
			paramArray[1] = new SqlParameter("@OrgDescription", Sanitize(USCBase.Truncate(this.OrgDescription, 499, true)));
			paramArray[2] = new SqlParameter("@OrgType", this.OrgType);
			paramArray[3] = new SqlParameter("@OwnerID", this.OwnerAccountID);
			paramArray[4] = new SqlParameter("@Language", this.Language);
			paramArray[5] = new SqlParameter("@Address1", Sanitize(USCBase.Truncate(this.Address1, 49, true)));
			paramArray[6] = new SqlParameter("@Address2", Sanitize(USCBase.Truncate(this.Address2, 49, true)));
			paramArray[7] = new SqlParameter("@City", Sanitize(USCBase.Truncate(this.City, 49, true)));
			paramArray[8] = new SqlParameter("@State", Sanitize(USCBase.Truncate(this.State, 49, true)));
			paramArray[9] = new SqlParameter("@PostalCode", Sanitize(USCBase.Truncate(this.Zip, 49, true)));
			paramArray[10] = new SqlParameter("@Country", Sanitize(USCBase.Truncate(this.Country, 49, true)));
			paramArray[11] = new SqlParameter("@EmailAddress", Sanitize(USCBase.Truncate(this.Email, 49, true)));
			paramArray[12] = new SqlParameter("@Phone", Sanitize(USCBase.Truncate(this.Phone, 49, true)));
			paramArray[13] = new SqlParameter("@Cell", Sanitize(USCBase.Truncate(this.Cell, 49, true)));
			paramArray[14] = new SqlParameter("@Fax", Sanitize(USCBase.Truncate(this.Fax, 49, true)));
			paramArray[15] = new SqlParameter("@URL", Sanitize(USCBase.Truncate(this.URL, 249, true)));
			paramArray[16] = new SqlParameter("@LogoURL", Sanitize(USCBase.Truncate(this.LogoURL, 249, true)));
			paramArray[17] = new SqlParameter("@ShowContact", SQLBitFromBool(this.ShowContactInfo));
			paramArray[18] = new SqlParameter("@AllowMemberRequests", SQLBitFromBool(this.AllowMemberRequests));
			paramArray[19] = new SqlParameter("@AllowFollowerRequests", SQLBitFromBool(this.AllowFollowerRequests));
			paramArray[20] = new SqlParameter("@AllowGuestViews", SQLBitFromBool(this.AllowGuestViews));
			paramArray[21] = new SqlParameter("@Deleted", SQLBitFromBool(this.Deleted));
			paramArray[22] = new SqlParameter("@Update", this.LastUpdate);
			paramArray[23] = new SqlParameter("@Key", Key);

			if (ExecuteSPNoValue("sp_UpdateOrg", paramArray))
			{
				fRet = true;
			}

			return fRet;
		}
#endregion

#region PageOptions
		public OrgPageOptions GetOrgPageOption(OrgPageID index)
		{
			OrgPageOptions opo = null;
			if( orgPageOptionList.htPageOptions.ContainsKey( index ) )
			{
				opo = (OrgPageOptions)orgPageOptionList.htPageOptions[index];
			}
			return opo;
		}

#endregion

#region OrgMembers
		public OrgMember GetOrganizationMember(long acctID)
		{
			return orgMemberList.GetOrgMember(acctID);
		}

		public OrgMember GetOrganizationMember(string strIndex)
		{
			long lMemberID = 0;
			OrgMember member = null;
			if (long.TryParse(strIndex, out lMemberID))
			{
				member = GetOrganizationMember(lMemberID);
			}
			return member;
		}

#endregion

#region Org Folders
		public bool CreateOrgFolders(string strRootPath)
		{
			bool fRet = true;

			string strRootOrgPath = GetOrgRootPath(strRootPath);
			string strRootOrgPathLogo = GetOrgLogoPath(strRootPath);
			string strRootOrgPathMedia = GetOrgMediaPath(strRootPath);

			try
			{
				//here we create the folder in the Users Folder
				System.IO.Directory.CreateDirectory(strRootOrgPath);
				System.IO.Directory.CreateDirectory(strRootOrgPathLogo);
				System.IO.Directory.CreateDirectory(strRootOrgPathMedia);
			}
			catch( Exception ex )
			{
				fRet = false;
				string strEx = "Organization.CreateOrgFolders for " + orgEventList.OrgID;
				EvtLog.WriteException( strEx, ex, 1 );
			}

			return fRet;
		}

		public string GetOrgRootPath( string strRootPath )
		{
			string strRet = "";
			string strOrgBaseFolder = GetSiteSetting(SiteAdmin.saKeyOrgFolders, "OrgBaseFolder", this.Language);
			string strRootUserPath = strRootPath + strOrgBaseFolder;

			strRet = strRootUserPath + "\\" + this.OrgID;

			return strRet;
		}

		public string GetOrgLogoPath( string strRootPath )
		{
			string strRet = "";
			string strLogoFolder = GetSiteSetting(SiteAdmin.saKeyOrgLogo, "OrgLogoFolder", this.Language);

			strRet = GetOrgRootPath(strRootPath) + "\\" + strLogoFolder;

			return strRet;
		}

		public string GetOrgMediaPath( string strRootPath )
		{
			string strRet = "";
			string strUsersMediaFolder = GetSiteSetting(SiteAdmin.saKeyOrgMedia, "OrgMediaFolder", this.Language);

			strRet = GetOrgRootPath(strRootPath) + "\\" + strUsersMediaFolder;

			return strRet;
		}
#endregion
	}

	public sealed class OrganizationList : USCBaseList
	{
#region Column Constants
/*
	OrgID bigint NOT NULL IDENTITY,
	OrgName nvarchar(250) NOT NULL,
	OrgDescription nvarchar(500) NOT NULL,
	OrgType int NOT NULL,
	OwnerID bigint NOT NULL,
	[Language] int NOT NULL DEFAULT(1),
	Address1 nvarchar(50),
	Address2 nvarchar(50),
	City nvarchar(50),
	[State] nvarchar(50),
	PostalCode  nvarchar(50),
	Country  nvarchar(50),
	EmailAddress  nvarchar(50),
	Phone  nvarchar(50),
	Cell nvarchar(50),
	Fax nvarchar(50),
	URL nvarchar(250),
	LogoURL nvarchar(250),
	ShowContact bit NOT NULL,
	AllowMemberRequests bit NOT NULL,
	AllowFollowerRequests bit NOT NULL,
	AllowGuestViews bit NOT NULL,
	Deleted bit NOT NULL DEFAULT(0),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
 */
		const int colKey = 0;
		const int colOrgName = 1;
		const int colOrgDescription = 2;
		const int colOrgType = 3;
		const int colOwnerID = 4;
		const int colLanguage = 5;
		const int colAddress1 = 6;
		const int colAddress2 = 7;
		const int colCity = 8;
		const int colState = 9;
		const int colZip = 10;
		const int colCountry = 11;
		const int colEmailAddress = 12;
		const int colPhone = 13;
		const int colCell = 14;
		const int colFax = 15;
		const int colURL = 16;
		const int colLogoURL = 17;
		const int colShowContact = 18;
		const int colAllowMemberRequests = 19;
		const int colAllowFollowerRequests = 20;
		const int colAllowGuestViews = 21;
		const int colDeleted = 22;
		const int colCreator = 23;
		const int colCreateDate = 24;
		const int colLastUpdate = 25;
#endregion

		private static volatile OrganizationList instance = null;
		private static object syncRoot = new object();

		const string strSQLGetAllOrgs = "SELECT * FROM MyOrg";
#region Init
		private OrganizationList()
		{
		}

		public static OrganizationList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new OrganizationList();
					}
				}

				return instance;
			}
		}

		public Hashtable htOrgList;

		public void Init(string cnxString, bool fForce)
		{
			m_strConnectionString = cnxString;
			if (null == htOrgList || fForce)
			{
				htOrgList = new Hashtable();
				Load();
			}
		}

		public int Count
		{
			get
			{
				return htOrgList.Count;
			}
		}

		public Organization GetOrganization(long index)
		{
			return (Organization)htOrgList[index];
		}

		public Organization GetOrganization(string strIndex)
		{
			long lOrgID = 0;
			Organization org = null;
			if (long.TryParse(strIndex, out lOrgID))
			{
				org = GetOrganization( lOrgID );
			}
			return org;
		}

#endregion

#region Load
		private bool LoadOrganizations()
		{
			bool fRet = false;

			htOrgList.Clear();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllOrgs, sqlConn);
				daLocStrings.Fill(locStrDS, "MyOrg");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("MyOrg.LoadOrganizations failure", ex, 0);
				ExceptionText = "LoadOrganizations:" + ex.Message;
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["MyOrg"].Rows;
			foreach (DataRow dr in dra)
			{
				Organization org = new Organization();
				fRet = ReadOrg(dr, org);
				if (fRet)
				{
					// Set now so objects can update themselves
					org.ConnectionString = m_strConnectionString;
					org.LoadLists();

					htOrgList.Add(org.Key, org);
				}
			}
			fRet = true;

			return fRet;
		}

		public bool Load()
		{
			bool fRet = true;

			LoadOrganizations();

			return fRet;
		}
#endregion

#region ReadValues
		private bool ReadOrg(DataRow dr, Organization org)
		{
			bool fRet = true;
			try
			{
				org.Key = ObjectToLong(dr.ItemArray[colKey]);
				org.OrgName = ObjectToString(dr.ItemArray[colOrgName]);
				org.OrgDescription = ObjectToString(dr.ItemArray[colOrgDescription]);
				org.OrgType = (OrgTypes)ObjectToInt(dr.ItemArray[colOrgType]);
				org.OwnerAccountID = ObjectToLong(dr.ItemArray[colOwnerID]);
				org.Language = ObjectToInt(dr.ItemArray[colLanguage]);
				org.Address1 = ObjectToString(dr.ItemArray[colAddress1]);
				org.Address2 = ObjectToString(dr.ItemArray[colAddress2]);
				org.City = ObjectToString(dr.ItemArray[colCity]);
				org.State = ObjectToString(dr.ItemArray[colState]);
				org.Zip = ObjectToString(dr.ItemArray[colZip]);
				org.Country = ObjectToString(dr.ItemArray[colCountry]);
				org.Email = ObjectToString(dr.ItemArray[colEmailAddress]);
				org.Phone = ObjectToString(dr.ItemArray[colPhone]);
				org.Cell = ObjectToString(dr.ItemArray[colCell]);
				org.Fax = ObjectToString(dr.ItemArray[colFax]);
				org.URL = ObjectToString(dr.ItemArray[colURL]);
				org.LogoURL = ObjectToString(dr.ItemArray[colLogoURL]);
				org.ShowContactInfo = ObjectToBool(dr.ItemArray[colShowContact]);
				org.AllowMemberRequests = ObjectToBool(dr.ItemArray[colAllowMemberRequests]);
				org.AllowFollowerRequests = ObjectToBool(dr.ItemArray[colAllowFollowerRequests]);
				org.AllowGuestViews = ObjectToBool(dr.ItemArray[colAllowGuestViews]);
				org.Deleted = ObjectToBool(dr.ItemArray[colDeleted]);
				org.Creator = ObjectToString(dr.ItemArray[colCreator]);
				org.CreateDate = ObjectToDateTime(dr.ItemArray[colCreateDate]);
				org.LastUpdate = ObjectToString(dr.ItemArray[colLastUpdate]);
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("OrganizationList.ReadOrg failure", ex, 0);
				ExceptionText = "ReadOrg:" + ex.Message;
				fRet = false;
			}
			return fRet;
		}
#endregion

#region Add Org
/*
CREATE PROCEDURE sp_AddOrg
	@OrgName nvarchar(250),
	@OrgDescription nvarchar(500),
	@OrgType int, 
	@OwnerID bigint, 
	@Language int, 
	@Address1 nvarchar(50),
	@Address2 nvarchar(50),
	@City nvarchar(50),
	@State nvarchar(50),
	@PostalCode nvarchar(50),
	@Country nvarchar(50),
	@EmailAddress nvarchar(50),
	@Phone nvarchar(50),
	@Cell nvarchar(50),
	@Fax nvarchar(50),
	@URL nvarchar(250),
	@LogoURL nvarchar(250),
	@ShowContact bit, 
	@AllowMemberRequests bit, 
	@AllowFollowerRequests bit, 
	@AllowGuestViews bit, 
	@Creator nvarchar(50)
*/
		public bool AddOrg(Organization org, UserAccount acct)
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[22];
			paramArray[0] = new SqlParameter("@OrgName", Sanitize(USCBase.Truncate(org.OrgName, 249, true)));
			paramArray[1] = new SqlParameter("@OrgDescription", Sanitize(USCBase.Truncate(org.OrgDescription, 499, true)));
			paramArray[2] = new SqlParameter("@OrgType", org.OrgType);
			paramArray[3] = new SqlParameter("@OwnerID", org.OwnerAccountID);
			paramArray[4] = new SqlParameter("@Language", org.Language);
			paramArray[5] = new SqlParameter("@Address1", Sanitize(USCBase.Truncate(org.Address1, 49, true)));
			paramArray[6] = new SqlParameter("@Address2", Sanitize(USCBase.Truncate(org.Address2, 49, true)));
			paramArray[7] = new SqlParameter("@City", Sanitize(USCBase.Truncate(org.City, 49, true)));
			paramArray[8] = new SqlParameter("@State", Sanitize(USCBase.Truncate(org.State, 49, true)));
			paramArray[9] = new SqlParameter("@PostalCode", Sanitize(USCBase.Truncate(org.Zip, 49, true)));
			paramArray[10] = new SqlParameter("@Country", Sanitize(USCBase.Truncate(org.Country, 49, true)));
			paramArray[11] = new SqlParameter("@EmailAddress", Sanitize(USCBase.Truncate(org.Email, 49, true)));
			paramArray[12] = new SqlParameter("@Phone", Sanitize(USCBase.Truncate(org.Phone, 49, true)));
			paramArray[13] = new SqlParameter("@Cell", Sanitize(USCBase.Truncate(org.Cell, 49, true)));
			paramArray[14] = new SqlParameter("@Fax", Sanitize(USCBase.Truncate(org.Fax, 49, true)));
			paramArray[15] = new SqlParameter("@URL", Sanitize(USCBase.Truncate(org.URL, 249, true)));
			paramArray[16] = new SqlParameter("@LogoURL", Sanitize(USCBase.Truncate(org.LogoURL, 249, true)));
			paramArray[17] = new SqlParameter("@ShowContact", SQLBitFromBool(org.ShowContactInfo));
			paramArray[18] = new SqlParameter("@AllowMemberRequests", SQLBitFromBool(org.AllowMemberRequests));
			paramArray[19] = new SqlParameter("@AllowFollowerRequests", SQLBitFromBool(org.AllowFollowerRequests));
			paramArray[20] = new SqlParameter("@AllowGuestViews", SQLBitFromBool(org.AllowGuestViews));
			paramArray[21] = new SqlParameter("@Creator", Sanitize(USCBase.Truncate(org.Creator, 49, true)));

			org.Key = ExecuteSPInsert("sp_AddOrg", paramArray);
			if (org.Key > 0)
			{
				htOrgList.Add(org.Key, org);

				// Initialize the lists
				org.LoadLists();

				// Create initial season
				org.CreateInitialSeason();

				// Add the owner
				OrgMember om = new OrgMember();
				om.OrgID = org.OrgID;
				om.UserID = org.OwnerAccountID;
				om.MemberType = OrgAccessTypes.Owner;
				om.Creator = org.Creator;
				org.orgMemberList.Add( om );

				// Add to the owners account
				acct.m_sdMyOrgs.Add(om.Key, om);

				fRet = true;
			}

			return fRet;
		}

#endregion

#region Update Org
		public bool Update(Organization org, UserAccount acct)
		{
			return org.Update(acct);
		}

#endregion

#region DeleteOrg
		public bool Delete(Organization org, UserAccount acct)
		{
			// We don't really want to delete it, just make inactive.
			org.Deleted = false;
			return org.Update(acct);
		}
#endregion

	} //class OrganizationList
}