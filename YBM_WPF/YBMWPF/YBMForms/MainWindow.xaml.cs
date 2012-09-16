﻿using System;
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

        private void SpawnControl(object sender, RoutedEventArgs e)
        {
            Control c = sender as Control;

            switch (c.Name)
            {
                case"_SpawnTextBox":
                    CreateTextBox();
                    break;

                case "_SpawnRect":
                    CreateRect();
                    break;

                default:
                    break;
                    

            }
        }

        private void CreateTextBox()
        {
            ContentControl cc = new ContentControl();
            RichTextBox rtb = new RichTextBox();

            rtb.IsHitTestVisible = true;
            cc.Content = rtb;
            cc.Style = (Style)FindResource("DesignerItemStyle");
            cc.Width = 300;
            cc.Height = 300;
            cc.Padding = new Thickness(3);
            cc.MouseDoubleClick += new MouseButtonEventHandler(DoubleClickSelect);
            cc.ClipToBounds = true;
            DesignerCanvas.Children.Add(cc);
            Canvas.SetLeft(cc, 0);
            Canvas.SetTop(cc, 0);
        }

        private void CreateRect()
        {
            ContentControl cc = new ContentControl();
            Rectangle r = new Rectangle();
            r.IsHitTestVisible = true;
            r.Fill = Brushes.LightCoral;
            r.RenderSize = new Size(100, 100);
            cc.Content = r;
            cc.Style = (Style)FindResource("DesignerItemStyle");
            cc.Width = 300;
            cc.Height = 300;
            cc.Padding = new Thickness(3);
            cc.MouseDoubleClick += new MouseButtonEventHandler(DoubleClickSelect);
            cc.ClipToBounds = true;
            DesignerCanvas.Children.Add(cc);
            Canvas.SetLeft(cc, 0);
            Canvas.SetTop(cc, 0);
        }

    }
}