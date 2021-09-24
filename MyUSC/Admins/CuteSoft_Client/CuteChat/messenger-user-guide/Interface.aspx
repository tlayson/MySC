<%@ Page Language="C#" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>

<html>
  <head>
		<title>Interface - No. 1 ASP.NET Chat Pro</title>
		<link href="style.css" type="text/css" rel="stylesheet">
		<meta name="keywords" content="ASP.NET,Chat,support, Live, Help,ASP.NET Chat, ASP.NET Chat application, ASP support, live support">
		<meta name="description" content="Interface - No. 1 ASP.NET Chat Pro">
	</head>
	<body bottommargin="0" topmargin="0" marginheight="0" marginwidth="0">
        <form runat="server" ID="Form1">
			<portal:Banner id="Banner" runat="server" />			
			<table width="750" border="0" cellpadding="3" cellspacing="0">			
				<tr>
					<td valign="top" id="leftcolumn">
						<!-- #include virtual="leftmenu.inc" -->			
					</td>
					<td width=15>&nbsp;</td>
					<td valign="top" id="content">
						<h1>Getting Started</h1>	
						<h4>Interface Overview</h4>
						After you have entered a room from the Login Window or via direct login, you 
will see the main chat interface. This is where you will see chat messages going 
through the chat room. You will also use it to chat with other users in the 
room. 
						<br><br>
						<IMG alt="ASP.NET Chat" src="/data/maininterface.gif" border=0>
						<br>							
						</td>
				<tr>
			</table>	
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>			
		</form>
	</body>
</html>