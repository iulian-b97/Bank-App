using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.BankServer.Entities
{
    public class BankTransferIBAN
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string IBAN { get; set; }
        [Required]
        public float Sum { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public string BankId { get; set; }
        public Bank Bank { get; set; }
        public string BankingOperatorId { get; set; }
        public BankingOperator BankingOperator { get; set; }
        public string BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
