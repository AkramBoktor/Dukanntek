using Dukkantek.common.Enum;
using Dukkantek.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dukkantek.Entities
{
    public class Products : BaseEntity
    {
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Barcode { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Description { get; set; }

        public string Weight { get; set; }

        public int statusId { get; set; }

        public StatusOfProduct status { get; set; }

        [ForeignKey("ProductId")]
        public virtual ICollection<UserProduct> UserProduct { get; set; }
    }
}
