<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" %>
<html>
	<head>
		<title>Introduction - No. 1 ASP.NET Chat Software</title>
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
						<h1>Getting Started</h1>
						<h4>Introduction</h4>						
						<p>
						   Welcome to the web's finest, online chat service. 
						   <br /><br />
						   The <b>Cute Chat</b> software includes features such as high load support, font/color/ customization, emoticons, private messaging, private chat room, profanity filtering, ignoring users, file Transfer, and many more! 
                           <br /><br />
                           This <b>Ajax</b> based chat system has been the choice of the leading web sites, from around the world, from small to largest Portals for community building, distance learning, live events, romance and dating, online business meetings, support groups, online sales, help desks, chat room hosting, and more . 
                           <br /><br />
                           This guide will provide a brief summary of many of the features that you presently have access to. 
						   <br /><br />						   
						</p>
						<p><img alt="ASP.NET Chat" src="images/cutechat_mainbanner2.gif" border="0" align="middle" /></p>
						<br />
					    Click one of the items below to learn more about Cute Chat.
					    <br />
                        <br />
                        <table cellspacing="3" cellpadding="2" border="0">
                            <tbody>
                                <tr>
                                    <td width="16"><img src="images/read2.gif" align="center" border="0"  alt="" /> </td>
                                    <td><a href="Sendingmessages.aspx">Sending messages </a><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td><img src="images/read2.gif" align="center" border="0"  alt="" /> </td>
                                    <td><a href="SendingPrivateMessages.aspx">Whisper to other people</a><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td><img src="images/read2.gif" align="center" border="0"  alt="" /> </td>
                                    <td><a href="PrivateChat.aspx">Private Chat Window </a><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td><img src="images/read2.gif" align="center" border="0"  alt="" /> </td>
                                    <td><a href="Chat-Acronyms.aspx">Chat Acronyms</a><br />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
						<br /><br />
					</td>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</html>
