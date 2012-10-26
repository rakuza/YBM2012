using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.IO;
using System.IO.Compression;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Converters;
namespace YBMForms.DLL.IOL
{
    class PageSaver
    {
        private Canvas page;

        public Canvas Page
        {
            set { page = value; }
        }

        public PageSaver(Canvas c)
        {
            page = c;
        }

        public PageSaver()
        {

        }

        private List<PageElement> elements = new List<PageElement>(); 

        public void SavePage()
        {
            foreach(UIElement child in page.Children)
            {
                ContentControl cc = child as ContentControl;
                PageElement PE = new PageElement();
                PE.Width = cc.Width;
                PE.Height = cc.Height;
                PE.Left = Canvas.GetLeft(cc);
                PE.Top = Canvas.GetTop(cc);
                PE.Zindex = Canvas.GetZIndex(cc);
                PE.Type = cc.Content.GetType().ToString();
                if (PE.Type == "System.Windows.Controls.RichTextBox")
                {
                    RichTextBox rtb = cc.Content as RichTextBox;
                    TextRange content = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                    using(MemoryStream ME = new MemoryStream())
                    {
                    content.Save(ME, DataFormats.Rtf);
                    StreamReader sr = new StreamReader(ME);
                    
                    ME.Position = 0;
                    PE.Child.Document = sr.ReadToEnd();
                    sr.Close();
                    }
                }
                else if (PE.Type == "System.Windows.Controls.Image")
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
                        stream.Read(img,0,img.Length);
                        PE.Child.Image = img;
                    }
                    
                }
                else if (PE.Type == "System.Windows.Shapes.Ellipse" || PE.Type == "System.Windows.Shapes.Rectangle" )
                {
                    Shape s = cc.Content as Shape;
                    PE.Child.Brush = s.Fill.ToString();
                }
                elements.Add(PE);
            }

            PrintPage(elements);
        }


        /// <summary>
        /// A Method for saving the actual page to the file
        /// 
        /// p.s. the image saving/loading from this point works fine
        /// </summary>
        /// <param name="elements">All the components of the page</param>
        public void PrintPage(List<PageElement> elements)
        {
            
            using (FileStream fs = File.Open("durp.txt", FileMode.Create))
            {
                fs.Write(UnicodeEncoding.Unicode.GetBytes("page:0" + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount("page:0" + "\r\n"));
                fs.Write(UnicodeEncoding.Unicode.GetBytes("node:" + elements.Count + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount("node:" + elements.Count + "\r\n"));
                foreach (PageElement PE in elements)
                {
                    fs.Write(UnicodeEncoding.Unicode.GetBytes("cc:" + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount("cc:" + "\r\n"));
                    fs.Write(UnicodeEncoding.Unicode.GetBytes(" width:" + PE.Width + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount(" width:" + PE.Width + "\r\n"));
                    fs.Write(UnicodeEncoding.Unicode.GetBytes(" height:" + PE.Height + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount(" height:" + PE.Height + "\r\n"));
                    fs.Write(UnicodeEncoding.Unicode.GetBytes(" top:" + PE.Top + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount(" top:" + PE.Top + "\r\n"));
                    fs.Write(UnicodeEncoding.Unicode.GetBytes(" left:" + PE.Left + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount(" left:" + PE.Left + "\r\n"));
                    fs.Write(UnicodeEncoding.Unicode.GetBytes(" zindex:" + PE.Zindex + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount(" zindex:" + PE.Zindex + "\r\n"));
                    fs.Write(UnicodeEncoding.Unicode.GetBytes(" child:" + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount(" child:" + "\r\n"));
                    fs.Write(UnicodeEncoding.Unicode.GetBytes("  type:" + PE.Type + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount("  type:" + PE.Type + "\r\n"));
                    if (PE.Type == "System.Windows.Controls.RichTextBox")
                    {
                        fs.Write(UnicodeEncoding.Unicode.GetBytes("  rtf:" + PE.Child.Document + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount("  rtf:" + PE.Child.Document + "\r\n"));
                    }
                    else if (PE.Type == "System.Windows.Controls.Image")
                    {

                        fs.Write(UnicodeEncoding.Unicode.GetBytes("  img:" + PE.Child.Image.Length + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount("  img:" + PE.Child.Image.Length + "\r\n"));


                        fs.Write(PE.Child.Image, 0, PE.Child.Image.Length);
                        fs.Write(UnicodeEncoding.Unicode.GetBytes("\r\n" + "  fill:" + PE.Child.Fill + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount("\r\n" + "  fill:" + PE.Child.Fill + "\r\n"));

                    }
                    else if (PE.Type == "System.Windows.Shapes.Ellipse" || PE.Type == "System.Windows.Shapes.Rectangle")
                    {
                        fs.Write(UnicodeEncoding.Unicode.GetBytes("  brush:" + PE.Child.Brush + "\r\n"), 0, UnicodeEncoding.Unicode.GetByteCount("  brush:" + PE.Child.Brush + "\r\n"));
                    }
                }
                fs.Close();
            }
        }
            

    }
}
