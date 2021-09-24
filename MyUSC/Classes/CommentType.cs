using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyUSC.Classes
{
	public class CommentType
	{
		private long m_lKey;
		private string m_strName;
		private float m_flSequence;
		private string m_strCreator;
		private DateTime m_dtCreateDate;
		private string m_strLastUpdate;

		private void Init()
		{
			m_lKey = 0;
			m_strName = "";
			m_flSequence = 0;
			m_strCreator = "";
			m_dtCreateDate = DateTime.Now;
			m_strLastUpdate = "";
		}

		public CommentType()
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

		public string Name
		{
			get
			{
				return this.m_strName;
			}
			set
			{
				this.m_strName = value;
			}
		}

		public float Sequence
		{
			get
			{
				return this.m_flSequence;
			}
			set
			{
				this.m_flSequence = value;
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

	public sealed class CommentTypeList
	{
		#region Column Constants
		const int colKey = 0;
		const int colName = 1;
		const int colSequence = 2;
		const int colCreator = 3;
		const int colCreateDate = 4;
		const int colLastUpdate = 5;
		#endregion

		private static volatile CommentTypeList instance = null;
		private static object syncRoot = new object();

		const string strSQLGetAllCommentTypes = "SELECT * FROM CommentType";

		private CommentTypeList()
		{
			Init();
		}

		public static CommentTypeList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new CommentTypeList();
					}
				}

				return instance;
			}
		}

		public Hashtable htCommentType;


		private void Init()
		{
			htCommentType = new Hashtable();
			Load();
		}

		public bool Load()
		{
			bool fRet = false;

			htCommentType.Clear();

			// TODO - Load from web config
			string strSQLConn = "Server=localhost;Database=MyUSC;Integrated Security=true";
			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(strSQLConn);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllCommentTypes, sqlConn);
				daLocStrings.Fill(locStrDS, "CommentType");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("CommentType.Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["CommentType"].Rows;
			foreach (DataRow dr in dra)
			{
				CommentType sd = new CommentType();
				fRet = ReadCommentType(dr, sd);
				if (fRet)
				{
					htCommentType.Add(sd.Key, sd);
				}
			}
			fRet = true;

			return fRet;
		}

		private bool ReadCommentType(DataRow dr, CommentType sd)
		{
			bool fRet = true;
			try
			{
				sd.Key = (long)dr.ItemArray[colKey];
				sd.Name = dr.ItemArray[colName].ToString();
				sd.Sequence = (int)dr.ItemArray[colSequence];

				sd.Creator = dr.ItemArray[colCreator].ToString();
				//sd.CreateDate = DateTime.Parse(dr.ItemArray[colCreateDate].ToString());
				sd.LastUpdate = dr.ItemArray[colLastUpdate].ToString();
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("CommentType.ReadCommentType failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		public int Count
		{
			get
			{
				return htCommentType.Count;
			}
		}

		public CommentType GetCommentType(long index)
		{
			return (CommentType)htCommentType[index];
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

		public bool Add(CommentType zip)
		{
			bool fRet = false;
			return fRet;
		}

		public bool Update(CommentType zip)
		{
			bool fRet = false;
			return fRet;
		}

		public bool Delete(CommentType zip)
		{
			bool fRet = false;
			return fRet;
		}

	}
}