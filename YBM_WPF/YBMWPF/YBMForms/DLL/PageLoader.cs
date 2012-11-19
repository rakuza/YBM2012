using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace YBMForms.DLL
{
    internal class PageLoader : YamlToType
    {

        internal PageLoader()
        {
            HeaderOffset = 0;
        }

        private int HeaderOffset;

        /// <summary>
        /// a class to parse all the controls in a file
        /// </summary>
        /// <param name="fileLocation">location of the file</param>
        /// <param name="Length">length of the section</param>
        /// <param name="offSet">where in the file it is stored</param>
        /// <returns>a list of page elements</returns>
        internal List<PageElement> ReadPage(string fileLocation, int Length, int offSet)
        {
            List<PageElement> readControls = new List<PageElement>();

            //opens up a memory stream to store the bytes from the file
            using (MemoryStream pageStream = new MemoryStream())
            {
                using (FileStream fs = File.Open(fileLocation, FileMode.Open))
                {
                    //reads in all the bytes
                    byte[] bytebuffer = new byte[Length];
                    fs.Position = offSet + HeaderOffset;
                    fs.Read(bytebuffer, 0, Length);
                    //saves the bytes into the memory stream
                    pageStream.Write(bytebuffer, 0, bytebuffer.Length);
                }
                //flushes the memory stream to save all the data in and resets the position
                pageStream.Flush();
                pageStream.Position = 0;

                int nodes = 0;
                string buffer = "";
                //opens the line reader
                //and sets the expected number of nodes
                LineReader lr = new LineReader(pageStream);
                PageElement PE = new PageElement();
                buffer = lr.ReadLine();

                nodes = Getint(buffer);
                nodes *= 13;
                while (nodes != 0)
                {

                    buffer = lr.ReadLine();
                    //if the buffer is empty skip to the next line
                    if (!string.IsNullOrWhiteSpace(buffer))
                    {
                        //gets the action
                        string action = GetParam(buffer);


                        switch (action)
                        {

                            case "cc":
                                //adds a new page element if it is not new
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
                                PE.ControlType = GetString(buffer);
                                if (PE.ControlType == "System.Windows.Controls.Image")
                                    nodes++;
                                break;

                            case "fill":
                                PE.Child.Fill = GetString(buffer);
                                break;

                            case "brush":
                                PE.Child.Brush = GetString(buffer);
                                break;

                            case "zindex":
                                PE.ZIndex = Getint(buffer);
                                break;

                            case "rotation":
                                PE.Rotation = GetDouble(buffer);
                                break;

                            case "background":
                                PE.Child.BackgroundColor = GetString(buffer);
                                break;

                            case "borderthickness":
                                //read in an array of numbers
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
                                //if the rtf document still has more content keep reading
                                PE.Child.Document = GetString(buffer);
                                while (lr.Peek() == '{' || lr.Peek() == '}')
                                {
                                    PE.Child.Document += lr.ReadLine();
                                }
                                nodes++;
                                break;

                            case "img":
                                //read the entire image
                                int block = Getint(buffer);
                                PE.Child.Image = new byte[block];
                                pageStream.Read(PE.Child.Image, 0, block);
                                pageStream.Position += 4;
                                break;

                            default:
                                break;
                        }
                    }
                    //deincrement the amount of nodes remaining
                    nodes--;

                }
                //ad any page elements that may remain in buffer
                readControls.Add(PE);
            }
            return readControls;

        }

    }
}
