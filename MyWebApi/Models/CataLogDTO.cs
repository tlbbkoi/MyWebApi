using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{
    public class CreateCataLogDTO
    {
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "CaTalog Product Is Too Log")]
        public string Name { get; set; }
    }
    public class CataLogDTO : CreateCataLogDTO
    {
        public int Id { get; set; }

        public IList<ProductDTO> Products { get; set; }

    }
}
