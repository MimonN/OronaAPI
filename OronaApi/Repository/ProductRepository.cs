using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Product> ProductExistAsync(Product obj)
        {
            var productExist = await _db.Products.AsNoTracking().Where(p => p.WindowTypeId == obj.WindowTypeId).FirstOrDefaultAsync(p => p.CleaningTypeId == obj.CleaningTypeId);
            return productExist;
        }

        public async Task UpdateAsync(Product obj)
        {
            _db.Products.Update(obj);
            await _db.SaveChangesAsync();
        }

    }
}
