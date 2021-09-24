<%@ Page Language="c#" Inherits="CuteChat.ChatFramePage" AutoEventWireup="false" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="CuteChat" %>
<html>
	<HEAD runat="server">
		<title>Top</title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
		<meta HTTP-EQUIV="Content-Type" CONTENT="text/html;">
				<meta http-equiv="REFRESH" content="15;URL=<%=Server.HtmlEncode(ReloadURL)%>" />
		<script><!--
		setTimeout(RedirectRefresh,12000);
		function RedirectRefresh()
		{
			location.href="<%=ReloadURL%>";
		}
		--></script>
	</head>
	<link rel="stylesheet" href="style.css" />
	<link rel="stylesheet" href='Skins/<%=ChatWebUtility.SkinName%>/style.css' />
	<BODY style="margin: 0px;padding: 0px;">
		<div style="border: 1px solid #aaaaaa;width:100%;height:100%;background-color: #ffffff;padding:10px">
			<div style="height:30;" class="ChannelTitle"><%=ChannelTitle%>&nbsp;<img width="19" height="19" src="images/invite.png" alt="" align="absmiddle" /></div>
			<div id="chat_onlinelist"><%=GetOnlineHtml()%></div>
		</div>
	</BODY>
</html>
