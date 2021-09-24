<%@ Page Language="c#" ContentType="text/plain" %>
//<html runat=server Visible=false ID="Html1"><head runat=server ID="Head1"></head></html>
//<script>
<%
	string __cc_urlbase=Request.Url.ToString();
	__cc_urlbase=__cc_urlbase.Substring(0,__cc_urlbase.LastIndexOf("/")+1 );	
%>
// Do modify this line
var __cc_urlbase='<%=__cc_urlbase%>';

// URL of the image that pops up when you send a chat invitation to your website visitors
var URL_live_help_window = __cc_urlbase+"images/live-help-window.gif";

// URL of the close image button
var URL_close_chat_button = __cc_urlbase+"images/closechat.gif";


// write the live support window to the page
document.write('<div id="invitechatwindow" style="Z-INDEX: 99999;position:absolute;visibility:hidden;left:0px;top:0px;"><a href="javascript:acceptchatwindow();"><img src="'+URL_live_help_window+'" border=0></a><div style="margin-top:0px;margin-left:50px"><a href="javascript:closechatwindow();"><img src="'+URL_close_chat_button+'" border="0"></a></div></div>');


// Default window ornaments for the live help dialog
var _liveHelpDialogFeature = "status=1,width=500,height=400,resizable=1";

StartInterval();
function StartInterval()
{
	var theinterval=10000;//10s
	Chat_IntervalVisit(theinterval);
}























































//this file provide some function for the pages

var chat_visitTimerid=0;
var chat_visitTimeout=10000;
var chat_inviteconfirming=false;
var chat_customerid=null;

function Chat_VisitResetTimeout()
{
	clearTimeout(chat_visitTimerid);
	chat_visitTimerid=setTimeout(Chat_VisitSendRequest,chat_visitTimeout);
}

function Chat_IntervalVisit(interval)
{
	if(interval)chat_visitTimeout=parseInt(interval)||chat_visitTimeout;
	if(chat_visitTimeout<3000)chat_visitTimeout=10000;
	
	Chat_VisitSendRequest();
}
function Chat_CreateRequest()
{
	if(typeof(XMLHttpRequest)!="undefined")
		return new XMLHttpRequest();
	return new ActiveXObject("Microsoft.XMLHTTP");
}
function Chat_AcceptInvite()
{
	chat_inviteconfirming=false;
	
	var xh=Chat_CreateRequest();
	var url=__cc_urlbase+"SupportCustomerHandler.ashx?_temp="+(new Date().getTime())+"&Type=ACCEPT";
	if(chat_customerid!=null)
	{
		url+="&CCCustomerId="+escape(chat_customerid);
	}
	xh.open("POST",url,false,null,null);
	xh.send("");
	if(xh.status!=200)
	{
		alert("Network or server error!");
		Chat_VisitResetTimeout();
		return;
	}
	var text=xh.responseText;
	if(text==null||text=="")
	{
		alert("Unknown error!");
		Chat_VisitResetTimeout();
		return;
	}
	var arr=text.split(":");
	switch(arr[0])
	{
		case "READY":
			var placename=text.substring(6);
			var win;
			
			//var url=__cc_urlbase+"SupportClient.aspx?PlaceName="+escape(placename);
			var url=__cc_urlbase+"SupportClient.aspx?PromptWait=0";
			if(chat_customerid!=null)
			{
				url+="&CCCustomerId="+escape(chat_customerid);
			}
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
			break;
		case "EXPIRED":
			alert(" operation expired.");
			Chat_VisitResetTimeout();
			break;
		case "ERROR":
			alert(text.substring(6));
			Chat_VisitResetTimeout();
			break;
		default:
			alert("Unknown error!");
			Chat_VisitResetTimeout();
			break;
	}
	if(window.prepopup)
	{
		window.prepopup.close();
		window.prepopup=null;
	}
}
function Chat_RejectInvite()
{
	chat_inviteconfirming=false;
	
	var xh=Chat_CreateRequest();
	var url=__cc_urlbase+"SupportCustomerHandler.ashx?_temp="+(new Date().getTime())+"&Type=REJECT";
	if(chat_customerid!=null)
	{
		url+="&CCCustomerId="+escape(chat_customerid);
	}
	xh.open("GET",url,false,null,null);
	xh.send("");
}

function Chat_InviteConfirm(opname)
{
	if(confirm("Need help? \r\n Website operator '"+opname+"' invites you for a live chat."))
		Chat_AcceptInvite();
	else
		Chat_RejectInvite();
}
function Chat_CloseConfirm()
{
}
function Chat_IntervalVisitHandleResponse(xh)
{
	if(xh.status!=200)return;//ignore error
	var text=xh.responseText;
	if(text==null||text=="")return;//unknown output
	
	//document.title=new Date().getSeconds()+" - "+text;
	
	var arr=text.split(":");
	
	if(arr[0]!="INVITE")
	{
		if(chat_inviteconfirming)
		{
			Chat_CloseConfirm()
			chat_inviteconfirming=false;
		}
	}
	
	switch(arr[0])
	{
		case "READY":
			if(arr[1]=="Guest"&&arr[2])
			{
				if(document.cookie&&document.cookie.indexOf(arr[2])!=-1)
					return;
				chat_customerid=arr[2];
			}
			return;
		case "INVITE":
			//do not stop the timer, so the server can know the client is still online.
			//clearTimeout(chat_visitTimerid);//suppend
			if(!chat_inviteconfirming)
			{
				var opname=arr[1];
				Chat_InviteConfirm(opname);
				chat_inviteconfirming=true;
			}
			break;
		case "ERROR":
			var errmsg=text.substring(6);
		default:
			return;
	}
	
}

function Chat_VisitSendRequest()
{
	var xh=Chat_CreateRequest();
	
	function HandleReadyState()
	{
		if(xh.readyState<4)return;
		
		xh.onreadystatechange=new Function();
		var xh2=xh;
		xh=null;
		
		Chat_VisitResetTimeout();
		
		Chat_IntervalVisitHandleResponse(xh2);
	}
	
	xh.onreadystatechange=HandleReadyState;
	var url=__cc_urlbase+"SupportCustomerHandler.ashx?_temp="+(new Date().getTime());
	if(chat_customerid!=null)
	{
		url+="&CCCustomerId="+escape(chat_customerid);
	}
	url+="&Type=VISIT&Referrer="+escape(document.referrer)
	xh.open("GET",url,true,null,null);
	xh.send("");
}


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
function Chat_CloseConfirm()
{
	if(crossobj)crossobj.visibility="hidden";
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

function truebody(){
	return (document.compatMode && document.compatMode!="BackCompat")? document.documentElement : document.body;
}