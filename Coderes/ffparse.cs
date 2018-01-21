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
using System.Runtime.InteropServices;

namespace FFEffect
{
    internal static class ffparse
    {
        private static class ffeval32
        {
            [DllImport("ffparse_x86.dll", EntryPoint = "SetupBitmap")]
            public static extern int SetupBitmap(IntPtr pixelData, int width, int height, int stride, int pixelSize);
            [DllImport("ffparse_x86.dll", EntryPoint = "SetControls")]
            public static extern void SetControls(int val, int ctl);
            [DllImport("ffparse_x86.dll", EntryPoint = "UpdateEnvir")]
            public static extern void UpdateEnvir(int x, int y);
            [DllImport("ffparse_x86.dll", EntryPoint = "SetupTree")]
            public static extern void SetupTree(System.IntPtr src, int c);
            [DllImport("ffparse_x86.dll", EntryPoint = "CalcColor")]
            public static extern int CalcColor(int c);
            [DllImport("ffparse_x86.dll", EntryPoint = "FreeData")]
            public static extern void FreeData();
            [DllImport("ffparse_x86.dll", EntryPoint = "datafreed")]
            [return: MarshalAs(UnmanagedType.I1)]
            public static extern bool datafreed();

        }

        private static class ffeval64
        {
            [DllImport("ffparse_x64.dll", EntryPoint = "SetupBitmap")]
            public static extern int SetupBitmap(IntPtr pixelData, int width, int height, int stride, int pixelSize);
            [DllImport("ffparse_x64.dll", EntryPoint = "SetControls")]
            public static extern void SetControls(int val, int ctl);
            [DllImport("ffparse_x64.dll", EntryPoint = "UpdateEnvir")]
            public static extern void UpdateEnvir(int x, int y);
            [DllImport("ffparse_x64.dll", EntryPoint = "SetupTree")]
            public static extern void SetupTree(System.IntPtr src, int c);
            [DllImport("ffparse_x64.dll", EntryPoint = "CalcColor")]
            public static extern int CalcColor(int c);
            [DllImport("ffparse_x64.dll", EntryPoint = "FreeData")]
            public static extern void FreeData();
            [DllImport("ffparse_x64.dll", EntryPoint = "datafreed")]
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
        /// Sets up the unmanaged access to the Bitmap data
        /// </summary>
        /// <param name="pixeldata">The pointer to the start of the pixeldata, Scan0</param>
        /// <param name="width">The width of the image in Pixels</param>
        /// <param name="height">The height of the image in Pixels</param>
        /// <param name="stride">The stride of the image</param>
        /// <returns>1 on Success, negitive on failure</returns>
        public static int SetupBitmap(IntPtr pixeldata, int width, int height, int stride)
        {
            if (IntPtr.Size == 8)
            {
                return ffeval64.SetupBitmap(pixeldata, width, height, stride, pixelSize);
            }
            else
            {
                return ffeval32.SetupBitmap(pixeldata, width, height, stride, pixelSize);
            }
        }


        /// <summary>
        /// Setups the unmanaged source code parse tree.
        /// </summary>
        /// <param name="src">The (char*) pointer to the  source code.</param>
        /// <param name="c">The channel that the source code belongs to (0 - 3 in RGBA order).</param>
        public static void SetupTree(IntPtr src, int c)
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
        /// Frees the unmanaged data and cleans up
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
        /// Gets if the unmanaged data has been freed
        /// </summary>
        /// <returns>True if the data has been freed, otherwise false.</returns>
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
