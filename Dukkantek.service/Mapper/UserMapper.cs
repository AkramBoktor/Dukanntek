using System.Linq;
using Dukkantek.Domain.Entities;
using Dukkantek.Domain.Interfaces.Mapper;
using Dukkantek.Domain.ViewModels.General;

namespace Gezira.Service.Mapper
{
    public class UserMapper : IUserMapper
    {
        public UserMapper()
        {

        }

        public UserLoginResponseVm User_To_UserLoginResponseVm(ApplicationUser user)
        {
            UserLoginResponseVm userLoginDto = new UserLoginResponseVm
            {
                Email = user.Email,
                Phone = user.PhoneNumber,
                FirstName =  user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
            };

            return userLoginDto;
        }
    }
}
