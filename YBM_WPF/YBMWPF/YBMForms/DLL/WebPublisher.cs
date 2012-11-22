using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using YBMForms.UIL;

namespace YBMForms.DLL
{
    static class WebPublisher
    {
        /// <summary>
        /// publicly exposed class for saving a book to web
        /// </summary>
        /// <param name="bv">the current bookviewer</param>
        /// <param name="folderloc">the folder where the page is being saved</param>
        static public void PublishBook(BookViewer bv, string folderloc, MainWindow h)
        {
            h.SetBackGroundInvisible();
            CopyPreSetFiles(folderloc);
            RenderPages(bv, folderloc);
            h.SetBackGroundVisible();
        }


        /// <summary>
        /// Generates an image of each page in the year book
        /// and saves it to the src folder
        /// </summary>
        /// <param name="bv"></param>
        /// <param name="folderloc"></param>
        static private void RenderPages(BookViewer bv, string folderloc)
        {
            int currentpage = bv.ViewIndex;
            //loops though each page
            foreach (Page p in bv.CurrentBook.Pages)
            {
                bv.ViewIndex = p.PageNumber;
                //forces the canvas to re-render
                BookViewer.DesignerCanvas.UpdateLayout();
                //takes a picture of the canvas
                RenderTargetBitmap rtb = new RenderTargetBitmap(PaperSize.Pixel.PaperWidth, PaperSize.Pixel.PaperHeight, 96, 96, PixelFormats.Default);
                rtb.Render(BookViewer.DesignerCanvas);
                //getting the bleed margin
                Int32Rect bleedmargin = new Int32Rect((PaperSize.Pixel.PaperWidth - PaperSize.Pixel.BleedWidth) / 2, (PaperSize.Pixel.PaperHeight - PaperSize.Pixel.BleedHeight) / 2, PaperSize.Pixel.BleedWidth, PaperSize.Pixel.BleedHeight);
                //cropping the image
                CroppedBitmap cb = new CroppedBitmap(rtb, bleedmargin);
                //encodes the image in png format
                PngBitmapEncoder pbe = new PngBitmapEncoder();
                pbe.Frames.Add(BitmapFrame.Create(cb));
                //saves the resulting image
                FileStream fs = File.Open(folderloc + "\\src\\" + (p.PageNumber+1) + ".png", FileMode.Create);
                pbe.Save(fs);
                fs.Flush();
                fs.Close();

            }
            bv.ViewIndex = currentpage;




        }

        /// <summary>
        /// Grabs pre existing files out of the assembly
        /// </summary>
        /// <param name="folderloc"></param>
        static private void CopyPreSetFiles(string folderloc)
        {

            //creating the folders
            Directory.CreateDirectory(folderloc + "\\js\\");
            Directory.CreateDirectory(folderloc + "\\src\\");

            //getting the asembly info
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            //grabing a stream of the asembly
            Stream htmlFile = thisAssembly.GetManifestResourceStream("YBMForms.WebTemplate.yearbook.html");
            //creating a buffer
            byte[] html = new byte[htmlFile.Length];
            //read all bytes out of stream
            htmlFile.Read(html, 0, (int)htmlFile.Length);
            //save the bytes
            File.WriteAllBytes(folderloc + "\\yearbook.html", html);

            Stream jsFile = thisAssembly.GetManifestResourceStream("YBMForms.WebTemplate.js.jquery-1.8.2.js");
            byte[] js = new byte[jsFile.Length];
            jsFile.Read(js, 0, (int)jsFile.Length);
            File.WriteAllBytes(folderloc + "\\js\\" + "jquery-1.8.2.js",js);
        }

    }
}
