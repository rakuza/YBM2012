using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YBMForms.DLL;

namespace YBMForms.UIL
{
    public class BookViewer
    {
        public BookViewer(Canvas c, MainWindow h, Book b)
        {
            designerCanvas = c;
            host = h;
            currentbook = b;
            viewIndex = 0;
        }

        static private MainWindow host;
        static private Canvas designerCanvas;

        public static Canvas DesignerCanvas
        {
            get { return BookViewer.designerCanvas; }
        }

        private Book currentbook;

        public Book CurrentBook
        {
            get { return currentbook; }

            set
            {
                currentbook = value;
                SetNewViewIndex(0);
            }
        }

        private int viewIndex;

        public int ViewIndex
        {
            get { return viewIndex; }
            set
            {
                if (viewIndex >= 0 && viewIndex <= currentbook.Pages.Count)
                {
                    SavePage();
                    viewIndex = value;
                    RenderPage(designerCanvas);
                }
            }
        }

        public void SetNewViewIndex(int index)
        {
            viewIndex = index;
            RenderPage(designerCanvas);
        }

        public void OpenBook(string filelocation)
        {
            BookReader br = new BookReader(filelocation);
            Book openedBook = br.ReadBook();
            currentbook = openedBook;
            SetNewViewIndex(0);
        }

        public void SaveBook(string filelocation)
        {
            BookSaver bs = new BookSaver(designerCanvas, currentbook);
            bs.SaveBook(filelocation, viewIndex);
        }

        public void DeletePage()
        {
            designerCanvas.Children.RemoveRange(0, designerCanvas.Children.Count);
            this.currentbook.Pages.Remove(this.currentbook.Pages[viewIndex]);
            SetNewViewIndex(viewIndex--);
        }

        public void NewPage()
        {
            DLL.Page p = new DLL.Page();
            p.PageNumber = this.currentbook.Pages.Count - 1;
            this.currentbook.Pages[this.currentbook.Pages.Count -1].PageNumber++;
            this.currentbook.Pages.Insert(this.currentbook.Pages.Count-1, p);
            SetNewViewIndex( p.PageNumber);
        }



        private void SavePage()
        {
            PageSaver ps = new PageSaver(designerCanvas);
            currentbook.Pages[viewIndex].Children.AddRange(ps.SaveCanvas());
        }


        #region Render
        private void RenderPage(Canvas Canvas)
        {

            Canvas.Children.RemoveRange(0, Canvas.Children.Count);
            foreach (PageElement PE in this.CurrentBook.Pages[viewIndex].Children)
            {
                ContentControl cc = FormContentControl(PE);


                cc.Height = PE.Height;
                cc.Width = PE.Width;
                if (PE.ControlType == "System.Windows.Controls.RichTextBox")
                {
                    GenerateTextBox(PE, cc);
                }
                else if (PE.ControlType == "System.Windows.Controls.Image")
                {
                    GenerateImage(PE, cc);
                }
                else if (PE.ControlType == "System.Windows.Shapes.Ellipse")
                {
                    Ellipse e = new Ellipse();
                    e.Fill = new BrushConverter().ConvertFromString(PE.Child.Brush) as SolidColorBrush;
                    cc.Content = e;
                }
                else if (PE.ControlType == "System.Windows.Shapes.Rectangle")
                {
                    Rectangle r = new Rectangle();
                    r.Fill = new BrushConverter().ConvertFromString(PE.Child.Brush) as SolidColorBrush;
                    cc.Content = r;
                }



                if (!string.IsNullOrWhiteSpace(PE.ControlType))
                {
                    Canvas.Children.Add(cc);
                }

            }
            Canvas.InvalidateVisual();
        }

        static private ContentControl FormContentControl(PageElement PE)
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
            Canvas.SetZIndex(cc, PE.ZIndex);
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
