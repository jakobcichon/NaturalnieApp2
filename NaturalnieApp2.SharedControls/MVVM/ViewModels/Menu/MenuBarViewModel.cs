namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.Menu
{
    using System.Collections.ObjectModel;

    public class MenuBarViewModel: BaseViewModel
    {
        public ObservableCollection<MenuGroupViewModel> MenuGroupItems { get; init; }

        private static MenuGroupViewModel? lastSelectedGroup;
        private static MenuGroupViewModel? currentSelectedGroup;

        public MenuBarViewModel()
        {
            MenuGroupItems = new ObservableCollection<MenuGroupViewModel>();
        }

    }
}
