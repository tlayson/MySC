<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" %>
<html>
	<head>
		<title>Introduction - No. 1 ASP.NET Chat Software</title>
		<link href="style.css" type="text/css" rel="stylesheet" />
		<meta name="KEYWORDS" content="ASP.NET Chat, ASP.NET Chat application, Chat Software">
		<meta name="DESCRIPTION" content="Introduction - No. 1 ASP.NET Chat Software">
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
						<h1>Admin Interface Overview</h1>
						<h4>Introduction</h4>						
						An Admin (Administrator) <img src="../Images/moderator.gif" align="absMiddle"  alt="" />  is the highest level of staff, and has the ability to access any function of the chat software. 


						<br><br>
						<img alt="ASP.NET Chat" src="images/Admin-Interface.gif" border="0" />
						<br>
						<br /><br />
					</td>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2005 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</html>
