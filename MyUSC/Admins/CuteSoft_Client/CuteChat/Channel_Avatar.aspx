<%@ Page Language="c#" Inherits="CuteChat.ChatPageBase" %>
<%@ Register TagPrefix="uc1" TagName="TopAds" Src="Advertising/TopAds.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BottomAds" Src="Advertising/BottomAds.ascx" %>
<%@ Import Namespace="CuteChat" %>
<script runat="server">

string AvatarChatURL="";

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
		if(place.StartsWith("Lobby-"))
		{
			IChatLobby lobby=ChatApi.GetLobby(int.Parse(place.Remove(0,6)));
			if(lobby!=null&&lobby.IsAvatarChat)
			{
				AvatarChatURL=ChatWebUtility.ProcessWebPath(HttpContext.Current,null,lobby.AvatarChatURL);
			}
		}
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
		<link rel="stylesheet" href="2d.css" id="skincsslink" />
		<style type="text/css">
			body, table, select
			{
				font-family: MS Sans Serif, Arial, Helvetica, sans-serif;
				font-size: 8pt;
			}				
			body
			{
				padding: 5px 0 0 0; margin: auto;
				vertical-align:middle;
				background-color: #000000;
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
			#messagePanel
			{
				width:100%;padding:0px;background-color: #ffffff;
			}
			#rightPanel
			{
				width:108px;padding:4px;background-color: #ffffff;border:1px solid #A5B6DE;
			}
			
			.ShowVerticalScroll
			{
				overflow:scroll!important;
				overflow-x:hidden!important;
				overflow-y:scroll!important;
				background-image:url('<%=ResolveUrl("images/msgbg.png")%>');
				background-repeat: repeat-y;
			}
			#maintable
			{
				width:800px;
				height:600px!important;	
				margin-top: -320px;
				margin-left:-400px;
				position: absolute;
				top: 50%;
				left: 50%;
				background-color: #eeeeee;
			}
			#mmgpanel
			{
				background-image:url('<%=AvatarChatURL%>');position:relative;width:800px;height:570px;
			}
			#messageList
			{
				text-align:left;overflow:hidden;overflow-x:hidden;overflow-y:hidden;width:400px;height:160px;padding:10px;
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
		
	var placename='<%=Request.QueryString["Place"]%>';
	if(placename.toLowerCase().match(/private-/g))
	{
		initializelocation="Private";
	}
	GlobalEnableHtmlBox=false;
	ShowAvatarBeforeMessage=false;
	window.IsAvatarLayout=true;

		</script>
		<table id="maintable" border="0" cellspacing="1" cellpadding="0">
			<tr>
				<td id="messagePanel" valign="top">
					<div id="mmgpanel">
						<div id="messageList" hoverclass="ShowVerticalScroll">
						</div>
					</div>
				</td>
			</tr>
			<tr style="height:25px">
				<td style="border: #A5B6DE 1px solid; width: 100%;" valign="top">
					<div style="float:left;padding-top:3px;">
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
						<!-- do not show by default.
						<select style="vertical-align:middle;" onchange="SetFontName(this.value)">
							<option value="">[[UI_Font]]</option>
							<option value="Arial">Arial</option>
							<option value="Verdana">Verdana</option>
							<option value="Courier">Courier</option>
							<option value="Impact">Impact</option>
							<option value="Georgia">Georgia</option>
							<option value="Comic Sans MS">Comic Sans MS</option>
						</select>
						-->
						<%}%>
						<%if(ChatWebUtility.GlobalShowFontSize){%>
						<!-- do not show by default.
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
						-->
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
						<!-- do not show by default.
						<%if(ChatWebUtility.GlobalAllowSendFile){%>
						<img title="[[UI_SendFile]]" src='<%=ResolveUrl("images/icon_file.gif")%>' class="button" onmouseover="this.className='buttonover'"
					onmouseout="this.className='button'" onclick="ChatUI_ShowSendFile()" />
						<%}%>						
						-->
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
								<!-- do not show by default.
								<td>
									<button id="btn_connect" class="ConnectionButton" style="width:80px;height:22px;">Connect</button>
								</td>								
								-->
							</tr>
						</table>
					</div>
					<div style="float:right;padding-right: 2px; padding-bottom: 2px; padding-top: 1px;">
						<table border="0" cellspacing="0" cellpadding="2">
							<tr>
								<td>
									<input type="text" id="inputBox" style="overflow:hidden;width:240px;padding:1px;background-color: #ffffff;border:1px solid #A5B6DE;" />
								</td>
								<td>
									<div style="overflow:hidden;height:21px;width:69px" onmouseup="this.scrollTop='21'" onmousedown="this.scrollTop='42'"
										onmouseover="this.scrollTop='21'" onmouseout="this.scrollTop='0'">
										<img id="buttonSend" src="<%=ResolveUrl("images/IM_BtnSendMsg.png")%>" />
									</div>
								</td>
							</tr>
						</table>
					</div>
					<!--<div id="TypingStatus"></div>-->
				</td>
			</tr>
		</table>
	</body>
	<script src="script/Channel_New.js"></script>
	<script src="script/MMG.js"></script>
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
		
		$("maintable").style.height=(ch-4)+"px";
	}

	window.onload=function()
	{
		//_SL_ParseXml(LoadChatClasses("ChatUI.Xml.aspx?Type=Channel"),Desktop);
	
		Connect(placename);
		
		MMG_Start(document.getElementById("mmgpanel"))
	}

	</script>
</html>
