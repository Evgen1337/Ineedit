using Identity.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Identity.API.Infrastructure
{
    public class AccountContext : IdentityDbContext<ApplicationUser>
    {
        public const string DefaultSchema = "dbo";

        public AccountContext(DbContextOptions<AccountContext> options) :
            base(options)
        {
        }

        public class AccountContextDesignFactory : IDesignTimeDbContextFactory<AccountContext>
        {
            public AccountContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AccountContext>()
                    .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Ineedit.Accounts;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

                return new AccountContext(optionsBuilder.Options);
            }
        }
    }
}
