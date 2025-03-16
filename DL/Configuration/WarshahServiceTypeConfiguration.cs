using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
namespace DL.Configuration
{
   
   class WarshahServiceTypeConfiguration : IEntityTypeConfiguration<WarshahServiceType>
    {

        public void Configure(EntityTypeBuilder<WarshahServiceType> builder)
        {
            builder.ToTable("WarshahServiceType");



            builder.HasData
            (

                new WarshahServiceType
                {
                    Id = 1,
                   Name = "فى الورشة"

                },

                new WarshahServiceType
                {
                    Id = 2,
                    Name = "متنقلة"

                },

                new WarshahServiceType
                {
                    Id = 3,
                    Name = "زيارة فني"

                },


                new WarshahServiceType
                {
                    Id = 4,
                    Name = "المساعدة على الطريق"

                }


            );
        }
    }

}
