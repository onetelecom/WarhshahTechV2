using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Configuration
{
  
    class CityConfiguration : IEntityTypeConfiguration<City>
    {

        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");



            builder.HasData
            (

                new City
                {
                    Id = 1,
                    CityNameAr = "الرياض",
                    CityNameEn = " Elryad ",
                    RegionId = 1

                }


            );
        }
    }

}
