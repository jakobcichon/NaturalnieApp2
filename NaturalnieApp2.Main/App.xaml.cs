namespace NaturalnieApp2.Main
{
    using Microsoft.Extensions.DependencyInjection;
    using NaturalnieApp2.Main.MVVM.ViewModels;
    using System;
    using System.Windows;

    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
        }

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mainWindow = new MainWindow(new MainViewModel());
            mainWindow.Show();  
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();


            return services.BuildServiceProvider();
        }
    }


}
