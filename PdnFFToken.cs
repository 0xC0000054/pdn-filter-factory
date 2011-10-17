using System;
using PaintDotNet;

namespace PdnFF
{
    public sealed class PdnFFConfigToken : PaintDotNet.Effects.EffectConfigToken
    {
        private filter_data data;
        private string lastFileName;
        private string lastFFL;
        private string fflOfs;

        public filter_data Data
        {
            get
            {
                return data;
            }
            internal set
            {
                data = value;
            }
        }

        public string LastFileName
        {
            get
            {
                return lastFileName;
            }
            internal set
            {
                lastFileName = value;
            }
        }

        public string LastFFL
        {
            get
            {
                return lastFFL;
            }
            internal set
            {
                lastFFL = value;
            }
        }

        public string FFLOffset
        {
            get
            {
                return fflOfs;
            }
            internal set
            {
                fflOfs = value;
            }
        }


        public PdnFFConfigToken(filter_data data, String lastfilename, String lastffl, String fflofs) : base()
        {
            this.Data = data;
            this.lastFileName = lastfilename;
            this.lastFFL = lastffl;
            this.fflOfs = fflofs;
        }
#pragma warning disable 0628
        protected PdnFFConfigToken(PdnFFConfigToken copyMe) : base(copyMe)
        {
            this.Data = copyMe.Data;
            this.lastFileName = copyMe.lastFileName;
            this.lastFFL = copyMe.lastFFL;
            this.fflOfs = copyMe.fflOfs;
        }
#pragma warning restore 0628

        public override object Clone()
        {
            return new PdnFFConfigToken(this);
        }
    }
}