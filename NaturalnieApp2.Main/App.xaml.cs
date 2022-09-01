namespace NaturalnieApp2.Main
{
    using Microsoft.Extensions.DependencyInjection;
    using NaturalnieApp2.Logger;
    using NaturalnieApp2.Main.Exceptions;
    using NaturalnieApp2.Main.MVVM.Models.MenuGeneral;
    using NaturalnieApp2.Main.MVVM.ViewModels;
    using NaturalnieApp2.Main.MVVM.ViewModels.Product;
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

            NaturalnieExceptionBase.Logger = Services.GetService<Logger>();

            MainViewModel mainWindowContex = Services.GetService<MainViewModel>()!;

            MainWindow mainWindow = new(mainWindowContex);
            mainWindow.Show();
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Main kwindow
            services.AddSingleton<MainViewModel>((s) =>
            {
                return new MainViewModel(s.GetService<MenuBarModel>());
            });

            services.AddSingleton<MenuBarModel>((s) =>
            {
                return CreateMenuScreens(s);
            });

            #region Menu screens
            // Default screen
            services.AddSingleton<DefaulMenuScreenViewModel>();

            // Product screen
            services.AddSingleton<ShowProductViewModel>();

            #endregion

            // Logger
            services.AddSingleton<Logger>();

            // Exception
            services.AddSingleton<NaturalnieExceptionBase>();


            return services.BuildServiceProvider();
        }

        private static MenuBarModel CreateMenuScreens(IServiceProvider services)
        {
            MenuBarModel menuBarModel = new();

            // Default screen
            menuBarModel.AddDefaultScreen(services.GetService<DefaulMenuScreenViewModel>());

            // Product menu
            string groupName = "Menu produktu";
            menuBarModel.AddGroup(groupName);
            menuBarModel.AddScreen(groupName, "Informacje o produkcie", services.GetService<ShowProductViewModel>()!);

            // Stock menu
            groupName = "Menu magazyn";
            menuBarModel.AddGroup(groupName);
            menuBarModel.AddScreen(groupName, "Testowy przycisk", null);



            return menuBarModel;
        }
    }


}
