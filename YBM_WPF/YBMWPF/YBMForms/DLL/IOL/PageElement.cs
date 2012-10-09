using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace YBMForms.DLL.IOL
{
    class PageElement
    {
        private double height;

        public double Height
        {
            get { return height; }
            set { height = value; }
        }
        private double width;

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        private ChildElement child;

        public ChildElement Child
        {
            get { return child; }
            set { child = value; }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private double left;

        public double Left
        {
            get { return left; }
            set { left = value; }
        }

        private double top;

        public double Top
        {
            get { return top; }
            set { top = value; }
        }

        internal class ChildElement
        {
            public string brush;
            public Size size;
            public string fill;
            public byte[] image;

        }

    }
}
