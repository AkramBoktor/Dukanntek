using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dukkantek.Domain.Repositories;
using Dukkantek.Infrastructure.Repositories;
using Dukkantek.Entities;
using Dukkantek.Data;

namespace Dukkantek.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Products,ApplicationDbContext>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        #region Add
        public async Task AddAProducts(Products Products)
        {
            await AddAsync(Products);
            SaveEntities();
        }
        #endregion

        #region Delete
        public async Task DeleteAProductsById(int id)
        {
            if (id!=null||id!=0)
            {
                var patient = await _context.Product.FirstOrDefaultAsync(s => s.Id == id);
                _context.Product.Remove(patient);
                SaveEntities();
            }
        }
        #endregion
        #region Get

        public Task<List<Products>> GetAllAProducts()
        {
            var product = _context.Product.Include(u => u.UserProduct).AsQueryable();

            return product.ToListAsync();
        }

        public async Task<Products> GetAProductsById(int id)
        {
          
                var product = _context.Product.Include(u => u.UserProduct).AsQueryable().FirstOrDefaultAsync(p => p.Id == id);

                return await product;
           
        }

        public async Task<Products> GetAProductsByName(string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                var product = _context.Product.Include(u => u.UserProduct).AsQueryable().FirstOrDefaultAsync(p => p.Name == name);

                return await product;
            }
            return null;
        }

        #endregion

        #region Update
        public void UpdateAProducts(Products Products)
        {
            Update(Products);
            SaveEntities();
        }
        #endregion
    }
}
