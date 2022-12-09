global using static NaturalnieApp2.Main.StaticSettings.StaticSettings;

namespace NaturalnieApp2.Main
{
    using global::Main.MVVM.Models.GlobalSettingsModel.DatabaseSettings;
    using global::Main.MVVM.Models.GlobalSettingsModel.DatabaseSettings.DatabaseSettingsModel;
    using Microsoft.Extensions.DependencyInjection;
    using NaturalnieApp2.Database.Commands;
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.Logger;
    using NaturalnieApp2.Main.Exceptions;
    using NaturalnieApp2.Main.Interfaces.GlobalSettings;
    using NaturalnieApp2.Main.Interfaces.GlobalSettings.DatabaseSettings;
    using NaturalnieApp2.Main.MVVM.Models.GlobalSettingsModel;
    using NaturalnieApp2.Main.MVVM.Models.MenuGeneral;
    using NaturalnieApp2.Main.MVVM.Models.Product;
    using NaturalnieApp2.Main.MVVM.ViewModels;
    using NaturalnieApp2.Main.MVVM.ViewModels.Product;
    using NaturalnieApp2.Main.MVVM.ViewModels.SettingsMenu;
    using NaturalnieApp2.Main.Sandbox;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter;
    using NaturalnieApp2.SharedControls.Services.DialogBoxService;
    using NaturalnieApp2.SharedControls.Services.ModelPresenter;
    using NaturalnieApp2.SharedInterfaces.Database;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using NaturalnieApp2.SharedInterfaces.Logger;
    using NaturalnieApp2.SharedInterfaces.Xml;
    using NaturalnieApp2.XmlSerializer;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    using static NaturalnieApp2.Main.Services.ModelServices.NaturalnieAppPropertyPresenterService;

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
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();

            #region Logger
            ConfigureLogger(services);
            #endregion

            #region Main window
            ConfigureMainWindow(services);
            #endregion

            #region Menu screens
            ConfigureMenuScreens(services);
            #endregion

            #region Model converter services
            ConfigureModelConverters(services);
            #endregion

            #region Exceptions
            ConfigureExceptions(services);
            #endregion

            #region Dialog box
            ConfigureDialogBox(services);
            #endregion

            #region XmlSerializer
            ConfigureXmlSerializer(services);
            #endregion

            #region Global settings
            ConfigureGlobalSettings(services);
            #endregion

            #region Databse
            ConfigureDatabase(services);
            #endregion

            return services.BuildServiceProvider();
        }

        private static void ConfigureXmlSerializer(ServiceCollection services)
        {
            services.AddTransient<IXmlSerializer>((s) =>
            {
                return new XmlSerializer(AppDomain.CurrentDomain.BaseDirectory, s.GetRequiredService<ILogger>());
            });
        }

        private static void ConfigureExceptions(ServiceCollection services)
        {
            // Exception
            services.AddSingleton<NaturalnieExceptionBase>();
        }

        private static void ConfigureDialogBox(ServiceCollection services)
        {
            // Screen dialog box
            services.AddTransient((s) =>
            {
                return new DialogBoxService(s.GetRequiredService<ILogger>());
            });
        }

        private static void ConfigureModelConverters(ServiceCollection services)
        {
            services.AddSingleton<IModelToPropertyPresenterConverter>((s) =>
            {
                ModelToPropertyPresenterConverter converter = new();
                ConfigureConverter(converter);
                return converter;

            });

            services.AddTransient<IModelPresenter>((s) =>
            {
                return new ModelPresenterViewModel()
                {
                    ModelToPropertyPresenterConverter = s.GetService<IModelToPropertyPresenterConverter>()
                };
            });
        }

        private static void ConfigureMenuScreens(ServiceCollection services)
        {
            // Default screen
            services.AddSingleton<DefaulMenuScreenViewModel>();

            #region Product group
            // Product screen
            services.AddSingleton((s) =>
            {
                ShowProductViewModel showProductViewModel = new()
                {
                    DialogBox = s.GetService<DialogBoxService>(),
                    ModelPresenter = s.GetService<IModelPresenter>(),
                    ProductDatabaseCommands = s.GetService<IDatabaseGeneralCommands<ProductModel>>()
                };

                return showProductViewModel;
            });
            #endregion

            #region Settings
            // Database settings screen
            services.AddSingleton((s) =>
            {
                DatabaseSettingsViewModel databaseSettingsViewModel = new()
                {
                    XmlSerializer = s.GetService<IXmlSerializer>(),
                    ModelPresenter = s.GetService<IModelPresenter>(),
                    Model = s.GetRequiredService<IGlobalSettingsProvider>().DatabaseSettings,
                    DialogBox = s.GetService<DialogBoxService>(),
                };

                return databaseSettingsViewModel;
            });
            #endregion
        }

        private static void ConfigureMainWindow(ServiceCollection services)
        {
            services.AddSingleton((s) =>
            {
                return new MainViewModel(s.GetService<MenuBarModel>());
            });

            services.AddSingleton((s) =>
            {
                return CreateMenuScreens(s);
            });
        }

        private static void ConfigureLogger(ServiceCollection services)
        {
            // Logger
            services.AddSingleton<ILogger>((s) =>
            {
                return new Logger();
            });
        }

        private static void ConfigureGlobalSettings(ServiceCollection services)
        {
            ConfigureDatabaseSettings(services);

            // Global settings
            services.AddSingleton<IGlobalSettingsProvider>((s) =>
            {
                return new GlobalSettingsModel()
                {
                    DatabaseSettings = s.GetRequiredService<IDatabaseConnectionSettingsProvider>()
                };
            });
        }

        private static void ConfigureDatabase(ServiceCollection services)
        {
            services.AddTransient<IDatabaseGeneralCommands<ProductModel>>((s) =>
            {
                return new ProductCommands(s.GetRequiredService<IDatabaseConnectionSettingsProvider>().ConnectionString);
            });
        }

        private static void ConfigureDatabaseSettings(ServiceCollection services)
        {
            // Logger
            services.AddSingleton<IDatabaseConnectionSettingsProvider>((s) =>
            {
                return new DatabaseConnectionSettingsModel() 
                { 
                    ConnectionString = string.Format("server = {0}; port = 3306; database = shop; uid = admin; password = admin; Connection Timeout = 10", "localhost")
                };
            });
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

            // Settings menu
            groupName = "Ustawienia";
            menuBarModel.AddGroup(groupName);
            menuBarModel.AddScreen(groupName, "Ustawienia bazy danych", services.GetService<DatabaseSettingsViewModel>()!);



            return menuBarModel;
        }
    }


}
