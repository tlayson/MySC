<%@ Page Language="c#" Inherits="CuteChat.ChatPageBase" %>
<%@ OutputCache VaryByParam="*" Location="Client" Duration="3600" %>
<html runat="server" Visible="false">
	<head runat="server">
	</head>
</html>
<script runat=server>
bool writesl=true;
override protected void OnInit(EventArgs args)
{
	base.OnInit(args);
	Response.ContentType="text/plain";
	CuteChat.ChatWebUtility.InstallGzipForResponse();
	
	if(Request.QueryString["nosl"]=="1")
	{
		writesl=false;
	}
}
</script>
<%if(false){%><%}%>


function ScriptResolver(name)
{
	var xh;
	if(typeof(XMLHttpRequest)!="undefined")
		xh=new XMLHttpRequest();
	else
		xh=new ActiveXObject("Microsoft.XMLHTTP"); 
	//xh.open("GET",__cc_urlbase+name+".js?_random="+new Date().getTime(),false);//no cache 
	xh.open("GET",__cc_urlbase+name+".js",false);
	xh.send(""); 
	if(xh.status!=200)
	{
		throw(new Error("unable to load library "+name));
	}
	return xh.responseText; 
}

function ScriptExecutor(script,name)
{ 
	if(window.execScript)
		window.execScript(script,"JavaScript"); 
	else
		eval(script);
}

	
//
<%
Response.WriteFile("script/protoutil.js");
%>


<%
if(writesl)Response.WriteFile("script/core.js");
%>

//

<%
if(writesl)Response.WriteFile("script/html.js");
%>

//

<%
if(writesl)Response.WriteFile("script/oldmenutreeimpl.js");
%>

//

<%
if(writesl)Response.WriteFile("script/htmlmenu.js");
%>

//
<%
if(writesl){
%>
//
_SL_SetVariable("ScriptLibraryPath",__cc_urlbase);
_SL_SetVariable("CuteChatUrlBase",__cc_urlbase);
TEXT_CUTECHATURLBASE=__cc_urlbase;

_SL_AddTranslater(GetString);
_SL_SetContentFilter(ReplaceStrings);

<%
}
%>

<%
Response.WriteFile("script/chatclient.js");
%>

//

<%
Response.WriteFile("script/ChatUI.js");
%>

//