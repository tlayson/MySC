<%@ Page Language="C#" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>

<html>
  <head>
		<title>Register - No. 1 ASP.NET Instant Messenger</title>
		<link href="style.css" type="text/css" rel="stylesheet">
		<meta name="keywords" content="Instant Messaging,Instant Messenger,Web Messenger,ASP.NET Web Messenger,ASP.NET,Web-Based Instant Messenger,Web-Based Messenger">
		<meta name="description" content="Register - No. 1 ASP.NET Instant Messenger">
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
						<h4>Register</h4>
						<p>To use Cute Web Messenger in web sites, you need to register your paticular user name. Registering your nickname creates a profile for you and prevents other users from using that nickname, as the profile is secured by a password that you choose.</p>
 						<br>
						<p>If there's a "register" button at the top menu, you can click it to enter registration form.</p>
						<br>
						<P>You will be taken to a screen where you can enter a "user", "password","re-password", "email" or other informations.
						Be careful that "user","password","re-passowrd" must be filled and "password" should be identical with "re-password". If you input an email address, it should be in the valid format with an "@".
						After that, please push button "Register".<br><br>
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