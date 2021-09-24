<%@ Page Language="C#" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>

<html>
  <head>
		<title>Block others from seeing or contacting you - ASP.NET Web Messenger</title>
		<link href="style.css" type="text/css" rel="stylesheet">
		<meta name="keywords" content="Instant Messaging,Instant Messenger,Web Messenger,ASP.NET Web Messenger,ASP.NET,Web-Based Instant Messenger,Web-Based Messenger">
		<meta name="description" content="Add a contact - ASP.NET Web Messenger">
		<style>
			.noteBox { BORDER-RIGHT: #999999 1px solid; PADDING-RIGHT: 11px; BORDER-TOP: #999999 1px solid; PADDING-LEFT: 11px; PADDING-BOTTOM: 11px; BORDER-LEFT: #999999 1px solid; WIDTH: 160px; PADDING-TOP: 11px; BORDER-BOTTOM: #999999 1px solid; BACKGROUND-COLOR: #ffffff }
		</style>
	</head>
	<body bottommargin="0" topmargin="0" marginheight="0" marginwidth="0">
        <form runat="server" ID="Form1">
			<portal:Banner id="Banner" runat="server" />			
			<table width="750" border="0" cellpadding="3" cellspacing="0">			
				<tr>
					<td valign="top" id="leftcolumn">
						<!-- #include virtual="leftmenu.inc" -->			
					</td>
					<td width=20>&nbsp;</td>
					<td valign="top" id="content">
						<h1>Managing Contacts</h1>	
						<h4>Block others from seeing or contacting you</h4>
						<p>
							You can control who sees your status and who sends you instant messages. This helps preserve your privacy and protect you from unwanted contact.
						</p>
						<br>						
						<p>
							<li>In the conversation window, click the <img src="../images/im_blocked.png" alt="Block" align=absmiddle> <b>Block</b> button .
							In your contact list, the status of the contact changes to <b>Blocked</b>.</li>
							<li>To unblock a contact, click the <b>Unblock</b> button.</li>
						</p>
	
						<br>
						<table>
							<tr>
								<td><img src="images/block-user1.gif" border="0" align="middle">
								</td>
							</tr>
							<tr>
								<td><img src="images/block-user2.gif" border="0" align="middle">
								</td>
							</tr>
						</table>
					</td>
				<tr>
			</table>	
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>			
		</form>
	</body>
</html>