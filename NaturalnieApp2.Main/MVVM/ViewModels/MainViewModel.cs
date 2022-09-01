namespace NaturalnieApp2.Main.MVVM.ViewModels
{
    using NaturalnieApp2.Main.Exceptions;
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.Main.MVVM.Models.MenuGeneral;
    using NaturalnieApp2.Main.MVVM.ViewModels.MenuGeneral;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.Menu;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    internal class MainViewModel : BaseViewModel
    {
        public MenuBarViewModel MenuBar { get; init; }
        public MenuBarModel? MenuBarModel { get; init; }

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

        public MainViewModel(MenuBarModel? menuBarModel)
        {
            MenuBarModel = menuBarModel;
            MenuBar = new MenuBarViewModel();

            SelectedScreen = MenuBarModel?.DefaultScreen;

            CreateMenuBarFromModel(MenuBarModel, MenuBar);
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

        private async void OnMenuScreenButtonPressed(object? parameters)
        {
            if (parameters == null)
            {
                return;
            }

            if (parameters is not MenuButtonViewModel button)
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

            SelectedScreen = new MenuScreenLoadingViewModel(screen.ScreenInfo);
            await screen.LoadAsync();

            SelectedScreen = screen;

        }

        private IMenuScreen? GetMenuScreenByName(string name)
        {
            IMenuScreen? screen = MenuBarModel?.GetScreenByName(name);

            return screen;
        }


    }
}
