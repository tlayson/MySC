<%@ Page Language="C#" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>

<html>
  <head>
		<title>Why am I receiving alerts and sounds? - ASP.NET Web Messenger</title>
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
						<h4>Why am I receiving alerts and sounds?</h4>
						<p>
							To be notified of each new message the users are provided with the option to set "Play sound". The Play sound option provides an audio cue when a new message is received only when the window is not in focus. 
						</p>
						<br>
						<p>Every user has the abillity to enable/disable the sound alerts, if he wants/does not want to hear them. </p>
						<br>
						<p>
						To configure your alerts and sounds: 
						<OL>
							<LI>Click the control panel button in the Cute Chat toolbar. 
							<P></P>
							<LI>Enable or disable the alert and sound options by click "Play Sounds".</SPAN> 
						</OL>
						</P>
						<br>
						<img src="images/Play-Sounds.gif" border=0>						
					</td>
				<tr>
			</table>	
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>			
		</form>
	</body>
</html>