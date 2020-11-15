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

using PaintDotNet;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace FFEffect
{
    internal static class ffparse
    {
        private struct BitmapData
        {
            public int width;
            public int height;
            public int stride;
            public int pixelSize;
            public IntPtr scan0;
        }

        [System.Security.SuppressUnmanagedCodeSecurity]
        private static class ffeval32
        {
            [DllImport("ffparse_x86.dll", ExactSpelling = true)]
            public static extern SafeEnvironmentDataHandle86 CreateEnvironmentData(
                [In] ref BitmapData input,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeConst = 4)] string[] source,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeConst = 8)] int[] controlValues);
            [DllImport("ffparse_x86.dll", ExactSpelling = true)]
            public static extern void FreeEnvironmentData(IntPtr handle);
            [DllImport("ffparse_x86.dll", ExactSpelling = true)]
            public static extern unsafe void Render(SafeEnvironmentDataHandle handle, Rectangle* rois, int length, [In] ref BitmapData data);
        }
        [System.Security.SuppressUnmanagedCodeSecurity]
        private static class ffeval64
        {
            [DllImport("ffparse_x64.dll", ExactSpelling = true)]
            public static extern SafeEnvironmentDataHandle64 CreateEnvironmentData(
                [In] ref BitmapData input,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeConst = 4)] string[] source,
                [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeConst = 8)] int[] controlValues);
            [DllImport("ffparse_x64.dll", ExactSpelling = true)]
            public static extern void FreeEnvironmentData(IntPtr handle);
            [DllImport("ffparse_x64.dll", ExactSpelling = true)]
            public static extern unsafe void Render(SafeEnvironmentDataHandle handle, Rectangle* rois, int length, [In] ref BitmapData data);
        }

        private sealed class SafeEnvironmentDataHandle64 : SafeEnvironmentDataHandle
        {
            private SafeEnvironmentDataHandle64() : base(true)
            {
            }

            protected override bool ReleaseHandle()
            {
                ffeval64.FreeEnvironmentData(handle);
                return true;
            }
        }

        private sealed class SafeEnvironmentDataHandle86 : SafeEnvironmentDataHandle
        {
            private SafeEnvironmentDataHandle86() : base(true)
            {
            }

            protected override bool ReleaseHandle()
            {
                ffeval32.FreeEnvironmentData(handle);
                return true;
            }
        }

        /// <summary>
        /// Creates the filter environment data
        /// </summary>
        /// <param name="srcSurface">The source surface</param>
        /// <param name="data">The filter data.</param>
        /// <returns>A handle to the created filter environment.</returns>
        public static SafeEnvironmentDataHandle CreateEnvironmentData(Surface srcSurface, string[] source, int[] controlValues)
        {
            BitmapData sourceBitmapData = new BitmapData
            {
                width = srcSurface.Width,
                height = srcSurface.Height,
                stride = srcSurface.Stride,
                pixelSize = ColorBgra.SizeOf,
                scan0 = srcSurface.Scan0.Pointer
            };

            if (IntPtr.Size == 8)
            {
                return ffeval64.CreateEnvironmentData(ref sourceBitmapData, source, controlValues);
            }
            else
            {
                return ffeval32.CreateEnvironmentData(ref sourceBitmapData, source, controlValues);
            }
        }

        /// <summary>
        /// Renders the Filter Factory output to the destination surface.
        /// </summary>
        /// <param name="handle">The filter environment handle.</param>
        /// <param name="rois">The array of rectangles to render.</param>
        /// <param name="startIndex">The starting index in the rectangle array.</param>
        /// <param name="length">The number of rectangles to render.</param>
        /// <param name="dstSurface">The destination surface.</param>
        public static unsafe void Render(SafeEnvironmentDataHandle handle, Rectangle[] rois, int startIndex, int length, Surface dstSurface)
        {
            if (length == 0)
            {
                return;
            }

            BitmapData bitmap = new BitmapData
            {
                width = dstSurface.Width,
                height = dstSurface.Height,
                stride = dstSurface.Stride,
                pixelSize = ColorBgra.SizeOf,
                scan0 = dstSurface.Scan0.Pointer
            };

            fixed (Rectangle* rectanglePointer = &rois[startIndex])
            {
                if (IntPtr.Size == 8)
                {
                    ffeval64.Render(handle, rectanglePointer, length, ref bitmap);
                }
                else
                {
                    ffeval32.Render(handle, rectanglePointer, length, ref bitmap);
                }
            }
        }
    }
}
