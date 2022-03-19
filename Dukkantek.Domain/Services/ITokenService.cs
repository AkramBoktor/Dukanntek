using System;
using System.Collections.Generic;
using Dukkantek.Domain.Entities;
using System.Text;
using System.Threading.Tasks;

namespace Dukkantek.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        public (string AccessToken,string ExpireDate) GenerateAccessToken(ApplicationUser user);
        public string GenerateRefreshToken();
        public bool SaveRefreshToken(string userId, string refreshToken);
        public bool UpdateRefreshToken(string userId, string refreshToken);
    }
}
