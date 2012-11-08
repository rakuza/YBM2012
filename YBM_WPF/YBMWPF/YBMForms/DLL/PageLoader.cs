using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Converters;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Documents;
using System.Windows.Input;

namespace YBMForms.DLL
{
     class PageLoader 
    {
        private List<UIElement> controls;
        private List<PageElement> readControls;
        private UIElement canvas;
        private MainWindow host;

        public MainWindow Host
        {
            set { host = value; }
        }

        public UIElement VisualParent { get; set; }


        public PageLoader(UIElement c, MainWindow h)
        {
            controls = new List<UIElement>();
            readControls = new List<PageElement>();
            canvas = c;
            host = h;
        }

        

        public void ReadPage(string fileLocation)
        {
            using (FileStream fs = File.Open(fileLocation, FileMode.Open))
            {
                int nodes = 0;
                string buffer = "";
                LineReader lr = new LineReader(fs);
                lr.ReadLine();//grabs the page number, irrelevent for now
                PageElement PE = new PageElement(); ;
                buffer = lr.ReadLine();
                nodes = Convert.ToInt32(GetNum(buffer));
                nodes *= 13;
                while (nodes != 0)
                {

                    buffer = lr.ReadLine();
                    if ( !string.IsNullOrWhiteSpace(buffer))
                    {
                        string action = GetParam(buffer);


                        switch (action)
                        {

                            case "cc":
                                if (!PE.Equals(new PageElement()))
                                    readControls.Add(PE);
                                PE = new PageElement();
                                break;

                            case "width":
                                PE.Width = GetNum(buffer);
                                break;

                            case "height":
                                PE.Height = GetNum(buffer);
                                break;

                            case "top":
                                PE.Top = GetNum(buffer);
                                break;

                            case "left":
                                PE.Left = GetNum(buffer);
                                break;

                            case "type":
                                PE.Type = GetString(buffer);
                                if (PE.Type == "System.Windows.Controls.Image")
                                    nodes++;
                                break;

                            case "fill":
                                PE.Child.Fill = GetString(buffer);
                                break;

                            case "brush":
                                PE.Child.Brush = GetString(buffer);
                                break;

                            case "zindex":
                                PE.Zindex = (int)GetNum(buffer);
                                break;

                            case "rotation":
                                PE.Rotation = GetNum(buffer);
                                break;

                            case "background":
                                PE.Child.BackgroundColor = GetString(buffer);
                                break;

                            case "borderthickness":
                                string[] numbers = GetString(buffer).Split(',');
                                int[] directions = new int[4];
                                for (int i = 0; i < 4; i++)
                                {
                                    directions[i] = int.Parse(numbers[i]);
                                }
                                PE.Child.BorderThickness = new Thickness(directions[0], directions[1], directions[2], directions[3]);
                                break;

                            case "rtf":
                                PE.Child.Document = GetString(buffer);
                                while (lr.Peek() == '{' || lr.Peek() == '}')
                                {
                                    PE.Child.Document += lr.ReadLine();
                                }
                                nodes++;
                                break;

                            case "img":
                                int block = Convert.ToInt32(GetNum(buffer));
                                PE.Child.Image = new byte[block];
                                fs.Read(PE.Child.Image, 0, block);
                                fs.Position += 4;
                                break;

                            default:
                                break;
                        }
                    }
                    nodes--;

                }
                //ad any page elements that may remain in buffer
                readControls.Add(PE);
            }
            
            FormPage();

        }

        private void FormPage()
        {
            Canvas c = canvas as Canvas;
            c.Children.RemoveRange(0, c.Children.Count);
            foreach (PageElement PE in readControls)
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
                    c.Children.Add(cc);
                }
                
            }
            c.InvalidateVisual();
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

        static private double GetNum(string s)
        {
            string number = s.Substring(s.IndexOf(':')+1);
            return double.Parse(number);
        }

        static private string GetString(string s)
        {
            string line = s.Substring(s.IndexOf(':')+1);
            return line;
        }

        static private string GetParam(string s)
        {

            string param = s.Remove(s.IndexOf(':'));
            param = param.Replace(":","");
            param = param.TrimStart();
            return param;
        }
    }
}
