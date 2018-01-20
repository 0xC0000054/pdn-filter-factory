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
    public sealed class PdnFFConfigToken : PaintDotNet.Effects.EffectConfigToken
    {
        private FilterData data;
        private string lastFileName;
        private string lastFFL;
        private string fflOfs;

        public FilterData Data
        {
            get
            {
                return data;
            }
            internal set
            {
                data = value;
            }
        }

        public string LastFileName
        {
            get
            {
                return lastFileName;
            }
            internal set
            {
                lastFileName = value;
            }
        }

        public string LastFFL
        {
            get
            {
                return lastFFL;
            }
            internal set
            {
                lastFFL = value;
            }
        }

        public string FFLOffset
        {
            get
            {
                return fflOfs;
            }
            internal set
            {
                fflOfs = value;
            }
        }


        public PdnFFConfigToken(FilterData data, String lastfilename, String lastffl, String fflofs) : base()
        {
            this.Data = data;
            this.lastFileName = lastfilename;
            this.lastFFL = lastffl;
            this.fflOfs = fflofs;
        }
#pragma warning disable 0628
        protected PdnFFConfigToken(PdnFFConfigToken copyMe) : base(copyMe)
        {
            this.Data = copyMe.Data;
            this.lastFileName = copyMe.lastFileName;
            this.lastFFL = copyMe.lastFFL;
            this.fflOfs = copyMe.fflOfs;
        }
#pragma warning restore 0628

        public override object Clone()
        {
            return new PdnFFConfigToken(this);
        }
    }
}