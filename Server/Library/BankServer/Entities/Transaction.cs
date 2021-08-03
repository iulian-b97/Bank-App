using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.BankServer.Entities
{
    public class Transaction
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public bool TransactionType { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public string BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
        public string DepositId { get; set; }
        public Deposit Deposit { get; set; }
        public string WithdrawalId { get; set; }
        public Withdrawal Withdrawal { get; set; }
    }
}
