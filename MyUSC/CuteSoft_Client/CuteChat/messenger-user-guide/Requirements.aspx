<%@ Page Language="C#" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>

<html>
  <head>
		<title>Requirements - ASP.NET Web Messenger</title>
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
			<table width="750" border="0" cellpadding="3" cellspacing="0">			
				<tr>
					<td valign="top" id="leftcolumn">
						<!-- #include virtual="leftmenu.inc" -->			
					</td>
					<td width="20">&nbsp;</td>
					<td valign="top" id="content">
						<h1>Getting Started</h1>	
						<h4>Requirements</h4>
						<strong>Supported browsers:</strong><BR>
						<BR>
						<P>Cute Web Messenger is available to Internet Explorer versions from <strong>
								5.5</strong> up on Windows, and <strong>Firefox 0.7</strong>, <strong>Netscape 7.1</strong>, <strong>Mozilla 
								1.4</strong> or any other browser with an equivalent gecko layout engine on 
							any platform where these browsers are available. <strong>This includes Macintosh and Linux.</strong>
						</P>
						<P><BR>
							<table bgColor="#999999" cellSpacing="1" cellPadding="8" border="0">
								<tbody>
									<tr bgColor="#cccccc">
										<td vAlign="top" width="50%">
											<P><strong>Windows (all)</strong>
											</P>
										</td>
										<td vAlign="top" width="50%">
											<P><strong>Mac OS X/Linux/Unix</strong>
											</P>
										</td>
									</tr>
									<tr bgColor="#ffffff">
										<td vAlign="top" width="33%">
											<P>Internet Explorer 5.5+</P>
											<P>Netscape 7.1+</P>
											<P>Mozilla 1.4+</P>
											<P>Firefox 0.7+</P>
											<P>Or any browser based on Mozilla 1.4+</P>
										</td>
										<td vAlign="top" width="33%">
											<P>Netscape 7.1+</P>
											<P>Mozilla 1.4+</P>
											<P>Firefox 0.7+</P>
											<P>Or any browser based on Mozilla 1.4+</P>
										</td>
									</tr>
								</tbody>
							</table>
							
							<br><br>
							<p><strong>Popups enabled for this web site:</strong><p>
							<br>
							Please note that you must disable your pop-up blocker for Cute Web Messenger to work correctly. After you have logged in you will be notified if you have a blocker activated for the server you are chatting on.
						</P>
						
					</td>
				<tr>
			</table>	
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>			
		</form>
	</body>
</html>