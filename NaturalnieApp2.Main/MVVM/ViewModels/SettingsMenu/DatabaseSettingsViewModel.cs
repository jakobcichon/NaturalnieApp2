namespace NaturalnieApp2.Main.MVVM.ViewModels.SettingsMenu
{
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.Main.MVVM.Models.Settings;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedInterfaces.Xml;
    using System.Threading.Tasks;

    public class DatabaseSettingsViewModel : BaseViewModel, IMenuScreen
    {
        public override string ScreenInfo => "Ustawienia bazy danych";
        public IXmlSerializer? XmlSerializer { get; init; }
        public IModelPresenter ModelPresenter { get; init; }
        private DatabaseSettingsModel? model;

        protected override async Task LoadOperation()
        {
            if (XmlSerializer is not null)
            {
                model = XmlSerializer.Deserialize<DatabaseSettingsModel>(this);
                if (model is null)
                {
                    model = new();
                }
                ;
            }

            await CreateModelPresenter();
        }

        private async Task CreateModelPresenter()
        {
            await ModelPresenter.CreateFromModel(model);
        }
    }
}
