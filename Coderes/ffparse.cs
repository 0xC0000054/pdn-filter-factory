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
            public static extern int SetupBitmap(IntPtr pixelData, int width, int height, int stride, int pixelSize);
            [DllImport("ffparse_x86.dll", ExactSpelling = true)]
            public static extern void DestroyBitmap();
            [DllImport("ffparse_x86.dll", ExactSpelling = true)]
            public static extern SafeEnvironmentDataHandle86 CreateEnvironmentData(
                int width,
                int height,
                int pixelSize,
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
            public static extern int SetupBitmap(IntPtr pixelData, int width, int height, int stride, int pixelSize);
            [DllImport("ffparse_x64.dll", ExactSpelling = true)]
            public static extern void DestroyBitmap();
            [DllImport("ffparse_x64.dll", ExactSpelling = true)]
            public static extern SafeEnvironmentDataHandle64 CreateEnvironmentData(
                int width,
                int height,
                int pixelSize,
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
        /// The number channels in the image, always 4 in Paint.NET
        /// </summary>
        private const int pixelSize = 4;

        /// <summary>
        /// Sets up the unmanaged access to the Bitmap Data
        /// </summary>
        /// <param name="pixelData">The pointer to the start of the pixeldata, Scan0</param>
        /// <param name="width">The width of the image in Pixels</param>
        /// <param name="height">The height of the image in Pixels</param>
        /// <param name="stride">The stride of the image</param>
        /// <returns>1 on Success, negitive on failure</returns>
        public static int SetupBitmap(IntPtr pixelData, int width, int height, int stride)
        {
            if (IntPtr.Size == 8)
            {
                return ffeval64.SetupBitmap(pixelData, width, height, stride, pixelSize);
            }
            else
            {
                return ffeval32.SetupBitmap(pixelData, width, height, stride, pixelSize);
            }
        }

        /// <summary>
        /// Destroys the unmanaged access to the Bitmap Data
        /// </summary>
        public static void DestroyBitmap()
        {
            if (IntPtr.Size == 8)
            {
                ffeval64.DestroyBitmap();
            }
            else
            {
                ffeval32.DestroyBitmap();
            }
        }

        /// <summary>
        /// Creates the filter environment data
        /// </summary>
        /// <param name="width">The width of the image in pixels</param>
        /// <param name="height">The height of the image in pixels</param>
        /// <param name="data">The filter data.</param>
        /// <returns>A handle to the created filter environment.</returns>
        public static SafeEnvironmentDataHandle CreateEnvironmentData(int width, int height, string[] source, int[] controlValues)
        {
            if (IntPtr.Size == 8)
            {
                return ffeval64.CreateEnvironmentData(width, height, pixelSize, source, controlValues);
            }
            else
            {
                return ffeval32.CreateEnvironmentData(width, height, pixelSize, source, controlValues);
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
