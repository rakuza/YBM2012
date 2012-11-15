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
using System.Globalization;
using YBMForms.DLL;

namespace YBMForms.UIL
{
    public class BookViewer
    {
        public BookViewer(Canvas c,MainWindow h,Book b)
        {
            designerCanvas = c;
            host = h;
            currentbook = b;
            viewIndex = 0;
        }

       static private MainWindow host;
        static private Canvas designerCanvas;

        private Book currentbook;

        public Book CurrentBook
        {
            get { return currentbook; }
            set { currentbook = value;
            this.ViewIndex = 0;

            }
        }

        private int viewIndex;

        public int ViewIndex
        {
            get { return viewIndex; }
            set {
                if (viewIndex >= 0 && viewIndex <= currentbook.Pages.Count)
                {
                    viewIndex = value;
                    RenderPage();
                }
                }
        }

        public void OpenBook(string filelocation)
        {
            BookReader br = new BookReader(filelocation, designerCanvas, host);
            this.CurrentBook = br.ReadBook();
        }

        public void SaveBook(string filelocation)
        {
            BookSaver bs = new BookSaver(designerCanvas, currentbook);
            bs.SaveBook(filelocation, viewIndex);
        }

        public void DeletePage()
        {
            this.currentbook.Pages.Remove(this.currentbook.Pages[viewIndex]);
            this.viewIndex = viewIndex - 1;
        }

        public void NewPage()
        {
            DLL.Page p = new DLL.Page();
            p.PageNumber = this.currentbook.Pages.Count - 1;
            this.currentbook.Pages.Insert(this.currentbook.Pages.Count,p);
            viewIndex = this.currentbook.Pages.Count;
        }

        #region Render
        private void RenderPage()
        {
            
            designerCanvas.Children.RemoveRange(0, designerCanvas.Children.Count);
            foreach (PageElement PE in currentbook.Pages[viewIndex].Children)
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



                if (!string.IsNullOrWhiteSpace(PE.Type))
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
    }
}
