namespace NaturalnieApp2.SharedControls.Services.ModelPresenter
{
    using NaturalnieApp2.Common.Attributes.DisplayableModel;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ModelAttributeHelpers
    {
        public static List<string> GetPropertiesDisplayableNames(Type modelType)
        {
            List<PropertyInfo> properties = GetDisplayableProperties(modelType);

            List<string> result = new();

            foreach (PropertyInfo prop in properties)
            {
                DisplayableNameAttribute? attribute = prop.GetCustomAttribute(typeof(DisplayableNameAttribute)) as DisplayableNameAttribute;
                if(attribute != null)
                {
                    result.Add(attribute.DisplayName);
                    continue;
                }

                result.Add(prop.Name);
            }

            return result;
        }

        public static PropertyInfo? GetPropertyByDisplayableName(Type modelType, string displayableName)
        {
            Dictionary<PropertyInfo, DisplayableNameAttribute?> searchProperties = GetPropertiesWithCustomAttribute<DisplayableNameAttribute>(modelType.GetProperties().ToList());

            return searchProperties.Where(p => p.Value?.DisplayName == displayableName).Select(p => p.Key).FirstOrDefault();
        }

        public static Dictionary<PropertyInfo, T> GetPropertiesWithCustomAttribute<T>(List<PropertyInfo> properties)
        {
            IEnumerable<PropertyInfo> searchedProps = properties.Where(p => p.GetCustomAttribute(typeof(T)) != null);

            Dictionary<PropertyInfo, T> retDict = new();

            foreach(var prop in searchedProps)
            {
                var att = prop.GetCustomAttribute(typeof(T));
                
                retDict[prop] = (T)Convert.ChangeType(att, typeof(T));
            }

            return retDict;
        }


        public static List<PropertyInfo> GetDisplayableProperties(Type modelType)
        {
            List<PropertyInfo> properties = modelType.GetProperties().Where(p => p.CustomAttributes.All(a => a.AttributeType != typeof(DoNotDisplayAttribute))).ToList();

            if (properties is null)
            {
                return new();
            }

            return properties;
        }

        public static List<PropertyInfo> GetDisplayableProperties(object model)
        {
            return GetDisplayableProperties(model.GetType());
        }

        public static bool CheckIfPropertyCanBeDisplayed(PropertyInfo propertyInfo)
        {
            if (propertyInfo.CustomAttributes.Any(a => a.AttributeType == typeof(DoNotDisplayAttribute)))
            {
                return false;
            }

            return true;
        }

        public static IEnumerable? GetAddmisibleList(PropertyInfo propertyInfo, object model)
        {
            if (HasPropertyAddmisibleList(propertyInfo))
            {
                string? propertyName = (propertyInfo.GetCustomAttributes().Where(a => a is HasAdmissibleListAttribute).FirstOrDefault() as HasAdmissibleListAttribute)?.PropertyName;

                if (propertyName == null)
                {
                    return null;
                }

                return model.GetType().GetProperty(propertyName)?.GetValue(model) as IEnumerable;
            }

            return null;
        }

        public static bool HasPropertyAddmisibleList(PropertyInfo propertyInfo)
        {
            if (propertyInfo.CustomAttributes.Any(a => a.AttributeType == typeof(HasAdmissibleListAttribute)))
            {
                return true;
            }

            return false;
        }
    }
}