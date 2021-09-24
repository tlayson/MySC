<%@ Import Namespace="CuteChat" %>
<%@ Page Language="C#" Inherits="CuteChat.ChatAdminPage" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Menu" Src="Menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD id="Head1">
		<title>Live Support Configurationn</title>
		<script runat="server">
			override protected void OnLoad(EventArgs args)
			{
				base.OnLoad(args);
				
				if(!IsPostBack)
				{
					Control[] ctrls=GetControls();
					foreach(Control ctrl in ctrls)
					{
						if(ctrl is TextBox)
						{
							TextBox tb=(TextBox)ctrl;
							string configname=tb.Attributes["ConfigName"];
							tb.Text=ChatApi.GetConfig(configname);
						}
						if(ctrl is DropDownList)
						{
							DropDownList ddl=(DropDownList)ctrl;
							string configname=ddl.Attributes["ConfigName"];
							ddl.SelectedValue=ChatApi.GetConfig(configname);
						}
					}
				}
			}
			Control[] GetControls()
			{
				ArrayList al=new ArrayList();
				foreach(Control ctrl in Form1.Controls)
				{
					if(ctrl is WebControl)
					{
						if(((WebControl)ctrl).Attributes["ConfigName"]!=null)
							al.Add(ctrl);
					}
				}
				return (Control[])al.ToArray(typeof(Control));
			}

		private void ButtonUpdate_Click(object sender,EventArgs args)
		{
			Control[] ctrls=GetControls();
			foreach(Control ctrl in ctrls)
			{
				if(ctrl is TextBox)
				{
					TextBox tb=(TextBox)ctrl;
					string configname=tb.Attributes["ConfigName"];
					string val=tb.Text.Trim();
					if(val=="")val=null;
					ChatApi.SetConfig(configname,val);
		
				}
				if(ctrl is DropDownList)
				{
					DropDownList ddl=(DropDownList)ctrl;
					string configname=ddl.Attributes["ConfigName"];
					string val=ddl.SelectedValue;
					if(val=="")val=null;
					ChatApi.SetConfig(configname,val);
				}
			}
		}
		</script>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="style.css">
	</HEAD>
	<BODY>
		<form runat="server" id="Form1">
			<uc1:Banner id="banner1" runat="server"></uc1:Banner>
			<table border="0" cellspacing="0" cellpadding="0" width="100%">
				<tr>
					<td id="leftcolumn" valign="top" nowrap>
						<uc1:Menu id="Menu1" runat="server"></uc1:Menu>
					</td>
					<td width="15">&nbsp;</td>
					<td valign="top" align="left" id="content">
						<h1>Live Support Configuration</h1>
						<TABLE class="normal" cellSpacing="0" cellPadding="2" width="850" border="1" bordercolor="#a5b6de"
							style="BORDER-BOTTOM:#a5b6de 1px solid;BORDER-LEFT:#a5b6de 1px solid;BORDER-COLLAPSE:collapse;BORDER-TOP:#a5b6de 1px solid;BORDER-RIGHT:#a5b6de 1px solid">
							<TR bgcolor="#ffffff">
								<td>Greeting message of client window</td>
								<td><asp:TextBox ConfigName="SupportRoomTitle" Runat="server" Width="200" ID="Textbox16"></asp:TextBox></td>
								<td>
									Welcome to yoursite.com
								</td>
							</TR>
							<TR bgcolor="#f5f5f5">
								<td>Image in waiting page</td>
								<td><asp:TextBox ConfigName="SupportWaitImage" Runat="server" Width="200" ID="Textbox17"></asp:TextBox></td>
								<td>
									images/yoursite.gif
								</td>
							</TR>
							<TR bgcolor="#ffffff">
								<td>Image in feedback page</td>
								<td><asp:TextBox ConfigName="SupportFeedbackImage" Runat="server" Width="200" ID="Textbox18"></asp:TextBox></td>
								<td>
									images/yoursite2.gif
								</td>
							</TR>
							<TR bgcolor="#f5f5f5">
								<td>Require email in login page</td>
								<td>
									<asp:DropDownList ConfigName="SupportRequireMail" Runat="server" Width="200" ID="Dropdownlist11">
										<asp:ListItem Value="">Default (False)</asp:ListItem>
										<asp:ListItem Value="True">True</asp:ListItem>
										<asp:ListItem Value="False">False</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td>
								</td>
							</TR>
							<tr bgcolor="#ffffff">
								<td nowrap>Enable sound by default</td>
								<td><asp:DropDownList ConfigName="DefaultEnableSound" Runat="server" Width="200" ID="Dropdownlist12" NAME="Dropdownlist1">
										<asp:ListItem Value="">Default (True)</asp:ListItem>
										<asp:ListItem Value="True">True</asp:ListItem>
										<asp:ListItem Value="False">False</asp:ListItem>
									</asp:DropDownList></td>
								<td>
								</td>
							</tr>
							<TR bgcolor="#f5f5f5">
								<td nowrap width="200">Flood Control</td>
								<td nowrap width="200"><asp:TextBox ConfigName="FloodControlCount" Runat="server" Width="50" ID="Textbox4"></asp:TextBox>&nbsp;/&nbsp;<asp:TextBox ConfigName="FloodControlDelay" Runat="server" Width="50" ID="Textbox5"></asp:TextBox></td>
								<td width="450">Set how many messages in how many seconds constitutes a flood.
									<br>
									Defalult: 5 messages / 10 seconds.
								</td>
							</TR>
							<TR bgcolor="#ffffff">
								<td>Max length of a single word</td>
								<td><asp:TextBox ConfigName="MaxWordLength" Runat="server" Width="200" ID="Textbox8"></asp:TextBox></td>
								<td>Set th maximum length of a single word.<br>
									Defalult:
									<%=ChatConsts.DefaultMaxWordLength%>
								</td>
							</TR>
							<TR bgcolor="#f5f5f5">
								<td>Max length of a message
								</td>
								<td><asp:TextBox ConfigName="MaxMSGLength" Runat="server" Width="200" ID="Textbox9"></asp:TextBox></td>
								<td>Set th maximum length of a message.<br>
									Defalult:
									<%=ChatConsts.DefaultMaxMSGLength%>
								</td>
							</TR>
							<TR bgcolor="#ffffff">
								<TD>Custom field name</TD>
								<TD><asp:TextBox ConfigName="LS_CustomDataName" Runat="server" Width="200" ID="Textbox1"></asp:TextBox></TD>
								<TD>You can define your own custom field here. Cute Live support will display this custom field under Email field.</TD>
							</TR>
							<TR bgcolor="#f5f5f5">
								<td>Live Support Allow send file</td>
								<td>
									<asp:DropDownList ConfigName="SupportAllowSendFile" Runat="server" Width="200" ID="Dropdownlist8">
										<asp:ListItem Value="">Default (True)</asp:ListItem>
										<asp:ListItem Value="True">True</asp:ListItem>
										<asp:ListItem Value="False">False</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td>
								</td>
							</TR>
							<TR bgcolor="#ffffff">
								<td>Live Support File Upload Filters</td>
								<td><asp:TextBox ConfigName="SupportSendFileType" Runat="server" Width="200" ID="Textbox14"></asp:TextBox></td>
								<td style="WORD-WRAP:break-word;WORD-BREAK:break-all">
									Restricting Upload Files by Extensions and Types
									<br>
									Defalult: "<%=ChatConsts.DefaultSupportSendFileType%>"
								</td>
							</TR>
							<TR bgcolor="#f5f5f5">
								<td nowrap>Live Support Max File Upload Size</td>
								<td><asp:TextBox ConfigName="SupportSendFileSize" Runat="server" Width="200" ID="Textbox15"></asp:TextBox></td>
								<td>
									Defalult:
									<%=ChatConsts.DefaultSupportSendFileSize%>
									byte
								</td>
							</TR>
							<!--- LS_ShowAllCustomers Need to rename to LS_ShowUsersByDepartment--->
							<TR bgcolor="#ffffff">
								<td>Show users in queue by department</td>
								<td>
									<asp:DropDownList ConfigName="LS_ShowAllCustomers" Runat="server" Width="200" ID="Dropdownlist1">
										<asp:ListItem Value="">Default (False)</asp:ListItem>
										<asp:ListItem Value="True">False</asp:ListItem>
										<asp:ListItem Value="False">True</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td>
									If true, the operator would only see a list of users that requested a specific 
									department.
								</td>
							</TR>
							<TR bgcolor="#ffffff">
								<td>Enable long time cookies</td>
								<td>
									<asp:DropDownList ConfigName="EnableLongTimeCookies" Runat="server" Width="200" ID="Dropdownlist2">
										<asp:ListItem Value="">Default (True)</asp:ListItem>
										<asp:ListItem Value="True">True</asp:ListItem>
										<asp:ListItem Value="False">False</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td>
									If false, the livesupport would not save cookie on user's browsers.
								</td>
							</TR>
						</TABLE>
						<br>
						<asp:Button ID="ButtonUpdate" OnClick="ButtonUpdate_Click" Runat="server" Text="Update"></asp:Button>
					</td>
				</tr>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</BODY>
</HTML>
