using BankServiceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BankServiceApi.Data
{
    public class BankServiceContext : DbContext
    {
        public BankServiceContext(DbContextOptions<BankServiceContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TransactionType>()
                .HasData(
                    new TransactionType{
                        Id = 1,
                        TypeName = "Debit"
                    },
                    new TransactionType{
                        Id = 2,
                        TypeName = "Credit"
                    }
                );            
        }
        
        public DbSet<BankMaster> BankMasters { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set;} 
        public DbSet<BankLedger> BankLedgers { get; set; }
    }
}