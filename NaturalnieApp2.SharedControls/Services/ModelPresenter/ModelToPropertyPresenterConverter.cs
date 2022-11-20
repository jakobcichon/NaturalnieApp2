namespace NaturalnieApp2.SharedControls.Services.ModelPresenter
{
    using NaturalnieApp2.Common.Attributes.DisplayableModel;
    using NaturalnieApp2.Common.Collections;
    using NaturalnieApp2.Common.Properties;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Media;
    using static NaturalnieApp2.Common.Attributes.DisplayableModel.DoNotDisplayAttribute;

    public class ModelToPropertyPresenterConverter : IModelToPropertyPresenterConverter
    {

        private Dictionary<Type, Func<string, object, IPropertyPresenterDataField>> propertyPresenterForPropTypeDict = new();
        private Dictionary<string, IPropertyPresenterDataField> propertyPresenterForPropNameDict = new();

        public void AddPresenterForPropertyType<T>(IPropertyPresenterDataField propertyPresenter)
        {
            if (!propertyPresenterForPropTypeDict.Any(e => e.Key == typeof(T)))
            {
                propertyPresenterForPropTypeDict.Add(typeof(T), (name, context) =>
                { return propertyPresenter; });
            }
        }

        public void AddPresenterForPropertyType<T>(Func<string, object, IPropertyPresenterDataField> func)
        {
            if (!propertyPresenterForPropTypeDict.Any(e => e.Key == typeof(T)))
            {
                propertyPresenterForPropTypeDict.Add(typeof(T), func);
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
            if(model is null)
            {
                return Enumerable.Empty<IPropertyPresenter>();
            }

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
            if (!CheckIfPropertyCanBeDisplayed(propertyInfo))
            {
                return null;
            }

            IPropertyPresenter? propertyPresenter = new PropertyPresenterBaseViewModel();

            IPropertyPresenterDataField? propertyPresenterDataField = GetPropertyPresenterDataField(propertyInfo, model);

            propertyPresenter.PropertyPresenterDataField = propertyPresenterDataField;
            propertyPresenter.HeaderText = GetPropertyDisplayabeName(propertyInfo) ?? propertyInfo.Name;

            return propertyPresenter;
        }

        private static string? GetPropertyDisplayabeName(PropertyInfo propertyInfo)
        {
            DisplayableNameAttribute? attribute = propertyInfo.GetCustomAttributes().Where(a => a is DisplayableNameAttribute).FirstOrDefault() as DisplayableNameAttribute;

            if(attribute is null)
            {
                return null;
            }

            return attribute.DisplayName;
        }

        private IPropertyPresenterDataField? GetPropertyPresenterDataField(PropertyInfo propertyInfo, object model)
        {
            IPropertyPresenterDataField? propertyPresenterDataField;

            // Check if property has addmisible list of values
            if(HasPropertyAddmisibleList(propertyInfo))
            {
                IEnumerable? list = GetAddmisibleList(propertyInfo, model);

                if(list is not null)
                {
                    return GetDataFieldForAddmisibleList(propertyInfo.Name, model, list);
                }
            }

            // First serched by name
            bool result = this.propertyPresenterForPropNameDict.TryGetValue(propertyInfo.Name, out propertyPresenterDataField);

            // First serched by type
            if (!result)
            {
                Func<string, object, IPropertyPresenterDataField>? func;
                result = this.propertyPresenterForPropTypeDict.TryGetValue(propertyInfo.PropertyType, out func);

                if (result && func != null)
                {
                    propertyPresenterDataField = func.Invoke(propertyInfo.Name, model);
                }
            }

            // Last - get default
            if (!result)
            {
                propertyPresenterDataField = GetDefaultPropertyPresenter(propertyInfo.Name, model);
            }

            return propertyPresenterDataField;
        }

        private static IEnumerable? GetAddmisibleList(PropertyInfo propertyInfo, object model)
        {
            if (HasPropertyAddmisibleList(propertyInfo))
            {
                string? propertyName = (propertyInfo.GetCustomAttributes().Where(a => a is HasAdmissibleListAttribute).FirstOrDefault() as HasAdmissibleListAttribute)?.PropertyName;

                if(propertyName == null)
                {
                    return null;
                }

                return model.GetType().GetProperty(propertyName)?.GetValue(model) as IEnumerable;
            }

            return null;
        }

        private static bool HasPropertyAddmisibleList(PropertyInfo propertyInfo)
        {
            if (propertyInfo.CustomAttributes.Any(a => a.AttributeType == typeof(HasAdmissibleListAttribute)))
            {
                return true;
            }

            return false;
        }

        private static bool CheckIfPropertyCanBeDisplayed(PropertyInfo propertyInfo)
        {
            if (propertyInfo.CustomAttributes.Any(a => a.AttributeType == typeof(DoNotDisplayAttribute)))
            {
                return false;
            }

            return true;
        }

        private static ObservableCollectionCustom<object> CreateObservableCollectionFromList(IEnumerable inputList)
        {
            ObservableCollectionCustom<object> collection = new();

            foreach (var element in inputList)
            {
                collection.Add(element);
            }

            return collection;
        }

        private static IPropertyPresenterDataField GetDefaultPropertyPresenter(string propertyName, object propertyOwner)
        {
            return new PropertyPresenterTextBoxViewModel()
            {
                ProxyProperty = new(propertyOwner, propertyName)
            };
        }

        private static IPropertyPresenterDataField GetDataFieldForAddmisibleList(string propertyName, object propertyOwner, IEnumerable list)
        {
            return new PropertyPresenterListViewModel()
            {
                ProxyProperty = new(propertyOwner, propertyName),
                HintList =  CreateObservableCollectionFromList(list)
            };
        }
    }
}
