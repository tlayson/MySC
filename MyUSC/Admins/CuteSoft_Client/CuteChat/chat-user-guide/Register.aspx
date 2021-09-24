<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" %>
<html>
	<head>
		<title>Register - No. 1 ASP.NET Chat Software</title>
		<link href="style.css" type="text/css" rel="stylesheet" />
		<meta name="keywords" content="ASP.NET Chat, ASP.NET Chat application, Chat Software">
		<meta name="description" content="Introduction - No. 1 ASP.NET Chat Software">
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
						<h1>Join a Chat</h1>
						<h4>Register</h4>						
						<p>When you visit Cute Chat room , you may need to register your paticular user name. Registering your nickname creates a profile for you and prevents other users from using that nickname, as the profile is secured by a password that you choose.</p>
 						<br>
 						<p>Most of the chat rooms allow registration while some of them don't. (In latter case, please contact webmaster to seek help.)</p>
						<br>
						<p>If there's a "register" button at the top menu, you can click it to enter registration form.</p>
						<br>
						<P>You will be taken to a screen where you can enter a "user", "password","re-password", "email" or other informations.
						Be careful that "user","password","re-passowrd" must be filled and "password" should be identical with "re-password". 
						After that, please push button "Register".<br><br>
						</P>
						<br /><br />
					</td>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</html>
