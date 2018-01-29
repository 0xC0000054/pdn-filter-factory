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

using System.Drawing;
using PaintDotNet;
using PaintDotNet.Effects;

namespace PdnFF
{
    [PluginSupportInfo(typeof(PluginSupportInfo))]
    public sealed class PdnFFEffect : PaintDotNet.Effects.Effect
    {

        public static string StaticName
        {
            get
            {
                return "PDN FF";
            }
        }
        public static Bitmap StaticIcon
        {
            get
            {
                //EffectPluginIcon.png
                return new Bitmap(typeof(PdnFFEffect), "script_code_red.png");
            }
        }

        private SafeEnvironmentDataHandle enviromentDataHandle;

        public PdnFFEffect()
            : base(StaticName, StaticIcon, EffectFlags.Configurable)
        {
        }

        protected override void OnDispose(bool disposing)
        {
            if (disposing)
            {
                if (enviromentDataHandle != null)
                {
                    enviromentDataHandle.Dispose();
                    enviromentDataHandle = null;
                }
            }

            base.OnDispose(disposing);
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            return new PdnFFConfigDialog();
        }

        private bool filterparsed = false;
        protected override void OnSetRenderInfo(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs)
        {
            PdnFFConfigToken token = (PdnFFConfigToken)parameters;

            if (enviromentDataHandle != null)
            {
                enviromentDataHandle.Dispose();
                enviromentDataHandle = null;
            }

            filterparsed = false;
            if (token.Data != null && !string.IsNullOrEmpty(token.Data.Author))
            {
                enviromentDataHandle = ffparse.CreateEnvironmentData(srcArgs.Surface, token.Data);

                filterparsed = !enviromentDataHandle.IsInvalid;
            }

            base.OnSetRenderInfo(parameters, dstArgs, srcArgs);
        }
        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            if (filterparsed)
            {
                ffparse.Render(enviromentDataHandle, rois, startIndex, length, dstArgs.Surface);
            }
            else
            {
                dstArgs.Surface.CopySurface(srcArgs.Surface, rois, startIndex, length);
            }

        }

    }
}