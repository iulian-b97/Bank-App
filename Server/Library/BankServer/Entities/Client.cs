using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.BankServer.Entities
{
    public class Client
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string UserName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Address { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string CNP { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }

        public string BankId { get; set; }
        public Bank Bank { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; }
    }
}
