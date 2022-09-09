namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    public class DialogBoxViewModelBase
    {
        protected virtual void ButtonPressed(object? parameters)
        {
            if (parameters is Window view)
            {
                view.Close();
            }
        }

        protected virtual bool CanBePresed(object? parameters)
        {
            return true;
        }
    }
}
