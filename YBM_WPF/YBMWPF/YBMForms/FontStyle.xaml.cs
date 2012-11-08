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
using System.Windows.Shapes;
using Xceed;
using Xceed.Wpf;
using Xceed.Wpf.Toolkit;
using YBMForms.UIL;

namespace YBMForms
{
    /// <summary>
    /// Interaction logic for FontStyle.xaml
    /// </summary>
    public partial class FontStyle : Window
    {
        public FontStyle(TextSelection t)
        {
            
            InitializeComponent();
            text = t;
        }

        private TextSelection text;

        private void BasicEffectHandler(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            switch (checkbox.Name)
            {
                case "chkStrikeThrough":
                    //do strikethrough method
                    FormatRTF.Labels.StrikeThrough(lblDemo, (bool)checkbox.IsChecked);
                    break;

                case "chkUnderLine":
                    //do overline method
                    FormatRTF.Labels.UnderLine(lblDemo, (bool)checkbox.IsChecked);
                    break;

                case "chkBold":
                    //do bold method
                    FormatRTF.Labels.Bold(lblDemo, (bool)checkbox.IsChecked);
                    break;

                case "chkItalic":
                    //do Italic method
                    FormatRTF.Labels.Italics(lblDemo, (bool)checkbox.IsChecked);
                    break;

                default:
                    break;
            }
        }

        private void ColorChangedHandler(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            TextBlock text = lblDemo.Content as TextBlock;
            text.Foreground = new SolidColorBrush(e.NewValue);
        }


        private void FontFamilyChangeHandler(object sender, SelectionChangedEventArgs e)
        {
            lblDemo.FontFamily = new FontFamilyConverter().ConvertFromString(Font.SelectedItem.ToString()) as FontFamily;
        }

        private void tbxSize_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            try
            {
                lblDemo.FontSize = double.Parse(tbxSize.Text);
            }
            catch (Exception)
            {
            }
        }

        public void Submit(object sender, RoutedEventArgs e)
        {
            text.ClearAllProperties();
            text.ApplyPropertyValue(TextElement.FontFamilyProperty,lblDemo.FontFamily);
            text.ApplyPropertyValue(TextElement.FontSizeProperty, lblDemo.FontSize);
            text.ApplyPropertyValue(Inline.TextDecorationsProperty, ((TextBlock)lblDemo.Content).TextDecorations);
            text.ApplyPropertyValue(TextElement.FontWeightProperty, lblDemo.FontWeight);
            text.ApplyPropertyValue(TextElement.ForegroundProperty, ((TextBlock)lblDemo.Content).Foreground);
            this.Close();
        }

        public void Exit(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        
    }
}
