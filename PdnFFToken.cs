using System;
using PaintDotNet;

namespace PdnFF
{
    public class PdnFFConfigToken : PaintDotNet.Effects.EffectConfigToken
    {
        internal filter_data data;
        internal string lastFileName;
        internal string lastFFL;
        internal string fflOfs;
        public PdnFFConfigToken(filter_data data, String lastfilename, String lastffl, String fflofs) : base()
        {
            this.data = data;
            this.lastFileName = lastfilename;
            this.lastFFL = lastffl;
            this.fflOfs = fflofs;
        }

        protected PdnFFConfigToken(PdnFFConfigToken copyMe) : base(copyMe)
        {
            this.data = copyMe.data;
            this.lastFileName = copyMe.lastFileName;
            this.lastFFL = copyMe.lastFFL;
            this.fflOfs = copyMe.fflOfs;
        }

        public override object Clone()
        {
            return new PdnFFConfigToken(this);
        }
    }
}