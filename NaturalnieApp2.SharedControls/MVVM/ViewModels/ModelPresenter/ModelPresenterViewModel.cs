namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter
{
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.Common.Collections;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.Services.ModelPresenter;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Threading;

    public class ModelPresenterViewModel : BaseViewModel, IModelPresenter
    {
        #region Fields
        private ObservableCollectionCustom<IPropertyPresenter> displayableProperties;
        #endregion

        #region Properties
        public ObservableCollectionCustom<IPropertyPresenter> DisplayableProperties
        {
            get { return displayableProperties; }
            set
            {
                displayableProperties = value;
                OnPropertyChanged();
            }
        }

        public bool IsModelCreated { get; private set; }

        public IModelToPropertyPresenterConverter? ModelToPropertyPresenterConverter { get; init; }

        public ModelPresenterViewModel()
        {
            DisplayableProperties = new();
        }

        public async Task CreateFromModel(object model)
        {
            if (!IsModelCreated)
            {
                await Task.Run(() =>
                {
                    CreatePropertiesFromModel(model);
                    IsModelCreated = true;
                });
            }
        }

        public void ClearModel()
        {
            DisplayableProperties.Clear();
            IsModelCreated = false;
        }

        private void CreatePropertiesFromModel(object model)
        {
            IEnumerable<IPropertyPresenter>? properiesPresenters = ModelToPropertyPresenterConverter?.GetPropertyPresenterForModel(model);

            if (properiesPresenters is null)
            {
                return;
            }


            DisplayableProperties.AddRange(properiesPresenters);


        }

        #endregion
    }
}
