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
using System.Web;
using System.Web.Security;
using YAF.Providers.Membership;
using YAF.Providers.Utils;
using YAF.Types.Extensions;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.Classes
{

	public class UserPreferences : USCBaseItem
	{
#region Member Variables
		private long m_nAccountID;
		private bool m_bOffersFromUs;
		private bool m_bOffersFromPartners;
		private bool m_bDeleteFriendsWarning;
		private bool m_bDeleteMsgWarning;
		private string m_strNewsSubjects;
		private string m_strInterests;
		private bool m_bArchive;
		private bool m_bPublicSportsInterest;
		private bool m_bSendCommentsEmail;
		private bool m_bKeepLoggedIn;
		private bool m_bShowNickname;
		private bool m_bProfileUpdated;
		private bool m_bProvideSecurityQuestion;
		public Hashtable htNewsMenuItems;
#endregion

#region INIT
		public UserPreferences()
		{
			Init();
		}

		private void Init()
		{
			m_nAccountID = -1;
			m_bOffersFromUs = false;
			m_bOffersFromPartners = false;
			m_bDeleteFriendsWarning = false;
			m_bDeleteMsgWarning = false;
			m_strNewsSubjects = "";
			m_strInterests = "";
			m_bArchive = false;
			m_bPublicSportsInterest = false;
			m_bSendCommentsEmail = false;
			m_bKeepLoggedIn = false;
			m_bShowNickname = false;
			m_bProfileUpdated = false;
			m_bProvideSecurityQuestion = true;
			htNewsMenuItems = new Hashtable();
		}
#endregion

#region Accessors
		public bool ProvideSecurityQuestion
		{
			get
			{
				return this.m_bProvideSecurityQuestion;
			}
			set
			{
				this.m_bProvideSecurityQuestion = value;
			}
		}

		public bool ShowNickname
		{
			get
			{
				return this.m_bShowNickname;
			}
			set
			{
				this.m_bShowNickname = value;
			}
		}

		public bool ProfileUpdated
		{
			get
			{
				return this.m_bProfileUpdated;
			}
			set
			{
				this.m_bProfileUpdated = value;
			}
		}

		public long AccountID
		{
			get
			{
				return this.m_nAccountID;
			}
			set
			{
				this.m_nAccountID = value;
			}
		}

		public bool OffersFromUs
		{
			get
			{
				return this.m_bOffersFromUs;
			}
			set
			{
				this.m_bOffersFromUs = value;
			}
		}

		public bool OffersFromPartners
		{
			get
			{
				return this.m_bOffersFromPartners;
			}
			set
			{
				this.m_bOffersFromPartners = value;
			}
		}

		public bool DeleteFriendsWarning
		{
			get
			{
				return this.m_bDeleteFriendsWarning;
			}
			set
			{
				this.m_bDeleteFriendsWarning = value;
			}
		}

		public bool DeleteMsgWarning
		{
			get
			{
				return this.m_bDeleteMsgWarning;
			}
			set
			{
				this.m_bDeleteMsgWarning = value;
			}
		}

		public string NewsSubjects
		{
			get
			{
				return this.m_strNewsSubjects;
			}
			set
			{
				this.m_strNewsSubjects = value;
			}
		}

		public string Interests
		{
			get
			{
				return this.m_strInterests;
			}
			set
			{
				this.m_strInterests = value;
			}
		}

		public bool Archive
		{
			get
			{
				return this.m_bArchive;
			}
			set
			{
				this.m_bArchive = value;
			}
		}

		public bool PublicSportsInterest
		{
			get
			{
				return this.m_bPublicSportsInterest;
			}
			set
			{
				this.m_bPublicSportsInterest = value;
			}
		}

		public bool SendCommentsEmail
		{
			get
			{
				return this.m_bSendCommentsEmail;
			}
			set
			{
				this.m_bSendCommentsEmail = value;
			}
		}

		public bool KeepLoggedIn
		{
			get
			{
				return this.m_bKeepLoggedIn;
			}
			set
			{
				this.m_bKeepLoggedIn = value;
			}
		}

#endregion

	}

/// <summary>
/// class Account
/// Represents a single account stored in the MyAccounts table
/// </summary>
	public class UserAccount : Address
    {
		public enum UserTypes { SuperUser = 1, Admin = 2, Contributor = 3, Trusted = 4, Normal = 5, Unverified = 6, Roster = 7, Anonymous = 8, Inactive = 9, Banned = 10 }
#region Column Constants
		const int colKey = 0;
		const int colOrgID = 1;
		const int colUserID = 2;
		const int colMemberType = 3;
		const int colNumber = 4;
		const int colPositions = 5;
		const int colNote = 6;
		const int colDeleted = 7;
		const int colAcceptedInvite = 8;
		const int colCreator = 9;
		const int colCreateDate = 10;
		const int colLastUpdate = 11;
#endregion

#region Member Variables
		private string m_strUserName;
		private UserTypes m_nUserType;
		private string m_strPassword;
		private bool m_bIsActive;
		private bool m_bAcceptedTOU;
		private string m_strTitle;
		private string m_strFirst;
		private string m_strMI;
		private string m_strLast;
		private string m_strNickname;
		private string m_strSuffix;
		private string m_dtBirthDate;
		private string m_strDefaultPage;
		private string m_strPhotoFile;
		private string m_strSecurityQuestion;
		private string m_strSecurityAnswer;
		private int m_nLoginAttempts;
		private DateTime m_dtLastLogin;
		private long m_CreatorID;
		private UserPreferences m_upPreferences;

		public SortedDictionary<long, object> m_sdMyOrgs = new SortedDictionary<long, object>();
#endregion

#region INIT
        private void Init()
		{
			InitAddress();
			m_strUserName = "";
			m_nUserType = UserTypes.Unverified;
			m_strPassword = "";
			m_bIsActive = true;
			m_bAcceptedTOU = false;
			m_strTitle = "";
			m_strFirst = "";
			m_strMI = "";
			m_strLast = "";
			m_strSuffix = "";
			m_dtBirthDate = "";
			m_strDefaultPage = "";
			m_strPhotoFile = "";
			m_strSecurityQuestion = "";
			m_strSecurityAnswer = "";
			m_nLoginAttempts = 0;
			m_upPreferences = new UserPreferences();
			m_dtLastLogin = DateTime.MinValue;
			m_strNickname = "";
			m_CreatorID = 0;
		}

		public UserAccount()
		{
			Init();
		}

#endregion

#region MyOrgs
		private bool ReadMyOrgs(IDataRecord dr, OrgMember om)
		{
			bool fRet = true;
			try
			{

				om.ConnectionString = m_strConnectionString;
				om.Key = ObjectToLong(dr[colKey]);
				om.OrgID = ObjectToLong(dr[colOrgID]);
				om.UserID = ObjectToLong(dr[colUserID]);
				om.MemberType = (OrgAccessTypes)ObjectToInt(dr[colMemberType]);
				om.Number = ObjectToString(dr[colNumber]);
				om.Positions = ObjectToString(dr[colPositions]);
				om.Note = ObjectToString(dr[colNote]);
				om.Deleted = ObjectToBool(dr[colDeleted]);
				om.AcceptedInvite = ObjectToBool(dr[colAcceptedInvite]);
				om.Creator = ObjectToString(dr[colCreator]);
				om.CreateDate = ObjectToDateTime(dr[colCreateDate]);
				om.LastUpdate = ObjectToString(dr[colLastUpdate]);
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Account:ReadOrgMember.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		public bool LoadMyOrgs()
		{
			bool fRet = false;

			m_sdMyOrgs.Clear();
			SqlDataReader reader = null;
			SqlConnection sqlConn = null;
			SqlParameter[] paramArray = new SqlParameter[1];
			paramArray[0] = new SqlParameter("@UserID", AccountID);

			if (ExecuteSPRows("sp_GetMyOrgs", paramArray, out reader, out sqlConn) && null != reader)
			{
				try
				{
					while (reader.Read())
					{
						IDataRecord dr = (IDataRecord)reader;
						OrgMember om = new OrgMember();
						fRet = ReadMyOrgs(dr, om);
						if (fRet)
						{
							m_sdMyOrgs.Add(om.Key, om);
						}
					}
					reader.Close();
					sqlConn.Close();
					fRet = true;
				}
				catch (Exception ex)
				{
					String strIntro = "Accounts:Error loading Orgs for " + AccountID;
					EvtLog.WriteException(strIntro, ex, 1);
				}
			}

			return fRet;
		}

#endregion

#region Misc
		public string DisplayName()
		{
			string strDisplay = "";
			if( m_strFirst.Length > 0 || m_strLast.Length > 0 )
			{
				strDisplay = m_strFirst + " " + m_strLast + " (" + m_strUserName + ")";
			}
			else
			{
				strDisplay = m_strUserName;
			}

			return strDisplay;
		}

		public bool IsAdmin()
		{
			bool fRet = false;
			if (UserType == UserAccount.UserTypes.Admin || UserType == UserAccount.UserTypes.SuperUser)
			{
				fRet = true;
			}
			return fRet;
		}
#endregion

#region Accessors
		public long AccountID
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

		public long CreatorID
		{
			get
			{
				return this.m_CreatorID;
			}
			set
			{
				this.m_CreatorID = value;
			}
		}

		public DateTime LastLogin
		{
			get
			{
				return this.m_dtLastLogin;
			}
			set
			{
				this.m_dtLastLogin = value;
			}
		}

		public UserPreferences Preferences
		{
			get
			{
				return this.m_upPreferences;
			}
			set
			{
				this.m_upPreferences = value;
			}
		}

		public string NickName
		{
			get
			{
				return this.m_strNickname;
			}
			set
			{
				this.m_strNickname = value;
			}
		}

		public string UserName
		{
			get
			{
				return this.m_strUserName;
			}
			set
			{
				this.m_strUserName = value;
			}
		}

		public UserTypes UserType
		{
			get
			{
				return this.m_nUserType;
			}
			set
			{
				this.m_nUserType = value;
			}
		}

		public string Password
		{
			get
			{
				return this.m_strPassword;
			}
			set
			{
				this.m_strPassword = value;
			}
		}

		public bool IsActive
		{
			get
			{
				return this.m_bIsActive;
			}
			set
			{
				this.m_bIsActive = value;
			}
		}

		public bool AcceptedTOU
		{
			get
			{
				return this.m_bAcceptedTOU;
			}
			set
			{
				this.m_bAcceptedTOU = value;
			}
		}
		
		public string Title
		{
			get
			{
				return this.m_strTitle;
			}
			set
			{
				this.m_strTitle = value;
			}
		}

		public string First
		{
			get
			{
				return this.m_strFirst;
			}
			set
			{
				this.m_strFirst = value;
			}
		}

		public string MI
		{
			get
			{
				return this.m_strMI;
			}
			set
			{
				this.m_strMI = value;
			}
		}

		public string Last
		{
			get
			{
				return this.m_strLast;
			}
			set
			{
				this.m_strLast = value;
			}
		}

		public string Suffix
		{
			get
			{
				return this.m_strSuffix;
			}
			set
			{
				this.m_strSuffix = value;
			}
		}

		public string BirthDate
		{
			get
			{
				return this.m_dtBirthDate;
			}
			set
			{
				this.m_dtBirthDate = value;
			}
		}

		public string DefaultPage
		{
			get
			{
				return this.m_strDefaultPage;
			}
			set
			{
				this.m_strDefaultPage = value;
			}
		}

		public string PhotoFile
		{
			get
			{
				return this.m_strPhotoFile;
			}
			set
			{
				this.m_strPhotoFile = value;
			}
		}

		public string SecurityQuestion
		{
			get
			{
				return this.m_strSecurityQuestion;
			}
			set
			{
				this.m_strSecurityQuestion = value;
			}
		}

		public string SecurityAnswer
		{
			get
			{
				return this.m_strSecurityAnswer;
			}
			set
			{
				this.m_strSecurityAnswer = value;
			}
		}

		public int LoginAttempts
		{
			get
			{
				return this.m_nLoginAttempts;
			}
			set
			{
				this.m_nLoginAttempts = value;
			}
		}

#endregion

#region Update All
/*
	@UserName nvarchar(50), 
	@UserType int,
	@Pswd nvarchar(50),
	@Language int,
	@IsActive bit,
	@AcceptedTOU bit,
	@Title nvarchar(50),
	@First nvarchar(50),
	@MI nvarchar(1),
	@Last nvarchar(50),
	@Nickname nvarchar(50),
	@Suffix nvarchar(50),
	@BirthDate nvarchar(50),
	@Address1 nvarchar(50),
	@Address2 nvarchar(50),
	@City nvarchar(50),
	@State nvarchar(50),
	@Zip nvarchar(50),
	@Country nvarchar(50),
	@Email nvarchar(50),
	@EmailVerified bit,
	@DefaultPage nvarchar(50),
	@PhotoFile nvarchar(50),
	@SecurityQuestion nvarchar(100),
	@SecurityAnswer nvarchar(50),
	@LoginAttempts int,
	@LastLogin datetime2(7),
	@CreatorID bigint,
	@LastUpdate nvarchar(max),
	@AcctID bigint
 */
		public bool Update()
		{
			bool fRet = false;

			try
			{
				string strClean = UserName + " -- " + DateTime.Now.ToString();
				this.LastUpdate = Sanitize(strClean);
				this.LastLogin = DateTime.Now;

				SqlParameter[] paramArray = new SqlParameter[30];
				paramArray[0] = new SqlParameter("@UserName", Sanitize(USCBase.Truncate(this.UserName, 49, true)));
				paramArray[1] = new SqlParameter("@UserType", UserType);
				paramArray[2] = new SqlParameter("@Pswd", Sanitize(USCBase.Truncate(this.Password, 49, true)));
				paramArray[3] = new SqlParameter("@Language", Language);
				paramArray[4] = new SqlParameter("@IsActive", SQLBitFromBool(IsActive));
				paramArray[5] = new SqlParameter("@AcceptedTOU", SQLBitFromBool(AcceptedTOU));
				paramArray[6] = new SqlParameter("@Title", Sanitize(USCBase.Truncate(this.Title, 49, true)));
				paramArray[7] = new SqlParameter("@First", Sanitize(USCBase.Truncate(this.First, 49, true)));
				paramArray[8] = new SqlParameter("@MI", Sanitize(USCBase.Truncate(this.MI, 1, false)));
				paramArray[9] = new SqlParameter("@Last", Sanitize(USCBase.Truncate(this.Last, 49, true)));
				paramArray[10] = new SqlParameter("@Nickname", Sanitize(USCBase.Truncate(this.NickName, 49, true)));
				paramArray[11] = new SqlParameter("@Suffix", Sanitize(USCBase.Truncate(this.Suffix, 49, true)));
				paramArray[12] = new SqlParameter("@BirthDate", Sanitize(USCBase.Truncate(this.BirthDate, 49, true)));
				paramArray[13] = new SqlParameter("@Address1", Sanitize(USCBase.Truncate(this.Address1, 49, true)));
				paramArray[14] = new SqlParameter("@Address2", Sanitize(USCBase.Truncate(this.Address2, 49, true)));
				paramArray[15] = new SqlParameter("@City", Sanitize(USCBase.Truncate(this.City, 49, true)));
				paramArray[16] = new SqlParameter("@State", Sanitize(USCBase.Truncate(this.State, 49, true)));
				paramArray[17] = new SqlParameter("@Zip", Sanitize(USCBase.Truncate(this.Zip, 49, true)));
				paramArray[18] = new SqlParameter("@Country", Sanitize(USCBase.Truncate(this.Country, 49, true)));
				paramArray[19] = new SqlParameter("@Email", Sanitize(USCBase.Truncate(this.Email, 49, true)));
				paramArray[20] = new SqlParameter("@EmailVerified", SQLBitFromBool(this.EmailValid));
				paramArray[21] = new SqlParameter("@DefaultPage", Sanitize(USCBase.Truncate(this.DefaultPage, 49, true)));
				paramArray[22] = new SqlParameter("@PhotoFile", Sanitize(USCBase.Truncate(this.PhotoFile, 49, true)));
				paramArray[23] = new SqlParameter("@SecurityQuestion", Sanitize(USCBase.Truncate(this.SecurityQuestion, 99, true)));
				paramArray[24] = new SqlParameter("@SecurityAnswer", Sanitize(USCBase.Truncate(this.SecurityAnswer, 49, true)));
				paramArray[25] = new SqlParameter("@LoginAttempts", LoginAttempts);
				paramArray[26] = new SqlParameter("@LastLogin", this.LastLogin);
				paramArray[27] = new SqlParameter("@CreatorID", this.CreatorID);
				paramArray[28] = new SqlParameter("@LastUpdate", this.LastUpdate);
				paramArray[29] = new SqlParameter("@AcctID", this.Key);

				if (ExecuteSPNoValue("sp_UpdateUserAccount", paramArray))
				{
					fRet = true;
				}
			}
			catch (Exception ex)
			{
				string strErr = "Account.Update failure";
				short sCat = 0;
				if (IsLocalInstance())
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.AccountUpdate, sCat);
				fRet = false;
			}

			return fRet;
		}
#endregion

#region Updates
		private void BuildNewsSubjects()
		{
			Preferences.NewsSubjects = "";
			foreach (DictionaryEntry de in Preferences.htNewsMenuItems)
			{
				int item = (int)de.Value;
				Preferences.NewsSubjects += item + ",";
			}

			// Remove the last comma
			if (Preferences.NewsSubjects.Length > 0)
			{
				Preferences.NewsSubjects.TrimEnd(',');
			}
		}

/*
		CREATE PROCEDURE sp_UpdateUserPrefs 
			-- Add the parameters for the stored procedure here
			@OffersFromUs bit, 
			@OffersFromPartners bit, 
			@DeleteFriendsWarning bit, 
			@DeleteMessageWarning bit,
			@NewsSubjects nvarchar(200), 
			@Interests nvarchar(500), 
			@Archive bit, 
			@PublicSportsInterest bit, 
			@CommentsEmails bit, 
			@KeepLoggedIn bit,
			@LastUpdate nvarchar(max),
			@ShowNickname bit,
			@ProfileUpdated bit,
			@ProvideSecurityQuestion bit,
			@AcctID bigint
*/
		public bool UpdatePreferences()
		{
			bool fRet = false;

			try
			{
				// Timestamp it
				string strClean = UserName + " -- " + DateTime.Now.ToString();
				this.Preferences.LastUpdate = Sanitize(strClean);
				BuildNewsSubjects();

				//Sanitize(USCBase.Truncate(this.Title, 49, true))
				SqlParameter[] paramArray = new SqlParameter[15];
				paramArray[0] = new SqlParameter("@OffersFromUs", SQLBitFromBool(this.Preferences.OffersFromUs));
				paramArray[1] = new SqlParameter("@OffersFromPartners", SQLBitFromBool(this.Preferences.OffersFromPartners));
				paramArray[2] = new SqlParameter("@DeleteFriendsWarning", SQLBitFromBool(this.Preferences.DeleteFriendsWarning));
				paramArray[3] = new SqlParameter("@DeleteMessageWarning", SQLBitFromBool(this.Preferences.DeleteMsgWarning));
				paramArray[4] = new SqlParameter("@NewsSubjects", Sanitize(USCBase.Truncate(this.Preferences.NewsSubjects, 199, true)));
				paramArray[5] = new SqlParameter("@Interests", Sanitize(USCBase.Truncate(this.Preferences.Interests, 499, true)));
				paramArray[6] = new SqlParameter("@Archive", SQLBitFromBool(this.Preferences.Archive));
				paramArray[7] = new SqlParameter("@PublicSportsInterest", SQLBitFromBool(this.Preferences.PublicSportsInterest));
				paramArray[8] = new SqlParameter("@CommentsEmails", SQLBitFromBool(this.Preferences.SendCommentsEmail));
				paramArray[9] = new SqlParameter("@KeepLoggedIn", SQLBitFromBool(this.Preferences.KeepLoggedIn));
				paramArray[10] = new SqlParameter("@LastUpdate", Sanitize(this.Preferences.LastUpdate));
				paramArray[11] = new SqlParameter("@ShowNickname", SQLBitFromBool(this.Preferences.ShowNickname));
				paramArray[12] = new SqlParameter("@ProfileUpdated", SQLBitFromBool(this.Preferences.ProfileUpdated));
				paramArray[13] = new SqlParameter("@ProvideSecurityQuestion", SQLBitFromBool(this.Preferences.ProvideSecurityQuestion));
				paramArray[14] = new SqlParameter("@AcctID", Key);

				if (ExecuteSPNoValue("sp_UpdateUserPrefs", paramArray))
				{
					fRet = true;
				}
			}
			catch (Exception ex)
			{
				string strErr = "Account.UpdatePreferences failure";
				short sCat = 0;
				if (IsLocalInstance())
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.AccountUpdate, sCat);
				fRet = false;
			}

			return fRet;
		}

/*
CREATE PROCEDURE sp_UpdateUserLastLogin
	-- Add the parameters for the stored procedure here
	@LastLogin datetime2(7),
	@AcctID bigint
*/
		public bool UpdateUserLastLogin()
		{
			bool fRet = false;

			try
			{
				string strClean = UserName + " -- " + DateTime.Now.ToString();
				this.LastUpdate = Sanitize(strClean);
				this.LastLogin = DateTime.Now;

				SqlParameter[] paramArray = new SqlParameter[2];
				paramArray[0] = new SqlParameter("@LastLogin", this.LastLogin);
				paramArray[1] = new SqlParameter("@AcctID", this.Key);

				if (ExecuteSPNoValue("sp_UpdateUserLastLogin", paramArray))
				{
					fRet = true;
				}
			}
			catch (Exception ex)
			{
				string strErr = "Account.UpdateUserLastLogin failure";
				short sCat = 0;
				if (IsLocalInstance())
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.AccountUpdate, sCat);
				fRet = false;
			}


			return fRet;
		}

		public bool UpdateUserVerified()
		{
			UserType = UserTypes.Normal;
			EmailValid = true;
			return Update();
		}

		public bool UpdateUserType()
		{
			return Update();
		}

		public bool UpdateUserPhoto()
		{
			return Update();
		}

		public bool UpdatePassword()
		{
			USCEncrypt usce = new USCEncrypt();
			this.Password = usce.EncryptString(this.Password);
			return Update();
		}

		public bool UpdateTOU()
		{
			return Update();
		}

#endregion

#region User Folders
		public bool CreateUserFolders(string strRootPath)
		{
			bool fRet = true;

			string strRootUserNamePath = GetUserRootPath( strRootPath );
			string strRootUserNamePathDisplayPhoto = GetUserPhotoPath( strRootPath );
			string strRootUserNamePathMediaFolder = GetUserMediaPath( strRootPath );

			try
			{
				//here we create the folder in the Users Folder
				System.IO.Directory.CreateDirectory(strRootUserNamePath);
				System.IO.Directory.CreateDirectory(strRootUserNamePathDisplayPhoto);
				System.IO.Directory.CreateDirectory(strRootUserNamePathMediaFolder);
			}
			catch( Exception ex )
			{
				fRet = false;
				string strEx = "Account.CreateUserFolders for " + this.UserName;
				EvtLog.WriteException( strEx, ex, 1 );
			}

			return fRet;
		}

		public string GetUserRootPath( string strRootPath )
		{
			string strRet = "";
			string strUsersFolder = GetSiteSetting(SiteAdmin.saKeyUserImageUrl, "StoreUserImageURL", this.Language);
			string strRootUserPath = strRootPath + strUsersFolder;

			strRet = strRootUserPath + "\\" + this.UserName;

			return strRet;
		}

		public string GetUserPhotoPath( string strRootPath )
		{
			string strRet = "";
			string strDisplayPhotoFolder = GetSiteSetting(SiteAdmin.saKeyDisplayPhotoFolder, "DisplayPhoto", this.Language);

			strRet = GetUserRootPath( strRootPath ) + "\\" + strDisplayPhotoFolder;

			return strRet;
		}

		public string GetUserMediaPath( string strRootPath )
		{
			string strRet = "";
			string strUsersMediaFolder = GetSiteSetting(SiteAdmin.saKeyUserMediaFolder, "UserMediaFolderName", this.Language);

			strRet = GetUserRootPath( strRootPath ) + "\\" + strUsersMediaFolder;

			return strRet;
		}
#endregion

	}

	public sealed class AccountsList : USCBaseList
	{
#region Column Constants
        const int colKey = 0;
        const int colUserName = 1;
		const int colUserType = 2;
		const int colPassword = 3;
		const int colLanguage = 4;
		const int colIsActive = 5;
		const int colAcceptedTOU = 6;
		const int colTitle = 7;
        const int colFirst = 8;
        const int colMI = 9;
        const int colLast = 10;
		const int colNickname = 11;
		const int colSuffix = 12;
        const int colBirthDate = 13; 
        const int colAddress1 = 14;
        const int colAddress2 = 15;
        const int colCity = 16;
        const int colState = 17;
        const int colZip = 18;
		const int colCountry = 19;
		const int colEmail = 20;
		const int colEmailValid = 21;
		const int colDefaultPage = 22;
		const int colPhotoFile = 23;
		const int colSecurityQuestion = 24;
		const int colSecurityAnswer = 25;
		const int colLoginAttempts = 26;
		const int colLastLogin = 27;
		const int colCreatorID = 28;
		const int colCreator = 29;
		const int colCreateDate = 30;
		const int colLastUpdate = 31;

		const int colPrefKey = 0;
		const int colPrefAcctID = 1;
		const int colPrefOffersFromUs = 2;
		const int colPrefOffersFromPartners = 3;
		const int colPrefDeleteFriendsWarning = 4;
		const int colPrefDeleteMsgWarning = 5;
		const int colPrefNewsSubjects = 6;
		const int colPrefInterests = 7;
		const int colPrefArchive = 8;
		const int colPrefPublicSportsInterest = 9;
		const int colPrefCommentsEmails = 10;
		const int colPrefKeepLoggedIn = 11;
		const int colPrefCreator = 12;
		const int colPrefCreateDate = 13;
		const int colPrefLastUpdate = 14;
		const int colPrefShowNickname = 15;
		const int colPrefProfileUpdated = 16;
#endregion

		private static volatile AccountsList instance = null;
		private static object syncRoot = new object();
		private string m_strForumCnxString = "";

        const string strSQLGetAllAccounts = "SELECT * FROM Accounts";
		const string strSQLGetAllPreferences = "SELECT * FROM Preferences";
		const string strSQLAddUser = "INSERT INTO Accounts(UserName,UserType,Password,Language,IsActive,AcceptedTOU,Title,FirstName,MI,LastName,Suffix,BirthDate,Address1,Address2,City,State,Zipcode,Country,EmailAddress,EmailVerified,DefaultPage,PhotoFile,SecurityQuestion,SecurityAnswer,LoginAttempts,IM_ID1,IM_ID2,IM_ID3,IM_ID4,IM_ID5,IM_ID6,CreationUser,CreationDate,LastUpdateUserTime) VALUES (";
		const string strSQLAddPrefs = "INSERT INTO Preferences(AccountID,OffersFromUs,OffersFromPartners,DeleteFriendsWarning,DeleteMessageWarning,NewsSubjects,Interests,Archive,PublicSportsInterest,CommentsEmails,KeepLoggedIn,CreationUser,CreationDate,LastUpdateUserTime) VALUES (";
#region Init
        private AccountsList()
		{
		}

        public static AccountsList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
                            instance = new AccountsList();
					}
				}

				return instance;
			}
		}

		public Hashtable htAccountsList;
		public Hashtable htAccountPrefsList;

		public void Init(string cnxString, string strForumCnxString,  bool fForce)
		{
			m_strConnectionString = cnxString;
			if( null != strForumCnxString && strForumCnxString.Length > 0 )
			{
				m_strForumCnxString = strForumCnxString;
			}
			if (null == htAccountsList || fForce)
			{
				htAccountsList = new Hashtable();
				htAccountPrefsList = new Hashtable();
				Load();
			}
		}

		public int Count
		{
			get
			{
				return htAccountsList.Count;
			}
		}
#endregion

#region Load
		private bool LoadPreferences()
		{
			bool fRet = false;

			htAccountPrefsList.Clear();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

            EvtLog.WriteEvent( m_strConnectionString, "LogTest", EventLogEntryType.Information, 69, 1);

			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllPreferences, sqlConn);
				daLocStrings.Fill(locStrDS, "Preferences");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Accounts.LoadPreferences failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			try
			{
				DataRowCollection dra = locStrDS.Tables["Preferences"].Rows;
				foreach (DataRow dr in dra)
				{
					UserPreferences pref = new UserPreferences();
					pref.ConnectionString = ConnectionString;
					fRet = ReadPreference(dr, pref);
					if (fRet)
					{
						if (pref.NewsSubjects.Length == 0)
						{
							BuildInitialNewMenuItems(pref);
						}
						if( 0 >= pref.AccountID )
						{
							//int z=0;
						}
						else
						{
							htAccountPrefsList.Add(pref.AccountID, pref);
						}
					}
				}
			}
			catch( Exception ex )
			{
				EvtLog.WriteException("Accounts.LoadPreferences (enum) failure", ex, 0);
				return false;
			}
			fRet = true;

			return fRet;
		}

		private bool LoadAccounts()
		{
			bool fRet = false;

			htAccountsList.Clear();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllAccounts, sqlConn);
				daLocStrings.Fill(locStrDS, "Accounts");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Accounts.LoadAccounts failure", ex, 0);
				ExceptionText = "LoadAccounts:" + ex.Message;
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["Accounts"].Rows;
			foreach (DataRow dr in dra)
			{
				UserAccount acct = new UserAccount();
				acct.ConnectionString = ConnectionString;
				fRet = ReadAccount(dr, acct);
				if (fRet)
				{
					// Set now so objects can update themselves
					acct.ConnectionString = m_strConnectionString;

					UserPreferences pref = (UserPreferences)htAccountPrefsList[acct.Key];
					if( null != pref )
					{
						acct.Preferences = pref;
					}
					else
					{
						acct.Preferences = new UserPreferences();
						acct.Preferences.AccountID = acct.Key;
					}

					htAccountsList.Add(acct.Key, acct);
				}
			}
			fRet = true;

			return fRet;
		}

		public bool Load()
        {
            bool fRet = true;

            LoadPreferences();
			LoadAccounts();

            return fRet;
        }
#endregion

#region ReadValues
        private bool ReadAccount(DataRow dr, UserAccount acct)
        {
            bool fRet = true;
            try
            {
				acct.Key = ObjectToLong(dr.ItemArray[colKey]);
				acct.UserName = ObjectToString( dr.ItemArray[colUserName]);
				acct.UserType = (UserAccount.UserTypes)ObjectToInt( dr.ItemArray[colUserType] );
				acct.Password = ObjectToString( dr.ItemArray[colPassword]);
				acct.Language = ObjectToInt( dr.ItemArray[colLanguage]);
				acct.IsActive = ObjectToBool(dr.ItemArray[colIsActive]);
				acct.AcceptedTOU = ObjectToBool(dr.ItemArray[colAcceptedTOU] );
				acct.Title = ObjectToString( dr.ItemArray[colTitle] );
				acct.First = ObjectToString( dr.ItemArray[colFirst] );
				acct.MI = ObjectToString( dr.ItemArray[colMI] );
				acct.Last = ObjectToString( dr.ItemArray[colLast] );
				acct.NickName = ObjectToString(dr.ItemArray[colNickname]);
				acct.Suffix = ObjectToString(dr.ItemArray[colSuffix]);
				acct.BirthDate = ObjectToString( dr.ItemArray[colBirthDate] );
				acct.Address1 = ObjectToString( dr.ItemArray[colAddress1] );
				acct.Address2 = ObjectToString( dr.ItemArray[colAddress2] );
				acct.City = ObjectToString( dr.ItemArray[colCity] );
				acct.State = ObjectToString( dr.ItemArray[colState] );
				acct.Zip = ObjectToString( dr.ItemArray[colZip] );
				acct.Country = ObjectToString( dr.ItemArray[colCountry] );
				acct.Email = ObjectToString( dr.ItemArray[colEmail] );
				acct.EmailValid = ObjectToBool ( dr.ItemArray[colEmailValid] );
				acct.DefaultPage = ObjectToString( dr.ItemArray[colDefaultPage] );
				acct.PhotoFile = ObjectToString( dr.ItemArray[colPhotoFile] );
				acct.SecurityQuestion = ObjectToString( dr.ItemArray[colSecurityQuestion] );
				acct.SecurityAnswer = ObjectToString( dr.ItemArray[colSecurityAnswer] );
				acct.LoginAttempts = ObjectToInt( dr.ItemArray[colLoginAttempts] );
				acct.LastLogin = ObjectToDateTime(dr.ItemArray[colLastLogin]);
				acct.CreatorID = ObjectToLong(dr.ItemArray[colCreatorID]);
				acct.Creator = ObjectToString(dr.ItemArray[colCreator]);
				acct.CreateDate = ObjectToDateTime( dr.ItemArray[colCreateDate] );
				acct.LastUpdate = ObjectToString( dr.ItemArray[colLastUpdate] );
            }
            catch (Exception ex)
            {
				string strErr = "Accounts.ReadAccount failure";
				short sCat = 0;
				if (IsLocalInstance())
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.AccountRead, sCat);
				//ExceptionText = "ReadAccount:" + ex.Message;
				fRet = false;
            }
            return fRet;
        }

		private bool ReadPreference(DataRow dr, UserPreferences pref)
		{
			bool fRet = true;
			try
			{
				pref.Key = ObjectToLong(dr.ItemArray[colPrefKey]);
				pref.AccountID = ObjectToLong(dr.ItemArray[colPrefAcctID]);
				pref.OffersFromUs = ObjectToBool(dr.ItemArray[colPrefOffersFromUs]);
				pref.OffersFromPartners = ObjectToBool( dr.ItemArray[colPrefOffersFromPartners]);
				pref.DeleteFriendsWarning = ObjectToBool(dr.ItemArray[colPrefDeleteFriendsWarning]);
				pref.DeleteMsgWarning = ObjectToBool(dr.ItemArray[colPrefDeleteMsgWarning]);
				pref.NewsSubjects = ObjectToString( dr.ItemArray[colPrefNewsSubjects]);
				pref.Interests = ObjectToString( dr.ItemArray[colPrefInterests]);
				pref.Archive = ObjectToBool(dr.ItemArray[colPrefArchive]);
				pref.PublicSportsInterest = ObjectToBool(dr.ItemArray[colPrefPublicSportsInterest]);
				pref.SendCommentsEmail = ObjectToBool(dr.ItemArray[colPrefCommentsEmails]);
				pref.KeepLoggedIn = ObjectToBool(dr.ItemArray[colPrefKeepLoggedIn]);
				pref.Creator = ObjectToString( dr.ItemArray[colPrefCreator]);
				pref.CreateDate = ObjectToDateTime(dr.ItemArray[colPrefCreateDate]);
				pref.LastUpdate = ObjectToString( dr.ItemArray[colPrefLastUpdate]);
				pref.ShowNickname = ObjectToBool(dr.ItemArray[colPrefShowNickname]);
				pref.ProfileUpdated = ObjectToBool(dr.ItemArray[colPrefProfileUpdated]);

				string strFavSports = pref.NewsSubjects;
				if (strFavSports.Length > 0)
				{
					List<string> lstSports = new List<string>();
					lstSports = strFavSports.Trim().Split(',').ToList();

					for (int intCounter = 0; intCounter < lstSports.Count; intCounter++)
					{
						string strT = lstSports[intCounter].Trim();
						if (strT.Length > 0)
						{
							int key = Convert.ToInt32(strT);
							pref.htNewsMenuItems.Add(key, key);
						}
					}
				}
			}
			catch (Exception ex)
			{
				string strErr = "Accounts.ReadPrefs failure";
				short sCat = 0;
				if (IsLocalInstance())
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.AccountRead, sCat);
				//ExceptionText = "ReadPrefs: " + ex.Message;
				fRet = false;
			}
			return fRet;
		}
#endregion

#region FindPossibleFriends
		private bool IsInterest( UserAccount acct, string strInterest )
		{
			bool fRet = false;

			string strSearch = acct.Preferences.Interests.ToLower();
			strInterest = strInterest.ToLower();
			int nIndex = strSearch.IndexOf( strInterest );
			if( 0 <= nIndex )
			{
				fRet = true;
			}


			return fRet;
		}

		/// <summary>
		/// FindPossibleFriends
		/// Find people that match the search criteria provided by the user.
		/// Note: This is not the ideal solution as it relies on all accounts being in memory which is not sustainable.  It will eventually need to be a query.
		/// </summary>
		/// <param name="strFirst"></param>
		/// <param name="strLast"></param>
		/// <param name="strCity"></param>
		/// <param name="strState"></param>
		/// <param name="strZipcode"></param>
		/// <param name="htResult"></param>
		/// <returns>The number of matching people.</returns>
		/// 
		public int QueryPossibleFriends(UserAccount acctQuery, int nMaxCount, SortedDictionary<long, object> sdResults)
		{
			sdResults.Clear();

			//Set all search terms to lower case once so we don't have to do it for every iteration.
			string strFirst = acctQuery.First.ToLower();

			string strLast = acctQuery.Last.ToLower();
			string strCity = acctQuery.City.ToLower();
			string strState = acctQuery.State.ToLower();
			string strZipcode = acctQuery.Zip.ToLower();
			string strInterest = acctQuery.Preferences.Interests.ToLower();

			if( nMaxCount > 100 )
			{
				nMaxCount = 100;
			}
			int nCount = 0;

			foreach (DictionaryEntry de in htAccountsList)
			{
				UserAccount acct = (UserAccount)de.Value;
				if ( (strFirst.Equals("") ||  acct.First.ToLower().Contains(strFirst)) &&
					(strLast.Equals("") || acct.Last.ToLower().Contains(strLast)) &&
					(strCity.Equals("") || acct.City.ToLower().Contains(strCity)) &&
					(strState.Equals("") || acct.State.ToLower().Contains(strState)) &&
					(strZipcode.Equals("") || acct.Zip.ToLower().Contains(strZipcode)) &&
					(strInterest.Equals("") || IsInterest( acct, strInterest )))
				{
					// TODO: Check for current friends, self and current requests
					if(acct.IsActive)
					{
						sdResults.Add( acct.Key, acct );
					}
					nCount++;
					if( nMaxCount <= nCount )
					{
						break;
					}
				}
			}

			return sdResults.Count;
		}
#endregion

#region Account Access
		public UserAccount GetAccountByUserName( string strUserName )
		{
			UserAccount acctRet = null;

			string strTemp = strUserName.ToLower();
			foreach (DictionaryEntry de in htAccountsList)
			{
				UserAccount acct = (UserAccount)de.Value;
				if (strTemp.Equals(acct.UserName.ToLower()))
				{
					acctRet = acct;
					// No need to keep looking.
					break;
				}
			}

			return acctRet;
		}

		public UserAccount GetAccountByKey(long lID)
		{
			UserAccount acctRet = null;

			foreach (DictionaryEntry de in htAccountsList)
			{
				UserAccount acct = (UserAccount)de.Value;
				if( lID == acct.Key )
				{
					acctRet = acct;
					// No need to keep looking.
					break;
				}
			}

			return acctRet;
		}

		public UserAccount GetAccountByKey( string strID )
		{
			UserAccount acctRet = null;
			long lID = -1;
			if( long.TryParse( strID, out lID ) )
			{
				acctRet = GetAccountByKey( lID );
			}

			return acctRet;
		}

		public bool IsUserNameAvailable(string strUserName)
		{
			bool fNameIsAvailable = true;

			// Assumes minimum length and char requirements were already verified.
			string strTemp = strUserName.ToLower();
			foreach (DictionaryEntry de in htAccountsList)
			{
				UserAccount acct = (UserAccount)de.Value;
				if(strTemp.Equals( acct.UserName.ToLower()))
				{
					fNameIsAvailable = false;
					// No need to keep looking.
					break;
				}
			}

			return fNameIsAvailable;
		}
#endregion

#region Add User
#region Forum Acct
		public bool SetForumPswd( UserAccount acct, string pswd )
		{
			YafMembershipProvider yaf = new YafMembershipProvider();
			yaf.ApplicationName = "YetAnotherForum";
			return yaf.ChangePassword(acct.UserName, "-", pswd);
/*
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[3];
			paramArray[0] = new SqlParameter("@UserID", Convert.ToInt32(lKey));
			paramArray[1] = new SqlParameter("@OldPassword", "-");
			paramArray[2] = new SqlParameter("@NewPassword", Sanitize(USCBase.Truncate(pswd, 32, false)));

			if( ExecuteSPNoValue("yaf_user_changepassword", m_strForumCnxString, paramArray))
			{
				fRet = true;
			}

			return fRet;
*/
		}

		public bool SetForumUserGroup(long lKey, int nGroup)
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[2];
			paramArray[0] = new SqlParameter("@UserID", Convert.ToInt32(lKey));
			paramArray[1] = new SqlParameter("@GroupID", nGroup);

			if (ExecuteSPNoValue("sp_AddForumUserToGroup", m_strForumCnxString, paramArray))
			{
				fRet = true;
			}

			return fRet;
		}

/*
       public override MembershipUser CreateUser(
            string username,
            string password,
            string email,
            string passwordQuestion,
            string passwordAnswer,
            bool isApproved,
            object providerUserKey,
            out MembershipCreateStatus status)
*/
		public bool AddForumUser(UserAccount acct)
		{
			bool fRet = false;

			SqlParameter[] paramArray = new SqlParameter[5];
			paramArray[0] = new SqlParameter("@BoardID", 1);
			paramArray[1] = new SqlParameter("@UserName", Sanitize(USCBase.Truncate(acct.UserName, 49, true)));
			paramArray[2] = new SqlParameter("@DisplayName", Sanitize(USCBase.Truncate(acct.DisplayName(), 49, true)));
			paramArray[3] = new SqlParameter("@Email", Sanitize(USCBase.Truncate(acct.Email, 49, true)));
			paramArray[4] = new SqlParameter("@UTCTIMESTAMP", DateTime.Now.ToShortDateString());

			long lKey = ExecuteSPInsert("sp_AddForumUser", m_strForumCnxString, paramArray);
			if (lKey > 0)
			{
				YafMembershipProvider yaf = new YafMembershipProvider();
				yaf.ApplicationName = "YafMembershipProvider";
				USCEncrypt usce = new USCEncrypt();
				string strDecryptedPswd = usce.DecryptString(acct.Password);

				MembershipCreateStatus status;
				MembershipUser user = yaf.CreateUser(acct.UserName,
														strDecryptedPswd,
														acct.Email,
														acct.SecurityQuestion,
														acct.SecurityAnswer,
														true,
														null,
														out status);

				if (status == MembershipCreateStatus.Success)
				{
					fRet = true;
				}
			}

			return fRet;
		}
#endregion

/*
CREATE PROCEDURE sp_AddUserPrefs 
	-- Add the parameters for the stored procedure here
	@AcctID bigint,
	@OffersFromUs bit, 
	@OffersFromPartners bit, 
	@DeleteFriendsWarning bit, 
	@DeleteMessageWarning bit,
	@NewsSubjects nvarchar(200), 
	@Interests nvarchar(500), 
	@Archive bit, 
	@PublicSportsInterest bit, 
	@CommentsEmails bit, 
	@KeepLoggedIn bit,
    @CreationUser nvarchar(50),
	@ShowNickname bit,
	@ProfileUpdated bit,
	@ProvideSecurityQuestion bit
 */
		public bool AddPrefs(UserAccount acct)
		{
			bool fRet = false;

			try
			{
				acct.Preferences.AccountID = acct.Key;
				acct.Preferences.ConnectionString = ConnectionString;

				string strClean = acct.UserName + " -- " + DateTime.Now.ToString();
				acct.LastUpdate = Sanitize(strClean);
				acct.LastLogin = DateTime.Now;
				BuildInitialNewMenuItems(acct.Preferences);

				SqlParameter[] paramArray = new SqlParameter[15];
				paramArray[0] = new SqlParameter("@AcctID", acct.Preferences.AccountID);
				paramArray[1] = new SqlParameter("@OffersFromUs", SQLBitFromBool(acct.Preferences.OffersFromUs));
				paramArray[2] = new SqlParameter("@OffersFromPartners", SQLBitFromBool(acct.Preferences.OffersFromPartners));
				paramArray[3] = new SqlParameter("@DeleteFriendsWarning", SQLBitFromBool(acct.Preferences.DeleteFriendsWarning));
				paramArray[4] = new SqlParameter("@DeleteMessageWarning", SQLBitFromBool(acct.Preferences.DeleteMsgWarning));
				paramArray[5] = new SqlParameter("@NewsSubjects", Sanitize(USCBase.Truncate(acct.Preferences.NewsSubjects, 199, true)));
				paramArray[6] = new SqlParameter("@Interests", Sanitize(USCBase.Truncate(acct.Preferences.Interests, 499, true)));
				paramArray[7] = new SqlParameter("@Archive", SQLBitFromBool(acct.Preferences.Archive));
				paramArray[8] = new SqlParameter("@PublicSportsInterest", SQLBitFromBool(acct.Preferences.PublicSportsInterest));
				paramArray[9] = new SqlParameter("@CommentsEmails", SQLBitFromBool(acct.Preferences.SendCommentsEmail));
				paramArray[10] = new SqlParameter("@KeepLoggedIn", SQLBitFromBool(acct.Preferences.KeepLoggedIn));
				paramArray[11] = new SqlParameter("@CreationUser", Sanitize(USCBase.Truncate(acct.UserName, 49, true)));
				paramArray[12] = new SqlParameter("@ShowNickname", SQLBitFromBool(acct.Preferences.ShowNickname));
				paramArray[13] = new SqlParameter("@ProfileUpdated", SQLBitFromBool(acct.Preferences.ProfileUpdated));
				paramArray[14] = new SqlParameter("@ProvideSecurityQuestion", SQLBitFromBool(acct.Preferences.ProvideSecurityQuestion));

				acct.Preferences.Key = ExecuteSPInsert("sp_AddUserPrefs", paramArray);
				if (acct.Preferences.Key > 0)
				{
					fRet = true;
				}
			}
			catch (Exception ex)
			{
				string strErr = "AccountsList.AddPrefs failure";
				short sCat = 0;
				if (IsLocalInstance())
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.AccountAdd, sCat);
				ExceptionText = "sp_AddUserPrefs:" + ex.Message;
				fRet = false;
			}



			return fRet;
		}

		private void BuildInitialNewMenuItems( UserPreferences prefs )
		{
			prefs.NewsSubjects = "";
			for( int x=1; x<=20; x++ )
			{
				prefs.htNewsMenuItems.Add(x, x);
				prefs.NewsSubjects += x + ",";
			}

			// Remove the last comma
			if (prefs.NewsSubjects.Length > 0)
			{
				prefs.NewsSubjects.TrimEnd(',');
			}
		}

/*
		CREATE PROCEDURE sp_AddUserAccount 
			-- Add the parameters for the stored procedure here
			@UserName nvarchar(50), 
			@UserType int,
			@Pswd nvarchar(50),
			@Language int,
			@IsActive bit,
			@AcceptedTOU bit,
			@Title nvarchar(50),
			@First nvarchar(50),
			@MI nvarchar(1),
			@Last nvarchar(50),
			@Suffix nvarchar(50),
			@BirthDate nvarchar(50),
			@Address1 nvarchar(50),
			@Address2 nvarchar(50),
			@City nvarchar(50),
			@State nvarchar(50),
			@Zip nvarchar(50),
			@Country nvarchar(50),
			@Email nvarchar(50),
			@EmailVerified bit,
			@DefaultPage nvarchar(50),
			@PhotoFile nvarchar(50),
			@SecurityQuestion nvarchar(50),
			@SecurityAnswer nvarchar(50),
			@CreationUser nvarchar(50),
			@Nickname nvarchar(50)
 */
		public bool AddUser(UserAccount acct)
		{
			bool fRet = false;

			try
			{
				acct.ConnectionString = ConnectionString;

				string strClean = acct.UserName + " -- " + DateTime.Now.ToString();
				acct.LastUpdate = Sanitize(strClean);
				acct.LastLogin = DateTime.Now;

				USCEncrypt usce = new USCEncrypt();
				acct.Password = usce.EncryptString(acct.Password);

				SqlParameter[] paramArray = new SqlParameter[27];
				paramArray[0] = new SqlParameter("@UserName", Sanitize(USCBase.Truncate(acct.UserName, 49, true)));
				paramArray[1] = new SqlParameter("@UserType", acct.UserType);
				paramArray[2] = new SqlParameter("@Pswd", Sanitize(USCBase.Truncate(acct.Password, 49, true)));
				paramArray[3] = new SqlParameter("@Language", acct.Language);
				paramArray[4] = new SqlParameter("@IsActive", SQLBitFromBool(acct.IsActive));
				paramArray[5] = new SqlParameter("@AcceptedTOU", SQLBitFromBool(acct.AcceptedTOU));
				paramArray[6] = new SqlParameter("@Title", Sanitize(USCBase.Truncate(acct.Title, 49, true)));
				paramArray[7] = new SqlParameter("@First", Sanitize(USCBase.Truncate(acct.First, 49, true)));
				paramArray[8] = new SqlParameter("@MI", Sanitize(USCBase.Truncate(acct.MI, 1, false)));
				paramArray[9] = new SqlParameter("@Last", Sanitize(USCBase.Truncate(acct.Last, 49, true)));
				paramArray[10] = new SqlParameter("@Nickname", Sanitize(USCBase.Truncate(acct.NickName, 49, true)));
				paramArray[11] = new SqlParameter("@Suffix", Sanitize(USCBase.Truncate(acct.Suffix, 49, true)));
				paramArray[12] = new SqlParameter("@BirthDate", Sanitize(USCBase.Truncate(acct.BirthDate, 49, true)));
				paramArray[13] = new SqlParameter("@Address1", Sanitize(USCBase.Truncate(acct.Address1, 49, true)));
				paramArray[14] = new SqlParameter("@Address2", Sanitize(USCBase.Truncate(acct.Address2, 49, true)));
				paramArray[15] = new SqlParameter("@City", Sanitize(USCBase.Truncate(acct.City, 49, true)));
				paramArray[16] = new SqlParameter("@State", Sanitize(USCBase.Truncate(acct.State, 49, true)));
				paramArray[17] = new SqlParameter("@Zip", Sanitize(USCBase.Truncate(acct.Zip, 49, true)));
				paramArray[18] = new SqlParameter("@Country", Sanitize(USCBase.Truncate(acct.Country, 49, true)));
				paramArray[19] = new SqlParameter("@Email", Sanitize(USCBase.Truncate(acct.Email, 49, true)));
				paramArray[20] = new SqlParameter("@EmailVerified", SQLBitFromBool(acct.EmailValid));
				paramArray[21] = new SqlParameter("@DefaultPage", Sanitize(USCBase.Truncate(acct.DefaultPage, 49, true)));
				paramArray[22] = new SqlParameter("@PhotoFile", Sanitize(USCBase.Truncate(acct.PhotoFile, 49, true)));
				paramArray[23] = new SqlParameter("@SecurityQuestion", Sanitize(USCBase.Truncate(acct.SecurityQuestion, 49, true)));
				paramArray[24] = new SqlParameter("@SecurityAnswer", Sanitize(USCBase.Truncate(acct.SecurityAnswer, 49, true)));
				paramArray[25] = new SqlParameter("@CreatorID", acct.CreatorID);
				paramArray[26] = new SqlParameter("@CreationUser", Sanitize(USCBase.Truncate(acct.UserName, 49, true)));

				acct.Key = ExecuteSPInsert("sp_AddUserAccount", paramArray);
				if (acct.Key > 0)
				{
					htAccountsList.Add(acct.Key, acct);
					fRet = true;

					if (AddPrefs(acct))
					{
						fRet = true;
						//					if (AddForumUser(acct))
						//					{
						//						fRet = true;
						//					}
					}
				}
			}
			catch( Exception ex )
			{
				string strErr = "AccountsList.AddUser failure";
				short sCat = 0;
				if (IsLocalInstance())
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.AccountAdd, sCat);
				ExceptionText = "sp_AddUserAccount:" + ex.Message;
				fRet = false;
			}


			return fRet;
		}

#endregion

#region Delete User
		public bool Delete(UserAccount acct)
		{
			bool fRet = false;
			acct.IsActive = false;
			acct.Update();
			return fRet;
		}
#endregion

    } //class AccountsList
}