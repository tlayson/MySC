<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" %>
<html>
	<head>
		<title>Changing Avatar - No. 1 ASP.NET Chat Software</title>
		<link href="style.css" type="text/css" rel="stylesheet" />
		<meta name="keywords" content="ASP.NET Chat, ASP.NET Chat application, Chat Software">
		<meta name="description" content="Changing Avatar - No. 1 ASP.NET Chat Software">
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
						<h1>Chatting</h1>	
						<h4>Changing Avatar</h4>
						<P>
							Avatars are small icons or graphics that represent you.  You can choose an avatar to appear next to your nickname in the room’s member list and next to every message you type into the room.  <br><br>
							To change the avatar:<br><br>
							1. Click the control panel button.<BR>
							2. Click "Pick an Avatar" option in the control panel menu. <BR>
							3. The Avatar pop-up window lists the available Avatars. Select the Avatar you like.  <BR>
						</P>
						<br /><br />
						<img src="images/Changing-Avatar.gif" border=0>
						<img src="images/Changing-Avatar2.gif" border=0>
						<br /><br />
					</td>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</html>
