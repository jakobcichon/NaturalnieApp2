namespace NaturalnieApp2.Main
{
    using NaturalnieApp2.Main.MVVM.ViewModels;
    using System.Windows;

    public partial class App : Application
    {
        public App()
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mainWindow = new MainWindow(new MainViewModel());
            mainWindow.Show();  
        }
    }


}
