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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using Microsoft.Win32;
using System.IO;
using YBMForms.DLL;


namespace YBMForms
{
    partial class MainWindow
    {
        /// <summary>
        /// creates a new control depending on which butter was pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// creates a new rich textbox
        /// </summary>
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
            rtb.SelectionChanged += new RoutedEventHandler(TextSelectionChanged);
            cc.ClipToBounds = true;
            DesignerCanvas.Children.Add(cc);
            Canvas.SetLeft(cc, 0);
            Canvas.SetTop(cc, 0);
        }

        //creates a new rect
        private void CreateRect()
        {
            ContentControl cc = new ContentControl();
            Rectangle r = new Rectangle();
            r.IsHitTestVisible = true;
            r.Fill = new SolidColorBrush(colourShape.SelectedColor);
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

        /// <summary>
        /// creates a new picture box
        /// </summary>
        private void CreatePictureBox()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = "JPEG Image File (*.jpg) |*.jpg;|PNG Image FIle (*.png) |*.png;|GIF Image File(*.gif) |*.gif;|All Files (*.*) |*.*;|Images (*.jpg,*.gif,*.png) | *.jpg *.gif *.png";
            if ((bool)OFD.ShowDialog())
            {

                ContentControl cc = new ContentControl();
                Image picture = new Image();
                picture.IsHitTestVisible = true;
                byte[] file = File.ReadAllBytes(new Uri(OFD.FileName).LocalPath);
                BitmapImage image = new BitmapImage();
                using (MemoryStream MS = new MemoryStream(file))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = MS;
                    image.EndInit();
                }
                picture.Source = image;
                picture.Stretch = Stretch.Fill;
                cc.Content = picture;
                cc.Style = (Style)FindResource("DesignerItemStyle");
                cc.Width = image.PixelWidth;
                cc.Height = image.PixelHeight;
                cc.Padding = new Thickness(3);
                cc.MouseDoubleClick += new MouseButtonEventHandler(DoubleClickSelect);
                cc.ClipToBounds = true;
                DesignerCanvas.Children.Add(cc);
                Canvas.SetLeft(cc, 0);
                Canvas.SetTop(cc, 0);
            }

        }

        /// <summary>
        /// creates a new elipse
        /// </summary>
        private void CreateElipse()
        {
            ContentControl cc = new ContentControl();
            Ellipse e = new Ellipse();
            e.Fill = new SolidColorBrush(colourShape.SelectedColor);
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
