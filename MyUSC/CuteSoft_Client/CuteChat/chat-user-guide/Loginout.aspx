<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" %>
<html>
	<head>
		<title>Logging In/Out - No. 1 ASP.NET Chat Software</title>
		<link href="style.css" type="text/css" rel="stylesheet" />
		<meta name="keywords" content="ASP.NET Chat, ASP.NET Chat application, Chat Software">
		<meta name="description" content="Introduction - No. 1 ASP.NET Chat Software">
	</head>
	<body bottommargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form runat="server" ID="Form1">
			<portal:Banner id="Banner" runat="server" />
			<table width="750" border="0" cellpadding="3" cellspacing="0">
				<tr>
					<td valign="top" nowrap id="leftcolumn">
						<!-- #include virtual="leftmenu.inc" -->
					</td>
					<td width="10">&nbsp;</td>
					<td valign="top" id="content">
						<h1>Join a Chat</h1>
						<h4>Logging In/Out</h4>						
						<p>
							<b>Logging In</b><br><br>
							When signing on to a server for the first time, the login page will be displayed. If you come back to this server, depending on how the Administrator has configured the site, it may remember the login information you used last time -- in which case you won't have to enter them again. 
							<br><br>
							<b>Logging Out</b><br><br>
							If you wish to exit the chat room at any time, simply click the Logout button. On many systems after logging out you will automatically be taken to another web page, on others, you will have the option of logging back in again.
						</p>
						<br /><br />
					</td>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</html>
