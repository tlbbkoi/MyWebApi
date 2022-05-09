using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Configurations.Entities
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "Vim",
                    Price = 45000,
                    Context = "Dung dịch tẩy rửa bồn cầu ",
                    Discount = 0,
                    CataLogId = 1,
                },
                new Product
                {
                    Id = 2,
                    Name = "Tv Vinmart",
                    Price = 500000,
                    Context = " Tx xem siêu nét từ Vinmart",
                    Discount = 10,
                    CataLogId = 2,
                },
                new Product
                {
                    Id = 3,
                    Name = "SamSung Galaxy S10",
                    Price = 1200000,
                    Context = "Điện Thoại Sam Sung mới nhất",
                    Discount = 5,
                    CataLogId = 3,
                },
                new Product
                {
                    Id = 4,
                    Name = "Iphone 11",
                    Price = 1500000,
                    Context = "Điện Thoại Apple mới nhất",
                    Discount = 10,
                    CataLogId = 3,
                }
                );
            builder.HasKey(s => s.Id);
            builder.HasOne(b => b.CataLog).WithMany(a => a.Products).HasForeignKey(s => s.CataLogId);
        }
        
    }
}
