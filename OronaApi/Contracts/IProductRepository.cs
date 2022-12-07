using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task UpdateAsync(Product obj);
        Task<Product> ProductExistAsync(Product obj);
    }
}
