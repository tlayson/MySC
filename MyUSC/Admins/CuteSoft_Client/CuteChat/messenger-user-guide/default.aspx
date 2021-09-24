<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" %>
<html>
	<head>
		<title>Introduction - ASP.NET Web Messenger</title>
		<link href="style.css" type="text/css" rel="stylesheet">
			<meta name="keywords" content="Instant Messaging,Instant Messenger,Web Messenger,ASP.NET Web Messenger,ASP.NET,Web-Based Instant Messenger,Web-Based Messenger">
			<meta name="description" content="Introduction - ASP.NET Web Messenger">
	</head>
	<body bottommargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form runat="server" ID="Form1">
			<portal:Banner id="Banner" runat="server" />
			<table width="850" border="0" cellpadding="3" cellspacing="0">
				<tr>
					<td valign="top" id="leftcolumn">
						<!-- #include virtual="leftmenu.inc" -->
					</td>
					<td width="20">&nbsp;</td>
					<td valign="top" id="content">
						<h1>Getting Started</h1>
						<h4>Introduction</h4>
						<b>Get connected with Cute Web Messenger!</b>
						<br><br>
						<p>
						The <b>Cute Web Messenger</b> lets you talk online and in real-time with 
						friends and family using just a web browser! 
						<br><br>						
						If you are familiar with public IM systems like AOL Instant Messenger or MSN Messenger, then you will be using our products like a pro in no time.

						</p>
						<br>
						<P>
							With Cute Web Messenger, you can: <br><br>
							<ui>
								<li>Exchange instant messages in real time with one or more friends.</li>
								<li>Grab attention and add fun to your messages with emoticons. </li>
								<li>Add contacts to your contact list. </li>
								<li>Send/Transfer files & photos</li>
							</ui> 
						</P>
						<br>
						Click one of the items below to learn more about Cute Web Messenger.<br>
						<br>
						<table cellpadding="2" cellspacing="3" border="0">							
							<tr>
								<td>
									<img src="images/read2.gif" border="0" align="middle">
								</td>
								<td><a href="contacts-list.aspx">About your contact list</a><br>
								</td>
							</tr>
							<tr>
								<td>
									<img src="images/read2.gif" border="0" align="middle">
								</td>
								<td><a href="Add-contact.aspx">Add a contact</a><br>
								</td>
							</tr>
							<tr>
								<td>
									<img src="images/read2.gif" border="0" align="middle">
								</td>
								<td><a href="Sending-Messages.aspx">Send a message</a><br>
								</td>
							</tr>
						</table>
					</td>
					<td><br><br><P><img src="images/web-messenger.gif" border="0" align="middle" title="Web Messenger"></p>	</td>
				<tr>
				</tr>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</html>
