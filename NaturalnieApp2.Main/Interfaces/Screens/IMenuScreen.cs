namespace NaturalnieApp2.Main.Interfaces.Screens
{
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Threading.Tasks;

    internal interface IMenuScreen: IDisposable
    {
        public EventHandler CloseRequestHandler { get; set; }

        public string ScreenInfo { get; }
        public bool ShowScreenInfo { get; }
        public IDialogBox? DialogBox { get; }
        public bool IsInitialized { get; }

        public void Load();
        public Task LoadAsync();

    }
}
