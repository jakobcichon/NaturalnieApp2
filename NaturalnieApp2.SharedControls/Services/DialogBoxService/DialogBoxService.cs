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
    using System.Threading;
    using System.Threading.Tasks;

    public class DialogBoxService : IDialogBox, INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Fields
        private DialogBoxBaseViewModel? dialogBoxViewModel;
        private DialogBoxBaseViewModel? lastlyShownDialogBox;
        private readonly List<DialogBoxBaseViewModel> dialogBoxViewModelWaitingList = new();
        #endregion

        #region Properties
        public DialogBoxBaseViewModel? DialogBoxViewModel
        {
            get { return dialogBoxViewModel; }
            set
            {
                dialogBoxViewModel = value;
                AssignEvents(dialogBoxViewModel);
                OnPropertyChanged();
            }
        }
        #endregion

        #region Public methods
        public IDialogBox Show(string message)
        {

            if (DialogBoxViewModel == null)
            {
                DialogBoxViewModel = CreateDialogBox(DialogBoxTypes.Ok, message);
                DialogBoxViewModel.Show();
                lastlyShownDialogBox = DialogBoxViewModel;
                return this;
            }

            dialogBoxViewModelWaitingList.Add(CreateDialogBox(DialogBoxTypes.Ok, message));
            lastlyShownDialogBox = dialogBoxViewModelWaitingList.Last();
            return this;
        }

        public IDialogBox ShowYesNo(string message)
        {
            throw new NotImplementedException();
        }

        public IDialogBox ShowYesNoCancel(string message)
        {
            throw new NotImplementedException();
        }

        public IDialogBox AddAction(DialogBoxResults resultType, Action action)
        {
            lastlyShownDialogBox?.AddAction(resultType, action);
            return this;
        }
        #endregion

        #region Private/Protected methods
        private static DialogBoxBaseViewModel CreateDialogBox(DialogBoxTypes type, string message)
        {
            switch (type)
            {
                case DialogBoxTypes.Ok:
                    return CreateOkDialogBox(message);
                case DialogBoxTypes.YesNo:
                case DialogBoxTypes.YesNoCancel:
                default:
                    return CreateOkDialogBox(message);
            }
        }

        private static DialogBoxBaseViewModel CreateOkDialogBox(string message)
        {
            return new DialogBoxBaseViewModel
            {
                Message = message,
                ButtonsPanel = new DialogBoxOkViewModel()
            };
        }

        private DialogBoxBaseViewModel? GetNextWaitingDialogBox()
        {
            if(dialogBoxViewModelWaitingList.Count > 0)
            {
                DialogBoxBaseViewModel retVal = dialogBoxViewModelWaitingList.First();
                dialogBoxViewModelWaitingList.RemoveAt(0);
                return retVal;
            }

            return null;
            
        }
        private void AssignEvents(DialogBoxBaseViewModel? dialogBox)
        {
            if (dialogBox == null)
            {
                return;
            }
            dialogBox.ButtonPressed += DialogBox_ButtonPressed;
        }

        private void DialogBox_ButtonPressed(object? sender, DialogBoxResults e)
        {
            CheckPendingViewModels();
        }

        private void CheckPendingViewModels()
        {
            DialogBoxViewModel = GetNextWaitingDialogBox();
            if(DialogBoxViewModel != null)
            {
                DialogBoxViewModel.Show();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
        #endregion
    }
}
