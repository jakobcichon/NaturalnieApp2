namespace NaturalnieApp2.Main.Interfaces.Screens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Navigation;

    internal interface IMenuScreen
    {
        public string ScreenInfo { get; }
        public bool ShowScreenInfo { get; }
        public void Load();

        public Task LoadAsync();
    }
}
