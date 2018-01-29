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

using Microsoft.CSharp;
using PdnFF.Properties;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;

namespace PdnFF
{
	internal sealed class FilterBuilder : IDisposable
	{
		private readonly FilterData data;
		private readonly DirectoryInfo tempFolder;

		private CompilerParameters compilerParameters;
		private bool disposed;

		public FilterBuilder(FilterData data)
		{
			this.data = data;
			this.compilerParameters = new CompilerParameters();
			this.tempFolder = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
			this.disposed = false;
			tempFolder.Create();
		}

		public string Build(string fileName)
		{
			string path = Path.Combine(Path.GetDirectoryName(typeof(PdnFFEffect).Assembly.Location), fileName);

			if (File.Exists(path))
			{
				return Resources.BuildFilterFileAlreadyExists;
			}

			string errorMessage = string.Empty;

			try
			{
				string classname = Path.GetFileNameWithoutExtension(fileName);

				for (int i = 0; i < classname.Length; i++)
				{
					char c = classname[i];
					if (!char.IsLetterOrDigit(c))
					{
						classname = classname.Remove(i, 1);
						i--;
					}
				}

				if (char.IsDigit(classname[0]))
				{
					classname = string.Concat("FF_", classname);
				}

				// Setup the source code files after setting up the effect classname
				string[] files = SetupSourceCodeFiles(fileName, classname);

				SetupCompilerParameters(fileName);

				using (CSharpCodeProvider cscp = new CSharpCodeProvider())
				{
					CompilerResults cr = cscp.CompileAssemblyFromFile(this.compilerParameters, files);

					if (cr.Errors.HasErrors)
					{
						errorMessage = string.Format("Unable to build filter.\n\nError Text: \n {0}", cr.Errors[0].ErrorText);
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}

			return errorMessage;
		}

		public void Dispose()
		{
			if (!disposed)
			{
				disposed = true;

				try
				{
					if (tempFolder.Exists)
					{
						tempFolder.Delete(true);
					}
				}
				catch (ArgumentException)
				{
				}
				catch (IOException)
				{
				}
				catch (SecurityException)
				{
				}
				catch (UnauthorizedAccessException)
				{
				}
			}
		}

		private static string GetBooleanKeywordString(bool value)
		{
			return value ? "true" : "false";
		}

		private static string BuildFilterData(FilterData data)
		{
			string ret = string.Empty;

			using (StringWriter sw = new StringWriter(CultureInfo.InvariantCulture))
			{
				sw.WriteLine("new FilterData {");
				sw.WriteLine("Author = \"{0}\",", data.Author);
				sw.WriteLine("Category =  \"{0}\",", data.Category);
				sw.WriteLine("Title =  \"{0}\",", data.Title);
				sw.WriteLine("Copyright = \"{0}\",", data.Copyright);
				sw.WriteLine("MapEnable = new bool[4] {0},{1},{2},{3}",
					new object[] { "{ " + GetBooleanKeywordString(data.MapEnable[0]),
										  GetBooleanKeywordString(data.MapEnable[1]),
										  GetBooleanKeywordString(data.MapEnable[2]),
										  GetBooleanKeywordString(data.MapEnable[3]) + "}," });
				sw.WriteLine("MapLabel = new string[4] {0}\",\"{1}\",\"{2}\",\"{3}", "{ \"" + data.MapLabel[0], data.MapLabel[1], data.MapLabel[2], data.MapLabel[3] + "\"" + "},");

				sw.WriteLine("ControlEnable = new bool[8] {0},{1},{2},{3},{4},{5},{6},{7} ",
					new object[] { "{ " + GetBooleanKeywordString(data.ControlEnable[0]),
										  GetBooleanKeywordString(data.ControlEnable[1]),
										  GetBooleanKeywordString(data.ControlEnable[2]),
										  GetBooleanKeywordString(data.ControlEnable[3]),
										  GetBooleanKeywordString(data.ControlEnable[4]),
										  GetBooleanKeywordString(data.ControlEnable[5]),
										  GetBooleanKeywordString(data.ControlEnable[6]),
										  GetBooleanKeywordString(data.ControlEnable[7]) + "}," });
				sw.WriteLine("ControlLabel = new string[8] {0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}", new object[] { "{ \"" + data.ControlLabel[0], data.ControlLabel[1], data.ControlLabel[2], data.ControlLabel[3], data.ControlLabel[4], data.ControlLabel[5], data.ControlLabel[6], data.ControlLabel[7] + "\"" + "}," });
				sw.WriteLine("ControlValue = new int[8] {0},{1},{2},{3},{4},{5},{6},{7} ", new object[] { "{ " + data.ControlValue[0].ToString(CultureInfo.InvariantCulture), data.ControlValue[1].ToString(CultureInfo.InvariantCulture), data.ControlValue[2].ToString(CultureInfo.InvariantCulture), data.ControlValue[3].ToString(CultureInfo.InvariantCulture), data.ControlValue[4].ToString(CultureInfo.InvariantCulture), data.ControlValue[5].ToString(CultureInfo.InvariantCulture), data.ControlValue[6].ToString(CultureInfo.InvariantCulture), data.ControlValue[7].ToString(CultureInfo.InvariantCulture) + "}," });
				sw.WriteLine("Source = new string[4]  {0}\",\"{1}\",\"{2}\",\"{3}", "{ \"" + data.Source[0], data.Source[1], data.Source[2], data.Source[3] + "\"" + "}" + ",");
				sw.WriteLine("PopDialog = {0},", GetBooleanKeywordString(data.PopDialog));
				sw.WriteLine("};");

				ret = sw.ToString();
			}
			return ret;
		}

		private static string GetSubmenuCategory(FilterData data)
		{
			string cat = string.Empty;

			switch (data.Category.ToUpperInvariant())
			{
				case "ARTISTIC":
					cat = "SubmenuNames.Artistic";
					break;
				case "BLURS":
					cat = "SubmenuNames.Blurs";
					break;
				case "DISTORT":
					cat = "SubmenuNames.Distort";
					break;
				case "NOISE":
					cat = "SubmenuNames.Noise";
					break;
				case "PHOTO":
					cat = "SubmenuNames.Photo";
					break;
				case "RENDER":
					cat = "SubmenuNames.Render";
					break;
				case "STYLIZE":
					cat = "SubmenuNames.Stylize";
					break;

				default:
					cat = "\"" + data.Category + "\"";
					break;
			}

			return cat;
		}

		/// <summary>
		/// Builds the effect class.
		/// </summary>
		/// <param name="classname">The class name of the Effect.</param>
		/// <param name="FileName">The FileName of the output file.</param>
		/// <returns>The generated Effect class Source code</returns>
		private string BuildEffectClass(string classname, string FileName)
		{
			string ret = string.Empty;
			using (StringWriter sw = new StringWriter(CultureInfo.InvariantCulture))
			{
				// usings
				sw.WriteLine("using System;");
				sw.WriteLine("using System.Drawing;");
				sw.WriteLine("using System.Runtime.InteropServices;");
				sw.WriteLine("using System.Reflection;");
				sw.WriteLine("using PaintDotNet;");
				sw.WriteLine("using PaintDotNet.Effects;");
				sw.WriteLine("using FFEffect;\n");
				// AssemblyInfo
				sw.WriteLine("[assembly: AssemblyTitle(\"" + FileName + " Plugin (Compiled by PdnFF)\")]");
				sw.WriteLine("[assembly: AssemblyCompany(\"" + data.Author + "\")]");
				sw.WriteLine("[assembly: AssemblyProduct(\"" + FileName + " Plugin (Compiled by PdnFF)\")]");
				sw.WriteLine("[assembly: AssemblyCopyright(\"" + data.Copyright + "\")]");
				// namespace and class
				sw.WriteLine("namespace FFEffect_" + classname + " \n{\n");
				sw.WriteLine("public class " + classname + " : PaintDotNet.Effects.Effect \n{\n");

				sw.WriteLine("private readonly FilterData data;");
				sw.WriteLine("Common com = new Common();");
				// Constructor
				string Category = GetSubmenuCategory(data);
				sw.WriteLine(string.Format(CultureInfo.InvariantCulture, "public {0}() : base(\"{1}\", null, {2}, EffectFlags.{3})", classname, data.Title, Category, data.PopDialog ? "Configurable" : "None"));
				sw.WriteLine("{");
				sw.WriteLine("data = {0}", BuildFilterData(data));
				sw.WriteLine("}");
				//OnDispose
				sw.WriteLine("protected override void OnDispose(bool disposing)\n{");
				sw.WriteLine("if (disposing) \n {");
				sw.WriteLine("if (com != null) \n { com.Dispose();\n com = null; } \n } \n base.OnDispose(disposing); \n }");
				// CreateConfigDialog
				if (data.PopDialog)
				{
					sw.WriteLine(" public override EffectConfigDialog CreateConfigDialog() \n{\n");
					sw.WriteLine("return new FFEffectConfigDialog(data); \n }\n");
				}
				// OnSetRenderInfo
				sw.WriteLine("protected override void OnSetRenderInfo(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs)\n { \n");

				if (data.PopDialog) // is the Effect configurable
				{
					sw.WriteLine("FFEffectConfigToken token = (FFEffectConfigToken)parameters;");
					sw.WriteLine("\n com.SetupFilterData(srcArgs.Surface, token.ctlvalues, data.Source);\n ");
				}
				else
				{
					sw.WriteLine("com.SetupFilterData(srcArgs.Surface, data.ControlValue, data.Source);");
				}
				sw.WriteLine("base.OnSetRenderInfo(parameters, dstArgs, srcArgs); \n } \n");

				// Render
				sw.WriteLine("public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length) \n { \n");
				sw.WriteLine("com.Render(srcArgs.Surface, dstArgs.Surface, rois, startIndex, length); \n }");
				sw.WriteLine("}\n }"); // end the class and the namespace

				ret = sw.ToString();
			}

			return ret;
		}

		private void SetupCompilerParameters(string fileName)
		{
			compilerParameters.GenerateInMemory = true;
			compilerParameters.GenerateExecutable = false;
			String resourceName = Path.Combine(tempFolder.FullName, "FFEffect.FFEffectConfigDialog.resources");

			compilerParameters.CompilerOptions = string.Format(CultureInfo.InvariantCulture, "/debug- /unsafe /optimize /target:library /resource:\"{0}\"", resourceName);

			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			string effectsDir = Path.GetDirectoryName(typeof(PdnFFEffect).Assembly.Location);
			string fileTypesDir = Path.Combine(Path.GetDirectoryName(effectsDir), "FileTypes");

			foreach (var asm in assemblies)
			{
				if (!asm.Location.StartsWith(effectsDir, StringComparison.OrdinalIgnoreCase) &&
					!asm.Location.StartsWith(fileTypesDir, StringComparison.OrdinalIgnoreCase))
				{
					compilerParameters.ReferencedAssemblies.Add(asm.Location);
				}
			}

			compilerParameters.OutputAssembly = Path.Combine(effectsDir, fileName);
		}

		/// <summary>
		/// Sets up the temp Source code files.
		/// </summary>
		/// <param name="effectFileName">The effect FileName.</param>
		/// <param name="effectClassName">The effect ClasssName</param>
		/// <returns>The list of temp files</returns>
		private string[] SetupSourceCodeFiles(string effectFileName, string effectClassName)
		{
			List<string> files = new List<string>(8);

			string[] embeddedfiles = new string[] {"PdnFF.Coderes.FFEffect.FFEffectConfigDialog.resources","PdnFF.Coderes.Common.cs",
			"PdnFF.Coderes.FFEffectConfigDialog.cs","PdnFF.Coderes.FFEffectConfigToken.cs","PdnFF.Coderes.ffparse.cs","PdnFF.Coderes.FilterData.cs"
			, "PdnFF.Coderes.SafeEnvironmentDataHandle.cs"};

			string dir = tempFolder.FullName;

			for (int i = 0; i < embeddedfiles.Length; i++)
			{
				string resourceName = embeddedfiles[i];

				using (Stream res = Assembly.GetAssembly(typeof(PdnFFEffect)).GetManifestResourceStream(resourceName))
				{
					// Remove the 'PdnFF.Coderes.' name space prefix.
					string fileName = resourceName.Substring(14, resourceName.Length - 14);

					string outfile = Path.Combine(dir, fileName);

					using (FileStream fs = new FileStream(outfile, FileMode.Create, FileAccess.Write))
					{
						byte[] bytes = new byte[res.Length];
						int numBytesToRead = (int)res.Length;
						int numBytesRead = 0;
						while (numBytesToRead > 0)
						{
							// Read may return anything from 0 to numBytesToRead.
							int n = res.Read(bytes, numBytesRead, numBytesToRead);
							// The end of the file is reached.
							if (n == 0)
								break;
							numBytesRead += n;
							numBytesToRead -= n;
						}

						fs.Write(bytes, 0, bytes.Length);
					}
					if (i > 0)
					{
						files.Add(outfile);
					}
				}

			}
			string outpath = Path.Combine(dir, "ffeffect.cs");
			string code = BuildEffectClass(effectClassName, effectFileName);
			File.WriteAllText(outpath, code);
			files.Add(outpath);

			return files.ToArray();
		}
	}
}
