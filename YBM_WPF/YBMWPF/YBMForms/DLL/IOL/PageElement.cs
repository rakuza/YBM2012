using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
namespace YBMForms.DLL.IOL
{
    class PageElement
    {

        public PageElement()
        {
            height = 0;
            width = 0;
            child = new ChildElement();
            type = "";
            left = 0;
            top = 0;
            zindex = 0;

        }

        
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

        private int zindex;

        public int Zindex
        {
            get { return zindex; }
            set { zindex = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal class ChildElement
        {
            public ChildElement()
            {
                brush = "";
                fill = "";
                document = "";
            }

            private string brush;

            public string Brush
            {
                get { return brush; }
                set { brush = value; }
            }
            private string fill;

            public string Fill
            {
                get { return fill; }
                set { fill = value; }
            }
            private byte[] image;

            public byte[] Image
            {
                get {  return image; }
                set { image = value; }
            }
            private string document;

            public string Document
            {
                get { return document; }
                set { document = value; }
            }
        }

    }
}
