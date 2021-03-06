using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dukkantek.Common.Enums;
using Dukkantek.Domain.Entities;
using Dukkantek.Domain.Helper;
using Dukkantek.Domain.Interfaces.Mapper;
using Dukkantek.Domain.Interfaces.Services;
using Dukkantek.Domain.Repositories;
using Dukkantek.Domain.ViewModels.General;
using Dukkantek.Domain.ViewModels.Products;
using Dukkantek.Entities;
using Dukkantek.Service.Validators;
using Microsoft.AspNetCore.Identity;

namespace Dukkantek.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperStore _mapperStore;
        private readonly IIdentityService _identityService;
        private readonly IChecker _checkerService;

        public ProductService(IUnitOfWork unitOfWork, IMapperStore mapperStore, IIdentityService identityService, IChecker checkerService)
        {
            _unitOfWork = unitOfWork;
            _mapperStore = mapperStore;
            _identityService = identityService;
            _checkerService = checkerService;
        }

      
        public async Task<GeneralResponse<List<GetProductsResponseVm>>> GetAllProducts()
        {
            GeneralResponse<List<GetProductsResponseVm>> response = new GeneralResponse<List<GetProductsResponseVm>>();

            var productList =await _unitOfWork.ProductRepository.GetAllAProducts();
            response.IsSucceeded = true;
            response.Data = _mapperStore.ProductMapper.Product_To_GetProductResponseVm(productList);
            return response;

        }

        public async Task<GeneralResponse<GetProductsDetailsResponseVm>> GetProductById(int id)
        {
            GeneralResponse<GetProductsDetailsResponseVm> response = new GeneralResponse<GetProductsDetailsResponseVm>();

            if (id != 0)
            {

                var product = await _unitOfWork.ProductRepository.GetAProductsById(id);
                response.IsSucceeded = true;
                response.Data = (GetProductsDetailsResponseVm)_mapperStore.ProductMapper.Product_To_GetProductResponseVm(product);
                return response;
            }

            response.IsSucceeded = false;
            response.Data =null;
            response.Errors = new List<Error> { new Error { ErrorMessage = "Empty id" } };
            return response;
        }

        public async Task<GeneralResponse<bool>> AddProduct(AddProductsRequestVm addProductProfileViewModel)
        {
            GeneralResponse<bool> response = new GeneralResponse<bool>();

            if (addProductProfileViewModel == null)
            {
                response.Errors = new List<Error> { new Error { ErrorMessage = "invalid Product" } };
                response.IsSucceeded = false;
                response.Data = false;
                return response;

            }
            Products product = new Products();
            product.Barcode = addProductProfileViewModel.Barcode;
            product.Description = addProductProfileViewModel.Description;
            product.Name = addProductProfileViewModel.Name;
            product.CreateBy = _identityService.UserId;
            product.CreateDate = DateTime.Now;
            product.statusId =addProductProfileViewModel.status;
           await _unitOfWork.ProductRepository.AddAProducts(product);

            response.Errors = null;
            response.IsSucceeded = true;
            response.Data = true;
            return response;
        }

        public async Task<GeneralResponse<GetProductsResponseVm>> DeleteProductById(int id)
        {
            GeneralResponse<GetProductsResponseVm> response = new GeneralResponse<GetProductsResponseVm>();
            if (id==0)
            {
                response.IsSucceeded = false;
                response.Errors =  new List<Error> { new Error { ErrorMessage = "product Isn't exist." } };
                response.Data = null;
                return response;
            }

            Products products = await _unitOfWork.ProductRepository.GetAProductsById(id);
            if (products == null)
            {
                response.IsSucceeded = false;
                response.Errors = new List<Error> { new Error { ErrorMessage = "product Isn't exist." } };
                response.Data = null;
                return response;
            }

            await _unitOfWork.ProductRepository.DeleteAProductsById(id);
            response.IsSucceeded = true;
            response.Data = _mapperStore.ProductMapper.Product_To_GetProductResponseVm(products);
            return response;
        }

        public async Task<GeneralResponse<string>> EditProductBasicInfo(EditProductsBasicInfoRequestVm editProductProfileBasicInfoRequestVm)

        {
            GeneralResponse<string> response = new GeneralResponse<string>();

            if (editProductProfileBasicInfoRequestVm == null)
            {
                response.Errors = new List<Error> { new Error { ErrorMessage = "empty Product" } };

                response.IsSucceeded = false;
                response.Data = "Failed";
                return response;

            }
            Products product = new Products();
            product.Barcode = editProductProfileBasicInfoRequestVm.Barcode;
            product.Description = editProductProfileBasicInfoRequestVm.Description;
            product.Name = editProductProfileBasicInfoRequestVm.Name;
            product.CreateBy = _identityService.UserId;
            product.CreateDate = DateTime.Now;
            product.statusId = editProductProfileBasicInfoRequestVm.status;
             _unitOfWork.ProductRepository.UpdateAProducts(product);

            response.Errors = null;
            response.IsSucceeded = true;
            response.Data = "Product updated";
            return response;
         }
    }
}
