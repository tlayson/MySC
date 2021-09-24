<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RSSDisplay.aspx.cs" Inherits="MyUSC.RSSDisplay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
		<table style="width: 700px; background-color: #000000;">
			<tr>
				<td colspan="2" class="tdInput">
			    	<asp:Xml ID="xmlRSSDisplay" runat="server" TransformSource="~/RSSDisplay.xslt"></asp:Xml>
				</td>
			</tr>
		</table>
    </form>
</body>
</html>
