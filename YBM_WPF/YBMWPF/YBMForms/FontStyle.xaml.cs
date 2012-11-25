using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using YBMForms.UIL;

namespace YBMForms
{
    //Bug:Advance form will strip off any values that have more than one amount
    //fix:none



    /// <summary>
    /// Interaction logic for FontStyle.xaml
    /// </summary>
    public partial class FontStyleForm : Window
    {
        public FontStyleForm(TextSelection t)
        {

            InitializeComponent();
            GetTextProperties(t);
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

        private void Submit(object sender, RoutedEventArgs e)
        {
            text.ClearAllProperties();
            text.ApplyPropertyValue(TextElement.FontFamilyProperty, lblDemo.FontFamily);
            text.ApplyPropertyValue(TextElement.FontSizeProperty, lblDemo.FontSize);
            text.ApplyPropertyValue(Inline.TextDecorationsProperty, ((TextBlock)lblDemo.Content).TextDecorations);
            text.ApplyPropertyValue(TextElement.FontWeightProperty, lblDemo.FontWeight);
            text.ApplyPropertyValue(TextElement.ForegroundProperty, ((TextBlock)lblDemo.Content).Foreground);
            this.Close();
        }

        private void GetTextProperties(TextSelection ts)
        {
            var size = ts.GetPropertyValue(Inline.FontSizeProperty);
            if (size != DependencyProperty.UnsetValue)
            {
                tbxSize.Text = size.ToString();
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
                    chkBold.IsChecked = true;
                }
            }

            var td = ts.GetPropertyValue(Inline.TextDecorationsProperty);
            if (td != DependencyProperty.UnsetValue && td is TextDecorationCollection)
            {
                foreach (TextDecoration tdec in (TextDecorationCollection)td)
                {
                    if (tdec == TextDecorations.Strikethrough[0])
                        chkStrikeThrough.IsChecked = true;
                    if (tdec == TextDecorations.Underline[0])
                        chkUnderLine.IsChecked = true;
                }

            }

            var italic = ts.GetPropertyValue(Inline.FontStyleProperty);
            if (italic != DependencyProperty.UnsetValue)
            {
                if ((FontStyle)italic == FontStyles.Italic)
                {
                    chkItalic.IsChecked = true;
                }
            }

            var colorValue = ts.GetPropertyValue(Inline.ForegroundProperty);
            if (colorValue != DependencyProperty.UnsetValue)
            {
                SolidColorBrush b = (SolidColorBrush)colorValue;
                color.SelectedColor = b.Color;
            }
        }

        public void Exit(object sender, RoutedEventArgs e)
        {

            this.Close();
        }


    }
}
