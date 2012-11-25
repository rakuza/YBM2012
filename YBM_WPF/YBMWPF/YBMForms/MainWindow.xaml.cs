using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using YBMForms.DLL;
using YBMForms.UIL;

namespace YBMForms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        //main constructor
        public MainWindow()
        {
            InitializeComponent();
            //a split off from the constructor to ease the size
            PaperSizeConstructor();
            //sets the zoom to a resonible level
            zoom = 1.8M;
            double temp = Math.Pow(10, (double)zoom);
            //sets up the sizes for the zoom box
            UpdateZoomControls(temp);
        }

        /// <summary>
        /// Sets up the backgrounds on the page and the zoom box sizes
        /// 
        /// its really big really boring and not all that much exciting
        /// </summary>
        /// <param name="temp"></param>
        private void PaperSizeConstructor()
        {
            PaperSize.Type = PaperSize.Standard.A4;
            PaperSize.Dpi = 96;
            DesignerCanvas.Height = PaperSize.Pixel.PaperHeight;
            DesignerCanvas.Width = PaperSize.Pixel.PaperWidth;
            backgroundPaper.Width = PaperSize.Pixel.PaperWidth;
            backgroundPaper.Height = PaperSize.Pixel.PaperHeight;
            borderBleed.Width = PaperSize.Pixel.BleedWidth;
            borderBleed.Height = PaperSize.Pixel.BleedHeight;
            Canvas.SetZIndex(borderBleed, 0);
            borderUnsafe.Width = PaperSize.Pixel.UnsafeWidth;
            borderUnsafe.Height = PaperSize.Pixel.UnsafeHeight;
            Canvas.SetZIndex(borderUnsafe, 1);
            bordersafe.Width = PaperSize.Pixel.SafeWidth;
            bordersafe.Height = PaperSize.Pixel.SafeHeight;
            Canvas.SetZIndex(borderUnsafe, 2);
        }

        //global vars
        private ContentControl lastContentControl;
        private UIElement LastUIElement;
        private static BookViewer current;
        private decimal zoom;
        private bool justdetecting = false;

        /// <summary>
        /// DoubleClick Event Handler
        /// 
        /// toggles selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DoubleClickSelect(object sender, MouseButtonEventArgs e)
        {

            ContentControl cc = sender as ContentControl;
            UIElement uie = cc.Content as UIElement;

            //asks double clicked control if it is selected
            if ((bool)cc.GetValue(Selector.IsSelectedProperty))
            {
                //if it is selected then it checks to make sure it is set
                //if it is set it nulls out the controls
                if (lastContentControl != null && LastUIElement != null)
                {
                    lastContentControl = null;
                    LastUIElement = null;
                }
                //unsets the selector property
                cc.SetValue(Selector.IsSelectedProperty, false);

                //makes the control unmovible again
                uie.IsHitTestVisible = true;
            }
            else
            {
                //if the last seleted item is set  run the following
                if (lastContentControl != null && LastUIElement != null)
                {
                    //unselect the previous control
                    lastContentControl.SetValue(Selector.IsSelectedProperty, false);
                    //make it unmovible
                    LastUIElement.IsHitTestVisible = true;
                }
                //set the current control as selected
                cc.SetValue(Selector.IsSelectedProperty, true);
                //make it movible and the inner control as unuseible
                uie.IsHitTestVisible = false;
                //set the previous control and container
                lastContentControl = cc;
                LastUIElement = uie;
            }


        }

        private void GetTextProperties(TextSelection ts)
        {
            var size = ts.GetPropertyValue(Inline.FontSizeProperty);
            if (size != DependencyProperty.UnsetValue)
            {
                fontsize.Text = size.ToString();
            }


            var font = ts.GetPropertyValue(Inline.FontFamilyProperty);
            if (font != DependencyProperty.UnsetValue)
            {
                Font.SelectedIndex = Font.Items.IndexOf(font);
            }

            var bold = ts.GetPropertyValue(Inline.FontWeightProperty);
            if (bold != DependencyProperty.UnsetValue)
            {
                if ((FontWeight)bold == FontWeights.Bold)
                {
                    btnBold.IsChecked = true;
                }
                else
                {
                    btnBold.IsChecked = false;
                }
            }

            var td = ts.GetPropertyValue(Inline.TextDecorationsProperty);
            if (td != DependencyProperty.UnsetValue && td is TextDecorationCollection)
            {
                foreach (TextDecoration tdec in (TextDecorationCollection)td)
                {

                        if (tdec == TextDecorations.Underline[0])
                        btnUnderLine.IsChecked = true;
                        else
                            btnUnderLine.IsChecked = false ;
                }

            }
            else
                btnUnderLine.IsChecked = false;

            var italic = ts.GetPropertyValue(Inline.FontStyleProperty);
            if (italic != DependencyProperty.UnsetValue)
            {
                if ((FontStyle)italic == FontStyles.Italic)
                {
                    btnItalic.IsChecked = true;
                }
                else
                {
                    btnItalic.IsChecked = false;
                }
            }

            var colorValue = ts.GetPropertyValue(Inline.ForegroundProperty);
            if (colorValue != DependencyProperty.UnsetValue)
            {
                SolidColorBrush b = (SolidColorBrush)colorValue;
                color.SelectedColor = b.Color;
            }    

        }

        /// <summary>
        /// Save book method
        /// 
        /// shows a safe file dialogue then activates the save book classes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "YearBookMaker Document (*.ybm) | *.ybm";
            if ((bool)sfd.ShowDialog())
            {
                current.SaveBook(new Uri(sfd.FileName).LocalPath);
                statuspanel.Background = Brushes.LightBlue;
                lbxstatus.Content = sfd.SafeFileName +" Saved at: " + DateTime.Now.ToShortTimeString();
            }
        }

        /// <summary>
        /// Print page method
        /// 
        /// show a print page dialogue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Click(object sender, RoutedEventArgs e)
        {

            SetBackGroundInvisible();
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            //prints the canvas
            { pd.PrintVisual(DesignerCanvas, "my canvas");
            statuspanel.Background = Brushes.LightBlue;
            lbxstatus.Content = "Printed at: " + DateTime.Now.ToShortTimeString();
            }
            SetBackGroundVisible();

        }

            public void SetBackGroundVisible()
        {
            borderBleed.Visibility = Visibility.Visible;
            borderUnsafe.Visibility = Visibility.Visible;
            bordersafe.Visibility = Visibility.Visible;
            borderBleed.Visibility = Visibility.Visible;
        }

        public void SetBackGroundInvisible()
        {
            borderBleed.Visibility = Visibility.Hidden;
            borderUnsafe.Visibility = Visibility.Hidden;
            bordersafe.Visibility = Visibility.Hidden;
            borderBleed.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Load page Click method
        /// Shows a open file dialogue to select the files you wish to open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "YearBookMaker Document (*.ybm) | *.ybm";
            if ((bool)ofd.ShowDialog())
            {
                current.OpenBook(new Uri(ofd.FileName).LocalPath);
                statuspanel.Background = Brushes.LightBlue;
                lbxstatus.Content = ofd.SafeFileName+"Loaded";
            }
        }




        #region zoomchange

        /// <summary>
        /// ZoomIn method
        /// by a factor of 0.1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomIn(object sender, RoutedEventArgs e)
        {
            //increments by 0.1 m means decimal
            zoom += 0.1m;
            //increase temp by 10 to the power of the zoom factor
            double temp = Math.Pow(10, (double)zoom);
            //fires off the update zoom controls method
            UpdateZoomControls(temp);
        }

        /// <summary>
        /// Click event for zooming out
        /// by a factor of 0.1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomOut(object sender, RoutedEventArgs e)
        {
            //decrements by 0.1 m means decimal
            zoom -= 0.1M;
            //increase temp by 10 to the power of the zoom factor
            double temp = Math.Pow(10, (double)zoom);
            //fires off the update zoom controls method
            UpdateZoomControls(temp);
        }

        /// <summary>
        /// Method for showing the changes to the zoombox and zoom indicator
        /// </summary>
        /// <param name="temp">zoom factor in percentage i.e. 142.2% = 142.2</param>
        private void UpdateZoomControls(double temp)
        {
            //gets a decimal representation of the percentage
            temp = temp / 100;
            //sets the text as the percentage with percentage formatting
            tbxZoom.Text = temp.ToString("p");
            //updates the zoom box
            DesignerCanvasZoomBox.Width = (int)(temp * PaperSize.Pixel.BleedWidth);
            DesignerCanvasZoomBox.Height = (int)(temp * PaperSize.Pixel.BleedHeight);
        }

        /// <summary>
        /// if the box is focused it will parse the new text and adjust the zoom
        /// </summary>
        private void ZoomChange()
        {
            //removes the formatting from the text and trims the white space
            string parsestring = tbxZoom.Text.Replace('%', ' ').Trim();
            double temp;

            //if it is focused correctly it will attempt to pharse the string otherwise it will fail and wipe the 
            //origonal imput
            if (tbxZoom.IsFocused && double.TryParse(parsestring, out temp))
            {
                //sets the zoom factor to the log of temp 
                //i.e 100% = 2 10% = 1
                zoom = (decimal)Math.Log10(temp);
                //prepares the origonal imput to be reformatted
                UpdateZoomControls(temp);
            }
            else
            {
                //reseting the text input
                temp = Math.Pow(10, (double)zoom);
                temp = temp / 100;
                tbxZoom.Text = temp.ToString("p");
            }
        }

        /// <summary>
        /// A override method for where the focus is just being skipped
        /// </summary>
        /// <param name="skipFocus">True to activate the method otherwise will just reset the textbox back to orgional</param>
        private void ZoomChange(bool skipFocus)
        {
            //removes the formatting from the text and trims the white space
            string parsestring = tbxZoom.Text.Replace('%', ' ').Trim();
            //general purpose double var
            double temp;

            //if it is trigger correctly it will attempt to pharse the string otherwise it will fail and wipe the 
            //origonal imput
            if (skipFocus && double.TryParse(parsestring, out temp))
            {
                //sets the zoom factor to the log of temp 
                //i.e 100% = 2 10% = 1
                zoom = (decimal)Math.Log10(temp);
                //prepares the origonal imput to be reformatted
                temp = temp / 100;
                tbxZoom.Text = (temp).ToString("p");

                //chanes the dimensions of the zoom box
                DesignerCanvasZoomBox.Width = (int)(temp * PaperSize.Pixel.BleedWidth);
                DesignerCanvasZoomBox.Height = (int)(temp * PaperSize.Pixel.BleedHeight);
            }
            else
            {
                //reseting the text input
                temp = Math.Pow(10, (double)zoom);
                temp = temp / 100;
                tbxZoom.Text = temp.ToString("p");
            }
        }
        #endregion

        /// <summary>
        /// Focus lost event for adjusting the zoom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxZoom_LostFocus_1(object sender, RoutedEventArgs e)
        {
            ZoomChange(true);
        }

        /// <summary>
        /// Click event to show the crop images form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _CropImage_Click_1(object sender, RoutedEventArgs e)
        {
            //grabs the selected document
            UIElement u = SeekSelection();
            //if the selected element is an image control
            if (u is Image)
            {
                Image img = u as Image;
                //opens the new form with the bitmapsource from the image
                PhotoEditor croper = new PhotoEditor((BitmapSource)img.Source);
                croper.ShowDialog();
                //changes the source to the cropped image
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
            if (u is Shape)
            {

                Shape shape = u as Shape;
                //sets the brush as a solid color brush and changes it to the new value
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

        /// <summary>
        /// new page handler
        /// 
        /// adds a new page for the backpage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewPageClick(object sender, RoutedEventArgs e)
        {
            //requests a new page from the bookviewer
            current.NewPage();
            //adjusts navigation bar
            lblTotalPages.Content = "/ " + current.CurrentBook.Pages.Count.ToString();
            tbxPageIndex.Text = (current.ViewIndex + 1).ToString();
        }

        /// <summary>
        /// Delete page handler
        /// 
        /// it deletes pages from the current book
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletePageClick(object sender, RoutedEventArgs e)
        {
            //requests to delete the page
            current.DeletePage();
            //adjusts navigation bar
            lblTotalPages.Content = "/ " + current.CurrentBook.Pages.Count.ToString();
            tbxPageIndex.Text = (current.ViewIndex + 1).ToString();
        }

        /// <summary>
        /// Navigates to the next page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextPageClick(object sender, RoutedEventArgs e)
        {
            //if not at the end of the book switch to the next page
            if (current.ViewIndex != current.CurrentBook.Pages.Count - 1)
            {
                current.ViewIndex++;
                tbxPageIndex.Text = (current.ViewIndex + 1).ToString();
            }
        }

        /// <summary>
        /// Navigates to the previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousPageClick(object sender, RoutedEventArgs e)
        {
            //drops the view index down and loads the previous page
            if (current.ViewIndex != 0)
            {
                current.ViewIndex--;
                tbxPageIndex.Text = (current.ViewIndex + 1).ToString();
            }
        }

        /// <summary>
        /// Change Page Number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxPageJump_LostFocus(object sender, RoutedEventArgs e)
        {
            //just jumps to the selected page
            int index;

            //if it can parse take the number out otherwise reset text
            if (int.TryParse(tbxPageIndex.Text, out index))
            {
                //if it is alread on that view index cancel the event
                if (current.ViewIndex == index - 1)
                {
                    e.Handled = true;
                }
                //if the index is within the view range set the current page to the desired page
                else if (index > 0 && index <= current.CurrentBook.Pages.Count)
                {
                    current.ViewIndex = index - 1;
                }
                //else reset text
                else
                {
                    tbxPageIndex.Text = (current.ViewIndex + 1).ToString();
                }
            }
            else
            {
                tbxPageIndex.Text = (current.ViewIndex + 1).ToString();
            }
        }

        /// <summary>
        /// Advanced Font Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdvancedFontClick(object sender, RoutedEventArgs e)
        {
            //opens up the advanced formatting form with the selected text selection
            FontStyleForm fs = new FontStyleForm(new RichTextBox().Selection);
            fs.ShowDialog();
        }

        /// <summary>
        /// resets the focus on pressing enter while in tbxpage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxPageIndex_Enter(object sender, KeyEventArgs e)
        {
            //if enter or return key
            if (e.Key == Key.Enter)
            {
                //remove all focus
                FocusManager.SetFocusedElement(this, null);
            }
        }

        /// <summary>
        /// After the actual form has initilized run this event, mainly here for debuging purposes as the main form
        /// constructor wont show exceptions in visual studio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Initialized_1(object sender, EventArgs e)
        {

            //initilizes the book viewer for a new book and sets up the page navigation
            Book b = new Book();
            b.SetAsStarterBook();
            current = new BookViewer(DesignerCanvas, this, b);
            lblTotalPages.Content = "/ " + current.CurrentBook.Pages.Count.ToString();
            tbxPageIndex.Text = (current.ViewIndex + 1).ToString();
        }

        /// <summary>
        /// This button click just runs another button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Font_Dialogue_Click(sender, e);
        }

        /// <summary>
        /// Button Click for publishing a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebPublish_Click_1(object sender, RoutedEventArgs e)
        {
            //legacey code for making the view folder work
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult dr = fbd.ShowDialog();
            if (dr.ToString() == "OK")
            {
                WebPublisher.PublishBook(current, fbd.SelectedPath,this);
            }
        }

        private void new_Click(object sender, RoutedEventArgs e)
        {
            statuspanel.Background = Brushes.LightGoldenrodYellow;
            lbxstatus.Content = "New Yearbook Created";
        }


        private void acknowlegements(object sender, RoutedEventArgs e)
        {
            //opps totally forgot who to reference for this
            MessageBox.Show("Thanks to all the provides of the icons","Thanks");
        }

        /// <summary>
        /// Text the properties of the selected text
        /// and prevents any changing of other font
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextSelectionChanged(object sender, RoutedEventArgs e)
        {
            justdetecting = true;
            GetTextProperties(((RichTextBox)sender).Selection);
            justdetecting = false;
        }

        /// <summary>
        /// Changes the color of the text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontColorChange(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            //checks if the color is just being detected rather than changed
            if (!justdetecting)
            {
                //gets the selected richtextbox
                RichTextBox rtb = SeekFocus();
                //sets the brush as a solid color brush and changes it to the new value
                SolidColorBrush b = new SolidColorBrush(e.NewValue);
                rtb.Selection.ApplyPropertyValue(Inline.ForegroundProperty, b);
            }
        }

        private void EnterdownChangeFontSize(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RichTextBox rtb = SeekFocus();
                double fontSize;
                if (double.TryParse(fontsize.Text, out fontSize))
                    rtb.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSize);
            }
        }

        private void fontsizetextchanged(object sender, RoutedEventArgs e)
        {
            if (!justdetecting)
            {
                RichTextBox rtb = SeekFocus();
                double fontSize;
                if (double.TryParse(fontsize.Text, out fontSize))
                    rtb.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSize);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"\chm\Yearbook Maker Help.chm");
        }
    }
}
