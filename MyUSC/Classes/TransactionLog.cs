using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Security;

namespace MyUSC.Classes
{
	public class TransactionLog : USCBaseItem
	{
		public enum TransactionLevel { Unknown = 0, Information = 1, Warning = 2, Error = 3, Exception = 4, Audit = 5 }
		public enum TransactionTypes { SuperUser = 0, Admin = 1, AccountCreate = 2, AccountModify = 3, AccountDelete = 4, OrgCreate = 5, OrgModify = 6, OrgDelete = 7, QueryGeneral = 10000, Generic = 99999, DebugGeneral = 500000000, ExceptionGeneral = 1000000000, Unknown = 2000000000 }

		public TransactionLog()
		{
		}

/*
	@Creator nvarchar(50),
	@Title nvarchar(100),
	@Detail nvarchar(2000),
	@Source nvarchar(50),
	@TID int,
	@Level int,
	@Type int,
	@Keywords nvarchar(50),
	@Category nvarchar(50)
*/
		public static bool LogTransactionShort(UserAccount acct,  string strCnx, string strTitle, string strDetail, string strSource, int nTID, TransactionLevel tlLevel, TransactionTypes ttType, string strKeywords, string strCategory)
		{
			return LogTransaction(acct, strCnx, strTitle, strDetail, strSource, 0, TransactionLevel.Information, TransactionTypes.Generic, "", "");
		}

		public static bool LogTransactionQuery(UserAccount acct, string strCnx, string strTitle, string strDetail, string strSource, int nTID, TransactionLevel tlLevel, TransactionTypes ttType, string strKeywords, string strCategory)
		{
			return LogTransaction(acct, strCnx, strTitle, strDetail, strSource, 0, TransactionLevel.Information, TransactionTypes.QueryGeneral, "query", "");
		}

		public static bool LogTransactionDebug(UserAccount acct, string strCnx, string strTitle, string strDetail, string strSource, TransactionTypes ttType )
		{
			if( null == acct || acct.UserType > UserAccount.UserTypes.Admin )
			{
				LogTransaction(acct, strCnx, "", "", "", 69, TransactionLevel.Error, ttType, "debug", "violation");
			}
			return LogTransaction(acct, strCnx, strTitle, strDetail, strSource, 0, TransactionLevel.Information, ttType, "debug", "");
		}

		public static bool LogTransaction(UserAccount acct,  string strCnx, string strTitle, string strDetail, string strSource, int nTID, TransactionLevel tlLevel, TransactionTypes ttType, string strKeywords, string strCategory)
		{
			bool fRet = false;
			int nRowsAffected = -1;
			string strUser = "Unknown";
			string strClean = "";
            if (null != acct)
            {
                strUser = acct.UserName;
            }
            else
            {
                strUser = "Unknown";
            }

            if (strCnx == string.Empty)
            {
                //strCnx = m_strConnectionString;
            }
			strClean = strUser + " -- " + DateTime.Now.ToString();
			strClean = SQLHelper.Sanitize(strClean);

			SqlParameter[] paramArray = new SqlParameter[9];
			paramArray[0] = new SqlParameter("@Creator", SQLHelper.Sanitize(USCBase.Truncate(strUser, 49, true)));
			paramArray[1] = new SqlParameter("@Title", SQLHelper.Sanitize(USCBase.Truncate(strTitle, 99, true)));
			paramArray[2] = new SqlParameter("@Detail", SQLHelper.Sanitize(USCBase.Truncate(strDetail, 1999, true)));
			paramArray[3] = new SqlParameter("@Source", SQLHelper.Sanitize(USCBase.Truncate(strSource, 49, true)));
			paramArray[4] = new SqlParameter( "@TID", nTID );
			paramArray[5] = new SqlParameter("@Level", tlLevel);
			paramArray[6] = new SqlParameter("@Type", ttType);
			paramArray[7] = new SqlParameter("@Keywords", SQLHelper.Sanitize(USCBase.Truncate(strKeywords, 49, true)));
			paramArray[8] = new SqlParameter("@Category", SQLHelper.Sanitize(USCBase.Truncate(strCategory, 49, true)));


			// 		public static bool StExecuteSPNoValue("sp_WriteTransactionToLog", strCnx, paramArray, out nRowsAffected)

			if( USCBase.StExecuteSPNoValue( "sp_WriteTransactionToLog", strCnx, paramArray, out nRowsAffected ) )
			{
				// Make sure we got something back
				if( 0 < nRowsAffected )
				{
					fRet = true;
				}
			}

			return fRet;
		}

	}
}