using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyUSC.Classes
{
	public class Comments
	{
		private long m_lKey;
		private long m_lCommentTypeKey;
		private long m_lMyAccountKey;
		private string m_strHeading;
		private string m_strCommentText;
		private string m_strCreator;
		private DateTime m_dtCreateDate;
		private string m_strLastUpdate;

		private void Init()
		{
			m_lKey = 0;
			m_lCommentTypeKey = 0;
			m_lMyAccountKey = 0;
			m_strHeading = "";
			m_strCommentText = "";
			m_strCreator = "";
			m_dtCreateDate = DateTime.Now;
			m_strLastUpdate = "";
		}

		public Comments()
		{
			Init();
		}

		#region Accessors
		public long Key
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

		public long CommentTypeKey
		{
			get
			{
				return this.m_lCommentTypeKey;
			}
			set
			{
				this.m_lCommentTypeKey = value;
			}
		}

		public long MyAccountKey
		{
			get
			{
				return this.m_lMyAccountKey;
			}
			set
			{
				this.m_lMyAccountKey = value;
			}
		}

		public string Heading
		{
			get
			{
				return this.m_strHeading;
			}
			set
			{
				this.m_strHeading = value;
			}
		}

		public string CommentText
		{
			get
			{
				return this.m_strCommentText;
			}
			set
			{
				this.m_strCommentText = value;
			}
		}

		public string Creator
		{
			get
			{
				return this.m_strCreator;
			}
			set
			{
				this.m_strCreator = value;
			}
		}

		public DateTime CreateDate
		{
			get
			{
				return this.m_dtCreateDate;
			}
			set
			{
				this.m_dtCreateDate = value;
			}
		}

		public string LastUpdate
		{
			get
			{
				return this.m_strLastUpdate;
			}
			set
			{
				this.m_strLastUpdate = value;
			}
		}
		#endregion
	}

	public sealed class CommentsList
	{
		#region Column Constants
		const int colKey = 0;
		const int colMyAccountKey = 1;
		const int colCommentTypeKey = 2;
		const int colHeading = 3;
		const int colCommentText = 4;
		const int colCreator = 5;
		const int colCreateDate = 6;
		const int colLastUpdate = 7;
		#endregion

		private static volatile CommentsList instance = null;
		private static object syncRoot = new object();

		const string strSQLGetAllCommentss = "SELECT * FROM Comments";

		private CommentsList()
		{
			Init();
		}

		public static CommentsList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new CommentsList();
					}
				}

				return instance;
			}
		}

		public Hashtable htComments;


		private void Init()
		{
			htComments = new Hashtable();
			Load();
		}

		public bool Load()
		{
			bool fRet = false;

			htComments.Clear();

			// TODO - Load from web config
			string strSQLConn = "Server=localhost;Database=MyUSC;Integrated Security=true";
			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(strSQLConn);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllCommentss, sqlConn);
				daLocStrings.Fill(locStrDS, "Comments");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Comments.Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["Comments"].Rows;
			foreach (DataRow dr in dra)
			{
				Comments sd = new Comments();
				fRet = ReadComments(dr, sd);
				if (fRet)
				{
					htComments.Add(sd.Key, sd);
				}
			}
			fRet = true;

			return fRet;
		}

		private bool ReadComments(DataRow dr, Comments sd)
		{
			bool fRet = true;
			try
			{
				sd.Key = (long)dr.ItemArray[colKey];
				sd.CommentTypeKey = (long)dr.ItemArray[colCommentTypeKey];
				sd.MyAccountKey = (long)dr.ItemArray[colMyAccountKey];
				sd.Heading = dr.ItemArray[colHeading].ToString();
				sd.CommentText = dr.ItemArray[colCommentText].ToString();

				sd.Creator = dr.ItemArray[colCreator].ToString();
				//sd.CreateDate = DateTime.Parse(dr.ItemArray[colCreateDate].ToString());
				sd.LastUpdate = dr.ItemArray[colLastUpdate].ToString();
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Comments.ReadComments failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		public int Count
		{
			get
			{
				return htComments.Count;
			}
		}

		public Comments GetComments(long index)
		{
			return (Comments)htComments[index];
		}

		private string Sanitize(string line)
		{
			string val = line;

			int index = line.IndexOf('\'');
			if (-1 != index)
			{
				//val = Regex.Escape( line );
				val = line.Replace("'", "''");
			}

			return val;
		}

		public bool Add(Comments zip)
		{
			bool fRet = false;
			return fRet;
		}

		public bool Update(Comments zip)
		{
			bool fRet = false;
			return fRet;
		}

		public bool Delete(Comments zip)
		{
			bool fRet = false;
			return fRet;
		}

	}
}