using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private ContentControl lastContentControl;
        private UIElement LastUIElement;


        private void DoubleClickSelect(object sender, MouseButtonEventArgs e)
        {

            ContentControl cc = sender as ContentControl;
            UIElement uie = cc.Content as UIElement;

            if ((bool)cc.GetValue(Selector.IsSelectedProperty))
            {
                if (lastContentControl != null && LastUIElement != null)
                {
                    lastContentControl = null;
                    LastUIElement = null;
                }
                cc.SetValue(Selector.IsSelectedProperty, false);
                uie.IsHitTestVisible = true;
            }
            else
            {
                if (lastContentControl != null && LastUIElement != null)
                {
                    lastContentControl.SetValue(Selector.IsSelectedProperty, false);
                    LastUIElement.IsHitTestVisible = true;
                    lastContentControl = cc;
                    LastUIElement = uie;
                }
                cc.SetValue(Selector.IsSelectedProperty, true);
                uie.IsHitTestVisible = false;
                lastContentControl = cc;
                LastUIElement = uie;
            }


        }

    }
}
