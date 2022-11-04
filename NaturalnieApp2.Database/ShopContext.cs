namespace NaturalnieApp2.Database
{
    using MySql.Data.EntityFramework;
    using NaturalnieApp2.Database.Models;
    using System.Data.Common;
    using System.Data.Entity;

    // Code-Based Configuration and Dependency resolution
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ShopContext : DbContext
    {
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ProductChangelogModel> ProductsChangelog { get; set; }
        public DbSet<SaleModel> Sales { get; set; }
        public DbSet<StockModel> Stock { get; set; }
        public DbSet<StockHistoryModel> StockHistory { get; set; }
        public DbSet<SupplierModel> Suppliers { get; set; }
        public DbSet<ManufacturerModel> Manufacturers { get; set; }
        public DbSet<TaxModel> Tax { get; set; }
        public DbSet<ElzabCommunicationModel> ElzabCommunication { get; set; }
        public DbSet<InventoryModel> Inventory { get; set; }
        public DbSet<InventoryHistoryModel> InventoryHistory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configures one-to-many relationship
            modelBuilder.Entity<ProductModel>()
                .HasRequired(p => p.Tax)
                .WithMany(x => x.Products)
                .HasForeignKey(p => p.TaxId);

            modelBuilder.Entity<ProductModel>()
                .HasRequired(p => p.Supplier)
                .WithMany(x => x.Products)
                .HasForeignKey(p => p.SupplierId);

            modelBuilder.Entity<ProductModel>()
                .HasRequired(p => p.Manufacturer)
                .WithMany(x => x.Products)
                .HasForeignKey(p => p.ManufacturerId);
        }

        public ShopContext()
            : base("shop")
        {
            CommonSettings();
        }

        public ShopContext(string connectionString)
            : base(connectionString)
        {
            CommonSettings();
        }
        // Constructor to use on a DbConnection that is already opened
        public ShopContext(DbConnection existingConnection, bool contextOwnsConnection)
          : base(existingConnection, contextOwnsConnection)
        {
            CommonSettings();
        }

        private void CommonSettings()
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}
