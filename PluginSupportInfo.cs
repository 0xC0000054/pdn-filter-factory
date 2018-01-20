/*
*  This file is part of pdn-filter-factory, a Paint.NET Effect that
*  interprets Filter Factory-based Adobe Photoshop filters.
*
*  Copyright (C) 2010, 2011, 2012, 2015, 2018 Nicholas Hayes
*
*  This program is free software: you can redistribute it and/or modify
*  it under the terms of the GNU General Public License as published by
*  the Free Software Foundation, either version 3 of the License, or
*  (at your option) any later version.
*
*  This program is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU General Public License for more details.
*
*  You should have received a copy of the GNU General Public License
*  along with this program.  If not, see <http://www.gnu.org/licenses/>.
*
*/

using PaintDotNet;
using System;
using System.Reflection;

namespace PdnFF
{
    public sealed class PluginSupportInfo : IPluginSupportInfo
    {
        public string DisplayName
        {
            get
            {
                return PdnFFEffect.StaticName;
            }
        }

        public string Author
        {
            get
            {
                return ((AssemblyCompanyAttribute)typeof(PdnFFEffect).Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)[0]).Company;
            }
        }

        public string Copyright
        {
            get
            {
                return ((AssemblyCopyrightAttribute)typeof(PdnFFEffect).Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
            }
        }

        public Version Version
        {
            get
            {
                return typeof(PdnFFEffect).Assembly.GetName().Version;
            }
        }

        public Uri WebsiteUri
        {
            get
            {
                return new Uri("https://forums.getpaint.net/index.php?showtopic=20380");
            }
        }
    }
}
