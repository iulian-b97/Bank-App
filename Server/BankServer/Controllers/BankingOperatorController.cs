using BankServer.Services;
using Library.BankServer.Data;
using Library.BankServer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingOperatorController : ControllerBase
    {
        private readonly IBankingOperatorRepository _bankingOperatorRepository;

        public BankingOperatorController(IBankingOperatorRepository bankingOperatorRepository)
        {
            _bankingOperatorRepository = bankingOperatorRepository;
        }


        [HttpPost]
        [Route("CreateBankAccount")]
        public ActionResult CreateBankAccount(BankAccount model, string bankId, string clientId, string accountTypeId)
        {
            bool success = _bankingOperatorRepository.createBankAccount(model, bankId, clientId, accountTypeId);

            if (success)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("GetBankAccount")]
        public ActionResult GetBankAccount(string bankAccountId)
        {
            var bankAccount = _bankingOperatorRepository.getBankAccount(bankAccountId);

            return Ok(bankAccount);
        }

        [HttpGet]
        [Route("GetClients")]
        public ActionResult GetClients(string bankId)
        {
            var bankAccount = _bankingOperatorRepository.getClients(bankId);

            return Ok(bankAccount);
        }

        [HttpDelete]
        [Route("DeleteClient")]
        public ActionResult DeleteClient(string clientId)
        {
            _bankingOperatorRepository.deleteClientAccount(clientId);

            return Ok();
        }

        [HttpPost]
        [Route("TransferIBAN")]
        public ActionResult TransferIBAN(BankTransferIBAN model, string bankId, string bankAccountId)
        {
            var res = _bankingOperatorRepository.transferIBAN(model, bankId, bankAccountId);

            return Ok(res);
        }
    }
}
