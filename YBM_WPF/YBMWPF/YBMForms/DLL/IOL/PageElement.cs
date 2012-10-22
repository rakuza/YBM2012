using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
namespace YBMForms.DLL.IOL
{
    /// <summary>
    /// Stores the content control and the content of the content control
    /// </summary>
    class PageElement
    {

        public PageElement()
        {
            Height = 0;
            Width = 0;
            Child = new ChildElement();
            Type = "";
            Left = 0;
            Top = 0;
            Zindex = 0;

        }
        //getters and setters
        public double Height { get; set; }
        public double Width { get; set; }
        public ChildElement Child { get; set; }
        public string Type { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public int Zindex { get; set; }

        /// <summary>
        /// The Child Element For storing the content of the content control
        /// </summary>
        internal class ChildElement
        {
            public ChildElement()
            {
                Brush = "";
                Fill = "";
                Document = "";
            }
            //getters and setters
            public string Brush { get; set; }
            public string Fill { get; set; }
            public byte[] Image { get; set; }
            public string Document { get; set; }

        }

    }
}
