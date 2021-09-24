<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" %>
<html>
	<head>
		<title>Auto focus - No. 1 ASP.NET Chat Software</title>
		<link href="style.css" type="text/css" rel="stylesheet" />
		<meta name="keywords" content="ASP.NET Chat, ASP.NET Chat application, Chat Software">
		<meta name="description" content="Auto focus - No. 1 ASP.NET Chat Software">
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
						<h4>Auto focus</h4>
						<p>
							To be notified of each new message the users are provided with the option to set "Auto focus".  The Auto focus option sets individual message windows to automatically gain focus when a new message is received. 
						</p>
						<br>
						<p>
						To configure "Auto focus" option: 
						<OL>
							<LI>Click the control panel button in the Cute Chat toolbar. 
							<P></P>
							<LI>Click "Auto focus" to set focus option.</SPAN> 
						</OL>
						</P>
						<br>
						<img src="images/Auto-focus.gif" border=0>
					</td>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</html>
