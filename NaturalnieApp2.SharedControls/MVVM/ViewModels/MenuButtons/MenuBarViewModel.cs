namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.MenuButtons
{
    using System.Collections.ObjectModel;

    public class MenuBarViewModel: BaseViewModel
    {
        public string? DisplayName { get; init; }
        public ObservableCollection<MenuButtonViewModel> MenuBarItems { get; init; }

        public MenuBarViewModel()
        {
            MenuBarItems = new ObservableCollection<MenuButtonViewModel>();
        }

    }
}
