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

using PdnFF.Interop;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PdnFF
{
    /// <summary>
    /// Encapsulates a ShellLink shortcut file
    /// </summary>
    internal sealed class ShellLink : IDisposable
    {
        private NativeInterfaces.IShellLinkW shellLink;
        private bool disposed;

        [ComImport(), Guid(NativeConstants.CLSID_ShellLink)]
        private class ShellLinkCoClass
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellLink"/> class.
        /// </summary>
        public ShellLink()
        {
            shellLink = (NativeInterfaces.IShellLinkW)new ShellLinkCoClass();
        }

        /// <summary>
        /// Loads a shortcut from a file.
        /// </summary>
        /// <param name="linkPath">The shortcut to load.</param>
        public bool Load(string linkPath)
        {
            return (((NativeInterfaces.IPersistFile)shellLink).Load(linkPath, NativeConstants.STGM_READ) == NativeConstants.S_OK);
        }

        /// <summary>
        /// Gets the target path of the shortcut.
        /// </summary>
        public string Path
        {
            get
            {
                StringBuilder sb = new StringBuilder(NativeConstants.MAX_PATH);

                if (shellLink.GetPath(sb, sb.Capacity, IntPtr.Zero, 0U) != NativeConstants.S_OK)
                {
                    return string.Empty;
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ShellLink"/> is reclaimed by garbage collection.
        /// </summary>
        ~ShellLink()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;

                if (disposing)
                {
                }

                if (shellLink != null)
                {
                    Marshal.ReleaseComObject(shellLink);
                    shellLink = null;
                }
            }
        }
    }
}
