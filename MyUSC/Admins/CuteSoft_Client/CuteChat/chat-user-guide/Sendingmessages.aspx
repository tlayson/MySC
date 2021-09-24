<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" %>
<html>
	<head>
		<title>Sending messages - No. 1 ASP.NET Chat Software</title>
		<link href="style.css" type="text/css" rel="stylesheet" />
		<meta name="keywords" content="ASP.NET Chat, ASP.NET Chat application, Chat Software">
		<meta name="description" content="Sending messages - No. 1 ASP.NET Chat Software">
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
						<h4>Sending messages</h4>
						<p>
							After entering a room, you can type a message in the input field near the bottom of the chat area. Press Enter or click Send to send the message. Your message then appears on the chat screen preceded by your username. <br><br>
							
							The message will be reaching all the users online in the same room. <br><br>
							Press Ctrl+Enter to start a new line within your current message. <br><br>
							<img src="images/Sending-messages.gif">
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
