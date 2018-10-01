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
using PaintDotNet;

namespace PdnFF
{
    public sealed class FilterData
    {
        private string title;
        private string category;
        private string copyright;
        private string author;
        private bool[] mapEnable;
        private string[] mapLabel;
        private bool[] controlEnable;
        private string[] controlLabel;
        private int[] controlValue;
        private string[] source;
        private bool popDialog; // display parameter sliders
        private string fileName; // filename for the FFL filters

        public FilterData()
        {
            title = string.Empty;
            category = string.Empty;
            copyright = string.Empty;
            author = string.Empty;
            mapEnable = new bool[4];
            mapLabel = new string[4];
            controlEnable = new bool[8];
            controlLabel = new string[8];
            controlValue = new int[8];
            source = new string[4];
            popDialog = false; // display parameter sliders
            fileName = string.Empty; // filename for the FFL filters
        }

        private FilterData(FilterData cloneMe)
        {
            title = cloneMe.title;
            category = cloneMe.category;
            copyright = cloneMe.copyright;
            author = cloneMe.author;
            mapEnable = cloneMe.mapEnable.CloneT();
            mapLabel = cloneMe.mapLabel.CloneT();
            controlEnable = cloneMe.controlEnable.CloneT();
            controlLabel = cloneMe.controlLabel.CloneT();
            controlValue = cloneMe.controlValue.CloneT();
            source = cloneMe.source.CloneT();
            popDialog = cloneMe.popDialog;
            fileName = cloneMe.fileName;
        }

        public FilterData Clone()
        {
            return new FilterData(this);
        }

        public string Title
        {
            get
            {
                return title;
            }
            internal set
            {
                title = value;
            }
        }

        public string Category
        {
            get
            {
                return category;
            }
            internal set
            {
                category = value;
            }
        }

        public string Copyright
        {
            get
            {
                return copyright;
            }
            internal set
            {
                copyright = value;
            }
        }

        public string Author
        {
            get
            {
                return author;
            }
            internal set
            {
                author = value;
            }
        }

        public bool[] MapEnable
        {
            get
            {
                return mapEnable;
            }
            internal set
            {
                if (value == null || value.Length == 0)
                {
                    throw new ArgumentException("value is null or empty.", "value");
                }

                mapEnable = value;
            }
        }

        public string[] MapLabel
        {
            get
            {
                return mapLabel;
            }
            internal set
            {
                if (value == null || value.Length == 0)
                {
                    throw new ArgumentException("value is null or empty.", "value");
                }

                mapLabel = value;
            }
        }

        public bool[] ControlEnable
        {
            get
            {
                return controlEnable;
            }
            internal set
            {
                if (value == null || value.Length == 0)
                {
                    throw new ArgumentException("value is null or empty.", "value");
                }

                controlEnable = value;
            }
        }

        public string[] ControlLabel
        {
            get
            {
                return controlLabel;
            }
            internal set
            {
                if (value == null || value.Length == 0)
                {
                    throw new ArgumentException("value is null or empty.", "value");
                }

                controlLabel = value;
            }
        }

        public int[] ControlValue
        {
            get
            {
                return controlValue;
            }
            internal set
            {
                if (value == null || value.Length == 0)
                {
                    throw new ArgumentException("value is null or empty.", "value");
                }

                controlValue = value;
            }
        }

        public string[] Source
        {
            get
            {
                return source;
            }
            internal set
            {
                if (value == null || value.Length == 0)
                {
                    throw new ArgumentException("value is null or empty.", "value");
                }

                source = value;
            }
        }

        public bool PopDialog
        {
            get
            {
                return popDialog;
            }
            internal set
            {
                popDialog = value;
            }
        }

        /// <summary>
        /// The FileName for the FFL filters
        /// </summary>
        public string FileName
        {
            get
            {
                return fileName;
            }
            internal set
            {
                fileName = value;
            }
        }
    }

}
