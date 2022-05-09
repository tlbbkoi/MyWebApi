using MyWebApi.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{
    public class CreateProducDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Context { get; set; }
        public byte Discount { get; set; }
        public int CataLogId { get; set; }
    }

    public class ProductDTO : CreateProducDTO
    {
        public int Id { get; set; }

        public CataLogDTO CataLog { get; set; }

    }
}
