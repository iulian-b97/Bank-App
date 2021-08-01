using Library.BankServer.Data;
using Library.BankServer.Entities;
using Library.IdentityServer.Data;
using Microsoft.AspNetCore.Hosting;
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
                            PhoneNumber = user.PhoneNumber
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
                            PhoneNumber = user.PhoneNumber
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
