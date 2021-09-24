<%@ Page Language="C#" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>

<html>
  <head>
		<title>Save a conversation - ASP.NET Web Messenger</title>
		<link href="style.css" type="text/css" rel="stylesheet">
		<meta name="keywords" content="Instant Messaging,Instant Messenger,Web Messenger,ASP.NET Web Messenger,ASP.NET,Web-Based Instant Messenger,Web-Based Messenger">
		<meta name="description" content="Register - ASP.NET Web Messenger">
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
						<h1>Sending and Receiving Instant Messages</h1>	
						<h4>Save a conversation</h4>
						<p>
						1. Click the control panel button.<br><br>
						2. Click "Save the chat log" option in the control panel menu. <br><br>
						3. Double-click the folder you want to save the conversation in.<br><br>
						4. In the File name box, type a name for the conversation, and then click Save.<br><br>
						</P>
						<br>
						<table width=90% border=0 cellpadding=0 cellspacing=3>
							<tr>
								<td valign="top">
									<img src="images/Save-conversation.gif" border=0>
								</td>
								<td width="5">
								</td>
								<td valign="top">
									<img src="images/Save-conversation2.gif" border=0>
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