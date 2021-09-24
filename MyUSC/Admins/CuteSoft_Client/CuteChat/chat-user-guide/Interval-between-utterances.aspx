<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" %>
<html>
	<head>
		<title>Interval between utterances - No. 1 ASP.NET Chat Software</title>
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
						<h4>Interval between utterances</h4>
						<h4>Interval between utterances</h4>
						<p>
							When one chat user sends too many messages at one time, either in open chat or in private message, Flooding occurs. Cute Chat has a Flood Control feature that automatically ignores a user when flooding occurs. <br><br>
							The Interval between utterances is defined by administrator and the default value is one second.<br><br>
							Sometimes, you may get carried away writing a long message and flood accidentally, but users who flood deliberately in an attempt to disrupt the chat or the chat server will be banned by the administrator. 
							<br><br>
							<img src="images/flooding-control.gif" border=0>
						</P>
					</td>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</html>
