<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" %>
<html>
	<head>
		<title>How to participate in the chat - No. 1 ASP.NET Chat Software</title>
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
						<h4>Entering a Chat Room</h4>						
						<p>
							After logging in with your registered user name or as a guest, you will be presented with the different chat rooms that are available. The list also shows how many people are currently chatting. 
							<br><br>
							Click on the appropriate chat room you wish to enter. You are now in the chat room.
						</p><br /><br />
						<p><img alt="Entering a Chat Room" src="images/Entering-Chat-Room.gif" border="0" align="middle" /></p>
						<br /><br />
					</td>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</html>
