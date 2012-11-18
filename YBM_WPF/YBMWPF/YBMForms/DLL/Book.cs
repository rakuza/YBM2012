using System;
using System.Collections.Generic;

namespace YBMForms.DLL
{
    public class Book
    {
        public Book()
        {
            created = DateTime.Now;
            lastModified = DateTime.Now;
            title = "";
            pages = new List<Page>();
            Page[] p = new Page[3];
            p[0] = new Page();
            p[0].Type = PageType.Title;
            p[0].PageNumber = 0;
            p[1] = new Page();
            p[1].Offset = 10;
            p[1].PageNumber = 1;
            p[2] = new Page();
            p[2].Offset = 20;
            p[2].Type = PageType.BackPage;
            p[2].PageNumber = 2;
            pages.AddRange(p);
            int offset = BookSaver.WriteHeader(this).Length;
            this.pages[0].Offset = offset;
            this.pages[1].Offset += offset;
            this.pages[2].Offset += offset;
        }

        public Book(bool isBlank)
        {
            title = "";
            created = DateTime.Now;
            lastModified = DateTime.Now;
            pages = new List<Page>();
        }


        private List<Page> pages;

        public List<Page> Pages
        {
            get { return pages; }
            set { pages = value; }
        }
        private string title;

        public string BookName
        {
            get { return title; }
            set { title = value; }
        }
        private DateTime created;

        public DateTime Created
        {
            get { return created; }
            set { created = value; }
        }
        private DateTime lastModified;

        public DateTime LastModified
        {
            get { return lastModified; }
            set { lastModified = value; }
        }
    }
}
