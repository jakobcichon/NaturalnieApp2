namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter
{
    using NaturalnieApp2.Common.Properties;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class PropertyPresenterListViewModel : BaseViewModel, IPropertyPresenterListView
    {
        public ProxyPropertyService ProxyProperty { get; init; }

        private ObservableCollection<object> hintList;
        public ObservableCollection<object> HintList
        {
            get
            {
                if(hintList == null)
                {
                    return new();
                }

                return hintList;
            }
        }

    }
 
}
