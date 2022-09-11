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

    public class DialogBoxOkViewModel : DialogBoxViewModelBase
    {

        public CommandBase OkButton => new(OnButtonPressed, CanBePresed);

        public DialogBoxOkViewModel()
        {
        }

        protected override void OnButtonPressed(object? parameters)
        {
            base.OnButtonPressed(parameters);

            DialogResult = DialogResultEnum.OK;
        }
    }
}
