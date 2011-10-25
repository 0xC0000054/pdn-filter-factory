using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using PaintDotNet;
using PaintDotNet.Effects;

namespace PdnFF
{
    public sealed class PdnFF_Effect : PaintDotNet.Effects.Effect
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
                return new Bitmap(typeof(PdnFF_Effect), "script_code_red.png");
            }
        }
        public PdnFF_Effect()
            : base(PdnFF_Effect.StaticName, PdnFF_Effect.StaticIcon, EffectFlags.Configurable | EffectFlags.SingleThreaded)
        {
        }
        private bool disposed = false;
        protected override void OnDispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {
                    if ((envdata != null) && !ffparse.datafreed())
                    {
                        ffparse.FreeData(); // free the unmanaged ffparse data
#if DEBUG
                        Debug.WriteLine("Dispose called");
#endif
                        envdata.Dispose(); // free the enviroment data
                        envdata = null;
                        this.disposed = true;
                    }
                }
            }
            base.OnDispose(disposing);

        }

        /// <summary>
        /// Sets up the unmanaged FilterEnviromentData
        /// </summary>
        private void SetupFilterEnviromentData()
        {
            if (envdata == null)
            {
                envdata = new FilterEnviromentData(base.EnvironmentParameters.SourceSurface);

                bool resetEnvir = envdata.ResetEnvir();
#if DEBUG
                Debug.WriteLine(string.Format("firstrun resetEnvir = {0}", resetEnvir.ToString()));
#endif
            }
        }
        public override EffectConfigDialog CreateConfigDialog()
        {
            SetupFilterEnviromentData();
            return new PdnFFConfigDialog();
        }
        FilterEnviromentData envdata = null;

        /// <summary>
        /// Sets the Filter source code and Control values 
        /// </summary>
        private static void SetupFilterData(filter_data data)
        {
            for (int i = 0; i < 8; i++)
            {
                ffparse.SetControls(data.ControlValue[i], i);
            }
            for (int i = 0; i < 4; i++)
            {
                IntPtr s = Marshal.StringToHGlobalAnsi(data.Source[i]);
                ffparse.SetupTree(s, i);
                Marshal.FreeHGlobal(s);
            }
        }
    
        private bool filterparsed = false;
        protected override void OnSetRenderInfo(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs)
        {
            PdnFFConfigToken token = (PdnFFConfigToken)parameters;

            SetupFilterEnviromentData();

            filterparsed = false;

#if DEBUG
            Debug.WriteLine(string.Format("BitmapEnvirSetup = {0}", envdata.BmpEnvirSetup.ToString()));
            Debug.WriteLine(string.Format("datafreed = {0}", ffparse.datafreed().ToString()));
#endif
            if (token.Data != null && !string.IsNullOrEmpty(token.Data.Author) && envdata.BmpEnvirSetup && !ffparse.datafreed())
            {
                SetupFilterData(token.Data); 
                filterparsed = true;
            }
            
            base.OnSetRenderInfo(parameters, dstArgs, srcArgs);
        }
        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            Surface dest = dstArgs.Surface;
            if (filterparsed)
            {
                unsafe
                {
                    for (int i = startIndex; i < startIndex + length; ++i)
                    {
                        Rectangle rect = rois[i];
                        for (int y = rect.Top; y < rect.Bottom; ++y)
                        {                                
                            if (IsCancelRequested) return; // stop if a cancel is requested

                            ColorBgra *p = dest.GetPointAddressUnchecked(rect.Left, y);
                            for (int x = rect.Left; x < rect.Right; ++x)
                            {
                                ffparse.UpdateEnvir(x, y); // update the (x, y) position in the unmanaged ffparse data

                                p->R = (byte)ffparse.CalcColor(0); // red channel
                                p->G = (byte)ffparse.CalcColor(1); // green channel 
                                p->B = (byte)ffparse.CalcColor(2); // blue channel 
                                p->A = (byte)ffparse.CalcColor(3); // alpha channel

                                p++;
                            }
                        }

                    } 
                }

            }
            else
            {
                dest.CopySurface(srcArgs.Surface);
            }

        }

    }
}