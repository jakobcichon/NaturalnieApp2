namespace NaturalnieApp2.Logger
{
    using NaturalnieApp2.SharedInterfaces.Logger;
    using System;

    public class LoggerEntry : ILoggerEntry
    {
        #region Fields
        private object locker = new();

        private string message;
        private string details;
        private LoggerEntryType entryType;
        private DateTime dateTime;

        #endregion

        public LoggerEntry(string message, LoggerEntryType entryType, string? details = null)
        {
            lock (locker)
            {
                this.message = message;
                this.entryType = entryType;
                this.dateTime = DateTime.Now;
                this.details = details ?? string.Empty;
            }
        }

        #region Properties
        public string Message
        {
            get
            {
                return message;
            }

            private set
            {
                message = value;
            }
        }

        public string Details
        {
            get
            {
                return details;
            }

            private set
            {
                details = value;
            }
        }

        public LoggerEntryType EntryType
        {
            get
            {
                return entryType;
            }

            private set
            {
                entryType = value;
            }
        }

        public DateTime DateTime
        {
            get
            {
                return dateTime;
            }

            private set
            {
                dateTime = value;
            }
        }
        #endregion

        #region Private methods
        #endregion
    }
}
