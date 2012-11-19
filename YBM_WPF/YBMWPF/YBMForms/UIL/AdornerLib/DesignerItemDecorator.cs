
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;

namespace YBMForms.UIL.AdornerLib
{
    public class DesignerItemDecorator : Control
    {
        private Adorner adnorner;

        public bool ShowDecorator
        {
            get { return (bool)GetValue(ShowDecoratorProperty); }
            set { SetValue(ShowDecoratorProperty, value); }
        }

        public static readonly DependencyProperty ShowDecoratorProperty = DependencyProperty.Register("ShowDecorator", typeof(bool), typeof(DesignerItemDecorator),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ShowDecoratorProperty_Changed)));

        public DesignerItemDecorator()
        {
            Unloaded += new RoutedEventHandler(this.DesignerItemDecorator_Unloaded);
        }

        private void HideAdorner()
        {
            if (this.adnorner != null)
            {
                this.adnorner.Visibility = Visibility.Hidden;
            }
        }

        private void ShowAdorner()
        {
            if (this.adnorner == null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);

                if (adornerLayer != null)
                {
                    ContentControl designerItem = this.DataContext as ContentControl;
                    this.adnorner = new ResizeRotateAdorner(designerItem);
                    adornerLayer.Add(this.adnorner);

                    if (this.ShowDecorator)
                    {
                        this.adnorner.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.adnorner.Visibility = Visibility.Hidden;
                    }
                }
            }
            else
            {
                this.adnorner.Visibility = Visibility.Visible;
            }
        }

        private void DesignerItemDecorator_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.adnorner != null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(this.adnorner);
                }

                this.adnorner = null;

            }
        }

        private static void ShowDecoratorProperty_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DesignerItemDecorator decorator = (DesignerItemDecorator)sender;
            bool showDecorator = (bool)e.NewValue;

            if (showDecorator)
            {
                decorator.ShowAdorner();
            }
            else
            {
                decorator.HideAdorner();
            }
        }
    }
}
