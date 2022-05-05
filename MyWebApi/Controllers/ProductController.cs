using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.IRepository;
using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Services;
using MyWebApi.Properties;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRespository _productRespository;

        public ProductController(IProductRespository productRespository)
        {
            _productRespository = productRespository;
        }
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetProducts([FromQuery] RequestParams requestParams)
        {
            var results = await _productRespository.GetProducts(requestParams);
            return Ok(new Repsonse(Resource.GET_SUCCESS, requestParams, results));
        }


        [HttpGet("{id}")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]


        public async Task<IActionResult> GetByIdProduct(int id)
        {
            var result = await _productRespository.GetProduct(id);
            return Ok(new Repsonse(Resource.GET_SUCCESS, new { id = id }, result));
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> CreateProduct([FromBody] CreateProducDTO producDTO)
        {
            var result = await _productRespository.CreateProduct(producDTO);
            return Ok(new Repsonse(Resource.CREATE_SUCCESS, null, result));
        }

        [HttpPut("{id}")]
        [Authorize]


        public async Task<IActionResult> UpdateCataLog(int id, [FromBody] CreateProducDTO producDTO)
        {
            var result = await _productRespository.UpdateProduct(id, producDTO);
            return Ok(new Repsonse(result));
        }

        [HttpDelete("{id}")]


        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productRespository.DeleteProduct(id);
            return Ok(new Repsonse(result));
        }

    }
}

