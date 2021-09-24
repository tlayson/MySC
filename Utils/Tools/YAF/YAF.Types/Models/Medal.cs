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
namespace YAF.Types.Models
{
    using System;
    using System.Data.Linq.Mapping;

    using ServiceStack.DataAnnotations;

    using YAF.Types.Interfaces;
    using YAF.Types.Interfaces.Data;

    /// <summary>
    ///     A class which represents the Medal table.
    /// </summary>
    [Serializable]
    public partial class Medal : IEntity, IHaveBoardID, IHaveID
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Medal"/> class.
        /// </summary>
        public Medal()
        {
            this.OnCreated();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the medal id.
        /// </summary>
        [AutoIncrement]
        [Alias("MedalID")]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the board id.
        /// </summary>
        public int BoardID { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the flags.
        /// </summary>
        public int Flags { get; set; }

        /// <summary>
        /// Gets or sets the medal url.
        /// </summary>
        public string MedalURL { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ribbon url.
        /// </summary>
        public string RibbonURL { get; set; }

        /// <summary>
        /// Gets or sets the small medal height.
        /// </summary>
        public short SmallMedalHeight { get; set; }

        /// <summary>
        /// Gets or sets the small medal url.
        /// </summary>
        public string SmallMedalURL { get; set; }

        /// <summary>
        /// Gets or sets the small medal width.
        /// </summary>
        public short SmallMedalWidth { get; set; }

        /// <summary>
        /// Gets or sets the small ribbon height.
        /// </summary>
        public short? SmallRibbonHeight { get; set; }

        /// <summary>
        /// Gets or sets the small ribbon url.
        /// </summary>
        public string SmallRibbonURL { get; set; }

        /// <summary>
        /// Gets or sets the small ribbon width.
        /// </summary>
        public short? SmallRibbonWidth { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        public byte SortOrder { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The on created.
        /// </summary>
        partial void OnCreated();

        #endregion
    }
}