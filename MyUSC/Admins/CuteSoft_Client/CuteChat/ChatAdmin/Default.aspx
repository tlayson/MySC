<%@ Import Namespace="CuteChat" %>
<%@ Page Language="C#" Inherits="CuteChat.ChatAdminPage" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Menu" Src="Menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
    <head runat="server" ID="Head1">
		<link rel="stylesheet" href="style.css" />    
		<title>Cute Chat Administration Console</title>   
    </head>
    
	<body bottommargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form runat="server" ID="Form2">
			<uc1:Banner id="banner1" runat="server" />
			<table width="750" border="0" cellpadding="3" cellspacing="0">
				<tr>
					<td id="leftcolumn" valign="top">
						<uc1:Menu id="Menu1" runat="server"></uc1:Menu>
					</td>
					<td width="10">&nbsp;</td>
					<td valign="top" id="content">
						<h1>Getting Started</h1>
						
						<h4>Introduction</h4>						
						<p>
							Cute Chat is a full-featured <strong>chat software</strong> which allows website owners to add chat rooms to their website. It includes features such as high load support, Moderated Chat, font/color/ customization, emoticons, private messaging, private chat room, profanity filtering, ignoring users, file Transfer, and many more! 
                   		</p>
						<br>
						<p>This <strong>Administration Console</strong> is a web based control panel that lets you 
						control or monitor almost every aspect of your chat room. Different pages 
						within the control panel perform various functions:
						</p>
						<br>
						<table cellpadding="10" cellspacing="1" border="0" width="100%" bgcolor="#dddddd">
							<tr bgColor="#ffffff">
								<td valign="top"><img src="../images/cup.gif" border="0" alt="Room Administration" /></td>
								<td valign="top">									
								<b>Room Administration</b><br>
								Chat Room Administration allows you to add one or more chat rooms to your site 
								and control access to your rooms. You can add public, restricted and password 
								protected chat rooms.
								</td>
							</tr>
							<tr bgColor="#f5f5f5">
								<td valign="top"><img src="../images/setting.gif" border="0" alt="Configuration"/></td>
								<td valign="top">
									<b>Configuration</b>
									<br>
									The Configuration Tab allows an Admin to configure a number of different 
									settings that affect the appearance and the functionality of the Chat Room.
								</td>
							</tr>
							<tr bgColor="#ffffff">
								<td valign="top"><img src="../images/find.gif" border="0" alt="Bad word filter" /></td>
								<td valign="top">
									<b>Bad word filter</b><br>
									The profanity/word filter Tab allows you to remove words that you feel would be 
									inappropriate for your chatters to use.
								</td>
							</tr>
							<tr bgColor="#f5f5f5">
								<td valign="top"><img src="../images/icon_ban_allow.gif" border="0" alt="Deny & Allow IP" /></td>
								<td valign="top">
									<b>Deny & Allow IP</b><br>In some situations, you may want to only allow people with specific IP 
									addresses to access your chat room or you may want to ban certian IP addresses  (for example, keeping disruptive users out of chat rooms).
								</td>
							</tr>
							<tr bgColor="#ffffff">
								<td valign="top"><img src="../images/archive.gif" border="0" alt="View/delete/export Chat Log"/></td>
								<td valign="top">
									<b>View/delete/export Chat Log </b><br>
									With Cute Chat, all records are recorded and stored in a database for further retrieval and auditing.
									The View/delete/export chat log Tab allows the system administrator to view, delete or export log file. 

								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
    </body>
</html>