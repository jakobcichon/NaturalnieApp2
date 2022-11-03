namespace NaturalnieApp2.Common.Attributes.ValidationRules
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class StringLengthCustomAttribute: StringLengthAttribute
    {
        public int ActualLength { get; set; }

        public StringLengthCustomAttribute(int maximumLength): base(maximumLength)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return $"Wprowadzony tekst jest zbyt długi! Maksymalna długość teksu: {MaximumLength}. Aktualna długośc teksu {ActualLength}";
        }

        public override bool IsValid(object? value)
        {
            if (value != null)
            {
                ActualLength = value.ToString()?.Length ?? 0;
            }

            return base.IsValid(value);
        }
    }


}
