namespace NaturalnieApp2.Main.MVVM.ViewModels.Inventory
{
    using NaturalnieApp2.Common.Collections;
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.Main.MVVM.Models.Inventory;
    using NaturalnieApp2.Main.MVVM.Models.Product;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.FilterControl;
    using NaturalnieApp2.SharedInterfaces.Database;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class InventoryViewModel : BaseViewModel, IMenuScreen
    {
        #region Properties
        public override string ScreenInfo => "Inwentaryzacja";

        public IDatabaseGeneralCommands<InventoryModel> InventoryDatabaseCommands { get; init; }

        public bool IsInitialized { get; private set; }

        public bool WizzardVisbility { get; set; }

        public ObservableCollectionCustom<InventoryModelDTO> InventoryEntries { get; set; } = new();
        #endregion

        #region Private/Protected methods
        protected override async Task LoadOperation()
        {
            if (!IsInitialized)
            {
                await Initialization();
                IsInitialized = true;
            }
        }

        private async Task Initialization()
        {
            var ents = await InventoryDatabaseCommands.GetAllElementsAsync();
            ents.ToList().ForEach(e => InventoryEntries.Add(new InventoryModelDTO(e)));
            await Task.CompletedTask;
        }
        #endregion
    }
}
