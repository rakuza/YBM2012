using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using Microsoft.Win32;
using System.IO;
using YBMForms.DLL.IOL;


namespace YBMForms
{
    partial class MainWindow
    {
        private void SpawnControl(object sender, RoutedEventArgs e)
        {
            Control c = sender as Control;

            switch (c.Name)
            {
                case "_SpawnTextBox":
                    CreateTextBox();
                    break;

                case "_SpawnRect":
                    CreateRect();
                    break;

                case "_SpawnPictureBox":
                    CreatePictureBox();
                    break;

                case "_SpawnElipse":
                    CreateElipse();
                    break;

                default:
                    break;


            }
        }

        private void CreateTextBox()
        {
            ContentControl cc = new ContentControl();
            RichTextBox rtb = new RichTextBox();

            rtb.IsHitTestVisible = true;
            cc.Content = rtb;
            cc.Style = (Style)FindResource("DesignerItemStyle");
            cc.Width = 300;
            cc.Height = 300;
            cc.Padding = new Thickness(3);
            cc.MouseDoubleClick += new MouseButtonEventHandler(DoubleClickSelect);
            cc.ClipToBounds = true;
            DesignerCanvas.Children.Add(cc);
            Canvas.SetLeft(cc, 0);
            Canvas.SetTop(cc, 0);
        }

        private void CreateRect()
        {
            ContentControl cc = new ContentControl();
            Rectangle r = new Rectangle();
            r.IsHitTestVisible = true;
            r.Fill = Brushes.LightCoral;
            cc.Content = r;
            cc.Style = (Style)FindResource("DesignerItemStyle");
            cc.Width = 300;
            cc.Height = 300;
            cc.Padding = new Thickness(3);
            cc.MouseDoubleClick += new MouseButtonEventHandler(DoubleClickSelect);
            cc.ClipToBounds = true;
            DesignerCanvas.Children.Add(cc);
            Canvas.SetLeft(cc, 0);
            Canvas.SetTop(cc, 0);
        }

        private void CreatePictureBox()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = "JPEG Image File (*.jpg) |*.jpg;|PNG Image FIle (*.png) |*.png;|GIF Image File(*.gif) |*.gif;|All Files (*.*) |*.*";
            if ((bool)OFD.ShowDialog())
            {

                ContentControl cc = new ContentControl();
                Image picture = new Image();
                picture.IsHitTestVisible = true;
                MemImage image = new MemImage(new Uri(OFD.FileName));
                picture.Source = image.Image;
                picture.Stretch = Stretch.Fill;
                cc.Content = picture;
                cc.Style = (Style)FindResource("DesignerItemStyle");
                cc.Width = image.Image.PixelWidth;
                cc.Height = image.Image.PixelHeight;
                cc.Padding = new Thickness(3);
                cc.MouseDoubleClick += new MouseButtonEventHandler(DoubleClickSelect);
                cc.ClipToBounds = true;
                DesignerCanvas.Children.Add(cc);
                Canvas.SetLeft(cc, 0);
                Canvas.SetTop(cc, 0);
            }

        }

        private void CreateElipse()
        {
            ContentControl cc = new ContentControl();
            Ellipse e = new Ellipse();
            e.Fill = Brushes.Black;
            cc.Content = e;
            cc.Style = (Style)FindResource("DesignerItemStyle");
            cc.Width = 300;
            cc.Height = 300;
            cc.Padding = new Thickness(3);
            cc.MouseDoubleClick += new MouseButtonEventHandler(DoubleClickSelect);
            cc.ClipToBounds = true;
            DesignerCanvas.Children.Add(cc);
            Canvas.SetLeft(cc, 0);
            Canvas.SetTop(cc, 0);
        }
    }
}
