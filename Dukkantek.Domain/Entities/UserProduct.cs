using Dukkantek.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dukkantek.Entities
{
    public class UserProduct : BaseEntity
    {

        public string UserId { get; set; }
        

        public int ProductId { get; set; }
    }
}
