<%@ Page language="c#" AutoEventWireup="false"  Inherits="CuteChat.ChatPageBase" %>
<%@ Import Namespace="CuteChat" %>
<%@ Import Namespace="System.IO" %>

<script runat=server>

override protected void OnLoad(EventArgs args)
{
	base.OnLoad(args);
	
	ArrayList avatars=new ArrayList();
	
	for(int i=1;i<=33;i++)
	{
		avatars.Add("AvatarChat/a"+i+".png");
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
			<div Style="margin-bottom: 15px; padding: 15px 0 0 10px;height:180px;"> 
				<asp:Repeater ID=Repeater1 Runat=server>
					<ItemTemplate>
						<div class="AvatarImage" hspace="2" style="float:left;margin:4px;border: #cccccc 1px solid;width:32px;height:52px;background-image:url(<%#(string)Container.DataItem%>);background-position:-16px -6px"
						 onclick="ShowAvatar('<%#HttpUtility.HtmlEncode((string)Container.DataItem)%>')" 
						 onmouseover="this.style.backgroundColor='#D2DBEE';this.style.border='1px solid #FD5301';" 
						 onmouseout="this.style.backgroundColor='';this.style.border='1px solid #cccccc'">
						 </div >
					</ItemTemplate>
				</asp:Repeater>			
			</div>			
			<div class="dialogPageButtons">
				<table width="100%" border=0 cellspacing=0 cellpadding=0 background='images/up.gif'>
					<tr >
						<td height="30" align=center valign=top class="dialogButtonRow">
							<button id="btncancel" style="width:72px">[[Cancel]]</button>
						</td>            
					</tr>
				</table>
			</div>	
	</body>
	<script>
	function ShowAvatar(avatar)
	{
		var re=/AvatarChat\/a(\d+)\.png/ig
		avatar=avatar.replace(re,"$1");

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
