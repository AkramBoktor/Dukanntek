using Dukkantek.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dukkantek.Domain.Repositories
{
    public interface IUserRefreshTokenRepository : IGenericRepository<UserRefreshToken>
    {
        UserRefreshToken GetRefreshToken(string userId);
        bool IsRefreshTokenExist(string userId);
    }
}