﻿/*
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

using System.Collections;
using System.Windows.Forms;

namespace PdnFF
{
    class TreeNodeItemComparer : IComparer
    {
        private static readonly TreeNodeItemComparer instance = new TreeNodeItemComparer();

        private TreeNodeItemComparer()
        {
        }

        public static TreeNodeItemComparer Instance
        {
            get
            {
                return instance;
            }
        }

        public int Compare(object x, object y)
        {
            return StringLogicalComparer.Compare(((TreeNode)x).Text, ((TreeNode)y).Text);
        }
    }
}
