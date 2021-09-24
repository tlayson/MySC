//this variable already defined in settings.js.aspx
//var Chat_Sync_Timeout=1000

var TYPINGINTERNVAL=2000;
var TYPINGDISPLAYTIME=6000
var ConnectAppearOffline=false;

function ChatService_CreateXmlHttp()
{
	var xh;
	if(typeof(XMLHttpRequest)=="undefined")
		xh=new ActiveXObject("Microsoft.XMLHTTP");
	else
		xh=new XMLHttpRequest();
	//xh.setTimeouts(1000,1000,3000,8000)
	return xh;
}
function ChatService_ParseResponse(xh,callback,onerror)
{

	
	var Exception;
	var response={};
	
	response.Messages=[];
	
	try
	{
		ParseResponse(xh)
	}
	catch(x)
	{
		if(onerror)
			onerror(x);
		else
			throw(x);
		return;
	}
	if(callback)
	{
		callback(response)
	}

	function ParseResponse(xh)
	{
		var xhstatus=-1;
		try
		{
			xhstatus=xh.status;				
		}
		catch(x)
		{
			throw(new Error('http error :'+xhstatus+':'+xh.statusText+':\n'+xh.responseText));
		}				
		if(xhstatus!=200)
		{
			throw(new Error('http error1 :'+xhstatus+':'+xh.statusText+":\n"+xh.responseText));
		}
	
		var df=xh.responseXML;
		if(df==null)
		{
			throw(new Error('http error2 :'+xh.responseText));
		}

		if(df.documentElement==null)
		{
			throw(new Error('http error3 :'+xh.responseText));
		}

		resElement=df.documentElement;
		
		if(resElement.tagName=='exception')
		{
			Exception={};
			Exception.Message=resElement.getAttribute('message');
			Exception.StackTrace=resElement.getAttribute('stacktrace');
			Exception.Description='server side '+(resElement.text||resElement.innerText||resElement.firstChild.value);
			throw(new Error(Exception.Description));
			return;
		}
		
		response.QueuePosition=resElement.getAttribute("QueuePosition");
		response.ReturnCode=resElement.getAttribute("ReturnCode");
		response.ServerMessage=resElement.getAttribute("ServerMessage");
		
		var argi=0;
		for(var i=0;i<resElement.childNodes.length;i++)
		{
			var node=resElement.childNodes.item(i);
			if(node.nodeType!=1)continue;
			
			switch(node.tagName)
			{
				case "cookie":
					response.Cookie={};
					response.Cookie.ResponseId=node.getAttribute("ResponseId");
					response.Cookie.GuestName=node.getAttribute("GuestName");
					response.Cookie.Password=node.getAttribute("Password");
					response.Cookie.ConnectionId=node.getAttribute("ConnectionId");
					response.Cookie.ConnectionKey=node.getAttribute("ConnectionKey");
					break;
				case "message":
					response.Messages.push(node.getAttribute("value"));
					break;
			}
		}
	}
}
function ChatService_SendRequest(cookie,placename,method,args,nvc,callback,onerror)
{
	var xmlhttp=ChatService_CreateXmlHttp()

	//var largedata="12345678";
	//for(var i=0;i<16;i++)largedata+=largedata;
	//xmlhttp.largedata=xmlhttp;
	
	var async=!!callback;
	xmlhttp.open("POST",chatservice_url,async);
	xmlhttp.onreadystatechange=function()
	{
		if(xmlhttp.readyState!=4)return;
		xmlhttp.onreadystatechange=new Function();
		var xh=xmlhttp;
		xmlhttp=null;
		if(typeof(ChatService_ParseResponse)=="undefined"||ChatService_ParseResponse==null)
		{
			onerror(new Error("invalid ChatService_ParseResponse?"));
			return;//just ignore the unknown error for firefox
		}
		
		//setTimeout(function(){
		ChatService_ParseResponse(xh,callback,onerror);
		//}
		//,
		//Math.random()*15000);
	}
	var arr=[];
	arr[arr.length]="PLACE="+encodeURIComponent(placename);
	arr[arr.length]="METHOD="+encodeURIComponent(method);
	var argcount=0;
	if(args!=null&&args.length!=0)
	{
		argcount=args.length;
		for(var i=0;i<argcount;i++)
		{
			var arg=args[i];
			if(arg==null)continue;
			arr[arr.length]="ARG"+i+"="+encodeURIComponent(String(arg));
		}
	}
	arr[arr.length]="ARGCOUNT="+argcount;
	var nvccount=0;
	if(nvc)
	{
		for(var key in nvc)
		{
			if(!nvc.hasOwnProperty(key))continue;
			var val=nvc[key];
			if(val==null)continue;
			arr[arr.length]="KEY"+nvccount+"="+encodeURIComponent(String(key))
			arr[arr.length]="VAL"+nvccount+"="+encodeURIComponent(String(val))
			nvccount++;
		}
	}
	arr[arr.length]="NVCCOUNT="+nvccount;
	if(cookie)
	{
		for(var key in cookie)
		{
			if(!cookie.hasOwnProperty(key))continue;
			var val=cookie[key];
			if(val==null)continue;
			arr[arr.length]="COOKIE_"+key+"="+encodeURIComponent(String(val))
		}
	}
	
	xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=utf-8");
	var data=arr.join("&")
	//alert(data);
	xmlhttp.send(data);
	return xmlhttp;
}
function ChatService_Connect(cookie,placename,callback,onerror)
{
	var nvc=null;
	if(ConnectAppearOffline)
	{
		nvc={};
		nvc.AppearOffline="1";
	}
	ChatService_SendRequest(cookie,placename,"CONNECT",null,nvc,callback,onerror);
}



var _chatEventMap={};

function AttachChatEvent(name,handler)
{
	var list=_chatEventMap[name];
	if(list==null)list=_chatEventMap[name]=[];
	for(var i=0;i<list.length;i++)
	{
		if(list[i]==handler)
			return;
	}
	list[list.length]=handler;
}
function DetachChatEvent(name,handler)
{
	var list=_chatEventMap[name];
	if(list==null)return;
	for(var i=0;i<list.length;i++)
	{
		if(list[i]==handler)
		{
			list.splice(i,1);
			return;
		}
	}
}
function _InvokeChatEvent(name,args)
{
	var list=_chatEventMap[name];
	if(list==null)return;
	
	//so that if new handler attached , the handler should not handle this event.
	list=list.concat();
	
	if(args&&args.length)
	{
		for(var i=0;i<list.length;i++)
		{
			try
			{
				list[i].apply(window,args);
			}
			catch(x)
			{
				alert("Error on event : "+name+", "+x.message+" at "+_InvokeChatEvent.caller);
				alert("Event Handler : "+list[i]);
			}
		}
	}
	else
	{
		for(var i=0;i<list.length;i++)
		{
			list[i]();
		}
	}
}



var chatclient={};
chatclient.placename="NOPLACE";

function SetGuestName(name)
{
	if(name==null)throw(_SL_CreateException(TEXT("UI_CONNECTION_MissingNickName")));
	if(name.length<3)throw(_SL_CreateException(TEXT("UI_CONNECTION_ShortNickName")));
	if(name.length>18)throw(_SL_CreateException(TEXT("UI_CONNECTION_LongNickName")));
	
	for(var i=0;i<name.length;i++)
	{
		var c=name.charCodeAt(i);
		if(c>256)
			continue;
		if(c>=48 && c<=57)
			continue;
		if(c>=65 && c<=90)
			continue;
		if(c>=97 && c<=122)
			continue;
		throw(_SL_CreateException("unsupport char '"+name.charAt(i)+"'"));
	}

	chatclient.guestname=name;
}
function SetPassword(pwd)
{
	chatclient.password=pwd;
}

function SetFontName(fontname)
{
	chatclient.fontname=fontname;
	_InvokeChatEvent("OPTION",["OPTION","FONTNAME",fontname]);
}
function GetFontName()
{
	return chatclient.fontname;
}
function SetFontSize(fontsize)
{
	chatclient.fontsize=fontsize;
	_InvokeChatEvent("OPTION",["OPTION","FONTSIZE",fontsize]);
}
function GetFontSize()
{
	return chatclient.fontsize;
}
function SetFontColor(fontcolor)
{
	chatclient.fontcolor=fontcolor;
	_InvokeChatEvent("OPTION",["OPTION","FONTCOLOR",fontcolor]);
}
function GetFontColor()
{
	return chatclient.fontcolor;
}
function SetFontBold(fontbold)
{
	chatclient.fontbold=fontbold;
	_InvokeChatEvent("OPTION",["OPTION","FONTBOLD",fontbold]);
}
function GetFontBold()
{
	return chatclient.fontbold;
}
function SetFontItalic(fontitalic)
{
	chatclient.fontitalic=fontitalic;
	_InvokeChatEvent("OPTION",["OPTION","FONTITALIC",fontitalic]);
}
function GetFontItalic()
{
	return chatclient.fontitalic;
}
function SetFontUnderline(fontunderline)
{
	chatclient.fontunderline=fontunderline;
	_InvokeChatEvent("OPTION",["OPTION","FONTUNDERLINE",fontunderline]);
}
function GetFontUnderline()
{
	return chatclient.fontunderline;
}

function SetWhisper(whisper)
{
	chatclient.whisper=whisper;
	_InvokeChatEvent("OPTION",["OPTION","WHISPER",whisper]);
}
function GetWhisper()
{
	return chatclient.whisper;
}

function SetInstantContact(contact)
{
	chatclient.targetContact=contact;
}
function GetInstantContact()
{
	return chatclient.targetContact;
}

var chatmessages=[];
var chatvars={};

__ResetChatVars()

function __ResetChatVars()
{
	chatvars={};
	chatvars.traces=[];
	chatvars.errors=[];
	
	//chatvars.place=null;
	//chatvars.myinfo=null;
	chatvars.itemmap={};
	chatvars.userbyguid={};
	chatvars.userbyname={}
	chatvars.userbyid={};
	chatvars.users=[];
	
	chatvars.contactbyid={};
	chatvars.contactbyname={};
	chatvars.ignorebyid={};
	chatvars.ignorebyname={};
	
	chatvars.contacts=[];
	chatvars.ignores=[];
	
	chatvars.tabusers=[];
	
	chatvars.cts=[];
}

function GetTraces()
{
	return chatvars.traces;
}
function GetErrors()
{
	return chatvars.errors;
}
function _Trace(message)
{
	chatvars.traces[chatvars.traces.length]=message;
	_InvokeChatEvent("TRACE",["TRACE",message]);
}
function _Error(error)
{
	chatvars.errors[chatvars.errors.length]=error;
	_InvokeChatEvent("ERROR",["ERROR",error]);
}

function _General_Return(res)
{
}
function _General_Error(err)
{
	_Error(err);
}

function SetItemInfo(guid,name,value)
{
	if(chatvars.tempiteminfomap==null)chatvars.tempiteminfomap={};
	var userinfo=chatvars.tempiteminfomap[guid];
	if(userinfo==null)
	{
		userinfo={};
		chatvars.tempiteminfomap[guid]=userinfo;
	}
	userinfo[name]=value;
}
function GetItemInfo(guid,name)
{
	if(chatvars.tempiteminfomap==null)return;
	var userinfo=chatvars.tempiteminfomap[guid];
	if(userinfo==null)return;
	return userinfo[name];
}

function IsConnected()
{
	return chatvars.connected;
}

function IsConnecting()
{
	return chatvars.connecting || chatvars.connectinqueue;
}

function Connect(placename)
{
	if(chatvars.connecting)return;
	if(chatvars.connected)return;
	
	if(!chatvars.connectinqueue)
	{
		if(placename)
		{
			chatclient.placename=placename;
		}
		__ResetChatVars();
	}

	var cookie=chatvars.cookie={};
	cookie.GuestName=chatclient.guestname;
	cookie.Password=chatclient.password;
	
	chatvars.connecting=true;
	chatvars.connect_cancelled=false;
	
	_Trace("Connecting..");
	
	ChatService_Connect(cookie,chatclient.placename,Connect_Callback,Connect_Error);
	
	if(!chatvars.connectinqueue)
	{
		_InvokeChatEvent("CONNECTION",["CONNECTION","CONNECTING"]);
	}
}
function Connect_Callback(res)
{
	if(chatvars.connect_cancelled && res.ReturnCode!="READY")return;
	
	chatvars.connecting=false;
	chatvars.connected=false;
	
	if(res.ReturnCode!="INQUEUE")
	{
		_Trace("Connect_Callback . "+res.ReturnCode+" , "+res.ServerMessage);
	}
	
	switch(res.ReturnCode)
	{
		case "READY":
			chatvars.cookie=res.Cookie||chatvars.cookie;
			chatclient.guestname=chatvars.cookie.GuestName;
			if(chatvars.connect_cancelled)
			{
				_Disconnect();
				return;
			}
			chatvars.connected=true;
			chatvars.connectedtime=new Date().getTime();
			chatvars.connectinqueue=false;

			_Chat_Start_Sync();

			_InvokeChatEvent("CONNECTION",["CONNECTION","READY"]);

			if(IsMessenger())
			{
				PushCTSMessage(JoinToMsg("USER_COMMAND",null,["LOADHISTORY"]))
				chatclient.loadedhistories=true;
			}
			
			break;
		case "INQUEUE"://TODO:not tested..
			chatvars.cookie=res.Cookie||chatvars.cookie;
			chatclient.guestname=chatvars.cookie.GuestName;
			chatvars.connectTimerid=setTimeout("Connect()",5000);
			if(!chatvars.connectinqueue)
			{
				chatvars.connectinqueue=true;
				_InvokeChatEvent("CONNECTION",["CONNECTION","INQUEUE"]);
			}
			break;
		case "LIMITED"://there are too many connections..
		case "IDEXISTS":
		case "ERROR":
		case "NOPLACE":
		case "KICK":
		case "REJECTED":
		case "REMOVED":
		case "NEEDLOGIN":
		case "NEEDNAME":
		case "LOCKED":
		case "NOTENABLE":
		case "NEEDPASSWORD":
		//case "NOCONNECTION"://only when syncing..
			chatvars.connectinqueue=false;
			_InvokeChatEvent("CONNECTION",["CONNECTION",res.ReturnCode,res.ServerMessage]);
			break;
		default:
			chatvars.connectinqueue=false;
			_Trace("Unknown connection response code : '"+res.ReturnCode+"'");
			_InvokeChatEvent("CONNECTION",["CONNECTION","ERROR","Unknown connection response code : '"+res.ReturnCode+"'"]);
			break;
	}
}
function Connect_Error(err)
{
	chatvars.connecting=false;
	chatvars.connected=false;
	chatvars.connectinqueue=false;
	
	if(chatvars.connect_cancelled)return;

	_InvokeChatEvent("CONNECTION",["CONNECTION","ERROR",err.message]);
	
	_Error(err);
}


function Disconnect(rightNow)
{
	if(chatvars.connecting)
	{
		chatvars.connecting=false;
		chatvars.connect_cancelled=true;
		chatvars.connectinqueue=false;
		_InvokeChatEvent("CONNECTION",["CONNECTION","CANCELLED"]);
	}
	if(chatvars.connectinqueue)
	{
		chatvars.connectinqueue=false;
		clearTimeout(chatvars.connectTimerid);
		_InvokeChatEvent("CONNECTION",["CONNECTION","CANCELLED"]);
		return;
	}
	if(chatvars.connected)
	{
		chatvars.connected=false;
		
		_Chat_Stop_Sync();
		
		_Disconnect(rightNow);
		
		_InvokeChatEvent("CONNECTION",["CONNECTION","DISCONNECT"]);
	}
}
function _Disconnect(rightNow)
{
	if(chatvars.cookie==null)
		return;
		
	_Trace("_Disconnect("+rightNow+")");
	
	if(rightNow)
	{
		try
		{
			ChatService_SendRequest(chatvars.cookie,chatclient.placename,"DISCONNECT");
		}
		catch(err)
		{
			_Error(err);
		}
	}
	else
	{
		ChatService_SendRequest(chatvars.cookie,chatclient.placename,"DISCONNECT",null,null,_Disconnect_Callback,_Disconnect_Error);
	}
}
function _Disconnect_Callback()
{
}
function _Disconnect_Error(err)
{
	_Error(err);
}




function DoSync()
{
	if(chatvars.syncing)
	{
		//skip use this flag
		//chatvars.forcesync=true;
		return;
	}
	else
	{
		chatvars.forcesync=false;
		__CallSync();
	}
}
function __CallSync()
{
	if(!chatvars.sync)return;
	
	var resyncforcts=!!chatvars.lastctscount;
	var sendmsg=chatvars.hasmessagesending;
	chatvars.hasmessagesending=false;
	
	var messages=chatvars.cts;
	chatvars.lastctscount=messages.length;
	
	chatvars.cts=[];
	
	var ms1=new Date().getTime() - (chatvars.typingTime||0);
	var ms2=new Date().getTime() - (chatvars.typingSend||0);
	if(ms1<TYPINGINTERNVAL&&ms2>TYPINGINTERNVAL)
	{
		chatvars.typingSend=new Date().getTime()
		
		var typingtarget=null;

		var addtyping=true;
		if(IsMessenger())
		{
			if(chatvars.typingContact==null)
			{
				addtyping=false;
			}
			else
			{
				typingtarget=chatvars.typingContact.Guid;
			}
		}
		else
		{
			if(GetSelectedUser())
			{
				typingtarget=GetSelectedUser().Guid;
			}
		}
		if(addtyping)
		{
			_Trace("_CTS:USER_TYPING:"+(typingtarget||""));
			messages.push(JoinToMsg("USER_TYPING",null,[typingtarget]));
		}
	}
	
	var nvc={};
	if(resyncforcts && RequireReSyncForCTS)
	{
		nvc["CTSRESYNC"]="1";
	}
	if(sendmsg)
	{
		nvc["HASMESSAGE"]="1";
	}

	chatvars.syncxh=ChatService_SendRequest(chatvars.cookie,chatclient.placename,"SYNCDATA",messages,nvc,_Sync_Return,_Sync_Error);
	
	chatvars.syncing=true;
}

function _Chat_Start_Sync()
{
	if(chatvars.sync)return;
	
	chatvars.sync=true;
	chatvars.synctimerid=setTimeout(_Chat_Sync_HandleTimeout,Chat_Sync_Timeout);
}
function _Chat_Stop_Sync()
{
	if(chatvars.sync)
	{
		chatvars.sync=false;
		clearTimeout(chatvars.synctimerid);
	}
}
function _Chat_Sync_HandleTimeout()
{
	//sync timeout reset here , just equals interval
	clearTimeout(chatvars.synctimerid);
	chatvars.synctimerid=setTimeout(_Chat_Sync_HandleTimeout,Chat_Sync_Timeout);
	
	var nowtime=new Date().getTime();
	
	if(chatvars.syncing)
	{
		var time=chatvars.lastsynctime||0;
		if(nowtime-time<10000)
		{
			return;
		}
		else
		{
			_Trace("* * * * * * * * re sync for xh timeout.");
			if(chatvars.syncxh!=null)
			{
				try
				{
					chatvars.syncxh.abort();
				}
				catch(x)
				{
				}
			}
		}
	}
	
	chatvars.lastsynctime=nowtime;
	__CallSync();
}

function _Sync_Return(res)
{
	chatvars.syncing=false;
	
	if(!chatvars.connected)
		return;
		
	//invalid request .
	if(res.Cookie!=null&&res.Cookie.ConnectionId!=chatvars.cookie.ConnectionId)
	{
		return;
	}
	
	_Sync_HandleResponse(res);
	
	//if I have send something.
	if(chatvars.lastctscount && RequireReSyncForCTS)
	{
		_Trace("re-sync for CTS Res");
		DoSync();
	}
	else if(chatvars.cts.length>0)
	{
		_Trace("re-sync for new CTS");
		DoSync();
	}
	else if(chatvars.forcesync)
	{
		_Trace("force sync");
		DoSync();
		//chatvars.forcesync=false;
		//clearTimeout(chatvars.synctimerid);
		//chatvars.synctimerid=setTimeout(_Chat_Sync_HandleTimeout,1);
		_Trace("force sync");
	}
	
}
function _Sync_HandleResponse(res)
{
	switch(res.ReturnCode)
	{
		case "READY":
		
			chatvars.cookie=res.Cookie||chatvars.cookie;

			//TODO:handle items
			for(var i=0;i<res.Messages.length;i++)
			{
				//try
				//{
					_Sync_HandleSTCMessage(res.Messages[i]);
				//}
				//catch(err)
				//{
				//	_Error(err);
				//}
			}
			
			//_Trace("_Sync_ReturnReady:"+chatvars.cookie.ResponseId);
			
			break;
		case "NOCONNECTION":
			_Trace("_Sync_Return:"+res.ReturnCode+":"+res.ServerMessage);
			chatvars.connected=false;
			_Chat_Stop_Sync();
			_InvokeChatEvent("CONNECTION",["CONNECTION",res.ReturnCode,res.ServerMessage]);
			break;
		case "INQUEUE"://only when connecting
		case "LIMITED"://only when connecting
		case "IDEXISTS"://only when connecting
		case "ERROR":
		case "NOPLACE":
		case "KICK":
		case "REJECTED":
		case "REMOVED":
		case "NEEDLOGIN":
		case "NEEDNAME":
		case "LOCKED":
		case "NOTENABLE":
		case "NEEDPASSWORD":
		
			
			_Trace("_Sync_Return:"+res.ReturnCode+":"+res.ServerMessage);
			
			chatvars.connected=false;
			
			_Chat_Stop_Sync();
			
			_Disconnect();
		
			_InvokeChatEvent("CONNECTION",["CONNECTION",res.ReturnCode,res.ServerMessage]);
			
			break;
		default:
			_Trace("Unknown connection response code : '"+res.ReturnCode+"'");
			_InvokeChatEvent("CONNECTION",["CONNECTION","ERROR","Unknown connection response code : '"+res.ReturnCode+"'"]);
			break;
	}
}
function _Sync_Error(err)
{
	chatvars.syncing=false;
	
	_Error(err);
	//TODO:stop if have many error ? or unable to sync for a long time?
}

function _Sync_HandleSTCMessage(message)
{
	var obj=SplitMsg(message);
	var msgid=obj.msg;
	var args=obj.args;
	var nvc=obj.nvc;
	
	_Trace("STC:"+message);
	
	_InvokeChatEvent("RAWSTCMSG",["RAWSTCMSG",msgid,args,nvc]);
	
	switch(msgid)
	{
		case "PLACE_UPDATED":
			var newplace={};
			newplace.Guid=args[0];
			newplace.Name=args[1];
			newplace.Title=args[2];
			newplace.Location=args[3];
			newplace.Locked=args[4]=="1";
			newplace.AllowAnonymous=args[5]=="1";
			newplace.ModerateMode=args[6]=="1";
			var oldplace=chatvars.place;
			chatvars.place=newplace;
			_InvokeChatEvent("PLACE",["PLACE","UPDATED",newplace,oldplace]);
			break;
		case "MYINFO_UPDATED":
			var newuser={};
			newuser.Guid=args[1];
			newuser.UserId=args[2];
			newuser.UserName=args[3];
			newuser.DisplayName=args[4];
			newuser.Description=args[5];
			newuser.IsAnonymous=args[6]=="1";
			newuser.IsAdmin=newuser.IsModerator=args[7]=="1";
			newuser.Moderated=args[8]=="1";
			newuser.OnlineStatus=args[9];
			newuser.RemoveReason=args[10];
			newuser.PublicProperties=PropStrToObj(args[11])||{}
			newuser.PrivateProperties=PropStrToObj(args[12])||{}
			newuser.IPAddress=args[13];//or "";
			newuser.AppearOffline=args[14]=="1";
			newuser.Level=args[15];
			
			ConnectAppearOffline=newuser.AppearOffline;
			
			newuser.IsOnline=true;
			var oldinfo=chatvars.myinfo;
			chatvars.myinfo=newuser;
			_InvokeChatEvent("MYINFO",["MYINFO","UPDATED",newuser,oldinfo]);
			
			if(newuser.Moderated)
			{
				if(!chatclient.loadedhistories)
				{
					PushCTSMessage(JoinToMsg("USER_COMMAND",null,["LOADHISTORY"]))
					chatclient.loadedhistories=true;
				}
			}
			
			break;
		
		case "ITEM_ADDED":
		case "ITEM_UPDATED":
		case "ITEM_REMOVED":

			var itemtype=args[0];
			var itemguid=args[1];
			
			var newitem={Type:itemtype,Guid:itemguid,Args:args,Nvc:nvc||{}};
			var olditem=chatvars.itemmap[itemguid]
			chatvars.itemmap[itemguid]=newitem;//save data to itemmap.
			newitem.Exists=(msgid!="ITEM_REMOVED");
			_InvokeChatEvent("ITEM",["ITEM",msgid.substring(5),newitem,olditem]);

			if(itemtype=="USER")
			{
				var newuser={};
				newuser.Guid=args[1];
				newuser.UserId=args[2];
				newuser.UserName=args[3];
				newuser.DisplayName=args[4];
				newuser.Description=args[5];
				newuser.IsAnonymous=args[6]=="1";
				newuser.IsAdmin=newuser.IsModerator=args[7]=="1";
				newuser.Moderated=args[8]=="1";
				newuser.OnlineStatus=args[9];
				newuser.RemoveReason=args[10];
				newuser.PublicProperties=PropStrToObj(args[11])||{}
				newuser.PrivateProperties=PropStrToObj(args[12])||{}
				newuser.InfoProperties=nvc||{};
				newuser.IsAgent=newuser.InfoProperties["IsAgent"]=="1";
				newuser.IPAddress=args[13];//or "";
				newuser.AppearOffline=args[14]=="1";
				newuser.Level=args[15];
				
				newuser.IsOnline=true;
				if(msgid=="ITEM_REMOVED")
				{
					newuser.IsOnline=false;
					newuser.OnlineStatus="OFFLINE";
				}
				
				__UpdateUserTo(newuser,GetContactById(newuser.UserId),true);
				__UpdateUserTo(newuser,GetIgnoreById(newuser.UserId),true);
				
				chatvars.userbyname[newuser.DisplayName.toLowerCase()]=newuser;
				chatvars.userbyguid[newuser.Guid]=newuser;
				chatvars.userbyid[newuser.UserId.toLowerCase()]=newuser;
				
				for(var i=0;i<chatvars.users.length;i++)
				{
					var user=chatvars.users[i];
					if(UserEquals(user,newuser))
					{
						if(msgid=="ITEM_ADDED"||msgid=="ITEM_UPDATED")
						{
							chatvars.users[i]=newuser;
							_InvokeChatEvent("USER",["USER","UPDATED",newuser,user]);
						}
						else
						{
							chatvars.users.splice(i,1);
							user.IsOnline=false;
							_InvokeChatEvent("USER",["USER","REMOVED",newuser,user]);
						}
						
						return;
					}
				}
				if(msgid=="ITEM_ADDED"||msgid=="ITEM_UPDATED")
				{
					chatvars.users.push(newuser);
					_InvokeChatEvent("USER",["USER","ADDED",newuser]);
				}
			}//if(itemtype=="USER")
			break;
		case "CONTACT_ADDED":
		case "CONTACT_UPDATED":
		case "CONTACT_REMOVED":
			var newcontact={};
			newcontact.DisplayName=args[0];// or null
			newcontact.UserId=args[1];
			newcontact.UserName=args[2];
			newcontact.Description=args[3];
			newcontact.PublicProperties=PropStrToObj(args[4])||{};
			newcontact.IsContact=true;
			
			__UpdateUserTo(GetUserById(args[1]),newcontact);
					
			if(msgid=="CONTACT_REMOVED")
			{
				chatvars.contactbyid[newcontact.UserId.toLowerCase()]=null;
				chatvars.contactbyname[newcontact.DisplayName.toLowerCase()]=null;
			}
			else
			{
				chatvars.contactbyid[newcontact.UserId.toLowerCase()]=newcontact;
				chatvars.contactbyname[newcontact.DisplayName.toLowerCase()]=newcontact;
			}
			
			for(var i=0;i<chatvars.contacts.length;i++)
			{
				var contact=chatvars.contacts[i];
				if(UserEquals(contact,newcontact))
				{
					if(msgid=="CONTACT_ADDED"||msgid=="CONTACT_UPDATED")
					{
						chatvars.contacts[i]=newcontact;
						_InvokeChatEvent("CONTACT",["CONTACT","UPDATED",newcontact,contact]);
					}
					else
					{
						chatvars.contacts.splice(i,1);
						_InvokeChatEvent("CONTACT",["CONTACT","REMOVED",newcontact,contact]);
					}
					
					var user=GetUserById(newcontact.UserId);
					if(user&&user.IsOnline)_InvokeChatEvent("USER",["USER","UPDATED",user,user]);

					return;
				}
			}
			if(msgid=="CONTACT_ADDED"||msgid=="CONTACT_UPDATED")
			{
				chatvars.contacts.push(newcontact);
				_InvokeChatEvent("CONTACT",["CONTACT","ADDED",newcontact]);
				
				var user=GetUserById(newcontact.UserId);
				if(user&&user.IsOnline)_InvokeChatEvent("USER",["USER","UPDATED",user,user]);
			}
			break;
		case "IGNORE_ADDED":
		case "IGNORE_UPDATED":
		case "IGNORE_REMOVED":
			var newignore={};
			newignore.DisplayName=args[0];// or null
			newignore.UserId=args[1];
			newignore.Description=args[2];
			newignore.PublicProperties=PropStrToObj(args[3])||{};
			newignore.IsIgnore=true;
			
			__UpdateUserTo(GetUserById(args[1]),newignore);
			
			if(msgid=="IGNORE_REMOVED")
			{
				chatvars.ignorebyid[newignore.UserId.toLowerCase()]=null;
				chatvars.ignorebyname[newignore.DisplayName.toLowerCase()]=null;
			}
			else
			{
				chatvars.ignorebyid[newignore.UserId.toLowerCase()]=newignore;
				chatvars.ignorebyname[newignore.DisplayName.toLowerCase()]=newignore;
			}
			
			
			for(var i=0;i<chatvars.ignores.length;i++)
			{
				var ignore=chatvars.ignores[i];
				if(UserEquals(ignore,newignore))
				{
					if(msgid=="IGNORE_ADDED"||msgid=="IGNORE_UPDATED")
					{
						chatvars.ignores[i]=newignore;
						_InvokeChatEvent("IGNORE",["IGNORE","UPDATED",newignore,ignore]);
					}
					else
					{
						chatvars.ignores.splice(i,1);
						_InvokeChatEvent("IGNORE",["IGNORE","REMOVED",newignore,ignore]);
					}
					
					var user=GetUserById(newignore.UserId);
					if(user&&user.IsOnline)_InvokeChatEvent("USER",["USER","UPDATED",user,user]);
					
					return;
				}
			}
			if(msgid=="IGNORE_ADDED"||msgid=="IGNORE_UPDATED")
			{
				chatvars.ignores.push(newignore);
				_InvokeChatEvent("IGNORE",["IGNORE","ADDED",newignore]);
				
				var user=GetUserById(newignore.UserId);
				if(user&&user.IsOnline)_InvokeChatEvent("USER",["USER","UPDATED",user,user]);
					
			}
			break;
		case "USER_RENAME":
			var guid=args[0];
			var newname=args[1];
			var oldname=args[2];
			var user=GetUserByGuid(guid);
			_InvokeChatEvent("USER",["USER","RENAME",user,newname,oldname]);
			break;
		case "USER_TYPING":
			var guid=args[0];
			if(guid==GetMyInfo().Guid)return;
			var typingTime=new Date().getTime();
			SetItemInfo(guid,"TypingTime",typingTime);
			//IsUserTyping
			var user=GetUserByGuid(guid);
			if(user&&user.IsOnline)_InvokeChatEvent("USER",["USER","UPDATED",user,user]);
			setTimeout(function(){
				var user=GetUserByGuid(guid)
				if(user&&GetItemInfo(guid,"TypingTime")==typingTime)
				{
					_InvokeChatEvent("USER",["USER","UPDATED",user,user]);
				}
			},TYPINGDISPLAYTIME+1000);
			break;
		case "USER_BROADCAST":
			var user=GetUserById(args[0]);
			if(user)
			{
				if(args[1]=="NOTIFYVIDEOALIVE"||args[1]=="NOTIFYVIDEOSTART")
				{
					var oldvn=GetItemInfo(user.Guid,"VideoName");
					SetItemInfo(user.Guid,"VideoName",args[2]);
					SetItemInfo(user.Guid,"VideoTime",new Date().getTime());
					
					if(args[1]=="NOTIFYVIDEOSTART" || oldvn!=args[2] )
					{
						_InvokeChatEvent("USER",["USER","UPDATED",user,user]);
					}
				}
				if(args[1]=="NOTIFYVIDEOCLOSE")
				{
					SetItemInfo(user.Guid,"VideoTime",null);
					_InvokeChatEvent("USER",["USER","UPDATED",user,user]);
				}
				_InvokeChatEvent("BROADCAST",["BROADCAST",args[1],args]);
				
			}
			break;
		case "HISTORY_BEGIN":
			chatvars.loadingHistory=true;
			break;
		case "HISTORY_END":
			chatvars.loadingHistory=false;
			break;
		case "USER_DOEMOTE":
		case "USER_MESSAGE":
			var msg={};
			msg.Font={};
			msg.Type="USER";
			
			msg.ClientDate=new Date();
			msg.SenderGuid=args[0];
			msg.SenderUserId=args[1];
			msg.SenderDisplayName=args[2];
			msg.Sender=GetUserById(msg.SenderUserId)||GetContactById(msg.SenderUserId)||GetIgnoreById(msg.SenderUserId)
			if(msg.Sender==null)
			{
				msg.Sender={};
				msg.Sender.UserId=msg.SenderUserId;
				msg.Sender.UserName=msg.SenderDisplayName;
				msg.Sender.DisplayName=msg.SenderDisplayName;
				msg.Sender.PublicProperties={};
			}
			if(msg.SenderGuid)
			{
				msg.Sender.Guid=msg.SenderGuid;
			}
			msg.Text=args[3];
			msg.Html=args[4];
			if(chatvars.loadingHistory)
			{
				msg.IsHistory=true;
			}
			
			if(args[5])
			{
				msg.TargetUserId=args[5];
				msg.TargetDisplayName=args[6];
				msg.Target=GetUserById(msg.TargetUserId)||GetContactById(msg.TargetUserId)||GetIgnoreById(msg.TargetUserId)
				if(msg.Target==null)
				{
					msg.Target={};
					msg.Target.UserId=msg.TargetUserId;
					msg.Target.UserName=msg.TargetDisplayName;
					msg.Target.DisplayName=msg.TargetDisplayName;
					msg.Target.PublicProperties={};
				}
				msg.Offline=msg.Whisper=args[7]=="1";
			}
			msg.ServerTick=args[8];
			msg.ServerTime= new Date( parseFloat(msg.ServerTick)/10000-parseFloat("63082281600000")+parseFloat("946684800000") );

			msg.Properties=nvc;
			
			//add user to tab-user
			if(msg.Target)
			{
				var relativeid=null;
				if(UserEquals(msg.Sender,GetMyInfo()))
				{
					relativeid=msg.Target.UserId;
				}
				else if(UserEquals(msg.Target,GetMyInfo()))
				{
					relativeid=msg.Sender.UserId;
				}
				if(relativeid)
				{
					var selid=(GetSelectedUser()||{}).UserId;
					var map={};
					var newarr=[];
					if(selid)
					{
						newarr.push(selid);
						map[selid]=true;
					}
					if(relativeid!=selid)
					{
						newarr.push(relativeid);
						map[relativeid]=1;
					}
					for(var i=0;i<chatvars.tabusers.length;i++)
					{
						var tabid=chatvars.tabusers[i];
						if(!map[tabid])
						{
							newarr.push(tabid);
							map[tabid]=true;
						}
					}
					chatvars.tabusers=newarr;
				}
			}
			
			if(msgid=="USER_DOEMOTE")
			{
				msg.Emotion=nvc["Emotion"]
				
				msg.Type="EMOTION";

				_InvokeChatEvent("MESSAGE",["MESSAGE","DOEMOTE",msg,msg.Emotion]);
			}
			else
			{
				if(chatvars.loadingHistory && IsMessenger())
				{
					if(msg.Offline)
						FireMessage(msg);
					else
						chatmessages.push(msg);
				}
				else
				{
					FireMessage(msg);
				}
			}
			break;
		case "SYS_ANNOUNCEMENT":
			var msg={};
			msg.Font={};
			msg.Type="ANNOUNCEMENT";
			msg.Text=args[0];
			msg.Html=args[1];
			msg.Arguments=args;
			FireMessage(msg);
			break;
		case "SYS_INFO_MESSAGE":
			var msg={};
			msg.Font={};
			msg.Type="SYS_INFO";
			msg.Text=TEXT(args[0]);
			msg.Html=args[1];
			msg.Arguments=args;
			FireMessage(msg);
			break;
		case "SYS_ERROR_MESSAGE":
			var msg={};
			msg.Font={};
			msg.Type="SYS_ERROR";
			msg.Text=TEXT(args[0]);
			msg.Arguments=args;
			FireMessage(msg,true);
			break;
		case "SYS_DEBUG_MESSAGE":
			_Trace(args[0]);
			break;
	}
}
function FireMessage(msg,notpush)
{
	if(!notpush)
	{
		chatmessages.push(msg);
	}
	_InvokeChatEvent("MESSAGE",["MESSAGE","NEW",msg]);
}

function PushCTSMessage(message)
{
	_Trace("CTS:"+message);
	chatvars.cts.push(message);
	DoSync();
}


function DoSendMessage()
{
	_InvokeChatEvent("SENDMESSAGE",["SENDMESSAGE","COMMAND"]);
}

function GetMessages()
{
	return chatmessages;
}

function __SendMessage(text,html,nvc)
{
	var ismessenger=IsMessenger();
	
	//TODO:
	var targetid=null;
	var targetname=null;
	var whisper=null;
	var seluser=GetSelectedUser();
	if(ismessenger && chatclient.targetContact)
	{
		targetid=chatclient.targetContact.UserId;
		targetname=chatclient.targetContact.DisplayName;
		whisper="1";
	}
	else if(seluser)
	{
		targetid=seluser.UserId;
		targetname=seluser.DisplayName;
		if(chatclient.whisper || ismessenger )
		{
			whisper="1";
		}
	}
	else if(ismessenger)
	{
		return false;
	}
	else if(chatclient.whisper)
	{
		//warning:do not use Desktop.Alert here...
		Desktop.Alert(null,TEXT("CANTNOTWHISPERTONOTARGET"));
		return false;
	}
	
	var message=JoinToMsg("USER_MESSAGE",nvc,[text,html,targetid,targetname,whisper?"1":"0"]);
	chatvars.hasmessagesending=true;
	PushCTSMessage(message);
	
	var msg={};
	msg.Text=text;
	msg.Html=html;
	_InvokeChatEvent("SENDMESSAGE",["SENDMESSAGE","SENDING",msg])

	return true;
}
function SendMessage(text,html)
{
	if(!chatvars.connected)return false;
	if(html!=null)
	{
		if(html==text)
			html=null;
		else if(text!=null)
			if(html.indexOf('<')==-1)
				html=null;
	}
	if(text!=null)
		text=text.replace(/^\s/g,'').replace(/\s$/g,'')
	if( (text==null||text.length==0) && (html==null||html.length==0) )
		return false;
	

	var nvc={};
	nvc.FontName=chatclient.fontname;
	nvc.FontSize=chatclient.fontsize;
	nvc.FontColor=chatclient.fontcolor;
	nvc.Bold=chatclient.fontbold;
	nvc.Italic=chatclient.fontitalic;
	nvc.Underline=chatclient.fontunderline;
	
	if(html==null)
	{
		html=Html_Encode(text);
	}
	var div="<div style='";
	if(chatclient.fontname)div+="font-family:"+chatclient.fontname+";";
	if(chatclient.fontsize)div+="font-size:"+chatclient.fontsize+"px;";
	if(chatclient.fontcolor)div+="color:"+chatclient.fontcolor+";";
	if(chatclient.fontbold)div+="font-weight:bold;";
	if(chatclient.fontitalic)div+="font-style:italic;";
	if(chatclient.fontunderline)div+="text-decoration:underline;";
	div+="'>";
	html=div+html+"</div>";
	
	return __SendMessage(text,html,nvc);
}

function SendEmotion(emotion)
{
	var nvc={};
	nvc.Emotion=emotion;
	return __SendMessage(emotion,null,nvc);
}

function __UpdateUserTo(user,obj,userUpdated)
{
	if(obj==null)return;
	if(user==null)
	{
		obj.OnlineStatus="OFFLINE";
		obj.IsOnline=false;
		if(!obj.CopyUser&&!obj.UserName)
		{
			obj.UserName=obj.DisplayName;
		}
		return;
	}

	obj.IsOnline=user.IsOnline;
	obj.Guid=user.Guid;//setup runtime guid
	obj.UserName=user.UserName;
	obj.DisplayName=user.DisplayName
	obj.Description=user.Description;
	obj.OnlineStatus=user.OnlineStatus;
	obj.PublicProperties=user.PublicProperties;
	obj.CopyUser=true;
	
	if(userUpdated)
	{
		if(obj.IsContact)
			_InvokeChatEvent("CONTACT",["CONTACT","UPDATED",obj,obj]);
		if(obj.IsIgnore)
			_InvokeChatEvent("IGNORE",["IGNORE","UPDATED",obj,obj]);
	}
}

function UserEquals(user1,user2)
{
	if(user1==user2)return true;
	if(user1==null||user2==null)return false;
	if(user1.UserId.toLowerCase()==user2.UserId.toLowerCase())return true;
	//do not check Guid
	return false;
}

function GetUsers()
{
	return chatvars.users;
}
function GetUserByGuid(userguid)
{
	return chatvars.userbyguid[userguid];
}
function GetUserByName(name)
{
	if(name)
		return chatvars.userbyname[name.toLowerCase()];
}
function GetUserById(userid)
{
	if(userid)
		return chatvars.userbyid[userid.toLowerCase()];
}

function GetTypingUsers()
{
	var typings=[];
	for(var i=0;i<chatvars.users.length;i++)
	{
		var user=chatvars.users[i];
		var time=GetItemInfo(user.Guid,"TypingTime");
		if(time && time > (new Date().getTime()-TYPINGDISPLAYTIME))
		{
			typings.push(user.DisplayName);
		}
	}
	return typings;
}
function IsUserTyping(user)
{
	if(!user)return false;
	var time=GetItemInfo(user.Guid,"TypingTime");
	if(time && time > (new Date().getTime()-TYPINGDISPLAYTIME))
	{
		return true;
	}
	return false;
}


function GetSelectedUser()
{
	return GetUserById( (chatclient.selecteduser||{}).UserId );
}
function SetSelectedUser(user)
{
	var users=GetUsers();
	
	var olduser;
	if(chatclient.selecteduser)
	{
		for(var i=0;i<users.length;i++)
		{
			if(UserEquals(chatclient.selecteduser,users[i]))
			{
				olduser=users[i];
				break;
			}
		}
	}
	
	if(user==null)
	{
		chatclient.selecteduser=null;
		_InvokeChatEvent("SELECTEDUSER",["SELECTEDUSER","UPDATED"]);
		if(olduser)_InvokeChatEvent("USER",["USER","UPDATED",olduser,olduser]);
		return;
	}
	
	for(var i=0;i<users.length;i++)
	{
		if(UserEquals(user,users[i]))
		{
			chatclient.selecteduser=users[i];
			if(olduser)_InvokeChatEvent("USER",["USER","UPDATED",olduser,olduser]);
			_InvokeChatEvent("USER",["USER","UPDATED",chatclient.selecteduser,chatclient.selecteduser]);
			_InvokeChatEvent("SELECTEDUSER",["SELECTEDUSER","UPDATED"]);
		}
	}
}

function DoTabNextUser()
{

	var sel=GetSelectedUser();
	var arr=chatvars.tabusers;
	
	var index=-1;
	if(sel)
	{
		for(var i=0;i<arr.length;i++)
		{
			if(arr[i]==sel.UserId)
			{
				index=i;
				break;
			}
		}
		if(index+1==arr.length)
			index=-1;
	}
	index++;
	SetSelectedUser(GetUserById(arr[index]));
}

function GetContacts()
{
	return chatvars.contacts;
}
function GetContactByName(name)
{
	if(name)
		return chatvars.contactbyname[name.toLowerCase()];
}
function GetContactById(userid)
{
	if(userid)
		return chatvars.contactbyid[userid.toLowerCase()];
}

function GetIgnores()
{
	return chatvars.ignores;
}
function GetIgnoreByName(name)
{
	if(name)
		return chatvars.ignorebyname[name.toLowerCase()];
}
function GetIgnoreById(userid)
{
	if(userid)
		return chatvars.ignorebyid[userid.toLowerCase()];
}

function GetSelectedContact()
{
	return chatclient.selectedcontact;
}
function SetSelectedContact(contact)
{
	if(contact==null)
	{
		chatclient.selectedcontact=null;
		_InvokeChatEvent("SELECTEDCONTACT",["SELECTEDCONTACT","UPDATED"]);
		return;
	}

	var contacts=GetContacts();
	for(var i=0;i<contacts.length;i++)
	{
		if(UserEquals(contact,contacts[i]))
		{
			chatclient.selectedcontact=contacts[i];
			_InvokeChatEvent("SELECTEDCONTACT",["SELECTEDCONTACT","UPDATED"]);
			return;
		}
	}
	
	chatclient.selectedcontact=contact;
	_InvokeChatEvent("SELECTEDCONTACT",["SELECTEDCONTACT","UPDATED"]);
}

function GetSelectedIgnore()
{
	return chatclient.selectedignore;
}
function SetSelectedIgnore(ignore)
{
	if(ignore==null)
	{
		chatclient.selectedignore=null;
		_InvokeChatEvent("SELECTEDIGNORE",["SELECTEDIGNORE","UPDATED"]);
		return;
	}

	var ignores=GetIgnores();
	for(var i=0;i<ignores.length;i++)
	{
		if(UserEquals(ignore,ignores[i]))
		{
			chatclient.selectedignore=ignores[i];
			_InvokeChatEvent("SELECTEDIGNORE",["SELECTEDIGNORE","UPDATED"]);
			return;
		}
	}
}

function AddContact(user)
{
	if(!chatvars.connected)return;
	
	var message=JoinToMsg("USER_COMMAND",null,["ADDCONTACT",user.UserId]);
	PushCTSMessage(message);
}

function RemoveContact(user)
{
	if(!chatvars.connected)return;
	
	var message=JoinToMsg("USER_COMMAND",null,["REMOVECONTACT",user.UserId]);
	PushCTSMessage(message);
}

function AddIgnore(user)
{
	if(!chatvars.connected)return;
	
	var message=JoinToMsg("USER_COMMAND",null,["ADDIGNORE",user.UserId]);
	PushCTSMessage(message);
}

function RemoveIgnore(user)
{
	if(!chatvars.connected)return;
	
	var message=JoinToMsg("USER_COMMAND",null,["REMOVEIGNORE",user.UserId]);
	PushCTSMessage(message);
}

function IsContact(user)
{
	return !!GetContactById(user.UserId);
}
function IsBlock(user)
{
	return !!GetIgnoreById(user.UserId);
}
function SetBlock(user,block)
{
	if(block)
		AddIgnore(user);
	else
		RemoveIgnore(user);
}


function InvitePrivateChat(user)
{
	InvitePrivateChatBatch([user]);
}
function InvitePrivateChatBatch(users)
{
	if(users.length==0)return;
	var args=[];
	args.push("PRIVATEINVITE");
	var added={};
	for(var i=0;i<users.length;i++)
	{
		var userid=users[i].UserId;
		if(added[userid])continue;
		added[userid]=userid;
		args.push(userid);
	}
	var message=JoinToMsg("USER_COMMAND",null,args);
	PushCTSMessage(message);
}
function AcceptPrivateChat(item)
{
	var message=JoinToMsg("USER_COMMAND",null,["ACCEPTPRIVATE",item.Args[2]]);
	PushCTSMessage(message);
}
function RejectPrivateChat(item)
{
	var message=JoinToMsg("USER_COMMAND",null,["REJECTPRIVATE",item.Args[2]]);
	PushCTSMessage(message);
}

function InviteIntoPrivateChat(username)
{
	var message=JoinToMsg("USER_COMMAND",null,["INVITEINTOPRIVATE",username]);
	PushCTSMessage(message);
}



/****************************************************************\
	Place
\****************************************************************/
function GetPlace()
{
	return chatvars.place;
}
function IsMessenger()
{
	if(chatvars.place==null)
		return (chatclient.placename||"").toLowerCase()=="messenger";
	return chatvars.place.Location=="Messenger";
}
function GetLocation()
{
	if(chatvars.place==null)return initializelocation;
	return chatvars.place.Location;
}
var initializelocation="Unknown";


/****************************************************************\
	MyInfo
\****************************************************************/
function GetMyInfo()
{
	if(chatvars.myinfo)
		return chatvars.myinfo;

	var unknown={};
	unknown.IsUnknown=true;
	unknown.PublicProperties={}
	unknown.PrivateProperties={}
	unknown.DisplayName="";
	unknown.UserName="";
	unknown.Guid="";
	unknown.UserId="";

	unknown.Description=""
	unknown.IsAnonymous=true;
	unknown.IsAdmin=false
	unknown.Moderated=true
	unknown.OnlineStatus="ONLINE"
	unknown.RemoveReason=""
	unknown.IPAddress="";
	unknown.AppearOffline=false;
	unknown.Level="Normal";
			
	return unknown;
}

function SetOnlineStatus(onlinestatus)
{
	if(!chatvars.connected)return;
	
	var message=JoinToMsg("USER_COMMAND",null,["ONLINESTATUS",onlinestatus]);
	PushCTSMessage(message);
}
function ChangeDisplayName(newname)
{
	var message=JoinToMsg("USER_COMMAND",null,["DISPLAYNAME",newname]);
	PushCTSMessage(message);
}
function SetDescription(desc)
{
	var message=JoinToMsg("USER_COMMAND",null,["DESCRIPTION",desc]);
	PushCTSMessage(message);
}
function SetPrivateProperty(name,strval)
{
	var message=JoinToMsg("USER_COMMAND",null,["PRIVATEPROPERTY",name,strval]);
	PushCTSMessage(message);
}
function SetPublicProperty(name,strval)
{
	var message=JoinToMsg("USER_COMMAND",null,["PUBLICPROPERTY",name,strval]);
	PushCTSMessage(message);
}
function SetAvatar(avatar)
{
	SetPublicProperty("Avatar",avatar);
}
function SetInstantAvatar(avatar)
{
	SetPublicProperty("InstantAvatar",avatar);
}

function SetIsTyping()
{
	chatvars.typingTime=new Date().getTime();
}
function InstantSetIsTyping(contact)
{
	chatvars.typingTime=new Date().getTime();
	chatvars.typingContact=contact;
}

function SaveSkin(skin)
{
	SetPrivateProperty("Skin",skin);
	SetCookie("CuteChatSkin",skin);
}


/****************************************************************\
Admin
\****************************************************************/

function AdminSetPassword(newpwd)
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["SETPASSWORD",newpwd||""]);
	PushCTSMessage(message);
}
function AdminSetMaxOnline(count)
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["SETMAXONLINECOUNT",count]);
	PushCTSMessage(message);
}
function AdminSetEnableAnonymous()
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["SETALLOWANONYMOUS","1"]);
	PushCTSMessage(message);
}
function AdminSetDisableAnonymous()
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["SETALLOWANONYMOUS","0"]);
	PushCTSMessage(message);
}
function AdminSetLockChannel(locked)
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["SETLOCKCHANNEL",locked?"1":"0"]);
	PushCTSMessage(message);
}
function AdminAcceptUser(user)
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["ACCEPTUSER",user.UserId]);
	PushCTSMessage(message);
}
// just reject once.
function AdminRejectUser(user)
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["REJECTUSER",user.UserId]);
	PushCTSMessage(message);
}
//the kick user will make the user into kickeduserlist
function AdminKickUser(user)
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["KICKUSER",user.UserId]);
	PushCTSMessage(message);
}
function AdminUnkickUsers()
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["UNKICKUSERS"]);
	PushCTSMessage(message);
}
function AdminSetUserLevel(user,level)
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["SETUSERLEVEL",user.UserId,level]);
	PushCTSMessage(message);
}

function AdminGetUserIP(user)
{
	return user.IPAddress;
}

function AdminDenyUserIP(user)
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["KICKIP",user.UserId]);
	PushCTSMessage(message);
}
function AdminUnDenyIPs()
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["UNKICKIPS"]);
	PushCTSMessage(message);
}

function AdminSetModerateMode(mode)
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["SETMODERATORMODE",mode?"1":"0"]);
	PushCTSMessage(message);
}
function AcceptModerateItem(guid)
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["ACCEPTMODERATEITEM",guid]);
	PushCTSMessage(message);
}
function RejectModerateItem(guid)
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["REJECTMODERATEITEM",guid]);
	PushCTSMessage(message);
}


function AdminAddModerator(name)
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["ADDMODERATOR",name]);
	PushCTSMessage(message);
}
function AdminRemoveModerator(name)
{
	var message=JoinToMsg("MODERATOR_COMMAND",null,["REMOVEMODERATOR",name]);
	PushCTSMessage(message);
}
















