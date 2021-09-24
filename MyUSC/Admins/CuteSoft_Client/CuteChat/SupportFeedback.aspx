<%@ Page Language="c#" Inherits="CuteChat.ChatPageBase" %>

<%@ Import Namespace="CuteChat" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="Head1" name="Head1">
    <link rel="icon" href="Icons/Support.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="Icons/Support.ico" type="image/x-icon" />
    <title>[[UI_LeaveMessage]] </title>

    <script runat="server">
        string displayname;

        bool shouldclose = false;
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AppChatIdentity identity = ChatWebUtility.GetLogonIdentity();
            if (identity != null)
                displayname = identity.DisplayName;

            input1.Text = displayname;

            btnok.Click += new EventHandler(btnok_click);
        }
        void btnok_click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            LabelErrorMsg.Visible = false;

            try
            {
                ChatWebUtility.AddFeedback(ChatWebUtility.GetLogonIdentity(), input1.Text, input2.Text, input3.Text, input4.Text);
            }
            catch (Exception x)
            {
                ChatSystem.Instance.LogException(x);
                LabelErrorMsg.Visible = true;
                LabelErrorMsg.Text = "Failed to send message! " + x.Message;
                return;
            }

            shouldclose = true;
            //Response.Redirect("~/");

        }
        public string EJS(string str)
        {
            return ChatWebUtility.EncodeJScriptString(str);
        }

    </script>

    <base target="_self" />
    <style type="text/css">
			body
			{
				background-color: #edf1fa;
				margin:0;	
			}
			body, td
			{
				font: 12px Arial, Helvetica, sans-serif;
			}
            table { border: none; }
            img{border:0;}
            td{vertical-align:top;}
            textarea{width:190px;}
            .inputwidth{width:190px;}		
		</style>
</head>
<body onload="document.focus();">
    <form runat="server" id="Form1">
        <img src="<%=ChatWebUtility.SupportFeedbackImage%>" alt="offline message" />
        <table cellspacing="0" cellpadding="2" style="width: 99%; height: 100%">
            <tr>
                <td>
                    <div style="border: 0px solid #ffffff; background-color: #ffffff; padding: 15px;
                        height: 100%;">
                        <p>
                            There are no operators available right now to take your call. Please leave a message
                            and we will get back to you soon.</p>
                        <table cellspacing="0" cellpadding="3" width="100%">
                            <tr>
                                <td style="width: 90px">
                                    <b>[[Name]]</b>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="input1" CssClass="inputwidth"></asp:TextBox><span style="color:red">*</span>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="input1" ErrorMessage="<br>Name is required!"
                                        ID="RequiredFieldValidator1" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>[[Email]]</b>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="input2" CssClass="inputwidth"></asp:TextBox><span style="color:red">*</span>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="<br>Please Enter a Valid Email address"
                                        ControlToValidate="input2" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">  
                                    </asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="<br>Email is Required."
                                        ControlToValidate="input2"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>[[Subject]]</b></td>
                                <td>
                                    <asp:TextBox runat="server" ID="input3" CssClass="inputwidth"></asp:TextBox><span style="color:red">*</span>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="input3" ErrorMessage="<br>Subject is required!"
                                        ID="RequiredFieldValidator3" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>[[Enquiry]]</b></td>
                                <td>
                                    <asp:TextBox runat="server" TextMode="MultiLine" Rows="4" ID="input4" Height="80"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button runat="server" CssClass="button" ID="btnok" Text="[[UI_SEND]]" Width="100px"></asp:Button>
                                    &nbsp;&nbsp;
                                    <button class="button" onclick="window.close()">
                                        [[CANCEL]]</button>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="LabelErrorMsg" runat="server" ForeColor="#ff0000" Font-Bold="True"
                                        Visible="False"></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td>
                    <img alt="Please leave a message" src="images/live-support-woman.jpg" />
                </td>
            </tr>
        </table>
    </form>

    <script type="text/javascript">
     document.getElementById("input1").focus();
    <%if (shouldclose)
    {%>
			alert("Message sent! We will respond as soon as possible.");
    <%}%>
    </script>

</body>
</html>
