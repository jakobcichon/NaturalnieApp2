namespace NaturalnieApp2.Common.Attributes.ValidationRules
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public  class RangeCustomAttribute: RangeAttribute
    {
        public RangeCustomAttribute(int minimum, int maximum): base(minimum, maximum)
        { 
        }

        public RangeCustomAttribute(double minimum, double maximum) : base(minimum, maximum)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return $"Wprowadzona wartość wykracza poza dopuszczalny zakres. Dopuszczalny zakres to Minimum: {Minimum}, Maksimum {Maximum}";
        }
    }
}
