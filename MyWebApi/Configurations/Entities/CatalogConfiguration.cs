using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Configurations.Entities
{
    public class CatalogConfiguration : IEntityTypeConfiguration<CataLog>
    {
        public void Configure(EntityTypeBuilder<CataLog> builder)
        {
            builder.HasData(
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
            builder.HasKey(s => s.Id);
                
            }
        }
    }

