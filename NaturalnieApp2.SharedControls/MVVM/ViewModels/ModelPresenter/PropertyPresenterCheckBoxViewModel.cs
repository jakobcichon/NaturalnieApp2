namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter
{
    using NaturalnieApp2.Common.Properties;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;

    public class PropertyPresenterCheckBoxViewModel : PropertyPresenterBaseViewModel, IPropertyPresenterInputField
    {
        public ProxyPropertyService ProxyProperty { get; init; }
    }
}
