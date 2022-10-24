namespace NaturalnieApp2.Main.MVVM.ViewModels.Product
{
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.Main.MVVM.Models.Product;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter;
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
        public ModelPresenterViewModel ModelPresenter { get; init; }

        private DummyProductModel model;

        private IModelToPropertyPresenterConverter? modelToPropertyPresenterConverter;
        public IModelToPropertyPresenterConverter? ModelToPropertyPresenterConverter 
        {
            get
            {
                return modelToPropertyPresenterConverter;
            }
            set
            {
                modelToPropertyPresenterConverter = value;
                OnPropertyPresenterConverterChange();
            }
        }
        #endregion

        public ShowProductViewModel()
        {
            this.model = new DummyProductModel();
            this.model.Price = 20;
            ModelPresenter = new ModelPresenterViewModel();

            double testVal = 20;
            string testString;

            testString = string.Format("{0:f2}", "20.");
            ;
        }

        private void OnPropertyPresenterConverterChange()
        {
            IEnumerable<IPropertyPresenter>? propPresenter = ModelToPropertyPresenterConverter?.GetPropertyPresenterForModel(model);

            if (propPresenter == null)
            {
                return;
            }

            foreach (var propP in propPresenter)
            {
                ModelPresenter.DisplayableProperties.Add(propP);
            }
        }

        #region Public methods
        public void Load()
        {
            _ = LoadOperation();
        }

        public async Task LoadAsync()
        {
            await LoadOperation();
        }
        #endregion

        #region Private methods
        private async Task LoadOperation()
        {
            object model = new DummyProductModel();

            await Task.CompletedTask;
        }
        #endregion
    }
}
