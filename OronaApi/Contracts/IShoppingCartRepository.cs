using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IShoppingCartRepository : IRepositoryBase<ShoppingCart>
    {
        //Task UpdateAsync(ShoppingCart obj);

        //Task<ShoppingCart> ShoppingCartExistAsync(ShoppingCart obj);
    }
}
