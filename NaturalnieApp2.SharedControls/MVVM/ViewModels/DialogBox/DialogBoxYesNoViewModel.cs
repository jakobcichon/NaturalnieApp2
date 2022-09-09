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

    public class DialogBoxYesNoViewModel : DialogBoxViewModelBase
    {

        public CommandBase LeftButton => new(ButtonPressed, CanBePresed);
        public CommandBase RightButton => new(ButtonPressed, CanBePresed);

        public DialogBoxYesNoViewModel()
        {
        }
    }
}
