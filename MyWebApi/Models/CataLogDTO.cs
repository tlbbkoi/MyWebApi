using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{
    public class CreateCataLogDTO
    {
        public string Name { get; set; }
    }
    public class CataLogDTO : CreateCataLogDTO
    {
        public int Id { get; set; }

        public IList<ProductDTO> Products { get; set; }

    }
}
