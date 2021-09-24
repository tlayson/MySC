using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyUSC.Classes
{
	public class AdRotatorAdvertiser
	{
		private long m_lKey;
		private string m_stAltText;
		private string m_strImgUrl;
		private string m_strNavUrl;
		private string m_strRotatorName;
		private int m_nLanguage;
		private string m_strCreator;
		private DateTime m_dtCreateDate;
		private string m_strLastUpdate;

		private void Init()
		{
			m_lKey = 0;
			m_stAltText = "";
			m_strImgUrl = "";
			m_strNavUrl = "";
			m_strRotatorName = "";
			m_nLanguage = 1;
			m_strCreator = "";
			m_dtCreateDate = DateTime.Now;
			m_strLastUpdate = "";
		}

		public AdRotatorAdvertiser()
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

		public string AltText
		{
			get
			{
				return this.m_stAltText;
			}
			set
			{
				this.m_stAltText = value;
			}
		}

		public string ImgUrl
		{
			get
			{
				return this.m_strImgUrl;
			}
			set
			{
				this.m_strImgUrl = value;
			}
		}

		public string NavUrl
		{
			get
			{
				return this.m_strNavUrl;
			}
			set
			{
				this.m_strNavUrl = value;
			}
		}

		public string RotatorName
		{
			get
			{
				return this.m_strRotatorName;
			}
			set
			{
				this.m_strRotatorName = value;
			}
		}

		public int Language
		{
			get
			{
				return this.m_nLanguage;
			}
			set
			{
				this.m_nLanguage = value;
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

	public sealed class AdRotatorList
	{
		#region Column Constants
		const int colKey = 0;
		const int colAltText = 1;
		const int colImgUrl = 2;
		const int colNavUrl = 3;
		const int colRotatorName = 4;
		const int colLanguage = 5;
		const int colCreator = 6;
		const int colCreateDate = 7;
		const int colLastUpdate = 8;
		#endregion

		private static volatile AdRotatorList instance = null;
		private static object syncRoot = new object();

		const string strSQLGetAllZipcodes = "SELECT * FROM AdRotator";

		private AdRotatorList()
		{
			Init();
		}

		public static AdRotatorList Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new AdRotatorList();
					}
				}

				return instance;
			}
		}

		public Hashtable htAdRotatorList;


		private void Init()
		{
			htAdRotatorList = new Hashtable();
			Load();
		}

		public bool Load()
		{
			bool fRet = false;

			htAdRotatorList.Clear();

			// TODO - Load from web config
			string strSQLConn = "Server=localhost;Database=MyUSC;Integrated Security=true";
			SqlConnection sqlConn = null;
			DataSet locStrDS = new DataSet();

			try
			{
				sqlConn = new SqlConnection(strSQLConn);
				sqlConn.Open();
				SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllZipcodes, sqlConn);
				daLocStrings.Fill(locStrDS, "AdRotator");
			}
			catch (Exception ex)
			{
				string str = ex.Message;
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			DataRowCollection dra = locStrDS.Tables["AdRotator"].Rows;
			foreach (DataRow dr in dra)
			{
				AdRotatorAdvertiser ad = new AdRotatorAdvertiser();
				fRet = ReadAdvertiser(dr, ad);
				if (fRet)
				{
					htAdRotatorList.Add(ad.Key, ad);
				}
			}
			fRet = true;

			return fRet;
		}

		private bool ReadAdvertiser(DataRow dr, AdRotatorAdvertiser ad)
		{
			bool fRet = true;
			try
			{
				ad.Key = (long)dr.ItemArray[colKey];
				ad.AltText = dr.ItemArray[colAltText].ToString();
				ad.ImgUrl = dr.ItemArray[colImgUrl].ToString();
				ad.NavUrl = dr.ItemArray[colNavUrl].ToString();
				ad.RotatorName = dr.ItemArray[colRotatorName].ToString();
				ad.Language = (int)dr.ItemArray[colLanguage];
				ad.Creator = dr.ItemArray[colCreator].ToString();
				//ad.CreateDate = (DateTime)dr.ItemArray[colCreateDate];s
				ad.LastUpdate = dr.ItemArray[colLastUpdate].ToString();
			}
			catch (Exception ex)
			{
				string str = ex.Message;
				fRet = false;
			}
			return fRet;
		}

		public int Count
		{
			get
			{
				return htAdRotatorList.Count;
			}
		}

		public Zipcode GetAdvertiser(long index)
		{
			return (Zipcode)htAdRotatorList[index];
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