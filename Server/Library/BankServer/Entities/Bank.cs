using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.BankServer.Entities
{
    public class Bank
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(3)")]
        public string CountryCode { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(3)")]
        public string CountrolDigits { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(5)")]
        public string BankCode { get; set; }

        public ICollection<BankingOperator> BankingOperators { get; set; }
        public ICollection<Client> Clients { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; }
        public ICollection<BankTransferIBAN> BankTransferIBANs { get; set; }
    }
}
