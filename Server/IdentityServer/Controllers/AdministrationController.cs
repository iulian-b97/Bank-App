using Library.IdentityServer.Entities;
using Library.IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdministrationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole()
        {
            var client = await _roleManager.CreateAsync(new IdentityRole("Client"));
            var bankingOperator = await _roleManager.CreateAsync(new IdentityRole("Operator Bancar"));

            return Ok();
        }


        /*[HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole()
        {
            ApplicationUser user = new ApplicationUser
            {
                Id= "8be8c188-6ec0-4e97-96f2-43449938a5b7",
                UserName = "admin",
                PasswordHash = "admin",
                Email = "admin@gmail.com",
                FirstName = "admin",
                LastName = "admin",
                CNP = "admin",
                Address = "admin",
                Role = "Banking Operator"
            };

            var result = await _userManager.AddToRoleAsync(user, "Banking Operator");

            return Ok(result);
        }*/
    }
}
