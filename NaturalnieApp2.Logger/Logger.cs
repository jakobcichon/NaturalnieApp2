namespace NaturalnieApp2.Logger
{
    using NaturalnieApp2.Common.Disposable;
    using NaturalnieApp2.SharedInterfaces.Logger;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class Logger : DisposableBase, ILogger
    {
        #region Fields
        object locker = new object();
        readonly BlockingCollection<ILoggerEntry> entries = new();
        #endregion

        #region Public methods
        public void Debug(string message)
        {
            AddEntry(message, LoggerEntryType.Debug);
        }

        public void Error(string message)
        {
            AddEntry(message, LoggerEntryType.Error);
        }

        public void Exception(string message)
        {
            AddEntry(message, LoggerEntryType.Exception);
        }

        public void Info(string message)
        {
            AddEntry(message, LoggerEntryType.Info);
        }

        public void UiAction(string message)
        {
            AddEntry(message, LoggerEntryType.UiAction);
        }

        public void UiAction(string uiElementName, string message)
        {
            AddEntry(message, LoggerEntryType.UiAction);
        }

        public void Warn(string message)
        {
            AddEntry(message, LoggerEntryType.Warn);
        }
        public List<ILoggerEntry> GetLoggerEntries()
        {
            return entries.ToList();
        }
        #endregion

        #region Private methods
        private void AddEntry(string message, LoggerEntryType entryType)
        {
            entries.Add(new LoggerEntry(message, entryType));

        }
        #endregion

        #region Disposable
        private bool _disposedValue;
        protected override void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).
                    foreach(ILoggerEntry element in entries)
                    {
                        element.Dispose();
                    }    
                    entries.Dispose();
                }

                // Free unmanaged resources (unmanaged objects) and override a finalizer below.
                // Set large fields to null.
                _disposedValue = true;
            }

            // Call the base class implementation.
            base.Dispose(disposing);
        }
        #endregion

    }
}