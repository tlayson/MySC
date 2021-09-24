<%@ Page Language="c#" Inherits="CuteChat.ChatPageBase" %>
<%@ Import Namespace="CuteChat" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head runat="server" id="aspnetHead">
		<base target="_blank" />
		<title>Private Channel</title>
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
				margin:0; padding:0;
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
				width:100%;padding:4px;background-color: #ffffff;border:1px solid #A5B6DE;
			}
			#rightPanel
			{
				width:108px;padding:4px;background-color: #ffffff;border:1px solid #A5B6DE;
			}
		</style>
	</head>
	<body>
		<table id="maintable" style="width:100%;" border="0" cellspacing="2" cellpadding="0">
			<tr>
				<td id="messagePanel" valign="top">
					<div id="messageList" style="overflow:auto;overflow-y:auto;height:100%">
					</div>
				</td>
				<td valign="top" id="rightPanel" onclick="SetSelectedUser(null);">
					<table border="0" cellspacing="2" cellpadding="0" height="100%">
						<tr>
							<td valign="top" width="160">
								<div id="userList" style="height:100%"/>
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr style="height:75px">
				<td colspan="2" style="border: #A5B6DE 1px solid; width: 100%;" valign="top">
					<div style="padding-right: 0px; padding-left: 5px; padding-bottom: 2px; width: 100%; padding-top: 3px; border-bottom: #A5B6DE 1px solid">
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
						<img title='[[UI_MENU_Invite]]' src='<%=ResolveUrl("images/invite.png")%>' class="button" onmouseover="this.className='buttonover'"
					onmouseout="this.className='button'" onclick="ChatUI_InviteIntoPrivate();">
					</div>
					<table style="border-top: #A5B6DE 0px solid; width: 100%;background-color: #ffffff;" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td valign="top">
								<textarea id="inputBox" style="overflow:hidden;width:100%;height:56px;padding:1px;background-color: #ffffff;border:0px solid #ffffff;"></textarea>
							</td>
							<td style="padding-right: 2px; padding-left: 2px; padding-bottom: 2px; width: 80px; padding-top: 2px; background-color: #ffffff"
								valign="top" align="right">
								<button id="buttonSend" class="SendButton">Send</button>
							</td>
						</tr>
					</table>
					<table style="border-top: #A5B6DE 1px solid; width: 100%; height:20px;" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td><div id="TypingStatus"></div>
							</td>
							<td style="color: #A5B6DE" align="right">[[GoNextLine]]</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</body>
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

	</script>
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
		$("maintable").style.height=Math.max(12,ch+2)+"px";
		$("messageList").style.height=Math.max(12,$("messagePanel").offsetHeight-12)+"px";
	}
	setTimeout(AdjustUIHeight,1000);
	setTimeout(AdjustUIHeight,100);
	
	window.onload=function()
	{
		//_SL_ParseXml(LoadChatClasses("ChatUI.Xml.aspx?Type=Channel"),Desktop);
	
		Connect(placename);
	}

	</script>
</html>
