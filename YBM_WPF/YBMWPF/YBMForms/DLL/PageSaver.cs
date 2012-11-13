﻿using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace YBMForms.DLL
{
    internal class PageSaver
    {
        private Canvas page;

        internal PageSaver(Canvas c)
        {
            page = c;
        }


        private List<PageElement> elements = new List<PageElement>();

        internal void SavePage()
        {
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

            PrintPage(elements);
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
        static internal void PrintPage(List<PageElement> page)
        {
            using (FileStreamOut fs = new FileStreamOut("durp.txt", FileMode.Create))
            {
                fs.WriteLine("node:" + page.Count + "\r\n");
                foreach (PageElement PE in page)
                {
                    fs.WriteLine("cc:" + "\r\n");
                    fs.WriteLine(" width:" + PE.Width + "\r\n");
                    fs.WriteLine(" height:" + PE.Height + "\r\n");
                    fs.WriteLine(" top:" + PE.Top + "\r\n");
                    fs.WriteLine(" left:" + PE.Left + "\r\n");
                    fs.WriteLine(" zindex:" + PE.Zindex + "\r\n");

                    fs.WriteLine(" bordercolor:" + PE.Child.BorderColor + "\r\n");
                    fs.WriteLine(" borderthickness:" + PE.Child.BorderThickness + "\r\n");
                    fs.WriteLine(" rotation:" + PE.Rotation + "\r\n");
                    fs.WriteLine(" child:" + "\r\n");
                    fs.WriteLine("  type:" + PE.Type + "\r\n");

                    if (PE.Type == "System.Windows.Controls.RichTextBox")
                    {
                        fs.WriteLine("  rtf:" + PE.Child.Document + "\r\n");
                        fs.WriteLine(" background:" + PE.Child.BackgroundColor + "\r\n");
                    }
                    else if (PE.Type == "System.Windows.Controls.Image")
                    {

                        fs.WriteLine("  img:" + PE.Child.Image.Length + "\r\n");


                        fs.Write(PE.Child.Image, 0, PE.Child.Image.Length);
                        fs.WriteLine("\r\n" + "  fill:" + PE.Child.Fill + "\r\n");

                    }
                    else if (PE.Type == "System.Windows.Shapes.Ellipse" || PE.Type == "System.Windows.Shapes.Rectangle")
                    {
                        fs.WriteLine("  brush:" + PE.Child.Brush + "\r\n");
                    }
                }
            }
        }

    }
}