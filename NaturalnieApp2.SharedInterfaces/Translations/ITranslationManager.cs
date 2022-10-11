namespace NaturalnieApp2.SharedInterfaces.Translations
{
    using System.Reflection;

    public interface ITranslationManager
    {
        string GetTranslationForProperty(Type objectType, PropertyInfo propertyInfo);
    }
}
