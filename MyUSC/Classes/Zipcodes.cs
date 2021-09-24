using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyUSC.Classes
{
	public class Zipcode
	{
		private long m_lKey;
		private string m_strZipcode;
		private string m_strCity;
		private string m_strState;
		private string m_strCreator;
		private DateTime m_dtCreateDate;
		private string m_strLastUpdate;

		private void Init()
		{
			m_lKey = 0;
			m_strZipcode = "";
			m_strCity = "";
			m_strState = "";
			m_strCreator = "";
			m_dtCreateDate = DateTime.Now;
			m_strLastUpdate = "";
		}

		public Zipcode()
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

		public string Value
		{
			get
			{
				return this.m_strZipcode;
			}
			set
			{
				this.m_strZipcode = value;
			}
		}

		public string City
		{
			get
			{
				return this.m_strCity;
			}
			set
			{
				this.m_strCity = value;
			}
		}

		public string State
		{
			get
			{
				return this.m_strState;
			}
			set
			{
				this.m_strState = value;
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

	public sealed class ZipcodeList
	{
		#region Column Constants
		const int colKey = 0;
		const int colZipcode = 1;
		const int colCity = 2;
		const int colState = 3;
		const int colCreator = 4;
		const int colCreateDate = 5;
		const int colLastUpdate = 6;
		#endregion

		private static volatile ZipcodeList instance = null;
		private static object syncRoot = new object();
		private string m_strCnxString;

		const string strSQLGetAllZipcodes = "SELECT * FROM Zipcode";

		private ZipcodeList()
		{
		}

		public static ZipcodeList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new ZipcodeList();
					}
				}

				return instance;
			}
		}

		public Hashtable htZipcode;


		public void Init(string cnxString, bool fForce)
		{
			m_strCnxString = cnxString;
			if (null == htZipcode || fForce)
			{
				htZipcode = new Hashtable();
				Load();
			}
		}

		public bool Load()
		{
			bool fRet = false;

			htZipcode.Clear();

			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(m_strCnxString);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllZipcodes, sqlConn);
				daLocStrings.Fill(locStrDS, "Zipcode");
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Zipcode.Load failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["Zipcode"].Rows;
			foreach (DataRow dr in dra)
			{
				Zipcode zip = new Zipcode();
				fRet = ReadZipcode(dr, zip);
				if (fRet)
				{
					htZipcode.Add(zip.Value, zip);
				}
			}
			fRet = true;

			return fRet;
		}

		private bool ReadZipcode(DataRow dr, Zipcode zip)
		{
			bool fRet = true;
			try
			{
				zip.Key = (long)dr.ItemArray[colKey];
				zip.Value = dr.ItemArray[colZipcode].ToString();
				zip.City = dr.ItemArray[colCity].ToString();
				zip.State = dr.ItemArray[colState].ToString();
				zip.Creator = dr.ItemArray[colCreator].ToString();
				//zip.CreateDate = (DateTime)dr.ItemArray[colCreateDate];
				zip.LastUpdate = dr.ItemArray[colLastUpdate].ToString();
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Zipcode.Read failure", ex, 0);
				fRet = false;
			}
			return fRet;
		}

		public int Count
		{
			get
			{
				return htZipcode.Count;
			}
		}

		public Zipcode GetZipcode(string zip)
		{
			return (Zipcode)htZipcode[zip];
		}

		public string GetZipcode(string city, string state)
		{
			string strRet = "";
			foreach (DictionaryEntry de in htZipcode)
			{
				Zipcode zip = (Zipcode)de.Value;
				if (zip.City == city && zip.State == state)
				{
					strRet = zip.Value;
					// Break since we found it.  Quit searching.
					break;
				}
			}
			return strRet;
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

		public bool Add(Zipcode zip)
		{
			bool fRet = false;
			return fRet;
		}

		public bool Update(Zipcode zip)
		{
			bool fRet = false;
			return fRet;
		}

		public bool Delete(Zipcode zip)
		{
			bool fRet = false;
			return fRet;
		}

	}
}