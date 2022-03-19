using Dukkantek.common.Enum;
using Dukkantek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dukkantek.Domain.ViewModels.Products
{
    public class EditProductsDetailsInfoRequestVm
    {
        public string Name { get; set; }

        public string Barcode { get; set; }


        public string Description { get; set; }

        public string Weight { get; set; }

        public StatusOfProduct status { get; set; }
    }
}
