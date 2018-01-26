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
using System.Collections.Generic;
using System.Text;
using PaintDotNet;
using PaintDotNet.Effects;
using System.Drawing;
using System.Runtime.InteropServices;

namespace FFEffect
{
    class Common : IDisposable
    {
        private SafeEnvironmentDataHandle environmentDataHandle;

        /// <summary>
        /// Sets the Filter source code and Control values
        /// </summary>
        public void SetupFilterData(Surface srcSurface, int[] control_values, string[] source)
        {
            if (environmentDataHandle != null)
            {
                environmentDataHandle.Dispose();
                environmentDataHandle = null;
            }

            environmentDataHandle = ffparse.CreateEnvironmentData(srcSurface, source, control_values);
        }

        public unsafe void Render(Surface src, Surface dst, Rectangle[] rois, int startIndex, int length)
        {
            if (!environmentDataHandle.IsInvalid)
            {
                ffparse.Render(environmentDataHandle, rois, startIndex, length, dst);
            }
            else
            {
                dst.CopySurface(src, rois, startIndex, length);
            }
        }

        public void Dispose()
        {
            if (environmentDataHandle != null)
            {
                environmentDataHandle.Dispose();
                environmentDataHandle = null;
            }
        }
    }
}
