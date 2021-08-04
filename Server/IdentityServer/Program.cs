using Library.BankServer.Data;
using Library.BankServer.Entities;
using Library.IdentityServer.Data;
using Library.IdentityServer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost webHost = CreateHostBuilder(args).Build();

            using (var scope = webHost.Services.CreateScope())
            {
                IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                

                //1.Copy data from UserTable in ClientTable and BankingOperatorTable
                AuthenticationContext authenticationContext = scope.ServiceProvider.GetRequiredService<AuthenticationContext>();
                BankContext bankContext = scope.ServiceProvider.GetRequiredService<BankContext>();

                var clients = new List<Client>();
                var bankingOperators = new List<BankingOperator>();
                foreach (var user in authenticationContext.ApplicationUsers)
                {
                    if (user.Role == "Client")
                    {
                        clients.Add(new Client
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Address = user.Address,
                            CNP = user.CNP,
                            PhoneNumber = user.PhoneNumber,
                            BankId = "92fe6e9b-44f5-43ed-aef7-b1cb131bfc68"
                        });
                    }
                    else if (user.Role == "Operator Bancar")
                    {
                        bankingOperators.Add(new BankingOperator
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Address = user.Address,
                            CNP = user.CNP,
                            PhoneNumber = user.PhoneNumber,
                            BankId = "92fe6e9b-44f5-43ed-aef7-b1cb131bfc68"
                        });
                    }
                }

                var allClients = await bankContext.Clients.AsNoTracking().ToListAsync();
                var allBankingOperators = await bankContext.BankingOperators.AsNoTracking().ToListAsync();

                var resClients = clients.Where(u => !allClients.Any(s => u.Id.Equals(s.Id) && u.UserName.Equals(s.UserName)));
                var resBankingOperators = bankingOperators.Where(u => !allBankingOperators.Any(s => u.Id.Equals(s.Id) && u.UserName.Equals(s.UserName)));

                foreach (var user in resClients)
                {
                    bankContext.Clients.Add(user);
                }

                foreach (var user in resBankingOperators)
                {
                    bankContext.BankingOperators.Add(user);
                }


                //2.Create AccountTypes 
                AccountType accountTypeGold = new AccountType
                {
                    Id = "cb426db8-96e0-4e2d-9477-2e8fb7272a04",
                    OfferType = "Cont Gold",
                    Commission = 0
                };
                var resGold = bankContext.AccountTypes.FirstOrDefault(x => x.Id.Equals(accountTypeGold.Id));

                AccountType accountTypeSilver = new AccountType
                {
                    Id = "225dcadb-74cf-4843-b00a-1b43839ac892",
                    OfferType = "Cont Silver",
                    Commission = 1
                };
                var resSilver = bankContext.AccountTypes.FirstOrDefault(x => x.Id.Equals(accountTypeSilver.Id));

                AccountType accountTypeBasic = new AccountType
                {
                    Id = "1dac8b6e-baa8-4c0b-97ad-b305ebcd24bf",
                    OfferType = "Cont Basic",
                    Commission = 2
                };
                var resBasic = bankContext.AccountTypes.FirstOrDefault(x => x.Id.Equals(accountTypeBasic.Id));

                if ((resGold == null) || (resSilver == null) || (resBasic == null))
                {
                    bankContext.AccountTypes.Add(accountTypeGold);
                    bankContext.AccountTypes.Add(accountTypeSilver);
                    bankContext.AccountTypes.Add(accountTypeBasic);
                }
                

                //3.Create Roles
                RoleManager<IdentityRole> _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                bool existClient = await _roleManager.RoleExistsAsync("Client");
                bool existBankingOperator = await _roleManager.RoleExistsAsync("Operator Bancar");
                if (!existClient || !existBankingOperator)
                {
                    var client = await _roleManager.CreateAsync(new IdentityRole { Id = "3213ef6b-1d6a-4676-941d-45132ceaa022", Name = "Client" });
                    var bankingOperator = await _roleManager.CreateAsync(new IdentityRole { Id = "a064ba53-869f-4a07-9c76-b634ea9eece6", Name = "Operator Bancar" });
                }


                //4.Create Bank
                Bank bank = new Bank
                {
                    Id = "92fe6e9b-44f5-43ed-aef7-b1cb131bfc68",
                    Name = "TestBank",
                    CountryCode = "22",
                    CountrolDigits = "44",
                    BankCode = "6666"
                };
                var resBank = bankContext.Banks.FirstOrDefault(x => x.Id.Equals(bank.Id));

                if (resBank == null)
                {
                    bankContext.Banks.Add(bank);
                }


                await bankContext.SaveChangesAsync();
            }

            await webHost.RunAsync();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
