using ECommerceAPI.Entities;
using ECommerceAPI.Entities.Complex;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.DataAccess
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext()
        {
        }

        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property<decimal>(p => p.UnitPrice)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Sale>()
                .Property(p => p.TotalSale)
                .HasPrecision(8, 2)
                .IsRequired();

            modelBuilder.Entity<SaleDetail>()
                .Property(p => p.UnitPrice)
                .HasPrecision(8, 2)
                .IsRequired();

            modelBuilder.Entity<SaleDetail>()
                .Property(p => p.Quantity)
                .HasPrecision(8, 2)
                .IsRequired();

            modelBuilder.Entity<SaleDetail>()
                .Property(p => p.Total)
                .HasPrecision(8, 2)
                .IsRequired();

            #region Datos para Store Procedures

            modelBuilder.Entity<InvoiceDetailInfo>()
                .HasNoKey();

            modelBuilder.Entity<InvoiceDetailInfo>()
                .Property(p => p.UnitPrice)
                .HasPrecision(8, 2);

            modelBuilder.Entity<InvoiceDetailInfo>()
                .Property(p => p.Quantity)
                .HasPrecision(8, 2);

            modelBuilder.Entity<InvoiceDetailInfo>()
                .Property(p => p.Total)
                .HasPrecision(8, 2);

            modelBuilder.Entity<ReportByMonthInfo>()
                .HasNoKey();

            modelBuilder.Entity<ReportByMonthInfo>()
                .Property(p => p.TotalSales)
                .HasPrecision(8, 2);

            #endregion
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
