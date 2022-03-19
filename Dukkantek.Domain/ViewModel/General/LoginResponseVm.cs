using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dukkantek.Domain.ViewModels.General
{
    public class LoginResponseVm
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Expiration { get; set; }
        public UserLoginResponseVm User { get; set; }
    }
}
