namespace NaturalnieApp2.Database.Commands
{
    using System.Data.Entity;

    public class CommandBase
    {
        public string ConnectionString { get; private set; }
        protected ShopContext dbContext;

        public CommandBase(string connectionString)
        {
            ConnectionString = connectionString;
            dbContext = new ShopContext(connectionString);
        }
    }
}
