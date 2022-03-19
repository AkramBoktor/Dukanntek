using System.Collections.Generic;
using Dukkantek.Common.Enums;


namespace Dukkantek.Domain.Interfaces.Services
{
    public interface IIdentityService
    {
        //string GetUserIdentity();
        string UserId { get; }
        //Asp Identity Identifier 
        string UserName { get; }
        string DisplayName { get; }
        string Email { get; }
        string CountryCode { get; }
        string TenantCode { get; }
        string CityCode { get; }
        string AreaCode { get; }
        List<string> UserRoles { get; }
        string PhoneNumber { get; }
        string PhoneNumberConfirmed { get; }
        string EmailConfirmed { get; }
        UserSuspensionStatusEnum UserStatus { get; }
        string ClientId { get; }
        public bool IsValidToken { get; }

    }
}