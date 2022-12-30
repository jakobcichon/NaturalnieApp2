namespace NaturalnieApp2.Database.Commands
{
    using NaturalnieApp2.Common.Disposable;
    using System.Data.Entity;

    public class DbCommandBase: DisposableBase
    {
        public string ConnectionString { get; private set; }
        protected ShopContext dbContext;

        public DbCommandBase(string connectionString)
        {
            ConnectionString = connectionString;
            dbContext = new ShopContext(connectionString);
        }

        #region Disposable
        private bool _disposedValue;
        protected override void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    dbContext.Dispose();
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
