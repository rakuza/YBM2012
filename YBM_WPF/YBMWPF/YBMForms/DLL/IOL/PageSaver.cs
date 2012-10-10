using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
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
                PE.Type = cc.Content.GetType().ToString();
                if (PE.Type == "System.Windows.Controls.RichTextBox")
                {
                    RichTextBox rtb = cc.Content as RichTextBox;
                    TextRange content = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                    MemoryStream ME = new MemoryStream();
                    content.Save(ME, DataFormats.Rtf);
                    StreamReader sr = new StreamReader(ME);
                    PE.Child.Document = sr.ReadToEnd();
                    sr.Close();
                    ME.Dispose();
                }
                else if (PE.Type == "System.Windows.Controls.Image")
                {
                    Image i = cc.Content as Image;
                    PE.Child.Fill = i.Stretch.ToString();
                    RenderTargetBitmap rtb = new RenderTargetBitmap((int)i.ActualWidth, (int)i.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                    PngBitmapEncoder png = new PngBitmapEncoder();
                    png.Frames.Add(BitmapFrame.Create(rtb));
                    MemoryStream ME = new MemoryStream();
                    png.Save(ME);
                    StreamReader sr = new StreamReader(ME);
                    PE.Child.Image = sr.ReadToEnd();
                    sr.Close();
                    ME.Dispose();
                }
                elements.Add(PE);
            }

            //FlowDocument content = new FlowDocument();

            //foreach(UIElement child in page.Children)
            //{
            //    ContentControl cc = child as ContentControl;
            //    PageElement PE = new PageElement();
            //    if (PE.Type == "System.Windows.Controls.RichTextBox")
            //    {
            //        RichTextBox rtb = cc.Content as RichTextBox;
            //        content = rtb.Document;
            //    }
            //}
            //StreamWriter sw = new StreamWriter("durp.txt", false);
            //TextRange document = new TextRange(content.ContentStart, content.ContentEnd);

            //document.Save(sw.BaseStream, DataFormats.Rtf);
        }

        public void PrintPage()
        {
            StreamWriter sw = new StreamWriter("durp.txt", false);

            //print page
            sw.WriteLine("page:0");
            //print node count
            sw.WriteLine("node:" + elements.Count);
            foreach (PageElement pe in elements)
            {
                sw.WriteLine("cc:");
                sw.WriteLine(" width:");
                sw.WriteLine(" height:");
                sw.WriteLine(" top:");
                sw.WriteLine(" left:");
                sw.WriteLine(" child:");
                sw.WriteLine("  type:");


            }
        }
            

    }
}
