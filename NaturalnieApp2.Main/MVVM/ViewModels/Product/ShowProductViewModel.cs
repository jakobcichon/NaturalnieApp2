namespace NaturalnieApp2.Main.MVVM.ViewModels.Product
{
    using NaturalnieApp2.Common.Collections;
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

        public ProductModel model { get; set; }
        public ObservableCollectionCustom<ProductModel> Products { get; set;}
        public FilterControlViewModel<ProductModel> FilteredProducts { get; set; }
        #endregion

        public ShowProductViewModel()
        {
            FilteredProducts = new(typeof(ProductModel));
            FilteredProducts.FilterChangedHandler += OnFilterChanged;
            Products = new();
        }

        private void OnFilterChanged(object? sender, List<ProductModel> e)
        {
            Products.Clear();
            foreach(var element in e) 
            {
                Products.Add(element);
            }
        }

        #region Private/Protected methods
        protected override async Task LoadOperation()
        {
            object model = new DummyProductModel();

            await CreateModelPresenter();
            var products = await ProductDatabaseCommands.GetAllElements();
            FilteredProducts.ReferenceList = products.ToList();
            Products.AddRange(products);

            await Task.CompletedTask;
        }

        private async Task CreateModelPresenter()
        {
            await ModelPresenter.CreateFromModel(model);
        }
        #endregion
    }
}
