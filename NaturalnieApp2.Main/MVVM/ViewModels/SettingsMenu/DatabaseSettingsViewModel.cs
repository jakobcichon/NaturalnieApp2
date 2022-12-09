namespace NaturalnieApp2.Main.MVVM.ViewModels.SettingsMenu
{
    using global::Main.MVVM.Models.GlobalSettingsModel.DatabaseSettings.DatabaseSettingsModel;
    using NaturalnieApp2.Main.Interfaces.GlobalSettings.DatabaseSettings;
    using NaturalnieApp2.Main.Interfaces.Model;
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.SharedControls.Interfaces.ModelPresenter;
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.ButtonsPanel;
    using NaturalnieApp2.SharedInterfaces.Xml;
    using System;
    using System.Threading.Tasks;

    public class DatabaseSettingsViewModel : BaseViewModel, IMenuScreen
    {
        private const string saveButtonName = "Zapisz";

        public override string ScreenInfo => "Ustawienia bazy danych";
        public IXmlSerializer? XmlSerializer { get; init; }
        public IModelPresenter ModelPresenter { get; init; }
        public IModel? Model { get; set; }
        public ButtonsPanelViewModel ButtonsPanel { get; init; }
        public bool IsInitialized { get; private set; }

        public DatabaseSettingsViewModel()
        {
            ButtonsPanel = new ButtonsPanelViewModel();
            ButtonsPanel.AddRightButton(saveButtonName, OnSaveClick, (a) => true);
        }

        protected override async Task LoadOperation()
        {
            if(!IsInitialized)
            {
                if (XmlSerializer is not null)
                {
                    Model = XmlSerializer.Deserialize<IDatabaseConnectionSettingsProvider>(this);
                    Model ??= new DatabaseConnectionSettingsModel();
                }

                await CreateModelPresenter();
            }

            IsInitialized = true;

        }

        private async Task CreateModelPresenter()
        {
            if(Model is null)
            {
                return;
            }

            await ModelPresenter.CreateFromModel(Model);
        }

        private void OnSaveClick(object? obj)
        {
            if (Model is null )
            {
                return;
            }

            if (!Model!.IsValid)
            {
                DialogBox?.Show("Nie można zapisać modelu, gdyż posiada on błędy!");
            }
        }

    }
}
