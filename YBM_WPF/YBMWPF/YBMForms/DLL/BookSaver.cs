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

        private FileStream NewBook;

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
            byte[] pageBytes = SavePageElements(adjusted.Pages[viewIndex].Children);
            int incrementOffset = book.Pages[viewIndex].Length - pageBytes.Length;
            adjusted.Pages[viewIndex].Length = pageBytes.Length;

            for (int i = viewIndex; i < book.Pages.Count; i++)
            {
                adjusted.Pages[i].Offset += incrementOffset;
            }

            return adjusted;
        }

        public void SaveBook(string fileloc, int viewIndex)
        {
            book = RealignPage(viewIndex);
            NewBook = new FileStream(fileloc,FileMode.Create);
            byte[] header = WriteHeader(book);
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
                mso.Flush();
                buffer = mso.ToArray();
                return buffer;
            }
        }


        
    }
}
