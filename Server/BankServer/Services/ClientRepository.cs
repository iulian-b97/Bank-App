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

    }
}
