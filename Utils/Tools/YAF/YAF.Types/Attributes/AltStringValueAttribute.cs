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
namespace YAF.Types.Attributes
{
  using System;

  /// <summary>
  /// This attribute is used to represent a string value
  /// for a value in an enum.
  /// </summary>
  [AttributeUsage(AttributeTargets.Enum)]
  public class AltStringValueAttribute : Attribute
  {
    #region Properties

    /// <summary>
    /// Holds the stringvalue for a value in an enum.
    /// </summary>
    public string AltStringValue
    {
      get;
      protected set;
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="AltStringValueAttribute"/> class. 
    /// Constructor used to init a StringValue Attribute
    /// </summary>
    /// <param name="value">
    /// </param>
    public AltStringValueAttribute(string value)
    {
      AltStringValue = value;
    }

    #endregion
  }
}