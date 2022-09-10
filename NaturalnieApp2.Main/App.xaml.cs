namespace NaturalnieApp2.Main
{
    using Microsoft.Extensions.DependencyInjection;
    using NaturalnieApp2.Logger;
    using NaturalnieApp2.Main.Exceptions;
    using NaturalnieApp2.Main.MVVM.Models.MenuGeneral;
    using NaturalnieApp2.Main.MVVM.ViewModels;
    using NaturalnieApp2.Main.MVVM.ViewModels.Product;
    using NaturalnieApp2.SharedControls.MVVM.Models.MessageBox;
    using NaturalnieApp2.SharedControls.Services.DialogBoxService;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Threading.Tasks;
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

            // Main window context
            MainViewModel mainWindowContex = Services.GetService<MainViewModel>()!;
            mainWindowContex.DefaultScreen = Services.GetService<DefaulMenuScreenViewModel>();

            MainWindow mainWindow = new(mainWindowContex);

            mainWindow.Show();

            ;
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Main window
            services.AddSingleton((s) =>
            {
                return new MainViewModel(s.GetService<MenuBarModel>());
            });

            services.AddSingleton((s) =>
            {
                return CreateMenuScreens(s);
            });

            #region Menu screens
            // Default screen
            services.AddSingleton<DefaulMenuScreenViewModel>();

            // Product screen
            services.AddSingleton((s) =>
            {
                ShowProductViewModel showProductViewModel = new();
                showProductViewModel.DialogBox = s.GetService<DialogBoxService>();
                return showProductViewModel;
            });

            #endregion

            // Logger
            services.AddSingleton<Logger>();

            // Exception
            services.AddSingleton<NaturalnieExceptionBase>();

            // Screen dialog box
            services.AddTransient<DialogBoxService>();


            return services.BuildServiceProvider();
        }

        private static MenuBarModel CreateMenuScreens(IServiceProvider services)
        {
            MenuBarModel menuBarModel = new();

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
