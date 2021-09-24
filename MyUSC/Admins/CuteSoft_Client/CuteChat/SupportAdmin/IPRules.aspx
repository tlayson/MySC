<%@ Register TagPrefix="uc1" TagName="Menu" Src="Menu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" Inherits="CuteChat.ChatAdminPage" %>
<%@ Import Namespace="CuteChat" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD id="Head1">
		<title>CuteChat IP Rules</title>
		<script runat="server">
override protected void OnLoad(EventArgs args)
{
	base.OnLoad(args);
	
	if(!IsPostBack)
	{
		BindGrid();
	}
}
void BindGrid()
{
	DataGrid1.DataSource=ChatApi.GetIPRules();
	DataGrid1.DataBind();
}
void ButtnAddNew_Click(object sender,EventArgs args)
{
	string exp=tb_new_exp.Text.Trim();
	
	try
	{
		IPChecker.TestIPList(exp);
	}
	catch(Exception x)
	{
		tb_new_exp.BackColor=System.Drawing.Color.Yellow;
		return;
	}
	
	tb_new_exp.BackColor=System.Drawing.Color.Empty;
	
	int sort=1;
	try
	{
		sort=int.Parse(sel_new_sort.SelectedValue);
	}
	catch
	{
	}
	
	ChatApi.AddIPRule(exp,sort,ddl_new_op.SelectedValue);
	
	BindGrid();
}
void DataGrid1_DeleteCommand(object sender,DataGridCommandEventArgs args)
{
	int itemid=Convert.ToInt32(DataGrid1.DataKeys[args.Item.ItemIndex]);
	ChatApi.RemoveIPRule(itemid);
	BindGrid();
}
void DataGrid1_UpdateCommand(object sender,DataGridCommandEventArgs args)
{
	UpdateItem(args.Item);
	
	//BindGrid();
}
void ButtonUpdateAll_Click(object sender,EventArgs args)
{
	foreach(DataGridItem item in DataGrid1.Items)
	{
		UpdateItem(item);
	}
	
	BindGrid();
}
void UpdateItem(DataGridItem griditem)
{
	int itemid=Convert.ToInt32(DataGrid1.DataKeys[griditem.ItemIndex]);
	IChatRule item=ChatApi.GetIPRule(itemid);
	TextBox tb=(TextBox)griditem.FindControl("TextBox1");
	string exp=tb.Text.Trim();
	
	try
	{
		IPChecker.TestIPList(exp);
	}
	catch(Exception x)
	{
		tb.BackColor=System.Drawing.Color.Yellow;
		return;
	}
	
	tb_new_exp.BackColor=System.Drawing.Color.Empty;
	
	try
	{
		item.Sort=int.Parse(((TextBox)griditem.FindControl("TextBox2")).Text);
	}
	catch
	{
	}
	
	item.Expression=exp;
	item.Mode=((DropDownList)griditem.FindControl("DropDownList1")).SelectedValue;
	ChatApi.UpdateIPRule(item);
}
		</script>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="style.css">
	</HEAD>
	<body bottommargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form runat="server" ID="Form2">
			<uc1:Banner id="Banner2" runat="server" />
			<table width="900" border="0" cellpadding="3" cellspacing="0">
				<tr>
					<td id="leftcolumn" valign="top">
						<uc1:Menu id="Menu2" runat="server"></uc1:Menu>
					</td>
					<td width="10">&nbsp;</td>
					<td valign="top" id="content">
						<h1>Deny/Allow certain IP addresses or IP ranges</h1>
						<p>In some situations, you may want to only allow people with specific IP addresses 
							to access your chat (for example, only allowing people using a particular IP 
							address to get into a chat) or you may want to ban certian IP addresses (for 
							example, keeping disruptive memembers out of your chat rooms).</p>
						<h4><b>New rule:</b></h4>
						<table cellpadding="0" cellspacing="3" border="0" width="100%">
							<tr>
								<td>
									Action:
								</td>
								<td><asp:DropDownList id="ddl_new_op" runat="server">
										<asp:ListItem Value="Deny">Deny</asp:ListItem>
										<asp:ListItem Value="Allow">Allow</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td>
									IP Expression:
								</td>
								<td>
									<asp:TextBox id="tb_new_exp" runat="server"></asp:TextBox>
								</td>
								<td>
									Priority:
								</td>
								<td>
									<asp:DropDownList id="sel_new_sort" Runat="server">
										<asp:ListItem Value="1">1</asp:ListItem>
										<asp:ListItem Value="2">2</asp:ListItem>
										<asp:ListItem Value="4">3</asp:ListItem>
										<asp:ListItem Value="4">4</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td>
									<asp:Button id="ButtnAddNew" onclick="ButtnAddNew_Click" runat="server" Text="Add New Rule"></asp:Button>
								</td>
							</tr>
						</table>
						<BR>
						<BR>
						<h4><b>Rule (applied in the order of Priority):</b></h4>
						<asp:DataGrid DataKeyField="RuleId" id="DataGrid1" OnDeleteCommand="DataGrid1_DeleteCommand" OnUpdateCommand="DataGrid1_UpdateCommand"
							Runat="server" AutoGenerateColumns="False" BorderColor="#cccccc" BorderStyle="None" Width="600"
							Font-Name="Verdana" Font-Size="8pt" BorderWidth="1px" BackColor="White" CellPadding="2" CellSpacing="0">
							<FooterStyle ForeColor="#002266" BackColor="#f5f5f5"></FooterStyle>
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#F3F7FC"></AlternatingItemStyle>
							<ItemStyle BackColor="White"></ItemStyle>
							<HeaderStyle Font-Bold="True" BackColor="#f5f5f5"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Expression">
									<ItemTemplate>
										<asp:TextBox id="TextBox1" Text='<%# DataBinder.Eval(Container.DataItem,"Expression") %>' runat="server" Width="240px">
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Priority">
									<ItemTemplate>
										<asp:TextBox id="TextBox2" Text='<%# DataBinder.Eval(Container.DataItem,"Sort") %>' runat="server" Width="48">
										</asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Action">
									<ItemTemplate>
										<asp:DropDownList id="DropDownList1" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"Mode") %>' runat="server">
											<asp:ListItem Value="Deny">Deny</asp:ListItem>
											<asp:ListItem Value="Allow">Allow</asp:ListItem>
										</asp:DropDownList>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:ButtonColumn Text="Update" ButtonType="PushButton" CommandName="Update"></asp:ButtonColumn>
								<asp:ButtonColumn Text="Delete" ButtonType="PushButton" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
						</asp:DataGrid>
						<br>
						<asp:Button id="ButtonUpdateAll" OnClick="ButtonUpdateAll_Click" runat="server" Text="Update All"></asp:Button>
						<br>
						<h4><b>IP Expression Guide</b></h4>
						<ol start="1">
							<li>
							xxx.xxx.xxx.xxx (an exact IP address).
							<li>
							Use '|' to split each subexpression.
							<li>
							Partial IP Address support. You can specify the first one, two, or three bytes 
							of an IP address. Any IP address containing those will match this rule. For 
							example, the expression '10.1' will match any address starting with '10.1.', 
							such as 10.1.1.0 and 10.1.0.1 , but not the '10.12.1.1'
							<li>
							Use '-' to specify an IP address range. For example, '222.111-222.113' will 
							match IP addresses 222.111.0.1, 222.111.0.2, and so on, to 222.111.0.254.
							<li>
							Use '!' as a prefix to exclude specified IP addresses. For example, 
							'!222.111-222.113' will exclude IP addresses 222.111.0.1, 222.111.0.2, and so 
							on, to 222.111.0.254.
							<li>
							Use '*' to include all the rest ip addresses.
							<li>
							Use '!*' to exclude all the rest ip addresses.
							<li>
								'xxx.xxx' equals 'xxx.xxx.' equals 'xxx.xxx.*'</li>
						</ol>
					</td>
				</tr>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
    </body>
</html>