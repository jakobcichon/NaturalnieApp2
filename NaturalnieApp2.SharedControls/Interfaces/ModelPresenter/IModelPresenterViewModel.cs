namespace NaturalnieApp2.SharedControls.Interfaces.ModelPresenter
{

    using NaturalnieApp2.Common.Collections;
    using System.Threading.Tasks;

    public interface IModelPresenter
    {
        ObservableCollectionCustom<IPropertyPresenter> DisplayableProperties { get; set; }
        IModelToPropertyPresenterConverter? ModelToPropertyPresenterConverter { get; init; }
        Task CreateFromModel(object model);
    }
}