<%@ Import Namespace="CuteChat" %>
<%@ Control Language="c#" AutoEventWireup="false" Inherits="CuteChat.ChatCtrlBase" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="LobbyItem" Src="LobbyItem.ascx" %>
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
	
	HtmlButton managerlistbutton=(HtmlButton)item.FindControl("ButtonManagerList");
	managerlistbutton.Attributes["onclick"]="window.open('"+
		ResolveUrl("EditLobbyManagerList.aspx")+"?LobbyId="+lobby.LobbyId+"','','resizable=1,status=0,help=0,width=500,height=330')";
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
}

string _alertmsg;
void Alert(string msg)
{
	_alertmsg=msg;
	holder_alert.Visible=true;
}


</script>
<asp:PlaceHolder ID="holder_alert" Runat="server" Visible="False" EnableViewState="False">
	<SCRIPT>
setTimeout(function(){
alert('<%=_alertmsg%>');
},400);
	</SCRIPT>
</asp:PlaceHolder>
<asp:DataGrid id="DataGrid1" DataKeyField="LobbyId" OnItemDataBound="DataGrid1_ItemDataBound"
	OnDeleteCommand="DataGrid1_DeleteCommand"	
	runat="server" AutoGenerateColumns="False" BorderColor="#999999" Font-Name="Verdana" Font-Size="8pt"
	BorderWidth="1px" BackColor="White" width="630" CellPadding="5" CellSpacing="0">
	<FooterStyle BackColor="#E9F0FA"></FooterStyle>
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#888888"></SelectedItemStyle>
	<AlternatingItemStyle BackColor="#f6f6f6"></AlternatingItemStyle>
	<ItemStyle BackColor="White"></ItemStyle>
	<HeaderStyle Font-Bold="True" BackColor="#eeeeee"></HeaderStyle>
	<Columns>	
		<asp:BoundColumn DataField="LobbyId" HeaderText="ID"></asp:BoundColumn>
		<asp:BoundColumn DataField="Title" HeaderText="Name" ItemStyle-Width="200px"></asp:BoundColumn>
		<asp:BoundColumn DataField="AllowAnonymous" HeaderText="Anonymous" ItemStyle-Width="50px"></asp:BoundColumn>
		<asp:BoundColumn DataField="SortIndex" HeaderText="Sort"></asp:BoundColumn>
		<asp:BoundColumn DataField="Password" HeaderText="Password"></asp:BoundColumn>
		<asp:BoundColumn DataField="Integration" HeaderText="Integration" ItemStyle-Width="50px"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Delete">
			<ItemTemplate>
				<asp:LinkButton OnInit="Button_Confirm_Init" ConfirmMessage="Are you sure you want to delete this chat room?"
					runat="server" Text="Delete" CommandName="Delete" CausesValidation="false"></asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:DataGrid>