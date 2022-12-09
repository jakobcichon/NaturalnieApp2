namespace NaturalnieApp2.Main.MVVM.ViewModels.Product
{
    using NaturalnieApp2.Common.Collections;
    using NaturalnieApp2.Database.Models;
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.Main.MVVM.Models.Product;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.FilterControl;
    using NaturalnieApp2.SharedInterfaces.Database;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using NaturalnieApp2.Main.MVVM.Models.Product;

    internal class ShowProductViewModel : BaseViewModel, IMenuScreen
    {
        #region Properties
        public override string ScreenInfo => "Informacje o produkcie";
        public IModelPresenter ModelPresenter { get; init; }
        public IDatabaseGeneralCommands<ProductModel> ProductDatabaseCommands { get; init; }

        public bool IsInitialized { get; private set; }

        public ProductModel model { get; set; }
        public ObservableCollectionCustom<ProductModelDTO> Products { get; set;}
        public FilterControlViewModel<ProductModelDTO> FilteredProducts { get; set; }
        #endregion

        public ShowProductViewModel()
        {
            FilteredProducts = new(typeof(ProductModelDTO));
            FilteredProducts.FilterChangedHandler += OnFilterChanged;
            Products = new();
        }

        private void OnFilterChanged(object? sender, List<ProductModelDTO> e)
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
            await CreateModelPresenter();
            List<ProductModelDTO> dtoProductModel = new();
            var products = await ProductDatabaseCommands.GetAllElements();
            foreach(var element in products)
            {
                dtoProductModel.Add(new ProductModelDTO(element));
            }

            FilteredProducts.ReferenceList = dtoProductModel;
            Products.AddRange(dtoProductModel);

            await Task.CompletedTask;
        }

        private async Task CreateModelPresenter()
        {
            await ModelPresenter.CreateFromModel(model);
        }
        #endregion
    }
}
