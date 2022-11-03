namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter
{
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.Common.Collections;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.Services.ModelPresenter;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public class ModelPresenterViewModel : BaseViewModel, IModelPresenter
    {
        #region Properties
        private ObservableCollectionCustom<IPropertyPresenter> displayableProperties;

        public ObservableCollectionCustom<IPropertyPresenter> DisplayableProperties
        {
            get { return displayableProperties; }
            set
            {
                displayableProperties = value;
                OnPropertyChanged();
            }
        }

        public IModelToPropertyPresenterConverter? ModelToPropertyPresenterConverter { get; init; }

        public ModelPresenterViewModel()
        {
            DisplayableProperties = new();
        }

        public async Task CreateFromModel(object model)
        {
            await Task.Run(() =>
            {
                IEnumerable<IPropertyPresenter>? properiesPresenters = ModelToPropertyPresenterConverter?.GetPropertyPresenterForModel(model);

                if (properiesPresenters is null)
                {
                    return;
                }

                DisplayableProperties.AddRange(properiesPresenters);
            });
        }

        #endregion
    }
}
