using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.BankServer.Entities
{
    public class Deposit
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public float Sum { get; set; }

        public Transaction Transaction { get; set; }
    }
}
