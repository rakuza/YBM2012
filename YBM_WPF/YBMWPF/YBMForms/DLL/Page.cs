using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBMForms.DLL
{
    public enum PageType
    {
        Title,
        BackPage,
        BlackWhite,
        Colour

    }

        public class Page
        {
            public Page()
            {
                offset = 0;
                length = 10;
                type = PageType.BlackWhite;
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
