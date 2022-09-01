namespace NaturalnieApp2.Main.MVVM.ViewModels.MenuGeneral
{
    using NaturalnieApp2.Main.Interfaces.Screens;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MenuScreenLoadingViewModel : IMenuScreen
    {
        public string ScreenInfo => $"Ładowanie ekranu \"{additionalInfo}\"";

        public bool ShowScreenInfo => false;

        private readonly string additionalInfo;

        public MenuScreenLoadingViewModel(string screenInfo)
        {
            additionalInfo = screenInfo;
        }

        public void Load()
        {
            
        }

        public Task LoadAsync()
        {
            return Task.CompletedTask;
        }
    }
}
