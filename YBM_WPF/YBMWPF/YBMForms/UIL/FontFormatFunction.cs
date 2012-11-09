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
            UIElement u = SeekSelection();
            if (u.IsFocused && u.GetType().ToString() == "System.Windows.Controls.RichTextBox")
            {
                RichTextBox rtb = u as RichTextBox;
                rtb.Selection.ApplyPropertyValue(RichTextBox.FontWeightProperty, "Bold");
            }
        }

        /// <summary>
        /// underline
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            UIElement u = SeekSelection();
            if (u.IsFocused && u.GetType().ToString() == "System.Windows.Controls.RichTextBox")
            {
                RichTextBox rtb = u as RichTextBox;
                //rtb.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty,TextDecorations.Underline);
                rtb.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.OverLine);
            }

        }

        /// <summary>
        /// font
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Font_Dialogue_Click(object sender, RoutedEventArgs e)
        {
            UIElement u = SeekSelection();
                if (u.IsFocused && u.GetType().ToString() == "System.Windows.Controls.RichTextBox")
                {
                    RichTextBox rtb = u as RichTextBox;
                    FontStyle fs = new FontStyle(rtb.Selection);
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
            UIElement u = SeekSelection();
            if (u.IsFocused && u.GetType().ToString() == "System.Windows.Controls.RichTextBox")
            {
                RichTextBox rtb = u as RichTextBox;
                rtb.Selection.ApplyPropertyValue(RichTextBox.FontStyleProperty, "Italic");
            }
        }

    }
}
