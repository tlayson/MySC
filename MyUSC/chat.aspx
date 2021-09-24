<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="Footer.ascx" %>
<%@ Import Namespace="CuteChat" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Page language="c#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<head>
		<title>Cute Chat - ASP.NET Chat</title>
		<link rel="stylesheet" href="sample.css" type="text/css">
	</head>
	<body>
        <form id="Form1" method="post" runat="server">
            <uc1:TopBanner ID="TopBanner1" runat="server"></uc1:TopBanner>
            <div class="infobox" style="width: 700px; margin: 30px auto">
                <h2>
                    Chat
                </h2>
                <div class="padding10">
                    <table cellspacing="0" border="0" cellpadding="8" style="width: 90%">
                        <tr>
                            <td>
                                <asp:DataList ID="DataList_Lobbies" CellSpacing="3" Width="400" RepeatColumns="2"
                                    runat="server">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkLobby" runat="server">
															<nobr><img src="images/home.gif" width="49" height="49" border="0" align="middle"><%#DataBinder.Eval(Container.DataItem, "Name")%></nobr>
                                        </asp:HyperLink>
                                        <br />
                                        (
                                        <asp:Label ID="lblOnlineChater" runat="server"></asp:Label>
                                        chatting)
                                    </ItemTemplate>
                                </asp:DataList>
                                <asp:Literal ID="Literal1" runat="server" />
                            </td>
                            <td>
                                <img alt="" src="images/cutechat_picture.jpg" height="180" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
        </form>
		<script runat="server">
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (! this.IsPostBack)
			{
				ArrayList lobbies=new ArrayList();
				foreach(LobbyInfo lobby in ChatApi.GetLobbyInfoArray())
				{
					if(lobby.Lobby.Integration==""||lobby.Lobby.Integration==null)
						lobbies.Add(lobby);
				}
				this.DataList_Lobbies.DataSource = lobbies;
				this.DataList_Lobbies.ItemDataBound+=new DataListItemEventHandler(rptLobbies_ItemDataBound);
				this.DataList_Lobbies.DataBind();
			}

            if (CuteChat.ChatApi.GetLobbyInfoArray().Length < 1)
                Literal1.Text = "<p style='font-size:14px;'>There is no chat room availables now.<br><br>Please login as admin and create chat rooms in the admin console!</p>";
		}

		
		private void rptLobbies_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if (e.Item.DataItem == null)
			{
				return;
			}

			LobbyInfo lobby = (LobbyInfo)e.Item.DataItem;

					
			HyperLink lnkLobby = e.Item.FindControl("lnkLobby") as HyperLink;
			lnkLobby.NavigateUrl = CuteChat.ChatWebUtility.ResolveResource(this.Context, "Channel.Aspx") + "?Place=Lobby-" + lobby.Lobby.LobbyId;
			lnkLobby.Target = "_blank";

			String sOnlineChater = lobby.JoinClient(",");

			Label lblOnlineChater = e.Item.FindControl("lblOnlineChater") as Label;
			lblOnlineChater.Text = lobby.ClientCount.ToString();
			lnkLobby.ToolTip = sOnlineChater;
		}

		</script>
	</body>
</html>
