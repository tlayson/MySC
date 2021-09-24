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
	public class MyFriend : USCBaseItem
	{
		private long m_lUserAccountID;
		private long m_lFriendAcctID;
		private bool m_fIsActive;

		private void Init()
		{
			m_lUserAccountID = 0;
			m_lFriendAcctID = 0;
			m_fIsActive = false;
		}

		public MyFriend()
		{
			Init();
		}

#region Accessors
		public long UserAccountID
		{
			get
			{
				return this.m_lUserAccountID;
			}
			set
			{
				this.m_lUserAccountID = value;
			}
		}

		public long FriendID
		{
			get
			{
				return this.m_lFriendAcctID;
			}
			set
			{
				this.m_lFriendAcctID = value;
			}
		}

		public bool IsActive
		{
			get
			{
				return this.m_fIsActive;
			}
			set
			{
				this.m_fIsActive = value;
			}
		}

#endregion
	}

	public sealed class FriendsList : USCBaseList
	{
		#region Column Constants
		const int colKey = 0;
		const int colMyAccountKey = 1;
		const int colMyFriendsFKey = 2;
		const int colstrActive = 3;
		const int colCreator = 4;
		const int colCreateDate = 5;
		const int colLastUpdate = 6;
		#endregion

		private static volatile FriendsList instance = null;
		private static object syncRoot = new object();

		const string strSQLGetAllMyFriends = "SELECT * FROM Friends";
		const string strSQLAddFriend = "INSERT INTO Friends(AccountID,FriendID,IsActive,CreationUser,CreationDate,LastUpdateUserTime) VALUES (";
		//const string strSQLDeleteFriend = "DELETE FROM Friends WHERE FriendReqID={0} OR ";

		private FriendsList()
		{
		}

		public static FriendsList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new FriendsList();
					}
				}

				return instance;
			}
		}

		public Hashtable htMyFriends;

		public void Init(string cnxString, bool fForce)
		{
			m_strConnectionString = cnxString;
			if (null == htMyFriends || fForce)
			{
				htMyFriends = new Hashtable();
				Load();
			}
		}


		public bool Load()
		{
			bool fRet = false;

			htMyFriends.Clear();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllMyFriends, sqlConn);
				daLocStrings.Fill(locStrDS, "Friends");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Friends.Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["Friends"].Rows;
			foreach (DataRow dr in dra)
			{
				MyFriend sd = new MyFriend();
				fRet = ReadMyFriends(dr, sd);
				if (fRet)
				{
					htMyFriends.Add(sd.Key, sd);
				}
			}
			fRet = true;

			return fRet;
		}

		private bool ReadMyFriends(DataRow dr, MyFriend sd)
		{
			bool fRet = true;
			try
			{
				sd.Key = (long)dr.ItemArray[colKey];
				sd.UserAccountID = (long)dr.ItemArray[colMyAccountKey];
				sd.FriendID = (long)dr.ItemArray[colMyFriendsFKey];
				sd.IsActive = bool.Parse(dr.ItemArray[colstrActive].ToString());

				sd.Creator = dr.ItemArray[colCreator].ToString();
				//sd.CreateDate = DateTime.Parse(dr.ItemArray[colCreateDate].ToString());
				sd.LastUpdate = dr.ItemArray[colLastUpdate].ToString();
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Friends.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		public int Count
		{
			get
			{
				return htMyFriends.Count;
			}
		}

		public int FindUserFriends(long lUserID, out Hashtable htResult)
		{
			htResult = new Hashtable();
			htResult.Clear();

			foreach (DictionaryEntry de in htMyFriends)
			{
				MyFriend friend = (MyFriend)de.Value;
				if (friend.UserAccountID == lUserID)
				{
					htResult.Add(friend.Key, friend);
				}
			}

			return htResult.Count;
		}

		public bool AddFromRequest(FriendRequest fr)
		{
			bool fRet = false;

			// Build friend from the request
			MyFriend friend1 = new MyFriend();
			friend1.UserAccountID = fr.AccountID;
			friend1.FriendID = fr.FriendID;
			friend1.IsActive = true;

			// Build the reverse so friendship show for both users.
			MyFriend friend2 = new MyFriend();
			friend2.UserAccountID = fr.FriendID;
			friend2.FriendID = fr.AccountID;
			friend2.IsActive = true;

			if( Add( friend1 ) )
			{
				fRet = Add( friend2 );
			}
			
			return fRet;
		}

		private string BuildAddString( MyFriend friend )
		{
			StringBuilder sbSQLQuery = new StringBuilder(strSQLAddFriend);
			sbSQLQuery.Append(friend.UserAccountID);
			sbSQLQuery.Append(",").Append(friend.FriendID);
			sbSQLQuery.Append(",").Append(1);
			// Creator Etc
			string strUser = "system";
			string strClean = Sanitize(USCBase.Truncate(strUser, 49, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");
			sbSQLQuery.Append(",").Append("GETDATE()");

			strClean = strUser + " -- " + DateTime.Now.ToString();
			sbSQLQuery.Append(",'").Append(Sanitize(strClean)).Append("'");
			sbSQLQuery.Append(")");
			return sbSQLQuery.ToString();
		}

		public bool Add(MyFriend friend)
		{
			bool fRet = false;
			SqlConnection sqlConn = null;

			string strSQLQuery = BuildAddString( friend );
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
				friend.Key = insertID;

				if (0 < insertID)
				{
					htMyFriends.Add(friend.Key, friend);
					fRet = true;
				}
				sqlConn.Close();
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Friends.Add failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}
			return fRet;
		}

		public bool Update(MyFriend friend)
		{
			bool fRet = false;
			return fRet;
		}

		public bool Delete(MyFriend friend)
		{
			bool fRet = false;
			return fRet;
		}

	}
}