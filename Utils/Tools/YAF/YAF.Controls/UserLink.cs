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
namespace YAF.Controls
{
    #region Using

    using System;
    using System.Web;
    using System.Web.UI;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Extensions;
    using YAF.Types.Interfaces;
    using YAF.Utils;

    #endregion

    /// <summary>
    ///     Provides a basic "profile link" for a YAF User
    /// </summary>
    public class UserLink : UserLabel
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether
        ///     Make the link target "blank" to open in a new window.
        /// </summary>
        public bool BlankTarget
        {
            get
            {
                return this.ViewState["BlankTarget"] != null && Convert.ToBoolean(this.ViewState["BlankTarget"]);
            }

            set
            {
                this.ViewState["BlankTarget"] = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [enable hover card].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [enable hover card]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableHoverCard
        {
            get
            {
                return this.ViewState["EnableHoverCard"] == null || this.ViewState["EnableHoverCard"].ToType<bool>();
            }

            set
            {
                this.ViewState["EnableHoverCard"] = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether
        ///     User Is a Guest.
        /// </summary>
        public bool IsGuest
        {
            get
            {
                return this.ViewState["IsGuest"] != null && Convert.ToBoolean(this.ViewState["IsGuest"]);
            }

            set
            {
                this.ViewState["IsGuest"] = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the user has Profile View Permission
        /// </summary>
        /// <value>
        /// <c>true</c> if the user can view profiles; otherwise, <c>false</c>.
        /// </value>
        private bool CanViewProfile
        {
            get
            {
                return this.Get<IPermissions>().Check(this.Get<YafBoardSettings>().ProfileViewPermissions);
            }
        }

        /// <summary>
        /// Gets a value indicating whether is hover card enabled.
        /// </summary>
        private bool IsHoverCardEnabled
        {
            get
            {
                return this.Get<YafBoardSettings>().EnableUserInfoHoverCards && this.EnableHoverCard;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The On PreRender event.
        /// </summary>
        /// <param name="e">
        /// the Event Arguments
        /// </param>
        protected override void OnPreRender([NotNull] EventArgs e)
        {
            if (!this.CanViewProfile || !this.IsHoverCardEnabled)
            {
                return;
            }

            // setup jQuery and YAF JS...
            YafContext.Current.PageElements.RegisterJQuery();
            YafContext.Current.PageElements.RegisterJQueryUI();

            YafContext.Current.PageElements.RegisterJsResourceInclude("hovercard", "js/jquery.hovercard.js");
            YafContext.Current.PageElements.RegisterCssIncludeResource("css/jquery.hovercard.css");

            // Setup Hover Card JS
            YafContext.Current.PageElements.RegisterJsBlockStartup(
                "yafhovercardtjs",
                "if (typeof(jQuery.fn.hovercard) != 'undefined'){{ {0}('.userHoverCard').hovercard({{showYafCard: true, delay: {1}, width: 350,loadingHTML: '{2}',errorHTML: '{3}'}}); }}"
                    .FormatWith(
                        Config.JQueryAlias,
                        this.Get<YafBoardSettings>().HoverCardOpenDelay,
                        this.GetText("DEFAULT", "LOADING_HOVERCARD"),
                        this.GetText("DEFAULT", "ERROR_HOVERCARD")));
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="output">
        /// The output.
        /// </param>
        protected override void Render([NotNull] HtmlTextWriter output)
        {
            var displayName = this.ReplaceName.IsNotSet()
                                  ? this.Get<IUserDisplayName>().GetName(this.UserID)
                                  : this.ReplaceName;

            if (this.UserID == -1 || !displayName.IsSet())
            {
                return;
            }

            // is this the guest user? If so, guest's don't have a profile.
            var isGuest = this.IsGuest ? this.IsGuest : UserMembershipHelper.IsGuestUser(this.UserID);

            output.BeginRender();

            if (!isGuest)
            {
                output.WriteBeginTag("a");

                output.WriteAttribute("href", YafBuildLink.GetLink(ForumPages.profile, "u={0}", this.UserID));

                if (this.CanViewProfile && this.IsHoverCardEnabled)
                {
                    if (this.CssClass.IsSet())
                    {
                        this.CssClass += " userHoverCard";
                    }
                    else
                    {
                        this.CssClass = "userHoverCard";
                    }

                    output.WriteAttribute(
                        "data-hovercard", 
                        "{0}resource.ashx?userinfo={1}&type=json&forumUrl={2}".FormatWith(
                            YafForumInfo.ForumClientFileRoot, 
                            this.UserID, 
                            HttpUtility.UrlEncode(YafBuildLink.GetBasePath())));
                }
                else
                {
                    output.WriteAttribute("title", this.GetText("COMMON", "VIEW_USRPROFILE"));
                }

                if (this.Get<YafBoardSettings>().UseNoFollowLinks)
                {
                    output.WriteAttribute("rel", "nofollow");
                }

                if (this.BlankTarget)
                {
                    output.WriteAttribute("target", "_blank");
                }
            }
            else
            {
                output.WriteBeginTag("span");
            }

            this.RenderMainTagAttributes(output);

            output.Write(HtmlTextWriter.TagRightChar);

            // Replace Name with Crawler Name if Set, otherwise use regular display name or Replace Name if set
            if (this.CrawlerName.IsSet())
            {
                output.WriteEncodedText(this.CrawlerName);
            }
            else if (!this.CrawlerName.IsSet() && this.ReplaceName.IsSet() && isGuest)
            {
                output.WriteEncodedText(this.ReplaceName);
            }
            else
            {
                output.WriteEncodedText(displayName);
            }

            output.WriteEndTag(!isGuest ? "a" : "span");

            if (this.PostfixText.IsSet())
            {
                output.Write(this.PostfixText);
            }

            output.EndRender();
        }

        #endregion
    }
}