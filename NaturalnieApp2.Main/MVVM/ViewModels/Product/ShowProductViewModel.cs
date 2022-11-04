﻿namespace NaturalnieApp2.Main.MVVM.ViewModels.Product
{
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.Main.MVVM.Models.Product;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter;
    using NaturalnieApp2.SharedControls.Services.ModelPresenter;
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

        private DummyProductModel model;
        #endregion

        public ShowProductViewModel()
        {
            this.model = new DummyProductModel();
            this.model.TaxValuesProvider = new List<int> { 1, 2, 3, 4 };
            this.model.Price = 20;
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
            
            await CreateModelPresenter();

            await Task.CompletedTask;
        }

        private async Task CreateModelPresenter()
        {
            await ModelPresenter.CreateFromModel(model);
        }
        #endregion
    }
}
