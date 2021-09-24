<%@ Page Language="C#" Inherits="CuteChat.ChatPageBase" %>
<%@ Import Namespace="CuteChat" %>
<html>
	<head>
		<title>[[UI_MessageHistory]]</title>
		<meta http-equiv=content-type content="text/html; charset=UTF-8" />
		<link rel="icon" href="Icons/Messenger.ico" type="image/x-icon" />
		<link rel="shortcut icon" href="Icons/Messenger.ico" type="image/x-icon" />
		<style type="text/css">
		table.Grid
		{
			border-width: 5px;
			border-style: none;
			background-color: White;
			border-color: #BED6E0;
			border-collapse: collapse;
			Width:99%;
		}

		table.Grid TD, table.Grid TH
		{
			padding: 4px 6px 4px 6px;
			border: solid 1px #BED6E0;
			vertical-align: top;
		}

		table.Grid TH
		{
			background-color: #E1E1E1;
			font-weight: normal;
			color: #505050;
		}
		
		</style>
		<script runat="server">

ChatIdentity identity;

override protected void OnLoad(EventArgs args)
{
	identity=ChatWebUtility.GetLogonIdentity();
	
	if(identity==null)
	{
		Response.Redirect(ChatWebUtility.LoginUrl,true);
		return;
	}
	
	base.OnLoad(args);

	if(!IsPostBack)
	{
		BindMsgs(int.MaxValue);
		
		ButtonDeleteAll.Attributes["onclick"]="return confirm('Are you sure that you want to delete all messages?')";
	}
}

private void ButtonDeleteAll_Click(object sender, System.EventArgs e)
{
//	ButtonDeleteAll.Text=identity.UniqueId;
	string targetid=Request.QueryString["TargetId"];
	if(targetid=="")
		targetid=null;
	ChatWebUtility.DeleteInstantMessages(identity.UniqueId,targetid);
	BindMsgs(0);
}

private void ButtonNext_Click(object sender, System.EventArgs e)
{
	int maxval=(int)ViewState["NextMaxId"];
	BindMsgs(maxval);
}

private void BindMsgs(int currentMaxId)
{
	string targetid=Request.QueryString["TargetId"];
	ChatMsgData[] msgs=ChatWebUtility.LoadInstantMessages(identity.UniqueId,targetid,false,currentMaxId,DataGrid1.PageSize);
	int maxval=0;
	if(msgs.Length!=0)
	{
		maxval=msgs[msgs.Length-1].MessageId;
	}
	ViewState["NextMaxId"]=maxval;
	DataGrid1.DataSource=msgs;
	DataGrid1.DataBind();
	ButtonNext.Enabled=DataGrid1.Items.Count==DataGrid1.PageSize;
}
protected string GetOfflineColumnHTML(object item)
{
	ChatMsgData msg=(ChatMsgData)item;
	if(msg.Offline)
	{
		return "<span style='color:red'>True</span>";
	}
	return "&nbsp;";
}
protected string GetSenderColumnHTML(object item)
{
	ChatMsgData msg=(ChatMsgData)item;
	if(msg.SenderId==identity.UniqueId)
	{
		return msg.Sender;
	}
	else
	{
		return "<a href='"+ChatWebUtility.ReplaceParam(Request.RawUrl,"TargetId",msg.SenderId.ToString())+"'>"
			+msg.Sender+"</a>";
	}
}
protected string GetTargetColumnHTML(object item)
{
	ChatMsgData msg=(ChatMsgData)item;
	if(msg.TargetId==identity.UniqueId)
	{
		return msg.Target;
	}
	else
	{
		return "<a href='"+ChatWebUtility.ReplaceParam(Request.RawUrl,"TargetId",msg.TargetId.ToString())+"'>"
			+msg.Target+"</a>";
	}
}
protected string GetMessageColumnHTML(object item)
{
	ChatMsgData msg=(ChatMsgData)item;
	if(msg.Html!=null&&msg.Html!="")
	{
		return msg.Html;
	}
	if(msg.Text!=null&&msg.Text!="")
	{
		return msg.Text;
	}
	return "&nbsp;";
}

		</script>
		<link rel="stylesheet" href="IM_style.css">
		<style>		
			input,button
			{
				font:normal 9pt Arial;
			}
		</style>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<BODY>
		<form name="Form1" runat="server" ID="Form1">
			<div class="dialogPageHeader" style="height:25px">
				<table width="100%" background='images/up.gif' cellpadding="4" cellspacing="0" border="0">
					<tr>
						<td style="padding-left:15px">
							<a href="MessageHistory.aspx"><img src="images/group.png" title="Show messages of all users" hspace="5" border=0 align="absmiddle">All Users</a>
						</td>
						<td>
							<asp:linkbutton id="ButtonDeleteAll" Runat="server" OnClick="ButtonDeleteAll_Click" ><img src="images/delete.png" title="[[UI_MENU_DeleteAll]]" hspace="5" border=0 align="absmiddle">[[UI_MENU_DeleteAll]]</asp:linkbutton>
						</td>
						<td style="text-align:right;">
							<a id="buttonback" href="####" onclick="do_history_back()"><img src="images/arrow_left.png" title="[[Back]]" hspace="5" border=0 align="absmiddle">[[Back]]</a>&nbsp;&nbsp;&nbsp;
							<asp:linkbutton id="ButtonNext" Runat="server" OnClick="ButtonNext_Click" >[[Next]]<img src="images/arrow_right.png" title="[[Next]]" hspace="5" border=0 align="absmiddle"></asp:linkbutton>
					
						</td>
					</tr>
				</table>
			</div>
			<div Style="padding:5px 10px 5px 10px;height:300px">
				<asp:DataGrid runat="server" id="DataGrid1" CssClass="Grid" CellPadding="3" AutoGenerateColumns="False">
					<ItemStyle BackColor="#F5F7F9" Height="23" Font-Size="11px"></ItemStyle>
					<AlternatingItemStyle BackColor="#F2F4F7" Height="23" Font-Size="11px"></AlternatingItemStyle>
					<HeaderStyle BackColor="#DDE8F0"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="Time" HeaderText="Time" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
							<ItemStyle Font-Size="9px" Wrap="False"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Offline">
							<ItemStyle Wrap="False"></ItemStyle>
							<ItemTemplate>
								<%# GetOfflineColumnHTML(Container.DataItem) %>&nbsp;&nbsp;
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Sender">
							<ItemStyle Wrap="False"></ItemStyle>
							<ItemTemplate>
								<%# GetSenderColumnHTML(Container.DataItem) %>&nbsp;&nbsp;
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Target">
							<ItemStyle Wrap="False"></ItemStyle>
							<ItemTemplate>
								<%# GetTargetColumnHTML(Container.DataItem) %>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Message">
							<ItemStyle Width="100%"></ItemStyle>
							<ItemTemplate>
								<%# GetMessageColumnHTML(Container.DataItem) %>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Right" ForeColor="#000066" Position="TopAndBottom" BackColor="White"
						Mode="NumericPages"></PagerStyle>
				</asp:DataGrid>
				<asp:Label id="LabelMessage" runat="server" Visible="False" EnableViewState="False" ForeColor="#C00000">Label</asp:Label>
			</div>
		</form>
	</BODY>
	<script>
		var ispostback=<%=IsPostBack?"true":"false"%>;
		if(!ispostback)
		{
			document.getElementById("buttonback").disabled=true;
		}
		function do_history_back()
		{
			if(ispostback)
				history.back();
		}
	</script>
</html>