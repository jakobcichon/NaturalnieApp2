namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox
{
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
        public event EventHandler<DialogBoxResults> ButtonPressed;
        #endregion

        #region Fields
        private string message = string.Empty;
        private Dictionary<DialogBoxResults, Action> dialogResultActionList = new();
        private DialogBoxResults dialogResult;
        private DialogBoxButtonsPanelBaseViewModel? buttonsPanel;
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

        public DialogBoxButtonsPanelBaseViewModel? ButtonsPanel
        {
            get { return buttonsPanel; }
            set 
            { 
                buttonsPanel = value; 
                if (buttonsPanel != null)
                {
                    buttonsPanel.ButtonPressed += this.ButtonsPanel_ButtonPressed;
                }
            }
        }

        private void ButtonsPanel_ButtonPressed(object? sender, DialogBoxResults e)
        {
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
            dialogResultActionList.TryGetValue(dialogResult, out var action);
            action?.Invoke();
        }
        #endregion
    }
}
