﻿namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox.ButtonsPanel
{
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using NaturalnieApp2.SharedInterfaces.DialogBox;

    public class ButtonsPanelYesNoViewModel : ButtonsPanelBaseViewModel
    {

        public CommandBase YesButton => new(YesButtonPressed, CanBePresed);
        public CommandBase NoButton => new(NoButtonPressed, CanBePresed);

        private void YesButtonPressed(object? parameters)
        {
            OnButtonPressed(DialogBoxResults.Yes);
        }

        private void NoButtonPressed(object? parameters)
        {
            OnButtonPressed(DialogBoxResults.No);
        }
    }
}
