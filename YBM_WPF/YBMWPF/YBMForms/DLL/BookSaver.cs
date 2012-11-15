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


    class BookSaver : PageSaver
    {


        public BookSaver(Canvas c, Book b)
            : base(c)
        {
            book = b;
        }

        Book book;

        /// <summary>
        /// Adjusts all the offsets for the book
        /// </summary>
        /// <param name="fileloc"></param>
        public Book RealignPage( int viewIndex)
        {
            Book adjusted = book;
            adjusted.Pages[viewIndex].Children = SaveCanvas();
            for (int i = 0; i < adjusted.Pages.Count; i++)
            {
                
                int length = SavePageElements(adjusted.Pages[i].Children).Length;
                adjusted.Pages[i].Length = length;
                if (i == 0)
                {
                    adjusted.Pages[i].Offset = 0;
                }
                else
                {
                    adjusted.Pages[i].Offset = adjusted.Pages[i - 1].Offset + adjusted.Pages[i - 1].Length;
                }
            }

            return adjusted;
        }

        public void SaveBook(string fileloc, int viewIndex)
        {
            book = RealignPage(viewIndex);
            byte[] header = WriteHeader(book);
            

            FileStream NewBook = new FileStream(fileloc,FileMode.Create);
            
            NewBook.Write(header, 0, header.Length);
            foreach (Page p in book.Pages)
            {
                byte[] pageBytes = SavePageElements(p.Children);
                NewBook.Write(pageBytes,0,pageBytes.Length);
            }
        }

        static public byte[] WriteHeader(Book b)
        {
            byte[] buffer;
            using (MemorystreamOut mso = new MemorystreamOut())
            {
                mso.WriteLine("bookname:"+b.BookName);
                mso.WriteLine("created:" + b.Created.ToString());
                mso.WriteLine("lastmodified:" + b.LastModified.ToString());
                mso.WriteLine("index:");
                foreach (Page p in b.Pages)
                {
                    mso.WriteLine("page:");
                    mso.WriteLine("offset:" + p.Offset);
                    mso.WriteLine("length:" + p.Length);
                    mso.WriteLine("pagetype:" + p.Type);
                }
                mso.WriteLine("indexend:");
                mso.Flush();
                buffer = mso.ToArray();
                return buffer;
            }
        }


        
    }
}
