using Library.BankServer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BankServer.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {

        }

        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankingOperator> BankingOperators { get; set; }
        public DbSet<BankTransferIBAN> BankTransferIBANs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Primary Keys
            modelBuilder.Entity<AccountType>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Bank>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<BankAccount>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<BankingOperator>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<BankTransferIBAN>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Client>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Deposit>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Transaction>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Withdrawal>()
                .HasKey(x => x.Id);

            //Bank - BankingOperator (one to many)
            modelBuilder.Entity<Bank>()
                .HasMany(x => x.BankingOperators)
                .WithOne(y => y.Bank);

            //Bank - Client (one to many)
            modelBuilder.Entity<Bank>()
                .HasMany(x => x.Clients)
                .WithOne(y => y.Bank);

            //Bank - BankAccount (one to many)
            modelBuilder.Entity<Bank>()
                .HasMany(x => x.BankAccounts)
                .WithOne(y => y.Bank);

            //Bank - BankTransferIBAN (one to many)
            modelBuilder.Entity<Bank>()
                .HasMany(x => x.BankTransferIBANs)
                .WithOne(y => y.Bank);

            //BankingOperator - BankTransferIBAN (one to many)
            modelBuilder.Entity<BankingOperator>()
                .HasMany(x => x.BankTransferIBANs)
                .WithOne(y => y.BankingOperator);

            //Client - BankAccount (one to many)
            modelBuilder.Entity<Client>()
                .HasMany(x => x.BankAccounts)
                .WithOne(y => y.Client);

            //BankAccount - Transaction (one to many)
            modelBuilder.Entity<BankAccount>()
                .HasMany(x => x.Transactions)
                .WithOne(y => y.BankAccount);

            //AccountType - BankAccount (one to one)
            modelBuilder.Entity<AccountType>()
                .HasOne(x => x.BankAccount)
                .WithOne(y => y.AccountType)
                .HasForeignKey<BankAccount>(y => y.AccountTypeId);

            //BankAccount - BankTransferIBAN (one to many)
            modelBuilder.Entity<BankAccount>()
                .HasMany(x => x.BankTransferIBANs)
                .WithOne(y => y.BankAccount);

            //Deposit - Transaction (one to one)
            modelBuilder.Entity<Deposit>()
                .HasOne(x => x.Transaction)
                .WithOne(y => y.Deposit)
                .HasForeignKey<Transaction>(y => y.DepositId);

            //Withdrawal - Transaction (one to one)
            modelBuilder.Entity<Withdrawal>()
                .HasOne(x => x.Transaction)
                .WithOne(y => y.Withdrawal)
                .HasForeignKey<Transaction>(y => y.WithdrawalId);
        }
    }
}
