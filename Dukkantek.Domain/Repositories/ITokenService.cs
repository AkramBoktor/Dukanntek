using Dukkantek.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dukkantek.Domain.Repositories
{
    public interface ITokenService
    {
        (string AccessToken, string ExpireDate) GenerateAccessToken(ApplicationUser user);
        public string GenerateRefreshToken();
        public bool SaveRefreshToken(string userId, string refreshToken);
        public bool UpdateRefreshToken(string userId, string refreshToken);
    }
}
