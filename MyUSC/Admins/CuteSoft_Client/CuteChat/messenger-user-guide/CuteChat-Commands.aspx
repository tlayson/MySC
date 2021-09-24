<%@ Page Language="C#" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>

<html>
  <head>
		<title>CuteChat Commands - No. 1 ASP.NET Chat Pro</title>
		<link href="style.css" type="text/css" rel="stylesheet">
		<meta name="keywords" content="Instant Messaging,Instant Messenger,Web Messenger,ASP.NET Web Messenger,ASP.NET,Web-Based Instant Messenger,Web-Based Messenger">
		<meta name="description" content="Register - No. 1 ASP.NET Chat Pro">
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
						<h1>Chatting</h1>	
						<h4>CuteChat Commands</h4>
						<DIV>
				CuteChat uses a lot of command lines to perform its functions. When you want to 
				enter a command, the very first character in the editbox must be a slash '/' 
				followed by the command word. There&nbsp;is no space between the '/' prefix and 
				the command word.
			</DIV>
			<DIV>&nbsp;</DIV>
			<DIV>This tutorial will cover the commands you may be likely to use at the command 
				line.
			</DIV>
			<DIV><FONT color="#0099ff" size="+1"><A id="formatting" name="formatting"></A> </FONT>
				&nbsp;</DIV>
			<P valign="bottom">
				<TABLE class="nstexttable" id="Table2" cellSpacing="4" cellPadding="4" width="100%" bgColor="#ffffff"
					border="0">
					<TBODY>
						<TR bgColor="#edf1fa">
							<TD vAlign="top" style="WIDTH: 212px">Command</TD>
							<TD vAlign="top">
								Description
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 212px">/help
							</TD>
							<TD>If you’ve forgotten how to do something, the /help command will show you&nbsp;a 
								concise list of the most frequently used&nbsp;commands.
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 212px">/clear
							</TD>
							<TD>
								<DIV>If the discussion gets too long, you can clear the screen by typing /clear.
								</DIV>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 212px">
								<P>/avatars</P>
							</TD>
							<TD>
								<DIV>
									Avatars are small graphical images that&nbsp;are displayed by your username in 
									the chat rooms. You can select the image that best describes yourself by typing 
									/avatars.</DIV>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 212px">&nbsp;/msg DisplayName
							</TD>
							<TD vAlign="top">
								<DIV>The /msg command will send a private message to the person named which no 
									others in the room can see.
								</DIV>
								<DIV>&nbsp;</DIV>
								<DIV>Example:</DIV>
								<DIV>/msg Robert
								</DIV>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 212px">/invite DisplayName</TD>
							<TD vAlign="top">
								<DIV>
									Invite another to join the private window; Only can be used in the private 
									window.
								</DIV>
								<DIV>&nbsp;</DIV>
								<DIV>
									<DIV>Example:</DIV>
									<DIV>/invite Jim</DIV>
								</DIV>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 212px">/admin</TD>
							<TD vAlign="top">
								<DIV>
									Open the administration windows. Only can be used by room adminstrators.
								</DIV>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 212px">/admin kick DisplayName</TD>
							<TD vAlign="top">
								<P>Kicks a member out of the room! Only can be used by room adminstrators.</P>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 212px">/admin setpassword passwordstring</TD>
							<TD vAlign="top">
								<P>Set a password on the room. Only can be used by room adminstrators.</P>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 212px">/admin setpassword</TD>
							<TD vAlign="top">
								<P>Remove a room password. Only can be used by room adminstrators.</P>
							</TD>
						</TR>
					</TBODY>
				</TABLE>
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