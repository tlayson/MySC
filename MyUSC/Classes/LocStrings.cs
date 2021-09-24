using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyUSC.Classes
{
	public class LocString
	{
		private long m_Key;
		private string m_Parent;
		private int m_Language;
		private string m_ObjectName;
		private string m_Value;
		private float m_Sequence;
		private bool m_Editable;
		private bool m_Deletable;
		private string m_Creator;
		private DateTime m_CreateDate;
		private string m_LastUpdate;

		private void Init()
		{
			m_Key = 0;
			m_Parent = "";
			m_Language = 0;
			m_ObjectName = "";
			m_Value = "";
			m_Sequence = 0;
			m_Editable = false;
			m_Deletable = false;
			m_Creator = "";
			m_CreateDate = DateTime.Now;
			m_LastUpdate = "";
		}

		public LocString()
		{
			Init();
		}

#region Accessors
		public long Key
		{
			get
			{
				return this.m_Key;
			}
			set
			{
				this.m_Key = value;
			}
		}

		public string Parent
		{
			get
			{
				return this.m_Parent;
			}
			set
			{
				this.m_Parent = value;
			}
		}

		public int Language
		{
			get
			{
				return this.m_Language;
			}
			set
			{
				this.m_Language = value;
			}
		}

		public string ObjectName
		{
			get
			{
				return this.m_ObjectName;
			}
			set
			{
				this.m_ObjectName = value;
			}
		}

		public string Value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		public float Sequence
		{
			get
			{
				return this.m_Sequence;
			}
			set
			{
				this.m_Sequence = value;
			}
		}

		public bool Editable
		{
			get
			{
				return this.m_Editable;
			}
			set
			{
				this.m_Editable = value;
			}
		}

		public bool Deletable
		{
			get
			{
				return this.m_Deletable;
			}
			set
			{
				this.m_Deletable = value;
			}
		}

		public string Creator
		{
			get
			{
				return this.m_Creator;
			}
			set
			{
				this.m_Creator = value;
			}
		}

		public DateTime CreateDate
		{
			get
			{
				return this.m_CreateDate;
			}
			set
			{
				this.m_CreateDate = value;
			}
		}

		public string LastUpdate
		{
			get
			{
				return this.m_LastUpdate;
			}
			set
			{
				this.m_LastUpdate = value;
			}
		}


#endregion
	}

	public sealed class LocStrings : USCBaseList
	{
		#region Column Constants
		const int colKey = 0;
		const int colLocID = 1;
		const int colPage = 2;
		const int colLanguage = 3;
		const int colObjectName = 4;
		const int colText = 5;
		const int colSequence = 6;
		const int colEditable = 7;
		const int colDeletable = 8;
		const int colCreator = 0;
		const int colCreateDate = 10;
		const int colLastUpdate = 11;
		#endregion

		private static volatile LocStrings instance = null;
		private static object syncRoot = new object();

		const string strSQLGetAllStrings = "SELECT * FROM Localization";

		private LocStrings()
		{
		}

		public static LocStrings Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new LocStrings();
					}
				}

				return instance;
			}
		}

		public Hashtable htLocalizationStrings;
		public Hashtable htOffensiveTerms;


		public void Init(string cnxString, bool fForce )
		{
			m_strConnectionString = cnxString;
			if (null == htLocalizationStrings || fForce)
			{
				htLocalizationStrings = new Hashtable();
				htOffensiveTerms = new Hashtable();
				Load();
			}
		}

		public bool Load()
		{
			bool fRet = false;

			htLocalizationStrings.Clear();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllStrings, sqlConn);
				daLocStrings.Fill(locStrDS, "Localization");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("LocStrings.Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["Localization"].Rows;
			foreach (DataRow dr in dra)
			{
				LocString ls = new LocString();
				fRet = ReadString(dr, ls);
				if (fRet)
				{
					if ("OffensiveLanguage" == ls.Parent)
					{
						htOffensiveTerms.Add(ls.Key, ls);
					}
					else
					{
						htLocalizationStrings.Add(ls.Key, ls);
					}
				}
			}
			fRet = true;

			return fRet;
		}

		private bool ReadString(DataRow dr, LocString ls)
		{
			bool fRet = true;
			try
			{
				ls.Key = (long)dr.ItemArray[colLocID];
				ls.Parent = dr.ItemArray[colPage].ToString();
				ls.Language = (int)dr.ItemArray[colLanguage];
				ls.ObjectName = dr.ItemArray[colObjectName].ToString();
				ls.Value = dr.ItemArray[colText].ToString();

				Object obj = dr.ItemArray[colSequence];
				if( null != obj )
				{
					ls.Sequence = float.Parse(obj.ToString());
				}
				obj = dr.ItemArray[colEditable];
				if (null != obj)
				{
					ls.Editable = bool.Parse(obj.ToString());
				}
				obj = dr.ItemArray[colDeletable];
				if (null != obj)
				{
					ls.Deletable = bool.Parse(dr.ItemArray[colDeletable].ToString());
				}
				ls.Creator = dr.ItemArray[colCreator].ToString();
				//ls.CreateDate = (DateTime)dr.ItemArray[colCreateDate];
				ls.LastUpdate = dr.ItemArray[colLastUpdate].ToString();
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("LocStrings.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		public int Count
		{
			get
			{
				return htLocalizationStrings.Count;
			}
		}

		public string GetValue( long index )
		{
			string strRet = "";
			LocString ls = (LocString)htLocalizationStrings[index];
			if( null != ls )
			{
				strRet = ls.Value;
			}

			return strRet;
		}

		public string GetValue(string parent, int lang, string objName)
		{
			string strRet = "";
			foreach (DictionaryEntry de in htLocalizationStrings)
			{
				LocString ls = (LocString)de.Value;
				if (ls.Language == lang && ls.Parent == parent && ls.ObjectName == objName)
				{
					strRet = ls.Value;
					// Break since we found it.  Quit searching.
					break;
				}
			}
			return strRet;
		}

		/*************************************************************************************************
		 * public bool IsOffensiveTerm( int lang, string term )
		 * Determine if the term is in the list of offensive terms for the given language
		*************************************************************************************************/
		public bool IsOffensiveTerm( int lang, string term )
		{
			bool bRet = false;
			foreach (DictionaryEntry de in htOffensiveTerms)
			{
				LocString ls = (LocString)de.Value;
				// Do the language and term match?
				if ( ls.Language == lang && ls.Value == term )
				{
					bRet = true;
					// Break since we found it.  Quit searching.
					break;
				}
				else if(ls.Language > lang)
				{
					// We've reached the next language.  Quit searching.
					break;
				}
			}
			return bRet;
		}

		public LocString this[long index]
		{
			get
			{
				// This indexer is very simple, and just returns or sets 
				// the corresponding element from the internal array. 
				return (LocString)htLocalizationStrings[index];
			}
		}

		public bool Add(LocString locString)
		{
			bool fRet = false;
			return fRet;
		}

		public bool Update(LocString locString)
		{
			bool fRet = false;
			return fRet;
		}

		public bool Delete(LocString locString)
		{
			bool fRet = false;
			return fRet;
		}

	}
}