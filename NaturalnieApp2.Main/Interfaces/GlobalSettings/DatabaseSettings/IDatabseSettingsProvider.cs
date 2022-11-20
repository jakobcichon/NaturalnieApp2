namespace NaturalnieApp2.Main.Interfaces.GlobalSettings.DatabaseSettings
{
    using NaturalnieApp2.Main.Interfaces.Model;

    public interface IDatabseSettingsProvider : IModel
    {
        public string ConnectionString { get; set; }
    }
}
