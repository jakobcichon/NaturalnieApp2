namespace NaturalnieApp2.Common.Extension_Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    public static class TypeExtensionMethods
    {
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
