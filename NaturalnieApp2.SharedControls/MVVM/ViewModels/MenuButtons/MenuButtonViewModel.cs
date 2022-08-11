namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.MenuButtons
{
    using System.Collections.ObjectModel;

    public class MenuButtonViewModel : BaseViewModel
    {
        public string? DisplayName { get; init; }
        public ObservableCollection<MenuButtonViewModel> ChildElements { get; init; }

        public MenuButtonViewModel(string displayName)
        {
            DisplayName = displayName;
            ChildElements = new ObservableCollection<MenuButtonViewModel>();
        }

    }
}
