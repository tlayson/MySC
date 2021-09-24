<%@ Import Namespace="CuteChat" %>
<%@ Page Language="C#" Inherits="CuteChat.ChatAdminPage" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Menu" Src="Menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD id="Head1">
		<title>Functionalities</title>
		<script runat="server">
			string GlobalAnnouncement;
			string NetworkErrorTimeout;
			string FloodControl;
			string MaxLengthperWord;
			string MaxLengthperMessage;
			string FlashChatServer;
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
					FlashChatServer="for example : /ChatAppName or rtmp://localhost/ChatAppName , the /ChatAppName must be a app folder of the FMS";
					GlobalAnnouncement="The Global Announcement appears when each user joins every chat room.";
					NetworkErrorTimeout="Default: 60 seconds. The user connection will be automatically cut off after a timeout of network problem.";
					FloodControl="Set how many messages in how many seconds constitutes a flood. Defalult: 5 messages / 10 seconds.";
					MaxLengthperWord="Set the maximum length of a single word.<br>Defalult:"+ChatConsts.DefaultMaxWordLength;
					MaxLengthperMessage="Set the maximum length of a single message.<br>Defalult:"+ChatConsts.DefaultMaxMSGLength;
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
		<link rel="stylesheet" href="style.css">
			<style>
			#ToolTip { BORDER-RIGHT: #000 1px solid; PADDING-RIGHT: 4px; BORDER-TOP: #000 1px solid; PADDING-LEFT: 4px; FONT-SIZE: 11px; Z-INDEX: 10000; LEFT: 0px; VISIBILITY: hidden; PADDING-BOTTOM: 4px; BORDER-LEFT: #000 1px solid; WIDTH: 200px; COLOR: #000; LINE-HEIGHT: 1.3; PADDING-TOP: 4px; BORDER-BOTTOM: #000 1px solid; FONT-FAMILY: verdana; POSITION: absolute; TOP: 0px; BACKGROUND-COLOR: lightyellow }
			</style>
			<script language="javascript">
			function showToolTip(e,text){
				var ToolTip=document.getElementById("ToolTip");
				ToolTip.innerHTML=text;
				ToolTip.style.pixelLeft=(e.x+20+document.body.scrollLeft);
				ToolTip.style.pixelTop=(e.y+document.body.scrollTop);
				ToolTip.style.visibility="visible";
			}
			function hideToolTip(){
				var ToolTip=document.getElementById("ToolTip");
				ToolTip.style.visibility="hidden";
			}
			</script>
	</HEAD>
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
						<h1><img src="../images/setting.gif" border="0" alt="Configuration" align="absMiddle">Configuration</h1>
						<table cellspacing="1" cellpadding="3" border="0" class="box">
							<tr>
								<td valign="top" class="boxTitle" height="30">
									Functionalities
								</td>
							</tr>
							<tr>
								<td valign="top" class="boxArea">
									<table cellSpacing="0" cellPadding="2" width="600" border="0">
										<tr bgcolor="#f5f5f5">
											<td nowrap width="200">Show Video Chat Button</td>
											<td nowrap width="200"><asp:DropDownList ConfigName="ShowVideoButton" Runat="server" Width="200" ID="Dropdownlist3">
													<asp:ListItem Value="">Default (True)</asp:ListItem>
													<asp:ListItem Value="True">True</asp:ListItem>
													<asp:ListItem Value="False">False</asp:ListItem>
												</asp:DropDownList></td>
											<td valign="top">&nbsp;</td>
										</tr>
										<tr bgcolor="#ffffff">
											<td>Flash Vedio Chat Server</td>
											<td><asp:TextBox ConfigName="FlashChatServer" Runat="server" Width="200" ID="Textbox2"></asp:TextBox></td>
											<td valign="top" onmouseover='javascript:showToolTip(event,"<%=FlashChatServer%>")' onmouseout='javascript:hideToolTip()'><img src="../images/help.png">
											</td>
										</tr>
										<tr bgcolor="#f5f5f5">
											<td nowrap width="200">Global Announcement</td>
											<td nowrap width="200"><asp:TextBox TextMode="MultiLine" ConfigName="Announcement" Runat="server" Width="200" ID="Textbox1"></asp:TextBox></td>
											<td valign="top" onmouseover='javascript:showToolTip(event,"<%=GlobalAnnouncement%>")' onmouseout='javascript:hideToolTip()'><img src="../images/help.png">
											</td>
										</tr>
										<tr bgcolor="#ffffff">
											<td nowrap>Network Error Timeout</td>
											<td nowrap><asp:TextBox ConfigName="ChannelOnlineExpires" Runat="server" Width="200" ID="Textbox3" NAME="Textbox1"></asp:TextBox></td>
											<td valign="top" onmouseover='javascript:showToolTip(event,"<%=NetworkErrorTimeout%>")' onmouseout='javascript:hideToolTip()'><img src="../images/help.png">
											</td>
										</tr>
										<tr bgcolor="#f5f5f5">
											<td nowrap>Flood Control</td>
											<td nowrap><asp:TextBox ConfigName="FloodControlCount" Runat="server" Width="50" ID="Textbox4"></asp:TextBox>&nbsp;/&nbsp;<asp:TextBox ConfigName="FloodControlDelay" Runat="server" Width="50" ID="Textbox5"></asp:TextBox></td>
											<td valign="top" onmouseover='javascript:showToolTip(event,"<%=FloodControl%>")' onmouseout='javascript:hideToolTip()'><img src="../images/help.png">
											</td>
										</tr>
										<tr bgcolor="#ffffff">
											<td>Disable Whisper feature</td>
											<td><asp:DropDownList ConfigName="DisableWhisper" Runat="server" Width="200" ID="Dropdownlist1">
													<asp:ListItem Value="">Default (False)</asp:ListItem>
													<asp:ListItem Value="True">True</asp:ListItem>
													<asp:ListItem Value="False">False</asp:ListItem>
												</asp:DropDownList></td>
											<td></td>
										<tr bgcolor="#f5f5f5">
											<td>Disable Whisper in private chat</td>
											<td><asp:DropDownList ConfigName="DisablePrivateChatWhisper" Runat="server" Width="200" ID="Dropdownlist14">
													<asp:ListItem Value="">Default (True)</asp:ListItem>
													<asp:ListItem Value="True">True</asp:ListItem>
													<asp:ListItem Value="False">False</asp:ListItem>
												</asp:DropDownList></td>
											<td></td>
										<tr bgcolor="#ffffff">
											<td>Max length of a single word</td>
											<td><asp:TextBox ConfigName="MaxWordLength" Runat="server" Width="200" ID="Textbox8"></asp:TextBox></td>
											<td valign="top" onmouseover='javascript:showToolTip(event,"<%=MaxLengthperWord%>")' onmouseout='javascript:hideToolTip()'><img src="../images/help.png">
											</td>
										</tr>
										<tr bgcolor="#f5f5f5">
											<td>Max length of a message
											</td>
											<td><asp:TextBox ConfigName="MaxMSGLength" Runat="server" Width="200" ID="Textbox9"></asp:TextBox></td>
											<td valign="top" onmouseover='javascript:showToolTip(event,"<%=MaxLengthperMessage%>")' onmouseout='javascript:hideToolTip()'><img src="../images/help.png">
											</td>
										</tr>
										<tr bgcolor="#ffffff">
											<td>Allow Html box</td>
											<td>
												<asp:DropDownList ConfigName="GlobalEnableHtmlBox" Runat="server" Width="200" ID="Dropdownlist2">
													<asp:ListItem Value="">Default (True)</asp:ListItem>
													<asp:ListItem Value="True">True</asp:ListItem>
													<asp:ListItem Value="False">False</asp:ListItem>
												</asp:DropDownList>
											</td>
											<td>
											</td>
										</tr>
										<tr bgcolor="#f5f5f5">
											<td>Allow Outsite Image</td>
											<td>
												<asp:DropDownList ConfigName="AllowOutsiteImage" Runat="server" Width="200" ID="Dropdownlist9">
													<asp:ListItem Value="">Default (True)</asp:ListItem>
													<asp:ListItem Value="True">True</asp:ListItem>
													<asp:ListItem Value="False">False</asp:ListItem>
												</asp:DropDownList>
											</td>
											<td>
											</td>
										</tr>
										<tr bgcolor="#ffffff">
											<td>Allow change name</td>
											<td><asp:DropDownList ConfigName="AllowChangeName" Runat="server" Width="200" ID="Dropdownlist10">
													<asp:ListItem Value="">Default (True)</asp:ListItem>
													<asp:ListItem Value="True">True</asp:ListItem>
													<asp:ListItem Value="False">False</asp:ListItem>
												</asp:DropDownList></td>
											<td>
											</td>
										</tr>
										<tr bgcolor="#f5f5f5">
											<td>Auto-generate username for anonymous users</td>
											<td><asp:DropDownList ConfigName="AutoGenerateAnonymousName" Runat="server" Width="200" ID="Dropdownlist4">
													<asp:ListItem Value="">Default (True)</asp:ListItem>
													<asp:ListItem Value="True">True</asp:ListItem>
													<asp:ListItem Value="False">False</asp:ListItem>
												</asp:DropDownList></td>
											<td>
											</td>
										</tr>
										<tr bgcolor="#f5f5f5">
											<td>Allow anonymous</td>
											<td><asp:DropDownList ConfigName="GlobalAllowAnonymous" Runat="server" Width="200" ID="Dropdownlist7">
													<asp:ListItem Value="">Default (True)</asp:ListItem>
													<asp:ListItem Value="True">True</asp:ListItem>
													<asp:ListItem Value="False">False</asp:ListItem>
												</asp:DropDownList></td>
											<td>
											</td>
										</tr>
										<tr bgcolor="#ffffff">
											<td nowrap>Allow whisper</td>
											<td><asp:DropDownList ConfigName="GlobalAllowWhisper" Runat="server" Width="200" ID="Dropdownlist5">
													<asp:ListItem Value="">Default (True)</asp:ListItem>
													<asp:ListItem Value="True">True</asp:ListItem>
													<asp:ListItem Value="False">False</asp:ListItem>
												</asp:DropDownList></td>
											<td>
											</td>
										</tr>
										<tr bgcolor="#f5f5f5">
											<td nowrap>Allow private chat</td>
											<td><asp:DropDownList ConfigName="GlobalAllowPrivateMessage" Runat="server" Width="200" ID="Dropdownlist6"
													NAME="Dropdownlist1">
													<asp:ListItem Value="">Default (True)</asp:ListItem>
													<asp:ListItem Value="True">True</asp:ListItem>
													<asp:ListItem Value="False">False</asp:ListItem>
												</asp:DropDownList></td>
											<td>
											</td>
										</tr>
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
										<tr bgcolor="#f5f5f5">
											<td nowrap>Show Join/Leave system message</td>
											<td><asp:DropDownList ConfigName="ShowJoinLeaveMessage" Runat="server" Width="200" ID="Dropdownlist13"
													NAME="Dropdownlist1">
													<asp:ListItem Value="">Default (True)</asp:ListItem>
													<asp:ListItem Value="True">True</asp:ListItem>
													<asp:ListItem Value="False">False</asp:ListItem>
												</asp:DropDownList></td>
											<td>
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
			<div id="ToolTip"></div>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</body>
</HTML>
