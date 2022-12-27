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
        InventoryContinuationOptionsViewModel inventoryContinuationOptionsPage = new();

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
                await InitializeAsync();

                inventorySelectionPage.InventoriesNames = inventoriesNames;
                inventorySelectionPage.InventorySelectionDoneHandler += OnInventorySelectionDone;
                inventoryContinuationOptionsPage.StartRequestHandler += OnStartRequest;
                inventoryContinuationOptionsPage.PreviousPageRequestHandler += OnPreviousPageRequest;

                WizardDialog.AddPage(inventorySelectionPage);
                WizardDialog.AddPage(inventoryContinuationOptionsPage);

                WizardDialog.Open();

                IsInitialized = true;
            }
        }

        private void OnInventorySelectionDone(object? sender, InventorySelectionDoneArgs e)
        {
            if (e.DoneStatus == InventorySelectionDoneStatus.ContinueExisitngInventory)
            {
                WizardDialog.GoToPage(inventoryContinuationOptionsPage);
            }
        }

        private async void OnStartRequest(object? sender, ResultOptions optionName)
        {
            switch(optionName)
            {
                case ResultOptions.FullList:
                    await GetFullListAsync();
                    break;
                case ResultOptions.EmptyList:
                    GetEmptyList();
                    break;
            }
            WizardDialog.Close();
        }

        private void OnPreviousPageRequest(object? sender, EventArgs e)
        {
            WizardDialog.GoToPreviousPage();
        }

        private async Task InitializeAsync()
        {
            inventoriesNames.AddRange(await InventoryDatabaseCommands.GetInventoriesNamesAsync());
        }

        private async Task GetFullListAsync()
        {
            var ents = await InventoryDatabaseCommands.GetAllElementsAsync();
            ents.ToList().ForEach(e => InventoryEntries.Add(new InventoryModelDTO(e)));
        }

        private void GetEmptyList()
        {
            InventoryEntries.Clear();
        }
        #endregion
    }
}
