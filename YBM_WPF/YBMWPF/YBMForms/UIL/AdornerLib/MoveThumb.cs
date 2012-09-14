using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace YBMForms.UIL.AdornerLib
{
    public class MoveThumb : Thumb
    {
        public MoveThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
        }

        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            //magically connects this to content control?
            Control item = this.DataContext as Control;


            if (item != null)
            {
                //getting locations of the object we're moving
                double left = Canvas.GetLeft(item);
                double top = Canvas.GetTop(item);
                //setting the locations of the object offset by the changes
                Canvas.SetLeft(item, left + e.HorizontalChange);
                Canvas.SetTop(item, top + e.VerticalChange);
            }
        }
    }
}