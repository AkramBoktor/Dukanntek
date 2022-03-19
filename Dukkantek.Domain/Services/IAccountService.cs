using System;
using System.Collections.Generic;
using Dukkantek.Domain.ViewModels.General;
using System.Threading.Tasks;

namespace Dukkantek.Domain.Interfaces.Services
{
    public interface IAccountService
    {
        Task<GeneralResponse<LoginResponseVm>> Login(LoginRequestVm loginViewModel);
        Task<GeneralResponse<LoginResponseVm>> RefreshToken(RefreshTokenRequestVm refreshTokenView);
        Task<GeneralResponse<UserLoginResponseVm>> GetCurrentUser();
    }
}
