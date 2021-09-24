<%@ Page Language="C#" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>

<html>
  <head>
		<title>Whisper to other people - No. 1 ASP.NET Chat Pro</title>
		<link href="style.css" type="text/css" rel="stylesheet">
		<meta name="keywords" content="Instant Messaging,Instant Messenger,Web Messenger,ASP.NET Web Messenger,ASP.NET,Web-Based Instant Messenger,Web-Based Messenger">
		<meta name="description" content="Register - No. 1 ASP.NET Chat Pro">
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
						<h1>Chatting</h1>	
						<h4>Whisper to other people</h4>
						<p>
							In the list of users, select the people you want to whisper to. 
							<br><br>							
							In the message text box at the bottom of the window, type what you want to say, and then click the Whisper button.
							<br><br>
							Your message appears only to the people you selected.
							<br><br>
							<img src="images/whisper-check.gif" border=0>
							<br><br>
							<img src="images/private-message.gif" border=0>
						</P>
					</td>
				<tr>
			</table>	
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>			
		</form>
	</body>
</html>