namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox
{
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    public class DialogBoxOkViewModel : DialogBoxButtonsPanelBaseViewModel
    {

        public CommandBase OkButton => new(ButtonPressedAction, CanBePresed);

        public DialogBoxOkViewModel()
        {
        }

        private void ButtonPressedAction(object? parameters)
        {
            if (parameters is string)
            {
                if(parameters.ToString().ToLower() == "ok")
                {
                    OnButtonPressed(DialogBoxResults.OK);
                }
                
            }
        }
    }
}
