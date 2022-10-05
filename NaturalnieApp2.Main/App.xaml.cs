namespace NaturalnieApp2.Main
{
    using Microsoft.Extensions.DependencyInjection;
    using NaturalnieApp2.Logger;
    using NaturalnieApp2.Main.Exceptions;
    using NaturalnieApp2.Main.MVVM.Models.MenuGeneral;
    using NaturalnieApp2.Main.MVVM.ViewModels;
    using NaturalnieApp2.Main.MVVM.ViewModels.Product;
    using NaturalnieApp2.Main.Sandbox;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter;
    using NaturalnieApp2.SharedControls.Services.DialogBoxService;
    using NaturalnieApp2.SharedControls.Services.ModelPresenter;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using NaturalnieApp2.SharedInterfaces.Logger;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
   
    public partial class App : Application
    {
        public App()
        {
            Sandbox1 sandbox = new();
            sandbox.Play();

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
            mainWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            mainWindow.Left = SystemParameters.PrimaryScreenWidth - mainWindow.Width;
            mainWindow.Top = SystemParameters.PrimaryScreenHeight - mainWindow.Height;

            mainWindow.Show();

            ;
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            #region Logger
            // Logger
            services.AddSingleton<ILogger>((s) =>
            {
                return new Logger();
            });
            #endregion

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
                showProductViewModel.ModelToPropertyPresenterConverter = s.GetRequiredService<IModelToPropertyPresenterConverter>();

                return showProductViewModel;
            });

            #endregion

            #region Model converter services
            services.AddTransient<PropertyPresenterTextBoxViewModel>();

            services.AddTransient<IModelToPropertyPresenterConverter>((s) =>
            {
                return new ModelToPropertyPresenterConverter 
                { 
                    DefaultPropertyPresenter = s.GetRequiredService<PropertyPresenterTextBoxViewModel>() 
                };
            });
            #endregion

            // Exception
            services.AddSingleton<NaturalnieExceptionBase>();

            // Screen dialog box
            services.AddTransient((s) =>
            {
                return new DialogBoxService(s.GetRequiredService<ILogger>());
            });


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
