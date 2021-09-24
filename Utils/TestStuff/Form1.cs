using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using MyUSC.Classes;
using MyUSC.Classes.MyOrg;

namespace TestStuff
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void OnClickLoadXML(object sender, EventArgs e)
		{
			OpenFileDialog dlgOpenFile = new OpenFileDialog();
			dlgOpenFile.InitialDirectory = "C:\\MyUSC\\XML";
			DialogResult dr = dlgOpenFile.ShowDialog();
			if( dr == DialogResult.OK )
			{
				string strPath = dlgOpenFile.FileName;
				lblFilePath.Text = strPath;

				XmlDocument doc = new XmlDocument();
				doc.Load(strPath);

				txtBefore.Text = doc.OuterXml;

				XmlNode root = doc.FirstChild;
				XmlNode feedContent = root.NextSibling;
				XmlAttribute attr = feedContent.Attributes["xmlns"];
				if (null != attr)
				{
					StringReader stringReader = new StringReader(doc.OuterXml);
					XmlTextReader xmlReader = new XmlTextReader(stringReader);
					xmlReader.Namespaces = false; //A trick to handle special xmlns attributes as regular

					//Build DOM
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(xmlReader);

					//Do the job
					xmlDocument.DocumentElement.RemoveAttribute("xmlns");

					//Prepare a writer
					StringWriter stringWriter = new StringWriter();
					XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);

					//Optional: Make an output nice ;)
					xmlWriter.Formatting = Formatting.Indented;
					xmlWriter.IndentChar = ' ';
					xmlWriter.Indentation = 2;

					//Build output
					xmlDocument.Save(xmlWriter);

					txtAfter.Text = xmlDocument.OuterXml;
				}
				else
				{
					txtAfter.Text = "The xmlns attribute was not found in the XML file.";
				}
			}
		}

		private void OnClickTest(object sender, EventArgs e)
		{
			String strRootPath = "C:\\USC\\Website\\MyUSC\\";
			USCBase.UpdateImpressions(strRootPath, "Nike", "test");
			USCBase.UpdateImpressions(strRootPath, "Bogus", "test");
			USCBase.UpdateImpressions(strRootPath, "Bogus", "test");
			USCBase.UpdateImpressions(strRootPath, "Bogus", "test");
			USCBase.UpdateImpressions(strRootPath, "Bogus", "test");
			USCBase.UpdateImpressions(strRootPath, "Bogus", "test");
			USCBase.UpdateImpressions(strRootPath, "Bogus", "test");
		}

		private void OnClickEmailTest(object sender, EventArgs e)
		{
			const string smtpAddress = "smtpout.secureserver.net";
			const int portNumber = 80;
			const bool enableSSL = false;
			string strSupportAccount = "support@mysportsconnect.net";
			string strSupportPassword = "Mysc2013";
			string strEmailFrom = "tlayson@mysportsconnect.net";

			// smtpout.secureserver.net  80,3535,25

			using (MailMessage mail = new MailMessage())
			{
				mail.From = new MailAddress(strEmailFrom);
				mail.To.Add("tlayson@amazon.com");
				mail.Subject = "Test Subject";
				mail.Body = "This is the body of the test email.";
				mail.IsBodyHtml = true;
				// Can set to false, if you are sending pure text.
				//mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
				//mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));
				using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
				{
					smtp.Credentials = new NetworkCredential(strSupportAccount, strSupportPassword);
					smtp.EnableSsl = enableSSL;
					try
					{
						smtp.Send(mail);
					}
					catch (Exception ex)
					{
						EvtLog.WriteException("EmailUtil:SendSupportMail", ex, 10);
					}
				}
			}

		}

		private void OnClickLoadPageOptions(object sender, EventArgs e)
		{
			Organization org = new Organization();
			org.ConnectionString = "Server=localhost;Database=MyUSC;User ID=myscadmin;Password=Mysc2013!";
			org.OrgID = 1;
			//org.LoadPageOptions();
		}

		private void OnClickExit(object sender, EventArgs e)
		{

		}

		private void OnClickTestSQLSPWithRows(object sender, EventArgs e)
		{
			USCBase obj = new USCBase();
			obj.ConnectionString = "Server=localhost;Database=MyUSC;User ID=myscadmin;Password=Mysc2013!";

			SqlDataReader reader = null;
			SqlConnection sqlConn = null;
			SqlParameter[] paramArray = new SqlParameter[1];
			paramArray[0] = new SqlParameter("@OrgID", 10002);

			if (obj.ExecuteSPRows("sp_GetOrgMembers", paramArray, out reader, out sqlConn) && null != reader )
			{
				try
				{
					while (reader.Read())
					{
						// SELECT Member.MemberIdx, Member.OrgID, Member.MemberType, Member.Note, Member.Deleted, account.AccountID , account.LastName 
						long idx = (long)((IDataRecord)reader)[0];
						long orgID = (long)((IDataRecord)reader)[1];
						int type = (int)((IDataRecord)reader)[2];
						string note = ((IDataRecord)reader)[3].ToString();
						bool deleted = (bool)((IDataRecord)reader)[4];
						long acctID = (long)((IDataRecord)reader)[5];
						string name = ((IDataRecord)reader)[6].ToString();
					}
					reader.Close();
					sqlConn.Close();
				}
				catch( Exception ex )
				{
					string err = ex.ToString();
				}
			}

		}

		private void ParseTest()
		{
			DateTime dtResult = new DateTime();
			bool fRet = true;

			string strDate = "8/16/1963";
			if (!DateTime.TryParse(strDate, out dtResult))
			{
				fRet = false;
			}

			strDate = "08/16/1963";
			if (!DateTime.TryParse(strDate, out dtResult))
			{
				fRet = false;
			}

			strDate = "8/06/1963";
			if (!DateTime.TryParse(strDate, out dtResult))
			{
				fRet = false;
			}

			strDate = "08/06/1963";
			if (!DateTime.TryParse(strDate, out dtResult))
			{
				fRet = false;
			}

			strDate = "8-16-1963";
			if (!DateTime.TryParse(strDate, out dtResult))
			{
				fRet = false;
			}

			strDate = "08-16-1963";
			if (!DateTime.TryParse(strDate, out dtResult))
			{
				fRet = false;
			}
		}

		private void OnClickParseTest(object sender, EventArgs e)
		{
		}
	}
}
