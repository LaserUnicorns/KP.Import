using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using KP.Import.Common.Contracts;

namespace KP.Import.DA.Sql
{
    public class KpImportContext : DbContext
    {
        public KpImportContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Appartment> Appartments { get; set; }
        public DbSet<Reading> Readings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Appartment>()
                        .HasKey(x => x.AccountNumber)
                        .Property(x => x.AccountNumber).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Reading>().Property(x => x.Value).HasPrecision(10, 3);
        }
    }
}