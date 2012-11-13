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
    internal class MemorystreamOut : MemoryStream
    {
        internal MemorystreamOut()
        {

        }

        internal void WriteLine(string s)
        {
         Write(UnicodeEncoding.Unicode.GetBytes(s), 0, UnicodeEncoding.Unicode.GetByteCount(s));
        }
    }

    class BookSaver : PageSaver
    {

        public BookSaver(Canvas c, Book b)
            : base(c)
        {
            unmodifiedYearBook = b;
            designerCanvas = c;
        }

        private Book unmodifiedYearBook;
        private Canvas designerCanvas;
        private Page pageToSave;
        private Book modifiedYearBook;

        public void WriteHeader(byte[] page, string fileLoc)
        {
            var pageEdit = (from p in unmodifiedYearBook.Pages
                            where p.PageNumber == pageToSave.PageNumber
                            select p);

            using (FileStreamOut fs = new FileStreamOut(fileLoc, FileMode.OpenOrCreate))
            {
                //Header
                fs.WriteLine("bookname:" + modifiedYearBook.Title);
                fs.WriteLine("created:" + modifiedYearBook.Created);
                fs.WriteLine("lastmodified:" + modifiedYearBook.LastModified);
                //print index
                fs.WriteLine("index:");

                var yearbookwrite = (from u in modifiedYearBook.Pages
                                     orderby u.PageNumber
                                     select modifiedYearBook).Single();

                foreach (Page pageToWrite in yearbookwrite.Pages)
                {
                    fs.WriteLine("pagetype:"+pageToWrite.Type.ToString());
                    fs.WriteLine("pagenumber:" + pageToWrite.PageNumber);
                    fs.WriteLine("pagetype:" + pageToWrite.Offset);
                    fs.WriteLine("pagetype:" + pageToWrite.Length);
                    fs.WriteLine("pagetype:" + pageToWrite.Type.ToString());
                }
            }

        }

        public byte[] SavePage(Page p, List<PageElement> page)
        {
            byte[] buffer;
            pageToSave = p;
            using (MemorystreamOut ms = new MemorystreamOut())
            {
                ms.WriteLine("node:" + page.Count);
                foreach (PageElement PE in page)
                {
                    ms.WriteLine("cc:");
                    ms.WriteLine(" width:" + PE.Width);
                    ms.WriteLine(" height:" + PE.Height);
                    ms.WriteLine(" top:" + PE.Top);
                    ms.WriteLine(" left:" + PE.Left);
                    ms.WriteLine(" zindex:" + PE.Zindex);

                    ms.WriteLine(" bordercolor:" + PE.Child.BorderColor);
                    ms.WriteLine(" borderthickness:" + PE.Child.BorderThickness);
                    ms.WriteLine(" rotation:" + PE.Rotation);
                    ms.WriteLine(" child:");
                    ms.WriteLine("  type:" + PE.Type);

                    if (PE.Type == "System.Windows.Controls.RichTextBox")
                    {
                        ms.WriteLine("  rtf:" + PE.Child.Document);
                        ms.WriteLine(" background:" + PE.Child.BackgroundColor);
                    }
                    else if (PE.Type == "System.Windows.Controls.Image")
                    {

                        ms.WriteLine("  img:" + PE.Child.Image.Length);


                        ms.Write(PE.Child.Image, 0, PE.Child.Image.Length);
                        ms.WriteLine("\r\n" + "  fill:" + PE.Child.Fill);

                    }
                    else if (PE.Type == "System.Windows.Shapes.Ellipse" || PE.Type == "System.Windows.Shapes.Rectangle")
                    {
                        ms.WriteLine("  brush:" + PE.Child.Brush);
                    }
                }

                ms.Flush();
                buffer = new byte[ms.Length];
                  buffer =  ms.ToArray();
            }
            p.Length = buffer.Length;
            return buffer;
        }

        public void SavePages(Dictionary<Page,List<PageElement>> pages )
        {

        }
    }
}
