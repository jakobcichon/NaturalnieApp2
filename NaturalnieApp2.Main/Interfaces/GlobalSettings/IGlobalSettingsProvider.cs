namespace NaturalnieApp2.Main.Interfaces.GlobalSettings
{
    using NaturalnieApp2.Main.Interfaces.GlobalSettings.DatabaseSettings;

    public interface IGlobalSettingsProvider
    {
        IDatabseSettingsProvider? DatabaseSettings { get;}
    }
}
