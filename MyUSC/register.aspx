<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="SamplePortal"%>
<%@ Page language="c#" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopBanner" Src="Banner.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Registration for New Account</title>
		<link rel="stylesheet" href="sample.css" type="text/css" />
	</head>
	<body>
        <form id="Form1" method="post" runat="server">
            <uc1:TopBanner ID="TopBanner1" runat="server"></uc1:TopBanner>
            <div class="infobox" style="width: 700px; margin: 30px auto">
                <h2>
                    Registration Information
                </h2>
                <div class="padding10">
                    <table cellspacing="0" border="0" cellpadding="8" style="width: 90%">
                        <tr>
                            <td align="right" style="width:150px">
                                Username:
                            </td>
                            <td>
                                <asp:TextBox ID="txtUsername" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username is required"
                                    ControlToValidate="txtUsername" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtUsername" ErrorMessage="Please keep Username one word with no spaces or special characters."
                                    ValidationExpression="^[a-zA-Z0-9\._-]+$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Password:</td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password is required."
                                    ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="You must re-enter the new password."
                                    ControlToValidate="txtPassword2" Display="Dynamic" ControlToCompare="txtPassword"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Re-enter Password:</td>
                            <td>
                                <asp:TextBox ID="txtPassword2" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Password is required."
                                    ControlToValidate="txtPassword2" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Gender:</td>
                            <td>
                                <asp:RadioButton ID="rbtBoy" runat="server" GroupName="Gender" Checked="True" Text="Male">
                                </asp:RadioButton>&nbsp;
                                <asp:RadioButton ID="rbtGirl" runat="server" GroupName="Gender" Text="Female"></asp:RadioButton>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Age:</td>
                            <td>
                                <asp:TextBox ID="txtAge" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" ErrorMessage="Age is required."
                                    ControlToValidate="txtAge" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="rangeValInteger" Type="Integer" ControlToValidate="txtAge"
                                    MaximumValue="100" MinimumValue="14" runat="server">	
															Age must be between 14 and 100.
                                </asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Location:</td>
                            <td>
                                <asp:TextBox ID="txtLocation" MaxLength="100" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Occupation:</td>
                            <td>
                                <asp:TextBox ID="txtOccupation" MaxLength="100" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Interests:</td>
                            <td>
                                <asp:TextBox ID="txtInterests" MaxLength="100" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="btnReg" runat="server" OnClick="btnReg_Click" Width="80px" Text="Register">
                                </asp:Button>
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Width="80px"
                                    Text="Cancel" CausesValidation="False"></asp:Button></td>
                        </tr>
                    </table>
                </div>
            </div>
            <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
        </form>
		<script runat="server">
		
			public RegNewUserResult DoRegNewUser(String username, String password, String location, String occupation, String interests, Boolean gender, int age)
			{
				if (this.IsUsernameExists(username))
				{
					return RegNewUserResult.UsernameAlreadyExists;
				}

				SqlConnection conn = new SqlConnection(Global.ConnectionString);
				SqlCommand cmd = new SqlCommand("insert into sampleUsers (Username, Password, Location, Occupation, Interests, Age, Gender,LastLoginTime,DateCreated) values (@Username, @Password, @Location, @Occupation, @Interests, @Age, @Gender,GETDATE(),GETDATE())", conn);

				SqlParameter paraUsername = cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
				paraUsername.Value = username;

				SqlParameter paraPassword = cmd.Parameters.Add("@Password", SqlDbType.NVarChar);
				paraPassword.Value = password;

				SqlParameter paraLocation = cmd.Parameters.Add("@Location", SqlDbType.NVarChar);
				paraLocation.Value = location;

				SqlParameter paraOccupation = cmd.Parameters.Add("@Occupation", SqlDbType.NVarChar);
				paraOccupation.Value = occupation;

				SqlParameter paraInterests = cmd.Parameters.Add("@Interests", SqlDbType.NVarChar);
				paraInterests.Value = interests;
				
				SqlParameter paraAge = cmd.Parameters.Add("@Age", SqlDbType.Int,4);
				paraAge.Value = age;				

				SqlParameter paraGender= cmd.Parameters.Add("@Gender", SqlDbType.Bit);
				paraGender.Value = gender;

				try
				{
					conn.Open();
					cmd.ExecuteNonQuery();
				}
				catch(Exception x)
				{
					return RegNewUserResult.DatabaseFail;
				}
				
				finally
				{
					conn.Close();
				}
		
				// Add new user to "Members" role
				Global.AddUserToRole("Members", username);

				return RegNewUserResult.Success;
			}

			public Boolean IsUsernameExists(String username)
			{
				SqlConnection conn = new SqlConnection(Global.ConnectionString);
				SqlCommand cmd = new SqlCommand("select Username from sampleUsers where Username=@Username", conn);
	
				SqlParameter paraUsername = cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
				paraUsername.Value = username;

				try
				{
					conn.Open();
					Object result = cmd.ExecuteScalar();
					return result != null;
				}	
				finally
				{
					conn.Close();
				}
			}

			private void btnCancel_Click(object sender, System.EventArgs e)
			{
				this.Response.Redirect("~/Default.aspx");
			}

			private void btnReg_Click(object sender, System.EventArgs e)
			{
				if (! this.IsValid)
				{
					return;
				}
	
				String sUsername = this.txtUsername.Text;
				String sPassword = this.txtPassword.Text;
				String sLocation = this.txtLocation.Text;
				String sOccupation = this.txtOccupation.Text;
				String sInterests = this.txtInterests.Text;
				int dAge = Int32.Parse(this.txtAge.Text);
				bool male = true;
				
				if (rbtGirl.Checked){ male = false; }

				RegNewUserResult result = this.DoRegNewUser(sUsername, sPassword, sLocation, sOccupation, sInterests, male, dAge);

				if (result == RegNewUserResult.UsernameAlreadyExists)
				{
					JSHelper.MsgBox("Username [" + sUsername + "] is already exists. !");
				}
				else if (result == RegNewUserResult.DatabaseFail)
				{
					JSHelper.MsgBox("Database error, please try later.");
				}
				else if (result == RegNewUserResult.Success)
				{
					FormsAuthentication.SetAuthCookie(sUsername, false);
					this.Response.Redirect("~/Default.aspx");
				}
			}

			public enum RegNewUserResult
			{
				Success,
				UsernameAlreadyExists,
				DatabaseFail
			}
		</script>
	</body>
</html>
