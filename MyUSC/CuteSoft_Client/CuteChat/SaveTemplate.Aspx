<%@ Page Language="C#" ValidateRequest="false" Inherits="CuteChat.ChatPageBase" %>
<script runat="server">
protected override void OnInit(EventArgs args)
{
	base.OnInit(args);
}
string postedhtml;
protected override void OnLoad(EventArgs args)
{
	base.OnLoad(args);
	
	if(Request.HttpMethod=="POST")
	{
		postedhtml=Request.Form["input1"];
	}
}
</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD runat=server>
		<TITLE>Chat Log
			<%=DateTime.Now%>
		</TITLE>
		<base href='<%=Request.Url.ToString().Substring(0,Request.Url.ToString().LastIndexOf("/"))%>' />
		<link rel='stylesheet' href='<%=ResolveUrl("style.css")%>' />
		<link rel='stylesheet' href='<%=ResolveUrl("im_style.css")%>' />
		<link rel='stylesheet' href='<%=ResolveUrl("Skins/Normal/style.css")%>' />
	</head>
	<%if(postedhtml==null){%>
	<BODY>
		<div id="chat_messagelist" style="padding:8px">
		</div>
		<form id="form1" method=post action="<%=Request.RawUrl%>">
			<input name="input1" type="hidden">
		</form>
	</BODY>
	<%}else{%>
		<script>
		if(document.referrer&&location.href.substring(0,10)=='<%=Request.Url.ToString().Substring(0,10).ToLower()%>')
		{
			//TODO: localization
			alert("To save the messages, right click the page and choose 'save as'");
		}
		</script>
	<BODY>
		<div id="chat_messagelist" style="padding:8px">
			<%=postedhtml%>
		</div>
	</BODY>
	<%}%>
</html>
