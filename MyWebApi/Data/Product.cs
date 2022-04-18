using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Context { get; set; }
        public byte Discount { get; set; }

        [ForeignKey(nameof(CataLog))]

        public int CataLogId { get; set; }
        public CataLog CataLog { get; set; }

    }
}
