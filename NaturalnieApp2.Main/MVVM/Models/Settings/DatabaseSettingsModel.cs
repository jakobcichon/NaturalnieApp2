namespace NaturalnieApp2.Main.MVVM.Models.Settings
{
    using NaturalnieApp2.Common.Attributes.DisplayableModel;
    using NaturalnieApp2.Common.Binding;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot]
    public record DatabaseSettingsModel : ValidatableBindableRecordBase
    {
        private string connectionString;

        [XmlElement]
        [DoNotDisplay]
        public string ConnectionString
        {
            get { return connectionString; }
            set { SetProperty(ref connectionString, value); }
        }

    }
}
