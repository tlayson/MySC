using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using MyUSC.Classes;

namespace BuildRSSMenu
{
	public partial class BuildRSSMenu : Form
	{
		SportNameList m_SportsNameList = null; // 1st level menu items
		SportTypeList m_SportsTypeList = null; // 2nd level menu items
		SportDivisionList m_SportsDivList = null; // 3rd level menu items
		SportTeamList m_SportsTeamList = null; // 4th level menu items

		SortedDictionary<long, object> m_sdSportNames = new SortedDictionary<long, object>();
		SortedDictionary<long, object> m_sdSportTypes = new SortedDictionary<long, object>();
		SortedDictionary<long, object> m_sdSportDivisions = new SortedDictionary<long, object>();
		SortedDictionary<long, object> m_sdSportTeams = new SortedDictionary<long, object>();

		string m_strDisplayURL = "SportsNews.aspx?";

		public BuildRSSMenu()
		{
			string strSQLConn = "Server=localhost;Database=MyUSC;Integrated Security=true";

			InitializeComponent();

			m_SportsNameList = SportNameList.Instance;
			m_SportsNameList.Init(strSQLConn, false);
			m_SportsTypeList = SportTypeList.Instance;
			m_SportsTypeList.Init(strSQLConn, false);
			m_SportsDivList = SportDivisionList.Instance;
			m_SportsDivList.Init(strSQLConn, false);
			m_SportsTeamList = SportTeamList.Instance;
			m_SportsTeamList.Init(strSQLConn, false);

			SortData();
		}

		private void SortData()
		{
			foreach (DictionaryEntry de in m_SportsNameList.htSportName)
			{
				SportName sportName = (SportName)de.Value;
				long lID = Convert.ToInt64(sportName.Sequence);
				m_sdSportNames.Add(lID, sportName);
			}

			foreach (DictionaryEntry de in m_SportsTypeList.htSportType)
			{
				SportType sportType = (SportType)de.Value;
				m_sdSportTypes.Add(sportType.ID, sportType);
			}

			foreach (DictionaryEntry de in m_SportsDivList.htSportDivision)
			{
				SportDivision sportDivision = (SportDivision)de.Value;
				m_sdSportDivisions.Add(sportDivision.ID, sportDivision);
			}

			foreach (DictionaryEntry de in m_SportsTeamList.htSportTeam)
			{
				SportTeam sportTeam = (SportTeam)de.Value;
				m_sdSportTeams.Add(sportTeam.ID, sportTeam);
			}
		}

		private void OnClickBrowse(object sender, EventArgs e)
		{
		}

		private bool AddSportNames(XElement xeParent)
		{
			bool fRet = true;
			try
			{
				foreach (KeyValuePair<long, object> kvp in m_sdSportNames)
				{
					SportName sportName = (SportName)kvp.Value;
					XElement xeSportName = new XElement("siteMapNode");
					XAttribute xaTitle = new XAttribute("title", sportName.Name);
					XAttribute xaSportNameID = new XAttribute("SportNameID", sportName.ID.ToString());
					string strRSS = Convert.ToString(sportName.RSSFeedKey);
					XAttribute xaRSSID = new XAttribute("RSSID", strRSS);
					xeSportName.Add(xaTitle);
					xeSportName.Add(xaSportNameID);
					xeSportName.Add(xaRSSID);

					string strURL = m_strDisplayURL + "NameID=" + sportName.ID;
					XAttribute xaRSSURL = new XAttribute("url", strURL);
					xeSportName.Add(xaRSSURL);
					fRet = AddSportTypes(xeSportName, sportName);

					xeParent.Add(xeSportName);
				}
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("BuildRSSMenu AddSportNames failure", ex, 0);
				return false;
			}
			return fRet;
		}

		private bool AddSportTypes(XElement xeParent, SportName sportName)
		{
			bool fRet = true;
			try
			{
				foreach (KeyValuePair<long, object> kvp in m_sdSportTypes)
				{
					SportType sportType = (SportType)kvp.Value;
					if (sportName.ID == sportType.SportNameKey)
					{
						XElement xeSportType = new XElement("siteMapNode");
						XAttribute xaTitle = new XAttribute("title", sportType.Name);
						XAttribute xaSportTypeID = new XAttribute("SportTypeID", sportType.ID.ToString());
						string strRSS = Convert.ToString(sportType.RSSFeedKey);
						XAttribute xaRSSID = new XAttribute("RSSID", strRSS);
						xeSportType.Add(xaTitle);
						xeSportType.Add(xaSportTypeID);
						xeSportType.Add(xaRSSID);

						string strURL = m_strDisplayURL + "TypeID=" + sportType.ID;
						XAttribute xaRSSURL = new XAttribute("url", strURL);
						xeSportType.Add(xaRSSURL);

						fRet = AddSportDivisions(xeSportType, sportType);

						xeParent.Add(xeSportType);
					}
				}
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("BuildRSSMenu AddSportTypes failure", ex, 0);
				return false;
			}
			return fRet;
		}

		private bool AddSportDivisions(XElement xeParent, SportType sportType)
		{
			bool fRet = true;
			try
			{
				foreach (KeyValuePair<long, object> kvp in m_sdSportDivisions)
				{
					SportDivision sportDivision = (SportDivision)kvp.Value;
					if (sportType.ID == sportDivision.SportTypeKey)
					{
						XElement xeSportDivision = new XElement("siteMapNode");
						XAttribute xaTitle = new XAttribute("title", sportDivision.Name);
						XAttribute xaSportDivID = new XAttribute("SportDivID", sportDivision.ID.ToString());
						string strRSS = Convert.ToString(sportDivision.RSSFeedKey);
						XAttribute xaRSSID = new XAttribute("RSSID", strRSS);
						xeSportDivision.Add(xaTitle);
						xeSportDivision.Add(xaSportDivID);
						xeSportDivision.Add(xaRSSID);

						string strURL = m_strDisplayURL + "DivID=" + sportDivision.ID;
						XAttribute xaRSSURL = new XAttribute("url", strURL);
						xeSportDivision.Add(xaRSSURL);

						fRet = AddSportTeams(xeSportDivision, sportDivision);

						xeParent.Add(xeSportDivision);
					}
				}
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("BuildRSSMenu AddSportDivisions failure", ex, 0);
				return false;
			}
			return fRet;
		}

		private bool AddSportTeams(XElement xeParent, SportDivision sportDivision)
		{
			bool fRet = true;
			try
			{
				foreach (KeyValuePair<long, object> kvp in m_sdSportTeams)
				{
					SportTeam sportTeam = (SportTeam)kvp.Value;
					if (sportTeam.SportDivisionKey == sportDivision.ID)
					{
						XElement xeSportTeam = new XElement("siteMapNode");
						XAttribute xaTitle = new XAttribute("title", sportTeam.Name);
						XAttribute xaSportTeamID = new XAttribute("SportTeamID", sportTeam.ID.ToString());
						string strRSS = Convert.ToString(sportTeam.RSSFeedKey);
						XAttribute xaRSSID = new XAttribute("RSSID", strRSS);
						xeSportTeam.Add(xaTitle);
						xeSportTeam.Add(xaSportTeamID);
						xeSportTeam.Add(xaRSSID);

						string strURL = m_strDisplayURL + "TeamID=" + sportTeam.ID;
						XAttribute xaRSSURL = new XAttribute("url", strURL);
						xeSportTeam.Add(xaRSSURL);

						xeParent.Add(xeSportTeam);
					}
				}
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("BuildRSSMenu AddSportTeams failure", ex, 0);
				return false;
			}
			return fRet;
		}

		private void OnClickBuildFile(object sender, EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.DefaultExt = "sitemap";
			dlg.Filter = "sitemap files (*.sitemap)|*.sitemap|All files (*.*)|*.*";

			dlg.InitialDirectory = Directory.GetCurrentDirectory();
			dlg.OverwritePrompt = true;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				string strPath = dlg.FileName;
				string strComment = "Sitemap generated on " + DateTime.Now.ToShortDateString();
				string strReplace = "xmlns=\"http://schemas.microsoft.com/AspNet/SiteMap-File-1.0\"";
				XDocument document = new XDocument();
				document.Add(new XComment(strComment));
				document.Add(new XComment(strReplace));
				XElement xe = new XElement("siteMap");

				XElement xeTop = new XElement("siteMapNode");
				XAttribute xaTop = new XAttribute("title", "Sports");
				xeTop.Add(xaTop);
				xe.Add(xeTop);

				AddSportNames(xeTop);

				document.Add(xe);
				document.Save(strPath);
			}
		}
	}
}
