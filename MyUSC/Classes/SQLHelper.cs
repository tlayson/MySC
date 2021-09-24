using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC.Classes
{
	public class SQLHelper
	{
#region SQL
		// Used to determine if this is the production instance or the development instance on the same server.
		// This should never be an issue on dev only machines.
		public static bool IsLocalInstance(string strCnx)
		{
			bool fRet = false;
			if (null != strCnx)
			{
				if (strCnx.Contains("MyUSCLocal"))
				{
					fRet = true;
				}
			}

			return fRet;
		}

		public static bool ExecuteSPNoValue(string strSP, string strCnx, SqlParameter[] paramArray, out int rowsAffected)
		{
			bool fRet = false;
			SqlConnection sqlConn = null;
			rowsAffected = 0;
			try
			{
				sqlConn = new SqlConnection(strCnx);
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

				rowsAffected = cmd.ExecuteNonQuery();

				cmd.Parameters.Clear();
				sqlConn.Close();
				fRet = true;
			}
			catch (Exception ex)
			{
				string strErr = "SQLHelper:ExecuteSPNoValue failure";
				short sCat = 0;
				if (IsLocalInstance( strCnx ))
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.SQLExecNoVal, sCat);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}
			return fRet;
		}

		public static bool ExecuteSPRows(string strSP, string strCnx, SqlParameter[] paramArray, out SqlDataReader reader, out SqlConnection sqlConn)
		{
			bool fRet = false;
			sqlConn = null;
			reader = null;
			try
			{
				sqlConn = new SqlConnection(strCnx);
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

				reader = cmd.ExecuteReader();

				fRet = true;
			}
			catch (Exception ex)
			{
				string strErr = "SQLHelper:ExecuteSPRows failure";
				short sCat = 0;
				if (IsLocalInstance(strCnx))
				{
					strErr += " [Local] ";
					sCat = 99;
				}
				EvtLog.WriteException(strErr, ex, EventErrors.ErrorType.SQLExecNoVal, sCat);
				return false;
			}
			finally
			{
			}
			return fRet;
		}

		public static string Sanitize(string line)
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

		public static string ObjectToString(Object obj)
		{
			string strRet = "";
			if (null != obj && !DBNull.Value.Equals(obj))
			{
				strRet = obj.ToString();
				// remove any double '
				int index = strRet.IndexOf("''");
				if (-1 != index)
				{
					string val = strRet;
					strRet = val.Replace("''", "'");
				}
			}
			return strRet;
		}

		public static int ObjectToInt(Object obj)
		{
			int nRet = 0;
			if (null != obj && !DBNull.Value.Equals(obj))
			{
				nRet = Convert.ToInt32(obj);
			}
			return nRet;
		}

		public static long ObjectToLong(Object obj)
		{
			long lRet = 0;
			if (null != obj && !DBNull.Value.Equals(obj))
			{
				lRet = Convert.ToInt64(obj);
			}
			return lRet;
		}

		public static float ObjectToFloat(Object obj)
		{
			float flRet = 0;
			if (null != obj && !DBNull.Value.Equals(obj))
			{
				flRet = (float)Convert.ToDecimal(obj);
			}
			return flRet;
		}

		public static bool ObjectToBool(Object obj)
		{
			bool fRet = false;
			if (null != obj && !DBNull.Value.Equals(obj))
			{
				fRet = Convert.ToBoolean(obj);
			}
			return fRet;
		}

		public static DateTime ObjectToDateTime(Object obj)
		{
			DateTime dtRet = DateTime.Parse("1/1/1800");
			if (null != obj && !DBNull.Value.Equals(obj))
			{
				dtRet = Convert.ToDateTime(obj);
			}
			return dtRet;
		}

#endregion
	}
}