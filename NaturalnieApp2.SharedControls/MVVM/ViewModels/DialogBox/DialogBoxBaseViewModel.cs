namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox
{
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox.ButtonsPanel;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    public class DialogBoxBaseViewModel: BaseViewModel
    {
        #region Events
        public event EventHandler<DialogBoxResults>? ButtonPressed;
        #endregion

        #region Fields
        private static readonly string defaultTitle = "Okno wiadomości";
        private string message = string.Empty;
        private string title = defaultTitle;
        private readonly Dictionary<DialogBoxResults, Action> dialogResultActionList = new();
        private DialogBoxResults dialogResult;
        private ButtonsPanelBaseViewModel? buttonsPanel;
        #endregion

        #region Constructor 
        public DialogBoxBaseViewModel(DialogBoxTypes dialogBoxType)
        {
            ButtonsPanel = GetDialogBoxButtonsPanel(dialogBoxType);
        }
        #endregion 

        #region Properties
        public string Message
        {
            get { return message; }
            set 
            {
                message = value;
                OnPropertyChanged();
            }
        }
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                if (title is null || title == string.Empty)
                {
                    title = defaultTitle;
                }
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

        public DialogBoxResults DialogResult
        {
            get { return dialogResult; }
            set 
            { 
                dialogResult = value;
                CallActionForDialogResult(dialogResult);
            }
        }

        public ButtonsPanelBaseViewModel? ButtonsPanel
        {
            get { return buttonsPanel; }
            set 
            { 
                if (value != buttonsPanel && buttonsPanel != null)
                {
                    buttonsPanel.ButtonPressed -= ButtonsPanel_ButtonPressed;
                }
                buttonsPanel = value; 
            }
        }

        private void ButtonsPanel_ButtonPressed(object? sender, DialogBoxResults e)
        {
            DialogResult = e;
            ButtonPressed?.Invoke(this, e);
        }
        #endregion

        #region Public methods
        public void Show()
        {
            Visibility = true;
        }

        public void Hide()
        {
            Visibility = false;
        }

        public void AddAction(DialogBoxResults dialogResult, Action action)
        {
            dialogResultActionList.Add(dialogResult, action);
        }
        #endregion

        #region Private/Protected methods
        private void CallActionForDialogResult(DialogBoxResults dialogResult)
        {
            dialogResultActionList.TryGetValue(dialogResult, out Action? action);
            action?.Invoke();
        }

        private ButtonsPanelBaseViewModel? GetDialogBoxButtonsPanel(DialogBoxTypes buttonPanelType)
        {
            switch(buttonPanelType)
            {
                case DialogBoxTypes.Ok:
                    return GetButtonsPanel_Ok();
                case DialogBoxTypes.YesNo:
                    return GetButtonsPanel_YesNo();
                case DialogBoxTypes.YesNoCancel:
                    return GetButtonsPanel_YesNoCancel();
                default:
                    return null;
            }
        }

        private ButtonsPanelOkViewModel GetButtonsPanel_Ok()
        {
            ButtonsPanelOkViewModel buttonsPanel = new();
            buttonsPanel.ButtonPressed += ButtonsPanel_ButtonPressed;

            return buttonsPanel;
        }

        private ButtonsPanelYesNoViewModel GetButtonsPanel_YesNo()
        {
            ButtonsPanelYesNoViewModel buttonsPanel = new();
            buttonsPanel.ButtonPressed += ButtonsPanel_ButtonPressed;

            return buttonsPanel;
        }

        private ButtonsPanelYesNoCancelViewModel GetButtonsPanel_YesNoCancel()
        {
            ButtonsPanelYesNoCancelViewModel buttonsPanel = new();
            buttonsPanel.ButtonPressed += ButtonsPanel_ButtonPressed;

            return buttonsPanel;
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
                    // Dispose managed state (managed objects).
                    if(buttonsPanel != null)
                    {
                        buttonsPanel.ButtonPressed -= ButtonsPanel_ButtonPressed;
                        buttonsPanel.Dispose();
                        buttonsPanel = null;
                    }

                    if(dialogResultActionList != null)
                    {
                        dialogResultActionList.Clear();
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
