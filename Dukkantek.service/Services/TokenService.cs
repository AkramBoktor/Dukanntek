using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using Dukkantek.Domain.Interfaces.Services;
using Dukkantek.Domain.Repositories;
using Dukkantek.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Dukkantek.Service.Services
{
    public class TokenService : Domain.Interfaces.Services.ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public TokenService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public (string AccessToken, string ExpireDate) GenerateAccessToken(ApplicationUser user)
        {
            var roles = _unitOfWork.UserManagerRepository.GetRolesAsync(user);

            List<Claim> claim = new List<Claim>
            {
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                    // new Claim("LanguageId",user.UserProfile.LanguageId.ToString()),
                    new Claim (ClaimTypes.Role,string.Join(",", roles.Result))
            };

            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
            int expiryInMinutes = Convert.ToInt32(_configuration["JWT:ExpiryInMinutes"]);

            JwtSecurityToken token = new JwtSecurityToken(
                      issuer: _configuration["JWT:Issuer"],
                      audience: _configuration["JWT:Audience"],
                      expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                      signingCredentials: credentials,
                      claims: claim
                    );

            return (new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo.ToString(CultureInfo.InvariantCulture));
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            string refreshToken;
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken = Convert.ToBase64String(randomNumber);
            }
            return refreshToken;
        }
        public bool SaveRefreshToken(string userId, string refreshToken)
        {
            if (!_unitOfWork.UserRefreshTokenRepository.IsRefreshTokenExist(userId))
            {
                UserRefreshToken userRefreshToken = new UserRefreshToken();
                userRefreshToken.RefreshToken = refreshToken;
                userRefreshToken.UserId = userId;

                _unitOfWork.UserRefreshTokenRepository.Add(userRefreshToken);
                return _unitOfWork.Complete() > 0;
            }
            return true;
        }
        public bool UpdateRefreshToken(string userId, string refreshToken)
        {
            if (_unitOfWork.UserRefreshTokenRepository.IsRefreshTokenExist(userId))
            {
                UserRefreshToken refreshTokenObj = _unitOfWork.UserRefreshTokenRepository.GetRefreshToken(userId);
                refreshTokenObj.RefreshToken = refreshToken;

                _unitOfWork.UserRefreshTokenRepository.Update(refreshTokenObj);
                return _unitOfWork.Complete() > 0;
            }
            return true;
        }
    }
}
