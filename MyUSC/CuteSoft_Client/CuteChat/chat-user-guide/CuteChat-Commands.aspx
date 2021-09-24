<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" %>
<html>
	<head>
		<title>Chat Commands - No. 1 ASP.NET Chat Software</title>
		<link href="style.css" type="text/css" rel="stylesheet" />
		<meta name="keywords" content="ASP.NET Chat, ASP.NET Chat application, Chat Software">
		<meta name="description" content="Chat Commands - No. 1 ASP.NET Chat Software">
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
						<h1>Chatting</h1>
<h4>Chat Commands</h4>
<div>Cute Chat uses a lot of command lines to perform its functions. When you want to enter a command, the very first character in the editbox must be a slash '/' followed by the command word. There&nbsp;is no space between the '/' prefix and the command word. </div>
<div>&nbsp;</div>
<div>This tutorial will cover the commands you may be likely to use at the command line. </div>
<br>
<p valign="bottom">
<table class="nstexttable" id="Table2" cellspacing="4" cellpadding="4" width="100%" bgcolor="#ffffff" border="0">
    <tbody>
        <tr bgcolor="#eeeeee">
            <td style="width: 212px" valign="top">Command</td>
            <td valign="top">Description </td>
        </tr>
        <tr>
            <td style="width: 212px">/help </td>
            <td>If you've forgotten how to do something, the /help command will show you&nbsp;a concise list of the most frequently used&nbsp;commands. </td>
        </tr>
        <tr>
            <td style="width: 212px">/clear </td>
            <td>
            <div>If the discussion gets too long, you can clear the screen by typing /clear. </div>
            </td>
        </tr>
        <tr>
            <td style="width: 212px">
            <p>/avatars</p>
            </td>
            <td>
            <div>Avatars are small graphical images that&nbsp;are displayed by your username in the chat rooms. You can select the image that best describes yourself by typing /avatars.</div>
            </td>
        </tr>
        <tr>
            <td style="width: 212px">&nbsp;/msg DisplayName </td>
            <td valign="top">
            <div>The /msg command will send a private message to the person named which no others in the room can see. </div>
            <div>&nbsp;</div>
            <div>Example:</div>
            <div>/msg Robert </div>
            </td>
        </tr>
        <tr>
            <td style="width: 212px">/invite DisplayName</td>
            <td valign="top">
            <div>Invite another to join the private window; Only can be used in the private window. </div>
            <div>&nbsp;</div>
            <div>
            <div>Example:</div>
            <div>/invite Jim</div>
            </div>
            </td>
        </tr>
        <tr>
            <td style="width: 212px">/admin</td>
            <td valign="top">
            <div>Open the administration windows. Only can be used by room adminstrators. </div>
            </td>
        </tr>
        <tr>
            <td style="width: 212px">/admin kick DisplayName</td>
            <td valign="top">
            <p>Kicks a member out of the room! Only can be used by room adminstrators.</p>
            </td>
        </tr>
        <tr>
            <td style="width: 212px">/admin setpassword passwordstring</td>
            <td valign="top">
            <p>Set a password on the room. Only can be used by room adminstrators.</p>
            </td>
        </tr>
        <tr>
            <td style="width: 212px">/admin setpassword</td>
            <td valign="top">
            <p>Remove a room password. Only can be used by room adminstrators.</p>
            </td>
        </tr>
    </tbody>
</table>
</p>

					</td>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</html>
