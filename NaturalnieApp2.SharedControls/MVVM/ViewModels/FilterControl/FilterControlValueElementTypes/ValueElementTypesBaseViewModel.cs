namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.FilterControl.FilterControlValueElementTypes
{
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.Common.Properties;

    internal class ValueElementTypesBaseViewModel : ValidatableBindableBase
    {
        public ProxyPropertyService Value { get; init; }

        public ValueElementTypesBaseViewModel(object propertyContext, string propertyName)
        {
            Value = new(propertyContext, propertyName);
        }
    }

}
