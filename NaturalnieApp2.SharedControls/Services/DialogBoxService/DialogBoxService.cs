﻿namespace NaturalnieApp2.SharedControls.Services.DialogBoxService
{
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using NaturalnieApp2.SharedInterfaces.Logger;
    using System;
    using System.Collections.Concurrent;
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
            ShowCommonaAction(message, DialogBoxTypes.Ok, title);
            logger.Info("Okno dialogowe z przyciskiem Ok zostało utworzone");
            return this;
        }

        public IDialogBox ShowYesNo(string message, string? title = null)
        {
            ShowCommonaAction(message, DialogBoxTypes.YesNo, title);
            return this;
        }

        public IDialogBox ShowYesNoCancel(string message, string? title = null)
        {
            ShowCommonaAction(message, DialogBoxTypes.YesNoCancel, title);
            return this;
        }

        public IDialogBox AddAction(DialogBoxResults resultType, Action action)
        {
            lastlyAddedDialogBox?.AddAction(resultType, action);
            return this;
        }
        #endregion

        #region Private/Protected methods
        private void ShowCommonaAction(string message, DialogBoxTypes buttonsPanelType, string? title = null)
        {
            if (DialogBoxViewModel == null)
            {
                DialogBoxViewModel = CreateDialogBox(buttonsPanelType, message, title);
                DialogBoxViewModel.Show();
                lastlyAddedDialogBox = DialogBoxViewModel;
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
    }
}
