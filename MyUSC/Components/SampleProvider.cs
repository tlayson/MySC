using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Web;

using CuteChat;

namespace SamplePortal.Components
{
	public class SampleProvider : ChatProvider
	{

		#region		//TODO:integration code for friend list:
//		public override AppDataManager CreateDataManagerInstance(AppPortal portal)
//		{
//			return new MyDataManager(portal);
//		}
//
//		class MyDataManager : AppDataManager
//		{
//			public override void AddContact(ChatIdentity identity, string userid)
//			{
//				string myname=ChatProvider.Instance.FromUserId(identity.UniqueId);
//				string friendname=ChatProvider.Instance.FromUserId(userid);
//				MyCommunityDatabase.AddFriend(myname);
//				this.OnContactAdded(identity,userid,friendname);
//			}
//			public override void RemoveContact(ChatIdentity identity, string userid)
//			{
//				//get the username from cutechat's userid
//				string myname=ChatProvider.Instance.FromUserId(identity.UniqueId);
//				string friendname=ChatProvider.Instance.FromUserId(userid);
//
//				//my custom implementation
//				MyCommunityDatabase.RemoveFriend(myname,friendname);
//
//				//tell cutechat that the relative have been removed.
//				this.OnContactRemoved(identity,userid);
//			}
//			public override IChatUserInfo[] GetContacts(ChatIdentity identity)
//			{
//				string myname=ChatProvider.Instance.FromUserId(identity.UniqueId);
//				string[] friends=MyCommunityDatabase.GetFriends(myname);
//				IChatUserInfo[] arr=new IChatUserInfo[friends.Length];
//				for(int i=0;i<friends.Length;i++)
//				{
//					string friendid=ChatProvider.Instance.ToUserId(friends[i]);
//					arr[i]=base.GetUserInfo(friendid);
//				}
//				return arr;
//			}
//
//			public override void AddIgnore(ChatIdentity identity, string userid)
//			{
//				base.AddIgnore (identity, userid);
//			}
//			public override void RemoveIgnore(ChatIdentity identity, string userid)
//			{
//				base.RemoveIgnore (identity, userid);
//			}
//			public override IChatUserInfo[] GetIgnores(ChatIdentity identity)
//			{
//				return base.GetIgnores (identity);
//			}
//
//
//			public MyDataManager(AppPortal portal):base(portal)
//			{
//			}
//		}
		#endregion

//		public override string GetConnectionString()
//		{
//			return System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
//		}


		/// <summary>
		/// Get the information of the current user
		/// </summary>
		public override AppChatIdentity GetLogonIdentity()
		{
			HttpContext context=HttpContext.Current;
			if(context!=null)
			{
				if(context.User.Identity.IsAuthenticated)
				{
					string loginName=context.User.Identity.Name;
					string cachekey="NickName:"+loginName;
					string userid=ToUserId(loginName);
					string nickName=null;

					bool exists=GetUserInfo(loginName,ref nickName);
					if(!exists)
						return null;
					
					return new AppChatIdentity(nickName,false,userid,context.Request.UserHostAddress);
				}
			}
			return null;
		}

		/// <summary>
		/// find the username by the displayname
		/// </summary>
		public override string FindUserLoginName(string nickName)
		{
			UserData userdata=new UserData();
			if(userdata.IsUsernameExists(nickName))
				return nickName;
			return null;
		}

		public override bool GetUserInfo(string loginName, ref string nickName)
		{
			UserData userdata=new UserData();
			if(!userdata.IsUsernameExists(loginName))
				return false;

			nickName=loginName;

			return true;
		}

		/// <summary>
		/// get the information from the user
		/// This function is very important and be called very frequently. 
		/// </summary>
		public override bool GetUserInfo(string loginName, ref string nickName, ref bool isAdmin)
		{
			UserData userdata=new UserData();
			if(!userdata.IsUsernameExists(loginName))
				return false;

			nickName=loginName;

			isAdmin=SamplePortal.Global.IsUserInRole("Admins",loginName);

			return true;
		}

		/// <summary>
		/// validate the user , and set the cookie
		/// </summary>
		public override bool ValidateUser(string loginName, string pwd)
		{
			UserData userdata=new UserData();
			if(!userdata.Login(loginName,pwd))
				return false;

			System.Web.Security.FormsAuthentication.SetAuthCookie(loginName,false,"/");

			return true;
		}




	}

}
