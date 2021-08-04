using Library.BankServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServer.Services
{
    public interface IClientRepository
    {   
        Object connectBankAccount(string CNP, int PIN);
        Object getClientSolds(string clientId);
        Object cashWithdrawal(string bankAccountId, float sum);
        Object cashDeposit(string bankAccountId, float sum);
        Object changePIN(string bankAccountId, int newPIN);
    }
}
