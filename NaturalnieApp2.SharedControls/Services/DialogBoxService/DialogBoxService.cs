namespace NaturalnieApp2.SharedControls.Services.DialogBoxService
{
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    public class DialogBoxService : IDialogBox, INotifyPropertyChanged
    {
        private DialogBoxViewModelBase? dialogBoxViewModel;

        public DialogBoxViewModelBase? DialogBoxViewModel
        {
            get { return dialogBoxViewModel; }
            set 
            { 
                dialogBoxViewModel = value;
                AssignEvents(dialogBoxViewModel);
                OnPropertyChanged();
            }
        }

        private bool visibility;

        public bool Visibility
        {
            get { return visibility; }
            set 
            { 
                visibility = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void AssignEvents(DialogBoxViewModelBase? dialogBox)
        {
            if(dialogBox == null)
            {
                return;
            }
            dialogBox.ButtonPressed += DialogBox_ButtonPressed;
        }

        private void DialogBox_ButtonPressed(object? sender, string e)
        {
            DialogBoxViewModel = null;
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName=null)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }

        public void Show(string message)
        {
            DialogBoxViewModel = new DialogBoxYesNoViewModel();
            DialogBoxViewModel.Message = message;
            Visibility = true;
        }

        public SharedInterfaces.DialogBox.DialogResultEnum ShowYesNo(string message)
        {
            throw new NotImplementedException();
        }

        public SharedInterfaces.DialogBox.DialogResultEnum ShowYesNoCancel(string message)
        {
            throw new NotImplementedException();
        }
    }
}
