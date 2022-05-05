using MyWebApi.Data;
using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Services
{
    public interface ICataLogRespository
    {
        Task<object> GetCataLogs(RequestParams requestParams);
        Task<object> GetCataLog(int id);
        Task<object> CreateCataLog(CreateCataLogDTO cataLogDTO);
        Task<string> UpdateCataLog(int id, CreateCataLogDTO cataLogDTO);
        Task<string> DeleteCataLog(int id);
    }
}
