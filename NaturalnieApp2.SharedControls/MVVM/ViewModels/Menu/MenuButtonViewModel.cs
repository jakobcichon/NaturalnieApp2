namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.Menu
{
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class MenuButtonViewModel : BaseViewModel
    {
        public string? DisplayName { get; init; }
        public CommandBase Command { get; init; }

        public MenuButtonViewModel(string displayName, 
            Action<object?>? execute = null, Predicate<object?>? canExecute = null)
        {
            DisplayName = displayName;
            Command = new CommandBase(execute, canExecute);
        }

    }
}
