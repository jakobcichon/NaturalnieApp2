using NaturalnieApp2.SharedControls.MVVM.ViewModels.WizardDialog;

namespace NaturalnieApp2.SharedControls.Interfaces.WizardDialog
{
    using System;

    public interface IWizardDialog: IDisposable
    {
        string HeaderText { get; set; }
        object? Page { get; set; }
        bool Visibility { get; set; }
        bool IsClosed { get; }
        bool IsOpened { get; }

        EventHandler<PageChangedArgs> PageChangedHandler { get; set; }

        void AddPage(object page, object? nextPage = null);
        void Close();
        void GoToPreviousPage();
        void GoToPage(object page);
        void Open();
    }
}