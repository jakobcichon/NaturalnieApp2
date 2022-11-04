namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter
{
    using NaturalnieApp2.Common.Collections;
    using NaturalnieApp2.Common.Properties;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class PropertyPresenterListViewModel : BaseViewModel, IPropertyPresenterListView
    {
        public ProxyPropertyService ProxyProperty { get; init; }

        private ObservableCollectionCustom<object> hintList;
        public ObservableCollectionCustom<object> HintList
        {
            get
            {
                hintList ??= new();

                return hintList;
            }

            set
            {
                hintList = value;
            }
        }

    }
 
}
