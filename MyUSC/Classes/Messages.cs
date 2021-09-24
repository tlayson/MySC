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
	public class Messages : USCBaseItem
	{
#region members
		private long m_lAcctFrom;
		private long m_lAcctTo;
		private long m_lThreadID;
		private string m_strUserName;
		private string m_strDataText1;
		private string m_strDataText2;
		private string m_strDataText3;
		private string m_strDataText4;
		private DateTime m_dtMsgDate;
		private string m_strDiscussionURL;
		private string m_strDiscussionURLText;
		private string m_strPhotoFile;
		private string m_strPhotoUploadURL;
		private bool m_fPrivateMsg;
		private bool m_fThreadParent;
		private string m_strVideoLink;
		private bool m_fNewMessage;
		private bool m_fLikeMessage;
		private bool m_fDislikeMessage;


		private void Init()
		{
			m_lAcctFrom = 0;
			m_lAcctTo = 0;
			m_lThreadID = 0;
			m_strUserName = "";
			m_strDataText1 = "";
			m_strDataText2 = "";
			m_strDataText3 = "";
			m_strDataText4 = "";
			m_dtMsgDate = DateTime.Now;
			m_strDiscussionURL = "";
			m_strDiscussionURLText = "";
			m_strPhotoFile = "";
			m_strPhotoUploadURL = "";
			m_strVideoLink = "";
			m_fPrivateMsg = false;
			m_fThreadParent = false;
			m_fNewMessage = false;
			m_fLikeMessage = false;
			m_fDislikeMessage = false;
		}
#endregion

		const string strSQLMarkMsgAsNew = "UPDATE Messages Set NewMessage={0}, LastUpdateUserTime='{1}' Where MsgID={2}";
		const string strSQLDeleteMsg = "DELETE Messages Where MsgID={0}";

//INSERT INTO Messages (AccountID, FriendID, ThreadID, UserName, DataText1,DataText4,NewMessage,CreationUser, CreationDate,LastUpdateUserTime)
//VALUES (3,91,0,'Eddie Rivera', 'This is the body of the message', 'Msg Title 1',1,'tlayson',GETDATE(),'');

		public Messages()
		{
			Init();
		}

#region SQL
		public void MarkAsNew( bool fNew, string strUserName )
		{
			int nNew = SQLBitFromBool(fNew);
			string userUpdate = strUserName + " -- " + DateTime.Now.ToString();

			string strSQLQuery = String.Format(strSQLMarkMsgAsNew, nNew, Sanitize(userUpdate), Key);
			ExecuteSQL(strSQLQuery);
		}

		public void Delete()
		{
			//TODO: May want to copy to archive
			string strSQLQuery = String.Format(strSQLDeleteMsg, Key);
			ExecuteSQL(strSQLQuery);
		}
#endregion

#region Accessors
		public long AcctFrom
		{
			get
			{
				return this.m_lAcctFrom;
			}
			set
			{
				this.m_lAcctFrom = value;
			}
		}

		public long AcctTo
		{
			get
			{
				return this.m_lAcctTo;
			}
			set
			{
				this.m_lAcctTo = value;
			}
		}

		public long ThreadID
		{
			get
			{
				return this.m_lThreadID;
			}
			set
			{
				this.m_lThreadID = value;
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

		public string DataText1
		{
			get
			{
				return this.m_strDataText1;
			}
			set
			{
				this.m_strDataText1 = value;
			}
		}

		public string DataText2
		{
			get
			{
				return this.m_strDataText2;
			}
			set
			{
				this.m_strDataText2 = value;
			}
		}

		public string DataText3
		{
			get
			{
				return this.m_strDataText3;
			}
			set
			{
				this.m_strDataText3 = value;
			}
		}

		public string DataText4
		{
			get
			{
				return this.m_strDataText4;
			}
			set
			{
				this.m_strDataText4 = value;
			}
		}

		public DateTime MsgDate
		{
			get
			{
				return this.m_dtMsgDate;
			}
			set
			{
				this.m_dtMsgDate = value;
			}
		}

		public string DiscussionURL
		{
			get
			{
				return this.m_strDiscussionURL;
			}
			set
			{
				this.m_strDiscussionURL = value;
			}
		}

		public string DiscussionURLText
		{
			get
			{
				return this.m_strDiscussionURLText;
			}
			set
			{
				this.m_strDiscussionURLText = value;
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

		public string PhotoUploadURL
		{
			get
			{
				return this.m_strPhotoUploadURL;
			}
			set
			{
				this.m_strPhotoUploadURL = value;
			}
		}

		public string VideoLink
		{
			get
			{
				return this.m_strVideoLink;
			}
			set
			{
				this.m_strVideoLink = value;
			}
		}

		public bool PrivateMsg
		{
			get
			{
				return this.m_fPrivateMsg;
			}
			set
			{
				this.m_fPrivateMsg = value;
			}
		}

		public bool ThreadParent
		{
			get
			{
				return this.m_fThreadParent;
			}
			set
			{
				this.m_fThreadParent = value;
			}
		}

		public bool NewMessage
		{
			get
			{
				return this.m_fNewMessage;
			}
			set
			{
				this.m_fNewMessage = value;
			}
		}

		public bool LikeMessage
		{
			get
			{
				return this.m_fLikeMessage;
			}
			set
			{
				this.m_fLikeMessage = value;
			}
		}

		public bool DislikeMessage
		{
			get
			{
				return this.m_fDislikeMessage;
			}
			set
			{
				this.m_fDislikeMessage = value;
			}
		}
#endregion
	}

	public sealed class MessagesList : USCBaseList
	{
#region Column Constants
		const int colKey = 0;
		const int colAcctFromKey = 1;
		const int colAcctToKey = 2;
		const int colThreadID = 3;
		const int colUserName = 4;
		const int colDataText1 = 5;
		const int colDataText2 = 6;
		const int colDataText3 = 7;
		const int colDataText4 = 8;
		const int colMsgDate = 9;
		const int colDiscussionURL = 10;
		const int colDiscussionURLTxt = 11;
		const int colPhotoFile = 12;
		const int colPhotoUploadURL = 13;
		const int colPrivateMsg = 14;
		const int colThreadParent = 15;
		const int colVideoLink = 16;
		const int colNewMessage = 17;
		const int colLikeMessage = 18;
		const int colDislikeMessage = 19;
		const int colCreator = 20;
		const int colCreateDate = 21;
		const int colLastUpdate = 22;
#endregion

		private static volatile MessagesList instance = null;
		private static object syncRoot = new object();

		const string strSQLGetAllMessagess = "SELECT * FROM Messages";

		private MessagesList()
		{
		}

		public static MessagesList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new MessagesList();
					}
				}

				return instance;
			}
		}

		public Hashtable htMessages;


		public void Init(string cnxString, bool fForce)
		{
			m_strConnectionString = cnxString;
			if (null == htMessages || fForce)
			{
				htMessages = new Hashtable();
				Load();
			}
		}

		private void Init()
		{
			htMessages = new Hashtable();
			Load();
		}

		public bool Load()
		{
			bool fRet = false;

			htMessages.Clear();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllMessagess, sqlConn);
				daLocStrings.Fill(locStrDS, "Messages");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Messages.Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["Messages"].Rows;
			foreach (DataRow dr in dra)
			{
				Messages msg = new Messages();
				fRet = ReadMessages(dr, msg);
				if (fRet)
				{
					msg.ConnectionString = m_strConnectionString;
					htMessages.Add(msg.Key, msg);
				}
			}
			fRet = true;

			return fRet;
		}

		private bool ReadMessages(DataRow dr, Messages msg)
		{
			bool fRet = true;
			try
			{
				msg.Key = (long)dr.ItemArray[colKey];
				msg.AcctFrom = (long)dr.ItemArray[colAcctFromKey];
				msg.AcctTo = (long)dr.ItemArray[colAcctToKey];
				msg.ThreadID = (long)dr.ItemArray[colThreadID];
				msg.UserName = dr.ItemArray[colUserName].ToString();
				msg.DataText1 = dr.ItemArray[colDataText1].ToString();
				msg.DataText2 = dr.ItemArray[colDataText2].ToString();
				msg.DataText3 = dr.ItemArray[colDataText3].ToString();
				msg.DataText4 = dr.ItemArray[colDataText4].ToString();
				msg.MsgDate = DateTime.Parse(dr.ItemArray[colMsgDate].ToString());
				msg.DiscussionURL = dr.ItemArray[colDiscussionURL].ToString();
				msg.DiscussionURLText = dr.ItemArray[colDiscussionURLTxt].ToString();
				msg.PhotoFile = dr.ItemArray[colPhotoFile].ToString();
				msg.PhotoUploadURL = dr.ItemArray[colPhotoUploadURL].ToString();
				
				string strTemp = dr.ItemArray[colPrivateMsg].ToString();
				bool fTmp = false;
				if( bool.TryParse(strTemp, out fTmp ) )
				{
					msg.PrivateMsg = fTmp;
				}
				else
				{
					msg.PrivateMsg = false;
				}
				
				strTemp = dr.ItemArray[colThreadParent].ToString();
				if (bool.TryParse(strTemp, out fTmp))
				{
					msg.ThreadParent = fTmp;
				}
				else
				{
					msg.ThreadParent = false;
				}
				msg.VideoLink = dr.ItemArray[colVideoLink].ToString();
				
				strTemp = dr.ItemArray[colNewMessage].ToString();
				if (bool.TryParse(strTemp, out fTmp))
				{
					msg.NewMessage = fTmp;
				}
				else
				{
					msg.NewMessage = false;
				}
				
				strTemp = dr.ItemArray[colLikeMessage].ToString();
				if (bool.TryParse(strTemp, out fTmp))
				{
					msg.LikeMessage = fTmp;
				}
				else
				{
					msg.LikeMessage = false;
				}
				
				strTemp = dr.ItemArray[colDislikeMessage].ToString();
				if (bool.TryParse(strTemp, out fTmp))
				{
					msg.DislikeMessage = fTmp;
				}
				else
				{
					msg.DislikeMessage = false;
				}

				msg.Creator = dr.ItemArray[colCreator].ToString();
				msg.CreateDate = DateTime.Parse(dr.ItemArray[colCreateDate].ToString());
				msg.LastUpdate = dr.ItemArray[colLastUpdate].ToString();
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Messages.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		public Messages GetMessageByID( long lKey )
		{
			Messages msg = (Messages)htMessages[lKey];
			return msg;
		}

		public int Count
		{
			get
			{
				return htMessages.Count;
			}
		}

#region FindUserMessages
		public void FindUserMessageCounts(long lUserID, out int nTotalCount, out int nNewCount )
		{
			nTotalCount = 0;
			nNewCount = 0;

			foreach (DictionaryEntry de in htMessages)
			{
				Messages msg = (Messages)de.Value;
				if (msg.AcctTo == lUserID)
				{
					nTotalCount++;
					if( msg.NewMessage )
					{
						nNewCount++;
					}
				}
			}
		}

		public int FindUserMessages(long lUserID, out Hashtable htResult)
		{
			htResult = new Hashtable();
			htResult.Clear();

			foreach (DictionaryEntry de in htMessages)
			{
				Messages msg = (Messages)de.Value;
				if( msg.AcctTo == lUserID )
				{
					htResult.Add(msg.Key, msg);
				}
			}

			return htResult.Count;
		}

		public int FindMessagesSent(long lUserID, out Hashtable htResult)
		{
			htResult = new Hashtable();
			htResult.Clear();

			foreach (DictionaryEntry de in htMessages)
			{
				Messages msg = (Messages)de.Value;
				if (msg.AcctFrom == lUserID)
				{
					htResult.Add(msg.Key, msg);
				}
			}

			return htResult.Count;
		}
#endregion

#region Add new message
		private string BuildAddMsgString(Messages msg)
		{
			const string strSQLAddUser = "INSERT INTO Messages (AccountID,FriendID,ThreadID,UserName,DataText1,DataText2,DataText3,DataText4,MessageDate,DiscussionURL,DiscussionURLText,PhotoURL1,PhotoUploadURL,PrivateMsg,ThreadParent,VideoLink,NewMessage,LikeMessage,DislikeMessage,CreationUser,CreationDate,LastUpdateUserTime) VALUES (";

			StringBuilder sbSQLQuery = new StringBuilder(strSQLAddUser);
			sbSQLQuery.Append(msg.AcctFrom);
			sbSQLQuery.Append(",").Append(msg.AcctTo);
			sbSQLQuery.Append(",").Append(msg.ThreadID);

			string strClean = Sanitize( USCBase.Truncate(msg.UserName, 99, true ) );
			sbSQLQuery.Append(",'").Append(strClean).Append("'");

			strClean = Sanitize(USCBase.Truncate(msg.DataText1, 2999, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");
			strClean = Sanitize(USCBase.Truncate(msg.DataText2, 49, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");
			strClean = Sanitize(USCBase.Truncate(msg.DataText3, 49, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");
			strClean = Sanitize(USCBase.Truncate(msg.DataText4, 49, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");

			sbSQLQuery.Append(",").Append("GETDATE()");

			strClean = Sanitize(USCBase.Truncate(msg.DiscussionURL, 499, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");
			strClean = Sanitize(USCBase.Truncate(msg.DiscussionURLText, 499, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");

			strClean = Sanitize(USCBase.Truncate(msg.PhotoFile, 499, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");
			strClean = Sanitize(USCBase.Truncate(msg.PhotoUploadURL, 499, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");

			sbSQLQuery.Append(",").Append(SQLBitFromBool(msg.PrivateMsg));
			sbSQLQuery.Append(",").Append(SQLBitFromBool(msg.ThreadParent));

			strClean = Sanitize(USCBase.Truncate(msg.VideoLink, 499, true));
			sbSQLQuery.Append(",'").Append(strClean).Append("'");

			sbSQLQuery.Append(",").Append(1);  // New message
			sbSQLQuery.Append(",").Append(0);  // Like
			sbSQLQuery.Append(",").Append(0);  // Dislike

			strClean = Sanitize(USCBase.Truncate(msg.UserName, 49, true));  //Creator
			sbSQLQuery.Append(",'").Append(strClean).Append("'");

			sbSQLQuery.Append(",").Append("GETDATE()");

			string strLastUpdate = msg.UserName + " -- " + DateTime.Now.ToString();  //Last update
			sbSQLQuery.Append(",'").Append(Sanitize(strLastUpdate)).Append("'");
			sbSQLQuery.Append(")");
			return sbSQLQuery.ToString();
		}

		public bool Add(Messages msg)
		{
			bool fRet = false;
			string strSQL = BuildAddMsgString( msg );
			msg.Key = ExecuteSQLInsert( strSQL );
			if (0 < msg.Key)
			{
				htMessages.Add(msg.Key, msg);
				fRet = true;
			}
			return fRet;
		}
#endregion

		public bool Update(Messages msg)
		{
			bool fRet = false;
			return fRet;
		}

		public bool Delete(Messages msg)
		{
			bool fRet = false;
			return fRet;
		}

	}
}