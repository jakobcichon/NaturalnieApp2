namespace NaturalnieApp2.SharedControls.Services.ModelPresenter
{
    using NaturalnieApp2.Common.Properties;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter;
    using NaturalnieApp2.SharedInterfaces.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class ModelToPropertyPresenterConverter : IModelToPropertyPresenterConverter
    {

        private Dictionary<Type, IPropertyPresenterDataField> propertyPresenterForPropTypeDict = new();
        private Dictionary<string, IPropertyPresenterDataField> propertyPresenterForPropNameDict = new();

        public void AddPresenterForPropertyType(Type propType, IPropertyPresenterDataField propertyPresenter)
        {
            if (!propertyPresenterForPropTypeDict.Any(e => e.Key == propType))
            {
                propertyPresenterForPropTypeDict.Add(propType, propertyPresenter);
            }
        }

        public void AddPresenterForPropertyName(string propertyName, IPropertyPresenterDataField propertyPresenter)
        {
            if (!propertyPresenterForPropNameDict.Any(e => e.Key == propertyName))
            {
                propertyPresenterForPropNameDict.Add(propertyName, propertyPresenter);
            }
        }

        public IEnumerable<IPropertyPresenter> GetPropertyPresenterForModel(IModel model)
        {
            List<IPropertyPresenter> result = new();
            List<PropertyInfo> propertyInfos = model.GetType().GetProperties().ToList();

            foreach(PropertyInfo prop in propertyInfos)
            {
                IPropertyPresenter? presenter = GetPropertyPresenter(prop, model);
                if(presenter == null)
                {
                    continue;
                }    

                result.Add(presenter);
            }

            return result;
        }

        public IPropertyPresenter? GetPropertyPresenter(PropertyInfo propertyInfo, IModel model)
        {
            IPropertyPresenter? propertyPresenter = new PropertyPresenterBaseViewModel();
            IPropertyPresenterDataField? propertyPresenterDataField;

            // First serched by name
            bool result = this.propertyPresenterForPropNameDict.TryGetValue(propertyInfo.Name, out propertyPresenterDataField);

            // First serched by type
            if (!result)
            {
                result = this.propertyPresenterForPropTypeDict.TryGetValue(propertyInfo.PropertyType, out propertyPresenterDataField);
            }

            // Last - get default
            if (!result)
            {
                propertyPresenterDataField = GetDefaultPropertyPresenter(propertyInfo.Name, model);
            }

            propertyPresenter.PropertyPresenterDataField = propertyPresenterDataField;
            propertyPresenter.HeaderText = propertyInfo.Name;

            if (propertyPresenter.PropertyPresenterDataField != null)
            {
                propertyPresenter.PropertyPresenterDataField.DisplayableValue = propertyInfo.GetValue(model);
            }

            return propertyPresenter;
        }

        private static IPropertyPresenterDataField GetDefaultPropertyPresenter(string propertyName, object propertyOwner)
        {
            return new PropertyPresenterTextBoxViewModel() 
            { 
                ProxyProperty = new() 
                { 
                    PropertyName = propertyName,
                    PropertyOwnerObject = propertyOwner
                } 
            };
        }
    }
}
