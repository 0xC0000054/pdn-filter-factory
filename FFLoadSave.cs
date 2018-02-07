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
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;
using PaintDotNet.IO;

namespace PdnFF
{
	internal static class FFLoadSave
	{
		private sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			private SafeLibraryHandle() : base(true)
			{
			}

			protected override bool ReleaseHandle()
			{
				return UnsafeNativeMethods.FreeLibrary(handle);
			}
		}


		[System.Security.SuppressUnmanagedCodeSecurity]
		private static class UnsafeNativeMethods
		{
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			public static extern SafeLibraryHandle LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool EnumResourceNames([In()]IntPtr hModule, [In]string lpszType, EnumResNameDelegate lpEnumFunc, IntPtr lParam);

			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			public static extern IntPtr FindResource([In()]IntPtr hModule, [In()]IntPtr lpName, [In()]IntPtr lpType);

			[DllImport("Kernel32.dll", EntryPoint = "SizeofResource", SetLastError = true)]
			public static extern uint SizeofResource(IntPtr hModule, IntPtr hReSource);

			[DllImport("Kernel32.dll", EntryPoint = "LoadResource", SetLastError = true)]
			public static extern IntPtr LoadResource(IntPtr hModule, IntPtr hReSource);

			[DllImport("Kernel32.dll", EntryPoint = "LockResource")]
			public static extern IntPtr LockResource(IntPtr hGlobal);

			[DllImport("kernel32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool FreeLibrary(IntPtr hModule);
		}

		private const uint LOAD_LIBRARY_AS_DATAFILE = 0x00000002;
		#region EnumRes
		private delegate bool EnumResNameDelegate(IntPtr hModule, IntPtr lpszType, IntPtr lpszName, IntPtr lParam);
		private static bool IS_INTRESOURCE(IntPtr value)
		{
			if (((uint)value) > ushort.MaxValue)
			{
				return false;
			}
			return true;
		}

		private static bool EnumRes(IntPtr hModule, IntPtr lpszType, IntPtr lpszName, IntPtr lParam)
		{
			GCHandle gch = GCHandle.FromIntPtr(lParam);
			if (IS_INTRESOURCE(lpszName))
			{
				IntPtr hres = UnsafeNativeMethods.FindResource(hModule, lpszName, lpszType);

				if (hres != IntPtr.Zero)
				{
					IntPtr loadres = UnsafeNativeMethods.LoadResource(hModule, hres);
					if (loadres != IntPtr.Zero)
					{
						IntPtr reslock = UnsafeNativeMethods.LockResource(loadres);
						if (reslock != IntPtr.Zero)
						{
							uint ds = UnsafeNativeMethods.SizeofResource(hModule, hres);

							if (ds == 8296) // All valid Filter Factory resources are this size
							{
								byte[] ffdata = new byte[ds];
								Marshal.Copy(reslock, ffdata, 0, (int)ds);

								gch.Target = ffdata;
							}
						}
					}
				}
			}

			// Only read the first PARM resource.
			return false;
		}
		#endregion

		/// <summary>
		///  Builds a string from a byte array containing ASCII characters.
		/// </summary>
		/// <param name="buf">The input buffer.</param>
		/// <returns>The resulting string</returns>
		private static string StringFromASCIIBytes(byte[] buf)
		{
			int terminatorIndex = Array.IndexOf(buf, (byte)0);
			int length = terminatorIndex != -1 ? terminatorIndex : buf.Length;

			StringBuilder builder = new StringBuilder(length);

			for (int i = 0; i < length; i++)
			{
				char value = (char)buf[i];

				if (value == '\r' || value == '\n')
				{
					if (buf.Length <= 256)
					{
						builder.Append(' ');
					}
				}
				else
				{
					builder.Append(value);
				}
			}

			return builder.ToString();
		}
		/// <summary>
		/// Loads a Filter Factory file, automatically determining the type.
		/// </summary>
		/// <param name="FileName">The FileName to load.</param>
		/// <param name="data">The output filter_data</param>
		/// <returns>True if successful otherwise false.</returns>
		/// <exception cref="System.ArgumentNullException">The FileName is null.</exception>
		/// <exception cref="System.ArgumentException">The FileName is Empty.</exception>
		public static bool LoadFile(string fileName, out FilterData data)
		{
			bool loaded = false;

			if (fileName == null)
				throw new ArgumentNullException("fileName");

			if (string.IsNullOrEmpty(fileName))
				throw new ArgumentException("fileName must not be empty", "fileName");

			data = null;

			if (Path.GetExtension(fileName).Equals(".8BF", StringComparison.OrdinalIgnoreCase))
			{
				loaded = LoadBinFile(fileName, out data);
			}
			else
			{
				using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
				{
					byte[] buf = new byte[8];

					fs.ProperRead(buf, 0, 8);
					if (Encoding.ASCII.GetString(buf, 0, 4).Equals("%RGB"))
					{
						LoadAfs(fs, out data);
						loaded = true;
					}
					else if (Encoding.ASCII.GetString(buf, 0, 8).Equals("Category"))
					{
						LoadTxt(fs, out data);
						loaded = true;
					}

				}
			}

			return loaded;
		}

		/// <summary>
		/// Load the Filter Factory data from the specified 8bf file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="data">The data.</param>
		/// <returns>
		/// <c>true</c> if the 8bf file is a Filter Factory filter and the data was loaded successfully; otherwise, <c>false</c>
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
		public static bool LoadFrom8bf(string path, out FilterData data)
		{
			if (path == null)
			{
				throw new ArgumentNullException(nameof(path));
			}

			return LoadBinFile(path, out data);
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage(
			"Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods",
			MessageId = "System.Runtime.InteropServices.SafeHandle.DangerousGetHandle",
			Justification = "Required by EnumResourceNames due to marshaller restrictions.")]
		private static bool LoadBinFile(String fileName, out FilterData data)
		{
			if (String.IsNullOrEmpty(fileName))
				throw new ArgumentException("fileName is null or empty.", "fileName");

			data = null;
			bool result = false;

			using (SafeLibraryHandle hm = UnsafeNativeMethods.LoadLibraryEx(fileName, IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE))
			{
				if (!hm.IsInvalid)
				{
					byte[] ffdata = null;

					GCHandle gch = GCHandle.Alloc(ffdata);
					bool needsRelease = false;
					try
					{
						hm.DangerousAddRef(ref needsRelease);
						IntPtr hMod = hm.DangerousGetHandle();
						if (!UnsafeNativeMethods.EnumResourceNames(hMod, "PARM", new EnumResNameDelegate(EnumRes), GCHandle.ToIntPtr(gch)))
						{
							ffdata = (byte[])gch.Target;
							if (ffdata != null)
							{
								data = GetFilterDataFromParmBytes(ffdata);
								result = true;
							}
						}
					}
					finally
					{
						gch.Free();
						if (needsRelease)
						{
							hm.DangerousRelease();
						}
					}

				}
			}


			return result;

		}
		/// <summary>
		/// Builds the filter_data values from the Filter Factory PARM ReSource
		/// </summary>
		/// <param name="parmbytes">The PARM reSource byte array</param>
		/// <returns>The filter data constructed from the PARM resource.</returns>
		private static FilterData GetFilterDataFromParmBytes(byte[] parmbytes)
		{
			if (parmbytes == null || parmbytes.Length == 0)
				throw new ArgumentException("parmbytes is null or empty.", "parmbytes");
			MemoryStream ms = null;
			FilterData data = new FilterData();

			try
			{
				ms = new MemoryStream(parmbytes);
				using (BinaryReader br = new BinaryReader(ms, Encoding.Default))
				{
					ms = null;

#if false
					int cbsize = br.ReadInt32();
					int standalone = br.ReadInt32();
#else
					br.BaseStream.Position += 8L;
#endif

					for (int i = 0; i < 8; i++)
					{
						data.ControlValue[i] = br.ReadInt32();
					}
					data.PopDialog = br.ReadInt32() != 0;

					br.BaseStream.Position += 12; // Skip the 3 unknown data values

					for (int i = 0; i < 4; i++)
					{
						data.MapEnable[i] = br.ReadInt32() != 0;
					}

					for (int i = 0; i < 8; i++)
					{
						data.ControlEnable[i] = br.ReadInt32() != 0;
					}
					data.Category = StringFromASCIIBytes(br.ReadBytes(252)); // read the Category

					//Michael Johannhanwahr's protect flag...
#if false
					int iProtected = br.ReadInt32(); // 1 means protected
#else
					br.BaseStream.Position += 4L;
#endif
					data.Title = StringFromASCIIBytes(br.ReadBytes(256));
					data.Copyright = StringFromASCIIBytes(br.ReadBytes(256));
					data.Author = StringFromASCIIBytes(br.ReadBytes(256));

					for (int i = 0; i < 4; i++)
					{
						data.MapLabel[i] = StringFromASCIIBytes(br.ReadBytes(256));
					}

					for (int i = 0; i < 8; i++)
					{
						data.ControlLabel[i] = StringFromASCIIBytes(br.ReadBytes(256));
					}

					for (int i = 0; i < 4; i++)
					{
						data.Source[i] = StringFromASCIIBytes(br.ReadBytes(1024));
					}

				}
			}
			finally
			{
				if (ms != null)
				{
					ms.Close();
				}
			}

			return data;
		}
		private static void LoadTxt(Stream infile, out FilterData data)
		{
			if (infile == null)
				throw new ArgumentNullException("infile", "infile is null.");

			data = new FilterData();
			infile.Position = 0L;

			string line = string.Empty;
			bool ctlread = false;
			bool inforead = false;
			bool srcread = false;
			bool valread = false;
			using (StreamReader sr = new StreamReader(infile, Encoding.Default))
			{
				while ((line = sr.ReadLine()) != null)
				{
					if (!string.IsNullOrEmpty(line))
					{
						if (!inforead)
						{
							for (int i = 0; i < 4; i++)
							{
								if (!string.IsNullOrEmpty(line))
								{
									string[] split = line.Split(new char[] { ':' }, StringSplitOptions.None);

									if (!string.IsNullOrEmpty(split[1]))
									{
										switch (split[0])
										{
											case "Category":
												data.Category = split[1].Trim();
												break;
											case "Title":
												data.Title = split[1].Trim();
												break;
											case "Copyright":
												data.Copyright = split[1].Trim();
												break;
											case "Author":
												data.Author = split[1].Trim();
												break;
										}
									}
									line = sr.ReadLine();

								}
								else
								{
									i = (i - 1);
								}
							}
							inforead = true;
						}
						else if (line.StartsWith("Filename", StringComparison.Ordinal))
						{
							continue;
						}
						else
						{
							if (!srcread)
							{
								for (int i = 0; i < 4; i++)
								{
									if (!string.IsNullOrEmpty(line))
									{
										string id = line.Substring(0, 1).ToUpperInvariant();
										string src = line.Substring(2, (line.Length - 2)).Trim();

										if (!string.IsNullOrEmpty(src))
										{
											switch (id)
											{
												case "R":
													data.Source[0] = src;
													break;
												case "G":
													data.Source[1] = src;
													break;
												case "B":
													data.Source[2] = src;
													break;
												case "A":
													data.Source[3] = src;
													break;
											}
										}


									}
									else
									{
										i = (i - 1);
									}
									line = sr.ReadLine();
								}
								srcread = true;
							}
							else
							{
								if (!ctlread)
								{

									while (!string.IsNullOrEmpty(line) && line.StartsWith("ctl", StringComparison.Ordinal))
									{
										string lbl = line.Substring(7, (line.Length - 7)).Trim();
										int cn = int.Parse(line[4].ToString(), CultureInfo.InvariantCulture); // get the control number
										if (!string.IsNullOrEmpty(lbl))
										{
											data.ControlLabel[cn] = lbl;
											data.ControlEnable[cn] = true;
										}
										line = sr.ReadLine();
									}

									bool[] mapsUsed = UsesMap(data.Source);
									for (int i = 0; i < 4; i++)
									{
										data.MapLabel[i] = string.Format(CultureInfo.InvariantCulture, "Map: {0}", i.ToString(CultureInfo.InvariantCulture));
										data.MapEnable[i] = mapsUsed[i];
									}
									data.PopDialog = HasControls(data);

									ctlread = true;
								}
								else
								{

									while (!string.IsNullOrEmpty(line) && line.StartsWith("val", StringComparison.Ordinal))
									{
										string lbl = line.Substring(7, (line.Length - 7)).Trim();
										int cn = int.Parse(line[4].ToString(), CultureInfo.InvariantCulture); // get the control number
										if (!string.IsNullOrEmpty(lbl))
										{
											data.ControlValue[cn] = int.Parse(lbl, CultureInfo.InvariantCulture);
										}
										line = sr.ReadLine();
									}
									valread = true;
								}
							}
						}

					}
				}
			}
			if (!ctlread && !valread)
			{
				bool[] mapsUsed = UsesMap(data.Source);
				bool[] ctlsUsed = UsesCtl(data.Source);
				for (int i = 0; i < 4; i++)
				{
					data.MapLabel[i] = string.Format(CultureInfo.InvariantCulture, "Map: {0}", i.ToString(CultureInfo.InvariantCulture));
					data.MapEnable[i] = mapsUsed[i];
				}
				for (int i = 0; i < 8; i++)
				{
					data.ControlLabel[i] = string.Format(CultureInfo.InvariantCulture, "Control: {0}", i.ToString(CultureInfo.InvariantCulture));
					data.ControlEnable[i] = ctlsUsed[i];
				}
				data.PopDialog = HasControls(data);
			}
		}

		private static string ReadAfsString(BinaryReader br)
		{
			StringBuilder sb = new StringBuilder();

			char value = br.ReadChar();

			if (value == '\r')
			{
				// Return null for a blank line to allow the lines that separate the source code for
				// the different channels to be distinguished from a line that only contains whitespace.
				return null;
			}
			else
			{
				do
				{
					if (value == '\\')
					{
						char nextChar = br.ReadChar();
						if (nextChar == 'r')
						{
							// Exit if the string contains an embedded carriage return.
							break;
						}
					}
					else
					{
						sb.Append(value);
					}

					value = br.ReadChar();

				} while (value != '\r');
			}

			return sb.ToString().Trim();
		}

		private static void LoadAfs(Stream infile, out FilterData data)
		{
			if (infile == null)
				throw new ArgumentNullException("infile", "infile is null.");

			data = new FilterData();

			infile.Position = 9L; // we have already read the signature skip it
			string line = string.Empty;
			bool ctlread = false;
			bool srcread = false;

			using (BinaryReader br = new BinaryReader(infile, Encoding.Default))
			{
				if (!ctlread)
				{
					for (int i = 0; i < 8; i++)
					{
						line = ReadAfsString(br);
						if (!string.IsNullOrEmpty(line) && line.Length <= 3)
						{
							data.ControlValue[i] = int.Parse(line, CultureInfo.InvariantCulture);
						}
					}
					ctlread = true;
				}

				if (!srcread)
				{
					StringBuilder builder = new StringBuilder(1024);
					int i = 0;
					while (i < 4)
					{
						line = ReadAfsString(br);
						if (line == null)
						{
							data.Source[i] = builder.ToString();
							builder.Length = 0;
							i++;
						}
						else
						{
							if (line.Length > 0)
							{
								builder.Append(line);
							}
						}

					}

					srcread = true;
				}


			}

			data.Category = "Filter Factory";
			FileStream fs = infile as FileStream;
			string Title;
			if (fs != null)
			{
				Title = Path.GetFileName(fs.Name);
			}
			else
			{
				Title = "Untitled";
			}
			data.Title = Title;
			data.Author = "Unknown";
			data.Copyright = "Copyright © Unknown";
			bool[] mapsUsed = UsesMap(data.Source);
			bool[] ctlsUsed = UsesCtl(data.Source);
			for (int i = 0; i < 4; i++)
			{
				data.MapLabel[i] = string.Format(CultureInfo.InvariantCulture, "Map: {0}", new object[] { i.ToString(CultureInfo.InvariantCulture) });
				data.MapEnable[i] = mapsUsed[i];
			}
			for (int i = 0; i < 8; i++)
			{
				data.ControlLabel[i] = string.Format(CultureInfo.InvariantCulture, "Control: {0}", new object[] { i.ToString(CultureInfo.InvariantCulture) });
				data.ControlEnable[i] = ctlsUsed[i];
			}
			data.PopDialog = HasControls(data);

		}
		private static void SaveAfs(Stream output, FilterData data)
		{
			if (output == null)
				throw new ArgumentNullException("output", "output is null.");
			if (data == null)
				throw new ArgumentNullException("data", "data is null.");

			using (StreamWriter sw = new StreamWriter(output, Encoding.Default))
			{
				sw.NewLine = "\r"; // Filter factory uses the Mac end of line format
				sw.WriteLine("%RGB-1.0");
				for (int i = 0; i < 8; i++)
				{
					sw.WriteLine(data.ControlValue[i]);
				}

				for (int i = 0; i < 4; i++)
				{
					if (data.Source[i].Length > 63) // split the items into lines 63 chars long
					{
						string temp = data.Source[i];
						int pos = 0;
						List<string> srcarray = new List<string>();
						while ((temp.Length - pos) > 63)
						{
							string sub = temp.Substring(pos, 63);
							pos += 63;

							srcarray.Add(sub);

							if ((temp.Length - pos) < 63)
							{
								string remain = temp.Substring(pos);
								remain += "\r"; // add an extra return to write an extra line
								srcarray.Add(remain);
							}
						}

						foreach (var item in srcarray)
						{
							sw.WriteLine(item);
						}

					}
					else
					{
						sw.WriteLine(data.Source[i] + "\r"); // add an extra return to write an extra line
					}

				}

			}
		}
		private static void SaveTxt(Stream output, FilterData data)
		{
			if (output == null)
				throw new ArgumentNullException("output", "output is null.");
			if (data == null)
				throw new ArgumentNullException("data", "data is null.");

			using (StreamWriter sw = new StreamWriter(output, Encoding.Default))
			{
				sw.WriteLine("{0}: {1}", "Category", data.Category);
				sw.WriteLine("{0}: {1}", "Title", data.Title);
				sw.WriteLine("{0}: {1}", "Copyright", data.Copyright);
				sw.WriteLine("{0}: {1}", "Author", data.Author);

				sw.WriteLine(Environment.NewLine);

				sw.WriteLine("R: {0}", data.Source[0] + Environment.NewLine);
				sw.WriteLine("G: {0}", data.Source[1] + Environment.NewLine);
				sw.WriteLine("B: {0}", data.Source[2] + Environment.NewLine);
				sw.WriteLine("A: {0}", data.Source[3] + Environment.NewLine);

				sw.WriteLine(Environment.NewLine);
				for (int i = 0; i < 8; i++)
				{
					if (data.ControlEnable[i])
					{
						sw.WriteLine(string.Format(CultureInfo.InvariantCulture, "ctl[{0}]: {1}", i.ToString(CultureInfo.InvariantCulture), data.ControlLabel[i]));
					}
				}

				sw.WriteLine(Environment.NewLine);

				for (int i = 0; i < 8; i++)
				{
					if (data.ControlEnable[i])
					{
						sw.WriteLine(string.Format(CultureInfo.InvariantCulture, "val[{0}]: {1}", i.ToString(CultureInfo.InvariantCulture), data.ControlValue[i]));
					}
				}
			}
		}
		/// <summary>
		/// Saves a filter_data Source as either an .afs or .txt Source code
		/// </summary>
		/// <param name="FileName">The output FileName to save as</param>
		/// <param name="data">The filter_data to save</param>
		public static void SaveFile(string FileName, FilterData data)
		{
			if (String.IsNullOrEmpty(FileName))
				throw new ArgumentException("FileName is null or empty.", "FileName");
			if (data == null)
				throw new ArgumentNullException("data", "data is null.");

			using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write))
			{
				if (Path.GetExtension(FileName).Equals(".afs", StringComparison.OrdinalIgnoreCase))
				{
					SaveAfs(fs, data);
				}
				else
				{
					SaveTxt(fs, data);
				}
			}
		}
		/// <summary>
		/// Gets a FilterData set to the default values.
		/// </summary>
		/// <returns></returns>The FilterData set to the default values.</param>
		public static FilterData DefaultFilter()
		{
			FilterData data = new FilterData();

			string[] src = new string[] { "r", "g", "b", "a" };
			data.Category = "Untitled";
			data.Title = "Untitled";
			data.Author = Environment.UserName;
			data.Copyright = string.Format(CultureInfo.InvariantCulture, "Copyright © {0} {1}", DateTime.Now.Year.ToString(CultureInfo.CurrentCulture), Environment.UserName);

			data.PopDialog = true;
			for (int i = 0; i < 4; i++)
			{
				data.MapLabel[i] = string.Format(CultureInfo.InvariantCulture, "Map {0}:", i.ToString(CultureInfo.InvariantCulture));
				data.MapEnable[i] = false;
			}
			for (int i = 0; i < 8; i++)
			{
				data.ControlLabel[i] = string.Format(CultureInfo.InvariantCulture, "ctl {0}", i.ToString(CultureInfo.InvariantCulture));
				data.ControlValue[i] = 0;
				data.ControlEnable[i] = true;
			}

			for (int i = 0; i < 4; i++)
			{
				data.Source[i] = src[i];
			}

			return data;
		}
		/// <summary>
		/// Reads a string from a FFL file.
		/// </summary>
		/// <param name="br">The BinaryReader to read from.</param>
		/// <returns></returns>
		private static string ReadFFLString(BinaryReader br)
		{
			StringBuilder sb = new StringBuilder();

			while (true)
			{
				char value = br.ReadChar();

				if (value == '\r' || value == '\n')
				{
					if (value == '\r' && br.PeekChar() == '\n')
					{
						br.ReadChar();
					}

					break;
				}

				sb.Append(value);
			}
			return sb.ToString().Trim();
		}
		/// <summary>
		/// Loads A Filter Factory Library file
		/// </summary>
		/// <param name="fn">The FileName to load.</param>
		/// <param name="items">The list of filters in the file.</param>
		/// <returns>True if successful otherwise false.</returns>
		/// <exception cref="System.ArgumentNullException">The Filename is null.</exception>
		/// <exception cref="System.ArgumentException">The Filename is empty.</exception>
		public static bool LoadFFL(string fn, out List<FilterData> items)
		{
			bool loaded = false;
			FileStream fs = null;
			try
			{
				if (fn == null)
					throw new ArgumentNullException("fn");

				if (string.IsNullOrEmpty(fn))
					throw new ArgumentException("Filename must not be empty", "fn");

				items = new List<FilterData>();

				fs = new FileStream(fn, FileMode.Open, FileAccess.Read, FileShare.None);

				using (BinaryReader br = new BinaryReader(fs))
				{
					fs = null;
					string id = ReadFFLString(br);
					if (id != null && id == "FFL1.0")
					{
						string num = ReadFFLString(br);
						if (!string.IsNullOrEmpty(num))
						{
							int len = int.Parse(num, CultureInfo.InvariantCulture);

							try
							{
								for (int i = 0; i < len; i++)
								{
									items.Add(GetFilterfromFFL(br));
								}
							}
							catch (EndOfStreamException)
							{
								// ignore it
							}

							loaded = true;
						}

					}

				}

			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				if (fs != null)
				{
					fs.Close();
				}
			}
			return loaded;
		}
		/// <summary>
		/// Extracts a filter from the current location in the FFL
		/// </summary>
		/// <param name="br">The BimaryReader to read from.</param>
		/// <returns>The extracted filter data.</returns>
		/// <exception cref="System.ArgumentNullException">The BinaryReader is null.</exception>
		private static FilterData GetFilterfromFFL(BinaryReader br)
		{
			if (br == null)
				throw new ArgumentNullException("br", "br is null.");

			FilterData data = new FilterData
			{
				FileName = ReadFFLString(br),
				Category = ReadFFLString(br),
				Title = ReadFFLString(br),
				Author = ReadFFLString(br),
				Copyright = ReadFFLString(br)
			};

			for (int i = 0; i < 4; i++)
			{
				string map = ReadFFLString(br);
				if (!string.IsNullOrEmpty(map))
				{
					data.MapLabel[i] = map;
					data.MapEnable[i] = true;
				}
				else
				{
					data.MapLabel[i] = string.Format(CultureInfo.InvariantCulture, "Map {0}:", i.ToString(CultureInfo.InvariantCulture));
					data.MapEnable[i] = false;
				}
			}
			for (int i = 0; i < 8; i++)
			{
				string ctl = ReadFFLString(br);
				if (!string.IsNullOrEmpty(ctl))
				{
					data.ControlLabel[i] = ctl;
					data.ControlEnable[i] = true;
				}
				else
				{
					data.ControlLabel[i] = string.Format(CultureInfo.InvariantCulture, "Control: {0}", i.ToString(CultureInfo.InvariantCulture));
					data.ControlEnable[i] = false;
				}
			}

			for (int i = 0; i < 8; i++)
			{
				string cv = ReadFFLString(br);
				data.ControlValue[i] = int.Parse(cv, CultureInfo.InvariantCulture);
			}

			string[] rgba = new string[] { "r", "g", "b", "a" };

			for (int i = 0; i < 4; i++)
			{
				string src = ReadFFLString(br);
				if (!string.IsNullOrEmpty(src))
				{
					data.Source[i] = src;
				}
				else
				{
					data.Source[i] = rgba[i];
				}
			}

			return data;
		}

		private static bool HasControls(FilterData data)
		{
			bool ctlused = false;
			bool mapused = false;

			for (int i = 0; i < 4; i++)
			{
				mapused |= data.MapEnable[i];
			}

			for (int i = 0; i < 8; i++)
			{
				ctlused |= data.ControlEnable[i];
			}

			return (mapused | ctlused);
		}
		private static bool[] UsesMap(string[] Source)
		{
			bool[] mapsUsed = new bool[4] { false, false, false, false, };

			for (int i = 0; i < 4; i++)
			{
				string src = Source[i];

				if (src.Contains("map"))
				{
					int startOffset = 0;
					int pos = -1;
					while ((pos = src.IndexOf("map", startOffset, StringComparison.Ordinal)) > 0)
					{
						startOffset = pos + 3;
						int mapNumberOffset = pos + 4;

						int map = int.Parse(src[mapNumberOffset].ToString(), CultureInfo.InvariantCulture);
						if (map >= 0 && map <= 3)
						{
							mapsUsed[map] = true;
						}
					}
				}
			}

			return mapsUsed;
		}
		private static bool[] UsesCtl(string[] Source)
		{
			bool[] controlsUsed = new bool[8] { false, false, false, false, false, false, false, false };
			for (int i = 0; i < 4; i++)
			{
				string src = Source[i];

				if (src.Contains("ctl"))
				{
					int startOffset = 0;
					int pos = -1;
					while ((pos = src.IndexOf("ctl", startOffset, StringComparison.Ordinal)) > 0)
					{
						startOffset = pos + 3;
						int ctlNumberOffset = pos + 4;

						int ctl = int.Parse(src[ctlNumberOffset].ToString(), CultureInfo.InvariantCulture);
						if (ctl >= 0 && ctl <= 7)
						{
							controlsUsed[ctl] = true;
						}
					}
				}
				if (src.Contains("val"))
				{
					int startOffset = 0;
					int pos = -1;
					while ((pos = src.IndexOf("val", startOffset, StringComparison.Ordinal)) > 0)
					{
						startOffset = pos + 3;
						int ctlNumberOffset = pos + 4;

						int ctl = int.Parse(src[ctlNumberOffset].ToString(), CultureInfo.InvariantCulture);
						if (ctl >= 0 && ctl <= 7)
						{
							controlsUsed[ctl] = true;
						}
					}
				}
			}
			return controlsUsed;
		}
	}
}
