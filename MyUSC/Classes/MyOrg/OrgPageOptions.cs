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
	public class OrgPageOptions : USCBaseItem
	{
#region Member Variables
		private long m_lOrgID;
		private OrgPageID m_nPageID;
		private bool m_fVisible;
		private OrgAccessTypes m_nAdminLevel;
		private OrgAccessTypes m_nEditLevel;
		private OrgAccessTypes m_nAccessLevel;
		private OrgAccessTypes m_nViewLevel;
#endregion

#region Init
		protected void InitOrgPageOptions()
		{
			m_lOrgID = -1;
			m_nPageID = OrgPageID.Undefined;
			m_fVisible = true;
			m_nAdminLevel = OrgAccessTypes.Undefined;
			m_nEditLevel = OrgAccessTypes.Undefined;
			m_nAccessLevel = OrgAccessTypes.Undefined;
			m_nViewLevel = OrgAccessTypes.Undefined;
		}

		public OrgPageOptions()
		{
			InitOrgPageOptions();
		}
#endregion

#region Accessors
		public long OrgPageOptionID
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

		public OrgPageID PageID
		{
			get
			{
				return this.m_nPageID;
			}
			set
			{
				this.m_nPageID = value;
			}
		}

		public bool Visible
		{
			get
			{
				return this.m_fVisible;
			}
			set
			{
				this.m_fVisible = value;
			}
		}

		public OrgAccessTypes AdminLevel
		{
			get
			{
				return this.m_nAdminLevel;
			}
			set
			{
				this.m_nAdminLevel = value;
			}
		}

		public OrgAccessTypes EditLevel
		{
			get
			{
				return this.m_nEditLevel;
			}
			set
			{
				this.m_nEditLevel = value;
			}
		}

		public OrgAccessTypes AccessLevel
		{
			get
			{
				return this.m_nAccessLevel;
			}
			set
			{
				this.m_nAccessLevel = value;
			}
		}

		public OrgAccessTypes ViewLevel
		{
			get
			{
				return this.m_nViewLevel;
			}
			set
			{
				this.m_nViewLevel = value;
			}
		}

#endregion

#region Update
/*
CREATE PROCEDURE sp_UpdateOrgPageOption
	@OrgID bigint, 
	@PageID int, 
	@Visible bit,
	@AdminLevel int, 
	@EditLevel int, 
	@AccessLevel int, 
	@ViewLevel int, 
	@Update nvarchar(max),
	@Key bigint
*/
		public bool Update(UserAccount acct)
		{
			bool fRet = false;
			string strClean = acct.UserName + " -- " + DateTime.Now.ToString();
			this.LastUpdate = Sanitize(strClean);


			SqlParameter[] paramArray = new SqlParameter[9];
			paramArray[0] = new SqlParameter("@OrgID", OrgID);
			paramArray[1] = new SqlParameter("@PageID", this.PageID);
			paramArray[2] = new SqlParameter("@Visible", SQLBitFromBool(this.Visible));
			paramArray[3] = new SqlParameter("@AdminLevel", this.AdminLevel);
			paramArray[4] = new SqlParameter("@EditLevel", this.EditLevel);
			paramArray[5] = new SqlParameter("@AccessLevel", this.AccessLevel);
			paramArray[6] = new SqlParameter("@ViewLevel", this.ViewLevel);
			paramArray[7] = new SqlParameter("@Update", this.LastUpdate);
			paramArray[8] = new SqlParameter("@Key", Key);

			if (ExecuteSPNoValue("sp_UpdateOrgPageOption", paramArray))
			{
				fRet = true;
			}

			return fRet;
		}
#endregion

	}

	public class OrgPageOptionList : USCBaseList
	{
#region Column Constants
/*
 	PageOptionIdx bigint NOT NULL IDENTITY,
	OrgID bigint NOT NULL,
	PageID int NOT NULL,
	Visible bit NOT NULL,
	AdminLevel int NOT NULL,
	EditLevel int NOT NULL,
	AccessLevel int NOT NULL,
	ViewLevel int NOT NULL,
	CreationUser nvarchar(50),
	CreationDate datetime2(7) DEFAULT(getdate()),
	LastUpdate nvarchar(max),
 */
		const int colKey = 0;
		const int colOrgID = 1;
		const int colPageID = 2;
		const int colVisible = 3;
		const int colAdminLevel = 4;
		const int colEditLevel = 5;
		const int colAccessLevel = 6;
		const int colViewLevel = 7;
		const int colCreator = 8;
		const int colCreateDate = 9;
		const int colLastUpdate = 10;
#endregion

#region Init
		private long m_lOrgID;

		public OrgPageOptionList()
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

		public Hashtable htPageOptions;

		public void Init(string cnxString, long orgID, bool fForce)
		{
			m_strConnectionString = cnxString;
			if (null == htPageOptions || fForce)
			{
				OrgID = orgID;
				htPageOptions = new Hashtable();
				LoadPageOptions();
			}
		}
#endregion

#region Load
		private OrgPageOptions FillPageOption( OrgPageID pageID )
		{
			OrgPageOptions opo = GetOrgPageOption( pageID );
			if( null == opo )
			{
				opo = new OrgPageOptions();
				opo.OrgID = this.OrgID;
				opo.PageID = pageID;
				opo.Visible = true;
				opo.AdminLevel = OrgAccessTypes.Admin;
				opo.EditLevel = OrgAccessTypes.Contributor;
				opo.AccessLevel = OrgAccessTypes.Member;
				opo.ViewLevel = OrgAccessTypes.Follower;

				switch( pageID )
				{
					case OrgPageID.Home:
					{
						opo.ViewLevel = OrgAccessTypes.Guest;
						break;
					}

					case OrgPageID.Roster:
					{
						break;
					}

					case OrgPageID.Schedule:
					{
						break;
					}

					case OrgPageID.MsgBoard:
					{
						opo.ViewLevel = OrgAccessTypes.Member;
						break;
					}

					case OrgPageID.Media:
					{
						break;
					}

					case OrgPageID.Email:
					{
						opo.ViewLevel = OrgAccessTypes.Member;
						break;
					}

					case OrgPageID.Stats:
					{
						opo.Visible = false;
						break;
					}

					case OrgPageID.Venue:
					{
						break;
					}

					case OrgPageID.Manage:
					{
						opo.EditLevel = OrgAccessTypes.Admin;
						opo.AccessLevel = OrgAccessTypes.Admin;
						opo.ViewLevel = OrgAccessTypes.Admin;
						break;
					}

					default:
					{
						break;
					}
				}
				Add(opo);
			}

			return opo;
		}

		public void FillInMissingPageOptions()
		{
			// OrgPageID{ Home = 0, Roster = 1, Schedule = 2, MsgBoard = 3, Media = 4, Email = 5, Stats = 6, Venue = 7, Manage = 100 }
			if( 0 < OrgID )
			{
				FillPageOption(OrgPageID.Home);
				FillPageOption(OrgPageID.Roster);
				FillPageOption(OrgPageID.Schedule);
				FillPageOption(OrgPageID.MsgBoard);
				FillPageOption(OrgPageID.Media);
				FillPageOption(OrgPageID.Email);
				FillPageOption(OrgPageID.Stats);
				FillPageOption(OrgPageID.Venue);
				FillPageOption(OrgPageID.Manage);
			}
		}

		private bool ReadPageOptions(DataRow dr, OrgPageOptions opo)
		{
			#region Column Constants
			const int colKey = 0;
			const int colOrgID = 1;
			const int colPageID = 2;
			const int colVisible = 3;
			const int colAdminLevel = 4;
			const int colEditLevel = 5;
			const int colAccessLevel = 6;
			const int colViewLevel = 7;
			const int colCreator = 8;
			const int colCreateDate = 9;
			const int colLastUpdate = 10;
			#endregion
			bool fRet = true;
			try
			{
				opo.Key = ObjectToLong(dr.ItemArray[colKey]);
				opo.OrgID = ObjectToLong(dr.ItemArray[colOrgID]);
				opo.PageID = (OrgPageID)ObjectToInt(dr.ItemArray[colPageID]);
				opo.Visible = ObjectToBool(dr.ItemArray[colVisible]);
				opo.AdminLevel = (OrgAccessTypes)ObjectToInt(dr.ItemArray[colAdminLevel]);
				opo.EditLevel = (OrgAccessTypes)ObjectToInt(dr.ItemArray[colEditLevel]);
				opo.AccessLevel = (OrgAccessTypes)ObjectToInt(dr.ItemArray[colAccessLevel]);
				opo.ViewLevel = (OrgAccessTypes)ObjectToInt(dr.ItemArray[colViewLevel]);
				opo.Creator = ObjectToString(dr.ItemArray[colCreator]);
				opo.Creator = ObjectToString(dr.ItemArray[colCreator]);
				opo.CreateDate = ObjectToDateTime(dr.ItemArray[colCreateDate]);
				opo.LastUpdate = ObjectToString(dr.ItemArray[colLastUpdate]);

			}
			catch (Exception ex)
			{
				EvtLog.WriteException("ReadPageOptions failure", ex, 0);
				ExceptionText = "ReadPageOptions:" + ex.Message;
				fRet = false;
			}
			return fRet;
		}

		private bool LoadPageOptions()
		{
			bool fRet = false;

			if (null == htPageOptions)
			{
				htPageOptions = new Hashtable();
			}
			else
			{
				htPageOptions.Clear();
			}

			if (0 > OrgID)
			{
				return false;
			}

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				StringBuilder sbSQLQuery = new StringBuilder("SELECT * FROM MyOrgPageOptions WHERE OrgID=");
				sbSQLQuery.Append(this.OrgID);

				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(sbSQLQuery.ToString(), sqlConn);
				daLocStrings.Fill(locStrDS, "MyOrgPageOptions");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("MyOrgPageOptions.LoadPageOptions failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			try
			{
				DataRowCollection dra = locStrDS.Tables["MyOrgPageOptions"].Rows;
				foreach (DataRow dr in dra)
				{
					OrgPageOptions opo = new OrgPageOptions();
					opo.ConnectionString = m_strConnectionString;
					fRet = ReadPageOptions(dr, opo);
					if (fRet)
					{
						htPageOptions.Add(opo.PageID, opo);
					}
				}
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("MyOrgPageOptions.LoadPageOptions read failure", ex, 0);
				return false;
			}

			FillInMissingPageOptions();

			return fRet;
		}

#endregion Load

		public int Count
		{
			get
			{
				return htPageOptions.Count;
			}
		}

		public OrgPageOptions GetOrgPageOption(OrgPageID index)
		{
			return (OrgPageOptions)htPageOptions[index];
		}

/*
CREATE PROCEDURE sp_AddOrgPageOption
	@OrgID bigint, 
	@PageID int, 
	@Visible bit,
	@AdminLevel int, 
	@EditLevel int, 
	@AccessLevel int, 
	@ViewLevel int, 
	@Creator nvarchar(50)
*/
		public bool Add(OrgPageOptions orgPageOption)
		{
			bool fRet = false;

			if( orgPageOption.OrgID < 0 )
			{
				return false;
			}

			SqlParameter[] paramArray = new SqlParameter[8];
			paramArray[0] = new SqlParameter("@OrgID", orgPageOption.OrgID);
			paramArray[1] = new SqlParameter("@PageID", orgPageOption.PageID);
			paramArray[2] = new SqlParameter("@Visible", SQLBitFromBool(orgPageOption.Visible));
			paramArray[3] = new SqlParameter("@AdminLevel", orgPageOption.AdminLevel);
			paramArray[4] = new SqlParameter("@EditLevel", orgPageOption.EditLevel);
			paramArray[5] = new SqlParameter("@AccessLevel", orgPageOption.AccessLevel);
			paramArray[6] = new SqlParameter("@ViewLevel", orgPageOption.ViewLevel);
			paramArray[7] = new SqlParameter("@Creator", Sanitize(USCBase.Truncate(orgPageOption.Creator, 49, true)));

			orgPageOption.Key = ExecuteSPInsert("sp_AddOrgPageOption", paramArray);
			if (orgPageOption.Key > 0)
			{
				htPageOptions.Add(orgPageOption.PageID, orgPageOption);
				fRet = true;
			}

			return fRet;
		}

		public bool Update(OrgPageOptions orgPageOption, UserAccount acct)
		{
			return orgPageOption.Update(acct);
		}

		public bool Delete(OrgPageOptions orgPageOption, UserAccount acct)
		{
			// We don't really want to delete it, just make inactive.
			//orgPageOption.Deleted = false;
			return orgPageOption.Update(acct);
		}

	}

}