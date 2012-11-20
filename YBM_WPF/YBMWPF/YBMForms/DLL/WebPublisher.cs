using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using YBMForms.DLL;
using YBMForms.UIL;
using System.Reflection;

namespace YBMForms.DLL
{
    static class WebPublisher
    {
        static public void PublishBook(BookViewer bv, string folderloc)
        {
            CopyPreSetFiles(folderloc);
            RenderPages(bv, folderloc);
        }


        static private void RenderPages(BookViewer bv, string folderloc)
        {
            int currentpage = bv.ViewIndex;
            foreach (Page p in bv.CurrentBook.Pages)
            {
                bv.ViewIndex = p.PageNumber;
                RenderTargetBitmap rtb = new RenderTargetBitmap(PaperSize.Pixel.PaperWidth, PaperSize.Pixel.PaperHeight, 96, 96, PixelFormats.Default);
                rtb.Render(BookViewer.DesignerCanvas);
                Int32Rect bleedmargin = new Int32Rect((PaperSize.Pixel.PaperWidth - PaperSize.Pixel.BleedWidth) / 2, (PaperSize.Pixel.PaperHeight - PaperSize.Pixel.BleedHeight) / 2, PaperSize.Pixel.BleedWidth, PaperSize.Pixel.BleedHeight);
                CroppedBitmap cb = new CroppedBitmap(rtb, bleedmargin);
                PngBitmapEncoder pbe = new PngBitmapEncoder();
                pbe.Frames.Add(BitmapFrame.Create(cb));

                FileStream fs = File.Open(folderloc + "\\src\\" + (p.PageNumber+1) + ".png", FileMode.Create);
                pbe.Save(fs);
                fs.Flush();
                fs.Close();

            }





        }

        static private void CopyPreSetFiles(string folderloc)
        {
            Directory.CreateDirectory(folderloc + "\\js\\");
            Directory.CreateDirectory(folderloc + "\\src\\");

            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            Stream htmlFile = thisAssembly.GetManifestResourceStream("YBMForms.WebTemplate.yearbook.html");
            byte[] html = new byte[htmlFile.Length];
            htmlFile.Read(html, 0, (int)htmlFile.Length);
            File.WriteAllBytes(folderloc + "\\yearbook.html", html);

            Stream jsFile = thisAssembly.GetManifestResourceStream("YBMForms.WebTemplate.js.jquery-1.8.2.js");
            byte[] js = new byte[jsFile.Length];
            jsFile.Read(js, 0, (int)jsFile.Length);
            File.WriteAllBytes(folderloc + "\\js\\" + "jquery-1.8.2.js",js);
        }

    }
}
