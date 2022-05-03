using Microsoft.EntityFrameworkCore;
using StockApi.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApi.Repository
{
    public class StockContext : DbContext
    {

        public DbSet<Broker> Brokers { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Transaction> Transactions { get; set; }


        public StockContext(DbContextOptions<StockContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Broker>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).IsRequired();
            });

            modelBuilder.Entity<Stock>(s =>
            {
                s.HasKey(p => p.Id);
                s.Property(p => p.Symbol).HasMaxLength(5).IsRequired();
                s.Property(p => p.Name).IsRequired();
            });

            modelBuilder.Entity<Transaction>(t =>
            {
                t.HasKey(p => p.Id);
                t.Property(p => p.PriceGbp).IsRequired();
                t.Property(p => p.Quantity).IsRequired();
                t.Property(p => p.TransactionDate).HasDefaultValueSql("GETDATE()").IsRequired();
                t.HasOne(p => p.Broker).WithMany().IsRequired();
                t.HasOne(p => p.Stock).WithMany().IsRequired();
            });

        }
    }
}
