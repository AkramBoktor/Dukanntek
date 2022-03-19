using System;
using System.Collections.Generic;
using Dukkantek.Common.Enums;
using Dukkantek.Domain.Helper;
using Dukkantek.Domain.Interfaces.Mapper;
using Dukkantek.Domain.Interfaces.Services;
using Dukkantek.Domain.Repositories;
using Dukkantek.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using Dukkantek.Domain.ViewModels.General;
using ITokenService = Dukkantek.Domain.Interfaces.Services.ITokenService;
using Dukkantek.Service.Validators;
using Dukkantek.Service.Validators.General;

namespace Dukkantek.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperStore _mapperStore;
        private readonly ITokenService _tokenService;
        private readonly IChecker _checkerService;
        private readonly IIdentityService _identityService;

        public AccountService(IUnitOfWork unitOfWork, IMapperStore mapperStore, ITokenService tokenService, IChecker checkerService, IIdentityService identityService)
        {
            _unitOfWork = unitOfWork;
            _mapperStore = mapperStore;
            _tokenService = tokenService;
            _checkerService = checkerService;
            _identityService = identityService;
        }

        public async Task<GeneralResponse<LoginResponseVm>> Login(LoginRequestVm userLoginViewModel)
        {
            List<Error> errors = await ValidateUserForLogin(userLoginViewModel);
            GeneralResponse<LoginResponseVm> response = new GeneralResponse<LoginResponseVm>();
            if (errors.Any())
            {
                response.IsSucceeded = false;
                response.Data = null;
                response.Errors = errors;
                return response;
            }

            // get user
            var user = await _unitOfWork.UserManagerRepository.FindByEmailAsync(userLoginViewModel.UserName);
            if (user == null)
            {
              
                user = await _unitOfWork.UserManagerRepository.FindByNameAsync(userLoginViewModel.UserName);
            }

            // get token
            (string token, string expireDate) = _tokenService.GenerateAccessToken(user);
            response.Data.RefreshToken = _tokenService.GenerateRefreshToken();
            if (!_tokenService.SaveRefreshToken(user.Id, response.Data.RefreshToken))
            {
                response.IsSucceeded = false;
                response.Errors = 
                     new List<Error> { new Error { ErrorMessage = "RefreshToken Error, Please Try Again" } };
                response.Data = null;
                return response;
            }
            UserLoginResponseVm userLoginResponseVm = _mapperStore.UserMapper.User_To_UserLoginResponseVm(user);

            response.IsSucceeded = true;
            response.Data.AccessToken = token;
            response.Data.Expiration = expireDate;
            response.Data.RefreshToken = _unitOfWork.UserRefreshTokenRepository.GetRefreshToken(user.Id)?.RefreshToken;
            response.Data.User = userLoginResponseVm;
            return response;
        }
        public async Task<GeneralResponse<LoginResponseVm>> RefreshToken(RefreshTokenRequestVm refreshTokenView)
        {
            List<Error> errors = await ValidateUserForRefreshToken(refreshTokenView);
            GeneralResponse<LoginResponseVm> response = new GeneralResponse<LoginResponseVm>();
            if (errors.Any())
            {
                response.IsSucceeded = false;
                response.Data = null;
                response.Errors = errors;
                return response;
            }

            var user = await _unitOfWork.UserManagerRepository.FindByIdAsync(refreshTokenView.UserId);
            (string token, string expireDate) = _tokenService.GenerateAccessToken(user);
            response.Data.RefreshToken = _tokenService.GenerateRefreshToken();
            if (!_tokenService.UpdateRefreshToken(user.Id, response.Data.RefreshToken))
            {
                response.IsSucceeded = false;
                response.Errors = ( new List<Error> { new Error { ErrorMessage = "RefreshToken Error, Please Try Again" } });
                response.Data = null;
                return response;
            }
            UserLoginResponseVm userLoginResponseVm = _mapperStore.UserMapper.User_To_UserLoginResponseVm(user);

            response.IsSucceeded = true;
            response.Data.AccessToken = token;
            response.Data.Expiration = expireDate;
            response.Data.User = userLoginResponseVm;
            return response;
        }
        public async Task<GeneralResponse<UserLoginResponseVm>> GetCurrentUser()
        {
            GeneralResponse<UserLoginResponseVm> response = new GeneralResponse<UserLoginResponseVm>();
            ApplicationUser user = await _unitOfWork.UserManagerRepository.FindByIdAsync(_identityService.UserId);
            if (user == null)
            {
                response.IsSucceeded = false;
                response.Errors = 
                     new List<Error> { new Error { ErrorMessage = "User Isn`t Exist" } };
                response.Data = null;
                return response;
            }
            UserLoginResponseVm userLoginResponseVm = _mapperStore.UserMapper.User_To_UserLoginResponseVm(user);

            response.IsSucceeded = true;
            response.Data = userLoginResponseVm;
            return response;
        }

        
        #region Validations
        private async Task<List<Error>> ValidateUserForLogin(LoginRequestVm userLoginViewModel)
        {
            // general validation
            List<Error> errors = ValidatorHandler.Validate<LoginRequestVm>(userLoginViewModel, (LoginRequestVmValidator)Activator.CreateInstance(typeof(LoginRequestVmValidator)));
            if (errors != null && errors.Any())
                errors.Add(
                 new Error { ErrorMessage = "Login Failed" });

            errors ??= new List<Error>();
            // validate UserName
            var user = await _unitOfWork.UserManagerRepository.FindByEmailAsync(userLoginViewModel.UserName);
            if (user == null)
            {
                user = await _unitOfWork.UserManagerRepository.FindByNameAsync(userLoginViewModel.UserName);
                if (user == null)
                    errors.Add(
                         new Error { ErrorMessage = "Invalid UserName Or Password" });
            }

            // validate EmailConfirmed
            if (user != null && !user.EmailConfirmed)
                errors.Add(
                     new Error { ErrorMessage = "Please Confirm Your Email" });

            // validate PhoneNumberConfirmed
            if (user != null && !user.PhoneNumberConfirmed)
                errors.Add(
                    new Error { ErrorMessage = "Please Confirm Your PhoneNumber" });

            // validate IsActive
            if (user != null)
                errors.Add(
                     new Error { ErrorMessage = "Account Isn`t Exisits" });


            return errors;
        }
        private async Task<List<Error>> ValidateUserForRefreshToken(RefreshTokenRequestVm refreshTokenRequestVm)
        {
            // general validation
            List<Error> errors = ValidatorHandler.Validate<RefreshTokenRequestVm>(refreshTokenRequestVm, (RefreshTokenRequestVmValidator)Activator.CreateInstance(typeof(RefreshTokenRequestVmValidator)));
            if (errors != null && errors.Any())
                errors.Add(
                    new Error { ErrorMessage = "Login Failed" });

            errors ??= new List<Error>();

            // validate UserId
            var user = await _unitOfWork.UserManagerRepository.FindByIdAsync(refreshTokenRequestVm.UserId);
        

            // validate IsActive
            if (user != null)
                errors.Add(new Error { ErrorMessage = "Account Isn`t Exisit" });

            // validate refresh token
            if (user != null)
            {
                string refreshToken = _unitOfWork.UserRefreshTokenRepository.GetRefreshToken(user.Id)?.RefreshToken;
                if (string.IsNullOrWhiteSpace(refreshToken) && refreshToken != refreshTokenRequestVm.RefreshToken)
                    errors.Add(new Error { ErrorMessage = "Invalid RefreshToken Error" });
            }

            return errors;
        }
        #endregion
    }
}
