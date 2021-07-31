using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.IdentityServer.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(25)")]
        public string CNP { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Address { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Role { get; set; }

    }
}
