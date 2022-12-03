namespace NaturalnieApp2.Common.Extension_Methods
{
    using NaturalnieApp2.Common.Attributes.DisplayableModel;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public static class FieldInfoExtensionMethods
    {
        public static Dictionary<FieldInfo, T> GetFieldsWithCustomAttribute<T>(this List<FieldInfo> Fields)
        {
            IEnumerable<FieldInfo> searchedProps = Fields.Where(p => p.GetCustomAttribute(typeof(T)) != null);

            Dictionary<FieldInfo, T> retDict = new();

            foreach (var prop in searchedProps)
            {
                var att = prop.GetCustomAttribute(typeof(T));

                retDict[prop] = (T)Convert.ChangeType(att, typeof(T));
            }

            return retDict;
        }

        public static bool CheckIfFieldCanBeDisplayed(this FieldInfo FieldInfo)
        {
            if (FieldInfo.CustomAttributes.Any(a => a.AttributeType == typeof(DoNotDisplayAttribute)))
            {
                return false;
            }

            return true;
        }

        public static IEnumerable? GetAddmisibleList(this FieldInfo FieldInfo, object model)
        {
            if (HasFieldAddmisibleList(FieldInfo))
            {
                string? FieldName = (FieldInfo.GetCustomAttributes().Where(a => a is HasAdmissibleListAttribute).FirstOrDefault() as HasAdmissibleListAttribute)?.Name;

                if (FieldName == null)
                {
                    return null;
                }

                return model.GetType().GetField(FieldName)?.GetValue(model) as IEnumerable;
            }

            return null;
        }

        public static bool HasFieldAddmisibleList(this FieldInfo FieldInfo)
        {
            if (FieldInfo.CustomAttributes.Any(a => a.AttributeType == typeof(HasAdmissibleListAttribute)))
            {
                return true;
            }

            return false;
        }
    }
}
