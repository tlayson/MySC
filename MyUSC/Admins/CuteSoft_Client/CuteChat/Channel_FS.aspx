<%@ Page Language="c#" Inherits="CuteChat.ChatFramePage" AutoEventWireup="false" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="CuteChat" %>
<script runat="server">
protected override void OnLoad(EventArgs args)
{
	base.OnLoad(args);

	if(this.GuestName==null||this.GuestName=="")
	{
		bool autogenname=true;
		if(autogenname)
		{
			this.GuestName="Guest_"+Guid.NewGuid().ToString().Substring(0,4);
			this.Connect();
		}
		else
		{
			this.ConnectStatus="NEEDNAME";
		}
	}
	else
	{
		this.Connect();
	}
}
</script>
<%if(this.ConnectStatus=="READY"||this.ConnectStatus==null){%>
<%	
topframe.Attributes["src"]="Channel_FS_Top.aspx"+UrlQuery;
messageframe.Attributes["src"]="Channel_FS_Message.aspx"+UrlQuery;
onlineframe.Attributes["src"]="Channel_FS_Online.aspx"+UrlQuery;
inputframe.Attributes["src"]="Channel_FS_Input.aspx"+UrlQuery;
%>
<html>
	<head runat=server>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
		<meta HTTP-EQUIV="Content-Type" CONTENT="text/html;">
		<title>
			<%=Server.HtmlEncode(this.ChannelTitle)%>
		</title>
	</head>	
	<frameset rows="80,*,80" border="0" style="margin-left:10" >
		<frame runat="server" name="topframe" id="topframe" scrolling="no" />
		<frameset cols="*,200">
			<frame runat="server" id="messageframe" name="messageframe" scrolling="auto" />
			<frame runat="server" id="onlineframe" name="onlineframe" scrolling="auto" />	
		</frameset>		
		<frame runat="server" id="inputframe" name="inputframe" scrolling="no" />
		<noframes>
			<body>
				<p>Your Web Browser Does Not Support FRAMESET</p>
			</body>
		</noframes>
	</frameset>
</html>
<%}else if(this.ConnectStatus=="ERROR"){%>
<html>
	<head>
		<title>
			<%=this.ConnectStatusMessage%>
		</title>
	</head>
	<body>
		Error
		<br>
		<%=this.ConnectStatusMessage%>
		<br>
		If you haven't yet logged in, please click <a href="<%=ResolveUrl("~/Login.aspx")%>">
			here</a> to log in.
		<br>
		<br>
		<a href="<%=ResolveUrl("~/")%>">Back</a>
	</body>
</html>
<%}else if(this.ConnectStatus=="IDEXISTS"){%>
<html>
	<head>
		<title>Loading...</title>
		<meta http-equiv="REFRESH" content="3;URL=<%=Server.HtmlEncode(ReloadURL)%>" />
		<script><!--
		setTimeout(RedirectRefresh,2000);
		function RedirectRefresh()
		{
			location.href="<%=ReloadURL%>";
		}
		--></script>
	</head>
	<body>
		Loading...
	</body>
</html>
<%}else{%>
<html>
	<head>
		<title>
			<%=this.ConnectStatus%>
		</title>
	</head>
	<body>
		<%=this.ConnectStatus%>
		<form action="<%=Request.Url%>" method=POST id=form1>
			<table>
				<%if(ChatWebUtility.GetLogonIdentity()==null){%>
				<tr>
					<td>[[GuestName]]</td>
					<td><input type=text name="GuestName" value="<%=GuestName%>"></td>
					<td>
						Or <a href="<%=ResolveUrl("~/Login.aspx")%>">Login</a></td>
				</tr>
				<%}else{%>
				<input type="hidden" name="GuestName" value="<%=GuestName%>">
				<%}%>

				<tr>
					<td>[[Password]]</td>
					<td><input type=text name="Password" value="<%=Password%>"></td>
					<td></td>
				</tr>

				<tr>
					<td></td>
					<td><input type="submit" value="Submit"></td>
					<td></td>
				</tr>
			</table>
		</form>
	</body>
</html>
<%}%>
