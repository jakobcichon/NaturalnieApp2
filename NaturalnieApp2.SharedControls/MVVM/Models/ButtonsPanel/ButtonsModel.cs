namespace NaturalnieApp2.SharedControls.MVVM.Models.ButtonsPanel
{
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using System;

    public class ButtonsModel : BindableBase
    {
        #region Fields
        private string buttonName;
        private CommandBase buttonCommand;
        private bool isEnabled;
        #endregion

        #region Properties
        public string ButtonName
		{
			get { return buttonName; }
			set { SetProperty(ref buttonName, value); }
		}

        public CommandBase ButtonCommand
        {
            get { return buttonCommand; }
            set { SetProperty(ref buttonCommand, value); }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { SetProperty(ref isEnabled, value); }
        }
        #endregion

    }
}
