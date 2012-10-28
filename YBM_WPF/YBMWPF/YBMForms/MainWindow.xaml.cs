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
using YBMForms.DLL.IOL;
using YBMForms.UIL.AdornerLib;
using System.Globalization;

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
            PaperSizes.Dpi = 300;
            DesignerCanvas.Height = PaperSizes.PixelBleedHeight;
            DesignerCanvas.Width = PaperSizes.PixelBleedWidth;
            DesignerCanvasZoomBox.Height = (int)(PaperSizes.PixelBleedHeight * temp);
            DesignerCanvasZoomBox.Width = (int)(PaperSizes.PixelBleedWidth * temp);
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




        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PageSaver ps = new PageSaver();
            ps.Page = DesignerCanvas;
            ps.SavePage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            { pd.PrintVisual(DesignerCanvas, "my canvas"); }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            PageLoader pl = new PageLoader(DesignerCanvas, this);
            pl.ReadPage("durp.txt");
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
                tbxZoom.Text = (temp/100).ToString("p");
                temp = Math.Log10(temp);
                
                tbxZoom.Text = (temp / 100).ToString("p");
                temp = Math.Log10(temp);
                zoom = (decimal)temp;
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
                tbxZoom.Text = (temp / 100).ToString("p");
                temp = Math.Log10(temp);
                zoom = (decimal)temp;
                DesignerCanvasZoomBox.Width = (int)(temp * PaperSizes.PixelBleedWidth);
                DesignerCanvasZoomBox.Height = (int)(temp * PaperSizes.PixelBleedHeight);
            }
        }
    #endregion

        private void tbxZoom_LostFocus_1(object sender, RoutedEventArgs e)
        {
            ZoomChange(true);
        }
    }
}
