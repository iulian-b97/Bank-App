using BankServer.Services;
using Library.BankServer.Data;
using Library.BankServer.Entities;
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
        public ActionResult CreateBankAccount(BankAccount model, string accountTypeId)
        {
            _bankingOperatorRepository.createBankAccount(model, accountTypeId);

            return Ok();
        }

        [HttpGet]
        [Route("GetBankAccount")]
        public ActionResult GetBankAccount(string bankAccountId)
        {
            var bankAccount = _bankingOperatorRepository.getBankAccount(bankAccountId);

            return Ok(bankAccount);
        }
    }
}
