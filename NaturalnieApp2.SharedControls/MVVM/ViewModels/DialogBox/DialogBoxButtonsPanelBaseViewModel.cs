namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox
{
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static NaturalnieApp2.SharedInterfaces.DialogBox.DialogBoxResults;

    public class DialogBoxButtonsPanelBaseViewModel: BaseViewModel
    {
        #region Events
        public event EventHandler<DialogBoxResults> ButtonPressed;
        #endregion

        #region Private/Protected methods
        protected void OnButtonPressed(DialogBoxResults dialogResult)
        {
            ButtonPressed.Invoke(this, dialogResult);
        }

        protected virtual bool CanBePresed(object? parameters)
        {
            return true;
        }
        #endregion

    }
}
