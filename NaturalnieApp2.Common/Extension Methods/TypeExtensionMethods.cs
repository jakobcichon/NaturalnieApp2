namespace NaturalnieApp2.Common.Extension_Methods
{
    using NaturalnieApp2.Common.Attributes.DisplayableModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    public static class TypeExtensionMethods
    {
        public static List<PropertyInfo> GetDisplayableProperties(this Type modelType)
        {
            List<PropertyInfo> properties = modelType.GetProperties().Where(p => p.CustomAttributes.All(a => a.AttributeType != typeof(DoNotDisplayAttribute))).ToList();

            if (properties is null)
            {
                return new();
            }

            return properties;
        }

        public static List<FieldInfo> GetDisplayableFields(this Type modelType)
        {
            List<FieldInfo> properties = modelType.GetFields().Where(p => p.CustomAttributes.All(a => a.AttributeType != typeof(DoNotDisplayAttribute)) && !p.IsSpecialName).ToList();

            if (properties is null)
            {
                return new();
            }

            return properties;
        }

        public static List<string> GetPropertiesDisplayableNames(this Type modelType)
        {
            List<PropertyInfo> properties = modelType.GetDisplayableProperties();

            List<string> result = new();

            foreach (PropertyInfo prop in properties)
            {
                DisplayableNameAttribute? attribute = prop.GetCustomAttribute(typeof(DisplayableNameAttribute)) as DisplayableNameAttribute;
                if (attribute != null)
                {
                    result.Add(attribute.DisplayName);
                    continue;
                }

                result.Add(prop.Name);
            }

            return result;
        }

        public static List<string> GetFieldsDisplayableNames(this Type modelType)
        {
            List<FieldInfo> fields = modelType.GetDisplayableFields();

            List<string> result = new();

            foreach (FieldInfo field in fields)
            {
                DisplayableNameAttribute? attribute = field.GetCustomAttribute(typeof(DisplayableNameAttribute)) as DisplayableNameAttribute;
                if (attribute != null)
                {
                    result.Add(attribute.DisplayName);
                    continue;
                }

                result.Add(field.Name);
            }

            return result;
        }

        public static PropertyInfo? GetPropertyByDisplayableName(this Type modelType, string displayableName)
        {
            Dictionary<PropertyInfo, DisplayableNameAttribute> searchProperties = modelType.GetProperties().ToList().GetPropertiesWithCustomAttribute<DisplayableNameAttribute>();

            return searchProperties.Where(p => p.Value?.DisplayName == displayableName).Select(p => p.Key).FirstOrDefault();
        }

        public static FieldInfo? GetFieldByDisplayableName(this Type modelType, string displayableName)
        {
            Dictionary<FieldInfo, DisplayableNameAttribute> searchProperties = modelType.GetFields().ToList().GetFieldsWithCustomAttribute<DisplayableNameAttribute>();

            return searchProperties.Where(p => p.Value?.DisplayName == displayableName).Select(p => p.Key).FirstOrDefault();
        }

        public static bool IsNumeric(this Type? type)
        {
            switch(Type.GetTypeCode(type))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return true;
                default:
                    return false;

            }
        }

        public static bool IsInteger(this Type? type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;

            }
        }

        public static bool IsFloatingPoint(this Type? type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return true;
                default:
                    return false;

            }
        }

        public static bool IsString(this Type? type)
        {
            if(type == typeof(string))
            {
                return true;
            }
            return false;
        }

        public static bool IsBool(this Type? type)
        {
            if (type == typeof(bool))
            {
                return true;
            }
            return false;
        }

        public static bool IsEnum(this Type? type)
        {
            if (type == typeof(Enum))
            {
                return true;
            }
            return false;
        }
    }
}
