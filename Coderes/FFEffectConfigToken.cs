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

namespace FFEffect
{
    [Serializable]
    public class FFEffectConfigToken : PaintDotNet.Effects.EffectConfigToken
    {
        private int[] control_values = new int[8];

        public int[] ctlvalues
        {
            get
            {
                return this.control_values;
            }
            set
            {
                this.control_values = value;
            }
        }

        public FFEffectConfigToken(int[] ctlvalues)
            : base()
        {
            this.control_values = ctlvalues;
        }

        protected FFEffectConfigToken(FFEffectConfigToken copyMe)
            : base(copyMe)
        {
            this.control_values = copyMe.control_values;
        }

        public override object Clone()
        {
            return new FFEffectConfigToken(this);
        }
    }
}