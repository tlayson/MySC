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

namespace YAF.Pages.Admin
{
    #region Using

    using System;

    using YAF.Classes;
    using YAF.Core;
    using YAF.Core.Services.Import;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Extensions;
    using YAF.Types.Interfaces;
    using YAF.Utils;

    #endregion

    /// <summary>
    /// The Admin Banned IP Import List Page.
    /// </summary>
    public partial class bannedip_import : AdminPage
    {
        #region Methods

        /// <summary>
        /// Cancel Import and Return Back to Previous Page
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Cancel_OnClick([NotNull] object sender, [NotNull] EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.admin_bannedip);
        }

        /// <summary>
        /// Try to Import from selected File
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Import_OnClick([NotNull] object sender, [NotNull] EventArgs e)
        {
            // import selected file (if it's the proper format)...
            if (!this.importFile.PostedFile.ContentType.StartsWith("text"))
            {
                this.PageContext.AddLoadMessage(
                    this.GetText("ADMIN_BANNEDIP_IMPORT", "IMPORT_FAILED").FormatWith(
                        this.importFile.PostedFile.ContentType));

                return;
            }

            try
            {
                int importedCount = DataImport.BannedIpAdressesImport(
                    this.PageContext.PageBoardID, this.PageContext.PageUserID, this.importFile.PostedFile.InputStream);

                this.PageContext.LoadMessage.AddSession(
                    importedCount > 0
                        ? this.GetText("ADMIN_BANNEDIP_IMPORT", "IMPORT_SUCESS").FormatWith(importedCount)
                        : this.GetText("ADMIN_BANNEDIP_IMPORT", "IMPORT_NOTHING"),
                    MessageTypes.Information);

                YafBuildLink.Redirect(ForumPages.admin_bannedip);
            }
            catch (Exception x)
            {
                this.PageContext.AddLoadMessage(
                    this.GetText("ADMIN_BANNEDIP_IMPORT", "IMPORT_FAILED").FormatWith(x.Message));
            }
        }

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

            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinks.AddLink(
                this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
            this.PageLinks.AddLink(
                this.GetText("ADMIN_BANNEDIP", "TITLE"), YafBuildLink.GetLink(ForumPages.admin_bannedip));
            this.PageLinks.AddLink(this.GetText("ADMIN_BANNEDIP_IMPORT", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
                this.GetText("ADMIN_ADMIN", "Administration"),
                this.GetText("ADMIN_BANNEDIP", "TITLE"),
                this.GetText("ADMIN_BANNEDIP_IMPORT", "TITLE"));

            this.Import.Text = this.GetText("ADMIN_BANNEDIP_IMPORT", "IMPORT");
            this.cancel.Text = this.GetText("CANCEL");
        }

        #endregion
    }
}