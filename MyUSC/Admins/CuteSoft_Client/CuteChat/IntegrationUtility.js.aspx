<%@ Page language="c#" AutoEventWireup="false" %>
//<html runat=server Visible=false><head runat=server></head></html>
//<script>

//this file provide some function for the pages

var __cc_urlbase='<%=ResolveUrl("./")%>';
var chatservice_url=__cc_urlbase+"ChatAjax.ashx";
var partialmessengertimeout=10000;



/****************************************************************\
	Cookie Functions
\****************************************************************/

function SetCookie(name,value,seconds)
{
	var cookie=name+"="+escape(value)+"; path=/;";
	if(seconds)
	{
		var d=new Date();
		d.setSeconds(d.getSeconds()+seconds);
		cookie+=" expires="+d.toUTCString()+";";
	}
	document.cookie=cookie;
}
function GetCookie(name)
{
	var cookies=document.cookie.split(';');
	for(var i=0;i<cookies.length;i++)
	{
		var parts=cookies[i].split('=');
		if(name==parts[0].replace(/\s/g,''))
			return unescape(parts[1])
	}
	//return undefined..
}
function GetCookieDictionary()
{
	var dict={};
	var cookies=document.cookie.split(';');
	for(var i=0;i<cookies.length;i++)
	{
		var parts=cookies[i].split('=');
		dict[parts[0].replace(/\s/g,'')]=unescape(parts[1]);
	}
	return dict;
}
function GetCookieArray()
{
	var arr=[];
	var cookies=document.cookie.split(';');
	for(var i=0;i<cookies.length;i++)
	{
		var parts=cookies[i].split('=');
		var cookie={name:parts[0].replace(/\s/g,''),value:unescape(parts[1])};
		arr[arr.length]=cookie;
	}
	return arr;
}

var CHATUI_COOKIETIMEOUT=3;
/****************************************************************\
	Messenger
\****************************************************************/

function Chat_AddMessengerCss()
{
	var found=false;
	var arr=document.getElementsByTagName("LINK")
	for(var i=0;i<arr.length;i++)
	{
		var link=arr[i];
		if(link.href&&link.href.toLowerCase().indexOf("newmessenger.css")>-1)
		{
			found=true;
			break;
		}
	}
	if(!found)
	{
		var tag=document.getElementsByTagName("HEAD")[0]||docujent.body;
		var link=document.createElement("LINK");
		link.setAttribute("rel","stylesheet");
		link.setAttribute("href",__cc_urlbase+"NewMessenger.css");
		tag.appendChild(link);
	}
}
function Chat_LoadMessengerScript(varname,filepath,handler)
{
	if(window[varname])return;
	if(window.__loadccmxmlhttp)return;

	var xh=Chat_CreateXmlHttp();
	window.__loadccmxmlhttp=xh;
	
	var url=__cc_urlbase+filepath;
	if(window.opera)url+="?"+new Date().getTime();
	
	xh.onreadystatechange=function()
	{
		if(xh.readyState<4)return;
		window.__loadccmxmlhttp=null;
		
		var link=document.createElement("SCRIPT");
		link.type="text/javascript";
		link.text=xh.responseText;
		document.body.appendChild(link);

		setTimeout(handler,1);
	}
	
	xh.open("GET",url,true);
	xh.send("");
}

function Chat_OpenMessengerDialog()
{
	window.open(__cc_urlbase+"Messenger.aspx","mymessenger",'status=1,width=300,height=500,resizable=1');
}

function Chat_OpenMessenger()
{
	Chat_AddMessengerCss();
	
	function ContinueOpenMessenger()
	{
		Chat_LoadMessengerScript("CuteWebUI","Script/CuteWebUI.js",ContinueOpenMessenger);
		Chat_LoadMessengerScript("__cc_version","LoadSettings.aspx",ContinueOpenMessenger);
		Chat_LoadMessengerScript("ChatService_CreateXmlHttp","LoadScripts.aspx",ContinueOpenMessenger);
		Chat_LoadMessengerScript("CuteChatMessenger","Script/NewMessenger.js",ContinueOpenMessenger);
		if(typeof(CuteWebUI)!="undefined")
		{
			CuteWebUI.HTML.ShowDialogMask();
		}
		if(typeof(CuteChatMessenger)!="undefined")
		{
			CuteChatMessenger.Start();
			CuteWebUI.HTML.HideDialogMask();
		}
	}
	ContinueOpenMessenger();
}

function Chat_CreateXmlHttp()
{
	var xh;
	if(typeof(XMLHttpRequest)=="undefined")
		xh=new ActiveXObject("Microsoft.XMLHTTP");
	else
		xh=new XMLHttpRequest();
	return xh;
}


var partialmessengerintervalid;

function Chat_StartPartialMessenger()
{
	if(partialmessengerintervalid)return;
	partialmessengerintervalid=setTimeout(Chat_PartialMessengerTimeout,partialmessengertimeout);
}
function Chat_PartialMessengerTimeout()
{
	var xmlhttp=Chat_CreateXmlHttp();
	xmlhttp.open("POST",chatservice_url,true);
	xmlhttp.onreadystatechange=function()
	{
		if(xmlhttp.readyState!=4)return;
		xmlhttp.onreadystatechange=new Function();
		var xh=xmlhttp;
		xmlhttp=null;
		
		
		if(xh.status!=200)
		{
			//document.body.innerHTML=xh.responseText;
			return;
		}
		
		if(xh.responseText=="NEEDLOGIN")return;
		
		//document.title=xh.responseText;
		
		var pos1=xh.responseText.indexOf(":");
		var status=xh.responseText.substr(0,pos1);
		var detail=xh.responseText.substring(pos1+1);
		
		switch(status)
		{
			case "ONLINE"://already open the messenger
				break;
			case "ACTIVE":
				var info=eval("info="+detail);
				Chat_ShowPartialResponse(info);
				break;
		}
		
		partialmessengerintervalid=setTimeout(Chat_PartialMessengerTimeout,partialmessengertimeout);
	}
	xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=utf-8");
	var arr=[];
	arr[arr.length]="METHOD=PARTIALMESSENGER";
	var data=arr.join("&")
	xmlhttp.send(data);
}

var __cc_lastmsgcount=0;

function Chat_ShowPartialResponse(info)
{
	var count=info.OfflineMsgCount+info.Messages.length;
	if(count==__cc_lastmsgcount)
		return;
	__cc_lastmsgcount=count;
		
	Chat_AddMessengerCss()
	
	function ContinueLoadScript()
	{
		Chat_LoadMessengerScript("CuteWebUI","Script/CuteWebUI.js",ContinueLoadScript);
		Chat_LoadMessengerScript("__cc_version","LoadSettings.aspx",ContinueLoadScript);
		Chat_LoadMessengerScript("ChatService_CreateXmlHttp","LoadScripts.aspx",ContinueLoadScript);
		Chat_LoadMessengerScript("CuteChatMessenger","Script/NewMessenger.js",ContinueLoadScript);
		if(typeof(CuteWebUI)!="undefined")
		{
			CuteWebUI.HTML.ShowDialogMask();
		}
		if(typeof(CuteChatMessenger)!="undefined")
		{
			CuteWebUI.HTML.HideDialogMask();
			ContinueShowResponse();
		}
	}
	ContinueLoadScript();
	
	function ContinueShowResponse()
	{
		var msgs=[];
		if(info.OfflineMsgCount>0)
			msgs.push("You have "+info.OfflineMsgCount+" offline message(s).");
		if(info.Messages.length>0)
		{
			msgs.push("You have "+info.Messages.length+" new message(s).");
			
			for(var i=0;i<info.Messages.length;i++)
			{
				var msg=info.Messages[i];
				var uid=msg[0];
				var uname=msg[1];
				var text=msg[2];
				var html=msg[3];
				msgs.push(uname+" says "+text);
			}
		}
		
		var win=window.__ccprwin;
		
		if(msgs.length==0)
		{
			if(win!=null)
			{
				win.Close()
			}
			return;
		}
		
		if(window.__ccprwin==null)
		{
			window.__ccprwin=new CuteWebUI.HTML.Window({
				onclose:function()
				{
					window.__ccprwin=null;
				}
			});
			win=window.__ccprwin;
			
			win.SetTitle("Cute Web Messenger");
			win.SetHeight(150);
			win.MoveToScreenCenter();

			win.GetContentElement().innerHTML="<div id='cwui_msg'></div><div id='cwui_btns'><button id='cwui_ok'>OK</button>&nbsp;&nbsp;&nbsp;<button id='cwui_cancel'>Cancel</button></div>";
		}
		
		var ce=win.GetContentElement();
		var msglist=CuteWebUI.HTML.FindChild(ce,"cwui_msg");
		msglist.innerHTML=Chat_HtmlEncode(msgs.join("\r\n"));
		var ok=CuteWebUI.HTML.FindChild(ce,"cwui_ok");
		ok.onclick=function()
		{
			Chat_OpenMessengerDialog();
			win.Close();
		}
		var cc=CuteWebUI.HTML.FindChild(ce,"cwui_cancel");
		cc.onclick=function()
		{
			win.Close()
		}
		win.SetOKText=function(text)
		{
			ok.innerHTML=CuteWebUI.HTML.Encode(text);
		}
		win.SetCancelText=function(text)
		{
			cc.innerHTML=CuteWebUI.HTML.Encode(text);
		}
		win.SetOKText(TEXT("OK"));
		win.SetCancelText(TEXT("Cancel"));
	}
}

function Chat_HtmlEncode(text)
{
	if(!text)return "";
	text=String(text);
	return text.replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/\x22/g,"&quot;").replace(/\x27/g,"&#39;").replace(/\n/g,"<br/>").replace(/\r/g,"");
}

function Chat_ShowMessages(info)
{
	if(info.OfflineMsgCount==0 && info.Messages.length==0)
		return;

	var msgs=[];
	if(info.OfflineMsgCount>0)
		msgs.push("You have "+info.OfflineMsgCount+" offline message(s).");
	if(info.Messages.length>0)
	{
		msgs.push("You have "+info.Messages.length+" new message(s).");
		
		for(var i=0;i<info.Messages.length;i++)
		{
			var msg=info.Messages[i];
			var uid=msg[0];
			var uname=msg[1];
			var text=msg[2];
			var html=msg[3];
			msgs.push(uname+" says "+text);
		}
	}

	var pmmessagespanel=document.getElementById("pmmessagespanel");
	
	pmmessagespanel.innerHTML=Chat_HtmlEncode(msgs.join("\r\n"));
	
	if(document.getElementById("pmmessageswindow").style.visibility!="hidden")
		return;

	var ie=document.all;
	var ns4=document.layers;
	var dom=document.getElementById;
	var calunits=(ns4)? "" : "px";
	var crossobj;

	function dropin(){
		scroll_top=(ie)? truebody().scrollTop : window.pageYOffset
		if (parseInt(crossobj.top)<150+scroll_top) {
			crossobj.top=parseInt(crossobj.top)+40+calunits;
		} else {
			clearInterval(dropstart)
		}
	}

	function truebody(){
		return (document.compatMode && document.compatMode!="BackCompat")? document.documentElement : document.body;
	}
	
	if (!dom&&!ie&&!ns4) return;
	crossobj=(dom)?document.getElementById("pmmessageswindow").style : ie? document.all.pmmessageswindow : document.pmmessageswindow;
	scroll_top=(ie)? truebody().scrollTop : window.pageYOffset;
	scroll_left=(ie)? (truebody().offsetWidth)/2+truebody().scrollLeft-(300/2): 200;
	crossobj.left=scroll_left+calunits;
	crossobj.top=scroll_top-250+calunits;
	crossobj.visibility=(dom||ie)? "visible" : "show";
	dropstart=setInterval(dropin,60);
	window.focus();
}

function Chat_OpenMessages()
{
	document.getElementById("pmmessageswindow").style.visibility="hidden";
	Chat_OpenMessengerDialog();
}
function Chat_CloseMessages()
{
	document.getElementById("pmmessageswindow").style.visibility="hidden";
}

var URL_close_chat_button = __cc_urlbase+"images/closechat.gif";
document.write('<div id="pmmessageswindow" style="Z-INDEX: 99999;position:absolute;visibility:hidden;left:0px;top:0px;border:solid 1px gray;background-color:white;padding:4px;"><a href="javascript:Chat_OpenMessages();"><div style="text-align:center;background-color:steelblue;color:white;">Messages</div><div id="pmmessagespanel" style="padding:4px;"></div></a><div style="margin-top:0px;margin-left:50px"><a href="javascript:Chat_CloseMessages();"><img src="'+URL_close_chat_button+'" border="0"></a></div></div>');


//document.title="partial messenger script OK"
//partialmessengertimeout=1000;




function Chat_OpenContact(targetuserloginname)
{
	function SendOpenContactCommand()
	{		
		var xmlhttp=Chat_CreateXmlHttp();
		xmlhttp.open("POST",chatservice_url,true);
		xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=utf-8");
		var arr=[];
		arr[arr.length]="METHOD=OPENCONTACT";
		arr[arr.length]="CONTACT="+encodeURIComponent(targetuserloginname);
		var data=arr.join("&")
		xmlhttp.send(data);
	}
	function CheckMessenger()
	{
		if(GetCookie("CuteChatIMMainForm"))
		{
			SendOpenContactCommand()
		}
		else
		{
			setTimeout(CheckMessenger,100);
		}
	}
	
	Chat_OpenMessengerDialog();
	CheckMessenger()
	
}

if(<%=Context.Request.QueryString["Chat_StartPartialMessenger"]=="1"?"true":"false"%>)
{
	Chat_StartPartialMessenger();
}















