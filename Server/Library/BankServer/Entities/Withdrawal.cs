using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.BankServer.Entities
{
    public class Withdrawal
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public float Sum { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
