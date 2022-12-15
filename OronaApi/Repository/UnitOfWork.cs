using Contracts;
using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        private IConfiguration _configuration;
        public UnitOfWork(ApplicationDbContext db, IConfiguration configuration)
        {
            _configuration = configuration;
            _db = db;
            CleaningType = new CleaningTypeRepository(_db);
            WindowType = new WindowTypeRepository(_db);
            Product = new ProductRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            LocalUser = new UserRepository(_db, _configuration);
        }
        public IWindowTypeRepository WindowType {  get; private set; }

        public ICleaningTypeRepository CleaningType { get; private set; }

        public IProductRepository Product { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IUserRepository LocalUser { get; private set; }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
