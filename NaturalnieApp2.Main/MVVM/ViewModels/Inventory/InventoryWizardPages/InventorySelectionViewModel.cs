namespace NaturalnieApp2.Main.MVVM.ViewModels.Inventory.InventoryWizardPages
{
    using NaturalnieApp2.Common.Binding;
	using System.Collections.Generic;
	using System.Windows.Documents;

	internal class InventorySelectionViewModel : ValidatableBindableBase
    {
		private List<string> inventoriesNames = new();
		private string selectedInventoryName = string.Empty;

        public List<string> InventoriesNames
        {
			get { return inventoriesNames; }
			set { SetProperty(ref inventoriesNames, value); }
		}

		public string SelectedInventoryName
        {
			get { return selectedInventoryName; }
			set { SetProperty(ref selectedInventoryName, value); }
		}


	}
}
