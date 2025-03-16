using DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Configuration
{
  

        class CountriesWarshahModelConfiguration : IEntityTypeConfiguration<FixedCountryMotor>
        {


            public void Configure(EntityTypeBuilder<FixedCountryMotor> builder)
            {
                builder.ToTable("FixedCountryMotors");



                builder.HasData
                (

                    new FixedCountryMotor
                    {
                        Id = 1,
                        CountryMotorName = "عام"


                    },

                    new FixedCountryMotor
                    {
                        Id = 2,
                        CountryMotorName = "المانى"


                    },

                    new FixedCountryMotor
                    {
                        Id = 3,
                        CountryMotorName = "امريكى"


                    },

                    new FixedCountryMotor
                    {
                        Id = 4,
                        CountryMotorName = "يابانى"


                    },

                    new FixedCountryMotor
                    {
                        Id = 5,
                        CountryMotorName = "كوري"


                    },
new FixedCountryMotor
{
    Id = 6,
    CountryMotorName = "صينى"


},

new FixedCountryMotor
{
    Id = 7,
    CountryMotorName = "أوروبي"


},

new FixedCountryMotor
{
    Id = 8,
    CountryMotorName = "روسى"


}





                );
            }
        }
    }
