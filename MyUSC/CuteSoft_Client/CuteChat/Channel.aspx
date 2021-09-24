<%@ Page Language="c#" Inherits="CuteChat.ChatPageBase" %>
<%@ Register TagPrefix="uc1" TagName="TopAds" Src="Advertising/TopAds.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BottomAds" Src="Advertising/BottomAds.ascx" %>
<%@ Import Namespace="CuteChat" %>
<script runat="server">


override protected void OnInit(EventArgs args)
{
	//SkinName="Skins/"+ CuteSoft.Chat.ChatWebUtility.SkinName + "/style.css";
	
	if(ChatWebUtility.IsDownLevelBrowser)
	{
		Response.Redirect("Channel_Flash.aspx"+Request.Url.Query,true);
	}
	
	string place=Request.QueryString["Place"];
	if(place!=null)
	{
		if(place.ToLower().StartsWith("lobby-"))
		{
			IChatLobby lobby=ChatApi.GetLobby(int.Parse(place.Remove(0,6)));
			if(lobby!=null&&lobby.IsAvatarChat)
			{
				Response.Redirect("Channel_Avatar.aspx"+Request.Url.Query,true);
			}
		}
		if(place.ToLower().StartsWith("private-"))
		{
			Response.Redirect("Channel_Private.aspx"+Request.Url.Query,true);
		}
	}
	
	if(!ChatWebUtility.DisplayTopBanner)
	{
		TopAdsRow.Visible=false;
	}
		
	if(!ChatWebUtility.DisplayBottomBanner)
	{
		BottomAdsRow.Visible=false;
	}
	
	
	base.OnInit(args);
}

</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server" id="aspnetHead">
		<base target="_blank" />
		<title>Channel</title>
		<link rel="icon" href="Icons/chat.ico" type="image/x-icon" />
		<link rel="shortcut icon" href="Icons/chat.ico" type="image/x-icon" />
		<link rel="stylesheet" href="style.css" />
		<link rel="stylesheet" href="Channel_new.css" id="skincsslink" />
		<style type="text/css">
			body, table, select
			{
				font-family: MS Sans Serif, Arial, Helvetica, sans-serif;
				font-size: 8pt;
			}				
			body
			{
				margin-bottom:5px; padding: 5px;
			}		
			a, a:visited, a:link
			{
				color: #0000ff;
				text-decoration: underline;
			}

			a:hover
			{
				color: #FF3300;
				text-decoration: underline;
			}
			#maintable
			{
				width:100%;
			}
			#messagePanel
			{
				padding:4px;background-color: #ffffff;border:1px solid #A5B6DE;
			}
			#rightPanel
			{
				width:180px;padding:4px;background-color: #ffffff;border:1px solid #A5B6DE;
			}
			
		</style>
	</head>
	<body>
		<script>

	window.onerror=function window_error(a,b,c)
	{
		alert(b+","+c+"\r\n"+a+"\r\n"+GetStackTrace());
	}

		</script>
		<script src="script/CuteWebUI.js"></script>
		<script src="LoadSettings.aspx"></script>
		<script src="LoadScripts.aspx"></script>
		<script>
		
	var placename=<%=ChatWebUtility.EncodeJScriptString(Request.QueryString["Place"])%>;
	if(placename.toLowerCase().match(/private-/g))
	{
		initializelocation="Private";
	}

		</script>
		<table id="maintable" border="0" cellspacing="2" cellpadding="0" align="center">
			<tr style="height:64px" runat="server" id="TopAdsRow">
				<td>
					<table width="100%" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td width="200">
								<img title="CuteChat" src="<%=ChatWebUtility.LogoUrl %>">
							</td>
							<td align="center">
								<uc1:topads id="TopAds1" runat="server"></uc1:topads>
							</td>
							<td width="200">
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<table id='centertable' style='width:100%;height:100%'>
						<tr>
							<td id="messagePanel" valign="top" style='width:100%'>
								<div id="messageList" style="overflow:auto;overflow-y:auto;height:100%;width:100%;">
								</div>
							</td>
							<td valign="top" id="rightPanel" onclick="SetSelectedUser(null);" nowrap='true' style='width:180px;'>
								<table border="0" cellspacing="2" cellpadding="0" height="100%" style='width:180px;display:hidden'>
									<tr>
										<td valign="top" width="150">
											<div id="userList" style="height:100%;overflow:auto;" />
											</div>
										</td>
									</tr>
									<%if(ChatWebUtility.DisplayLobbyList){%>
									<tr>
										<td valign="bottom">
											<div id="channellistid" style="margin:6px">
												<select style="width:90%;" onchange="ChannelList_OnChange(this)">
													<option>Channel List</option>
													<%IChatLobby[] lobbies=ChatApi.GetLobbyArray();%>
													<%foreach(IChatLobby lobby in lobbies)%>
													<%{%>
													<option value="Lobby-<%=lobby.LobbyId%>"><%=Server.HtmlEncode(lobby.Title)%></option>
													<%}%>
												</select>
											</div>
										</td>
									</tr>
									<%}%>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr style="height:75px">
				<td style="border: #A5B6DE 1px solid; width: 100%;" valign="top">
					<div style="float:left;padding-left: 5px; padding-bottom: 2px; padding-top: 3px; ">
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
						<%if(ChatWebUtility.ShowVideoButton){%>
						<img title="[[UI_MENU_ShowMyCamera]]" src='<%=ResolveUrl("images/camera.png")%>' class="button" onmouseover="this.className='buttonover'"
					onmouseout="this.className='button'" onclick="if(GetPlace())ChatUI_PublishVideo(GetPlace().Name)" />
						<%}%>
						<%if(ChatWebUtility.GlobalAllowSendFile){%>
						<img title="[[UI_SendFile]]" src='<%=ResolveUrl("images/icon_file.gif")%>' class="button" onmouseover="this.className='buttonover'"
					onmouseout="this.className='button'" onclick="ChatUI_ShowSendFile()" />
						<%}%>
						<%if(ChatWebUtility.GlobalShowSignoutButton){%>
						<img title="[[UI_MENU_Signout]]" src='<%=ResolveUrl("images/logoff.png")%>' class="button" onmouseover="this.className='buttonover'"
					onmouseout="this.className='button'" onclick="ChatUI_QuitWithConfirm()" />
						<%}%>
					</div>
					<div style="float:right;padding-right: 2px; padding-bottom: 2px; padding-top: 3px;">
						<table border="0" cellspacing="0" cellpadding="2">
							<tr>
								<td><select id="select_targetuser" title="tips : Right click to target 'All'" style="vertical-align:middle;width:120px;">
										<option value="">[[UI_MENU_TargetAll]]</option>
									</select>
								</td>
								<td>
									<input type="checkbox" id="checkbox_whisper" onchange="SetWhisper(this.checked);" style="" />
								</td>
								<td>
									<label id="label_whisper" for="checkbox_whisper">[[UI_Whisper]]</label>
								</td>
								<td>
									<button id="btn_connect" class="ConnectionButton" style="width:92px;height:22px;">Connect</button>
								</td>
							</tr>
						</table>
					</div>
					<div style="clear:both;border-top: #A5B6DE 1px solid;border-bottom: #A5B6DE 1px solid;width:100%;background-color: #ffffff;">
						<table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td valign="top">
									<textarea id="inputBox" style="overflow:hidden;width:100%;height:56px;background-color: #ffffff;border:0px solid #ffffff;"></textarea>
								</td>
								<td style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 80px; padding-top: 2px; background-color: #ffffff"
									valign="top" align="right">
									<button id="buttonSend" class="SendButton">Send</button>
								</td>
							</tr>
						</table>
					</div>
					<table style="width: 100%; height:20px;" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td><div id="TypingStatus"></div>
							</td>
							<td style="color:#A5B6DE" align="right">Press Shift+Enter to go to the next line.
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr style="height:64px" runat="server" id="BottomAdsRow">
				<td colspan="2"><uc1:bottomads id="BottomAds1" runat="server"></uc1:bottomads></td>
			</tr>
		</table>
	</body>
	<script src="script/Channel_New.js"></script>
	<script>

	window.onresize=function()
	{
		AdjustUIHeight();
	}
	function AdjustUIHeight()
	{
		var cw=window.document.body.clientWidth;
		var ch=window.document.body.clientHeight;

		if(window.document.compatMode=='CSS1Compat')
		{
			cw=window.document.documentElement.clientWidth;
			ch=window.document.documentElement.clientHeight;
		}
		
		$("messageList").style.height="1px";
		$("userList").style.height="1px";
		$("maintable").style.height=(ch-4)+"px";
		$("centertable").style.height=(ch-200)+"px";
		$("messageList").style.height=Math.max(12,$("messagePanel").offsetHeight-12)+"px";
		$("userList").style.height=Math.max(12,$("messagePanel").offsetHeight-48)+"px";
	}
	setTimeout(AdjustUIHeight,1000);
	setTimeout(AdjustUIHeight,100);
	
	window.onload=function()
	{
		
		//_SL_ParseXml(LoadChatClasses("ChatUI.Xml.aspx?Type=Channel"),Desktop);
		
		var paramlist=(window.location.href.split("?")[1]||"").split("&");
		for(var i=0;i<paramlist.length;i++)
		{
			if(paramlist[i].split('=')[0]=="nickname")
			{
				SetGuestName(paramlist[i].split('=')[1]);
			}
		}
	
		Connect(placename);
	}

	</script>
</html>
