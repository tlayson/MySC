<%@ Page Language="c#" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="Banner.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Copyright Policy</title>
    <link rel="stylesheet" href="sample.css" type="text/css" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <uc1:TopBanner ID="TopBanner1" runat="server"></uc1:TopBanner>
        <div class="infobox" style="width: 800px; margin: 30px auto">
            <h2>
                Copyright Policy
            </h2>
            <div class="padding10" style="min-height:300px">
            </div>
        </div>
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
    </form>
</body>
</html>
