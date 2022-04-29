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
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetProducts()
        {
                var products = await _unitOfWork.Products.GetAll();
                var result = _mapper.Map<IList<ProductDTO>>(products);
                return Ok(result);
        }

        [HttpGet ("{id}")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]


        public async Task<IActionResult> GetByIdProduct(int id)
        {
                var product = await _unitOfWork.Products.Get(pt => pt.Id == id);
                
                var result = _mapper.Map<ProductDTO>(product);
                return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> CreateProduct([FromBody] CreateProducDTO producDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post attempt in {nameof(CreateProduct)}");
                return BadRequest(ModelState);
            }
            try
            {
                var product = _mapper.Map<Product>(producDTO);
                await _unitOfWork.Products.Insert(product);
                await _unitOfWork.Save();

                return Ok(product);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error in the {nameof(CreateProduct)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }

        [HttpPut ("{id}")]
        [Authorize (Roles = "User")]

        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateProducDTO productDTO)
        {
                var product = await _unitOfWork.Products.Get(pt => pt.Id == id);
                if(product == null)
                {
                    _logger.LogError($"Invalid Update attemp in {nameof(UpdateProduct)}");
                    return BadRequest("Submitted data is invalid");
                }
                else
                {
                    _mapper.Map(productDTO, product);
                    _unitOfWork.Products.Update(product);
                    await _unitOfWork.Save();

                    return Ok(product);
                }

        }

        [HttpDelete ("{id}")]
        [Authorize (Roles = "User")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            if(!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid Post attempt in {nameof(DeleteProduct)}");
                return BadRequest(ModelState);
            }
                var product = await _unitOfWork.Products.Get(pt => pt.Id == id);
                if(product == null)
                {
                    _logger.LogError($"Invalid Delete attemp in {nameof(DeleteProduct)}");
                    return BadRequest("Submitted data is invalid");
                }
                else
                {
                    await _unitOfWork.Products.Delete(id);
                    await _unitOfWork.Save();

                    return NoContent();
                } 
        }
    }
}
