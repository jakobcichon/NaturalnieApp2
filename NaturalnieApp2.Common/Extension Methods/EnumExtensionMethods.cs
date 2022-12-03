namespace NaturalnieApp2.Common.Extension_Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumExtensionMethods
    {
        public static List<string> GetDisplayableNamesOrDefault(this Enum obj)
        {
            return obj.GetType().GetFieldsDisplayableNames();
        }

    }
}
