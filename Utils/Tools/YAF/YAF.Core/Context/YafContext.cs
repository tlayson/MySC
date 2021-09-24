/* Yet Another Forum.NET
 * Copyright (C) 2006-2013 Jaben Cargman
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

namespace YAF.Core
{
    #region Using

    using System;
    using System.Web;
    using System.Web.Security;

    using Autofac;

    using YAF.Classes;
    using YAF.Classes.Pattern;
    using YAF.Core.Services;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.EventProxies;
    using YAF.Types.Extensions;
    using YAF.Types.Interfaces;
    using YAF.Utils;
    using YAF.Utils.Helpers;

    #endregion

    /// <summary>
    /// Context class that accessible with the same instance from all locations
    /// </summary>
    public class YafContext : UserPageBase, IDisposable, IHaveServiceLocator
    {
        #region Constants and Fields

        /// <summary>
        /// The _context lifetime container.
        /// </summary>
        protected ILifetimeScope _contextLifetimeContainer;

        /// <summary>
        /// The _current forum page.
        /// </summary>
        protected ForumPage _currentForumPage;

        /// <summary>
        /// The _repository.
        /// </summary>
        protected ContextVariableRepository _repository;

        /// <summary>
        /// The _user.
        /// </summary>
        protected MembershipUser _user;

        /// <summary>
        /// The _variables.
        /// </summary>
        protected TypeDictionary _variables = new TypeDictionary();

        /// <summary>
        /// The _combined user data.
        /// </summary>
        private CombinedUserDataHelper _combinedUserData;

        /// <summary>
        /// The _load message.
        /// </summary>
        private LoadMessage _loadMessage;

        /// <summary>
        /// The _page elements.
        /// </summary>
        private PageElementRegister _pageElements;

        /// <summary>
        /// The _query i ds.
        /// </summary>
        private QueryStringIDHelper _queryIDs;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="YafContext"/> class. YafContext Constructor
        /// </summary>
        /// <param name="contextLifetimeContainer">
        /// The context Lifetime Container.
        /// </param>
        internal YafContext(ILifetimeScope contextLifetimeContainer)
        {
            this._contextLifetimeContainer = contextLifetimeContainer;

            // init the respository
            this._repository = new ContextVariableRepository(this._variables);

            // init context...
            if (this.Init != null)
            {
                this.Init(this, new EventArgs());
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The after init.
        /// </summary>
        public event EventHandler<EventArgs> AfterInit;

        /// <summary>
        /// The before init.
        /// </summary>
        public event EventHandler<EventArgs> BeforeInit;

        /// <summary>
        /// On YafContext Constructor Call
        /// </summary>
        public event EventHandler<EventArgs> Init;

        /// <summary>
        /// On YafContext Unload Call
        /// </summary>
        public event EventHandler<EventArgs> Unload;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the current application.
        /// </summary>
        [Obsolete("Use Service Location: this.Get<HttpApplicationStateBase>()")]
        public static HttpApplicationStateBase Application
        {
            get
            {
                return GlobalContainer.Container.Resolve<HttpApplicationStateBase>();
            }
        }

        /// <summary>
        /// Gets the instance of the Forum Context
        /// </summary>
        public static YafContext Current
        {
            get
            {
                return GlobalContainer.Container.Resolve<YafContext>();
            }
        }

        /// <summary>
        /// Gets or sets the Current Board Settings
        /// </summary>
        public YafBoardSettings BoardSettings
        {
            get
            {
                return this.Get<YafBoardSettings>();
            }

            set
            {
                this.Get<CurrentBoardSettings>().Instance = value;
            }
        }

        /// <summary>
        /// Gets or sets the Forum page instance of the current forum page. May not be valid until everything is initialized.
        /// </summary>
        public ForumPage CurrentForumPage
        {
            get
            {
                return this._currentForumPage;
            }

            set
            {
                this._currentForumPage = value;
            }
        }

        /// <summary>
        /// Gets the Instance of the Combined UserData for the current user.
        /// </summary>
        public IUserData CurrentUserData
        {
            get
            {
                return this._combinedUserData ?? (this._combinedUserData = new CombinedUserDataHelper());
            }
        }

        /// <summary>
        /// Gets the current page as the forumPage Enum (for comparison)
        /// </summary>
        public ForumPages ForumPageType
        {
            get
            {
                if (this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("g").IsNotSet())
                {
                    return ForumPages.forum;
                }

                try
                {
                    return this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("g").ToEnum<ForumPages>(true);
                }
                catch (Exception)
                {
                    return ForumPages.forum;
                }
            }
        }

        /// <summary>
        /// Gets the Access to the Context Global Variable Repository Class which is a helper class that accesses YafContext.Vars with strongly typed properties for primary variables.
        /// </summary>
        public ContextVariableRepository Globals
        {
            get
            {
                return this._repository;
            }
        }

        /// <summary>
        /// Gets the current Page Load Message
        /// </summary>
        public LoadMessage LoadMessage
        {
            get
            {
                return this._loadMessage ?? (this._loadMessage = new LoadMessage());
            }
        }

        /// <summary>
        /// Gets the Current Page Elements
        /// </summary>
        public PageElementRegister PageElements
        {
            get
            {
                return this._pageElements ?? (this._pageElements = new PageElementRegister());
            }
        }

        /// <summary>
        /// Gets the Current Page User Profile
        /// </summary>
        public YafUserProfile Profile
        {
            get
            {
                return (YafUserProfile)HttpContext.Current.Profile;
            }
        }

        /// <summary>
        /// Gets or sets the Current Page Query ID Helper
        /// </summary>
        public QueryStringIDHelper QueryIDs
        {
            get
            {
                return this._queryIDs;
            }

            set
            {
                this._queryIDs = value;
            }
        }

        /// <summary>
        /// Gets the Provides access to the Service Locatorer
        /// </summary>
        public IServiceLocator ServiceLocator
        {
            get
            {
                return this._contextLifetimeContainer.Resolve<IServiceLocator>();
            }
        }

        /// <summary>
        /// Gets the Current Page Control Settings from Forum Control
        /// </summary>
        public YafControlSettings Settings
        {
            get
            {
                return YafControlSettings.Current;
            }
        }

        /// <summary>
        /// Gets the UrlBuilder
        /// </summary>
        public IUrlBuilder UrlBuilder
        {
            get
            {
                return YafFactoryProvider.UrlBuilder;
            }
        }

        /// <summary>
        /// Gets or sets the Current Membership User
        /// </summary>
        public MembershipUser User
        {
            get
            {
                return this._user ?? (this._user = UserMembershipHelper.GetUser(true));
            }

            set
            {
                this._user = value;
            }
        }

        /// <summary>
        /// Gets the YafContext Global Instance Variables Use for plugins or other situations where a value is needed per instance.
        /// </summary>
        public TypeDictionary Vars
        {
            get
            {
                return this._variables;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Returns a value from the YafContext Global Instance Variables (Vars) collection.
        /// </summary>
        /// <returns>
        /// Value if it's found, null if it doesn't exist. 
        /// </returns>
        public object this[[NotNull] string varName]
        {
            get
            {
                return this._variables.ContainsKey(varName) ? this._variables[varName] : null;
            }

            set
            {
                this._variables[varName] = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Helper Function that adds a "load message" to the load message class.
        /// </summary>
        /// <param name="message">
        /// The message. 
        /// </param>
        /// <param name="messageType">
        /// The message type. 
        /// </param>
        public void AddLoadMessage([NotNull] string message, MessageTypes messageType = MessageTypes.Information)
        {
            this.LoadMessage.Add(message, messageType);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            if (this.Unload != null)
            {
                this.Unload(this, new EventArgs());
            }

            this._contextLifetimeContainer.Dispose();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize the user data and page data...
        /// </summary>
        protected override void InitUserAndPage()
        {
            if (this._initUserPage)
            {
                return;
            }

            if (this.BeforeInit != null)
            {
                this.BeforeInit(this, new EventArgs());
            }

            if (this.User != null
                && (this.Get<HttpSessionStateBase>()["UserUpdated"] == null
                    || this.Get<HttpSessionStateBase>()["UserUpdated"].ToString() != this.User.UserName))
            {
                RoleMembershipHelper.UpdateForumUser(this.User, this.PageBoardID);
                this.Get<HttpSessionStateBase>()["UserUpdated"] = this.User.UserName;
            }

            var pageLoadEvent = new InitPageLoadEvent();

            this.Get<IRaiseEvent>().Raise(pageLoadEvent);

            this.Page = pageLoadEvent.DataDictionary;

            if (this.AfterInit != null)
            {
                this.AfterInit(this, new EventArgs());
            }
        }

        #endregion
    }
}