<%@ Page Language="C#" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>

<html>
  <head>
		<title>Change your status - ASP.NET Web Messenger</title>
		<link href="style.css" type="text/css" rel="stylesheet">
		<meta name="keywords" content="Instant Messaging,Instant Messenger,Web Messenger,ASP.NET Web Messenger,ASP.NET,Web-Based Instant Messenger,Web-Based Messenger">
		<meta name="description" content="Requirements - ASP.NET Web Messenger">
		<style>
			.noteBox { BORDER-RIGHT: #999999 1px solid; PADDING-RIGHT: 11px; BORDER-TOP: #999999 1px solid; PADDING-LEFT: 11px; PADDING-BOTTOM: 11px; BORDER-LEFT: #999999 1px solid; WIDTH: 160px; PADDING-TOP: 11px; BORDER-BOTTOM: #999999 1px solid; BACKGROUND-COLOR: #ffffff }
		</style>
	</head>
	<body bottommargin="0" topmargin="0" marginheight="0" marginwidth="0">
        <form runat="server" ID="Form1">
			<portal:Banner id="Banner" runat="server" />			
			<table width="850" border="0" cellpadding="3" cellspacing="0">			
				<tr>
					<td valign="top" id="leftcolumn">
						<!-- #include virtual="leftmenu.inc" -->			
					</td>
					<td width=20>&nbsp;</td>
					<td valign="top" id="content">
						<h1>Configuring Cute Web Messenger</h1>	
						<h4>Change your status</h4>
						<p>Updating your status is an easy way to let your contacts know at a glance what you're doing and if you're available for a conversation. 
							Each status setting has a different meaning or function:
							<ul style="">
								<li><b>Offline</b>: You are unavailable for messaging. This setting is automatically applied when you sign out of Cute Web Messenger. Offline contact icons are grey and appear in the <b>Offline</b> section of your contact list.</li>
								<li><b>Online</b>: You are available for messaging. This setting is automatically applied when you sign in to Cute Web Messenger. Online contact icons are appear in the <b>Online</b> section of your contact list.</li>
								<li><b>Busy</b>: You are online, but don't want to be disturbed. If your status is set to <b>Busy</b>, you are not alerted to incoming messages.</li>
								<li><b>Be Right Back</b>: You are online, but away from your computer for a short time.</li>
								<li><b>Away</b>: You are online, but away from your computer for an unspecified period of time.</li>
								<li><b>On the Phone</b>: You are online, but on the phone and do not want to be disturbed. If your status is set to <b>On the Phone</b>, incoming messages will not generate an alert sound on your desktop.</li>
								<li><b>Out to Lunch</b>: You are online, but away from your computer.</li>
								<li><b>Appear Offline</b>: You are online, able to view your contacts, and able to initiate conversations; however, you will appear offline to your contacts and no one will be able to initiate a conversation with you.</li>
							</ul>
						</p>
						<br>
						<p>To change your status, in the Cute Web Messenger window, in the My Status box, select the option that best describes your status. </p>
						<br>						
					</td>
					<td width="80"><br><br><P><img src="images/Changing-status.gif" border="0" align="middle"></p>	</td>
				<tr>
			</table>	
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>			
		</form>
	</body>
</html>