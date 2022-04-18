using MyWebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<CataLog> CataLogs { get; }
        IGenericRepository<Product> Products { get; }
        Task Save();
    }
}
