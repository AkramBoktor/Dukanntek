using System;
using System.Collections.Generic;
using Dukkantek.Common.Enums;
using Dukkantek.Domain.ViewModels.Products;
using Dukkantek.Entities;

namespace Dukkantek.Domain.Interfaces.Mapper
{
    public interface IProductMapper
    {
        List<GetProductsResponseVm> Product_To_GetProductResponseVm(List<Products> Products);
        GetProductsResponseVm Product_To_GetProductResponseVm(Products Product);
    }
}
