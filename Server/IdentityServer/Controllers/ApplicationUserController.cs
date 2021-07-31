using IdentityServer.Services;
using Library.IdentityServer.Entities;
using Library.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _appSettings;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                                         IOptions<ApplicationSettings> appSettings, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _roleManager = roleManager;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<Object> PostApplicationUser(ApplicationUserModel model)
        {
            if(!await _roleManager.RoleExistsAsync(model.Role))
            {
                return Ok("Role does not exist");
            }

            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CNP = model.CNP,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Role = model.Role
            };

            var result = await _userManager.CreateAsync(applicationUser, model.Password);

            if(result.Succeeded)
            {
                var tempUser = await _userManager.FindByEmailAsync(model.Email);
                await _userManager.AddToRoleAsync(tempUser, model.Role);
            }

            return Ok(result);
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (String.IsNullOrEmpty(model.UserName))
                throw new ArgumentNullException(nameof(model.UserName));

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ApplicationSettings:JWT_Secret")), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logout successfull");
        }

        /*
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            ICollection<Library.Server.Models.User> Users = new List<Library.Server.Models.User>();

            var allUsers = _userRepository.GetUsers();

            if (allUsers == null)
                return Ok("Users not found.");

            Users = allUsers.Select(x => new Library.Server.Models.User { Id = x.Id, Name = x.UserName }).ToList();

            return Ok(Users);
        } */
    }
}
