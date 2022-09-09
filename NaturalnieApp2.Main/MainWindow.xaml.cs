namespace NaturalnieApp2.Main
{
    using NaturalnieApp2.Main.MVVM.ViewModels;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

        }
        public MainWindow(BaseViewModel context)
        {
            InitializeComponent();
            DataContext = context;
        }
    }
}
