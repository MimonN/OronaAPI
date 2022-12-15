using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUnitOfWork
    {
        IWindowTypeRepository WindowType { get; }
        ICleaningTypeRepository CleaningType { get; }
        IProductRepository Product { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IUserRepository LocalUser { get; }

        Task SaveAsync();
    }
}
