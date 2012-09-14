using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace YBMForms.UIL.AdornerLib
{
    public class ResizeThumb : Thumb
    {
        public ResizeThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.ResizeThumb_DragDelta);
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Control item = this.DataContext as Control;

            if (item != null)
            {
                double deltaVerticle, deltaHorizontal;

                switch (VerticalAlignment)
                {
                    case VerticalAlignment.Bottom:
                        deltaVerticle = Math.Min(-e.VerticalChange, item.ActualHeight - item.MinHeight);
                        item.Height -= deltaVerticle;
                        break;
                    case VerticalAlignment.Top:
                        deltaVerticle = Math.Min(e.VerticalChange, item.ActualHeight - item.MinHeight);
                        Canvas.SetTop(item, Canvas.GetTop(item) + deltaVerticle);
                        item.Height -= deltaVerticle;
                        break;
                    default:
                        break;
                }

                switch (HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        deltaHorizontal = Math.Min(e.HorizontalChange, item.ActualWidth - item.MinWidth);
                        Canvas.SetLeft(item, Canvas.GetLeft(item) + deltaHorizontal);
                        item.Width -= deltaHorizontal;
                        break;
                    case HorizontalAlignment.Right:
                        deltaHorizontal = Math.Min(-e.HorizontalChange, item.ActualWidth - item.MinWidth);
                        item.Width -= deltaHorizontal;
                        break;
                    default:
                        break;
                }
            }

            e.Handled = true;

        }
    }


}
