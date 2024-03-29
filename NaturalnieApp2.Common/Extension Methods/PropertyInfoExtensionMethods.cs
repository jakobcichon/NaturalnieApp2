﻿namespace NaturalnieApp2.Common.Extension_Methods
{
    using NaturalnieApp2.Common.Attributes.DisplayableModel;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public static class PropertyInfoExtensionMethods
    {

        public static bool IsReadOnly(this PropertyInfo property)
        {
            if (property.GetCustomAttribute(typeof(IsReadOnlyAttribute)) is IsReadOnlyAttribute)
            {
                return true;
            }

            return false;
        }
        public static string? GetDisplayableName(this PropertyInfo property)
        {
            if (property.GetCustomAttribute(typeof(DisplayableNameAttribute)) is DisplayableNameAttribute attribute)
            {
                return attribute.DisplayName;
            }

            return null;
        }

        public static string? GetDisplayableNameOrName(this PropertyInfo property)
        {
            return property.GetDisplayableName() ?? property.Name;
        }

        public static Dictionary<PropertyInfo, T> GetPropertiesWithCustomAttribute<T>(this List<PropertyInfo> properties)
        {
            IEnumerable<PropertyInfo> searchedProps = properties.Where(p => p.GetCustomAttribute(typeof(T)) != null);

            Dictionary<PropertyInfo, T> retDict = new();

            foreach (var prop in searchedProps)
            {
                var att = prop.GetCustomAttribute(typeof(T));

                retDict[prop] = (T)Convert.ChangeType(att, typeof(T));
            }

            return retDict;
        }

        public static bool CheckIfPropertyCanBeDisplayed(this PropertyInfo propertyInfo)
        {
            if (propertyInfo.CustomAttributes.Any(a => a.AttributeType == typeof(DoNotDisplayAttribute)))
            {
                return false;
            }

            return true;
        }

        public static IEnumerable? GetAddmisibleList(this PropertyInfo propertyInfo, Type contextType)
        {
            if (HasPropertyAddmisibleList(propertyInfo))
            {
                string? propertyName = (propertyInfo.GetCustomAttributes().Where(a => a is HasAdmissibleListAttribute).FirstOrDefault() as HasAdmissibleListAttribute)?.Name;

                if (propertyName == null)
                {
                    return null;
                }

                PropertyInfo? addmisibleListProp = contextType.GetProperty(propertyName);
                if(addmisibleListProp == null) 
                { 
                    return new List<object>();
                }

                return addmisibleListProp.GetValue(null) as IEnumerable;
            }

            return null;
        }

        public static bool HasPropertyAddmisibleList(this PropertyInfo propertyInfo)
        {
            if (propertyInfo.CustomAttributes.Any(a => a.AttributeType == typeof(HasAdmissibleListAttribute)))
            {
                return true;
            }

            return false;
        }
    }
}
