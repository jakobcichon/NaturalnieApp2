namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter
{
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedInterfaces.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class ModelPresenterViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<IPropertyPresenter> displayableProperties;

        public ObservableCollection<IPropertyPresenter> DisplayableProperties
        {
            get { return displayableProperties; }
            set 
            { 
                displayableProperties = value;
                OnPropertyChanged();
            }
        }

        public ModelPresenterViewModel()
        {
            DisplayableProperties = new();
        }

        #endregion
    }
}
