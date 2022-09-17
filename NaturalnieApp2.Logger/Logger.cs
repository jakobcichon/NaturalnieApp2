namespace NaturalnieApp2.Logger
{
    using NaturalnieApp2.SharedInterfaces.Logger;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class Logger : ILogger
    {
        #region Fields
        BlockingCollection<ILoggerEntry> entries = new();
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

    }
}