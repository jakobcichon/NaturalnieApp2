namespace NaturalnieApp2.Main.Services.GlobaSettingsService
{
    using NaturalnieApp2.Main.Interfaces.GlobalSettings;
    using NaturalnieApp2.Main.Interfaces.GlobalSettings.DatabaseSettings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GlobalSettingsService : IGlobalVariablesProvider
    {
        public IDatabseSettingsProvider DatabaseSettings
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
