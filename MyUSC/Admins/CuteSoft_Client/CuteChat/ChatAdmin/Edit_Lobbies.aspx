<%@ Import Namespace="CuteChat" %>
<%@ Page Language="C#" Inherits="CuteChat.ChatAdminPage" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LobbyItem" Src="LobbyItem.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Menu" Src="Menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD id="Head1">
		<title>Edit chat rooms</title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="style.css">
		<script runat="server">
void Button_Confirm_Init(object sender,EventArgs args)
{
	WebControl c=(WebControl)sender;
	string msg=c.Attributes["ConfirmMessage"];
	if(msg==null)msg="Are you sure?";
	c.Attributes["onclick"]="return confirm('"+msg+"');";
}

string _interationName=null;
public string Integration	//this property is just init , wouldn't save to ViewState
{
	get
	{
		return _interationName;
	}
	set
	{
		_interationName=value;
	}
}

override protected void OnLoad(EventArgs args)
{
	if(null==ViewState["Initialized"])	
	{
		BindGrid();
		
		ViewState["Initialized"]=true;
	}
	
	base.OnLoad(args);
	
	if(Integration!=null)
	{
		LobbyItem1.TextBoxIntegration.ReadOnly=true;
		LobbyItem1.TextBoxIntegration.Text=Integration;
	}
}

void BindGrid()
{
	DataGrid1.SelectedIndex=-1;
	if(Integration==null)
	{
		DataGrid1.DataSource=ChatApi.GetLobbyArray();
	}
	else
	{
		ArrayList lobbies=new ArrayList();
		foreach(IChatLobby lobby in ChatApi.GetLobbyArray())
		{
			if(lobby.Integration==Integration)
				lobbies.Add(lobby);
			DataGrid1.DataSource=lobbies;
		}
	}
	DataGrid1.DataBind();
}

void DataGrid1_ItemDataBound(object sender,DataGridItemEventArgs args)
{
	DataGridItem item=args.Item;
	DropDownList ddl=(DropDownList)item.FindControl("ManagerList");
	if(ddl==null)return;
	
	ddl.Items.Clear();
	
	IChatLobby lobby=(IChatLobby)item.DataItem;
	if(lobby.ManagerList=="")
	{
		ddl.Items.Add(" -  -  -  -  - ");
	}
	else
	{
		try{
			foreach(string strmid in lobby.ManagerList.Split(",".ToCharArray()))
			{
				ddl.Items.Add( ChatApi.GetUserDisplayName(strmid) );
			}
		}
		catch
		{
		}
	}
	
	System.Web.UI.WebControls.Image managerlistbutton=(System.Web.UI.WebControls.Image)item.FindControl("ButtonManagerList");
	managerlistbutton.Attributes["onclick"]="window.open('"+
		ResolveUrl("EditLobbyManagerList.aspx")+"?LobbyId="+lobby.LobbyId+"','','resizable=1,status=0,help=0,width=500,height=330')";
}

void DataGrid1_SelectedIndexChanged(object sender,EventArgs args)
{
	if(DataGrid1.SelectedIndex==-1)
	{
	}
	else
	{
		int lobbyid=(int)DataGrid1.DataKeys[DataGrid1.SelectedIndex];
		IChatLobby lobby=ChatApi.GetLobby(lobbyid);

		LobbyItem1.SetUIValues(lobby);
	}
}
void DataGrid1_DeleteCommand(object sender,DataGridCommandEventArgs args)
{
	int lobbyid=(int)DataGrid1.DataKeys[args.Item.ItemIndex];
	ChatApi.RemoveLobby(lobbyid);
	BindGrid();
}

override protected void OnPreRender(EventArgs args)
{
	base.OnPreRender(args);
	
	if(DataGrid1.SelectedIndex==-1)
	{
	//	ButtonAddNew.Enabled=true;
		ButtonUpdate.Enabled=false;
	//	ButtonCancel.Enabled=false;
	}
	else
	{
	//	ButtonAddNew.Enabled=false;
		ButtonUpdate.Enabled=true;
	//	ButtonCancel.Enabled=true;
	}
}


void ButtonAddNew_Click(object sender,EventArgs args)
{
	IChatLobby lobby=ChatApi.CreateLobbyInstance();

	if(!LobbyItem1.GetUIValues(lobby))
		return;
	
	ChatApi.CreateLobby(lobby);
	
	BindGrid();
}
void ButtonUpdate_Click(object sender,EventArgs args)
{
	int lobbyid=(int)DataGrid1.DataKeys[DataGrid1.SelectedIndex];
	IChatLobby lobby=ChatApi.GetLobby(lobbyid);
	
	if(!LobbyItem1.GetUIValues(lobby))
		return;
	
	ChatApi.UpdateLobby(lobby);
}
void ButtonCancel_Click(object sender,EventArgs args)
{
	DataGrid1.SelectedIndex=-1;
}


string _alertmsg;
void Alert(string msg)
{
	_alertmsg=msg;
	holder_alert.Visible=true;
}


		</script>
	</HEAD>
	<body bottommargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form runat="server" ID="Form2">
			<uc1:Banner id="banner1" runat="server" />
			<table width="840" border="0" cellpadding="3" cellspacing="0">
				<tr>
					<td id="leftcolumn" valign="top">
						<uc1:Menu id="Menu1" runat="server"></uc1:Menu>
					</td>
					<td width="10">&nbsp;</td>
					<td valign="top" align="left" id="content">
						<h1><img src="../images/cup.gif" border="0" alt="Room Administration" align="absMiddle">
							Room Administration</h1>
						<h4>Edit chat rooms</h4>
						<asp:PlaceHolder ID="holder_alert" Runat="server" Visible="False" EnableViewState="False">
							<SCRIPT>
setTimeout(function(){
alert('<%=_alertmsg%>');
},400);
							</SCRIPT>
						</asp:PlaceHolder>
						<asp:DataGrid id="DataGrid1" DataKeyField="LobbyId" OnItemDataBound="DataGrid1_ItemDataBound"
							OnSelectedIndexChanged="DataGrid1_SelectedIndexChanged" runat="server" AutoGenerateColumns="False"
							BorderColor="#999999" Font-Name="Verdana" Font-Size="8pt" BorderWidth="1px" BackColor="White"
							width="630" CellPadding="3" CellSpacing="0">
							<FooterStyle BackColor="#E9F0FA"></FooterStyle>
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#888888"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#f6f6f6"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" BackColor="#eeeeee"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="">
									<ItemTemplate>
										<asp:ImageButton Runat="server" AlternateText="Select this chat room to edit" ImageUrl="../images/ok.gif"
											CommandName="Select" CausesValidation="false" ID="Linkbutton1"></asp:ImageButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="LobbyId" HeaderText="ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="Title" HeaderText="Name" ItemStyle-Width="200px"></asp:BoundColumn>
								<asp:BoundColumn DataField="AllowAnonymous" HeaderText="Anonymous" ItemStyle-Width="50px"></asp:BoundColumn>
								<asp:BoundColumn DataField="SortIndex" HeaderText="Sort"></asp:BoundColumn>
								<asp:BoundColumn DataField="Password" HeaderText="Password"></asp:BoundColumn>
								<asp:BoundColumn DataField="Integration" HeaderText="Integration" ItemStyle-Width="50px"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Moderators">
									<ItemTemplate>
										<asp:DropDownList ID="ManagerList" Runat="server" Width="120px"></asp:DropDownList>
										<asp:Image ID="ButtonManagerList" runat="server" ImageUrl="../images/edit.gif"></asp:Image>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
						<br>
						<uc1:LobbyItem id="LobbyItem1" runat="server"></uc1:LobbyItem>
						<br>
						<asp:Button id="ButtonUpdate" OnClick="ButtonUpdate_Click" runat="server" Text="Update Chat Room"></asp:Button>
					</td>
				</tr>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</HTML>
