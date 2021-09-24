<%@ Page Language="c#" Inherits="CuteChat.ChatPageBase"  %>
<%@ Import Namespace="CuteChat" %>
<%-- Do not cache this file --%>
<html runat="server" Visible="false">
	<head runat="server">
	</head>
</html>
<script runat=server>
override protected void OnInit(EventArgs args)
{
	base.OnInit(args);
	Response.ContentType="text/plain";
	CuteChat.ChatWebUtility.InstallGzipForResponse();
}
</script>

<%if(false){%>//<script><%}%>

var __cc_version=4.201;//3.1;

var __cc_urlbase='<%=ResolveUrl("./")%>';

var __cc_strings={

<%
NameValueCollection nvc=CuteChat.ChatWebUtility.Strings.GetValues();
foreach(string key in nvc.Keys)
{
%>

<%=CuteChat.ChatWebUtility.EncodeJScriptString(key)%>:<%=CuteChat.ChatWebUtility.EncodeJScriptString(nvc[key])%>
,
<%
}
%>

"__end":"__end"
};

var __cc_strings2={};
for(var __cc_strname in __cc_strings)
{
	__cc_strings2[__cc_strname]=__cc_strings[__cc_strname];
}
for(var __cc_strname in __cc_strings2)
{
	__cc_strings[__cc_strname.toLowerCase()]=__cc_strings[__cc_strname];
	__cc_strings[__cc_strname.toUpperCase()]=__cc_strings[__cc_strname];
}

//this==window
var __cc_global=this;

for(var __cc_strname in __cc_strings)
{
	__cc_global["TEXT_"+__cc_strname]=__cc_strings[__cc_strname];
}

function GetString(name)
{
	if(!name)return "";
	
	var upper=name.toUpperCase();
	var v=__cc_strings[upper];
	if(v!=null)
		return v;
	v=__cc_global["TEXT_"+upper];
	if(v!=null)
		return v;
	//return "[("+name+")]";
	return name;
}
function TEXT(text)
{
	text=GetString(text);

	for(var i=1;i < arguments.length;i++)
	{
		var item=""+arguments[i];
		text=text.split('{'+(i-1)+'}').join(item);
	}
	
	return text;
}

function _CCTEXT(text,prefix)
{
	if(prefix)
	{
		return GetString(prefix+"_"+text) || text;
	}
	return GetString(text) || text;
}

var ReplaceStrings_RE=/\[\[([^\[]+)\]\]/ig;
function ReplaceStrings(val)
{
	if(typeof(val)!="string")return val;
	
	val=val.replace(ReplaceStrings_RE,ReplaceStrings_Match);
	
	return val;
}
function ReplaceStrings_Match(text,quoted,index)
{
	return GetString(quoted)
}

//TODO:load the setting of current user ..
var SkinName="<%=ChatWebUtility.SkinName%>";

var SkinNames=[];
var SkinTexts=[];
	
<%
string[] skins=ChatWebUtility.ChannelSkins;
%>
<%foreach(string skin in skins)%>
<%{%>
SkinNames[SkinNames.length]=<%=CuteChat.ChatWebUtility.EncodeJScriptString(skin.Trim('"'))%>;
SkinTexts[SkinTexts.length]=<%=CuteChat.ChatWebUtility.EncodeJScriptString(skin.Trim('"'))%>;
<%}%>


var skin_found=false;
for(var i=0;i<SkinNames.length;i++)
{
	if(SkinName==SkinNames[i])
	{
		skin_found=true;
		break;
	}
}
if(!skin_found)
{
	SkinName=SkinNames[0];
}



var RequireReSyncForCTS=<%=ChatWebUtility.RequireReSyncForCTS?"true":"false"%>;
var ShowBoldButton=<%=ChatWebUtility.GlobalShowBoldButton?"true":"false"%>;
var ShowItalicButton=<%=ChatWebUtility.GlobalShowItalicButton?"true":"false"%>;
var ShowUnderlineButton=<%=ChatWebUtility.GlobalShowUnderlineButton?"true":"false"%>;
var ShowColorButton=<%=ChatWebUtility.GlobalShowColorButton?"true":"false"%>;
var ShowFontName=<%=ChatWebUtility.GlobalShowFontName?"true":"false"%>;
var ShowFontSize=<%=ChatWebUtility.GlobalShowFontSize?"true":"false"%>;
var ShowEmotion=<%=ChatWebUtility.GlobalShowEmotion?"true":"false"%>;
var ShowControlPanel=<%=ChatWebUtility.GlobalShowControlPanel?"true":"false"%>;
var ShowSignoutButton=<%=ChatWebUtility.GlobalShowSignoutButton?"true":"false"%>;
var ShowControlPanel=<%=ChatWebUtility.GlobalShowControlPanel?"true":"false"%>;
var ShowSkinButton=<%=ChatWebUtility.GlobalShowSkinButton?"true":"false"%>;
var ShowSystemMessages=<%=ChatWebUtility.GlobalShowSystemMessages?"true":"false"%>;
var TextWrittenfromRighttoLeft=<%=ChatWebUtility.GlobalTextWrittenfromRighttoLeft?"true":"false"%>;
var ShowAvatarBeforeMessage=<%=ChatWebUtility.GlobalShowAvatarBeforeMessage?"true":"false"%>;
var ShowTimeStampBeforeMessage=<%=ChatWebUtility.GlobalShowTimeStampBeforeMessage?"true":"false"%>;
var ShowTimeStampWebMessenger=<%=ChatWebUtility.GlobalShowTimeStampWebMessenger?"true":"false"%>;
var ShowTypingIndicator=<%=ChatWebUtility.GlobalShowTypingIndicator?"true":"false"%>;
var ShowTypingIndicator=<%=ChatWebUtility.GlobalShowTypingIndicator?"true":"false"%>;

var DisplayLobbyList=<%=ChatWebUtility.DisplayLobbyList?"true":"false"%>;
var DisplayLobbyListEmbed=<%=ChatWebUtility.DisplayLobbyListEmbed?"true":"false"%>;

var DisplayHeaderPanel=<%=ChatWebUtility.DisplayHeaderPanel?"true":"false"%>;
var DisplayLogo=<%=ChatWebUtility.DisplayLogo?"true":"false"%>;
var DisplayTopBanner=<%=ChatWebUtility.DisplayTopBanner?"true":"false"%>;
var DisplayBottomBanner=<%=ChatWebUtility.DisplayBottomBanner?"true":"false"%>;
var DisableWhisper=<%=ChatWebUtility.DisableWhisper?"true":"false"%>;
var DisablePrivateChatWhisper=<%=ChatWebUtility.DisablePrivateChatWhisper?"true":"false"%>;

var DefaultEnableSound=<%=ChatWebUtility.DefaultEnableSound?"true":"false"%>;
var ShowJoinLeaveMessage=<%=ChatWebUtility.ShowJoinLeaveMessage?"true":"false"%>;

var GlobalEnableHtmlBox=<%=ChatWebUtility.GlobalEnableHtmlBox?"true":"false"%>;
var GlobalAllowOutsiteImage=<%=ChatWebUtility.GlobalAllowOutsiteImage?"true":"false"%>;

var AllowChangeName=<%=ChatWebUtility.AllowChangeName?"true":"false"%>;

var GlobalAllowSendFile=<%=ChatWebUtility.GlobalAllowSendFile?"true":"false"%>;
var GlobalSendFileSize=<%=ChatWebUtility.GlobalSendFileSize%>;
var GlobalSendFileType='<%=ChatWebUtility.GlobalSendFileType%>';

var SupportAllowSendFile=<%=ChatWebUtility.SupportAllowSendFile?"true":"false"%>;
var SupportAllowSendMail=<%=ChatWebUtility.SupportAllowSendMail?"true":"false"%>;

var SupportSendFileSize=<%=ChatWebUtility.SupportSendFileSize%>;
var SupportSendFileType='<%=ChatWebUtility.SupportSendFileType%>';

var MessengerAllowSendFile=<%=ChatWebUtility.MessengerAllowSendFile?"true":"false"%>;
var MessengerSendFileSize=<%=ChatWebUtility.MessengerSendFileSize%>;
var MessengerSendFileType='<%=ChatWebUtility.MessengerSendFileType%>';

var DenyAnonymous=<%=ChatWebUtility.GlobalDenyAnonymous?"true":"false"%>;
var GlobalAllowPrivateMessage=<%=ChatWebUtility.GlobalAllowPrivateMessage?"true":"false"%>;
var GlobalSendFileSize=<%=ChatWebUtility.GlobalSendFileSize%>;
var ColorsArray='<%=ChatWebUtility.ColorsArray%>'
var EmotionsArray='<%=ChatWebUtility.EmotionsArray%>'
var LoginUrl='<%=ChatWebUtility.LoginUrl%>'
var LogoutUrl='<%=ChatWebUtility.LogoutUrl%>'
var LogoUrl='<%=ChatWebUtility.LogoUrl%>'

var FloodControlCount=<%=ChatWebUtility.FloodControlCount%>
var FloodControlDelay=<%=ChatWebUtility.FloodControlDelay%>
var MaxMSGLength=<%=ChatWebUtility.MaxMSGLength%>
var MaxWordLength=<%=ChatWebUtility.MaxWordLength%>

var FlashChatServer='<%=ChatWebUtility.FlashChatServer%>'
var ShowVideoButton=<%=ChatWebUtility.ShowVideoButton?"true":"false"%>;

var LogoutCloseMessenger=<%=ChatWebUtility.LogoutCloseMessenger?"true":"false"%>;

TEXT_SYS_INVALIDCOMMAND=" Invalid Command : {0} !";


var IsAdministrator=<%=ChatWebUtility.CurrentIdentityIsAdministrator?"true":"false"%>;
GlobalAllowSendFile=IsAdministrator||GlobalAllowSendFile;
SupportAllowSendFile=IsAdministrator||SupportAllowSendFile;
MessengerAllowSendFile=IsAdministrator||MessengerAllowSendFile;

CuteChatUrlBase=__cc_urlbase;
UseHookupEventForMsnImages=true;
var Chat_Sync_Timeout=1000;//sync 1 times per second.
var chatservice_url=__cc_urlbase+"ChatAjax.ashx";

var __cc_culture='<%=ChatWebUtility.CurrentCulture%>';

if(__cc_culture.split('-')[0]=="ar"||__cc_culture.toLowerCase()=="he-il")
{
	document.body.style.direction="rtl";
}

