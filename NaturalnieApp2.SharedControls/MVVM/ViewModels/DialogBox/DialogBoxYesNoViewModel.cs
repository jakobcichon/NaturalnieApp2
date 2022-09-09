namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox
{
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    public class DialogBoxYesNoViewModel
    {

        public CommandBase LeftButton => new(ButtonPressed, CanBePresed);
        public CommandBase RightButton => new(ButtonPressed, CanBePresed);

        public DialogBoxYesNoViewModel()
        {
        }

    }
}
