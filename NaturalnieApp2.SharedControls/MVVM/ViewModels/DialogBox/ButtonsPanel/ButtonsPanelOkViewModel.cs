namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox.ButtonsPanel
{
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using NaturalnieApp2.SharedInterfaces.DialogBox;

    public class ButtonsPanelOkViewModel : ButtonsPanelBaseViewModel
    {
        public CommandBase OkButton => new(OkButtonPressed, CanBePresed);

        private void OkButtonPressed(object? parameters)
        {
            OnButtonPressed(DialogBoxResults.OK);
        }
    }
}
