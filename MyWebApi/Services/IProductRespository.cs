using MyWebApi.Data;
using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Services
{
    public interface IProductRespository
    {
        Task<object> GetProducts(RequestParams requestParams);
        Task<object> GetProduct(int id);
        Task<object> CreateProduct(CreateProducDTO producDTO);
        Task<string> UpdateProduct(int id, CreateProducDTO producDTO);
        Task<string> DeleteProduct(int id);
    }
}
