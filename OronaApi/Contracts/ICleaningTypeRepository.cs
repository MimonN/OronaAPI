using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICleaningTypeRepository : IRepositoryBase<CleaningType>
    {
        Task UpdateAsync(CleaningType obj);

        Task<CleaningType> CleaningTypeExistAsync(CleaningType obj);
    }
}
