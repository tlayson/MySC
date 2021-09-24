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

    using ServiceStack.DataAnnotations;

    using YAF.Types.Interfaces.Data;

    /// <summary>
    /// A class which represents the WatchTopic table.
    /// </summary>
    [Serializable]
    public partial class WatchTopic : IEntity, IHaveID
    {
        partial void OnCreated();

        public WatchTopic()
        {
            this.OnCreated();
        }

        #region Properties

        [AutoIncrement]
        [Alias("WatchTopicID")]
        public int ID { get; set; }

        public int TopicID { get; set; }

        public int UserID { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastMail { get; set; }

        #endregion
    }
}