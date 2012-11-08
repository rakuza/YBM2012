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
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            foreach (ContentControl cc in DesignerCanvas.Children)
            {
                UIElement u = cc.Content as UIElement;
                if (u.IsFocused && u.GetType().ToString() == "System.Windows.Controls.RichTextBox")
                {
                    RichTextBox rtb = u as RichTextBox;
                    rtb.Selection.ApplyPropertyValue(RichTextBox.FontWeightProperty, "Bold");
                }
            }
        }

        /// <summary>
        /// underline
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            foreach (ContentControl cc in DesignerCanvas.Children)
            {
                UIElement u = cc.Content as UIElement;
                if (u.IsFocused && u.GetType().ToString() == "System.Windows.Controls.RichTextBox")
                {
                    RichTextBox rtb = u as RichTextBox;
                    //rtb.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty,TextDecorations.Underline);
                    rtb.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.OverLine);
                }
            }
        }

        /// <summary>
        /// font
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            foreach (ContentControl cc in DesignerCanvas.Children)
            {
                UIElement u = cc.Content as UIElement;
                if (u.IsFocused && u.GetType().ToString() == "System.Windows.Controls.RichTextBox")
                {
                    
                    
                    RichTextBox rtb = u as RichTextBox;
                    FontStyle fs = new FontStyle(rtb.Selection);
                    fs.ShowDialog();
                    //rtb.Selection.ApplyPropertyValue(RichTextBox.FontWeightProperty, "Bold");
                     
                }
            }

           // FontStyle fs = new FontStyle();
           // fs.ShowDialog();
        }

        /// <summary>
        /// italics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (ContentControl cc in DesignerCanvas.Children)
            {
                UIElement u = cc.Content as UIElement;
                if (u.IsFocused && u.GetType().ToString() == "System.Windows.Controls.RichTextBox")
                {
                    RichTextBox rtb = u as RichTextBox;
                    rtb.Selection.ApplyPropertyValue(RichTextBox.FontStyleProperty, "Italic");
                }
            }
        }

        private void BasicEffectHandler(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            switch (checkbox.Name)
            {
                case "chkStrikeThrough":
                    //do strikethrough method
                    break;

                case "chkOverline":
                    //do overline method
                    break;

                case "chkUnderLine":
                    //do overline method
                    break;

                case "chkBold":
                    //do bold method
                    break;

                case "chkItalic":
                    //do italic
                    break;

                default:
                    break;
            }
        }
    }
}
