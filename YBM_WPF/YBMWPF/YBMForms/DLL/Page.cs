﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YBMForms.DLL
{
    enum PageType
    {
        Title,
        BackPage,
        BlackWhite,
        Colour

    }

        internal class Page
        {
            public Page()
            {
                offset = 0;
                length = 0;
                controls = new List<PageElement>();
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

            private List<PageElement> controls;

            public List<PageElement> Controls
            {
                get { return controls; }
                set { controls = value; }
            }
        
    }
}
