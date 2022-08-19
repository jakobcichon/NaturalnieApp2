namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.MenuButtons
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class MenuButtonViewModel : BaseViewModel
    {
        public string? DisplayName { get; init; }
        public ICommand? Command { get; set; }

        public MenuButtonViewModel(string displayName)
        {
            DisplayName = displayName;
        }

    }
}
