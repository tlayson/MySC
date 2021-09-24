<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="DialogTest.aspx.cs" Inherits="MyUSC.Experiment.DialogTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
	// Used the following example to open the dialog withJquery 	var dl;	$(document).ready(function ()
	{
		//Adding the event of opening the dialog from the asp.net add button.		//setup edit person dialog             		$('#addPerson').dialog({			//Setting up the dialog properties.			show: "blind",			hide: "fold",			resizable: false,			modal: true,			height: 400,			width: 700,			title: "Add New Member",			open: function (type, data) {
				$(this).parent().appendTo("form:first");
			}
		});

		function showDialog()
		{
			var id = "addPerson";
			$('#' + id).dialog("open");
		}
		//        function closeDialog(id) {		//            $('#' + id).dialog("close"); 		//        } 
		//Adding a event handler for the close button that will close the dialog 		$("a[id*=ButtonCloseDlg]").click(function (e) {
			$("#divDlg").dialog("close");			return false;
		});
	});
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:Button ID="ButtonAdd" runat="server" Text="Add" OnClientClick="showDialog();" /></asp:Content>
