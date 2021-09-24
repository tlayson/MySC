<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" %>
<html>
	<head>
		<title>Private Chat Window - No. 1 ASP.NET Chat Software</title>
		<link href="style.css" type="text/css" rel="stylesheet" />
		<meta name="keywords" content="ASP.NET Chat, ASP.NET Chat application, Chat Software">
		<meta name="description" content="Private Chat Window - No. 1 ASP.NET Chat Software">
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
						<h4>Private Chat Window</h4>
						<p>
							Private Chat Window is used for a longer, private conversation taking place in it’s own window along side the room chat. 
							<br><br>
							Chatters are able to open a private window to any other chatter in the same channel to talk one on one. 
							<br><br>
							To initiate a private chat window, either right-click the user's nickname in the room user list, and select the "Private Chat" option or double-click the user's nickname directly to activate a private window with him or her. 
							<br><br>
							If someone initiates a private chat window with you, an "Accept" button will be displayed. By clicking the "Accept" button, you will accept the private chat and get in the chat window.
							<br><br>
							<table width=90% border=0 cellpadding=0 cellspacing=3>
								<tr>
									<td>
										<img src="images/private-chat.gif" border=0>
									</td>
									<td width="5">
									</td>
									<td valign="top">
										<img src="images/private-Accept.gif" border=0>
									</td>
								</tr>
								<tr>
									<td colspan="3">
										<img src="images/private-chat-window.gif" border=0>
									</td>
								</tr>
								<tr>
									<td colspan="3">
										<img src="images/private-chat-window-firefox.gif" border=0>
									</td>
								</tr>
							</table>
						</P>
					</td>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</html>
