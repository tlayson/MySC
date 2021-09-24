using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Xml.Linq;

namespace MyUSC.Classes
{
	public class USCBase
	{
		protected const int langENUS = 1;

#region Member Variables
		protected static ZipcodeList m_zipList;
		protected string m_strConnectionString;
		protected string m_strExceptionText;
#endregion

#region Init
		protected void InitBaseMembers()
		{
			m_strConnectionString = "";
			m_strExceptionText = "";
			m_zipList = null;
		}
		
		public USCBase()
		{
			InitBaseMembers();
		}

		private void InitZipList()
		{
			if( null == m_zipList )
			{
				m_zipList = ZipcodeList.Instance;
				m_zipList.Init(m_strConnectionString, false);
			}
		}

		public string GetSiteSetting(long index, string keyName, int nLang)
		{
			string strRet = "";

			SiteAdmin sa = SiteAdmin.Instance;

			if (null == sa)
			{
				// What to do here?  The SA should already be instantiated
				return "";
			}

			// If this is English and we know the index, use the faster method.
			if (langENUS == nLang && index > 0)
			{
				strRet = sa.GetValue(index);
			}
			else
			{
				strRet = sa.GetValue(nLang, keyName);
			}
			return strRet;
		}
#endregion

#region SQL functions
		// Used to determine if this is the production instance or the development instance on the same server.
		// This should never be an issue on dev only machines.
		public bool IsLocalInstance()
		{
			bool fRet = false;
			if (null != m_strConnectionString)
			{
				if (m_strConnectionString.Contains("MyUSCLocal"))
				{
					fRet = true;
				}
			}

			return fRet;
		}
		public long ExecuteSQLInsert(string strSQLQuery)
		{
			long lInsertID = -1;
			SqlConnection sqlConn = null;

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
				lInsertID = Convert.ToInt64(sqlCommand.ExecuteScalar());
				sqlConn.Close();
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("USCBase:ExecuteSQLInsert failure", ex, 0);
				return -1;
			}
			finally
			{
				sqlConn.Close();
			}

			return lInsertID;
		}

		public static bool StExecuteSPNoValue(string strSP, string strCnx, SqlParameter[] paramArray, out int rowsAffected)
		{
			return SQLHelper.ExecuteSPNoValue( strSP, strCnx, paramArray, out rowsAffected );
		}

		public static bool StExecuteSPRows(string strSP, string strCnx, SqlParameter[] paramArray, out SqlDataReader reader, out SqlConnection sqlConn)
		{
			return SQLHelper.ExecuteSPRows( strSP, strCnx, paramArray, out reader, out sqlConn );
		}

		public bool ExecuteSPRows(string strSP, SqlParameter[] paramArray, out SqlDataReader reader, out SqlConnection sqlConn)
		{
			bool fRet = false;
			sqlConn = null;
			reader = null;
			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				SqlCommand cmd = new SqlCommand();

				if (null != paramArray )
				{
					foreach (SqlParameter sp in paramArray)
					{
						if (null != sp)
						{
							cmd.Parameters.Add(sp);
						}
					}
				}
				cmd.CommandText = strSP;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Connection = sqlConn;
				sqlConn.Open();

				reader = cmd.ExecuteReader();

				fRet = true;
			}
			catch (Exception ex)
			{
				string strError = "USCBase:ExecuteSPRows failure " + strSP + " - ";
				EvtLog.WriteException(strError, ex, 0);
				return false;
			}
			finally
			{
			}
			return fRet;
		}

		public int ExecuteSPCount( string strSP, SqlParameter[] paramArray )
		{
			int count = 0;

			Object retObj = null;

			if (ExecuteSPValue(strSP, paramArray, out retObj))
			{
				if (null != retObj)
				{
					count = Convert.ToInt32(retObj);
				}
			}

			return count;
		}

		public long ExecuteSPInsert(string strSP, SqlParameter[] paramArray)
		{
			return ExecuteSPInsert( strSP, m_strConnectionString, paramArray );
		}

		public long ExecuteSPInsert(string strSP, string strCnxString, SqlParameter[] paramArray)
		{
			long lRet = 0;
			SqlConnection sqlConn = null;
			try
			{
				sqlConn = new SqlConnection(strCnxString);
				SqlCommand cmd = new SqlCommand();

				if (null != paramArray)
				{
					foreach (SqlParameter sp in paramArray)
					{
						if (null != sp)
						{
							cmd.Parameters.Add(sp);
						}
					}
				}
				cmd.CommandText = strSP;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Connection = sqlConn;
				sqlConn.Open();

				cmd.ExecuteNonQuery();

				cmd.Parameters.Clear();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT @@IDENTITY";
				// Get the last inserted id.
				object obj = cmd.ExecuteScalar();
				if( null != obj )
				{
					lRet = Convert.ToInt64( obj );
				}
				cmd.Parameters.Clear();

				sqlConn.Close();
			}
			catch (Exception ex)
			{
				string strError = "USCBase:ExecuteSPInsert failure " + strSP + " - ";
				EvtLog.WriteException(strError, ex, 0);
				return -1;
			}
			finally
			{
				sqlConn.Close();
			}
			return lRet;
		}

		public bool ExecuteSPValue(string strSP, SqlParameter[] paramArray, out Object returnValue)
		{
			return ExecuteSPValue(strSP, m_strConnectionString, paramArray, out returnValue);
		}

		public bool ExecuteSPValue(string strSP, string strCnx, SqlParameter[] paramArray, out Object returnValue)
		{
			bool fRet = false;
			SqlConnection sqlConn = null;
			returnValue = null;
			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				SqlCommand cmd = new SqlCommand();

				if (null != paramArray )
				{
					foreach (SqlParameter sp in paramArray)
					{
						if (null != sp)
						{
							cmd.Parameters.Add(sp);
						}
					}
				}
				cmd.CommandText = strSP;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Connection = sqlConn;
				sqlConn.Open();

				returnValue = cmd.ExecuteScalar();

				cmd.Parameters.Clear();
				sqlConn.Close();
				fRet = true;
			}
			catch (Exception ex)
			{
				string strError = "USCBase:ExecuteSPValue failure " + strSP + " - ";
				EvtLog.WriteException(strError, ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}
			return fRet;
		}

		public bool ExecuteSPNoValue(string strSP, SqlParameter[] paramArray)
		{
			int rowsAffected = 0;
			return ExecuteSPNoValue(strSP, m_strConnectionString, paramArray, out rowsAffected);
		}

		public bool ExecuteSPNoValue(string strSP, string strCnx, SqlParameter[] paramArray)
		{
			int rowsAffected = 0;
			return ExecuteSPNoValue( strSP, strCnx, paramArray, out rowsAffected );
		}

		public bool ExecuteSPNoValue( string strSP, string strCnx, SqlParameter[] paramArray, out int rowsAffected )
		{
			bool fRet = false;
			SqlConnection sqlConn = null;
			rowsAffected = 0;
			try
			{
				sqlConn = new SqlConnection(strCnx);
				SqlCommand cmd = new SqlCommand();

				if (null != paramArray )
				{
					foreach (SqlParameter sp in paramArray)
					{
						if( null != sp )
						{
							cmd.Parameters.Add(sp);
						}
					}
				}
				cmd.CommandText = strSP;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Connection = sqlConn;
				sqlConn.Open();

				rowsAffected = cmd.ExecuteNonQuery();

				cmd.Parameters.Clear();
				sqlConn.Close();
				fRet = true;
			}
			catch (Exception ex)
			{
				string strError = "USCBase:ExecuteSPNoValue failure " + strSP + " - ";
				EvtLog.WriteException(strError, ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}
			return fRet;
		}

		public bool ExecuteSQL(string strSQLQuery)
		{
			bool fRet = false;
			SqlConnection sqlConn = null;

			try
			{
				sqlConn = new SqlConnection(m_strConnectionString);
				sqlConn.Open();
				SqlCommand sqlCommand = new SqlCommand(strSQLQuery, sqlConn);
				sqlCommand.ExecuteNonQuery();
				sqlConn.Close();
				fRet = true;
			}
			catch (Exception ex)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("USCBase:ExecuteSQL failure");
				sb.AppendLine();
				sb.AppendLine(strSQLQuery);
				sb.AppendLine();
				EvtLog.WriteException(sb.ToString(), ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}

			return fRet;
		}

		protected string Sanitize(string line)
		{
			return SQLHelper.Sanitize(line);
		}

		protected string ObjectToString(Object obj)
		{
			return SQLHelper.ObjectToString(obj);
		}

		protected int ObjectToInt(Object obj)
		{
			return SQLHelper.ObjectToInt(obj);
		}

		protected long ObjectToLong(Object obj)
		{
			return SQLHelper.ObjectToLong(obj);
		}

		protected float ObjectToFloat(Object obj)
		{
			return SQLHelper.ObjectToFloat(obj);
		}
		protected bool ObjectToBool(Object obj)
		{
			return SQLHelper.ObjectToBool(obj);
		}
		protected DateTime ObjectToDateTime(Object obj)
		{
			return SQLHelper.ObjectToDateTime(obj);
		}

#endregion

#region FunctionGetCityStateFromZip
		public bool GetCityStateFromZip(string strZipcode, out string strCity, out string strState)
		{
			bool fRet = false;

			// Make sure the list has been initialized
			InitZipList();

			strCity = "";
			strState = "";
			try
			{
				Zipcode zip = (Zipcode)m_zipList.GetZipcode(strZipcode);
				if (null != zip)
				{
					strCity = zip.City;
					strState = zip.State;
					fRet = true;
				}
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("USCBase:FunctionGetCityStateFromZip failure", ex, 0);
				string except = ex.Message;
			}
			return fRet;
		}
#endregion

#region XML Functions
		public static XElement XElementByName(XElement root, string strNodeName)
		{
			XElement xRet = null;
			IEnumerable<XElement> elements = from el in root.Elements(strNodeName)
											 select el;
			foreach (XElement el in elements)
			{
				if (el.Name == strNodeName)
				{
					xRet = el;
					break;
				}
			}
			return xRet;
		}

		public static XElement XElementByValue(XElement root, string strNodeName, string strSubElement, string strValue)
		{
			XElement xRet = null;
			IEnumerable<XElement> elements = from el in root.Elements(strNodeName)
											 select el;
			foreach (XElement el in elements)
			{
				XElement xe = USCBase.XElementByName(el, strSubElement);
				if (xe.Value == strValue)
				{
					xRet = el;
					break;
				}
			}
			return xRet;
		}

		// Assumes attribute name are unique
		public static XAttribute XAttributeByName( XElement root, string strAttribute )
		{
			XAttribute xAttr = null;

			IEnumerable<XAttribute> attList = root.Attributes( strAttribute );
			foreach (XAttribute att in attList)
			{
				xAttr = att;
				break;
			}

			return xAttr;
		}

		public static void UpdateImpressions( string strServerRoot, string strAdName, string strSource )
		{
			if( null != strServerRoot && strServerRoot.Length > 0 &&
				null != strAdName && strAdName.Length > 0 &&
				null != strSource && strSource.Length > 0)
				{
					string strFile = strServerRoot + "App_Data\\AdImpressions.xml";
					UpdateXMLFile(strFile, strAdName, strSource);
				}
		}

		public static void UpdateClicks(string strServerRoot, string strAdName, string strSource)
		{
			if (null != strServerRoot && strServerRoot.Length > 0 &&
				null != strAdName && strAdName.Length > 0 &&
				null != strSource && strSource.Length > 0)
			{
				string strFile = strServerRoot + "App_Data\\AdResponses.xml";
				UpdateXMLFile(strFile, strAdName, strSource);
			}
		}

		public static void UpdateXMLFile(string strFile, string strAdName, string strSource)
		{
			XDocument doc = null;
			try
			{
				String adName = strAdName;
				string source = strSource;
				if( adName.Length == 0 || strSource.Length == 0 )
				{
					// Just go back to site.
					return;
				}

				doc = XDocument.Load(strFile);

				XElement root = doc.Root;

				XElement rotatorNode = null;
				if (source.Length > 0)
				{
					string rotator = "adRotator" + source;
					rotatorNode = USCBase.XElementByValue( root, "adRotator", "id", strSource );
				}

				//Rotator was not found
				if (null == rotatorNode)
				{
					rotatorNode = new XElement("adRotator");
					XElement xeID = new XElement("id", strSource);
					rotatorNode.Add( xeID );
					root.Add( rotatorNode );
				}

				XElement adNode = USCBase.XElementByValue(rotatorNode, "ad", "adname", adName);
				if (adNode != null)
				{
					//Update existing node
					XElement xeHitCount = USCBase.XElementByName(adNode, "hitCount");
					if (null != xeHitCount)
					{
						int ctr = int.Parse(xeHitCount.Value);
						ctr += 1;
						xeHitCount.Value = ctr.ToString();
					}
				}
				else
				{
					//Create a new node.
					XElement xeAd = new XElement("ad");
					XElement xeAdName = new XElement("adname", adName);
					int ctr = 1;
					XElement xeHitCount = new XElement("hitCount", ctr.ToString());

					xeAd.Add(xeAdName);
					xeAd.Add(xeHitCount);
					rotatorNode.Add(xeAd);
				}
				doc.Save(strFile);
			}
			catch( Exception ex )
			{
				EvtLog.WriteException("USCBase.UpdateImpressions", ex, 2);
			}
		}

#endregion

#region STATIC FUNCTIONS
		public int SQLBitFromBool( bool fIn )
		{
			int nRet = 0;
			if( fIn )
			{
				nRet = 1;
			}
			return nRet;
		}

		public static string Truncate( string strIn, int maxLen, bool fEllipse )
		{
			string strRet = strIn;
			if( maxLen < strRet.Length )
			{
				if( fEllipse )
				{
					strRet.Remove(maxLen - 4);
					strRet += "...";
				}
				else
				{
					strRet.Remove(maxLen-1);
				}
			}
			return strRet;
		}

		public static string Capitalize(string strString)
        {
	        // Check for empty string.
	        if (string.IsNullOrEmpty(strString))
	        {
	            return string.Empty;
	        }
	        // Return char and concat substring.
            strString = strString.ToLower();
	        return char.ToUpper(strString[0]) + strString.Substring(1);
        }

		public static bool IsAlphaNumericString(string strString)
		{
			char[] myChars = strString.ToCharArray();
			foreach (char myChr in myChars)
			{
				if (!char.IsLetterOrDigit(myChr))
				{
					return false;
				}
			}

			return true;
		}

		public static bool IsNumericString(string strString)
		{
			char[] myChars = strString.ToCharArray();
			foreach (char myChr in myChars)
			{
				if (!char.IsDigit(myChr))
				{
					return false;
				}
			}

			return true;
		}

		public static bool IsDecimalString(string strString)
		{
			char[] myChars = strString.ToCharArray();
			foreach (char myChr in myChars)
			{
				if (!char.IsDigit(myChr) && myChr != '.' )
				{
					return false;
				}
			}

			return true;
		}

		public static bool IsPasswordString(string strString)
		{
			if (System.Text.RegularExpressions.Regex.IsMatch(strString, @"^[a-zA-Z0-9!@#$%^&*]+$"))
			{
				return true;
			}

			return false;
		}

		public static string ReplaceString(string strString, string strOriginal, string strReplacement)
		{
			string tempReplaceit = null;
			Int32 intPosition = 1;
			string strPre = "";
			string strPost = "";

			while (intPosition > 0)
			{
				intPosition = (strString.IndexOf(strOriginal, 0) + 1);

				if (intPosition > 0)
				{
					strPre = strString.Substring(0, (intPosition - 1));
					strPost = strString.Substring((intPosition + 1) - 1);
					strString = strPre + strReplacement + strPost;
				}
				else
					break;

			}

			tempReplaceit = strString;
			return tempReplaceit;
		}
#endregion

#region Accessors
		public string ExceptionText
		{
			get
			{
				return this.m_strExceptionText;
			}
			set
			{
				this.m_strExceptionText = value;
			}
		}

		public string ConnectionString
		{
			get
			{
				return this.m_strConnectionString;
			}
			set
			{
				this.m_strConnectionString = value;
			}
		}

#endregion
	}

	public class USCBaseItem : USCBase
	{
#region Member Variables
		protected long m_lKey;
		protected int m_nLanguage;
		protected string m_strCreator;
		protected DateTime m_dtCreateDate;
		protected string m_strLastUpdate;
#endregion

#region Init
		protected void InitBaseItemMembers()
		{
			InitBaseMembers();
			m_lKey = -1;
			m_nLanguage = 1;
			m_strCreator = "";
			m_dtCreateDate = DateTime.Now;
			m_strLastUpdate = "";
		}

		public USCBaseItem()
		{
			InitBaseItemMembers();
		}
#endregion

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

	public class USCBaseList : USCBase
	{
		public USCBaseList()
		{
		}

	}
}