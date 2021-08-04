using Library.BankServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServer.Services
{
    public interface IBankingOperatorRepository
    {
        bool createBankAccount(BankAccount model, string bankId, string clientId, string accountTypeId);
        Object getBankAccount(string bankAccountId);
        Object getClients(string bankId);
        void deleteClientAccount(string clientId);
        Object transferIBAN(BankTransferIBAN model, string bankId, string bankAccountId);
    }
}
