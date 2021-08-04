using Library.BankServer.Data;
using Library.BankServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServer.Services
{
    public class ClientRepository : IClientRepository
    {
        private readonly BankContext _bankContext;

        public ClientRepository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }
        

        public Object connectBankAccount(string CNP, int PIN)
        {
            var getClientByCNP = _bankContext.BankAccounts
                                    .Where(a => a.Client.CNP.Equals(CNP))
                                    .Where(b => b.PIN == PIN)
                                    .Join(_bankContext.Clients,
                                            x => x.ClientId,
                                            y => y.Id,
                                            (x, y) => new
                                            {
                                                y.CNP,
                                                x.PIN,
                                                x.BankAccountNumber,
                                                x.CurrencyType,
                                                x.Sold
                                            });

            return getClientByCNP;
        }

        public Object getClientSolds(string clientId)
        {
            var getSolds = _bankContext.BankAccounts
                               .Where(a => a.ClientId.Equals(clientId))
                               .Join(_bankContext.Clients,
                                        x => x.ClientId,
                                        y => y.Id,
                                        (x, y) => new
                                        {
                                            x.BankAccountNumber,
                                            x.Sold,
                                            x.CurrencyType
                                        }).ToList();

            return getSolds;
        }

        public object cashWithdrawal(string bankAccountId, float sum)
        {
            Withdrawal newWithdrawal = new Withdrawal
            {
                Sum = sum
            };
            newWithdrawal.Id = Guid.NewGuid().ToString();

            Transaction newTransaction = new Transaction
            {
                TransactionType = false
            };
            newTransaction.Id = Guid.NewGuid().ToString();
            newTransaction.Date = DateTime.Now;
            newTransaction.BankAccountId = bankAccountId;
            newTransaction.WithdrawalId = newWithdrawal.Id;

            var bankAccount = _bankContext.BankAccounts.FirstOrDefault(x => x.Id.Equals(bankAccountId));
            bankAccount.Sold -= sum;

            _bankContext.SaveChanges();

            return bankAccount;
        }

        public object cashDeposit(string bankAccountId, float sum)
        {
            Deposit newDeposit = new Deposit
            {
                Sum = sum
            };
            newDeposit.Id = Guid.NewGuid().ToString();

            Transaction newTransaction = new Transaction
            {
                TransactionType = false
            };
            newTransaction.Id = Guid.NewGuid().ToString();
            newTransaction.Date = DateTime.Now;
            newTransaction.BankAccountId = bankAccountId;
            newTransaction.WithdrawalId = newDeposit.Id;

            var bankAccount = _bankContext.BankAccounts.FirstOrDefault(x => x.Id.Equals(bankAccountId));
            bankAccount.Sold += sum;

            _bankContext.SaveChanges();

            return bankAccount;
        }

        public object changePIN(string bankAccountId, int newPIN)
        {
            var bankAccount = _bankContext.BankAccounts.FirstOrDefault(x => x.Id.Equals(bankAccountId));
            bankAccount.PIN = newPIN;

            _bankContext.SaveChanges();

            return bankAccount;
        }
    }
}
