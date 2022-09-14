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
        private DialogBoxBaseViewModel? lastlyAddedDialogBox;
        private readonly List<DialogBoxBaseViewModel> dialogBoxViewModelWaitingList = new();
        #endregion

        #region Properties
        public DialogBoxBaseViewModel? DialogBoxViewModel
        {
            get { return dialogBoxViewModel; }
            set
            {
                // Dispose old dialog box view model
                if(dialogBoxViewModel != null)
                {
                    dialogBoxViewModel.ButtonPressed -= DialogBox_ButtonPressed;
                    dialogBoxViewModel.Dispose();
                }

                dialogBoxViewModel = value;
                AssignEvents(dialogBoxViewModel);
                OnPropertyChanged();
            }
        }
        #endregion

        #region Public methods
        public IDialogBox Show(string message)
        {
            ShowCommonaAction(message, DialogBoxTypes.Ok);
            return this;
        }

        public IDialogBox ShowYesNo(string message)
        {
            ShowCommonaAction(message, DialogBoxTypes.YesNo);
            return this;
        }

        public IDialogBox ShowYesNoCancel(string message)
        {
            ShowCommonaAction(message, DialogBoxTypes.YesNoCancel);
            return this;
        }

        public IDialogBox AddAction(DialogBoxResults resultType, Action action)
        {
            lastlyAddedDialogBox?.AddAction(resultType, action);
            return this;
        }
        #endregion

        #region Private/Protected methods
        private void ShowCommonaAction(string message, DialogBoxTypes buttonsPanelType)
        {
            if (DialogBoxViewModel == null)
            {
                DialogBoxViewModel = CreateDialogBox(buttonsPanelType, message);
                DialogBoxViewModel.Show();
                lastlyAddedDialogBox = DialogBoxViewModel;
            }

            dialogBoxViewModelWaitingList.Add(CreateDialogBox(buttonsPanelType, message));
            lastlyAddedDialogBox = dialogBoxViewModelWaitingList.Last();
        }

        private static DialogBoxBaseViewModel CreateDialogBox(DialogBoxTypes type, string message)
        {
            switch (type)
            {
                case DialogBoxTypes.Ok:
                    return CreateOkDialogBox(message);
                case DialogBoxTypes.YesNo:
                    return CreateYesNoDialogBox(message);
                case DialogBoxTypes.YesNoCancel:
                    return CreateYesNoCancelDialogBox(message);
                default:
                    return CreateOkDialogBox(message);
            }
        }

        private static DialogBoxBaseViewModel CreateOkDialogBox(string message)
        {
            return new DialogBoxBaseViewModel(DialogBoxTypes.Ok)
            {
                Message = message,
            };
        }

        private static DialogBoxBaseViewModel CreateYesNoDialogBox(string message)
        {
            return new DialogBoxBaseViewModel(DialogBoxTypes.YesNo)
            {
                Message = message,
            };
        }

        private static DialogBoxBaseViewModel CreateYesNoCancelDialogBox(string message)
        {
            return new DialogBoxBaseViewModel(DialogBoxTypes.YesNoCancel)
            {
                Message = message,
            };
        }

        private DialogBoxBaseViewModel? GetNextWaitingDialogBox()
        {
            if(dialogBoxViewModelWaitingList.Count > 0)
            {
                DialogBoxBaseViewModel retVal = dialogBoxViewModelWaitingList.First();
                dialogBoxViewModelWaitingList.Remove(retVal);
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
