﻿/* Yet Another Forum.NET
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

namespace YAF.Pages.Admin
{
    #region Using

    using System;
    using System.Data;
    using System.Linq;
    using System.Web.UI.WebControls;

    using YAF.Classes;
    using YAF.Classes.Data;
    using YAF.Core;
    using YAF.Core.Model;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Extensions;
    using YAF.Types.Interfaces;
    using YAF.Types.Models;
    using YAF.Types.Objects;
    using YAF.Utils;
    using YAF.Utils.Helpers;

    #endregion

    /// <summary>
    /// The Board Settings Admin Page.
    /// </summary>
    public partial class boardsettings : AdminPage
    {
        #region Methods

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }

            var boardSettings = this.Get<YafBoardSettings>();

            this.PageLinks.AddLink(boardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinks.AddLink(
                this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
            this.PageLinks.AddLink(this.GetText("ADMIN_BOARDSETTINGS", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1}".FormatWith(
                this.GetText("ADMIN_ADMIN", "Administration"), this.GetText("ADMIN_BOARDSETTINGS", "TITLE"));

            this.Save.Text = this.GetText("COMMON", "SAVE");

            // create list boxes by populating datasources from Data class
            var themeData = StaticDataHelper.Themes().AsEnumerable().Where(x => !x.Field<bool>("IsMobile"));

            if (themeData.Any())
            {
                this.Theme.DataSource = themeData.CopyToDataTable();
                this.Theme.DataTextField = "Theme";
                this.Theme.DataValueField = "FileName";
            }

            var mobileThemeData = StaticDataHelper.Themes().AsEnumerable().Where(x => x.Field<bool>("IsMobile"));

            if (mobileThemeData.Any())
            {
                var mobileThemes = mobileThemeData.CopyToDataTable();

                // Add Dummy Disabled Mobile Theme Item to allow disabling the Mobile Theme
                DataRow dr = mobileThemes.NewRow();
                dr["Theme"] = "[ {0} ]".FormatWith(this.GetText("ADMIN_COMMON", "DISABLED"));

                dr["FileName"] = string.Empty;
                dr["IsMobile"] = false;

                mobileThemes.Rows.InsertAt(dr, 0);

                this.MobileTheme.DataSource = mobileThemes;
                this.MobileTheme.DataTextField = "Theme";
                this.MobileTheme.DataValueField = "FileName";
            }

            this.Culture.DataSource =
                StaticDataHelper.Cultures().AsEnumerable().OrderBy(x => x.Field<string>("CultureNativeName")).
                    CopyToDataTable();

            this.Culture.DataTextField = "CultureNativeName";
            this.Culture.DataValueField = "CultureTag";

            this.ShowTopic.DataSource = StaticDataHelper.TopicTimes();
            this.ShowTopic.DataTextField = "TopicText";
            this.ShowTopic.DataValueField = "TopicValue";

            this.FileExtensionAllow.DataSource = StaticDataHelper.AllowDisallow();
            this.FileExtensionAllow.DataTextField = "Text";
            this.FileExtensionAllow.DataValueField = "Value";

            this.JqueryUITheme.DataSource = StaticDataHelper.JqueryUIThemes();
            this.JqueryUITheme.DataTextField = "Theme";
            this.JqueryUITheme.DataValueField = "Theme";

            this.BindData();

            // bind poll group list
            var pollGroup =
                LegacyDb.PollGroupList(this.PageContext.PageUserID, null, this.PageContext.PageBoardID)
                        .Distinct(new AreEqualFunc<TypedPollGroup>((v1, v2) => v1.PollGroupID == v2.PollGroupID))
                        .ToList();

            pollGroup.Insert(0, new TypedPollGroup(string.Empty, -1));

            // TODO: vzrus needs some work, will be in polls only until feature is debugged there.
            this.PollGroupListDropDown.Items.AddRange(pollGroup.Select(x => new ListItem(x.Question, x.PollGroupID.ToString())).ToArray());

            // population default notification setting options...
            var items = EnumHelper.EnumToDictionary<UserNotificationSetting>();

            if (!boardSettings.AllowNotificationAllPostsAllTopics)
            {
                // remove it...
                items.Remove(UserNotificationSetting.AllTopics.ToInt());
            }

            var notificationItems =
                items.Select(x => new ListItem(HtmlHelper.StripHtml(this.GetText("CP_SUBSCRIPTIONS", x.Value)), x.Key.ToString())).ToArray();

            this.DefaultNotificationSetting.Items.AddRange(notificationItems);

            SetSelectedOnList(ref this.JqueryUITheme, boardSettings.JqueryUITheme);

            // Get first default full culture from a language file tag.
            string langFileCulture = StaticDataHelper.CultureDefaultFromFile(boardSettings.Language)
                                     ?? "en-US";

            SetSelectedOnList(ref this.Theme, boardSettings.Theme);
            SetSelectedOnList(ref this.MobileTheme, boardSettings.MobileTheme);

            // If 2-letter language code is the same we return Culture, else we return  a default full culture from language file
            /* SetSelectedOnList(
                ref this.Culture, 
                langFileCulture.Substring(0, 2) == this.Get<YafBoardSettings>().Culture
                  ? this.Get<YafBoardSettings>().Culture
                  : langFileCulture);*/
            SetSelectedOnList(ref this.Culture, boardSettings.Culture);
            if (this.Culture.SelectedIndex == 0)
            {
                // If 2-letter language code is the same we return Culture, else we return  a default full culture from language file
                SetSelectedOnList(
                    ref this.Culture, langFileCulture.Substring(0, 2) == boardSettings.Culture ? boardSettings.Culture : langFileCulture);
            }

            SetSelectedOnList(ref this.ShowTopic, boardSettings.ShowTopicsDefault.ToString());
            SetSelectedOnList(
                ref this.FileExtensionAllow, boardSettings.FileExtensionAreAllowed ? "0" : "1");

            SetSelectedOnList(
                ref this.DefaultNotificationSetting,
                boardSettings.DefaultNotificationSetting.ToInt().ToString());

            this.NotificationOnUserRegisterEmailList.Text =
                boardSettings.NotificationOnUserRegisterEmailList;
            this.AllowThemedLogo.Checked = boardSettings.AllowThemedLogo;
            this.EmailModeratorsOnModeratedPost.Checked = boardSettings.EmailModeratorsOnModeratedPost;
            this.EmailModeratorsOnReportedPost.Checked = boardSettings.EmailModeratorsOnReportedPost;
            this.AllowDigestEmail.Checked = boardSettings.AllowDigestEmail;
            this.DefaultSendDigestEmail.Checked = boardSettings.DefaultSendDigestEmail;
            this.JqueryUIThemeCDNHosted.Checked = boardSettings.JqueryUIThemeCDNHosted;
            this.ForumEmail.Text = boardSettings.ForumEmail;

            this.CopyrightRemovalKey.Text = boardSettings.CopyrightRemovalDomainKey;

            this.DigestSendEveryXHours.Text = boardSettings.DigestSendEveryXHours.ToString();

            if (boardSettings.BoardPollID > 0)
            {
                this.PollGroupListDropDown.SelectedValue = boardSettings.BoardPollID.ToString();
            }
            else
            {
                this.PollGroupListDropDown.SelectedIndex = 0;
            }

            this.PollGroupList.Visible = true;

            // Copyright Linkback Algorithm
            // Please keep if you haven't purchased a removal or commercial license.
            this.CopyrightHolder.Visible = true;
        }

        /// <summary>
        /// Save the Board Settings
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Save_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            string languageFile = "english.xml";

            var cultures =
                StaticDataHelper.Cultures().AsEnumerable().Where(
                    c => c.Field<string>("CultureTag").Equals(this.Culture.SelectedValue));

            if (cultures.Any())
            {
                languageFile = cultures.First().Field<string>("CultureFile");
            }

            this.GetRepository<Board>()
                .Save(this.PageContext.PageBoardID, this.Name.Text, languageFile, this.Culture.SelectedValue, this.AllowThreaded.Checked);

            // save poll group
            var boardSettings = this.Get<YafBoardSettings>();

            boardSettings.BoardPollID = this.PollGroupListDropDown.SelectedIndex.ToType<int>() > 0
                                                           ? this.PollGroupListDropDown.SelectedValue.ToType<int>()
                                                           : 0;

            boardSettings.Language = languageFile;
            boardSettings.Culture = this.Culture.SelectedValue;
            boardSettings.Theme = this.Theme.SelectedValue;

            // allow null/empty as a mobile theme many not be desired.
            boardSettings.MobileTheme = this.MobileTheme.SelectedValue ?? string.Empty;

            boardSettings.ShowTopicsDefault = this.ShowTopic.SelectedValue.ToType<int>();
            boardSettings.AllowThemedLogo = this.AllowThemedLogo.Checked;
            boardSettings.FileExtensionAreAllowed = this.FileExtensionAllow.SelectedValue.ToType<int>()
                                                                   == 0;
            boardSettings.NotificationOnUserRegisterEmailList =
                this.NotificationOnUserRegisterEmailList.Text.Trim();

            boardSettings.EmailModeratorsOnModeratedPost = this.EmailModeratorsOnModeratedPost.Checked;
            boardSettings.EmailModeratorsOnReportedPost = this.EmailModeratorsOnReportedPost.Checked;
            boardSettings.AllowDigestEmail = this.AllowDigestEmail.Checked;
            boardSettings.DefaultSendDigestEmail = this.DefaultSendDigestEmail.Checked;
            boardSettings.DefaultNotificationSetting =
                this.DefaultNotificationSetting.SelectedValue.ToEnum<UserNotificationSetting>();

            boardSettings.ForumEmail = this.ForumEmail.Text;
            boardSettings.CopyrightRemovalDomainKey = this.CopyrightRemovalKey.Text.Trim();
            boardSettings.JqueryUITheme = this.JqueryUITheme.SelectedValue;
            boardSettings.JqueryUIThemeCDNHosted = this.JqueryUIThemeCDNHosted.Checked;

            int hours;

            if (!int.TryParse(this.DigestSendEveryXHours.Text, out hours))
            {
                hours = 24;
            }

            boardSettings.DigestSendEveryXHours = hours;

            // save the settings to the database
            ((YafLoadBoardSettings)boardSettings).SaveRegistry();

            // Reload forum settings
            this.PageContext.BoardSettings = null;

            // Clearing cache with old users permissions data to get new default styles...
            this.Get<IDataCache>().Remove(x => x.StartsWith(Constants.Cache.ActiveUserLazyData));
            YafBuildLink.Redirect(ForumPages.admin_admin);
        }

        /// <summary>
        /// The set selected on list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="value">The value.</param>
        private static void SetSelectedOnList([NotNull] ref DropDownList list, [NotNull] string value)
        {
            ListItem selItem = list.Items.FindByValue(value);

            if (selItem != null)
            {
                selItem.Selected = true;
            }
            else if (list.Items.Count > 0)
            {
                // select the first...
                list.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            DataRow row;
            using (DataTable dt = this.GetRepository<Board>().List(this.PageContext.PageBoardID))
            {
                row = dt.Rows[0];
            }

            this.DataBind();
            this.Name.Text = row["Name"].ToString();
            this.AllowThreaded.Checked = Convert.ToBoolean(row["AllowThreaded"]);
        }

        #endregion
    }
}