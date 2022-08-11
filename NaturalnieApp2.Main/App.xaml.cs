namespace NaturalnieApp2.Main
{
    using NaturalnieApp2.Main.MVVM.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            NaturalnieApp2.Main.MainWindow mainWindow = new NaturalnieApp2.Main.MainWindow(new MainViewModel());
            mainWindow.Show();  
        }
    }


}
