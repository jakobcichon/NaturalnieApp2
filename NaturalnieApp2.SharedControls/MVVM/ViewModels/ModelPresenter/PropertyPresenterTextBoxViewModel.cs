namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter
{
	using NaturalnieApp2.Common.Properties;
	using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;

	public class PropertyPresenterTextBoxViewModel : BaseViewModel, IPropertyPresenterDataField
    {
		public ProxyPropertyService ProxyProperty { get; init; }

        public object? DisplayableValue
        {
			get 
			{ 
				return ProxyProperty?.GetValue(); 
			}
			set 
			{
                ProxyProperty?.SetValue(value);
			}
		}

	}
}
