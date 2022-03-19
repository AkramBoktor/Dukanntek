
using Dukkantek.Domain.Entities;
using System.Linq;
using Dukkantek.Data;
using Dukkantek.Domain.Repositories;

namespace Dukkantek.Infrastructure.Repositories
{
    public class UserRefreshTokenRepository : GenericRepository<UserRefreshToken, ApplicationDbContext>, IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(ApplicationDbContext context) : base(context)
        {

        }

        public UserRefreshToken GetRefreshToken(string userId)
        {
            return _context.UserRefreshTokens.FirstOrDefault(x => x.UserId == userId);
        }
        public bool IsRefreshTokenExist(string userId)
        {
            return _context.UserRefreshTokens.Any(x => x.UserId == userId);
        }
    }
}
