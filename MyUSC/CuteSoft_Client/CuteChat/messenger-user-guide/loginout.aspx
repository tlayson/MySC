<%@ Page Language="C#" %>	
<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>

<html>
  <head>
		<title>Logging In/Out - Web Messenger</title>
		<link href="style.css" type="text/css" rel="stylesheet">
		<meta name="keywords" content="Instant Messaging,Instant Messenger,Web Messenger,ASP.NET Web Messenger,ASP.NET,Web-Based Instant Messenger,Web-Based Messenger">
		<meta name="description" content="Logging In/Out - Web Messenger">
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
						<h1>Signing In</h1>	
						<h4>Logging In/Out</h4>
						<p>
							<b>Logging In</b><br><br>
								On the Start Cute Web Messenger page:<br>
								<OL type=1>
									<LI>In the <B>Sign in as</B> list, click the option that best describes the 
										status you want others to see when you sign in. 
									</li>
									<LI>Click <B>Sign in</B> button. 
									</li>
								</ol>
							<b>Logging Out</b><br><br>
							If you wish to exit the Cute Web Messenger at any time, simply click the Logout button. On many systems after logging out you will automatically be taken to another web page, on others, you will have the option of logging back in again.
						</P>
						<br>
						<P><img src="images/messenger-login.gif" border="0" align="middle" title="Logging In"><img hspace="8" src="images/messenger-logout.gif" border="0" align="middle" title="Logging out">
						</p>						
					</td>
				<tr>
			</table>	
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>			
		</form>
	</body>
</html>