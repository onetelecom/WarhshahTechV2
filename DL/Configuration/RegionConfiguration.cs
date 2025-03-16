using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Configuration
{
   

    class RegionConfiguration : IEntityTypeConfiguration<Region>
    {

        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("Regions");



            builder.HasData
            (

                new Region
                {
                    Id = 1,
                    RegionNameAr = "الرياض",
                    RegionNameEn = " Elryad ",
                    CountryId =1 

                }


            );
        }
    }

}
