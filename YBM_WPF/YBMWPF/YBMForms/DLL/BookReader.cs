using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Converters;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace YBMForms.DLL
{
    class BookReader : PageLoader
    {
        

        public BookReader(string fileLocation, Canvas c, MainWindow h) : base(c)
        {
            fileLoc = fileLocation;
            ReadPageLocations();
            ReadPage(0);
            designerCanvas = c;
            host = h;
        }

        private string fileLoc;
        private string bookTitle;
        private int viewIndex;
        private MainWindow host;

        public int ViewIndex
        {
            get { return viewIndex; }
        }
        private List<Page> pages = new List<Page>();

        public List<Page> Pages
        {
            get { return pages; }
            set { pages = value; }
        }

        List<PageElement> current, previous, next;

        Canvas designerCanvas;


        #region Page Navigation
        public void ReadPage(int index)
        {
            if (index != 0 || index != pages.Count)
            {
                viewIndex = index;
                previous = GetPage(index - 1);
                current = GetPage(index);
                next = GetPage(index + 1);
            }
            else if (index == 0)
            {
                previous = null;
                current = GetPage(index);
                next = GetPage(index + 1);
            }
            else if (index == pages.Count)
            {
                previous = GetPage(index - 1);
                current = GetPage(index);
                next = null;
            }
            RenderPage(current);
        }

        public void NextPage()
        {
            if (viewIndex < pages.Count || next != null)
            {
                viewIndex++;
                previous = current;
                current = next;
                next = GetPage(viewIndex + 1);
            }
            else if (viewIndex == pages.Count)
            {
                previous = current;
                current = next;
                next = null;
            }
            RenderPage(current);
        }

        public void PreviousPage()
        {
            if (viewIndex > 0 || previous != null)
            {
                viewIndex--;
                next = current;
                current = previous;
                previous = GetPage(viewIndex - 1);
            }
            else if (viewIndex == 0)
            { 
                next = current;
                current = previous;
                previous = null;
            }
            RenderPage(current);
        }
        #endregion

        private List<PageElement> GetPage(int index)
        {
            return ReadPage(fileLoc,pages[index].Offset,pages[index].Length);
        }

#region page Rendering
        private void RenderPage(List<PageElement> Controls)
        {
            designerCanvas.Children.RemoveRange(0, designerCanvas.Children.Count);
            foreach (PageElement PE in Controls)
            {
                ContentControl cc = FormContentControl(PE);
                
                
                cc.Height = PE.Height;
                cc.Width = PE.Width;
                if (PE.Type == "System.Windows.Controls.RichTextBox")
                {
                    GenerateTextBox(PE, cc);
                }
                else if (PE.Type == "System.Windows.Controls.Image")
                {
                    GenerateImage(PE, cc);
                }
                else if (PE.Type == "System.Windows.Shapes.Ellipse")
                {
                    Ellipse e = new Ellipse();
                    e.Fill = new BrushConverter().ConvertFromString(PE.Child.Brush) as SolidColorBrush;
                    cc.Content = e;
                }
                else if (PE.Type == "System.Windows.Shapes.Rectangle")
                {
                    Rectangle r = new Rectangle();
                    r.Fill = new BrushConverter().ConvertFromString(PE.Child.Brush) as SolidColorBrush;
                    cc.Content = r;
                }

                

                if ( !string.IsNullOrWhiteSpace(PE.Type))
                {
                    designerCanvas.Children.Add(cc);
                }
                
            }
            designerCanvas.InvalidateVisual();
        }

        private ContentControl FormContentControl(PageElement PE)
        {
            ContentControl cc = new ContentControl();
            cc.IsHitTestVisible = true;
            cc.Padding = new Thickness(3);
            cc.MouseDoubleClick += new MouseButtonEventHandler(host.DoubleClickSelect);
            cc.Style = (Style)host.FindResource("DesignerItemStyle");
            cc.ClipToBounds = true;
            cc.RenderTransform = new RotateTransform(PE.Rotation);
            Canvas.SetLeft(cc, PE.Left);
            Canvas.SetTop(cc, PE.Top);
            Canvas.SetZIndex(cc, PE.Zindex);
            return cc;
        }

        private static void GenerateImage(PageElement PE, ContentControl cc)
        {
            Image i = new Image();

            using (MemoryStream me = new MemoryStream(PE.Child.Image))
            {
                BitmapImage temp = new BitmapImage();

                temp.BeginInit();
                temp.CacheOption = BitmapCacheOption.OnLoad;
                temp.StreamSource = me;
                temp.EndInit();

                i.Source = temp;
            }
            i.Stretch = (Stretch)Enum.Parse(typeof(Stretch), PE.Child.Fill);
            cc.Content = i;

        }

        private static void GenerateTextBox(PageElement PE, ContentControl cc)
        {
            RichTextBox rtb = new RichTextBox();
            rtb.SpellCheck.IsEnabled = true;
            rtb.Background = new BrushConverter().ConvertFromString(PE.Child.BackgroundColor) as SolidColorBrush;
            //rtb.Background = Brushes.Transparent;
            using (MemoryStream me = new MemoryStream(ASCIIEncoding.Default.GetBytes(PE.Child.Document)))
            {
                TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                me.Position = 0;
                tr.Load(me, DataFormats.Rtf);
                cc.Content = rtb;
            }
            ((Control)cc.Content).BorderThickness = PE.Child.BorderThickness;
        }


#endregion


        /// <summary>
        /// Reads out the offsets and the lengths in the file for the pages in question
        /// </summary>
        /// <param name="fileLocation"></param>
        private void ReadPageLocations()
        {

            using (FileStream fs = File.Open(fileLoc, FileMode.Open))
            {
                LineReader lr = new LineReader(fs);
                Page p = new Page();
                int index = 0;
                while (fs.Position != fs.Length)
                {
                    string buffer = lr.ReadLine();
                    bool indexRead = false;
                    string action = GetParam(buffer);

                    switch (action)
                    {

                        case "bookname":
                            bookTitle = GetString(buffer);
                            break;

                        case"index":
                            index = Getint(buffer);
                            break;

                        case"page":
                            if (index != 0)
                            {
                                pages.Add(p);
                            }
                            p = new Page();
                            break;

                        case "offset":
                            p.Offset = Getint(buffer);
                            break;

                        case "length":
                            p.Length = Getint(buffer);
                            break;

                        case"pagetype":
                            p.Type = (PageType)Enum.Parse(typeof(PageType),GetString(buffer));
                            break;

                        case"node":
                            indexRead = true;
                            break;

                        default:
                            break;

                            
                    }

                    if (indexRead)
                    {
                        break;
                    }
                }
            }
        }
    }
}
