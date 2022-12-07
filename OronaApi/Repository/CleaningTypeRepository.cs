using Contracts;
using Entities.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CleaningTypeRepository : RepositoryBase<CleaningType>, ICleaningTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public CleaningTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(CleaningType obj)
        {
            _db.CleaningTypes.Update(obj);
            await _db.SaveChangesAsync();
        }
    }
}
