using System;
using System.IO;
using System.Text;
using System.Windows.Controls;
namespace YBMForms.DLL
{

    class BookReader : PageLoader
    {
        /// <summary>
        /// initilizes the book reader class
        /// </summary>
        /// <param name="fileLocation">The Path of the file</param>
        /// <param name="c">The Main canvas for the mainwindowform</param>
        /// <param name="h">The mainwindow its self</param>
        public BookReader(string fileLocation)
        {
            fileLoc = fileLocation;
        }

        //global vars
        private Book newBook;
        private string fileLoc;

        /// <summary>
        /// Reads out the offsets and the lengths in the file for the pages in question
        /// </summary>
        /// <param name="fileLocation">string of the file location</param>
        public Book ReadBook()
        {
            //adds in a new blank book 
            newBook = new Book();
            int Offset = 0 ;
            //opens the file at the location provided
            using (FileStream fs = File.Open(fileLoc, FileMode.Open))
            {
                
                string headerLog = "";
                LineReader lr = new LineReader(fs);
                Page p = new Page();
                int index = 0;

                //reads untill the end of the file
                while (fs.Position != fs.Length)
                {
                    //reads in the inital line
                    string buffer = lr.ReadLine();
                    //adds the buffer and the end line demimiter to the log
                    headerLog += buffer + "\r\n";
                    bool indexRead = false;
                    //gets the action from the buffer
                    string action = GetParam(buffer);

                    switch (action)
                    {

                        case "bookname":
                            newBook.BookName = GetString(buffer);
                            break;

                        case "created":
                            newBook.Created = GetDate(buffer);
                            break;

                        case "lastmodified":
                            newBook.LastModified = GetDate(buffer);
                            break;

                        case "pagenumber":
                            p.PageNumber = Getint(buffer);
                            break;

                        case "page":
                            //if not the first page add the page to the book
                            if (index != 0)
                            {
                                newBook.Pages.Add(p);
                            }
                            p = new Page();
                            index++;
                            break;

                        case "offset":
                            p.Offset = Getint(buffer);
                            break;

                        case "length":
                            p.Length = Getint(buffer);
                            break;

                        case "pagetype":
                            //parses the enum value
                            p.PageType = (PageType)Enum.Parse(typeof(PageType), GetString(buffer));
                            break;

                        case "indexend":
                            //adds the last page to the book
                            newBook.Pages.Add(p);
                            //sets the number of bytes in the header
                            Offset = UnicodeEncoding.Unicode.GetByteCount(headerLog);
                            indexRead = true;
                            break;

                        //probally unreachible code now
                        case "node":
                            indexRead = true;
                            break;

                        default:
                            break;


                    }
                    //if we read the end of the header end the loop
                    if (indexRead)
                    {
                        break;
                    }
                }
            }

            //loop through the number of pages found
            //assigns a page number
            // and gets the page contents
            for (int i = 0; i < newBook.Pages.Count; i++)
            {
                newBook.Pages[i].PageNumber = i;
                newBook.Pages[i].Children.AddRange(ReadPage(fileLoc, newBook.Pages[i].Length, newBook.Pages[i].Offset+Offset));
            }
            return newBook;
        }


    }
}
