using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.IdentityServer.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string CNP { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Address { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Role { get; set; }

    }
}
