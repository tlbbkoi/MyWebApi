using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Data
{
    public class CataLog
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<Product> Products { get; set; }
    }
}
