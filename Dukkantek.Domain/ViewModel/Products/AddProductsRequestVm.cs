using Dukkantek.common.Enum;

namespace Dukkantek.Domain.ViewModels.Products
{
    public class AddProductsRequestVm
    {
        public string Name { get; set; }

        
        public string Barcode { get; set; }

        public string Description { get; set; }

        public string Weight { get; set; }

        public int status { get; set; }

    }
}
