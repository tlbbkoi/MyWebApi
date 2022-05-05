using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebApi.Data;
using MyWebApi.IRepository;
using MyWebApi.Models;
using MyWebApi.Properties;
using MyWebApi.Services;
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
        private readonly ICataLogRespository _cataLogRespository;

        public CataLogController(ICataLogRespository cataLogRespository)
        {
            _cataLogRespository = cataLogRespository;
        }
        [HttpGet]
        [HttpCacheExpiration(CacheLocation= CacheLocation.Public,MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetCataLogs([FromQuery] RequestParams requestParams) 
        {
            var results = await _cataLogRespository.GetCataLogs(requestParams);
            return Ok(new Repsonse(Resource.GET_SUCCESS, requestParams, results));
        }

        
        [HttpGet ("{id}")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]


        public async Task<IActionResult> GetByIdCataLog(int id)
        {
            var result = await _cataLogRespository.GetCataLog(id);
            return Ok(new Repsonse(Resource.GET_SUCCESS, new { id = id }, result));
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> CreateCataLog([FromBody] CreateCataLogDTO cataLogDTO)
        {
            var result = await _cataLogRespository.CreateCataLog(cataLogDTO);
            return Ok(new Repsonse(Resource.CREATE_SUCCESS, null, result));
        }

        [HttpPut("{id}")]
        [Authorize]


        public async Task<IActionResult> UpdateCataLog(int id, [FromBody] CreateCataLogDTO cataLogDTO)
        {
            var result = await _cataLogRespository.UpdateCataLog(id, cataLogDTO);
            return Ok(new Repsonse(result));
        }

        [HttpDelete("{id}")]


        public async Task<IActionResult> DeleteCataLog(int id)
        {
            var result = await _cataLogRespository.DeleteCataLog(id);
            return Ok(new Repsonse(result));
        }

    }
}
