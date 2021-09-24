<%@ Control Language="c#" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="SamplePortal" %>

<script language=C# runat=server>
		
		public event EventHandler AfterUpdate;
		public event EventHandler AfterCancel;
		
		public String Username
		{
			get
			{
				return this.ViewState["Username"] as String;
			}
			set
			{
				this.ViewState["Username"] = value;
			}
		}

		public String ParentPageRelativePath
		{
			get
			{
				if (this.ViewState["ParentPageRelativePath"] == null)
				{
					return "";
				}
				return (String) this.ViewState["ParentPageRelativePath"];
			}
			set
			{
				this.ViewState["ParentPageRelativePath"] = value;
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (this.Username == null)
			{
				this.Visible = false;
				return;
			}

			if (! this.IsPostBack)
			{
				this.FillUserProfile();
			}

		}

		private void FillUserProfile()
		{
			DataSet ds = SamplePortal.Components.UserData.GetUserInfo(this.Username);
			DataRow row = ds.Tables[0].Rows[0];

			this.txtLocation.Text = row["Location"].ToString();
			this.txtOccupation.Text = row["Occupation"].ToString();
			this.txtInterests.Text = row["Interests"].ToString();
			this.txtAge.Text = row["Age"].ToString();
			
			if((Boolean) row["Gender"])
				rbtBoy.Checked = true;
			else
				rbtGirl.Checked = true;
		}

		public void DoModifyUserProfile(String username, String password, String location, String occupation, String interests, bool gender, int age)
		{
			Boolean bChangePassword = (password != null) && (password.Length > 0);

			SqlConnection conn = new SqlConnection(Global.ConnectionString);
			SqlCommand cmd = new SqlCommand("", conn);

			if (bChangePassword)
			{
				cmd.CommandText = "update sampleUsers set Password=@Password, Location=@Location, Occupation=@Occupation, Interests=@Interests, Age=@Age where Username=@Username";
			}
			else
			{
				cmd.CommandText = "update sampleUsers set Location=@Location, Occupation=@Occupation, Interests=@Interests, Age=@Age where Username=@Username";
			}

			if (bChangePassword)
			{
				SqlParameter paraPassword = cmd.Parameters.Add("@Password", SqlDbType.NVarChar);
				paraPassword.Value = password;
			}

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

			SqlParameter paraUsername = cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
			paraUsername.Value = username;

			try
			{
				conn.Open();
				cmd.ExecuteNonQuery();
			}
			catch
			{
				throw;
			}
			finally
			{
				conn.Close();
			}

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: Required method for Designer support
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{
			this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			if (this.AfterCancel != null)
			{
				this.AfterCancel(this, EventArgs.Empty);
			}
		}

		private void btnReg_Click(object sender, System.EventArgs e)
		{
			if (! this.Page.IsValid)
			{
				return;
			}

			String sPassword = this.txtPassword.Text;
			String sLocation = this.txtLocation.Text;
			String sOccupation = this.txtOccupation.Text;
			String sInterests = this.txtInterests.Text;
			int dAge = Int32.Parse(this.txtAge.Text);
			bool male = true;
				
			if (rbtGirl.Checked){ male = false; }

			this.DoModifyUserProfile(this.Username,sPassword, sLocation, sOccupation, sInterests, male, dAge);


			if (this.AfterUpdate != null)
			{
				this.AfterUpdate(this, EventArgs.Empty);
			}
		}

</script>


<table cellspacing="0" border="0" cellpadding="6" width="90%">
    <tr>
        <td align="right">
            Password:</td>
        <td>
            <asp:TextBox ID="txtPassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password is required."
                ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            Re-enter Password:</td>
        <td>
            <asp:TextBox ID="txtPassword2" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Password is required."
                ControlToValidate="txtPassword2" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Passwords do not match!"
                ControlToValidate="txtPassword2" Display="Dynamic" ControlToCompare="txtPassword"></asp:CompareValidator>
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
            <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ErrorMessage="Age is required."
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
            </td>
        <td>
            <asp:Button ID="btnReg" runat="server" CssClass="TextButton" Width="80px" Text="Update">
            </asp:Button>
            <asp:Button ID="btnCancel" runat="server" CssClass="TextButton" Width="80px" Text="Cancel"
                CausesValidation="False"></asp:Button></td>
    </tr>
</table>
