namespace NaturalnieApp2.SharedControls.MVVM.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class CommandBase : ICommand
    {
        private readonly Action<object?>? execute;
        private readonly Predicate<object?>? canExecute;

        public CommandBase(Action<object?>? execute, Predicate<object?>? canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public event EventHandler? CanExecuteChanged;

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
