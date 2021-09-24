<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="MyUSC.Experiment.Test" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<%@ Register Assembly="MyUSC" Namespace="MyUSC.Classes" TagPrefix="MSC" %>
<%@ Register src="../Classes/ResultsTable.ascx" tagname="ResultsTable" tagprefix="MSC" %>
<%@ Register src="../Classes/TBSCResponseDlg.ascx" tagname="TBSCResponseDlg" tagprefix="MSC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div>
		<asp:Button ID="btnResults" runat="server" Text="Results"  OnClick="OnClickResults" />
		<asp:Panel ID="pnlResults" runat="server" CssClass="popupControl">
			<asp:UpdatePanel runat="server" ID="UpdatePanel1">
			<ContentTemplate>
				<MSC:ResultsTable ID="ResultsTable1" runat="server"></MSC:ResultsTable>
			</ContentTemplate>
			</asp:UpdatePanel>
		</asp:Panel>
        <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" runat="server"
            TargetControlID="btnResults"
            PopupControlID="pnlResults"
            Position="Bottom" />
		
	</div>
	<!--
    <div>
		<asp:Button ID="btnTest" runat="server" Text="Test" OnClick="OnClickTest" />
		<asp:Panel ID="pnlTest" runat="server" CssClass="popupControl">
			<asp:UpdatePanel runat="server" ID="upDlg">
			<ContentTemplate>
				<MSC:TBSCResponseDlg ID="TBSCRSVPDlg1" runat="server" EventID="1" OrgID="1" ResponseID="1" VenueID="1"></MSC:TBSCResponseDlg>
			</ContentTemplate>
			</asp:UpdatePanel>
		</asp:Panel>
        <ajaxToolkit:PopupControlExtender ID="PopupControlExtender3" runat="server"
            TargetControlID="btnTest"
            PopupControlID="pnlTest"
            Position="Bottom" />
    </div>
	-->
</asp:Content>
