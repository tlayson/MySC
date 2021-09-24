function $(id)
{
	return document.getElementById(id)||window[id];
}
function Html_Encode(text)
{
	if(text==null)return "";
	if(typeof(text)!="string")text=text+"";
	return text.replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/\x22/g,"&quot;").replace(/\x27/g,"&#39;").replace(/\n/g,"<br/>").replace(/\r/g,"");
}
function _SL_ToBoolean(val)
{
	switch(typeof(val))
	{
		case "boolean":
			return val;
		case "string":
			switch(val.toLowerCase())
			{
				case "true":
				case "yes":
				case "on":
				case "1":
				case "-1":
					return true;
				default:
					return false;
			}
		default:
			return !!val;
	}
}

var Desktop=CuteWebUI.HTML;




var Html_Browser;

var Html_UserAgent=window.navigator.userAgent;

if(Html_UserAgent.indexOf("Gecko")!=-1)
{
	Html_Browser="Gecko";
}
else if(Html_UserAgent.indexOf("Opera")!=-1)
{
	Html_Browser="Opera";
}
else
{
	Html_Browser="IE";
}

var Html_IsWinIE=(Html_Browser=="IE");
var Html_IsGecko=(Html_Browser=="Gecko");
var Html_IsOpera=(Html_Browser=="Opera");


if(Html_IsGecko)
{
	Html_AttachEventInternal(document,"mousedown",Html_FCM_DocumentMouseDown);
}
function Html_FCM_DocumentMouseDown(event)
{
	_html_fcm_down=true;
}

//Fix ContextMenu problem of firefox ..
function Html_FCM(event)
{
	if(!Html_IsGecko)return true;
	
	if(_html_fcm_down)
	{
		_html_fcm_down=false;
		return true;
	}
	
	event.returnValue=false;
	event.cancelBubble=true;
	
	return false;
}

function Html_SetCssText(obj,text)
{
	if( ! Html_IsOpera)
	{
		obj.style.cssText=text;
		return;
	}
	
	//bug , not clear the exists css style.
	
	var rules=text.split(";");
	for(var ri=0;ri<rules.length;ri++)
	{
		var rule=rules[ri];
		var pair=rule.split(":");
		var name=pair[0].replace(/\s/g,"");
		var value=(pair[1]||"").replace(/\s/g,"");
		
		if(name.indexOf('-')!=-1)
		{
			var arr=name.split('-');
			name=arr[0];
			if(arr.length==2)
			{
				var item=arr[1];
				name+=item.substr(0,1).toUpperCase()+item.substring(1).toLowerCase();
			}
			else
			{
				for(var i=1;i<arr.length;i++)
				{
					var item=arr[i];
					name+=item.substr(0,1).toUpperCase()+item.substring(1).toLowerCase();
				}
			}
			obj.style[name]=value;
		}
		else
		{
			obj.style[name.toLowerCase()]=value;
		}
	}
}


var Html_EventNameMap={};
var Html_EventNames=[];

function Html_AttachEvent(obj,name,handler,capture)
{
	var list=Html_EventNameMap[name];
	if(list==null)
	{
		list=[];
		Html_EventNameMap[name]=list;
		Html_EventNames[Html_EventNames.length]=name;
	}

	for(var i=0;i<list.length;i++)
	{
		var item=list[i];
		if(item[0]==obj&&item[1]==name&&item[2]==handler)
		{
			var eventName=name;
			if(obj.tagName)
			{
				if(obj.type)
				{
					eventName=obj.tagName+"-"+obj.type+":"+name;
				}
				else
				{
					eventName=obj.tagName+":"+name;
				}
			}
			if(Html_Debug)
			{
				alert("Double AttachEvent!\r\n"+eventName+"\r\n"+GetStackTrace());
			}
			return;
		}
	}
	
	Html_Dom_Event_Count++;
	
	capture=!!capture;
	
	list[list.length]=[obj,name,handler,capture];

	Html_AttachEventInternal(obj,name,handler,capture);
}
function Html_AttachEventInternal(obj,name,handler,capture)
{
	if(Html_IsGecko)
	{
		obj.addEventListener(name,handler,capture);
	}
	else
	{
		obj.attachEvent("on"+name,handler);
	}
}
function Html_DetachEvent(obj,name,handler)
{
	var item;
	var list=Html_EventNameMap[name];
	if(list!=null)
	{
		for(var i=0;i<list.length;i++)
		{
			item=list[i];
			if(item[0]==obj&&item[1]==name&&item[2]==handler)
			{
				list.splice(i,1);
				break;
			}
		}
	}
	
	if(item)
	{
		Html_Dom_Event_Count--;
		
		Html_DetachEventInternal(obj,name,handler,item[3]);
		
		return;
	}

	var eventName=name;
	if(obj.tagName)
	{
		if(obj.type)
		{
			eventName=obj.tagName+"-"+obj.type+":"+name;
		}
		else
		{
			eventName=obj.tagName+":"+name;
		}
	}
	if(Html_Debug)
	{
		alert("Double DetachEvent!\r\n"+eventName+"\r\n"+GetStackTrace());
	}
}
function Html_DetachEventInternal(obj,name,handler,capture)
{
	if(Html_IsGecko)
	{
		obj.removeEventListener(name,handler,capture);
	}
	else
	{
		obj.detachEvent("on"+name,handler);
	}
}

function Html_ReleaseObjects()//called by Html_DomWindowUnload
{
	for(var k=0;k<Html_EventNames.length;k++)
	{
		var name=Html_EventNames[k];
		var list=Html_EventNameMap[name];
		for(var i=0;i<list.length;i++)
		{
			var item=list[i];
			if(Html_Debug)
			{
				alert("NoReleased:"+item[0]+":"+item[1]);
			}
			Html_DetachEventInternal(item[0],item[1],item[2]);
		}
	}

	Html_Dom_Event_Count=0;
	Html_EventNames=[];
	Html_EventNameMap={};
}




function GetStackTrace()//For debug
{

	function GetCaller(func,oldf)
	{
		if(func.GetCaller)
		{
			var c=func.GetCaller(oldf);
			if(c!=null)
				return c;
		}
		return func.caller;
	}

	var s="";
	var checkedfuncs={};
	
	var temp=null;
	var oldf=null;
	var times=0;
	//loop the function stack
	for(var f=GetStackTrace.caller;f!=null;temp=f,f=GetCaller(f,oldf),oldf=temp)
	{
		times++;
		if(times>200)break;
		
		var fn=f.MethodName;
		
		if(!fn)
		{
			fn=f+"";
			fn=fn.substr(9,fn.indexOf("(")-9);
		}
		
		if(f.ClassName)
			fn=f.ClassName+"."+fn;
		
		if(fn=="")fn="anonymous function";
		
		var cf=checkedfuncs[fn];
		
		if(!cf)
		{
			checkedfuncs[fn]=f;
		}
		else if(f==cf)
		{
			s+=fn+" ... (recursive)";
			break;
		}
		else if(typeof(cf)=='function')
		{
			checkedfuncs[fn]=[cf,f];
		}
		else
		{		
			var found=false;
			for(var i=0;i<cf.length;i++)
			{
				if(cf[i]==f)
				{
					found=true;
					break;
				}
			}
			if(found)
			{
				s+=fn+" .., (recursive)";
				break;
			}
		}
		
		if(!f.SkipStackTrace)	
		{
			s+=fn+"\n";
		}
	}
	return s;
}


function IEprompt(promptCallback,innertxt,def) {
	Desktop.Prompt(promptCallback,innertxt,"Prompt",def);
}



function Frame_GetContentWindow(frame)
{
	if(frame.contentWindow)
		return frame.contentWindow;
	
	if(frame.contentDocument)
	{
		if(frame.contentDocument.parentWindow)
			return frame.contentDocument.parentWindow;
	}
	
	var win;
	if(frame.id)
	{
		win=window.frames[frame.id];
		if(win)return win;
	}
	
	var len=window.frames.length;
	for(var i=0;i<len;i++)
	{
		win=window.frames[i];
		if(win.frameElement==frame)
			return win;
		if(win.document==frame.contentDocument)
			return win;
	}
	
	throw(new Error("iframe window not found!"));
}

function OpenWindowWaitReturn(handler,url,name,option)
{
	if(handler!=null)CuteWebUI.HTML.ShowDialogMask();
	
	var win;
	var frame;
	var ce;
	var _topclose=top.close;
	
	function _onclose(arg1,arg)
	{
		var fwin=Frame_GetContentWindow(frame);
		
		try{
		fwin.location.href="about:blank";
		//frame.close();	
		}catch(x){}
		
		if(handler==null)return;

		top.close=_topclose;
		CuteWebUI.HTML.HideDialogMask();

		var res;
		try
		{
			res=fwin.returnValue||top.returnValue;
		}
		catch(x)
		{
		}
		if(handler)
		{
			handler(res);
		}
	}
	function _onresize()
	{
		
	}
	
	win=new CuteWebUI.HTML.Window({
		onclose:_onclose
		,
		onresize:_onresize
		,
		zIndex:handler!=null?80000000:0
	});
	win.Show();
	frame=document.createElement("IFrame");
	frame.style.width="100%";
	frame.style.height="100%";
	if(name)frame.name=name;
	frame.setAttribute('frameBorder', 'no');
	
	ce=win.GetContentElement()
	ce.appendChild(frame);
	setTimeout(function(){frame.src=url;},100);

	
	var optionmap={};
	if(option)
	{
		var items=option.split(",")
		for(var i=0;i<items.length;i++)
		{
			var pair=items[i].split("=")
			optionmap[pair[0]]=pair[1]
		}
	}
	
	if(optionmap["width"])
	{
		win.SetWidth(parseInt(optionmap["width"])||100+12);
	}
	if(optionmap["height"])
	{
		win.SetHeight(parseInt(optionmap["height"])||100+32);
	}
	
	_onresize();
	
	win.MoveToScreenCenter();
	
	
	top.close=top.cc_close=function()
	{
		win.Close();
	}

	
	function updateTitle(){
		try
		{
			var fwin=Frame_GetContentWindow(frame);
			win.SetTitle(fwin.document.title);
		}
		catch(x)
		{
		}
	}
	
	setTimeout(updateTitle,100);
	setTimeout(updateTitle,200);
	setTimeout(updateTitle,500);
	setTimeout(updateTitle,1000);
	setTimeout(updateTitle,2000);
	setTimeout(updateTitle,5000);
}
function OpenWindowAsync(handler,url,name,option)
{
	OpenWindowWaitReturn(handler,url,name,option);
}


function AdminMenu_AddItems(menu)
{
	var submenu,menuitem;
	if( (GetLocation()=="Lobby"||GetLocation()=="Private") && GetMyInfo().IsAdmin)
	{
		submenu=menu.Add(1,TEXT("UI_MENU_Admin"),""+__cc_urlbase+"Images/admin.gif",null,null);
		
		menuitem=submenu.Add(1,TEXT(GetPlace().ModerateMode?"UI_Menu_CloseModeratorMode":"UI_Menu_OpenModeratorMode"),""+__cc_urlbase+"Images/ModeratorMode.gif"
			,function(){
				AdminSetModerateMode(!GetPlace().ModerateMode)
			},null);
		
		menuitem=submenu.Add(1,TEXT("UI_Menu_SetPassword"),""+__cc_urlbase+"Images/password.png"
			,function(){
				AdminPromptSetPassword();
			},null);
		
		menuitem=submenu.Add(1,TEXT(GetPlace().AllowAnonymous?"UI_Menu_DisableAnonymous":"UI_PROMPT_ENABLEANONYMOUS"),""+__cc_urlbase+"Images/Anonymous.gif"
			,function(){
				if(GetPlace().AllowAnonymous)
					AdminPromptSetDisableAnonymous();
				else
					AdminPromptSetEnableAnonymous();
			},null);
		
		menuitem=submenu.Add(1,TEXT(GetPlace().Locked?"UI_Menu_UnLockChannel":"UI_Menu_LockChannel"),""+__cc_urlbase+"Images/lock.png"
			,function(){
				AdminSetLockChannel(!GetPlace().Locked);
			},null);

		menuitem=submenu.Add(1,TEXT("UI_SetMaxUsers"),""+__cc_urlbase+"Images/group.png"
			,function(){
				AdminPromptSetMaxOnline();
			},null);
		
		menuitem=submenu.Add(1,TEXT("UI_MENU_UnKickUsers"),null
			,function(){
				AdminUnkickUsers();
			},null);

		menuitem=submenu.Add(1,TEXT("UI_MENU_UnDenyIPs"),null
			,function(){
				AdminUnDenyIPs();
			},null);
		
		if(IsAdministrator)
		{
			menuitem=submenu.Add(1,TEXT("UI_Menu_GoAdminPage"),null
				,function(){
					window.open(CuteChatUrlBase+"ChatAdmin/");
				},null);
		}
	}
}


var MyInfoMenu={}
function MyInfoMenu_AddItems(menu)
{
	var myinfo=GetMyInfo();
	
	var menuitem;
	
	if(AllowChangeName)
	{
		menuitem=menu.Add(1,TEXT("UI_ChangeName"),""+__cc_urlbase+"Images/edit.gif",function()
		{
			var myinfo=GetMyInfo();
			function OnChangeMyName(newname)
			{
				if(newname)
				{
					ChangeDisplayName(newname);
				}
			}
			Desktop.Prompt(OnChangeMyName,"Please specify new display name","Rename",myinfo.DisplayName);
		},null);
	}
	menuitem=menu.Add(1,TEXT("UI_Avatar"),ChatUI_GetAvatar(GetMyInfo()),function()
	{
		if(window.IsAvatarLayout)
		{
			ChatUI_ShowCharacterDialog();
		}
		else
		{
			ChatUI_ShowAvatarDialog();
		}
	},null);
	
	if(ShowVideoButton)
	{
		menuitem=menu.Add(1,TEXT("UI_MENU_ShowMyCamera"),""+__cc_urlbase+"Images/camera.png",function()
		{
			ChatUI_PublishVideo(GetPlace().Name);
		},null);
	}
	
	menuitem=menu.Add(1,TEXT("UI_MENU_Online"),""+__cc_urlbase+"Images/im_online.png",function()
	{
		SetOnlineStatus("ONLINE");
	},null);
	menuitem=menu.Add(1,TEXT("UI_MENU_Away"),""+__cc_urlbase+"Images/im_away.png",function()
	{
		SetOnlineStatus("AWAY");
	},null);
	menuitem=menu.Add(1,TEXT("UI_MENU_Busy"),""+__cc_urlbase+"Images/im_busy.png",function()
	{
		SetOnlineStatus("BUSY");
	},null);
	if( myinfo.IsAdmin )
	{
		menuitem=menu.Add(1,TEXT("UI_MENU_AppearOffline"),""+__cc_urlbase+"Images/im_invisible.png",function()
		{
			SetOnlineStatus("APPEAROFFLINE");
		},null);
	}
}
MyInfoMenu.ShowMenuItems=function(myinfo,x,y)
{
	if(myinfo==null)return;
	
	var menu=CreateOldMenuImplementation();
	
	MyInfoMenu_AddItems(menu)

	menu.Show(document.body,x,y);
}
var UserMenu={}
UserMenu.ShowMenuItems=function(user,x,y)
{
	var menu=CreateOldMenuImplementation();
	
	var menuitem,submemu;
	
	menuitem=menu.Add(1,TEXT("UI_MENU_TargetIt"),""+__cc_urlbase+"Images/icon_target.gif",function()
	{
		SetSelectedUser(user);
	},null);
	if(GlobalAllowPrivateMessage && GetLocation()!="Private")
	{
		menuitem=menu.Add(1,TEXT("UI_InvitePrivate"),""+__cc_urlbase+"Images/invite.png",function()
		{
			InvitePrivateChat(user);
		},null);
	}
	if(!GetMyInfo().IsAnonymous)
	{
		menuitem=menu.Add(user.IsAnonymous?0:1,TEXT(IsContact(user)?"UI_MENU_RemoveContact":"UI_MENU_AddContact"),""+__cc_urlbase+"Images/DefaultAvatar.gif",function()
		{
			if(IsContact(user))
			{
				RemoveContact(user);
			}
			else
			{
				AddContact(user);
			}
		},null);
	}
	menuitem=menu.Add(1,TEXT(IsBlock(user)?"UI_Menu_UnBlock":"UI_Menu_Block"),""+__cc_urlbase+"Images/im_blocked.png",function()
	{
		SetBlock( user , !IsBlock(user) );
	},null);
	menuitem=menu.Add(user.IsAnonymous?0:1,TEXT("UI_MENU_ViewProfile"),""+__cc_urlbase+"Images/profile.gif",function()
	{
		ChatUI_ShowProfile(user);
	},null);
	if(IsAdministrator)
	{
		submemu=menu.Add(1,TEXT("UI_Menu_UserLevel"),""+__cc_urlbase+"Images/icon_target.gif",null,null);
		menuitem=submemu.Add(1,TEXT("UI_Menu_UserLevel_Silence"),""+__cc_urlbase+"Images/silence.gif",function()
		{
			AdminSetUserLevel(user,"Silence");
		},null);
		menuitem=submemu.Add(1,TEXT("UI_Menu_UserLevel_Normal"),""+__cc_urlbase+"Images/im_online.png",function()
		{
			AdminSetUserLevel(user,"Normal");
		},null);
		menuitem=submemu.Add(1,TEXT("UI_Menu_UserLevel_VIP"),""+__cc_urlbase+"Images/vip.gif",function()
		{
			AdminSetUserLevel(user,"VIP");
		},null);
		menuitem=submemu.Add(1,TEXT("UI_Menu_UserLevel_Speaker"),""+__cc_urlbase+"Images/speaker.gif",function()
		{
			AdminSetUserLevel(user,"Speaker");
		},null);
		menuitem=submemu.Add(1,TEXT("UI_Menu_UserLevel_Moderator"),""+__cc_urlbase+"Images/moderator.png",function()
		{
			AdminAddModerator(user.DisplayName);
		},null);
	}
	if(GetMyInfo().IsAdmin)
	{
		menuitem=menu.Add(1,TEXT("UI_MENU_Kick"),""+__cc_urlbase+"Images/icon_kick.gif",function()
		{
			AdminKickUserWithPrompt(user);
		},null);
		if(IsAdministrator)
		{
			var ip=user.IPAddress;
			if(ip=="::1")ip="loopback";
			menuitem=menu.Add(1,TEXT("UI_MENU_DenyIP")+" - "+ip,null,function()
			{
				AdminKickUserWithPrompt(user);
			},null);
		}
	}
		
	menu.Show(document.body,x,y);
}

var OptionMenu={}
OptionMenu.ShowMenuItems=function(user,x,y,img)
{
	var menu=CreateOldMenuImplementation();
	
	var menuitem,submemu;
	
	menuitem=menu.Add(1,TEXT("UI_Avatar"),ChatUI_GetAvatar(GetMyInfo()),function()
	{
		ChatUI_ShowAvatarDialog();
	},null);

	menuitem=menu.Add(1,TEXT( ChatUI_GetAutoFocus()?"UI_DisableAutoFocus":"UI_EnableAutoFocus" ),CuteChatUrlBase+(ChatUI_GetAutoFocus()?"Images/focus-on.gif":"Images/focus-off.gif"),function()
	{
		ChatUI_SetAutoFocus( !ChatUI_GetAutoFocus() );
	},null);
	
	menuitem=menu.Add(1,TEXT( ChatUI_GetEnableSound()?"UI_DisableSound":"UI_EnableSound" ),CuteChatUrlBase+(ChatUI_GetEnableSound()?"Images/sound_on.png":"Images/sound_off.png"),function()
	{
		ChatUI_SetEnableSound( !ChatUI_GetEnableSound() );
	},null);

	menuitem=menu.Add(1,TEXT("UI_Help"),__cc_urlbase+"Images/help.png",function()
	{
		ChatUI_ShowHelp("MENU");
	},null);
	
	AdminMenu_AddItems(menu);
	
	menu.Show(img||document.body,x,y);
}




if($("TypingStatus"))setInterval(CheckTypingStatus,200);
var _lasttypingstatushtml="";
function CheckTypingStatus()
{
	var element=$("TypingStatus");

	var tusers=GetTypingUsers();
	if(tusers.length==0)
	{
		if(_lasttypingstatushtml!="")
		{
			element.innerHTML="";
			_lasttypingstatushtml="";
		}
	}
	else
	{
		var html="";
		for(var i=0;i<tusers.length;i++)
		{
			if(html!="")
				html+=",";
				
			var username=tusers[i];
			html+=Html_Encode(username);
		}
		if(html!=_lasttypingstatushtml)
		{
			_lasttypingstatushtml=html;
			element.innerHTML=html+" "+TEXT("UI_USER_TYPING");
		}
		
	}
}



function UpdateDocumentTitle()
{
	document.title=GetPlace().Title
}
AttachChatEvent("PLACE",UpdateDocumentTitle);


function ChannelList_OnChange(select)
{
	if(placename!=select.value)
	{
		location.href="Channel.aspx?Place="+select.value;
	}
	select.selectedIndex=0;
}

document.onkeydown=function(event)
{
	event=window.event||event;
	if(event.keyCode==8)
	{
		var e=event.srcElement||event.target;
		if(e.nodeName=="INPUT")return;
		if(e.nodeName=="TEXTAREA")return;
		if(event.preventDefault)event.preventDefault();
		return event.returnValue=false;
	}
}


function window_onunload()
{
	Disconnect(true);
	Html_ReleaseObjects();
}

window.onunload=window_onunload;
if(window.attachEvent)window.attachEvent("onunload",window_onunload);
else if(window.addEventListener)window.addEventListener("unload",window_onunload,true);	

function UserList_OnContextMenu(event)
{
	event=window.event||event;
	if(!IsConnected())return;

	var menu=CreateOldMenuImplementation();
	
	var menuitem,submemu;
	
	menuitem=menu.Add(1,TEXT("UI_MENU_TargetAll"),""+__cc_urlbase+"Images/group.png",function()
	{
		SetSelectedUser(null);
	},null);
	if(GlobalAllowSendFile)
	{
		menuitem=menu.Add(1,TEXT("UI_SendFile"),""+__cc_urlbase+"Images/icon_file.gif",function()
		{
			ChatUI_ShowSendFile();
		},null);
	}

	MyInfoMenu_AddItems(menu);
		
	menu.Show(document.body,document.documentElement.scrollLeft+event.clientX,document.documentElement.scrollTop+event.clientY);
	

	if(event.preventDefault)event.preventDefault()
	return event.returnValue=false;
}

if($("userList"))
{
	AttachChatEvent("CONNECTION",UserList_OnChatEvent);
	AttachChatEvent("USER",UserList_OnChatEvent);
	AttachChatEvent("MYINFO",UserList_OnChatEvent);
	AttachChatEvent("SELECTEDUSER",UserList_OnChatEvent);

	UserList_OnChatEvent("USER","RELIST");
	UserList_OnChatEvent("SELECTEDUSER","UPDATE");


	$("userList").oncontextmenu=UserList_OnContextMenu;
}

function UserList_OnChatEvent(name,type,info1,info2)
{
	var element=$("userList")
	
	//element.innerHTML="<br/>hello<br/>hello<br/>hello<br/>hello<br/>hello<br/>hello<br/>hello<br/>hello<br/>hello<br/>hello<br/>hello<br/>hello<br/>hello<br/>hellov";
	//return;
	
	var myinfo=GetMyInfo();
	if(myinfo.IsUnknown)//not connected.
	{
		element.innerHTML="";
		return;
	}

	function RelistUser()
	{
		element.innerHTML="";
		if(myinfo.Guid!="0")
		{
			element.appendChild(FormatUser(myinfo));//put myself on the front
			var arr=GetUsers();
			for(var i=0;i<arr.length;i++)
			{
				if(UserEquals(myinfo,arr[i]))continue;
				element.appendChild(FormatUser(arr[i]));
			}
		}
	}
	function UpdateUser(user)
	{
		var childs=element.childNodes;
		for(var i=0;i<childs.length;i++)
		{
			var div=childs.item(i);
			var divuser=ChatUI_UserFromExp(div.getAttribute("userexp"));
			if(UserEquals(divuser,user))
			{
				div.innerHTML="";
				ChatUI_AppendUser(div,user,"USERLIST")
				return;
			}
		}
		element.appendChild(FormatUser(user));
	}
	function RemoveUser(user)
	{
		var childs=element.childNodes;
		for(var i=0;i<childs.length;i++)
		{
			var div=childs.item(i);
			var divuser=ChatUI_UserFromExp(div.getAttribute("userexp"));
			if(UserEquals(divuser,user))
			{
				element.removeChild(div);
				i--;
			}
		}
	}
	function FormatUser(user)
	{
		var div=document.createElement("DIV");
		div.setAttribute("userexp",ChatUI_UserToExp(user));
		Html_SetCssText(div,"margin:2px;padding-left:8px;padding-top:2px;padding-bottom:2px;");
		div.className="UserListItem";
		ChatUI_AppendUser(div,user,"USERLIST");
		div.onclick=div.ondblclick=function(event)
		{
			event=window.event||event;
			event.cancelBubble=true;
		}
		return div;
	}
	
	if(name=="CONNECTION"&&type=="READY")RelistUser.call(this);
	if(name=="USER"&&type=="RELIST")RelistUser.call(this);
	if(name=="USER"&&type=="ADDED")UpdateUser.call(this,info1);
	if(name=="USER"&&type=="UPDATED")UpdateUser.call(this,info1);
	if(name=="USER"&&type=="REMOVED")RemoveUser.call(this,info1);
	if(name=="MYINFO"&&type=="UPDATED")UpdateUser.call(this,myinfo);
	
	if(name=="SELECTEDUSER")
	{
		var selecteduser=GetSelectedUser();
		var childs=element.childNodes;
		for(var i=0;i<childs.length;i++)
		{
			var div=childs.item(i);
			var divuser=ChatUI_UserFromExp(div.getAttribute("userexp"));
			if(UserEquals(divuser,selecteduser))
			{
				div.style.backgroundColor='#dfdfdf';
			}
			else
			{
				div.style.backgroundColor='';
			}
		}
	}

		
}	






var IEHtmlBox

function SUI_SendMessage()
{
	var text;
	var html=null;
	
	if(IEHtmlBox)
	{
		text=CuteWebUI.Utility.StringTrim(IEHtmlBox.innerText);
		html=IEHtmlBox.innerHTML;
	}
	else
	{
		text=$("inputBox").value;
		//=ChatUI_TranslateText(text);
	}

	if(text.replace(/\s/g,'').length || (html&&html.length>10) )
	{
		if(ChatUI_SendMessageWithFloodControl(text,html))
		{
			if(IEHtmlBox)
				IEHtmlBox.innerHTML="";
			else
				$("inputBox").value='';
		}
	}
	
	if(IEHtmlBox)
		IEHtmlBox.focus();
	else
		$("inputBox").focus();
}
function SUI_ClearInput()
{
	if(IEHtmlBox)
	{
		IEHtmlBox.innerHTML="";
		IEHtmlBox.focus();
	}
	else
	{
		$("inputBox").value="";
		$("inputBox").focus();
	}
}


var lastmessagetime=0;

AttachChatEvent("CONNECTION",SUI_HandleChatEvent);
AttachChatEvent("USER",SUI_HandleChatEvent);
AttachChatEvent("SELECTEDUSER",SUI_HandleChatEvent);
AttachChatEvent("MESSAGE",SUI_HandleChatEvent);
AttachChatEvent("SENDMESSAGE",SUI_HandleChatEvent);
AttachChatEvent("COMMAND",SUI_HandleChatEvent);
AttachChatEvent("UICOMMAND",SUI_HandleChatEvent);

function SUI_HandleChatEvent(name,type,info1,info2)
{
	if(name=="MESSAGE")
	{
		if(type=="NEW"||type=="RELOAD")
		{
			var msg=info1;
			
			if( msg.Sender && IsBlock(msg.Sender) )
				return;
			
			var msgdiv=document.createElement("DIV");
			ChatUI_AppendMessage(msgdiv,msg);
			$("messageList").appendChild(msgdiv);
			if(!$("messageList")._stopscroll)
			{
				$("messageList").scrollTop=$("messageList").scrollHeight;
			}
			lastmessagetime=new Date().getTime();
		}
	}
	if(name=="SENDMESSAGE")
	{
		if(type=="COMMAND")
		{
			SUI_SendMessage();
		}
	}
	
	if(name=="USER")
	{
		var userList=$("userList");
		var arr=GetUsers();
	}
	if(name=="UICOMMAND")
	{
		if(type=="EMOTION")
		{
			var emotion=info1;
			if(IEHtmlBox)
			{
				var range=document.body.createTextRange();
				range.moveToElementText(IEHtmlBox);
				range.collapse(false);
				range.pasteHTML('<IMG align="absMiddle" src="'+CuteChatUrlBase+'images/emotions/'+emotion+'" meaning="'+'[Emotion='+emotion+']'+'" border=0>')	
			}
			else
			{
				$("inputBox").value+="[Emotion="+emotion+"]";
			}
		}
		if(type=="FOCUSINPUT")
		{
			if(IEHtmlBox)
				IEHtmlBox.focus();
			else
				$("inputBox").focus();
		}
	}
}



$("inputBox").onkeypress=function(event)
{
	event=event||window.event;
	
	SetIsTyping();

	if(event.keyCode=='13'&&!event.shiftKey)
	{
		SUI_SendMessage();
		if(Html_IsWinIE)
		{
			return event.returnValue=false;
		}
		else
		{
			event.preventDefault();
		}
	}
}
$("buttonSend").onclick=function(event)
{
	event=event||window.event;
	
	SUI_SendMessage();
}

$("inputBox").onkeydown=function(event)
{
	SetIsTyping()
	
	event=event||window.event;
	if(event.keyCode==8)event.cancelBubble=true;
}

if(Html_IsWinIE && GlobalEnableHtmlBox)
{
	IEHtmlBox=ChatUI_CreateWinIEInputElement();
	IEHtmlBox.className="InputBoxElement";
	IEHtmlBox.style.cssText=$("inputBox").style.cssText
	$("inputBox").parentNode.insertBefore(IEHtmlBox,$("inputBox"));
	$("inputBox").style.display="none";
	IEHtmlBox.onkeydown=$("inputBox").onkeydown;
	IEHtmlBox.onkeypress=$("inputBox").onkeypress
}












function messageList_contextmenuclick(menuitem)
{
	var list=$("messageList");
	switch(menuitem.command)
	{
		case "MsgClear":
			list.innerHTML="";
			break;
		case "MsgReload":
			list.innerHTML="";
			var arr=GetMessages();
			for(var i=0;i<arr.length;i++)SUI_HandleChatEvent("MESSAGE","RELOAD",arr[i]);
			break;
		case "MsgSave":
			SaveMessages(list);
			break;
		case "MsgScroll":
			list._stopscroll=!list._stopscroll;
			break;
	}
}

function MessageList_OnContextMenu(event)
{
	event=event||window.event;
	
	var list=$("messageList");
	
	var menu=CreateOldMenuImplementation();
	var menuitem;
	var submenu;
	
	menuitem=menu.Add(1,TEXT("UI_MENU_Clear"),""+__cc_urlbase+"Images/cleanup.png"
		,messageList_contextmenuclick,null);
	menuitem.command="MsgClear";
	
	menuitem=menu.Add(1,TEXT("UI_MENU_ReloadMessages"),""+__cc_urlbase+"Images/refresh.png"
		,messageList_contextmenuclick,null);
	menuitem.command="MsgReload";
	
	menuitem=menu.Add(1,TEXT("UI_MENU_SaveMessages"),""+__cc_urlbase+"Images/save.png"
		,messageList_contextmenuclick,null);
	menuitem.command="MsgSave";
	
	menuitem=menu.Add(1,TEXT(list._stopscroll?"UI_MENU_AutoScroll":"UI_MENU_StopScroll"),null
		,messageList_contextmenuclick,null);
	menuitem.command="MsgScroll";

	AdminMenu_AddItems(menu);
	
	menu.Show(document.body,document.documentElement.scrollLeft+event.clientX,document.documentElement.scrollTop+event.clientY);
	
	event.cancelBubble=true;
	if(event.preventDefault)event.preventDefault();
	return event.returnValue=false;
}

$("messageList").oncontextmenu=MessageList_OnContextMenu

new function()//Connection Button
{
	var btn=$("btn_connect");
	if(!btn)return;
	
	btn.onclick=function()
	{
		if(IsConnected())
		{
			Disconnect();
		}
		else if(IsConnecting())
		{
			Disconnect();
		}
		else
		{
			Connect();
		}
		
		return false;
	}

	AttachChatEvent("CONNECTION",function(name,type,info1,info2){
		if(IsConnected())
		{
			btn.innerHTML=TEXT("Disconnect")
		}
		else if(IsConnecting())
		{
			btn.innerHTML=TEXT("CANCEL")
		}
		else
		{
			btn.innerHTML=TEXT("Connect")
		}
	});

}

new function()//Select Target
{
	var select=$("select_targetuser");
	if(!select)return;
	
	select.onchange=function()
	{
		var option=select.options[select.selectedIndex];
		var user=ChatUI_UserFromExp(option.value);
		SetSelectedUser(user);
	}
	
	function HandleUser(name,type,info1,info2){
		switch(type)
		{
			case "RELIST":
				select.options.length=1;
				var users=GetUsers();
				for(var i=0;i<users.length;i++)
				{
					var user=users[i];
					if(UserEquals(user,GetMyInfo()))
						continue;

					var option=document.createElement("OPTION");
					option.value=ChatUI_UserToExp(user);
					option.innerHTML=Html_Encode(user.DisplayName);
					select.appendChild(option);
				}
				break;
			case "ADDED":
				var user=info1;
				if(UserEquals(user,GetMyInfo()))
					break;
				var option=document.createElement("OPTION");
				option.value=ChatUI_UserToExp(user);
				option.innerHTML=Html_Encode(user.DisplayName);
				select.appendChild(option);
				break;
			case "REMOVED":
				var user=info1;
				if(UserEquals(user,GetMyInfo()))
					break;
				var exp=ChatUI_UserToExp(user);
				for(var i=0;i<select.options.length;i++)
				{
					if(select.options[i].value==exp)
					{
						select.removeChild(select.options[i]);
						break;
					}
				}
				break;
			case "UPDATED":
				break;
		}
	}
	
	function HandleSelectedUser(name,type,info1,info2)
	{
		if(type=="UPDATED")
		{
			var user=GetSelectedUser();
			if(user==null)
			{
				select.selectedIndex=0;
			}
			else
			{
				var exp=ChatUI_UserToExp(user);
				for(var i=0;i<select.options.length;i++)
				{
					var option=select.options.item(i);
					if(option.value==exp)
					{
						select.selectedIndex=i;
						//IE BUG:force it update.
						try{option.selected=true;}catch(x){}
						break;
					}
				}
			}
		}
	}
	
	function HandleConnection(name,type,info1,info2)
	{
		if(type=="READY")
		{
		
			//reset the list
			for(var i=select.options.length-1;i>0;i--)
			{
				select.removeChild(select.options[i]);
			}
		}
	}
	
	AttachChatEvent("USER",HandleUser)
	AttachChatEvent("SELECTEDUSER",HandleSelectedUser)
	AttachChatEvent("CONNECTION",HandleConnection)
}




















//ModerateMode

function ModerateMode_USER_MESSAGE()
{
	this.toString=function()
	{
		return "ModerateMode_USER_MESSAGE";
	}
	return this;
}
ModerateMode_USER_MESSAGE.prototype.GetLeftHTML=function()
{
	return this.UserName;
}
ModerateMode_USER_MESSAGE.prototype.GetRightHTML=function()
{
	var text=this.Item.Args[6];
	var html=this.Item.Args[7];
	if(!html)html=Html_Encode(text);
	return html;
}




var moderatevars;
function InitModerateVars()
{
	if(moderatevars!=null)return;
	
	var vars={};
	
	var div=document.createElement("DIV");
//	div.style.position="absolute";
//	div.style.right="240px";
//	div.style.top="30px";
//	div.style.border="outset 1px";
//	div.style.backgroundColor="white";
	div.style.paddingLeft="12px";
	div.style.paddingRight="12px";
	div.style.paddingBottom="12px";
	div.style.width="100%";
//	div.style.zIndex=3000;
	
	var newquestion=document.createElement("button");	
	newquestion.innerHTML="<img src='"+__cc_urlbase+"Images/help.png' border='0' hspace=4 align='absMiddle'>"+TEXT("CreateQuestion");
	newquestion.onclick=ModerateMode_NewQuestion;
	
	var title=document.createElement("DIV");
	title.style.width="100%";
	title.style.padding="12px";
	title.style.textAlign="center";
	title.innerHTML="  ";
	title.appendChild(newquestion);
	div.appendChild(title);
	
	var table=document.createElement("TABLE");
	table.style.width="100%";
	table.cellSpacing=0;
	table.cellPadding=4;
	table.border=1;
	//table.style.borderCollapse="collapse";
	div.appendChild(table);

	vars.mlist=table;
	
	var table=document.createElement("TABLE");
	table.style.width="100%";
	table.cellSpacing=0;
	table.cellPadding=4;
	table.border=1;
	table.style.borderCollapse="collapse";
	div.appendChild(table);
	
	vars.qlist=table;
	
	vars.panel=div;
	
	vars.scrolldiv=document.createElement("DIV");
	vars.scrolldiv.style.overflow="auto";
	vars.scrolldiv.style.backgroundColor="#E9EDF3";
	
	var win=new CuteWebUI.HTML.Window({zIndex:10000,minbtn:true,onclose:function(reason){
		moderatevars=null
	}
	,
	onresize:function()
	{
		var ce=win.GetContentElement();
		vars.scrolldiv.style.width=ce.style.width;
		vars.scrolldiv.style.height=ce.style.height
		div.style.width=Math.max(12,ce.offsetWidth-42)+"px";
	}
	});
	win.HideCloseButton();
	win.SetTitle("Moderator message queue");
	win.SetTop(60);
	win.SetLeft(300);
	win.SetWidth(480);
	win.SetHeight(320);
	
	win.GetContentElement().appendChild(vars.scrolldiv);
	vars.scrolldiv.appendChild(div);
	vars.window=win;
	
	//document.body.appendChild(div);
	
	moderatevars=vars;
}



function ModerateMode_NewQuestion()
{
	function HandlePrompt(res)
	{
		if(!res)return;
		var message=JoinToMsg("MODERATOR_COMMAND",null,["POSTQUESTION",res]);
		PushCTSMessage(message);
	}
	Desktop.Prompt(HandlePrompt,TEXT("TypeQuestion"));
}


function ModerateMode_ShowOrHidePanel()
{
	var place=GetPlace();
	function ShowPanel()
	{
		if(GetMyInfo()!=null)
		{
			if(GetMyInfo().IsAdmin)
			{
				InitModerateVars();
			}
			else
			{
				HidePanel()
			}
		}
		else
		{
			setTimeout(ShowPanel,100);
		}
	}
	function HidePanel()
	{
		//hide the panel ? ignore the messages which need be moderated?
		if(moderatevars!=null)
		{
			moderatevars.window.Close();
			//moderatevars.panel.parentNode.removeChild(moderatevars.panel);
			moderatevars=null;
		}
	}
	
	if(place.ModerateMode)
	{
		ShowPanel()
	}
	else
	{
		HidePanel()
	}
}

function ModerateMode_OnItemEvent(evt,msgid,newitem,olditem){
	if(newitem.Type!="MODERATORITEM")return;
	
	InitModerateVars();
	vars=moderatevars;

	if(msgid=="ADDED")
	{
		var obj;
		switch(newitem.Args[5])
		{
			case "USER_MESSAGE":
				obj=new ModerateMode_USER_MESSAGE();
				break;
			default:
				return;
		}
		obj.Item=newitem;
		obj.ItemGuid=newitem.Args[1];
		obj.UserGuid=newitem.Args[2];
		obj.UserId=newitem.Args[3];
		obj.UserName=newitem.Args[4];
		obj.MsgId=newitem.Args[5];

		var row=vars.mlist.insertRow(-1);
		row._obj=obj;
		var sendercell=row.insertCell(-1);
		sendercell.innerHTML=obj.GetLeftHTML();

		var messagecell=row.insertCell(-1);	
		messagecell.style.width="100%";
		messagecell.innerHTML=obj.GetRightHTML();	
			
		var opcell=row.insertCell(-1);
		var buttonAccept=document.createElement("img");
		buttonAccept.src=__cc_urlbase+"Images/focus-on.gif";
		buttonAccept.title=TEXT("Accept");
		var buttonReject=document.createElement("img");
		buttonReject.src=__cc_urlbase+"Images/delete.png";
		buttonReject.title=TEXT("Reject");
		buttonAccept.style.marginLeft="12px";
		buttonReject.style.marginLeft="12px";
		buttonReject.style.marginRight="12px";
		opcell.appendChild(buttonAccept);
		opcell.appendChild(buttonReject);
		buttonAccept.opmode="accept";
		buttonReject.opmode="reject";
		row.onclick=ModerateMode_MList_RowClick;
	}
	if(msgid=="UPDATED")
	{
		
	}
	if(msgid=="REMOVED")
	{
		for(var i=0;i<vars.mlist.rows.length;i++)
		{
			var row=vars.mlist.rows.item(i);
			
			if(row._obj.ItemGuid==newitem.Args[1])
			{
				row.parentNode.removeChild(row);
			}
		}
	}
	
	
};

function ModerateMode_OnRawMsgEvent(evt,msgid,args)
{
	if(msgid=="POSTQUESTION")
	{
		if(!GetMyInfo().IsAdmin&&GetMyInfo().Level!="Speaker")
			return;
			
		InitModerateVars();
		vars=moderatevars;
	
		var question=args[0];
		var row=vars.qlist.insertRow(-1);
		row.question=question;
		
		var sendercell=row.insertCell(-1);
		//sendercell.style.width="120px";
		sendercell.innerHTML="Question:";
		
		var messagecell=row.insertCell(-1)
		messagecell.style.width="100%";
		messagecell.innerHTML=question;
		
		var opcell=row.insertCell(-1);
		var buttonAccept=document.createElement("button");
		buttonAccept.innerHTML="Answer";
		var buttonReject=document.createElement("button");
		buttonReject.innerHTML="Ignore";
		opcell.appendChild(buttonAccept);
		opcell.appendChild(buttonReject);
		buttonAccept.opmode="answer";
		buttonReject.opmode="ignore";
		row.onclick=ModerateMode_QList_RowClick;
	}
}


function ModerateMode_MList_RowClick(event)
{
	event=window.event||event;
	var btn=event.srcElement||event.target;
	if(!btn.opmode)return;
	
	var row=this;
	var obj=row._obj;
	if(btn.opmode=="accept")
	{
		AcceptModerateItem(obj.ItemGuid);
	}
	if(btn.opmode=="reject")
	{
		RejectModerateItem(obj.ItemGuid);
	}
}
function ModerateMode_QList_RowClick(event)
{
	event=window.event||event;
	var btn=event.srcElement||event.target;
	if(!btn.opmode)return;
	
	var row=this;
	if(btn.opmode=="answer")
	{
		function HandleAnswer(msg)
		{
			if(!msg)return;
			var message=JoinToMsg("USER_COMMAND",null,["ANSWERQUESTION",row.question,msg]);
			PushCTSMessage(message);
			row.parentNode.removeChild(row);
		}
		Desktop.Prompt(HandleAnswer,row.question);
	}
	if(btn.opmode=="ignore")
	{
		row.parentNode.removeChild(row);
	}
}


AttachChatEvent("ITEM",ModerateMode_OnItemEvent);
AttachChatEvent("PLACE",ModerateMode_ShowOrHidePanel);
AttachChatEvent("MYINFO",ModerateMode_ShowOrHidePanel);
AttachChatEvent("RAWSTCMSG",ModerateMode_OnRawMsgEvent);




