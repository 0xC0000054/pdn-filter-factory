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
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace PdnFF
{
	internal static class FFLoadSave
	{


		private static class NativeMethods
		{
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool EnumResourceNames([In()]IntPtr hModule, [In]string lpszType, EnumResNameDelegate lpEnumFunc, [MarshalAs(UnmanagedType.SysInt)]IntPtr lParam);

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
				IntPtr fr = NativeMethods.FindResource(hModule, lpszName, lpszType);

				gch.Target = fr;

				return true;
			}
			return false;
		}
		#endregion

		/// <summary>
		///  Builds a string from a variable length char array.
		/// </summary>
		/// <param name="buf">The input buffer.</param>
		/// <returns>The resulting string</returns>
		private static string StringFromChar(char[] buf)
		{
			string temp = new string(buf);
			temp = temp.Substring(0, temp.IndexOf("\0", StringComparison.Ordinal)).Trim();

			for (int i = 0; i < temp.Length; i++)
			{
				if (temp[i] == '\r' || temp[i] == '\n')
				{
					if (buf.Length > 256)
					{
						temp = temp.Remove(i, 1);
						i = (i - 1);
					}
					else
					{
						temp = temp.Replace(temp[i], ' ');
					}
				}
			}

			return temp;
		}
		/// <summary>
		/// Loads a Filter Factory file, auitomatically determining the type.
		/// </summary>
		/// <param name="FileName">The FileName to load.</param>
		/// <param name="data">The output filter_data</param>
		/// <returns>True if successful otherwise false.</returns>
		/// <exception cref="System.ArgumentNullException">The FileName is null.</exception>
		/// <exception cref="System.ArgumentNullException">The data is null.</exception>
		/// <exception cref="System.ArgumentException">The FileName is Empty.</exception>
		public static bool LoadFile(string fileName, filter_data data)
		{
			bool loaded = false;
			try
			{
				if (fileName == null)
					throw new ArgumentNullException("fileName");

				if (string.IsNullOrEmpty(fileName))
					throw new ArgumentException("fileName must not be empty", "fileName");

				if (data == null)
					throw new ArgumentNullException("data");

				if (Path.GetExtension(fileName).ToUpperInvariant() == ".8BF")
				{
				   loaded = LoadBinFile(fileName, data);
				}
				else
				{
					using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
					{
						byte[] buf = new byte[32];

						fs.Read(buf, 0, 16);
						if (Encoding.ASCII.GetString(buf, 0, 4).Equals("%RGB"))
						{
							LoadAfs(fs, data);
							loaded = true;
						}
						else if (Encoding.ASCII.GetString(buf, 0, 8).Equals("Category"))
						{
							LoadTxt(fs, data);
							loaded = true;
						}

					}
				}


			}
			catch (Exception)
			{
				throw;
			}

			return loaded;
		}

		private static bool LoadBinFile(String fileName, filter_data data)
		{
			if (String.IsNullOrEmpty(fileName))
				throw new ArgumentException("fileName is null or empty.", "fileName");
			if (data == null)
				throw new ArgumentNullException("data", "data is null.");
			bool result = false;
			try
			{
				IntPtr hm = NativeMethods.LoadLibraryEx(fileName, IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);
				if (hm != IntPtr.Zero)
				{
					IntPtr hres = IntPtr.Zero;

					GCHandle gch = GCHandle.Alloc(hres);
					try
					{

						if (NativeMethods.EnumResourceNames(hm, "PARM", new EnumResNameDelegate(EnumRes), GCHandle.ToIntPtr(gch)))
						{
							hres = (IntPtr)gch.Target;
							if (hres != IntPtr.Zero)
							{
								IntPtr loadres = NativeMethods.LoadResource(hm, hres);
								if (loadres != IntPtr.Zero)
								{
									IntPtr reslock = NativeMethods.LockResource(loadres);
									if (reslock != IntPtr.Zero)
									{
										uint ds = NativeMethods.SizeofResource(hm, hres);

										if (ds == 8296) // All valid Filter Factory reSources are this size
										{
											byte[] ffdata = new byte[ds];
											Marshal.Copy(reslock, ffdata, 0, (int)ds);
											GetFilterDataFromParmBytes(data, ffdata);
											result = true;
										}
										NativeMethods.FreeLibrary(hm);
									}
									else
									{
										NativeMethods.FreeLibrary(hm);
										throw new Win32Exception(Marshal.GetLastWin32Error());
									}
								}
								else
								{
									NativeMethods.FreeLibrary(hm);
									throw new Win32Exception(Marshal.GetLastWin32Error());
								}
							}
							else
							{
								NativeMethods.FreeLibrary(hm);
								throw new Win32Exception(Marshal.GetLastWin32Error());
							}
						}
						else // Not a Filter Factory Filter
						{
							NativeMethods.FreeLibrary(hm);
						}

					}
					finally
					{
						gch.Free();
					}

				}
				else
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}


			}
			catch (Exception)
			{
				throw;
			}

			return result;

		}
		/// <summary>
		/// Builds the filter_data values from the Filter Factory PARM ReSource
		/// </summary>
		/// <param name="data">The filter_data to build</param>
		/// <param name="parmbytes">The PARM reSource byte array</param>
		private static void GetFilterDataFromParmBytes(filter_data data, byte[] parmbytes)
		{
			if (data == null)
				throw new ArgumentNullException("data", "data is null.");
			if (parmbytes == null || parmbytes.Length == 0)
				throw new ArgumentException("parmbytes is null or empty.", "parmbytes");
			MemoryStream ms = null;

			try
			{
				ms = new MemoryStream(parmbytes);
				using (BinaryReader br = new BinaryReader(ms, Encoding.Default))
				{
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
					data.PopDialog = br.ReadInt32();

					br.BaseStream.Position += 12; // Skip the 3 unknown data values

					for (int i = 0; i < 4; i++)
					{
						data.MapEnable[i] = br.ReadInt32();
					}

					for (int i = 0; i < 8; i++)
					{
						data.ControlEnable[i] = br.ReadInt32();
					}
					data.Category = StringFromChar(br.ReadChars(252)); // read the Category

					//Michael Johannhanwahr's protect flag...
#if false
					int iProtected = br.ReadInt32(); // 1 means protected
#else
					br.BaseStream.Position += 4L;
#endif
					data.Title = StringFromChar(br.ReadChars(256));
					data.Copyright = StringFromChar(br.ReadChars(256));
					data.Author = StringFromChar(br.ReadChars(256));

					for (int i = 0; i < 4; i++)
					{
						data.MapLabel[i] = StringFromChar(br.ReadChars(256));
					}

					for (int i = 0; i < 8; i++)
					{
						data.ControlLabel[i] = StringFromChar(br.ReadChars(256));
					}

					for (int i = 0; i < 4; i++)
					{
						data.Source[i] = StringFromChar(br.ReadChars(1024));
					}

				}
				ms = null;
			}
			finally
			{
				if (ms != null)
				{
					ms.Close();
				}
			}

		}
		private static void LoadTxt(Stream infile, filter_data data)
		{
			if (infile == null)
				throw new ArgumentNullException("infile", "infile is null.");
			if (data == null)
				throw new ArgumentNullException("data", "data is null.");
			infile.Position = 0L;
			try
			{
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
												data.ControlEnable[cn] = 1;
											}
											line = sr.ReadLine();
										}

										for (int i = 0; i < 4; i++)
										{
											data.MapLabel[i] = string.Format(CultureInfo.InvariantCulture, "Map: {0}", i.ToString(CultureInfo.InvariantCulture));
											data.MapEnable[i] = UsesMap(data.Source, i) ? 1 : 0;
										}
										SetPopDialog(data);

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
					for (int i = 0; i < 4; i++)
					{
						data.MapLabel[i] = string.Format(CultureInfo.InvariantCulture, "Map: {0}", i.ToString(CultureInfo.InvariantCulture));
						data.MapEnable[i] = UsesMap(data.Source, i) ? 1 : 0;
					}
					for (int i = 0; i < 8; i++)
					{
						data.ControlLabel[i] = string.Format(CultureInfo.InvariantCulture, "Control: {0}", i.ToString(CultureInfo.InvariantCulture));
						data.ControlEnable[i] = UsesCtl(data.Source, i) ? 1 : 0;
					}
					SetPopDialog(data);
				}


			}
			catch (Exception)
			{
				throw;
			}

		}

		private static string ReadAfsString(BinaryReader br, int length)
		{
			StringBuilder sb = new StringBuilder();
			char lastChar = br.ReadChar();
			for (int i = 0; i < length; i++)
			{

				try
				{
					char newChar = br.ReadChar();
					bool isLineBreak = (lastChar == 0x5c && newChar == 'r');


					if (lastChar == '\r')
					{

						br.BaseStream.Position -= 1L;
						break;
					}

					if (lastChar != 0x5c || !isLineBreak)
					{
						sb.Append(lastChar);
					}

					if (!isLineBreak)
					{
						lastChar = newChar;
					}
					else
					{
						lastChar = ' ';
					}

				}
				catch (EndOfStreamException)
				{
					sb.Append(lastChar);
					break;
				}
			}
			return sb.ToString().Trim();
		}

		private static void LoadAfs(Stream infile, filter_data data)
		{
			if (infile == null)
				throw new ArgumentNullException("infile", "infile is null.");
			if (data == null)
				throw new ArgumentNullException("data", "data is null.");
			try
			{
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
							line = ReadAfsString(br, 256);
							if (!string.IsNullOrEmpty(line) && line.Length <= 3)
							{
								data.ControlValue[i] = int.Parse(line, CultureInfo.InvariantCulture);
							}
						}
						ctlread = true;
					}

					if (!srcread)
					{
						int i = 0;
						while (i < 4)
						{
							line = ReadAfsString(br, 1024);
							if (!string.IsNullOrEmpty(line))
							{
								if (!string.IsNullOrEmpty(data.Source[i]) && (data.Source[i].Length + line.Length) < 1024)
								{
									data.Source[i] += line; // append the line to te existing Source
								}
								else
								{
									data.Source[i] = line;
								}
							}
							else
							{
								i++;
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
				for (int i = 0; i < 4; i++)
				{
					data.MapLabel[i] = string.Format(CultureInfo.InvariantCulture, "Map: {0}", new object[] { i.ToString(CultureInfo.InvariantCulture) });
					data.MapEnable[i] = UsesMap(data.Source, i) ? 1 : 0;
				}
				for (int i = 0; i < 8; i++)
				{
					data.ControlLabel[i] = string.Format(CultureInfo.InvariantCulture, "Control: {0}", new object[] { i.ToString(CultureInfo.InvariantCulture) });
					data.ControlEnable[i] = UsesCtl(data.Source, i) ? 1 : 0;
				}
				SetPopDialog(data);

			}
			catch (Exception)
			{
				throw;
			}

		}
		private static void SaveAfs(Stream output, filter_data data)
		{
			if (output == null)
				throw new ArgumentNullException("output", "output is null.");
			if (data == null)
				throw new ArgumentNullException("data", "data is null.");
			try
			{
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
			catch (Exception)
			{
				throw;
			}

		}
		private static void SaveTxt(Stream output, filter_data data)
		{
			if (output == null)
				throw new ArgumentNullException("output", "output is null.");
			if (data == null)
				throw new ArgumentNullException("data", "data is null.");
			try
			{
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
						if (data.ControlEnable[i] == 1)
						{
							sw.WriteLine(string.Format(CultureInfo.InvariantCulture, "ctl[{0}]: {1}", i.ToString(CultureInfo.InvariantCulture), data.ControlLabel[i]));
						}
					}

					sw.WriteLine(Environment.NewLine);

					for (int i = 0; i < 8; i++)
					{
						if (data.ControlEnable[i] == 1)
						{
							sw.WriteLine(string.Format(CultureInfo.InvariantCulture, "val[{0}]: {1}", i.ToString(CultureInfo.InvariantCulture), data.ControlValue[i]));
						}
					}
				}

			}
			catch (Exception)
			{
				throw;
			}

		}
		/// <summary>
		/// Saves a filter_data Source as either an .afs or .txt Source code
		/// </summary>
		/// <param name="FileName">The output FileName to save as</param>
		/// <param name="data">The filter_data to save</param>
		public static void SaveFile(string FileName, filter_data data)
		{
			if (String.IsNullOrEmpty(FileName))
				throw new ArgumentException("FileName is null or empty.", "FileName");
			if (data == null)
				throw new ArgumentNullException("data", "data is null.");
			try
			{

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
			catch (Exception)
			{
				throw;
			}

		}
		/// <summary>
		/// Sets the filter_data to default values.
		/// </summary>
		/// <param name="data">The filter_data to set.</param>
		public static void DefaultFilter(filter_data data)
		{
			string[] src = new string[] { "r", "g", "b", "a" };
			data.Category = "Untitled";
			data.Title = "Untitled";
			data.Author = Environment.UserName;
			data.Copyright = string.Format(CultureInfo.InvariantCulture, "Copyright © {0} {1}", DateTime.Now.Year.ToString(CultureInfo.CurrentCulture), Environment.UserName);

			data.PopDialog = 1;
			for (int i = 0; i < 4; i++)
			{
				data.MapLabel[i] = string.Format(CultureInfo.InvariantCulture, "Map {0}:", i.ToString(CultureInfo.InvariantCulture));
				data.MapEnable[i] = 0;
			}
			for (int i = 0; i < 8; i++)
			{
				data.ControlLabel[i] = string.Format(CultureInfo.InvariantCulture, "ctl {0}", i.ToString(CultureInfo.InvariantCulture));
				data.ControlValue[i] = 0;
				data.ControlEnable[i] = 1;
			}

			for (int i = 0; i < 4; i++)
			{
				data.Source[i] = src[i];
			}
		}
		/// <summary>
		/// Reads a string from a FFL or AFS file.
		/// </summary>
		/// <param name="br">The BinaryReader to read from.</param>
		/// <param name="length">The max length to read from the string.</param>
		/// <returns></returns>
		private static string ReadString(BinaryReader br, int length)
		{
			StringBuilder sb = new StringBuilder();
			char lastChar = br.ReadChar();
			for (int i = 0; i < length; i++)
			{

				try
				{
					char newChar = br.ReadChar();

					if ((lastChar == '\r' && newChar == '\n') || (lastChar == '\n' && newChar == '\r'))
					{
						break;
					}

					sb.Append(lastChar);
					lastChar = newChar;
				}
				catch (EndOfStreamException)
				{
					sb.Append(lastChar);
					break;
				}
			}
			return sb.ToString().Trim();
		}
		/// <summary>
		/// Loads A Filter Factory Library file
		/// </summary>
		/// <param name="fn">The FileName to load.</param>
		/// <param name="items">The output TreeNode list of items in the file.</param>
		/// <returns>True if successful otherwise false.</returns>
		/// <exception cref="System.ArgumentNullException">The Filename is null.</exception>
		/// <exception cref="System.ArgumentNullException">The list of items is null.</exception>
		/// cref="System.ArgumentException">The Filename is empty.</exception>
		public static bool LoadFFL(string fn, List<TreeNode> items)
		{
			bool loaded = false;
			FileStream fs = null;
			try
			{
				if (fn == null)
					throw new ArgumentNullException("fn");

				if (string.IsNullOrEmpty(fn))
					throw new ArgumentException("Filename must not be empty", "fn");

				if (items == null)
					throw new ArgumentNullException("items");


				filter_data data = new filter_data();
				fs = new FileStream(fn, FileMode.Open, FileAccess.Read, FileShare.None);

				using (BinaryReader br = new BinaryReader(fs))
				{
					string id = ReadString(br, 10);
					if (id != null && id == "FFL1.0")
					{
						string num = ReadString(br, 10);
						if (!string.IsNullOrEmpty(num))
						{
							int len = int.Parse(num, CultureInfo.InvariantCulture);
							Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();
							for (int i = 0; i < len - 1; i++)
							{
								try
								{
									long pos = br.BaseStream.Position;
									if (GetFilterfromFFL(br, pos, data))
									{
										if (nodes.ContainsKey(data.Category))
										{
											TreeNode node = nodes[data.Category];

											TreeNode subnode = new TreeNode(data.Title) { Name = data.FileName, Tag = pos }; // Title
											node.Nodes.Add(subnode);
										}
										else
										{
											TreeNode node = new TreeNode(data.Category);
											TreeNode subnode = new TreeNode(data.Title) { Name = data.FileName, Tag = pos }; // Title
											node.Nodes.Add(subnode);

											nodes.Add(data.Category, node);
										}

									}
								}
								catch (EndOfStreamException)
								{
									// ignore it
								}

							}

							items.AddRange(nodes.Values);

							loaded = true;
						}

					}

				}
				fs = null;

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
		/// Extracts a filter fom an offset in the FFL
		/// </summary>
		/// <param name="br">The BimaryReader to read from.</param>
		/// <param name="offset">The offset in the FFL to read from.</param>
		/// <param name="data">The output filter_data for the extraced filter.</param>
		/// <returns>True if successful otherwise false.</returns>
		/// <exception cref="System.ArgumentNullException">The BinaryReader is null.</exception>
		/// <exception cref="System.ArgumentNullException">The filter_data is null.</exception>
		public static bool GetFilterfromFFL(BinaryReader br, long offset, filter_data data)
		{
			if (br == null)
				throw new ArgumentNullException("br", "br is null.");
			if (data == null)
				throw new ArgumentNullException("data", "data is null.");

			bool loaded = false;

			try
			{

				br.BaseStream.Seek(offset, SeekOrigin.Begin);

				data.FileName = ReadString(br, 256);
				data.Category = ReadString(br, 256);
				data.Title = ReadString(br, 256);
				data.Author = ReadString(br, 256);
				data.Copyright = ReadString(br, 256);

				for (int i = 0; i < 4; i++)
				{
					string map = ReadString(br, 256);
					if (!string.IsNullOrEmpty(map))
					{
						data.MapLabel[i] = map;
						data.MapEnable[i] = 1;
					}
					else
					{
						data.MapLabel[i] = string.Format(CultureInfo.InvariantCulture, "Map {0}:", i.ToString(CultureInfo.InvariantCulture));
						data.MapEnable[i] = 0;
					}
				}
				for (int i = 0; i < 8; i++)
				{
					string ctl = ReadString(br, 256);
					if (!string.IsNullOrEmpty(ctl))
					{
						data.ControlLabel[i] = ctl;
						data.ControlEnable[i] = 1;
					}
					else
					{
						data.ControlLabel[i] = string.Format(CultureInfo.InvariantCulture, "Control: {0}", i.ToString(CultureInfo.InvariantCulture));
						data.ControlEnable[i] = 0;
					}
				}

				for (int i = 0; i < 8; i++)
				{
					string cv = ReadString(br, 10);
					data.ControlValue[i] = int.Parse(cv, CultureInfo.InvariantCulture);
				}

				string[] rgba = new string[] { "r", "g", "b", "a" };

				for (int i = 0; i < 4; i++)
				{
					string src = ReadString(br, 8192);
					if (!string.IsNullOrEmpty(src))
					{
						data.Source[i] = src;
					}
					else
					{
						data.Source[i] = rgba[i];
					}
				}
				loaded = true;
			}
			catch (Exception)
			{
				throw;
			}

			return loaded;
		}
		/// <summary>
		/// Sets the filter_data PopDialog for the loaded afs or txt Source
		/// </summary>
		/// <param name="data">The data to set</param>
		private static void SetPopDialog(filter_data data)
		{
			List<bool> ctlused = new List<bool>(8);
			List<bool> mapused = new List<bool>(4);

			for (int i = 0; i < 4; i++)
			{
				bool val = data.MapEnable[i] == 1;
				mapused.Add(val);
			}

			for (int i = 0; i < 8; i++)
			{
				bool val = data.ControlEnable[i] == 1;
				ctlused.Add(val);
			}

			if (ctlused.Contains(true) || mapused.Contains(true))
			{
				data.PopDialog = 1;
			}
			else
			{
				data.PopDialog = 0;
			}
		}
		private static bool UsesMap(string[] Source, int mapnum)
		{
			bool hasmap = false;
			for (int i = 0; i < 4; i++)
			{
				if (Source[i].Contains("map"))
				{
					string tmpsrc = Source[i];
					int pos = -1;
					while ((pos = tmpsrc.IndexOf("map", StringComparison.Ordinal)) > 0)
					{
						string str = tmpsrc.Substring(pos, 5);
						tmpsrc = tmpsrc.Substring(pos + 3);
						int num = int.Parse(str.Substring(4, 1), CultureInfo.InvariantCulture);
						if (num == mapnum && !hasmap)
						{
							hasmap = true;
						}
					}
				}
			}
			return hasmap;
		}
		private static bool UsesCtl(string[] Source, int ctlnum)
		{
			bool hasctl = false;
			for (int i = 0; i < 4; i++)
			{
				if (Source[i].Contains("ctl"))
				{
					string tmpsrc = Source[i];
					int pos = -1;
					while ((pos = tmpsrc.IndexOf("ctl", StringComparison.Ordinal)) > 0)
					{
						string str = tmpsrc.Substring(pos, 5);
						tmpsrc = tmpsrc.Substring(pos + 3);
						int num = int.Parse(str.Substring(4, 1), CultureInfo.InvariantCulture);
						if (num == ctlnum  && !hasctl)
						{
							hasctl = true;
						}
					}
				}
				if (Source[i].Contains("val"))
				{
					string tmpsrc = Source[i];
					int pos = -1;
					while ((pos = tmpsrc.IndexOf("val", StringComparison.Ordinal)) > 0)
					{
						string str = tmpsrc.Substring(pos, 5);
						tmpsrc = tmpsrc.Substring(pos + 3);
						int num = int.Parse(str.Substring(4, 1), CultureInfo.InvariantCulture);
						if (num == ctlnum && !hasctl)
						{
							hasctl = true;
						}
					}
				}
			}
			return hasctl;
		}
	}
}
