using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<CataLog> CataLogs { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CataLog>().HasData(
                new CataLog
                {
                    Id = 1,
                    Name = "Detergents"
                },
                new CataLog
                {
                    Id = 2,
                    Name = "Tv"
                },
                new CataLog
                {
                    Id = 3,
                    Name = "Mobie Phone"
                }
                );

            builder.Entity<Product>().HasData(
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
        }


    }
}
