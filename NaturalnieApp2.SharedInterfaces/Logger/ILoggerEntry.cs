namespace NaturalnieApp2.SharedInterfaces.Logger
{
    using System;

    public interface ILoggerEntry
    {
        string Message { get; }
        string Details { get; }
        LoggerEntryType EntryType { get; }
        DateTime DateTime { get; }

        public void Dispose();
    }
}
