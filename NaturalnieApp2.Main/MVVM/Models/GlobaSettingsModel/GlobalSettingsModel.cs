namespace NaturalnieApp2.Main.MVVM.Models.GlobalSettingsModel
{
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.Main.Interfaces.GlobalSettings;
    using NaturalnieApp2.Main.Interfaces.GlobalSettings.DatabaseSettings;

    public record GlobalSettingsModel : ValidatableBindableRecordBase, IGlobalSettingsProvider
    {
        #region Fields
        private IDatabseSettingsProvider? databaseSettings;
        #endregion

        #region Properties
        public IDatabseSettingsProvider? DatabaseSettings
        {
            get { return databaseSettings; }
            set { SetProperty(ref databaseSettings, value); }
        }
        #endregion

    }
}
