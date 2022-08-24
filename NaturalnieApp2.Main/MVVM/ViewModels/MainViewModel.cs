namespace NaturalnieApp2.Main.MVVM.ViewModels
{
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.Menu;

    internal class MainViewModel : BaseViewModel
    {
        public MenuBarViewModel MenuBar { get; init; }

        public MainViewModel()
        {
            MenuBar = new MenuBarViewModel();
            MenuGroupViewModel group = new MenuGroupViewModel("Test group 1");
            group.Buttons.Add(new MenuButtonViewModel("Test sub button 1"));
            group.Buttons.Add(new MenuButtonViewModel("Test sub button 2"));
            MenuBar.MenuGroupItems.Add(group);

            group = new MenuGroupViewModel("Test group 2");
            group.Buttons.Add(new MenuButtonViewModel("Test sub button 1"));
            group.Buttons.Add(new MenuButtonViewModel("Test sub button 2"));
            group.Buttons.Add(new MenuButtonViewModel("Test sub button 3"));
            group.Buttons.Add(new MenuButtonViewModel("Test sub button 4"));
            MenuBar.MenuGroupItems.Add(group);
        }
    }
}
