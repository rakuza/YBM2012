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
using YBMForms.DLL;


namespace YBMForms
{
    partial class MainWindow
    {
        /// <summary>
        /// bold
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            RichTextBox rtb = SeekFocus();
            if (rtb != null)
            {
                if (rtb.Selection.GetPropertyValue(Run.FontWeightProperty) is FontWeight && ((FontWeight)rtb.Selection.GetPropertyValue(Run.FontWeightProperty)) == FontWeights.Normal)
                {
                    rtb.Selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Bold);
                }
                else
                {
                    rtb.Selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
                }
            }
        }

        /// <summary>
        /// underline
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            RichTextBox rtb = SeekFocus();
            if (rtb != null)
            {
                var value = rtb.Selection.GetPropertyValue(Run.TextDecorationsProperty);

                //This if statement exists just to catch the an unset collection or a collection that doesnt match
                //e.g. if you have a mix or underlined or not it returns DP.unset
                if (value == null || value == DependencyProperty.UnsetValue)
                {
                    rtb.Selection.ApplyPropertyValue(Run.TextDecorationsProperty, TextDecorations.Underline);
                }
                else if (value is TextDecorationCollection)
                {
                    if (((TextDecorationCollection)value).Count == 0)
                    {
                        rtb.Selection.ApplyPropertyValue(Run.TextDecorationsProperty, TextDecorations.Underline);
                    }
                    else
                    {
                        rtb.Selection.ApplyPropertyValue(Run.TextDecorationsProperty, null);
                    }
                }
            }

        }

        /// <summary>
        /// font
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Font_Dialogue_Click(object sender, RoutedEventArgs e)
        {
            RichTextBox rtb = SeekFocus();
            if (rtb != null)
            {
                FontStyleForm fs = new FontStyleForm(rtb.Selection);
                fs.ShowDialog();
            }


        }

        /// <summary>
        /// italics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            RichTextBox rtb = SeekFocus();
            if (rtb != null)
            {
                if (rtb.Selection.GetPropertyValue(Run.FontStyleProperty) is FontStyle && ((FontStyle)rtb.Selection.GetPropertyValue(Run.FontStyleProperty)) == FontStyles.Normal)
                    rtb.Selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Italic);
                else
                   rtb.Selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);
            }
        }

        /// <summary>
        /// Grabs the control with foxus
        /// </summary>
        /// <returns></returns>
        private RichTextBox SeekFocus()
        {
            RichTextBox rtb = new RichTextBox(); ;

            foreach (ContentControl cc in DesignerCanvas.Children)
            {
                UIElement u = cc.Content as UIElement;
                if (u is RichTextBox && u.IsFocused)
                {

                    rtb = cc.Content as RichTextBox;
                }
            }

            return rtb;
        }

    }
}
