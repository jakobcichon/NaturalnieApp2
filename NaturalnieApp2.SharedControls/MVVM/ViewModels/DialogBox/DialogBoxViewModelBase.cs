namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox
{
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    public class DialogBoxViewModelBase: BaseViewModel
    {
        public event EventHandler<string> ButtonPressed;
        private string message = string.Empty;

        public string Message
        {
            get { return message; }
            set 
            {
                message = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnButtonPressed(object? parameters)
        {
/*            if (parameters is not string buttonName)
            {
                return;
            }*/
            ButtonPressed?.Invoke(this, "test");
        }

        protected virtual bool CanBePresed(object? parameters)
        {
            return true;
        }
    }
}
