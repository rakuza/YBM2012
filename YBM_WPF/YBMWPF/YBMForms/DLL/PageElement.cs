using System.Windows;
using System.Windows.Media;
namespace YBMForms.DLL
{
    /// <summary>
    /// Stores the content control and the content of the content control
    /// </summary>
    public class PageElement
    {

        public PageElement()
        {
            Height = 0;
            Width = 0;
            Child = new ChildElement();
            ControlType = "";
            Left = 0;
            Top = 0;
            ZIndex = 0;
            Rotation = 0;

        }
        //getters and setters
        public double Height { get; set; }
        public double Width { get; set; }
        public ChildElement Child { get; set; }
        public string ControlType { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public int ZIndex { get; set; }
        public double Rotation { get; set; }


        /// <summary>
        /// wrote a quick method which compares the object with its self
        /// </summary>
        /// <param name="pe">the page element</param>
        /// <returns>if it was equal or not</returns>
        public bool Equals(PageElement pe)
        {
            if (
                this.Height == pe.Height &&
                this.Left == pe.Left &&
                this.Top == pe.Top &&
                this.ControlType == pe.ControlType &&
                this.Width == pe.Width &&
                this.ZIndex == pe.ZIndex &&
                this.Rotation == pe.Rotation &&
                this.Child.Equals(pe.Child)
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
        public class ChildElement
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
            public string Brush { get; set; }
            public string Fill { get; set; }
            public byte[] Image { get; set; }
            public string Document { get; set; }
            public string BackgroundColor { get; set; }
            public string BorderColor { get; set; }
            public Thickness BorderThickness { get; set; }

            /// <summary>
            /// a class for determining if it is equal with its self
            /// </summary>
            /// <param name="ce">the childern elements</param>
            /// <returns>if it was true or not</returns>
            public bool Equals(ChildElement ce)
            {

                if (
                    this.Brush == ce.Brush &&
                    this.Document == ce.Document &&
                    this.Fill == ce.Fill &&
                    this.BackgroundColor == ce.BackgroundColor &&
                    this.BorderThickness == ce.BorderThickness &&
                    this.BorderColor == ce.BorderColor &&
                    this.Image == ce.Image

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
