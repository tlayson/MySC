﻿/* YetAnotherForum.NET
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
namespace YAF.Modules
{
    #region Using

    using System;
    using System.Web;

    using YAF.Classes;
    using YAF.Controls;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Attributes;
    using YAF.Types.Constants;
    using YAF.Types.Extensions;
    using YAF.Types.Interfaces;
    using YAF.Utils;

    #endregion

    /// <summary>
    /// The Page PM Popup Module
    /// </summary>
    [YafModule("Page Title Module", "Tiny Gecko", 1)]
    public class PagePmPopupForumModule : SimpleBaseForumModule
    {
        #region Public Methods

        /// <summary>
        /// The init after page.
        /// </summary>
        public override void InitAfterPage()
        {
            this.CurrentForumPage.Load += this.ForumPage_Load;
        }

        /// <summary>
        /// The init before page.
        /// </summary>
        public override void InitBeforePage()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Displays the PM popup.
        /// </summary>
        /// <returns>
        /// The display pm popup.
        /// </returns>
        protected bool DisplayPMPopup()
        {
            return (this.PageContext.UnreadPrivate > 0)
                   && (this.PageContext.LastUnreadPm > this.Get<IYafSession>().LastPm);
        }

        /// <summary>
        /// The last pending buddies.
        /// </summary>
        /// <returns>
        /// whether we should display the pending buddies notification or not
        /// </returns>
        protected bool DisplayPendingBuddies()
        {
            return (this.PageContext.PendingBuddies > 0)
                   && (this.PageContext.LastPendingBuddies > this.Get<IYafSession>().LastPendingBuddies);
        }

        /// <summary>
        /// Handles the Load event of the ForumPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ForumPage_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.GeneratePopUp();
        }

        /// <summary>
        /// Creates this pages title and fires a PageTitleSet event if one is set
        /// </summary>
        private void GeneratePopUp()
        {
            var notification = (DialogBox)this.PageContext.CurrentForumPage.Notification;

            // This happens when user logs in
            if (this.DisplayPMPopup()
                &&
                (!this.PageContext.ForumPageType.Equals(ForumPages.cp_pm)
                 || !this.PageContext.ForumPageType.Equals(ForumPages.cp_editbuddies)))
            {
                if (this.Get<YafBoardSettings>().MessageNotificationSystem.Equals(0)
                    &&
                    !(this.Get<YafBoardSettings>().NotifcationNativeOnMobile
                      && this.Get<HttpRequestBase>().Browser.IsMobileDevice))
                {
                    notification.Show(
                        this.GetText("COMMON", "UNREAD_MSG2").FormatWith(this.PageContext.UnreadPrivate),
                        this.GetText("COMMON", "UNREAD_MSG_TITLE"),
                        DialogBox.DialogIcon.Mail,
                        new DialogBox.DialogButton
                            {
                                Text = this.GetText("COMMON", "YES"),
                                CssClass = "StandardButton OkButton",
                                ForumPageLink = new DialogBox.ForumLink { ForumPage = ForumPages.cp_pm }
                            },
                        new DialogBox.DialogButton
                            {
                                Text = this.GetText("COMMON", "NO"), 
                                CssClass = "StandardButton CancelButton"
                            });
                }
                else
                {
                    this.PageContext.AddLoadMessage(
                        this.GetText("COMMON", "UNREAD_MSG").FormatWith(this.PageContext.UnreadPrivate));
                }

                this.Get<IYafSession>().LastPm = this.PageContext.LastUnreadPm;

                // Avoid Showing Both Popups
                return;
            }

            if (!this.DisplayPendingBuddies()
                ||
                (this.PageContext.ForumPageType.Equals(ForumPages.cp_editbuddies)
                 || this.PageContext.ForumPageType.Equals(ForumPages.cp_pm)))
            {
                return;
            }

            if (this.Get<YafBoardSettings>().MessageNotificationSystem.Equals(0)
                &&
                !(this.Get<YafBoardSettings>().NotifcationNativeOnMobile
                  && this.Get<HttpRequestBase>().Browser.IsMobileDevice))
            {
                notification.Show(
                    this.GetText("BUDDY", "PENDINGBUDDIES2").FormatWith(this.PageContext.PendingBuddies),
                    this.GetText("BUDDY", "PENDINGBUDDIES_TITLE"),
                    DialogBox.DialogIcon.Info,
                    new DialogBox.DialogButton
                        {
                            Text = this.GetText("COMMON", "YES"),
                            CssClass = "StandardButton OkButton",
                            ForumPageLink = new DialogBox.ForumLink { ForumPage = ForumPages.cp_editbuddies }
                        },
                    new DialogBox.DialogButton
                        {
                            Text = this.GetText("COMMON", "NO"),
                            CssClass = "StandardButton CancelButton",
                            ForumPageLink = new DialogBox.ForumLink { ForumPage = YafContext.Current.ForumPageType }
                        });
            }
            else
            {
                this.PageContext.AddLoadMessage(
                    this.GetText("BUDDY", "PENDINGBUDDIES2").FormatWith(this.PageContext.PendingBuddies));
            }

            this.Get<IYafSession>().LastPendingBuddies = this.PageContext.LastPendingBuddies;
        }
    }

    #endregion
}