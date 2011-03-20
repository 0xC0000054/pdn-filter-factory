using System;
using System.Diagnostics;
using PaintDotNet;

namespace PdnFF
{
    class FilterEnviromentData : IDisposable
    {

        /// <summary>
        /// Sets Up the unmanaged ffparse filter enviroment data
        /// </summary>
        /// <param name="source">The SourceSurface of the Image</param>
        public FilterEnviromentData(Surface source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
           
            this.SourceSurface = new Surface(source.Size);
            this.SourceSurface.CopySurface(source);
        }

        private Surface SourceSurface = null;
       
        private bool bmpenvirsetup;
        /// <summary>
        /// Is the unmanaged Source Bitmap setup
        /// </summary>
        public bool BmpEnvirSetup
        {
            get
            {
                return bmpenvirsetup;
            }
        }


        /// <summary>
        /// Resets the enviroment for the filter
        /// </summary>
        /// <returns>1 on sucess negative number on error</returns>
        private int ResetEnviroment()
        {                
            int ret = 0;
            if (!bmpenvirsetup)
            {
                unsafe
                {
#if DEBUG
                    Debug.WriteLine(string.Format("Width = {0}, Height = {1}, Stride = {2}", SourceSurface.Width, SourceSurface.Height, SourceSurface.Stride));
#endif
                    ret = ffparse.SetupBitmap(new IntPtr(SourceSurface.Scan0.VoidStar), SourceSurface.Width, SourceSurface.Height, SourceSurface.Stride);
                }
#if DEBUG
                Debug.WriteLine(string.Format("ffparse.SetupBitmap returned {0}", ret.ToString()));
#endif
                
                bmpenvirsetup  = (ret == 1);

            }
            return ret;
        }
        /// <summary>
        /// Resets the Filter Enviroment 
        /// </summary>
        /// <returns>True if successful otherwise false</returns>
        public bool ResetEnvir()
        {
            return (ResetEnviroment() >= 0); // is the result greater than or equal to zero?  
        }



        #region IDisposable Members
        private bool disposed = false; 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.SourceSurface.Dispose();
                    this.SourceSurface = null;
                }
                this.disposed = true;
            }
        }

        #endregion
    }
}
