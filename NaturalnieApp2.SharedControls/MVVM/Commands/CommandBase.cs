namespace NaturalnieApp2.SharedControls.MVVM.Commands
{
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Action<object?>? execute;
        private readonly Predicate<object?>? canExecute;

        public CommandBase(Action<object?>? execute, Predicate<object?>? canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public CommandBase(Action<object?>? execute)
        {
            this.execute = execute;
            this.canExecute = null;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute != null ? canExecute(parameter) : true;
        }

        public void Execute(object? parameter)
        {
            execute?.Invoke(parameter);
        }

        public void OnCanExecuteChange()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }


    }
}
