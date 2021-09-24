<%@ Register TagPrefix="portal" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" %>
<html>
	<head>
		<title>Text Formatting - No. 1 ASP.NET Chat Software</title>
		<link href="style.css" type="text/css" rel="stylesheet" />
		<meta name="keywords" content="ASP.NET Chat, ASP.NET Chat application, Chat Software">
		<meta name="description" content="Text Formatting - No. 1 ASP.NET Chat Software">
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
<h4>Text Formatting</h4>
<p>To spice up your chat Messages, you may want to make some text formatting changes. You can make the formatting change before you type anything and the new style will appear when you begin typing. </p>
<br />
<table class="nstexttable" id="Table2" cellspacing="4" cellpadding="4" width="100%" bgcolor="#ffffff" border="0">
    <tbody>
        <tr bgcolor="#eeeeee">
            <td style="width: 148px" valign="top">Menu/Button</td>
            <td valign="top">Function Description </td>
        </tr>
        <tr>
            <td style="width: 148px"><img hspace="0" src="images/bold.gif" align="baseline"  alt="" /> </td>
            <td>The "B" icon represents bold text. You don't have to choose any words in your message to change it bold, just click this "B" icon and your message become bold. Click it again to change it back to normal text. </td>
        </tr>
        <tr>
            <td style="width: 148px"><img hspace="0" src="images/italic.gif" align="baseline"  alt="" /> </td>
            <td>
            <div>The "I" icon represents italic text. You don't have to choose any words in your message to change it italic, just click this "I" icon and your message become italic. Click it again to change it back to normal text. </div>
            </td>
        </tr>
        <tr>
            <td style="width: 148px">
            <p><img hspace="0" src="images/underline.gif" align="baseline"  alt="" /></p>
            </td>
            <td>
            <div>The "U" icon represents underline text. You don't have to choose any words in your message to change it underline, just click this "U" icon and your message become underline. Click it again to change it back to normal text.</div>
            </td>
        </tr>
        <tr>
            <td style="width: 148px"><img hspace="0" src="images/colourpick.gif" align="baseline"  alt="" /> </td>
            <td valign="top">
            <div>The color picker button&nbsp;is for choosing colors. You don't have to choose any words in your message to change its color, just click this color block to prompt a color-window and click the color you prefer. </div>
            </td>
        </tr>
        <tr>
            <td style="width: 148px"><img hspace="0" src="images/fontdropdown.gif" align="baseline"  alt="" /></td>
            <td valign="top">
            <div>The font face drop-down set the font face. If a selection is active, the font will be applied to it. You don't have to choose any words in your message to set the font face, just click the font face you prefer. </div>
            </td>
        </tr>
        <tr>
            <td style="width: 148px"><img hspace="0" src="images/sizedropdown.gif" align="baseline"  alt="" /></td>
            <td valign="top">
            <div>The font size drop-down set the font size. If a selection is active, the font size will be applied to it. You don't have to choose any words in your message to set the font size, just click the font size you prefer. </div>
            </td>
        </tr>
    </tbody>
</table>

					</td>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</html>
