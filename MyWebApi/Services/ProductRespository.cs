using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyWebApi.Controllers;
using MyWebApi.Data;
using MyWebApi.IRepository;
using MyWebApi.Models;
using MyWebApi.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Services
{
    public class ProductRespository : IProductRespository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;

        public ProductRespository(IUnitOfWork unitOfWork, ILogger<ProductController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<object> CreateProduct(CreateProducDTO producDTO)
        {
            var product = _mapper.Map<Product>(producDTO);
            await _unitOfWork.Products.Insert(product);
            await _unitOfWork.Save();
            return new
            {
                id = product.Id,
                product
            };
        }

        public async Task<string> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Products.Get(pt => pt.Id == id);
            if (product == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteProduct)}");
                throw new BusinessException(Resource.NOT_DATA);
            }

            await _unitOfWork.Products.Delete(id);
            await _unitOfWork.Save();
            return Resource.DELETE_SUCCESS;
        }

        public async Task<object> GetProduct(int id)
        {
            var product = await _unitOfWork.Products.Get(pt => pt.Id == id, include: q => q.Include(x => x.CataLog));
            var result = _mapper.Map<ProductDTO>(product);
            return new
            {
                result
            };
        }

        public async Task<object> GetProducts(RequestParams requestParams)
        {
            var products = await _unitOfWork.Products.GetPagedList(requestParams, include: q => q.Include(x => x.CataLog));
            var results = _mapper.Map<IList<ProductDTO>>(products);
            return new
            {
                results
            };
        }

        public async Task<string> UpdateProduct(int id, CreateProducDTO producDTO)
        {
            var product = await _unitOfWork.Products.Get(pt => pt.Id == id);
            if (product == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProduct)}");
                throw new BusinessException(Resource.NOT_DATA);
            }

            _mapper.Map(producDTO, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.Save();
            return Resource.UPDATE_SUCCESS;
        }
    }
}
