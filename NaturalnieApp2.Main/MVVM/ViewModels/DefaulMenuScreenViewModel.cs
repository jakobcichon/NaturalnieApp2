namespace NaturalnieApp2.Main.MVVM.ViewModels
{
    using NaturalnieApp2.Common.Disposable;
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    internal class DefaulMenuScreenViewModel : DisposableBase, IMenuScreen
    {
        public string ScreenInfo => String.Empty;

        public bool ShowScreenInfo => false;

        public IDialogBox? DialogBox => null;

        public bool IsInitialized => true;

        public EventHandler CloseRequestHandler { get; set; } = delegate { }!;

        public void Load()
        {
            ;
        }

        public Task LoadAsync()
        {
            return Task.CompletedTask;
        }
    }
}
