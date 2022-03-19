using Dukkantek.Common.Enums;
using Dukkantek.Domain.Entities;
using Dukkantek.Domain.ViewModels.General;

namespace Dukkantek.Domain.Interfaces.Mapper
{
    public interface IUserMapper
    {
        UserLoginResponseVm User_To_UserLoginResponseVm(ApplicationUser user);
    }
}
