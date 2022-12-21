namespace NaturalnieApp2.Main.MVVM.ViewModels.Inventory
{
    using NaturalnieApp2.Common.Collections;
    using NaturalnieApp2.Database.Interfaces;
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.Main.MVVM.Models.Inventory;
    using NaturalnieApp2.Main.MVVM.Models.Product;
    using NaturalnieApp2.Main.MVVM.ViewModels.Inventory.InventoryWizardPages;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.Interfaces.WizardDialog;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.FilterControl;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.WizardDialog;
    using NaturalnieApp2.SharedInterfaces.Database;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class InventoryViewModel : BaseViewModel, IMenuScreen
    {
        #region Fields
        InventorySelectionViewModel inventorySelectionPage = new();

        private readonly List<string> inventoriesNames = new();
        #endregion

        #region Properties
        public override string ScreenInfo => "Inwentaryzacja";

        public IInventoryCommands InventoryDatabaseCommands { get; init; }

        public bool IsInitialized { get; private set; }

        public IWizardDialog WizardDialog { get; init; }

        public ObservableCollectionCustom<InventoryModelDTO> InventoryEntries { get; set; } = new();
        #endregion

        #region Private/Protected methods
        protected override async Task LoadOperation()
        {
            if (!IsInitialized)
            {
                await Initialization();
                IsInitialized = true;

                inventorySelectionPage.InventoriesNames = inventoriesNames;
                WizardDialog.AddPage(inventorySelectionPage);

                WizardDialog.Open();
            }
        }

        private async Task Initialization()
        {
            var ents = await InventoryDatabaseCommands.GetAllElementsAsync();
            ents.ToList().ForEach(e => InventoryEntries.Add(new InventoryModelDTO(e)));

            inventoriesNames.AddRange(await InventoryDatabaseCommands.GetInventoriesNamesAsync());
            await Task.CompletedTask;
        }
        #endregion
    }
}
