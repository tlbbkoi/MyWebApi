using AutoMapper;
using MyWebApi.Data;
using MyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Configurations
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<CataLog, CataLogDTO>().ReverseMap();
            CreateMap<CataLog, CreateCataLogDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, CreateProducDTO>().ReverseMap();
        }
    }
}
