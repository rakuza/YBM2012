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


namespace YBMForms
{
    partial class MainWindow
    {
        /// <summary>
        /// Form Key press hander, this is just a generic handler for short cut keys
        /// </summary>
        /// <param name="sender">This will be the main form</param>
        /// <param name="e">Key Event arguments</param>
        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {

            switch (e.Key)
                {
                    case Key.Delete:
                        Delete(e);
                        break;

                    case Key.Back:
                        Delete(e);
                        break;

                    case Key.Add:
                        ZIndex(1,e);
                        break;

                    case Key.Subtract:
                        ZIndex(-1,e);
                        break;

                    case Key.Enter:
                        ZoomChange();
                        break;

                    default:
                        break;
                }
            }
        
        /// <summary>
        /// Deletes Controls off the main Canvas
        /// </summary>
        /// <param name="e">Key Event arguments</param>
        private void Delete(KeyEventArgs e)
        {
            foreach (ContentControl cc in DesignerCanvas.Children)
            {
                //if its the selected control
                if (Selector.GetIsSelected(cc))
                {
                    //removes the content control box
                    DesignerCanvas.Children.Remove(cc);
                    //stops the event from being sent on to the richtextbox if its selected
                    e.Handled = true;
                    return;
                }
            }
        }

        /// <summary>
        /// Zindex handling method
        /// </summary>
        /// <param name="i">The quality to change the zindex by</param>
        /// <param name="e">Key Event arguments</param>
        private void ZIndex(int i, KeyEventArgs e)
        {
            foreach (ContentControl cc in DesignerCanvas.Children)
            {
                if (Selector.GetIsSelected(cc))
                {
                    //getting the zindex
                    int zindex = Canvas.GetZIndex(cc);
                    //modifing the zindex
                    zindex += i;
                    //setting the zindex again
                    Canvas.SetZIndex(cc, zindex);
                    //stops the event from being sent on to the richtextbox if its selected
                    e.Handled = true;
                    return;
                }
            }
        }
      


    }
}
