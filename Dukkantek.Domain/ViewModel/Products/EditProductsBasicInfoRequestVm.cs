using Dukkantek.common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dukkantek.Domain.ViewModels.Products
{
    public class EditProductsBasicInfoRequestVm
    {
        public string Name { get; set; }

        public string Barcode { get; set; }

        
        public string Description { get; set; }

        public string Weight { get; set; }

        public int status { get; set; }

    }
}
