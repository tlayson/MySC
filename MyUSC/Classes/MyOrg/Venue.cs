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
	public class Venue : Address
	{
#region Member Variables
		private string m_strVenueName;
		private long m_lOwnerID;
		private long m_lOrgID;
		private string m_strVenueType;
		private string m_strDisplayLocation;
		private string m_strNote;
		private string m_strMapURL;
		private string m_strImageURL;
		private bool m_fDeleted;
		private bool m_fPublicVenue;
#endregion

#region Init
		protected void InitVenue()
		{
			m_strVenueName = "";
			m_lOwnerID = -1;
			m_lOrgID = -1;
			m_strVenueType = "";
			m_strDisplayLocation = "";
			m_strNote = "";
			m_strMapURL = "";
			m_strImageURL = "";
			m_fDeleted = false;
			m_fPublicVenue = false;
		}

		public Venue()
		{
			InitVenue();
		}
#endregion

#region Accessors
		public long VenueID
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

		public string VenueName
		{
			get
			{
				return this.m_strVenueName;
			}
			set
			{
				this.m_strVenueName = value;
			}
		}

		public string VenueType
		{
			get
			{
				return this.m_strVenueType;
			}
			set
			{
				this.m_strVenueType = value;
			}
		}

		public long OwnerID
		{
			get
			{
				return this.m_lOwnerID;
			}
			set
			{
				this.m_lOwnerID = value;
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

		public string DisplayLocation
		{
			get
			{
				return this.m_strDisplayLocation;
			}
			set
			{
				this.m_strDisplayLocation = value;
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

		public string MapURL
		{
			get
			{
				return this.m_strMapURL;
			}
			set
			{
				this.m_strMapURL = value;
			}
		}

		public string ImageURL
		{
			get
			{
				return this.m_strImageURL;
			}
			set
			{
				this.m_strImageURL = value;
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

		public bool PublicVenue
		{
			get
			{
				return this.m_fPublicVenue;
			}
			set
			{
				this.m_fPublicVenue = value;
			}
		}

#endregion

#region Update

/*
	@VenueName nvarchar(250),
	@OwnerID bigint, 
	@OrgID bigint, 
	@DisplayLocation nvarchar(50),
	@Address1 nvarchar(50),
	@Address2 nvarchar(50),
	@City nvarchar(50),
	@State nvarchar(50),
	@PostalCode nvarchar(50),
	@Country nvarchar(50),
	@Phone nvarchar(50),
	@Website nvarchar(250),
	@MapURL nvarchar(250),
	@ImageURL nvarchar(250),
	@Note nvarchar(1000),
	@MakePublic bit,
	@Deleted bit,
	@LastUpdate nvarchar(max),
	@VenueID bigint
*/
		public bool Update( UserAccount acct )
		{
			bool fRet = false;
			string strClean = acct.UserName + " -- " + DateTime.Now.ToString();
			this.LastUpdate = Sanitize(strClean);


			SqlParameter[] paramArray = new SqlParameter[20];
			paramArray[0] = new SqlParameter("@VenueName", Sanitize(USCBase.Truncate(this.VenueName, 249, true)));
			paramArray[1] = new SqlParameter("@OwnerID", OwnerID);
			paramArray[2] = new SqlParameter("@OrgID", this.OrgID);
			paramArray[3] = new SqlParameter("@VenueType", Sanitize(USCBase.Truncate(this.VenueType, 24, true)));
			paramArray[4] = new SqlParameter("@DisplayLocation", Sanitize(USCBase.Truncate(this.DisplayLocation, 49, true)));
			paramArray[5] = new SqlParameter("@Address1", Sanitize(USCBase.Truncate(this.Address1, 49, true)));
			paramArray[6] = new SqlParameter("@Address2", Sanitize(USCBase.Truncate(this.Address2, 49, true)));
			paramArray[7] = new SqlParameter("@City", Sanitize(USCBase.Truncate(this.City, 49, true)));
			paramArray[8] = new SqlParameter("@State", Sanitize(USCBase.Truncate(this.State, 49, true)));
			paramArray[9] = new SqlParameter("@PostalCode", Sanitize(USCBase.Truncate(this.Zip, 49, true)));
			paramArray[10] = new SqlParameter("@Country", Sanitize(USCBase.Truncate(this.Country, 49, true)));
			paramArray[11] = new SqlParameter("@Phone", Sanitize(USCBase.Truncate(this.Phone, 49, true)));
			paramArray[12] = new SqlParameter("@Website", Sanitize(USCBase.Truncate(this.URL, 249, true)));
			paramArray[13] = new SqlParameter("@MapURL", Sanitize(USCBase.Truncate(this.MapURL, 249, true)));
			paramArray[14] = new SqlParameter("@ImageURL", Sanitize(USCBase.Truncate(this.ImageURL, 249, true)));
			paramArray[15] = new SqlParameter("@Note", Sanitize(USCBase.Truncate(this.Note, 999, true)));
			paramArray[16] = new SqlParameter("@MakePublic", SQLBitFromBool(this.PublicVenue));
			paramArray[17] = new SqlParameter("@Deleted", SQLBitFromBool(this.Deleted));
			paramArray[18] = new SqlParameter("@LastUpdate", this.LastUpdate);
			paramArray[19] = new SqlParameter("@VenueID", Key);

			if (ExecuteSPNoValue("sp_UpdateVenue", paramArray))
			{
				fRet = true;
			}

			return fRet;
		}

		public bool UpdateOwner( UserAccount acct )
		{
			bool fRet = false;
			string strClean = acct.UserName + " -- " + DateTime.Now.ToString();
			this.LastUpdate = Sanitize(strClean);


			SqlParameter[] paramArray = new SqlParameter[5];
			paramArray[0] = new SqlParameter("@OwnerID", OwnerID);
			paramArray[1] = new SqlParameter("@OrgID", this.OrgID);
			paramArray[2] = new SqlParameter("@Deleted", SQLBitFromBool(this.Deleted));
			paramArray[3] = new SqlParameter("@LastUpdate", this.LastUpdate);
			paramArray[4] = new SqlParameter("@VenueID", Key);

			if (ExecuteSPNoValue("sp_UpdateVenueOwner", paramArray))
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

			if( this.PublicVenue )
			{
				
			}

			if( fDelete )
			{
				SqlParameter[] paramArray = new SqlParameter[3];
				paramArray[0] = new SqlParameter("@Deleted", SQLBitFromBool(this.Deleted));
				paramArray[1] = new SqlParameter("@LastUpdate", this.LastUpdate);
				paramArray[2] = new SqlParameter("@VenueID", Key);

				if (ExecuteSPNoValue("sp_DeleteVenue", paramArray))
				{
					fRet = true;
				}
			}

			return fRet;
		}
#endregion

	}

	public sealed class VenueList : USCBaseList
	{
#region Column Constants
/*
	VenueID bigint NOT NULL IDENTITY,
	VenueName nvarchar(250) NOT NULL,
	OwnerID bigint NOT NULL,
	OrgID bigint NOT NULL,
	DisplayLocation nvarchar(50),
	Address1 nvarchar(50),
	Address2 nvarchar(50),
	City nvarchar(50),
	[State] nvarchar(50),
	PostalCode  nvarchar(50),
	Country  nvarchar(50),
	Phone  nvarchar(50),
	Website nvarchar(250),
	MapURL nvarchar(250),
	ImageURL nvarchar(250),
	Note nvarchar(1000),
	MakePublic bit NOT NULL,
	Deleted bit NOT NULL DEFAULT(0),
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
 */
		const int colKey = 0;
		const int colVenueName = 1;
		const int colOwnerID = 2;
		const int colOrgID = 3;
		const int colVenueType = 4;
		const int colDisplayLocation = 5;
		const int colAddress1 = 6;
		const int colAddress2 = 7;
		const int colCity = 8;
		const int colState = 9;
		const int colPostalCode = 10;
		const int colCountry = 11;
		const int colPhone = 12;
		const int colWebsite = 13;
		const int colMapURL = 14;
		const int colImageURL = 15;
		const int colNote = 16;
		const int colPublicVenue = 17;
		const int colDeleted = 18;
		const int colCreator = 19;
		const int colCreateDate = 20;
		const int colLastUpdate = 21;
#endregion

		private static volatile VenueList instance = null;
		private static object syncRoot = new object();

		const string strSQLGetAllOrgVenues = "SELECT * FROM Venue";

		private VenueList()
		{
		}

		public static VenueList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new VenueList();
					}
				}

				return instance;
			}
		}

		//public Hashtable htVenues;
		public SortedDictionary<long, object> m_sdVenues = new SortedDictionary<long, object>();

		public void Init(string cnxString, bool fForce)
		{
			m_strConnectionString = cnxString;
			m_sdVenues.Clear();
			Load();
		}

#region Load
		private bool Load()
		{
			bool fRet = false;

			m_sdVenues.Clear();

			SqlDataReader reader = null;
			SqlConnection sqlConn = null;
			SqlParameter[] paramArray = null;

			if (ExecuteSPRows("sp_GetAllVenues", paramArray, out reader, out sqlConn) && null != reader)
			{
				try
				{
					while (reader.Read())
					{
						IDataRecord dr = (IDataRecord)reader;
						Venue venue = new Venue();
						fRet = ReadVenue(dr, venue);
						if (fRet)
						{
							long lIndex = m_sdVenues.Count + 1;
							m_sdVenues.Add( lIndex, venue );
						}
					}
					reader.Close();
					sqlConn.Close();
					fRet = true;
				}
				catch (Exception ex)
				{
					String strIntro = "Error loading Venue list";
					EvtLog.WriteException( strIntro, ex, 1 );
				}
			}

			return fRet;
		}

/*
		const int colKey = 0;
		const int colVenueName = 1;
		const int colOwnerID = 2;
		const int colOrgID = 3;
		const int colVenueType = 4;
		const int colDisplayLocation = 5;
		const int colAddress1 = 6;
		const int colAddress2 = 7;
		const int colCity = 8;
		const int colState = 9;
		const int colPostalCode = 10;
		const int colCountry = 11;
		const int colPhone = 12;
		const int colWebsite = 13;
		const int colMapURL = 14;
		const int colImageURL = 15;
		const int colNote = 16;
		const int colPublicVenue = 17;
		const int colDeleted = 18;
		const int colCreator = 19;
		const int colCreateDate = 20;
		const int colLastUpdate = 21;
 */
		private bool ReadVenue(IDataRecord dr, Venue venue)
		{
			bool fRet = true;
			try
			{
				venue.ConnectionString = m_strConnectionString;
				venue.Key = ObjectToLong(dr[colKey]);
				venue.VenueID = venue.Key;
				venue.VenueName = ObjectToString(dr[colVenueName]);
				venue.OwnerID = ObjectToLong(dr[colOwnerID]);
				venue.OrgID = ObjectToLong(dr[colOrgID]);
				venue.VenueType = ObjectToString(dr[colVenueType]);
				venue.DisplayLocation = ObjectToString(dr[colDisplayLocation]);
				venue.Address1 = ObjectToString(dr[colAddress1]);
				venue.Address2 = ObjectToString(dr[colAddress2]);
				venue.City = ObjectToString(dr[colCity]);
				venue.State = ObjectToString(dr[colState]);
				venue.Zip = ObjectToString(dr[colPostalCode]);
				venue.Country = ObjectToString(dr[colCountry]);
				venue.Phone = ObjectToString(dr[colPhone]);
				venue.URL = ObjectToString(dr[colWebsite]);
				venue.MapURL = ObjectToString(dr[colMapURL]);
				venue.ImageURL = ObjectToString(dr[colImageURL]);
				venue.Note = ObjectToString(dr[colNote]);
				venue.PublicVenue = ObjectToBool(dr[colPublicVenue]);
				venue.Deleted = ObjectToBool(dr[colDeleted]);
				venue.Creator = ObjectToString(dr[colCreator]);
				venue.CreateDate = ObjectToDateTime(dr[colCreateDate]);
				venue.LastUpdate = ObjectToString(dr[colLastUpdate]);
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Venue.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}
#endregion Load

		public int Count
		{
			get
			{
				return m_sdVenues.Count;
			}
		}

		public Venue GetVenue(long index)
		{
			Venue venue = null;
			if (index > 0)
			{
				foreach (KeyValuePair<long, object> kvp in m_sdVenues)
				{
					Venue tmp = (Venue)kvp.Value;
					if (tmp.VenueID == index)
					{
						venue = tmp;
						break;
					}
				}
			}
			return venue;
		}

		public Venue GetVenueByID(long lID)
		{
			Venue venue = null;

			foreach( KeyValuePair<long, object> kvp in m_sdVenues )
			{
				Venue v = (Venue)kvp.Value;
				if( v.Key == lID )
				{
					venue = v;
					break;
				}
			}
			return venue;
		}

/*
CREATE PROCEDURE sp_AddVenue
	@VenueName nvarchar(250),
	@OwnerID bigint, 
	@OrgID bigint, 
	@DisplayLocation nvarchar(50),
	@Address1 nvarchar(50),
	@Address2 nvarchar(50),
	@City nvarchar(50),
	@State nvarchar(50),
	@PostalCode nvarchar(50),
	@Country nvarchar(50),
	@Phone nvarchar(50),
	@Website nvarchar(250),
	@MapURL nvarchar(250),
	@ImageURL nvarchar(250),
	@Note nvarchar(1000),
	@MakePublic bit,
	@Creator nvarchar(50)
*/
		public bool Add(Venue venue)
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[18];
			paramArray[0] = new SqlParameter("@VenueName", Sanitize(USCBase.Truncate(venue.VenueName, 249, true)));
			paramArray[1] = new SqlParameter("@OwnerID", venue.OwnerID);
			paramArray[2] = new SqlParameter("@OrgID", venue.OrgID);
			paramArray[3] = new SqlParameter("@VenueType", Sanitize(USCBase.Truncate(venue.VenueType, 24, true)));
			paramArray[4] = new SqlParameter("@DisplayLocation", Sanitize(USCBase.Truncate(venue.DisplayLocation, 49, true)));
			paramArray[5] = new SqlParameter("@Address1", Sanitize(USCBase.Truncate(venue.Address1, 49, true)));
			paramArray[6] = new SqlParameter("@Address2", Sanitize(USCBase.Truncate(venue.Address2, 49, true)));
			paramArray[7] = new SqlParameter("@City", Sanitize(USCBase.Truncate(venue.City, 49, true)));
			paramArray[8] = new SqlParameter("@State", Sanitize(USCBase.Truncate(venue.State, 49, true)));
			paramArray[9] = new SqlParameter("@PostalCode", Sanitize(USCBase.Truncate(venue.Zip, 49, true)));
			paramArray[10] = new SqlParameter("@Country", Sanitize(USCBase.Truncate(venue.Country, 49, true)));
			paramArray[11] = new SqlParameter("@Phone", Sanitize(USCBase.Truncate(venue.Phone, 49, true)));
			paramArray[12] = new SqlParameter("@Website", Sanitize(USCBase.Truncate(venue.URL, 249, true)));
			paramArray[13] = new SqlParameter("@MapURL", Sanitize(USCBase.Truncate(venue.MapURL, 249, true)));
			paramArray[14] = new SqlParameter("@ImageURL", Sanitize(USCBase.Truncate(venue.ImageURL, 249, true)));
			paramArray[15] = new SqlParameter("@Note", Sanitize(USCBase.Truncate(venue.Note, 999, true)));
			paramArray[16] = new SqlParameter("@MakePublic", SQLBitFromBool(venue.PublicVenue));
			paramArray[17] = new SqlParameter("@Creator", Sanitize(USCBase.Truncate(venue.Creator, 49, true)));

			venue.Key = ExecuteSPInsert("sp_AddVenue", paramArray);
			if (venue.Key > 0)
			{
				long lIndex = m_sdVenues.Count + 1;
				m_sdVenues.Add( lIndex, venue );
				fRet = true;
			}

			return fRet;
		}

		public bool Update(Venue venue, UserAccount acct)
		{
			return venue.Update(acct);
		}

		public bool Delete(Venue venue, UserAccount acct)
		{
			// We don't really want to delete it, just make inactive.
			venue.Deleted = false;
			return venue.Update(acct);
		}

	}

}