using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dukkantek.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
