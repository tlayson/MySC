﻿/* Yet Another Forum.NET
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
namespace YAF.Core.Events
{
    using YAF.Providers.Membership;
    using YAF.Types.Attributes;
    using YAF.Types.EventProxies;
    using YAF.Types.Interfaces;

    /// <summary>
    ///     The membership upgrade event.
    /// </summary>
    [ExportService(ServiceLifetimeScope.OwnedByContainer)]
    public class MembershipUpgradeEvent : IHandleEvent<AfterUpgradeDatabaseEvent>
    {
        #region Public Properties

        /// <summary>
        ///     Gets the order.
        /// </summary>
        public int Order
        {
            get
            {
                return 1000;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The handle.
        /// </summary>
        /// <param name="event">
        /// The event.
        /// </param>
        public void Handle(AfterUpgradeDatabaseEvent @event)
        {
            DB.Current.UpgradeMembership(@event.PreviousVersion, @event.CurrentVersion);
        }

        #endregion
    }
}