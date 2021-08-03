using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.BankServer.Entities
{
    public class BankAccount
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(17)")]
        public string BankAccountNumber { get; set; }
        [Required]
        public int PIN { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string CurrencyType { get; set; }
        [Required]
        public float Sold { get; set; }

        public string BankId { get; set; }
        public Bank Bank { get; set; }
        public string ClientId { get; set; }
        public Client Client { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public string AccountTypeId { get; set; }
        public AccountType AccountType { get; set; }
        public ICollection<BankTransferIBAN> BankTransferIBANs { get; set; }
    }
}
