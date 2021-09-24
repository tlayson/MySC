<%@ Page Language="c#" Inherits="CuteChat.ChatFramePage" AutoEventWireup="false" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="CuteChat" %>
<script runat="server">
string msghtml;
override protected void OnLoad(EventArgs args)
{
	base.OnLoad(args);
	
	msghtml=GetMessageHtml();
}

</script>
<html>
	<HEAD runat="server">
		<title>Top</title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
		<meta http-equiv="REFRESH" content="5;URL=<%=Server.HtmlEncode(ReloadURL)%>" />
		<script><!--
		setTimeout(RedirectRefresh,4000);
		function RedirectRefresh()
		{
			location.href="<%=ReloadURL%>";
		}
		--></script>
		<style>
			#ContentPanel {
				border: 1px solid #aaaaaa; margin-left:10px; overflow-y:scroll;width:100%;height:100%;background-color: #ffffff;padding:8px;	
			}
		</style>
	</head>
	<link rel="stylesheet" href="style.css" />
	<link rel="stylesheet" href='Skins/<%=ChatWebUtility.SkinName%>/style.css' />
	<BODY style="margin: 0px;padding: 0px;">
		<div id="ContentPanel">
			<div id="MessageList"><%=msghtml%></div>
		</div>
	</BODY>
	<script>
	try
	{
		window.parent.inputframe.document.forms['Form1'].TextBox1.focus();
	}
	catch(x)
	{
	}
	</script>
</html>
