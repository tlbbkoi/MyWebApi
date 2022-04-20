using AutoMapper;
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
        public async Task<IActionResult> GetCataLogs()
        {
            try
            {
                var cataLogs = await _unitOfWork.CataLogs.GetAll();
                var results = _mapper.Map<IList<CataLogDTO>>(cataLogs);
                return Ok(results);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,$"Error in the {nameof(GetCataLogs)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        [HttpGet ("{id:int}")]
        public async Task<IActionResult> GetByIdCataLog(int id)
        {
            try
            {
                var cataLog = await _unitOfWork.CataLogs.GetById(cl => cl.Id == id,new List<string> {"Products"});
                var result = _mapper.Map<CataLogDTO>(cataLog);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in the {nameof(GetByIdCataLog)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        
    }
}
