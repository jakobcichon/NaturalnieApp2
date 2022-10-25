namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter
{
	using NaturalnieApp2.Common.Extension_Methods;
	using NaturalnieApp2.Common.Properties;
	using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
	using System;
	using System.Windows.Input;

	public class PropertyPresenterTextBoxViewModel : BaseViewModel, IPropertyPresenterInputField
    {
		public ProxyPropertyService ProxyProperty { get; init; }

		public string? CustomMask
		{
			get
			{
				if(ProxyProperty.PropertyType == null )
				{
					return null;
				}
				
				if(ProxyProperty.PropertyType.IsFloatingPoint())
				{
					return "^(?:\\d*)?(?:\\.\\d*)?$";
                }

                if (ProxyProperty.PropertyType.IsInteger())
                {
                    return "^\\d$";
                }

                return null;
            }

		}
	}
}
