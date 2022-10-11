namespace NaturalnieApp2.SharedInterfaces.Models
{
    public interface IModel
    {
        public event EventHandler? ModelUpdated;
        public void OnModelUpdated();
        public object? GetValue(string propertyName);

    }
}
