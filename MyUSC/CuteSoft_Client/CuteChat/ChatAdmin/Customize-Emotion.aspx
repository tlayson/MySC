<%@ Register TagPrefix="uc1" TagName="Menu" Src="Menu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="Banner.ascx" %>
<%@ Page Language="C#" Inherits="CuteChat.ChatAdminPage" %>
<%@ Import Namespace="CuteChat" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	 <head id="Head1" runat="server">
		<title>Customize Emotion - ASP.NET Chat Software</title>  
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
			if(val=="")
				val=null;
			else
				val = val.Replace(System.Environment.NewLine, "");
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
									Customize Emotion
								</td>
							</tr>
							<tr>
								<td valign=top class="boxArea">		
									The Emotion Panel of Cute Chat by default displays a predefined set of emotions. You can easily modify this default set.<br><br>
										
										<img alt="Customize Emotion" src="../images/Chat_Customize_Emotion.gif" border=0>
										<br><br>
										<b>By default all emotions in the following string will be used in Cute Chat emotion panel. </b> <br>
										<BR>emsmile.gif,emteeth.gif,emwink.gif,emsmileo.gif,emsmilep.gif,emsmiled.gif,emangry.gif,emembarrassed.gif,<BR>
										emcrook.gif,emsad.gif,emcry.gif,emdgust.gif,emangel.gif,emlove.gif,emunlove.gif,emmessag.gif,<BR>
										emcat.gif,emdog.gif,emmoon.gif,emstar.gif,emfilm.gif,emnote.gif,emrose.gif,emrosesad.gif,<BR>
										emclock.gif,emlips.gif,emgift.gif,emcake.gif,emphoto.gif,emidea.gif,emtea.gif,emphone.gif,<BR>
										emhug.gif,emhug2.gif,embeer.gif,emcocktl.gif,emmale.gif,emfemale.gif,emthup.gif,emthdown.gif<BR>
										<br>
										<p><b>You can easily modify this default set by creating your own emotion array.</b><p>
										<BR>emsmile.gif,emteeth.gif,emwink.gif,emsmileo.gif,emsmilep.gif,emsmiled.gif,emangry.gif,emembarrassed.gif,<BR>
										emcrook.gif,emsad.gif,emcry.gif,emdgust.gif,emangel.gif,emlove.gif,emunlove.gif,emmessag.gif<BR><BR><BR>
										<asp:TextBox ConfigName="emotionsArray" Runat="server" Width="500" ID="emotionsArray" TextMode="MultiLine" Rows="4"></asp:TextBox>
									<br>
									<asp:Button ID="ButtonUpdate" OnClick="ButtonUpdate_Click" Runat="server" Text="Update"></asp:Button>
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
