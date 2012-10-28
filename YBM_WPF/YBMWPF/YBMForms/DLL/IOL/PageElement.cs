using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
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
            Rotation = 0;

        }
        //getters and setters
        public double Height { get; set; }
        public double Width { get; set; }
        public ChildElement Child { get; set; }
        public string Type { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public int Zindex { get; set; }
        public double Rotation { get; set; }



        public bool Equals(PageElement PE)
        {
            if (
                this.Height == PE.Height &&
                this.Left == PE.Left &&
                this.Top == PE.Top &&
                this.Type == PE.Type &&
                this.Width == PE.Width &&
                this.Zindex == PE.Zindex &&
                this.Rotation == PE.Rotation &&
                this.Child.Equals(PE.Child)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        } 

        /// <summary>
        /// The Child Element For storing the content of the content control
        /// </summary>
        internal class ChildElement
        {
            internal ChildElement()
            {
                Brush = "";
                Fill = "";
                Document = "";
                BackgroundColor = "";
                BorderThickness = new Thickness(1);

                BorderColor = Brushes.Transparent.ToString();
            }


            //getters and setters
            internal string Brush { get; set; }
            internal string Fill { get; set; }
            internal byte[] Image { get; set; }
            internal string Document { get; set; }
            internal string BackgroundColor { get; set; }
            internal string BorderColor { get; set; }
            internal Thickness BorderThickness { get; set; }


            internal  bool Equals(ChildElement CE)
            {

                if (
                    this.Brush == CE.Brush &&
                    this.Document == CE.Document &&
                    this.Fill == CE.Fill &&
                    this.BackgroundColor == CE.BackgroundColor &&
                    this.BorderThickness == CE.BorderThickness &&
                    this.BorderColor == CE.BorderColor &&
                    this.Image == CE.Image

                    )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



    }
}
