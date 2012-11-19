using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YBMForms.DLL
{
    internal class PageSaver
    {
        private Canvas page;

        /// <summary>
        /// main constructor
        /// </summary>
        /// <param name="c">host canvas</param>
        internal PageSaver(Canvas c)
        {
            page = c;
        }

        /// <summary>
        /// this is the mastersave controls method
        /// </summary>
        /// <returns>returns a list of all controls on the main form</returns>
        internal List<PageElement> SaveCanvas()
        {
            List<PageElement> elements = new List<PageElement>();
            foreach (UIElement child in page.Children)
            {
                ContentControl cc = child as ContentControl;
                PageElement PE = new PageElement();
                SaveContentController(cc, PE);

                if (PE.ControlType == "System.Windows.Controls.RichTextBox")
                    SaveTextBox(cc, PE);
                else if (PE.ControlType == "System.Windows.Controls.Image")
                    SaveImage(cc, PE);
                else if (PE.ControlType == "System.Windows.Shapes.Ellipse" || PE.ControlType == "System.Windows.Shapes.Rectangle")
                {
                    Shape s = cc.Content as Shape;
                    PE.Child.Brush = s.Fill.ToString();
                }
                elements.Add(PE);
            }

            return elements;
        }


        /// <summary>
        /// saves a image control
        /// </summary>
        /// <param name="cc">parent content controll</param>
        /// <param name="PE">target page element</param>
        private static void SaveImage(ContentControl cc, PageElement PE)
        {
            Image i = cc.Content as Image;
            PE.Child.Fill = i.Stretch.ToString();
            //create a new memory stream and pngbitmapencoder for obtaining the bitmap
            PngBitmapEncoder png = new PngBitmapEncoder();
            using (MemoryStream ms = new MemoryStream())
            {
                var imagesource = i.Source;
                //if the image was cropped treat it slightly differently
                if (i.Source is CroppedBitmap)
                {
                    png.Frames.Add(BitmapFrame.Create(imagesource as CroppedBitmap));
                }
                else
                {
                    png.Frames.Add(BitmapFrame.Create(imagesource as BitmapImage));
                }
                //save the png and flush the stream
                png.Save(ms);
                ms.Flush();
                //save the stream to a byte[];
                PE.Child.Image = ms.ToArray();
            }
        }

        /// <summary>
        /// saves a rich text box
        /// </summary>
        /// <param name="cc">content control parent</param>
        /// <param name="PE">target page element</param>
        private static void SaveTextBox(ContentControl cc, PageElement PE)
        {
            PE.Child.BorderColor = ((Control)cc.Content).BorderBrush.ToString();
            PE.Child.BorderThickness = ((Control)cc.Content).BorderThickness;
            RichTextBox rtb = cc.Content as RichTextBox;
            //start a new text range
            TextRange content = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            //open a memory stream 
            using (MemoryStream ME = new MemoryStream())
            {
                //saves the richtextbox document to the memory stream as an rtf
                content.Save(ME, DataFormats.Rtf);
                StreamReader sr = new StreamReader(ME);

                //reset memory stream position
                ME.Position = 0;
                //read in contents of the streamreader
                PE.Child.Document = sr.ReadToEnd();
                PE.Child.BackgroundColor = rtb.Background.ToString();
            }
        }

        /// <summary>
        /// Saves the content controller
        /// 
        /// </summary>
        /// <param name="cc">content control</param>
        /// <param name="PE">page element</param>
        private static void SaveContentController(ContentControl cc, PageElement PE)
        {
            PE.Width = cc.Width;
            PE.Height = cc.Height;
            PE.Left = Canvas.GetLeft(cc);
            PE.Top = Canvas.GetTop(cc);
            PE.ZIndex = Canvas.GetZIndex(cc);
            string sc = cc.RenderTransform.GetType().ToString();
            //if the control has been rotated then save it
            if (sc == new RotateTransform().GetType().ToString())
                PE.Rotation = ((RotateTransform)cc.RenderTransform).Angle;
            PE.ControlType = cc.Content.GetType().ToString();
        }


        /// <summary>
        /// A Method for saving the actual page to the file
        /// 
        /// p.s. the image saving/loading from this point works fine
        /// </summary>
        /// <param name="page">All the components of the page</param>
        static internal byte[] SavePageElements(List<PageElement> page)
        {
            byte[] buffer;
            //open memorystream to write data into memory for conversion
            using (MemorystreamOut mso = new MemorystreamOut())
            {
                mso.WriteLine("node:" + page.Count);
                //loop through each element
                foreach (PageElement PE in page)
                {
                    //write out content control
                    mso.WriteLine("cc:");
                    mso.WriteLine(" width:" + PE.Width);
                    mso.WriteLine(" height:" + PE.Height);
                    mso.WriteLine(" top:" + PE.Top);
                    mso.WriteLine(" left:" + PE.Left);
                    mso.WriteLine(" zindex:" + PE.ZIndex);

                    mso.WriteLine(" bordercolor:" + PE.Child.BorderColor);
                    mso.WriteLine(" borderthickness:" + PE.Child.BorderThickness);
                    mso.WriteLine(" rotation:" + PE.Rotation);
                    //write out child control info
                    mso.WriteLine(" child:");
                    mso.WriteLine("  type:" + PE.ControlType);

                    if (PE.ControlType == "System.Windows.Controls.RichTextBox")
                    {
                        mso.WriteLine("  rtf:" + PE.Child.Document);
                        mso.WriteLine(" background:" + PE.Child.BackgroundColor);
                    }
                    else if (PE.ControlType == "System.Windows.Controls.Image")
                    {

                        mso.WriteLine("  img:" + PE.Child.Image.Length);


                        mso.Write(PE.Child.Image, 0, PE.Child.Image.Length);
                        mso.WriteLine("\r\n" + "  fill:" + PE.Child.Fill);

                    }
                    else if (PE.ControlType == "System.Windows.Shapes.Ellipse" || PE.ControlType == "System.Windows.Shapes.Rectangle")
                    {
                        mso.WriteLine("  brush:" + PE.Child.Brush);
                    }
                }
                //flush and save the data
                mso.Flush();
                buffer = mso.ToArray();
                return buffer;
            }
        }

    }
}
