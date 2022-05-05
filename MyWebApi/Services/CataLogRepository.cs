using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyWebApi.Controllers;
using MyWebApi.Data;
using MyWebApi.IRepository;
using MyWebApi.Models;
using MyWebApi.Properties;
using MyWebApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MyWebApi.Services
{
    public class CataLogRepository : ICataLogRespository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CataLogController> _logger;
        private readonly IMapper _mapper;

        public CataLogRepository(IUnitOfWork unitOfWork, ILogger<CataLogController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<object> CreateCataLog(CreateCataLogDTO cataLogDTO)
        {
            var cataLog = _mapper.Map<CataLog>(cataLogDTO);
            await _unitOfWork.CataLogs.Insert(cataLog);
            await _unitOfWork.Save();
            return new
            {
                id = cataLog.Id,
                cataLog
            };
        }

        public async Task<string> DeleteCataLog(int id)
        {
            var country = await _unitOfWork.CataLogs.Get(q => q.Id == id);
            if (country == null || id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCataLog)}");
                throw new BusinessException(Resource.NOT_DATA);
            }

            await _unitOfWork.CataLogs.Delete(id);
            await _unitOfWork.Save();
            return Resource.DELETE_SUCCESS;
        }

        public async Task<object> GetCataLog(int id)
        {
            var cataLog = await _unitOfWork.CataLogs.Get(q => q.Id == id, include: q => q.Include(x => x.Products));
            var result = _mapper.Map<CataLogDTO>(cataLog);
            return new
            {
                result
            };
        }

        public async Task<object> GetCataLogs(RequestParams requestParams)
        {
            var cataLogs = await _unitOfWork.CataLogs.GetPagedList(requestParams, include: q => q.Include(x => x.Products));
            var results = _mapper.Map<IList<CataLog>>(cataLogs);

            return new
            {
                results
            };
        }

        public async Task<string> UpdateCataLog(int id, CreateCataLogDTO cataLogDTO)
        {
            var cataLog = await _unitOfWork.CataLogs.Get(q => q.Id == id);
            if (cataLog == null)
            {
                throw new BusinessException(Resource.NOT_DATA);
            }

            _mapper.Map(cataLogDTO, cataLog);
            _unitOfWork.CataLogs.Update(cataLog);
            await _unitOfWork.Save();
            return Resource.UPDATE_SUCCESS;
        }
    }
 }
