using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Controls;

namespace YBMForms.UIL
{
    static class FormatRTF
    {
        static public class Labels
        {
            static public void Italics(Label control,bool chked)
            {
                if (chked)
                {
                    control.FontStyle = FontStyles.Italic;
                }
                else
                {
                    control.FontStyle = FontStyles.Normal;
                }
            }

            static public void Bold(Label control, bool chked)
            {
                if (chked)
                {
                    control.FontWeight = FontWeights.Bold;
                }
                else
                {
                    control.FontWeight = FontWeights.Normal;
                }
            }

            static public void UnderLine(Label control, bool chked)
            {
                if (chked)
                {
                    TextBlock text = control.Content as TextBlock;
                    text.TextDecorations.Add(TextDecorations.Underline);
                }
                else
                {
                    TextBlock text = control.Content as TextBlock;
                    text.TextDecorations.Remove(TextDecorations.Underline[0]);
                }
            }

            static public void StrikeThrough(Label control, bool chked)
            {
                if (chked)
                {
                    TextBlock text = control.Content as TextBlock;
                    text.TextDecorations.Add(TextDecorations.Strikethrough);
                }
                else
                {
                    TextBlock text = control.Content as TextBlock;
                    text.TextDecorations.Remove(TextDecorations.Strikethrough[0]);
                }
            }
        }
    }
}
