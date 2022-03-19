using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Dukkantek.Common.Enums;
using Dukkantek.Domain.Interfaces.Services;
using Dukkantek.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Dukkantek.Data;
using Dukkantek.Domain.Repositories;

namespace Gezira.Infrastructure.Repositories
{
    public class IdentityManagerRepository : UserManager<ApplicationUser>, IUserManagerRepository
    {
        private readonly ApplicationDbContext _context;

        public IdentityManagerRepository(ApplicationDbContext context, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger)
         : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _context = context;
        }

    
        public override async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return await Users
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
        public override async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
