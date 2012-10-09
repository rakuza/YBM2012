using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;

namespace YBMForms.DLL.IOL
{
    class MemImage
    {
        private MemoryStream MS;

        public BitmapImage image = new BitmapImage();

        public BitmapImage Image
        {
            get { return image; }
        }

        public MemImage (Uri uri)
        {
            byte[] file = File.ReadAllBytes(uri.LocalPath);
            MS = new MemoryStream(file);
            image.BeginInit();
            image.StreamSource = MS;
            image.EndInit();
        }

        /// <summary>
        /// For some reason it requires me to use another method in order to acess the bitmap with it properly there
        /// </summary>
        /// <returns>Bitmap for Get method Image
        /// Dont use for anything else</returns>
        private BitmapImage Convert()
        {
            return image;
        }
    }
}
