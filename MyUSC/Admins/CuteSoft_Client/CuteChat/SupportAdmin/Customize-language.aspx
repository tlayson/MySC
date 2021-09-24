<%@ Register TagPrefix="uc1" TagName="Menu" Src="Menu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" Inherits="CuteChat.ChatAdminPage" %>
<%@ Import Namespace="CuteChat" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	 <head id="Head1" runat="server">
		<title>Customize language settings - ASP.NET Chat Software</title>  
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
	//		HttpContext.Current.Response.Write("<script language='javascript'>alert('You have successfully updated the language settings!');</scr" + "ipt>");
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
	</head>
	<body bottommargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form runat="server" ID="Form1">
			<uc1:Banner id="banner1" runat="server" />
			<table width="840" border="0" cellpadding="3" cellspacing="0">
				<tr>
					<td id="leftcolumn" valign="top">
						<uc1:Menu id="Menu1" runat="server"></uc1:Menu>
					</td>
					<td width="10">&nbsp;</td>
					<td valign="top" align="left" id="content">
						<h1><img src="../images/setting.gif" border="0" alt="Configuration" align="absMiddle">Configurationn</h1>
						Cute Live Support auto-detects Client Browser's culture setting to decide what language to use. If developers need server side control, please set CultureType to server and input the custom culture name.
									<br><br><table cellspacing="1" cellpadding="3" border="0" class="box">
							<tr>
								<td valign=top class="boxTitle" height="30">
									Customize language settings
								</td>
							</tr>
							<tr>
								<td valign=top class="boxArea">	
									
									<table cellSpacing="0" cellPadding="5" width="550" border="0">
										<tr bgcolor="#ffffff">
											<td width="100">Culture Type</td>
											<td>
												<asp:DropDownList ConfigName="CultureType" Width="150" Runat="server" ID="Dropdownlist1">
													<asp:ListItem Value="">Default (Client)</asp:ListItem>
													<asp:ListItem Value="Client">Client</asp:ListItem>
													<asp:ListItem Value="Server">Server</asp:ListItem>
												</asp:DropDownList>
											</td>
											<td>
												Change will not take effect until you reset the application.
											</td>
										</tr>
										<tr>
											<td valign="top">Custom Culture</td>
											<td valign="top"><asp:TextBox ConfigName="CustomCulture" Runat="server" Width="150" ID="Textbox2"></asp:TextBox></td>
											<td>
												Specify the custom culture name.
												For example: 
												<table cellpadding="5" cellspacing="5" width="250">
													<tr>
														<td width="34%">
														en-us<br />
														de-DE<br />
														da<br />
														es<br />
														es-co<br />
														es-mx<br />
														es-us<br />
														</td>
														<td width="33%">
														fr<br />
														fr-be<br />
														fr-ca<br />
														he<br />
														nb-NO<br />
														no<br />
														pt<br />											
														</td>
														<td width="33%" valign="top">
														pt-BR<br />
														pt-PT<br />
														tr<br />
														zh-chs<br />
														zh-cn<br />	
														</td>
													</tr>
												</table>	
											</td>
										</tr>
									</table>
									
									<br>
									<p style="margin-left:100px">
										<asp:Button ID="ButtonUpdate" OnClick="ButtonUpdate_Click" Runat="server" Text="Update"></asp:Button>									
									</p>
								</td>
							</tr>
						</table>						
					</td>
				</tr>
			</table>
			<div id="footer">
				<p><a href="http://cutesoft.net">Copyright 2002-2008 CuteSoft.Net. All rights reserved.</a></p>
			</div>
		</form>
	</BODY>
</html>
