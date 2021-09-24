<%@ Page Language="c#" Inherits="CuteChat.ChatPageBase" %>
<%@ Register TagPrefix="uc1" TagName="TopAds" Src="Advertising/TopAds.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BottomAds" Src="Advertising/BottomAds.ascx" %>
<%@ Import Namespace="CuteChat" %>
<script runat=server >


override protected void OnInit(EventArgs args)
{
	//SkinName="Skins/"+ CuteSoft.Chat.ChatWebUtility.SkinName + "/style.css";
	
	if(ChatWebUtility.IsDownLevelBrowser)
	{
		Response.Redirect("Channel_Flash.aspx"+Request.Url.Query,true);
	}
	
	if(!ChatWebUtility.DisplayTopBanner)
	{
		TopAds1.Visible=false;
	}
		
	if(!ChatWebUtility.DisplayBottomBanner)
	{
		BottomAds1.Visible=false;
	}
	
	
	base.OnInit(args);
}

</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head runat="server" id="aspnetHead">
		<base target="_blank" />
		<title>Channel</title>
        <link rel="icon" href="Icons/chat.ico" type="image/x-icon" />
		<link rel="shortcut icon" href="Icons/chat.ico" type="image/x-icon" />
		<link rel="stylesheet" href="style.css" />
	</head>
	<link rel="stylesheet" href="Skins/<%=ChatWebUtility.SkinName%>/style.css" id="skincsslink">
	<body>
		<div id="loadingtext">
			[[UI_LOADING]]....
		</div>
		<div style="display:none">
			<table id="toptableid" width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td width="200">
						<img title="CuteChat" src="Images/logo.gif">
					</td>
					<td align="center">
						<uc1:TopAds id="TopAds1" runat="server"></uc1:TopAds>
					</td>
					<td width="200">
					</td>
				</tr>
			</table>
			<div id="toolbarid">
				<%if(ChatWebUtility.GlobalShowBoldButton){%>
				<img title="[[UI_Bold]]" src='<%=ResolveUrl("images/bold.gif")%>' class="button" onclick="SetFontBold((this.className=(this.className=='button'?'buttondown':'button'))=='buttondown')" />
				<%}%>
				<%if(ChatWebUtility.GlobalShowItalicButton){%>
				<img title="[[UI_Italic]]" src='<%=ResolveUrl("images/italic.gif")%>' class="button" onclick="SetFontItalic((this.className=(this.className=='button'?'buttondown':'button'))=='buttondown')" />
				<%}%>
				<%if(ChatWebUtility.GlobalShowUnderlineButton){%>
				<img title="[[UI_Underline]]" src='<%=ResolveUrl("images/underline.gif")%>' class="button" onclick="SetFontUnderline((this.className=(this.className=='button'?'buttondown':'button'))=='buttondown')" />
				<%}%>
				<%if(ChatWebUtility.GlobalShowColorButton){%>
				<img title="[[UI_FontColor]]" src='<%=ResolveUrl("images/colourpick.gif")%>' style="background-color: black"
					class="button" onmouseover="this.className='buttonover'" onmouseout="this.className='button'"
					onclick="ChatUI_ShowColorPanel(this)" />
				<%}%>
				<%if(ChatWebUtility.GlobalShowFontName){%>
				<select style="vertical-align:middle;" onchange="SetFontName(this.value)">
					<option value="">[[UI_Font]]</option>
					<option value="Arial">Arial</option>
					<option value="Verdana">Verdana</option>
					<option value="Courier">Courier</option>
					<option value="Impact">Impact</option>
					<option value="Georgia">Georgia</option>
					<option value="Comic Sans MS">Comic Sans MS</option>
				</select>
				<%}%>
				<%if(ChatWebUtility.GlobalShowFontSize){%>
				<select style="vertical-align:middle;width:60px;" onchange="SetFontSize(this.value)">
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
				</select>
				<%}%>
				<%if(ChatWebUtility.GlobalShowEmotion){%>
				<img title="[[UI_Emotion]]" src='<%=ResolveUrl("images/emotion.png")%>' class="button" onmouseover="this.className='buttonover'"
					onmouseout="this.className='button'" onclick="ChatUI_ShowEmotionPanel(this)" />
				<%}%>
				<%if(ChatWebUtility.GlobalShowSkinButton && (Context.Request.QueryString["Place"]).IndexOf("private")==-1 ){%>
				<img title="[[UI_SelectSkin]]" src='<%=ResolveUrl("images/skin.gif")%>' class="button" onmouseover="this.className='buttonover'"
					onmouseout="this.className='button'" onclick="ChatUI_ShowSkinPanel(this)" />
				<%}%>
				
				<img title="[[UI_EnableSound]]" id=toolbarsoundbutton src='<%=ResolveUrl("images/sound_on.png")%>' class="button" onmouseover="this.className='buttonover'" onmouseout="this.className='button'"/>
				<script>				
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
		
				<%if(ChatWebUtility.GlobalShowControlPanel){%>
				<img title="[[UI_ControlPanel]]" src='<%=ResolveUrl("images/control_panel.png")%>' class="button" onmouseover="this.className='buttonover'"
					onmouseout="this.className='button'" onclick="ChatUI_ShowControlPanel(this)" />
				<%}%>
				<%if(ChatWebUtility.GlobalShowSignoutButton){%>
				<img title="[[UI_MENU_Signout]]" src='<%=ResolveUrl("images/logoff.png")%>' class="button" onmouseover="this.className='buttonover'"
					onmouseout="this.className='button'" onclick="ChatUI_QuitWithConfirm()" />
				<%}%>
				<%if( (Context.Request.QueryString["Place"]).IndexOf("private")!=-1){%>
				<img title='[[UI_MENU_Invite]]' src='<%=ResolveUrl("images/invite.png")%>' class="button" onmouseover="this.className='buttonover'"
					onmouseout="this.className='button'" onclick="ChatUI_InviteIntoPrivate();">
				<%}%>
			</div>
			<div id="bottomadsid">
				<uc1:BottomAds id="BottomAds1" runat="server"></uc1:BottomAds>
			</div>
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
	</body>
	<script>
	
	var toolbarArea=document.getElementById("toolbarid");
	var toptable=document.getElementById("toptableid");
	var BottomAds=document.getElementById("bottomadsid");
	function GetStackTrace()
	{
		return "";
	}
	window.onerror=function window_error(a,b,c)
	{
		alert(b+","+c+"\r\n"+a+"\r\n"+GetStackTrace());
	}

	</script>
	
	<script src="LoadSettings.aspx"></script>

	<script src="LoadScripts.aspx"></script>
	
	<script src="script/moderatemode.js"></script>
	<script>
	
	
	function UpdateDocumentTitle()
	{
		document.title=GetPlace().Title
	}
	AttachChatEvent("PLACE",UpdateDocumentTitle);
	
	
	var placename='<%=Request.QueryString["Place"]%>';
	if(placename.toLowerCase().match(/private-/g))
	{
		initializelocation="Private";
	}
	
	function ReloadUI()
	{
		Desktop.SuspendLayout();
				
		LoadSkinClasses(SkinName,"ChatUI.Xml.aspx?Type=Channel");

		Desktop.AppendWindow( CreateInstance("ChannelMainForm") );

		Desktop.ResumeLayout();
	}
	
	window.onload=function()
	{
		document.getElementById("loadingtext").style.display='none';
	
		document.body.style.overflow="hidden";
	
		HtmlInitialize();
		
		_SL_ParseXml(LoadChatClasses("ChatUI.Xml.aspx?Type=Channel"),Desktop);
	
		ReloadUI();
		
		Connect(placename);
	}
	
	function window_onunload()
	{
		Disconnect(true);
	}
	window.onunload=window_onunload;
	if(window.attachEvent)
	{
		window.attachEvent("onunload",window_onunload);
	}
	else if(window.addEventListener)
	{
		window.addEventListener("unload",window_onunload,true);
	}
	
		
	function ChannelList_OnChange(select)
	{
		if(placename!=select.value)
		{
			location.href="Channel.aspx?Place="+select.value;
		}
		select.selectedIndex=0;
	}

	</script>

</html>
