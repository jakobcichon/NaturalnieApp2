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
        #region Events
        public event EventHandler<object?> ButtonPressed;
        #endregion

        #region Fields
        private string message = string.Empty;
        private Dictionary<DialogResultEnum, Action> dialogResultActionList = new();
        private DialogResultEnum dialogResult;
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

        public DialogResultEnum DialogResult
        {
            get { return dialogResult; }
            set 
            { 
                dialogResult = value;
                CallActionForDialogResult(dialogResult);
            }
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

        public void AddAction(DialogResultEnum dialogResult, Action action)
        {
            dialogResultActionList.Add(dialogResult, action);
        }
        #endregion

        #region Private/Protected methods
        protected virtual void OnButtonPressed(object? parameters)
        {
            ButtonPressed?.Invoke(this, parameters);
        }

        protected virtual bool CanBePresed(object? parameters)
        {
            return true;
        }

        private void CallActionForDialogResult(DialogResultEnum dialogResult)
        {
            dialogResultActionList.TryGetValue(dialogResult, out var action);
            action?.Invoke();
        }
        #endregion
    }
}
