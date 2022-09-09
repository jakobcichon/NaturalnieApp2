namespace NaturalnieApp2.Main.MVVM.ViewModels.Product
{
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ShowProductViewModel : IMenuScreen
    {
        public string ScreenInfo => "Informacje o produkcie";

        public bool ShowScreenInfo => true;

        public IDialogBox? DialogBox { get; set; }

        public void Load()
        {
            _ = LoadOperation();
        }

        public async Task LoadAsync()
        {
            await LoadOperation();
        }

        private async Task LoadOperation()
        {
            await Task.Delay(1000);
        }
    }
}
