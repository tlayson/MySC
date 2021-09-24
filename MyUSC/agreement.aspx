<%@ Page Language="c#" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="Banner.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Registration Agreement Terms </title>
    <link rel="stylesheet" href="sample.css" type="text/css" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <uc1:TopBanner ID="TopBanner1" runat="server"></uc1:TopBanner>
        <div class="infobox" style="width: 800px; margin: 30px auto">
            <h2>
                Registration Agreement Terms
            </h2>
            <div class="padding10">
                <p>
                    The Web Chat is provided as a free service to any Visitor who agrees to abide by
                    the Rules. YourSite.com reserves the right to change the nature of this relationship
                    at any time. VISITORS WHO VIOLATE THE TERMS OF THE RULES OF THE WEB CHAT MAY PERMANENTLY
                    BE BANNED FROM USING THE WEB CHAT. ENTERING THE WEB CHAT WILL CONSTITUTE ACCEPTANCE
                    OF THE TERMS AND CONDITIONS OF THESE RULES.</p>
                <p>
                    The following rules apply while using the Web Chat:
                </p>
                <p>
                    I. Pornographic, obscene, nude, graphically violent, and other inappropriate images
                    are not permitted. Pornographic, profane or obscene language is not permitted in
                    the Web Chat, forums, and Guest Books, and/or private messages.
                </p>
                <p>
                    II. Offensive and indecent handles are not allowed and will be blocked.
                </p>
                <p>
                    III. Harassment of another Visitor on the Web Chat or use of obscene or abusive
                    language is not permitted.
                </p>
                <p>
                    IV. Do not disrupt the flow of a discussion by repeatedly posting the same message
                    or image or posting excessively large images.
                </p>
                <p>
                    V. Impersonation of others, including a YourSite.com employee or representative
                    is forbidden. Handles that impersonate will be blocked.
                </p>
                <p>
                    VI. YourSite.com RESERVES THE RIGHT TO TERMINATE ANY VISITOR'S ACCOUNT FOR ANY REASON.
                </p>
                By clicking Register below you agree to be bound by these conditions.
                <br />
                <br />
                <div align="center">
                    <a href="Register.aspx">I Agree to these terms and am <b>over</b> or <b>exactly</b>
                        13 years of age</a><br />
                    <br />
                    <a href="default.aspx">I do not agree to these terms</a>
                </div>
            </div>
        </div>
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
    </form>
</body>
</html>
