using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, ILogger<ProductController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _unitOfWork.Products.GetAll();
                var result = _mapper.Map<IList<ProductDTO>>(products);
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error in the {nameof(GetProducts)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetByIdProduct(int id)
        {
            try
            {
                var product = await _unitOfWork.Products.GetById(pt => pt.Id == id, new List<string> { "CataLog" });
                var result = _mapper.Map<CataLogDTO>(product);
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error in the {nameof(GetByIdProduct)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
    }
}
