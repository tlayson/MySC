﻿<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="DialogTest.aspx.cs" Inherits="MyUSC.Experiment.DialogTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
	// Used the following example to open the dialog withJquery 
	{
		//Adding the event of opening the dialog from the asp.net add button.
				$(this).parent().appendTo("form:first");
			}
		});

		function showDialog()
		{
			var id = "addPerson";
			$('#' + id).dialog("open");
		}
		//        function closeDialog(id) {
		//Adding a event handler for the close button that will close the dialog 
			$("#divDlg").dialog("close");
		});
	});
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
