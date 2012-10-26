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

namespace YBMForms.DLL.IOL
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
                nodes *= 9;
                while (nodes != 0)
                {

                    buffer = lr.ReadLine();
                    if (buffer != null && buffer != "")
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

                            case "rtf":
                                PE.Child.Document = GetString(buffer);
                                while (lr.Peek() == '{' || lr.Peek() == '}')
                                {
                                    PE.Child.Document += lr.ReadLine();
                                }
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
                fs.Close();
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
                ContentControl cc = new ContentControl();
                cc.IsHitTestVisible = true;
                cc.Padding = new Thickness(3);
                cc.MouseDoubleClick += new MouseButtonEventHandler(host.DoubleClickSelect);
                cc.Style = (Style)host.FindResource("DesignerItemStyle");
                cc.ClipToBounds = true;
                Canvas.SetLeft(cc,PE.Left);
                Canvas.SetTop(cc, PE.Top);
                Canvas.SetZIndex(cc, PE.Zindex);
                cc.Height = PE.Height;
                cc.Width = PE.Width;
                if (PE.Type == "System.Windows.Controls.RichTextBox")
                {
                    RichTextBox rtb = new RichTextBox();
                    rtb.Background = Brushes.Transparent;
                    using (MemoryStream me = new MemoryStream(ASCIIEncoding.Default.GetBytes(PE.Child.Document)))
                    {
                        TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                        me.Position = 0;
                        tr.Load(me, DataFormats.Rtf);
                        cc.Content = rtb;
                        me.Close();
                    }
                }
                else if (PE.Type == "System.Windows.Controls.Image")
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

                if (PE.Type != "")
                {
                    c.Children.Add(cc);
                }
                
            }
            c.InvalidateVisual();
        }

        private double GetNum(string s)
        {
            string number = s.Substring(s.IndexOf(':')+1);
            return double.Parse(number);
        }

        private string GetString(string s)
        {
            string line = s.Substring(s.IndexOf(':')+1);
            return line;
        }

        private string GetParam(string s)
        {

            string param = s.Remove(s.IndexOf(':'));
            param = param.Replace(":","");
            param = param.TrimStart();
            return param;
        }
    }
}
