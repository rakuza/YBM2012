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
     public class PageLoader 
    {
        
        private Canvas canvas;

        internal PageLoader(Canvas c)
        {
            canvas = c;
        }

        

        static internal List<PageElement> ReadPage(string fileLocation,int offSet, int Length)
        {
            //method life long vars
            List<PageElement> readControls = new List<PageElement>();

            using (MemoryStream pageStream = new MemoryStream())
            {
                using (FileStream fs = File.Open(fileLocation, FileMode.Open))
                {
                    fs.CopyTo(pageStream);
                    fs.Flush();
                }

                int nodes = 0;
                string buffer = "";
                LineReader lr = new LineReader(pageStream);
                PageElement PE = new PageElement(); ;
                buffer = lr.ReadLine();
                nodes = Convert.ToInt32(GetDouble(buffer));
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
                                PE.Width = GetDouble(buffer);
                                break;

                            case "height":
                                PE.Height = GetDouble(buffer);
                                break;

                            case "top":
                                PE.Top = GetDouble(buffer);
                                break;

                            case "left":
                                PE.Left = GetDouble(buffer);
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
                                PE.Zindex = Getint(buffer);
                                break;

                            case "rotation":
                                PE.Rotation = GetDouble(buffer);
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
                                PE.Child.BorderThickness = new Thickness(
                                    directions[0], directions[1],
                                    directions[2], directions[3]
                                    );
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
                                int block = Getint(buffer);
                                PE.Child.Image = new byte[block];
                                pageStream.Read(PE.Child.Image, 0, block);
                                pageStream.Position += 4;
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
            return readControls;

        }

        static internal double GetDouble(string s)
        {
            string number = s.Substring(s.IndexOf(':')+1);
            return double.Parse(number);
        }

        static internal int Getint(string s)
        {
            string number = s.Substring(s.IndexOf(':') + 1);
            return int.Parse(number);
        }

        static internal string GetString(string s)
        {
            string line = s.Substring(s.IndexOf(':')+1);
            return line;
        }

        static internal string GetParam(string s)
        {

            string param = s.Remove(s.IndexOf(':'));
            param = param.Replace(":","");
            param = param.TrimStart();
            return param;
        }
    }
}
