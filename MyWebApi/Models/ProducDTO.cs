using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{
    public class CreateProducDTO
    {
        [Required]
        [StringLength(maximumLength:100, ErrorMessage ="Product Name Is Too Long")]
        public string Name { get; set; }
        [Required]
        [Range(0,double.MaxValue)]
        public double Price { get; set; }
        public string Context { get; set; }
        [Range(0,100)]
        public byte Discount { get; set; }
        [Required]
        public int CataLogId { get; set; }
    }

    public class ProductDTO : CreateCataLogDTO
    {
        public int Id { get; set; }

        public CataLogDTO CataLog { get; set; }
    }
}
