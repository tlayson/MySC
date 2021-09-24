<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" %>
<html>
	<head>
		<title>Sending messages to a user - No. 1 ASP.NET Chat Software</title>
		<link href="style.css" type="text/css" rel="stylesheet" />
		<meta name="keywords" content="ASP.NET Chat, ASP.NET Chat application, Chat Software">
		<meta name="description" content="Sending messages to a user - No. 1 ASP.NET Chat Software">
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
						<h4>Sending messages to a user</h4>
						<p>
							The messages are still public, but directed to a particular user. In a face-to-face conversation with several people, you sometimes look at a particular person when you're talking. <br><br>
							If you are chatting in a room and want to send messages to one of the other chatters, click on his/her name in the user list or select the user nickname from the dropdown menu at the bottom of the window.
						</p>
						<br /><br />
						<p>
							<img src="images/select-username.gif" border="0" />
						</p>
						<p>
							<img src="images/click-username.gif" border="0" />
						</p>
						<p>
							<img src="images/still-public.gif" border="0" />
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
