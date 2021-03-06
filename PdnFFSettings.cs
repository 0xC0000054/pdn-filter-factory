﻿/*
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace PdnFF
{
    [DataContract(Name = "PdnFFSettings", Namespace = "")]
    internal sealed class PdnFFSettings
    {
        private readonly string path;
        private bool changed;

        [DataMember(Name = "SearchDirectories")]
        private HashSet<string> searchDirectories;

        [DataMember(Name = "SearchSubdirectories")]
        private bool searchSubdirectories;

        /// <summary>
        /// Initializes a new instance of the <see cref="PSFilterPdnSettings"/> class.
        /// </summary>
        /// <param name="path">The path of the settings file.</param>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
        public PdnFFSettings(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            this.path = path;
            changed = false;
            searchSubdirectories = true;
            searchDirectories = null;

            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    Load(stream);
                }
            }
            catch (FileNotFoundException)
            {
                // Use the default settings if the file is not present.
            }
        }

        /// <summary>
        /// Gets or sets the search directories.
        /// </summary>
        /// <value>
        /// The search directories.
        /// </value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        public HashSet<string> SearchDirectories
        {
            get
            {
                return searchDirectories;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                searchDirectories = value;
                changed = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether subdirectories are searched.
        /// </summary>
        /// <value>
        ///   <c>true</c> if subdirectories are searched; otherwise, <c>false</c>.
        /// </value>
        public bool SearchSubdirectories
        {
            get
            {
                return searchSubdirectories;
            }
            set
            {
                if (searchSubdirectories != value)
                {
                    searchSubdirectories = value;
                    changed = true;
                }
            }
        }

        /// <summary>
        /// Saves any changes to this instance.
        /// </summary>
        public void Flush()
        {
            if (changed)
            {
                Save();
                changed = false;
            }
        }

        private void Load(Stream stream)
        {
            XmlReaderSettings readerSettings = new XmlReaderSettings
            {
                CloseInput = false,
                IgnoreComments = true,
                XmlResolver = null
            };

            using (XmlReader xmlReader = XmlReader.Create(stream, readerSettings))
            {
                // Detect the old settings format and convert it to the new format.
                if (xmlReader.MoveToContent() == XmlNodeType.Element &&
                    xmlReader.Name == OldXmlSettings.RootNodeName)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(xmlReader);

                    XmlNode searchDirsNode = xmlDocument.SelectSingleNode(OldXmlSettings.SearchDirectoriesPath);
                    if (searchDirsNode != null)
                    {
                        string dirs = searchDirsNode.InnerText.Trim();

                        if (!string.IsNullOrEmpty(dirs))
                        {
                            List<string> directories = new List<string>();

                            string[] splitDirs = dirs.Split(new char[] { ',' }, StringSplitOptions.None);

                            for (int i = 0; i < splitDirs.Length; i++)
                            {
                                string dir = splitDirs[i];

                                try
                                {
                                    if (Path.IsPathRooted(dir))
                                    {
                                        directories.Add(dir);
                                    }
                                    else
                                    {
                                        // If the path contains a comma it will not be rooted
                                        // append it to the previous path with a comma added.

                                        int index = directories.Count - 1;
                                        string lastPath = directories[index];

                                        directories[index] = lastPath + "," + dir;
                                    }
                                }
                                catch (ArgumentException)
                                {
                                }
                            }

                            if (directories.Count > 0)
                            {
                                searchDirectories = new HashSet<string>(directories, StringComparer.OrdinalIgnoreCase);
                            }
                        }
                    }
                    XmlNode searchSubDirNode = xmlDocument.SelectSingleNode(OldXmlSettings.SearchSubdirectoriesPath);
                    if (searchSubDirNode != null)
                    {
                        bool result;
                        if (bool.TryParse(searchSubDirNode.InnerText.Trim(), out result))
                        {
                            searchSubdirectories = result;
                        }
                    }

                    changed = true;
                }
                else
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(PdnFFSettings));
                    PdnFFSettings settings = (PdnFFSettings)serializer.ReadObject(xmlReader);

                    searchDirectories = settings.searchDirectories;
                    searchSubdirectories = settings.searchSubdirectories;
                }
            }
        }

        private void Save()
        {
            XmlWriterSettings writerSettings = new XmlWriterSettings
            {
                Indent = true
            };

            using (XmlWriter writer = XmlWriter.Create(path, writerSettings))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(PdnFFSettings));
                serializer.WriteObject(writer, this);
            }
        }

        private static class OldXmlSettings
        {
            internal const string RootNodeName = "settings";
            internal const string SearchSubdirectoriesPath = "settings/SearchSubDirs";
            internal const string SearchDirectoriesPath = "settings/SearchDirs";
        }
    }
}
