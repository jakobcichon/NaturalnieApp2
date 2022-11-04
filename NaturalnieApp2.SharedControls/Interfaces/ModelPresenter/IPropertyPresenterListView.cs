namespace NaturalnieApp2.SharedControls.Interfaces.ModelPresenter
{
    using NaturalnieApp2.Common.Collections;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPropertyPresenterListView : IPropertyPresenterDataField
    {
        public ObservableCollectionCustom<object> HintList { get;}
    }
}
