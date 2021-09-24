using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyUSC.Classes
{
	public class EventErrors
	{
		public enum ErrorType 
		{ 
			Generic = 0,
			RSSFeed = 1, 
			SQLExecNoVal = 50,
			AccountAdd = 500,
			AccountUpdate = 501,
			AccountDelete = 502,
			AccountRead = 503,
			MyOrgGeneric = 10000,
			OrgAddEvents = 10100,
			OrgUpdateEvents = 10101,
			OrgDeleteEvents = 10102,
			OrgLoadEvents = 10103,
			NullOrg = 10200,
			NullAffiliate = 10201,
			NullEvent = 10202,
			NullResponse = 10203,
			NullMember = 10204,
			NullOfficial = 10205,
			NullSeason = 10206,
			NullVenue = 10207
		}
	}
}