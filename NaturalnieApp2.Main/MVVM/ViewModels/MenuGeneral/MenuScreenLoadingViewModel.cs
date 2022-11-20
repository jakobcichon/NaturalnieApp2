namespace NaturalnieApp2.Main.MVVM.ViewModels.MenuGeneral
{
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MenuScreenLoadingViewModel : BaseViewModel, IMenuScreen
    {
        #region Fields
        private readonly string additionalInfo;
        #endregion

        #region Constructor
        public MenuScreenLoadingViewModel(string screenInfo)
        {
            additionalInfo = screenInfo;
        }
        #endregion

        #region Properties
        public override string ScreenInfo => $"Ładowanie ekranu \"{additionalInfo}\"";

        public override bool ShowScreenInfo => false;

        public bool IsInitialized => true;
        #endregion

        #region Public methods
        public void Load()
        {
            
        }

        public Task LoadAsync()
        {
            return Task.CompletedTask;
        }
        #endregion
    }
}
