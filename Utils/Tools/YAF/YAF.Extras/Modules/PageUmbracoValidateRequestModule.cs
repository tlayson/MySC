﻿/* YetAnotherForum.NET
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
using System;
using System.Collections.Generic;
using System.Web;
using YAF.Classes;
using YAF.Classes.Core;
using YAF.Classes.Utils;

namespace YAF.Modules
{
	[YafModule( "Page Umbraco Validate Request Module", "Tiny Gecko", 1 )]
	public class PageUmbracoValidateRequestModule : SimpleBaseModule
	{
		public PageUmbracoValidateRequestModule()
		{
			
		}

		override public void InitForum()
		{
			ForumControl.Init += new EventHandler(ForumControl_Init);
		}
		
		void ForumControl_Init(object sender, EventArgs e)
		{
			if ( ForumControl.Page is umbraco.UmbracoDefault )
			{
				((umbraco.UmbracoDefault)ForumControl.Page).ValidateRequest = false;
			}
		}		
	}	
}