//<script>

var CHATUI_DEBUG=true;

var CHATUI_MAX_MESSAGE_ITEM_COUNT=500;
var CHATUI_AUTOAWAYTIMEOUT=1800;

if(typeof(__cc_version)=="undefined")
{
	alert("Unable to load LoadSettings.aspx!");
}

var chatui_random=__cc_version;
if(CHATUI_DEBUG)chatui_random=Math.random();

function LoadSkinClasses(skin,layout)
{
	_SL_ParseXml(_SL_LoadXmlDocument(__cc_urlbase+"Skins/"+skin+"/"+layout),null);
}

function LoadChatClasses(url)
{
	var lower=url.toLowerCase();
	if(lower.substr(0,1)!="/"&&lower.substr(0,4)!="http")
	{
		url=__cc_urlbase+"Xml/"+url
	}
	return _SL_LoadXmlDocument(url);
}


function m_over(element) {
	element.className='buttonover';
}
function m_out(element) {
	element.className='button';
}


function ChatUI_FormatTime(time)
{
	var h=String(time.getHours());
	var m=String(time.getMinutes());
	var s=String(time.getSeconds());
	if(h.length==1)h="0"+h;
	if(m.length==1)m="0"+m;
	if(s.length==1)s="0"+s;
	return h+":"+m+":"+s;
}


function ChatUI_ShowHelp()
{
	OpenWindowAsync(null,"chat-user-guide/");
}
function ChatUI_ShowMessengerHelp()
{
	OpenWindowAsync(null,"messenger-user-guide/");
}
function HelpSplashCommandHandler()
{
	//TODO:change the help text
	var msg={};
	msg.Type="LOCAL";	
	msg.Text=TEXT("UI_ChatCommands");
	
	for(var key in CHATUI_SPLASHCOMMANDS)
	{
		msg.Text+="\r\n /"+key;
	}
	FireMessage(msg,true);
	return true;
}

function FindUserByName(name)
{
	if(!name)return null;
	name=name.toLowerCase();
	var users=GetUsers();
	for(var i=0;i<users.length;i++)
	{
		if(users[i].DisplayName.toLowerCase()==name)
			return users[i];
	}
	return null;
}

function ChatUI_InvitePrivateChatByName(name)
{
	var user=FindUserByName(name);
	if(user)
	{
		if(!GlobalAllowPrivateMessage)
		{	
			Desktop.Alert(null,TEXT("DenyPrivateMessage"),TEXT("UI_ALERT"));
			return;
		}
		InvitePrivateChat(user);
	}
	else
	{
		function HandlePrompt(newname)
		{
			user=FindUserByName(newname);
			if(user)
			{
				InvitePrivateChat(user);
			}
			else
			{
				//alert invalid user
			}
		}
		Desktop.Prompt(HandlePrompt,TEXT("UI_CONNECTION_NeedName_Message"),"User",name);
	}
}
function ChatUI_InviteIntoPrivateByName(name)
{
	if(name)
	{
		InviteIntoPrivateChat(name);
	}
	else
	{
		function HandlePrompt(newname)
		{
			if(newname)
			{
				InviteIntoPrivateChat(newname);
			}
			else
			{
				//alert invalid user
			}
		}
		Desktop.Prompt(HandlePrompt,TEXT("UI_CONNECTION_NeedName_Message"),"User",name);
	}
}

var CHATUI_SPLASHCOMMANDS=
{
	"HELP":HelpSplashCommandHandler
	,
	"HELPURL":function(){ChatUI_ShowHelp("COMMAND");return true;}
	,
	"ADMIN":AdminSplashCommandHandler
	,
	"AVATAR":function(){ChatUI_ShowAvatarDialog();return true;}
	,
	"AVATARS":function(){ChatUI_ShowAvatarDialog();return true;}
	,
	"DISCONNECT":function(){Disconnect();return true;}
	,
	"CONNECT":function(){Connect();return true;}
	,
	"QUIT":function(){Disconnect();if(window.opener){window.close()}else{ChatUI_QuitRedirect()};return true;}
	,
	"MSG":function(name){ ChatUI_InvitePrivateChatByName(name);return true; }
	,
	"INVITE":function(name){ ChatUI_InviteIntoPrivateByName(name);return true; }
	,
	"HELLO"://sample
	{
		Arg0:"%ME Say Hello To everybody!"
		,
		Arg1:"%ME Say Hello To {0}"
		,
		Arg2:function()
		{
			var args=new Array();
			for(var i=0;i<arguments.length;i++)
				args[i]=arguments[i];
			 return GetMyInfo().DisplayName+" Say Hello To "+args.join(" ")
		}
	}
	,
	"CLEAR":function(){ _InvokeChatEvent("UICOMMAND",["UICOMMAND","CLEAR"]);return true; }
	,
	"RELOAD":function(){ _InvokeChatEvent("UICOMMAND",["UICOMMAND","RELOAD"]);return true; }
	,
	"MODERATE":function(){ AdminSetModerateMode(true);return true;}
	,
	"STOPMODERATE":function(){ AdminSetModerateMode(false);return true;}
	,
	"ADDMODERATOR":function(name){ AdminAddModerator(name);return true;}
	,
	"REMOVEMODERATOR":function(name){ AdminRemoveModerator(name);return true;}
	//TODO: Add splash command handlers here..
}
CHATUI_SPLASHCOMMANDS["EXIT"]=CHATUI_SPLASHCOMMANDS["QUIT"];


function ChatUI_HandleSplashCommand(text)
{
	var arr=text.split(/\s+/g);
	
	var cmd=CHATUI_SPLASHCOMMANDS[arr[0].toUpperCase()];
	
	if(cmd==null)
	{
		Desktop.Alert(null,TEXT("UI_UnknownSplashCommand",arr[0]),TEXT("UI_ALERT"));
		return false;
	}
	
	arr.shift();//remove first
	//remove blank string
	for(var i=0;i<arr.length;i++)
	{
		if(arr[i]=="")
		{
			arr.splice(i,1);
			i--;//and then i++
		}
	}
	
	var res;
	
	if(typeof(cmd)=="function")
	{
		res=cmd.apply(this,arr);
	}
	else if(cmd.Handler)
	{
		res=cmd.Handler.apply(this,arr);
	}
	else //search ArgX
	{
		for(var i=arr.length;i>=0;i--)
		{
			var handler=cmd["Arg"+i];
			if(handler)
			{
				if(typeof(handler)=="function")
				{
					res=handler.apply(this,arr);
				}
				else //string
				{
					res=handler.replace(/\%ME/ig,GetMyInfo().DisplayName);
					for(var j=0;j<arr.length;j++)
					{
						res=res.replace(new RegExp("\\{"+j+"\\}","ig"),arr[j]);
					}
				}
				break;
			}
		}
	}
	
	if(typeof(res)!="string")
	{
		return res;
	}

	return ChatUI_SendEmotionWithFloodCoontrol(res);
}
//TODO: finish the admin handler
function AdminSplashCommandHandler(arg0,arg1,arg2)
{
	if(!GetMyInfo().IsAdmin)
	{
		//Desktop.Alert(null,TEXT("UI_UnknownSplashCommand","ADMIN"));
		Desktop.Alert(null,TEXT("UI_CommandNoPermission","ADMIN"));
		return false;
	}
	
	var cmd=arg0;
	if(!cmd)
	{
		ListAdminCommands();
		return false;
	}
	switch(cmd.toUpperCase())
	{
		case "KICK":
			if(arg1==null)return false;
			
			var users=GetUsers();
			for(var i in users)
			{
				var user=users[i];
				if(user.DisplayName.toLowerCase()==arg1.toLowerCase())
				{
					AdminKickUser(user);
					return true;
				}
			}
		case "PASSWORD":
		case "SETPASSWORD":
			AdminSetPassword(arg1||null);
			return true;
		case "LOCK":
			AdminSetLockChannel(true);
		case "UNLOCK":
			AdminSetLockChannel(false);
			return true;	
	}
	
	ListAdminCommands();
	return false;
}
function ListAdminCommands()
{
	var msg={};
	msg.Type="LOCAL";
	msg.Text="Usage :	\r\n\
/Admin	- this command list	\r\n\
/Admin SetPassword	- clear the room password	\r\n\
/Admin SetPassword newpassword	- set the new room password	\r\n\
/Admin Lock - lock this channel	\r\n\
/Admin Unlock - unlock this channel \r\n\
/Admin Kick targetname	- ban that user	\r\n\
	";
	FireMessage(msg,true);
}


function ChatUI_QuitRedirect()
{
	location.href="QuitRedirect.aspx";
}


function GetOpenWindowSizeText(width,height)
{
	var left=(screen.availWidth-width)/2;
	var top=(screen.availHeight-height)/2;
	return "left="+left+",top="+top+",width="+width+",height="+height;
}
function OpenWindowAsync(handler,url,name,option)
{
	if(typeof(handler)=="string")
	{
		throw(new Error("OpenWindowAsync(handler,url,name,option) - handler should not be string"));
	}
	
	var win;
	function Show()
	{
		try
		{
			if(option)
				win=window.open(url,name,option);
			else if(name)
				win=window.open(url,name);
			else
				win=window.open(url);
				
			
		}
		catch(x)
		{
		}
		if(win==null)
		{
			ChatUI_PlaySound("Alert");
			Desktop.Alert(HandleAlert,TEXT("UI_WindowBlocked"),TEXT("UI_ALERT"));
			return;
		}
		
		if(handler)
		{
			handler(win);
		}
	}
	function HandleAlert()
	{
		Show();
	}
	Show();
}
function OpenWindowWaitReturn(handler,url,name,option)
{
	var win;
	var res;
	var closed=false;
	
	OpenWindowAsync(HandleOpen,url,name,option)
	function HandleOpen(thewin)
	{
		win=thewin;
		AttachAgain();
		setTimeout(CheckClosed,10);
	}
	function AttachAgain()
	{
		try
		{
			Html_AttachEventInternal(win,"beforeunload",win_onbeforeunload);
		}
		catch(x)
		{
		}
	}
	
	function win_onbeforeunload()
	{
		res=win.returnValue;
	}

	function CheckClosed()
	{
		if(closed)return;
		setTimeout(CheckClosed,10);
		try
		{
			if(!win.closed)
			{
				//refresh ? navigate ?
				setTimeout(AttachAgain,1);
				return;
			}
		}catch(x)
		{
		}
		closed=true;
		handler(res);
	}
}

var _lastsoundplaytime=0;
function ChatUI_PlaySound(soundfile)
{
	if( ! ChatUI_GetEnableSound() ) return;
	
	//don't play sound in 2 seconds
	if(_lastsoundplaytime+2000>new Date().getTime())
		return;
	
	_lastsoundplaytime=new Date().getTime();
	
	SoundManager_Play(soundfile);
}
function ChatUI_SetEnableSound(enable)
{
	enable=_SL_ToBoolean(enable);
	if(enable)
	{
		SetCookie("CCNoSound","false",3600*24*365);
	}
	else
	{
		SetCookie("CCNoSound","true",3600*24*365);
	}
}
function ChatUI_GetEnableSound()
{
	var v=GetCookie("CCNoSound");
	if(v=="true")
		return false;
	if(v=="false")
		return true;
	return DefaultEnableSound;		//default allow sound..
}

function ChatUI_PlayBuzz()
{
	ChatUI_PlaySound("nudge");
	
	ChatUI_FocusWindow();

	var movefactor=5;
	var posarr=[
		[1,1],[1,-1],[-1,-1],[-1,1],
		[1,-1],[-1,-1],[-1,1],[1,1],
		[-1,-1],[-1,1],[1,1],[1,-1],
		[-1,1],[1,1],[1,-1],[-1,-1],
		[1,1],[1,-1],[-1,-1],[-1,1]
		,
		[1,1],[1,-1],[-1,-1],[-1,1],
		[1,-1],[-1,-1],[-1,1],[1,1],
		[-1,-1],[-1,1],[1,1],[1,-1],
		[-1,1],[1,1],[1,-1],[-1,-1],
		[1,1],[1,-1],[-1,-1],[-1,1]
	];
	var index=0;
	function MoveFunc()
	{
		var pos=posarr[index];
		if(pos)
		{
			try
			{
				window.moveBy(pos[0]*movefactor,pos[1]*movefactor);
			}
			catch(x)
			{
			}
		}
		index++;
		if(index<posarr.length)
		{
			intervalId=setTimeout(MoveFunc,20);
		}
		else
		{
			
		}
	}
	var intervalId=setTimeout(MoveFunc,20);
}

function ChatUI_SetAutoFocus(enable)
{
	enable=_SL_ToBoolean(enable);
	if(enable)
	{
		SetCookie("CCFocus","true",3600*24*365);
	}
	else
	{
		SetCookie("CCFocus","false",-1);
	}
}
function ChatUI_GetAutoFocus()
{
	var v=GetCookie("CCFocus");
	if(v=="true")
		return true;
	return false;
}
function FlashWindowTitle()
{
	if(GetWindowIsFocus())return;
	if(window._isflashingtitle)return;
	window._isflashingtitle=true;
	var backuptitle=document.title;
	var step=0
	function flash_title()
	{
		if(GetWindowIsFocus())
		{
			window._isflashingtitle=false;
			document.title=backuptitle;
			return;
		}
		step=step%6+1
		var thetext="";
		if (step==1) {document.title='>==='+thetext+'===<'}
		if (step==2) {document.title='=>=='+thetext+'==<='}
		if (step==3) {document.title='>=>='+thetext+'=<=<'}
		if (step==4) {document.title='=>=>'+thetext+'<=<='}
		if (step==5) {document.title='==>='+thetext+'=<=='}
		if (step==6) {document.title='===>'+thetext+'<==='}
		setTimeout(flash_title,400);
	}
	flash_title()
}
function ChatUI_FocusWindow()
{
	FlashWindowTitle();
	
	if( ! ChatUI_GetAutoFocus() ) return;	
	
	FocusWindow();
}

var chatui_sendmsgtimes=[];
var chatui_floodmsgsend=false;
function ChatUI_CheckFloodControl()
{
	if(IsMessenger()||GetLocation()=="Support")return true;

	var count = FloodControlCount;
	
	var maxms = FloodControlDelay* 1000;
	
	if(chatui_sendmsgtimes.length>=count)
	{
		var timespan=new Date().getTime()-chatui_sendmsgtimes[chatui_sendmsgtimes.length-count];
		if(timespan < maxms)
		{
			if(!chatui_floodmsgsend)
			{				
				ChatUI_PlaySound("Alert");
				Desktop.Alert(null,TEXT("UI_FloodControlMessage"),TEXT("UI_FloodControlTitle"));
				chatui_floodmsgsend=true;
			}
			return false;
		}
		
		//reset while 100 sent messages
		if(chatui_sendmsgtimes.length>100)
		{
			chatui_sendmsgtimes.splice(0,90);
		}
	}
	
	chatui_floodmsgsend=false;
	
	return true;
}



function ChatUI_SendMessageWithFloodControl(text,html)
{
	if(text && text.substr(0,1)=="/")
	{
		//TODO:
		return ChatUI_HandleSplashCommand(text.substring(1));
	}

	if( ! ChatUI_CheckFloodControl() )
	{
		return false;
	}
	
	if(html==null)
	{
		html=ChatUI_TranslateText(text)
	}
	var sended=SendMessage(text,html);
	
	if(sended)
	{
		chatui_sendmsgtimes[chatui_sendmsgtimes.length]=new Date().getTime();
	}
	return sended;
}
function ChatUI_SendEmotionWithFloodCoontrol(text)
{
	if( ! ChatUI_CheckFloodControl() )
	{
		return false;
	}
	
	var ismessenger=IsMessenger();
	if(ismessenger)
	{
		var tc=GetInstantContact();
		if(tc==null||!tc.IsOnline)
		{
			ChatUI_PlaySound("Alert");
			Desktop.Alert(null,TEXT("TargetUserNotOnline"),TEXT("UI_ALERT"));
			return false;
		}
	}
	
	var sended=SendEmotion(text);
	if(sended)
	{
		chatui_sendmsgtimes[chatui_sendmsgtimes.length]=new Date().getTime();
	}
	return sended;
}


function ChatUI_SetContextMenu(obj,func)
{
	obj.oncontextmenu=func;
	
	if(!Html_IsOpera)
	{
		return;
	}
	if(obj.getAttribute("_cui_scm")=="yes")
	{
		return;
	}
	
	obj.setAttribute("_cui_scm","yes");
	Html_AttachEventInternal(obj,"click",ChatUI_SCM_Opera_OnClick);
}
function ChatUI_SCM_Opera_OnClick(event)
{
	event=event||window.event;
	
	if(!event.shiftKey)return;
	
	var obj=event.srcElement;
	while(obj)
	{
		if(obj.getAttribute("_cui_scm")=="yes")
			break;
			
		obj=obj.parentNode;
	}
	if(obj)
	{
		var cm=obj.oncontextmenu;
		if(cm)
		{
			event.cancelBubble=true;
			return cm.apply(obj,arguments)
		}
	}
}


/****************************************************************\
	InputBox
\****************************************************************/
function InputBoxElement_RunScript(event)
{
	event=event||window.event;
	
	if(CHATUI_DEBUG && event.ctrlKey)
	{
		var element=event.srcElement||event.target;
		
		eval( element.innerText||element.value );
	}
}

function InputBoxElement_ContextMenu(event)
{
	event=event||window.event;
	
	if(!Html_FCM(event))return;
	
	var inputbox=this;
	
	if(typeof(InputBoxMenu)!="undefined")
		InputBoxMenu.ShowMenuItems(inputbox,event.clientX,event.clientY);
	
	event.returnValue=false;
	event.cancelBubble=true;
}

function ChatUI_CreateWinIEInputElement()
{
	var div=document.createElement("DIV");
	div.contentEditable=true;
	div.onkeydown=InputBoxElement_OnKeyDown
	div.onkeypress=IEInputBoxElement_OnKeyPress;
	div.ondblclick=InputBoxElement_RunScript;
	
	if(UseHookupEventForMsnImages)
	{
		div.onkeyup=HookupEventForMsnImages;
	}
	
	ChatUI_SetContextMenu(div,InputBoxElement_ContextMenu);
	
	return div;
}
function IEInputBoxElement_OnKeyPress()
{
	SetIsTyping();
	
	if( (event.keyCode=='13'||event.keyCode=='10') &&!event.shiftKey)
	{
		//event.returnValue=false;
		var inputbox=this;
		var text=inputbox.innerText;
		if( (/(http|ftp)\:\/\/\S+$/ig).exec(text))
		{
			event.keyCode=' '.charCodeAt(0);//just let http://url.com/+ENTER don't forgot it's a url.
		}
		else
		{
			event.returnValue=false;
		}
		setTimeout(DoSendMessage,10);
	}
}
function ChatUI_CreateInputTextArea()
{
	var ta=document.createElement("TEXTAREA");
	
	ta.onkeypress=InputBoxTextArea_OnKeyPress;
	ta.ondblclick=InputBoxElement_RunScript;
	ChatUI_SetContextMenu(ta,InputBoxElement_ContextMenu);
	
	return ta;
}
function InputBoxTextArea_OnKeyPress(event)
{
	event=event||window.event;
	
	SetIsTyping();
	
	if((event.keyCode=='13'||event.keyCode=='10')&&!event.shiftKey)
	{
		if(Html_IsWinIE)
			event.returnValue=false;
		else
			event.preventDefault();

		DoSendMessage();
		
		return false;
	}
	
}


function InputBoxElement_OnKeyDown(event)
{
	event=event||window.event;

	if(event.keyCode==9)
	{
		if(Html_IsWinIE)
			event.returnValue=false;
		else
			event.preventDefault();

		DoTabNextUser();
		
		return false;
	}
}

/****************************************************************\
	MSN Images
\****************************************************************/
//the data map the text to the image index
//http://messenger.msn.com/resource/Emoticons.aspx
var msnimagesarr=[
	{i:1,t:":)"},
	{i:1,t:":-)"},
	{i:2,t:":D"},
	{i:2,t:":-D"},
	{i:3,t:":O"},
	{i:3,t:":-O"},
	{i:4,t:":P"},
	{i:4,t:":-P"},
	{i:5,t:";-)"},
	{i:5,t:";)"},
	{i:6,t:":-("},
	{i:6,t:":("},
	{i:7,t:":S"},
	{i:7,t:":-S"},
	{i:8,t:":|"},
	{i:8,t:":-|"},
	{i:9,t:":'("},
	{i:10,t:":$"},
	{i:10,t:":-$"},
	{i:11,t:"(H)"},
	{i:12,t:":-@"},
	{i:12,t:":@"},
	{i:13,t:"(A)"},
	{i:14,t:"(6)"},
	{i:15,t:":-#"},
	{i:16,t:"8O|"},
	{i:17,t:"8-|"},
	{i:18,t:"^O)"},
	{i:19,t:":-*"},
	{i:20,t:"+O("},
	{i:21,t:":^)"},
	{i:22,t:"*-)"},
	{i:23,t:"<O:)"},
	{i:24,t:"8-)"},
	{i:25,t:"|-)"},
	//others
	{i:26,t:"(C)"},
	{i:27,t:"(Y)"},
	{i:28,t:"(N)"},
	{i:29,t:"(B)"},
	{i:30,t:"(D)"},
	{i:31,t:"(X)"},
	{i:32,t:"(Z)"},
	{i:33,t:"({)"},
	{i:34,t:"(})"},
	{i:35,t:":-["},
	{i:35,t:":["},
	{i:36,t:"(^)"},
	{i:37,t:"(L)"},
	{i:38,t:"(U)"},
	{i:39,t:"(K)"},
	{i:40,t:"(G)"},
	{i:41,t:"(F)"},
	{i:42,t:"(W)"},
	{i:43,t:"(P)"},
	{i:44,t:"(~)"},
	{i:45,t:"(@)"},
	{i:46,t:"(&)"},
	{i:47,t:"(T)"},
	{i:48,t:"(I)"},
	{i:49,t:"(8)"},
	{i:50,t:"(S)"},
	{i:51,t:"(*)"},
	{i:52,t:"(E)"},
	{i:53,t:"(O)"},
	{i:54,t:"(M)"},
	{i:55,t:"(SN)"},
	{i:56,t:"(BAH)"},
	{i:57,t:"(PL)"},
	{i:58,t:"(||)"},
	{i:59,t:"(PI)"},
	{i:60,t:"(SO)"},
	{i:61,t:"(AU)"},
	{i:62,t:"(AP)"},
	{i:63,t:"(UM)"},
	{i:64,t:"(IP)"},
	{i:65,t:"(CO)"},
	{i:66,t:"(MP)"},
	{i:67,t:"(ST)"},
	{i:68,t:"(LI)"},
	{i:69,t:"(MO)"}
	];
var msnimagesarr2=[];
var msnimagesarr3=[];
var msnimagesarr4=[];
var msnimagesarr5=[];
var msnimagesarr6=[];


var msnimgregexps=[];

SetupMsnImages();
function SetupMsnImages()
{
	Setup(msnimagesarr2,2);
	Setup(msnimagesarr3,3);
	Setup(msnimagesarr4,4);
	Setup(msnimagesarr5,5);
	Setup(msnimagesarr6,6);
	
	function Setup(arr,l)
	{
		for(var i=0;i<msnimagesarr.length;i++)
		{
			var o=msnimagesarr[i];
			var t=o.t;
			if(t.length==l)
				arr[arr.length]=o;
			
			var s="";
			for(var j=0;j<o.t.length;j++)
			{
				var c=o.t.charCodeAt(j).toString(16);
				if(c.length==4)
					s+="\\u"+c;
				else if(c.length==3)
					s+="\\u0"+c;
				else if(c.length==2)
					s+="\\u00"+c;
				else
					s+="\\u000"+c;
			}
			msnimgregexps[msnimgregexps.length]={r:new RegExp(s,"ig"),t:"[MSNImage="+o.i+".gif]"}
		}
	}
}

function HookupEventForMsnImages()
{
	if(document.selection.type=='None')
	{
		var range=document.selection.createRange();
		
		if(Test(msnimagesarr2,2))return;
		if(Test(msnimagesarr3,3))return;
		if(Test(msnimagesarr4,4))return;
		if(Test(msnimagesarr5,5))return;
		if(Test(msnimagesarr6,6))return;
		
		function Test(arr,l)
		{
			var sel=range.duplicate();
			sel.move('character',-l);
			sel.moveEnd('character',l);
			var txt=sel.text.toUpperCase();
			
			if(txt.length!=l)return false;

			for(var n=0;n<arr.length;n++)
			{
				var o=arr[n];
				if(txt==o.t)
				{
					sel.select();
					sel.pasteHTML("<img title='"+txt+"' width='19' height='19' src='"+CuteChatUrlBase+"images/msn/"+o.i+".gif' meaning='[MSNImage="+o.i+".gif]'/>");
					return true;
				}
			}
		}
	}
}




















/****************************************************************\
	Popups
\****************************************************************/

	
function ChatUI_ShowColorPanel(img)
{
	var chat_forecolor=document.createElement("div");
	chat_forecolor.id="chat_forecolor";
	Html_SetCssText(chat_forecolor,"position:absolute;z-index:888888;display:none;border:solid 0px #A5B6DE;padding:0px;background-color:#ffffff;");
	chat_forecolor.onmousedown=new Function("event","(event||window.event).cancelBubble=true;");
	document.body.appendChild(chat_forecolor);

	var temp_html = '';		
	var colors;
	//ColorsArray defined in settings.js
	try{
	
	}
	catch(x)
	{
		ColorsArray="#000000,#993300,#333300,#003300,#003366,#000080,#333399,#333333,";
		ColorsArray=ColorsArray+"#800000,#FF6600,#808000,#008000,#008080,#0000FF,#666699,#808080,";
		ColorsArray=ColorsArray+"#FF0000,#FF9900,#99CC00,#339966,#33CCCC,#3366FF,#800080,#999999,";
		ColorsArray=ColorsArray+"#FF00FF,#FFCC00,#FFFF00,#00FF00,#00FFFF,#00CCFF,#993366,#C0C0C0,";
		ColorsArray=ColorsArray+"#FF99CC,#FFCC99,#FFFF99,#CCFFCC,#CCFFFF,#99CCFF,#CC99FF,#FFFFFF";
	}
	colors=ColorsArray.split(",");	
						
	var total = colors.length;
	var width = 8;

	temp_html += "<table cellpadding='0' bgcolor='black' cellspacing='1' style='cursor: hand;'>";	
	for (var i=0; i<total; i++) {
		if ((i % width) == 0) 
			temp_html += "<tr>"; 
		temp_html += '<td align=center title='+colors[i]+' style="border:solid 1px '+colors[i]+';background-color:'+colors[i]+';font-size:8px;padding:0px;width:16px;height:16px;" onmouseover="this.style.border=\'1px solid #ffffff\';" onmouseout="this.style.border=\'1px solid '+colors[i]+'\';" onClick=ChatUI_SetForeColor("'+colors[i]+'")>';
		temp_html += '&nbsp;';
		temp_html += '</td>';
		
		if ( ((i+1)>=total) || (((i+1) % width) == 0)) { 
			temp_html += "</tr>";
		}
	}	
					
	temp_html += "</table>";
	
	chat_forecolor.innerHTML=temp_html;
	
	
	chat_forecolor.style.display='block';
	var pos=CalcPosition(chat_forecolor,img);
	pos.top-=chat_forecolor.firstChild.offsetHeight;
	chat_forecolor.style.left=pos.left+"px";
	chat_forecolor.style.top=pos.top-3+"px";
	
	chatui_lastcolorimg=img;
	
	Html_AttachEvent(document,"mousedown",ChatUI_ForeColor_OnDocumentMouseDown);
}
function ChatUI_ForeColor_OnDocumentMouseDown()
{
	var chat_forecolor=document.getElementById("chat_forecolor");
	document.body.removeChild(chat_forecolor);
	Html_DetachEvent(document,"mousedown",ChatUI_ForeColor_OnDocumentMouseDown);
}
var chatui_lastcolorimg=null;
function ChatUI_SetForeColor(color)
{
	ChatUI_ForeColor_OnDocumentMouseDown();
	
	chatui_lastcolorimg.style.backgroundColor=color;
	SetFontColor(color);
	_InvokeChatEvent("UICOMMAND",["UICOMMAND","FOCUSINPUT"]);
}

function ChatUI_ShowEmotionPanel(img)
{
	var chat_emotion=document.createElement("div");
	chat_emotion.id="chat_emotion";
	Html_SetCssText(chat_emotion,"position:absolute;z-index:888888;display:none;left:0px;top:0px;width:200px;height:120px;overflow:visible;");
	chat_emotion.onmousedown=new Function("event","(event||window.event).cancelBubble=true;");
	document.body.appendChild(chat_emotion);
	
	var temp_html = '';
	var emotionURL = '';
//	var emotions = new Array("emsmile.gif","emteeth.gif","emwink.gif","emsmileo.gif","emsmilep.gif","emsmiled.gif","emangry.gif","emembarrassed.gif",
//				"emcrook.gif","emsad.gif","emcry.gif","emdgust.gif","emangel.gif","emlove.gif","emunlove.gif","emmessag.gif",
//				"emcat.gif","emdog.gif","emmoon.gif","emstar.gif","emfilm.gif","emnote.gif","emrose.gif","emrosesad.gif",
//				"emclock.gif","emlips.gif","emgift.gif","emcake.gif","emphoto.gif","emidea.gif","emtea.gif","emphone.gif",
//				"emhug.gif","emhug2.gif","embeer.gif","emcocktl.gif","emmale.gif","emfemale.gif","emthup.gif","emthdown.gif");	
	
	var emotions,emotionsArray;
	
	try{
		emotionsArray=EmotionsArray;//defined in setting.js
		//throw(new Error("TODO:"));//TODO:emotionsArray
	}
	catch(x)
	{
	}
	if(emotionsArray==null||emotionsArray.Length==0)
	{
		emotionsArray="emsmile.gif,emteeth.gif,emwink.gif,emsmileo.gif,emsmilep.gif,emsmiled.gif,emangry.gif,emembarrassed.gif,";
		emotionsArray=emotionsArray+"emcrook.gif,emsad.gif,emcry.gif,emdgust.gif,emangel.gif,emlove.gif,emunlove.gif,emmessag.gif,";
		emotionsArray=emotionsArray+"emcat.gif,emdog.gif,emmoon.gif,emstar.gif,emfilm.gif,emnote.gif,emrose.gif,emrosesad.gif,";
		emotionsArray=emotionsArray+"emclock.gif,emlips.gif,emgift.gif,emcake.gif,emphoto.gif,emidea.gif,emtea.gif,emphone.gif,";
		emotionsArray=emotionsArray+"emhug.gif,emhug2.gif,embeer.gif,emcocktl.gif,emmale.gif,emfemale.gif,emthup.gif,emthdown.gif";
	}
	emotions=emotionsArray.split(",");							
	var total = emotions.length;
	var width = 8;

	temp_html += "<table cellpadding=3 bgcolor=#A5B6DE cellspacing=1 border=0 bordercolor=#A5B6DE style='cursor: hand;font-size: 3px;'>";	
	for (var i=0; i<total; i++) {
		if ((i % width) == 0) 
			temp_html += "<tr bgcolor=#ffffff>"; 
		temp_html += '<td style="BORDER: white 1px solid" onclick=ChatUI_InsertEmotion("'+emotions[i]+'") onmouseover="this.style.backgroundColor=\'#D2DBEE\';this.style.border=\'1px solid #00107b\';" onmouseout="this.style.backgroundColor=\'\';this.style.border=\'1px solid #ffffff\'"><IMG  height="19" src="'+CuteChatUrlBase+'images/emotions/'+emotions[i]+'" width="19" unselectable=on></td>';
		if ( ((i+1)>=total) || (((i+1) % width) == 0)) { 
			temp_html += "</tr>";
		}
	}				
	temp_html += "</table>";
	
	chat_emotion.innerHTML=temp_html;
	
	chat_emotion.style.display='block';
	var pos=CalcPosition(chat_emotion,img);
	pos.top-=chat_emotion.firstChild.offsetHeight;
	chat_emotion.style.left=pos.left+"px";
	chat_emotion.style.top=pos.top+"px";
	
	Html_AttachEvent(document,"mousedown",ChatUI_Emotion_OnDocumentMouseDown);
}
function ChatUI_Emotion_OnDocumentMouseDown()
{
	var chat_emotion=document.getElementById("chat_emotion");
	document.body.removeChild(chat_emotion);
	Html_DetachEvent(document,"mousedown",ChatUI_Emotion_OnDocumentMouseDown);
}
function ChatUI_InsertEmotion(emotion)
{
	ChatUI_Emotion_OnDocumentMouseDown();

	_InvokeChatEvent("UICOMMAND",["UICOMMAND","EMOTION",emotion]);
	_InvokeChatEvent("UICOMMAND",["UICOMMAND","FOCUSINPUT"]);
}
function ChatUI_InviteIntoPrivate()
{
	Desktop.Prompt(handle,TEXT("UI_SpecifyInviteName"),TEXT("UI_MENU_Invite"),"");
	
	function handle(res)
	{
		if(res)
		{
			InviteIntoPrivateChat(res);
		}
	}
}
function ChatUI_ShowAddContact()
{
	//OpenWindowWaitReturn(_ChatUI_ShowAddContact_Return,CuteChatUrlBase+"IM_AddContact.aspx","","resizable=1,status=0,menubar=0,"+GetOpenWindowSizeText(577,390));
	
	function OnPrompt(name)
	{
		if(name)
		{
			ChatUI_AddContactByName(name)
		}
	}
	Desktop.Prompt(OnPrompt,TEXT("UI_CONTACT_AddContact_subTitle"),TEXT("UI_MENU_AddContact"));
}
function _ChatUI_ShowAddContact_Return()
{
}

function ChatUI_ShowSkinPanel(img)
{
	var total = SkinNames.length;
	var width = 2;

	var chat_skin=document.createElement("div");
	chat_skin.id="chat_skin";
	Html_SetCssText(chat_skin,"position:absolute;z-index:888888;display:none;border:solid 1px #3162A6;padding:3px;background-color:#EBF1FC;");
	chat_skin.onmousedown=new Function("event","(event||window.event).cancelBubble=true;");
	document.body.appendChild(chat_skin);
	
	var html="";
	html += "<table cellpadding=3 bgcolor=#A5B6DE cellspacing=1 border=0 bordercolor=#A5B6DE style='cursor:hand;font-size: 3px;'>";
	for (var i=0; i<total; i++) {
		if ((i % width) == 0) 
			html += "<tr bgcolor=#ffffff>"; 
		var s_name = CuteChatUrlBase+"Skins/"+SkinNames[i]+".gif";
		html += '<td style="BORDER: white 1px solid" onclick=\'ChatUI_Skin_OnDocumentMouseDown();ChatUI_ChangeSkin("'+SkinNames[i]+'")\' onmouseover="this.style.backgroundColor=\'#D2DBEE\';this.style.border=\'1px solid #00107b\';" onmouseout="this.style.backgroundColor=\'\';this.style.border=\'1px solid #ffffff\'"><IMG title="'+SkinTexts[i]+'" width=80 height=64 src="'+s_name+'" unselectable=on></td>';
		if ( ((i+1)>=total) || (((i+1) % width) == 0)) { 
			html += "</tr>";
		}
	}				
	html += "</table>";
	
	chat_skin.innerHTML=html;
	
	chat_skin.style.display='block';
	var pos=CalcPosition(chat_skin,img);
	pos.top-=chat_skin.firstChild.offsetHeight+6;
	chat_skin.style.left=pos.left+"px";
	chat_skin.style.top=pos.top+"px";
	
	Html_AttachEvent(document,"mousedown",ChatUI_Skin_OnDocumentMouseDown);
}
function ChatUI_Skin_OnDocumentMouseDown()
{
	var chat_skin=document.getElementById("chat_skin");
	document.body.removeChild(chat_skin);
	Html_DetachEvent(document,"mousedown",ChatUI_Skin_OnDocumentMouseDown);
}
function ChatUI_ChangeSkin(newskin)
{
	var found=false;
	for(var i=0;i<SkinNames.length;i++)
	{
		if(newskin.toLowerCase()==SkinNames[i].toLowerCase()
			||
			newskin.toLowerCase()==SkinTexts[i].toLowerCase().replace(/\s*/g,'')
		)
		{
			newskin=SkinNames[i];
			found=true;
			break;
		}
	}
	if(!found)
	{
		Desktop.Alert(null,"Unknown skin:"+newskin,TEXT("UI_ALERT"));
		return;
	}
	if(SkinName==newskin)
	{
		return;
	}
	
	SkinName=newskin;
	SaveSkin(SkinName);

	var tags=document.getElementsByName("skincsslink");
	var tarr=[];
	for(var i=0;i<tags.length;i++)
	{
		tarr[i]=tags[i];
	}
	for(var i=0;i<tarr.length;i++)
	{
		tarr[i].parentNode.removeChild(tarr[i]);
	}
	
	var css=document.createElement("LINK")
	css.id="skincsslink";
	css.rel="stylesheet";
	css.href="Skins/"+SkinName+"/style.css";
	document.body.appendChild(css);

	HtmlUninitialize();
	HtmlInitialize();
		
	ReloadUI();
	_InvokeChatEvent("UICOMMAND",["UICOMMAND","FOCUSINPUT"]);
}

function ChatUI_ShowControlPanel(img)
{
	OptionMenu.ShowMenuItems(null,0,img.offsetHeight,img);
}

function ChatUI_GetAvatarType(user)
{
	if(user.IsAgent)
		return "Operators";
	if(user.IsModerator && GetLocation()!="Private")
	{
		return "Moderators";
	}
	if(user.IsAnonymous)
		return "Anonymous";
	return "Members";
}
function ChatUI_GetAvatar(user)
{
	var atype=ChatUI_GetAvatarType(user);
	
	var avatar=user.PublicProperties.Avatar||"";

	return CuteChatUrlBase+"DrawAvatar.Ashx?Avatar="+escape(avatar)+"&AvatarType="+atype;
}
function ChatUI_GetInstantAvatar(user)
{
	var atype="Messenger";
	var avatar=user.PublicProperties.InstantAvatar||"";
	return CuteChatUrlBase+"DrawAvatar.Ashx?Avatar="+escape(avatar)+"&AvatarType="+atype;
}

function ChatUI_ShowCharacterDialog()
{
	OpenWindowWaitReturn(ChatUI_CharacterDialog_Return,CuteChatUrlBase+"DialogCharacters.aspx?AvatarType="+ChatUI_GetAvatarType(GetMyInfo()),null
		,"resizable=1,status=0,menubar=0,"+GetOpenWindowSizeText(510,360));
}
function ChatUI_CharacterDialog_Return(avatar)
{
	if(avatar)
	{
		SetPublicProperty("AvatarCharacter",avatar);
	}
}

function ChatUI_ShowAvatarDialog()
{	
	OpenWindowWaitReturn(ChatUI_AvatarDialog_Return,CuteChatUrlBase+"DialogAvatars.aspx?AvatarType="+ChatUI_GetAvatarType(GetMyInfo()),null
		,"resizable=1,status=0,menubar=0,"+GetOpenWindowSizeText(360,300));
}
function ChatUI_AvatarDialog_Return(avatar)
{
	if(avatar)
	{
		SetAvatar(avatar);
	}
}


function ChatUI_ShowInstantAvatarDialog()
{	
	OpenWindowWaitReturn(ChatUI_InstantAvatarDialog_Return,CuteChatUrlBase+"DialogAvatars.aspx?AvatarType=Messenger",null
		,"resizable=1,status=0,menubar=0,"+GetOpenWindowSizeText(500,390));
}
function ChatUI_InstantAvatarDialog_Return(avatar)
{
	if(avatar)
	{
		SetInstantAvatar(avatar);
	}
}

function ChatUI_ShowSendFile()
{
	OpenWindowAsync(new Function("",""),CuteChatUrlBase+"DialogSendFile.aspx?PlaceName="+escape(GetPlace().Name)+"&MyGUID="+escape(GetMyInfo().Guid),null
		,"resizable=1,status=0,menubar=0,"+GetOpenWindowSizeText(400,300));
}
function ChatUI_InstantShowSendFile(contact)
{
	if(contact==null)contact=GetInstantContact();
	if(contact==null)return;
	
	if(!contact.IsOnline)
	{
		Desktop.Alert(null,TEXT("TargetUserNotOnline"),TEXT("UI_ALERT"));
		return;
	}
	
	//PlaceName="+escape(GetPlace().Name)+"&MyGUID="+escape(GetMyInfo().Guid)+"&
	OpenWindowAsync(new Function("",""),CuteChatUrlBase+"DialogSendFile.aspx?ContactId="+contact.UserId,null
		,"resizable=1,status=0,menubar=0,"+GetOpenWindowSizeText(400,320));
}

function ChatUI_ShowMessageHistory(target)
{
	var args="";
	if(target!=null)
		args="?TargetId="+escape(target.UserId);
	OpenWindowAsync(null,CuteChatUrlBase+"MessageHistory.aspx"+args,null
		,"resizable=1,status=0,menubar=0,"+GetOpenWindowSizeText(600,400));
}

/****************************************************************\
	Others
\****************************************************************/

function ChatUI_QuitWithConfirm()
{
	function HandleConfirm(res)
	{
		if(res)
		{
			Disconnect();
			
			function Quit()
			{
				if(window.opener)
					window.close();
				else
					ChatUI_QuitRedirect();
			}	
			setTimeout(Quit,100);
		}
	}
	Desktop.Confirm(HandleConfirm,TEXT("UI_AreYouSureQuit"),TEXT("UI_MENU_Signout"))
}


function ChatUI_TranslateText(text)
{
	if(text==null)return "";

	for(var i=0;i<msnimgregexps.length;i++)
	{
		var o=msnimgregexps[i];
		text=text.replace(o.r,o.t);
	}
	text=Html_Encode(text);
	text=text.split("\n").join("<br/>");
	text=text.replace(/(\S+:\/\/\S+)/ig,"<a target='_blank' href='$1'>$1</a>");
	text=ChatUI_TranslateHtml(text);

	return text;
}
function ChatUI_TranslateHtml(html)
{
	if(html==null)return "";
	html=html.replace(/\[Emotion=([^\[\]=\s]+)\]/ig,"<img align='absMiddle' src="+CuteChatUrlBase+"images/emotions/$1>");
	html=html.replace(/\[MsnImage=([^\[\]=\s]+)\]/ig,"<img align='absMiddle' src="+CuteChatUrlBase+"images/msn/$1>");
	return html;
}
function ChatUI_ProcessInnerHTML(element)
{
	if(!GlobalAllowOutsiteImage)
	{
		ChatUI_ProcessInnerHTML_RemoveOutsiteImage(element);
	}
}
function ChatUI_ProcessInnerHTML_RemoveOutsiteImage(element)
{
	if(element.tagName=="IMG")
	{
		var url=window.location.href;
		var pos1=url.indexOf("://");
		var pos2=url.indexOf("/",pos1+4);
		if(url.substring(0,pos2).toLowerCase()!=element.src.substring(0,pos2).toLowerCase())
		{
			var link=document.createElement("A");
			link.innerHTML=element.src;
			link.target="_blank";
			link.href=element.src;
			element.parentNode.insertBefore(link,element);
			element.parentNode.removeChild(element);
		}
		return;
	}
	for(var i=0;i<element.childNodes.length;i++)
	{
		ChatUI_ProcessInnerHTML_RemoveOutsiteImage(element.childNodes.item(i));
	}
}

function ChatUI_ContactToExp(contact)
{
	var exp=contact.UserId+":"+contact.DisplayName;
	
	if(!chatvars._ui_contactcache)chatvars._ui_contactcache={};
	chatvars._ui_contactcache[exp]=contact;
	
	return exp;
}
function ChatUI_ContactFromExp(exp)
{
	var pos=exp.indexOf(":");
	var userid=parseInt(exp.substr(0,pos));
	var username=exp.substring(pos+1);
	
	//return the data from Chat.js as much as possible
	var contacts=GetContacts();
	for(var i=0;i<contacts.length;i++)
	{
		var contact=contacts[i];
		if(contact.UserId!=userid)
			continue;

		if(userid!=0)
			return contact;
		
		if(username==contact.DisplayName)
			return contact;
	}
	
	//maybe the contact is removed!
	if(chatvars._ui_contactcache)
	{
		return chatvars._ui_contactcache[exp];
	}
	
	return null;
}
function ChatUI_SelectContactExp(exp,event)
{
	var contact=ChatUI_ContactFromExp(exp);
	if(contact==null)return;
	event=event||window.event;
	event.cancelBubble=true;
	SetSelectedContact(contact);
}
function ChatUI_IMOpenConversationExp(exp)
{
	var contact=ChatUI_ContactFromExp(exp);
	if(contact==null)return;
	ChatUI_IMOpenConversation(contact);
}
function ChatUI_IMOpenConversation(contact)
{
	_InvokeChatEvent("IMCOMMAND",["IMCOMMAND","CHATCONTACT",contact]);
}

function ChatUI_ShowContactMenuExp(exp,event)
{
	event=event||window.event;
	event.returnValue=false;
	event.cancelBubble=true;
	
	var contact=ChatUI_ContactFromExp(exp);
	if(contact==null)return;//NOT ONLINE..

	ChatUI_ShowContactMenu(contact,event.clientX,event.clientY);
}
function ChatUI_ShowContactMenu(contact,x,y)
{
	ContactMenu.ShowMenuItems(contact,x,y);
}

function ChatUI_ShowUserMenuExp(exp,event)
{
	event=event||window.event;
	event.returnValue=false;
	event.cancelBubble=true;
	
	var user=ChatUI_UserFromExp(exp);
	if(user==null)return;//NOT ONLINE..

	ChatUI_ShowUserMenu(user,event.clientX,event.clientY);
}
function ChatUI_ShowUserMenu(user,x,y)
{
	if(typeof(UserMenu)!="undefined")
		UserMenu.ShowMenuItems(user,x,y);
}
function ChatUI_DoUserDefaultActionExp(exp,event)
{
	event=event||window.event;
	event.returnValue=false;
	event.cancelBubble=true;
	
	var user=ChatUI_UserFromExp(exp);
	if(user==null)return;//NOT ONLINE..
	
	_InvokeChatEvent("UICOMMAND",["UICOMMAND","TRACKUSER",user]);
	_InvokeChatEvent("UICOMMAND",["UICOMMAND","FOCUSINPUT"]);
}
function ChatUI_ShowMyInfoEvent(event)
{
	event=event||window.event;
	
	event.returnValue=false;
	event.cancelBubble=true;

	ChatUI_ShowMyInfoMenu(GetMyInfo(),event.clientX,event.clientY);
}
function ChatUI_ShowMyInfoMenu(myinfo,x,y)
{
	if(typeof(MyInfoMenu)!="undefined")
		MyInfoMenu.ShowMenuItems(myinfo,x,y);
}

function ChatUI_UserToExp(user)
{
	return user.UserId;
}
function ChatUI_UserFromExp(exp)
{
	return GetUserById(exp);
}
function ChatUI_SelectUserExp(exp)
{
	var user=ChatUI_UserFromExp(exp);
	if(user==null)return;//NOT ONLINE..
	SetSelectedUser(user);
}

function ChatUI_ShowProfile(user)
{
	if(user.IsAnonymous)return;
	OpenWindowAsync(null,CuteChatUrlBase+"ViewProfile.aspx?UserId="+user.UserId+"&DisplayName="+user.DisplayName+"",null
		,"resizable=1,status=0,menubar=0,"+GetOpenWindowSizeText(577,410));
}

function ChatUI_AppendContact(div,contact)
{
	var t=document.createElement("TABLE");
	t.cellSpacing=0;
	t.cellPadding=1;
	t.border=0;
	var p=t;
	var r=t.insertRow(-1);
		
	var img=document.createElement("IMG");

	switch(contact.OnlineStatus)
	{
		case "OFFLINE":
			if(IsBlock(contact))
			{
				img.src=CuteChatUrlBase+"Images/im_blocked_offline.png";
				img.alt=TEXT("UI_MENU_Blocked");
			}
			else
			{
				img.src=CuteChatUrlBase+"Images/im_offline.png";
			}
			
			break;
		case "ONLINE":
			if(IsBlock(contact))
			{
				img.src=CuteChatUrlBase+"Images/im_blocked.png";
				img.alt=TEXT("UI_MENU_Blocked");
			}
			else
			{
				img.src=CuteChatUrlBase+"Images/im_online.png";
			}
			break;
		case "OUTTOLUNCH":
		case "BUSY":
			if(IsBlock(contact))
			{
				img.src=CuteChatUrlBase+"Images/im_blocked.png";
				//img.src=CuteChatUrlBase+"Images/icon_blocked_busy.png";//busy but offline?
				img.alt=TEXT("UI_MENU_Blocked");
			}
			else
			{
				img.src=CuteChatUrlBase+"Images/im_busy.png";
			}
			break;
		case "AWAY":
		case "PARTIAL":	
			if(IsBlock(contact))
			{
				img.src=CuteChatUrlBase+"Images/im_blocked.png";
				//img.src=CuteChatUrlBase+"Images/icon_blocked_away.png";//no this file ?
				img.alt=TEXT("UI_MENU_Blocked");
			}
			else
			{
				img.src=CuteChatUrlBase+"Images/im_away.png";
			}
			break;
		default:
			img.src=CuteChatUrlBase+"Images/im_online.png";
			break;
	}
	
	img.align='absmiddle';
	r.insertCell(-1).appendChild(img);
	
	var avatar=document.createElement("IMG");
	avatar.src=ChatUI_GetInstantAvatar(contact);
	avatar.align='absmiddle';
	avatar.style.width="16px";
	avatar.style.height="16px";
	avatar.alt=TEXT("UI_Avatar");
	avatar.title=TEXT("UI_Avatar");
	
	r.insertCell(-1).appendChild(avatar);
	
	var textcell=r.insertCell(-1);
	
	var span=document.createElement("DIV");
	
	span.className="Contact";
	span.innerHTML=Html_Encode(contact.DisplayName);
	

	switch(contact.OnlineStatus)
	{
		case "OFFLINE":
			break;
		case "ONLINE":
			span.innerHTML+=" "+TEXT("UI_USER_Online");
			break;
		case "BUSY":
			span.innerHTML+=" "+TEXT("UI_USER_Busy");
			break;
		case "AWAY":
			span.innerHTML+=" "+TEXT("UI_USER_Away");
			break;
		default:
			span.innerHTML+=" "+TEXT("UI_USER_"+contact.OnlineStatus);
			break;
	}
	
	textcell.appendChild(span);
	
	var span=document.createElement("DIV");
	//span.style.color="gray";
	//span.style.paddingLeft="4px";
	span.innerHTML=Html_Encode(contact.Description||"");
	textcell.appendChild(span);
	
	p.style.cursor="hand";
	p.onclick=new Function("event","ChatUI_SelectContactExp('"+CodeEncode(ChatUI_ContactToExp(contact))+"',event)");
	p.ondblclick=new Function("event","ChatUI_IMOpenConversationExp('"+CodeEncode(ChatUI_ContactToExp(contact))+"')");
	ChatUI_SetContextMenu(p,new Function("event","if(Html_FCM(event))ChatUI_ShowContactMenuExp('"+CodeEncode(ChatUI_ContactToExp(contact))+"',event);"));
	
	div.appendChild(p);
}

function ChatUI_AppendUser(div,user,where)
{
	user=GetUserByGuid(user.Guid)||GetUserById(user.UserId)||user;
	
	var p=document.createElement("NOBR");
	
	var img=document.createElement("IMG");
	
	img.src=ChatUI_GetAvatar(user);
	
	if(GetLocation()=="Messenger")
	{
	//	img.src=ChatUI_GetInstantAvatar(user);
	//	img.width="16";
	//	img.height="16";
	//	p.appendChild(img);
	//	p.appendChild(document.createTextNode(" "));	
	}
	else
	{
		if(IsBlock(user))
		{
			img.src=CuteChatUrlBase+"Images/im_blocked.png";
		}
		else if(user.IsAgent)
		{
			img.src=CuteChatUrlBase+"Images/icon_operator.gif";
		}
		else if(GetLocation()=="Support")
		{
			img.src=CuteChatUrlBase+"Images/user.gif";
		}		
		if(where=="USERLIST"||ShowAvatarBeforeMessage)
		{
			if(img.src)
			{
				img.align='absmiddle';
				if(img.width>19||img.height>19)
				{
					img.width="19";
					img.height="19";
				}
				p.appendChild(img);
				p.appendChild(document.createTextNode(" "));
			}
		}
		if(where=="USERLIST")
		{
			var vt=GetItemInfo(user.Guid,"VideoTime")||0;
			if( new Date().getTime() - vt < 10000 )
			{
				img.src=CuteChatUrlBase+"Images/camera.png";
				
				if(!UserEquals(user,GetMyInfo()))
				{
					img.onclick=function()
					{
						ChatUI_ShowRecieveVideoWindow(GetPlace().Name,GetItemInfo(user.Guid,"VideoName"),user.DisplayName)
					}
				}
			}
		}
	}
	
	//if(where!="USERLIST"&&ShowTimeStampBeforeMessage){
	//	p.appendChild(document.createTextNode(" ("+ new Date().toLocaleTimeString() +") "));
	//}

	var span=document.createElement("SPAN");
	
	if(UserEquals(user,GetMyInfo()))
	{
		span.className="You";
		span.innerHTML=Html_Encode(user.DisplayName);
		
		ChatUI_SetContextMenu(p,new Function("event","if(Html_FCM(event))ChatUI_ShowMyInfoEvent(event);return false;"));
	}
	else
	{
		if(IsContact(user))
			span.className="User Contact";
		else
			span.className="User";
		span.innerHTML=Html_Encode(user.DisplayName);
		
		p.style.cursor="hand";
		p.onclick=new Function("event","ChatUI_SelectUserExp('"+CodeEncode(ChatUI_UserToExp(user))+"',event)");
		p.ondblclick=new Function("event","ChatUI_DoUserDefaultActionExp('"+CodeEncode(ChatUI_UserToExp(user))+"',event)");
		ChatUI_SetContextMenu(p,new Function("event","if(Html_FCM(event))ChatUI_ShowUserMenuExp('"+CodeEncode(ChatUI_UserToExp(user))+"',event);return false;"));
	}

	if(user.IsAdmin)
	{
		span.className="Admin "+span.className;
	}
	
	p.appendChild(span);
	
	//TODO: Double check this
	if(where=="USERLIST")
	{
		switch(user.OnlineStatus)
		{
			case "OFFLINE":
				break;
			case "SILENCE"://TODO:silence image;
				span.innerHTML+=" "+TEXT("UI_USER_Silence");
				break;
			case "ONLINE":
				span.innerHTML+=" "+TEXT("UI_USER_Online");
				break;
			case "BUSY":
				span.innerHTML+=" "+TEXT("UI_USER_Busy");
				break;
			case "AWAY":
				span.innerHTML+=" "+TEXT("UI_USER_Away");
				break;
			default:
				span.innerHTML+=" "+TEXT("UI_USER_"+user.OnlineStatus);
				break;
		}
		
			
		if(user.Level!="Normal"||user.IsAdmin)
		{
			var img=document.createElement("IMG");
			if(user.IsAdmin)
			{
				img.src=CuteChatUrlBase+"Images/moderator.png";
			}
			else
			{
				if(user.Level=="Silence")img.src=CuteChatUrlBase+"Images/silence.gif";
				if(user.Level=="VIP")img.src=CuteChatUrlBase+"Images/VIP.gif";
				if(user.Level=="Speaker")img.src=CuteChatUrlBase+"Images/speaker.gif";
			}
			
			if(img.src)
			{
				img.align='absmiddle';
				if(img.width>19||img.height>19)
				{
					img.width="19";
					img.height="19";
				}
				p.appendChild(img);
				p.appendChild(document.createTextNode(" "));
			}
		}
	}
	
	div.appendChild(p);
}

function ChatUI_AppendInstantUserMessage(div,msg)
{
	var className="UserMessage";
	var user=msg.Sender;
	var table=window.document.createElement("TABLE");
	table.border=0;table.cellSpacing=0;table.cellPadding=1;
	var tr=table.insertRow(-1);
	var td=tr.insertCell(-1);
	var td2=tr.insertCell(-1);
	var span=document.createElement("SPAN");
	span.innerHTML=Html_Encode(user.DisplayName);
	td.appendChild(span);
	td.width="100%";
	td.appendChild(document.createTextNode(" "+TEXT("Says")));	

	//if(msg.Sender.IsAgent)
	//{
	//	td.className=td2.className="OperatorMessage";
	//}
		
	if(UserEquals(user,GetMyInfo()))
	{
		td.className=td2.className="You";
	}
	else
	{
		td.className=td2.className="User";
	}

	if(ShowTimeStampWebMessenger && msg.ServerTime)
	{
		if(GetLocation()=="Support")
			td2.style.cssText="text-align:right;width:80;white-space:nowrap;font: normal 8pt Arial;color:#999999";
		else
			td2.style.cssText="text-align:right;width:80;white-space:nowrap;font: normal 8pt Arial;";
		var span2=document.createElement("SPAN");
		span2.innerHTML=ChatUI_FormatTime(msg.ServerTime)+" ";
		span2.style.float="right";
		td2.appendChild(span2);
	}
	
	tr=table.insertRow(-1);
	td=tr.insertCell(-1);
	td.style.padding="4px";
	td.style.paddingLeft="10px";

	if(msg.Html)
	{
		td.innerHTML=ChatUI_TranslateHtml(msg.Html);
	}
	else
	{
		td.innerHTML=ChatUI_TranslateText(msg.Text);
	}
	if(UserEquals(msg.Sender,GetMyInfo()))
	{
		className+=" MyMessage";
	}
	if(msg.Sender.IsAgent)
	{
		className+=" OperatorMessage";
	}
	
	if(msg.Offline)
	{
		className+=" OfflineMessage";
	}
	td.className=className;
	td.colSpan=2;
	ChatUI_ProcessInnerHTML(td);
	
	if(msg.Font)
	{
		if(msg.Font.Bold)td.style.fontWeight='bold';
		if(msg.Font.Italic)td.style.fontStyle='italic';
		if(msg.Font.Underline)td.style.textDecoration='underline';
		if(msg.Font.FontName)td.style.fontFamily=msg.Font.FontName;
		if(msg.Font.FontSize)td.style.fontSize=msg.Font.FontSize;
		if(msg.Font.FontColor)td.style.color=msg.Font.FontColor;
	}
	div.appendChild(table);
}
function ChatUI_AppendMessage(div,msg,mode)
{
	ChatUI_AppendMessage_Core(div,msg,mode);
	var images=[];
	function FindNodes(node)
	{
		if(node.nodeName=="IMG")
			images.push(node);
		var l=node.childNodes.length;
		for(var i=0;i<l;i++)
		{
			//node.childNodes[i]
		}
	}
	FindNodes(div);
}
function ChatUI_AppendMessage_Core(div,msg,mode)
{
	//mode - GENERAL,INSTANTMAIN,INSTANTCHAT
	switch(msg.Type)
	{
		case "EMOTION":
			var className="EmotionMessage";
			var table=window.document.createElement("TABLE");
			table.border=0;table.cellSpacing=0;table.cellPadding=1;
			var tr=table.insertRow(-1);
			var td=tr.insertCell(-1);
			if(UserEquals(msg.Sender,GetMyInfo()))
			{
				className+=" MyMessage";
			}
			if(ShowTimeStampBeforeMessage && msg.ServerTime)
			{
				td.appendChild(document.createTextNode(" ["+ ChatUI_FormatTime(msg.ServerTime) +"] "));
			}
			ChatUI_AppendUser(td,msg.Sender,mode);
			if(mode=="INSTANTMAIN" && UserEquals(msg.Sender,GetMyInfo()) )
			{
				td.appendChild(document.createTextNode(TEXT("MSG_TO")));
				ChatUI_AppendUser(td,msg.Target,mode)
			}
			
			td.appendChild(document.createTextNode(" - "));
			
			td=tr.insertCell(-1);
			td.innerHTML=Html_Encode(msg.Text);
			table.className=className;
			div.appendChild(table);
			break;
		case "USER":
			if(IsMessenger() || GetLocation()=="Support")
			{
				ChatUI_AppendInstantUserMessage(div,msg);
				return;
			}
			var className="UserMessage";
			if(msg.Whisper&&mode=="GENERAL")
				className="WhisperUserMessage";
			var table=window.document.createElement("TABLE");
			table.border=0;table.cellSpacing=1;table.cellPadding=1;
			var tr=table.insertRow(-1);
			var td=tr.insertCell(-1);
			if(UserEquals(msg.Sender,GetMyInfo()))
			{
				className+=" MyMessage";
			}
	
			if(ShowTimeStampBeforeMessage && msg.ServerTime)
			{
				td.appendChild(document.createTextNode(" ["+ ChatUI_FormatTime(msg.ServerTime) +"] "));
			}
			ChatUI_AppendUser(td,msg.Sender,mode);
			td.style.verticalAlign="top";
			if(GetLocation()!="Support")
			{
				if(msg.Target&&msg.Target.DisplayName)
				{
					//if(GetChannel().ChannelId!=0)//ChannelId==0 -> Instant Chat
					if(mode=="INSTANTMAIN" || mode=="INSTANTCHAT")
					{
						if(mode=="INSTANTMAIN" && msg.Sender.DisplayName==GetMyInfo().DisplayName )
						{
							td.appendChild(document.createTextNode(TEXT("MSG_TO")));
							ChatUI_AppendUser(td,msg.Target,mode)
						}
					}
					else
					{
						if(msg.Whisper)
							td.appendChild(document.createTextNode(TEXT("WHISPER_TO")));
						else
							td.appendChild(document.createTextNode(TEXT("MSG_TO")));
						ChatUI_AppendUser(td,msg.Target,mode)
					}
					
				}
			}
			
			td.appendChild(document.createTextNode(": "));
			
			td=tr.insertCell(-1);
			td.style.verticalAlign="top";

			if(msg.Html)
				td.innerHTML=ChatUI_TranslateHtml(msg.Html);
			else
				td.innerHTML=ChatUI_TranslateText(msg.Text);
				
			ChatUI_ProcessInnerHTML(td);
			
			if(msg.Font)
			{
				if(msg.Font.Bold)td.style.fontWeight='bold';
				if(msg.Font.Italic)td.style.fontStyle='italic';
				if(msg.Font.Underline)td.style.textDecoration='underline';
				if(msg.Font.FontName)td.style.fontFamily=msg.Font.FontName;
				if(msg.Font.FontSize)td.style.fontSize=msg.Font.FontSize;
				if(msg.Font.FontColor)td.style.color=msg.Font.FontColor;
			}
			table.className=className;
			div.appendChild(table);
			break;
		case "ANNOUNCEMENT":
		case "SYS_INFO":
		case "SYS_ERROR":
			var span=document.createElement("span");
			if(msg.Html)
			{
				span.innerHTML=msg.Html;
			}
			else
			{
				var text=window["TEXT_SYS_"+msg.Text];
				if(!text)
				{
					text=ChatUI_TranslateText(msg.Text);
				}
				else if(msg.Arguments)
				{
					for(var i=1;i<msg.Arguments.length;i++)
					{
						text=text.split('{'+(i-1)+'}').join(msg.Arguments[i]);
					}
				}
				span.innerHTML=text;
			}
			if(span.innerHTML=="INVALIDIDENTITY")
			{
				span.innerHTML=TEXT("UI_INVALIDIDENTITY");
			}
			span.className="System"
			div.appendChild(span);
			break;
		case "LOCAL":
			//for example - ChatUI_SendMessageWithFloodControl
			var span=document.createElement("span");
			if(msg.Html)
				span.innerHTML=msg.Html;
			else
				span.innerHTML=ChatUI_TranslateText(msg.Text);
			if(msg.MessageCss)
				span.className=msg.MessageCss;
			div.appendChild(span);
			break;
		default:
			var span=document.createElement("span");
			span.innerHTML=msg.Type+" : "+(msg.Html||Html_Encode(msg.Text));
			div.appendChild(span);
			break;
	}
}




//AdminPrompt

function AdminPromptSetPassword()
{
	Desktop.Prompt(handle,TEXT("UI_PROMPT_CHANNELPASSWORD"));
	function handle(res)
	{
		if(res!=null)
		{
			AdminSetPassword(res);
		}
	}
}
function AdminPromptSetMaxOnline()
{
	Desktop.Prompt(handle,TEXT("UI_PROMPT_CHANNELMAXONLINE"));
	function handle(res)
	{
		if(res!=null)
		{
			if(isNaN(parseInt(res)))
				return;
			AdminSetMaxOnline(parseInt(res));
		}
	}
}
function AdminPromptSetEnableAnonymous()
{
	Desktop.Confirm(handle,TEXT("UI_PROMPT_ENABLEANONYMOUS"));
	function handle(res)
	{
		if(res)
		{
			AdminSetEnableAnonymous();
		}
	}
}
function AdminPromptSetDisableAnonymous()
{
	Desktop.Confirm(handle,TEXT("UI_PROMPT_DISABLEANONYMOUS"));
	function handle(res)
	{
		if(res)
		{
			AdminSetDisableAnonymous();
		}
	}
}


function AdminKickUserWithPrompt(user)
{
	Desktop.Confirm(handle,TEXT("UI_PROMPT_KICKUSER",user.DisplayName),TEXT("UI_MENU_Kick"));
	function handle(res)
	{
		if(res)
		{
			AdminKickUser(user);
		}
	}
}
function AdminDenyUserIPWithPrompt(user)
{
	Desktop.Confirm(handle,TEXT("UI_PROMPT_DENYUSERIP",user.DisplayName),TEXT("UI_MENU_DenyIP"));
	function handle(res)
	{
		if(res)
		{
			AdminDenyUserIP(user);
		}
	}
}


function SaveMessages(target)
{
	var coll=document.getElementsByTagName("BGSOUND");
	for(var i=coll.length-1;i>=0;i--)
	{
		coll.item(i).parentNode.removeChild(coll.item(i));
	}

	var win;
	
	try
	{
		var d=new Date();
		var ds=d.getFullYear()+"-"+(d.getMonth()+1)+"-"+d.getDate()+"--"+d.getHours()+"-"+d.getMinutes();
		win=open(CuteChatUrlBase+"SaveTemplate.aspx/ChatLog"+ds+".htm","CCS"+new Date().getTime()
			,"resizable=1,menubar=1,toolbar=0,statusbar=0,width=1,height=1");
		setTimeout(TestDocumentReady,1);
	}
	catch(x)
	{
		alert(x.message);
	}
	
	function TestDocumentReady()
	{
		if(win.closed)return;
		
		if(win.document==null||win.document.getElementById("chat_messagelist")==null)
		{			
			setTimeout(TestDocumentReady,1);
			
			return;
		}

		win.document.getElementById("chat_messagelist").innerHTML=target.innerHTML;

		setTimeout(Rewrite,1);
	}
	
	function Rewrite()
	{
		if(Html_IsWinIE)
		{
			win.execScript(RewriteInNewWindow+"");//copy script into win
			win.execScript("setTimeout(RewriteInNewWindow,1);");
		}
		else
		{
			var form1=win.document.getElementById("form1");
			var chat_messagelist=win.document.getElementById("chat_messagelist")
			form1.input1.value=chat_messagelist.innerHTML;
			form1.submit();
		}
	}
	
	function RewriteInNewWindow()
	{
		var html=document.documentElement.outerHTML;
				
		document.open("text/html");
		document.write(html);
		document.close();
				
		document.execCommand("SaveAs",true,null);
		top.close();
	}
}








/****************************************************************\
	Manage the IM Conversitions
\****************************************************************/

var CHATUI_COOKIETIMEOUT=3;
function ChatUI_IMDeclareMainForm()
{
	if(GetCookie("CuteChatIMMainForm")=="Activate")
	{
		window.focus();
	}
	SetCookie("CuteChatIMMainForm","Here",CHATUI_COOKIETIMEOUT);
}
function ChatUI_IMHasMainForm()
{
	return GetCookie("CuteChatIMMainForm");
}
function ChatUI_IMActivateMainForm()
{
	if(GetCookie("CuteChatIMMainForm"))//Here or Activate
	{
		SetCookie("CuteChatIMMainForm","Activate",CHATUI_COOKIETIMEOUT);
	}
}
function ChatUI_IMOpenMainForm()	//need event to prevent popup blocker
{
	if(GetCookie("CuteChatIMMainForm"))//Here or Activate
	{
		SetCookie("CuteChatIMMainForm","Activate",CHATUI_COOKIETIMEOUT);
		return true;
	}
	else
	{
		var url=__cc_urlbase+"Messenger.aspx?_t"+(new Date().getTime());
		var win;
		try
		{
			win=window.open(url,"",'status=1,width=300,height=500,resizable=1');
		}
		catch(x)
		{
		}
		
		if(win==null)
		{
			throw(new Error(TEXT("UI_PopupBlocked")));
		}
	}
}
function ChatUI_IMUnloadMainForm()
{
	SetCookie("CuteChatIMMainForm","",-1);
}


//By Cookie

function ChatUI_IMCheckCookie()
{
	setTimeout(ChatUI_IMCheckCookie,500);
	
	_InvokeChatEvent("IMCOOKIE",["IMCOOKIE","DOUPDATE"]);
	_InvokeChatEvent("CONVERSATION",["CONVERSATION","RELIST"]);
}
setTimeout(ChatUI_IMCheckCookie,500);





/****************************************************************\
	Convert The Event To Message..
\****************************************************************/


var _connectionReadyCount=0;
var _retryConnectionTimes=0;


function ChatUI_HANDLE_EVENT_CONNECTION(name,type,info1,info2)
{
	var msghtml=null;
	switch(type)
	{
		case "CONNECTING":
			if(_retryConnectionTimes==0)
				msghtml=TEXT("UI_CONNECTION_CONNECTING");
			break;
		case "ERROR":
			_retryConnectionTimes++;
			if(_retryConnectionTimes<5)
			{
				setTimeout(Connect,100);
			}
			else
			{
				_retryConnectionTimes=0;
				msghtml=TEXT("UI_CONNECTION_ERROR",info1);
			}
			break;
		case "READY":
			_connectionReadyCount++;
			if(_retryConnectionTimes==0)
				msghtml=TEXT("UI_CONNECTION_READY");
			_retryConnectionTimes=0;
			break;
		case "CANCELLED":
			msghtml=TEXT("UI_CONNECTION_CANCELLED");
			break;
		case "NOTENABLE":
			msghtml=TEXT("UI_CONNECTION_NOTENABLE");
			break;
		case "NEEDLOGIN":
			msghtml=TEXT("UI_CONNECTION_NEEDLOGIN");
			Desktop.Alert(null,TEXT("UI_CONNECTION_NEEDLOGIN"),TEXT("UI_ALERT"));
			break;
		case "NEEDNAME":
			Desktop.Prompt(ChatUI_HANDLE_EVENT_CONNECTION_NAME,TEXT("UI_CONNECTION_NeedName_Message")+(info1?(" : ("+info1+")"):""),TEXT("UI_CONNECTION_NeedName_Title"));
			break;
		case "KICK":
			msghtml=TEXT("UI_CONNECTION_Kick");
			break;
		case "LOCKED":
			msghtml=TEXT("UI_CONNECTION_Locked");
			break;
		case "REACHMAX":
			msghtml=TEXT("UI_CONNECTION_ReachMax");
			break;
		case "NEEDPASSWORD":
			msghtml=TEXT("UI_CONNECTION_NeedPassword");
			Desktop.Prompt(ChatUI_HANDLE_EVENT_CONNECTION_PASSWORD,TEXT("UI_CONNECTION_NeedPassword_Message"),TEXT("UI_CONNECTION_NeedPassword_Title"));
			break;
		case "DISCONNECT":
			msghtml=TEXT("UI_CONNECTION_Disconnect");
			break;
		case "KICK":
			msghtml=TEXT("UI_CONNECTION_KICK");
			break;
		case "REJECTED":
			msghtml=TEXT("UI_CONNECTION_REJECTED");
			break;
		case "REMOVED":
			msghtml=TEXT("UI_CONNECTION_REMOVED");
			Desktop.Confirm(function(res){if(res)Connect()},msghtml,null,TEXT("UI_MENU_Connect"),TEXT("Cancel"));
			break;
		case "NOCONNECTION":
			msghtml=TEXT("UI_CONNECTION_NOCONNECTION");
			setTimeout(Connect,100);
			break;
		default:
			msghtml=TEXT("UI_CONNECTION_"+type);
			break;
	}
	if(msghtml)
	{
		var msg={};
		msg.Type="LOCAL";
		msg.MessageCss="Connection";
		msg.Html="<img src='"+CuteChatUrlBase+"Images/arrow.gif'>"+msghtml;
		FireMessage(msg);
	}
	
	ChatUI_FocusWindow();
}
function ChatUI_HANDLE_EVENT_CONNECTION_NAME(name)
{
	if(name==null)return;
	try
	{
		SetGuestName(name);
	}
	catch(x)
	{
		//TODO:OK, the name is wrong , tell the user type the name again
		Desktop.Prompt(ChatUI_HANDLE_EVENT_CONNECTION_NAME,TEXT("UI_CONNECTION_NeedName_Message"),TEXT("UI_CONNECTION_NeedName_Title")+":"+x.message);
		return;
	}
	Connect();
}
function ChatUI_HANDLE_EVENT_CONNECTION_PASSWORD(password)
{
	if(password==null)return;
	SetPassword(password);
	Connect();
}
function ChatUI_HANDLE_EVENT_USER(name,type,user,info2,info3)
{
	var showjoinleavemsg=ShowJoinLeaveMessage;
	if(GetLocation()!="Lobby")showjoinleavemsg=true;

	var msgtext=null;
	switch(type)
	{
		case "ADDED":
			if(UserEquals(GetMyInfo(),user))
			{
				if(!user.Moderated)
				{
					Desktop.Alert(null,TEXT("UI_AdminLockRoom"),"Moderated");
				}
				return;
			}
			
			if(chatvars.connectedtime&&new Date().getTime()-chatvars.connectedtime>3000)
			{
				if(showjoinleavemsg)
				{
					ChatUI_PlaySound("ChatJoin");
					msgtext=TEXT("UI_USER_"+type,user.DisplayName);
				}
			}
			
			if(!user.Moderated)
			{
				//if I am moderator
				if(GetMyInfo().IsModerator)
				{
					function HandleModerateUserJoin(res)
					{
						if(res)
						{
							AdminAcceptUser(user);
						}
						else
						{
							AdminRejectUser(user);
						}
					}
					Desktop.Confirm(HandleModerateUserJoin,TEXT("UI_USER_JOIN_MODERATE",user.DisplayName)
						,user.DisplayName);
				}
			}
			break;
		case "REMOVED":
			if(showjoinleavemsg)
			{
				ChatUI_PlaySound("Online");
				msgtext=TEXT("UI_USER_"+type+"_"+user.RemoveReason,user.DisplayName);
			}
			break;
		case "UPDATED":
			break;
		case "RENAME":
			//info2 - newname
			msgtext=TEXT("UI_USER_"+type,user.DisplayName,info2);
			break;
	}
	if(msgtext)
	{
		var msg={};
		msg.Type="LOCAL";
		msg.MessageCss="System";
		msg.Text=msgtext;
		FireMessage(msg);
	}
}
function ChatUI_HANDLE_EVENT_RAWSTCMSG(name,type,info1,info2)
{
	var msgtext=null;
	switch(type)
	{
		case "CONTACT_ADDED":
			msgtext=TEXT("UI_CONTACT_ADDED",info1[0]);
			break;
		case "CONTACT_REMOVED":
			msgtext=TEXT("UI_CONTACT_REMOVED",info1[0]);
			break;
		case "IGNORE_ADDED":
			msgtext=TEXT("UI_IGNORE_ADDED",info1[0]);
			break;
		case "IGNORE_REMOVED":
			msgtext=TEXT("UI_IGNORE_REMOVED",info1[0]);
			break;
		
		case "PLACE_AUTOAWAYMINUTE":
			CHATUI_AUTOAWAYTIMEOUT=parseInt(info1[0])*60
			break;
		
		case "SYS_ALERT_MESSAGE":
			msgtext=TEXT(info1[0],info1[1]);
			break;
		
	}
	if(msgtext)
	{
		Desktop.Alert(null,msgtext);
	}
}


function ChatUI_HANDLE_EVENT_MESSAGE(name,type,msg,emotion)
{
	if(msg.Sender && IsBlock(msg.Sender))return;
	
	if(type=="DOEMOTE")
	{
		switch(emotion)
		{
			case "BUZZ":
				if(GetLocation()=="Support")
				{
					ChatUI_PlayBuzz();
					return;
				}
				//othercase processed in messenger code.
				break;
		}
	}
	
	if(UserEquals(GetMyInfo(),msg.Sender))return;
		
	if(msg.ServerTime)
	{
		var time=msg.ServerTime.getTime();
		if(time+120000 > new Date().getTime())
		{
			ChatUI_PlaySound("type");
		}
	}
	
	if(msg.Type!="LOCAL" && !IsMessenger())
	{
		ChatUI_FocusWindow();
	}
}

function ChatUI_HANDLE_EVENT_ITEM(evt,msgid,newitem,olditem)
{
	if(newitem.Type=="PRIVATEINVITE" && msgid=="ADDED")
	{
		function DoAccept()
		{
			AcceptPrivateChat(newitem)
			var placename=newitem.Args[3];
			OpenWindowAsync(null,CuteChatUrlBase+"Channel.aspx?Place="+placename,"","status=0,width=560,height=400,resizable=1");
		}
		function HandleInvite(res)
		{
			if(res)
			{
				DoAccept();
			}
			else
			{
				RejectPrivateChat(newitem)
			}
		}
		var userid=newitem.Args[4];
		
		if(userid==GetMyInfo().UserId)
		{
			try
			{
				DoAccept();
				return;
			}
			catch(x)
			{
				//blocked.
			}
		}
		else
		{
			var user=GetUserById(userid);
			if(user && IsBlock(user))
			{
				RejectPrivateChat(newitem)
				return;
			}
		}
		
		Desktop.Confirm(HandleInvite,TEXT("UI_INVITEPRIVATECHAT",newitem.Args[5]),TEXT("UI_InvitePrivate"))
	}
}

function ChatUI_HANDLE_EVENT_PLACE(name,type,newplace,oldplace)
{
	if(oldplace==null)
	{
		function ShowMessageLater()
		{
			if(newplace.Locked)
			{
				var msg={};
				msg.Type="LOCAL";
				msg.MessageCss="System";
				msg.Text=TEXT("UI_AdminLockRoom");
				FireMessage(msg);
			}
			if(newplace.ModerateMode)
			{
				var msg={};
				msg.Type="LOCAL";
				msg.MessageCss="System";
				msg.Text=TEXT("UI_AdminModerateModeEntering");
				FireMessage(msg);
			}
		}
		setTimeout(ShowMessageLater,1000);
	}
	else
	{
		if(newplace.Locked!=oldplace.Locked)
		{
			var msg={};
			msg.Type="LOCAL";
			msg.MessageCss="System";
			if(newplace.Locked)
				msg.Text=TEXT("UI_AdminLockRoom");
			else
				msg.Text=TEXT("UI_AdminUnLockRoom");
			FireMessage(msg);
		}
		
		if(newplace.ModerateMode!=oldplace.ModerateMode)
		{
			var msg={};
			msg.Type="LOCAL";
			msg.MessageCss="System";
			if(newplace.ModerateMode)
				msg.Text=TEXT("UI_AdminModerateModeStart");
			else
				msg.Text=TEXT("UI_AdminModerateModeEnd");
			FireMessage(msg);
		}
	}
}
function ChatUI_HANDLE_EVENT_MYINFO(name,type)
{
	if(type=="UPDATED")
	{
		if(GetMyInfo().IsAnonymous)
		{
			SetCookie("CCGuestName",GetMyInfo().DisplayName);
		}
	}
}

AttachChatEvent("ITEM",ChatUI_HANDLE_EVENT_ITEM);

AttachChatEvent("CONNECTION",ChatUI_HANDLE_EVENT_CONNECTION);
AttachChatEvent("MESSAGE",ChatUI_HANDLE_EVENT_MESSAGE);
AttachChatEvent("USER",ChatUI_HANDLE_EVENT_USER);
AttachChatEvent("RAWSTCMSG",ChatUI_HANDLE_EVENT_RAWSTCMSG);
AttachChatEvent("PLACE",ChatUI_HANDLE_EVENT_PLACE);
AttachChatEvent("MYINFO",ChatUI_HANDLE_EVENT_MYINFO);

if(GetCookie("CCGuestName"))
{
	//SetGuestName(GetCookie("CCGuestName"));
	chatclient.guestname=GetCookie("CCGuestName")
}
//

function ChatUI_AddContactByName(username)
{
	if(!IsConnected())return;

	var message=JoinToMsg("USER_APPCOMMAND",null,["ADDCONTACTBYNAME",username]);
	PushCTSMessage(message);
}

var _lastActiveTime=new Date().getTime();
var _lastIsAutoAway=false;
function ChatUI_HandleDocEvent()
{
	_lastActiveTime=new Date().getTime();
	
	var autoRestoreOnline=true;
	
	var myinfo=GetMyInfo();
	if(myinfo==null)return;
	if(_lastIsAutoAway && autoRestoreOnline)
	{
		if(myinfo.OnlineStatus=="AWAY")
		{
			SetOnlineStatus("ONLINE");
		}
	}
	_lastIsAutoAway=false;
}

function CheckAway()
{
	//TODO:myinfo.PrivateProperties["AutoAwayTimeout"]
	if(!IsConnected())
	{
		_lastActiveTime=new Date().getTime();
		return;
	}
	var span=new Date().getTime()-_lastActiveTime
	if(span<CHATUI_AUTOAWAYTIMEOUT*1000)return;
	var myinfo=GetMyInfo();
	if(myinfo==null)return;

	if(myinfo.OnlineStatus!="ONLINE")return;
	if(_lastIsAutoAway)return;
	_lastIsAutoAway=true;
	SetOnlineStatus("AWAY");
	ChatUI_AlertAutoAway();
}

function ChatUI_AlertAutoAway()
{
	ChatUI_PlaySound("Alert");
	Desktop.Alert(null,TEXT("UI_MESSAGE_SETAUTOAWAY"),TEXT("UI_MENU_Away"));
}

setInterval(CheckAway,1000);

if(document.addEventListener)
{
	document.addEventListener("mouseover",ChatUI_HandleDocEvent,false);
	document.addEventListener("keydown",ChatUI_HandleDocEvent,false);
}
else
{
	document.attachEvent("onmouseover",ChatUI_HandleDocEvent);
	document.attachEvent("onkeydown",ChatUI_HandleDocEvent);
}












var cc_videomap={};
var cc_videoobj=0;
var cc_videosuf=String(new Date().getTime()%12345);

function ChatUI_GetFlashObject(flash)
{
	if(document.all)
		return flash;
	for(var i=0;i<flash.childNodes.length;i++)
	{
		if(flash.childNodes.item(i).nodeName=="EMBED")
			return flash.childNodes.item(i);
	}
	return flash;
}
function ChatUI_GetVideoUrl(channel)
{
	//channel=channel.split("-").join("_");

	if(FlashChatServer.charAt(0)=="/")
	{
		var p1=document.URL.indexOf("://");
		var p2=document.URL.indexOf("/",p1+3);
		return "rtmp"+document.URL.substring(p1,p2)+FlashChatServer+"/"+channel
	}
	
	return FlashChatServer+"/"+channel;
}
function ChatUI_GetVideoName(messengerTarget)
{
	var myinfo=GetMyInfo()||{Guid:"unknown",UserId:"unknown"}
	var vn="CC_"+cc_videosuf+"_"+myinfo.Guid+"_"+myinfo.UserId+"_x_";
	if(messengerTarget)
		vn=vn+messengerTarget
	else
		vn=vn+"_global_";
	//return vn.split(":").join("_").split("-").join("_");
	return vn.replace(/[^a-zA-Z0-9]/g,"_");
}

function ChatUI_IsPublishingVideo(messengerTarget)
{
	if(messengerTarget)
	{
		var vn=ChatUI_GetVideoName("Unique");
		var data=cc_videomap[vn];
		if(data)
		{
			if(data.targets)
			{
				for(var i=0;i<data.targets.length;i++)
				{
					if(data.targets[i].target==messengerTarget)
						return true;
				}
			}
		}
		return false;
	}
	else
	{
		var vn=ChatUI_GetVideoName("Unique");
		return !!cc_videomap[vn];
	}
}
function ChatUI_PublishVideo(channel,messengerTarget,container,width,height,onrelease)
{
	if(!FlashChatServer)
	{
		Desktop.Alert(null,TEXT("FlashChatNotAvailable"));
		return;
	}
	
	var data=ChatUI_PublishUniqueVideo(channel)
	
	if(messengerTarget)
	{
		if(!data.targets)
			data.targets=[]
		var obj={};
		obj.target=messengerTarget;
		var img=document.createElement("IMG");
		img.src=CuteChatUrlBase+"Images/webcam.png";
		img.style.margin=parseInt((width-48)/2)+"px";
		container.appendChild(img);
		obj.onrelease=function()
		{
			if(img.parentNode)
				container.removeChild(img);
			if(onrelease)
			{
				onrelease();
			}
		};
		data.targets.push(obj);
	}
	else
	{
		data.onrelease=onrelease;
	}
}
function ChatUI_PublishUniqueVideo(channel)
{
	var vn=ChatUI_GetVideoName("Unique");
	var data=cc_videomap[vn];
	if(data)return data;
	
	var container;
	var width,height;
	
	data={};
	
	var win=new CuteWebUI.HTML.Window({
		onresize:function()
		{
			if(!data.element)return;
			data.element.style.width=container.style.width
			data.element.style.height=container.style.height
			data.flash.style.width=container.style.width
			data.flash.style.height=container.style.height
		},
		onclose:function()
		{
			data.disposed=true;
			cc_videomap[vn]=null;
			ChatUI_DisposeVideoObject(data);
			if(data.onrelease)data.onrelease();
			var message=JoinToMsg("USER_BROADCAST",null,[null,"NOTIFYVIDEOCLOSE",vn]);
			PushCTSMessage(message);
		}
	});
	win.SetTitle("=== "+GetMyInfo().DisplayName+" ===");
	win.SetWidth(380);
	win.SetHeight(320);
	setTimeout(function(){
		win.SetWidth(390);
		win.SetWidth(400);
	},1);
	container=win.GetContentElement();
	width=container.offsetWidth;
	height=container.offsetHeight;
	
	data.mode="publish";
	data.window=win;
	data.container=container;
	data.element=document.createElement("DIV");
	data.element.style.width=width+"px";
	data.element.style.height=height+"px";
	//data.element.style.backgroundColor='red';
	if(container.firstChild)
		container.insertBefore(data.element,container.firstChild);
	else
		container.appendChild(data.element);
	var url=__cc_urlbase+"vc_publish.swf";
	var objid="video"+(cc_videoobj++);
	var wmode=CuteWebUI.HTML.IsWinIE?"opaque":"window"
	data.element.innerHTML='<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" width="'+width+'" height="'+height+'" id="'+objid+'" align="middle">'
		+'<param name="movie" value="'+url+'" /><param name="quality" value="high" /><param name="bgcolor" value="#ffffff" /><param name="allowScriptAccess" value="sameDomain" /><param name="wmode" value="'+wmode+'" />'
		+'<embed src="'+url+'" quality="high" bgcolor="#ffffff" width="'+width+'" height="'+height+'" name="'+objid+'" align="middle" allowScriptAccess="sameDomain" wmode="'+wmode+'" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />'
		+'</object>';
	data.flash=ChatUI_GetFlashObject(data.element.firstChild);
	setTimeout(function()
	{
		data.flash.SetVariable("FlashChatServer",ChatUI_GetVideoUrl(channel));
		data.flash.SetVariable("VideoName",vn);
	},500)
	cc_videomap[vn]=data;
	data.onrelease=function()
	{
		if(data.targets)
		{
			for(var i=0;i<data.targets.length;i++)
			{
				if(data.targets[i].onrelease)
					data.targets[i].onrelease();
			}
		}
	}
	return data
}


function ChatUI_OnVideoEvent(vn,code)
{


	var data=cc_videomap[vn];
	if(!data)
		return;

	switch(code)
	{
		case "NetConnection.Connect.Success":
			break;
		case "NetStream.Publish.Start":
			data.alive=true;
			if(data.targets)
			{
				for(var i=0;i<data.targets.length;i++)
				{
					var message=JoinToMsg("USER_BROADCAST",null,[data.targets[i].target,"NOTIFYVIDEOSTART",vn]);
					PushCTSMessage(message);
				}
			}
			else
			{
				var message=JoinToMsg("USER_BROADCAST",null,[null,"NOTIFYVIDEOSTART",vn]);
				PushCTSMessage(message);
			}
			break;
		case "NetStream.Play.Reset"://called before start.
			break;
		case "NetStream.Play.Start":
			data.alive=true;
			break;
		case "NetStream.Play.UnpublishNotify":
		case "NetConnection.Connect.Rejected":
		case "NetConnection.Connect.Closed":
		
			document.title=code+" : "+data.flash.GetVariable("FlashChatServer");

			data.alive=false;
			if(!data.disposed)
			{
				data.disposed=true;
				cc_videomap[vn]=null;
				ChatUI_DisposeVideoObject(data);
				if(data.onrelease)data.onrelease();
			}
			break;
		default:
			//alert(code+"\r\n"+vn);
			break;
	}
}
function ChatUI_RecieveVideo(channel,vn,title,container,width,height,onrelease,winmode)
{
	var data=cc_videomap[vn];
	if(data)return null;
	
	var data={};
	data.mode="recieve";
	data.onrelease=onrelease;
	data.container=container;
	data.element=document.createElement("DIV");
	data.element.style.width=width+"px";
	data.element.style.height=height+"px";
	//data.element.style.backgroundColor='red';
	if(container.firstChild)
		container.insertBefore(data.element,container.firstChild);
	else
		container.appendChild(data.element);
	var url=__cc_urlbase+"vc_recieve.swf";
	var objid="video"+(cc_videoobj++);
	if(!winmode)
		height=height-12;
	data.element.innerHTML='<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" width="'+width+'" height="'+height+'" id="'+objid+'" align="middle">'
		+'<param name="movie" value="'+url+'" /><param name="quality" value="high" /><param name="bgcolor" value="#ffffff" /><param name="allowScriptAccess" value="sameDomain" /><param name="wmode" value="opaque" />'
		+'<embed src="'+url+'" quality="high" bgcolor="#ffffff" width="'+width+'" height="'+height+'" name="'+objid+'" align="middle" allowScriptAccess="sameDomain" wmode="opaque" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />'
		+'</object>'
		+(winmode?"":('<div style="text-align:center;"><img src="'+CuteChatUrlBase+'Images/icon_enlarge.gif"/></div>'));
	data.flash=ChatUI_GetFlashObject(data.element.firstChild);
	setTimeout(function()
	{
		data.flash.SetVariable("FlashChatServer",ChatUI_GetVideoUrl(channel));
		data.flash.SetVariable("VideoName",vn);
	},500)
	if(!winmode)
	{
		data.element.onmousedown=function(event)
		{
			event=window.event||event;
			if(event.button!=1)return;
			
			data.element.onmousedown=new Function("","");
			data.onrelease=null;
			ChatUI_StopRecieveVideo(vn);
			var img=document.createElement("IMG");
			img.src=CuteChatUrlBase+"Images/webcam.png";
			img.style.margin=parseInt((width-48)/2)+"px";
			container.appendChild(img);
			data=ChatUI_ShowRecieveVideoWindow(channel,vn,title,function()
			{
				if(onrelease)onrelease();
				container.removeChild(img);
			});
		}
	}
	cc_videomap[vn]=data;
	return data;
}
function ChatUI_ShowRecieveVideoWindow(channel,vn,title,onrelease)
{
	var data=cc_videomap[vn];
	if(data)return;
	
	var container;
	var win=new CuteWebUI.HTML.Window({
		onresize:function()
		{
			if(!data)return;
			data.element.style.width=container.style.width
			data.element.style.height=container.style.height
			data.element.firstChild.style.width=container.style.width
			data.element.firstChild.style.height=container.style.height
		}
		,
		onclose:function()
		{
			if(!data)return;
			data.disposed=true;
			cc_videomap[vn]=null;
			ChatUI_DisposeVideoObject(data);
			if(onrelease)onrelease();
		}
	});
	container=win.GetContentElement();
	win.SetTitle(title);
	win.SetWidth(350);
	win.SetHeight(290);
	//alert([container.offsetWidth,container.offsetHeight])
	data=ChatUI_RecieveVideo(channel,vn,title,container,container.offsetWidth,container.offsetHeight,function(){
		win.Close();
	},true);
}

function ChatUI_StopPublishVideo(messengerTarget)
{
	if(messengerTarget)
	{
		var vn=ChatUI_GetVideoName("Unique");
		var data=cc_videomap[vn];
		if(data)
		{
			if(data.targets)
			{
				for(var i=0;i<data.targets.length;i++)
				{
					if(data.targets[i].target==messengerTarget)
					{
						var target=data.targets[i];
						data.targets.splice(i,1);
						var message=JoinToMsg("USER_BROADCAST",null,[messengerTarget,"NOTIFYVIDEOCLOSE",vn]);
						PushCTSMessage(message);
						if(target.onrelease)
							target.onrelease();
						break;
					}
				}
				if(data.targets.length==0)
				{
					data.window.Close();
					data.flash.SetVariable("_root.VideoClosed","1");
				}
			}
			else
			{
				data.window.Close();
			}
		}
	}
	else
	{
		var vn=ChatUI_GetVideoName("Unique");
		var data=cc_videomap[vn];
		if(data)
		{
			cc_videomap[vn]=null;
			ChatUI_DisposeVideoObject(data);
			var message=JoinToMsg("USER_BROADCAST",null,[null,"NOTIFYVIDEOCLOSE",vn]);
			PushCTSMessage(message);
		}
	}
}
function ChatUI_StopRecieveVideo(vn)
{
	var data=cc_videomap[vn];
	if(data)
	{
		cc_videomap[vn]=null;
		ChatUI_DisposeVideoObject(data);
		if(data.onrelease)
			data.onrelease();
	}
}

function ChatUI_DisposeVideoObject(data)
{
	if(data._disposecalled)return;
	data.flash.SetVariable("_root.VideoClosed","1");
	if(data.element.parentNode)
		data.container.removeChild(data.element);
	data._disposecalled=true;
}
function ChatUI_CheckVideoObjects()
{
	var vn;
	for(vn in cc_videomap)
	{
		var data=cc_videomap[vn];
		if(!data)
			continue;

		var e=data.element;
		var c=false;
		while(e=e.parentNode)
			if(e.nodeName=="BODY")
				c=true;
		if(!c)
		{
			cc_videomap[vn]=null;
			ChatUI_DisposeVideoObject(data);
		}
		else if(data.mode=="publish")
		{
			if(data.alive)
			{
				var time=new Date().getTime();
				if(time-(data.notifyalivetime||0)>5000)
				{
					data.notifyalivetime=time;
					if(data.targets)
					{
						for(var i=0;i<data.targets.length;i++)
						{
							var message=JoinToMsg("USER_BROADCAST",null,[data.targets[i].target,"NOTIFYVIDEOALIVE",vn]);
							PushCTSMessage(message);
						}
					}
					else
					{
						var message=JoinToMsg("USER_BROADCAST",null,[null,"NOTIFYVIDEOALIVE",vn]);
						PushCTSMessage(message);
					}
				}
			}
		}
	}
}
setInterval(ChatUI_CheckVideoObjects,1000);




