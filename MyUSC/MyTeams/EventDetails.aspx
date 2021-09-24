<%@ Page Title="" Language="C#" MasterPageFile="~/MyTeams/OrgMaster.master" AutoEventWireup="true" CodeBehind="EventDetails.aspx.cs" Inherits="MyUSC.MyTeams.EventDetails" %>
<%@ MasterType VirtualPath="~/MyTeams/OrgMaster.Master" %>
<%@ Register src="~/Classes/DateSelect.ascx" tagname="DateSelect" tagprefix="MSC" %>
<%@ Register Src="~/Classes/TimeSelect.ascx" TagPrefix="MSC" TagName="TimeSelect" %>


<asp:Content ID="Content1" ContentPlaceHolderID="OrgHeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="OrgContent" runat="server">
	<MSC:TBSCPanel ID='pnlEditEvent' runat="server" CssClass="pnlMyTeamsContent">

	</MSC:TBSCPanel>
</asp:Content>
