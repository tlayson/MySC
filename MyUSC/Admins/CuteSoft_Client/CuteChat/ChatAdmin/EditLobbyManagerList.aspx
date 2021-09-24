<%@ Page language="c#" AutoEventWireup="false" Inherits="CuteChat.ChatAdminPage" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="CuteChat" %>
<script runat="server">
void Button_Confirm_Init(object sender,EventArgs args)
{
	WebControl c=(WebControl)sender;
	string msg=c.Attributes["ConfirmMessage"];
	if(msg==null)msg="Are you sure?";
	c.Attributes["onclick"]="return confirm('"+msg+"');";
}
</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server">
		<title>[[ModeratorList]]</title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="../IM_Style.css">
	</head>
	<BODY style="background-color: #ffffff;">
		<form id="Form1" method="post" runat="server">
			<div class="dialogPageHeader">
				<table width="100%" background='../images/up.gif' cellpadding="4" cellspacing="0" border="0">
					<tr>
						<td width="60" height="65" align="center" valign="middle"><img src='../images/moderator.jpg'>
						<td>
						<td valign="middle">
							<strong>[[ModeratorList]]</strong><br>
							[[ModeratorList_subTitle]]
						</td>
					</tr>
				</table>
			</div>
			<div Style="margin-bottom: 55px; height: 230px; padding: 20px 0 5px 10px;">
				<p>
					<asp:DataGrid id="DataGrid1" runat="server" BorderColor="#99A6C8" AutoGenerateColumns="False"
						DataKeyField="UserId" Width="98%" Font-Name="Verdana" Font-Size="8pt" BorderWidth="1px"
						BackColor="White" CellPadding="2" CellSpacing="0">
						<FooterStyle ForeColor="#002266" BackColor="#E9F0FA"></FooterStyle>
						<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
						<AlternatingItemStyle ForeColor="#000066" BackColor="#F3F7FC"></AlternatingItemStyle>
						<ItemStyle ForeColor="#000066" BackColor="White"></ItemStyle>
						<HeaderStyle Font-Bold="True" ForeColor="#002266" BackColor="#E9F0FA"></HeaderStyle>
						<Columns>
							<asp:BoundColumn DataField="DisplayName" HeaderText="[[DisplayName]]"></asp:BoundColumn>
							<asp:TemplateColumn>
								<ItemTemplate>
									<asp:LinkButton runat="server" OnInit="Button_Confirm_Init" Text="Delete Moderator" CommandName="Delete" CausesValidation="false"></asp:LinkButton>
								</ItemTemplate>
							</asp:TemplateColumn>
						</Columns>
						<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
					</asp:DataGrid>
				</p>
				<br>
				<table cellpadding="3">
					<tr>
						<td><b>User Name:</b>&nbsp;&nbsp; </td>
						<td><asp:TextBox Runat="server" ID="TextBoxUsername" /></td>
						<td><asp:Button id="ButtonAdd" runat="server" Text="Add Moderator"></asp:Button></td>
					</tr>
				</table>
				<p>
				</p>
			</div>
		</form>
	</BODY>
</html>
<script runat="server">

	private void InitializeComponent()
	{    
		this.DataGrid1.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemCreated);
		this.DataGrid1.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_DeleteCommand);
		this.ButtonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
	}

	
	override protected void OnInit(EventArgs e)
	{
		InitializeComponent();
		base.OnInit(e);
	}

	IChatLobby _lobby;
	
	protected IChatLobby Lobby
	{
		get
		{
			if(_lobby==null)
			{
				_lobby=ChatApi.GetLobby(int.Parse(Request.QueryString["LobbyId"]));
			}
			return _lobby;
		}
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);

		if(ViewState["Inited"]==null)
		{
			BindDataGrid();
			ViewState["Inited"]=true;
		}
	}

	protected void BindDataGrid()
	{
		ArrayList source=ChatWebUtility.GetManagerList(Lobby.LobbyId);
		DataGrid1.DataSource=source;
		DataGrid1.DataBind();
	}


	private void DataGrid1_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
	{
		if(e.Item.ItemIndex>-1)
			e.Item.DataBinding+=new EventHandler(Item_DataBinding);
	}

	private void Item_DataBinding(object sender, EventArgs e)
	{
		
	}
	
	private void DataGrid1_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
	{
		string userid=(string)DataGrid1.DataKeys[e.Item.ItemIndex];
		
		StringBuilder sb=new StringBuilder();
		foreach(string eachuserid in Lobby.ManagerList.Split(','))
		{
			if(eachuserid.Length==0)continue;
			if(string.Compare(userid,eachuserid,true)!=0)
			{
				if(sb.Length!=0)sb.Append(",");
				sb.Append(eachuserid);
			}
		}
		
		Lobby.ManagerList=sb.ToString();
		ChatApi.UpdateLobby(Lobby);
		BindDataGrid();
	}

	private void ButtonAdd_Click(object sender, System.EventArgs e)
	{
		string username=TextBoxUsername.Text.Trim();
		if(username.Length==0)return;
		
		string userid=ChatWebUtility.IsMemberName(username);
		if(userid==null)
			return;
	
		foreach(string eachuserid in Lobby.ManagerList.Split(','))
		{
			if(eachuserid.Length==0)continue;
			if(string.Compare(userid,eachuserid,true)==0)return;
		}
		if(Lobby.ManagerList==null||Lobby.ManagerList.Length==0)
		{
			Lobby.ManagerList=userid;
		}
		else
		{
			Lobby.ManagerList+=","+userid;
		}
		
		ChatApi.UpdateLobby(Lobby);
		
		BindDataGrid();
	}


</script>
