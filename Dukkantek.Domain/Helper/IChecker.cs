using Dukkantek.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dukkantek.Domain.Helper
{
    public interface IChecker
    {
        bool IsEmailValid(string email);

        void CheckBaseEntity(BaseEntity baseEntity, bool isEdit, string userId);

    }
}
