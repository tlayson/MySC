﻿/* Yet Another Forum.net
 * Copyright (C) 2003-2005 Bj�rnar Henden
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

namespace YAF.Pages
{
    #region Using

    using System;
    using System.Data;
    using System.Linq;
    using System.Web;

    using YAF.Classes;
    using YAF.Classes.Data;
    using YAF.Core;
    using YAF.Core.Extensions;
    using YAF.Core.Model;
    using YAF.Core.Services;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Extensions;
    using YAF.Types.Flags;
    using YAF.Types.Interfaces;
    using YAF.Types.Interfaces.Data;
    using YAF.Types.Models;
    using YAF.Utils;
    using YAF.Utils.Helpers;

    #endregion

    /// <summary>
    /// The topics list page
    /// </summary>
    public partial class topics : ForumPage
    {
        #region Constants and Fields

        /// <summary>
        ///   The _show topic list selected.
        /// </summary>
        private int _showTopicListSelected;

        /// <summary>
        ///   The _forum.
        /// </summary>
        private DataRow _forum;

        /// <summary>
        ///   The _forum flags.
        /// </summary>
        private ForumFlags _forumFlags;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "topics" /> class. 
        ///   Overloads the topics page.
        /// </summary>
        public topics()
            : base("TOPICS")
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the last post image TT.
        /// </summary>
        /// <value>
        /// The last post image TT.
        /// </value>
        public string LastPostImageTT { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The style transform func wrap.
        /// </summary>
        /// <param name="dt">
        /// The DateTable
        /// </param>
        /// <returns>
        /// The style transform wrap.
        /// </returns>
        public DataTable StyleTransformDataTable([NotNull] DataTable dt)
        {
            if (this.Get<YafBoardSettings>().UseStyledNicks)
            {
                var styleTransform = this.Get<IStyleTransform>();
                styleTransform.DecodeStyleByTable(dt, false, new string[] { "StarterStyle", "LastUserStyle" });
            }

            return dt;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the sub forum title.
        /// </summary>
        /// <returns>The get sub forum title.</returns>
        protected string GetSubForumTitle()
        {
            return this.GetTextFormatted("SUBFORUMS", this.HtmlEncode(this.PageContext.PageForumName));
        }

        /// <summary>
        /// The new topic_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void NewTopic_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this._forumFlags.IsLocked)
            {
                this.PageContext.AddLoadMessage(this.GetText("WARN_FORUM_LOCKED"));
                return;
            }

            if (!this.PageContext.ForumPostAccess)
            {
                YafBuildLink.AccessDenied(/*"You don't have access to post new topics in this forum."*/);
            }
        }

        /// <summary>
        /// The Forum Search
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ForumSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.forumSearch.Text))
            {
                return;
            }

            YafBuildLink.Redirect(
                ForumPages.search,
                "search={0}&forum={1}",
                this.forumSearch.Text.TrimWordsOverMaxLengthWordsPreserved(
                    this.Get<YafBoardSettings>().SearchStringMaxLength),
                this.PageContext.PageForumID);
        }

        /// <summary>
        /// The initialization script for the topics page.
        /// </summary>
        /// <param name="e">
        /// The EventArgs object for the topics page.
        /// </param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.Unload += this.Topics_Unload;

            this.ShowList.SelectedIndexChanged += this.ShowList_SelectedIndexChanged;
            this.MarkRead.Click += this.MarkRead_Click;
            this.Pager.PageChange += this.Pager_PageChange;
            this.NewTopic1.Click += this.NewTopic_Click;
            this.NewTopic2.Click += this.NewTopic_Click;
            this.WatchForum.Click += this.WatchForum_Click;

            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            base.OnInit(e);
        }

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Get<IYafSession>().UnreadTopics = 0;
            this.AtomFeed.AdditionalParameters =
                "f={0}".FormatWith(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("f"));
            this.RssFeed.AdditionalParameters =
                "f={0}".FormatWith(this.Get<HttpRequestBase>().QueryString.GetFirstOrDefault("f"));
            this.MarkRead.Text = this.GetText("MARKREAD");
            this.ForumJumpHolder.Visible = this.Get<YafBoardSettings>().ShowForumJump
                                           && this.PageContext.Settings.LockedForum == 0;

            this.LastPostImageTT = this.GetText("DEFAULT", "GO_LAST_POST");

            if (this.ForumSearchHolder.Visible)
            {
                this.forumSearch.Attributes["onkeydown"] =
                    "if(event.which || event.keyCode){{if ((event.which == 13) || (event.keyCode == 13)) {{document.getElementById('{0}').click();return false;}}}} else {{return true}}; "
                        .FormatWith(this.forumSearchOK.ClientID);
            }

            if (!this.IsPostBack)
            {
                // PageLinks.Clear();
                if (this.PageContext.Settings.LockedForum == 0)
                {
                    this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
                    this.PageLinks.AddLink(
                        this.PageContext.PageCategoryName,
                        YafBuildLink.GetLink(ForumPages.forum, "c={0}", this.PageContext.PageCategoryID));
                }

                this.PageLinks.AddForumLinks(this.PageContext.PageForumID, true);

                this.ShowList.DataSource = StaticDataHelper.TopicTimes();
                this.ShowList.DataTextField = "TopicText";
                this.ShowList.DataValueField = "TopicValue";
                this._showTopicListSelected = (this.Get<IYafSession>().ShowList == -1)
                                                  ? this.Get<YafBoardSettings>().ShowTopicsDefault
                                                  : this.Get<IYafSession>().ShowList;

                this.moderate1.NavigateUrl =
                    this.moderate2.NavigateUrl =
                    YafBuildLink.GetLinkNotEscaped(ForumPages.moderating, "f={0}", this.PageContext.PageForumID);

                this.NewTopic1.NavigateUrl =
                    this.NewTopic2.NavigateUrl =
                    YafBuildLink.GetLinkNotEscaped(ForumPages.postmessage, "f={0}", this.PageContext.PageForumID);

                this.HandleWatchForum();
            }

            if (this.Request.QueryString.GetFirstOrDefault("f") == null)
            {
                YafBuildLink.AccessDenied();
            }

            if (this.PageContext.IsGuest && !this.PageContext.ForumReadAccess)
            {
                // attempt to get permission by redirecting to login...
                this.Get<IPermissions>().HandleRequest(ViewPermissions.RegisteredUsers);
            }
            else if (!this.PageContext.ForumReadAccess)
            {
                YafBuildLink.AccessDenied();
            }

            using (DataTable dt = LegacyDb.forum_list(this.PageContext.PageBoardID, this.PageContext.PageForumID))
            {
                this._forum = dt.Rows[0];
            }

            if (this._forum["RemoteURL"] != DBNull.Value)
            {
                this.Response.Clear();
                this.Response.Redirect((string)this._forum["RemoteURL"]);
            }

            this._forumFlags = new ForumFlags(this._forum["Flags"]);

            this.PageTitle.Text = this.HtmlEncode(this._forum["Name"]);

            this.BindData(); // Always because of yaf:TopicLine

            if (!this.PageContext.ForumPostAccess
                || (this._forumFlags.IsLocked && !this.PageContext.ForumModeratorAccess))
            {
                this.NewTopic1.Visible = false;
                this.NewTopic2.Visible = false;
            }

            if (this.PageContext.ForumModeratorAccess)
            {
                return;
            }

            this.moderate1.Visible = false;
            this.moderate2.Visible = false;
        }

        /// <summary>
        /// The watch forum_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void WatchForum_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (!this.PageContext.ForumReadAccess)
            {
                return;
            }

            if (this.PageContext.IsGuest)
            {
                this.PageContext.AddLoadMessage(this.GetText("WARN_LOGIN_FORUMWATCH"));
                return;
            }

            if (this.WatchForumID.InnerText == string.Empty)
            {
                this.GetRepository<WatchForum>().Add(this.PageContext.PageUserID, this.PageContext.PageForumID);

                this.PageContext.AddLoadMessage(this.GetText("INFO_WATCH_FORUM"));
            }
            else
            {
                var tmpID = this.WatchForumID.InnerText.ToType<int>();
                this.GetRepository<WatchForum>().DeleteByID(tmpID);

                this.PageContext.AddLoadMessage(this.GetText("INFO_UNWATCH_FORUM"));
            }

            this.HandleWatchForum();
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            DataSet ds = this.Get<YafDbBroker>().BoardLayout(
                this.PageContext.PageBoardID,
                this.PageContext.PageUserID,
                this.PageContext.PageCategoryID,
                this.PageContext.PageForumID);
            if (ds.Tables["Forum"].Rows.Count > 0)
            {
                this.ForumList.DataSource = ds.Tables["Forum"].Rows;
                this.SubForums.Visible = true;
            }

            this.Pager.PageSize = this.Get<YafBoardSettings>().TopicsPerPage;

            // when userId is null it returns the count of all deleted messages
            int? userId = null;

            // get the userID to use for the deleted posts count...
            if (!this.Get<YafBoardSettings>().ShowDeletedMessagesToAll)
            {
                // only show deleted messages that belong to this user if they are not admin/mod
                if (!this.PageContext.IsAdmin && !this.PageContext.ForumModeratorAccess)
                {
                    userId = this.PageContext.PageUserID;
                }
            }

            DataTable dt = LegacyDb.announcements_list(
                this.PageContext.PageForumID,
                userId,
                null,
                DateTime.UtcNow,
                0,
                10,
                this.Get<YafBoardSettings>().UseStyledNicks,
                true,
                this.Get<YafBoardSettings>().UseReadTrackingByDatabase);
            if (dt != null)
            {
                dt = this.StyleTransformDataTable(dt);
            }

            int baseSize = this.Get<YafBoardSettings>().TopicsPerPage;

            this.Announcements.DataSource = dt;
            /*if (!m_bIgnoreQueryString && Request.QueryString["p"] != null)
            {
                    // show specific page (p is 1 based)
                    int tPage = (int)Security.StringToLongOrRedirect(Request.QueryString["p"]);

                    if (tPage > 0)
                    {
                            Pager.CurrentPageIndex = tPage - 1;
                    }
            }*/

            int nCurrentPageIndex = this.Pager.CurrentPageIndex;

            DataTable dtTopics;

            if (this._showTopicListSelected == 0)
            {
                dtTopics = LegacyDb.topic_list(
                    this.PageContext.PageForumID,
                    userId,
                    DateTimeHelper.SqlDbMinTime(),
                    DateTime.UtcNow,
                    nCurrentPageIndex,
                    baseSize,
                    this.Get<YafBoardSettings>().UseStyledNicks,
                    true,
                    this.Get<YafBoardSettings>().UseReadTrackingByDatabase);
                if (dtTopics != null)
                {
                    dtTopics = this.StyleTransformDataTable(dtTopics);
                }
            }
            else
            {
                int[] days = new[] { 1, 2, 7, 14, 31, 2 * 31, 6 * 31, 356 };

                DateTime date = DateTime.UtcNow.AddDays(-days[this._showTopicListSelected]);

                dtTopics = LegacyDb.topic_list(
                    this.PageContext.PageForumID,
                    userId,
                    date,
                    DateTime.UtcNow,
                    nCurrentPageIndex,
                    baseSize,
                    this.Get<YafBoardSettings>().UseStyledNicks,
                    true,
                    this.Get<YafBoardSettings>().UseReadTrackingByDatabase);

                if (dtTopics != null)
                {
                    dtTopics = this.StyleTransformDataTable(dtTopics);
                }
            }

            this.TopicList.DataSource = dtTopics;

            this.DataBind();

            // setup the show topic list selection after data binding
            this.ShowList.SelectedIndex = this._showTopicListSelected;
            this.Get<IYafSession>().ShowList = this._showTopicListSelected;
            if (dtTopics != null && dtTopics.Rows.Count > 0)
            {
                this.Pager.Count = dtTopics.AsEnumerable().First().Field<int>("TotalRows");
            }
        }

        /// <summary>
        /// The handle watch forum.
        /// </summary>
        private void HandleWatchForum()
        {
            if (this.PageContext.IsGuest || !this.PageContext.ForumReadAccess)
            {
                return;
            }

            // check if this forum is being watched by this user
            var watchForumId = this.GetRepository<WatchForum>().Check(this.PageContext.PageUserID, this.PageContext.PageForumID);
            if (watchForumId.HasValue)
            {
                // subscribed to this forum
                this.WatchForum.Text = this.GetText("unwatchforum");
                this.WatchForumID.InnerText = watchForumId.Value.ToString();
            }
            else
            {
                // not subscribed
                this.WatchForumID.InnerText = string.Empty;
                this.WatchForum.Text = this.GetText("watchforum");
            }
        }

        /// <summary>
        /// The mark read_ click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MarkRead_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.Get<IReadTrackCurrentUser>().SetForumRead(this.PageContext.PageForumID);
            this.BindData();
        }

        /// <summary>
        /// The pager_ page change.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Pager_PageChange([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.SmartScroller1.Reset();
            this.BindData();
        }

        /// <summary>
        /// The show list_ selected index changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ShowList_SelectedIndexChanged([NotNull] object sender, [NotNull] EventArgs e)
        {
            this._showTopicListSelected = this.ShowList.SelectedIndex;
            this.BindData();
        }

        /// <summary>
        /// The Topics unload.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Topics_Unload([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.Get<IYafSession>().UnreadTopics == 0)
            {
                this.Get<IReadTrackCurrentUser>().SetForumRead(this.PageContext.PageForumID);
            }
        }

        #endregion
    }
}