<%@ Page Title="" Language="C#" MasterPageFile="~/USC.Master" AutoEventWireup="true" CodeBehind="Support.aspx.cs" Inherits="MyUSC.Support" %>
<%@ MasterType VirtualPath="~/USC.Master" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<%@ Register TagPrefix="CE" namespace="CuteEditor.ImageEditor" assembly="CuteEditor.ImageEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<script type="text/javascript" src="~/js/GA.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 1060px; height: 650px; background-image: url('Images/Background.png')">
		<tr valign="top" style="height: 100px">
			<td>
				<table width="100%">
					<tr align="center">
						<td class="tdInput">
							<asp:Label ID="lblSiteName" CssClass="xlgSiteColorTxt" runat="server" Text="MySportsConnect.net"></asp:Label>
						</td>
					</tr>
					<tr align="center">
						<td class="tdInput">
							<asp:Label ID="lblCustomerSupport" CssClass="lgSiteColorTxt" runat="server" Text="Customer Support"></asp:Label>
						</td>
					</tr>
					<tr align="center">
						<td class="tdInput">
							<span class="lgErrorTxt">* - </span>
							<asp:Label ID="lblRequiredFields" CssClass="medNormalTxt" runat="server" Text="indicates a required field."></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr valign="top" style="height: 220px">
			<td>
				<table width="100%">
					<tr align="center">
						<td class="tdInput">
							<table>
							<tr>
								<td rowspan="8">
									<asp:Image ID="imgSupport" runat="server" 
										ImageUrl="~/Images/Verification/SupportPerson.JPG" Height="134px" />
								</td>
								<td class="tdInput" align="right">
									<asp:Label ID="lblFirstName" runat="server" Text="First Name: " Width="450px"></asp:Label>
								</td>
								<td class="tdInput">               
									<asp:TextBox ID="txtFirstName" runat="server" Visible="True" Width="400px"></asp:TextBox>                
								</td>
								<td class="tdInput">               
									<asp:Label ID="lblMustFill1" runat="server" CssClass="lgErrorTxt" Text="*"></asp:Label>               
								</td>
							</tr>
								<tr>
									<td class="tdInput" align="right">
										<asp:Label ID="lblLastName" runat="server" Text="Last Name:" Width="450px"></asp:Label>
									</td>
									<td class="tdInput">
										<asp:TextBox ID="txtLastName" runat="server" Visible="True" Width="400px"></asp:TextBox>
									</td>
									<td class="tdInput">
										<asp:Label ID="lblMustFill2" CssClass="lgErrorTxt" runat="server" Text="*" ></asp:Label>
									</td>
								</tr>
								<tr>
									<td class="tdInput" align="right">
										<asp:Label ID="lblEmail" runat="server" Text="Email:" Width="450px"></asp:Label>
									</td>
									<td class="tdInput">
										<asp:TextBox ID="txtEmail" runat="server" Width="400px"></asp:TextBox>
									</td>
									<td class="tdInput">
										<asp:Label ID="lblMustFill3" runat="server" Text="*" CssClass="lgErrorTxt"></asp:Label>
									</td>
								</tr>
								<tr>
									<td class="tdInput" align="right">
										<asp:Label ID="lblPhone" runat="server" Text="Phone:" Width="450px"></asp:Label>
									</td>
									<td class="tdInput">
										<asp:TextBox ID="txtPhone" runat="server" Visible="True" Width="400px"></asp:TextBox>
									</td>
									<td class="tdInput">
										<asp:Label ID="lblMustFill4" runat="server"  Text=" " CssClass="lgErrorTxt"></asp:Label>
									</td>
								</tr>
								<tr>
									<td class="tdInput" align="right">
										<asp:Label ID="lblBrowser" runat="server" Text="Browser and version:" Width="450px"></asp:Label>
									</td>
									<td class="tdInput">
										<asp:TextBox ID="txtBrowser" runat="server" Visible="True" Width="400px"></asp:TextBox>
									</td>
									<td class="tdInput">
										<asp:Label ID="lblMustFill5" runat="server" Text=" " CssClass="lgErrorTxt"></asp:Label>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr valign="top" style="height: 30px">
			<td class="tdInput">
				<table id="Table1" width="100%" runat="server">
					<tr>
						<td class="tdInput" align="right">
						  <asp:Label ID="lblDescription" runat="server" Text="Brief Description:" Width="240px"></asp:Label>
						</td> 
						<td class="tdInput">                  
							<asp:TextBox ID="txtDescription" runat="server" Visible="True" Width="700px" MaxLength="50"></asp:TextBox>                    
						</td>
						<td>
							<asp:Label ID="lblMustFill6" runat="server" Text="*" CssClass="lgErrorTxt"></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr valign="top">
			<td class="tdInput">
                <table width="100%">
					<tr>
						<td class="tdInput" colspan="2">
                            <asp:Label ID="lblDetails" runat="server" Text="Details:" Width="378px"></asp:Label>
						</td>
					</tr>
                    <tr>
                        <td>
                            <CE:editor ID="txtDetails" runat="server" Width="1000px" 
                                AutoConfigure="Minimal" BorderWidth="3px" CodeViewTemplateItemList="" 
                                DownLevelRows="50" Focus="True" ResizeMode="AutoAdjust" ShowBottomBar="False" 
                                ShowCodeViewToolBar="False" ShowDecreaseButton="False" 
                                ShowEnlargeButton="False" ShowHtmlMode="False" ShowPreviewMode="False" 
                                ShowTagSelector="False" UseSimpleAmpersand="True" BackColor="#FFFFFF" 
                                Font-Underline="True" ForeColor="Black" Font-Names="Verdana" 
                                Font-Size="Small" Height="200px">
                            </CE:editor>
                        </td>
                        <td>
                            <asp:Label ID="lblMustFill7" runat="server" Text="*" CssClass="lgErrorTxt"></asp:Label>
                        </td>
                    </tr>
                </table>
			</td>
		</tr>
		<tr>
			<td class="tdInput" align="center">
                <table>
					<tr>
						<td align="right">
							<asp:Label ID="lblHumanCode" runat="server" Text="Human Code:" Width="160px"></asp:Label>
						</td>
						<td>
							<asp:TextBox ID="txtHumanCode" runat="server" Visible="True" Width="60px" MaxLength="5"></asp:TextBox>
						</td>
						<td>
							<asp:Label ID="lblMustFill8" runat="server" CssClass="lgErrorTxt" Text="*"></asp:Label>
						</td>
						<td>
							<asp:Image ID="imgHumanCode" runat="server" BorderColor="White" BorderStyle="Groove" BorderWidth="2px" ImageUrl="~/Images/Verification/9P79Y.JPG" />
						</td>
					</tr>
                </table>
			</td>
		</tr>
		<tr>
			<td class="tdInput" align="center" >
                <table id="tblButtons" runat="server">
                    <tr align="center">
                        <td align="right">
							<asp:ImageButton ID="btnSubmit" ImageUrl="~/Images/Button/btnSubmit.png" runat="server" OnClick="OnClickSubmit" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td align="center">
							<asp:ImageButton ID="btnCancel" ImageUrl="~/Images/Button/btnCancel.png" runat="server" OnClick="OnClickCancel" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td align="left">
							<asp:ImageButton ID="btnLogin" ImageUrl="~/Images/Button/btnSignIn.png" runat="server" OnClick="OnClickLogin" />
                        </td>
                    </tr>
                </table>
			</td>
		</tr>
    </table>
</asp:Content>
