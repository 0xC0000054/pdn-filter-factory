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
		private FilterEnviromentData envdata;
		public void SetupFilterEnviromentData(Surface source)
		{
		if (envdata == null)
			{
				envdata = new FilterEnviromentData(source);
			envdata.ResetEnvir();
			}
		}
		/// <summary>
		/// Sets the Filter source code and Control values
		/// </summary>
		public void SetupFilterData(int[] control_values, string[] source)
		{
		for (int i = 0; i < 8; i++)
		{
		ffparse.SetControls(control_values[i], i);
		}

		for (int i = 0; i < 4; i++)
		{
			IntPtr s = Marshal.StringToHGlobalAnsi(source[i]);
			ffparse.SetupTree(s, i);
			Marshal.FreeHGlobal(s);
		}

		}
		object sync = new object();
		public unsafe void Render(Surface dest, Rectangle rect, Func<bool> cancel)
		{
		for (int y = rect.Top; y < rect.Bottom; ++y)
		{
			ColorBgra *p = dest.GetPointAddressUnchecked(rect.Left, y);
			for (int x = rect.Left; x < rect.Right; ++x)
			{
			if (cancel()) return; // stop if a cancel is requested

			ffparse.UpdateEnvir(x, y); // update the (x, y) position in the unmanaged ffparse data

			p->R = (byte)ffparse.CalcColor(0); // red channel
			p->G = (byte)ffparse.CalcColor(1); // green channel
			p->B = (byte)ffparse.CalcColor(2); // blue channel
			p->A = (byte)ffparse.CalcColor(3); // alpha channel

			p++;
			}
		}
		}

		#region IDisposable Members
		private bool Disposed = false;
		public void Dispose()
		{
			Dispose(true);
		}
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!Disposed)
				{
					if ((envdata != null) && !ffparse.datafreed())
					{
						ffparse.FreeData(); // free the unmanaged ffparse data
#if DEBUG
						System.Diagnostics.Debug.WriteLine("Dispose called");
#endif
						envdata.Dispose(); // free the enviroment data
						envdata = null;
						this.Disposed = true;
					}
				}
			}
		}

		#endregion
	}
}
