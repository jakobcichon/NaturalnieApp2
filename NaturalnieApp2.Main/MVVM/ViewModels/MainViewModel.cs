namespace NaturalnieApp2.Main.MVVM.ViewModels
{
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.MenuButtons;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class MainViewModel : BaseViewModel
    {
        public ObservableCollection<MenuButtonViewModel> Buttons { get; init; }

        public MainViewModel()
        {
            Buttons = new ObservableCollection<MenuButtonViewModel>();
            MenuButtonViewModel button = new MenuButtonViewModel("Test button 1");
            button.ChildElements.Add(new MenuButtonViewModel("Test sub button 1"));
            Buttons.Add(button);
        }
    }
}
