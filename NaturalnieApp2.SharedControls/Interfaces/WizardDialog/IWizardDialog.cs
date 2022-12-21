using NaturalnieApp2.SharedControls.MVVM.ViewModels.WizardDialog;

namespace NaturalnieApp2.SharedControls.Interfaces.WizardDialog
{
    using System;

    public interface IWizardDialog
    {
        string HeaderText { get; set; }
        object? Page { get; set; }
        EventHandler<PageChangedArgs> PageChangedHandler { get; set; }
        bool Visibility { get; set; }

        void AddPage(object page);
        void Close();
        void GoToPage(object page);
        void Open();
    }
}