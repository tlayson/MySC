﻿/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjّrnar Henden
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
    using System.Web.UI.WebControls;

    using YAF.Classes;
    using YAF.Classes.Data;
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using YAF.Utilities;
    using YAF.Utils;

    #endregion

    /// <summary>
    /// The AlbumList control.
    /// </summary>
    public partial class AlbumList : BaseUserControl
    {
        #region Properties

        /// <summary>
        ///   Gets or sets the User ID.
        /// </summary>
        public int UserID { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// redirects to the add new album page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void AddAlbum_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.cp_editalbumimages, "u={0}&a=new", this.UserID);
        }

        /// <summary>
        /// The item command method for albums repeater. Redirects to the album page.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterCommandEventArgs"/> instance containing the event data.</param>
        protected void Albums_ItemCommand([NotNull] object source, [NotNull] RepeaterCommandEventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.cp_editalbumimages, "a={0}", e.CommandArgument);
        }

        /// <summary>
        /// Show a Random Cover if none is Set
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void Albums_ItemDataBound([NotNull] object sender, [NotNull] RepeaterItemEventArgs e)
        {
            // tha_watcha: TODO: Currently disabled this funtion, until yaf 2.0 build
            /*var coverImage = (Image)e.Item.FindControl("coverImage");

                  if (coverImage == null) return;

                  var curAlbum = DB.album_image_list(coverImage.AlternateText, null);

                  Random random = new Random();

                  if ((curAlbum != null) && (curAlbum.Rows.Count > 0))
                  {
                      coverImage.ImageUrl = String.Format("{0}resource.ashx?imgprv={1}", YafForumInfo.ForumClientFileRoot, curAlbum.Rows[random.Next(curAlbum.Rows.Count)]["ImageID"]);
                  }*/
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender([NotNull] EventArgs e)
        {
            if (this.UserID == this.PageContext.PageUserID)
            {
                // Register jQuery Ajax Plugin.
                YafContext.Current.PageElements.RegisterJsResourceInclude("yafPageMethodjs", "js/jquery.pagemethod.js");

                // Register Js Blocks.
                YafContext.Current.PageElements.RegisterJsBlockStartup(
                    "AlbumEventsJs",
                    JavaScriptBlocks.AlbumEventsJs(
                        this.Get<ILocalization>().GetText("ALBUM_CHANGE_TITLE"),
                        this.Get<ILocalization>().GetText("ALBUM_IMAGE_CHANGE_CAPTION")));
                YafContext.Current.PageElements.RegisterJsBlockStartup(
                    "ChangeAlbumTitleJs", JavaScriptBlocks.ChangeAlbumTitleJs);
                YafContext.Current.PageElements.RegisterJsBlockStartup(
                    "asynchCallFailedJs", JavaScriptBlocks.AsynchCallFailedJs);
                YafContext.Current.PageElements.RegisterJsBlockStartup(
                    "AlbumCallbackSuccessJS", JavaScriptBlocks.AlbumCallbackSuccessJS);
            }

            base.OnPreRender(e);
        }

        /// <summary>
        /// Called when the page loads
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }

            string umhdn = UserMembershipHelper.GetDisplayNameFromID(this.UserID);
            this.AlbumHeaderLabel.Param0 = this.Get<YafBoardSettings>().EnableDisplayName
                                               ? this.HtmlEncode(umhdn)
                                               : this.HtmlEncode(UserMembershipHelper.GetUserNameFromID(this.UserID));

            this.BindData();

            HttpContext.Current.Session["imagePreviewWidth"] = this.Get<YafBoardSettings>().ImageAttachmentResizeWidth;
            HttpContext.Current.Session["imagePreviewHeight"] = this.Get<YafBoardSettings>().ImageAttachmentResizeHeight;
            HttpContext.Current.Session["imagePreviewCropped"] =
                this.Get<YafBoardSettings>().ImageAttachmentResizeCropped;
            HttpContext.Current.Session["localizationFile"] = this.Get<ILocalization>().LanguageFileName;

            // Show Albums Max Info
            if (this.UserID == this.PageContext.PageUserID)
            {
                this.albumsInfo.Text = this.Get<ILocalization>().GetTextFormatted(
                    "ALBUMS_INFO", this.PageContext.NumAlbums, this.PageContext.UsrAlbums);
                if (this.PageContext.UsrAlbums > this.PageContext.NumAlbums)
                {
                    this.AddAlbum.Visible = true;
                }

                this.albumsInfo.Text = this.PageContext.UsrAlbums > 0
                                           ? this.Get<ILocalization>().GetTextFormatted(
                                               "ALBUMS_INFO", this.PageContext.NumAlbums, this.PageContext.UsrAlbums)
                                           : this.Get<ILocalization>().GetText("ALBUMS_NOTALLOWED");

                this.albumsInfo.Visible = true;
            }

            // vzrus: used if someone moderates usuful if a moderation is implemented 
            /* else 
            {
                DataTable sigData = LegacyDb.user_getalbumsdata(this.PageContext.PageUserID, YafContext.Current.PageBoardID);
                DataTable usrAlbumsData = LegacyDb.user_getalbumsdata(this.PageContext.PageUserID, YafContext.Current.PageBoardID);
                var allowedAlbums = usrAlbumsData.GetFirstRowColumnAsValue<int?>("UsrAlbums", null);
                var numAlbums = usrAlbumsData.GetFirstRowColumnAsValue<int?>("NumAlbums", null);
          
                if (allowedAlbums.HasValue && allowedAlbums > 0 && numAlbums < allowedAlbums)
                {
                    this.AddAlbum.Visible = true;
                }

                this.albumsInfo.Visible = false;
            } */

            if (this.AddAlbum.Visible)
            {
                this.AddAlbum.Text = this.Get<ILocalization>().GetText("BUTTON", "BUTTON_ADDALBUM");
            }
        }

        /// <summary>
        /// The pager_ page change.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Pager_PageChange([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.BindData();
        }

        /// <summary>
        /// Binds the album list data.
        /// </summary>
        private void BindData()
        {
            this.PagerTop.PageSize = this.Get<YafBoardSettings>().AlbumsPerPage;

            // set the Datatable
            var albumListDT = LegacyDb.album_list(this.UserID, null);

            if ((albumListDT == null) || (albumListDT.Rows.Count <= 0))
            {
                return;
            }

            this.PagerTop.Count = albumListDT.Rows.Count;

            // create paged data source for the albumlist
            var pds = new PagedDataSource
                {
                    DataSource = albumListDT.DefaultView,
                    AllowPaging = true,
                    CurrentPageIndex = this.PagerTop.CurrentPageIndex,
                    PageSize = this.PagerTop.PageSize
                };

            this.Albums.DataSource = pds;
            this.DataBind();
        }

        #endregion
    }
}