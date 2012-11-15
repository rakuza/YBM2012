using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Converters;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
namespace YBMForms.DLL
{
    class BookReader : PageLoader
    {
        

        public BookReader(string fileLocation, Canvas c, MainWindow h) : base(c)
        {
            fileLoc = fileLocation;       
            designerCanvas = c;
            host = h;
        }

        private string fileLoc;
        private MainWindow host;
        private Book newBook;

        //List<PageElement> current, previous, next;

        Canvas designerCanvas;

/*
     
 * */


#region page Rendering


#endregion


        /// <summary>
        /// Reads out the offsets and the lengths in the file for the pages in question
        /// </summary>
        /// <param name="fileLocation"></param>
        public Book ReadBook()
        {
            newBook = new Book();
            using (FileStream fs = File.Open(fileLoc, FileMode.Open))
            {
                LineReader lr = new LineReader(fs);
                Page p = new Page();
                int index = 0;
                while (fs.Position != fs.Length)
                {
                    string buffer = lr.ReadLine();
                    bool indexRead = false;
                    string action = GetParam(buffer);

                    switch (action)
                    {

                        case "bookname":
                            newBook.BookName = GetString(buffer);
                            break;

                        case"created":
                            newBook.Created = GetDate(buffer);
                            break;

                        case"lastmodified":
                            newBook.LastModified = GetDate(buffer);
                            break;

                        case"pagenumber":
                            p.PageNumber = Getint(buffer);
                            break;

                        case"page":
                            if (index != 0)
                            {
                                newBook.Pages.Add(p);
                            }
                            p = new Page();
                            break;

                        case "offset":
                            p.Offset = Getint(buffer);
                            break;

                        case "length":
                            p.Length = Getint(buffer);
                            break;

                        case"pagetype":
                            p.Type = (PageType)Enum.Parse(typeof(PageType),GetString(buffer));
                            break;

                        case"node":
                            indexRead = true;
                            
                            break;

                        default:
                            break;

                            
                    }

                    if (indexRead)
                    {
                        break;
                    }
                }
            }

            for(int i = 0; i <= newBook.Pages.Count;i++)
            {
               newBook.Pages[i].Children = ReadPage(fileLoc,newBook.Pages[i].Offset,newBook.Pages[i].Length);
            }
            return newBook;
        }

        private DateTime GetDate(string s)
        {
            DateTime temp = DateTime.Parse(s);
            return temp;
        }
    }
}
