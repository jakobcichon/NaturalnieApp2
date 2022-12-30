namespace NaturalnieApp2.Main.MVVM.ViewModels.Inventory
{
    using NaturalnieApp2.Common.Collections;
    using NaturalnieApp2.Database.Interfaces;
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.Main.MVVM.Models.Inventory;
    using NaturalnieApp2.Main.MVVM.ViewModels.Inventory.InventoryWizardPages;
    using NaturalnieApp2.SharedControls.Interfaces.WizardDialog;
    using NaturalnieApp2.SharedControls.MVVM.Commands;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class InventoryViewModel : BaseViewModel, IMenuScreen
    {
        #region Fields
        InventorySelectionViewModel inventorySelectionPage = new();
        InventoryContinuationOptionsViewModel inventoryContinuationOptionsPage = new();
        CreateInventoryViewModel createInventoryPage = new();

        private readonly List<string> inventoriesNames = new();
        private List<string> personsNames = new();

        private string selectedInventoryName = string.Empty;
        private string selectedPersonName = string.Empty;
        #endregion

        #region Properties
        public override string ScreenInfo => "Inwentaryzacja";

        public IInventoryCommands InventoryDatabaseCommands { get; init; }

        public bool IsInitialized { get; private set; }

        public IWizardDialog WizardDialog { get; init; }

        public ObservableCollectionCustom<InventoryModelDTO> InventoryEntries { get; set; } = new();

        public CommandBase CloseRequestCommnad { get; set; }

        public string SelectedInventoryName
        {
            get { return selectedInventoryName; }
            set
            { 
                selectedInventoryName = value; 
                OnPropertyChanged();
            }
        }

        public string SelectedPersonName
        {
            get { return selectedPersonName; }
            set
            {
                selectedPersonName = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Private/Protected methods
        protected override async Task LoadOperation()
        {
            if (!IsInitialized)
            {
                await InitializeAsync();

                AddInventorySelectionPage();
                AddInventoryContiuationOptionPage();
                AddCreateInventoryPage();

                WizardDialog.Open();

                CloseRequestCommnad = new CommandBase(OnCloseRequest);

                IsInitialized = true;
            }
        }

        private void AddInventoryContiuationOptionPage()
        {
            inventoryContinuationOptionsPage.StartRequestHandler += OnStartRequest;
            inventoryContinuationOptionsPage.PreviousPageRequestHandler += OnPreviousPageRequest;
            WizardDialog.AddPage(inventoryContinuationOptionsPage);
        }

        private void AddInventorySelectionPage()
        {
            inventorySelectionPage.InventoriesNames = inventoriesNames;
            inventorySelectionPage.InventorySelectionDoneHandler += OnInventorySelectionDone;
            WizardDialog.AddPage(inventorySelectionPage);
        }

        private void AddCreateInventoryPage()
        {
            createInventoryPage.ExisitngInvetoriesNames.AddRange(inventoriesNames);
            createInventoryPage.InvetoryCreatedHandler += OnInventoryCreated;
            createInventoryPage.PreviousPageRequestHandler += OnPreviousPageRequest;
            WizardDialog.AddPage(createInventoryPage);
        }

        private void OnInventoryCreated(object? sender, InventoryModelDTO e)
        {
            SelectedInventoryName = e.InventoryName;
            SelectedPersonName = e.PersonName;
            WizardDialog.Close();
        }

        private void OnCloseRequest(object? sender)
        {
            base.OnCloseRequest();
        }


        private async void OnInventorySelectionDone(object? sender, InventorySelectionDoneArgs e)
        {
            if (e.DoneStatus == InventorySelectionDoneStatus.ContinueExisitngInventory)
            {
                await GoToContinuationOptions();
            }
            if (e.DoneStatus == InventorySelectionDoneStatus.CreateNewInventory)
            {
                GoToCreateInventory();
            }
        }

        private async Task GoToContinuationOptions()
        {
            SelectedInventoryName = inventorySelectionPage.SelectedInventoryName;

            WizardDialog.GoToPage(inventoryContinuationOptionsPage);

            var names = await InventoryDatabaseCommands.GetPersonNamesForGivenInventoryAsync(selectedInventoryName);
            personsNames.AddRange(names);

            personsNames.ForEach(inventoryContinuationOptionsPage.AvailableNames.Add);
        }

        private void GoToCreateInventory()
        {
            WizardDialog.GoToPage(createInventoryPage);
        }

        private async void OnStartRequest(object? sender, ResultOptions optionName)
        {
            SelectedPersonName = inventoryContinuationOptionsPage.SelectedName;

            switch (optionName)
            {
                case ResultOptions.FullList:
                    await GetFullListAsync();
                    break;
                case ResultOptions.EmptyList:
                    GetEmptyList();
                    break;
                case ResultOptions.ListForTheGivenName:
                    await GetFilteredListAsync();
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
            if(string.IsNullOrEmpty(selectedInventoryName))
            {
                return;
            }

            var ents = await InventoryDatabaseCommands.GetEntriesForGivenInventoryAsync(selectedInventoryName);
            ents.ToList().ForEach(e => InventoryEntries.Add(new InventoryModelDTO(e)));
        }

        private async Task GetFilteredListAsync()
        {
            if (string.IsNullOrEmpty(selectedInventoryName) || string.IsNullOrEmpty(selectedPersonName))
            {
                return;
            }

            var ents = await InventoryDatabaseCommands.GetEntriesForGivenInventoryAndPersonAsync(selectedInventoryName, selectedPersonName);
            ents.ToList().ForEach(e => InventoryEntries.Add(new InventoryModelDTO(e)));
        }

        private void GetEmptyList()
        {
            InventoryEntries.Clear();
        }

        #endregion

        #region Disposable
        private bool _disposedValue;
        protected override void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    inventorySelectionPage.InventorySelectionDoneHandler -= OnInventorySelectionDone;
                    inventoryContinuationOptionsPage.StartRequestHandler -= OnStartRequest;
                    inventoryContinuationOptionsPage.PreviousPageRequestHandler -= OnPreviousPageRequest;

                    inventorySelectionPage.Dispose();
                    inventoryContinuationOptionsPage.Dispose();

                    foreach(var element in InventoryEntries)
                    {
                        element.Dispose();
                    }
                }

                // Free unmanaged resources (unmanaged objects) and override a finalizer below.
                // Set large fields to null.
                _disposedValue = true;
            }

            // Call the base class implementation.
            base.Dispose(disposing);
        }
        #endregion
    }
}
