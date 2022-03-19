using Dukkantek.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dukkantek.Domain.Repositories
{
    public interface IProductRepository : IGenericRepository<Products>
    {
        Task<List<Products>> GetAllAProducts();
        Task<Products> GetAProductsById(int id);
        Task<Products> GetAProductsByName(string name);
        Task AddAProducts(Products Products);
        void UpdateAProducts(Products Products);
        Task DeleteAProductsById(int id);
    }
}
