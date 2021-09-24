using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyUSC.Classes;

namespace CreateEvtSource
{
	class Program
	{
		static void Main(string[] args)
		{
			string sSource = "MSC Web";
			string sLog = "MySportsConnect";
			try
			{
				if (!EventLog.SourceExists(sSource))
				{
					EventLog.CreateEventSource(sSource, sLog);
				}

				EventLog.WriteEntry(sSource, "Test event", EventLogEntryType.Information, 0);
			}
			catch( Exception ex )
			{
				string str = ex.ToString();
				str = "";
			}
		}
	}
}
