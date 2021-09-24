<%@ Register TagPrefix="uc1" TagName="Banner" Src="Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Menu" Src="Menu.ascx" %>
<%@ Page language="c#" Inherits="CuteChat.ChatAdminPage" %>
<%@ Import Namespace="CuteChat"%>
<%@ Import Namespace="System.Drawing"%>
<%@ Import Namespace="System.Xml"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD id="Head1">
		<title>View/delete log</title>
		<script runat="server">
		
private void InitializeComponent()
{    
	this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
	this.DropDownLobbies.SelectedIndexChanged += new System.EventHandler(this.DropDownLobbies_SelectedIndexChanged);
	this.DropDownPageSize.SelectedIndexChanged += new System.EventHandler(this.DropDownPageSize_SelectedIndexChanged);
	this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
	this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
	this.ButtonExport.Click += new System.EventHandler(this.ButtonExport_Click);

}

override protected void OnInit(EventArgs e)
{
	InitializeComponent();
	base.OnInit(e);
}
		
void Button_Confirm_Init(object sender,EventArgs args)
{
	WebControl c=(WebControl)sender;
	string msg=c.Attributes["ConfirmMessage"];
	if(msg==null)msg="Are you sure you want to delete this?";
	c.Attributes["onclick"]="return confirm('"+msg+"');";
}


protected override void OnLoad(EventArgs e)
{
	base.OnLoad (e);

	if(!IsPostBack)
	{
	//	DropDownLobbies.Items.Add("All Lobbies");
		DropDownLobbies.Items.Add("Live Support");
	//	DropDownLobbies.Items.Add("All Operators");
	//	DropDownLobbies.Items.Add("All Privates");
	//	DropDownLobbies.Items.Add("Messenger");
	///	foreach(IChatLobby lobby in ChatApi.GetLobbyArray())
	//	{
	//		DropDownLobbies.Items.Add(new ListItem(lobby.Title,lobby.LobbyId.ToString()));
	//	}

		BindDataGrid();
	}
}

void BindDataGrid()
{
	string placename=null;

	string location;
	ChatMsgData[] msgs;

	location="Support";
		
	int totalCount;
	msgs=ChatApi.GetMessages(location,placename,DataGrid1.PageSize,DataGrid1.CurrentPageIndex,out totalCount);
	DataGrid1.VirtualItemCount=totalCount;
	DataGrid1.DataSource=msgs;
	DataGrid1.DataBind();
}

public string SubStr(string str,int len)
{
	if(str==null)return "";
	if(str.Length<len)return str;
	return str.Substring(0,len)+"...";
}
public string GetChannelHtml(ChatMsgData msg)
{
	if(msg.Location=="Lobby")
	{
		IChatLobby lobby=ChatApi.GetLobby(int.Parse(msg.PlaceName.Remove(0,"Lobby:".Length)));
		if(lobby==null)
			return "(Deleted)";
		return lobby.Title;
	}
	else
	{
		return msg.Location;
	}
}


private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
{
	if(e.Item.ItemIndex==-1)
		return;

	ChatMsgData msg=(ChatMsgData)e.Item.DataItem;

	e.Item.ToolTip=msg.Html;
}

private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
{
	DataGrid1.CurrentPageIndex=e.NewPageIndex;
	BindDataGrid();
}

private void DropDownLobbies_SelectedIndexChanged(object sender, System.EventArgs e)
{
	DataGrid1.CurrentPageIndex=0;
	BindDataGrid();
}

private void ButtonDelete_Click(object sender, System.EventArgs e)
{
	int day;
	try
	{
		day=int.Parse(TextBoxDay.Text);
	}
	catch(Exception x)
	{
		TextBoxDay.BackColor=Color.Yellow;
		TextBoxDay.ToolTip=x.Message;
		return;
	}

	TextBoxDay.BackColor=Color.Empty;
	TextBoxDay.ToolTip="";

	ChatApi.DeleteMessagesBeforeDays(day);

	BindDataGrid();
}

private void DropDownPageSize_SelectedIndexChanged(object sender, System.EventArgs e)
{
	DataGrid1.CurrentPageIndex=0;
	DataGrid1.PageSize=int.Parse(DropDownPageSize.SelectedValue);
	BindDataGrid();
}

private void ButtonExport_Click(object sender, System.EventArgs e)
{
	
	int pagesize=10000000;
	int pageindex=0;
	if(!CheckBoxAllPages.Checked)
	{
		pagesize=DataGrid1.PageSize;
		pageindex=DataGrid1.CurrentPageIndex;
	}
	
	string placename=null;

	string location;
	ChatMsgData[] msgs;

	location="Support";

	int totalCount;
	msgs=ChatApi.GetMessages(location,placename,pagesize,pageindex,out totalCount);
	
	Response.AddHeader("Content-Disposition","attachment;filename=export.xml");
	Response.ContentType="text/xml";
	Response.ContentEncoding=System.Text.Encoding.UTF8;
	XmlTextWriter xtw=new XmlTextWriter(Response.Output);
	
	xtw.Indentation=1;
	xtw.IndentChar='\t';
	xtw.Formatting=Formatting.Indented;

	xtw.WriteStartDocument();
	xtw.WriteStartElement("Export");

	ChatMsgData defmsg=new ChatMsgData();

	foreach(ChatMsgData msg in msgs)
	{
		xtw.WriteStartElement("ChatMsgData");
		
		xtw.WriteAttributeString("time",msg.Time.ToString("yyyy-MM-dd HH:mm:ss"));
		xtw.WriteAttributeString("text",msg.Text);
		xtw.WriteAttributeString("html",msg.Html);
		xtw.WriteAttributeString("sender",msg.Sender);
		xtw.WriteAttributeString("target",msg.Target);
		
		xtw.WriteEndElement();
	}
	xtw.WriteEndElement();//chatlog
	Response.End();
}
		</script>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="style.css">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form runat="server" id="Form1">
			<uc1:Banner id="banner1" runat="server"></uc1:Banner>
			<table border="0" cellspacing="0" cellpadding="5" width="100%">
				<tr>
					<td id="leftcolumn" valign="top">
						<uc1:Menu id="Menu1" runat="server"></uc1:Menu>
					</td>
					<td valign="top" align="left" id="content">
						<h1>View/delete/Export Chat log</h1>
						<table cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td style="BORDER-RIGHT:#a5b6de 1px solid; PADDING-RIGHT:8px; PADDING-LEFT:8px; BORDER-TOP:#a5b6de 1px solid; PADDING-BOTTOM:8px; BORDER-LEFT:#a5b6de 1px solid; PADDING-TOP:8px; BORDER-BOTTOM:#a5b6de 1px solid">
									<asp:DropDownList Runat="server" ID="DropDownLobbies" AutoPostBack="True"></asp:DropDownList>
									Page Size:
									<asp:DropDownList id="DropDownPageSize" runat="server" AutoPostBack="True">
										<asp:ListItem Value="20">20</asp:ListItem>
										<asp:ListItem Value="50">50</asp:ListItem>
										<asp:ListItem Value="100">100</asp:ListItem>
									</asp:DropDownList>&nbsp;
									<asp:Button id="ButtonExport" runat="server" Text="Export" Width="72px"></asp:Button>
									&nbsp;(<asp:CheckBox id="CheckBoxAllPages" runat="server" Text="All"></asp:CheckBox>)
								</td>
								<td width="10">&nbsp;
								</td>
								<td style="BORDER-RIGHT:#a5b6de 1px solid; PADDING-RIGHT:8px; PADDING-LEFT:8px; BORDER-TOP:#a5b6de 1px solid; PADDING-BOTTOM:8px; BORDER-LEFT:#a5b6de 1px solid; PADDING-TOP:8px; BORDER-BOTTOM:#a5b6de 1px solid">
									<table border="0" cellspacing="0" cellpadding="2">
										<tr>
											<td nowrap>
											Delete messages older than
											<td>
												<asp:TextBox Runat="server" Width="30" id="TextBoxDay" Text="30"></asp:TextBox>
											</td>
											<td nowrap>
												day(s)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
											<td>
												<asp:Button OnInit="Button_Confirm_Init" id="ButtonDelete" runat="server" Text="Delete"></asp:Button>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<p>
							<asp:DataGrid id="DataGrid1" runat="server" AutoGenerateColumns="False" DataKeyField="MessageId"
								BorderColor="#dddddd" BorderStyle="solid" BorderWidth="1px" BackColor="White" CellPadding="1"
								Font-Names="Verdana" AllowCustomPaging="True" AllowPaging="True" width="99%" PageSize="20">
								<SelectedItemStyle Font-Bold="True" ForeColor="White"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#f7f7f7"></AlternatingItemStyle>
								<ItemStyle BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#002266" BackColor="#f5f5f5"></HeaderStyle>
								<FooterStyle ForeColor="#002266" BackColor="#f5f5f5"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="Time" ItemStyle-Wrap="False" ItemStyle-Width="100" HeaderText="Time"
										DataFormatString="{0:MM-dd HH:mm:ss}"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Sender" ItemStyle-Wrap="False" ItemStyle-Width="80">
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# Server.HtmlEncode( DataBinder.Eval(Container, "DataItem.Sender",null) ) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Target" ItemStyle-Wrap="False" ItemStyle-Width="80">
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# Server.HtmlEncode( DataBinder.Eval(Container, "DataItem.Target",null) ) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Text">
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# Server.HtmlEncode( DataBinder.Eval(Container, "DataItem.Text",null) ) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="HTML">
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# Server.HtmlEncode( SubStr( DataBinder.Eval(Container, "DataItem.Html",null) , 20 ) ) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Whisper" ItemStyle-Wrap="False" ItemStyle-Width="80">
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.Whisper"))?"Yes":"" %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Location" ItemStyle-Wrap="False" ItemStyle-Width="80">
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# GetChannelHtml( (ChatMsgData)Container.DataItem ) %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
							</asp:DataGrid>
						</p>
						<br>
						<br>
					</td>
				</tr>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</HTML>
