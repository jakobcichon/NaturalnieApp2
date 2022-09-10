namespace NaturalnieApp2.Main.MVVM.ViewModels
{
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    public class BaseViewModel : INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler? PropertyChanged;

        private IDialogBox? dialogBox;

        public IDialogBox? DialogBox
        {
            get { return dialogBox; }
            set
            {
                dialogBox = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
