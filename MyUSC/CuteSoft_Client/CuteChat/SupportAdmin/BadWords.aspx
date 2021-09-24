<%@ Import Namespace="CuteChat" %>
<%@ Page Language="C#" Inherits="CuteChat.ChatAdminPage" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Menu" Src="Menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD id="Head1">
		<title>Bad Word Filter</title>
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
	DataGrid1.DataSource=ChatApi.GetBadWords();
	DataGrid1.DataBind();
}

void ButtnAddNew_Click(object sender,EventArgs args)
{
	string word=tb_new_badword.Text.Trim();
	if(word.Length==0)return;
	
	ChatApi.AddBadWord(word,ddl_new_type.SelectedValue);
	
	BindGrid();
}
void DataGrid1_DeleteCommand(object sender,DataGridCommandEventArgs args)
{
	int itemid=Convert.ToInt32(DataGrid1.DataKeys[args.Item.ItemIndex]);
	ChatApi.RemoveBadWord(itemid);
	BindGrid();
}
void DataGrid1_UpdateCommand(object sender,DataGridCommandEventArgs args)
{
	UpdateItem(args.Item);
	
	BindGrid();
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
	IChatRule item=ChatApi.GetBadWord(itemid);
	item.Expression=((TextBox)griditem.FindControl("TextBox1")).Text.Trim();
	item.Mode=((DropDownList)griditem.FindControl("DropDownList1")).SelectedValue;
	ChatApi.UpdateBadWord(item);
}
		</script>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="style.css">
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
						<h1>Bad word filter</h1>
						<div align="left" style="PADDING-RIGHT:5px;PADDING-LEFT:5px;PADDING-BOTTOM:5px;PADDING-TOP:0px">
							Bad Word:
							<asp:TextBox id="tb_new_badword" runat="server"></asp:TextBox>
							Type :
							<asp:DropDownList id="ddl_new_type" runat="server">
								<asp:ListItem Value="Wide">Wildcard</asp:ListItem>
								<asp:ListItem Value="Word">Word</asp:ListItem>
								<asp:ListItem Value="RegExp">RegExp</asp:ListItem>
								<asp:ListItem Value="Contains">Contains</asp:ListItem>
							</asp:DropDownList>
							<asp:Button id="ButtnAddNew" OnClick="ButtnAddNew_Click" runat="server" Text="Add New"></asp:Button>
							<br>
							<br>
							<asp:Button id="ButtonUpdateAll" OnClick="ButtonUpdateAll_Click" runat="server" Text="Update All"></asp:Button><BR>
							<br>
							Bad Word List:
							<br>
							<br>
							<asp:DataGrid DataKeyField="RuleId" OnDeleteCommand="DataGrid1_DeleteCommand" OnUpdateCommand="DataGrid1_UpdateCommand"
								Runat="server" id="DataGrid1" AutoGenerateColumns="False" BorderColor="#A5B6DE" BorderStyle="None"
								Width="500" Font-Name="Verdana" Font-Size="8pt" BorderWidth="1px" BackColor="White" CellPadding="2"
								CellSpacing="0">
								<FooterStyle ForeColor="#002266" BackColor="#f5f5f5"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
								<AlternatingItemStyle ForeColor="#000066" BackColor="#F3F7FC"></AlternatingItemStyle>
								<ItemStyle ForeColor="#000066" BackColor="White"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="#002266" BackColor="#f5f5f5"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Bad Word List">
										<ItemTemplate>
											<asp:TextBox id="TextBox1" Text='<%# DataBinder.Eval(Container.DataItem,"Expression") %>' runat="server" Width="240px">
											</asp:TextBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Type">
										<ItemTemplate>
											<asp:DropDownList id="DropDownList1" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"Mode") %>' runat="server">
												<asp:ListItem Value="Word">Word</asp:ListItem>
												<asp:ListItem Value="Wide">Wide</asp:ListItem>
												<asp:ListItem Value="RegExp">RegExp</asp:ListItem>
												<asp:ListItem Value="Contains">Contains</asp:ListItem>
											</asp:DropDownList>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:ButtonColumn Text="Update" ButtonType="PushButton" CommandName="Update"></asp:ButtonColumn>
									<asp:ButtonColumn Text="Delete" ButtonType="PushButton" CommandName="Delete"></asp:ButtonColumn>
								</Columns>
							</asp:DataGrid>
							<br>
							<br>
							<div align="left" style="PADDING-RIGHT:5px; PADDING-LEFT:5px; PADDING-BOTTOM:0px; WIDTH:500px; PADDING-TOP:0px">
								<font size="3"><b>Notes</b></font>
								<hr size="1">
								<b>Wildcard</b><br>
								The asterisk "*" is the Cute Chat wildcard character: it matches any single 
								character. For example, <nobr>'k-i-l-l you'</nobr> will match <nobr>'*k*i*l*l*'</nobr>.
								<br>
								<br>
								<b>Word</b><br>
								Performs a basic word search, not strings. For example , 'your skill' will not 
								match 'kill'.
								<br>
								<br>
								<b>RegExp</b><br>
								Regular Expression. If you do not know how to use regular expressions, you 
								probably just won't want to use this type.
								<br>
								<br>
								<b>Contains</b><br>
								Returns any item containing the letters you entered. For example, 'your skill' 
								will match 'kill'.
							</div>
						</div>
					</td>
				</tr>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</HTML>
