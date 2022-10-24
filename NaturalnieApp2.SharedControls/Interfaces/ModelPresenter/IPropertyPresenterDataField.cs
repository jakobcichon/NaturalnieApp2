namespace NaturalnieApp2.SharedControls.Interfaces.ModelPresenter
{
    using NaturalnieApp2.Common.Properties;

    public interface IPropertyPresenterDataField
    {
        public ProxyPropertyService ProxyProperty { get; init; }
        public string? CustomMask { get;}
    }
}
