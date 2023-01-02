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
    using NaturalnieApp2.Main.Extensions.Database.Models;

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
            var products = await ProductDatabaseCommands.GetAllElementsAsync();

            foreach(var element in products)
            {
                dtoProductModel.Add(element.ToDto());
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

        #region Disposable
        private bool _disposedValue;
        protected override void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).
                    foreach (var element in Products)
                    {
                        element.Dispose();
                    }

                    FilteredProducts.Dispose();
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
