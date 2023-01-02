namespace NaturalnieApp2.Main.Interfaces.Model
{
    public interface IModel
    { 
        public bool IsValid { get; }
        public bool IsPropertyValid(string propertyName);
    }
}
