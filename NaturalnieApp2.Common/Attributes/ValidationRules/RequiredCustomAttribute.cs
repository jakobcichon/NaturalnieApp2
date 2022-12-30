namespace NaturalnieApp2.Common.Attributes.ValidationRules
{
    using System.ComponentModel.DataAnnotations;

    public sealed class RequiredCustomAttribute: RequiredAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return $"Wprowadzona wartość nie może być pusta";
        }
    }
}
