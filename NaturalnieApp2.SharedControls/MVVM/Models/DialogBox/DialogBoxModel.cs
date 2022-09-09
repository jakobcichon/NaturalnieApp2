namespace NaturalnieApp2.SharedControls.MVVM.Models.MessageBox
{
    using NaturalnieApp2.SharedControls.MVVM.ViewModels.DialogBox;
    using NaturalnieApp2.SharedControls.MVVM.Views.DialogBox;
    using NaturalnieApp2.SharedInterfaces.DialogBox;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Threading;

    public class DialogBoxModel
    {
        public bool? Show(string message)
        {
            return CreateWindow();
        }

        private bool? CreateWindow()
        {
            bool? decision = null;
            Application.Current.Dispatcher.Invoke(() =>
           {
               Window dialogWindow = new DialogBoxMainWindow();
               DialogBoxYesNoViewModel dialogBoxViewModel = new();

               dialogWindow.DataContext = dialogBoxViewModel;
               dialogWindow.ShowDialog();


           });

            return decision;

        }
    }
}
