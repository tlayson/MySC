<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" %>
<html>
	<head>
		<title>Invite others into a room - No. 1 ASP.NET Chat Software</title>
		<link href="style.css" type="text/css" rel="stylesheet" />
		<meta name="keywords" content="ASP.NET Chat, ASP.NET Chat application, Chat Software">
		<meta name="description" content="Invite others into a room - No. 1 ASP.NET Chat Software">
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
						<h4>Invite others into a room</h4>
						You might want to invite others into a private chat room. To receive your invitation, the people you are inviting must be online. 
						<br><br>
						<p>To invite others into a chat room:</p>
						<div>
							<ol type="1">
								<li>In the conversation window, click the  <img src="../images/invite.png" alt="Block" align=absmiddle> <b>Invite</b> button.</li>
								<li>Type the person's username you want to invite, and click OK. </li>
							</ol>
						</div>
						<p><img src="images/invite-user.gif" border="0" align="middle"></p><br>
					</td>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</html>
