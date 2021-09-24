<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="MyTeams.aspx.cs" Inherits="MyUSC.MyTeamsTemp" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <div style="background-image: url('Images/MyTeamsPage.png'); width: 1062px;  height: 630px">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
