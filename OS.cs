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

using System;

namespace PdnFF
{
    internal static class OS
    {
        private static bool checkedIsVistaOrLater;
        private static bool checkedIsWindows7OrLater;
        private static bool isVistaOrLater;
        private static bool isWindows7OrLater;

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows Vista or later.
        /// </summary>
        /// <value>
        ///   <c>true</c> if operating system is Windows Vista or later; otherwise, <c>false</c>.
        /// </value>
        public static bool IsVistaOrLater
        {
            get
            {
                if (!checkedIsVistaOrLater)
                {
                    OperatingSystem os = Environment.OSVersion;

                    isVistaOrLater = os.Platform == PlatformID.Win32NT && os.Version.Major >= 6;
                    checkedIsVistaOrLater = true;
                }

                return isVistaOrLater;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current operating system is Windows 7 or later.
        /// </summary>
        /// <value>
        ///   <c>true</c> if operating system is Windows 7 or later; otherwise, <c>false</c>.
        /// </value>
        public static bool IsWindows7OrLater
        {
            get
            {
                if (!checkedIsWindows7OrLater)
                {
                    OperatingSystem os = Environment.OSVersion;

                    isWindows7OrLater = os.Platform == PlatformID.Win32NT && ((os.Version.Major == 6 && os.Version.Minor >= 1) || os.Version.Major > 6);
                    checkedIsWindows7OrLater = true;
                }

                return isWindows7OrLater;
            }
        }
    }
}
