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


        public AccountType getAccountType(string accountTypeId)
        {
            AccountType accountType = _bankContext.AccountTypes.FirstOrDefault(x => x.Id.Equals(accountTypeId));

            return accountType;
        }

        public void createBankAccount(BankAccount model, string accountTypeId)
        {
            BankAccount bankAccount = new BankAccount
            {
                BankAccountNumber = model.BankAccountNumber,
                PIN = model.PIN,
                CurrencyType = model.CurrencyType,
                Sold = model.Sold
            };

            bankAccount.Id = Guid.NewGuid().ToString();
            bankAccount.AccountTypeId = accountTypeId;

            _bankContext.BankAccounts.Add(bankAccount);
            _bankContext.SaveChanges();
        }

        public Object getBankAccount(string bankAccountId)
        {

            /*var query = (from x in _bankContext.AccountTypes
                            join y in _bankContext.BankAccounts
                            on x.Id equals y.AccountTypeId
                            select new
                            {
                                Id = x.Id,
                                OfferType = x.OfferType,
                                Commission = x.Commission
                            }).ToList(); */

            var query = _bankContext.BankAccounts
                            .Where(z => z.Id.Equals(bankAccountId))
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
    }
}
