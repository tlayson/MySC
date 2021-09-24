using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SamplePortal.Components
{
	/// <summary>
	/// Data Access for Role Management
	/// </summary>
	public class RoleData
	{
		public RoleData()
		{
		}

		private String ConnectionString
		{
			get
			{
				return System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
			}
		}

		private SqlConnection CreateConnection()
		{
			return new SqlConnection(this.ConnectionString);
		}

		public void AddUserToRole(String roleName, String username)
		{
			if (this.IsUserInRole(roleName, username))
			{
				return;
			}

			SqlConnection conn = this.CreateConnection();
			SqlCommand cmd = new SqlCommand("Insert into sampleUserRole (Username, RoleName) values (@Username, @RoleName)", conn);

			SqlParameter paraUsername = cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
			paraUsername.Value = username;

			SqlParameter paraRoleName = cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar);
			paraRoleName.Value = roleName;

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

		public Boolean IsUserInRole(String roleName, String username)
		{
			SqlConnection conn = this.CreateConnection();
			SqlCommand cmd = new SqlCommand("select COUNT(Username) from sampleUserRole where (Username=@Username) and (RoleName=@RoleName)", conn);

			SqlParameter paraUsername = cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
			paraUsername.Value = username;

			SqlParameter paraRoleName = cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar);
			paraRoleName.Value = roleName;

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

		public String[] GetRolesOfUser(String username)
		{
			SqlConnection conn = this.CreateConnection();
			SqlCommand cmd = new SqlCommand("select RoleName from sampleUserRole where Username=@Username", conn);

			SqlParameter paraUsername = cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
			paraUsername.Value = username;

			try
			{
				conn.Open();

				SqlDataReader reader = cmd.ExecuteReader();
				ArrayList roles = new ArrayList();
				while(reader.Read())
				{
					roles.Add(reader.GetString(0));
				}
				
				return (String[]) roles.ToArray(typeof(String));
			}
			finally
			{
				conn.Close();
			}
		}


	}
}
