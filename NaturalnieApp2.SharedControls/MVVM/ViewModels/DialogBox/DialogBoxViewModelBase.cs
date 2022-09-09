namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox
{
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    public class DialogBoxViewModelBase: IDialogBox
    {
        public DialogBoxViewModelBase? DialogBoxInstance { get; set; }
        public bool Visiable { get; private set; }

        public void Show(string message)
        {
            DialogBoxInstance = new DialogBoxYesNoViewModel();
            Visiable = true;
        }

        public DialogResultEnum ShowYesNo(string message)
        {
            throw new NotImplementedException();
        }

        public DialogResultEnum ShowYesNoCancel(string message)
        {
            throw new NotImplementedException();
        }

        protected virtual void ButtonPressed(object? parameters)
        {
            Visiable = false;
            DialogBoxInstance = null;
        }

        protected virtual bool CanBePresed(object? parameters)
        {
            return true;
        }
    }
}
