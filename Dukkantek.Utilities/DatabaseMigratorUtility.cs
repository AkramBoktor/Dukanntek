using Dukkantek.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Dukkantek.Data;
using Dukkantek.Domain.Repositories;

namespace Dukkantek.Utilities
{
    public class DatabaseMigratorUtility
    {
        public static void MigrateDbContexts(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var DukkantekContext = (ApplicationDbContext)serviceProvider.GetService(typeof(ApplicationDbContext));

            var userManager = (IUserManagerRepository)serviceProvider.GetService(typeof(IUserManagerRepository));
        
        }
    }
}
