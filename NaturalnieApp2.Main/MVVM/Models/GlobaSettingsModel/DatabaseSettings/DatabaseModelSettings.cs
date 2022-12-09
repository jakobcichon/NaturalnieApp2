namespace NaturalnieApp2.Main.MVVM.Models.GlobaSettingsModel.DatabaseSettings
{
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.Main.Interfaces.GlobalSettings;
    using NaturalnieApp2.Main.Interfaces.GlobalSettings.DatabaseSettings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public record DatabaseModelSettings : ValidatableBindableRecordBase, IDatabaseModelSettings
    {
        public static int MaxProductNameLenght { get; } = 39;
    }
}
