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

namespace PdnFF
{
    public sealed class PdnFFConfigToken : PaintDotNet.Effects.EffectConfigToken
    {
        private FilterData data;
        private FilterData resetData;

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

        public FilterData ResetData
        {
            get
            {
                return resetData;
            }
            internal set
            {
                resetData = value;
            }
        }

        public PdnFFConfigToken(FilterData data, FilterData resetData) : base()
        {
            Data = data;
            this.resetData = resetData;
        }

        private PdnFFConfigToken(PdnFFConfigToken copyMe) : base(copyMe)
        {
            Data = copyMe.Data;
            resetData = copyMe.resetData;
        }

        public override object Clone()
        {
            return new PdnFFConfigToken(this);
        }
    }
}
