using System.IO;
using System.Windows.Controls;

namespace YBMForms.DLL
{

    sealed class BookSaver : PageSaver
    {

        /// <summary>
        /// Book saver constructor
        /// </summary>
        /// <param name="c">Canvas all the controls being saved from are loaded on</param>
        /// <param name="b">the book being saved</param>
        public BookSaver(Canvas c, Book b)
            : base(c)
        {
            book = b;
        }

        //global vars
        private Book book;

        /// <summary>
        /// Readjusts all the offsets for the constructed book
        /// </summary>
        /// <param name="viewIndex">the index of the currently open page</param>
        private void RealignPage(int viewIndex)
        {
            //saves all the controls on the currently open canvas
            book.Pages[viewIndex].Children = SaveCanvas();
            //loop through each page of the book
            for (int i = 0; i < book.Pages.Count; i++)
            {
                //gets & sets the number of bytes on the page
                int length = SavePageElements(book.Pages[i].Children).Length;
                book.Pages[i].Length = length;
                //if first set offset to 0
                if (i == 0)
                {
                    book.Pages[i].Offset = 0;
                }
                else
                {
                    //sets the offset to the previous page offset plus the length of the previous page
                    book.Pages[i].Offset = book.Pages[i - 1].Offset + book.Pages[i - 1].Length;
                }
            }
        }

        /// <summary>
        /// Save Book Method
        /// </summary>
        /// <param name="fileloc">sets the location to save to book to</param>
        /// <param name="viewIndex">shows the current page</param>
        public void SaveBook(string fileloc, int viewIndex)
        {
            //adjusts all the offsets
            RealignPage(viewIndex);
            //get all the bytes from the header
            byte[] header = WriteHeader();

            //opens a filestream
            FileStream NewBook = new FileStream(fileloc, FileMode.Create);

            //writes the contents of the header
            NewBook.Write(header, 0, header.Length);

            //looks through each page writing the bytes of the page
            foreach (Page p in book.Pages)
            {
                byte[] pageBytes = SavePageElements(p.Children);
                NewBook.Write(pageBytes, 0, pageBytes.Length);
            }
        }

        /// <summary>
        /// Gets the bytes of the book header
        /// </summary>
        /// <returns>bytes of the book header</returns>
        private byte[] WriteHeader()
        {
            byte[] buffer;
            //opening a stream to memory to easily access bytes
            using (MemorystreamOut mso = new MemorystreamOut())
            {
                //writes out the lines
                mso.WriteLine("bookname:" + book.BookName);
                mso.WriteLine("created:" + book.Created.ToString());
                mso.WriteLine("lastmodified:" + book.LastModified.ToString());
                mso.WriteLine("index:");
                //writes the page details in
                foreach (Page p in book.Pages)
                {
                    mso.WriteLine("page:");
                    mso.WriteLine("offset:" + p.Offset);
                    mso.WriteLine("length:" + p.Length);
                    mso.WriteLine("pagetype:" + p.Type);
                }
                mso.WriteLine("indexend:");
                //writes all the bytes to memory
                mso.Flush();
                //saves the buffer
                buffer = mso.ToArray();
                return buffer;
            }
        }



    }
}
