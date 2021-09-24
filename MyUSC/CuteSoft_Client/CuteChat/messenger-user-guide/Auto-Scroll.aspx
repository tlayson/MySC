<%@ Page Language="C#" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>

<html>
  <head>
		<title>Auto focus - ASP.NET Web Messenger</title>
		<link href="style.css" type="text/css" rel="stylesheet">
		<meta name="keywords" content="Instant Messaging,Instant Messenger,Web Messenger,ASP.NET Web Messenger,ASP.NET,Web-Based Instant Messenger,Web-Based Messenger">
		<meta name="description" content="Register - ASP.NET Web Messenger">
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
						<h4>Auto Scroll</h4>
						<p>
							Auto scroll is the feature that ensures you are always looking at the most recent events in the chat room. Sometimes you may wish to look back upon previous messages entered into the chatroom. Without being annoyed by Auto scroll, you can optionally turn on/off this feature.</p>
						<br>
						<p>
						To configure "Auto Scroll" option: 
						<OL>
							<LI>Click the control panel button in the Cute Messenger toolbar. 
							<P></P>
							<LI>Click "Auto Scroll" to turn on/off this feature.</SPAN> 
						</OL>
						</P>
						<br>
						<img src="images/Auto-scroll.gif" border=0>
						
					</td>
				<tr>
			</table>	
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>			
		</form>
	</body>
</html>