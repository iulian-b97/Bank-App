using Library.BankServer.Data;
using Library.BankServer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServer.Services
{
    public class BankingOperatorRepository : IBankingOperatorRepository
    {
        private readonly BankContext _bankContext;

        public BankingOperatorRepository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }


        public bool createBankAccount(BankAccount model, string bankId, string clientId, string accountTypeId)
        {
            var existBank = _bankContext.Banks.FirstOrDefault(x => x.Id.Equals(bankId));
            var existClient = _bankContext.Clients.FirstOrDefault(x => x.Id.Equals(clientId));
            var existAccountType = _bankContext.AccountTypes.FirstOrDefault(x => x.Id.Equals(accountTypeId));

            if ((existBank != null) && (clientId != null) && (accountTypeId != null))
            {
                BankAccount bankAccount = new BankAccount
                {
                    BankAccountNumber = model.BankAccountNumber,
                    PIN = model.PIN,
                    CurrencyType = model.CurrencyType,
                    Sold = model.Sold
                };

                bankAccount.Id = Guid.NewGuid().ToString();

                bankAccount.BankId = bankId;
                bankAccount.ClientId = clientId;
                bankAccount.AccountTypeId = accountTypeId;

                _bankContext.BankAccounts.Add(bankAccount);
                _bankContext.SaveChanges();

                return true;
            }

            return false;
        }

        public Object getBankAccount(string bankAccountId)
        {
            var query = _bankContext.BankAccounts
                            .Where(a => a.Id.Equals(bankAccountId))
                            .Join(_bankContext.AccountTypes,
                                    x => x.AccountTypeId,
                                    y => y.Id,
                                    (x, y) => new { 
                                        y.Id,
                                        y.OfferType,
                                        y.Commission
                                    });
            
            return query;
        }

        public object getClients(string bankId)
        {
            var query = _bankContext.Clients.Where(x => x.BankId.Equals(bankId)).OrderBy(y => y.FirstName).ToList();

            return query;
        }

        public void deleteClientAccount(string clientId)
        {
            var query = _bankContext.Clients.FirstOrDefault(x => x.Id.Equals(clientId));

            _bankContext.Clients.Remove(query);
            _bankContext.SaveChanges();
        }

        public object transferIBAN(BankTransferIBAN model, string bankId, string bankAccountId)
        {
            var bank = _bankContext.Banks.FirstOrDefault(x => x.Id.Equals(bankId));
            var bankAccount = _bankContext.BankAccounts.FirstOrDefault(x => x.Id.Equals(bankAccountId));

            BankTransferIBAN bankTransfer = new BankTransferIBAN();
            bankTransfer.Id = Guid.NewGuid().ToString();
            bankTransfer.IBAN = bank.CountryCode.ToString() + bank.CountrolDigits.ToString() + bank.BankCode.ToString() + bankAccount.BankAccountNumber.ToString();
            bankTransfer.Sum = model.Sum;
            bankTransfer.Date = DateTime.Now;

            bankTransfer.BankId = bankId;
            bankTransfer.BankAccountId = bankAccountId;

            bankAccount.Sold += bankTransfer.Sum;

            _bankContext.BankTransferIBANs.Add(bankTransfer);
            _bankContext.SaveChanges();

            return bankTransfer;
        }
    }
}
