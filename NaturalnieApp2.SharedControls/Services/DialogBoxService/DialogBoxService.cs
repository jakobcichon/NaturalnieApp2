namespace NaturalnieApp2.SharedControls.Services.DialogBoxService
{
    using NaturalnieApp2.Common.Disposable;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using NaturalnieApp2.SharedInterfaces.Logger;
    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Linq;
    using System.Media;
    using System.Runtime.CompilerServices;

    public class DialogBoxService : DisposableBase, IDialogBox, INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Fields
        private ILogger logger;
        private DialogBoxBaseViewModel? dialogBoxViewModel;
        private DialogBoxBaseViewModel? lastlyAddedDialogBox;
        private readonly BlockingCollection<DialogBoxBaseViewModel> dialogBoxViewModelWaitingList = new();
        #endregion

        #region Constructor
        public DialogBoxService(ILogger logger)
        {
            this.logger = logger;
        }
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
        public IDialogBox Show(string message, string? title = null)
        {
            ShowCommonAction(message, DialogBoxTypes.Ok, title);
            logger.Info("Okno dialogowe z przyciskiem Ok zostało utworzone");
            return this;
        }

        public IDialogBox ShowError(string message, string? title = null)
        {
            if(title is null)
            {
                title = "Błąd";
            }

            ShowCommonAction(message, DialogBoxTypes.Ok, title);
            logger.Info("Okno dialogowe z przyciskiem Ok mówiące o błędzie zostało utworzone");

            SystemSounds.Asterisk.Play();
            return this;
        }

        public IDialogBox ShowYesNo(string message, string? title = null)
        {
            ShowCommonAction(message, DialogBoxTypes.YesNo, title);
            return this;
        }

        public IDialogBox ShowYesNoCancel(string message, string? title = null)
        {
            ShowCommonAction(message, DialogBoxTypes.YesNoCancel, title);
            return this;
        }

        public IDialogBox AddAction(DialogBoxResults resultType, Action action)
        {
            lastlyAddedDialogBox?.AddAction(resultType, action);
            return this;
        }
        #endregion

        #region Private/Protected methods
        private void ShowCommonAction(string message, DialogBoxTypes buttonsPanelType, string? title = null)
        {
            if (DialogBoxViewModel == null)
            {
                DialogBoxViewModel = CreateDialogBox(buttonsPanelType, message, title);
                DialogBoxViewModel.Show();
                lastlyAddedDialogBox = DialogBoxViewModel;
                return;
            }

            dialogBoxViewModelWaitingList.Add(CreateDialogBox(buttonsPanelType, message, title));
            lastlyAddedDialogBox = dialogBoxViewModelWaitingList.Last();
        }

        private static DialogBoxBaseViewModel CreateDialogBox(DialogBoxTypes type, string message, string? title = null)
        {
            switch (type)
            {
                case DialogBoxTypes.Ok:
                    return CreateOkDialogBox(message, title);
                case DialogBoxTypes.YesNo:
                    return CreateYesNoDialogBox(message, title);
                case DialogBoxTypes.YesNoCancel:
                    return CreateYesNoCancelDialogBox(message, title);
                default:
                    return CreateOkDialogBox(message, title);
            }
        }

        private static DialogBoxBaseViewModel CreateOkDialogBox(string message, string? title=null)
        {
            return new DialogBoxBaseViewModel(DialogBoxTypes.Ok)
            {
                Message = message,
                Title = title ?? string.Empty,
            };
        }

        private static DialogBoxBaseViewModel CreateYesNoDialogBox(string message, string? title= null)
        {
            return new DialogBoxBaseViewModel(DialogBoxTypes.YesNo)
            {
                Message = message,
                Title = title ?? string.Empty,
            };
        }

        private static DialogBoxBaseViewModel CreateYesNoCancelDialogBox(string message, string? title = null)
        {
            return new DialogBoxBaseViewModel(DialogBoxTypes.YesNoCancel)
            {
                Message = message,
                Title = title ?? string.Empty,
            };
        }

        private DialogBoxBaseViewModel? GetNextWaitingDialogBox()
        {
            if(dialogBoxViewModelWaitingList.Count > 0)
            {
                return dialogBoxViewModelWaitingList.Take();
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

        #region Disposable
        private bool _disposedValue;
        protected override void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    DialogBoxViewModel = null;
                    lastlyAddedDialogBox?.Dispose();
                    foreach (var element in dialogBoxViewModelWaitingList)
                    {
                        element.Dispose();
                    }
                }

                // Free unmanaged resources (unmanaged objects) and override a finalizer below.
                // Set large fields to null.
                _disposedValue = true;
            }

            // Call the base class implementation.
            base.Dispose(disposing);
        }
        #endregion
    }
}
