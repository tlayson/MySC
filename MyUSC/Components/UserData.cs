using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SamplePortal.Components
{
	/// <summary>
	/// Data Access for User Management
	/// </summary>
	public class UserData
	{
		public UserData()
		{
		}

		private String ConnectionString
		{
			get
			{
				return System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
			}
		}

		/// <summary>
		/// if LastOnlineTime before this time, then offline
		/// </summary>
		private DateTime OnlineTimeoutTime
		{
			get
			{
				Int32 iTimeoutSecond = Int32.Parse(System.Configuration.ConfigurationSettings.AppSettings["OnlineTimeout"]);
				return DateTime.Now.AddSeconds(0 - iTimeoutSecond);
			}
		}

		private SqlConnection CreateConnection()
		{
			return new SqlConnection(this.ConnectionString);
		}

		public Boolean Login(String username, String password)
		{
			SqlConnection conn = this.CreateConnection();
			SqlCommand cmd = new SqlCommand("select Password from sampleUsers where Username=@Username", conn);

			SqlParameter paraUsername = cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
			paraUsername.Value = username;

			try
			{
				conn.Open();
				String sPassword = (String) cmd.ExecuteScalar();
				return sPassword == password;
			}
			catch
			{
				return false;
			}
			finally
			{
				conn.Close();
			}
		}

		public void UpdateLastLoginTime(String username)
		{
			SqlConnection conn = this.CreateConnection();
			SqlCommand cmd = new SqlCommand("update sampleUsers set LastLoginTime=@LastLoginTime where Username=@Username", conn);

			SqlParameter paraLastLoginTime = cmd.Parameters.Add("@LastLoginTime", SqlDbType.DateTime);
			paraLastLoginTime.Value = DateTime.Now;

			SqlParameter paraUsername = cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
			paraUsername.Value = username;

			try
			{
				conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}
		}

		public RegNewUserResult RegNewUser(String username, String password, String location, String occupation, String interests, int age, Boolean gender)
		{
			if (this.IsUsernameExists(username))
			{
				return RegNewUserResult.UsernameAlreadyExists;
			}

			SqlConnection conn = this.CreateConnection();
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

			SqlParameter paraSex = cmd.Parameters.Add("@Gender", SqlDbType.Bit);
			paraSex.Value = gender;

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
			RoleData roleData = new RoleData();
			roleData.AddUserToRole(username, "Members");

			return RegNewUserResult.Success;
		}

		public Boolean IsUsernameExists(String username)
		{
			if(username==null||username=="")
				return false;

			SqlConnection conn = this.CreateConnection();
			SqlCommand cmd = new SqlCommand("select COUNT(Username) from sampleUsers where Username=@Username", conn);

			SqlParameter paraUsername = cmd.Parameters.Add("@Username", SqlDbType.NVarChar,80);
			paraUsername.Value = username;

			try
			{
				conn.Open();
				return Convert.ToInt32(cmd.ExecuteScalar())>0;
			}
			finally
			{
				conn.Close();
			}
		}

		public DataSet GetOnlineUsers()
		{
			SqlConnection conn = this.CreateConnection();
			SqlCommand cmd = new SqlCommand();
			cmd.Connection = conn;
			cmd.CommandText = "select * from sampleUsers where LastLoginTime>=@LastLoginTime";

			SqlParameter paraLastLoginTime = cmd.Parameters.Add("@LastLoginTime", SqlDbType.DateTime);
			paraLastLoginTime.Value = this.OnlineTimeoutTime;

			SqlDataAdapter ada = new SqlDataAdapter(cmd);
			DataSet result = new DataSet();

			try
			{				
				conn.Open();
				ada.Fill(result);
			}
			finally
			{
				conn.Close();
			}

			return result;
		}

		public static DataSet GetAllUsers()
		{
			SqlConnection conn = new SqlConnection(Global.ConnectionString);
			SqlCommand cmd = new SqlCommand("select * from sampleUsers order by Location", conn);

			SqlDataAdapter ada = new SqlDataAdapter(cmd);
			DataSet result = new DataSet();

			try
			{				
				conn.Open();
				ada.Fill(result);
			}
			finally
			{
				conn.Close();
			}

			result.Tables[0].Columns.Add("SexString", typeof(String));
			result.Tables[0].Columns.Add("Roles", typeof(String));

			foreach(DataRow row in result.Tables[0].Rows)
			{
				row["SexString"] = ((Boolean) row["Gender"]) ? "Male" : "Female";
				row["Roles"] = String.Join(",", Global.GetRolesOfUser(row["Username"].ToString()));
			}

			return result;
		}

		
		public static DataSet GetUserInfo(String username)
		{
			SqlConnection conn = new SqlConnection(Global.ConnectionString);
			SqlCommand cmd = new SqlCommand();
			cmd.Connection = conn;
			cmd.CommandText = "select * from sampleUsers where Username=@Username";

			SqlParameter paraUsername = cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
			paraUsername.Value = username;

			SqlDataAdapter ada = new SqlDataAdapter(cmd);
			DataSet result = new DataSet();

			try
			{				
				conn.Open();
				ada.Fill(result);
			}
			finally
			{
				conn.Close();
			}

			return result;
		}
	}
}
