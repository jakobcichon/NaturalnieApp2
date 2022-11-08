namespace NaturalnieApp2.SharedControls.Interfaces.ModelPresenter
{

    using NaturalnieApp2.Common.Collections;
    using System.Threading.Tasks;

    public interface IModelPresenter
    {
        bool IsModelCreated { get; }
        ObservableCollectionCustom<IPropertyPresenter> DisplayableProperties { get; set; }
        IModelToPropertyPresenterConverter? ModelToPropertyPresenterConverter { get; init; }
        Task CreateFromModel(object model);
        void ClearModel();
    }
}