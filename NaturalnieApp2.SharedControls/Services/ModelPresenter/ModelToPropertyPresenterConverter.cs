namespace NaturalnieApp2.SharedControls.Services.ModelPresenter
{
    using NaturalnieApp2.Common.Attributes.DisplayableModel;
    using NaturalnieApp2.Common.Properties;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using static NaturalnieApp2.Common.Attributes.DisplayableModel.DisplayableModel;

    public class ModelToPropertyPresenterConverter : IModelToPropertyPresenterConverter
    {

        private Dictionary<Type, Func<string, object, IPropertyPresenterDataField>> propertyPresenterForPropTypeDict = new();
        private Dictionary<string, IPropertyPresenterDataField> propertyPresenterForPropNameDict = new();

        public void AddPresenterForPropertyType(Type propType, IPropertyPresenterDataField propertyPresenter)
        {
            if (!propertyPresenterForPropTypeDict.Any(e => e.Key == propType))
            {
                propertyPresenterForPropTypeDict.Add(propType, (name, context) =>
                { return propertyPresenter; });
            }
        }

        public void AddPresenterForPropertyType(Type propType, Func<string, object, IPropertyPresenterDataField> func)
        {
            if (!propertyPresenterForPropTypeDict.Any(e => e.Key == propType))
            {
                propertyPresenterForPropTypeDict.Add(propType, func);
            }
        }

        public void AddPresenterForPropertyName(string propertyName, IPropertyPresenterDataField propertyPresenter)
        {
            if (!propertyPresenterForPropNameDict.Any(e => e.Key == propertyName))
            {
                propertyPresenterForPropNameDict.Add(propertyName, propertyPresenter);
            }
        }

        public IEnumerable<IPropertyPresenter> GetPropertyPresenterForModel(object model)
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

        public IPropertyPresenter? GetPropertyPresenter(PropertyInfo propertyInfo, object model)
        {
            if(!propertyInfo.CustomAttributes.Any(a => a.AttributeType == typeof(CanBeDisplayed)))
            {
                return null;
            }
            IPropertyPresenter? propertyPresenter = new PropertyPresenterBaseViewModel();
            IPropertyPresenterDataField? propertyPresenterDataField;

            // First serched by name
            bool result = this.propertyPresenterForPropNameDict.TryGetValue(propertyInfo.Name, out propertyPresenterDataField);

            // First serched by type
            if (!result)
            {
                Func<string, object, IPropertyPresenterDataField>? func;
                result = this.propertyPresenterForPropTypeDict.TryGetValue(propertyInfo.PropertyType, out func);

                if(result && func != null)
                {
                    propertyPresenterDataField = func.Invoke(propertyInfo.Name, model);
                }
            }

            // Last - get default
            if (!result)
            {
                propertyPresenterDataField = GetDefaultPropertyPresenter(propertyInfo.Name, model);
            }

            propertyPresenter.PropertyPresenterDataField = propertyPresenterDataField;
            propertyPresenter.HeaderText = propertyInfo.Name;

            return propertyPresenter;
        }

        private static IPropertyPresenterDataField GetDefaultPropertyPresenter(string propertyName, object propertyOwner)
        {
            return new PropertyPresenterTextBoxViewModel() 
            { 
                ProxyProperty = new() 
                { 
                    PropertyName = propertyName,
                    PropertyContext = propertyOwner
                } 
            };
        }
    }
}
