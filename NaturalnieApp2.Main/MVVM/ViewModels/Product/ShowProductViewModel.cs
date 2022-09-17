namespace NaturalnieApp2.Main.MVVM.ViewModels.Product
{
    using NaturalnieApp2.Main.Interfaces.Screens;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ShowProductViewModel : BaseViewModel, IMenuScreen
    {
        #region Properties
        public override string ScreenInfo => "Informacje o produkcie";
        #endregion

        #region Public methods
        public void Load()
        {
            _ = LoadOperation();
        }

        public async Task LoadAsync()
        {
            await LoadOperation();
        }
        #endregion

        #region Private methods
        private async Task LoadOperation()
        {
            await Task.Delay(1000);
            for (int i = 0; i < 1000; i++)
            {
                string text = " have a TextBlock in WPF. I write many lines to it, far exceeding its vertical height. I expected a vertical scroll bar to appear automatically when that happens, but it didn't. I tried to look for a scroll bar property in the Properties pane, but could not find one." +
                                "How can I make vertical scroll bar created automatically for my TextBlock once its contents exceed its height ?" +
                                    "Clarification : I would rather do it from the designer and not by directly writing to the XAML. This makes sure that the text in your textblock does not overflow and overlap the elements below the textblock as may be the case if you do not use the grid. That happened to me when I tried other solutions even though the textblock was already in a grid with other elements. Keep in mind that the width of the textblock should be Auto and you should specify the desired with in the Grid element. I did this in my code and it works beautifully. HTH.";
                DialogBox?.Show($"{text + i}").AddAction(DialogBoxResults.Cancel, OnUserPressedOk);
            }

            ;
        }

        private void OnUserPressedOk()
        {
            ;
        }
        #endregion
    }
}
