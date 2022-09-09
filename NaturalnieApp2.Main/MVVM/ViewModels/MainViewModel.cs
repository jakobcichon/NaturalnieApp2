namespace NaturalnieApp2.Main.MVVM.ViewModels
{
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.Main.MVVM.Models.MenuGeneral;
    using NaturalnieApp2.Main.MVVM.ViewModels.MenuGeneral;
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.Menu;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;


    internal class MainViewModel : BaseViewModel
    {
        #region Constructor
        public MainViewModel(MenuBarModel? menuBarModel)
        {
            MenuBarModel = menuBarModel;
            MenuBar = new MenuBarViewModel();

            LogoButtonPressed = new(OnLogoButtonPressed, null);

            CreateMenuBarFromModel(MenuBarModel, MenuBar);

            changeScreenCancellationToken = new();
        }
        #endregion

        #region Fields
        private IMenuScreen? previouslySelectedScreen;
        private Task previouslySelectedLoadingTask;
        private CancellationTokenSource changeScreenCancellationToken;
        #endregion

        #region Properties
        public MenuBarViewModel MenuBar { get; init; }
        public MenuBarModel? MenuBarModel { get; init; }

        public CommandBase LogoButtonPressed { get; init; }

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
                selectedScreen = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Public methods

        #endregion

        #region Private methods

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
                return;
            }

            await ChangeActiveScreen(screen);

        }

        private IMenuScreen? GetMenuScreenByName(string name)
        {
            IMenuScreen? screen = MenuBarModel?.GetScreenByName(name);

            return screen;
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
