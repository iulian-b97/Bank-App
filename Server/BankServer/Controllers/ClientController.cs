using BankServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServer.Controllers
{

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }


        [HttpGet]
        [Route("ConnectBankAccount")]
        public ActionResult ConnectBankAccount(string CNP, int PIN)
        {
            var res = _clientRepository.connectBankAccount(CNP, PIN);

            return Ok(res);
        }

        [HttpGet]
        [Route("GetClientSolds")]
        public ActionResult GetClientSolds(string clientId)
        {
            var res = _clientRepository.getClientSolds(clientId);

            return Ok(res);
        }

        [HttpPost]
        [Route("CashWithdrawal")]
        public ActionResult CashWithdrawal(string bankAccountId, float sum)
        {
            var res = _clientRepository.cashWithdrawal(bankAccountId, sum);

            return Ok(res);
        }

        [HttpPost]
        [Route("CashDeposit")]
        public ActionResult CashDeposit(string bankAccountId, float sum)
        {
            var res = _clientRepository.cashDeposit(bankAccountId, sum);

            return Ok(res);
        }

        [HttpPost]
        [Route("ChangePIN")]
        public ActionResult ChangePIN(string bankAccountId, int newPIN)
        {
            var res = _clientRepository.changePIN(bankAccountId, newPIN);

            return Ok(res);
        }
    }
}
