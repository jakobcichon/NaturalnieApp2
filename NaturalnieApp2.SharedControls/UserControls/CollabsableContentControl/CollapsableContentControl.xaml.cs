namespace NaturalnieApp2.SharedControls.UserControls.CollabsableContentControl
{
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

    /// <summary>
    /// Interaction logic for CollapsableContentControl.xaml
    /// </summary>
    public partial class CollapsableContentControl : UserControl
    {
        public CollapsableContentControl()
        {
            InitializeComponent();
            IsCollapsed = false;
            RightColumnDef
        }


        public bool IsCollapsed
        {
            get { return (bool)GetValue(IsCollapsedProperty); }
            set { SetValue(IsCollapsedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCollapsed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCollapsedProperty =
            DependencyProperty.Register("IsCollapsed", typeof(bool), typeof(CollapsableContentControl), new PropertyMetadata(default));


        public FrameworkElement RightContent
        {
            get { return (FrameworkElement)GetValue(RightContentProperty); }
            set { SetValue(RightContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightContentProperty =
            DependencyProperty.Register("RightContent", typeof(FrameworkElement), typeof(CollapsableContentControl), new PropertyMetadata(default));

        public FrameworkElement LeftContent
        {
            get { return (FrameworkElement)GetValue(LeftContentProperty); }
            set { SetValue(LeftContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftContentProperty =
            DependencyProperty.Register("LeftContent", typeof(FrameworkElement), typeof(CollapsableContentControl), new PropertyMetadata(default));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsCollapsed)
            {
                IsCollapsed = false;
                return;
            }

            IsCollapsed = true;
        }
    }
}
