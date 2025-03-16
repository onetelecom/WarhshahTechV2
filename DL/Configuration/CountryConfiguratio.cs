using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Configuration
{
    
    class CountryConfiguratio : IEntityTypeConfiguration<Country>
    {

        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");



            builder.HasData
            (

                new Country
                {
                    Id = 1,
                    CountryNameAr = "السعودية",
                    CountryNameEn = " Saudi Arabia "

                }
                 

            );
        }
    }

}
