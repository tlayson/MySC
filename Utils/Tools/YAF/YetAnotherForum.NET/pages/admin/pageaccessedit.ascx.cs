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
// written by vzrus

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

namespace YAF.Pages.Admin
{
  #region Using

  using System;
  using System.Data;

  using YAF.Classes.Data;
  using YAF.Core;
  using YAF.Core.Services;
  using YAF.Types;
  using YAF.Types.Constants;
  using YAF.Types.Extensions;
  using YAF.Types.Flags;
  using YAF.Types.Interfaces;
  using YAF.Utils;
  using YAF.Utils.Helpers;

  #endregion

  /// <summary>
  /// Summary description for WebForm1.
  /// </summary>
    public partial class pageaccessedit : AdminPage
  {
    /* Construction */
    #region Methods

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
      YafBuildLink.Redirect(ForumPages.admin_pageaccesslist);
    }

    /// <summary>
    /// Creates navigation page links on top of forum (breadcrumbs).
    /// </summary>
    protected override void CreatePageLinks()
    {
      // beard index
      this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));

      // administration index
     this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
  

     // current page label (no link)
     this.PageLinks.AddLink(this.GetText("ADMIN_PAGEACCESSEDIT", "TITLE"), string.Empty);

     this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
        this.GetText("ADMIN_ADMIN", "Administration"),
        this.GetText("ADMIN_PAGEACCESSLIST", "TITLE"),
        this.GetText("ADMIN_PAGEACCESSEDIT", "TITLE"));
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

        this.Save.Text = this.GetText("ADMIN_PAGEACCESSEDIT", "SAVE");
        this.Cancel.Text = this.GetText("ADMIN_PAGEACCESSEDIT", "CANCEL");
        this.GrantAll.Text = this.GetText("ADMIN_PAGEACCESSEDIT", "GRANTALL");
        this.RevokeAll.Text = this.GetText("ADMIN_PAGEACCESSEDIT", "REVOKEALL");
        // create page links
        this.CreatePageLinks();

        // bind data
        this.BindData();
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
        if (this.Request.QueryString.GetFirstOrDefault("u") != null)
        {
            object userId = this.Request.QueryString.GetFirstOrDefault("u");

            foreach (RepeaterItem ri in this.AccessList.Items)
            {
                bool readAccess = ((CheckBox) ri.FindControl("ReadAccess")).Checked;
                string pageName = ((Label) ri.FindControl("PageName")).Text.Trim();
                if (readAccess || "admin_admin".ToLowerInvariant() == pageName.ToLowerInvariant())
                {
                    // save it
                        LegacyDb.adminpageaccess_save(
                            userId,
                            pageName);
                }
                else
                {
                    LegacyDb.adminpageaccess_delete(userId, pageName);
                }
            }

            YafBuildLink.Redirect(ForumPages.admin_pageaccesslist);
        }
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
        if (this.Request.QueryString.GetFirstOrDefault("u") != null)
        {
            object userId = this.Request.QueryString.GetFirstOrDefault("u");
            foreach (RepeaterItem ri in this.AccessList.Items)
            {
                // save it
                LegacyDb.adminpageaccess_save(
                    userId,
                   ((Label)ri.FindControl("PageName")).Text.Trim());
            }
        }

        YafBuildLink.Redirect(ForumPages.admin_pageaccesslist);
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
        if (this.Request.QueryString.GetFirstOrDefault("u") != null)
        {
            object userId = this.Request.QueryString.GetFirstOrDefault("u");
            foreach (RepeaterItem ri in this.AccessList.Items)
            {
                string pageName = ((Label) ri.FindControl("PageName")).Text.Trim();
                // save it - admin index should be always available
                if ("admin_admin".ToLowerInvariant() != pageName.ToLowerInvariant())
                {
                    LegacyDb.adminpageaccess_delete(userId, ((Label) ri.FindControl("PageName")).Text.Trim());
                }
            }
        }

        YafBuildLink.Redirect(ForumPages.admin_pageaccesslist);
    }


    /* Methods */

    /// <summary>
    /// The bind data.
    /// </summary>
    private void BindData()
    {
        bool found = false;

        if (this.Request.QueryString.GetFirstOrDefault("u") != null)
        {
            // Load the page access list.
            DataTable dt = LegacyDb.adminpageaccess_list(
                this.Request.QueryString.GetFirstOrDefault("u"), null);
            
            // Get admin pages by page prefixes.
            var listPages = Enum.GetNames(typeof(ForumPages)).Where(
                e => e.IndexOf("admin_", System.StringComparison.Ordinal) >= 0);
            
            // Initialize list with a helper class.
            var adminPageAccesses = new List<AdminPageAccess>();
            
            // Protected hostadmin pages
            var hostPages = new[] { "admin_boards", "admin_hostsettings", "admin_pageaccesslist", "admin_pageaccessedit", "admin_eventloggroups", "admin_eventloggroupaccess" };
            // Iterate thru all admin pages
            foreach (var listPage in listPages.ToList())
            {
                if (dt != null && dt.Rows.Cast<DataRow>().Any(dr => dr["PageName"].ToString() == listPage && hostPages.All(s => s != dr["PageName"].ToString())))
                {
                    found = true;
                    adminPageAccesses.Add(new AdminPageAccess
                    {
                        UserId = this.Request.QueryString.GetFirstOrDefault("u").ToType<int>(),
                        PageName = listPage,
                        ReadAccess = true
                    });
                }
                
                // If it doesn't contain page for the user add it.
                if (!found && hostPages.All(s => s != listPage))
                {
                    adminPageAccesses.Add(new AdminPageAccess
                    {
                        UserId = this.Request.QueryString.GetFirstOrDefault("u").ToType<int>(),
                        PageName = listPage,
                        ReadAccess = false
                    });
                }
                
                // Reset flag in the end of the outer loop
                found = false;
            }
          
            this.UserName.Text = this.HtmlEncode(this.Get<IUserDisplayName>().GetName(this.Request.QueryString.GetFirstOrDefault("u").ToType<int>()));
            
            // get admin pages list with access flags.
            this.AccessList.DataSource = adminPageAccesses.AsEnumerable();
        }
        this.DataBind();
    }


   /// <summary>
    /// The PollGroup item command.
    /// </summary>
    /// <param name="source">
    /// The source.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    protected void AccessList_OnItemDataBound([NotNull] object source, [NotNull] RepeaterItemEventArgs e)
   {
       RepeaterItem item = e.Item;
       var drowv = (AdminPageAccess)e.Item.DataItem;

       if (item.ItemType != ListItemType.Item && item.ItemType != ListItemType.AlternatingItem) return;

       var pageName = item.FindControlRecursiveAs<Label>("PageName");
       var pageText = item.FindControlRecursiveAs<Label>("PageText");
       var readAccess = item.FindControlRecursiveAs<CheckBox>("ReadAccess");
       pageText.Text = this.GetText("ACTIVELOCATION", drowv.PageName.ToUpperInvariant());
       pageName.Text = drowv.PageName;
       readAccess.Checked = drowv.ReadAccess;
   }

      #endregion
   }

    /// <summary>
    /// Provides a common wrapper for variables of various origins.
    /// </summary>
    internal class AdminPageAccess
    {
             internal int UserId { get; set; }
             internal string PageName { get; set; }
             internal bool ReadAccess { get; set; }
    }
}