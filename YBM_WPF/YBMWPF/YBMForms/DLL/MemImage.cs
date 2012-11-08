using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;

namespace YBMForms.DLL
{
    sealed class MemImage : IDisposable
    {
        private MemoryStream MS;

       private BitmapImage image = new BitmapImage();

        public BitmapImage Image
        {
            get { return image; }
        }

        public MemImage (Uri uri)
        {
            byte[] file = File.ReadAllBytes(uri.LocalPath);
            MS = new MemoryStream(file);
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = MS;
            image.EndInit();
        }

        public void Dispose()
        {
            MS.Dispose();

        }


    }
}
