namespace NaturalnieApp2.Common.Attributes.ValidationRules
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RegexStringValidatorCustomAttribute: ValidationAttribute
    {
        private Regex regex;
        private string pattern;
        public RegexStringValidatorCustomAttribute(string regexPattern)
        {
            pattern = regexPattern;
            regex = new Regex(pattern);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"Wprowadzona wartość nie odpowiada podanemu wzorcowi walidacji. Podany wzorzec: '{pattern}'";
        }

        public override bool IsValid(object? value)
        {
            if (value is not null)
            {
                string? valueAsString = value.ToString();
                if(valueAsString is null)
                {
                    return true;
                }

                return regex.Match(valueAsString).Success;
            }

            return true;
        }
    }
}
