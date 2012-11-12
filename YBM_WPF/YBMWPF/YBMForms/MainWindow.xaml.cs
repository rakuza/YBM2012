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
using YBMForms.DLL;
using YBMForms.UIL.AdornerLib;
using System.Globalization;
using Xceed.Wpf;

namespace YBMForms
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private decimal zoom;


        public MainWindow()
        {
            InitializeComponent();

            //note to self throw this stuff in another method
            zoom = 1.8M;
            double temp = Math.Pow(10, (double)zoom);
            temp = temp / 100;
            tbxZoom.Text = temp.ToString("p");
            PaperSizeConstructor(temp);
            

        }

         private void PaperSizeConstructor(double temp)
        {
            PaperSizes.Dpi = 96;
            DesignerCanvas.Height = PaperSizes.PixelPaperHeight;
            DesignerCanvas.Width = PaperSizes.PixelPaperWidth;
            DesignerCanvasZoomBox.Height = (int)(PaperSizes.PixelPaperHeight * temp);
            DesignerCanvasZoomBox.Width = (int)(PaperSizes.PixelPaperWidth * temp);

            backgroundPaper.Width = PaperSizes.PixelPaperWidth;
            backgroundPaper.Height = PaperSizes.PixelPaperHeight;
            borderBleed.Width = PaperSizes.PixelBleedWidth;
            borderBleed.Height = PaperSizes.PixelBleedHeight;
            Canvas.SetZIndex(borderBleed, 0);
            borderUnsafe.Width = PaperSizes.PixelUnsafeWidth;
            borderUnsafe.Height = PaperSizes.PixelUnsafeHeight;
            Canvas.SetZIndex(borderUnsafe, 1);
            bordersafe.Width = PaperSizes.PixelSafeWidth;
            bordersafe.Height = PaperSizes.PixelSafeHeight;
            Canvas.SetZIndex(borderUnsafe, 2);
        }

        private ContentControl lastContentControl;
        private UIElement LastUIElement;



        /// <summary>
        /// DoubleClick Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DoubleClickSelect(object sender, MouseButtonEventArgs e)
        {

            ContentControl cc = sender as ContentControl;
            UIElement uie = cc.Content as UIElement;

            if ((bool)cc.GetValue(Selector.IsSelectedProperty))
            {
                if (lastContentControl != null && LastUIElement != null)
                {
                    lastContentControl = null;
                    LastUIElement = null;
                }
                cc.SetValue(Selector.IsSelectedProperty, false);
                uie.IsHitTestVisible = true;
            }
            else
            {
                if (lastContentControl != null && LastUIElement != null)
                {
                    lastContentControl.SetValue(Selector.IsSelectedProperty, false);
                    LastUIElement.IsHitTestVisible = true;
                    lastContentControl = cc;
                    LastUIElement = uie;
                }
                cc.SetValue(Selector.IsSelectedProperty, true);
                uie.IsHitTestVisible = false;
                lastContentControl = cc;
                LastUIElement = uie;
            }


        }




        private void Save_Click(object sender, RoutedEventArgs e)
        {
            PageSaver ps = new PageSaver(DesignerCanvas);
            ps.SavePage();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            { pd.PrintVisual(DesignerCanvas, "my canvas"); }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            //PageLoader pl = new PageLoader(DesignerCanvas, this);
           // pl.ReadPage("durp.txt");
        }


        private void ZoomIn(object sender, RoutedEventArgs e)
        {
            zoom += 0.1M;
            double temp = Math.Pow(10,(double)zoom);
            temp = temp / 100;
            tbxZoom.Text = temp.ToString("p");
            DesignerCanvasZoomBox.Width = (int)(temp * PaperSizes.PixelBleedWidth);
            DesignerCanvasZoomBox.Height = (int)(temp * PaperSizes.PixelBleedHeight);
        }

        private void ZoomOut(object sender, RoutedEventArgs e)
        {
            zoom -= 0.1M;
            double temp = Math.Pow(10, (double)zoom);
            temp = temp / 100;
            tbxZoom.Text = temp.ToString("p");
            DesignerCanvasZoomBox.Width = (int)(temp * PaperSizes.PixelBleedWidth);
            DesignerCanvasZoomBox.Height = (int)(temp * PaperSizes.PixelBleedHeight);
        }

        #region zoomchange()
        private void ZoomChange()
        {
            if (tbxZoom.IsFocused)
            {
                string parsestring = tbxZoom.Text.Replace('%', ' ').Trim();
                double temp = double.Parse(parsestring);
                zoom = (decimal)Math.Log10(temp);
                temp = temp/100;
                tbxZoom.Text = (temp).ToString("p");

                
                DesignerCanvasZoomBox.Width = (int)(temp * PaperSizes.PixelBleedWidth);
                DesignerCanvasZoomBox.Height = (int)(temp * PaperSizes.PixelBleedHeight);
            }
        }

        private void ZoomChange(bool skipFocus)
        {
            if (skipFocus)
            {
                string parsestring = tbxZoom.Text.Replace('%', ' ').Trim();
                double temp = double.Parse(parsestring);
                zoom = (decimal)Math.Log10(temp);
                temp = temp / 100;
                tbxZoom.Text = (temp).ToString("p");
                

                DesignerCanvasZoomBox.Width = (int)(temp * PaperSizes.PixelBleedWidth);
                DesignerCanvasZoomBox.Height = (int)(temp * PaperSizes.PixelBleedHeight);
            }
        }
    #endregion

        private void tbxZoom_LostFocus_1(object sender, RoutedEventArgs e)
        {
            ZoomChange(true);
        }

        private void _CropImage_Click_1(object sender, RoutedEventArgs e)
        {

                    UIElement u = SeekSelection();
                    if (u.GetType().ToString() == "System.Windows.Controls.Image")
                    {
                        Image img = u as Image;
                        PhotoEditor croper = new PhotoEditor((BitmapSource)img.Source);
                        croper.ShowDialog();
                        img.Source = croper.Image;

            }
        }

        /// <summary>
        /// Method for accessing selected/adorned control on the canvas
        /// </summary>
        /// <returns>Control with out content container</returns>
        private UIElement SeekSelection()
        {
            UIElement u = new UIElement();
            //Searches through existing controls
            foreach (ContentControl cc in DesignerCanvas.Children)
            {
                //if the content container is selected
                //return turn it else send back blank uielement
                if (Selector.GetIsSelected(cc))
                    u = cc.Content as UIElement;
            }
            
            return u;
        }

        /// <summary>
        /// This method handles the changing of the fill color for the selected shape
        /// </summary>
        /// <param name="sender">ColorPicker</param>
        /// <param name="e">Event args</param>
        private void Shape_Fill_Change_On_Selected_Color(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            //getting the selected controlls
            UIElement u = SeekSelection();
            //checking if its a shape
            // if it is it sets the color to the selected color
            if (u.GetType().ToString() == "System.Windows.Shapes.Rectangle" || u.GetType().ToString() == "System.Windows.Shapes.Ellipse")
            {
                
                Shape shape = u as Shape;
                SolidColorBrush b = new SolidColorBrush(e.NewValue);
                shape.Fill = b;
            }
        }

        /// <summary>
        /// Exit Handler
        /// </summary>
        /// <param name="sender">Menu</param>
        /// <param name="e">Event args</param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Font_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
