namespace NaturalnieApp2.SharedControls.Services.ModelPresenter
{
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.ModelPresenter;
    using NaturalnieApp2.SharedInterfaces.Models;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class ModelToPropertyPresenterConverter : IModelToPropertyPresenterConverter
    {
        public IPropertyPresenter? DefaultPropertyPresenter { get; set; }

        private Dictionary<Type, IPropertyPresenter> propertyPresenterForPropTypeDict = new();
        private Dictionary<string, IPropertyPresenter> propertyPresenterForPropNameDict = new();

        public void AddPresenterForPropertyType(Type propType, IPropertyPresenter propertyPresenter)
        {
            if (!propertyPresenterForPropTypeDict.Any(e => e.Key == propType))
            {
                propertyPresenterForPropTypeDict.Add(propType, propertyPresenter);
            }
        }

        public void AddPresenterForPropertyName(string propertyName, IPropertyPresenter propertyPresenter)
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
                IPropertyPresenter? presenter = GetPropertyPresenter(prop);
                if(presenter == null)
                {
                    continue;
                }    

                result.Add(presenter);
            }

            return result;
        }

        public IPropertyPresenter? GetPropertyPresenter(PropertyInfo propertyInfo)
        {
            IPropertyPresenter? propertyPresenter;

            // First serched by name
            bool result = this.propertyPresenterForPropNameDict.TryGetValue(propertyInfo.Name, out propertyPresenter);

            if (!result)
            {
                result = this.propertyPresenterForPropTypeDict.TryGetValue(propertyInfo.PropertyType, out propertyPresenter);
            }

            if (!result)
            {
                propertyPresenter = this.DefaultPropertyPresenter;
            }

            return propertyPresenter;
        }



    }
}
