namespace NaturalnieApp2.SharedControls.MVVM.ViewModels.WizardDialog
{
    using NaturalnieApp2.Common.Binding;
    using NaturalnieApp2.SharedControls.Interfaces.WizardDialog;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PageChangedArgs
	{
		public object? OldPage;
        public object? NewPage;

        public PageChangedArgs(object? oldPage, object? newPage)
        {
            this.OldPage = oldPage;
            this.NewPage = newPage;
        }
    }

    public class WizardDialogViewModel : BindableBase, IWizardDialog
    {
        #region Fields
        private bool visibility;
        private string headerText = "";
        private object? page;
        private readonly List<object> pages = new();
        private object? previousPage;
        private Dictionary<object, object?> pagesRelations = new();
        #endregion

        public EventHandler<PageChangedArgs> PageChangedHandler { get; set; } = delegate { };

        #region Properties
        public bool IsOpened { get; private set; }
        public bool IsClosed
        {
            get
            {
                return !IsOpened;
            }
        }
        public bool Visibility
        {
            get { return visibility; }
            set { SetProperty(ref visibility, value); }
        }

        public string HeaderText
        {
            get { return headerText; }
            set { SetProperty(ref headerText, value); }
        }

        public object? Page
        {
            get { return page; }
            set
            {
                previousPage = page;
                SetProperty(ref page, value);
                OnPageChange(previousPage, value);
            }
        }
        #endregion

        #region Public methods
        public void GoToNextPage()
        {
            if(Page != null && pagesRelations.ContainsKey(Page))
            {
                Page = pagesRelations[Page];
            }
        }

        public void GoToPage(object page)
        {
            object? demandPage = pages.Where(p => p.Equals(page)).FirstOrDefault();

            if (demandPage == null)
            {
                throw new ArgumentException("Podana strona nie istnieje na liście stron. Dodaj stronę i spróbuj ponownie");
            }

            Page = demandPage;
        }

        public void GoToPreviousPage()
        {
            if (previousPage != null)
            {
                Page = previousPage;
            }
        }

        public void AddPage(object page, object? nextPage = null)
        {
            pagesRelations[page] = nextPage;
            pages.Add(page);
        }

        public void Close()
        {
            Visibility = false;
            Page = null;
            IsOpened = false;
        }

        public void Open()
        {
            Visibility = true;
            Page = pages.First();
            IsOpened = true;
        }
        #endregion

        #region Private methods
        private void OnPageChange(object? oldPage, object? newPage)
        {
            PageChangedHandler?.Invoke(this, new PageChangedArgs(oldPage, newPage));
        }
        #endregion

    }
}
