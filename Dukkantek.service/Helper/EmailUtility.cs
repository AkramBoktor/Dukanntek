using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dukkantek.Service.Helpers
{
    public static class EmailUtility
    {
        public static bool Validate(string email)
        {
            email = email.Trim();
            var emailPattern = "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$";
            var emailRegex = new Regex(emailPattern, RegexOptions.None);
            Match emailMatch = emailRegex.Match(email);
            return emailMatch.Success;
        }
    }
}
