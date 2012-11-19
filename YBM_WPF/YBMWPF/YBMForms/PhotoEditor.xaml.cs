using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using YBMForms.DLL;
namespace YBMForms
{
    /// <summary>
    /// Interaction logic for PhotoEditor.xaml
    /// </summary>
    public partial class PhotoEditor : Window
    {
        private bool moving = false;
        private Point mDownLoc;
        private Rectangle selection = new Rectangle();
        private ImageSource img;

        public ImageSource Image
        {
            get { return img; }
        }

        public PhotoEditor(BitmapSource s)
        {
            InitializeComponent();
            ApplyImage(s);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            byte[] file = File.ReadAllBytes(new Uri(ofd.FileName).LocalPath);
            using (MemoryStream MS = new MemoryStream(file))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = MS;
                image.EndInit();
                ApplyImage(image);
            }
        }

        private void ApplyImage(BitmapSource image)
        {
            CropContainer.Width = image.PixelWidth;
            CropContainer.Height = image.PixelHeight;
            pbxContent.Width = image.PixelWidth;
            pbxContent.Height = image.PixelHeight;
            pbxContent.Source = image;
        }

        private void pbxContent_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            CropContainer.Children.Remove(selection);
            moving = true;
            mDownLoc = e.GetPosition(CropContainer);
            SolidColorBrush b = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
            selection.Fill = b;
            selection.Name = "selectionBox";
            selection.Visibility = Visibility.Visible;
            selection.Width = 10;
            selection.Stroke = Brushes.White;
            selection.StrokeThickness = 1;
            selection.Height = 10;
            Canvas.SetZIndex(selection, 2);
            Canvas.SetLeft(selection, mDownLoc.X);
            Canvas.SetTop(selection, mDownLoc.Y);
            CropContainer.Children.Add(selection);

        }

        private void pbxContent_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            moving = false;
        }

        private void pbxContent_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                double top, x, y, left;
                left = Canvas.GetLeft(selection);
                top = Canvas.GetTop(selection);
                x = e.GetPosition(CropContainer).X;
                y = e.GetPosition(CropContainer).Y;
                if (!(x - left < 10))
                {
                    selection.Width = x - left;

                }

                if (!(y - top < 10))
                {
                    selection.Height = y - top;
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int left, top, width, height;
            left = Convert.ToInt32(Canvas.GetLeft(selection));
            top = Convert.ToInt32(Canvas.GetTop(selection));
            width = Convert.ToInt32(selection.Width);
            height = Convert.ToInt32(selection.Height);
            Int32Rect r = new Int32Rect(left, top, width, height);
            CroppedBitmap cbm = new CroppedBitmap((BitmapSource)pbxContent.Source, r);
            img = cbm;
            ApplyImage((BitmapSource)cbm);
            CropContainer.Children.Remove(selection);
        }

        /// <summary>
        /// Exit Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
