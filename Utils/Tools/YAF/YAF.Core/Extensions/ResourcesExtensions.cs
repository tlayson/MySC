/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bj?rnar Henden
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
namespace YAF.Core
{
  #region Using

  using System;

  using YAF.Types;

  #endregion

  /// <summary>
  /// The resources extensions.
  /// </summary>
  public static class ResourcesExtensions
  {
    #region Public Methods

    /// <summary>
    /// The get hours offset.
    /// </summary>
    /// <param name="lanuageResource">
    /// The resource.
    /// </param>
    /// <returns>
    /// The get hours offset.
    /// </returns>
    public static decimal GetHoursOffset(this LanuageResourcesPageResource lanuageResource)
    {
      // calculate hours -- can use prefix of either UTC or GMT...
      decimal hours = 0;

      try
      {
        hours = Convert.ToDecimal(lanuageResource.tag.Replace("UTC", string.Empty).Replace("GMT", string.Empty));
      }
      catch (FormatException)
      {
        hours =
          Convert.ToDecimal(lanuageResource.tag.Replace(".", ",").Replace("UTC", string.Empty).Replace("GMT", string.Empty));
      }

      return hours;
    }

    #endregion
  }
}