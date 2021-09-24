<%@ Page Language="C#" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>

<html>
  <head>
		<title>Text Formatting - ASP.NET Web Messenger</title>
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
						<h4>Text Formatting</h4>
						<p>
							To spice up your chat Messages, you may want to make some text formatting changes. You can make the formatting change before you type anything and the new style will appear when you begin typing. 
						</p>
						<br>
						<TABLE class="nstexttable" id="Table2" cellSpacing="4" cellPadding="4" width="100%" bgColor="#ffffff"
					border="0">
					<TBODY>
						<TR bgColor="#edf1fa">
							<TD vAlign="top" style="WIDTH: 148px">Menu/Button</TD>
							<TD vAlign="top">Function Description
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 148px"><IMG hspace="0" src="images/bold.gif" align="baseline">
							</TD>
							<TD>The "B" icon represents bold text. You don't have to choose any words in your 
								message to change it bold, just click this "B" icon and your message become 
								bold. Click it again to change it back to normal text.
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 148px"><IMG hspace="0" src="images/italic.gif" align="baseline">
							</TD>
							<TD>
								<DIV>The "I" icon represents italic text. You don't have to choose any words in 
									your message to change it italic, just click this "I" icon and your message 
									become italic. Click it again to change it back to normal text.
								</DIV>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 148px">
								<P><IMG hspace="0" src="images/underline.gif" align="baseline"></P>
							</TD>
							<TD>
								<DIV>The "U" icon represents underline text. You don't have to choose any words in 
									your message to change it underline, just click this "U" icon and your message 
									become underline. Click it again to change it back to normal text.</DIV>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 148px"><IMG hspace="0" src="images/colourpick.gif" align="baseline">
							</TD>
							<TD vAlign="top">
								<DIV>The color picker button&nbsp;is for choosing colors. You don't have to choose 
									any words in your message to change its color, just click this color block to 
									prompt a color-window and click the color you prefer. 
								</DIV>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 148px"><IMG hspace="0" src="images/fontdropdown.gif" align="baseline"></TD>
							<TD vAlign="top">
								<DIV>The font face drop-down set the font face. If a selection is active, the font 
									will be applied to it. You don't have to choose any words in your message to 
									set the font face, just click the font face you prefer. 
								</DIV>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 148px"><IMG hspace="0" src="images/sizedropdown.gif" align="baseline"></TD>
							<TD vAlign="top">
								<DIV>The font size drop-down set the font size. If a selection is active, the font 
									size will be applied to it. You don't have to choose any words in your message 
									to set the font size, just click the font size you prefer. 
								</DIV>
							</TD>
						</TR>
					</TBODY>
				</TABLE>
						
					</td>
				<tr>
			</table>	
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>			
		</form>
	</body>
</html>