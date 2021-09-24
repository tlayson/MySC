<%@ Control Language="c#" AutoEventWireup="false" Inherits="CuteChat.ChatCtrlBase" %>
<%@ Import Namespace="CuteChat" %>
<script runat="server">

string _customQuery;
public string CustomQuery
{
	get
	{
		return _customQuery;
	}
	set
	{
		_customQuery=value;
	}
}

</script>
<!-- Begin EmbedChannel.Ascx -->
<%if( ChatWebUtility.IsDownLevelBrowser){%>
<%

string query=_customQuery;
if(query==null)
{
	query=Request.Url.Query;
}
string flashurl="FlashClient.swf"+query;
flashurl=ResolveUrl(flashurl);
string url=Request.Url.ToString();
flashurl=url.Substring(0,url.IndexOf("/",url.IndexOf("//")+2))+flashurl;

%>
<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0"
	width="720" height="400" id="FlashClient" align="middle" VIEWASTEXT>
	<param name="movie" value="<%=Server.HtmlEncode(flashurl)%>" />
	<param name="quality" value="high" />
	<embed src="<%=Server.HtmlEncode(flashurl)%>" quality="high" width="720" height="400"
	name="FlashClient" align="middle" type="application/x-shockwave-flash"
	pluginspage="http://www.macromedia.com/go/getflashplayer" />
</object>
<%}else{%>
<div style="display:none">
	<div id="channellistid" style="margin:12px">
		<select style="width:90%;" onchange="ChannelList_OnChange(this)">
			<option>Channel List</option>
			<%IChatLobby[] lobbies=ChatApi.GetLobbyArray();%>
			<%foreach(IChatLobby lobby in lobbies)%>
			<%{%>
			<option value="Lobby-<%=lobby.LobbyId%>"><%=lobby.Title%></option>
			<%}%>
		</select>
	</div>
</div>
<div id="td_channel_container" style="width: 700px; height: 480px; background-color: #edf1fa;
    border: 1px solid #BED6E0; padding-bottom: 5px;">
    <div id="toolbarid">
        <%if (ChatWebUtility.GlobalShowBoldButton)
      {%>
        <img alt="[[UI_Bold]]" src='<%=ResolveUrl("images/bold.gif")%>' class="button" onmouseover="this.className='buttonover'"
            onmouseout="this.className='button'" onclick="SetFontBold((this.className=(this.className=='button'?'buttondown':'button'))=='buttondown')" />
        <%}%>
        <%if (ChatWebUtility.GlobalShowItalicButton)
      {%>
        <img alt="[[UI_Italic]]" src='<%=ResolveUrl("images/italic.gif")%>' class="button"
            onmouseover="this.className='buttonover'" onmouseout="this.className='button'"
            onclick="SetFontItalic((this.className=(this.className=='button'?'buttondown':'button'))=='buttondown')" />
        <%}%>
        <%if (ChatWebUtility.GlobalShowUnderlineButton)
      {%>
        <img alt="[[UI_Underline]]" src='<%=ResolveUrl("images/underline.gif")%>' class="button"
            onmouseover="this.className='buttonover'" onmouseout="this.className='button'"
            onclick="SetFontUnderline((this.className=(this.className=='button'?'buttondown':'button'))=='buttondown')" />
        <%}%>
        <%if (ChatWebUtility.GlobalShowColorButton)
      {%>
        <img alt="[[UI_FontColor]]" src='<%=ResolveUrl("images/colourpick.gif")%>' style="background-color: black"
            class="button" onmouseover="this.className='buttonover'" onmouseout="this.className='button'"
            onclick="ChatUI_ShowColorPanel(this)" />
        <%}%>
        <%if (ChatWebUtility.GlobalShowFontName)
      {%>
        <select style="" onchange="SetFontName(this.value)">
            <option value="">[[UI_Font]]</option>
            <option value="Arial">Arial</option>
            <option value="Verdana">Verdana</option>
            <option value="Wingdings">Wingdings</option>
            <option value="Courier">Courier</option>
            <option value="Impact">Impact</option>
            <option value="Georgia">Georgia</option>
            <option value="Comic Sans MS">Comic Sans MS</option>
        </select>
        <%}%>
        <%if (ChatWebUtility.GlobalShowFontSize)
      {%>
        <%--<select style="width:60px;" onchange="SetFontSize(this.value)">
					<option value="">[[UI_Size]]</option>
					<option value="7">7</option>
					<option value="8">8</option>
					<option value="9">9</option>
					<option value="10">10</option>
					<option value="11">11</option>
					<option value="12">12</option>
					<option value="14">14</option>
					<option value="15">15</option>
					<option value="16">16</option>
					<option value="18">18</option>
					<option value="20">20</option>
					<option value="22">22</option>
					<option value="28">28</option>
					<option value="32">32</option>
				</select>--%>
        <%}%>
        <%if (ChatWebUtility.GlobalShowEmotion)
      {%>
        <img title="[[UI_Emotion]]" src='<%=ResolveUrl("images/emotion.png")%>' class="button"
            onmouseover="this.className='buttonover'" onmouseout="this.className='button'"
            onclick="ChatUI_ShowEmotionPanel(this)" />
        <%}%>
        <img alt="[[UI_EnableSound]]" title="[[UI_EnableSound]]" id="toolbarsoundbutton"
            src='<%=ResolveUrl("images/sound_on.png")%>' class="button" onmouseover="this.className='buttonover'"
            onmouseout="this.className='button'" />

        <script type="text/javascript">
				
				var toolbarsoundbutton=document.getElementById("toolbarsoundbutton"); 
				toolbarsoundbutton.onclick=function()
				{
					ChatUI_SetEnableSound( !ChatUI_GetEnableSound() );
				}
				setInterval(function(){
					if(typeof(ChatUI_GetEnableSound)=="undefined")return;
					var condition=ChatUI_GetEnableSound();
					if(toolbarsoundbutton.getAttribute("condition")==(condition?"1":"0"))return;
					toolbarsoundbutton.setAttribute("condition",condition?"1":"0");
					toolbarsoundbutton.title=TEXT( condition ?"UI_DisableSound":"UI_EnableSound" );
					toolbarsoundbutton.src= condition?'<%=ResolveUrl("images/sound_on.png")%>':'<%=ResolveUrl("images/sound_off.png")%>'
				},100);
        </script>

        <%if (ChatWebUtility.GlobalShowControlPanel)
      {%>
        <img title="[[UI_ControlPanel]]" src='<%=ResolveUrl("images/control_panel.png")%>'
            class="button" onmouseover="this.className='buttonover'" onmouseout="this.className='button'"
            onclick="ChatUI_ShowControlPanel(this)" alt="[[UI_ControlPanel]]"" />
        <%}%>
        <%if (ChatWebUtility.GlobalShowSignoutButton)
      {%>
        <img title="[[UI_MENU_Signout]]" src='<%=ResolveUrl("images/logoff.png")%>' class="button"
            onmouseover="this.className='buttonover'" onmouseout="this.className='button'"
            onclick="ChatUI_QuitWithConfirm()" alt="[[UI_MENU_Signout]]" />
        <%}%>
    </div>
</div>
<link rel="stylesheet" href="<%=ResolveUrl("EmbedChannel.css")%>" />
<script type="text/javascript" src="<%=ResolveUrl("LoadSettings.aspx")%>"></script>
<script type="text/javascript" src="<%=ResolveUrl("LoadScripts.aspx")%>"></script>
<script type="text/javascript" src="<%=ResolveUrl("script/moderatemode.js")%>"></script>
<script type="text/javascript">
var clientid='<%=Guid.NewGuid()%>';
var toolbarArea=document.getElementById("toolbarid");
IsEmbed=true;

if(typeof(Embed_Place)=="undefined")alert("Embed_Place not set!");

function ChannelList_OnChange(select)
{
	if(Embed_Place!=select.value)
	{		
		var url=location.href;	
		var pos=url.indexOf('?');		
		if(pos!=-1)
		{
			url=url.substring(0,pos+1);		
		}
		else
		{
			url=url+"?";
		}
		location.href=url+"Place="+select.value;
	}
	select.selectedIndex=0;
}

</script>
<script type="text/javascript" src='<%=ResolveUrl("EmbedChannel.js")%>'></script>
<%}%>
<!-- End EmbedChannel.Ascx -->
