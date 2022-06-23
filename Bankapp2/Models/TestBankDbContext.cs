using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace Bankapp2.Models
{
    public class TestBankDbContext : DbContext
    {
        public TestBankDbContext() : base("TestBankCustom") { }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ZipCode> ZipCodes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<CustAccount> CustAccounts { get; set; }
        public DbSet<SavingAccount> SavingAccounts { get; set; }
        public DbSet<LoanAccount> LoanAccounts { get; set; }
        public DbSet<SavingAccTxn> SavingAccTxns { get; set; }
        public DbSet<LoanAccTxn> LoanAccTxns { get; set; }
        public DbSet<LoanDuration> LoanDurations { get; set; } //new class
        public DbSet<MaritalStatus> MaritalStatuses { get; set; } //new class
    }
}