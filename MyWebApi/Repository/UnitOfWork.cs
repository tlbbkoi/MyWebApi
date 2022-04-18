using MyWebApi.Data;
using MyWebApi.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        private IGenericRepository<CataLog> _catalogs;
        private IGenericRepository<Product> _products;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }
        public IGenericRepository<CataLog> CataLogs => _catalogs ??= new GenericRepository<CataLog>(_context);

        public IGenericRepository<Product> Products => _products ??= new GenericRepository<Product>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
