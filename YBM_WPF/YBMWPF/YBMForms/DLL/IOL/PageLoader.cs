﻿using System;
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

        public UIElement VisualParent
        {
            get { return canvas; }
            set { canvas = value; }
        }

        public PageLoader(UIElement c, MainWindow h)
        {
            controls = new List<UIElement>();
            readControls = new List<PageElement>();
            canvas = c;
            host = h;
        }

        

        public void ReadPage(string fileLocation)
        {
            int nodes = 0;
            string buffer = "";
            StreamReader sr = new StreamReader(fileLocation);
            sr.ReadLine();//grabs the page number, irrelevent for now
            PageElement PE = new PageElement(); ;
            nodes = Convert.ToInt32(GetNum(sr.ReadLine())); 
            nodes *= 8;
            while (nodes != 0)
            {
                
                buffer = sr.ReadLine();
                if (buffer != null)
                {
                    string action = GetParam(buffer);
                
                
                switch (action)
                {

                    case "cc":
                        if(!PE.Equals(new PageElement()))
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

                    case "rtf":
                        PE.Child.Document = GetString(buffer);
                        while (sr.Peek() == '{' || sr.Peek() == '}')
                        {
                            PE.Child.Document += sr.ReadLine();
                        }
                        break;

                    case "dat":
                        int block = Convert.ToInt32(GetNum(buffer));
                        char[] temp = new char[block];
                        sr.ReadBlock(temp,0,block);
                        //sr.BaseStream.Position= block;
                        PE.Child.Image = GetString(qstring(temp));
                        break;

                    default:
                        break;
                }
                }
                nodes--;
                
            }
            readControls.Add(PE);
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
                cc.Height = PE.Height;
                cc.Width = PE.Width;
                if (PE.Type == "System.Windows.Controls.RichTextBox")
                {
                    RichTextBox rtb = new RichTextBox();
                    MemoryStream me = new MemoryStream(ASCIIEncoding.Default.GetBytes(PE.Child.Document));
                    
                    //rtb.Selection.Load(me,DataFormats.Rtf);
                    TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                    me.Position = 0;
                    tr.Load(me, DataFormats.Rtf);
                    cc.Content = rtb;
                }
                else if (PE.Type == "System.Windows.Controls.Image")
                {
                    Image i = new Image();
                    
                    MemoryStream me = new MemoryStream(ASCIIEncoding.Default.GetBytes(PE.Child.Image));
                    BitmapImage temp = new BitmapImage();
                    temp.BeginInit();
                    temp.StreamSource = me;
                    temp.EndInit();
                    i.Source = temp;
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

        private string qstring (char[] value)
        {
            return new string(value);
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
