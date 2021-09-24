using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;
using MyUSC.Classes;

namespace MyUSC
{
	public partial class RSSDisplay : USCPageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//string strMLBURL = "http://mlb.mlb.com/partnerxml/gen/news/rss/mlb.xml#";
			string strNFLURL = "http://www.nfl.com/rss/rsslanding?searchString=team&abbr=SEA";
			string strURL = strNFLURL;
			try
			{
				// Need to figure out how to load from XPath
				//XPathDocument xdoc = new XPathDocument( strURL );
				XmlDocument xmlRSSDocument = new XmlDocument();
				xmlRSSDocument.Load(strURL);
				xmlRSSDisplay.XPathNavigator = xmlRSSDocument.CreateNavigator();
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("SportsNews RSS failure", ex, EventErrors.ErrorType.RSSFeed);
			}
		}
	}
}