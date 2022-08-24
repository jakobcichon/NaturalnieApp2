namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.Menu
{
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;

    public class MenuGroupViewModel: BaseViewModel
    {
        public string? DisplayName { get; init; }
        public ObservableCollection<MenuButtonViewModel> Buttons { get; init; }
        public CommandBase CommandMainButton { get; init; }

        private Visibility groupShown;
        public Visibility GroupShown 
        { 
            get { return groupShown; }
            set 
            {
                groupShown = value;
                OnPropertyChanged();
            }
        }

        public MenuGroupViewModel(string displayName)
        {
            Buttons = new ObservableCollection<MenuButtonViewModel>();
            DisplayName = displayName;
            Action<object?> mainButtonClickAction = new Action<object?>(OnMainButtonClick);

            CommandMainButton = new CommandBase(OnMainButtonClick, CanExecute);

            this.GroupShown = Visibility.Collapsed;
        }

        private bool CanExecute(object? parameter)
        {
            return true;
        }

        private void OnMainButtonClick(object? paramter)
        {
            if (GroupShown == Visibility.Visible)
            {
                GroupShown = Visibility.Collapsed;
                return;
            }

            GroupShown = Visibility.Visible;
        }
    }
}
