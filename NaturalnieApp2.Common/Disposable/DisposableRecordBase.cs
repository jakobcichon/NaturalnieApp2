namespace NaturalnieApp2.Common.Disposable
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public record DisposableRecordBase: IDisposable
    {
        // To detect redundant calls
        private bool _disposedValue;

        ~DisposableRecordBase() => Dispose(false);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects)
                }

                // Free unmanaged resources (unmanaged objects) and override finalizer
                // Set large fields to null
                _disposedValue = true;
            }
        }


        //#region Disposable
        //private bool _disposedValue;
        //protected override void Dispose(bool disposing)
        //{
        //    if (!_disposedValue)
        //    {
        //        if (disposing)
        //        {
        //            // Dispose managed state (managed objects).
        //            foreach (ILoggerEntry element in entries)
        //            {
        //                element.Dispose();
        //            }
        //            entries.Dispose();
        //        }

        //        // Free unmanaged resources (unmanaged objects) and override a finalizer below.
        //        // Set large fields to null.
        //        _disposedValue = true;
        //    }

        //    // Call the base class implementation.
        //    base.Dispose(disposing);
        //}
        //#endregion
    }
}
