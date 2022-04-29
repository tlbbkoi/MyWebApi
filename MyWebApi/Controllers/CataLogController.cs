using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Data;
using MyWebApi.IRepository;
using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CataLogController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CataLogController> _logger;
        private readonly IMapper _mapper;

        public CataLogController(IUnitOfWork unitOfWork, ILogger<CataLogController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpCacheExpiration(CacheLocation= CacheLocation.Public,MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetCataLogs([FromQuery] RequestParams requestParams) 
        {
                var cataLogs = await _unitOfWork.CataLogs.GetPagedList(requestParams);
                var results = _mapper.Map<IList<CataLogDTO>>(cataLogs);
                return Ok(results);
        }

        
        [HttpGet ("{id}")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]


        public async Task<IActionResult> GetByIdCataLog(int id)
        {
                var cataLog = await _unitOfWork.CataLogs.Get(cl => cl.Id == id);
                var result = _mapper.Map<CataLogDTO>(cataLog);
                return Ok(result);
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> CreateCataLog([FromBody] CreateCataLogDTO cataLogDTO)
        {
                var cataLog = _mapper.Map<CataLog>(cataLogDTO);
                await _unitOfWork.CataLogs.Insert(cataLog);
                await _unitOfWork.Save();

                return Ok(cataLog);
        }

        [HttpPut("{id}")]
        [Authorize]


        public async Task<IActionResult> UpdateCataLog(int id, [FromBody] CreateCataLogDTO cataLogDTO)
        {
                var cataLog = await _unitOfWork.CataLogs.Get(pt => pt.Id == id);
                if (cataLog == null)
                {
                    _logger.LogError($"Invalid Update attemp in {nameof(UpdateCataLog)}");
                    return BadRequest("Submitted data is invalid");
                }
                else
                {
                    _mapper.Map(cataLogDTO, cataLog);
                    _unitOfWork.CataLogs.Update(cataLog);
                    await _unitOfWork.Save();

                    return NoContent();
                }
        }

        [HttpDelete("{id}")]


        public async Task<IActionResult> DeleteCataLog(int id)
        {
                var product = await _unitOfWork.Products.Get(pt => pt.Id == id);
                if (product == null)
                {
                    _logger.LogError($"Invalid Delete attemp in {nameof(DeleteCataLog)}");
                    return BadRequest("Submitted data is invalid");
                }
                else
                {
                    await _unitOfWork.CataLogs.Delete(id);
                    await _unitOfWork.Save();

                    return NoContent();
                }
        }

    }
}
