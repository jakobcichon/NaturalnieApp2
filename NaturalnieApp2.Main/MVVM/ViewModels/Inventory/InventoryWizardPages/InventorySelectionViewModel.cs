namespace NaturalnieApp2.Main.MVVM.ViewModels.Inventory.InventoryWizardPages
{
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public enum InventorySelectionDoneStatus
    {
        CreateNewInventory,
        ContinueExisitngInventory
    }

    public class InventorySelectionDoneArgs
    {
        public InventorySelectionDoneStatus DoneStatus { get; init; }
        public string? SelectedInventoryName { get; init; }
    }


    internal class InventorySelectionViewModel: ValidatableBindableBase
    {
		private List<string> inventoriesNames = new();
		private string selectedInventoryName = string.Empty;

        public EventHandler<InventorySelectionDoneArgs> InventorySelectionDoneHandler = delegate { };

        public InventorySelectionViewModel()
        {
            CreateNewInvetoryCommand = new(OnCreateNewInventory, CanCreateNewInvetory);
            MoveNextStepCommand = new(OnContinueExistingInventory, CanContinueExistingInventory);
        }

        public CommandBase CreateNewInvetoryCommand { get; private set; }
        public CommandBase MoveNextStepCommand { get; private set; }


        public List<string> InventoriesNames
        {
			get { return inventoriesNames; }
			set { SetProperty(ref inventoriesNames, value); }
		}

		public string SelectedInventoryName
        {
			get { return selectedInventoryName; }
			set 
            { 
                SetProperty(ref selectedInventoryName, value);
                MoveNextStepCommand.OnCanExecuteChange();
            }
		}

        private bool CanCreateNewInvetory(object? obj)
        {
            return true;
        }

        private void OnCreateNewInventory(object? obj)
        {
            InventorySelectionDoneHandler?.Invoke(this, new InventorySelectionDoneArgs()
            {
                DoneStatus = InventorySelectionDoneStatus.CreateNewInventory,
                SelectedInventoryName = null
            });;
        }

        private bool CanContinueExistingInventory(object? obj)
        {
            if(!InventoriesNames.Any(e => e.Equals(SelectedInventoryName)))
            {
                return false;
            }

            return true;
        }

        private void OnContinueExistingInventory(object? obj)
        {
            InventorySelectionDoneHandler?.Invoke(this, new InventorySelectionDoneArgs()
            {
                DoneStatus = InventorySelectionDoneStatus.ContinueExisitngInventory,
                SelectedInventoryName = SelectedInventoryName
            }); ;
        }
    }
}
