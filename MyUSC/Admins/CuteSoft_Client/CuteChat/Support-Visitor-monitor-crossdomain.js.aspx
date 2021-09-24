<%@ Page Language="c#" ContentType="text/plain" Inherits="CuteChat.ChatCrossDomainScript" %>
<%@ Import Namespace="CuteChat" %>
//<html runat=server Visible=false ID="Html1"><head runat=server ID="Head1"></head></html>

//<script>


function InitCCCustomerId()
{
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
	
	var customerid=GetCookie("CCCustomerId");
	if(!customerid)
	{
		customerid='<%=ChatWebUtility.CreateGuidByDate()%>';
		SetCookie("CCCustomerId",customerid,86400);
	}
	
//	document.ondblclick=function()
//	{
//		SetCookie("CCCustomerId",customerid,-1);
//		alert(GetCookie("CCCustomerId"));
//	}
	
	return customerid;
	
	
}


var CCCustomerId=InitCCCustomerId();



var _type='<%=type%>'
var _status='<%=status%>';
var _result='<%=result%>';

//document.title=CCCustomerId+" | "+_type+" | "+_status+" , "+_result+" - "+new Date().getSeconds();

// Do modify this line
var __cc_urlbase='<%=baseurl%>';

var chat_visitTimerid=0;
var _chat_interval=10000;//10s

// URL of the image that pops up when you send a chat invitation to your website visitors
var URL_live_help_window = __cc_urlbase+"images/live-help-window.gif";

// URL of the close image button
var URL_close_chat_button = __cc_urlbase+"images/closechat.gif";


// Default window ornaments for the live help dialog
var _liveHelpDialogFeature = "status=1,width=500,height=400,resizable=1";
var _chat_scriptid="LiveSupportVisitorMonitorScript";

	
function Chat_ChangeUrl(type)
{
	var _chat_script=document.getElementById("LiveSupportVisitorMonitorScript");
	if(_chat_script==null)
	{
		throw(new Error("tag LiveSupportVisitorMonitorScript not found!"));
	}
	var url=__cc_urlbase+'<%=System.IO.Path.GetFileName(Request.FilePath)%>'
		+"?_temp="+(new Date().getTime())+"&Type="+type
		+"&CCCustomerId="+CCCustomerId
		+"&Referrer="+escape(document.referrer);
		
	var _new_script = document.createElement('SCRIPT');
    _new_script.id = _chat_scriptid;
    _new_script.src = url;

    _chat_script.parentNode.replaceChild(_new_script,_chat_script);

}

function Chat_NextRequest()
{
	Chat_ChangeUrl("VISIT")
}

var chat_inviteconfirming=false;
function Chat_AcceptInvite()
{
	chat_inviteconfirming=false;
	Chat_ChangeUrl("ACCEPT")
}
function Chat_RejectInvite()
{
	chat_inviteconfirming=false;
	Chat_ChangeUrl("REJECT")
}
function Chat_CloseConfirm()
{
}

if( _status=="LOADING" )
{
	//script is loading first time
	
	// write the live support window to the page
	document.write('<div id="invitechatwindow" style="Z-INDEX: 99999;position:absolute;visibility:hidden;left:0px;top:0px;"><a href="javascript:acceptchatwindow()"><img src="'+URL_live_help_window+'" border=0></a><div style="margin-top:0px;margin-left:50px"><a href="javascript:closechatwindow();"><img src="'+URL_close_chat_button+'" border="0"></a></div></div>');

	//chat_visitTimerid=setTimeout(Chat_NextRequest,_chat_interval);
	chat_visitTimerid=setTimeout(Chat_NextRequest,1000);
}
else
{
	var arr=_result.split(":");
	if(_type=="VISIT")
	{
		if(_status!="INVITE")
		{
			if(chat_inviteconfirming)
			{
				Chat_CloseConfirm()
				chat_inviteconfirming=false;
			}
		}
		
		if(_status=="INVITE")
		{
			//do not stop the timer, so the server can know the client is still online.
			//clearTimeout(chat_visitTimerid);//suppend
			if(!chat_inviteconfirming)
			{
				var opname=arr[1];
				Chat_InviteConfirm(opname);
				chat_inviteconfirming=true;
			}
		}
		//else
		//{
			chat_visitTimerid=setTimeout(Chat_NextRequest,_chat_interval);
		//}
		
	}
	if(_type=="ACCEPT")
	{
		if(_status=="READY")
		{
			var placename=_result;
			var url=__cc_urlbase+"SupportClient.aspx?PromptWait=0&PlaceName="+escape(placename)+"&CCCustomerId="+CCCustomerId;
			var win=window.prepopup;
			if(win!=null)
			{
				win.location.href=url;
				window.prepopup=null;
			}
			else
			{
				try
				{
					win=window.open(url,'',_liveHelpDialogFeature);
				}
				catch(x)
				{
				}
				
				if(win==null)
				{
					alert("Pop-up blocker detected.");
				}
			}
		}
		if(_status=="EXPIRED")
		{
			alert(" operation expired .");
		}
		if(_status=="ERROR")
		{
			alert(text.substring(6));
		}
		if(window.prepopup)
		{
			window.prepopup.close();
			window.prepopup=null;
		}
	}
	
}



var dropstart=0;
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
function acceptchatwindow()
{
	crossobj.visibility="hidden";
	window.prepopup=window.open("about:blank",'',_liveHelpDialogFeature);
	Chat_AcceptInvite();
}
function closechatwindow(){
	crossobj.visibility="hidden";
	Chat_RejectInvite();
}

function truebody(){
	return (document.compatMode && document.compatMode!="BackCompat")? document.documentElement : document.body;
}


function Chat_InviteConfirm(){
	if (!dom&&!ie&&!ns4) return;
	crossobj=(dom)?document.getElementById("invitechatwindow").style : ie? document.all.invitechatwindow : document.invitechatwindow;
	scroll_top=(ie)? truebody().scrollTop : window.pageYOffset;
	scroll_left=(ie)? (truebody().offsetWidth)/2+truebody().scrollLeft-(300/2): 200;
	crossobj.left=scroll_left+calunits;
	crossobj.top=scroll_top-250+calunits;
	crossobj.visibility=(dom||ie)? "visible" : "show";
	dropstart=setInterval("dropin()",60);
	window.status='Live Support';
}
function Chat_CloseConfirm()
{
	if(crossobj)crossobj.visibility="hidden";
}




