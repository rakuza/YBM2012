using System;
using System.Collections.Generic;

namespace YBMForms.DLL
{

    public class Book
    {
        /// <summary>
        /// Book constructor
        /// 
        /// Initilises a new book for user use
        /// </summary>
        public Book()
        {
            title = "";
            created = DateTime.Now;
            lastModified = DateTime.Now;
            pages = new List<Page>();
        }

        /// <summary>
        /// creates a basic book
        /// </summary>
        public void SetAsStarterBook()
        {
            created = DateTime.Now;
            lastModified = DateTime.Now;
            title = "";
            pages = new List<Page>();
            Page[] p = new Page[3];
            p[0] = new Page();
            p[0].PageType = PageType.Title;
            p[0].PageNumber = 0;
            p[1] = new Page();
            p[1].Offset = 10;
            p[1].PageNumber = 1;
            p[2] = new Page();
            p[2].Offset = 20;
            p[2].PageType = PageType.BackPage;
            p[2].PageNumber = 2;
            pages.AddRange(p);
            this.pages[0].Offset = 0;
            this.pages[1].Offset += this.pages[0].Offset;
            this.pages[2].Offset += this.pages[1].Offset;
        }


        private List<Page> pages;
        
        /// <summary>
        /// A list of all pages in the year book
        /// </summary>
        public List<Page> Pages
        {
            get { return pages; }
        }
        private string title;

        /// <summary>
        /// the name of the book
        /// </summary>
        public string BookName
        {
            get { return title; }
            set { title = value; }
        }
        private DateTime created;

        /// <summary>
        /// the time the book was created
        /// </summary>
        public DateTime Created
        {
            get { return created; }
            set { created = value; }
        }
        private DateTime lastModified;

        /// <summary>
        /// the time the book was lastmodified
        /// </summary>
        public DateTime LastModified
        {
            get { return lastModified; }
            set { lastModified = value; }
        }
    }
}
