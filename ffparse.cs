﻿/*
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
using System.Runtime.InteropServices;

namespace PdnFF
{
    internal static class ffparse
    {
        [System.Security.SuppressUnmanagedCodeSecurity]
        private static class ffeval32
        {
            [DllImport("ffparse_x86.dll", ExactSpelling = true)]
            public static extern int SetupBitmap(IntPtr pixelData, int width, int height, int stride, int pixelSize);
            [DllImport("ffparse_x86.dll", ExactSpelling = true)]
            public static extern void SetControls(int val, int ctl);
            [DllImport("ffparse_x86.dll", ExactSpelling = true)]
            public static extern void UpdateEnvir(int x, int y);
            [DllImport("ffparse_x86.dll", ExactSpelling = true)]
            public static extern void SetupTree([MarshalAs(UnmanagedType.LPStr)] string src, int c);
            [DllImport("ffparse_x86.dll", ExactSpelling = true)]
            public static extern int CalcColor(int c);
            [DllImport("ffparse_x86.dll", ExactSpelling = true)]
            public static extern void FreeData();
            [DllImport("ffparse_x86.dll", ExactSpelling = true)]
            public static extern int ValidateSrc([MarshalAs(UnmanagedType.LPStr)] string src);
            [DllImport("ffparse_x86.dll", ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool datafreed();

        }
        [System.Security.SuppressUnmanagedCodeSecurity]
        private static class ffeval64
        {
            [DllImport("ffparse_x64.dll", ExactSpelling = true)]
            public static extern int SetupBitmap(IntPtr pixelData, int width, int height, int stride, int pixelSize);
            [DllImport("ffparse_x64.dll", ExactSpelling = true)]
            public static extern void SetControls(int val, int ctl);
            [DllImport("ffparse_x64.dll", ExactSpelling = true)]
            public static extern void UpdateEnvir(int x, int y);
            [DllImport("ffparse_x64.dll", ExactSpelling = true)]
            public static extern void SetupTree([MarshalAs(UnmanagedType.LPStr)] string src, int c);
            [DllImport("ffparse_x64.dll", ExactSpelling = true)]
            public static extern int CalcColor(int c);
            [DllImport("ffparse_x64.dll", ExactSpelling = true)]
            public static extern void FreeData();
            [DllImport("ffparse_x64.dll", ExactSpelling = true)]
            public static extern int ValidateSrc([MarshalAs(UnmanagedType.LPStr)] string src);
            [DllImport("ffparse_x64.dll", ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool datafreed();
        }

        /// <summary>
        /// The number channels in the image, always 4 in Paint.NET
        /// </summary>
        private const int pixelSize = 4;
        /// <summary>
        ///  Sets the control value
        /// </summary>
        /// <param name="val">The control value</param>
        /// <param name="ctl">The number of the control to set</param>
        public static void SetControls(int val, int ctl)
        {
            if (IntPtr.Size == 8)
            {
                ffeval64.SetControls(val, ctl);
            }
            else
            {
                ffeval32.SetControls(val, ctl);
            }
        }
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
        /// Setups the unmanaged source code parse tree.
        /// </summary>
        /// <param name="src">The source code.</param>
        /// <param name="c">The channel that the source code belongs to (0 - 3 in RGBA order).</param>
        public static void SetupTree(string src, int c)
        {
            if (IntPtr.Size == 8)
            {
                ffeval64.SetupTree(src, c);
            }
            else
            {
                ffeval32.SetupTree(src, c);
            }
        }

        /// <summary>
        /// Updates the filter enviroment's pixel location to x,y
        /// </summary>
        /// <param name="x">The x pixel position</param>
        /// <param name="y">The y pixel position</param>
        public static void UpdateEnvir(int x, int y)
        {
            if (IntPtr.Size == 8)
            {
                ffeval64.UpdateEnvir(x, y);
            }
            else
            {
                ffeval32.UpdateEnvir(x, y);
            }
        }
        /// <summary>
        /// Calculates the resulting pixel color for the specified channel
        /// </summary>
        /// <param name="channel">The channel to calculate (0 - 3 in RGBA order)</param>
        /// <returns>The resulting pixel color</returns>
        public static int CalcColor(int channel)
        {
            if (IntPtr.Size == 8)
            {
                return ffeval64.CalcColor(channel);
            }
            else
            {
                return ffeval32.CalcColor(channel);
            }
        }

        /// <summary>
        /// Frees the unmanaged Data and cleans up
        /// </summary>
        public static void FreeData()
        {
            if (IntPtr.Size == 8)
            {
                ffeval64.FreeData();
            }
            else
            {
                ffeval32.FreeData();
            }
        }

        /// <summary>
        /// Validates the Filter source code syntax
        /// </summary>
        /// <param name="src">The source code to validate</param>
        /// <returns>1 if source is valid, otherwise 0</returns>
        public static int ValidateSrc(string src)
        {
            int ret = 1;
            if (IntPtr.Size == 8)
            {
                ret = ffeval64.ValidateSrc(src);
            }
            else
            {
                ret = ffeval32.ValidateSrc(src);
            }

            return ret;
        }

        /// <summary>
        /// Gets if the unmanaged Data has been freed
        /// </summary>
        /// <returns>True if the Data has been freed, otherwise false.</returns>
        public static bool datafreed()
        {
            if (IntPtr.Size == 8)
            {
               return ffeval64.datafreed();
            }
            else
            {
               return ffeval32.datafreed();
            }
        }
    }
}
