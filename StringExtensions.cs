/*
*  This file is part of pdn-filter-factory, a Paint.NET Effect that
*  interprets Filter Factory-based Adobe Photoshop filters.
*
*  Copyright (C) 2010, 2011, 2012, 2015, 2018, 2020 Nicholas Hayes
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
    internal static class StringExtensions
    {
        /// <summary>
        /// Determines whether the string contains the specified value.
        /// </summary>
        /// <param name="s">The string to search.</param>
        /// <param name="value">The <see cref="string"/> object to seek.</param>
        /// <param name="comparisonType">One of the System.StringComparison values.</param>
        /// <returns>
        ///   <c>true</c> if the string contains the specified value; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="s"/> is null
        ///-or-
        ///<paramref name="value"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="comparisonType"/> is not a valid <see cref="StringComparison"/> value.</exception>
        public static bool Contains(this string s, string value, StringComparison comparisonType)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            return s.IndexOf(value, comparisonType) >= 0;
        }
    }
}
