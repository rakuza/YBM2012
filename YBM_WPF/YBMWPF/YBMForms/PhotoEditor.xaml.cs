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

        /// <summary>
        /// constructor
        /// 
        /// resizes form controls to match incoming image
        /// </summary>
        /// <param name="s"></param>
        public PhotoEditor(BitmapSource s)
        {
            InitializeComponent();
            img = s;
            ApplyImage(s);
        }

        /// <summary>
        /// loads in a file to crop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Obsolete("Surplus to current requirements")]
        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            //reads the file and converts it into a bit map
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

        /// <summary>
        /// Resizes the content container to handle certain sized images
        /// </summary>
        /// <param name="image"></param>
        private void ApplyImage(BitmapSource image)
        {
            CropContainer.Width = image.PixelWidth;
            CropContainer.Height = image.PixelHeight;
            pbxContent.Width = image.PixelWidth;
            pbxContent.Height = image.PixelHeight;
            pbxContent.Source = image;
        }

        /// <summary>
        /// This will start the selection process of
        /// drawing the selection box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectionBegin(object sender, MouseButtonEventArgs e)
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

        /// <summary>
        /// When the left click button is release
        /// this will set moving to false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectionLeftButtonOff(object sender, MouseButtonEventArgs e)
        {
            moving = false;
        }

        /// <summary>
        /// the event for detecting the mouse being dragged across a image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DragSelection(object sender, MouseEventArgs e)
        {
            //if the moving bool has been set trigger off the if statement
            if (moving)
            {
                double top, x, y, left;
                left = Canvas.GetLeft(selection);
                top = Canvas.GetTop(selection);
                x = e.GetPosition(CropContainer).X;
                y = e.GetPosition(CropContainer).Y;
                //if the size is within the smallest size then resize to the following
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

        /// <summary>
        /// Crop Image Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CropImageClick(object sender, RoutedEventArgs e)
        {
            int left, top, width, height;
            //gets and sets cordinates of the rectangle
            left = Convert.ToInt32(Canvas.GetLeft(selection));
            top = Convert.ToInt32(Canvas.GetTop(selection));
            width = Convert.ToInt32(selection.Width);
            height = Convert.ToInt32(selection.Height);
            //turns the cordinates of the rectangle and makes a rect and crops the image
            Int32Rect r = new Int32Rect(left, top, width, height);
            CroppedBitmap cbm = new CroppedBitmap((BitmapSource)pbxContent.Source, r);
            img = cbm;
            //applies the new cropped bitmap
            ApplyImage((BitmapSource)cbm);
            //remove selection container
            CropContainer.Children.Remove(selection);
        }

        /// <summary>
        /// Reject Image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RejectImage(object sender, RoutedEventArgs e)
        {
            //come back to here
        }
    }
}
