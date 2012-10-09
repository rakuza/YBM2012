using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.IO;

namespace YBMForms.DLL.IOL
{
    class PageSaver
    {
        private Canvas page;

        public Canvas Page
        {
            set { page = value; }
        }

        public PageSaver(Canvas c)
        {
            page = c;
        }

        public PageSaver()
        {

        }

        private List<PageElement> elements = new List<PageElement>(); 

        public void SavePage()
        {
            foreach(UIElement child in page.Children)
            {
                ContentControl cc = child as ContentControl;
                PageElement PE = new PageElement();
                PE.Width = cc.Width;
                PE.Height = cc.Height;
                PE.Left = Canvas.GetLeft(cc);
                PE.Top = Canvas.GetTop(cc);
                PE.Type = cc.Content.GetType().ToString();
                if (PE.Type == "System.Windows.Controls.RichTextBox")
                {
                    RichTextBox rtb = cc.Content as RichTextBox;
                    
                }
                elements.Add(PE);
            }
        }

        public void PrintPage()
        {
            StreamWriter sw = new StreamWriter("durp.txt", false);

            //print page
            sw.WriteLine("page:0");
            //print node count
            sw.WriteLine("node:" + elements.Count);
            foreach (PageElement pe in elements)
            {
                sw.WriteLine("cc:");
                sw.WriteLine(" width:");
                sw.WriteLine(" height:");
                sw.WriteLine(" top:");
                sw.WriteLine(" left:");
                sw.WriteLine(" child:");
                sw.WriteLine("  type:");

            }
        }
            

    }
}
