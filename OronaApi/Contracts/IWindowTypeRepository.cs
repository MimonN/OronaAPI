using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IWindowTypeRepository : IRepositoryBase<WindowType>
    {
        Task UpdateAsync(WindowType obj);
        Task<IEnumerable<WindowType>> GetAllWindowTypesWithProductsWithCleaningTypes();
        Task<WindowType> WindowTypeExistAsync(WindowType obj);
    }
}
