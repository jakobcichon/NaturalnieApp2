namespace NaturalnieApp2.SharedInterfaces.Xml
{
    using System.Runtime.CompilerServices;

    public interface IXmlSerializer
    {
        Task Serialize(object model, object source);
        T Deserialize<T>(object source);
    }
}
