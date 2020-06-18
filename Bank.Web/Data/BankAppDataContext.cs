using Bank.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bank.Web.Data
{
    public partial class BankAppDataContext : IdentityDbContext<BankUser>
    {
        public BankAppDataContext()
        {
        }

        public BankAppDataContext(DbContextOptions<BankAppDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Cards> Cards { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Dispositions> Dispositions { get; set; }
        public virtual DbSet<Loans> Loans { get; set; }
        public virtual DbSet<PermenentOrder> PermenentOrder { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Transactions>().HasIndex(x => x.AccountId);
            builder.Entity<Customers>().Property(x => x.CustomerId).ValueGeneratedNever();
        }
    }
}
