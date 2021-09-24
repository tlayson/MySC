<%@ Register TagPrefix="uc1" TagName="Menu" Src="Menu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" Inherits="CuteChat.ChatAdminPage" %>
<%@ Import Namespace="CuteChat" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server">
		<title>CuteChat Configuration</title>
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
		<link rel="stylesheet" href="style.css" />
	</head>
	<body bottommargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form runat="server" ID="Form1">
			<uc1:Banner id="banner1" runat="server" />			
			<table width="100%" border="0" cellpadding="3" cellspacing="0">
				<tr>
					<td id="leftcolumn" valign="top">
						<uc1:Menu id="Menu1" runat="server"></uc1:Menu>
					</td>
					<td width="10">&nbsp;</td>
					<td valign="top" align="left" id="content">
						<h1><img src="../images/setting.gif" border="0" alt="Configuration" align="absMiddle">Configurationn</h1>
						<table cellspacing="1" cellpadding="3" border="0" class="box">
							<tr>
								<td valign=top class="boxTitle" height="30">
									File Upload Control
								</td>
							</tr>
							<tr>
								<td valign=top class="boxArea">	
									<table cellSpacing="0" cellPadding="2" width="700" border="0">
										<tr bgcolor="#f5f5f5">
											<td>Allow send file (Chat Room)</td>
											<td>
												<asp:DropDownList ConfigName="GlobalAllowSendFile" Runat="server" Width="150" ID="Dropdownlist3">
													<asp:ListItem Value="">Default (True)</asp:ListItem>
													<asp:ListItem Value="True">True</asp:ListItem>
													<asp:ListItem Value="False">False</asp:ListItem>
												</asp:DropDownList>
											</td>
											<td>
											</td>
										</tr>
										<tr bgcolor="#ffffff">
											<td valign="top">File Upload Filters (Chat Room)</td>
											<td valign="top"><asp:TextBox ConfigName="GlobalSendFileType" Runat="server" Width="150" ID="Textbox6"></asp:TextBox></td>
											<td style="word-break:break-all;word-wrap:break-word;font:normal 11px Tahoma;">
												"<%=ChatConsts.DefaultGlobalSendFileType%>"
											</td>
										</tr>
										<tr bgcolor="#f5f5f5">
											<td nowrap>Max File Upload Size (Chat Room)</td>
											<td><asp:TextBox ConfigName="GlobalSendFileSize" Runat="server" Width="150" ID="Textbox7"></asp:TextBox></td>
											<td>
												Defalult:
												<%=ChatConsts.DefaultGlobalSendFileSize%>
												byte
											</td>
										</tr>
										<tr bgcolor="#ffffff">
											<td>Allow send file (Messenger)</td>
											<td>
												<asp:DropDownList ConfigName="MessengerAllowSendFile" Runat="server" Width="150" ID="Dropdownlist7">
													<asp:ListItem Value="">Default (True)</asp:ListItem>
													<asp:ListItem Value="True">True</asp:ListItem>
													<asp:ListItem Value="False">False</asp:ListItem>
												</asp:DropDownList>
											</td>
											<td>
											</td>
										</tr>
										<tr bgcolor="#ffffff">
											<td valign="top">File Upload Filters (Messenger)</td>
											<td valign="top"><asp:TextBox ConfigName="MessengerSendFileType" Runat="server" Width="150" ID="Textbox2"></asp:TextBox></td>
											<td style="word-break:break-all;word-wrap:break-word;font:normal 11px Tahoma;">
												"<%=ChatConsts.DefaultMessengerSendFileType%>"
											</td>
										</tr>
										<tr bgcolor="#f5f5f5">
											<td nowrap>Max File Upload Size (Messenger)</td>
											<td><asp:TextBox ConfigName="MessengerSendFileSize" Runat="server" Width="150" ID="Textbox10"></asp:TextBox></td>
											<td>
												Defalult:
												<%=ChatConsts.DefaultMessengerSendFileSize%>
												byte
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>					
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
</html>
