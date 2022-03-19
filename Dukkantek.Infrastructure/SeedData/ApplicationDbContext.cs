using Dukkantek.Data;
using Dukkantek.Domain.Entities;
using Dukkantek.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Dukkantek.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        public DbSet<Products> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<UserProduct> UserProduct { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration["ConnectionStrings:SqlConnection"];
            {


                optionsBuilder.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    string commandTimeout = configuration["DbCommandTimeout"];
                    if (string.IsNullOrEmpty(commandTimeout)) commandTimeout = "90";
                    sqlOptions.CommandTimeout(Convert.ToInt32(commandTimeout));
                });

            }



        }
    }
}

