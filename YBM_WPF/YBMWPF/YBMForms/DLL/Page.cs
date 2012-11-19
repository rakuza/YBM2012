﻿using System.Collections.Generic;

namespace YBMForms.DLL
{
    /// <summary>
    /// Different types of pages
    /// </summary>
    public enum PageType
    {
        Title,
        BackPage,
        BlackWhite,
        Colour

    }

    public class Page
    {

        /// <summary>
        /// Constructor for new page
        /// </summary>
        public Page()
        {
            offset = 0;
            length = 10;
            type = PageType.BlackWhite;
            //given a negitive to show its out of range
            pageNumber = -1;
            children = new List<PageElement>();
        }

        private int pageNumber;

        public int PageNumber
        {
            get { return pageNumber; }
            set { pageNumber = value; }
        }
        private int offset;

        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        private int length;

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        private PageType type;

        public PageType Type
        {
            get { return type; }
            set { type = value; }
        }

        private List<PageElement> children;

        public List<PageElement> Children
        {
            get { return children; }
            set { children = value; }
        }

    }
}
