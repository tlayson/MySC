<%@ Page language="c#" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="Banner.ascx" %>
<%@ Register TagPrefix="CuteChat" TagName="EmbedChannel" Src="CuteSoft_Client/CuteChat/EmbedChannel.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Embed Chat Example</title>
		<link rel="stylesheet" href="sample.css" type="text/css" />
	</head>
	<body>
        <form id="Form1" method="post" runat="server">
            <uc1:TopBanner ID="TopBanner1" runat="server"></uc1:TopBanner>
            <div style="width: 750px; margin: 30px auto; min-height: 400px;">
                <h1>
                    Embed Chat Example</h1>

                <script type="text/javascript">					
						Embed_Place='Lobby-1';
						if('<%=Request.QueryString["Place"]%>'+""!="")
							Embed_Place='<%=Request.QueryString["Place"]%>'+"";
                </script>

                <CuteChat:EmbedChannel ID="EmbedChannel1" runat="server"></CuteChat:EmbedChannel>
                <asp:Literal ID="Literal1" runat="server" />
            </div>
            <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
        </form>
		
		<script runat="server">
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (! this.IsPostBack)
			{
                if (CuteChat.ChatApi.GetLobbyInfoArray().Length>0)
                    EmbedChannel1.Visible = true;
                else
                {
                    Literal1.Text = "<p align='center'><b>You have to create a lobby first in the admin console to test this feature!</b></p>";
                    EmbedChannel1.Visible = false;
                }
			}			
		}
		</script>
	</body>
</html>
