namespace NaturalnieApp2.Main.MVVM.ViewModels
{
    using Microsoft.Extensions.DependencyInjection;
    using NaturalnieApp2.Common.Barcode;
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.Main.MVVM.Models.MenuGeneral;
    using NaturalnieApp2.Main.MVVM.ViewModels.MenuGeneral;
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.Menu;
    using NaturalnieApp2.SharedInterfaces.Logger;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    internal class MainViewModel : BaseViewModel
    {
        #region Constructor
        public MainViewModel(MenuBarModel? menuBarModel, IServiceProvider serviceProvider)
        {
            MenuBarModel = menuBarModel;
            MenuBar = new MenuBarViewModel();

            LogoButtonPressed = new(OnLogoButtonPressed, null);
            MenuCollapseButtoPressed = new(OnMenuCollapseButtonPressed);

            CreateMenuBarFromModel(MenuBarModel, MenuBar);

            changeScreenCancellationToken = new();

            this.serviceProvider = serviceProvider;

            barcodeListner = new BarcodeListner(200, serviceProvider.GetRequiredService<ILogger>());
            barcodeListner.BarcodeValid += BarcodeListner_BarcodeValid;
        }
        #endregion

        #region Fields
        private IMenuScreen? previouslySelectedScreen;
        private Task previouslySelectedLoadingTask;
        private CancellationTokenSource changeScreenCancellationToken;
        private readonly IServiceProvider serviceProvider;
        private BarcodeListner barcodeListner;
        #endregion

        #region Properties
        public string AssemblyVersion { get; init; } = Assembly.GetExecutingAssembly().GetName().Version.ToString(); 
        public MenuBarViewModel MenuBar { get; init; }
        public MenuBarModel? MenuBarModel { get; init; }

        public CommandBase LogoButtonPressed { get; init; }
        public CommandBase MenuCollapseButtoPressed { get; init; }

        private IMenuScreen? defaultScreen;

        public IMenuScreen? DefaultScreen
        {
            get { return defaultScreen; }
            set
            {
                defaultScreen = value;
                if (value != null)
                {
                    SelectedScreen = value;
                }
            }
        }

        private IMenuScreen? selectedScreen;

        public IMenuScreen? SelectedScreen
        {
            get { return selectedScreen; }
            set
            {
                if(selectedScreen != null)
                {
                    selectedScreen.CloseRequestHandler -= OnScreenCloseRequest;
                }

                selectedScreen = value;
                OnPropertyChanged();

                if (selectedScreen != null)
                {
                    selectedScreen.CloseRequestHandler += OnScreenCloseRequest;
                }
            }
        }

        private bool isMenuCollapsed;

        public bool IsMenuCollapsed
        {
            get { return isMenuCollapsed; }
            set 
            { 
                isMenuCollapsed = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Public methods
        private void BarcodeListner_BarcodeValid(object sender, BarcodeListner.BarcodeValidEventArgs e)
        {
            IBarcodeListner? barcodeListner = SelectedScreen as IBarcodeListner;

            if( barcodeListner is null)
            {
                return;
            }

            barcodeListner.OnBarcodeScanned(e.RecognizedBarcodeValue);
        }

        public void OnKeyPressed(object sender, KeyEventArgs args)
        {
            barcodeListner.CheckIfBarcodeFromReader(args.Key);
        }
        #endregion

        #region Private methods
        private void OnScreenCloseRequest(object sender, EventArgs e)
        {
            CloseActiveScreen();
        }

        private void CreateMenuBarFromModel(MenuBarModel? menuBarModel, MenuBarViewModel menuBarViewModel)
        {
            if (menuBarModel == null)
            {
                return;
            }

            foreach(KeyValuePair<string, Dictionary<string, IMenuScreen>> menuScreensGroupsDict in menuBarModel.MenuScreenList)
            {
                MenuGroupViewModel group = new MenuGroupViewModel(menuScreensGroupsDict.Key);
                foreach(var menuScreensDict in menuScreensGroupsDict.Value)
                {
                    group.Buttons.Add(new MenuButtonViewModel(menuScreensDict.Key, OnMenuScreenButtonPressed));
                }
                menuBarViewModel.MenuGroupItems.Add(group);
            }
        }

        private async void OnLogoButtonPressed(object? parameters)
        {
            if(DefaultScreen != null)
            {
                await ChangeActiveScreen(DefaultScreen);
            }

        }

        private void OnMenuCollapseButtonPressed(object? parameters)
        {
            if (IsMenuCollapsed)
            {
                IsMenuCollapsed = false;
                return;
            }

            IsMenuCollapsed = true;

        }

        private async void OnMenuScreenButtonPressed(object? parameters)
        {
            if (parameters == null)
            {
                return;
            }

            if (parameters is not MenuButtonViewModel)
            {
                return;
            }
            
            MenuButtonViewModel? buttonViewModel = parameters as MenuButtonViewModel;

            if (buttonViewModel is null || buttonViewModel.DisplayName is null)
            {
                return;
            }

            IMenuScreen? screen = GetMenuScreenByName(buttonViewModel.DisplayName);
            if(screen is null)
            {
                SetMenuScreen(buttonViewModel.DisplayName, null);
            }

            await ChangeActiveScreen(screen);

        }

        private void CloseActiveScreen()
        {
            string? name = GetScreenNameByValue(SelectedScreen);
            if( name is null) 
            {
                return;
            }

            Type screenType = SelectedScreen.GetType().UnderlyingSystemType;
            IMenuScreen? newInstance = serviceProvider.GetService(screenType) as IMenuScreen;

            if (newInstance is null) 
            {
                return;
            }

            SelectedScreen.Dispose();
            SetMenuScreen(name, newInstance);

            ChangeActiveScreen(DefaultScreen);
        }

        private IMenuScreen? GetMenuScreenByName(string name)
        {
            IMenuScreen? screen = MenuBarModel?.GetScreenByName(name);

            return screen;
        }
        private string? GetScreenNameByValue(IMenuScreen screen)
        {
            return MenuBarModel?.GetScreenNameByValue(screen);
        }

        private void SetMenuScreen(string name, IMenuScreen screen)
        {
            MenuBarModel?.SetScreenByName(name, screen);
        }

        private async Task ChangeActiveScreen(IMenuScreen newScreen)
        {
            // Get previously selected screen first
            if(SelectedScreen != null && SelectedScreen.GetType() != typeof(MenuScreenLoadingViewModel))
            {
                previouslySelectedScreen = SelectedScreen;
            }
             
            // Activate temporary screen
            SelectedScreen = new MenuScreenLoadingViewModel(newScreen.ScreenInfo);
            if (previouslySelectedLoadingTask != null && !previouslySelectedLoadingTask.IsCompleted)
            {
                changeScreenCancellationToken.Cancel();
                Thread.Sleep(100);
                changeScreenCancellationToken.Dispose();
                changeScreenCancellationToken = new();
            }

            previouslySelectedLoadingTask = newScreen.LoadAsync().WaitAsync(changeScreenCancellationToken.Token);

            try
            {
                await previouslySelectedLoadingTask;

                // Assigne new screen
                SelectedScreen = newScreen;
            }
            catch (TaskCanceledException)
            {
                // This was made to cancel old screen and do not show the new one
            }
        }
        #endregion


    }
}
