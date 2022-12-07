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
    public class WindowTypeRepository : RepositoryBase<WindowType>, IWindowTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public WindowTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<WindowType>> GetAllWindowTypesWithProductsWithCleaningTypes()
        {
            var result = await _db.WindowTypes.Include(u => u.Products).ThenInclude(c => c.CleaningType).ToListAsync();
            return result;
        }

        public async Task UpdateAsync(WindowType obj)
        {
            _db.WindowTypes.Update(obj);
            await _db.SaveChangesAsync();
        }
    }
}
