<%@ Page Language="C#" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>

<html>
  <head>
		<title>Sending and Receiving Instant Messages - ASP.NET Web Messenger</title>
		<link href="style.css" type="text/css" rel="stylesheet">
		<meta name="keywords" content="Instant Messaging,Instant Messenger,Web Messenger,ASP.NET Web Messenger,ASP.NET,Web-Based Instant Messenger,Web-Based Messenger">
		<meta name="description" content="Sending and Receiving Instant Messages - ASP.NET Web Messenger">
		<style>
			.noteBox { BORDER-RIGHT: #999999 1px solid; PADDING-RIGHT: 11px; BORDER-TOP: #999999 1px solid; PADDING-LEFT: 11px; PADDING-BOTTOM: 11px; BORDER-LEFT: #999999 1px solid; WIDTH: 160px; PADDING-TOP: 11px; BORDER-BOTTOM: #999999 1px solid; BACKGROUND-COLOR: #ffffff }
		</style>
	</head>
	<body bottommargin="0" topmargin="0" marginheight="0" marginwidth="0">
        <form runat="server" ID="Form1">
			<portal:Banner id="Banner" runat="server" />			
			<table width="750" border="0" cellpadding="3" cellspacing="0">			
				<tr>
					<td valign="top" id="leftcolumn">
						<!-- #include virtual="leftmenu.inc" -->			
					</td>
					<td width=20>&nbsp;</td>
					<td valign="top" id="content">
						<h1>Sending and Receiving Instant Messages</h1>	
						<h4>Sending-Messages</h4>
						<p>
							To send an instant message to a contact:
						</p>
						<br>						
						<p>
							<li>In the main Web Messenger window, click the name of the person you want to talk to. </li>
							<li>In the box at the bottom of the conversation window, type your message. </li>
							<li>Click Send. </li>
						</p>
	
	
						<br>
						<table>
							<tr>
								<td><img src="images/send-message.jpg" border="0" align="middle">
								</td>
								<td><img src="images/send-message2.jpg" border="0" align="middle">
								</td>
							</tr>
						</table>
		
					</td>
				<tr>
			</table>	
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>			
		</form>
	</body>
</html>