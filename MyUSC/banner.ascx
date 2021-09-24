<%@ Control Language="c#" %>
<%@ Import Namespace="CuteChat" %>
<div id="header">
    <table style="height:88px;" cellpadding="10" class="w920">
        <tr>
            <td rowspan="2" style="width: 250px;">
                <br />
                <a href="default.aspx">
                    <asp:Image ImageUrl="~/Images/samplelogo.gif" runat="server" ID="Image1" />
                </a>
            </td>
            <td style="vertical-align: top; text-align: right">
                <asp:Label ID="WelcomeMessage" runat="server" />
                <asp:LinkButton ID="link_Logout" OnClick="link_Logout_Click" runat="server">Logout</asp:LinkButton>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="topmenu" CssClass="topmenu" runat="server" />
            </td>
        </tr>
    </table>
</div>

<%="<"%><%="script"%><%=" src='"%><%=ResolveUrl("~/CuteSoft_Client/CuteChat/IntegrationUtility.js.aspx?Chat_StartPartialMessenger=1")%><%="'"%><%=">"%><%="<"%><%="/script"%><%=">"%>
<script runat="server">

	private void Page_Load(object sender, System.EventArgs e)
	{
		string temp  = string.Empty;
		// If user logged in, customize welcome message
		if (Request.IsAuthenticated == true) 
		{				
			this.link_Logout.Visible = true;
            temp = "Welcome " + this.Page.User.Identity.Name + " | ";
			temp += "<a href='editprofile.aspx'>Profile</a> | ";
			temp += "<a href='whoisonline.aspx'>Who's Online</a> | ";	
			temp += "<a href='memberlist.aspx'>Member List</a> | ";
		}
		else
		{
			this.link_Logout.Visible = false;
			temp += "<a href='Login.aspx'>Login</a> | ";
			temp += "<a href='agreement.aspx'>Register</a> | ";
			temp += "<a href='whoisonline.aspx'>Who's Online</a> | ";	
			temp += "<a href='memberlist.aspx'>Member List</a>";
		}			
		WelcomeMessage.Text = temp;		
		
		temp = "<ul>";
        temp += "<li><a href='default.aspx'>Home</a></li>";
        temp += "<li><a href='Chat.aspx'>Chat</a></li>";
        temp += "<li><a href='Embed.aspx'>Embed Chat</a></li>";
        temp += "<li><a href='###' onClick='Chat_OpenMessengerDialog()' ><img src='images/defaultavatar.gif' border=0 align='absMiddle'>&nbsp;&nbsp;Messenger</a></li>";
     
		// If user logged in, customize welcome message
		if (Request.IsAuthenticated == true) 
		{				
			if(IsAdministrator(this.Page.User.Identity.Name))
				temp += "<li><a href='Admins/UserAdmin.aspx'>Admin</a></li>";
		}
		
		if(CuteChat.ChatWebUtility.CurrentIdentityIsAdministrator)
			temp += "<li><a href='CuteSoft_Client/CuteChat/ChatAdmin'>Chat Admin</a></li>";			
		topmenu.Text = temp+"</ul>";
	}

	public String ParentPageRelativePath
	{
		get
		{
			if (this.ViewState["ParentPageRelativePath"] == null)
			{
				return "";
			}
			return (String) this.ViewState["ParentPageRelativePath"];
		}
		set
		{
			this.ViewState["ParentPageRelativePath"] = value;
		}
	}

	public bool IsAdministrator(string useruniquename)
	{
		String[] roles = SamplePortal.Global.GetRolesOfUser(useruniquename);
		return Array.IndexOf(roles, "Admins") > -1;
	}
	
	private void link_Logout_Click(object sender, System.EventArgs e)
	{
		FormsAuthentication.SignOut();
		Response.Redirect("~/");
	}
</script>