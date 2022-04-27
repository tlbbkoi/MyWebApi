using AutoMapper;
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

        
        [HttpGet ("{id}")]
        

        public async Task<IActionResult> GetByIdCataLog([FromQuery] int id)
        {
            try
            {
                var cataLog = await _unitOfWork.Products.Get(cl => cl.Id == id);
                var result = _mapper.Map<CataLogDTO>(cataLog);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in the {nameof(GetByIdCataLog)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> CreateCataLog([FromBody] CreateCataLogDTO cataLogDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post attempt in {nameof(CreateCataLog)}");
                return BadRequest(ModelState);
            }
            try
            {
                var cataLog = _mapper.Map<CataLog>(cataLogDTO);
                await _unitOfWork.CataLogs.Insert(cataLog);
                await _unitOfWork.Save();

                return Ok(cataLog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in the {nameof(CreateCataLog)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }

        [HttpPut("{id}")]
        [Authorize]


        public async Task<IActionResult> UpdateCataLog(int id, [FromBody] CreateCataLogDTO cataLogDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid Post attempt in {nameof(UpdateCataLog)}");

                return BadRequest(ModelState);
            }
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in the {nameof(UpdateCataLog)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }

        [HttpDelete("{id}")]


        public async Task<IActionResult> DeleteCataLog(int id)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid Post attempt in {nameof(DeleteCataLog)}");
                return BadRequest(ModelState);
            }
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in the {nameof(DeleteCataLog)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }

    }
}
