using Dukkantek.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dukkantek.Entities
{
    public class Category : BaseEntity
    {

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Name { get; set; }



        [ForeignKey("CategoryId")]
        public virtual ICollection<Products> Products { get; set; }


 


    }
}
