using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.IO;
using System.IO.Compression;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Converters;

namespace YBMForms.DLL
{
    internal class PageSaver
    {
        private Canvas page;

        internal PageSaver(Canvas c)
        {
            page = c;
        }

        internal class MemorystreamOut : MemoryStream
        {
            internal MemorystreamOut()
            {

            }

            internal void WriteLine(string s)
            {
                Write(UnicodeEncoding.Unicode.GetBytes(s + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount(s + "\r\n"));
            }
        }

        

        internal List<PageElement> SaveCanvas()
        {
            List<PageElement> elements = new List<PageElement>();
            foreach (UIElement child in page.Children)
            {
                ContentControl cc = child as ContentControl;
                PageElement PE = new PageElement();
                SaveContentController(cc, PE);

                if (PE.Type == "System.Windows.Controls.RichTextBox")
                    SaveTextBox(cc, PE);
                else if (PE.Type == "System.Windows.Controls.Image")
                    SaveImage(cc, PE);
                else if (PE.Type == "System.Windows.Shapes.Ellipse" || PE.Type == "System.Windows.Shapes.Rectangle")
                {
                    Shape s = cc.Content as Shape;
                    PE.Child.Brush = s.Fill.ToString();
                }
                elements.Add(PE);
            }

            return elements;
        }

        private static void SaveImage(ContentControl cc, PageElement PE)
        {
            Image i = cc.Content as Image;
            PE.Child.Fill = i.Stretch.ToString();
            PngBitmapEncoder png = new PngBitmapEncoder();
            using (MemoryStream ME = new MemoryStream())
            {
                var imagesource = i.Source as BitmapImage;
                png.Frames.Add(BitmapFrame.Create(imagesource));
                png.Save(ME);
                ME.Flush();
                var stream = ME;
                byte[] img = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(img, 0, img.Length);
                PE.Child.Image = img;
            }
        }

        private static void SaveTextBox(ContentControl cc, PageElement PE)
        {
            PE.Child.BorderColor = ((Control)cc.Content).BorderBrush.ToString();
            PE.Child.BorderThickness = ((Control)cc.Content).BorderThickness;
            RichTextBox rtb = cc.Content as RichTextBox;
            TextRange content = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            using (MemoryStream ME = new MemoryStream())
            {
                content.Save(ME, DataFormats.Rtf);
                StreamReader sr = new StreamReader(ME);

                ME.Position = 0;
                PE.Child.Document = sr.ReadToEnd();
                PE.Child.BackgroundColor = rtb.Background.ToString();
            }
        }

        private static void SaveContentController(ContentControl cc, PageElement PE)
        {
            PE.Width = cc.Width;
            PE.Height = cc.Height;
            PE.Left = Canvas.GetLeft(cc);
            PE.Top = Canvas.GetTop(cc);
            PE.Zindex = Canvas.GetZIndex(cc);
            string sc = cc.RenderTransform.GetType().ToString();
            if (sc == new RotateTransform().GetType().ToString())
                PE.Rotation = ((RotateTransform)cc.RenderTransform).Angle;
            PE.Type = cc.Content.GetType().ToString();
        }


        /// <summary>
        /// A Method for saving the actual page to the file
        /// 
        /// p.s. the image saving/loading from this point works fine
        /// </summary>
        /// <param name="page">All the components of the page</param>
        static internal byte[] SavePageElements(List<PageElement> page)
        {
            byte[] buffer;
            using (MemorystreamOut mso = new MemorystreamOut())
            {
                mso.WriteLine("node:" + page.Count);
                foreach (PageElement PE in page)
                {
                    mso.WriteLine("cc:");
                    mso.WriteLine(" width:" + PE.Width);
                    mso.WriteLine(" height:" + PE.Height);
                    mso.WriteLine(" top:" + PE.Top);
                    mso.WriteLine(" left:" + PE.Left);
                    mso.WriteLine(" zindex:" + PE.Zindex);

                    mso.WriteLine(" bordercolor:" + PE.Child.BorderColor);
                    mso.WriteLine(" borderthickness:" + PE.Child.BorderThickness);
                    mso.WriteLine(" rotation:" + PE.Rotation);
                    mso.WriteLine(" child:");
                    mso.WriteLine("  type:" + PE.Type);

                    if (PE.Type == "System.Windows.Controls.RichTextBox")
                    {
                        mso.WriteLine("  rtf:" + PE.Child.Document);
                        mso.WriteLine(" background:" + PE.Child.BackgroundColor);
                    }
                    else if (PE.Type == "System.Windows.Controls.Image")
                    {

                        mso.WriteLine("  img:" + PE.Child.Image.Length);


                        mso.Write(PE.Child.Image, 0, PE.Child.Image.Length);
                        mso.WriteLine("\r\n" + "  fill:" + PE.Child.Fill);

                    }
                    else if (PE.Type == "System.Windows.Shapes.Ellipse" || PE.Type == "System.Windows.Shapes.Rectangle")
                    {
                        mso.WriteLine("  brush:" + PE.Child.Brush);
                    }
                }

                mso.Flush();
                buffer = mso.ToArray();
                return buffer;
            }
        }

    }
}
