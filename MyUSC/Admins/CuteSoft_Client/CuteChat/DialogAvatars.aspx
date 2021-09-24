<%@ Page language="c#" AutoEventWireup="false"  Inherits="CuteChat.ChatPageBase" %>
<%@ Import Namespace="CuteChat" %>
<%@ Import Namespace="System.IO" %>

<script runat=server>

bool IsImage(string filename)
{
	string ext=Path.GetExtension(filename).ToLower();
	if(ext==".gif")return true;
	if(ext==".jpg")return true;
	if(ext==".png")return true;
	return false;
}

string AvatarType;
override protected void OnLoad(EventArgs args)
{
	base.OnLoad(args);
	AvatarType=Context.Request.QueryString["AvatarType"];
	
	string avatarfolder=Server.MapPath("Avatars")+"\\";
	int prefixlen=avatarfolder.Length;
	ArrayList avatars=new ArrayList();
	
	foreach(string filename in Directory.GetFiles(Path.Combine(avatarfolder,AvatarType),"*.*"))
	{
		if(IsImage(filename))avatars.Add( filename.Remove(0,prefixlen).Replace('\\','/') );
	}
	if(AvatarType!="Messenger")
	{
		foreach(string filename in Directory.GetFiles(Path.Combine(avatarfolder,"Everyone"),"*.*"))
		{
			if(IsImage(filename))avatars.Add( filename.Remove(0,prefixlen).Replace('\\','/') );
		}
	}
	
	
		
	Repeater1.DataSource=avatars;
	Repeater1.DataBind();
}
</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD runat=server ID="Head1" NAME="Head1">
		<title>[[UI_Avatar]]
		</title>
		<link rel="stylesheet" href="IM_Style.css">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		</head>
		<%if(AvatarType=="Messenger")%>
		<%{%>
		<style>
		.AvatarImage
		{
			width:48px;
			height:48px;
		}
		</style>
		<%}%>
	
	<BODY style="background-color: #ffffff;">
			<div class="dialogPageHeader">
				<table width="100%" background='images/up.gif' cellpadding="4" cellspacing="0" border="0">
					<tr>
						<td width=60 height=65 align=center valign=middle><img src='images/default.png'><td>
						<td valign=middle>
							<strong>[[UI_Avatar]]</strong><br>
							[[UI_AvatarDialogSubTitle]]
						</td>
					</tr>
				</table>
			</div>			
			<div Style="margin-bottom: 15px; padding: 15px 0 0 10px;"> 
				<asp:Repeater ID=Repeater1 Runat=server>
					<ItemTemplate>
						<img class="AvatarImage" hspace="2" style="BORDER: #cccccc 1px solid" vspace="1" src="DrawAvatar.Ashx?Avatar=<%#HttpUtility.UrlEncode((string)Container.DataItem)%>&AvatarType=<%#AvatarType%>" onclick="ShowAvatar('<%#HttpUtility.HtmlEncode((string)Container.DataItem)%>')" onmouseover="this.style.backgroundColor='#D2DBEE';this.style.border='1px solid #FD5301';" onmouseout="this.style.backgroundColor='';this.style.border='1px solid #cccccc'"/>
					</ItemTemplate>
				</asp:Repeater>			
			</div>			
			<div class="dialogPageButtons">
				<table width="100%" border=0 cellspacing=0 cellpadding=0 background='images/up.gif'>
					<tr >
						<td height="80" align=center valign=top class="dialogButtonRow">
							<button id="btncancel" style="width:72px">[[Cancel]]</button>
						</td>            
					</tr>
				</table>
			</div>	
	</body>
	<script>
	function ShowAvatar(avatar)
	{
		top.returnValue=avatar;
		(top.cc_close||top.close)();
	}
	btncancel.onclick=function()
	{
		top.returnValue=null;
		(top.cc_close||top.close)();
	}
	</script>
</html>
