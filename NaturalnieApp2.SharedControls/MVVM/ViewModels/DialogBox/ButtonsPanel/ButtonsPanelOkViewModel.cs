namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox.ButtonsPanel
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

    public class ButtonsPanelOkViewModel : ButtonsPanelBaseViewModel
    {
        public CommandBase OkButton => new(OkButtonPressed, CanBePresed);

        private void OkButtonPressed(object? parameters)
        {
            OnButtonPressed(DialogBoxResults.OK);
        }
    }
}
