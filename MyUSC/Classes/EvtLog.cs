using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Reflection;

namespace MyUSC.Classes
{
    public class EvtLog : USCBase
	{
		static string sSource = "MSC Web";
		static string sLog = "MySportsConnect";


		static string m_exePath = string.Empty;

		public static void WriteEvent(string strEvent, EventLogEntryType evtType, int evtID, short category)
		{
			if (!EventLog.SourceExists(sSource))
			{
				EventLog.CreateEventSource(sSource, sLog);
			}

			EventLog.WriteEntry( sSource, strEvent, evtType, evtID, category );
/*            
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            try
            {
				m_exePath = string.Empty;
                using (StreamWriter w = File.AppendText(m_exePath + "\\logs\\" + "msclog.txt"))
				//using (StreamWriter w = File.AppendText(m_exePath + "msclog.txt"))
				{
                    Log(strEvent, w);
                }
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                str = string.Empty;
				
            }
*/		
        }

        public static void WriteEvent(string cnxString, string strEvent, EventLogEntryType evtType, int evtID, short category)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            m_exePath = "\\logs\\" + "msclog.txt";
            TransactionLog.LogTransaction(null, cnxString, "LogTest", "Beginning Log Test", "EvtLog", 1, TransactionLog.TransactionLevel.Unknown, TransactionLog.TransactionTypes.DebugGeneral, "Keywords", "Category");

            try
            {
                using (StreamWriter w = File.AppendText(m_exePath ) )
                {
                    Log(strEvent, w);
                    TransactionLog.LogTransaction(null, cnxString, "LogTest Success", "Successfully wrote to " + m_exePath, "EvtLog", 1, TransactionLog.TransactionLevel.Information , TransactionLog.TransactionTypes.DebugGeneral, "Keywords", "Category");
                }
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                str = string.Empty;
                TransactionLog.LogTransaction(null, cnxString, "LogTest Failure", ex.ToString(), "EvtLog", 1, TransactionLog.TransactionLevel.Exception, TransactionLog.TransactionTypes.DebugGeneral, "", "Category");

            }
            /*            
                        if (!EventLog.SourceExists(sSource))
                        {
                            EventLog.CreateEventSource(sSource, sLog);
                        }

                        EventLog.WriteEntry( sSource, strEvent, evtType, evtID, category );
            */
        }

        public static void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                str = string.Empty;
            }
        }
        
        public static void WriteException(string strIntro, Exception ex, EventErrors.ErrorType evtID, short category)
		{
			WriteException( strIntro, ex, (int)evtID, category );
		}

		public static void WriteException( string strIntro, Exception ex, int evtID, short category )
		{
			StringBuilder sbError = new StringBuilder();
			sbError.Append(strIntro).Append(": ").AppendLine(ex.Message);
			if (null != ex.InnerException)
			{
				sbError.AppendLine().Append("[Reason]: ").AppendLine(ex.InnerException.Message);
				if (null != ex.InnerException.InnerException)
				{
					sbError.AppendLine().Append("  [Details]: ").AppendLine(ex.InnerException.InnerException.Message);
				}
			}
			EvtLog.WriteEvent(sbError.ToString(), EventLogEntryType.Error, evtID, category);
		}

		public static void WriteException( string strIntro, Exception ex, int evtID )
		{
			WriteException(strIntro, ex, evtID, 0);
		}

		public static void WriteException(string strIntro, Exception ex, EventErrors.ErrorType evtID)
		{
			WriteException( strIntro, ex, (int)evtID, 0 );
		}

		public static void WriteRSSException(string strIntro, string strRSSDetails, Exception ex, EventErrors.ErrorType evtID)
		{
			StringBuilder sbError = new StringBuilder();
			sbError.Append(strIntro).Append(": ").AppendLine(ex.Message);
			if (null != ex.InnerException)
			{
				sbError.AppendLine().Append("[Reason]: ").AppendLine(ex.InnerException.Message);
				if (null != ex.InnerException.InnerException)
				{
					sbError.AppendLine().Append("  [Details]: ").AppendLine(ex.InnerException.InnerException.Message);
				}
			}

			sbError.AppendLine().Append( strRSSDetails ).AppendLine();
			EvtLog.WriteEvent(sbError.ToString(), EventLogEntryType.Warning, (int)evtID, 0);
		}
	}
}