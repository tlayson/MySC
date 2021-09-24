using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace MyUSC.Classes
{
	public class FriendRequest : USCBaseItem
	{
		private long m_lAccountID;
		private long m_lFriendID;
		private DateTime m_dtRequestDate;
		private string m_strComments;

		private void Init()
		{
			m_lAccountID = 0;
			m_lFriendID = 0;
			m_dtRequestDate = DateTime.Now;
			m_strComments = "";
		}

		public FriendRequest()
		{
			Init();
		}

#region Accessors
		public long AccountID
		{
			get
			{
				return this.m_lAccountID;
			}
			set
			{
				this.m_lAccountID = value;
			}
		}

		public long FriendID
		{
			get
			{
				return this.m_lFriendID;
			}
			set
			{
				this.m_lFriendID = value;
			}
		}

		public DateTime RequestDate
		{
			get
			{
				return this.m_dtRequestDate;
			}
			set
			{
				this.m_dtRequestDate = value;
			}
		}

		public string Comments
		{
			get
			{
				return this.m_strComments;
			}
			set
			{
				this.m_strComments = value;
			}
		}

#endregion
	}

	public sealed class FriendRequestList : USCBaseList
	{
		#region Column Constants
		const int colKey = 0;
		const int colAccountID = 1;
		const int colFriendID = 2;
		const int colRequestDate = 3;
		const int colComments = 4;
		const int colCreator = 5;
		const int colCreateDate = 6;
		const int colLastUpdate = 7;
		#endregion

		private static volatile FriendRequestList instance = null;
		private static object syncRoot = new object();

		const string strSQLGetAllFR = "SELECT * FROM FriendsRequest";
		const string strSQLAddFR = "INSERT INTO FriendsRequest(AccountID,FriendID,RequestDate,Comments,CreationUser,CreationDate,LastUpdateUserTime) VALUES (";
		const string strSQLDeleteFR = "DELETE FROM FriendsRequest WHERE FriendReqID=";

		private FriendRequestList()
		{
		}

		public static FriendRequestList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new FriendRequestList();
					}
				}

				return instance;
			}
		}

		public Hashtable htFriendRequest;

		public void Init(string cnxString, bool fForce)
		{
			m_strConnectionString = cnxString;
			if (null == htFriendRequest || fForce)
			{
				htFriendRequest = new Hashtable();
				Load();
			}
		}

		public bool Load()
		{
			bool fRet = false;

			htFriendRequest.Clear();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllFR, sqlConn);
				daLocStrings.Fill(locStrDS, "FriendsRequest");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("FriendRequest:Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["FriendsRequest"].Rows;
			foreach (DataRow dr in dra)
			{
				FriendRequest sd = new FriendRequest();
				fRet = ReadFriendRequest(dr, sd);
				if (fRet)
				{
					htFriendRequest.Add(sd.Key, sd);
				}
			}
			fRet = true;

			return fRet;
		}

		private bool ReadFriendRequest(DataRow dr, FriendRequest fr)
		{
			bool fRet = true;
			try
			{
				fr.Key = (long)dr.ItemArray[colKey];

				fr.AccountID = (long)dr.ItemArray[colAccountID];
				fr.FriendID = (long)dr.ItemArray[colFriendID];
				fr.RequestDate = DateTime.Parse(dr.ItemArray[colCreateDate].ToString());
				fr.Comments = dr.ItemArray[colComments].ToString();

				fr.Creator = dr.ItemArray[colCreator].ToString();
				//sd.CreateDate = DateTime.Parse(dr.ItemArray[colCreateDate].ToString());
				fr.LastUpdate = dr.ItemArray[colLastUpdate].ToString();
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("FriendRequest:Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		public int Count
		{
			get
			{
				return htFriendRequest.Count;
			}
		}

		public int FindUserFriendRequests(long lUserID, out Hashtable htResult)
		{
			htResult = new Hashtable();
			htResult.Clear();

			foreach (DictionaryEntry de in htFriendRequest)
			{
				FriendRequest fr = (FriendRequest)de.Value;
				if (fr.FriendID == lUserID)
				{
					htResult.Add(fr.Key, fr);
				}
			}
			return htResult.Count;
		}

		//const string strSQLAddFR = "INSERT INTO FriendsRequest(AccountID,FriendID,RequestDate,Comments,CreationUser,CreationDate,LastUpdateUserTime) VALUES (";
		private string BuildAddFRString(FriendRequest friendReq, string strUser)
		{
			
			StringBuilder sbSQLQuery = new StringBuilder(strSQLAddFR);
			sbSQLQuery.Append(friendReq.AccountID);
			sbSQLQuery.Append(",").Append(friendReq.FriendID);
			sbSQLQuery.Append(",").Append("GETDATE()");

			string strClean = Sanitize(USCBase.Truncate(friendReq.Comments, 199, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");

			// Creator Etc
			strClean = Sanitize(USCBase.Truncate(strUser, 49, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");
			sbSQLQuery.Append(",").Append("GETDATE()");

			strClean = strUser + " -- " + DateTime.Now.ToString();
			sbSQLQuery.Append(",'").Append(Sanitize(strClean)).Append("'");
			sbSQLQuery.Append(")");
			return sbSQLQuery.ToString();
		}

		public bool Add(FriendRequest fr, string strUser)
		{
			bool fRet = false;
			SqlConnection sqlConn = null;

			string strSQLQuery = BuildAddFRString( fr, strUser );
			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlCommand sqlCommand = new SqlCommand(strSQLQuery, sqlConn);
				sqlCommand.ExecuteNonQuery();

				// Create another Command to get IDENTITY Value
				sqlCommand.Parameters.Clear();
				sqlCommand.CommandText = "SELECT @@IDENTITY";
				// Get the last inserted id.
				int insertID = Convert.ToInt32(sqlCommand.ExecuteScalar());
				fr.Key = insertID;

				if (0 < insertID)
				{
					htFriendRequest.Add(fr.Key, fr);
					fRet = true;
				}
				sqlConn.Close();
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("FriendRequest:Add failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}
			return fRet;
		}

		public bool Accept( long frID )
		{
			bool fRet = false;
			return fRet;
		}

		public bool Decline( long frID )
		{
			bool fRet = false;
			fRet = Delete(frID);
			return fRet;
		}

		public bool Delete(long frID)
		{
			bool fRet = false;
			string strSQL = strSQLDeleteFR + frID.ToString();
			fRet = ExecuteSQL( strSQL );
			if( fRet )
			{
				htFriendRequest.Remove(frID);
			}
			return fRet;
		}

	}
}