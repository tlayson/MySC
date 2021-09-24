using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Net;
using System.IO;

namespace MyUSC
{
    public partial class Scroller : System.Web.UI.UserControl
    {
        //private MyUSCDataContext MyUSCDC;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
/*
                MyUSCDC = new MyUSCDataContext();
                var strAllSports = (from c in MyUSCDC.SiteAdministrations
                                          where c.SiteAdministration_PK == 39
                                          select c.Value).Single();

                var strRSS = (from c in MyUSCDC.RSSFeeds
                              where c.URL == strAllSports
                              select c.URL).Single();

                DataTable dtNews = GetTable(strRSS);
                StringBuilder strScrollingNews = new StringBuilder();

                if (dtNews.Rows.Count > 0)
                {
                    strScrollingNews.Append("<Marquee OnMouseOver='this.stop();' OnMouseOut='this.start();' hspace='30' behavior='left' loop='-1' direction='left' scrolldelay='-1' align='middle' width='750' height='25' scrollamount='4'>");

                    //for (int i = 0; i < dtNews.Rows.Count; i++)
                    for (int i = 0; i < dtNews.Rows.Count; i++)
                    {
                        //strScrollingNews.Append("<div valign='middle'><a href=" + dtNews.Rows[i]["Link"].ToString() + " target='_blank'/>" + "<font color='orange' size=5><b>" + dtNews.Rows[i]["Title"].ToString() + "</b></font>" + "</a></div><div class='PostInfo'>" + dtNews.Rows[i]["Description"].ToString() + ".</div><div class='PostTitle' valign='middle'><a href=" + dtNews.Rows[i]["Link"].ToString() + "></a></div>");
                        //strScrollingNews.Append("<a href=" + dtNews.Rows[i]["Link"].ToString() + " target='_blank'/>" + "<font color='orange' size=3><b>" + dtNews.Rows[i]["Title"].ToString() + "</b></font>" + "</a>" + dtNews.Rows[i]["Description"].ToString() + "<a href=" + dtNews.Rows[i]["Link"].ToString() + "></a>");
                        //strScrollingNews.Append("<a href=" + dtNews.Rows[i]["Link"].ToString().Trim() + " target='_blank'/>" + "<font color='orange' size=3><b>" + dtNews.Rows[i]["Title"].ToString().Trim() + "</b></font>" + "</a>" + dtNews.Rows[i]["Description"].ToString().Trim() + "<a href=" + dtNews.Rows[i]["Link"].ToString().Trim() + "></a>");
                        //strScrollingNews.Append("<a href=" + dtNews.Rows[i]["Link"].ToString().Trim() + " target='_blank'/>" + "<font color='orange' size=3><b>" + dtNews.Rows[i]["Title"].ToString().Trim() + "</b></font>" + "</a>" + " -------- "); // + dtNews.Rows[i]["Description"].ToString().Trim() + "<a href=" + dtNews.Rows[i]["Link"].ToString().Trim() + "></a>");
                        strScrollingNews.Append("<a href=" + dtNews.Rows[i]["Link"].ToString().Trim() + " target='_blank'/>" + "<font color='orange' size=3><b>" + dtNews.Rows[i]["Title"].ToString().Trim() + "</b></font>" + "</a>" + " -------- "); // + dtNews.Rows[i]["Description"].ToString().Trim() + "<a href=" + dtNews.Rows[i]["Link"].ToString().Trim() + "></a>");
                    }

                    strScrollingNews.Append("</Marquee>");
                    divNews.InnerHtml = strScrollingNews.ToString();
                }

                else
                {
                    divNews.InnerHtml = "No News";
                }

 */
			}
            catch (Exception ex)
            {
                string strex = ex.Message;
                //Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        public DataTable GetTable(string rssUrl)
        {
/*
            WebRequest request = WebRequest.Create(rssUrl);
            WebResponse response = request.GetResponse();
            StringBuilder sb = new StringBuilder("");
            Stream rssStream = response.GetResponseStream();
            XmlDocument rssDoc = new XmlDocument();
            rssDoc.Load(rssStream);
            XmlNodeList rssItems = rssDoc.SelectNodes("rss/channel/item");
            DataTable NewsTable = new DataTable();
            NewsTable.Columns.Add(new DataColumn("Title", typeof(string)));
            NewsTable.Columns.Add(new DataColumn("Link", typeof(string)));
            NewsTable.Columns.Add(new DataColumn("Description", typeof(string)));
            int upperlimit = rssItems.Count;

            if (upperlimit > 20)
            {
                upperlimit = 20;
            }

            if (upperlimit > 0)
            {
                for (int i = 0; i < upperlimit; i++)
                {
                    DataRow NewsTableRow = NewsTable.NewRow();
                    XmlNode rssDetail;
                    rssDetail = rssItems.Item(i).SelectSingleNode("title");

                    if (rssDetail != null)
                    {
                        NewsTableRow["Title"] = rssDetail.InnerText;
                    }
                    else
                    {
                        NewsTableRow["Title"] = "";
                    }

                    rssDetail = rssItems.Item(i).SelectSingleNode("link");
                    if (rssDetail != null)
                    {
                        NewsTableRow["Link"] = rssDetail.InnerText;
                    }
                    else
                    {
                        NewsTableRow["Link"] = "";
                    }
                    rssDetail = rssItems.Item(i).SelectSingleNode("description");
                    if (rssDetail != null)
                    {
                        NewsTableRow["Description"] = rssDetail.InnerText;
                    }
                    else
                    {
                        NewsTableRow["Description"] = "";
                    }

                    NewsTable.Rows.Add(NewsTableRow);
                }
            }
            return NewsTable;
*/
			return null;
        }

        public void ChangeHeadline()
        {
            try
            {
/*
                MyUSCDC = new MyUSCDataContext();
                System.Random RandNum = new System.Random();
                Int64 intRandomNumber = RandNum.Next(31);
                var strRSS = (from c in MyUSCDC.RSSFeeds
                              where c.RSSFeeds_PK == intRandomNumber
                              select c.URL).Single();

                DataTable dtNews = GetTable(strRSS);
                StringBuilder strScrollingNews = new StringBuilder();

                if (dtNews.Rows.Count > 0)
                {
                    strScrollingNews.Clear();
                    strScrollingNews.Append("<Marquee OnMouseOver='this.stop();' OnMouseOut='this.start();' behavior='left' loop='-1' direction='left' scrolldelay='-1' align='middle' width='450' height='89' scrollamount='6'>");

                    //for (int i = 0; i < dtNews.Rows.Count; i++)
                    for (int i = 0; i < 2; i++)
                    {
                        //strScrollingNews.Append("<div valign='middle'><a href=" + dtNews.Rows[i]["Link"].ToString() + " target='_blank'/>" + "<font color='orange' size=5><b>" + dtNews.Rows[i]["Title"].ToString() + "</b></font>" + "</a></div><div class='PostInfo'>" + dtNews.Rows[i]["Description"].ToString() + ".</div><div class='PostTitle' valign='middle'><a href=" + dtNews.Rows[i]["Link"].ToString() + "></a></div><br>");
                        strScrollingNews.Append("<div valign='middle'><a href=" + dtNews.Rows[i]["Link"].ToString() + " target='_blank'/>" + "<font color='orange' size=5><b>" + dtNews.Rows[i]["Title"].ToString() + "</b></font>" + "</a></div><div class='PostInfo'>" + dtNews.Rows[i]["Description"].ToString() + ".</div><div class='PostTitle' valign='middle'><a href=" + dtNews.Rows[i]["Link"].ToString() + "></a></div>" + " --- ");
                    }

                    strScrollingNews.Append("</Marquee>");
                    divNews.InnerHtml = strScrollingNews.ToString();
                }

                else
                {
                    divNews.InnerHtml = "No News";
                }
*/
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        //protected void tmr1_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {                
        //        MyUSCDC = new MyUSCDataContext();
        //        System.Random RandNum = new System.Random();
        //        Int64 intRandomNumber = RandNum.Next(31);
        //        var strRSS = (from c in MyUSCDC.RSSFeeds
        //                      where c.RSSFeeds_PK == intRandomNumber
        //                      select c.URL).Single();

        //        DataTable dtNews = GetTable(strRSS);
        //        StringBuilder strScrollingNews = new StringBuilder();

        //        if (dtNews.Rows.Count > 0)
        //        {
        //            strScrollingNews.Clear();
        //            strScrollingNews.Append("<Marquee OnMouseOver='this.stop();' OnMouseOut='this.start();' behavior='left' loop='-1' direction='left' scrolldelay='-1' align='middle' width='950' height='89' scrollamount='6'>");

        //            //for (int i = 0; i < dtNews.Rows.Count; i++)
        //            for (int i = 0; i < 1; i++)
        //            {
        //                strScrollingNews.Append("<div valign='middle'><a href=" + "<font color='orange' size=1>" + dtNews.Rows[i]["Link"].ToString() + "</Font>" + " target='_blank'/>" + "<font color='orange' size=1>" + dtNews.Rows[i]["Title"].ToString() + "</font>" + "</a></div><div class='PostInfo'>" + dtNews.Rows[i]["Description"].ToString() + ".</div><div class='PostTitle' valign='middle'><a href=" + "<font color='orange' size=1>" + dtNews.Rows[i]["Link"].ToString() + "</font>" + "></a></div><br>");
        //            }

        //            strScrollingNews.Append("</Marquee>");
        //            divNews.InnerHtml = strScrollingNews.ToString();
        //        }

        //        else
        //        {
        //            divNews.InnerHtml = "No News";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<script>alert('" + ex.Message + "')</script>");
        //    }
        //}
    }
}