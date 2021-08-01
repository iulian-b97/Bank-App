using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.BankServer.Entities
{
    public class AccountType
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string OfferType { get; set; }
        [Required]
        public int Commission { get; set; }

        public BankAccount BankAccount { get; set; }
    }
}
