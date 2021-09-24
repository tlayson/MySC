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

var Desktop={};
Desktop.Alert=function(handler,message,title)
{
	alert(message);
	if(handler)setTimeout(handler,1);
}
Desktop.Confirm=function(handler,message,title)
{
	var res=confirm(message);
	if(handler)
	{
		function callhandler()
		{
			handler(res);
		}
		
		setTimeout(callhandler,1);
	}
}
Desktop.Prompt=function(handler,message,title)
{
	var res=prompt(message);
	if(handler)
	{
		function callhandler()
		{
			handler(res);
		}
		
		setTimeout(callhandler,1);
	}
}




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











	

function SUI_SendMail()
{
	var IE7 = ((document.all)&&(navigator.appVersion.indexOf("MSIE 7.")!=-1)) ? true : false;
	var email;
	
	if(IE7)
	{
		IEprompt(promptCallback,TEXT("UI_INPUTEMAILADDRESS"), "");		
		function promptCallback(dir)
		{
			if(dir)
			{					
				sendemail (dir);
			}
			else
			{
				return false;
			}
		}		
	}		
	else
	{
		email=prompt(TEXT("UI_INPUTEMAILADDRESS"),"");
		sendemail (email);
	}		
	function sendemail (mail)
	{
		if(!mail)return;
		if(mail.indexOf("@")==-1)return;		
		PushCTSMessage(JoinToMsg("LSCUSTOMER_COMMAND",null,["SENDTOEMAIL",mail]));		
		alert(TEXT("UI_SENDEMAILOK"));
	}		
}
function SUI_RateTheAgent(rating)
{
	PushCTSMessage(JoinToMsg("LSCUSTOMER_COMMAND",null,["SETAGENTRATING",rating]));
}

var cobrowsetd=null;
var cobrowseid=0;
var coframe;
var cotopdiv;
var coaddrinp;
var coaddrbtn;
var coaddress;



function CoBrowseStart(url)
{
	if(cobrowsetd!=null)return;
	
	var topcell=$("maintable").rows.item(0).cells.item(0);
	
	cobrowsetd=$("maintable").rows.item(0).insertCell(-1);
	cobrowsetd.rowSpan=$("maintable").rows.length;
	
	cobrowsetd.innerHTML="<iframe src='"+Html_Encode(url)+"' width='100%' height='100%'></iframe>";
	topcell.style.width='auto'
	cobrowsetd.style.width='200px'
	cobrowsetd.style.backgroundColor='white';

	coframe=cobrowsetd.firstChild;
	cotopdiv=document.createElement("DIV");
	coaddrinp=document.createElement("INPUT");
	coaddrbtn=document.createElement("BUTTON");
	coaddrbtn.innerHTML="GO";
	coaddrinp.style.width="400px"
	cotopdiv.appendChild(coaddrinp);
	cotopdiv.appendChild(coaddrbtn);
	coaddrbtn.onclick=GotoAddress
	cobrowsetd.insertBefore(cotopdiv,coframe);
	
	coaddrinp.readOnly=true;
	coaddrbtn.style.display="none";

	cobrowseid=setInterval(UpdateCoBrowse,1000);
	
	try
	{
		window.resizeTo(1000,600);
	}
	catch(x)
	{
	}
	
	AdjustUIHeight();
}
function CoBrowseStop()
{
	if(cobrowsetd==null)return;
	
	var topcell=$("maintable").rows.item(0).cells.item(0);
	
	cobrowsetd.parentNode.removeChild(cobrowsetd);
	cobrowsetd=null;
	topcell.style.width='100%'
	
	clearTimeout(cobrowseid);
	
	try
	{
		window.resizeTo(500,400);
	}
	catch(x)
	{
	}
}

function ToogleCoBrowse()
{
	if(cobrowsetd==null)
	{
		//CoBrowseStart(cohome);
		
		PushCTSMessage(JoinToMsg("LSCUSTOMER_COMMAND",null,["COBROWSE_START"]));
		
	}
	else
	{
		
		PushCTSMessage(JoinToMsg("LSCUSTOMER_COMMAND",null,["COBROWSE_STOP"]));
		
	}
}
function GetBrowserUrl()
{
	try
	{
		return coframe.contentWindow.location.href;
	}
	catch(x)
	{
		//coaddrinp.style.backgroundColor="yellow";
		return;
	}
}
function UpdateCoBrowse()
{
	var addr=GetBrowserUrl();
	
	if(addr==null)return;
	
	if(coaddress==addr)return;

	//disable client side ..
	//PushCTSMessage(JoinToMsg("LSCUSTOMER_COMMAND",null,["COBROWSE_UPDATE",addr]));
}
function GotoAddress()
{
	coframe.contentWindow.location.href=coaddrinp.value;
}
function SUI_CheckCoBrowseEvent(name,type,info1)
{
	if(type=="COBROWSE_START")
	{
		CoBrowseStart(cohome);
	}
	if(type=="COBROWSE_STOP")
	{
		CoBrowseStop();
	}
	if(type=="COBROWSE_UPDATE")
	{
		CoBrowseStart(cohome);
		var newaddr=info1[1];
		if(newaddr==coaddress)return;
		coaddress=newaddr;
		coaddrinp.value=newaddr;
		coframe.contentWindow.location.href=newaddr;
	}
}
AttachChatEvent("RAWSTCMSG",SUI_CheckCoBrowseEvent);

function SUI_SendMessage()
{
	var text=$("inputBox").value;
	var html=ChatUI_TranslateText(text);

	if(text.replace(/\s/g,'').length || (html&&html.length>10) )
	{
		if(ChatUI_SendMessageWithFloodControl(text,html))
		{
			$("inputBox").value='';
		}
	}
	
	$("inputBox").focus();
}
function SUI_ClearInput()
{
	$("inputBox").value="";
	$("inputBox").focus();
}

var lastmessagetime=0;

AttachChatEvent("CONNECTION",SUI_HandleChatEvent);
AttachChatEvent("USER",SUI_HandleChatEvent);
AttachChatEvent("SELECTEDUSER",SUI_HandleChatEvent);
AttachChatEvent("MESSAGE",SUI_HandleChatEvent);
AttachChatEvent("SENDMESSAGE",SUI_HandleChatEvent);
AttachChatEvent("COMMAND",SUI_HandleChatEvent);
AttachChatEvent("UICOMMAND",SUI_HandleChatEvent);

var currentAgent=null;
setInterval(CheckTypingStatus,100);

function IsAgentTyping()
{
	if(currentAgent==null)return false;
	var span=new Date().getTime()-(GetItemInfo(currentAgent.Guid,"TypingTime")||0);
	return span<6000;
}
function CheckTypingStatus()
{
	var TypingStatus=$("TypingStatus");
	if(IsAgentTyping())
	{
		TypingStatus.innerHTML=currentAgent.DisplayName+" "+TEXT("UI_USER_TYPING");;
	}
	else
	{
		TypingStatus.innerHTML="";
	}
}

function SUI_HandleChatEvent(name,type,info1,info2)
{
	if(name=="MESSAGE")
	{
		if(type=="NEW")
		{
			var msg=info1;
			var msgdiv=document.createElement("DIV");
			ChatUI_AppendMessage(msgdiv,msg);
			$("messageList").appendChild(msgdiv);
			$("messageList").scrollTop=$("messageList").scrollHeight;
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
	//	var OperatorName=$("OperatorName");
		var arr=GetUsers();							
		for(var i=0;i<arr.length;i++)
		{	
			var user = arr[i];
			if(user.IsAgent)
			{
				currentAgent=user;
				var photo=document.createElement("IMG");
				if(user.PublicProperties["PhotoUrl"])
					photo.src=user.PublicProperties["PhotoUrl"];
				else
					photo.src="images/live-support.jpg";						
				photo.vSpace=4;
				photo.hSpace=4;
				userList.innerHTML="";
				userList.appendChild(photo);
	//			OperatorName.innerHTML=Html_Encode(user.DisplayName);
			}
		}			
	}
	if(name=="UICOMMAND")
	{
		if(type=="EMOTION")
		{
			$("inputBox").value+="[Emotion="+info1+"]";
		}
		if(type=="FOCUSINPUT")
		{
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
		if(Html_IsWinIE)
		{
			event.returnValue=false;
		}
		else
		{
			event.preventDefault();
		}
		SUI_SendMessage();
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
	
	try{
		if(GetMyInfo().OnlineStatus!="ONLINE")
			SetOnlineStatus("ONLINE");
	}
	catch(x)
	{}
}
//called by ChatUI.js
function ChatUI_AlertAutoAway()
{
	// just do nothing . do not show the alert message.
}

window.onbeforeunload=function()
{
	if(IsConnected())
	{
		return "Are you sure you want to quit this conversation?";
	}
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

window.onresize=function()
{
	AdjustUIHeight();
}
function AdjustUIHeight()
{
	if($("maintable").style.display=="none")
		return;
		
	var cw=window.document.body.clientWidth;
	var ch=window.document.body.clientHeight;

	if(window.document.compatMode=='CSS1Compat')
	{
		cw=window.document.documentElement.clientWidth;
		ch=window.document.documentElement.clientHeight;
	}
	
	$("messageList").style.height="1px";
	$("maintable").style.height=(ch+4)+"px";
	$("messageList").style.height=($("messagePanel").offsetHeight-12)+"px";
	
	if(cobrowsetd!=null)
	{
		coframe.style.height=(ch-36)+"px";
		coframe.style.width=coaddrinp.style.width=Math.max(200,cw-500)+"px";
	}
}
setTimeout(AdjustUIHeight,1000);

chatclient.guestname=GetCookie("CCGuestName")
window.onload=function()
{
	if(promptwait)
	{
		ShowPromptWait();
	}
	else
	{
		SetPromptWaitContainerDisplay("none");
		Connect(placename);
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

var lastactivetime=new Date().getTime();
var psintervalid=setInterval(PlaySoundIfNotActive,10000);
function PlaySoundIfNotActive()
{
	var time=new Date().getTime()-lastactivetime;
	if(time>9000&&IsConnected())
	{
		if(lastactivetime<lastmessagetime||lastmessagetime==0)
		{
			ChatUI_PlaySound("nudge");
			window.focus();
		}
	}
}
window.onfocus=document.body.onkeydown=document.body.onmousemove=function(){
	lastactivetime=new Date().getTime();
};

function SetPromptWaitContainerDisplay(val)
{
	$("promptwaitcontainer").style.display=val;
	if(val=="")
	{
		$("maintable").style.display="none";
	}
	else
	{
		$("maintable").style.display="";
		AdjustUIHeight();
	}
}

function ShowPromptWait()
{
	SetPromptWaitContainerDisplay("");
	//$("promptwaitcontainer").style.backgroundColor="white";
}

function Chat_CreateRequest()
{
	if(typeof(XMLHttpRequest)!="undefined")
		return new XMLHttpRequest();
	return new ActiveXObject("Microsoft.XMLHTTP");
}

var _chat_waittimerid=0;
var _chat_waiting=false;
var _chat_waitprops={};
var _chat_customerid=null;

function Chat_IntervalWait(props)
{
	//changed by terry 2006-06-10
	var _chat_waittimeout=1000;
	
	clearTimeout(_chat_waittimerid);
	_chat_waittimerid=setTimeout(Chat_IntervalWait,_chat_waittimeout);
	
	if(props)
	{
		for(var p in props)
		{
			if(props.hasOwnProperty(p))
				_chat_waitprops[p]=props[p];
		}
	}
	
	if(_chat_waiting)return;
	_chat_waiting=true;

	var xh=Chat_CreateRequest();
	function HandleReadyState()
	{
		if(xh.readyState<4)return;

		xh.onreadystatechange=new Function();
		var xh2=xh;
		xh=null;
		
		_chat_waiting=false;
		
		Chat_IntervalWaitHandleResponse(xh2);
	}
	
	var encode=escape
	
	if(typeof(encodeURIComponent)!="undefined")
		encode=encodeURIComponent;
	
	xh.onreadystatechange=HandleReadyState;
	var url="SupportCustomerHandler.ashx?_temp="+(new Date().getTime())+"&Type=WAIT"
		+"&Url="+encode(_chat_waitprops["Url"]||"")
		+"&Referrer="+encode(_chat_waitprops["Referrer"]||"")
		+"&Name="+encode(_chat_waitprops["Name"]||"")
		+"&Email="+encode(_chat_waitprops["Email"]||"")
		+"&CustomData="+encode(_chat_waitprops["CustomData"]||"")
		+"&Department="+encode(_chat_waitprops["Department"]||"")
		+"&Question="+encode(_chat_waitprops["Question"]||"")
		
	if(_chat_customerid!=null)
	{
		url+="&CCCustomerId="+escape(_chat_customerid);
	}
	xh.open("GET",url,true,null,null);
	xh.send("");
}

function Chat_IntervalWaitHandleResponse(xh)
{
	if(xh.status!=200)return;//ignore error
	var text=xh.responseText;
	if(text==null||text=="")return;//unknown output
	
	//$("input_name").value=new Date().getSeconds()+" - "+text;
		
	var arr=text.split(":");
	switch(arr[0])
	{
		case "READY":
			
			$("span_waiting").style.display='none';
			$("span_nextline").style.display='';
			
			var agentname=text.substring(6).split("&")[1];
			clearTimeout(_chat_waittimerid);
			SetPromptWaitContainerDisplay("none");
			var msgdiv=document.createElement("DIV");
			msgdiv.style.color="darkgreen";
			msgdiv.innerHTML="<img src='"+CuteChatUrlBase+"Images/arrow.gif'>"+TEXT("UI_Support_Ready",agentname);
			$("messageList").appendChild(msgdiv);
			if(_chat_customerid!=null)
			{
				if(!window._backup_chatservice_url)window._backup_chatservice_url=chatservice_url;
				chatservice_url=window._backup_chatservice_url;
				if(chatservice_url.indexOf("?")!=-1)
					chatservice_url+="&CCCustomerId="+escape(_chat_customerid);
				else
					chatservice_url+="?CCCustomerId="+escape(_chat_customerid);
				placename="SupportSession:Guest:"+_chat_customerid;
			}
			Connect(placename);
			break;
		case "NOAGENT":
			SetPromptWaitContainerDisplay("");
			clearTimeout(_chat_waittimerid);
			WaitOperator_HandleError("NOAGENT");
			break;
		case "WAITING":
			WaitOperator_UpdatePosition(arr[1]);
			if(arr[2]=="Guest"&&arr[3])
			{
				if(document.cookie&&document.cookie.indexOf(arr[3])!=-1)
					break;
				_chat_customerid=arr[3];
			}
			break;
		case "ERROR":
			SetPromptWaitContainerDisplay("");
			clearTimeout(_chat_waittimerid);
			WaitOperator_HandleError("ERROR",arr[1]);
		default:
			break;
	}
}



var redirectIfNoAcceptForSeconds=600;

function submit_form()
{	
	if($("input_name").value=="")
	{
		alert("Please input your name");
		$("input_name").focus();
		return;
	}
	
	if(supportRequireMail)
	{
		if($("input_email").value=="")
		{
			alert("Please input your email");
			$("input_email").focus();
			return;
		}
		var exp=/[^@]+@[^@]+\.[^@]+/i;
		if(!exp.test($("input_email").value))
		{
			alert("Please input a valid email");
			$("input_email").focus();
			return;
		}
	}

	var props={};
	props["Name"]=$("input_name").value;
	props["Email"]=$("input_email").value||"";
	props["Department"]=$("input_department").value||"";
	if($("input_customdata"))
	{
		props["CustomData"]=$("input_customdata").value||"";
	}
	props["Question"]=$("input_question").value||"";
	props["Url"]=$("input_url").value||"";
	props["Referrer"]=$("input_referrer").value||"";
	
	//more public properties ?
	Chat_IntervalWait(props);

	$("input_name").disabled=true;
	$("input_email").disabled=true;
	$("button_submit").disabled=true;
	$("span_waiting").style.display='';
	$("span_nextline").style.display='none';
	
	//setTimeout(RedirectIfNoResponse,redirectIfNoAcceptForSeconds*1000);
	
	SetPromptWaitContainerDisplay("none");
	var msgdiv=document.createElement("DIV");
	msgdiv.style.color="darkgreen";
	var question=$("input_question").value||"";
	if(question.length>50)question=question.substr(0,50)+"...";
	
	html="<img src='"+CuteChatUrlBase+"Images/arrow.gif'>"+TEXT("UI_Support_Wait", question);
	
	msgdiv.innerHTML=html;
	
	$("messageList").appendChild(msgdiv);
	SetCookie("CCGuestName",$("input_name").value);
	chatclient.guestname=$("input_name").value;
	
}
function RedirectIfNoResponse()
{
	location.href="SupportFeedback.aspx"+querystring
}

function WaitOperator_HandleError(type,errmsg)
{
	$("input_name").disabled=false;
	$("input_email").disabled=false;
	$("button_submit").disabled=false;
	$("span_waiting").style.display='none';
	$("span_nextline").style.display='';
	
	if(type=="NOAGENT")
	{
		alert(TEXT("UI_Support_NOAGENT"));
	}
	else
	{
		alert(errmsg);
	}
	//location.href="SupportFeedback.aspx";
}

//override the function , as event handler to update the position information to UI
function WaitOperator_UpdatePosition(pos)
{
	$("span_pos").innerHTML=pos;
}

///////////////////////////////////////////////////////////
// Usage IEprompt("dialog descriptive text", "default starting value");
// 
// IEprompt will call promptCallback(val)
// Where val is the user's input or null if the dialog was canceled.
///////////////////////////////////////////////////////////
var _dialogPromptID=null;
function IEprompt(promptCallback,innertxt,def) {

   that=this;

   this.wrapupPrompt = function (cancled) {
		// wrapupPrompt is called when the user enters or cancels the box.
		// It's called only by the IE7 dialog box, not the non IE prompt box
        // Make sure we're in IE7 mode and get the text box value
        val=document.getElementById('iepromptfield').value;
        // clear out the dialog box
        _dialogPromptID.style.display='none';
        // clear out the text field
        document.getElementById('iepromptfield').value = '';
        // if the cancel button was pushed, force value to null.
        if (cancled) { val = '' }
        // call the user's function
        promptCallback(val);
		return false;
   }

   //if def wasn't actually passed, initialize it to null
   if (def==undefined) { def=''; }

      if (_dialogPromptID==null) {
         // Check to see if we've created the dialog divisions.
         // This block sets up the divisons
         // Get the body tag in the dom
         var tbody = document.getElementsByTagName("body")[0];
         // create a new division
         tnode = document.createElement('div');
         // name it
         tnode.id='IEPromptBox';
         // attach the new division to the body tag
         tbody.appendChild(tnode);
         // and save the element reference in a global variable
         _dialogPromptID=document.getElementById('IEPromptBox');
         // Create a new division (blackout)
         tnode = document.createElement('div');
         // name it.
         tnode.id='promptBlackout';
         // attach it to body.
         tbody.appendChild(tnode);
         // And get the element reference
         // assign the styles to the dialog box
         _dialogPromptID.style.border='2px solid #6885B9';
         _dialogPromptID.style.backgroundColor='#edf1fa';
         _dialogPromptID.style.position='absolute';
         _dialogPromptID.style.width='330px';
         _dialogPromptID.style.zIndex='100';
      }
      // This is the HTML which makes up the dialog box, it will be inserted into
      // innerHTML later. We insert into a temporary variable because
      // it's very, very slow doing multiple innerHTML injections, it's much
      // more efficient to use a variable and then do one LARGE injection.
      var tmp = '<div style="width: 100%; background-color: #6885B9; color: white; font-family: verdana; font-size: 10pt; font-weight: bold; height: 20px">[[InputRequired]]</div>';
      tmp += '<div style="padding: 10px">'+innertxt + '<BR><BR>';
      tmp += '<form action="" onsubmit="return wrapupPrompt()">';
      tmp += '<input id="iepromptfield" name="iepromptdata" type=text size=35 value="'+def+'">';
      tmp += '<br><br><center>';
      tmp += '<input type="submit" value="&nbsp;&nbsp;&nbsp;[[OK]]&nbsp;&nbsp;&nbsp;">';
      tmp += '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
      tmp += '<input type="button" onclick="wrapupPrompt(true)" value="&nbsp;[[Cancel]]&nbsp;">';
      tmp += '</form></div>';
      // Insert the tmp HTML string into the dialog box.
      // Then position the dialog box on the screen and make it visible.
      _dialogPromptID.innerHTML=tmp;
      _dialogPromptID.style.top='100px';
      _dialogPromptID.style.left=parseInt((document.body.offsetWidth-315)/2)+'px';
      _dialogPromptID.style.display='block';
      // Give the dialog box's input field the focus.
     var iepromptfield=document.getElementById('iepromptfield');
     
     try
     {
		var range = iepromptfield.createTextRange();
		range.collapse(false);
		range.select(); 
     }
     catch(x)
     {
		iepromptfield.focus(); 
     }       
}