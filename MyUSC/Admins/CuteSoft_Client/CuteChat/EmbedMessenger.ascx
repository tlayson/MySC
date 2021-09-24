<%@ Control Language="c#" AutoEventWireup="false" Inherits="CuteChat.ChatCtrlBase" %>
<%@ Register TagPrefix="uc1" TagName="MessengerAds" Src="Advertising/MessengerAds.ascx" %>
<%@ Import Namespace="CuteChat" %>

<!-- Begin EmbedMessenger.Ascx -->

<%if( ChatWebUtility.IsDownLevelBrowser){%>

<div>
Your browser do not support CuteSoft Messenger !
</div>

<%}else{%>

<table style="width:420px;height:480px;">
	<tr>
		<td id="td_messenger_container">
			<div id="messenger_ads_id">
				<uc1:MessengerAds id="MessengerAds1" runat="server"></uc1:MessengerAds>
			</div>
		</td>
	</tr>
</table>

<link rel=stylesheet href='<%=ResolveUrl("EmbedMessenger.css")%>' />
<script src="<%=ResolveUrl("LoadSettings.aspx")%>"></script>
<script src="<%=ResolveUrl("LoadScripts.aspx")%>"></script>
<script>
var clientid='<%=Guid.NewGuid()%>';
var messengerads=document.getElementById("messenger_ads_id");
IsEmbed=true;

	var toptable=document.getElementById("toptableid");
	var BottomAds=document.getElementById("BottomAds");
	var MessengerAds=document.getElementById("messenger_ads_id");
	
</script>
<script src='<%=ResolveUrl("EmbedMessenger.js")%>'></script>

<%}%>

<!-- End EmbedMessenger.Ascx -->

