namespace NaturalnieApp2.Main.MVVM.ViewModels.Inventory.InventoryWizardPages
{
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Documents;

    internal enum ResultOptions
    {
        None = 0,
        EmptyList,
        FullList,
        ListForTheGivenName
    }

    internal class InventoryContinuationOptionsViewModel : ValidatableBindableBase
    {
        #region Fields
        private string selectedOptionName;
        private string selectedName;
        private ObservableCollection<string> availableNames = new();
        #endregion

        #region Events
        public EventHandler<ResultOptions> StartRequestHandler = delegate { }!;
        public EventHandler PreviousPageRequestHandler = delegate { }!;
        #endregion

        #region Properties
        public CommandBase MouseClickCommand { get; set; }
        public CommandBase StartClickCommand { get; set; }
        public CommandBase PreviousPageClickCommnad { get; set; }

        public ObservableCollection<string> AvailableNames
        {
            get { return availableNames; }
            set { SetProperty(ref availableNames, value); }
        }

        public string SelectedOptionName
        {
            get { return selectedOptionName; }
            set
            {
                SetProperty(ref selectedOptionName, value);
                StartClickCommand.OnCanExecuteChange();
            }
        }

        public string SelectedName
        {
            get { return selectedName; }
            set 
            { 
                SetProperty(ref selectedName, value);
                SelectedOptionName = "Option3";
            }
        } 
        #endregion


        public InventoryContinuationOptionsViewModel()
        {
            MouseClickCommand = new(OnMouseClickAction);
            StartClickCommand = new(OnStartClickAction, (object? input) =>
            {
                if (!string.IsNullOrEmpty(SelectedOptionName) && SelectedOptionName.Equals("Option3"))
                {
                    if(!string.IsNullOrEmpty(SelectedName))
                    {
                        return true;
                    }

                    return false;
                }

                return !string.IsNullOrEmpty(SelectedOptionName);
            });
            PreviousPageClickCommnad = new(OnPreviousPageClickAction);
        }

        private void OnMouseClickAction(object? data)
        {
            if (data as string is null)
            {
                return;
            }

            SelectedOptionName = (data as string)!;
        }

        private void OnStartClickAction(object? data)
        {
            ResultOptions result = ResultOptions.None;
            if (SelectedOptionName.Equals("Option1"))
            {
                result = ResultOptions.EmptyList;
            }
            if (SelectedOptionName.Equals("Option2"))
            {
                result = ResultOptions.FullList;
            }
            if (SelectedOptionName.Equals("Option3"))
            {
                result = ResultOptions.ListForTheGivenName;
            }

            StartRequestHandler?.Invoke(this, result);
        }

        private void OnPreviousPageClickAction(object? data)
        {
            PreviousPageRequestHandler?.Invoke(this, EventArgs.Empty);
        }
    }


}
