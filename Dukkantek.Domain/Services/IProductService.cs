using System.Collections.Generic;
using System.Threading.Tasks;
using Dukkantek.Common.Enums;
using Dukkantek.Domain.ViewModels.General;
using Dukkantek.Domain.ViewModels.Products;


namespace Dukkantek.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<GeneralResponse<List<GetProductsResponseVm>>> GetAllProducts();
        Task<GeneralResponse<GetProductsDetailsResponseVm>> GetProductById(int id);
        Task<GeneralResponse<string>> AddProduct(AddProductsRequestVm addProductProfileViewModel);
        Task<GeneralResponse<GetProductsResponseVm>> DeleteProductById(int id);
        Task<GeneralResponse<string>> EditProductBasicInfo(EditProductsBasicInfoRequestVm editProductProfileBasicInfoRequestVm);

    }
}
