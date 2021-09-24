/* YetAnotherForum.NET
 * Copyright (C) 2006-2010 Jaben Cargman
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

namespace YAF.TranslateApp
{
    /// <summary>
    /// Support class for TextBox.Tag property value
    /// </summary>
    public class Translation
    {
        #region Constants and Fields
        
        ///<summary>
        /// Resource Page Name
        ///</summary>
        public string sPageName { get; set; }
        ///<summary>
        /// Resource Name
        ///</summary>
        public string sResourceName { get; set; }
        ///<summary>
        /// Resource Original Value
        ///</summary>
        public string sResourceValue { get; set; }
        ///<summary>
        /// Resource Localized Value
        ///</summary>
        public string sLocalizedValue { get; set; }
        #endregion
    }
}