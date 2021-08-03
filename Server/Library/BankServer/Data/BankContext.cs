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
                .WithOne(y => y.Bank)
                .HasForeignKey(y => y.BankId);

            //Bank - Client (one to many)
            modelBuilder.Entity<Bank>()
                .HasMany(x => x.Clients)
                .WithOne(y => y.Bank)
                .HasForeignKey(y => y.BankId);

            //Bank - BankAccount (one to many)
            modelBuilder.Entity<Bank>()
                .HasMany(x => x.BankAccounts)
                .WithOne(y => y.Bank)
                .HasForeignKey(y => y.BankId);

            //Bank - BankTransferIBAN (one to many)
            modelBuilder.Entity<Bank>()
                .HasMany(x => x.BankTransferIBANs)
                .WithOne(y => y.Bank)
                .HasForeignKey(y => y.BankId);

            //BankingOperator - BankTransferIBAN (one to many)
            modelBuilder.Entity<BankingOperator>()
                .HasMany(x => x.BankTransferIBANs)
                .WithOne(y => y.BankingOperator)
                .HasForeignKey(y => y.BankingOperatorId);

            //Client - BankAccount (one to many)
            modelBuilder.Entity<Client>()
                .HasMany(x => x.BankAccounts)
                .WithOne(y => y.Client)
                .HasForeignKey(y => y.ClientId);

            //BankAccount - Transaction (one to many)
            modelBuilder.Entity<BankAccount>()
                .HasMany(x => x.Transactions)
                .WithOne(y => y.BankAccount)
                .HasForeignKey(y => y.BankAccountId);

            //AccountType - BankAccount (one to many)
            modelBuilder.Entity<AccountType>()
                .HasMany(x => x.BankAccounts)
                .WithOne(y => y.AccountType)
                .HasForeignKey(y => y.AccountTypeId);

            //BankAccount - BankTransferIBAN (one to many)
            modelBuilder.Entity<BankAccount>()
                .HasMany(x => x.BankTransferIBANs)
                .WithOne(y => y.BankAccount)
                .HasForeignKey(y => y.BankAccountId);

            //Deposit - Transaction (one to many)
            modelBuilder.Entity<Deposit>()
                .HasMany(x => x.Transactions)
                .WithOne(y => y.Deposit)
                .HasForeignKey(y => y.DepositId);

            //Withdrawal - Transaction (one to many)
            modelBuilder.Entity<Withdrawal>()
                .HasMany(x => x.Transactions)
                .WithOne(y => y.Withdrawal)
                .HasForeignKey(y => y.WithdrawalId);
        }
    }
}
