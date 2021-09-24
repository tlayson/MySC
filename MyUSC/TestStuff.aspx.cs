using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace MyUSC
{
	public partial class TestStuff : USCPageBase
	{
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			DateSelect ds = dsDateSelect;
			DateTime dt = DateTime.Now;
			dsDateSelect.SetDate(dt);
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		void SendEmail2()
		{
			const string SERVER = "relay-hosting.secureserver.net";
			const string fromPassword = "Mysc2013";
			var fromAddress = new MailAddress("support@MySportsConnect.com", "From Name");
			var toAddress = new MailAddress("tlayson@hotmail.com", "To Name");
			const string subject = "Email test";
			const string body = "Sent using the website!!";
			var smtp = new SmtpClient
			{
				Host = SERVER,
				Port = 465,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
				Timeout = 40000
			};

			MailMessage message = new System.Net.Mail.MailMessage(fromAddress, toAddress);
			try
			{
				message.Priority = MailPriority.High;
				message.Subject = subject;
				message.Body = body;
				smtp.Send(message);
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("Send email failure", ex, 0);
			}
			message = null; // free up resources
			smtp = null;
		}

		void SendEmail()
		{
			var fromAddress = new MailAddress("support@MySportsConnect.com", "From Name");
			var toAddress = new MailAddress("tlayson@hotmail.com", "To Name");
			const string fromPassword = "Mysc2013";
			const string subject = "Email test";
			const string body = "Sent using the website!!";

			var smtp = new SmtpClient
			{
				Host = "smtpout.secureserver.net ",
				//Host = "smtp.ex2.secureserver.net",
				Port = 80,
				EnableSsl = false,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
				Timeout = 40000
			};
			using (var message = new MailMessage(fromAddress, toAddress))
			{
				try
				{
					message.Subject = subject;
					message.Body = body;
					smtp.Send(message);
				}
				catch( Exception ex )
				{
					EvtLog.WriteException("Send email failure", ex, 0);
				}
			}
		}

		void ExecuteSP()
		{
			USCBase obj = new USCBase();
			obj.ConnectionString = Master.g_strConnectionString;
			string strSP = "sp_GetUsersLastXDays";

			SqlParameter[] paramArray = new SqlParameter[1];
			SqlParameter sqlParam = new SqlParameter("@days", 30);
			paramArray[0] = sqlParam;

			int count = obj.ExecuteSPCount(strSP, paramArray);

			paramArray[0].Value = 90;
			count = obj.ExecuteSPCount(strSP, paramArray);

			count = 0;

		}

		protected void OnClickTest(object sender, EventArgs e)
		{
			ExecuteSP();
		}

		void RedirTest( string adName, string source)
		{
			String strRootPath = Server.MapPath("~");
			String docPath = strRootPath + "App_Data\\AdResponses.xml";
			XDocument doc = XDocument.Load(docPath);

			XElement root = doc.Root;

			XElement rotatorNode = null;
			if (source.Length > 0)
			{
				string rotator = "adRotator" + source;
				rotatorNode = USCBase.XElementByName(root, rotator);
			}

			//Rotator was not found
			if (null == rotatorNode)
			{
				rotatorNode = root;
			}

			XElement adNode = USCBase.XElementByValue(rotatorNode, "ad", "adname", adName);
			if (adNode != null)
			{
				XAttribute xAttr = USCBase.XAttributeByName(adNode, "hitCount");
				if (null != xAttr)
				{
					int ctr = int.Parse(xAttr.Value);
					ctr += 1;
					xAttr.Value = ctr.ToString();
				}
			}
			else
			{
				//Create a new node.
				XElement xeAd = new XElement("ad");
				XAttribute xaAdname = new XAttribute("adname", adName);
				int ctr = 1;
				XAttribute xaHitCount = new XAttribute("hitCount", ctr.ToString());
				xeAd.Add(xaAdname);
				xeAd.Add(xaHitCount);
				rotatorNode.Add(xeAd);
			}
			doc.Save(docPath);
		}

		void RedirTest1()
		{
			RedirTest( "Nike", "6" );
			RedirTest( "Nike", "6" );
		}

		void ImpressionTest()
		{
			String strRootPath = Server.MapPath("~");
			USCBase.UpdateImpressions(strRootPath, "Nike", "1");
			USCBase.UpdateImpressions(strRootPath, "Bogus", "1");
		}

		protected void OnClickRedirect(object sender, EventArgs e)
		{
			ImpressionTest();
		}
	}
}