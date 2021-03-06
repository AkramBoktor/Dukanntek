using Dukkantek.Domain.Interfaces.Mapper;
using Dukkantek.Domain.ViewModels.Products;
using Dukkantek.Entities;
using System;
using System.Collections.Generic;


namespace Dukkantek.Service.Mapper
{
    public class ProductMapper : IProductMapper
    {
        public ProductMapper()
        {

        }
        public List<GetProductsResponseVm> Product_To_GetProductResponseVm(List<Products> Products)
        {
            List<GetProductsResponseVm> getProductResponseVms = new List<GetProductsResponseVm>();

            foreach (var Product in Products)
            {
                GetProductsResponseVm getProductResponseVm = new GetProductsResponseVm();
                getProductResponseVm.Barcode = Product.Barcode;
                getProductResponseVm.Name = Product.Name;
                getProductResponseVm.Description = Product.Description;
                getProductResponseVm.status = Product.statusId;
                getProductResponseVm.Weight = Product.Weight;
                

                getProductResponseVms.Add(getProductResponseVm);
            }
            return getProductResponseVms;
        }
        public GetProductsResponseVm Product_To_GetProductResponseVm(Products Product)
        {
            GetProductsResponseVm getProductResponseVm = new GetProductsResponseVm();
            getProductResponseVm.Barcode = Product.Barcode;
            getProductResponseVm.Name = Product.Name;
            getProductResponseVm.Description = Product.Description;
            getProductResponseVm.status = Product.statusId;
            getProductResponseVm.Weight = Product.Weight;

            return getProductResponseVm;
        }
    }
}
