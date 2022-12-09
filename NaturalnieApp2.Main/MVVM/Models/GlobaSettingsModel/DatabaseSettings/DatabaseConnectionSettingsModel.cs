namespace Main.MVVM.Models.GlobalSettingsModel.DatabaseSettings.DatabaseSettingsModel;

using NaturalnieApp2.Common.Attributes.DisplayableModel;
using NaturalnieApp2.Common.Attributes.ValidationRules;
using NaturalnieApp2.Main.Interfaces.GlobalSettings.DatabaseSettings;
using NaturalnieApp2.Main.MVVM.Models;

internal record DatabaseConnectionSettingsModel : ModelBase, IDatabaseConnectionSettingsProvider
{
    #region Fields
    private string connectionString;
    #endregion

    #region Properties
    [DisplayableName("Ścieżka połączenia")]
    [StringLengthCustom(4)]
    public string ConnectionString
    {
        get { return connectionString; }
        set { SetProperty(ref connectionString, value); }
    }
    #endregion

}
