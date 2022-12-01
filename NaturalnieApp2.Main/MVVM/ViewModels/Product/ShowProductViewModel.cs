namespace NaturalnieApp2.Main.MVVM.ViewModels.Product
{
    using NaturalnieApp2.Database.Commands;
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.Main.MVVM.Models.Product;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.FilterControl;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter;
    using NaturalnieApp2.SharedControls.Services.ModelPresenter;
    using NaturalnieApp2.SharedInterfaces.Database;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media.TextFormatting;

    internal class ShowProductViewModel : BaseViewModel, IMenuScreen
    {
        #region Properties
        public override string ScreenInfo => "Informacje o produkcie";
        public IModelPresenter ModelPresenter { get; init; }
        public IDatabaseGeneralCommands<ProductModel> ProductDatabaseCommands { get; init; }

        public bool IsInitialized { get; private set; }

        public DummyProductModel model { get; set; }
        public List<DummyProductModel> Products { get; set;}
        public FilterControlViewModel FilteredProducts { get; set; }
        #endregion

        public ShowProductViewModel()
        {
            this.model = new DummyProductModel();
            this.model.Name = new string('a', 250);
            this.model.TaxValuesProvider = new List<int> { 1, 2, 3, 4 };
            this.model.Price = 20;

            FilteredProducts = new(typeof(DummyProductModel));

            Products = new();
            Products.Add(this.model);
        }

        #region Private/Protected methods
        protected override async Task LoadOperation()
        {
            object model = new DummyProductModel();

            await CreateModelPresenter();
            var products = await ProductDatabaseCommands.GetAllElements();

            await Task.CompletedTask;
        }

        private async Task CreateModelPresenter()
        {
            await ModelPresenter.CreateFromModel(model);
        }
        #endregion
    }
}
