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
    public class CleaningTypeRepository : RepositoryBase<CleaningType>, ICleaningTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public CleaningTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<CleaningType> CleaningTypeExistAsync(CleaningType obj)
        {
            var cleaningTypeExist = await _db.CleaningTypes.AsNoTracking().FirstOrDefaultAsync(p => p.CleaningName == obj.CleaningName);
            return cleaningTypeExist;
        }

        public async Task UpdateAsync(CleaningType obj)
        {
            _db.CleaningTypes.Update(obj);
            await _db.SaveChangesAsync();
        }
    }
}
