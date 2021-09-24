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
    using YAF.Utils;
    using YAF.Utils.Helpers;

    #endregion

    /// <summary>
    ///     Summary description for attachments.
    /// </summary>
    public partial class attachments : AdminPage
    {
        #region Methods

        /// <summary>
        /// The delete_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        protected void Delete_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            ControlHelper.AddOnClickConfirmDialog(sender, this.GetText("ADMIN_ATTACHMENTS", "CONFIRM_DELETE"));
        }

        /// <summary>
        /// The on init.
        /// </summary>
        /// <param name="e">
        /// The e. 
        /// </param>
        protected override void OnInit([NotNull] EventArgs e)
        {
            this.List.ItemCommand += this.List_ItemCommand;

            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            this.InitializeComponent();
            base.OnInit(e);
        }

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

            this.PageLinks.AddLink(this.PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinks.AddLink(this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
            this.PageLinks.AddLink(this.GetText("ADMIN_ATTACHMENTS", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1}".FormatWith(this.GetText("ADMIN_ADMIN", "Administration"), this.GetText("ADMIN_ATTACHMENTS", "TITLE"));

            this.BindData();
        }

        /// <summary>
        /// The pager top_ page change.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        protected void PagerTop_PageChange([NotNull] object sender, [NotNull] EventArgs e)
        {
            // rebind
            this.BindData();
        }

        /// <summary>
        ///     The bind data.
        /// </summary>
        private void BindData()
        {
            this.PagerTop.PageSize = this.Get<YafBoardSettings>().MemberListPageSize;
            var dt = this.GetRepository<Attachment>().List(null, null, this.PageContext.PageBoardID, this.PagerTop.CurrentPageIndex, this.PagerTop.PageSize);
            this.List.DataSource = this.GetRepository<Attachment>().List(null, null, this.PageContext.PageBoardID, this.PagerTop.CurrentPageIndex, this.PagerTop.PageSize);
            this.PagerTop.Count = dt != null && dt.Rows.Count > 0 ? dt.AsEnumerable().First().Field<int>("TotalRows") : 0;
            this.DataBind();
        }

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        /// <summary>
        /// The list_ item command.
        /// </summary>
        /// <param name="source">
        /// The source. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private void List_ItemCommand([NotNull] object source, [NotNull] RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "delete":
                    this.GetRepository<Attachment>().Delete(e.CommandArgument.ToType<int>());
                    this.BindData();
                    break;
            }
        }

        #endregion
    }
}