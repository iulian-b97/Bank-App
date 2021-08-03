using Library.BankServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServer.Services
{
    public interface IBankingOperatorRepository
    {
        AccountType getAccountType(string accountTypeId);
        void createBankAccount(BankAccount model, string accountTypeId);
        Object getBankAccount(string bankAccountId);
    }
}
