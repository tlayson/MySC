using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyUSC.Classes
{
	public class EmailUtil : USCBaseItem
	{
		// Default GoDaddy values
		string m_strSMTPAddress = "smtpout.secureserver.net";
		int m_nPortNumber = 80;
		bool m_fEnableSSL = false;
		string m_strInfoAccount = "information@mysportsconnect.net";
		string m_strInfoPassword = "Mysc2013";
		string m_strSupportAccount = "support@mysportsconnect.net";
		string m_strSupportPassword = "Mysc2013";

		public EmailUtil( SiteAdmin sa )
		{
/*
			if( null != sa )
			{
	            m_strInfoAccount = sa.GetValue(SiteAdmin.saKeyInfoEmail);
				m_strInfoPassword = sa.GetValue(SiteAdmin.saKeyInfoPswd);
				m_strSupportAccount = sa.GetValue(SiteAdmin.saKeySupportEmail);
				m_strSupportPassword = sa.GetValue(SiteAdmin.saKeySupportPswd);
				m_strSMTPAddress = sa.GetValue(SiteAdmin.saKeySMTPAddress);

				string strTemp = sa.GetValue(SiteAdmin.saKeyEmailPort);
				int nEmailPort = 80;
				if( null != strTemp && int.TryParse( strTemp, out nEmailPort ) )
				{
					m_nPortNumber = nEmailPort;
				}

				strTemp = sa.GetValue(SiteAdmin.saKeyEmailPort);
				if( null != strTemp && strTemp == "1" )
				{
					m_fEnableSSL = true;
				}
			}
*/
		}

		public string SupportAccount
		{
			get
			{
				return this.m_strSupportAccount;
			}
		}

		public string InfoAccount
		{
			get
			{
				return this.m_strInfoAccount;
			}
		}

		public bool SendSupportMail(string strEmailTo, string strSubject, string strBody)
		{
			return SendSupportMail( SupportAccount, strEmailTo, strSubject, strBody );
		}

		public bool SendSupportMail( string strEmailFrom, string strEmailTo, string strSubject, string strBody )
		{
			bool fRet = true;
			using (MailMessage mail = new MailMessage())
			{
				mail.From = new MailAddress(strEmailFrom);
				mail.To.Add(strEmailTo);
				mail.Subject = strSubject;
				mail.Body = strBody;
				mail.IsBodyHtml = true;
				// Can set to false, if you are sending pure text.
				//mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
				//mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));
				using (SmtpClient smtp = new SmtpClient(m_strSMTPAddress, m_nPortNumber))
				{
					smtp.Credentials = new NetworkCredential(m_strSupportAccount, m_strSupportPassword);
					smtp.EnableSsl = m_fEnableSSL;
					try
					{
						smtp.Send(mail);
					}
					catch (Exception ex)
					{
						fRet = false;
						EvtLog.WriteException("EmailUtil:SendSupportMail", ex, 10);
					}
				}
			}
			return fRet;
		}

		public bool SendInfoMail(string strEmailTo, string strSubject, string strBody)
		{
			return SendInfoMail( InfoAccount, strEmailTo, strSubject, strBody );
		}

		public bool SendInfoMail(string strEmailFrom, string strEmailTo, string strSubject, string strBody)
		{
			bool fRet = true;
			using (MailMessage mail = new MailMessage())
			{
				mail.From = new MailAddress(strEmailFrom);
				mail.To.Add(strEmailTo);
				mail.Subject = strSubject;
				mail.Body = strBody;
				mail.IsBodyHtml = true;
				// Can set to false, if you are sending pure text.
				//mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
				//mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));
				using (SmtpClient smtp = new SmtpClient(m_strSMTPAddress, m_nPortNumber))
				{
					smtp.Credentials = new NetworkCredential(m_strInfoAccount, m_strInfoPassword);
					smtp.EnableSsl = m_fEnableSSL;
					try
					{
						smtp.Send(mail);
					}
					catch (Exception ex)
					{
						fRet = false;
						EvtLog.WriteException("EmailUtil:SendInfoMail", ex, 10);
					}
				}
			}
			return fRet;
		}

	}
}