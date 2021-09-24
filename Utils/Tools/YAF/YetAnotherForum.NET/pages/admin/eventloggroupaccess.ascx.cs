/* Yet Another Forum.NET
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
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web.UI.WebControls;

    using YAF.Classes;
    using YAF.Classes.Data;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Extensions;
    using YAF.Types.Interfaces;
    using YAF.Utils;
    using YAF.Utils.Helpers;

    #endregion

    /// <summary>
    /// The Event Log Group Access Page.
    /// written by vzrus
    /// </summary>
    public partial class eventloggroupaccess : AdminPage
    {
        /* Construction */

        #region Methods

        /// <summary>
        /// Creates navigation page links on top of forum (breadcrumbs).
        /// </summary>
        protected override void CreatePageLinks()
        {
            // board index
            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));

            // administration index
            this.PageLinks.AddLink(
                this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));

            // parent page
            this.PageLinks.AddLink(
                this.GetText("ADMIN_EVENTLOGGROUPS", "TITLE"), YafBuildLink.GetLink(ForumPages.admin_eventloggroups));

            // current page label (no link)
            this.PageLinks.AddLink(this.GetText("ADMIN_EVENTLOGROUPACCESS", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
                this.GetText("ADMIN_ADMIN", "Administration"),
                this.GetText("ADMIN_EVENTLOGROUPS", "TITLE"),
                this.GetText("ADMIN_EVENTLOGROUPACCESS", "TITLE"));
        }

        /* Event Handlers */

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }

            this.Save.Text = this.GetText("ADMIN_EVENTLOGROUPACCESS", "SAVE");
            this.Cancel.Text = this.GetText("ADMIN_EVENTLOGROUPACCESS", "CANCEL");
            this.GrantAll.Text = this.GetText("ADMIN_EVENTLOGROUPACCESS", "GRANTALL");
            this.RevokeAll.Text = this.GetText("ADMIN_EVENTLOGROUPACCESS", "REVOKEALL");
            this.GrantAllDelete.Text = this.GetText("ADMIN_EVENTLOGROUPACCESS", "GRANTALLDELETE");
            this.RevokeAllDelete.Text = this.GetText("ADMIN_EVENTLOGROUPACCESS", "REVOKEALLDELETE");

            // create page links
            this.CreatePageLinks();

            // bind data
            this.BindData();
        }

        /// <summary>
        /// The cancel_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Cancel_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // get back to access admin list
            YafBuildLink.Redirect(ForumPages.admin_eventloggroups);
        }

        /// <summary>
        /// The save_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Save_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // retrieve access mask ID from parameter (if applicable)
            if (this.Request.QueryString.GetFirstOrDefault("r") != null)
            {
                object groupId = this.Request.QueryString.GetFirstOrDefault("r");

                foreach (RepeaterItem ri in this.AccessList.Items)
                {
                    string eventTypeName = ((Label)ri.FindControl("EventTypeName")).Text.Trim();
                    bool viewAccess = ((CheckBox)ri.FindControl("ViewAccess")).Checked;
                    bool deleteAccess = ((CheckBox)ri.FindControl("DeleteAccess")).Checked;

                    if (viewAccess)
                    {
                        // Parse enum value from name.
                        EventLogTypes eventTypeId;
                        Enum.TryParse(eventTypeName, true, out eventTypeId);

                        // save it
                        LegacyDb.eventloggroupaccess_save(
                            groupId, eventTypeId.ToType<int>(), eventTypeName, deleteAccess);
                    }
                    else
                    {
                        var etn = ((Label)ri.FindControl("EventTypeName")).Text.Trim();
                        EventLogTypes eventTypeId;
                        Enum.TryParse(etn, true, out eventTypeId);
                        LegacyDb.eventloggroupaccess_delete(groupId, eventTypeId.ToType<int>(), etn);
                    }
                }
            }

            YafBuildLink.Redirect(ForumPages.admin_eventloggroups);
        }

        /// <summary>
        /// The Grant All click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void GrantAll_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // save permissions to table -  checked only
            if (this.Request.QueryString.GetFirstOrDefault("r") != null)
            {
                object groupId = this.Request.QueryString.GetFirstOrDefault("r");

                foreach (
                    var etn in
                        from RepeaterItem ri in this.AccessList.Items
                        select ((Label)ri.FindControl("EventTypeName")).Text.Trim())
                {
                    // Parse enum value from name.
                    EventLogTypes eventTypeId;
                    Enum.TryParse(etn, true, out eventTypeId);

                    // save it
                    LegacyDb.eventloggroupaccess_save(groupId, eventTypeId.ToType<int>(), etn, false);
                }
            }

            YafBuildLink.Redirect(ForumPages.admin_eventloggroups);
        }

        /// <summary>
        /// The Grant All Delete click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void GrantAllDelete_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // save permissions to table -  checked only
            if (this.Request.QueryString.GetFirstOrDefault("r") != null)
            {
                object groupId = this.Request.QueryString.GetFirstOrDefault("r");
                foreach (var etn in from RepeaterItem ri in this.AccessList.Items
                                    where ((CheckBox)ri.FindControl("ViewAccess")).Checked
                                    select ((Label)ri.FindControl("EventTypeName")).Text.Trim())
                {
                    // Parse enum value from name.
                    EventLogTypes eventTypeId;
                    Enum.TryParse(etn, true, out eventTypeId);

                    // save it
                    LegacyDb.eventloggroupaccess_save(groupId, eventTypeId.ToType<int>(), etn, true);
                }
            }

            YafBuildLink.Redirect(ForumPages.admin_eventloggroups);
        }

        /// <summary>
        /// The RevokeAllDelete _Click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void RevokeAllDelete_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // revoke permissions by deleting records from table. Number of records ther should be minimal.
            if (this.Request.QueryString.GetFirstOrDefault("r") != null)
            {
                object groupId = this.Request.QueryString.GetFirstOrDefault("r");
                foreach (var etn in from RepeaterItem ri in this.AccessList.Items
                                    where ((CheckBox)ri.FindControl("ViewAccess")).Checked
                                    select ((Label)ri.FindControl("EventTypeName")).Text.Trim())
                {
                    // Parse enum value from name.
                    EventLogTypes eventTypeId;
                    Enum.TryParse(etn, true, out eventTypeId);

                    // save it
                    LegacyDb.eventloggroupaccess_save(groupId, eventTypeId.ToType<int>(), etn, false);
                }
            }

            YafBuildLink.Redirect(ForumPages.admin_eventloggroups);
        }

        /// <summary>
        /// The RevokeAll _Click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void RevokeAll_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            // revoke permissions by deleting records from table. Number of records ther should be minimal.
            if (this.Request.QueryString.GetFirstOrDefault("r") != null)
            {
                object groupId = this.Request.QueryString.GetFirstOrDefault("r");
                foreach (var etn in
                    from RepeaterItem ri in this.AccessList.Items
                    select ((Label)ri.FindControl("EventTypeName")).Text.Trim())
                {
                    // Parse enum value from name.
                    EventLogTypes eventTypeId;
                    Enum.TryParse(etn, true, out eventTypeId);

                    // save it
                    LegacyDb.eventloggroupaccess_delete(groupId, eventTypeId.ToType<int>(), etn);
                }
            }

            YafBuildLink.Redirect(ForumPages.admin_eventloggroups);
        }

        /* Methods */

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            bool found = false;

            if (this.Request.QueryString.GetFirstOrDefault("r") != null)
            {
                // Load the page access list.
                var dt = LegacyDb.eventloggroupaccess_list(this.Request.QueryString.GetFirstOrDefault("r"), null);

                // Get admin pages by page prefixes.
                var listEnumValues = Enum.GetValues(typeof(EventLogTypes));

                // Initialize list with a helper class.
                var adminPageAccesses = new List<GroupEventLogAccess>();

                // Iterate thru all admin pages
                foreach (int eventValue in listEnumValues)
                {
                    int eventTypeId = eventValue;
                    foreach (
                        var dr in dt.Rows.Cast<DataRow>().Where(dr => dr["EventTypeID"].ToType<int>() == eventTypeId))
                    {
                        found = true;
                        adminPageAccesses.Add(
                            new GroupEventLogAccess
                                {
                                    GroupId = this.Request.QueryString.GetFirstOrDefault("r").ToType<int>(),
                                    EventTypeId = eventTypeId,
                                    EventTypeName = Enum.GetName(typeof(EventLogTypes), eventTypeId),
                                    DeleteAccess = dr["DeleteAccess"].ToType<bool>(),
                                    ViewAccess = true
                                });
                    }

                    // If it doesn't contain page for the user add it.
                    if (!found)
                    {
                        adminPageAccesses.Add(
                            new GroupEventLogAccess
                                {
                                    GroupId = this.Request.QueryString.GetFirstOrDefault("r").ToType<int>(),
                                    EventTypeId = eventTypeId,
                                    EventTypeName = Enum.GetName(typeof(EventLogTypes), eventTypeId),
                                    DeleteAccess = false,
                                    ViewAccess = false
                                });
                    }

                    // Reset flag in the end of the outer loop
                    found = false;
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.GroupName.Text = this.HtmlEncode(dt.Rows[0]["GroupName"]);
                }

                // get admin pages list with access flags.
                this.AccessList.DataSource = adminPageAccesses.AsEnumerable();
            }

            this.DataBind();
        }

        /// <summary>
        /// The PollGroup item command.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void AccessList_OnItemDataBound([NotNull] object source, [NotNull] RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            var drowv = (GroupEventLogAccess)e.Item.DataItem;

            if (item.ItemType != ListItemType.Item && item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            var eventTypeName = item.FindControlRecursiveAs<Label>("EventTypeName");
            var eventText = item.FindControlRecursiveAs<Label>("EventText");
            var deleteAccess = item.FindControlRecursiveAs<CheckBox>("DeleteAccess");
            var viewAccess = item.FindControlRecursiveAs<CheckBox>("ViewAccess");
            eventText.Text = this.GetText(
                "ADMIN_EVENTLOGROUPACCESS", "LT_{0}".FormatWith(drowv.EventTypeName.ToUpperInvariant()));
            eventTypeName.Text = drowv.EventTypeName;
            deleteAccess.Checked = drowv.DeleteAccess;
            viewAccess.Checked = drowv.ViewAccess;
        }

        #endregion
    }

    /// <summary>
    /// Provides a common wrapper for variables of various origins.
    /// </summary>
    internal class GroupEventLogAccess
    {
        /// <summary>
        /// Gets or sets the group id.
        /// </summary>
        /// <value>
        /// The group id.
        /// </value>
        internal int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the event type id.
        /// </summary>
        /// <value>
        /// The event type id.
        /// </value>
        internal int EventTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the event type.
        /// </summary>
        /// <value>
        /// The name of the event type.
        /// </value>
        internal string EventTypeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [view access].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [view access]; otherwise, <c>false</c>.
        /// </value>
        internal bool ViewAccess { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [delete access].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete access]; otherwise, <c>false</c>.
        /// </value>
        internal bool DeleteAccess { get; set; }
    }
}