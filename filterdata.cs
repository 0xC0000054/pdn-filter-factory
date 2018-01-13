using System;

namespace PdnFF
{
    public sealed class filter_data
    {
        private string title = string.Empty;
        private string category = string.Empty;
        private string copyright = string.Empty;
        private string author = string.Empty;
        private int[] mapEnable = new int[4];
        private string[] mapLabel = new string[4];
        private int[] controlEnable = new int[8];
        private string[] controlLabel = new string[8];
        private int[] controlValue = new int[8];
        private string[] source = new string[4];
        private int popDialog; // display parameter sliders
        private string fileName; // filename for the FFL filters

        public string Title
        {
            get
            {
                return title;
            }
            internal set
            {
                title = value;
            }
        }
        public string Category
        {
            get
            {
                return category;
            }
            internal set
            {
                category = value;
            }
        }
        public string Copyright
        {
            get
            {
                return copyright;
            }
            internal set
            {
                copyright = value;
            }
        }
        public string Author
        {
            get
            {
                return author;
            }
            internal set
            {
                author = value;
            }
        }
        public int[] MapEnable
        {
            get
            {
                return mapEnable;
            }
            internal set
            {
                if (value == null || value.Length == 0)
                    throw new ArgumentException("value is null or empty.", "value");

                mapEnable = value;
            }
        }
        public string[] MapLabel
        {
            get
            {
                return mapLabel;
            }
            internal set
            {
                if (value == null || value.Length == 0)
                    throw new ArgumentException("value is null or empty.", "value");

                mapLabel = value;
            }
        }
        public int[] ControlEnable
        {
            get
            {
                return controlEnable;
            }
            internal set
            {
                if (value == null || value.Length == 0)
                    throw new ArgumentException("value is null or empty.", "value");
                controlEnable = value;
            }
        }
        public string[] ControlLabel
        {
            get
            {
                return controlLabel;
            }
            internal set
            {
                if (value == null || value.Length == 0)
                    throw new ArgumentException("value is null or empty.", "value");
                controlLabel = value;
            }
        }
        public int[] ControlValue
        {
            get
            {
                return controlValue;
            }
            internal set
            {
                if (value == null || value.Length == 0)
                    throw new ArgumentException("value is null or empty.", "value");
                controlValue = value;
            }
        }
        public string[] Source
        {
            get
            {
                return source;
            }
            internal set
            {
                if (value == null || value.Length == 0)
                    throw new ArgumentException("value is null or empty.", "value");
                source = value;
            }
        }

        public int PopDialog
        {
            get
            {
                return popDialog;
            }
            internal set
            {
                popDialog = value;
            }
        }
        /// <summary>
        /// The FileName for the FFL filters
        /// </summary>
        public string FileName
        {
            get
            {
                return fileName;
            }
            internal set
            {
                fileName = value;
            }
        }
    }

}
